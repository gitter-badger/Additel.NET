using Foundation;
using System.IO;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        private Stream GetSourceStream()
        {
            var source = Path.Combine(NSBundle.MainBundle.BundlePath, Source);

            if (!File.Exists(source))
                return null;

            var stream = File.OpenRead(source);
            return stream;
        }
    }
}
