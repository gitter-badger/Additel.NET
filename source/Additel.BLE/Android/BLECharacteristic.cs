using Android.Bluetooth;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLECharacteristic
    {
        #region 字段

        private BluetoothGatt _gatt;
        private BLEGATTCallback _callback;
        private BluetoothGattCharacteristic _characteristic;
        #endregion

        #region 属性

        private Guid PlatformUUID
            => _characteristic.Uuid.ToShared();

        private byte[] PlatformValue
            => _characteristic.GetValue();

        private BLECharacteristicProperty PlatformProperties
            => _characteristic.Properties.ToShared();

        private bool PlatformIsNotifying { get; set; }
        #endregion

        #region 构造

        internal BLECharacteristic(BluetoothGatt gatt, BLEGATTCallback callback, BluetoothGattCharacteristic characteristic)
        {
            _gatt = gatt;
            _callback = callback;
            _characteristic = characteristic;

            _callback.CharacteristicChanged += OnCharacteristicChanged;
            _callback.CharacteristicRead += OnCharacteristicRead;
            _callback.CharacteristicWrite += OnCharacteristicWrite;
        }
        #endregion

        #region 方法

        private void OnCharacteristicWrite(object sender, BLEGATTCharacteristicStatusEventArgs e)
        {
            if (!ReferenceEquals(e.Characteristic, _characteristic))
                return;

            if (e.Status != GattStatus.Success)
                RaiseWriteFailed(e.Status.ToShared());
            else
                RaiseValueWritten();
        }

        private void OnCharacteristicRead(object sender, BLEGATTCharacteristicStatusEventArgs e)
        {
            if (!ReferenceEquals(e.Characteristic, _characteristic))
                return;

            if (e.Status != GattStatus.Success)
                RaiseReadFailed(e.Status.ToShared());
            else
                RaiseValueChanged();
        }

        private void OnCharacteristicChanged(object sender, BLEGATTCharacteristicEventArgs e)
        {
            if (!ReferenceEquals(e.Characteristic, _characteristic))
                return;

            RaiseValueChanged();
        }

        private void PlatformRead()
        {
            if (!_gatt.ReadCharacteristic(_characteristic))
                RaiseReadFailed(BLEErrorCode.InternalError);
        }

        private void PlatformWrite(byte[] data)
        {
            if (!_characteristic.SetValue(data))
            {
                RaiseWriteFailed(BLEErrorCode.InternalError);
                return;
            }
            if (!_gatt.WriteCharacteristic(_characteristic))
            {
                RaiseWriteFailed(BLEErrorCode.InternalError);
            }
        }

        private void PlatformSetNotification(bool enabled)
        {
            if (!_gatt.SetCharacteristicNotification(_characteristic, enabled))
            {
                RaiseSetNotificationFailed(BLEErrorCode.InternalError);
                return;
            }
            using (var uuid = Java.Util.UUID.FromString(BLEConstants.CLIENT_CHARACTERISTIC_CONFIGURATION))
            {
                var descriptor = _characteristic.GetDescriptor(uuid);
                if (!descriptor.SetValue(BluetoothGattDescriptor.EnableNotificationValue.ToArray()))
                {
                    RaiseSetNotificationFailed(BLEErrorCode.InternalError);
                    return;
                }
                if (!_gatt.WriteDescriptor(descriptor))
                {
                    RaiseSetNotificationFailed(BLEErrorCode.InternalError);
                    return;
                }
            }

            PlatformIsNotifying = enabled;
            RaiseNotificationSet();
        }

        private void PlatformDiscoverDescriptors()
        {
            var descriptors = _characteristic
                .Descriptors
                .Select(d => new BLEDescriptor(_gatt, _callback, d))
                .ToList();
            RaiseDescriptorsDiscovered(descriptors);
        }

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                _callback.CharacteristicChanged -= OnCharacteristicChanged;
                _callback.CharacteristicRead -= OnCharacteristicRead;
                _callback.CharacteristicWrite -= OnCharacteristicWrite;

                _characteristic.Dispose();
            }
        }
        #endregion
    }
}