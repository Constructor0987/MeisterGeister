using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    public class EnumItemsSource : Collection<String>, IValueConverter
    {
        Type type;
        IDictionary<Object, Object> valueToNameMap;
        IDictionary<Object, Object> nameToValueMap;

        public Type Type
        {
            get { return this.type; }
            set
            {
                if (!value.IsEnum)
                    throw new ArgumentException("Type is not an enum.", "value");
                this.type = value;
                Initialize();
            }
        }

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return this.valueToNameMap[value];
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return this.nameToValueMap[value];
        }

        void Initialize()
        {
            this.valueToNameMap = this.type
              .GetFields(BindingFlags.Static | BindingFlags.Public)
              .ToDictionary(fi => fi.GetValue(null), GetDescription);
            this.nameToValueMap = this.valueToNameMap
              .ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
            Clear();
            foreach (String name in this.nameToValueMap.Keys)
                Add(name);
        }

        static Object GetDescription(FieldInfo fieldInfo)
        {
            var descriptionAttribute =
              (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
            return descriptionAttribute != null ? descriptionAttribute.Description : fieldInfo.Name;
        }

    }

}
