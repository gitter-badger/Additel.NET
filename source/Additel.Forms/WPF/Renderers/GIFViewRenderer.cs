using Additel.Forms.Converters;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Xamarin.Forms.Platform.WPF;

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
                    BindingOperations.ClearAllBindings(Control);
                }

                var binding1 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(GIFView.Source)) };
                var binding2 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(GIFView.Stretch)), Converter = new StretchConverter() };
                BindingOperations.SetBinding(Control, NativeGIFView.SourceProperty, binding1);
                BindingOperations.SetBinding(Control, NativeGIFView.StretchProperty, binding2);
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
                BindingOperations.ClearAllBindings(Control);
            }

            base.Dispose(disposing);
        }
    }
}
