using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;

using GIFView = Additel.Forms.Controls.GIFView;
using NativeGIFView = Additel.SkiaViews.GIFView;

namespace Additel.Forms.Renderers
{
    public class GIFViewRenderer : ViewRenderer<GIFView, NativeGIFView>
    {
        protected override NativeGIFView CreateNativeControl()
        {
            var control = new NativeGIFView();
            return control;
        }

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
                    var control = CreateNativeControl();
                    SetNativeControl(control);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}
