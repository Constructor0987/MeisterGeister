using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;


namespace MeisterGeister.Model
{
    public partial class Einstellung
    {
        public T Get<T>()
        {
            if(typeof(T).IsAssignableFrom(Type))
                return (T)Value;
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
                throw new InvalidCastException();
            if (converter.CanConvertFrom(Type))
                return (T)converter.ConvertFrom(Value);
            throw new InvalidCastException(String.Format("{2}: {0} kann nicht in {1} umgewandelt werden.", Type, typeof(T), Name));
        }

        public void Set<T>(T value)
        {
            if (Type.IsAssignableFrom(typeof(T)))
            {
                Value = value;
                return;
            }
            if (Converter.CanConvertFrom(typeof(T)))
                Value = Converter.ConvertFrom(value);
            throw new InvalidCastException(String.Format("{2}: {1} kann nicht in {0} umgewandelt werden.", Type, typeof(T), Name));
        }

        public object Value
        {
            get
            {
                if (Type == typeof(bool))
                {
                    if (Wert == "1")
                        return true;
                    if (Wert == "0")
                        return false;
                }
                else if (Type == typeof(DateTime))
                {
                    return DateTime.Parse(Wert);
                }
                return Converter.ConvertFromInvariantString(Wert);
            }
            set { Wert = Converter.ConvertToInvariantString(value); OnChanged("Value"); }
        }

        private TypeConverter Converter
        {
            get { return TypeDescriptor.GetConverter(Type); }
        }

        [DependentProperty("Typ")]
        public Type Type
        {
            get {
                if (Typ == "String")
                    return typeof(String);
                if (Typ == "Integer")
                    return typeof(int);
                if (Typ == "Boolean")
                    return typeof(bool);
                if (Typ == "Double" || Typ == "Float")
                    return typeof(double);
                if (Typ == "DateTime")
                    return typeof(DateTime);
                return Type.GetType(Typ, true, true); 
            }
        }

    }
}
