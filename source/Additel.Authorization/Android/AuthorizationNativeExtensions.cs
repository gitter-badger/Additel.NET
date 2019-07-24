using Android.Content.PM;

namespace Additel.Authorization
{
    internal static class AuthorizationNativeExtensions
    {
        public static AuthorizationState ToShared(this Permission state)
            => state == Permission.Granted
            ? AuthorizationState.Authorized
            : AuthorizationState.Denied;
    }
}