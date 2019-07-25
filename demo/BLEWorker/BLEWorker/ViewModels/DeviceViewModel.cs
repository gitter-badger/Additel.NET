using Additel.BLE;
using Additel.Core;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BLEWorker.ViewModels
{
    public class DeviceViewModel : BindableBase, IDisposable
    {
        public delegate void ConnectionStateHandler(object sender, bool state);
        public delegate void CommunicationQualityHandler(object sender, string state, string value);
        public delegate void ErrorHandler(object sender, string error);
        private delegate void SCPIHandler(string state, string value);

        public event ConnectionStateHandler ConnectionStateChanged;
        public event CommunicationQualityHandler CommunicationQualityReceived;
        public event ErrorHandler ErrorOccured;

        private readonly BLEDevice _device;
        private readonly IDictionary<string, SCPIHandler> _handlers;
        private ConcurrentQueue<byte[]> _values;
        private readonly SemaphoreSlim _codeSlim;
        private readonly SemaphoreSlim _writeSlim;

        private BLECharacteristic _characteristic;

        public Guid UUID
            => _device.UUID;

        public string Name
            => _device.Name;

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                if (!SetProperty(ref _isConnected, value))
                    return;

                ConnectionStateChanged?.Invoke(this, value);
            }
        }

        public DeviceViewModel(BLEDevice device)
        {
            _device = device;
            _device.ConnectionLost += OnConnectionLost;
            _handlers = BuildHandlers();
            _values = new ConcurrentQueue<byte[]>();
            _codeSlim = new SemaphoreSlim(1, 1);
            _writeSlim = new SemaphoreSlim(1, 1);
        }

        private IDictionary<string, SCPIHandler> BuildHandlers()
        {
            var actions = new Dictionary<string, SCPIHandler>()
            {
                [Config.CODE] = OnCodeReceived,
                [Config.DIAG_COMM_QUAL_ECHO] = OnCommunicationQualityReceived
            };

            return actions;
        }

        private void OnCommunicationQualityReceived(string state, string value)
        {
            CommunicationQualityReceived?.Invoke(this, state, value);
        }

        private async void OnCodeReceived(string state, string value)
        {
            try
            {
                // 保证握手指令只发送一次
                await _codeSlim.WaitAsync();
                if (IsConnected)
                    return;

                await WriteAsync("@BLE Worker");
                IsConnected = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _codeSlim?.Release();
            }
        }

        public async Task WriteAsync(string value)
        {
            try
            {
                // 保证指令间同步发送
                await _writeSlim.WaitAsync();
                var data = Encoding.UTF8.GetBytes($"{value}\r\n");
                // 大于20字节需要分包
                var count = data.Length / 20;
                var length = data.Length % 20;
                for (int i = 0; i < count; i++)
                {
                    var large = new byte[20];
                    Array.Copy(data, i * 20, large, 0, 20);
                    await _characteristic.WriteAsync(large);
                }
                if (length > 0)
                {
                    var small = new byte[length];
                    Array.Copy(data, count * 20, small, 0, length);
                    await _characteristic.WriteAsync(small);
                }
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(this, $"发送失败 - {ex.Message}");
            }
            finally
            {
                _writeSlim?.Release();
            }
        }

        private void OnConnectionLost(object sender, BLEErrorEventArgs e)
        {
            if (IsConnected) IsConnected = false;
            if (_characteristic != null)
            {
                _characteristic.ValueChanged -= OnValueChanged;
                _characteristic.Dispose();
                _characteristic = null;
            }
            if (_values.Count > 0) _values = new ConcurrentQueue<byte[]>();
        }

        private DelegateCommand _connectCommand;
        public DelegateCommand ConnectCommand =>
            _connectCommand ?? (_connectCommand = new DelegateCommand(ExecuteConnectCommand, CanExecuteConnectCommand).ObservesProperty(() => IsConnected));

        private bool CanExecuteConnectCommand()
            => !IsConnected;

        async void ExecuteConnectCommand()
        {
            try
            {
                if (_device.State == BLEDeviceState.Connected)
                    return;

                // 连接BLE
                await _device.ConnectAsync();
                // 获取设备中的服务
                var services = await _device.DiscoverServicesAsync();
                // 自定义服务
                var s1 = services.First(s => s.UUID == Guid.Parse(Config.ADT_S1_UUID));
                // 获取自定义服务中的特征值
                var characteristics1 = await s1.DiscoverCharacteristicsAsync();
                // 存储透传服务UUID的特征值
                var c3 = characteristics1.First(c => c.UUID == Guid.Parse(Config.ADT_C3_UUID));
                // 存储透传特征值UUID的特征值
                var c4 = characteristics1.First(c => c.UUID == Guid.Parse(Config.ADT_C4_UUID));
                // 透传服务UUID
                var su = (await c3.ReadAsync()).GetHexString().Replace("-", string.Empty);
                // 储透传特征值UUID
                var cu = (await c4.ReadAsync()).GetHexString().Replace("-", string.Empty);
                // 透传服务
                //var uuid1 = "00035B0358E607DD021A08123A000300";
                //var uuid2 = "00035B0358E607DD021A08123A000301";
                var s2 = services.First(s => s.UUID == Guid.Parse(su));
                // 获取透传服务中的特征值
                var characteristics2 = await s2.DiscoverCharacteristicsAsync();
                // 透传特征值
                _characteristic = characteristics2.First(c => c.UUID == Guid.Parse(cu));
                _characteristic.ValueChanged += OnValueChanged;
                // 打开通知
                await _characteristic.SetNotificationAsync(true);
                // 开始处理消息队列
                var task = Task.Run(() => StartListen());
                // 清理
                //var others = characteristics2.Where(c => c.UUID != _characteristic.UUID).ToList();
                //foreach (var item in others) item.Dispose();
                //foreach (var item in characteristics1) item.Dispose();
                //foreach (var item in services) item.Dispose();
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(this, $"连接失败 - {ex.Message}");
            }
        }

        private void StartListen()
        {
            var bufferList = new List<byte>();
            var pattern0 = @"(?<MESSAGES>\S+\r\n)+(?<SEGMENT>\S*)";
            var pattern1 = @"(?i)(?<CMD>CODE\?)|(?<CMD>\S+:ECHO\??),(?<RESULT>OK|ERROR),?(?<CONTENT>\S*)";
            while (_device.State == BLEDeviceState.Connected)
            {
                try
                {
                    while (_values.TryDequeue(out byte[] value))
                    {
                        bufferList.AddRange(value);
                        var input0 = Encoding.UTF8.GetString(bufferList.ToArray());
                        var match0 = Regex.Match(input0, pattern0);
                        if (!match0.Success)
                            continue;

                        bufferList.Clear();

                        var captures = match0.Groups["MESSAGES"].Captures;
                        var segment = match0.Groups["SEGMENT"];

                        foreach (Capture capture in captures)
                        {
                            var match1 = Regex.Match(capture.Value, pattern1);
                            if (!match1.Success)
                                continue;

                            var cmd = match1.Groups["CMD"].Value;
                            var result = match1.Groups["RESULT"].Value;
                            var content = match1.Groups["CONTENT"].Value;

                            if (!_handlers.ContainsKey(cmd))
                                continue;

                            _handlers[cmd]?.BeginInvoke(result, content, null, null);
                        }

                        if (segment.Length > 0)
                        {
                            bufferList.AddRange(Encoding.ASCII.GetBytes(segment.Value));
                        }
                    }

                    // 休息 100ms
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            _values.Enqueue(_characteristic.Value);
        }

        private DelegateCommand _disconnectCommand;
        public DelegateCommand DisconnectCommand =>
            _disconnectCommand ?? (_disconnectCommand = new DelegateCommand(ExecuteDisconnectCommand, CanExecuteDisconnectCommand).ObservesProperty(() => IsConnected));

        private bool CanExecuteDisconnectCommand()
            => IsConnected;

        async void ExecuteDisconnectCommand()
        {
            await _device.DisconnectAsync();

            IsConnected = false;

            _characteristic.ValueChanged -= OnValueChanged;
            _characteristic.Dispose();
            _characteristic = null;

            if (_values.Count > 0) _values = new ConcurrentQueue<byte[]>();
        }

        private DelegateCommand<string> _writeCommand;
        public DelegateCommand<string> WriteCommand =>
            _writeCommand ?? (_writeCommand = new DelegateCommand<string>(ExecuteWriteCommand, CanExecuteWriteCommand).ObservesProperty(() => IsConnected));

        private bool CanExecuteWriteCommand(string arg)
            => IsConnected;

        async void ExecuteWriteCommand(string value)
        {
            await WriteAsync(value);
        }

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    // 注销事件
                    _characteristic.ValueChanged -= OnValueChanged;
                    _device.ConnectionLost -= OnConnectionLost;
                    // 特征值
                    _characteristic.Dispose();
                    // 设备
                    _device.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~DeviceViewModel()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
