using CoreBluetooth;

namespace Additel.BLE
{
    public static partial class BLEManager
    {
        #region 方法

        private static BLEAdapter PlatformGetAdapter()
            => new BLEAdapter(new CBCentralManager());
        #endregion
    }
}