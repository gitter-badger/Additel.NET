using Additel.Core;

namespace Additel.Authorization
{
    public static partial class AuthorizationManager
    {
        #region 方法

        private static AuthorizationState PlatformGetState(AuthorizationType category)
            => throw new NotImplementedInReferenceAssemblyException();

        private static void PlatformRequest(AuthorizationType category)
            => throw new NotImplementedInReferenceAssemblyException();
        #endregion
    }
}
