using System;

namespace Additel.BLE
{
    public class BLEAdapterStateEventArgs : EventArgs
    {
        public BLEAdapterState OldState { get; }
        public BLEAdapterState NewState { get; }

        public BLEAdapterStateEventArgs(BLEAdapterState oldState, BLEAdapterState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
