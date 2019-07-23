using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEGATTCharacteristicStatusEventArgs : EventArgs
    {
        public BluetoothGatt GATT { get; }
        public BluetoothGattCharacteristic Characteristic { get; }
        public GattStatus Status { get; }

        public BLEGATTCharacteristicStatusEventArgs(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status)
        {
            GATT = gatt;
            Characteristic = characteristic;
            Status = status;
        }
    }
}