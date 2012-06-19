using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using MeisterGeister.ViewModel.Kampf.Logic;

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
        }

        [Test]
        public void HatXTests()
        {
            Held h = new Held();
            Assert.IsFalse(h.HatSonderfertigkeit(new Sonderfertigkeit()));
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
    }
}
