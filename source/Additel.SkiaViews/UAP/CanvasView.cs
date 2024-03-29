﻿using System;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Additel.SkiaViews
{
    partial class CanvasView : SKXamlCanvas
    {
        public CanvasView()
        {
            Loaded += OnCanvasLoaded;
            Unloaded += OnCanvasUnloaded;
            PointerPressed += OnPointerPressed;
            PointerMoved += OnPointerMoved;
            PointerReleased += OnPointerReleased;
            PointerCanceled += OnPointerCancelled;
        }

        private void OnCanvasUnloaded(object sender, RoutedEventArgs e)
        {
            OnUnloaded();
            // HACK: UWP 中无 Dispose 方法, 在 Unloaded 后调用清理资源的方法即可
            OnDispose();
        }

        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {
            OnLoaded();
        }

        private void OnPointerCancelled(object sender, PointerRoutedEventArgs e)
        {
            if (!CapturePointer(e.Pointer))
                return;

            var id = e.Pointer.PointerId;
            var current = e.GetCurrentPoint(this).Position;
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));

            var handled = OnTouch(id, SkTouchAction.Cancelled, location);

            ReleasePointerCapture(e.Pointer);

            e.Handled = handled;
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (!CapturePointer(e.Pointer))
                return;

            var id = e.Pointer.PointerId;
            var current = e.GetCurrentPoint(this).Position;
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));

            var handled = OnTouch(id, SkTouchAction.Released, location);

            ReleasePointerCapture(e.Pointer);

            e.Handled = handled;
        }

        private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (!CapturePointer(e.Pointer) ||
                !e.Pointer.IsInContact)
                return;

            var id = e.Pointer.PointerId;
            var current = e.GetCurrentPoint(this).Position;
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));

            var handled = OnTouch(id, SkTouchAction.Moved, location);

            e.Handled = handled;
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var id = e.Pointer.PointerId;
            var current = e.GetCurrentPoint(this).Position;
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));

            var handled = OnTouch(id, SkTouchAction.Pressed, location);

            CapturePointer(e.Pointer);

            e.Handled = handled;
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
            var converted = value * Dpi;

            return (float)converted;
        }

        protected double FromPixels(float value)
        {
            var converted = value / Dpi;

            return converted;
        }
    }
}
