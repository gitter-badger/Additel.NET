namespace Additel.BLE
{
    public static partial class BLEManager
    {
        #region 方法

        public static BLEAdapter GetAdapter()
            => PlatformGetAdapter();
        #endregion
    }
}
