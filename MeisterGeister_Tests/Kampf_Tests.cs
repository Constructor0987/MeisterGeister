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
using System.Diagnostics;

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
            kampf.Kämpfer[gero].Kampfstil = Kampfstil.BeidhändigerKampf;
            Assert.AreEqual(3, kampf.Kämpfer[gero].Aktionen);
            Assert.AreEqual(2, kampf.Kämpfer[gero].Angriffsaktionen);
            Assert.AreEqual(1, kampf.Kämpfer[gero].Abwehraktionen);
            kampf.Kämpfer[gero].Angriffsaktionen = 3;
            Assert.AreEqual(3, kampf.Kämpfer[gero].Aktionen);
            Assert.AreEqual(3, kampf.Kämpfer[gero].Angriffsaktionen);
            Assert.AreEqual(0, kampf.Kämpfer[gero].Abwehraktionen);
            kampf.Kämpfer[gero].Angriffsaktionen = 0;
            Assert.AreEqual(3, kampf.Kämpfer[gero].Aktionen);
            Assert.AreEqual(0, kampf.Kämpfer[gero].Angriffsaktionen);
            Assert.AreEqual(3, kampf.Kämpfer[gero].Abwehraktionen);
            kampf.Kämpfer[gero].Kampfstil = Kampfstil.Keiner;
            Assert.AreEqual(2, kampf.Kämpfer[gero].Aktionen);
            Assert.AreEqual(kampf.Kämpfer[gero].Aktionen, kampf.Kämpfer[gero].Angriffsaktionen + kampf.Kämpfer[gero].Abwehraktionen);
            Assert.AreEqual(0, kampf.Kämpfer[gero].Angriffsaktionen);
            Assert.AreEqual(2, kampf.Kämpfer[gero].Abwehraktionen);
            kampf.Kämpfer[gero].Kampfstil = Kampfstil.BeidhändigerKampf;
            kampf.Kämpfer[gero].Angriffsaktionen = 2;
            kampf.Kämpfer[gero].Kampfstil = Kampfstil.Keiner;
            Assert.AreEqual(2, kampf.Kämpfer[gero].Aktionen);
            Assert.AreEqual(kampf.Kämpfer[gero].Aktionen, kampf.Kämpfer[gero].Angriffsaktionen + kampf.Kämpfer[gero].Abwehraktionen);
            Assert.AreEqual(1, kampf.Kämpfer[gero].Angriffsaktionen);
            Assert.AreEqual(1, kampf.Kämpfer[gero].Abwehraktionen);
        }

        [Test]
        public void VerbrauchVonAktionenTest()
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

            kampf.Kämpfer[gero].Kampfstil = Kampfstil.BeidhändigerKampf;
            Assert.AreEqual(3, kampf.Kämpfer[gero].Aktionen);
            Assert.AreEqual(3, zant.Aktionen);

            //Jeder macht seine Aktion-Reaktion-Zuteilung
            kampf.Kämpfer[zant].Abwehraktionen = 0;
            Assert.AreEqual(3, kampf.Kämpfer[zant].Angriffsaktionen);
            kampf.Kämpfer[gero].Angriffsaktionen = 3;
            Assert.AreEqual(0, kampf.Kämpfer[gero].Abwehraktionen);
            Assert.AreEqual(kampf.Kämpfer[gero].Aktionen, kampf.Kämpfer[gero].Angriffsaktionen + kampf.Kämpfer[gero].Abwehraktionen);
            //bei der Aktion-Reaktion-Zuteilung regaiert der Kampf und fügt entsprechende Standardaktionen hinzu.
            Assert.AreEqual(3, kampf.InitiativListe.Where(mi => mi.KämpferInfo.Kämpfer == gero).Count());
            Assert.AreEqual(3, kampf.InitiativListe.Where(mi => mi.KämpferInfo.Kämpfer == zant).Count());
            //aktionenreihenfolge
            IKämpfer[] reihenfolge = { gero, //21
                                       zant, //18
                                       gero, //17 Zusatzangriff BHK
                                       zant, //14 Zusatzangriff Aktion+
                                       gero, //13 umgewandelt
                                       zant  //10 umgewandelt
                                     };
            Assert.AreEqual(reihenfolge, kampf.InitiativListe.Select(mi => mi.KämpferInfo.Kämpfer).ToArray(), "Aktionenreihenfolge");
            //zur ausführung:
            Assert.AreEqual(0, kampf.Kämpfer[gero].VerbrauchteAngriffsaktionen);
            Assert.AreEqual(0, kampf.Kämpfer[zant].VerbrauchteAngriffsaktionen);
            var manöver = kampf.Next();
            Assert.AreEqual(1, kampf.Kampfrunde);
            Assert.AreEqual("Attacke", manöver.Manöver.Name);
            Assert.AreEqual(21, manöver.Initiative);
            manöver = kampf.Next();
            Assert.AreEqual(1, kampf.Kämpfer[gero].VerbrauchteAngriffsaktionen);
            Assert.AreEqual("Attacke", manöver.Manöver.Name);
            Assert.AreEqual(18, manöver.Initiative);
            manöver = kampf.Next();
            Assert.AreEqual(1, kampf.Kämpfer[zant].VerbrauchteAngriffsaktionen);
            Assert.AreEqual("Zusätzliche Angriffsaktion", manöver.Manöver.Name);
            Assert.AreEqual(17, manöver.Initiative);
            manöver = kampf.Next();
            Assert.AreEqual(2, kampf.Kämpfer[gero].VerbrauchteAngriffsaktionen);
            Assert.AreEqual("Attacke", manöver.Manöver.Name);
            Assert.AreEqual(14, manöver.Initiative);
            manöver = kampf.Next();
            Assert.AreEqual(2, kampf.Kämpfer[zant].VerbrauchteAngriffsaktionen);
            Assert.AreEqual("Attacke", manöver.Manöver.Name);
            Assert.AreEqual(13, manöver.Initiative);
            manöver = kampf.Next();
            Assert.AreEqual(3, kampf.Kämpfer[gero].VerbrauchteAngriffsaktionen);
            Assert.AreEqual("Attacke", manöver.Manöver.Name);
            Assert.AreEqual(10, manöver.Initiative);
            manöver = kampf.Next();
            //neue Kampfrunde
            Assert.IsNull(manöver);
            Assert.AreEqual(0, kampf.Kämpfer[zant].VerbrauchteAngriffsaktionen);
            Assert.AreEqual(0, kampf.Kämpfer[gero].VerbrauchteAngriffsaktionen);
            Assert.AreEqual(2, kampf.Kampfrunde);
        }

        [Test]
        public void MöglicheManöverTest()
        {
            Held gero = Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").FirstOrDefault();
            Assert.IsNotNull(gero);
            //einen Kampf anlegen
            Kampf kampf = new Kampf();
            //gero hinzufügen
            kampf.Kämpfer.Add(gero); // Implizit Team 1
            kampf.Kämpfer[gero].Initiative = 21;
            kampf.Kämpfer[gero].Kampfstil = Kampfstil.Parierwaffenstil;
            var ki = kampf.Kämpfer[gero];
            var möglicheManöver = Manöver.MöglicheManöver(ki);
            Assert.Greater(möglicheManöver.Count, 0);
            Assert.IsTrue(möglicheManöver.Contains(typeof(TodVonLinks)));
            Assert.IsFalse(möglicheManöver.Contains(typeof(Gegenhalten)));
        }

        [Test]
        public void LängerfristigeHandlungTest()
        {
            Held gero = Global.ContextKampf.Liste<Held>().Where(g => g.Name == "Gero Kalai von Rodaschquell").FirstOrDefault();
            Assert.IsNotNull(gero);
            //einen Kampf anlegen
            Kampf kampf = new Kampf();
            //beide hinzufügen
            kampf.Kämpfer.Add(gero); // Implizit Team 1
            //INI Reihenfolge festlegen
            kampf.Kämpfer[gero].Initiative = 21;
            kampf.Kämpfer[gero].Kampfstil = Kampfstil.BeidhändigerKampf;
            Assert.AreEqual(3, kampf.Kämpfer[gero].Aktionen);
            var m = kampf.InitiativListe[0].Manöver = new LängerfristigeHandlung(kampf.Kämpfer[gero], 3);
            Assert.AreEqual(3, m.VerbleibendeDauer);
            Assert.AreEqual(0, kampf.Kämpfer[gero].Abwehraktionen, "Ein Kämpfer in einer längerfristigen Handlung von min. 2 Aktionen hat keine Abwehraktionen diese KR");
            Assert.AreEqual(m, kampf.InitiativListe[1].Manöver, "Die zweite Aktion wird automatisch festgelegt.");
            kampf.Next(); //sprung zur ersten aktion
            kampf.Next(); //sprung zur zweiten aktion - aktion 1 ausgeführt
            Assert.AreEqual(2, m.VerbleibendeDauer);
            kampf.Next(); //neue kampfrunde - aktion 2 ausgeführt
            Assert.AreEqual(1, m.VerbleibendeDauer);
            Assert.AreEqual(2, kampf.Kampfrunde);
            Assert.AreEqual(m, kampf.InitiativListe[0].Manöver, "Es bleibt eine Aktion Dauer -> Das Manöver ist in der Liste an Platz 0.");
            Assert.AreEqual(3, kampf.Kämpfer[gero].Aktionen, "Wieder 3 Aktionen");
            Assert.AreEqual(3, kampf.Kämpfer[gero].Angriffsaktionen, "Wieder 3 Angriffsaktionen");
            Assert.AreEqual("Attacke", kampf.InitiativListe[1].Manöver.Name, "Noch ein normaler Angriff");
            Assert.AreEqual(-8, kampf.InitiativListe[1].InitiativeMod);
            Assert.AreEqual("Zusätzliche Angriffsaktion", kampf.InitiativListe[2].Manöver.Name, "Und eine Zusatzattacke");
            Assert.AreEqual(-12, kampf.InitiativListe[2].InitiativeMod);
        }

        [Test]
        public void CustomModifikatorTest()
        {
            CustomModifikatorFactory cf = new CustomModifikatorFactory();
            cf.Name = "CMTest";
            string auswirkungen = "";

            Type t = typeof(IModTalentprobe);
            cf.AddModifikator(t); //neuen Modifikator von Typ t (IModTalentProbe)
            var d = cf[t];
            Assert.IsTrue(d.ContainsKey("ApplyTalentprobeMod"));
            Assert.IsTrue(d.ContainsKey("Talentname"));
            Assert.AreEqual(2, d.Count);
            d["Talentname"] = new SortedSet<string>() {"Reiten", "Schleichen"};
            auswirkungen += "Reiten-Probe +5, Schleichen-Probe +5";
            cf.SetModifikator("ApplyTalentprobeMod", "+", 5);
            Assert.AreEqual(0, cf.Errors.Count);

            t = typeof(IModTalentwert);
            cf.AddModifikator(t);
            d = cf[t];
            Assert.IsTrue(d.ContainsKey("ApplyTalentwertMod"));
            Assert.IsTrue(d.ContainsKey("Talentname"));
            Assert.AreEqual(2, d.Count);
            auswirkungen += ", Reiten +2, Schleichen +2";
            cf.SetModifikator("ApplyTalentwertMod", "+", 2);
            Assert.AreEqual(0, cf.Errors.Count);

            t = typeof(IModZauberwert);
            cf.AddModifikator(t);
            d = cf[t];
            Assert.IsTrue(d.ContainsKey("ApplyZauberwertMod"));
            Assert.IsTrue(d.ContainsKey("Zaubername"));
            Assert.AreEqual(2, d.Count);
            d["Zaubername"] = new SortedSet<string>() { "UNITATIO" };
            auswirkungen += ", UNITATIO *3";
            cf.SetModifikator("ApplyZauberwertMod", "*", 3);
            Assert.AreEqual(0, cf.Errors.Count);

            t = typeof(IModAE);
            cf.AddModifikator(t);
            d = cf[t];
            Assert.IsTrue(d.ContainsKey("ApplyAEMod"));
            Assert.AreEqual(1, d.Count);
            auswirkungen += ", AE -2";
            cf.SetModifikator("ApplyAEMod", "-", 2);
            Assert.AreEqual(0, cf.Errors.Count);

            ICustomModifikator result = cf.Finish();
            Assert.IsTrue(result is IModTalentprobe);
            Assert.IsTrue((result as IModTalentprobe).Talentname.Contains("Reiten"));
            Assert.IsTrue((result as IModTalentprobe).Talentname.Contains("Schleichen"));
            Assert.AreEqual(5, (result as IModTalentprobe).ApplyTalentprobeMod(0));

            Assert.IsTrue(result is IModTalentwert);
            Assert.AreEqual(2, (result as IModTalentwert).ApplyTalentwertMod(0));

            Assert.IsTrue(result is IModZauberwert);
            Assert.AreEqual(27, (result as IModZauberwert).ApplyZauberwertMod(9));

            Assert.IsTrue(result is IModAE);
            Assert.AreEqual(8, (result as IModAE).ApplyAEMod(10));

            Assert.AreEqual(auswirkungen, result.Auswirkung);

            string serialized = CustomModifikatorFactory.Serialize(result);
            ICustomModifikator deserialized = CustomModifikatorFactory.Deserialize(serialized);

            Assert.IsTrue(deserialized is IModTalentprobe);
            Assert.IsTrue((deserialized as IModTalentprobe).Talentname.Contains("Reiten"));
            Assert.IsTrue((deserialized as IModTalentprobe).Talentname.Contains("Schleichen"));
            Assert.AreEqual(5, (deserialized as IModTalentprobe).ApplyTalentprobeMod(0));

            Assert.IsTrue(deserialized is IModTalentwert);
            Assert.AreEqual(2, (deserialized as IModTalentwert).ApplyTalentwertMod(0));

            Assert.IsTrue(deserialized is IModZauberwert);
            Assert.AreEqual(27, (deserialized as IModZauberwert).ApplyZauberwertMod(9));

            Assert.IsTrue(deserialized is IModAE);
            Assert.AreEqual(8, (deserialized as IModAE).ApplyAEMod(10));

            Assert.AreEqual(auswirkungen, deserialized.Auswirkung);
        }
    }
}
