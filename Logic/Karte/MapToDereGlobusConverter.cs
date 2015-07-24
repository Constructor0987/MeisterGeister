using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MeisterGeister.Logic.Karte
{
    [ValueConversion(typeof(Point), typeof(Point))]
    public class MapToDereGlobusConverter : IValueConverter
    {
        DereGlobusToMapConverter conv;
        public MapToDereGlobusConverter()
        {
            conv = new DereGlobusToMapConverter();
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return conv.ConvertBack(value, targetType, parameter, culture);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return conv.Convert(value, targetType, parameter, culture);
        }
    }
}
