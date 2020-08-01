using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class IsEqualConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = ((value == null) ? 0 : System.Convert.ToInt32(value));
            int compareToValue = System.Convert.ToInt32(parameter);

            return intValue = compareToValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
