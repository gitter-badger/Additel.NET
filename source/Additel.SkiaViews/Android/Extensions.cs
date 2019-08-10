using Android.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Android;

namespace Additel.SkiaViews
{
    partial class Extensions
    {
        public static Color ToColor(this SKColor color)
            => AndroidExtensions.ToColor(color);

        public static SKColor ToSKColor(this Color color)
            => AndroidExtensions.ToSKColor(color);
    }
}
