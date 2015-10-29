using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.Karte;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class Karte
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        int breite, höhe;

        public int Höhe
        {
            get { return höhe; }
            set { höhe = value; }
        }

        public int Breite
        {
            get { return breite; }
            set { breite = value; }
        }
        string bildpfad;

        public string Bildpfad
        {
            get { return bildpfad; }
            set { bildpfad = value; }
        }

        public Karte(string name, string bildpfad, int breite, int höhe, Point map1, Point dg1, Point map2, Point dg2, Point map3, Point dg3, bool withProjection = false, bool withRotation = false)
        {
            Name = name;
            Bildpfad = bildpfad;
            Breite = breite;
            Höhe = höhe;
            Map1 = map1;
            Map2 = map2;
            Map3 = map3;
            DG1 = dg1;
            DG2 = dg2;
            DG3 = dg3;
            WithRotation = withRotation;
            WithProjection = withProjection;
        }

        private Point p1, p2, p3, l1, l2, l3;
        public Point Map1
        {
            get { return p1; }
            set { p1 = value;  }
        }
        public Point Map2
        {
            get { return p2; }
            set { p2 = value;  }
        }
        public Point Map3
        {
            get { return p3; }
            set { p3 = value; }
        }
        public Point DG1
        {
            get { return l1; }
            set { l1 = value; }
        }
        public Point DG2
        {
            get { return l2; }
            set { l2 = value;  }
        }
        public Point DG3
        {
            get { return l3; }
            set { l3 = value; }
        }
        bool withRotation = false;
        public bool WithRotation
        {
            get { return withRotation; }
            set { withRotation = value; }
        }
        bool withProjection = false;
        public bool WithProjection
        {
            get { return withProjection; }
            set { withProjection = value; }
        }

        public DereGlobusToMapConverter DereGlobusToMapConverter
        {
            get
            {
                var c = new DereGlobusToMapConverter(Map1, Map2, Map3, DG1, DG2, DG3, WithProjection, WithRotation);
                return c;
            }
        }

        public MapToDereGlobusConverter MapToDereGlobusConverter
        {
            get
            {
                var c = new MapToDereGlobusConverter(Map1, Map2, Map3, DG1, DG2, DG3, WithProjection, WithRotation);
                return c;
            }
        }

        public Point TopLeft
        {
            get { return (Point)MapToDereGlobusConverter.Convert(new Point(0, Höhe), typeof(Point), null, null); }
        }
        public Point BottomRight
        {
            get { return (Point)MapToDereGlobusConverter.Convert(new Point(Breite, 0), typeof(Point), null, null); }
        }
    }
}
