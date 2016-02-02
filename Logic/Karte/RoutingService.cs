using MeisterGeister.Logic.General.AStar;
using MeisterGeister.Model;
using MeisterGeister.Model.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Logic.Karte
{
    public class RoutingService
    {
        public IEnumerable<Node> AllNodes { get; private set; }

        public double GetZoomAdjustment(Size zoomControlSize, double zoom, Point center, Point routeStartingPoint)
        {
            // Standardmäßig soll keine Änderung stattfinden
            double result = 1.0;

            // Der Zoom muss erst Mal normalisiert werden
            Size normalizedSize = new Size(zoomControlSize.Width / zoom, zoomControlSize.Height / zoom);

            // Anschließend ist die höhere Distance zum zu vergleichenden Punkt zu ermitteln
            double xDistanceToTarget = Math.Abs(center.X - routeStartingPoint.X);
            double yDistanceToTarget = Math.Abs(center.Y - routeStartingPoint.Y);
            bool isXDistanceHigher = xDistanceToTarget > yDistanceToTarget;

            // Als nächstes wird geschaut, ob der Punkt außerhalb des sichtbaren Bereichs liegt
            double distanceToCompare = isXDistanceHigher ? xDistanceToTarget : yDistanceToTarget;
            double distanceToBorder = isXDistanceHigher ? normalizedSize.Width / 2 : normalizedSize.Height / 2;
            bool isOutOfSight = distanceToBorder <= distanceToCompare;

            // Falls dem so ist, muss der Zoom reduziert werden. Der Faktor 1.2 sorgt dafür, 
            // dass die beiden Punkte nicht genau auf dem Rand des sichtbaren Bereichs liegen.
            if (isOutOfSight)
                result = (distanceToCompare / distanceToBorder) * 1.2;

            return result;
        }

        public IEnumerable<Ort> GetShortestPath(SearchParametersRouting searchParameters)
        {
            var getShortestPathLengthInitStopWatch = Stopwatch.StartNew();
            IEnumerable<Ort> orte = Global.ContextGeo.Liste<Ort>();
            Ort target = (Ort)searchParameters.EndNode;


            foreach (var ort in orte)
                ort.Init(ort.X, ort.Y, true, target, searchParameters);

            var searchingService = new AStarService(searchParameters);
            getShortestPathLengthInitStopWatch.Stop();
            Debug.WriteLine("getShortestPathLengthInitStopWatch: " + getShortestPathLengthInitStopWatch.Elapsed.TotalMilliseconds);
            List<Node> result = searchingService.FindPath();
            AllNodes = searchingService.Nodes;

            return result.OfType<Ort>();
        }
    }
}
