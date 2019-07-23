using Additel.Core.WeakEvent;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Runtime;
using System;

namespace Additel.BLE
{
    internal class BLEGATTCallback : BluetoothGattCallback
    {
        #region 字段

        private readonly WeakEventSource _wes;
        #endregion

        #region 事件

        public event EventHandler<BLEGATTCharacteristicEventArgs> CharacteristicChanged
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTCharacteristicStatusEventArgs> CharacteristicRead
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTCharacteristicStatusEventArgs> CharacteristicWrite
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTProfileStateStatusEventArgs> ConnectionStateChanged
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTDescriptorStatusEventArgs> DescriptorRead
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTDescriptorStatusEventArgs> DescriptorWrite
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTMTUStatusEventArgs> MTUChanged
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTPHYStatusEventArgs> PHYRead
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTPHYStatusEventArgs> PHYUpdate
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTRSSIStatusEventArgs> ReadRemoteRSSI
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTStatusEventArgs> ReliableWriteCompleted
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        public event EventHandler<BLEGATTStatusEventArgs> ServicesDiscovered
        {
            add { _wes.AddEventHandler(value); }
            remove { _wes.RemoveEventHandler(value); }
        }
        #endregion

        #region 构造

        public BLEGATTCallback()
        {
            _wes = new WeakEventSource();
        }
        #endregion

        #region BluetoothGattCallback

        public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic)
        {
            base.OnCharacteristicChanged(gatt, characteristic);

            var args = new BLEGATTCharacteristicEventArgs(gatt, characteristic);
            _wes.RaiseEvent(nameof(CharacteristicChanged), this, args);
        }

        public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, [GeneratedEnum] GattStatus status)
        {
            base.OnCharacteristicRead(gatt, characteristic, status);

            var args = new BLEGATTCharacteristicStatusEventArgs(gatt, characteristic, status);
            _wes.RaiseEvent(nameof(CharacteristicRead), this, args);
        }

        public override void OnCharacteristicWrite(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, [GeneratedEnum] GattStatus status)
        {
            base.OnCharacteristicWrite(gatt, characteristic, status);

            var args = new BLEGATTCharacteristicStatusEventArgs(gatt, characteristic, status);
            _wes.RaiseEvent(nameof(CharacteristicWrite), this, args);
        }

        public override void OnConnectionStateChange(BluetoothGatt gatt, [GeneratedEnum] GattStatus status, [GeneratedEnum] ProfileState newState)
        {
            base.OnConnectionStateChange(gatt, status, newState);

            var args = new BLEGATTProfileStateStatusEventArgs(gatt, status, newState);
            _wes.RaiseEvent(nameof(ConnectionStateChanged), this, args);
        }

        public override void OnDescriptorRead(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, [GeneratedEnum] GattStatus status)
        {
            base.OnDescriptorRead(gatt, descriptor, status);

            var args = new BLEGATTDescriptorStatusEventArgs(gatt, descriptor, status);
            _wes.RaiseEvent(nameof(DescriptorRead), this, args);
        }

        public override void OnDescriptorWrite(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, [GeneratedEnum] GattStatus status)
        {
            base.OnDescriptorWrite(gatt, descriptor, status);

            var args = new BLEGATTDescriptorStatusEventArgs(gatt, descriptor, status);
            _wes.RaiseEvent(nameof(DescriptorWrite), this, args);
        }

        public override void OnMtuChanged(BluetoothGatt gatt, int mtu, [GeneratedEnum] GattStatus status)
        {
            base.OnMtuChanged(gatt, mtu, status);

            var args = new BLEGATTMTUStatusEventArgs(gatt, mtu, status);
            _wes.RaiseEvent(nameof(MTUChanged), this, args);
        }

        public override void OnPhyRead(BluetoothGatt gatt, [GeneratedEnum] ScanSettingsPhy txPhy, [GeneratedEnum] ScanSettingsPhy rxPhy, [GeneratedEnum] GattStatus status)
        {
            base.OnPhyRead(gatt, txPhy, rxPhy, status);

            var args = new BLEGATTPHYStatusEventArgs(gatt, txPhy, rxPhy, status);
            _wes.RaiseEvent(nameof(PHYRead), this, args);
        }

        public override void OnPhyUpdate(BluetoothGatt gatt, [GeneratedEnum] ScanSettingsPhy txPhy, [GeneratedEnum] ScanSettingsPhy rxPhy, [GeneratedEnum] GattStatus status)
        {
            base.OnPhyUpdate(gatt, txPhy, rxPhy, status);

            var args = new BLEGATTPHYStatusEventArgs(gatt, txPhy, rxPhy, status);
            _wes.RaiseEvent(nameof(PHYUpdate), this, args);
        }

        public override void OnReadRemoteRssi(BluetoothGatt gatt, int rssi, [GeneratedEnum] GattStatus status)
        {
            base.OnReadRemoteRssi(gatt, rssi, status);

            var args = new BLEGATTRSSIStatusEventArgs(gatt, rssi, status);
            _wes.RaiseEvent(nameof(ReadRemoteRSSI), this, args);
        }

        public override void OnReliableWriteCompleted(BluetoothGatt gatt, [GeneratedEnum] GattStatus status)
        {
            base.OnReliableWriteCompleted(gatt, status);

            var args = new BLEGATTStatusEventArgs(gatt, status);
            _wes.RaiseEvent(nameof(ReliableWriteCompleted), this, args);
        }

        public override void OnServicesDiscovered(BluetoothGatt gatt, [GeneratedEnum] GattStatus status)
        {
            base.OnServicesDiscovered(gatt, status);

            var args = new BLEGATTStatusEventArgs(gatt, status);
            _wes.RaiseEvent(nameof(ServicesDiscovered), this, args);
        }
        #endregion
    }
}