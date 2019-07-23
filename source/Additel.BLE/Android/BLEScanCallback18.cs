using Android.Bluetooth;
using System;
using JavaObject = Java.Lang.Object;

namespace Additel.BLE
{
    internal class BLEScanCallback18 : JavaObject, BluetoothAdapter.ILeScanCallback
    {
        #region 事件

        public event EventHandler<BLELEScanEventArgs> LEScan;
        #endregion

        #region BluetoothAdapter.ILeScanCallback

        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
            => LEScan?.Invoke(this, new BLELEScanEventArgs(device, rssi, scanRecord));
        #endregion
    }
}