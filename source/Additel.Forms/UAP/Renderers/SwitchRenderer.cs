using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;

using Switch = Additel.Forms.Controls.Switch;
using NativeSwitch = Additel.Controls.Switch;
using ColorConverter = Additel.Forms.Converters.ColorConverter;

namespace Additel.Forms.Renderers
{
    public class SwitchRenderer : ViewRenderer<Switch, NativeSwitch>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var control = new NativeSwitch();
                    SetNativeControl(control);
                }
                else
                {
                    Control.ClearValue(NativeSwitch.StateProperty);
                    Control.ClearValue(NativeSwitch.OnColorProperty);
                }

                var binding1 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(Switch.IsChecked)), Mode = BindingMode.TwoWay };
                var binding2 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(Switch.OnColor)), Converter = new ColorConverter() };
                Control.SetBinding(NativeSwitch.StateProperty, binding1);
                Control.SetBinding(NativeSwitch.OnColorProperty, binding2);
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
                Control.ClearValue(NativeSwitch.StateProperty);
                Control.ClearValue(NativeSwitch.OnColorProperty);
            }

            base.Dispose(disposing);
        }
    }
}
