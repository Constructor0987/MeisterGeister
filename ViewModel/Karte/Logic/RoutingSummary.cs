using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingSummary
    {
        public double TotalDistance { get; private set; }
        public string TravelType { get; private set; }
        public double Duration { get; set; }

        public RoutingSummary(double totalDistance, string travelType, double duration)
        {
            this.TotalDistance = totalDistance;
            this.TravelType = travelType;
            this.Duration = duration;
        }
    }
}