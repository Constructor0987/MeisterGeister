using MeisterGeister.Logic.General.AStar;
using MeisterGeister.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MeisterGeister.Logic.Karte;
using MeisterGeister.ViewModel.Karte.Logic;
using System.Diagnostics;

namespace MeisterGeister.Model
{
    public partial class Ort : Node
    {
        private Point _location;
        public override Point Location
        {
            get
            {
                if (_location == null || _location.Equals(new Point(-1, -1)))
                    Location = new Point(X, Y);
                return _location;
            }
            protected set
            {
                _location = value;
            }
        }

        public Strecke RoutingStrecke { get; set; }

        public Ort()
            : base(-1, -1, true, null, null)
        {
        }

        public double DistanceTo(Ort ort)
        {
            return DistanceTo(new Point(ort.X, ort.Y));
        }

        public double DistanceTo(Point ort)
        {
            double dX = ort.X - X;
            double dY = ort.Y - Y;
            double distance = Math.Sqrt(dX * dX + dY * dY) * (100.0 / 359.0);

            return distance;
        }

        public override void SetEndLocation(Node endLocation)
        {
            if (endLocation != null)
            {
                this.EndLocation = endLocation.Location;
                this.LengthToEnd = GetTraversalCost(endLocation) / Global.ContextGeo.MaxMovementModificator;
            }
        }

        public override IEnumerable<Node> GetAdjacentNodes()
        {
            return Global.ContextGeo.GetAdjacentOrte(this);
        }

        public override double GetTraversalCost(Node target)
        {
            //var stopWatch = Stopwatch.StartNew();
            Ort targetOrt = (Ort)target;
            if (target == null || target == this)
                return 0.0;

            if (target.Location.X == EndLocation.X && target.Location.Y == EndLocation.Y)
                return this.DistanceTo(targetOrt);

            FastestStreckeResult result = GetFastestStrecke(targetOrt);
            //stopWatch.Stop();
            //Debug.WriteLine("GetTraversalCost(Node target): " + stopWatch.Elapsed.TotalMilliseconds);
            if (result != null)
                return result.Distance;
            else
                return Double.MaxValue;
        }

        private double GetTraversalCost(Strecke strecke)
        {
            //var getFastestStreckeStopWatch = Stopwatch.StartNew();
            var parameters = (SearchParametersRouting)SearchParameters;
            Fortbewegung_Modifikation modifikator = strecke.Wegtyp.Fortbewegung_Modifikation.Single(f => f.Fortbewegung == parameters.Fortbewegung.ID);
            //getFastestStreckeStopWatch.Stop();
            //Debug.WriteLine("GetTraversalCost(Strecke strecke): " + getFastestStreckeStopWatch.Elapsed.TotalMilliseconds);
            return strecke.Strecke1 / modifikator.Multiplikator;
        }

        private FastestStreckeResult GetFastestStrecke(Ort targetOrt)
        {
            //var getFastestStreckeStopWatch = Stopwatch.StartNew();
            IEnumerable<Strecke> strecken = GetStreckenToTarget(targetOrt);
            FastestStreckeResult result = null;

            if (strecken != null && strecken.Count() != 0)
            {
                if (strecken.Count() == 1)
                {
                    Strecke strecke = strecken.First();
                    double distance = GetTraversalCost(strecke);
                    result = new FastestStreckeResult(distance, strecke);
                }
                else
                    result = GetFastestStrecke(strecken);
            }
            //getFastestStreckeStopWatch.Stop();
            //Debug.WriteLine("GetFastestStrecke: " + getFastestStreckeStopWatch.Elapsed.TotalMilliseconds);
            return result;
        }

        private IEnumerable<Strecke> GetStreckenToTarget(Ort targetOrt)
        {
            //var getStreckenToTargetStopWatch = Stopwatch.StartNew();
            var strecken = Global.ContextGeo.GetStreckenToTarget(this, targetOrt, (SearchParametersRouting)SearchParameters);
            //getStreckenToTargetStopWatch.Stop();
            //Debug.WriteLine("getStreckenToTargetStopWatch: " + getStreckenToTargetStopWatch.Elapsed.TotalMilliseconds);
            return strecken;
        }

        private FastestStreckeResult GetFastestStrecke(IEnumerable<Strecke> strecken)
        {
            //var getFastestStreckeStopWatch = Stopwatch.StartNew();
            Strecke resultStrecke = null;
            double resultTraversalCost = Double.MaxValue;

            foreach(var strecke in strecken)
            {
                double tempTraversalCost = GetTraversalCost(strecke);
                if(tempTraversalCost < resultTraversalCost)
                {
                    resultTraversalCost = tempTraversalCost;
                    resultStrecke = strecke;
                }
            }
            FastestStreckeResult result = new FastestStreckeResult(resultTraversalCost, resultStrecke);
            //getFastestStreckeStopWatch.Stop();
            //Debug.WriteLine("getFastestStreckeStopWatch: " + getFastestStreckeStopWatch.Elapsed.TotalMilliseconds);
            return result;
        }

        public override void EnrichData(List<Node> path)
        {
            //var getFastestStreckeStopWatch = Stopwatch.StartNew();
            var nodeIndex = path.IndexOf(this);
            if (nodeIndex >= 0 && nodeIndex < path.Count)
            {
                if (nodeIndex > 0)
                {
                    FastestStreckeResult result = GetFastestStrecke((Ort)ParentNode);
                    this.RoutingStrecke = result.Strecke;
                }
            }
            else
                throw new ArgumentException("Der gesuchte Ort ist nicht vorhanden in dem Routing-Ergebnis.", "path");
            //getFastestStreckeStopWatch.Stop();
            //Debug.WriteLine("EnrichData: " + getFastestStreckeStopWatch.Elapsed.TotalMilliseconds);
        }

        public bool Equals(Node target)
        {
            return target.Location.X == this.X && target.Location.Y == this.Y;
        }

        public override string ToString()
        {
            string result = Name;
            if (string.IsNullOrEmpty(Name))
                result = this.Typ;
            return result + " " + ID;
        }

        class FastestStreckeResult
        {
            public Strecke Strecke { get; private set; }
            public double Distance { get; private set; }

            public FastestStreckeResult(double distance, Strecke strecke)
            {
                this.Strecke = strecke;
                this.Distance = distance;
            }
        }
    }
}
