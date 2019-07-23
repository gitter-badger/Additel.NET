using Additel.Core;
using System;

namespace Additel.BLE
{
    public partial class BLEDescriptor
    {
        #region 属性

        private Guid PlatformUUID
            => throw new NotImplementedInReferenceAssemblyException();

        private byte[] PlatformValue
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion

        #region 方法

        private void PlatformRead()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformWrite(byte[] data)
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDispose(bool disposing)
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
