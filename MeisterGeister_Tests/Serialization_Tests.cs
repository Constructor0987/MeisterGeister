using System;
using System.Collections.Generic;
using System.IO;
using MeisterGeister.Model;
using MeisterGeister.Model.Service;
using NUnit.Framework;
using Global = MeisterGeister.Global;
using System.Linq;


namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Serialization_Tests
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
        public void ImportManualCreatedHeld()
        {
            SerializationService serializer = SerializationService.GetInstance(true);
            Held h1 = new Held();
            Guid heldGuid = h1.HeldGUID;
            Held_Sonderfertigkeit hs = new Held_Sonderfertigkeit();
            hs.HeldGUID = h1.HeldGUID;
            hs.SonderfertigkeitGUID = Global.ContextHeld.LoadSonderfertigkeitByName("Kampfreflexe").SonderfertigkeitGUID;
            h1.Held_Sonderfertigkeit.Add(hs);
            Assert.IsTrue(serializer.InsertOrUpdateHeld(h1));
            Assert.IsFalse(heldGuid == Guid.Empty);
            Global.ContextHeld.UpdateList<Held>();
            Held h2 = Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
            Assert.IsNotNull(h2);
            Assert.IsNotNull(h2.Held_Sonderfertigkeit.FirstOrDefault());
            Assert.AreEqual("Kampfreflexe", h2.Held_Sonderfertigkeit.FirstOrDefault().Sonderfertigkeit.Name);
        }

        //[Test]
        //public void Exportheld()
        //{
        //    string exportPfad = "Daten\\Helden\\Export";
        //    if (!Directory.Exists(exportPfad))
        //        Directory.CreateDirectory(exportPfad);
        //    Held h1 = Global.ContextKampf.Liste<Held>().First();
        //    Assert.IsNotNull(h1);
        //    string fileName = Path.Combine(exportPfad, Path.ChangeExtension(h1.Name, "xml"));
        //    h1.Export(fileName);
        //    Assert.IsTrue(File.Exists(fileName));
        //}

        [Test]
        public void ExportGegner()
        {
            string exportPfad = "Daten\\Gegner\\Export";
            if (!Directory.Exists(exportPfad))
                Directory.CreateDirectory(exportPfad);
            GegnerBase g1 = Global.ContextKampf.Liste<GegnerBase>().Where(g => g.Name == "Zant").First();
            Assert.IsNotNull(g1);
            string fileName = Path.Combine(exportPfad, Path.ChangeExtension(g1.Name, "xml"));
            g1.Export(fileName);
            Assert.IsTrue(File.Exists(fileName));
        }

        [Test]
        public void LoadAndSaveHeld()
        {
            string exportPfad = "Daten\\Helden\\Export";
            Held h1 = Global.ContextHeld.New<Held>();
            h1.Name = "Phexian der Exporteur";
            Sonderfertigkeit s1 = Global.ContextHeld.LoadSonderfertigkeitByName("Kampfreflexe");
            h1.AddSonderfertigkeit(s1, null);
            Global.ContextHeld.Insert<Held>(h1);

            if (!Directory.Exists(exportPfad))
                Directory.CreateDirectory(exportPfad);
            string fileName = Path.Combine(exportPfad, Path.ChangeExtension(h1.Name , "xml"));
            h1.Export(fileName);
            Assert.IsTrue(File.Exists(fileName));

            Held h2 = Held.Import(fileName);
            Assert.IsNotNull(h2);
            Assert.AreEqual(h1.HeldGUID, h2.HeldGUID);
            Assert.IsNotNull(h2.Held_Sonderfertigkeit);
            Assert.AreEqual(s1.SonderfertigkeitGUID, h2.Held_Sonderfertigkeit.First().Sonderfertigkeit.SonderfertigkeitGUID);
            
            /*
            //Wie Demo-Helden
            SerializationService context = new SerializationService();
            //Laden
            List<Held> hlist = new List<Held>();
            int count = 0;
            foreach (string fileName in Directory.EnumerateFiles("Daten\\Helden", "*.xml", SearchOption.TopDirectoryOnly))
            {
                count++;
                Held held = SerializationService.DeserializeObjectFromFile<Held>(fileName);
                Assert.NotNull(held);
                hlist.Add(held);
                string name = held.Name;
                held.Name = "Held " + count.ToString();
                //add to context
                Assert.IsTrue(context.InsertOrUpdateHeld(held), "Held aus {0} hinzufügen", fileName);
                held.Name = name;
                Assert.IsTrue(context.InsertOrUpdateHeld(held), "Held aus {0} aktualisieren", fileName);
            }
            Assert.Greater(count, 0, "Testdaten vorhanden");
            Assert.AreEqual(count, context.HeldenListe().Count, "Alle Helden abgespeichert");

            if (!Directory.Exists("Daten\\Helden\\Export"))
                Directory.CreateDirectory("Daten\\Helden\\Export");
            foreach (Held h in context.HeldenListe())
            {
                string fileName = Path.Combine("Daten\\Helden\\Export", Path.ChangeExtension(h.Name , "xml"));
                SerializationService.SerializeObject<Held>(fileName, h);
                Assert.IsTrue(File.Exists(fileName));
            }
             * */
        }
    }
}
