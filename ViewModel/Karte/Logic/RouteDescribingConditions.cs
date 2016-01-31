using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RouteDescribingConditions
    {
        public Fortbewegung Fortbewegung { get; private set; }
        public Ort RouteEnding { get; private set; }
        public bool IsToleranceActive { get; private set; }

        public RouteDescribingConditions(Fortbewegung fortbewegung, Ort routeEnding, bool isToleranceActive)
        {
            this.Fortbewegung = fortbewegung;
            this.RouteEnding = routeEnding;
            this.IsToleranceActive = isToleranceActive;
        }
    }
}
