using CoreBluetooth;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLEService
    {
        #region 字段

        private WeakReference _peripheralReference;
        private CBService _service;
        #endregion

        #region 属性

        private BLECBPeripheralWrapper PeripheralWrapper
            => _peripheralReference.IsAlive
            ? (BLECBPeripheralWrapper)_peripheralReference.Target
            : throw new ObjectDisposedException(nameof(Peripheral));

        private CBPeripheral Peripheral
            => PeripheralWrapper.Peripheral;

        private Guid PlatformUUID
            => _service.UUID.ToShared();
        #endregion

        #region 构造

        internal BLEService(BLECBPeripheralWrapper peripheral, CBService service)
        {
            _peripheralReference = new WeakReference(peripheral);
            _service = service;

            PeripheralWrapper.DiscoveredCharacteristic += OnDiscoveredCharacteristic;
        }
        #endregion

        #region 方法

        private void OnDiscoveredCharacteristic(object sender, CBServiceEventArgs e)
        {
            if (!ReferenceEquals(e.Service, _service))
                return;

            if (e.Error != null)
            {
                RaiseDiscoverCharacteristicsFailed(e.Error.ToShared());
            }
            else
            {
                var characteristics = _service
                    .Characteristics
                    .Select(c => new BLECharacteristic(PeripheralWrapper, c))
                    .ToList();
                RaiseCharacteristicsDiscovered(characteristics);
            }
        }

        private void PlatformDiscoverCharacteristics()
            => Peripheral.DiscoverCharacteristics(_service);

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                PeripheralWrapper.DiscoveredCharacteristic -= OnDiscoveredCharacteristic;

                _service.Dispose();
            }
        }
        #endregion
    }
}