using MeisterGeister.ViewModel.Karte.Logic;
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
        private bool withProjection = false;
        /// <summary>
        /// Soll die Kugelform mit einbezogen werden?
        /// </summary>
        public bool WithProjection
        {
            get { return withProjection; }
            set { withProjection = value; }
        }
        private bool withRotation = false;
        /// <summary>
        /// Soll eine Rotation der Karte mitbetrachtet werden?
        /// </summary>
        public bool WithRotation
        {
            get { return withRotation; }
            set { withRotation = value; }
        }

        public Point Map1
        {
            get { return p1; }
            set { p1 = value; Solve(); }
        }
        public Point Map2
        {
            get { return p2; }
            set { p2 = value; Solve(); }
        }
        public Point Map3
        {
            get { return p3; }
            set { p3 = value; Solve(); }
        }
        public Point DG1
        {
            get { return l1; }
            set { l1 = value; Solve(); }
        }
        public Point DG2
        {
            get { return l2; }
            set { l2 = value; Solve(); }
        }
        public Point DG3
        {
            get { return l3; }
            set { l3 = value; Solve(); }
        }

        /// <summary>
        /// Dient der Umwandlung zwischen Karten- und DereGlobus Koordinaten.
        /// Es wird eine Affine Transformation mit 3 Stützpunkten verwendet.
        /// </summary>
        public DereGlobusToMapConverter()
            // Dwurinsand, Neu-Bosparan, Gareth
            : this(new Point(279, 1958), new Point(6513, 10241), new Point(3996, 5271), new Point(-7.66832038, 38.96870934), new Point(11.45865558, 16.02534243), new Point(3.735098459, 29.79180236))
        {
        }

        public DereGlobusToMapConverter(Point m1, Point m2, Point m3, Point d1, Point d2, Point d3, bool withProjection = false, bool withRotation = false)
        {
            p1 = m1;
            p2 = m2;
            p3 = m3;
            l1 = d1;
            l2 = d2;
            l3 = d3;
            WithRotation = withRotation;
            WithProjection = withProjection;
            Solve();
        }

        private IAffineTransformation at = null;
        void Solve()
        {
            if(WithRotation)
                at = new AffineTransformation();
            else
                at = new SimpleAffineTransformation();
            var t1 = Project(DG1);
            var t2 = Project(DG2);
            var t3 = Project(DG3);
            at.Fit(new Point[] { t1, t2, t3 }, new Point[] { Map1, Map2, Map3 });
        }

        double width = 10000;
        double height = 10000;

        Point Project(Point p)
        {
            if (!WithProjection)
                return p;
            var t = new Point();
            t.X = (p.X + 180) * (width / 360);
            t.Y = (height / 2) - (width * Math.Log(Math.Tan((Math.PI / 4) + ((p.Y * Math.PI / 180) / 2))) / (2 * Math.PI));
            return t;
        }

        Point ProjectBack(Point p)
        {
            if (!WithProjection)
                return p;
            var t = new Point();
            t.X = p.X / (width / 360) - 180;
            t.Y = (Math.Atan(Math.Exp((-p.Y + (height / 2)) * (2 * Math.PI) / width)) - (Math.PI / 4)) * 2 * 180 / Math.PI;
            return t;
        }

        /// <summary>
        /// Wandelt einen DereGlobus-Point in einen Karten-Point um.
        /// </summary>
        /// <param name="value">Point mit DereGlobus Dezimalgrad (Longitude, Latitude)</param>
        /// <param name="targetType">typeof(Point)</param>
        /// <param name="parameter">null</param>
        /// <param name="culture">null</param>
        /// <returns></returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var karte = parameter as MeisterGeister.ViewModel.Karte.Logic.Karte;
            if (karte != null)
                return karte.DereGlobusToMapConverter.Convert(value, targetType, null, culture);

            if(value is Point)
            {
                var t = Project((Point)value);
                return at.transform(t);
            }
            return null;
        }

        /// <summary>
        /// Wandelt einen Karten-Point in einen DereGlobus-Point um.
        /// </summary>
        /// <param name="value">Point mit Karten-X und -Y</param>
        /// <param name="targetType">typeof(Point)</param>
        /// <param name="parameter">null</param>
        /// <param name="culture">null</param>
        /// <returns></returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var karte = parameter as MeisterGeister.ViewModel.Karte.Logic.Karte;
            if (karte != null)
                return karte.DereGlobusToMapConverter.ConvertBack(value, targetType, null, culture);

            if (value is Point)
            {
                var t = at.transformback((Point)value);
                return ProjectBack(t);
            }
            return null;
        }
    }
}
