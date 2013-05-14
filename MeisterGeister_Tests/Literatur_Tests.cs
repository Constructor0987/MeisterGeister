using System;
using MeisterGeister.Model;
using NUnit.Framework;
using Global = MeisterGeister.Global;
using MeisterGeister.Logic.Literatur;
using Pdf = MeisterGeister.Logic.General.Pdf;


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
        public void ParseLiteraturTest()
        {
            Scanner scanner = new Scanner();
            Parser parser = new Parser(scanner);
            ParseTree tree;
            string[] expressions = new string[]
            {
                "",
                "WdS 45-46, 56, Errata 4 / AA 100 / A123 2,Errata 3/AB123 2"
            };
            foreach (string input in expressions)
            {
                tree = parser.Parse(input);
                Assert.AreEqual(0, tree.Errors.Count, String.Format("ParseError bei der Expression {0}", input) );
                var res = tree.Eval();
                if(res != null)
                    System.Diagnostics.Debug.WriteLine(String.Join(" / ", (System.Collections.Generic.List<Literaturangabe>)res));
            }
        }

        [Test]
        public void OpenPdfReaderTest()
        {
            Pdf.OpenReader("WdA", 12);
        }
    }
}
