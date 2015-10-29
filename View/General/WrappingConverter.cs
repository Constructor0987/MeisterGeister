using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MeisterGeister.View.Karte
{
    //[ValueConversion(typeof(Point), typeof(Point))]
    /// <summary>
    /// Hat eine public Property Converter. Dieser wird für die Umwandlung verwendet.
    /// </summary>
    public class WrappingConverter : IValueConverter
    {
        public IValueConverter Converter
        {
            get;
            set;
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (Converter == null)
                return null;
            return Converter.Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (Converter == null)
                return null;
            return Converter.ConvertBack(value, targetType, parameter, culture);
        }

        #endregion
    }
}
