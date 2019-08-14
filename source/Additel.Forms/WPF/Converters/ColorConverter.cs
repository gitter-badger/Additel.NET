using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Xamarin.Forms.Platform.WPF;

using Color = Xamarin.Forms.Color;
using NativeColor = System.Windows.Media.Color;

namespace Additel.Forms.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var forms = (Color)value;
            var key = (string)parameter;
            var @default = key != null ? (NativeColor)Application.Current.Resources[key] : Colors.Transparent;
            var native = forms == Color.Default ? @default : forms.ToMediaColor();

            return native;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
