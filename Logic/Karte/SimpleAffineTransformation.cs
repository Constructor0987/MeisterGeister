using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Logic.Karte
{
    /// <summary>
    /// Ohne Rotation
    /// </summary>
    public class SimpleAffineTransformation : IAffineTransformation
    {
        double xscale, yscale;
        Point p1, p2, p3, t1, t2, t3;

        void CalcScale()
        {
            xscale = (t2.X - t1.X) / (p2.X - p1.X);
            yscale = (t2.Y - t1.Y) / (p2.Y - p1.Y);
        }

        public bool Fit(Point[] from_pts, Point[] to_pts)
        {
            t1 = from_pts[0];
            t2 = from_pts[1];
            t3 = from_pts[2];
            p1 = to_pts[0];
            p2 = to_pts[1];
            p3 = to_pts[2];
            CalcScale();
            return true;
        }

        public Point transform(Point t)
        {
            Point map = new Point();
            map.X = (t.X - t3.X) / xscale + p3.X;
            map.Y = (t.Y - t3.Y) / yscale + p3.Y;
            return map;
        }

        public Point transformback(Point p)
        {
            Point dg = new Point();
            dg.X = (p.X - p3.X) * xscale + t3.X;
            dg.Y = (p.Y - p3.Y) * yscale + t3.Y;
            return dg;
        }
    }
}
