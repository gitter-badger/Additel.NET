﻿using SkiaSharp;
using SkiaSharp.Views.UWP;
using Windows.UI;

namespace Additel.SkiaViews
{
    partial class Extensions
    {
        public static Color ToColor(this SKColor color)
            => UWPExtensions.ToColor(color);

        public static SKColor ToSKColor(this Color color)
            => UWPExtensions.ToSKColor(color);
    }
}
