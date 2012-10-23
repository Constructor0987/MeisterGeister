using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public class PABonusDurchHoheIni : Modifikator, IModPA
    {
        public PABonusDurchHoheIni(int bonus)
        {
            Bonus = bonus;
        }

        public int Bonus
        {
            get;
            private set;
        }

        public override string Name
        {
            get { return "Hohe Initiative"; }
        }

        public override string Auswirkung
        {
            get { return "PA +" + Bonus; }
        }

        public override string Literatur
        {
            get { return "WdS 78"; }
        }

        public int ApplyPAMod(int wert)
        {
            return wert + Bonus;
        }
    }
  
}
