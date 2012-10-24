using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public class NiedrigeLebensenergieModifikator : Modifikator, IModAlleEigenschaftsProben, IModAlleProben, IModGS
    {
        public override string Name
        {
            get { return "Niedrige Lebensenergie"; }
        }

        public override string Auswirkung
        {
            get { return "Eigenschaftsproben +1; Talent-/Zauberproben +3; GS -1"; }
        }

        public override string Literatur
        {
            get { return "WdS 57"; }
        }

        public int ApplyAlleProbenMod(int wert)
        {
            return wert + 3;
        }

        public int ApplyAlleEigenschaftsProbenMod(int wert)
        {
            return wert + 1;
        }

        public int ApplyGSMod(int wert)
        {
            return Math.Max(1, wert - 1);
        }
    }

    public class NiedrigeAusdauerModifikator : Modifikator, IModAlleEigenschaftsProben, IModAlleProben
    {
        public override string Name
        {
            get { return "Niedrige Ausdauer"; }
        }

        public override string Auswirkung
        {
            get { return "Eigenschaftsproben +1; Talent-/Zauberproben +3"; }
        }

        public override string Literatur
        {
            get { return "WdS 83"; }
        }

        public int ApplyAlleProbenMod(int wert)
        {
            return wert + 3;
        }

        public int ApplyAlleEigenschaftsProbenMod(int wert)
        {
            return wert + 1;
        }
    }

    public class LebensenergieKampfunfähigModifikator : Modifikator, IModGS
    {
        public override string Name
        {
            get { return "Kampfunfähig (Lebensenergie)"; }
        }

        public override string Auswirkung
        {
            get { return "GS 1"; }
        }

        public override string Literatur
        {
            get { return "WdS 57"; }
        }

        public int ApplyGSMod(int wert)
        {
            return 1;
        }
    }

    public class AusdauerKampfunfähigModifikator : Modifikator, IModGS
    {
        public override string Name
        {
            get { return "Kampfunfähig (Ausdauer)"; }
        }

        public override string Auswirkung
        {
            get { return "GS 1"; }
        }

        public override string Literatur
        {
            get { return "WdS 83"; }
        }

        public int ApplyGSMod(int wert)
        {
            return 1;
        }
    }

    
}
