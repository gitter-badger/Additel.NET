using SkiaSharp;
using SkiaSharp.Views.iOS;
using UIKit;

namespace Additel.SkiaViews
{
    partial class Extensions
    {
        public static UIColor ToColor(this SKColor color)
            => iOSExtensions.ToUIColor(color);

        public static SKColor ToSKColor(this UIColor color)
            => iOSExtensions.ToSKColor(color);
    }
}
