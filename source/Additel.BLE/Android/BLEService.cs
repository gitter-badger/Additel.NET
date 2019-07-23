using Android.Bluetooth;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLEService
    {
        #region 字段

        private BluetoothGatt _gatt;
        private BLEGATTCallback _callback;
        private BluetoothGattService _service;
        #endregion

        #region 属性

        private Guid PlatformUUID
            => _service.Uuid.ToShared();
        #endregion

        #region 构造

        internal BLEService(BluetoothGatt gatt, BLEGATTCallback callback, BluetoothGattService service)
            : this()
        {
            _gatt = gatt;
            _callback = callback;
            _service = service;
        }
        #endregion

        #region 方法

        private void PlatformDiscoverCharacteristics()
        {
            var characteristics = _service
                .Characteristics
                .Select(c => new BLECharacteristic(_gatt, _callback, c))
                .ToList();
            RaiseCharacteristicsDiscovered(characteristics);
        }

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
        }
        #endregion
    }
}