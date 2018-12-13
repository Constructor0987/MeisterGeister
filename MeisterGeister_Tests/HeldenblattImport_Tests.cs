using System.Collections.Generic;
using System.IO;
using MeisterGeister.Logic.HeldenImport;
using MeisterGeister.Model;
using NUnit.Framework;
using Global = MeisterGeister.Global;

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
            var dir = new DirectoryInfo(HELDENBLATT_PATH);
            FileInfo[] files = dir.GetFiles();

            var toImport = new List<FileInfo>();

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLowerInvariant() == ".xls" || file.Extension.ToLowerInvariant() == ".xlsx")
                {
                    toImport.Add(file);
                }
            }

            var importLog = new List<string>();
            foreach (FileInfo file in toImport)
            {
                Assert.IsTrue(HeldenblattImporter.IsHeldenblattFile(file.FullName));
                Held held = HeldenblattImporter.ImportHeldenblattFile(file.FullName, importLog);
                Assert.NotNull(held, "Held wurde importiert.");
            }
            if (importLog.Count > 0)
            {
                importLog.ForEach(s => System.Diagnostics.Debug.WriteLine(s));
            }

            Assert.AreEqual(0, importLog.Count, "Keine Fehler beim Import.");
        }

        private const string HELDENBLATT_PATH = "Daten\\Import";
    }
}
