using MeisterGeister.Logic.General.AStar;
using MeisterGeister.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            :base(-1, -1, true, null)
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
            var streckenStarts = this.StartStrecke.Select(s => s.ZielOrt);
            var streckenZiele = this.ZielStrecke.Select(s => s.StartOrt);
            return streckenStarts.Concat(streckenZiele);
        }

        public override double GetTraversalCost(Node target)
        {
            Ort targetOrt = (Ort)target;
            if (target == null || target == this)
                return 0.0;
            else
            {
                if (target.Location.X == EndLocation.X && target.Location.Y == EndLocation.Y)
                    return this.DistanceTo(targetOrt);

                Strecke strecke = GetFastestStrecke(targetOrt);

                return GetTraversalCost(strecke);
            }
        }

        private double GetTraversalCost(Strecke strecke)
        {
            return strecke.Strecke1 / strecke.Wegtyp.Multiplikator;
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
                throw new ArgumentException("Das angegebene Element existiert nicht.", "target");
        }

        private IEnumerable<Strecke> GetStreckenToTarget(Ort targetOrt)
        {
            var strecken = this.StartStrecke.Where(s => (s.StartOrt.ID == this.ID && s.ZielOrt.ID == targetOrt.ID));
            if (strecken == null || strecken.Count() == 0)
                strecken = this.ZielStrecke.Where(s => s.StartOrt.ID == targetOrt.ID && s.ZielOrt.ID == this.ID);
            return strecken;
        }

        private Strecke GetStreckeToTarget(Ort targetOrt)
        {
            var strecke = this.StartStrecke.SingleOrDefault(s => (s.StartOrt.ID == this.ID && s.ZielOrt.ID == targetOrt.ID));
            if (strecke == null)
                strecke = this.ZielStrecke.Single(s => s.StartOrt.ID == targetOrt.ID && s.ZielOrt.ID == this.ID);
            return strecke;
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
                if (nodeIndex < (path.Count - 1))
                {
                    var followingNode = (Ort)path[nodeIndex + 1];
                    this.RoutingStrecke = GetFastestStrecke(followingNode);
                }
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
