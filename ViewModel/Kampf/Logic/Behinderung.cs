using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    //Mir fallen nur die folgenden Anwendungsfälle ein (im Probentool bzw. Reisetool): Fingerfertigkeitsproben (Handschuhe), Sinneschärfe (Kopf oder Handschuhe), Meta-Talent Wache Schieben (Kopf)
    /// <summary>
    /// Die Behinderung abhängig vom Trefferzone-Enum.
    /// </summary>
    public class Behinderung : ITrefferzonenIndexer<int>
    {
        private Model.Held _held;
        public Behinderung(Model.Held held)
        {
            _held = held;
        }

        public int this[Trefferzone zone]
        {
            get
            {
                return _held.BE ?? 0;
            }
            set
            {
            }
        }
    }
}
