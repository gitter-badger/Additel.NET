using Additel.Core.WeakEvent;
using CoreBluetooth;
using Foundation;
using System;

namespace Additel.BLE
{
    internal class BLECBPeripheralWrapper : IDisposable
    {
        #region 事件

        public event EventHandler<CBPeripheralOpenL2CapChannelEventArgs> DidOpenL2CapChannel
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBServiceEventArgs> DiscoveredCharacteristic
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBCharacteristicEventArgs> DiscoveredDescriptor
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBServiceEventArgs> DiscoveredIncludedService
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<NSErrorEventArgs> DiscoveredService
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler InvalidatedService
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler IsReadyToSendWriteWithoutResponse
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBPeripheralServicesEventArgs> ModifiedServices
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBRssiEventArgs> RSSIRead
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<NSErrorEventArgs> RSSIUpdated
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBCharacteristicEventArgs> UpdatedCharacterteristicValue
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler UpdatedName
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBCharacteristicEventArgs> UpdatedNotificationState
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBDescriptorEventArgs> UpdatedValue
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBCharacteristicEventArgs> WroteCharacteristicValue
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<CBDescriptorEventArgs> WroteDescriptorValue
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        #endregion

        #region 字段

        private WeakEventSource _wes;
        #endregion

        #region 属性

        public CBPeripheral Peripheral { get; }
        #endregion

        #region 构造

        public BLECBPeripheralWrapper(CBPeripheral peripheral)
        {
            _wes = new WeakEventSource();

            Peripheral = peripheral;

            Peripheral.DidOpenL2CapChannel += OnDidOpenL2CapChannel;
            Peripheral.DiscoveredCharacteristic += OnDiscoveredCharacteristic;
            Peripheral.DiscoveredDescriptor += OnDiscoveredDescriptor;
            Peripheral.DiscoveredIncludedService += OnDiscoveredIncludedService;
            Peripheral.DiscoveredService += OnDiscoveredService;
            Peripheral.InvalidatedService += OnInvalidatedService;
            Peripheral.IsReadyToSendWriteWithoutResponse += OnIsReadyToSendWriteWithoutResponse;
            Peripheral.ModifiedServices += OnModifiedServices;
            Peripheral.RssiRead += OnRssiRead;
            Peripheral.RssiUpdated += OnRssiUpdated;
            Peripheral.UpdatedCharacterteristicValue += OnUpdatedCharacterteristicValue;
            Peripheral.UpdatedName += OnUpdatedName;
            Peripheral.UpdatedNotificationState += OnUpdatedNotificationState;
            Peripheral.UpdatedValue += OnUpdatedValue;
            Peripheral.WroteCharacteristicValue += OnWroteCharacteristicValue;
            Peripheral.WroteDescriptorValue += OnWroteDescriptorValue;
        }
        #endregion

        #region 方法

        private void OnWroteDescriptorValue(object sender, CBDescriptorEventArgs e)
            => _wes.RaiseEvent(nameof(WroteDescriptorValue), sender, e);

        private void OnWroteCharacteristicValue(object sender, CBCharacteristicEventArgs e)
            => _wes.RaiseEvent(nameof(WroteCharacteristicValue), sender, e);

        private void OnUpdatedValue(object sender, CBDescriptorEventArgs e)
            => _wes.RaiseEvent(nameof(UpdatedValue), sender, e);

        private void OnUpdatedNotificationState(object sender, CBCharacteristicEventArgs e)
            => _wes.RaiseEvent(nameof(UpdatedNotificationState), sender, e);

        private void OnUpdatedName(object sender, EventArgs e)
            => _wes.RaiseEvent(nameof(UpdatedName), sender, e);

        private void OnUpdatedCharacterteristicValue(object sender, CBCharacteristicEventArgs e)
            => _wes.RaiseEvent(nameof(UpdatedCharacterteristicValue), sender, e);

        private void OnRssiUpdated(object sender, NSErrorEventArgs e)
            => _wes.RaiseEvent(nameof(RSSIUpdated), sender, e);

        private void OnRssiRead(object sender, CBRssiEventArgs e)
            => _wes.RaiseEvent(nameof(RSSIRead), sender, e);

        private void OnModifiedServices(object sender, CBPeripheralServicesEventArgs e)
            => _wes.RaiseEvent(nameof(ModifiedServices), sender, e);

        private void OnIsReadyToSendWriteWithoutResponse(object sender, EventArgs e)
            => _wes.RaiseEvent(nameof(IsReadyToSendWriteWithoutResponse), sender, e);

        private void OnInvalidatedService(object sender, EventArgs e)
            => _wes.RaiseEvent(nameof(InvalidatedService), sender, e);

        private void OnDiscoveredService(object sender, NSErrorEventArgs e)
            => _wes.RaiseEvent(nameof(DiscoveredService), sender, e);

        private void OnDiscoveredIncludedService(object sender, CBServiceEventArgs e)
            => _wes.RaiseEvent(nameof(DiscoveredIncludedService), sender, e);

        private void OnDiscoveredDescriptor(object sender, CBCharacteristicEventArgs e)
            => _wes.RaiseEvent(nameof(DiscoveredDescriptor), sender, e);

        private void OnDiscoveredCharacteristic(object sender, CBServiceEventArgs e)
            => _wes.RaiseEvent(nameof(DiscoveredCharacteristic), sender, e);

        private void OnDidOpenL2CapChannel(object sender, CBPeripheralOpenL2CapChannelEventArgs e)
            => _wes.RaiseEvent(nameof(DidOpenL2CapChannel), sender, e);
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
                    Peripheral.DidOpenL2CapChannel -= OnDidOpenL2CapChannel;
                    Peripheral.DiscoveredCharacteristic -= OnDiscoveredCharacteristic;
                    Peripheral.DiscoveredDescriptor -= OnDiscoveredDescriptor;
                    Peripheral.DiscoveredIncludedService -= OnDiscoveredIncludedService;
                    Peripheral.DiscoveredService -= OnDiscoveredService;
                    Peripheral.InvalidatedService -= OnInvalidatedService;
                    Peripheral.IsReadyToSendWriteWithoutResponse -= OnIsReadyToSendWriteWithoutResponse;
                    Peripheral.ModifiedServices -= OnModifiedServices;
                    Peripheral.RssiRead -= OnRssiRead;
                    Peripheral.RssiUpdated -= OnRssiUpdated;
                    Peripheral.UpdatedCharacterteristicValue -= OnUpdatedCharacterteristicValue;
                    Peripheral.UpdatedName -= OnUpdatedName;
                    Peripheral.UpdatedNotificationState -= OnUpdatedNotificationState;
                    Peripheral.UpdatedValue -= OnUpdatedValue;
                    Peripheral.WroteCharacteristicValue -= OnWroteCharacteristicValue;
                    Peripheral.WroteDescriptorValue -= OnWroteDescriptorValue;

                    Peripheral.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BLECBPeripheralWrapper() {
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