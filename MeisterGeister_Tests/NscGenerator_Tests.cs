using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Collections.Generic;

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
        public void NscGeneratorNamensgenerierungStringTest()
        {
            MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactory aktuelleNamenFactory;
            foreach (String namenstyp in _namenstypen)
            {
                aktuelleNamenFactory = MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactoryHelper.GetFactory(namenstyp);
                if (aktuelleNamenFactory != null)
                {
                    foreach (MeisterGeister.ViewModel.NscGenerator.Logic.Geschlecht geschlecht in Enum.GetValues(typeof(MeisterGeister.ViewModel.NscGenerator.Logic.Geschlecht)))
                    {
                        foreach (MeisterGeister.ViewModel.NscGenerator.Logic.Stand stand in Enum.GetValues(typeof(MeisterGeister.ViewModel.NscGenerator.Logic.Stand)))
                            Assert.IsNotEmpty(aktuelleNamenFactory.GetName(geschlecht, stand), "Leerer Name bei Factory " + namenstyp + " generiert (Parameter geschlecht=" + geschlecht.ToString() + ", stand=" + stand.ToString() + ").");
                    }
                }
            }
        }

        [Test]
        public void NscGeneratorNamensbedeutungTest()
        {
            MeisterGeister.ViewModel.NscGenerator.Logic.PersonNurName person;
            MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactory aktuelleNamenFactory;
            foreach (String namenstyp in _namenstypen)
            {
                aktuelleNamenFactory = MeisterGeister.ViewModel.NscGenerator.Factorys.NamenFactoryHelper.GetFactory(namenstyp);
                if (aktuelleNamenFactory != null && aktuelleNamenFactory.GeneriertNamensbedeutungen)
                {
                    foreach (MeisterGeister.ViewModel.NscGenerator.Logic.Geschlecht geschlecht in Enum.GetValues(typeof(MeisterGeister.ViewModel.NscGenerator.Logic.Geschlecht)))
                    {
                        foreach (MeisterGeister.ViewModel.NscGenerator.Logic.Stand stand in Enum.GetValues(typeof(MeisterGeister.ViewModel.NscGenerator.Logic.Stand)))
                        {
                            person = new MeisterGeister.ViewModel.NscGenerator.Logic.PersonNurName();
                            aktuelleNamenFactory.RegeneratePersonNurName(ref person, geschlecht, stand);
                            Assert.IsNotEmpty(person.Namensbedeutung, "Leerer Namensbedeutung bei Factory " + namenstyp + " generiert (Parameter geschlecht=" + geschlecht.ToString() + ", stand=" + stand.ToString() + ").");
                        }
                    }
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
