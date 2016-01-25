using MeisterGeister.Logic.General.AStar;
using MeisterGeister.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MeisterGeister.Logic.Karte;

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

        public override IEnumerable<Node> GetAdjacentNodes()
        {
            IEnumerable<Strecke> streckenStarts = this.StartStrecke;
            streckenStarts = ApplyConditions(streckenStarts);
            IEnumerable<Strecke> streckenZiele = this.ZielStrecke;
            streckenZiele = ApplyConditions(streckenZiele);
            var result = streckenStarts.Select(s => s.ZielOrt).Concat(streckenZiele.Select(s => s.StartOrt));
            return result;
        }

        public override double GetTraversalCost(Node target)
        {
            Ort targetOrt = (Ort)target;
            if (target == null || target == this)
                return 0.0;

            if (target.Location.X == EndLocation.X && target.Location.Y == EndLocation.Y)
                return this.DistanceTo(targetOrt);

            Strecke strecke = GetFastestStrecke(targetOrt);

            if (strecke != null)
                return GetTraversalCost(strecke);
            else
                return Double.MaxValue;
        }

        private double GetTraversalCost(Strecke strecke)
        {
            // Veraltet 
            // return strecke.Strecke1 / strecke.Wegtyp.Multiplikator;

            var parameters = (SearchParametersRouting)SearchParameters;
            Fortbewegung_Modifikation modifikator = strecke.Wegtyp.Fortbewegung_Modifikation.Single(f => f.Fortbewegung == parameters.Fortbewegung.ID);
            return strecke.Strecke1 / modifikator.Multiplikator;
        }

        private Strecke GetFastestStrecke(Ort targetOrt)
        {
            IEnumerable<Strecke> strecken = GetStreckenToTarget(targetOrt);

            if (strecken != null && strecken.Count() != 0)
            {
                Strecke result = null;
                if (strecken.Count() == 1)
                    result = strecken.First();
                else
                    result = GetFastestStrecke(strecken);

                return result;
            }
            else
                return null;
        }

        private IEnumerable<Strecke> GetStreckenToTarget(Ort targetOrt)
        {
            var strecken = this.StartStrecke.Where(s => (s.StartOrt.ID == this.ID && s.ZielOrt.ID == targetOrt.ID));
            if (strecken == null || strecken.Count() == 0)
                strecken = this.ZielStrecke.Where(s => s.StartOrt.ID == targetOrt.ID && s.ZielOrt.ID == this.ID);

            strecken = ApplyConditions(strecken);
            return strecken;
        }

        private IEnumerable<Strecke> ApplyConditions(IEnumerable<Strecke> strecken)
        {
            var result = strecken;
            if (SearchParameters != null)
            {
                var routingConditions = SearchParameters as SearchParametersRouting;

                if (routingConditions != null)
                    result = strecken.Where(s => !routingConditions.WegtypenNotAllowed.Contains(s.Wegtyp));
            }
            return result;
        }

        private Strecke GetFastestStrecke(IEnumerable<Strecke> strecken)
        {
            Strecke result = null;
            double traversalCost = Double.MaxValue;

            foreach(var strecke in strecken)
            {
                double tempTraversalCost = GetTraversalCost(strecke);
                if(tempTraversalCost < traversalCost)
                {
                    traversalCost = tempTraversalCost;
                    result = strecke;
                }
            }
            return result;
        }

        public override void EnrichData(List<Node> path)
        {
            var nodeIndex = path.IndexOf(this);
            if (nodeIndex >= 0 && nodeIndex < path.Count)
            {
                if(nodeIndex > 0)
                    this.RoutingStrecke = GetFastestStrecke((Ort)ParentNode);
            }
            else
                throw new ArgumentException("Der gesuchte Ort ist nicht vorhanden in dem Routing-Ergebnis.", "path");
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
    }
}
