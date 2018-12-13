using System.IO;
using MeisterGeister.Daten;
using NUnit.Framework;

namespace MeisterGeister_Tests
{
    [TestFixture]
    internal class Update_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            // Testdatenbanken in neues Verzeichnis kopieren, um bei jedem Durchlauf gleiche
            // Ausgangsbedingungen zu haben

            if (Directory.Exists(DATABASE_TEMP))
            {
                Directory.Delete(DATABASE_TEMP, true);
            }

            Directory.CreateDirectory(DATABASE_TEMP);

            var dir = new DirectoryInfo(DATABASE_PATH);
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                var temppath = Path.Combine(DATABASE_TEMP, file.Name);
                file.CopyTo(temppath, false);
            }
        }

        [Test]
        public void UpdateAllDatabases()
        {
            SqlCeUpgrade.RunDirectory(DATABASE_TEMP);
            DatabaseUpdateResult result = DatabaseUpdate.PerformDirectoryDatabaseUpdate(DATABASE_TEMP);
            Assert.AreEqual(DatabaseUpdateResult.DatenbankUpdateOK, result, "Update aller Datenbanken OK");
        }

        private const string DATABASE_PATH = "Daten\\TestDBs";
        private const string DATABASE_TEMP = "DatabaseUpdateTest";
    }
}
