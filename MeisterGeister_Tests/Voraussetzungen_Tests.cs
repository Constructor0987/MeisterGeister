using System;
using MeisterGeister.Model;
using NUnit.Framework;
using Global = MeisterGeister.Global;
using MeisterGeister.Logic.Voraussetzungen;


namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Voraussetzungen_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            Global.Init();
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
        public void RekursivePrüfung()
        {
            Sonderfertigkeit kg = Global.ContextHeld.New<Sonderfertigkeit>();
            kg.Name = "Kampfgespür";
            kg.Vorraussetzungen = "IN 15, SF Aufmerksamkeit, SF Kampfreflexe";
            kg.HatWert = false;

            Sonderfertigkeit kr = Global.ContextHeld.New<Sonderfertigkeit>();
            kr.Name = "Kampfreflexe";
            kr.Vorraussetzungen = "INI 10";
            kr.HatWert = false;

            Sonderfertigkeit auf = Global.ContextHeld.New<Sonderfertigkeit>();
            auf.Name = "Aufmerksamkeit";
            auf.Vorraussetzungen = "IN 12";
            auf.HatWert = false;

            Held h = Global.ContextHeld.New<Held>();
            h.Name = "Held mit Kampfgespür";
            h.INI_Mod = 0;
            //INI = (MU * 2 + IN + GE ) / 5.0

            Assert.IsFalse(kr.CheckVoraussetzungen(h), "Zuwenig INI");
            h.AddSonderfertigkeit(kr, null);
            h.AddSonderfertigkeit(auf, null);
            
            h.IN = 8;
            h.MU = 8;
            h.GE = 8;
            Assert.Less(h.InitiativeBasisOhneSonderfertigkeiten, 10, "weniger als 10 INI");
            Assert.IsFalse(kr.CheckVoraussetzungen(h), "zu wenig INI");

            h.IN = 15;
            h.MU = 15;
            h.GE = 15;
            Assert.GreaterOrEqual(h.InitiativeBasisOhneSonderfertigkeiten, 10, "mindestens 10 INI");
            Assert.IsTrue(kr.CheckVoraussetzungen(h), "Alle Voraussetzungen von Kampfreflexen erfüllt");
            Assert.IsTrue(h.HatSonderfertigkeitUndVoraussetzzungen(kr), "Hat Kampfreflexe und die Voraussetzungen");
            Assert.IsFalse(h.HatSonderfertigkeitUndVoraussetzzungen(kg), "Hat kein Kampfgespür");
            Assert.IsTrue(h.HatSonderfertigkeitUndVoraussetzzungen(auf), "Hat Aufmerksamkeit und die Voraussetzungen");
            
            Assert.IsTrue(kg.CheckVoraussetzungen(h), "Alle Voraussetzungen von Kampfgespür erfüllt");

            h.IN = 15;
            h.MU = 10;
            h.GE = 10;
            Assert.Less(h.InitiativeBasisOhneSonderfertigkeiten, 10, "weniger als 10 INI");
            Assert.IsFalse(kg.CheckVoraussetzungen(h), "Alle Voraussetzungen von Kampfgespür erfüllt, aber nicht die von Kampfreflexe");
            Assert.IsFalse(h.HatSonderfertigkeitUndVoraussetzzungen(kr), "Zu wenig INI für Kampfgespür");
            Assert.IsFalse(h.HatSonderfertigkeitUndVoraussetzzungen(kg), "Alle Voraussetzungen von Kampfgespür erfüllt, aber nicht die von Kampfreflexe");
            
            h.IN = 15;
            h.MU = 15;
            h.GE = 15;
            h.AddSonderfertigkeit(kg, null);
            Assert.GreaterOrEqual(h.InitiativeBasisOhneSonderfertigkeiten, 10, "mindestens 10 INI");
            Assert.IsTrue(h.HatSonderfertigkeitUndVoraussetzzungen(kg), "Hat Kampfgespür und die Voraussetzungen");
        }

        [Test]
        public void ParseSomeExpressions()
        {
            Scanner scanner = new Scanner();
            Parser parser = new Parser(scanner);
            ParseTree tree;
            string[] expressions = new string[]
            {
                "",
                "SF Schlangenring-Zauber: Wasserbann",
                "SF Ritualkenntnis (Gildenmagie) \"7\"",
                "GE 12, SF Ausweichen I, SF Aufmerksamkeit",
                "GE 12, Sinnenschärfe 15, SF Kampfgespür", //Blindkampf
                "Reiten 7, Armbrust 7 | Blasrohr 7 | Bogen 7 | Diskus 7 | Schleuder 7 | Wurfbeile 7 | Wurfmesser 7 | Wurfspeere 7", //Berittener Schütze
                "IN 12, GE 12, SF Meisterparade | SF Parierwaffen I", //Binden
                "FF 12, Wurfmesser 10, SF Waffenspezialisierung Wurfringe | SF Waffenspezialisierung Wurfscheiben | SF Waffenspezialisierung Wurfsterne", //Eisenhagel
                "GE 12, AT 8", //Finte
                "Reiten 10, SF Reiterkampf (Streitwagen)", //Kriegsreiterei (Streitwagen)
                "SF Rüstungsgewöhnung \"Langes Kettenhemd\"",
                "V Flink",
                "N Prinzipientreu",
                "N Aberglaube \"6\"",
                "(IN 12, GE 12) | V Flink", // ) darf nicht nach einem Leerzeichen kommen
                "!N Unset",
                "!(N Aberglaube \"6\")",
                "IN 15, SF Ritualkenntnis (Gildenmagie) \"7\", !(SF Ritualkenntnis (Alchmie) | !Alchemie 21)",
                "KL 15, IN 15, Magiekunde 12, V Begabung für Merkmal (Elementar (gesamt)) | V Begabung für Merkmal (Elementar (Humus)) | V Begabung für Merkmal (Elementar (Erz)) | V Begabung für Merkmal (Elementar (Eis)) | V Begabung für Merkmal (Elementar (Feuer)) | V Begabung für Merkmal (Elementar (Luft)) | V Begabung für Merkmal (Elementar (Wasser)) | SF Merkmalskenntnis (Elementar (Eis)) | SF Merkmalskenntnis (Elementar (Erz)) | SF Merkmalskenntnis (Elementar (Feuer)) | SF Merkmalskenntnis (Elementar (gesamt)) | SF Merkmalskenntnis (Elementar (Humus)) | SF Merkmalskenntnis (Elementar (Luft)) | SF Merkmalskenntnis (Elementar (Wasser)), !(V Begabung für Merkmal (Dämonisch (gesamt)) | V Begabung für Merkmal (Dämonisch (Blakharaz)) | V Begabung für Merkmal (Dämonisch (Belhalhar)) | V Begabung für Merkmal (Dämonisch (Charyptoroth)) | V Begabung für Merkmal (Dämonisch (Lolgramoth)) | V Begabung für Merkmal (Dämonisch (Thargunitoth)) | V Begabung für Merkmal (Dämonisch (Amazeroth)) | V Begabung für Merkmal (Dämonisch (Belshirash)) | V Begabung für Merkmal (Dämonisch (Asfaloth)) | V Begabung für Merkmal (Dämonisch (Tasfarelel)) | V Begabung für Merkmal (Dämonisch (Belzhorash)) | V Begabung für Merkmal (Dämonisch (Agrimoth)) | V Begabung für Merkmal (Dämonisch (Belkelel)) | N Affinität zu Dämonen | N Animalische Magie | N Arkanophobie | N Astraler Block | N Schwache Ausstmhlung | N Schwacher Astralkörper | N Unstet | SF Merkmalskenntnis (Dämonisch (Agrimoth)) | SF Merkmalskenntnis (Dämonisch (Amazeroth)) | SF Merkmalskenntnis (Dämonisch (Asfaloth)) | SF Merkmalskenntnis (Dämonisch (Belhalhar)) | SF Merkmalskenntnis (Dämonisch (Belkelel)) | SF Merkmalskenntnis (Dämonisch (Belshirash)) | SF Merkmalskenntnis (Dämonisch (Belzhorash)) | SF Merkmalskenntnis (Dämonisch (Blakharaz)) | SF Merkmalskenntnis (Dämonisch (Charyptoroth)) | SF Merkmalskenntnis (Dämonisch (gesamt)) | SF Merkmalskenntnis (Dämonisch (Lolgramoth)) | SF Merkmalskenntnis (Dämonisch (Tasfarelel)) | SF Merkmalskenntnis (Dämonisch (Thargunitoth)))" //Elementarharmonisierte Aura
            };
            foreach (string input in expressions)
            {
                tree = parser.Parse(input);
                Assert.AreEqual(0, tree.Errors.Count, String.Format("ParseError bei der Expression {0}", input) );
            }
        }
    }
}
