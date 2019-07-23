using CoreBluetooth;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLEAdapter
    {
        #region 字段

        private BLECBCentralManagerWrapper _managerWrapper;
        #endregion

        #region 属性

        private CBCentralManager Manager
            => _managerWrapper.Manager;

        private BLEAdapterState PlatformState
            => Manager.State.ToShared();

        private bool PlatformIsScanning
            => Manager.IsScanning;
        #endregion

        #region 构造

        public BLEAdapter(CBCentralManager manager)
            : this()
        {
            _managerWrapper = new BLECBCentralManagerWrapper(manager);

            _managerWrapper.StateChanged += OnStateChanged;
            _managerWrapper.ScanStateChanged += OnScanStateChanged;
            _managerWrapper.DiscoveredPeripheral += OnDiscoveredPeripheral;
        }
        #endregion

        #region 方法

        private void OnDiscoveredPeripheral(object sender, CBDiscoveredPeripheralEventArgs e)
        {
            var device = new BLEDevice(_managerWrapper, e.Peripheral);
            var rssi = e.RSSI.Int32Value;
            var advertisements = BLEAdvertisement.Parse(e.AdvertisementData);

            RaiseDeviceDiscovered(device, rssi, advertisements);
        }

        private void OnScanStateChanged(object sender, EventArgs e)
        {
            RaiseScanStateChanged();
        }

        private void OnStateChanged(object sender, BLECBCentralManagerStateEventArgs e)
        {
            RaiseStateChanged(e.OldState.ToShared(), e.NewState.ToShared());
        }

        private void PlatformStartScan(Guid[] uuids)
        {
            var peripheralUuids = uuids?.Select(u => CBUUID.FromString(u.ToString())).ToArray();

            Manager.ScanForPeripherals(peripheralUuids);
        }

        private void PlatformStopScan()
            => Manager.StopScan();

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                _managerWrapper.StateChanged -= OnStateChanged;
                _managerWrapper.ScanStateChanged -= OnScanStateChanged;
                _managerWrapper.DiscoveredPeripheral -= OnDiscoveredPeripheral;

                _managerWrapper.Dispose();
            }
        }
        #endregion
    }
}