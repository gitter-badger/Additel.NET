using Additel.Core;
using System;
using Xamarin.Forms;

namespace Additel.Forms.Views
{
    public class SwitchView : View
    {
        public event EventHandler<ValueEventArgs<bool>> ValueChanged;

        public bool Value
        {
            get => (bool)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Using a BindableProperty as the backing store for <see cref="Value"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly BindableProperty ValueProperty
            = BindableProperty.Create(
                nameof(Value),
                typeof(bool),
                typeof(SwitchView),
                false,
                propertyChanged: OnValueChanged,
                defaultBindingMode: BindingMode.TwoWay);

        private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (SwitchView)bindable;
            var value = (bool)newValue;

            view.ValueChanged?.Invoke(view, new ValueEventArgs<bool>(value));
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
