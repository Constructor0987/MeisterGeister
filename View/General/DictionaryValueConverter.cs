using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    public class DictionaryValueConverter : IValueConverter
    {
        /// <summary>
        /// Store the key type.
        /// Setting this property is needed if your key is an enum and  
        /// </summary>
        public Type KeyType { get; set; }

        /// <summary>
        /// Store the key-value pairs for the conversion
        /// </summary>
        public Dictionary<object, object> Values { get; set; }

        public DictionaryValueConverter()
        {
            Values = new Dictionary<object, object>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // if key type is not set, get it from the first dictionary value, usually it's the same for all the keys
            if (KeyType == null)
            {
                KeyType = Values.Keys.First().GetType();
            }

            // if key type is an enum
            if (KeyType.IsEnum)
            {
                // convert integral value to enum value
                value = Enum.ToObject(KeyType, value);
            }

            // if dictionary contains the requested key
            if (Values.ContainsKey(value))
            {
                // return the relevant value
                return Values[value];
            }

            // otherwise, don't return a value, this will fall back to the binding FallbackValue
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // no support for converting back
            return DependencyProperty.UnsetValue;
        }
    }
}