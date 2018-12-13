using System.Linq;
using MeisterGeister.Logic.HeldenImport;
using NUnit.Framework;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class HeldenSoftwareOnline_Tests
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
            _token = "7f3dbee0878805b9e08c12795a4a9601b8f48d095ea4192079b0f8bc239eb44e";
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        [Test]
        public void GetHeldenListeTest()
        {
            var syncer = new HeldenSoftwareOnlineService(_token);
            HeldenListe heldenListe = syncer.GetHeldenListe();
            Assert.IsTrue(heldenListe.Helden != null);
            Assert.IsTrue(heldenListe.Helden.Count > 0);
        }

        [Test]
        public void GetHeldXmlTest()
        {
            var syncer = new HeldenSoftwareOnlineService(_token);
            HeldenListe heldenListe = syncer.GetHeldenListe();
            var heldXml = syncer.GetHeldXml(heldenListe.Helden.First());
            Assert.IsTrue(heldXml != null);
            Assert.IsTrue(heldXml.Length > 0);
        }

        [Test]
        public void SyncHeldTest()
        {
            var syncer = new HeldenSoftwareOnlineService(_token);
            HeldenListe heldenListe = syncer.GetHeldenListe();
            HeldElement heldElement = heldenListe.Helden.FirstOrDefault(h => h.Name.Contains("Rethis"));
            HeldenImportResult held = syncer.DownloadHeld(heldElement);
            Assert.IsTrue(held != null);
            Assert.IsTrue(held.Held.Name.Contains("Rethis"));
        }

        private string _token;
    }
}
