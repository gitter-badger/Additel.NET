using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Additel.Core
{
    public static partial class Storage
    {
        private async static Task<Stream> NativeGetResourceStreamAsync(string name, string ext)
        {
            var stream = await Task.Run(() => GetResourceStream(name, ext));
            return stream;
        }

        public static Stream GetResourceStream(string name, string ext)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ext))
                return null;

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

        private static Stream GetResourceStream(Uri uri)
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

        private static Stream GetContentStream(Uri uri)
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
