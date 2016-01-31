using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class TravelService
    {
        public static double FLYING_CONST = 13.333333333333333;
        public static double BASE_MILES_PER_HOUR = 3.75;
        public static double TRAVELING_HOURS_PER_DAY = 8;
        public static double TRAVELABLE_MAXIMUM = 1.25;
        public static double TRAVELABLE_MINIMUM = 0.9;

        public double GetTravelDuration(double strecke, double movementModifier)
        {
            return Math.Round(strecke / GetMilesPerHour(movementModifier), 2);
        }

        public double GetMilesPerHour(double movementModifier)
        {
            return (BASE_MILES_PER_HOUR * movementModifier);
        }
    }
}
