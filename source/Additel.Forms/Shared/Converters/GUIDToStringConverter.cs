using System;
using System.Globalization;
using Xamarin.Forms;

namespace Additel.Forms.Converters
{
    public class GUIDToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((Guid)value).ToString().ToUpper();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Guid.Parse((string)value);
    }
}
