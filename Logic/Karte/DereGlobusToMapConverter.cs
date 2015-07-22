using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MeisterGeister.Logic.Karte
{
    [ValueConversion(typeof(Point), typeof(Point))]
    public class DereGlobusToMapConverter : IValueConverter
    {
        private Point p1, p2, p3, l1, l2, l3;
        private double d1, d2;
        public DereGlobusToMapConverter()
        {
            // Dwurinsand
            p1 = new Point(279, 1958);
            l1 = new Point(-7.66832038, 38.96870934);
            // Neu-Bosparan
            p2 = new Point(6513, 10241);
            l2 = new Point(11.45865558, 16.02534243);
            // Gareth
            p3 = new Point(3996, 5271);
            l3 = new Point(3.735098459, 29.79180236);

            d1 = (l2.X - l1.X) / (p2.X - p1.X);
            d2 = (l2.Y - l1.Y) / (p2.Y - p1.Y);
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point map = new Point();
            if(value is Point)
            {
                map.X = (((Point)value).X - l3.X) / d1 + p3.X;
                map.Y = (((Point)value).Y - l3.Y) / d2 + p3.Y;
            }
            return map;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point dg = new Point();
            if (value is Point)
            {
                dg.X = (((Point)value).X - p3.X) * d1 + l3.X;
                dg.Y = (((Point)value).Y - p3.Y) * d2 + l3.Y;
            }
            return dg;
        }
    }
}
