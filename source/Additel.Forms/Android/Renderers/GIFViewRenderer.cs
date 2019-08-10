using System.ComponentModel;
using Android.Content;
using Xamarin.Forms.Platform.Android;

using GIFView = Additel.Forms.Controls.GIFView;
using NativeGIFView = Additel.SkiaViews.GIFView;

namespace Additel.Forms.Renderers
{
    public class GIFViewRenderer : ViewRenderer<GIFView, NativeGIFView>
    {
        public GIFViewRenderer(Context context)
            : base(context)
        {

        }

        protected override NativeGIFView CreateNativeControl()
        {
            var control = new NativeGIFView(Context);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
