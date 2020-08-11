using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

using System.Windows.Media;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class ColorStringToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return ((Brush)new SolidColorBrush(new Color() { R = 255, G = 255, B = 255 }));
            byte[] colorB = StrToByteArray((value as string).Substring(3));
            byte r = colorB[0];
            byte g = colorB[1];
            byte b = colorB[2];

            Color color = new Color() { R = r, G = g, B = b };
            return ((Brush)new SolidColorBrush(color));
        }
        public static byte[] StrToByteArray(string str)
        {
            Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
            for (int i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte)i);

            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // keine Rückkonvertierung vorgesehen
            return null;
        }
    }

    //public class InverseBooleanConverter : IValueConverter
    //{
    //    #region IValueConverter Members

    //    public object Convert(object value, Type targetType, object parameter,
    //        System.Globalization.CultureInfo culture)
    //    {
    //        if (targetType != typeof(bool) && targetType != typeof(bool?))
    //            throw new InvalidOperationException("The target must be a boolean");

    //        return !(bool)value;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter,
    //        System.Globalization.CultureInfo culture)
    //    {
    //        return Convert(value, targetType, parameter, culture);
    //    }

    //    #endregion
    //}
}
