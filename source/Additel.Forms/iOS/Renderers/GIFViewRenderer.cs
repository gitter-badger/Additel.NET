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

                UpdateSource();
                UpdateStretch();
            }
        }

        private void UpdateStretch()
        {
            Control.Stretch = Element.Stretch.ToSKStretch();
        }

        private void UpdateSource()
        {
            Control.Source = Element.Source;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == GIFView.SourceProperty.PropertyName)
                UpdateSource();
            else if (e.PropertyName == GIFView.StretchProperty.PropertyName)
                UpdateStretch();
        }
    }
}
