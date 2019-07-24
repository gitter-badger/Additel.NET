using Android.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Android;

namespace Additel.Controls
{
    public static class Extensions
    {
        public static Color ToColor(this SKColor color)
            => AndroidExtensions.ToColor(color);

        public static SKColor ToSKColor(this Color color)
            => AndroidExtensions.ToSKColor(color);
    }
}
