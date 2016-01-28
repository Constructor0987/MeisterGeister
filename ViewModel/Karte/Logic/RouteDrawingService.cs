using MeisterGeister.Model;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RouteDrawingService
    {
        private const double FLYING_CONST = 13.333333333333333;
        private Fortbewegung _selectedFortbewegung;
        private Ort _routeEnding;

        public RouteDrawingService(Fortbewegung selectedFortbewegung, Ort routeEnding)
        {
            this._selectedFortbewegung = selectedFortbewegung;
            this._routeEnding = routeEnding;
        }

        public ObservableCollection<ViewModelBase> DrawRoute(IEnumerable<Ort> nodes)
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
                routingElements.Add(CreateRoutingPoint(routingPointBuilder, ort, strecke));
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

        private RoutingPoint CreateRoutingPoint(RoutingPointBuilder routingPointBuilder, Ort ort, Strecke strecke)
        {
            RoutingPointBuilderArgs routingPointBuilderArgs = null;

            if (strecke != null)
            {
                var modifier = _selectedFortbewegung.Fortbewegung_Modifikation.Single(m => m.Wegtyp == strecke.Wegtyp.ID).Multiplikator;
                routingPointBuilderArgs = new RoutingPointBuilderArgs(ort.X, ort.Y, ort.Name, ort.Typ, ort.LengthToEnd, modifier, strecke.Wegtyp.Name, strecke.Strecke1);
            }
            else
            {
                routingPointBuilderArgs = new RoutingPointBuilderArgs(ort.X, ort.Y, _routeEnding.Name, ort.Typ, ort.LengthToEnd, FLYING_CONST, "Luftlinie", Math.Round(ort.LengthToEnd, 2));
            }

            return routingPointBuilder.Build(routingPointBuilderArgs);
        }
    }
}
