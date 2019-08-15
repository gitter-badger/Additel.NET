using Additel.Forms.Converters;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Xamarin.Forms.Platform.UWP;

using GIFView = Additel.Forms.Views.GIFView;
using NativeGIFView = Additel.SkiaViews.GIFView;

namespace Additel.Forms.Renderers
{
    public class GIFViewRenderer : ViewRenderer<GIFView, NativeGIFView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<GIFView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var control = new NativeGIFView();
                    SetNativeControl(control);
                }
                else
                {
                    Control.ClearValue(NativeGIFView.SourceProperty);
                    Control.ClearValue(NativeGIFView.StretchProperty);
                }

                var binding1 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(GIFView.Source)) };
                var binding2 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(GIFView.Stretch)), Converter = new StretchConverter() };
                Control.SetBinding(NativeGIFView.SourceProperty, binding1);
                Control.SetBinding(NativeGIFView.StretchProperty, binding2);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Control != null)
            {
                Control.ClearValue(NativeGIFView.SourceProperty);
                Control.ClearValue(NativeGIFView.StretchProperty);
            }

            base.Dispose(disposing);
        }
    }
}
