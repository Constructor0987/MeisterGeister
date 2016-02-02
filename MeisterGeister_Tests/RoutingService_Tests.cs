using MeisterGeister;
using MeisterGeister.Logic.Karte;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Karte.Logic;
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
    public class RoutingService_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            MeisterGeister.Global.Init();
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
        public void RoutingServicePerformanceZorganBeilunkTest()
        {
            var routingService = new RoutingService();
            var searchParameters = CreateSearchParameters("Zorgan", "Beilunk", TravelType.Afoot);
            var result = routingService.GetShortestPath(searchParameters);
            Assert.IsNotNull(result);
        }

        [Test]
        public void RoutingServicePerformanceTest()
        {
            var routingService = new RoutingService();
            var searchParameters = CreateSearchParameters("Al'Anfa", "Gerasim", TravelType.Afoot);
            var result = routingService.GetShortestPath(searchParameters);
            Assert.IsNotNull(result);
        }

        private SearchParametersRouting CreateSearchParameters(string startNodeName, string endNodeName, TravelType travelType)
        {
            var startNode = Global.ContextGeo.Liste<Ort>().Single(o => o.Name == startNodeName);
            var endNode = Global.ContextGeo.Liste<Ort>().Single(o => o.Name == endNodeName);
            return new SearchParametersRouting(new Size(10000, 10000), startNode, endNode, travelType,
                true, true, false, true);
        }
    }
}
