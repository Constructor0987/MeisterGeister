using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    /// <summary>
    /// Versieht ein Modifikator-Interface mit den Textinformationen Name und Gruppe.
    /// </summary>
    public class ModifikatorTyp
    {
        public ModifikatorTyp() { }
        public ModifikatorTyp(Type typ, string name, string gruppe)
        {
            Typ = typ; Name = name; Gruppe = gruppe;
        }

        public Type Typ;
        public string Name;
        public string Gruppe;

        static IList<ModifikatorTyp> liste = null;
        /// <summary>
        /// Eine Sammlung von Modifikatortypen.
        /// Die Liste wird aus den Interface-Quelldateien bei der Initialisierung gefüllt, damit die Angaben zusammenbleiben.
        /// </summary>
        public static IList<ModifikatorTyp> Liste
        {
            get {
                if (liste == null)
                    liste = new List<ModifikatorTyp>();
                return ModifikatorTyp.liste; 
            }
            set { ModifikatorTyp.liste = value; }
        }

    }
}
