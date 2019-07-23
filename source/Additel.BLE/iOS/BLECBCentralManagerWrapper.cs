using Additel.Core.WeakEvent;
using CoreBluetooth;
using Foundation;
using System;

namespace Additel.BLE
{
    internal class BLECBCentralManagerWrapper : IDisposable
    {
        #region 事件

        public event EventHandler<CBPeripheralEventArgs> ConnectedPeripheral
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBPeripheralErrorEventArgs> DisconnectedPeripheral
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBDiscoveredPeripheralEventArgs> DiscoveredPeripheral
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBPeripheralErrorEventArgs> FailedToConnectPeripheral
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBPeripheralsEventArgs> RetrievedConnectedPeripherals
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBPeripheralsEventArgs> RetrievedPeripherals
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler UpdatedState
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBWillRestoreEventArgs> WillRestoreState
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLECBCentralManagerStateEventArgs> StateChanged
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler ScanStateChanged
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        #endregion

        #region 字段

        private IDisposable _o1;
        private IDisposable _o2;
        private WeakEventSource _wes;
        #endregion

        #region 属性

        public CBCentralManager Manager { get; }
        #endregion

        #region 构造

        public BLECBCentralManagerWrapper(CBCentralManager manager)
        {
            _wes = new WeakEventSource();

            Manager = manager;

            _o1 = Manager.AddObserver(
                "state",
                NSKeyValueObservingOptions.OldNew,
                change =>
                {
                    using (var ov = (NSNumber)change.OldValue)
                    using (var nv = (NSNumber)change.NewValue)
                    {
                        var oldState = (CBCentralManagerState)ov.Int32Value;
                        var newState = (CBCentralManagerState)nv.Int32Value;

                        _wes.RaiseEvent(nameof(StateChanged), Manager, new BLECBCentralManagerStateEventArgs(oldState, newState));
                    }
                });

            _o2 = manager.AddObserver(
                "isScanning",
                NSKeyValueObservingOptions.New,
                change => _wes.RaiseEvent(nameof(ScanStateChanged), Manager, EventArgs.Empty));

            Manager.ConnectedPeripheral += OnConnectedPeripheral;
            Manager.DisconnectedPeripheral += OnDisconnectedPeripheral;
            Manager.DiscoveredPeripheral += OnDiscoveredPeripheral;
            Manager.FailedToConnectPeripheral += OnFailedToConnectPeripheral;
            Manager.RetrievedConnectedPeripherals += OnRetrievedConnectedPeripherals;
            Manager.RetrievedPeripherals += OnRetrievedPeripherals;
            Manager.UpdatedState += OnUpdatedState;
            Manager.WillRestoreState += OnWillRestoreState;
        }
        #endregion

        #region 方法

        private void OnWillRestoreState(object sender, CBWillRestoreEventArgs e)
            => _wes.RaiseEvent(nameof(WillRestoreState), sender, e);

        private void OnUpdatedState(object sender, EventArgs e)
            => _wes.RaiseEvent(nameof(UpdatedState), sender, e);

        private void OnRetrievedPeripherals(object sender, CBPeripheralsEventArgs e)
            => _wes.RaiseEvent(nameof(RetrievedPeripherals), sender, e);

        private void OnRetrievedConnectedPeripherals(object sender, CBPeripheralsEventArgs e)
            => _wes.RaiseEvent(nameof(RetrievedConnectedPeripherals), sender, e);

        private void OnFailedToConnectPeripheral(object sender, CBPeripheralErrorEventArgs e)
            => _wes.RaiseEvent(nameof(FailedToConnectPeripheral), sender, e);

        private void OnDiscoveredPeripheral(object sender, CBDiscoveredPeripheralEventArgs e)
            => _wes.RaiseEvent(nameof(DiscoveredPeripheral), sender, e);

        private void OnDisconnectedPeripheral(object sender, CBPeripheralErrorEventArgs e)
            => _wes.RaiseEvent(nameof(DisconnectedPeripheral), sender, e);

        private void OnConnectedPeripheral(object sender, CBPeripheralEventArgs e)
            => _wes.RaiseEvent(nameof(ConnectedPeripheral), sender, e);
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
                    Manager.ConnectedPeripheral -= OnConnectedPeripheral;
                    Manager.DisconnectedPeripheral -= OnDisconnectedPeripheral;
                    Manager.DiscoveredPeripheral -= OnDiscoveredPeripheral;
                    Manager.FailedToConnectPeripheral -= OnFailedToConnectPeripheral;
                    Manager.RetrievedConnectedPeripherals -= OnRetrievedConnectedPeripherals;
                    Manager.RetrievedPeripherals -= OnRetrievedPeripherals;
                    Manager.UpdatedState -= OnUpdatedState;
                    Manager.WillRestoreState -= OnWillRestoreState;

                    _o1.Dispose();
                    _o2.Dispose();
                    Manager.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BLECBCentralManagerWrapper() {
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