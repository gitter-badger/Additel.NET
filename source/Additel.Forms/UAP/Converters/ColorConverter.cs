using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

using Color = Xamarin.Forms.Color;
using NativeColor = Windows.UI.Color;
using Application = Xamarin.Forms.Application;
using NativeApplication = Windows.UI.Xaml.Application;
using IValueConverter = Xamarin.Forms.IValueConverter;
using INativeValueConverter = Windows.UI.Xaml.Data.IValueConverter;
using Windows.UI;

namespace Additel.Forms.Converters
{
    public class ColorConverter : INativeValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = (Color)value;
            var defaultColorKey = (string)parameter;

            var defaultBrush = defaultColorKey != null ? (NativeColor)NativeApplication.Current.Resources[defaultColorKey] : Colors.Transparent;

            return color == Color.Default ? defaultBrush : color.ToWindowsColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
