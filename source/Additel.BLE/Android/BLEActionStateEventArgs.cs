using Android.Bluetooth;
using System;

namespace Additel.BLE
{
    internal class BLEActionStateEventArgs : EventArgs
    {
        public State OldState { get; }
        public State NewState { get; }

        public BLEActionStateEventArgs(State oldState, State newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}