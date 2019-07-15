using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

using Color = Xamarin.Forms.Color;
using NativeColor = System.Windows.Media.Color;
using Application = Xamarin.Forms.Application;
using NativeApplication = System.Windows.Application;
using IValueConverter = Xamarin.Forms.IValueConverter;
using INativeValueConverter = System.Windows.Data.IValueConverter;

namespace Additel.Forms.Converters
{
    public class ColorConverter : INativeValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            var defaultColorKey = (string)parameter;

            var defaultBrush = defaultColorKey != null ? (NativeColor)NativeApplication.Current.Resources[defaultColorKey] : Colors.Transparent;

            return color == Color.Default ? defaultBrush : color.ToMediaColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
