using CoreBluetooth;
using Foundation;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLEDevice
    {
        #region 字段

        private WeakReference _managerReference;
        private BLECBPeripheralWrapper _peripheralWrapper;
        #endregion

        #region 属性

        private BLECBCentralManagerWrapper ManagerWrapper
            => _managerReference.IsAlive
            ? (BLECBCentralManagerWrapper)_managerReference.Target
            : throw new ObjectDisposedException(nameof(Manager));

        private CBCentralManager Manager
            => ManagerWrapper.Manager;

        private CBPeripheral Peripheral
            => _peripheralWrapper.Peripheral;

        private Guid PlatformUUID
            => Peripheral.Identifier.ToShared();

        private string PlatformName
            => Peripheral.Name;

        private BLEDeviceState PlatformState
            => Peripheral.State.ToShared();
        #endregion

        #region 构造

        internal BLEDevice(BLECBCentralManagerWrapper manager, CBPeripheral peripheral)
        {
            _managerReference = new WeakReference(manager);
            _peripheralWrapper = new BLECBPeripheralWrapper(peripheral);

            ManagerWrapper.ConnectedPeripheral += OnConnectedPeripheral;
            ManagerWrapper.DisconnectedPeripheral += OnDisconnectedPeripheral;
            ManagerWrapper.FailedToConnectPeripheral += OnFailedToConnectPeripheral;

            _peripheralWrapper.RSSIRead += OnRssiRead;
            _peripheralWrapper.DiscoveredService += OnDiscoveredService;
        }
        #endregion

        #region 方法

        private void OnDiscoveredService(object sender, NSErrorEventArgs e)
        {
            if (e.Error != null)
            {
                RaiseDiscoverServicesFailed(e.Error.ToShared());
            }
            else
            {
                var services = Peripheral
                    .Services
                    .Select(s => new BLEService(_peripheralWrapper, s))
                    .ToList();
                RaiseServicesDiscovered(services);
            }
        }

        private void OnRssiRead(object sender, CBRssiEventArgs e)
        {
            if (e.Error != null)
                RaiseReadRSSIFailed(e.Error.ToShared());
            else
                RaiseRSSIRead(e.Rssi.Int32Value);
        }

        private void OnFailedToConnectPeripheral(object sender, CBPeripheralErrorEventArgs e)
        {
            if (!ReferenceEquals(e.Peripheral, Peripheral))
                return;

            RaiseConnectFailed(e.Error.ToShared());
        }

        private void OnDisconnectedPeripheral(object sender, CBPeripheralErrorEventArgs e)
        {
            if (!ReferenceEquals(e.Peripheral, Peripheral))
                return;

            if (e.Error != null)
                RaiseConnectionLost(e.Error.ToShared());
            else
                RaiseDisconnected();
        }

        private void OnConnectedPeripheral(object sender, CBPeripheralEventArgs e)
        {
            if (!ReferenceEquals(e.Peripheral, Peripheral))
                return;

            RaiseConnected();
        }

        private void PlatformConnect()
            => Manager.ConnectPeripheral(Peripheral);

        private void PlatformDisconnect()
            => Manager.CancelPeripheralConnection(Peripheral);

        private void PlatformReadRSSI()
            => Peripheral.ReadRSSI();

        private void PlatformDiscoverServices()
            => Peripheral.DiscoverServices();

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                ManagerWrapper.ConnectedPeripheral -= OnConnectedPeripheral;
                ManagerWrapper.DisconnectedPeripheral -= OnDisconnectedPeripheral;
                ManagerWrapper.FailedToConnectPeripheral -= OnFailedToConnectPeripheral;

                _peripheralWrapper.RSSIRead -= OnRssiRead;
                _peripheralWrapper.DiscoveredService -= OnDiscoveredService;

                _peripheralWrapper.Dispose();
            }
        }
        #endregion
    }
}