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
    public class WetterToPathConverter : IValueConverter
    {
        public WetterToPathConverter()
        {
            Smooth = 4;
            Width = 300;
            Height = 100;
        }

        public int Smooth { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Wetter heute = (Wetter)value;
            int[] temps = new int[] { heute.Gestern.Nachttemperatur, heute.Tagestemperatur, heute.Nachttemperatur };

            Point midnight1, noon, midnight2;
            midnight1 = new Point(0, -temps[0]);
            noon = new Point(12, -temps[1]);
            midnight2 = new Point(24, -temps[2]);
            int diff = temps.Max() - temps.Min();

            BezierSegment seg1 = new BezierSegment(moveRight(midnight1), moveLeft(noon), noon, true);
            BezierSegment seg2 = new BezierSegment(moveRight(noon), moveLeft(midnight2), midnight2, true);
            PathFigure figure = new PathFigure(midnight1, new PathSegment[] {seg1, seg2}, false);
            PathFigureCollection col = new PathFigureCollection();
            col.Add(figure);
            PathGeometry geo = new PathGeometry(col);
            TransformGroup transforms = new TransformGroup();
            transforms.Children.Add(new TranslateTransform(0, temps.Max()));
            transforms.Children.Add(new ScaleTransform(Width / 24.0, Height / (double)diff, 0, 0));
            geo.Transform = transforms;
            return geo;
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



        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
