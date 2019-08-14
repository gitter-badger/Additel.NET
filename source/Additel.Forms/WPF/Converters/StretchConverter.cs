using System;
using System.Globalization;
using System.Windows.Data;

namespace Additel.Forms.Converters
{
    public class StretchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var forms = (Stretch)value;
            var native = forms.ToSKStretch();

            return native;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
