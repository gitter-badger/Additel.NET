using Additel.Core;
using AVFoundation;
using CoreLocation;
using Foundation;
using Photos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Additel.Authorization
{
    public static partial class AuthorizationManager
    {
        #region 字段

        private static readonly ConcurrentDictionary<AuthorizationCategory, int> _requests
            = new ConcurrentDictionary<AuthorizationCategory, int>();

        private static CLLocationManager _locationManager;
        #endregion

        #region 方法

        private static AuthorizationState PlatformGetState(AuthorizationCategory category)
        {
            EnsureDeclared(category);

            AuthorizationState state;
            switch (category)
            {
                case AuthorizationCategory.Camera:
                    state = AVCaptureDevice.GetAuthorizationStatus(AVAuthorizationMediaType.Video).ToShared();
                    break;
                case AuthorizationCategory.LocationWhenUse:
                    state = CLLocationManager.Status.ToShared(false);
                    break;
                case AuthorizationCategory.LocationAlways:
                    state = CLLocationManager.Status.ToShared(true);
                    break;
                case AuthorizationCategory.PhotoLibrary:
                    state = PHPhotoLibrary.AuthorizationStatus.ToShared();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category));
            }

            return state;
        }

        private static void EnsureDeclared(AuthorizationCategory category)
        {
            var requested = GetDescriptions(category);

            var info = NSBundle.MainBundle.InfoDictionary;
            var undeclared = requested.Where(r => !info.ContainsKey(r));
            if (undeclared.Any())
            {
                var aggregate = undeclared.Aggregate((total, next) => total += $", {next}");
                throw new AuthorizationException(AuthorizationState.Denied, $"未在 info.plist 文件中声明 `{aggregate}` 权限");
            }
        }

        private static string[] GetDescriptions(AuthorizationCategory category)
        {
            var descriptions = new List<string>();

            switch (category)
            {
                case AuthorizationCategory.Camera:
                    {
                        descriptions.Add("NSCameraUsageDescription");
                        break;
                    }
                case AuthorizationCategory.LocationWhenUse:
                    {
                        descriptions.Add("NSLocationWhenInUseUsageDescription");
                        break;
                    }
                case AuthorizationCategory.LocationAlways:
                    {
                        if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                        {
                            descriptions.Add("NSLocationWhenInUseUsageDescription");
                            descriptions.Add("NSLocationAlwaysAndWhenInUseUsageDescription");
                        }
                        else
                        {
                            descriptions.Add("NSLocationAlwaysUsageDescription");
                        }
                        break;
                    }
                case AuthorizationCategory.PhotoLibrary:
                    {
                        descriptions.Add("NSPhotoLibraryUsageDescription");
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(category));
                    }
            }

            return descriptions.ToArray();
        }

        private static void PlatformRequest(AuthorizationCategory category)
        {
            EnsureDeclared(category);

            switch (category)
            {
                case AuthorizationCategory.Camera:
                    RequestCamera();
                    break;
                case AuthorizationCategory.LocationWhenUse:
                    RequestLocationWhenInUse();
                    break;
                case AuthorizationCategory.LocationAlways:
                    RequestLocationAlways();
                    break;
                case AuthorizationCategory.PhotoLibrary:
                    RequestPhotoLibrary();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category));
            }
        }

        private static void RequestCamera()
        {
            var status = AVCaptureDevice.GetAuthorizationStatus(AVAuthorizationMediaType.Video);
            if (status != AVAuthorizationStatus.NotDetermined)
            {
                RaiseAuthorizationRequested(AuthorizationCategory.Camera, status.ToShared());
                return;
            }

            if (_requests.TryGetValue(AuthorizationCategory.Camera, out var value))
            {
                var result = _requests.TryUpdate(AuthorizationCategory.Camera, value + 1, value);
            }
            else
            {
                _requests.TryAdd(AuthorizationCategory.Camera, 1);
                AVCaptureDevice.RequestAccessForMediaType(AVAuthorizationMediaType.Video, OnCameraRequested);
            }
        }

        private static void RequestLocationWhenInUse()
        {
            var status = CLLocationManager.Status;
            if (status != CLAuthorizationStatus.NotDetermined)
            {
                RaiseAuthorizationRequested(AuthorizationCategory.LocationWhenUse, status.ToShared(false));
                return;
            }

            if (_requests.TryGetValue(AuthorizationCategory.LocationWhenUse, out var value1))
            {
                var result = _requests.TryUpdate(AuthorizationCategory.LocationWhenUse, value1 + 1, value1);
            }
            else if (_requests.ContainsKey(AuthorizationCategory.LocationAlways))
            {
                var result = _requests.TryAdd(AuthorizationCategory.LocationWhenUse, 1);
            }
            else
            {
                _requests.TryAdd(AuthorizationCategory.LocationWhenUse, 1);

                _locationManager = new CLLocationManager();
                _locationManager.AuthorizationChanged += OnLocationRequested;
                _locationManager.RequestWhenInUseAuthorization();
            }
        }

        private static void RequestLocationAlways()
        {
            var status = CLLocationManager.Status;
            if (status != CLAuthorizationStatus.NotDetermined &&
                status != CLAuthorizationStatus.AuthorizedWhenInUse)
            {
                RaiseAuthorizationRequested(AuthorizationCategory.LocationAlways, status.ToShared(true));
                return;
            }

            if (_requests.TryGetValue(AuthorizationCategory.LocationAlways, out var value1))
            {
                var result = _requests.TryUpdate(AuthorizationCategory.LocationAlways, value1 + 1, value1);
            }
            else if (_requests.ContainsKey(AuthorizationCategory.LocationWhenUse))
            {
                var result = _requests.TryAdd(AuthorizationCategory.LocationAlways, 1);
            }
            else
            {
                _requests.TryAdd(AuthorizationCategory.LocationAlways, 1);
                _locationManager = new CLLocationManager();
                _locationManager.AuthorizationChanged += OnLocationRequested;
                _locationManager.RequestAlwaysAuthorization();
            }
        }

        private static void RequestPhotoLibrary()
        {
            var status = PHPhotoLibrary.AuthorizationStatus;
            if (status != PHAuthorizationStatus.NotDetermined)
            {
                RaiseAuthorizationRequested(AuthorizationCategory.PhotoLibrary, status.ToShared());
                return;
            }

            if (_requests.TryGetValue(AuthorizationCategory.PhotoLibrary, out var value))
            {
                var result = _requests.TryUpdate(AuthorizationCategory.PhotoLibrary, value + 1, value);
            }
            else
            {
                _requests.TryAdd(AuthorizationCategory.PhotoLibrary, 1);
                PHPhotoLibrary.RequestAuthorization(OnPhotoLibraryRequested);
            }
        }

        private static void OnPhotoLibraryRequested(PHAuthorizationStatus status)
        {
            RaiseAuthorizationRequested(AuthorizationCategory.PhotoLibrary, status.ToShared());
        }

        private static void OnLocationRequested(object sender, CLAuthorizationChangedEventArgs e)
        {
            if (e.Status == CLAuthorizationStatus.NotDetermined)
                return;

            if (_requests.TryRemove(AuthorizationCategory.LocationWhenUse, out var times1))
            {
                for (int i = 0; i < times1; i++)
                {
                    RaiseAuthorizationRequested(AuthorizationCategory.LocationWhenUse, e.Status.ToShared(false));
                }
            }
            if (_requests.TryRemove(AuthorizationCategory.LocationAlways, out var times2))
            {
                for (int i = 0; i < times2; i++)
                {
                    RaiseAuthorizationRequested(AuthorizationCategory.LocationAlways, e.Status.ToShared(true));
                }
            }

            _locationManager.AuthorizationChanged -= OnLocationRequested;
            _locationManager.Dispose();
            _locationManager = null;
        }

        private static void OnCameraRequested(bool accessGranted)
        {
            var state = accessGranted
                ? AuthorizationState.Authorized
                : AuthorizationState.Denied;
            RaiseAuthorizationRequested(AuthorizationCategory.Camera, state);
        }
        #endregion
    }
}
