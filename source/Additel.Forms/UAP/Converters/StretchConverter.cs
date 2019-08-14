using System;
using Windows.UI.Xaml.Data;

namespace Additel.Forms.Converters
{
    public class StretchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var forms = (Stretch)value;
            var native = forms.ToSKStretch();

            return native;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
