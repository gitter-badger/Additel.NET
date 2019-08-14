using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

using SwitchView = Additel.Forms.Controls.SwitchView;
using NativeSwitchView = Additel.SkiaViews.SwitchView;
using Additel.Core;

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

            Control.Value = Element.Value;
        }

        private void UpdateOnColor()
        {
            if (Control == null || Element == null)
                return;

            Control.OnColor = Element.OnColor.ToUIColor();
        }

        private void OnElementValueChanged(object sender, ValueEventArgs<bool> e)
        {
            if (Control == null || Element == null)
                return;

            Control.Value = e.Value;
        }

        private void OnControlValueChanged(object sender, ValueEventArgs<bool> e)
        {
            ElementController.SetValueFromRenderer(SwitchView.ValueProperty, e.Value);
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
                e.OldElement.ValueChanged -= OnElementValueChanged;
            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var control = CreateNativeControl();
                    control.ValueChanged += OnControlValueChanged;
                    SetNativeControl(control);
                }

                e.NewElement.ValueChanged += OnElementValueChanged;
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
                Control.ValueChanged -= OnControlValueChanged;
            }

            base.Dispose(disposing);
        }
    }
}
