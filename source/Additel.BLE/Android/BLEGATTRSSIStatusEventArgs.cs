using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEGATTRSSIStatusEventArgs : EventArgs
    {
        public BluetoothGatt GATT { get; }
        public int RSSI { get; }
        public GattStatus Status { get; }

        public BLEGATTRSSIStatusEventArgs(BluetoothGatt gatt, int rssi, GattStatus status)
        {
            GATT = gatt;
            RSSI = rssi;
            Status = status;
        }
    }
}