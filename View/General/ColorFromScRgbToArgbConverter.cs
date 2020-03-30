using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace MeisterGeister.View.General
{
    public class ColorFromScRgbToArgbConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            if (value is Color)
            {
                //return Colors.Yellow;// 
                Color color = (Color)value;

                return color;// Color.FromArgb(
                     //System.Convert.ToByte(color.A),
                     //System.Convert.ToByte(color.G),
                     //System.Convert.ToByte(color.B),
                     //System.Convert.ToByte(color.R));
            }
            // You can support here more source types if you wish
            // For the example I throw an exception

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Check if which brush it is (if its one),
            // get its Color-value and return it.
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            if (value is Color)
            {
                return value;
                Color color = (Color)value;

                return Color.FromScRgb(
                    //Color.FromArgb(
                    (color).ScA,
                    (color).ScG,
                    (color).ScB,
                    (color).ScR);
            }
            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }
    }
}
