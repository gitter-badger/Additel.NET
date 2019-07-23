using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    public partial class BLEDescriptor
    {
        #region 字段

        private BluetoothGatt _gatt;
        private BLEGATTCallback _callback;
        private BluetoothGattDescriptor _descriptor;
        #endregion

        #region 属性

        private Guid PlatformUUID
            => _descriptor.Uuid.ToShared();

        private byte[] PlatformValue
            => _descriptor.GetValue();
        #endregion

        #region 构造

        internal BLEDescriptor(BluetoothGatt gatt, BLEGATTCallback callback, BluetoothGattDescriptor descriptor)
        {
            _gatt = gatt;
            _callback = callback;
            _descriptor = descriptor;

            _callback.DescriptorRead += OnDescriptorRead;
            _callback.DescriptorWrite += OnDescriptorWrite;
        }
        #endregion

        #region 方法

        private void OnDescriptorWrite(object sender, BLEGATTDescriptorStatusEventArgs e)
        {
            if (!ReferenceEquals(e.Descriptor, _descriptor))
                return;

            if (e.Status != GattStatus.Success)
                RaiseWriteFailed(e.Status.ToShared());
            else
                RaiseValueWritten();
        }

        private void OnDescriptorRead(object sender, BLEGATTDescriptorStatusEventArgs e)
        {
            if (!ReferenceEquals(e.Descriptor, _descriptor))
                return;

            if (e.Status != GattStatus.Success)
                RaiseReadFailed(e.Status.ToShared());
            else
                RaiseValueChanged();
        }

        private void PlatformRead()
        {
            if (!_gatt.ReadDescriptor(_descriptor))
                RaiseReadFailed(BLEErrorCode.InternalError);
        }

        private void PlatformWrite(byte[] data)
        {
            if (!_descriptor.SetValue(data))
            {
                RaiseWriteFailed(BLEErrorCode.InternalError);
                return;
            }
            if (!_gatt.WriteDescriptor(_descriptor))
            {
                RaiseWriteFailed(BLEErrorCode.InternalError);
            }
        }

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                _callback.DescriptorRead -= OnDescriptorRead;
                _callback.DescriptorWrite -= OnDescriptorWrite;

                _descriptor.Dispose();
            }
        }
        #endregion
    }
}