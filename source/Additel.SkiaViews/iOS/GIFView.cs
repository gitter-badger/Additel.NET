using Additel.Core;
using Foundation;
using System;
using System.IO;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        private Stream GetSourceStream()
        {
            if (string.IsNullOrWhiteSpace(Source))
                return null;

            //var source = Path.Combine(NSBundle.MainBundle.BundlePath, Source);

            var name = Source.TrimEnd(".gif", StringComparison.OrdinalIgnoreCase);
            var source = NSBundle.MainBundle.PathForResource(name, "gif");

            if (!File.Exists(source))
                return null;

            var stream = File.OpenRead(source);
            return stream;
        }
    }
}
