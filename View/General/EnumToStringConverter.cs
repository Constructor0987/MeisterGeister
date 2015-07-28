using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    public class EnumToStringConverter : IValueConverter
    {
        private static string GetDescription(FieldInfo fieldInfo)
        {
            var descriptionAttribute =
              (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
            return descriptionAttribute != null ? descriptionAttribute.Description : fieldInfo.Name;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !value.GetType().IsEnum) return String.Empty;
            return GetDescription(value.GetType().GetField(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is String) || String.IsNullOrEmpty((String)value))
                return null;
            foreach (FieldInfo info in targetType.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (GetDescription(info) == (String)value)
                    return info.GetValue(value.GetType());
            }
            return null;
        }
    }
}
