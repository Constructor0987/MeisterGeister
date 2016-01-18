using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Model.Service
{
    public class GeoService : ServiceBase
    {
        public Ort LoadClosestOrt(Point point, double tolerance = 150)
        {
            IEnumerable<Ort> candidates = Context.Ort
                .Where(o => o.X >= (point.X - tolerance) && o.X <= (point.X + tolerance) && o.Y >= (point.Y - tolerance) && o.Y <= (point.Y + tolerance));

            Ort result = candidates.First();
            double lowestDistance = result.DistanceTo(point);

            for(int i = 1; i < candidates.Count(); i++)
            {
                var ort = candidates.ElementAt(i);
                double tempDistance = ort.DistanceTo(point);
                if (tempDistance < lowestDistance)
                {
                    lowestDistance = tempDistance;
                    result = ort;
                }
            }
            return result;
        }
    }
}
