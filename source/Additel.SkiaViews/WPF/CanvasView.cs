using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Additel.SkiaViews
{
    partial class CanvasView : SKElement
    {
        public CanvasView()
        {
            Loaded += OnCanvasLoaded;
            Unloaded += OnCanvasUnloaded;
        }

        private void OnCanvasUnloaded(object sender, RoutedEventArgs e)
        {
            OnUnloaded();
        }

        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {
            OnLoaded();
        }

        protected void InvalidateSurface()
        {
            InvalidateVisual();
        }

        protected float ToPixels(double value)
        {
            var scale = VisualTreeHelper.GetDpi(this).PixelsPerDip;
            var converted = value * scale;

            return (float)converted;
        }

        protected double FromPixels(float value)
        {
            var scale = VisualTreeHelper.GetDpi(this).PixelsPerDip;
            var converted = value / scale;

            return converted;
        }

        private void UpdateEnabled()
        {
            IsHitTestVisible = IsEnabled;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == IsEnabledProperty.Name)
                UpdateEnabled();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            OnPaintSurface(e.Surface, e.Info);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            var id = 0L;
            var current = e.GetPosition(this);
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));

            var handled = OnTouch(id, SkTouchAction.Pressed, location);

            CaptureMouse();

            e.Handled = handled;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!IsMouseCaptured ||
                e.LeftButton != MouseButtonState.Pressed)
                return;

            var id = 0L;
            var current = e.GetPosition(this);
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));

            var handled = OnTouch(id, SkTouchAction.Moved, location);

            e.Handled = handled;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (!IsMouseCaptured)
                return;

            var id = 0L;
            var current = e.GetPosition(this);
            var location = new SKPoint(ToPixels(current.X), ToPixels(current.Y));

            var handled = OnTouch(id, SkTouchAction.Released, location);

            ReleaseMouseCapture();

            e.Handled = handled;
        }
    }
}
