using SkiaSharp;
using SkiaSharp.Views.WPF;
using System.Windows.Media;

namespace Additel.SkiaViews
{
    partial class Extensions
    {
        public static Color ToColor(this SKColor color)
            => WPFExtensions.ToColor(color);

        public static SKColor ToSKColor(this Color color)
            => WPFExtensions.ToSKColor(color);
    }
}
