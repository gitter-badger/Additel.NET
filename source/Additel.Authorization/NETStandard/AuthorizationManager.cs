using Additel.Core;

namespace Additel.Authorization
{
    public static partial class AuthorizationManager
    {
        #region 方法

        private static AuthorizationState PlatformGetState(AuthorizationCategory category)
            => throw new NotImplementedInReferenceAssemblyException();

        private static void PlatformRequest(AuthorizationCategory category)
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
