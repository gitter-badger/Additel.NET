using Additel.Core;
using Android.Content;
using Android.Views;
using SkiaSharp;
using SkiaSharp.Views.Android;
using NativeSKCanvasView = SkiaSharp.Views.Android.SKCanvasView;

namespace Additel.Controls
{
    partial class SKCanvasView : NativeSKCanvasView
    {
        public SKCanvasView(Context context)
            : base(context)
        {
            Touch += OnTouch;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            return base.OnTouchEvent(e);
        }

        private void OnTouch(object sender, TouchEventArgs e)
        {
            var evt = e.Event;
            var index = evt.ActionIndex;

            var id = evt.GetPointerId(index);
            var location = new SKPoint(evt.GetX(index), evt.GetY(index));

            switch (evt.ActionMasked)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown:
                    {
                        var handled = OnTouch(id, SKTouchAction.Pressed, location);
                        e.Handled = handled;
                        break;
                    }
                case MotionEventActions.Move:
                    {
                        var count = evt.PointerCount;
                        for (index = 0; index < count; index++)
                        {
                            id = evt.GetPointerId(index);
                            location = new SKPoint(evt.GetX(index), evt.GetY(index));
                            var handled = OnTouch(id, SKTouchAction.Moved, location);
                            e.Handled = handled;
                        }
                        break;
                    }
                case MotionEventActions.Up:
                case MotionEventActions.PointerUp:
                    {
                        var handled = OnTouch(id, SKTouchAction.Released, location);
                        e.Handled = handled;
                        break;
                    }
                case MotionEventActions.Cancel:
                    {
                        var handled = OnTouch(id, SKTouchAction.Cancelled, location);
                        e.Handled = handled;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            OnPaintSurface(e.Surface, e.Info);
        }

        protected void InvalidateSurface()
        {
            Invalidate();
        }

        protected float ToPixels(double value)
        {
            return Context.ToPixels(value);
        }

        protected double FromPixels(float value)
        {
            return Context.FromPixels(value);
        }
    }
}
