using Xamarin.Forms;

namespace Additel.Forms.Controls
{
    public class GIFView : View
    {
        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Using a BindableProperty as the backing store for <see cref="Source"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly BindableProperty SourceProperty
            = BindableProperty.Create(
                nameof(Source),
                typeof(string),
                typeof(GIFView));

        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        /// <summary>
        /// Using a BindableProperty as the backing store for <see cref="Stretch"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly BindableProperty StretchProperty
            = BindableProperty.Create(
                nameof(Stretch),
                typeof(Stretch),
                typeof(GIFView));

    }
}
