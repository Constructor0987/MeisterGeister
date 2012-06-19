using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
// Eigene Usings
using MeisterGeister.Logic.Settings;
using MeisterGeister.ViewModel.Kampf.Logic;
using Model = MeisterGeister.Model;

namespace MeisterGeister.Logic.General
{
    // Markus
    // Diese Klasse wird wohl später durch das HeldenInventar ersetzt
    public class HeldWaffeWert
    {
        #region Properties
        
        public Model.Waffe Waffe { get; set; }

        private Model.Held Held { get; set; }

        public Model.Talent Talent { get; set; }

        public string eBE
        {
            get
            {
                return Talent.eBE;
            }
        }

        //public int AttackeWert
        //{
        //    get
        //    {
        //        if (Held != null && Talent != null)
        //        {
        //            HeldKampfTalent kampfTalent = Held.KampfTalent(Talent.Name);
        //            if (kampfTalent != null)
        //                return kampfTalent.AttackeWert;
        //            else
        //                return Held.AttackeBasis - 2;
        //        }
        //        return 0;
        //    }
        //}

        //public int AttackeWertGesamt
        //{
        //    get
        //    {
        //        return AttackeWert
        //            + (Waffe != null ? Waffe.WMAT : 0) // Mod durch Waffenmodifikator
        //            + (Regeln.TPKK ? Math.Min(0, TPKK) : 0); // Abzug durch niedrige KK
        //    }
        //}

        //public int ParadeWert
        //{
        //    get
        //    {
        //        if (Held != null && Talent != null)
        //        {
        //            HeldKampfTalent kampfTalent = Held.KampfTalent(Talent.Name);
        //            if (kampfTalent != null)
        //                return kampfTalent.ParadeWert;
        //            else
        //                return Held.ParadeBasis - 3;
        //        }
        //        return 0;
        //    }
        //}

        //public int ParadeWertGesamt
        //{
        //    get
        //    {
        //        return ParadeWert 
        //            + (Waffe != null ? Waffe.WMPA : 0) // Mod durch Waffenmodifikator
        //            + (Regeln.TPKK ? Math.Min(0, TPKK) : 0); // Abzug durch niedrige KK
        //    }
        //}

        //public int TPKK
        //{
        //    get
        //    {
        //        if (Held != null && Waffe != null)
        //            return (Held.KK + Held.ModKK - Waffe.TPKKSchwelle) / Waffe.TPKKSchritt;
        //        return 0;
        //    }
        //}

        #endregion
    }
}
