using Android.Content;
using Android.Content.Res;
using System.IO;
using System.Linq;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        private Stream GetSourceStream()
        {
            var assets = Context.Assets.List(string.Empty);
            if (assets == null || !assets.Contains(Source))
                return null;

            var stream = Context.Assets.Open(Source);
            return stream;
        }
    }
}
