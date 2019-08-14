using Windows.Devices.Bluetooth.Advertisement;

namespace Additel.BLE
{
    public static partial class BLEManager
    {
        #region 方法

        private static BLEAdapter PlatformGetAdapter()
            => new BLEAdapter(new BluetoothLEAdvertisementWatcher());
        #endregion
    }
}
