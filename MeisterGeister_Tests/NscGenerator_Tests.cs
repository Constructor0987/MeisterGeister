using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace MeisterGeister_Tests
{
    [TestFixture]
    class NscGenerator_Tests
    {
        private StringCollection _namenstypen;

        [TestFixtureSetUp]
        public void SetupMethods()
        {
            _namenstypen = MeisterGeister_Tests.ReflectionHelper.GetConstantValueStringCollection(typeof(MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactoryHelper), false, false);
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
        public void NscGeneratorNamenFactoriesTest()
        {
            foreach (String namenstyp in _namenstypen)
            {
                Assert.IsNull( MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactoryHelper.GetFactory(namenstyp), "Die NamenFactory "+namenstyp+" existiert nicht." );
            }
        }

        [Test]
        public void NscGeneratorNamensgenerierungTest()
        {
            MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactory aktuelleNamenFactory;
            foreach (String namenstyp in _namenstypen)
            {
                aktuelleNamenFactory = MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactoryHelper.GetFactory(namenstyp);
                if (aktuelleNamenFactory != null)
                {
                    Assert.IsNotEmpty(aktuelleNamenFactory.GetName(MeisterGeister.ViewModel.NscGenerator.Logic.Geschlecht.weiblich), "Leerer weiblicher Name bei Factory "+namenstyp+" generiert.");
                    Assert.IsNotEmpty(aktuelleNamenFactory.GetName(MeisterGeister.ViewModel.NscGenerator.Logic.Geschlecht.männlich), "Leerer männlicher Name bei Factory " + namenstyp + " generiert.");
                }
            }
        }

        [Test]
        public void NscGeneratorOrtsnamensgenerierungTest()
        {
            MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactory aktuelleNamenFactory;
            foreach (String namenstyp in _namenstypen)
            {
                aktuelleNamenFactory = MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactoryHelper.GetFactory(namenstyp);
                if (aktuelleNamenFactory != null && aktuelleNamenFactory.GeneriertOrtsnamen)
                {
                    Assert.IsNotEmpty(aktuelleNamenFactory.GeneriereOrtsname(), "Die Factory " + _namenstypen + " hat einen leeren Ortsnamen generiert.");
                }
            }
        }
    }
}
