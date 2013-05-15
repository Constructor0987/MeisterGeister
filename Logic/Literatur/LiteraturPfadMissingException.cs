using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Literatur
{
    public class LiteraturPfadMissingException : Exception
    {
        Model.Literatur l = null;
        public LiteraturPfadMissingException(string abkürzung, Model.Literatur literatur)
        {
            this.Data.Add("Abkürzung", abkürzung);
            l = literatur;
        }

        public string Abkürzung
        {
            get { return (string)Data["Abkürzung"]; }
        }

        public Model.Literatur Literatur
        {
            get { return l; }
        }

        public override string Message
        {
            get
            {
                return String.Format("Für das Literaturkürzel {0} ({1}) ist kein Pfad eingestellt.", Abkürzung, Literatur);
            }
        }
    }
}
