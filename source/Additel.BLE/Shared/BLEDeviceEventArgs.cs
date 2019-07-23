using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    public class BLEDeviceEventArgs : EventArgs
    {
        public BLEDevice Device { get; }
        public int RSSI { get; }
        public IList<BLEAdvertisement> Advertisements { get; }

        public BLEDeviceEventArgs(BLEDevice device, int rssi, IList<BLEAdvertisement> advertisements)
        {
            Device = device;
            RSSI = rssi;
            Advertisements = advertisements;
        }
    }
}
