using Android.App;
using Android.Bluetooth;
using Android.Content;

namespace Additel.BLE
{
    public static partial class BLEManager
    {
        #region 构造

        private static BLEAdapter PlatformGetAdapter()
        {
            var manager = Application.Context.GetSystemService(Context.BluetoothService) as BluetoothManager;
            return new BLEAdapter(manager);
        }
        #endregion
    }
}