using Foundation;
using System.IO;
using System.Threading.Tasks;
using UIKit;

namespace Additel.Core
{
    public static partial class Storage
    {
        public static async Task<Stream> NativeGetResourceStreamAsync(string name, string ext)
        {
            var stream = await Task.Run(() => GetResourceStream(name, ext));
            return stream;
        }

        public static Stream GetResourceStream(string name, string ext)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ext))
                return null;

            var scale = Device.IsInvokeRequired
                      ? Device.InvokeOnMainThread(() => (int)UIScreen.MainScreen.Scale)
                      : (int)UIScreen.MainScreen.Scale;

            while (scale >= 0)
            {
                var path = scale > 0
                           ? NSBundle.MainBundle.PathForResource($"{name}@{scale}x", ext)
                           : NSBundle.MainBundle.PathForResource(name, ext);

                if (File.Exists(path))
                {
                    var stream = File.OpenRead(path);
                    return stream;
                }

                scale--;
            }

            return null;
        }
    }
}
