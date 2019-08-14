using Additel.Core;
using System;

namespace Additel.BLE
{
    public partial class BLECharacteristic
    {
        #region 属性

        private Guid PlatformUUID
            => throw new NotImplementedInReferenceAssemblyException();

        private byte[] PlatformValue
            => throw new NotImplementedInReferenceAssemblyException();

        private BLECharacteristicProperty PlatformProperties
            => throw new NotImplementedInReferenceAssemblyException();

        private bool PlatformIsNotifying
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion

        #region 方法

        private void PlatformRead()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformWrite(byte[] data)
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformSetNotification(bool enabled)
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDiscoverDescriptors()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDispose(bool disposing)
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
