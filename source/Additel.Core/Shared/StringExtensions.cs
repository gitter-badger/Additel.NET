using System;

namespace Additel.Core
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string str, string value, StringComparison comparisonType)
        {
            if (!str.EndsWith(value, comparisonType))
                return str;

            var index = str.LastIndexOf(value);
            var str1 = str.Remove(index, value.Length);

            return str1;
        }
    }
}
