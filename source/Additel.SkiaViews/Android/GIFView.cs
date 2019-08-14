using Additel.Core;
using Android.Content;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        private string _source;
        private SKStretch _stretch;

        public string Source
        {
            get => _source;
            set
            {
                if (_source == value)
                    return;

                var oldValue = _source;
                _source = value;
                UpdateSource(oldValue, _source);
            }
        }

        public SKStretch Stretch
        {
            get => _stretch;
            set
            {
                if (_stretch == value)
                    return;

                _stretch = value;
                UpdateStretch();
            }
        }

        public GIFView(Context context)
            : base(context)
        {

        }

        private async Task<Stream> GetSourceStreamAsync(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;

            var name = source.TrimEnd(".gif", StringComparison.OrdinalIgnoreCase);
            var stream = await Task.Run(() => GetResourceStream(name, "gif"));
            return stream;
        }

        private Stream GetResourceStream(string name, string ext)
        {
            // 优先使用 Resources 资源
            var id = Context.Resources.GetIdentifier(name, "drawable", Context.PackageName);
            if (id != 0)
            {
                var stream = Context.Resources.OpenRawResource(id);
                return stream;
            }
            // 若不存在，使用 Assets 资源
            var assets = Context.Assets.List(string.Empty);
            var value = $"{name}.{ext}";
            if (assets != null && assets.Contains(value))
            {
                var stream = Context.Assets.Open(value);
                return stream;
            }
            // 若不存在，返回 NULL
            return null;
        }
    }
}
