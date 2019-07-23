using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEGATTProfileStateStatusEventArgs : EventArgs
    {
        public BluetoothGatt GATT { get; }
        public GattStatus Status { get; }
        public ProfileState NewState { get; }

        public BLEGATTProfileStateStatusEventArgs(BluetoothGatt gatt, GattStatus status, ProfileState newState)
        {
            GATT = gatt;
            Status = status;
            NewState = newState;
        }
    }
}