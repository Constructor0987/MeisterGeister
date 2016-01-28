using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RouteDescribingResult
    {
        public RoutingSummary RoutingSummary { get; private set; }
        public ObservableCollection<RoutingPoint> RouteDescription { get; private set; }

        public RouteDescribingResult(ObservableCollection<RoutingPoint> routeDescription,
            RoutingSummary routingSummary)
        {
            this.RouteDescription = routeDescription;
            this.RoutingSummary = routingSummary;
        }
    }
}
