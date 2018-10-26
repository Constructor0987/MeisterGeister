using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class MerkmaleListeToBrushColor : IValueConverter
    {
        public static readonly IValueConverter Instance = new MerkmaleListeToBrushColor();

        private class merkmal
        {
            public string b;
            public System.Windows.Media.SolidColorBrush c;
        }
        private List<merkmal> möglicheMerkmale = new List<merkmal>(){
                        new merkmal{ b="Antimagie", c=System.Windows.Media.Brushes.Gray},
                        new merkmal{ b="Beschwörung", c=System.Windows.Media.Brushes.DarkGray },
                        new merkmal{ b="Dämonisch", c=System.Windows.Media.Brushes.Black },
                        new merkmal{ b="Eigenschaften", c=System.Windows.Media.Brushes.Orange},
                        new merkmal{ b="Einfluss", c=System.Windows.Media.Brushes.Yellow },
                        new merkmal{ b="Elementar", c=System.Windows.Media.Brushes.Turquoise },
                        new merkmal{ b="Form", c=System.Windows.Media.Brushes.Brown },
                        new merkmal{ b="Geisterwesen", c=System.Windows.Media.Brushes.LightCyan },
                        new merkmal{ b="Heilung", c=System.Windows.Media.Brushes.Cyan },
                        new merkmal{ b="Hellsicht", c=System.Windows.Media.Brushes.White },
                        new merkmal{ b="Herbeirufung", c=System.Windows.Media.Brushes.Ivory },
                        new merkmal{ b="Herrschaft", c=System.Windows.Media.Brushes.DarkSeaGreen },
                        new merkmal{ b="Illusion", c=System.Windows.Media.Brushes.Linen },
                        new merkmal{ b="Kraft", c=System.Windows.Media.Brushes.Crimson },
                        new merkmal{ b="Limbus", c=System.Windows.Media.Brushes.DarkBlue },
                        new merkmal{ b="Metamagie", c=System.Windows.Media.Brushes.Firebrick },
                        new merkmal{ b ="Objekt", c=System.Windows.Media.Brushes.Peru },
                        new merkmal{ b="Schaden", c=System.Windows.Media.Brushes.Red },
                        new merkmal{ b="Teletinese", c=System.Windows.Media.Brushes.Silver },
                        new merkmal{ b="Temporal", c=System.Windows.Media.Brushes.WhiteSmoke },
                        new merkmal{ b="Umwelt", c=System.Windows.Media.Brushes.Green },
                        new merkmal{ b="Verständigung", c=System.Windows.Media.Brushes.HotPink }};

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<string> lstMerkmale = new List<string>();
            lstMerkmale = (value as string).Trim(' ').Split(',').ToList();

            int compareToValue = System.Convert.ToInt32(parameter);
            if (compareToValue >= lstMerkmale.Count()) return System.Windows.Media.Brushes.Transparent;
            
            return möglicheMerkmale.FirstOrDefault(t => lstMerkmale[compareToValue].Trim().StartsWith(t.b)) != null ? 
                möglicheMerkmale.FirstOrDefault(t => lstMerkmale[compareToValue].Trim().StartsWith(t.b)).c : System.Windows.Media.Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
