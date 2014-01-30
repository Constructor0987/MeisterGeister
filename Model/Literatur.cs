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

        /// <summary>
        /// Gibt eine Literatur-Instanz anhand der Abkürzung zurück.
        /// </summary>
        /// <param name="abkürzung">Abkürzung einer Literaturangabe</param>
        /// <returns></returns>
        public static Literatur GetByAbkürzung(string abkürzung, bool isErrata = false)
        {
            return Global.ContextHeld.LoadLiteraturByAbkürzung(abkürzung, isErrata);
        }

        /// <summary>
        /// Tauscht abgekürzte Literturangaben gegen die Langfassung aus.
        /// </summary>
        /// <param name="litertur">Literturangaben mit Abkürzungen.</param>
        /// <returns>Literurangaben in Langfassung.</returns>
        public static string ReplaceAbkürzungen(string literatur)
        {
            
            if (string.IsNullOrWhiteSpace(literatur))
                return string.Empty;

            string text = literatur;

            foreach (var l in Global.ContextHeld.Liste<Literatur>())
                text = text.Replace(l.Abkürzung, l.Name);

            return text;
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Zulässige Abweichung der Dateigröße zum Original.
        /// </summary>
        private const double ORIGINAL_TOLERANZ = 0.1; // 10%

        public Nullable<bool> IsOriginal
        {
            get
            {
                if (Pfad == null || Größe == null)
                    return null;

                System.IO.FileInfo fi = new System.IO.FileInfo(Pfad);
                if (!fi.Exists)
                    return null;

                if (Größe == null)
                    return null;
                double größeByte = Größe.Value * 1000000;
                double tolerenz = größeByte * ORIGINAL_TOLERANZ; // in Byte
                bool isOriginal = größeByte >= fi.Length - tolerenz && größeByte <= fi.Length + tolerenz;
                if (isOriginal)
                    return true;

                if (GrößeKomprimiert == null)
                    return isOriginal;
                größeByte = Größe.Value * 1000000;
                tolerenz = größeByte * ORIGINAL_TOLERANZ; // in Byte
                return größeByte >= fi.Length - tolerenz && größeByte <= fi.Length + tolerenz;
            }
        }
    }
}
