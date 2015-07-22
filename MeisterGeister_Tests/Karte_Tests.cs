using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;

using NUnit.Framework;
using MeisterGeister.Logic.Karte;

namespace MeisterGeister_Tests
{
    [TestFixture]
    class Karte_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            //MeisterGeister.Global.Init();
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
        public void DereGlobusToMapConverterTest()
        {
            // Dwurinsand
            var p1 = new Point(279, 1958);
            var l1 = new Point(-7.66832038, 38.96870934);
            // Neu-Bosparan
            var p2 = new Point(6513, 10241);
            var l2 = new Point(11.45865558, 16.02534243);
            // Gareth
            var p3 = new Point(3996, 5271);
            var l3 = new Point(3.735098459, 29.79180236);

            var c = new DereGlobusToMapConverter();
            Point r = new Point();
            double diff = 0;
            r = (Point)c.Convert(l1, typeof(Point), null, null);
            diff = r.X - p1.X;
            Assert.LessOrEqual(diff, 1);
            diff = r.Y - p1.Y;
            Assert.LessOrEqual(diff, 1);
            r = (Point)c.Convert(l2, typeof(Point), null, null);
            diff = r.X - p2.X;
            Assert.LessOrEqual(diff, 1);
            diff = r.Y - p2.Y;
            Assert.LessOrEqual(diff, 1);
            r = (Point)c.Convert(l3, typeof(Point), null, null);
            diff = r.X - p3.X;
            Assert.LessOrEqual(diff, 1);
            diff = r.Y - p3.Y;
            Assert.LessOrEqual(diff, 1);

            r = (Point)c.ConvertBack(p1, typeof(Point), null, null);
            diff = r.X - l1.X;
            Assert.LessOrEqual(diff, 0.1);
            diff = r.Y - l1.Y;
            Assert.LessOrEqual(diff, 0.1);
            r = (Point)c.ConvertBack(p2, typeof(Point), null, null);
            diff = r.X - l2.X;
            Assert.LessOrEqual(diff, 0.1);
            diff = r.Y - l2.Y;
            Assert.LessOrEqual(diff, 0.1);
            r = (Point)c.ConvertBack(p3, typeof(Point), null, null);
            diff = r.X - l3.X;
            Assert.LessOrEqual(diff, 0.1);
            diff = r.Y - l3.Y;
            Assert.LessOrEqual(diff, 0.1);
        }


    }
}
