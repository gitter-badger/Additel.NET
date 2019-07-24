using Android.Content.PM;
using System;

namespace Additel.Core.Lifecycle
{
    public class PermissionsEventArgs : EventArgs
    {
        public int RequestCode { get; }
        public string[] Permissions { get; }
        public Permission[] GrantResults { get; }

        public PermissionsEventArgs(int requestCode, string[] permissions, Permission[] grantResults)
        {
            RequestCode = requestCode;
            Permissions = permissions;
            GrantResults = grantResults;
        }
    }
}
