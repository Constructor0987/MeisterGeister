using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RouteDescribingService
    {
        private Fortbewegung _selectedFortbewegung;

        public RouteDescribingService(Fortbewegung selectedFortbewegung)
        {
            this._selectedFortbewegung = selectedFortbewegung;
        }

        public RouteDescribingResult GetDescription(IEnumerable<RoutingPoint> routingPoints, bool showStages)
        {
            var resultingPoints = new ObservableCollection<RoutingPoint>();
            resultingPoints.Add(routingPoints.First());
            double strecke = 0;
            double totalDuration = 0;
            double totalStrecke = 0;

            for (int i = 1; i < routingPoints.Count(); i++)
            {
                var routingPoint = routingPoints.ElementAt(i);
                strecke += routingPoint.Strecke;

                if (!string.IsNullOrEmpty(routingPoint.Name)) //|| routingPoint.PointType == "Kreuzung")
                {
                    var result = routingPoint;
                    result.Strecke = Math.Round(strecke, 2);
                    resultingPoints.Add(result);
                    totalStrecke += result.Strecke;
                    totalDuration += result.Duration;
                    strecke = 0;
                }
            }

            if (showStages)
                resultingPoints = GetStages(resultingPoints);

            RoutingSummary routingSummary = GetSummary(totalStrecke, totalDuration);
            return new RouteDescribingResult(resultingPoints, routingSummary);
        }

        private RoutingSummary GetSummary(double totalStrecke, double totalDuration)
        {
            return new RoutingSummary(totalStrecke, _selectedFortbewegung.Name, Math.Round(totalDuration / 8, 2));
        }

        private ObservableCollection<RoutingPoint> GetStages(ObservableCollection<RoutingPoint> routingPoints)
        {


            return routingPoints;
        }
    }
}
