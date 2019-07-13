using Additel.Controls;
using Additel.Forms.Controls;
using Android.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Switch = Additel.Forms.Controls.Switch;
using NativeSwitch = Additel.Controls.Switch;
using SwitchRenderer = Additel.Forms.Renderers.SwitchRenderer;
using SwitchStateEventArgs = Additel.Forms.SwitchStateEventArgs;
using NativeSwitchStateEventArgs = Additel.Controls.SwitchStateEventArgs;

[assembly: ExportRenderer(typeof(Switch), typeof(SwitchRenderer))]
namespace Additel.Forms.Renderers
{
    public class SwitchRenderer : ViewRenderer<Switch, NativeSwitch>
    {
        private IViewController ViewController
            => Element;

        public SwitchRenderer(Context context)
            : base(context)
        {

        }

        private void UpdateOnColor()
        {
            if (Element == null)
                return;

            Control.OnColor = Element.OnColor.ToAndroid();
        }

        private void OnElementStateChanged(object sender, SwitchStateEventArgs e)
        {
            Control.State = Element.IsChecked;
        }

        private void OnControlStateChanged(object sender, NativeSwitchStateEventArgs e)
        {
            ViewController.SetValueFromRenderer(Switch.IsCheckedProperty, e.State);
        }

        protected override NativeSwitch CreateNativeControl()
        {
            return new NativeSwitch(Context);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.StateChanged -= OnElementStateChanged;
            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var control = CreateNativeControl();
                    control.StateChanged += OnControlStateChanged;
                    SetNativeControl(control);
                }

                e.NewElement.StateChanged += OnElementStateChanged;
                UpdateOnColor();
                Control.State = e.NewElement.IsChecked;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Switch.OnColorProperty.PropertyName)
                UpdateOnColor();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Control != null)
            {
                if (Element != null)
                {
                    Element.StateChanged -= OnElementStateChanged;
                }

                Control.StateChanged -= OnControlStateChanged;
            }

            base.Dispose(disposing);
        }
    }
}
