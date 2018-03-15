using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class IsEqualOrGreaterThanVisibleConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrGreaterThanVisibleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = System.Convert.ToInt32(value);
            int compareToValue = System.Convert.ToInt32(parameter);

            return intValue >= compareToValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
