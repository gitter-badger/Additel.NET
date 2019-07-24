using AVFoundation;
using CoreLocation;
using Photos;

namespace Additel.Authorization
{
    internal static partial class AuthorizationNativeExtensions
    {
        public static AuthorizationState ToShared(this CLAuthorizationStatus status, bool isAlways)
        {
            var state = AuthorizationState.NotDetermined;
            switch (status)
            {
                case CLAuthorizationStatus.NotDetermined:
                    break;
                case CLAuthorizationStatus.AuthorizedAlways:
                    state = AuthorizationState.Authorized;
                    break;
                case CLAuthorizationStatus.AuthorizedWhenInUse:
                    state = isAlways
                        ? AuthorizationState.NotDetermined
                        : AuthorizationState.Authorized;
                    break;
                case CLAuthorizationStatus.Restricted:
                case CLAuthorizationStatus.Denied:
                    state = AuthorizationState.Denied;
                    break;
            }
            return state;
        }

        public static AuthorizationState ToShared(this AVAuthorizationStatus status)
        {
            var state = AuthorizationState.NotDetermined;
            switch (status)
            {
                case AVAuthorizationStatus.NotDetermined:
                    break;
                case AVAuthorizationStatus.Authorized:
                    state = AuthorizationState.Authorized;
                    break;
                case AVAuthorizationStatus.Restricted:
                case AVAuthorizationStatus.Denied:
                    state = AuthorizationState.Denied;
                    break;
            }
            return state;
        }

        public static AuthorizationState ToShared(this PHAuthorizationStatus status)
        {
            var state = AuthorizationState.NotDetermined;
            switch (status)
            {
                case PHAuthorizationStatus.NotDetermined:
                    break;
                case PHAuthorizationStatus.Authorized:
                    state = AuthorizationState.Authorized;
                    break;
                case PHAuthorizationStatus.Restricted:
                case PHAuthorizationStatus.Denied:
                    state = AuthorizationState.Denied;
                    break;
            }
            return state;
        }
    }
}