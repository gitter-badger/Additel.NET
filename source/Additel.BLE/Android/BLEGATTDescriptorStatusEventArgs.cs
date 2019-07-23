using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEGATTDescriptorStatusEventArgs : EventArgs
    {
        public BluetoothGatt GATT { get; }
        public BluetoothGattDescriptor Descriptor { get; }
        public GattStatus Status { get; }

        public BLEGATTDescriptorStatusEventArgs(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, GattStatus status)
        {
            GATT = gatt;
            Descriptor = descriptor;
            Status = status;
        }
    }
}