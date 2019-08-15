using System.IO;
using System.Threading.Tasks;

namespace Additel.Core
{
    public static partial class Storage
    {
        public static async Task<Stream> GetResourceStreamAsync(string name, string ext)
            => await NativeGetResourceStreamAsync(name, ext);
    }
}
