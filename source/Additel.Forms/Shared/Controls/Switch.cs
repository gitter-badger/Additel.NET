using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Additel.Forms.Controls
{
    public class Switch : View
    {
        public event EventHandler<SwitchStateEventArgs> StateChanged;

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
                typeof(Switch),
                false,
                propertyChanged: OnStateChanged,
                defaultBindingMode: BindingMode.TwoWay);

        private static void OnStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (Switch)bindable;
            var value = (bool)newValue;

            view.StateChanged?.Invoke(view, new SwitchStateEventArgs(value));
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
                typeof(Switch),
                Color.FromHex("#FF4CD964"));

    }
}
