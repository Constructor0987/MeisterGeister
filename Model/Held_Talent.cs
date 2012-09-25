using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Held_Talent : MeisterGeister.Logic.General.Probe
    {
        public Held_Talent()
        {
            Werte = new int[3];

            if (Held != null && Talent != null)
            {
                Werte[0] = Held.GetEigenschaftWert(Talent.Eigenschaft1);
                Werte[1] = Held.GetEigenschaftWert(Talent.Eigenschaft2);
                Werte[2] = Held.GetEigenschaftWert(Talent.Eigenschaft3);
            }
        }

        public override int Fertigkeitswert
        {
            get
            {
                return TaW ?? 0;
            }
            set
            {
                TaW = value;
            }
        }
    }
}
