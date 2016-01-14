using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Model
{
    public partial class Ort
    {
        public double DistanceTo(Ort ort)
        {
            double dX = ort.X - X;
            double dY = ort.Y - Y;

            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
}
