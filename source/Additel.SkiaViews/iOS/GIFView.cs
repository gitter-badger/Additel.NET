using Additel.Core;
using Foundation;
using System;
using System.IO;
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

        private async Task<Stream> GetSourceStreamAsync(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;

            // 只可以从 UI 线程获取此属性
            var name = source.TrimEnd(".gif", StringComparison.OrdinalIgnoreCase);
            var scale = (int)ContentScaleFactor;
            var stream = await Task.Run(() => GetBundleStream(name, "gif", scale));
            return stream;
        }

        private Stream GetBundleStream(string name, string ext, int scale)
        {
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
