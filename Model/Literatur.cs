using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Literatur;

namespace MeisterGeister.Model
{
    public partial class Literatur
    {
        public Literatur()
        {
            LiteraturGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !LiteraturGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        private static Parser parser = null;
        private static Parser Parser
        {
            get {
                if (parser == null)
                    parser = new Parser(Scanner);
                return Literatur.parser; 
            }
        }
        private static Scanner scanner = null;
        private static Scanner Scanner
        {
            get {
                if (scanner == null)
                    scanner = new Scanner();
                return Literatur.scanner; 
            }
        }

        public static List<Literaturangabe> Parse(ILiteratur ilit)
        {
            if (ilit == null || ilit.Literatur == null)
                return null;
            return Parse(ilit.Literatur);
        }

        public static List<Literaturangabe> Parse(string lit)
        {
            if (lit == null)
                return null;
            ParseTree tree = Literatur.Parser.Parse(lit);
            if (tree.Errors.Count > 0)
                return null;
            return (List<Literaturangabe>)tree.Eval();
        }
    }
}
