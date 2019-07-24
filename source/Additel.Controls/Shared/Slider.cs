using SkiaSharp;

namespace Additel.Controls
{
    public partial class Slider : SKCanvasView
    {
        protected override void OnPaintSurface(SKSurface surface, SKImageInfo info)
        {
            base.OnPaintSurface(surface, info);

            var canvas = surface.Canvas;
            canvas.Clear();

            // 判断画布参数
            if (CanvasSize.Width <= 0 || CanvasSize.Height <= 0)
                return;

            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.IsStroke = false;
                paint.StrokeWidth = 4.0F;
                paint.Color = SKColors.GrayBackground;
                // 轨道
                var x0 = 0.0F;
                var y0 = CanvasSize.Height / 2.0F;
                var x1 = CanvasSize.Width;
                var y1 = y0;
                canvas.DrawLine(x0, y0, x1, y1, paint);
                // 刻度
                var count = 10;
                for (int i = 0; i < count; i++)
                {

                }
            }
        }
    }
}
