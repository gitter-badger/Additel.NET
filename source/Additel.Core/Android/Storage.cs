using Android.App;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Additel.Core
{
    public static partial class Storage
    {
        private static async Task<Stream> NativeGetResourceStreamAsync(string name, string ext)
        {
            var stream = await Task.Run(() => GetResourceStream(name, ext));
            return stream;
        }

        public static Stream GetResourceStream(string name, string ext)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ext))
                return null;

            var context = Application.Context;
            // 优先使用 Resources 资源
            var id = context.Resources.GetIdentifier(name, "drawable", context.PackageName);
            if (id != 0)
            {
                var stream = context.Resources.OpenRawResource(id);
                return stream;
            }
            // 若不存在，使用 Assets 资源
            var assets = context.Assets.List(string.Empty);
            var value = $"{name}.{ext}";
            if (assets != null && assets.Contains(value))
            {
                var stream = context.Assets.Open(value);
                return stream;
            }
            // 若不存在，返回 NULL
            return null;
        }
    }
}
