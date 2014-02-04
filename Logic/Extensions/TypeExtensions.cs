using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.Extensions
{
    public static class TypeExtensions
    {
        public static bool TryConvert(this Type oType, object value, out object ret)
        {
            Type vType = value.GetType();
            if (oType.IsAssignableFrom(vType))
            {
                ret = value;
                return true;
            }
            TypeConverter converter = TypeDescriptor.GetConverter(oType);
            if (converter.CanConvertFrom(vType))
            {
                ret = converter.ConvertFrom(value);
                return true;
            }
            if (oType.IsValueType)
            {
                ret =  Activator.CreateInstance(oType);
                return false;
            }
            ret = null;
            return false;
            //throw new InvalidCastException(String.Format("{2}: {1} kann nicht in {0} umgewandelt werden.", oType, vType, o));
        }
    }
}
