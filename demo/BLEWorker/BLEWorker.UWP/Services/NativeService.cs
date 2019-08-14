using System;
using System.Threading.Tasks;
using Additel.Authorization;

namespace BLEWorker.Services
{
    public class NativeService : INativeService
    {
        public Task<AuthorizationState> RequestBluetoothAuthorizationAsync()
        {
            throw new NotImplementedException();
        }
    }
}
