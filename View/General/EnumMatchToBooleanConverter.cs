using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumMatchToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                     StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();
            if(targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                targetType = Nullable.GetUnderlyingType(targetType);
            if (useValue)
                return Enum.Parse(targetType, targetValue);

            return null;
        }
    }
}
