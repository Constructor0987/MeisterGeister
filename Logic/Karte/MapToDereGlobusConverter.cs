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
    public class MapToDereGlobusConverter : IValueConverter
    {
        DereGlobusToMapConverter conv;
        public MapToDereGlobusConverter()
            // Dwurinsand, Neu-Bosparan, Gareth
            : this(new Point(279, 1958), new Point(6513, 10241), new Point(3996, 5271), new Point(-7.66832038, 38.96870934), new Point(11.45865558, 16.02534243), new Point(3.735098459, 29.79180236))
        {
        }

        public MapToDereGlobusConverter(Point m1, Point m2, Point m3, Point d1, Point d2, Point d3, bool withProjection = false, bool withRotation = false)
        {
            conv = new DereGlobusToMapConverter(m1, m2, m3, d1, d2, d3, withProjection, withRotation);
        }


        public Point Map1
        {
            get { return conv.Map1; }
            set { conv.Map1 = value; }
        }
        public Point Map2
        {
            get { return conv.Map2; }
            set { conv.Map2 = value; }
        }
        public Point Map3
        {
            get { return conv.Map3; }
            set { conv.Map3 = value; }
        }
        public Point DG1
        {
            get { return conv.DG1; }
            set { conv.DG1 = value; }
        }
        public Point DG2
        {
            get { return conv.DG2; }
            set { conv.DG2 = value; }
        }
        public Point DG3
        {
            get { return conv.DG3; }
            set { conv.DG3 = value; }
        }
        public bool WithRotation
        {
            get { return conv.WithRotation; }
            set { conv.WithRotation = value; }
        }
        public bool WithProjection
        {
            get { return conv.WithProjection; }
            set { conv.WithProjection = value; }
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return conv.ConvertBack(value, targetType, parameter, culture);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return conv.Convert(value, targetType, parameter, culture);
        }
    }
}
