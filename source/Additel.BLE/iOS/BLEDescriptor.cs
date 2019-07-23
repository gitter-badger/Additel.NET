using Additel.Core;
using CoreBluetooth;
using Foundation;
using System;

namespace Additel.BLE
{
    public partial class BLEDescriptor
    {
        #region 字段

        private WeakReference _peripheralReference;
        private CBDescriptor _descriptor;
        #endregion

        #region 属性

        private BLECBPeripheralWrapper PeripheralWrapper
            => _peripheralReference.IsAlive
            ? (BLECBPeripheralWrapper)_peripheralReference.Target
            : throw new ObjectDisposedException(nameof(Peripheral));

        private CBPeripheral Peripheral
            => PeripheralWrapper.Peripheral;

        private Guid PlatformUUID
            => _descriptor.UUID.ToShared();

        private byte[] PlatformValue
            => _descriptor.Value.ToArray();
        #endregion

        #region 构造

        internal BLEDescriptor(BLECBPeripheralWrapper peripheral, CBDescriptor descriptor)
        {
            _peripheralReference = new WeakReference(peripheral);
            _descriptor = descriptor;

            PeripheralWrapper.UpdatedValue += OnUpdatedValue;
            PeripheralWrapper.WroteDescriptorValue += OnWroteDescriptorValue;
        }
        #endregion

        #region 方法

        private void OnWroteDescriptorValue(object sender, CBDescriptorEventArgs e)
        {
            if (!ReferenceEquals(e.Descriptor, _descriptor))
                return;

            if (e.Error != null)
                RaiseWriteFailed(e.Error.ToShared());
            else
                RaiseValueWritten();
        }

        private void OnUpdatedValue(object sender, CBDescriptorEventArgs e)
        {
            if (!ReferenceEquals(e.Descriptor, _descriptor))
                return;

            if (e.Error != null)
                RaiseReadFailed(e.Error.ToShared());
            else
                RaiseValueChanged();
        }

        private void PlatformRead()
            => Peripheral.ReadValue(_descriptor);

        private void PlatformWrite(byte[] data)
            => Peripheral.WriteValue(NSData.FromArray(data), _descriptor);

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                PeripheralWrapper.UpdatedValue -= OnUpdatedValue;
                PeripheralWrapper.WroteDescriptorValue -= OnWroteDescriptorValue;

                _descriptor.Dispose();
            }
        }
        #endregion
    }
}