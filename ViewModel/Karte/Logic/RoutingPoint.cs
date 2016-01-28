using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingPoint : ViewModelBase, ICloneable
    {
        public double ActualX { get; private set; }
        public double ActualY { get; private set; }
        public double X { get { return ActualX - 20; } }
        public double Y { get { return ActualY - 30; } }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    Set(ref _isSelected, value);
                }
            }
        }
        public string Name { get; set; }
        public string PointType { get; private set; }
        public string Image { get; private set; }
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

        public RoutingPoint(double x, double y, string name, string wegtyp, double strecke, double routeToEnd, double movementModifier)
        {
            this.ActualX = x;
            this.ActualY = y;
            this.Name = name;
            this.Strecke = strecke;
            this.Wegtyp = wegtyp;
            this.RouteToEnd = routeToEnd;
            this.MovementModifier = movementModifier;
            this._travelService = new TravelService();
        }

        public RoutingPoint(double x, double y, string name, string wegtyp, double strecke, double routeToEnd, double movementModifier, 
            string image, string pointType)
            : this(x, y, name, wegtyp, strecke, routeToEnd, movementModifier)
        {
            this.PointType = pointType;
            this.Image = image;
        }

        public object Clone()
        {
            return new RoutingPoint(ActualX, ActualY, Name, Wegtyp, Strecke, RouteToEnd, MovementModifier, Image, PointType);
        }
    }
}
