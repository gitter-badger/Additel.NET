using Android.Bluetooth;
using Android.Content;
using System;

namespace Additel.BLE
{
    internal class BLEBroadcastReceiver : BroadcastReceiver
    {
        #region 事件

        public event EventHandler<BLEActionStateEventArgs> AdapterStateChanged;
        #endregion

        #region BroadcastReceiver

        public override void OnReceive(Context context, Intent intent)
        {
            switch (intent.Action)
            {
                case BluetoothAdapter.ActionStateChanged:
                    {
                        var oldState = (State)intent.GetIntExtra(BluetoothAdapter.ExtraPreviousState, -1);
                        var newState = (State)intent.GetIntExtra(BluetoothAdapter.ExtraState, -1);
                        AdapterStateChanged?.Invoke(this, new BLEActionStateEventArgs(oldState, newState));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion
    }
}