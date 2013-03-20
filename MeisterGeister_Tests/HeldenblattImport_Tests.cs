using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using Global = MeisterGeister.Global;
using MeisterGeister.Logic.HeldenImport;
using System.IO;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class HeldenblattImport_Tests
    {
        private const string HELDENBLATT_PATH = "Daten\\Import";

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
            DirectoryInfo dir = new DirectoryInfo(HELDENBLATT_PATH);
            FileInfo[] files = dir.GetFiles();

            List<FileInfo> toImport = new List<FileInfo>();

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLowerInvariant() == ".xls" || file.Extension.ToLowerInvariant() == ".xlsx")
                    toImport.Add(file);
            }

            List<string> importLog = new List<string>();
            foreach (FileInfo file in toImport)
            {
                Assert.IsTrue(HeldenblattImporter.IsHeldenblattFile(file.FullName));
                var held = HeldenblattImporter.ImportHeldenblattFile(file.FullName, importLog);
                Assert.NotNull(held, "Held wurde importiert.");
            }
            if (importLog.Count > 0)
                importLog.ForEach(s => System.Diagnostics.Debug.WriteLine(s));
            Assert.AreEqual(0, importLog.Count, "Keine Fehler beim Import.");
        }
    }
}
