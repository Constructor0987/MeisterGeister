using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Model
{
    public partial class Held_Talent : Probe, IHatHeld
    {
        public Held_Talent()
            : base()
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        #region //---- PROBE ----

        [DependentProperty("Talent")]
        override public string Probenname
        {
            get { return Talent != null ? Talent.Talentname : string.Empty; }
            set 
            { 
                if (Talent != null)
                    Talent.Talentname = value; 
            }
        }

        override public int[] Werte
        {
            get
            {
                if (_werte == null || _werte.Length != 3)
                    _werte = new int[3];
                if (Held != null && Talent != null)
                {
                    _werte[0] = Held.EigenschaftWert(Talent.Eigenschaft1);
                    _werte[1] = Held.EigenschaftWert(Talent.Eigenschaft2);
                    _werte[2] = Held.EigenschaftWert(Talent.Eigenschaft3);
                }
                return _werte;
            }
            set
            {
                _werte = value;
                _chanceBerechnet = false;
                OnChanged("Werte");
            }
        }

        [DependentProperty("TaW")]
        public override int Fertigkeitswert
        {
            get
            {
                return TaW ?? 0;
            }
            set
            {
                TaW = value;
                _chanceBerechnet = false;
                OnChanged("Fertigkeitswert");
            }
        }

        override public string WerteNamen
        {
            get
            {
                if (Talent != null)
                    return string.Format("({0}/{1}/{2})", Talent.Eigenschaft1, Talent.Eigenschaft2, Talent.Eigenschaft3);
                return string.Empty;
            }
        }

        #endregion //---- PROBE ----

        #region // ---- Kampfwerte ----

        [DependentProperty("ZuteilungAT"), DependentProperty("TaW")]
        public int Attacke
        {
            get
            {
                if (Held == null)
                    return 0;
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK)
                    return Held.Attacke + TaW.GetValueOrDefault();
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return Fernkampfwert;
                return Held.Attacke + ZuteilungAT.GetValueOrDefault();
            }
        }

        [DependentProperty("ZuteilungPA")]
        public int Parade
        {
            get
            {
                if (Held == null || Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK
                     || Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return 0;
                return Held.Parade + ZuteilungPA.GetValueOrDefault();
            }
        }

        [DependentProperty("TaW")]
        public int Fernkampfwert
        {
            get
            {
                if (Held == null)
                    return 0;
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return Held.FernkampfBasis + TaW.GetValueOrDefault();
                return 0;
            }
        }

        // TODO MT: AT/PA-Werte inkl. eBE

        #endregion // ---- Kampfwerte ----

        public int BerechneEffBehinderung()
        {
            int be = 0;

            string eBe = Talent == null ? string.Empty : Talent.eBE;
            if (string.IsNullOrEmpty(eBe))
                be = 0;
            else if (eBe == "BE")
                be = Held.Behinderung;
            else if (eBe.StartsWith("BEx"))
                be = Held.Behinderung * Convert.ToInt32(eBe.Substring(3));
            else if (eBe.StartsWith("BE-"))
                be = Held.Behinderung - Convert.ToInt32(eBe.Substring(3));
            else if (eBe.StartsWith("BE+"))
                be = Held.Behinderung + Convert.ToInt32(eBe.Substring(3));

            return Math.Max(be, 0);
        }

    }
}
