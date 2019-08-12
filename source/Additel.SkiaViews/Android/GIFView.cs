using Additel.Core;
using Android.Content;
using System;
using System.IO;
using System.Linq;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        public GIFView(Context context)
            : base(context)
        {

        }

        private Stream GetSourceStream()
        {
            if (string.IsNullOrWhiteSpace(Source))
                return null;

            var assets = Context.Assets.List(string.Empty);
            var name = Source.TrimEnd(".gif", StringComparison.OrdinalIgnoreCase);
            var value = $"{name}.gif";

            if (assets == null || !assets.Contains(value))
                return null;

            var stream = Context.Assets.Open(value);
            return stream;
        }
    }
}
