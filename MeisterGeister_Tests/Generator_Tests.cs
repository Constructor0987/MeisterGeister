using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;

namespace MeisterGeister_Tests
{
    [TestFixture]
    class Generator_Tests
    {
        private StringCollection _namenstypen;

        [TestFixtureSetUp]
        public void SetupMethods()
        {
            MeisterGeister.Global.Init();
            _namenstypen = MeisterGeister_Tests.ReflectionHelper.GetConstantValueStringCollection(typeof(MeisterGeister.ViewModel.Generator.Factorys.NamenFactoryHelper), false, false);
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
        public void GeneratorNamenFactoriesTest()
        {
            foreach (String namenstyp in _namenstypen)
            {
                Assert.NotNull( MeisterGeister.ViewModel.Generator.Factorys.NamenFactoryHelper.GetFactory(namenstyp), "Die NamenFactory "+namenstyp+" existiert nicht." );
            }
        }

        [Test]
        public void GeneratorNamenLeereNamenTest()
        {
            MeisterGeister.ViewModel.Generator.Factorys.NamenFactory aktuelleNamenFactory;
            foreach (String namenstyp in _namenstypen)
            {
                aktuelleNamenFactory = MeisterGeister.ViewModel.Generator.Factorys.NamenFactoryHelper.GetFactory(namenstyp);
                if (aktuelleNamenFactory != null)
                {
                    foreach (MeisterGeister.ViewModel.Generator.Container.Geschlecht geschlecht in Enum.GetValues(typeof(MeisterGeister.ViewModel.Generator.Container.Geschlecht)))
                    {
                        foreach (MeisterGeister.ViewModel.Generator.Container.Stand stand in Enum.GetValues(typeof(MeisterGeister.ViewModel.Generator.Container.Stand)))
                            Assert.IsNotEmpty(aktuelleNamenFactory.GetName(geschlecht, stand), "Leerer Name bei Factory " + namenstyp + " generiert (Parameter geschlecht=" + geschlecht.ToString() + ", stand=" + stand.ToString() + ").");
                    }
                }
            }
        }
        
        [Test]
        public void GeneratorOrtsnamenLeereNamenTest()
        {
            MeisterGeister.ViewModel.Generator.Factorys.NamenFactory aktuelleNamenFactory;
            foreach (String namenstyp in _namenstypen)
            {
                aktuelleNamenFactory = MeisterGeister.ViewModel.Generator.Factorys.NamenFactoryHelper.GetFactory(namenstyp);
                if (aktuelleNamenFactory != null && aktuelleNamenFactory.GeneriertOrtsnamen)
                {
                    Assert.IsNotEmpty(aktuelleNamenFactory.GeneriereOrtsname(), "Die Factory " + _namenstypen + " hat einen leeren Ortsnamen generiert.");
                }
            }
        }
    }
}
