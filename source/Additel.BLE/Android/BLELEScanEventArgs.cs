using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLELEScanEventArgs : EventArgs
    {
        public BluetoothDevice Device { get; }
        public int RSSI { get; }
        public byte[] ScanRecord { get; }

        public BLELEScanEventArgs(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            Device = device;
            RSSI = rssi;
            ScanRecord = scanRecord;
        }
    }
}