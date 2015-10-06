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
            {
                String s = (String)value;
                if (targetType == typeof(int))
                    return Int32.Parse(s);
                else if (targetType == typeof(double))
                    return Double.Parse(s);
            }
            else if (value.GetType() == targetType)
                return value;
            return null;
        }
    }
}
