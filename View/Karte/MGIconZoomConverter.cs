using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MeisterGeister.View.Karte
{
    [ValueConversion(typeof(double), typeof(double))]
    public class MGIconZoomConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(double) && targetType != typeof(double?))
                throw new InvalidOperationException("The target must be a double");

            return 25.0 * 1.0/(double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }

        #endregion
    }
}
