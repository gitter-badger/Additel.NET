using CoreBluetooth;
using System;

namespace Additel.BLE
{
    internal class BLECBCentralManagerStateEventArgs : EventArgs
    {
        public CBCentralManagerState OldState { get; }
        public CBCentralManagerState NewState { get; }

        public BLECBCentralManagerStateEventArgs(CBCentralManagerState oldState, CBCentralManagerState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}