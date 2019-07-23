using Additel.Core;
using System;

namespace Additel.BLE
{
    public partial class BLEAdapter
    {
        #region 属性

        private BLEAdapterState PlatformState
            => throw new NotImplementedInReferenceAssemblyException();

        private bool PlatformIsScanning
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion

        #region 方法

        private void PlatformStartScan(Guid[] uuids)
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformStopScan()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformDispose(bool disposing)
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
