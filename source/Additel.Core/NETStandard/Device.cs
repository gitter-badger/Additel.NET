using System;

namespace Additel.Core
{
    public static partial class Device
    {
        public static void BeginInvokeOnMainThread(Action action)
            => throw new NotImplementedInReferenceAssemblyException();

        public static bool IsInvokeRequired
            => throw new NotImplementedInReferenceAssemblyException();
    }
}
