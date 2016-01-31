using MeisterGeister.Model;
using MeisterGeister.ViewModel.Base;
using MeisterGeister.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MeisterGeister.Logic.Karte;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RouteDescribingService
    {
        double _traveledDistance = 0;
        bool _isDayOver, _isDayOverSoon = false;

        public RouteDescribingResult GetDescription(IEnumerable<RoutingOrt> routingPoints, RouteDescribingConditions conditions)
        {
            var resultingPoints = new ObservableCollection<RoutingOrt>();
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

            RoutingSummary routingSummary = GetSummary(totalStrecke, totalDuration, conditions.Fortbewegung.Name);
            return new RouteDescribingResult(resultingPoints, routingSummary);
        }

        private RoutingSummary GetSummary(double totalStrecke, double totalDuration, string fortbewegungName)
        {
            return new RoutingSummary(totalStrecke, fortbewegungName, Math.Round(totalDuration / 8, 2));
        }

        public ObservableCollection<ViewModelBase> DrawRoute(IEnumerable<Ort> nodes, RouteDescribingConditions conditions)
        {
            ObservableCollection<ViewModelBase> routingElements = new ObservableCollection<ViewModelBase>();
            var routingPointBuilder = new RoutingPointBuilder();
            for (int j = 0; j < nodes.Count(); j++)
            {
                Ort ort = nodes.ElementAt(j);
                string followingOrtName = j < (nodes.Count() - 1) ? nodes.ElementAt(j + 1).Name : null;
                Strecke strecke = ort.RoutingStrecke;
                foreach (var line in CreateRoutingLines(strecke))
                    routingElements.Add(line);
                routingElements.Add(CreateRoutingPoint(routingPointBuilder, ort, strecke, 
                    conditions.Fortbewegung, conditions.RouteEnding));
            }

            return routingElements;
        }

        private IEnumerable<RoutingLine> CreateRoutingLines(Strecke strecke)
        {
            ICollection<RoutingLine> routingLines = new List<RoutingLine>();
            if (strecke != null)
            {
                IEnumerable<Weg> wege = strecke.Weg.OrderBy(w => w.ID);
                RoutingLineType lineType = new RoutingLineType(strecke.Wegtyp);
                var firstWeg = wege.First();
                for (int i = 1; i < wege.Count(); i++)
                {
                    var secondWeg = wege.ElementAt(i);
                    routingLines.Add(new RoutingLine(firstWeg.X, firstWeg.Y, secondWeg.X, secondWeg.Y, lineType));
                    firstWeg = secondWeg;
                }
            }
            return routingLines;
        }

        private RoutingOrt CreateRoutingPoint(RoutingPointBuilder routingPointBuilder, Ort ort, Strecke strecke, 
            Fortbewegung fortbewegung, Ort routeEnding)
        {
            RoutingPointBuilderArgs routingPointBuilderArgs = null;

            if (strecke != null)
            {
                var modifier = fortbewegung.Fortbewegung_Modifikation.Single(m => m.Wegtyp == strecke.Wegtyp.ID).Multiplikator;
                routingPointBuilderArgs = new RoutingPointBuilderArgs(ort.X, ort.Y, ort.Name, ort.Typ, ort.LengthToEnd, modifier, strecke.Wegtyp.Name, strecke.Strecke1);
            }
            else
            {
                routingPointBuilderArgs = new RoutingPointBuilderArgs(ort.X, ort.Y, routeEnding.Name, ort.Typ, ort.LengthToEnd, TravelService.FLYING_CONST, "Luftlinie", Math.Round(ort.LengthToEnd, 2));
            }

            return routingPointBuilder.Build(routingPointBuilderArgs);
        }

        public IEnumerable<RoutingPoint> GetStages(IEnumerable<Strecke> strecken, RouteDescribingConditions conditions)
        {
            var travelService = new TravelService();
            ICollection<RoutingPoint> routingPoints = new List<RoutingPoint>();
            double travelingHoursPerDay = TravelService.TRAVELING_HOURS_PER_DAY;
            ResetDay();

            foreach (var strecke in strecken)
            {
                var movementModificator = strecke.Wegtyp.Fortbewegung_Modifikation.Single(fm => fm.Fortbewegung == conditions.Fortbewegung.ID);
                double milesPerHour = travelService.GetMilesPerHour(movementModificator.Multiplikator);
                double milesPerDay = travelingHoursPerDay * milesPerHour;
                
                foreach (var weg in strecke.Weg)
                {
                    _isDayOverSoon = _traveledDistance >= (TravelService.TRAVELABLE_MINIMUM * milesPerDay);
                    _isDayOver = _traveledDistance >= milesPerDay;
                    if (_isDayOver)
                    {
                        if (IsOrt(weg))
                        {
                            routingPoints.Add(CreateRoutingStage(weg.X, weg.Y));
                        }
                        else if (!IsNextOrtWithinTolerance(strecken, weg, _traveledDistance))
                        {
                            routingPoints.Add(CreateRoutingStage(weg.X, weg.Y));
                        }
                    }
                    else if (_isDayOverSoon && IsOrt(weg))
                    {
                        routingPoints.Add(CreateRoutingStage(weg.X, weg.Y));
                    }
                    _traveledDistance += weg.Strecke;
                }
            }


            return routingPoints;
        }

        private RoutingStage CreateRoutingStage(double x, double y)
        {
            string image = "/DSA MeisterGeister;component/Images/Icons/Karte/pin56.png";
            var routingStage = new RoutingStage(x, y, image);
            ResetDay();
            return routingStage;
        }

        private void ResetDay()
        {
            _isDayOver = _isDayOverSoon = false;
            _traveledDistance = 0;
        }

        private bool IsOrt(Weg weg)
        {
            return Global.ContextGeo.Liste<Ort>().Any(o => o.X == weg.X && o.Y == weg.Y);
        }

        private bool IsNextOrtWithinTolerance(IEnumerable<Strecke> strecken, Weg weg, double traveledDistance)
        {
            IList<Weg> wege = strecken.SelectMany(s => s.Weg).ToList();
            double travelableDistance = TravelService.TRAVELABLE_MAXIMUM * traveledDistance;
            var currentWeg = weg;
            int currentPosition = wege.IndexOf(weg);

            for (int i = currentPosition; i < wege.Count; i++)
            {
                var nextWeg = wege.ElementAt(i);
                var nextWegPoint = new Point(nextWeg.X, nextWeg.Y);
                var currentWegPoint = new Point(currentWeg.X, currentWeg.Y);
                traveledDistance += currentWegPoint.DistanceTo(nextWegPoint);

                if (traveledDistance > travelableDistance)
                    return false;
                else if (IsOrt(nextWeg))
                    return true;
            }
            return true;
        }
    }
}
