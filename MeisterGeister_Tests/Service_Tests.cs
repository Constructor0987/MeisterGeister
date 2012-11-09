using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;
using Global = MeisterGeister.Global;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Service_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            MeisterGeister.Global.Init();
        }

        [TestFixtureTearDown]
        public void TearDownMethods()
        {
        }

        [SetUp]
        public void SetupTest()
        {
            //leere Datenbank vor dem Test
            //DeleteHelden();
        }

        private void DeleteHelden()
        {
            List<Held> deleteMe = new List<Held>();
            deleteMe.AddRange(MeisterGeister.Global.ContextHeld.Liste<Held>().AsEnumerable());
            foreach (Held h in deleteMe)
                Assert.IsTrue(MeisterGeister.Global.ContextHeld.Delete<Held>(h), "Held gelöscht");
        }

        [TearDown]
        public void TearDownTest()
        {
            //wieder alles löschen
            //DeleteHelden();
        }

        [Test]
        public void ListeTest()
        {
            Assert.DoesNotThrow(LoadListen);
        }

        void LoadListen()
        {
            var zauber = Global.ContextHeld.Liste<Zauber>();
            var gegnerBase = Global.ContextHeld.Liste<GegnerBase>();
        }

        [Test]
        public void CloneTest()
        {
            int a = Global.ContextHeld.Liste<Held>().Count;
            Held h = Global.ContextHeld.New<Held>();
            h.Name = "Karl Ranseier der Zweite";
            Held h2 = Global.ContextHeld.Clone<Held>(h);
            Assert.AreEqual(h.Name, h2.Name);
        }

        [Test]
        public void SonderfertigkeitenTest()
        {
            DeleteHelden();
            //Keine Helden
            Assert.AreEqual(0, Global.ContextHeld.Liste<Held>().Count, "Keine Helden in der Datenbank");
            //Neuen Held erstellen            
            Held h = Global.ContextHeld.New<Held>();
            h.Name = "Karl Ranseier der Zweite";
            Held h2 = Global.ContextHeld.New<Held>();
            h2.Name = "Ludwig der Dritte";
            //Keine Helden
            Assert.AreEqual(0, Global.ContextHeld.Liste<Held>().Count, "Keine Helden in der Datenbank");
            //Einfügen
            Assert.IsTrue(Global.ContextHeld.Insert<Held>(h));
            //Ein Held
            Assert.AreEqual(1, Global.ContextHeld.Liste<Held>().Count, "Ein Held in der Datenbank");
            //Einfügen
            Assert.IsTrue(Global.ContextHeld.Insert<Held>(h2));
            //Zwei Helden
            Assert.AreEqual(2, Global.ContextHeld.Liste<Held>().Count, "Zwei Helden in der Datenbank");
            
            //Sonderfertigkeit laden
            Sonderfertigkeit eisernerWille = Global.ContextHeld.LoadSonderfertigkeitByName("Eiserner Wille I");
            //erfolgreich geladen
            Assert.NotNull(eisernerWille);
            //dem Helden hinzufügen
            h.AddSonderfertigkeit(eisernerWille, null);
            //hat nun eine Sonderfertigkeit
            Assert.AreEqual(1, h.Held_Sonderfertigkeit.Count);

            //waffe laden
            Waffe waffe = MeisterGeister.Global.ContextWaffe.WaffeListe.Where(w => w.Name == "Beil").First();
            Assert.IsNotNull(waffe);
            //Insert mit AutoID - geht ab SQL CE 4.0.
            Assert.IsTrue(MeisterGeister.Global.ContextWaffe.InsertHeldWaffe(h, waffe));

            //Speichern
            Global.ContextHeld.Save();
            //sollte nun in der Datenbank sein mit der Sonderfertigkeit in Held_Sonderfertigkeit und einem Beil als Waffe

        }

        // leider werden IDs nicht aktualisiert.
        [Test]
        [Ignore]
        public void IDUpdate()
        {
            Held h = Global.ContextHeld.New<Held>();
            Guid hguid = h.HeldGUID;
            Sonderfertigkeit s = Global.ContextHeld.LoadSonderfertigkeitByName("Eiserner Wille I");
            Assert.IsNotNull(s);
            h.AddSonderfertigkeit(s, null);
            Assert.AreEqual(hguid, s.Held_Sonderfertigkeit.First().HeldGUID);
            h.HeldGUID = Guid.NewGuid();
            Assert.AreEqual(h.HeldGUID, s.Held_Sonderfertigkeit.First().HeldGUID);
            Global.ContextHeld.DiscardChanges();
        }

    }
}
