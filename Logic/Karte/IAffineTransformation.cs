using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Logic.Karte
{
    public interface IAffineTransformation
    {
        bool Fit(Point[] from_pts, Point[] to_pts);
        Point transform(Point t);
        Point transformback(Point p);
    }
}
