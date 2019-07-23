using Additel.Core;

namespace Additel.BLE
{
    public static partial class BLEManager
    {
        #region 方法

        private static BLEAdapter PlatformGetAdapter()
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
