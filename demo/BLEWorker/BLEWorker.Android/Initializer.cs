using BLEWorker.Services;
using Prism;
using Prism.Ioc;

namespace BLEWorker
{
    public class Initializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.Register<INativeService, NativeService>();
        }
    }
}