using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingPointBuilderArgs
    {
        public double X { get; set; } 
        public double Y { get; set; }
        public string Name { get; set; }
        public string PointType { get; set; }
        public string Wegtyp { get; private set; }
        public double Strecke { get; private set; }
        public double RouteToEnd { get; private set; }
        public double MovementModifier { get; private set; }

        public RoutingPointBuilderArgs(double x, double y, string name, string pointType, double routeToEnd, double movementModifier)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
            this.PointType = pointType;
            this.RouteToEnd = routeToEnd;
            this.MovementModifier = movementModifier;
        }

        public RoutingPointBuilderArgs(double x, double y, string name, string pointType, double routeToEnd, double movementModifier, string wegtyp, double strecke)
            : this(x, y, name, pointType, routeToEnd, movementModifier)
        {
            this.Wegtyp = wegtyp;
            this.Strecke = strecke;
        }
    }
}
