using Windows.UI;
using Windows.UI.Xaml;

namespace Additel.SkiaViews
{
    partial class SwitchView
    {
        public bool Value
        {
            get => (bool)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="Value"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(bool),
                typeof(SwitchView),
                new PropertyMetadata(false, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (SwitchView)d;
            view.UpdateValue();
        }

        public Color OnColor
        {
            get => (Color)GetValue(OnColorProperty);
            set => SetValue(OnColorProperty, value);
        }

        /// <summary>
        ///  Using a DependencyProperty as the backing store for <see cref="OnColor"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty OnColorProperty =
            DependencyProperty.Register(
                "OnColor",
                typeof(Color),
                typeof(SwitchView),
                new PropertyMetadata(Colors.GreenActive.ToColor(), OnOnColorChanged));

        private static void OnOnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (SwitchView)d;
            view.UpdateOnColor();
        }
    }
}
