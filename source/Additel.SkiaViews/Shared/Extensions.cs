using SkiaSharp;
using System;

namespace Additel.SkiaViews
{
    public static partial class Extensions
    {
        public static void DrawBitmap(this SKCanvas canvas, SKBitmap bitmap, SKRect dest,
                                      SKStretch stretch,
                                      SKAlignment horizontal = SKAlignment.Center,
                                      SKAlignment vertical = SKAlignment.Center,
                                      SKPaint paint = null)
        {
            if (stretch == SKStretch.Fill)
            {
                canvas.DrawBitmap(bitmap, dest, paint);
            }
            else
            {
                float scale = 1;
                switch (stretch)
                {
                    case SKStretch.None:
                        break;
                    case SKStretch.Uniform:
                        scale = Math.Min(dest.Width / bitmap.Width, dest.Height / bitmap.Height);
                        break;
                    case SKStretch.UniformToFill:
                        scale = Math.Max(dest.Width / bitmap.Width, dest.Height / bitmap.Height);
                        break;
                }
                SKRect display = CalculateDisplayRect(dest, scale * bitmap.Width, scale * bitmap.Height, horizontal, vertical);
                canvas.DrawBitmap(bitmap, display, paint);
            }
        }

        public static void DrawBitmap(this SKCanvas canvas, SKBitmap bitmap, SKRect source, SKRect dest,
                                      SKStretch stretch,
                                      SKAlignment horizontal = SKAlignment.Center,
                                      SKAlignment vertical = SKAlignment.Center,
                                      SKPaint paint = null)
        {
            if (stretch == SKStretch.Fill)
            {
                canvas.DrawBitmap(bitmap, source, dest, paint);
            }
            else
            {
                float scale = 1;
                switch (stretch)
                {
                    case SKStretch.None:
                        break;
                    case SKStretch.Uniform:
                        scale = Math.Min(dest.Width / source.Width, dest.Height / source.Height);
                        break;
                    case SKStretch.UniformToFill:
                        scale = Math.Max(dest.Width / source.Width, dest.Height / source.Height);
                        break;
                }
                SKRect display = CalculateDisplayRect(dest, scale * source.Width, scale * source.Height, horizontal, vertical);
                canvas.DrawBitmap(bitmap, source, display, paint);
            }
        }

        static SKRect CalculateDisplayRect(SKRect dest, float width, float height,
                                           SKAlignment horizontal, SKAlignment vertical)
        {
            float x = 0;
            float y = 0;
            switch (horizontal)
            {
                case SKAlignment.Center:
                    x = (dest.Width - width) / 2;
                    break;
                case SKAlignment.Start:
                    break;
                case SKAlignment.End:
                    x = dest.Width - width;
                    break;
            }
            switch (vertical)
            {
                case SKAlignment.Center:
                    y = (dest.Height - height) / 2;
                    break;
                case SKAlignment.Start:
                    break;
                case SKAlignment.End:
                    y = dest.Height - height;
                    break;
            }
            x += dest.Left;
            y += dest.Top;

            return new SKRect(x, y, x + width, y + height);
        }
    }
}
