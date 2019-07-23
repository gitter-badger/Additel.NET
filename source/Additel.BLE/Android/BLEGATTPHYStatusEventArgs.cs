using Android.Bluetooth;
using Android.Bluetooth.LE;
using System;

namespace Additel.BLE
{
    internal class BLEGATTPHYStatusEventArgs : EventArgs
    {
        public BLEGATTPHYStatusEventArgs(BluetoothGatt gatt, ScanSettingsPhy txPHY, ScanSettingsPhy rxPHY, GattStatus status)
        {
            GATT = gatt;
            TXPHY = txPHY;
            RXPHY = rxPHY;
            Status = status;
        }

        public BluetoothGatt GATT { get; }
        public ScanSettingsPhy TXPHY { get; }
        public ScanSettingsPhy RXPHY { get; }
        public GattStatus Status { get; }
    }
}