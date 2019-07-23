using Android.Bluetooth.LE;
using System;

namespace Additel.BLE
{
    internal class BLEScanFailureEventArgs : EventArgs
    {
        public ScanFailure ErrorCode { get; }

        public BLEScanFailureEventArgs(ScanFailure errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}