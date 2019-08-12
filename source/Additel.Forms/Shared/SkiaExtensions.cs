using Additel.SkiaViews;
using Xamarin.Forms;

namespace Additel.Forms
{
    internal static class SKiaExtensions
    {
        public static SKStretch ToSKStretch(this Stretch stretch)
        {
            var native = (SKStretch)stretch;
            return native;
        }
    }
}
