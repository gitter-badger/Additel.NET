using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Additel.Authorization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BLEWorker.Services
{
    public class NativeService : INativeService
    {
        public Task<AuthorizationState> RequestBluetoothAuthorizationAsync()
            => AuthorizationManager.RequestAsync(AuthorizationCategory.Bluetooth);
    }
}