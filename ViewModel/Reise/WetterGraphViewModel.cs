using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Reise
{
    /// <summary>
    /// ViewModel für den WetterGraphen
    /// </summary>
    public class WetterGraphViewModel : ViewModelBase
    {
        public WetterGraphViewModel()
        {
            //Standardwerte setzen
            smooth = 4;
            width = 500;
            height = 200;
        }

        private int smooth;
        /// <summary>
        /// Wie weich soll die Kurve des Graphs sein
        /// </summary>
        public int Smooth
        {
            get { return smooth; }
            set
            {
                smooth = value;
                OnChanged();
            }
        }

        private int width;
        /// <summary>
        /// Wie breit soll der Graph sein
        /// </summary>
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                OnChanged();
            }
        }

        private int height;
        /// <summary>
        /// Wie hoch soll der Graph sein
        /// </summary>
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnChanged();
            }
        }

        private PathGeometry geometry;
        /// <summary>
        /// Temperaturkurve
        /// </summary>
        public PathGeometry Geometry
        {
            get { return geometry; }
            private set
            {
                geometry = value;
                OnChanged();
            }
        }

        private WetterViewModel wetter;
        /// <summary>
        /// Das WetterVM muss dem Graphen bekannt sein
        /// </summary>
        public WetterViewModel Wetter
        {
            get { return wetter; }
            set
            {
                wetter = value;
                OnChanged();
                createPath();
            }
        }


        private void createPath()
        {
            //Hier erstellen wir unsere Kurve anhand des heutigen Wetters

            //Wir beachten folgende 3 Temperaturen
            int[] temps = new int[] { wetter.Heute.Gestern.Nachttemperatur, wetter.Heute.Tagestemperatur, wetter.Heute.Nachttemperatur };

            //Wir machen Punkte an den Stützstellen
            Point midnight1, noon, midnight2;
            midnight1 = new Point(0, -temps[0]);
            noon = new Point(12, -temps[1]);
            midnight2 = new Point(24, -temps[2]);

            //Hier erstellen wir BezierSegmente, die unsere Kurve approximieren
            BezierSegment seg1 = new BezierSegment(moveRight(midnight1), moveLeft(noon), noon, true);
            BezierSegment seg2 = new BezierSegment(moveRight(noon), moveLeft(midnight2), midnight2, true);
            PathFigure figure = new PathFigure(midnight1, new PathSegment[] { seg1, seg2 }, false);
            PathFigureCollection col = new PathFigureCollection();
            col.Add(figure);
            PathGeometry geo = new PathGeometry(col);

            //Die Geometrie wird dann noch entsprechend transformiert
            //Wir möchten dass alle relevanten Temperaturzonen die Graphen vollständig ausfüllen
            TransformGroup transforms = new TransformGroup();
            transforms.Children.Add(new TranslateTransform(0, wetter.TemperaturZonen.First().MaxTemp));
            transforms.Children.Add(new ScaleTransform(Width / 24.0, wetter.TemperaturZonen.First().HeightPerDegree, 0, 0));
            transforms.Freeze();
            geo.Transform = transforms;

            geo.Freeze();
            Geometry = geo;
        }

        private Point moveRight(Point p)
        {
            p.Offset(Smooth, 0);
            return p;
        }

        private Point moveLeft(Point p)
        {
            p.Offset(-Smooth, 0);
            return p;
        }
    }
}
