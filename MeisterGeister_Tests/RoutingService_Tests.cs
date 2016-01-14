using MeisterGeister.Logic.Karte;
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
        [TestFixtureSetUp]
        public void SetupMethods()
        {
     
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
            Assert.IsTrue(result == 1.2833333333333334);
        }
    }
}
