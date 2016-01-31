using MeisterGeister.Logic.Karte;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingOrt : RoutingPoint, ICloneable
    {
        public double ActualX { get; private set; }
        public double ActualY { get; private set; }
        public override double X { get { return ActualX - 20; } }
        public override double Y { get { return ActualY - 30; } }
        public string Name { get; set; }
        public string PointType { get; private set; }
        public string Wegtyp { get; set; }
        public double Strecke { get; set; }
        public double RouteToEnd { get; set; }
        public double MovementModifier { get; set; }
        public double Duration
        {
            get
            {
                return _travelService.GetTravelDuration(Strecke, MovementModifier);
            }
        }
        private TravelService _travelService;

        public RoutingOrt(double x, double y, string name, string wegtyp, double strecke, double routeToEnd, double movementModifier,
            string image, string pointType)
            : base(x, y, image)
        {
            this.ActualX = x;
            this.ActualY = y;
            this.Name = name;
            this.Strecke = strecke;
            this.Wegtyp = wegtyp;
            this.RouteToEnd = routeToEnd;
            this.MovementModifier = movementModifier;
            this.PointType = pointType;
            this._travelService = new TravelService();
        }

        public object Clone()
        {
            return new RoutingOrt(ActualX, ActualY, Name, Wegtyp, Strecke, RouteToEnd, MovementModifier, Image, PointType);
        }
    }
}
