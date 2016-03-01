using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MeisterGeister.Logic.General.AStar;
using MeisterGeister.Logic.Karte;
using System.Diagnostics;

namespace MeisterGeister.Model.Service
{
    public class GeoService : ServiceBase
    {
        public GeoService()
        {
            LoadRouten();
        }
        private void LoadRouten()
        {
            Context.Ort
                .Include(o => o.StartStrecke)
                .Include(o => o.ZielStrecke)
                .Load();
        }

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
            //var getFastestStreckeStopWatch = Stopwatch.StartNew();
            long ortId = ort.ID;
            var result = ort.ZielStrecke.Select(o => o.StartOrt).Concat(ort.StartStrecke.Select(o => o.ZielOrt));
            //getFastestStreckeStopWatch.Stop();
            //Debug.WriteLine("GetAdjacentOrte: " + getFastestStreckeStopWatch.Elapsed.TotalMilliseconds);
            return result;
        }

        public IEnumerable<Strecke> GetStreckenToTarget(Ort sourceOrt, Ort targetOrt, SearchParametersRouting conditions)
        {
            var targetOrtId = targetOrt.ID;
            var strecken = sourceOrt.StartStrecke
                .Where(s => (s.Ziel == targetOrtId))
                .Concat(
                    sourceOrt.ZielStrecke
                        .Where(s => (s.Start == targetOrtId))
                );
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
