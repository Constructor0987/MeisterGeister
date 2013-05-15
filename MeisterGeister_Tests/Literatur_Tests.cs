using System;
using MeisterGeister.Model;
using NUnit.Framework;
using Global = MeisterGeister.Global;
using MeisterGeister.Logic.Literatur;
using Pdf = MeisterGeister.Logic.General.Pdf;
using System.Linq;


namespace MeisterGeister_Tests
{
    [TestFixture]
    public class Literatur_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
            Global.Init();
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
        public void ParseAllLiteratur()
        {
            Scanner scanner = new Scanner();
            Parser parser = new Parser(scanner);
            ParseTree tree;

            System.Collections.Generic.Dictionary<Type, System.Collections.Generic.List<object[]>> errors = new System.Collections.Generic.Dictionary<Type, System.Collections.Generic.List<object[]>>();
            System.Collections.Generic.List<string> pub = new System.Collections.Generic.List<string>();

            MeisterGeister.Model.DatabaseDSAEntities context = new DatabaseDSAEntities(MeisterGeister.Model.Service.ServiceBase.ConnectionString);

            var ass = System.Reflection.Assembly.GetAssembly(typeof(ILiteratur));
            var literaturTypes = ass.GetTypes().Where(t => t.FullName.StartsWith("MeisterGeister.Model") && typeof(ILiteratur).IsAssignableFrom(t));
            foreach (Type t in literaturTypes)
            {
                var os = context.GetObjectSet(t);
                foreach (var o in os)
                {
                    if (!(o is ILiteratur))
                        break;
                    ILiteratur l = o as ILiteratur;
                    if (l == null || l.Literatur == null)
                        continue;
                    tree = parser.Parse(l.Literatur);
                    if (tree.Errors.Count != 0)
                    {
                        if (!errors.ContainsKey(t))
                            errors.Add(t, new System.Collections.Generic.List<object[]>());
                        var key = context.CreateEntityKey(t.Name, o);
                        errors[t].Add(new object[] { key, l.Literatur });
                    }
                    var litList = (System.Collections.Generic.List<Literaturangabe>)tree.Eval();
                    foreach (var la in litList)
                    {
                        if (!pub.Contains(la.Kürzel))
                            pub.Add(la.Kürzel);
                    }
                }
            }
            string errString = "";
            foreach (var t in errors.Keys)
            {
                System.Diagnostics.Debug.WriteLine("\n" + t.Name + "\n-------------");
                foreach(var oarr in errors[t])
                {
                    errString += "\n" +
                        String.Join(", ", (oarr[0] as System.Data.EntityKey).EntityKeyValues.Select(ekv => Convert.ToString(ekv.Key) + ":\t" + Convert.ToString(ekv.Value)))
                        + "\n" + (string)oarr[1];
                }
            }
            Assert.AreEqual(0, errors.Count, "Es gibt Parserfehler bei folgenden Literaturangaben:\n" + errString);
            
            foreach (string abk in pub)
            {
                Assert.AreEqual(1, context.Literatur.Where(l => l.Abkürzung == abk).Count(), String.Format("Es gibt für die Abkürzung {0} keinen Eintrag in der Literaturliste.", abk));
            }
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
            Assert.Throws(typeof(LiteraturPfadMissingException), delegate() { Pdf.OpenReader("WdA", 2); });
            Literatur wda = Global.ContextHeld.Liste<Literatur>().Where(l => l.Abkürzung == "WdA").FirstOrDefault();
            Assert.IsNotNull(wda);
            wda.Pfad = "Daten\\Pdf\\10Seiten.pdf";
            Assert.IsTrue(Global.ContextHeld.Update<Literatur>(wda));
            Assert.DoesNotThrow(delegate() { Pdf.OpenReader("WdA", 2); });
            wda.Pfad = null;
            Assert.IsTrue(Global.ContextHeld.Update<Literatur>(wda));
        }
    }
}
