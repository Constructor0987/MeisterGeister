using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public static class BattlegroundUtilities
    {
        //using the more powerful pathgeometry instead of the light weighter streamgeometry, because for later animations and databindings.
        public static PathGeometry HexCellTile(double hexSideLength)
        {
            double s = hexSideLength;
            double sinhex = Math.Sin(DegreeToRadian(30));
            double coshex = Math.Cos(DegreeToRadian(30));

            double hx = 2 * s + 2 * s * sinhex;
            double hy = 4 * s + coshex;

            var p1 = new Point(0.5 * s, 0);
            var p2 = new Point(1.5 * s + 2 * s * sinhex, 0);
            var p3 = new Point(0.5 * s + s * sinhex, s * coshex);
            var p4 = new Point(1.5 * s + s * sinhex, s * coshex);
            var p5 = new Point(0.5 * s, 2 * s * coshex);
            var p6 = new Point(1.5 * s + 2 * s * sinhex, 2 * s * coshex);
            var p7 = new Point(0.5 * s + s * sinhex, 3 * s * coshex);
            var p8 = new Point(1.5 * s + s * sinhex, 3 * s * coshex);
            var p9 = new Point(0.5 * s, 4 * s * coshex);
            var p10 = new Point(1.5 * s + 2 * s * sinhex, 4 * s * coshex);

            PathFigure hexPathFigure1= new PathFigure();
            hexPathFigure1.StartPoint = new Point(0,0);
            PathSegmentCollection hexPathSegementCollection1 = new PathSegmentCollection();
            hexPathSegementCollection1.Add(new LineSegment(p1,true));
            hexPathSegementCollection1.Add(new LineSegment(p3, true));
            hexPathSegementCollection1.Add(new LineSegment(p5, true));
            hexPathSegementCollection1.Add(new LineSegment(new Point(0, p5.Y), true));

            PathFigure hexPathFigure2 = new PathFigure();
            hexPathFigure2.StartPoint = p5;
            PathSegmentCollection hexPathSegementCollection2 = new PathSegmentCollection();
            hexPathSegementCollection2.Add(new LineSegment(p7, true));
            hexPathSegementCollection2.Add(new LineSegment(p9, true));
            //hexPathSegementCollection2.Add(new LineSegment(new Point(0,p9.Y), true));

            PathFigure hexPathFigure3 = new PathFigure();
            hexPathFigure3.StartPoint = p7;
            PathSegmentCollection hexPathSegementCollection3 = new PathSegmentCollection();
            hexPathSegementCollection3.Add(new LineSegment(p8, true));
            hexPathSegementCollection3.Add(new LineSegment(p10, true));
            //hexPathSegementCollection3.Add(new LineSegment(new Point(hx,p10.Y), true));

            PathFigure hexPathFigure4 = new PathFigure();
            hexPathFigure4.StartPoint = p8;
            PathSegmentCollection hexPathSegementCollection4 = new PathSegmentCollection();
            hexPathSegementCollection4.Add(new LineSegment(p8, true));
            hexPathSegementCollection4.Add(new LineSegment(p6, true));
            hexPathSegementCollection4.Add(new LineSegment(new Point(hx,p6.Y), true));

            PathFigure hexPathFigure5 = new PathFigure();
            hexPathFigure5.StartPoint = p6;
            PathSegmentCollection hexPathSegementCollection5 = new PathSegmentCollection();
            hexPathSegementCollection5.Add(new LineSegment(p4, true));
            hexPathSegementCollection5.Add(new LineSegment(p3, true));

            PathFigure hexPathFigure6 = new PathFigure();
            hexPathFigure6.StartPoint = p4;
            PathSegmentCollection hexPathSegementCollection6 = new PathSegmentCollection();
            hexPathSegementCollection6.Add(new LineSegment(p2, true));
            hexPathSegementCollection6.Add(new LineSegment(new Point(hx, 0), true));

            hexPathFigure1.Segments = hexPathSegementCollection1;
            hexPathFigure2.Segments = hexPathSegementCollection2;
            hexPathFigure3.Segments = hexPathSegementCollection3;
            hexPathFigure4.Segments = hexPathSegementCollection4;
            hexPathFigure5.Segments = hexPathSegementCollection5;
            hexPathFigure6.Segments = hexPathSegementCollection6;

            PathFigureCollection hexFigureCollection = new PathFigureCollection();
            hexFigureCollection.Add(hexPathFigure1);
            hexFigureCollection.Add(hexPathFigure2);
            hexFigureCollection.Add(hexPathFigure3);
            hexFigureCollection.Add(hexPathFigure4);
            hexFigureCollection.Add(hexPathFigure5);
            hexFigureCollection.Add(hexPathFigure6);

            PathGeometry hexPathGeometry = new PathGeometry();
            hexPathGeometry.Figures = hexFigureCollection;

            Path hexPath = new Path();
            hexPath.Stroke = Brushes.DarkGray;
            hexPath.StrokeThickness = 1;

            var doublecollection = new DoubleCollection();
            doublecollection.Add(5);
            doublecollection.Add(3);

            hexPath.StrokeDashArray=doublecollection;
            hexPath.Data = hexPathGeometry;

            return hexPathGeometry;
        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
