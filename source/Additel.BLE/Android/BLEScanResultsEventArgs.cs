using Android.Bluetooth.LE;
using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    internal class BLEScanResultsEventArgs : EventArgs
    {
        public IList<ScanResult> Results { get; }

        public BLEScanResultsEventArgs(IList<ScanResult> results)
        {
            Results = results;
        }
    }
}