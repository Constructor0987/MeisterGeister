using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

using System.Windows.Media;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color = (value == null) ? new Color() { R = 255, G = 255, B = 255 } : ((Color)value);
            return ((Brush)new SolidColorBrush(color));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // keine Rückkonvertierung vorgesehen
            return null;
        }
    }

    //public class InverseBooleanConverter : IValueConverter
    //{
    //    #region IValueConverter Members

    //    public object Convert(object value, Type targetType, object parameter,
    //        System.Globalization.CultureInfo culture)
    //    {
    //        if (targetType != typeof(bool) && targetType != typeof(bool?))
    //            throw new InvalidOperationException("The target must be a boolean");

    //        return !(bool)value;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter,
    //        System.Globalization.CultureInfo culture)
    //    {
    //        return Convert(value, targetType, parameter, culture);
    //    }

    //    #endregion
    //}
}
