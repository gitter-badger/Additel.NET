using Android.Content;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using SwitchView = Additel.Forms.Controls.SwitchView;
using NativeSwitchView = Additel.SkiaViews.SwitchView;
using NativeStateEventArgs = Additel.SkiaViews.StateEventArgs;

namespace Additel.Forms.Renderers
{
    public class SwitchViewRenderer : ViewRenderer<SwitchView, NativeSwitchView>
    {
        private IViewController ViewController
            => Element;

        public SwitchViewRenderer(Context context)
            : base(context)
        {
            AutoPackage = false;
        }

        private void UpdateState()
        {
            if (Control == null || Element == null)
                return;

            Control.State = Element.IsChecked;
        }

        private void UpdateOnColor()
        {
            if (Control == null || Element == null)
                return;

            Control.OnColor = Element.OnColor.ToAndroid();
        }

        private void OnElementStateChanged(object sender, StateEventArgs e)
        {
            Control.State = Element.IsChecked;
        }

        private void OnControlStateChanged(object sender, NativeStateEventArgs e)
        {
            ViewController.SetValueFromRenderer(SwitchView.IsCheckedProperty, e.State);
        }

        protected override NativeSwitchView CreateNativeControl()
        {
            var control = new NativeSwitchView(Context);
            return control;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SwitchView> e)
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
                UpdateState();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == SwitchView.OnColorProperty.PropertyName)
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
