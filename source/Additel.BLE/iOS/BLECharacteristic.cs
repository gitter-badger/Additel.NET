using CoreBluetooth;
using Foundation;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLECharacteristic
    {
        #region 字段

        private WeakReference _peripheralReference;
        private CBCharacteristic _characteristic;
        #endregion

        #region 属性

        private BLECBPeripheralWrapper PeripheralWrapper
            => _peripheralReference.IsAlive
            ? (BLECBPeripheralWrapper)_peripheralReference.Target
            : throw new ObjectDisposedException(nameof(Peripheral));

        private CBPeripheral Peripheral
            => PeripheralWrapper.Peripheral;

        private Guid PlatformUUID
            => _characteristic.UUID.ToShared();

        private byte[] PlatformValue
            => _characteristic.Value.ToArray();

        private BLECharacteristicProperty PlatformProperties
            => _characteristic.Properties.ToShared();

        private bool PlatformIsNotifying
            => _characteristic.IsNotifying;
        #endregion

        #region 构造

        internal BLECharacteristic(BLECBPeripheralWrapper peripheral, CBCharacteristic characteristic)
        {
            _peripheralReference = new WeakReference(peripheral);
            _characteristic = characteristic;

            PeripheralWrapper.UpdatedCharacterteristicValue += OnUpdatedCharacterteristicValue;
            PeripheralWrapper.WroteCharacteristicValue += OnWroteCharacteristicValue;
            PeripheralWrapper.UpdatedNotificationState += OnUpdatedNotificationState;
            PeripheralWrapper.DiscoveredDescriptor += OnDiscoveredDescriptor;
        }
        #endregion

        #region 方法

        private void OnDiscoveredDescriptor(object sender, CBCharacteristicEventArgs e)
        {
            if (!ReferenceEquals(e.Characteristic, _characteristic))
                return;

            if (e.Error != null)
            {
                RaiseDiscoverDescriptorsFailed(e.Error.ToShared());
            }
            else
            {
                var descriptors = _characteristic
                    .Descriptors
                    .Select(d => new BLEDescriptor(PeripheralWrapper, d))
                    .ToList();
                RaiseDescriptorsDiscovered(descriptors);
            }
        }

        private void OnUpdatedNotificationState(object sender, CBCharacteristicEventArgs e)
        {
            if (!ReferenceEquals(e.Characteristic, _characteristic))
                return;

            if (e.Error != null)
                RaiseSetNotificationFailed(e.Error.ToShared());
            else
                RaiseNotificationSet();
        }

        private void OnWroteCharacteristicValue(object sender, CBCharacteristicEventArgs e)
        {
            if (!ReferenceEquals(e.Characteristic, _characteristic))
                return;

            if (e.Error != null)
                RaiseWriteFailed(e.Error.ToShared());
            else
                RaiseValueWritten();
        }

        private void OnUpdatedCharacterteristicValue(object sender, CBCharacteristicEventArgs e)
        {
            // iOS 中无法区分数据更新来源于 Read 还是 Notify，所以统一触发 ValueChanged 事件
            if (!ReferenceEquals(e.Characteristic, _characteristic))
                return;

            if (e.Error != null)
                RaiseReadFailed(e.Error.ToShared());
            else
                RaiseValueChanged();
        }

        private void PlatformRead()
            => Peripheral.ReadValue(_characteristic);

        private void PlatformWrite(byte[] data)
            => Peripheral.WriteValue(NSData.FromArray(data), _characteristic, CBCharacteristicWriteType.WithResponse);

        private void PlatformSetNotification(bool enabled)
            => Peripheral.SetNotifyValue(enabled, _characteristic);

        private void PlatformDiscoverDescriptors()
            => Peripheral.DiscoverDescriptors(_characteristic);

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                PeripheralWrapper.UpdatedCharacterteristicValue -= OnUpdatedCharacterteristicValue;
                PeripheralWrapper.WroteCharacteristicValue -= OnWroteCharacteristicValue;
                PeripheralWrapper.UpdatedNotificationState -= OnUpdatedNotificationState;
                PeripheralWrapper.DiscoveredDescriptor -= OnDiscoveredDescriptor;

                _characteristic.Dispose();
            }
        }
        #endregion
    }
}