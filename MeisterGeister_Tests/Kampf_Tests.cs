using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using MeisterGeister.ViewModel.Kampf.Logic;
using Global = MeisterGeister.Global;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Kampf_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            Global.Init();
            //Helden importieren.
            if(Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").Count() == 0)
                Held.Import("Daten\\Helden\\Gero Kalai von Rodaschquell.xml");
            //Gegner importieren
            if(Global.ContextKampf.Liste<Gegner>().Where(g => g.Name == "Zant").Count() == 0)
                Gegner.Import("Daten\\Gegner\\Zant.xml");
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
        public void TPKKTests()
        {
            Held h1 = new Held();
            h1.KK = 10;
            Waffe w1 = new Waffe();
            w1.TPKKSchwelle = 13;
            w1.TPKKSchritt = 3;
            Assert.AreEqual(-1, w1.TPKKBonus(h1));
            h1.KK = 12;
            Assert.AreEqual(0, w1.TPKKBonus(h1));
            h1.KK = 14;
            Assert.AreEqual(0, w1.TPKKBonus(h1));
            h1.KK = 16;
            Assert.AreEqual(1, w1.TPKKBonus(h1));
            h1.KK = 20;
            Assert.AreEqual(2, w1.TPKKBonus(h1));
        }

        [Test]
        public void INITests()
        {
            Gegner zant = Global.ContextKampf.Liste<Gegner>().Where(g => g.Name == "Zant").First();
            Assert.IsNotNull(zant);
            Held gero = Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").First();
            Assert.IsNotNull(gero);
            //einen Kampf anlegen
            Kampf kampf = new Kampf();
            //beide hinzufügen
            kampf.Kämpfer.Add(gero); // Implizit Team 1
            kampf.Kämpfer.Add(zant, 2);
            //INI Reihenfolge testen
            kampf.Kämpfer[gero].Initiative = 21;
            kampf.Kämpfer[zant].Initiative = 18;
            Assert.AreEqual(kampf.Kämpfer[0].Kämpfer.Name, gero.Name, "Gero vor Zant");
            Assert.AreEqual(kampf.Kämpfer[1].Kämpfer.Name, zant.Name, "Gero vor Zant");
            kampf.Kämpfer[gero].Initiative = 12;
            Assert.AreEqual(kampf.Kämpfer[1].Kämpfer.Name, gero.Name, "Gero hinter Zant");
            Assert.AreEqual(kampf.Kämpfer[0].Kämpfer.Name, zant.Name, "Gero hinter Zant");
            kampf.Orientieren(gero);
            Assert.Greater(kampf.Kämpfer[gero].Initiative, 18);
            Assert.AreEqual(kampf.Kämpfer[0].Kämpfer.Name, gero.Name, "Gero vor Zant");
        }

        [Test]
        public void ManöverTests()
        {
            Gegner zant = Global.ContextKampf.Liste<Gegner>().Where(g => g.Name == "Zant").First();
            Assert.IsNotNull(zant);
            Held gero = Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").First();
            Assert.IsNotNull(gero);
            //einen Kampf anlegen
            Kampf kampf = new Kampf();
            //beide hinzufügen
            kampf.Kämpfer.Add(gero); // Implizit Team 1
            kampf.Kämpfer.Add(zant, 2);
            //INI Reihenfolge festlegen
            kampf.Kämpfer[gero].Initiative = 21;
            kampf.Kämpfer[zant].Initiative = 18;

            kampf.Kämpfer[gero].Kämpfer.Kampfstil = Kampfstil.BeidhändigerKampf;
            Assert.AreEqual(3, gero.Aktionen);
            
            Assert.AreEqual(3, zant.Aktionen);
        }
    }
}
