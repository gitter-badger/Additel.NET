using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Xamarin.Forms.Platform.UWP;

using Color = Xamarin.Forms.Color;
using NativeColor = Windows.UI.Color;

namespace Additel.Forms.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var forms = (Color)value;
            var key = (string)parameter;
            var @default = key != null ? (NativeColor)Application.Current.Resources[key] : Colors.Transparent;
            var native = forms == Color.Default ? @default : forms.ToWindowsColor();

            return native;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
