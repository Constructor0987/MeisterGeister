using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using MeisterGeister.ViewModel.Kampf.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Held_Tests
    {
        Held testHeld1, testHeld2;

        [TestFixtureSetUp]
        public void SetupMethods()
        {
            #region Testdaten
            testHeld1 = new Held();
            testHeld1.Name = "Hartmann von Falkenstein";
            testHeld1.MU = 16;
            testHeld1.KL = 12;
            testHeld1.IN = 15;
            testHeld1.CH = 17;
            testHeld1.FF = 10;
            testHeld1.GE = 14;
            testHeld1.KO = 17;
            testHeld1.KK = 17;
            testHeld1.BE = 0;
            testHeld1.INI_Mod = 0;
            testHeld1.LE_Mod = 13;
            testHeld1.LE_Aktuell = 39;
            testHeld1.AU_Mod = 19;
            testHeld1.AU_Aktuell = 43;
            testHeld1.AE_Aktuell = 22;
            testHeld1.KE_Mod = 24;
            testHeld1.KE_Aktuell = 24;
            testHeld1.MR_Mod = -2;
            testHeld1.BildLink = "http://www.wiki-aventurica.de/images/a/aa/SC_Hartmann_von_Falkenstein.jpg";
            testHeld1.Rasse = "Mittelländer";
            testHeld1.Kultur = "Mittelländische Landbevölkerung (Landadel)";
            testHeld1.Profession = "Ritter alten Schlages/ Fasarer Gladiator";
            testHeld1.RSBrust = 5;
            testHeld1.RSKopf = 7;
            testHeld1.RSRücken = 5;
            testHeld1.RSArmL = 4;
            testHeld1.RSArmR = 4;
            testHeld1.RSBauch = 5;
            testHeld1.RSBeinL = 4;
            testHeld1.RSBeinR = 4;
            testHeld1.SO = 16;

            testHeld2 = new Held();
            testHeld2.Name = "Held Zwei";
            testHeld2.MU = 16;
            testHeld2.KL = 12;
            testHeld2.IN = 15;
            testHeld2.CH = 17;
            testHeld2.FF = 10;
            testHeld2.GE = 14;
            testHeld2.KO = 17;
            testHeld2.KK = 17;
            testHeld2.BE = 0;
            testHeld2.INI_Mod = 0;
            testHeld2.LE_Mod = 13;
            testHeld2.LE_Aktuell = 39;
            testHeld2.AU_Mod = 19;
            testHeld2.AU_Aktuell = 43;
            testHeld2.AE_Aktuell = 22;
            testHeld2.KE_Mod = 24;
            testHeld2.KE_Aktuell = 24;
            testHeld2.MR_Mod = -2;
            testHeld2.BildLink = "http://www.wiki-aventurica.de/images/a/aa/SC_Hartmann_von_Falkenstein.jpg";
            testHeld2.Rasse = "Mittelländer";
            testHeld2.Kultur = "Zweite Kultur";
            testHeld2.Profession = "Zweite Profession";
            testHeld2.RSBrust = 5;
            testHeld2.RSKopf = 7;
            testHeld2.RSRücken = 5;
            testHeld2.RSArmL = 4;
            testHeld2.RSArmR = 4;
            testHeld2.RSBauch = 5;
            testHeld2.RSBeinL = 4;
            testHeld2.RSBeinR = 4;
            testHeld2.SO = 16;
            #endregion
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

        private string firedEvents = String.Empty;
        [Test]
        public void DependendtPropertyTests()
        {
            firedEvents = String.Empty;
            Held h = new Held();
            h.PropertyChanged += OnPropertyChanged;
            h.Name = "Tester";
            Assert.AreEqual(",Kurzname,Name", firedEvents);
            h.Name = "Toaster";
            Assert.AreEqual(",Kurzname,Name,Kurzname,Name", firedEvents);
            h.PropertyChanged -= OnPropertyChanged;
        }

        [Test]
        public void HatXTests()
        {
            Held h = new Held();
            Assert.IsFalse(h.HatSonderfertigkeitUndVoraussetzungen(new Sonderfertigkeit()));
            Assert.IsFalse(h.HatVorNachteil(new VorNachteil()));
        }

        private void OnPropertyChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            firedEvents += "," + args.PropertyName;
            System.Diagnostics.Debug.WriteLine(args.PropertyName);
        }

        [Test]
        [Ignore]
        public void TrefferzonenTests()
        {
            Held h = new Held();
            h.RSKopf = 1;
            Assert.AreEqual(h.RSKopf, ((IKämpfer)h).RS[Trefferzone.Kopf]);
            h.RSKopf = 2;
            Assert.AreEqual(h.RSKopf, ((IKämpfer)h).RS[Trefferzone.Kopf]);
            ((IKämpfer)h).RS[Trefferzone.Gesamt] = 3;
            Assert.AreEqual(((IKämpfer)h).RS[Trefferzone.Kopf], h.RSKopf);
        }

        [Test]
        public void EnergieModifikatorTests()
        {
            testHeld1.LebensenergieAktuell = testHeld1.LebensenergieMax;
            testHeld1.Modifikatoren.Clear();
            Assert.Greater(testHeld1.LebensenergieAktuell, 10, "Mehr als 10 LeP");
            Assert.AreEqual(0, testHeld1.Modifikatoren.Count, "Keine Modifikatoren");
            int gs = testHeld1.Geschwindigkeit;
            Assert.Greater(gs, 1);
            testHeld1.LebensenergieAktuell = (int)Math.Round(testHeld1.LebensenergieMax * 1.0 / 2.0) - 1;
            Assert.AreEqual(1, testHeld1.Modifikatoren.Where(m => m is Mod.NiedrigeLebensenergieModifikator).Count() , "Ein Modifikator");
            Assert.Less(testHeld1.Geschwindigkeit, gs);
            testHeld1.LebensenergieAktuell = testHeld1.LebensenergieMax;
            Assert.AreEqual(0, testHeld1.Modifikatoren.Where(m => m is Mod.NiedrigeLebensenergieModifikator).Count(), "Kein Modifikator");
            testHeld1.LebensenergieAktuell = (int)Math.Round(testHeld1.LebensenergieMax * 1.0 / 3.0) - 1;
            Assert.AreEqual(2, testHeld1.Modifikatoren.Where(m => m is Mod.NiedrigeLebensenergieModifikator).Count(), "Zwei Modifikatoren");
            testHeld1.LebensenergieAktuell = (int)Math.Round(testHeld1.LebensenergieMax * 1.0 / 4.0) - 1;
            Assert.AreEqual(3, testHeld1.Modifikatoren.Where(m => m is Mod.NiedrigeLebensenergieModifikator).Count(), "Drei Modifikatoren");
            testHeld1.LebensenergieAktuell = 5;
            Assert.AreEqual(1, testHeld1.Modifikatoren.Where(m => m is Mod.LebensenergieKampfunfähigModifikator).Count(), "Kampfunfähig");
            Assert.AreEqual(1, testHeld1.Geschwindigkeit);
            testHeld1.LebensenergieAktuell = testHeld1.LebensenergieMax;
            
            testHeld1.AusdauerAktuell = testHeld1.AusdauerMax;
            Assert.AreEqual(0, testHeld1.Modifikatoren.Count, "Keine Modifikatoren");
            testHeld1.AusdauerAktuell = (int)Math.Round(testHeld1.AusdauerMax * 1.0 / 3.0) - 1;
            Assert.AreEqual(1, testHeld1.Modifikatoren.Where(m => m is Mod.NiedrigeAusdauerModifikator).Count(), "Ein Modifikator");
            testHeld1.AusdauerAktuell = (int)Math.Round(testHeld1.AusdauerMax * 1.0 / 4.0) - 1;
            Assert.AreEqual(2, testHeld1.Modifikatoren.Where(m => m is Mod.NiedrigeAusdauerModifikator).Count(), "Zwei Modifikatoren");
            testHeld1.AusdauerAktuell = 0;
            Assert.AreEqual(1, testHeld1.Modifikatoren.Where(m => m is Mod.AusdauerKampfunfähigModifikator).Count(), "Kampfunfähig");

            testHeld1.LebensenergieAktuell = testHeld1.LebensenergieMax;
            testHeld1.AusdauerAktuell = testHeld1.AusdauerMax;
        }

        [Test]
        public void WundenModifikatorTests()
        {
            testHeld1.LebensenergieAktuell = testHeld1.LebensenergieMax;
            testHeld1.AusdauerAktuell = testHeld1.AusdauerMax;
            testHeld1.Wunden = 0;
            ((IKämpfer)testHeld1).Wunden[Trefferzone.Bauch] = 1;
            Assert.Less(testHeld1.LebensenergieAktuell, testHeld1.LebensenergieMax, "LeP-Verlust duch Bauchwunde");
            Assert.AreEqual(1, testHeld1.GetModifikatorCount<Mod.WundenBauchModifikator>());
            ((IKämpfer)testHeld1).Wunden[Trefferzone.Bauch] = 0;
            Assert.AreEqual(0, testHeld1.GetModifikatorCount<Mod.WundenBauchModifikator>());
        }
    }
}
