using CoreGraphics;
using Foundation;
using SkiaSharp;
using SkiaSharp.Views.iOS;
using System.Diagnostics;
using System.Linq;
using UIKit;
using NativeSKCanvasView = SkiaSharp.Views.iOS.SKCanvasView;

namespace Additel.Controls
{
    partial class SKCanvasView : NativeSKCanvasView
    {
        //private readonly UITouchGestureRecognizer _touchRecognizer;

        private bool _disposed;

        public SKCanvasView()
            : base()
        {
            Opaque = false;
            //_touchRecognizer = new UITouchGestureRecognizer((action, touch) => OnTouch(action, touch));

            //AddGestureRecognizer(_touchRecognizer);
            MultipleTouchEnabled = true;
        }

        public SKCanvasView(CGRect frame)
            : base(frame)
        {
            Opaque = false;
            //_touchRecognizer = new UITouchGestureRecognizer((action, touch) => OnTouch(action, touch));

            //AddGestureRecognizer(_touchRecognizer);
            MultipleTouchEnabled = true;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            foreach (var touch in touches.Cast<UITouch>())
            {
                if (!OnTouch(SKTouchAction.Pressed, touch))
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
                if (!OnTouch(SKTouchAction.Moved, touch))
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
                if (!OnTouch(SKTouchAction.Released, touch))
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
                if (!OnTouch(SKTouchAction.Cancelled, touch))
                {
                    MultipleTouchEnabled = false;
                }
            }
        }

        private bool OnTouch(SKTouchAction action, UITouch touch)
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

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                //RemoveGestureRecognizer(_touchRecognizer);

                //_touchRecognizer.Dispose();
            }

            base.Dispose(disposing);

            _disposed = true;
        }
    }
}
