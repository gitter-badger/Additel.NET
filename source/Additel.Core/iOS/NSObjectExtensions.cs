using Foundation;
using System;
using System.Text;

namespace Additel.Core
{
    public static class NSObjectExtensions
    {
        public static byte[] ToArray(this NSObject obj)
        {
            switch (obj)
            {
                case NSData data:
                    return data.ToArray();
                case NSNumber number:
                    return BitConverter.GetBytes(number.UInt64Value);
                case NSString str:
                    return Encoding.UTF8.GetBytes(str.ToString());
                default:
                    return null;
            }
        }
    }
}
