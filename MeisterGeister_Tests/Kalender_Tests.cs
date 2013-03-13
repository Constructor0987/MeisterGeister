using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MeisterGeister.Model;
using MeisterGeister.Model.Service;

using Global = MeisterGeister.Global;
using MeisterGeister.Logic.HeldenImport;
using System.Diagnostics;

namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Kalender_Tests
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
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        [Test]
        public void RandomNumberTest()
        {
            for(int i=0; i< 100; i++)
                Debug.WriteLine(MeisterGeister.Logic.General.RandomNumberGenerator.RandomNormalDistribution());
        }
    }
}
