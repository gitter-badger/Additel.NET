using Android.Bluetooth.LE;
using System;

namespace Additel.BLE
{
    internal class BLEScanResultEventArgs : EventArgs
    {
        public ScanCallbackType CallbackType { get; }
        public ScanResult Result { get; }

        public BLEScanResultEventArgs(ScanCallbackType callbackType, ScanResult result)
        {
            CallbackType = callbackType;
            Result = result;
        }
    }
}