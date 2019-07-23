using System;

namespace Additel.BLE
{
    public class BLEErrorEventArgs : EventArgs
    {
        public BLEErrorCode ErrorCode { get; }

        public BLEErrorEventArgs(BLEErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
