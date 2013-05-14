using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Literatur
{
    public class Literaturangabe
    {
        public Literaturangabe(string kürzel, List<Seitenangabe> seiten)
        {
            Kürzel = kürzel;
            Seiten = seiten;
        }

        private string kürzel;
        public string Kürzel
        {
            get { return kürzel; }
            set { kürzel = value; }
        }

        private List<Seitenangabe> seiten = null;
        public List<Seitenangabe> Seiten
        {
            get { return seiten; }
            private set { seiten = value; }
        }

        public override string ToString()
        {
            return String.Format("[Literaturangabe: {0} {1}]", Kürzel, String.Join(", ", Seiten));
        }
    }
}
