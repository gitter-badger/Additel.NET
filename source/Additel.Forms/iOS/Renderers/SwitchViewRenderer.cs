using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

using SwitchView = Additel.Forms.Controls.SwitchView;
using NativeSwitchView = Additel.SkiaViews.SwitchView;
using StateEventArgs = Additel.Forms.StateEventArgs;
using NativeStateEventArgs = Additel.SkiaViews.StateEventArgs;

namespace Additel.Forms.Renderers
{
    public class SwitchViewRenderer : ViewRenderer<SwitchView, NativeSwitchView>
    {
        private IElementController ElementController
            => Element;

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

            Control.OnColor = Element.OnColor.ToUIColor();
        }

        private void OnControlStateChanged(object sender, NativeStateEventArgs e)
        {
            ElementController.SetValueFromRenderer(SwitchView.IsCheckedProperty, e.State);
        }

        private void OnElementStateChanged(object sender, StateEventArgs e)
        {
            if (Control == null || Element == null)
                return;

            Control.State = e.IsChecked;
        }

        protected override NativeSwitchView CreateNativeControl()
        {
            var control = new NativeSwitchView();
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

            if (e.PropertyName == Switch.OnColorProperty.PropertyName)
                UpdateOnColor();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Control != null)
            {
                Control.StateChanged -= OnControlStateChanged;
            }

            base.Dispose(disposing);
        }
    }
}
