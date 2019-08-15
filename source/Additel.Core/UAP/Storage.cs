using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Additel.Core
{
    public static partial class Storage
    {
        private static async Task<Stream> NativeGetResourceStreamAsync(string name, string ext)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ext))
                return null;

            // 优先在 Assets 资产目录下查找文件
            var uri = new Uri($"ms-appx:///Assets/{name}.{ext}");
            var stream = await GetApplicationUriStreamAsync(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在则在根目录下查找
            uri = new Uri($"ms-appx:///{name}.{ext}");
            stream = await GetApplicationUriStreamAsync(uri);
            if (stream != null)
            {
                return stream;
            }
            // 若不存在返回 NULL
            return null;
        }

        private static async Task<Stream> GetApplicationUriStreamAsync(Uri uri)
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
