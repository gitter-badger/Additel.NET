using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.WPF;
using Additel.Forms.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

using SwitchView = Additel.Forms.Controls.SwitchView;
using NativeSwitchView = Additel.SkiaViews.SwitchView;
using ColorConverter = Additel.Forms.Converters.ColorConverter;

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
                    BindingOperations.ClearAllBindings(Control);
                }

                var binding1 = new Binding() { Source = e.NewElement, Path = new PropertyPath(nameof(SwitchView.IsChecked)) };
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
                BindingOperations.ClearAllBindings(Control);
            }

            base.Dispose(disposing);
        }
    }
}
