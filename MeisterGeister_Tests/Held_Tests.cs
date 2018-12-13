using System;
using System.Linq;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Kampf.Logic;
using NUnit.Framework;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Held_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            #region Testdaten

            testHeld1 = new Held
            {
                Name = "Hartmann von Falkenstein",
                MU = 16,
                KL = 12,
                IN = 15,
                CH = 17,
                FF = 10,
                GE = 14,
                KO = 17,
                KK = 17,
                BE = 0,
                INI_Mod = 0,
                LE_Mod = 13,
                LE_Aktuell = 39,
                AU_Mod = 19,
                AU_Aktuell = 43,
                AE_Aktuell = 22,
                KE_Mod = 24,
                KE_Aktuell = 24,
                MR_Mod = -2,
                Bild = "http://www.wiki-aventurica.de/images/a/aa/SC_Hartmann_von_Falkenstein.jpg",
                Rasse = "Mittelländer",
                Kultur = "Mittelländische Landbevölkerung (Landadel)",
                Profession = "Ritter alten Schlages/ Fasarer Gladiator",
                RSBrust = 5,
                RSKopf = 7,
                RSRücken = 5,
                RSArmL = 4,
                RSArmR = 4,
                RSBauch = 5,
                RSBeinL = 4,
                RSBeinR = 4,
                SO = 16
            };

            testHeld2 = new Held
            {
                Name = "Held Zwei",
                MU = 16,
                KL = 12,
                IN = 15,
                CH = 17,
                FF = 10,
                GE = 14,
                KO = 17,
                KK = 17,
                BE = 0,
                INI_Mod = 0,
                LE_Mod = 13,
                LE_Aktuell = 39,
                AU_Mod = 19,
                AU_Aktuell = 43,
                AE_Aktuell = 22,
                KE_Mod = 24,
                KE_Aktuell = 24,
                MR_Mod = -2,
                Bild = "http://www.wiki-aventurica.de/images/a/aa/SC_Hartmann_von_Falkenstein.jpg",
                Rasse = "Mittelländer",
                Kultur = "Zweite Kultur",
                Profession = "Zweite Profession",
                RSBrust = 5,
                RSKopf = 7,
                RSRücken = 5,
                RSArmL = 4,
                RSArmR = 4,
                RSBauch = 5,
                RSBeinL = 4,
                RSBeinR = 4,
                SO = 16
            };

            #endregion Testdaten
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
        public void DependendtPropertyTests()
        {
            firedEvents = string.Empty;
            var h = new Held();
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
            var h = new Held();
            Assert.IsFalse(h.HatSonderfertigkeitUndVoraussetzungen(new Sonderfertigkeit()));
            Assert.IsFalse(h.HatVorNachteil(new VorNachteil()));
        }

        [Test]
        [Ignore]
        public void TrefferzonenTests()
        {
            var h = new Held
            {
                RSKopf = 1
            };
            Assert.AreEqual(h.RSKopf, h.RS[Trefferzone.Kopf]);
            h.RSKopf = 2;
            Assert.AreEqual(h.RSKopf, h.RS[Trefferzone.Kopf]);
            h.RS[Trefferzone.Gesamt] = 3;
            Assert.AreEqual(h.RS[Trefferzone.Kopf], h.RSKopf);
        }

        [Test]
        public void EnergieModifikatorTests()
        {
            testHeld1.LebensenergieAktuell = testHeld1.LebensenergieMax;
            testHeld1.Modifikatoren.Clear();
            Assert.Greater(testHeld1.LebensenergieAktuell, 10, "Mehr als 10 LeP");
            Assert.AreEqual(0, testHeld1.Modifikatoren.Count, "Keine Modifikatoren");
            var gs = testHeld1.Geschwindigkeit;
            Assert.Greater(gs, 1);
            testHeld1.LebensenergieAktuell = (int)Math.Round(testHeld1.LebensenergieMax * 1.0 / 2.0) - 1;
            Assert.AreEqual(1, testHeld1.Modifikatoren.Where(m => m is Mod.NiedrigeLebensenergieModifikator).Count(), "Ein Modifikator");
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

        private Held testHeld1, testHeld2;
        private string firedEvents = string.Empty;

        private void OnPropertyChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            firedEvents += "," + args.PropertyName;
            System.Diagnostics.Debug.WriteLine(args.PropertyName);
        }
    }
}
