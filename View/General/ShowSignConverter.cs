using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    public class ShowSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool sign;
            if (value is int)
                sign = Math.Sign((int)value) != -1;
            else if (value is double)
                sign = Math.Sign((double)value) != -1;
            else return null;
            return sign ? "+" + value.ToString() : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String)
                return ((string)value).Substring(1);
            return null;
        }
    }
}
