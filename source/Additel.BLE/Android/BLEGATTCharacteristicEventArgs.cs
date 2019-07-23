using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEGATTCharacteristicEventArgs : EventArgs
    {
        public BluetoothGatt GATT { get; }
        public BluetoothGattCharacteristic Characteristic { get; }

        public BLEGATTCharacteristicEventArgs(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic)
        {
            GATT = gatt;
            Characteristic = characteristic;
        }
    }
}