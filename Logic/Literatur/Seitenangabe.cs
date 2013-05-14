using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Literatur
{
    public class Seitenangabe
    {
        public Seitenangabe(int seite, int endeSeite = 0, bool isErrata = false)
        {
            if (endeSeite == 0)
                endeSeite = seite;
            Seite = seite;
            EndeSeite = endeSeite;
            IsErrata = isErrata;
        }

        private bool isErrata = false;
        public bool IsErrata
        {
            get { return isErrata; }
            set { isErrata = value; }
        }

        private int seite = 0;
        public int Seite
        {
            get { return seite; }
            set { seite = value; }
        }

        private int endeSeite = 0;
        public int EndeSeite
        {
            get { return endeSeite; }
            set { endeSeite = value; }
        }

        public override string ToString()
        {
            if(EndeSeite != Seite)
                return String.Format("{2}{0}-{1}", Seite, EndeSeite, IsErrata ? "Errata " : "");
            return String.Format("{1}{0}", Seite, IsErrata ? "Errata " : "");
        }
    }
}
