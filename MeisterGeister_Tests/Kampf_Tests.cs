using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using MeisterGeister.ViewModel.Kampf.Logic;
using Global = MeisterGeister.Global;
using MeisterGeister.ViewModel.Kampf.Logic.Manöver;
using MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

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
            if (Global.ContextKampf.Liste<GegnerBase>().Where(g => g.Name == "Zant").Count() == 0)
                GegnerBase.Import("Daten\\Gegner\\Zant.xml");
            Gegner zant = Global.ContextKampf.Liste<Gegner>().Where(g => g.Name == "Zant").FirstOrDefault();
            if (zant == null)
            {
                GegnerBase zantBase = Global.ContextHeld.Liste<GegnerBase>().Where(g => g.Name == "Zant").First();
                zant = Global.ContextHeld.CreateGegnerInstance(zantBase);
            }
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
            Gegner zant = Global.ContextKampf.Liste<Gegner>().Where(g => g.Name == "Zant").FirstOrDefault();
            Assert.IsNotNull(zant);
            Held gero = Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").FirstOrDefault();
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
        public void UmwandelnTest()
        {
            Held gero = Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").FirstOrDefault();
            Assert.IsNotNull(gero);
            //einen Kampf anlegen
            Kampf kampf = new Kampf();
            //beide hinzufügen
            kampf.Kämpfer.Add(gero); // Implizit Team 1
            kampf.Kämpfer[gero].Initiative = 21;
            kampf.Kämpfer[gero].Kämpfer.Kampfstil = Kampfstil.BeidhändigerKampf;
            Assert.AreEqual(3, gero.Aktionen);
            Assert.AreEqual(2, gero.Angriffsaktionen);
            Assert.AreEqual(1, gero.Abwehraktionen);
            gero.Angriffsaktionen = 3;
            Assert.AreEqual(3, gero.Aktionen);
            Assert.AreEqual(3, gero.Angriffsaktionen);
            Assert.AreEqual(0, gero.Abwehraktionen);
            gero.Angriffsaktionen = 0;
            Assert.AreEqual(3, gero.Aktionen);
            Assert.AreEqual(0, gero.Angriffsaktionen);
            Assert.AreEqual(3, gero.Abwehraktionen);
            kampf.Kämpfer[gero].Kämpfer.Kampfstil = Kampfstil.Keiner;
            Assert.AreEqual(2, gero.Aktionen);
            Assert.AreEqual(gero.Aktionen, gero.Angriffsaktionen + gero.Abwehraktionen);
            Assert.AreEqual(0, gero.Angriffsaktionen);
            Assert.AreEqual(2, gero.Abwehraktionen);
            kampf.Kämpfer[gero].Kämpfer.Kampfstil = Kampfstil.BeidhändigerKampf;
            gero.Angriffsaktionen = 2;
            kampf.Kämpfer[gero].Kämpfer.Kampfstil = Kampfstil.Keiner;
            Assert.AreEqual(2, gero.Aktionen);
            Assert.AreEqual(gero.Aktionen, gero.Angriffsaktionen + gero.Abwehraktionen);
            Assert.AreEqual(1, gero.Angriffsaktionen);
            Assert.AreEqual(1, gero.Abwehraktionen);
        }

        [Test]
        [Ignore] // Verhalten nicht mehr aktuell, wie der Test gerade geschrieben ist.
        public void ManöverTests()
        {
            Gegner zant = Global.ContextKampf.Liste<Gegner>().Where(g => g.Name == "Zant").FirstOrDefault();
            Assert.IsNotNull(zant);
            Held gero = Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").FirstOrDefault();
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

            kampf.NeueKampfrunde();
            //Jeder macht seine Aktion-Reaktion-Zuteilung
            zant.Abwehraktionen = 1;
            zant.Angriffsaktionen = 2;
            gero.Angriffsaktionen = 3;
            gero.Abwehraktionen = 0;
            Assert.AreEqual(gero.Aktionen, gero.Angriffsaktionen + gero.Abwehraktionen);
            //nun werden aktionen geplant, diese phase beginnt automatisch mit der ersten geplanten aktion.
            //der kampf sollte dann eventuell prüfen, ob auch alle kämpfer korrekte angaben zur umwandlung gemacht haben.
            ManöverInfo[] mInfos = {
                                       new ManöverInfo(kampf.Kämpfer[gero], new Attacke(gero), 0),
                                       new ManöverInfo(kampf.Kämpfer[zant], new Attacke(zant), 0),
                                       new ManöverInfo(kampf.Kämpfer[gero], new Attacke(gero), -4),
                                       new ManöverInfo(kampf.Kämpfer[gero], new Attacke(gero), -8),
                                       new ManöverInfo(kampf.Kämpfer[zant], new Attacke(zant), -8)
                                   }; 
            kampf.InitiativListe.Add(mInfos[1]);
            //Assert.IsFalse(kampf.UmwandelnMöglich, "ab jetzt sind die Aktion-Reaktion-Zuteilungen gesperrt");
            //ausser man hat Aufmerksamkeit oder Kampfgespür
            //auch wenn der zant aufmerksamkeit hat, ist er nun geperrt, da seine erste aktion durch ist.
            kampf.InitiativListe.Add(mInfos[4]);
            kampf.InitiativListe.Add(mInfos[0]);
            kampf.InitiativListe.Add(mInfos[2]);
            kampf.InitiativListe.Add(mInfos[3]);
            //Ini-Liste ist korrekt sortiert
            for(int i=0; i<mInfos.Length; i++)
                Assert.AreEqual(mInfos[i], kampf.InitiativListe[i], "Ini-Liste ist korrekt sortiert");

            //zur ausführung.
            ManöverInfo mi = null;
            //kampf sollte hier wieder prüfen, ob ein kämpfer überhaupt keine aktionen angesagt hat und das eventuell melden
            while((mi = kampf.Next()) != null)
            {
                Console.WriteLine(String.Format("In INI-Phase {0} führt {2} ein(e) {1} aus. Das dauert {3} Aktionen.", mi.Initiative, mi.Manöver.Name, mi.Manöver.Ausführender, mi.Manöver.Dauer));
                mi.Ausgeführt = true;
            }
            
            
        }

        [Test]
        public void CustomModifikatorTest()
        {
            CustomModifikatorFactory cf = new CustomModifikatorFactory();
            cf.Name = "CMTest";
            string auswirkungen = "";
            Type t = typeof(IModTalentprobe);
            cf.AddModifikator(t);
            var d = cf[t];
            Assert.IsTrue(d.ContainsKey("ApplyTalentprobeMod"));
            Assert.AreEqual(typeof(string), d["Talentname"].GetType());
            Assert.AreEqual(2, d.Count);
            d["Talentname"] = "Reiten";
            auswirkungen += "Talentprobe +5";
            cf.SetModifikator("ApplyTalentprobeMod", "+", 5);
            Assert.AreEqual(0, cf.Errors.Count);

            t = typeof(IModAE);
            cf.AddModifikator(t);
            d = cf[t];
            Assert.IsTrue(d.ContainsKey("ApplyAEMod"));
            Assert.AreEqual(1, d.Count);
            auswirkungen += ", AE -2";
            cf.SetModifikator("ApplyAEMod", "-", 2);
            Assert.AreEqual(0, cf.Errors.Count);

            IModifikator result = cf.Finish();
            Assert.IsTrue(result is IModTalentprobe);
            Assert.AreEqual("Reiten", (result as IModTalentprobe).Talentname);
            Assert.IsTrue(result is IModAE);
            Assert.AreEqual(auswirkungen, result.Auswirkung);
        }
    }
}
