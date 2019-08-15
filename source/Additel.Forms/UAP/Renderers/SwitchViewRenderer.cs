using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Xamarin.Forms.Platform.UWP;

using ColorConverter = Additel.Forms.Converters.ColorConverter;
using NativeSwitchView = Additel.SkiaViews.SwitchView;
using SwitchView = Additel.Forms.Views.SwitchView;

namespace Additel.Forms.Renderers
{
    public class SwitchViewRenderer : ViewRenderer<SwitchView, NativeSwitchView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SwitchView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var control = new NativeSwitchView();
                    SetNativeControl(control);
                }
                else
                {
                    Control.ClearValue(NativeSwitchView.ValueProperty);
                    Control.ClearValue(NativeSwitchView.OnColorProperty);
                }

                var binding1 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(SwitchView.Value)), Mode = BindingMode.TwoWay };
                var binding2 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(SwitchView.OnColor)), Converter = new ColorConverter() };
                Control.SetBinding(NativeSwitchView.ValueProperty, binding1);
                Control.SetBinding(NativeSwitchView.OnColorProperty, binding2);
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
                Control.ClearValue(NativeSwitchView.ValueProperty);
                Control.ClearValue(NativeSwitchView.OnColorProperty);
            }

            base.Dispose(disposing);
        }
    }
}
