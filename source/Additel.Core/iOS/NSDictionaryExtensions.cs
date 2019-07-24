using Foundation;

namespace Additel.Core
{
    public static class NSDictionaryExtensions
    {
        public static bool ContainsKey(this NSDictionary kvs, string key)
        {
            using (var nsKey = new NSString(key))
            {
                return kvs.ContainsKey(nsKey);
            }
        }
    }
}
