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
    public class WetterGraphViewModel : ViewModelBase
    {
        public WetterGraphViewModel()
        {
            smooth = 4;
            width = 300;
            height = 100;
        }

        private int smooth;
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
        public PathGeometry Geometry
        {
            get { return geometry; }
            set
            {
                geometry = value;
                OnChanged();
            }
        }

        private WetterViewModel wetter;
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
            int[] temps = new int[] { wetter.Heute.Gestern.Nachttemperatur, wetter.Heute.Tagestemperatur, wetter.Heute.Nachttemperatur };

            Point midnight1, noon, midnight2;
            midnight1 = new Point(0, -temps[0]);
            noon = new Point(12, -temps[1]);
            midnight2 = new Point(24, -temps[2]);
            int diff = wetter.TemperaturZonen.First().MaxTemp - wetter.TemperaturZonen.Last().MinTemp;

            BezierSegment seg1 = new BezierSegment(moveRight(midnight1), moveLeft(noon), noon, true);
            BezierSegment seg2 = new BezierSegment(moveRight(noon), moveLeft(midnight2), midnight2, true);
            PathFigure figure = new PathFigure(midnight1, new PathSegment[] { seg1, seg2 }, false);
            PathFigureCollection col = new PathFigureCollection();
            col.Add(figure);
            PathGeometry geo = new PathGeometry(col);
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
