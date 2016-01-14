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
            IQueryable<Ort> candidates = Context.Ort.Where(o => o.X >= (point.X - tolerance) && o.X <= (point.X + tolerance) &&
                o.Y >= (point.Y - tolerance) && o.Y <= (point.Y + tolerance));

            Ort result = candidates.First();
            double lowestDistance = 9999;

            foreach (var ort in candidates)
            {
                double tempDistance = result.Dista                                      nceTo(ort);
                if (tempDistance < lowestDistance)
                    lowestDistance = tempDistance;
            }
            return result;
        }
    }
}
