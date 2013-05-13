using System;
using MeisterGeister.Model;
using NUnit.Framework;
using Global = MeisterGeister.Global;
using MeisterGeister.Logic.Voraussetzungen;


namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Literatur_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            //Global.Init();
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
        public void OpenPdfReaderTest()
        {
            MeisterGeister.Logic.General.Pdf.OpenReader("WdA", 12);
        }
    }
}
