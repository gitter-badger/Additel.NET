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

            var name = Source.TrimEnd(".gif", StringComparison.OrdinalIgnoreCase);
            // 优先使用 Resources 资源
            var id = Context.Resources.GetIdentifier(name, "drawable", Context.PackageName);
            if (id != 0)
            {
                var stream = Context.Resources.OpenRawResource(id);
                return stream;
            }
            // 若 Resources 中不存在，优先使用 Assets 资源
            var assets = Context.Assets.List(string.Empty);
            var value = $"{name}.gif";
            if (assets != null && assets.Contains(value))
            {
                var stream = Context.Assets.Open(value);
                return stream;
            }
            // 若 Assets 中也不存在，返回 NULL
            return null;
        }
    }
}
