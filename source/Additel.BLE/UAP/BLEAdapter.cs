using Additel.Core;
using System;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;

namespace Additel.BLE
{
    public partial class BLEAdapter
    {
        #region 字段

        private readonly BluetoothLEAdvertisementWatcher _watcher;

        private bool _isScanning;
        #endregion

        #region 属性

        private BLEAdapterState PlatformState
            => throw new NotImplementedInReferenceAssemblyException();

        private bool PlatformIsScanning
        {
            get => _isScanning;
            set
            {
                if (_isScanning == value)
                    return;

                _isScanning = value;

                RaiseScanStateChanged();
            }
        }
        #endregion

        #region 构造

        public BLEAdapter(BluetoothLEAdvertisementWatcher watcher)
        {
            _watcher = watcher;
        }
        #endregion

        #region 方法

        private void PlatformStartScan(Guid[] uuids)
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformStopScan()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDispose(bool disposing)
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
