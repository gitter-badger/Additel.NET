using System.IO;
using System.Threading.Tasks;

namespace Additel.Core
{
    public static partial class Storage
    {
        public static Task<Stream> NativeGetResourceStreamAsync(string name, string ext)
             => throw new NotImplementedInReferenceAssemblyException();
    }
}
