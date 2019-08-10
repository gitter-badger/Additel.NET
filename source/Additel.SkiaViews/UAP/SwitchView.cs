using Windows.UI;
using Windows.UI.Xaml;

namespace Additel.SkiaViews
{
    partial class SwitchView
    {
        public bool State
        {
            get => (bool)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="State"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register(
                nameof(State),
                typeof(bool),
                typeof(SwitchView),
                new PropertyMetadata(false, OnStateChanged));

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (SwitchView)d;

            view.OnStateChanged();
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

            view.OnOnColorChanged();
        }
    }
}
