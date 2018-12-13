using System;
using System.Linq;
using MeisterGeister.Logic.Literatur;
using MeisterGeister.Model;
using NUnit.Framework;
using Global = MeisterGeister.Global;
using Pdf = MeisterGeister.Logic.General.Pdf;

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
            var scanner = new Scanner();
            var parser = new Parser(scanner);
            ParseTree tree;

            var errors = new System.Collections.Generic.Dictionary<Type, System.Collections.Generic.List<object[]>>();
            var pub = new System.Collections.Generic.List<string>();

            var context = new DatabaseDSAEntities(MeisterGeister.Model.Service.ServiceBase.ConnectionString);

            var ass = System.Reflection.Assembly.GetAssembly(typeof(ILiteratur));
            System.Collections.Generic.IEnumerable<Type> literaturTypes = ass.GetTypes().Where(t => t.FullName.StartsWith("MeisterGeister.Model") && typeof(ILiteratur).IsAssignableFrom(t));
            foreach (Type t in literaturTypes)
            {
                IQueryable os = context.GetObjectSet(t);
                foreach (var o in os)
                {
                    if (!(o is ILiteratur))
                    {
                        break;
                    }

                    var l = o as ILiteratur;
                    if (l == null || l.Literatur == null)
                    {
                        continue;
                    }

                    tree = parser.Parse(l.Literatur);
                    if (tree.Errors.Count != 0)
                    {
                        if (!errors.ContainsKey(t))
                        {
                            errors.Add(t, new System.Collections.Generic.List<object[]>());
                        }

                        System.Data.Entity.Core.EntityKey key = context.CreateEntityKey(t.Name, o);
                        errors[t].Add(new object[] { key, l.Literatur });
                    }
                    var litList = (System.Collections.Generic.List<Literaturangabe>)tree.Eval();
                    foreach (Literaturangabe la in litList)
                    {
                        if (!pub.Contains(la.Kürzel))
                        {
                            pub.Add(la.Kürzel);
                        }
                    }
                }
            }
            var errString = "";
            foreach (Type t in errors.Keys)
            {
                System.Diagnostics.Debug.WriteLine("\n" + t.Name + "\n-------------");
                foreach (var oarr in errors[t])
                {
                    errString += "\n" +
                        string.Join(", ", (oarr[0] as System.Data.Entity.Core.EntityKey).EntityKeyValues.Select(ekv => Convert.ToString(ekv.Key) + ":\t" + Convert.ToString(ekv.Value)))
                        + "\n" + (string)oarr[1];
                }
            }
            Assert.AreEqual(0, errors.Count, "Es gibt Parserfehler bei folgenden Literaturangaben:\n" + errString);

            foreach (var abk in pub)
            {
                Assert.AreEqual(1, context.Literatur.Where(l => l.Abkürzung == abk).Count(), string.Format("Es gibt für die Abkürzung {0} keinen Eintrag in der Literaturliste.", abk));
            }
        }

        [Test]
        public void ParseLiteraturTest()
        {
            var scanner = new Scanner();
            var parser = new Parser(scanner);
            ParseTree tree;
            var expressions = new string[]
            {
                "",
                "WdS 45-46, 56, Errata 4 / AA 100 / A123 2,Errata 3/AB123 2"
            };
            foreach (var input in expressions)
            {
                tree = parser.Parse(input);
                Assert.AreEqual(0, tree.Errors.Count, string.Format("ParseError bei der Expression {0}", input));
                var res = tree.Eval();
                if (res != null)
                {
                    System.Diagnostics.Debug.WriteLine(string.Join(" / ", (System.Collections.Generic.List<Literaturangabe>)res));
                }
            }
        }

        [Test]
        public void OpenPdfReaderTest()
        {
            Assert.Throws(typeof(LiteraturPfadMissingException), delegate ()
            { Pdf.OpenReader("WdA", 2); });
            Literatur wda = Global.ContextHeld.Liste<Literatur>().Where(l => l.Abkürzung == "WdA").FirstOrDefault();
            Assert.IsNotNull(wda);
            wda.Pfad = "Daten\\Pdf\\10Seiten.pdf";
            Assert.IsTrue(Global.ContextHeld.Update<Literatur>(wda));
            Assert.DoesNotThrow(delegate ()
            { Pdf.OpenReader("WdA", 2); });
            wda.Pfad = null;
            Assert.IsTrue(Global.ContextHeld.Update<Literatur>(wda));
        }
    }
}
