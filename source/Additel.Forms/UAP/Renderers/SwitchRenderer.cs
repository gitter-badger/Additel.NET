using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;

using SwitchView = Additel.Forms.Controls.SwitchView;
using NativeSwitchView = Additel.SkiaViews.SwitchView;
using ColorConverter = Additel.Forms.Converters.ColorConverter;

namespace Additel.Forms.Renderers
{
    public class SwitchRenderer : ViewRenderer<SwitchView, NativeSwitchView>
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
                    Control.ClearValue(NativeSwitchView.StateProperty);
                    Control.ClearValue(NativeSwitchView.OnColorProperty);
                }

                var binding1 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(SwitchView.IsChecked)), Mode = BindingMode.TwoWay };
                var binding2 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(SwitchView.OnColor)), Converter = new ColorConverter() };
                Control.SetBinding(NativeSwitchView.StateProperty, binding1);
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
                Control.ClearValue(NativeSwitchView.StateProperty);
                Control.ClearValue(NativeSwitchView.OnColorProperty);
            }

            base.Dispose(disposing);
        }
    }
}
