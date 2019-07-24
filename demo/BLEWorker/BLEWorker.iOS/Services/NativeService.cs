using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Additel.Authorization;
using Foundation;
using UIKit;

namespace BLEWorker.Services
{
    public class NativeService : INativeService
    {
        public Task<AuthorizationState> RequestBluetoothAuthorizationAsync()
            => Task.FromResult(AuthorizationState.Authorized);
    }
}