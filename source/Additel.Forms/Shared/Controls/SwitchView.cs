using System;
using Xamarin.Forms;

namespace Additel.Forms.Controls
{
    public class SwitchView : View
    {
        public event EventHandler<StateEventArgs> StateChanged;

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        /// Using a BindableProperty as the backing store for <see cref="IsChecked"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty
            = BindableProperty.Create(
                nameof(IsChecked),
                typeof(bool),
                typeof(SwitchView),
                false,
                propertyChanged: OnStateChanged,
                defaultBindingMode: BindingMode.TwoWay);

        private static void OnStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (SwitchView)bindable;
            var value = (bool)newValue;

            view.StateChanged?.Invoke(view, new StateEventArgs(value));
        }

        public Color OnColor
        {
            get => (Color)GetValue(OnColorProperty);
            set => SetValue(OnColorProperty, value);
        }

        /// <summary>
        /// Using a BindableProperty as the backing store for <see cref="OnColor"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly BindableProperty OnColorProperty
            = BindableProperty.Create(
                nameof(OnColor),
                typeof(Color),
                typeof(SwitchView),
                Color.FromHex("#FF4CD964"));

    }
}
