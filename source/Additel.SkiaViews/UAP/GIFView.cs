using Additel.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

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
            // 优先在 Assets 资产目录下查找文件
            var uri = new Uri($"ms-appx:///Assets/{name}.gif");
            var stream = await GetApplicationUriStreamAsync(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在则在根目录下查找
            uri = new Uri($"ms-appx:///{name}.gif");
            stream = await GetApplicationUriStreamAsync(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在返回 NULL
            return null;
        }

        private async Task<Stream> GetApplicationUriStreamAsync(Uri uri)
        {
            // UWP 不支持检查文件是否存在,使用 Try Catch 捕获文件不存在的异常
            try
            {
                var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                var stream = await file.OpenStreamForReadAsync();

                return stream;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}
