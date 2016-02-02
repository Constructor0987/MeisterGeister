using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MeisterGeister.Logic.General.AStar;
using MeisterGeister.Logic.Karte;

namespace MeisterGeister.Model.Service
{
    public class GeoService : ServiceBase
    {
        private double _maxMovementModificator = -1;
        public double MaxMovementModificator
        {
            get
            {
                if (_maxMovementModificator == -1)
                    _maxMovementModificator = Context.Fortbewegung_Modifikation.Max(f => f.Multiplikator);
                return _maxMovementModificator;
            }
        }

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

        public IEnumerable<Node> GetAdjacentOrte(Ort ort)
        {
            long ortId = ort.ID;
            var result = from strecke in Context.Strecke
                         where strecke.Start == ortId || strecke.Ziel == ortId
                         select strecke.Start == ortId ? strecke.ZielOrt : strecke.StartOrt;
            return result;
        }

        public IEnumerable<Strecke> GetStreckenToTarget(Ort sourceOrt, Ort targetOrt, SearchParametersRouting conditions)
        {
            var sourceOrtId = sourceOrt.ID;
            var targetOrtId = targetOrt.ID;
            var strecken = Context.Strecke
                .Where(s => (s.Start == sourceOrtId && s.Ziel == targetOrtId || s.Start == targetOrtId && s.Ziel == sourceOrtId ));
            ICollection<Strecke> result = new List<Strecke>();
            if (strecken.Any())
            {
                foreach(var strecke in strecken)
                {
                    if (IsAllowedStrecke(sourceOrt, strecke, conditions))
                        result.Add(strecke);
                }
            }
            return result;
        }

        private bool IsAllowedStrecke(Ort sourceOrt, Strecke strecke, SearchParametersRouting conditions)
        {
            return (sourceOrt.ID == strecke.Start || !TravelService.DIRECTION_DEPENDENT_WEGTYPEN.Contains(strecke.Wegtyp.ID))
                && !conditions.WegtypenNotAllowed.Any(w => w.ID == strecke.Wegtyp.ID);
        }
    }
}
