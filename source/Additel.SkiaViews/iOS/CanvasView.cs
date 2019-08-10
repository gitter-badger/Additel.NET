using CoreGraphics;
using Foundation;
using SkiaSharp;
using SkiaSharp.Views.iOS;
using System.Diagnostics;
using System.Linq;
using UIKit;

namespace Additel.SkiaViews
{
    partial class CanvasView : SKCanvasView
    {
        public CanvasView()
            : base()
        {
            Opaque = false;
            MultipleTouchEnabled = true;
        }

        public CanvasView(CGRect frame)
            : base(frame)
        {
            Opaque = false;
            MultipleTouchEnabled = true;
        }

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();

            OnLoaded();
        }

        public override void RemoveFromSuperview()
        {
            base.RemoveFromSuperview();

            OnUnloaded();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            foreach (var touch in touches.Cast<UITouch>())
            {
                if (!OnTouch(SkTouchAction.Pressed, touch))
                {
                    MultipleTouchEnabled = false;
                }
            }

            Debug.WriteLine(nameof(TouchesBegan));
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            foreach (var touch in touches.Cast<UITouch>())
            {
                if (!OnTouch(SkTouchAction.Moved, touch))
                {
                    MultipleTouchEnabled = false;
                }
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            foreach (var touch in touches.Cast<UITouch>())
            {
                if (!OnTouch(SkTouchAction.Released, touch))
                {
                    MultipleTouchEnabled = false;
                }
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            foreach (var touch in touches.Cast<UITouch>())
            {
                if (!OnTouch(SkTouchAction.Cancelled, touch))
                {
                    MultipleTouchEnabled = false;
                }
            }
        }

        private bool OnTouch(SkTouchAction action, UITouch touch)
        {
            var id = touch.Handle.ToInt64();
            var current = touch.LocationInView(this);
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));
            var handled = OnTouch(id, action, location);

            return handled;
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            OnPaintSurface(e.Surface, e.Info);
        }

        protected void InvalidateSurface()
        {
            SetNeedsDisplay();
        }

        protected float ToPixels(double value)
        {
            var converted = (float)(value * ContentScaleFactor);
            return converted;
        }

        protected double FromPixels(float value)
        {
            var converted = value / ContentScaleFactor;
            return converted;
        }
    }
}
