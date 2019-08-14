using Additel.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="Source"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                nameof(Source),
                typeof(string),
                typeof(GIFView),
                new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (GIFView)d;
            var oldValue = (string)e.OldValue;
            var newValue = (string)e.NewValue;

            view.UpdateSource(oldValue, newValue);
        }

        public SKStretch Stretch
        {
            get { return (SKStretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="Stretch"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(
                nameof(Stretch),
                typeof(SKStretch),
                typeof(GIFView),
                new PropertyMetadata(SKStretch.None, OnStretchChanged));

        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (GIFView)d;
            view.UpdateStretch();
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
            // 优先使用 Resources 目录下的 Resource 资源
            var uri = new Uri($"pack://application:,,,/Resources/{name}.{ext}");
            var stream = GetResourceStream(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在则使用根目录下是 Resource 资源
            uri = new Uri($"pack://application:,,,/{name}.{ext}");
            stream = GetResourceStream(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在则使用 Assets 目录下的 Content 资源
            uri = new Uri($"pack://application:,,,/Assets/{name}.{ext}");
            stream = GetContentStream(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在则使用根目录下的 Content 资源
            uri = new Uri($"pack://application:,,,/{name}.{ext}");
            stream = GetContentStream(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在返回 NULL
            return null;
        }

        private Stream GetResourceStream(Uri uri)
        {
            try
            {
                var info = Application.GetResourceStream(uri);
                return info?.Stream;
            }
            catch (IOException)
            {
                return null;
            }
        }

        private Stream GetContentStream(Uri uri)
        {
            try
            {
                var info = Application.GetContentStream(uri);
                return info?.Stream;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}
