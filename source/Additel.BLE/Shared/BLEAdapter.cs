using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    public partial class BLEAdapter : IDisposable
    {
        #region 事件

        public event EventHandler<BLEAdapterStateEventArgs> StateChanged;
        public event EventHandler ScanStateChanged;
        public event EventHandler<BLEErrorEventArgs> ScanFailed;
        public event EventHandler<BLEDeviceEventArgs> DeviceDiscovered;
        #endregion

        #region 属性

        public BLEAdapterState State
            => PlatformState;

        public bool IsScanning
            => PlatformIsScanning;
        #endregion

        #region 构造

        private BLEAdapter() { }
        #endregion

        #region 方法

        public void StartScan(params Guid[] uuids)
            => PlatformStartScan(uuids);

        public void StopScan()
            => PlatformStopScan();

        private void RaiseStateChanged(BLEAdapterState oldState, BLEAdapterState newState)
            => StateChanged?.Invoke(this, new BLEAdapterStateEventArgs(oldState, newState));

        private void RaiseScanStateChanged()
            => ScanStateChanged?.Invoke(this, EventArgs.Empty);

        private void RaiseScanFailed(BLEErrorCode code)
            => ScanFailed?.Invoke(this, new BLEErrorEventArgs(code));

        private void RaiseDeviceDiscovered(BLEDevice device, int rssi, IList<BLEAdvertisement> advertisements)
            => DeviceDiscovered?.Invoke(this, new BLEDeviceEventArgs(device, rssi, advertisements));
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
                    if (IsScanning)
                        StopScan();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                PlatformDispose(disposing);

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BLEAdapter() {
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
