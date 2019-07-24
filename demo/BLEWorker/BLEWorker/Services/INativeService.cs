using Additel.Authorization;
using System.Threading.Tasks;

namespace BLEWorker.Services
{
    public interface INativeService
    {
        Task<AuthorizationState> RequestBluetoothAuthorizationAsync();
    }
}
