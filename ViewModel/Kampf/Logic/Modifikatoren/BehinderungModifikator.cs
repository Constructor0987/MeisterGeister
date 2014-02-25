using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    /// <summary>
    /// Der BE Mod wird nirgendwo angewandt. Er dient nur zur Darstellung in der ModifikatorListe.
    /// An die ModifikatorListe muss eine Instanz vom BehinderungModifikator manuell angehängt werden.
    /// Und der betroffene Wert sollte manuell verändert werden.
    /// </summary>
    public class BehinderungModifikator : Modifikator, IModBE
    {
        public BehinderungModifikator(int malus)
        {
            Malus = malus;
        }

        public int Malus
        {
            get;
            private set;
        }

        public override string Name
        {
            get { return "Behinderung"; }
        }

        public override string Auswirkung
        {
            get { return "BE -" + Malus; }
        }

        public override string Literatur
        {
            get { return "WdS 78"; }
        }

        public int ApplyBEMod(int wert)
        {
            return wert - Malus;
        }
    }
  
}
