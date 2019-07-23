using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    public partial class BLEDevice : IDisposable
    {
        #region 事件

        public event EventHandler Connected;
        public event EventHandler<BLEErrorEventArgs> ConnectFailed;
        public event EventHandler Disconnected;
        public event EventHandler<BLEErrorEventArgs> ConnectionLost;
        public event EventHandler<BLERSSIEventArgs> RSSIRead;
        public event EventHandler<BLEErrorEventArgs> ReadRSSIFailed;
        public event EventHandler<BLEServicesEventArgs> ServicesDiscovered;
        public event EventHandler<BLEErrorEventArgs> DiscoverServicesFailed;
        #endregion

        #region 属性

        public Guid UUID
            => PlatformUUID;

        public string Name
            => PlatformName;

        public BLEDeviceState State
            => PlatformState;
        #endregion

        #region 构造

        private BLEDevice() { }
        #endregion

        #region 方法

        public void Connect()
            => PlatformConnect();

        public void Disconnect()
            => PlatformDisconnect();

        /// <summary>
        /// 读取蓝牙设备信号强度（Andorid只有在连接状态下才可以读取）
        /// </summary>
        public void ReadRSSI()
            => PlatformReadRSSI();

        public void DiscoverServices()
            => PlatformDiscoverServices();

        private void RaiseConnected()
            => Connected?.Invoke(this, EventArgs.Empty);

        private void RaiseConnectFailed(BLEErrorCode code)
            => ConnectFailed?.Invoke(this, new BLEErrorEventArgs(code));

        private void RaiseDisconnected()
            => Disconnected?.Invoke(this, EventArgs.Empty);

        private void RaiseConnectionLost(BLEErrorCode code)
            => ConnectionLost?.Invoke(this, new BLEErrorEventArgs(code));

        private void RaiseRSSIRead(int rssi)
            => RSSIRead?.Invoke(this, new BLERSSIEventArgs(rssi));

        private void RaiseReadRSSIFailed(BLEErrorCode code)
            => ReadRSSIFailed?.Invoke(this, new BLEErrorEventArgs(code));

        private void RaiseServicesDiscovered(IList<BLEService> services)
            => ServicesDiscovered?.Invoke(this, new BLEServicesEventArgs(services));

        private void RaiseDiscoverServicesFailed(BLEErrorCode code)
            => DiscoverServicesFailed?.Invoke(this, new BLEErrorEventArgs(code));
        #endregion

        #region IDisposable

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。

                    if (State != BLEDeviceState.Disconnected)
                        Disconnect();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                PlatformDispose(disposing);

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BLEDevice() {
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
