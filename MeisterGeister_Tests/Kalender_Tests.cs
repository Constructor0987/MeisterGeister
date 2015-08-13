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
            int i1, i2;
            DSADateCalendarTwelve c12 = new DSADateCalendarTwelve();
            c12.setDate(2, 0, 1, 1);
            Assert.AreEqual(366, c12.DaysSinceBF);
            Assert.AreEqual(2, c12.Day, "12");
            Assert.AreEqual(1, c12.Month, "12");
            Assert.AreEqual(1, c12.Year, "12");
            c12.setDate(26, 0, 12, 1038);
            Assert.AreEqual("26.12. (Rahja) 1038 BF", c12.getHeadingText());

            DSADateCalendarNovadi cnov = new DSADateCalendarNovadi();
            cnov.setDate(5, 8, 3, 279);
            //Assert.AreEqual(5, cnov.Day, "Novadi");
            Assert.AreEqual(5, cnov.WeekDay, "Novadi");
            Assert.AreEqual(8, cnov.Week, "Novadi");
            Assert.AreEqual(3, cnov.Month, "Novadi");
            Assert.AreEqual(279, cnov.Year, "Novadi");
            Assert.AreEqual("5. Tag (Heschinja) des 8. Gottesnamens nach dem 2. Rastullahellah des Jahres 279 Rastullah (n. d. O.)", cnov.getHeadingText());
            i1 = c12.DaysSinceBF;
            i2 = cnov.DaysSinceBF;
            Assert.AreEqual(i1, i2, "Novadi");

            c12.setDate(17, 0 , 11, 1023);
            Assert.AreEqual("17.11. (Ingerimm) 1023 BF", c12.getHeadingText());
            Assert.AreEqual(17, c12.Day, "12");
            Assert.AreEqual(6, c12.WeekDay, "12");
            Assert.AreEqual(11, c12.Month, "12");
            Assert.AreEqual(1023, c12.Year, "12");

            DSADateCalendarMyranisch cmyr = new DSADateCalendarMyranisch();
            cmyr.setDate(6, 5, 3, 4770);
            Assert.AreEqual(6, cmyr.WeekDay, "Myranor");
            Assert.AreEqual(5, cmyr.Week, "Myranor");
            Assert.AreEqual(3, cmyr.Month, "Myranor");
            Assert.AreEqual(4770, cmyr.Year, "Myranor");
            Assert.AreEqual("Rechtstag (6) der 5. None im Zatura (3) 4770 IZ", cmyr.getHeadingText());
            i1 = c12.DaysSinceBF;
            i2 = cmyr.DaysSinceBF;
            Assert.AreEqual(i1, i2, "Myranor");

            DSADateCalendarThorwal cthor = new DSADateCalendarThorwal();
            cthor.setDate(17, 0, 11, 2651);
            Assert.AreEqual(17, cthor.Day, "Thorwal");
            Assert.AreEqual(6, cthor.WeekDay, "Thorwal");
            Assert.AreEqual(11, cthor.Month, "Thorwal");
            Assert.AreEqual(2651, cthor.Year, "Thorwal");
            i2 = cthor.DaysSinceBF;
            Assert.AreEqual(i1, i2, "Thorwal");

            DSADateCalendarGjalskerland cgjal = new DSADateCalendarGjalskerland();
            cgjal.setDate(5, 2, 8, 2400);
            Assert.AreEqual(5, cgjal.WeekDay, "Gjalsker");
            Assert.AreEqual(2, cgjal.Week, "Gjalsker");
            Assert.AreEqual(8, cgjal.Month, "Gjalsker");
            Assert.AreEqual(2400, cgjal.Year, "Gjalsker");
            i2 = cgjal.DaysSinceBF;
            Assert.AreEqual(i1, i2, "Gjalsker");

            DSADateCalendarOrcs corks = new DSADateCalendarOrcs();
            corks.setDate(10, 0, 171, 2000);
            Assert.AreEqual(10, corks.Day, "Orks");
            Assert.AreEqual(171, corks.Month, "Orks");
            Assert.AreEqual(2000, corks.Year, "Orks");
            i2 = corks.DaysSinceBF;
            Assert.AreEqual(i1, i2, "Orks");

            DSADateCalendarSaurians cechs = new DSADateCalendarSaurians();
            int a = cechs.Era;
            Assert.AreEqual(18, cechs.Day, "Echsen");
            Assert.AreEqual(3, cechs.WeekDay, "Echsen");
            Assert.AreEqual(219, cechs.Month, "Echsen");
            Assert.AreEqual(5, cechs.Year, "Echsen");
            Assert.AreEqual(2, cechs.Era, "Echsen");
            Assert.AreEqual(4, cechs.Eon, "Echsen");
            cechs.setDate(17, 0, 483, 5, 4, 4);
            Assert.AreEqual(17, cechs.Day, "Echsen");
            Assert.AreEqual(2, cechs.WeekDay, "Echsen");
            Assert.AreEqual(483, cechs.Month, "Echsen");
            Assert.AreEqual(5, cechs.Year, "Echsen");
            Assert.AreEqual(4, cechs.Era, "Echsen");
            Assert.AreEqual(4, cechs.Eon, "Echsen");
            i2 = cechs.DaysSinceBF;
            Assert.AreEqual(i1, i2, "Echsen");


        }
    }
}
