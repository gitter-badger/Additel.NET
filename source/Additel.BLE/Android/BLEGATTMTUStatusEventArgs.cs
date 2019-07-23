using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEGATTMTUStatusEventArgs : EventArgs
    {
        public BluetoothGatt GATT { get; }
        public int MTU { get; }
        public GattStatus Status { get; }

        public BLEGATTMTUStatusEventArgs(BluetoothGatt gatt, int mtu, GattStatus status)
        {
            GATT = gatt;
            MTU = mtu;
            Status = status;
        }
    }
}