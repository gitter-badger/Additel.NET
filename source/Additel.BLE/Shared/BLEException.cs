using System;

namespace Additel.BLE
{
    public class BLEException : Exception
    {
        public BLEErrorCode ErrorCode { get; }

        public BLEException(BLEErrorCode errorCode)
            : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }
    }
}
