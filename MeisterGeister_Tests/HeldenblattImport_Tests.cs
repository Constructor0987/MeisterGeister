using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using Global = MeisterGeister.Global;
using MeisterGeister.Logic.HeldenImport;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class HeldenblattImport_Tests
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
            Global.Init();
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        [Test]
        public void HeldenblattImportTest()
        {
            string xlsFile = "Daten\\Import\\Heldenblatt_v351s_Testdaten_V2.xls";
            Assert.IsTrue(HeldenblattImporter.IsHeldenblattFile(xlsFile));
            var held = HeldenblattImporter.ImportHeldenblattFile(xlsFile);
            Assert.IsNotNull(held);
            Assert.AreEqual("Galdriel Lumpensammler", held.Name);
            Assert.AreEqual("Ideefix", held.Spieler);
            Assert.AreEqual(15, held.Mut);
            Assert.AreEqual(13, held.Klugheit);
            Assert.AreEqual(13, held.Intuition);
            Assert.AreEqual(15, held.Charisma);
            Assert.AreEqual(13, held.Fingerfertigkeit);
            Assert.AreEqual(15, held.Gewandtheit);
            Assert.AreEqual(12, held.Konstitution);
            Assert.AreEqual(11, held.Körperkraft);
            Assert.AreEqual(2, held.Behinderung);
            Assert.AreEqual(4, held.INI_Mod);
            Assert.AreEqual(8, held.LE_Mod);
            Assert.AreEqual(26, held.LE_Aktuell);
            Assert.AreEqual(26, held.LebensenergieMax);
            Assert.AreEqual(14, held.AU_Mod);
            Assert.AreEqual(35, held.AU_Aktuell);
            Assert.AreEqual(35, held.AusdauerMax);
            Assert.AreEqual(12, held.AE_Mod);
            Assert.AreEqual(34, held.AE_Aktuell);
            //Assert.AreEqual(34, held.AstralenergieMax); //vorteil fehlt noch
            Assert.AreEqual(0, held.KE_Mod);
            Assert.AreEqual(-3, held.MR_Mod);
            Assert.AreEqual("Auelf", held.Rasse);
            Assert.AreEqual("Firnelfische Sippe (Verweltlicht)", held.Kultur);
            Assert.AreEqual("Grabräuber", held.Profession);
            Assert.AreEqual(8, held.AttackeBasis);
            Assert.AreEqual(8, held.ParadeBasis);
            Assert.AreEqual(7, held.FernkampfBasis);
            Assert.AreEqual(0, held.Wunden);
            Assert.AreEqual(4, held.RSKopf);
            Assert.AreEqual(2, held.RSBrust);
            Assert.AreEqual(2, held.RSRücken);
            Assert.AreEqual(3, held.RSArmL);
            Assert.AreEqual(3, held.RSArmR);
            Assert.AreEqual(2, held.RSBauch);
            Assert.AreEqual(2, held.RSBeinL);
            Assert.AreEqual(2, held.RSBeinR);
            Assert.AreEqual(7, held.SO);
            Assert.AreEqual(98.11, held.Vermögen);
            Assert.AreEqual(5520, held.APGesamt);
            Assert.AreEqual(5655, held.APEingesetzt);
        }
    }
}
