using MeisterGeister.Logic.Karte;
using MeisterGeister.Model;
using MeisterGeister.Model.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister_Tests
{
    [TestFixture]
    class RoutingService_Tests
    {
        protected GeoService _geoService;

        [TestFixtureSetUp]
        public void SetupMethods()
        {
            this._geoService = new GeoService();
        }

        [TestFixtureTearDown]
        public void TearDownMethods()
        {
        }

        [SetUp]
        public void SetupTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        [Test]
        public void GetAdjustmentFactorIfNecessaryTest()
        {
            Size size = new Size(6.0, 6.0);
            double zoom = 1.0;
            Point center = new Point(9.0, 9.0);
            Point comparablePoint = new Point(2.0, 2.0);
            var routingService = new RoutingService();
            double result = routingService.GetZoomAdjustment(size, zoom, center, comparablePoint);
            Assert.IsTrue(result == 2.8000000000000003);
        }

        [Test]
        public void GetShortestPathDistanceGarethWeyringTest()
        {
            IEnumerable<Ort> result = TestShortestPath("Gareth", "Weyring");
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public void GetShortestPathDistanceGarethHirschfurtTest()
        {
            IEnumerable<Ort> result = TestShortestPath("Gareth", "Hirschfurt");
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count(s => !string.IsNullOrEmpty(s.Name)) == 4);
        }

        [Test]
        public void GetShortestPathDistanceGarethLowangenTest()
        {
            IEnumerable<Ort> result = TestShortestPath("Gareth", "Ferdok");
            Assert.IsTrue(result != null);
        }

        private IEnumerable<Ort> TestShortestPath(string startString, string targetString)
        {
            var orte = _geoService.Liste<Ort>();
            Ort start = orte.Single(o => o.Name == startString);
            Ort target = orte.Single(o => o.Name == targetString);
            var service = new RoutingService();
            var result = service.GetShortestPath(new Size(6000, 6000), start, target);
            return result;
        }
    }
}
