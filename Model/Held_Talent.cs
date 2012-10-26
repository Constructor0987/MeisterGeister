﻿using System;
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

        public int BerechneEffBehinderung()
        {
            int be = 0;

            string eBe = Talent.eBE;
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
