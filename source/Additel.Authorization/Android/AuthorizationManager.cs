using Additel.Core.Lifecycle;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Additel.Authorization
{
    public static partial class AuthorizationManager
    {
        #region 字段

        private static readonly ConcurrentDictionary<int, AuthorizationType> _requests
            = new ConcurrentDictionary<int, AuthorizationType>();

        private static int _requestCode = 0;
        #endregion

        #region 构造

        static AuthorizationManager()
        {
            LifecycleManager.PermissionsRequested += OnPermissionsResultRequested;
        }
        #endregion

        #region 方法

        private static AuthorizationState PlatformGetState(AuthorizationType category)
        {
            // 检查是否声明了权限
            var permissions = GetPermissions(category, false);
            EnsureDeclared(permissions);

            // 检查运行时权限状态
            permissions = GetPermissions(category, true);
            var state = GetState(permissions).ToShared();

            return state;
        }

        private static Permission GetState(string[] permissions)
        {
            var context = Application.Context;
            var state = permissions.Any(r => ContextCompat.CheckSelfPermission(context, r) != Permission.Granted)
                ? Permission.Denied
                : Permission.Granted;

            return state;
        }

        private static void EnsureDeclared(string[] permissions)
        {
            var context = Application.Context;

            var declared = context
                .PackageManager
                .GetPackageInfo(context.PackageName, PackageInfoFlags.Permissions)
                .RequestedPermissions;

            var undeclared = permissions.Where(r => !declared.Contains(r));

            if (undeclared.Any())
            {
                var aggregate = undeclared.Aggregate((total, next) => total += $", {next}");
                throw new AuthorizationException(AuthorizationState.Denied, $"未在 AndroidManifest.xml 文件中声明 `{aggregate}` 权限");
            }
        }

        private static string[] GetPermissions(AuthorizationType category, bool isRuntimeOnly)
        {
            var tuples = new List<(string Permission, bool IsRuntime)>();

            switch (category)
            {
                case AuthorizationType.Camera:
                    {
                        tuples.Add((Manifest.Permission.Camera, true));
                        break;
                    }
                case AuthorizationType.Location:
                    {
                        tuples.Add((Manifest.Permission.AccessCoarseLocation, true));
                        tuples.Add((Manifest.Permission.AccessFineLocation, true));
                        break;
                    }
                case AuthorizationType.Bluetooth:
                    {
                        tuples.Add((Manifest.Permission.Bluetooth, false));
                        tuples.Add((Manifest.Permission.BluetoothAdmin, false));
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                        {
                            tuples.Add((Manifest.Permission.AccessCoarseLocation, true));
                            tuples.Add((Manifest.Permission.AccessFineLocation, true));
                        }
                        break;
                    }
                case AuthorizationType.ExternalStorage:
                    {
                        tuples.Add((Manifest.Permission.ReadExternalStorage, true));
                        tuples.Add((Manifest.Permission.WriteExternalStorage, true));
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(category));
                    }
            }

            if (isRuntimeOnly)
            {
                return tuples
                    .Where(t => t.IsRuntime)
                    .Select(t => t.Permission)
                    .ToArray();
            }

            return tuples
                .Select(t => t.Permission)
                .ToArray();
        }

        private static void PlatformRequest(AuthorizationType category)
        {
            // 检查是否声明了权限
            var permissions = GetPermissions(category, false);
            EnsureDeclared(permissions);

            // 检查运行时权限状态
            permissions = GetPermissions(category, true);
            var state = GetState(permissions);

            if (state == Permission.Granted)
            {
                RaiseAuthorizationRequested(category, AuthorizationState.Authorized);
            }
            else
            {
                if (!_requests.TryAdd(_requestCode, category))
                    throw new ArgumentException($"键值已存在: {_requestCode}", nameof(_requestCode));

                // 请求未被用户授权的运行时权限
                ActivityCompat.RequestPermissions(LifecycleManager.CurrentActivity, permissions, _requestCode);

                // 请求码范围: 0 ~ 65535
                if (++_requestCode > ushort.MaxValue)
                    _requestCode = ushort.MinValue;
            }
        }

        private static void OnPermissionsResultRequested(object sender, PermissionsEventArgs e)
        {
            if (!_requests.TryRemove(e.RequestCode, out var category))
                return;

            var state = AuthorizationState.Authorized;
            for (int i = 0; i < e.GrantResults.Length; i++)
            {
                if (e.GrantResults[i] != Permission.Granted)
                {
                    state = ActivityCompat.ShouldShowRequestPermissionRationale(LifecycleManager.CurrentActivity, e.Permissions[i])
                        ? AuthorizationState.NotDetermined
                        : AuthorizationState.Denied;
                    break;
                }
            }

            RaiseAuthorizationRequested(category, state);
        }
        #endregion
    }
}
