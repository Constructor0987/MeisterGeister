using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class TravelService
    {
        private const double MILES_PER_HOUR = 3.75;

        public double GetTravelDuration(double strecke, double movementModifier)
        {
            return Math.Round(strecke / (MILES_PER_HOUR * movementModifier), 2);
        }
    }
}
