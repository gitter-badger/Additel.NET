using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEGATTStatusEventArgs : EventArgs
    {
        public BluetoothGatt GATT { get; }
        public GattStatus Status { get; }

        public BLEGATTStatusEventArgs(BluetoothGatt gatt, GattStatus status)
        {
            GATT = gatt;
            Status = status;
        }
    }
}