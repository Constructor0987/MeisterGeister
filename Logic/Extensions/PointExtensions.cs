using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Logic.Extensions
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point point1, Point point2)
        {
            double dX = point1.X - point2.X;
            double dY = point1.Y - point2.Y;

            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
}
