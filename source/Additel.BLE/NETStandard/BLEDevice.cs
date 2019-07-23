using Additel.Core;
using System;

namespace Additel.BLE
{
    public partial class BLEDevice
    {
        #region 属性

        private Guid PlatformUUID
            => throw new NotImplementedInReferenceAssemblyException();

        private string PlatformName
            => throw new NotImplementedInReferenceAssemblyException();

        private BLEDeviceState PlatformState
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion

        #region 方法

        private void PlatformConnect()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDisconnect()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformReadRSSI()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDiscoverServices()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDispose(bool disposing)
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
