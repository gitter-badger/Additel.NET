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

        private static readonly ConcurrentDictionary<AuthorizationType, int> _requests
            = new ConcurrentDictionary<AuthorizationType, int>();

        private static CLLocationManager _locationManager;
        #endregion

        #region 方法

        private static AuthorizationState PlatformGetState(AuthorizationType category)
        {
            EnsureDeclared(category);

            AuthorizationState state;
            switch (category)
            {
                case AuthorizationType.Camera:
                    state = AVCaptureDevice.GetAuthorizationStatus(AVAuthorizationMediaType.Video).ToShared();
                    break;
                case AuthorizationType.LocationWhenUse:
                    state = CLLocationManager.Status.ToShared(false);
                    break;
                case AuthorizationType.LocationAlways:
                    state = CLLocationManager.Status.ToShared(true);
                    break;
                case AuthorizationType.PhotoLibrary:
                    state = PHPhotoLibrary.AuthorizationStatus.ToShared();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category));
            }

            return state;
        }

        private static void EnsureDeclared(AuthorizationType category)
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

        private static string[] GetDescriptions(AuthorizationType category)
        {
            var descriptions = new List<string>();

            switch (category)
            {
                case AuthorizationType.Camera:
                    {
                        descriptions.Add("NSCameraUsageDescription");
                        break;
                    }
                case AuthorizationType.LocationWhenUse:
                    {
                        descriptions.Add("NSLocationWhenInUseUsageDescription");
                        break;
                    }
                case AuthorizationType.LocationAlways:
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
                case AuthorizationType.PhotoLibrary:
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

        private static void PlatformRequest(AuthorizationType category)
        {
            EnsureDeclared(category);

            switch (category)
            {
                case AuthorizationType.Camera:
                    RequestCamera();
                    break;
                case AuthorizationType.LocationWhenUse:
                    RequestLocationWhenInUse();
                    break;
                case AuthorizationType.LocationAlways:
                    RequestLocationAlways();
                    break;
                case AuthorizationType.PhotoLibrary:
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
                RaiseAuthorizationRequested(AuthorizationType.Camera, status.ToShared());
                return;
            }

            if (_requests.TryGetValue(AuthorizationType.Camera, out var value))
            {
                var result = _requests.TryUpdate(AuthorizationType.Camera, value + 1, value);
            }
            else
            {
                _requests.TryAdd(AuthorizationType.Camera, 1);
                AVCaptureDevice.RequestAccessForMediaType(AVAuthorizationMediaType.Video, OnCameraRequested);
            }
        }

        private static void RequestLocationWhenInUse()
        {
            var status = CLLocationManager.Status;
            if (status != CLAuthorizationStatus.NotDetermined)
            {
                RaiseAuthorizationRequested(AuthorizationType.LocationWhenUse, status.ToShared(false));
                return;
            }

            if (_requests.TryGetValue(AuthorizationType.LocationWhenUse, out var value1))
            {
                var result = _requests.TryUpdate(AuthorizationType.LocationWhenUse, value1 + 1, value1);
            }
            else if (_requests.ContainsKey(AuthorizationType.LocationAlways))
            {
                var result = _requests.TryAdd(AuthorizationType.LocationWhenUse, 1);
            }
            else
            {
                _requests.TryAdd(AuthorizationType.LocationWhenUse, 1);

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
                RaiseAuthorizationRequested(AuthorizationType.LocationAlways, status.ToShared(true));
                return;
            }

            if (_requests.TryGetValue(AuthorizationType.LocationAlways, out var value1))
            {
                var result = _requests.TryUpdate(AuthorizationType.LocationAlways, value1 + 1, value1);
            }
            else if (_requests.ContainsKey(AuthorizationType.LocationWhenUse))
            {
                var result = _requests.TryAdd(AuthorizationType.LocationAlways, 1);
            }
            else
            {
                _requests.TryAdd(AuthorizationType.LocationAlways, 1);
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
                RaiseAuthorizationRequested(AuthorizationType.PhotoLibrary, status.ToShared());
                return;
            }

            if (_requests.TryGetValue(AuthorizationType.PhotoLibrary, out var value))
            {
                var result = _requests.TryUpdate(AuthorizationType.PhotoLibrary, value + 1, value);
            }
            else
            {
                _requests.TryAdd(AuthorizationType.PhotoLibrary, 1);
                PHPhotoLibrary.RequestAuthorization(OnPhotoLibraryRequested);
            }
        }

        private static void OnPhotoLibraryRequested(PHAuthorizationStatus status)
        {
            RaiseAuthorizationRequested(AuthorizationType.PhotoLibrary, status.ToShared());
        }

        private static void OnLocationRequested(object sender, CLAuthorizationChangedEventArgs e)
        {
            if (e.Status == CLAuthorizationStatus.NotDetermined)
                return;

            if (_requests.TryRemove(AuthorizationType.LocationWhenUse, out var times1))
            {
                for (int i = 0; i < times1; i++)
                {
                    RaiseAuthorizationRequested(AuthorizationType.LocationWhenUse, e.Status.ToShared(false));
                }
            }
            if (_requests.TryRemove(AuthorizationType.LocationAlways, out var times2))
            {
                for (int i = 0; i < times2; i++)
                {
                    RaiseAuthorizationRequested(AuthorizationType.LocationAlways, e.Status.ToShared(true));
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
            RaiseAuthorizationRequested(AuthorizationType.Camera, state);
        }
        #endregion
    }
}
