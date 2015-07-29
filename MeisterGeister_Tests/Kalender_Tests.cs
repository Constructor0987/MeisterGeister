using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using Global = MeisterGeister.Global;
using MeisterGeister.Logic.Kalender.DsaTool;
using System.Diagnostics;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Kalender_Tests
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
        public void DatumKonvertierenTest()
        {
            DSADateCalendarTwelve c12 = new DSADateCalendarTwelve();
            c12.setDayMonthYear(2, 1, 1);
            Assert.AreEqual(366, c12.getDaysSinceBF());
            c12.setDayMonthYear(26, 12, 1038);
            Assert.AreEqual("26.12. (Rahja) 1038 BF", c12.getHeadingText());
            DSADateCalendarNovadi cnov = new DSADateCalendarNovadi();
            cnov.setDayWeekMonthYear(5, 8, 2, 279);
            Assert.AreEqual("5. Tag (Heschinja) des 8. Gottesnamens nach dem 2. Rastullahellah des Jahres 279 Rastullah (n. d. O.)", cnov.getHeadingText());
            int i1 = c12.getDaysSinceBF();
            int i2 = cnov.getDaysSinceBF();
            Assert.AreEqual(i1, i2);

            DSADateCalendarMyranisch cmyr = new DSADateCalendarMyranisch();
            cmyr.setDayNoneOctadeYear(5, 5, 8, 4785);
            Assert.AreEqual("5. Tag der 5. None der 8. Oktade 4785 IZ", cmyr.getHeadingText());
            i2 = cmyr.getDaysSinceBF();
            Assert.AreEqual(i1, i2);

        }
    }
}
