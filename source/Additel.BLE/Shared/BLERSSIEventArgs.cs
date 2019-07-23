using System;

namespace Additel.BLE
{
    public class BLERSSIEventArgs : EventArgs
    {
        public int RSSI { get; }

        public BLERSSIEventArgs(int rssi)
        {
            RSSI = rssi;
        }
    }
}
