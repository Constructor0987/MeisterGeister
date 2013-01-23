using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister.Model
{
    public partial class Held_Talent : Probe, IHatHeld
    {
        public Held_Talent()
            : base()
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        [DependentProperty("Talent")]
        public string Talentname
        {
            get { return Talent.Talentname; }
            set {
                Talent.Talentname = value;
                OnChanged("Talentname");
            }
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
                //_chanceBerechnet = false;
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
                //_chanceBerechnet = false;
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

        /// <summary>
        /// Gibt an, ob bei dem Talent AT/PA-Punkte zugeteilt werden können.
        /// </summary>
        public bool IsZuteilbar
        {
            get
            {
                if (Talent == null)
                    return false;
                return Talent.Untergruppe != Talent.UNTERGRUPPE_ATTECHNIK
                    && Talent.Untergruppe != Talent.UNTERGRUPPE_FERNKAMPF;
            }
        }

        public bool HatAttacke
        {
            get
            {
                return Talent.Untergruppe != Talent.UNTERGRUPPE_FERNKAMPF;
            }
        }

        public bool HatParade
        {
            get
            {
                return Talent.Untergruppe != Talent.UNTERGRUPPE_ATTECHNIK
                    && Talent.Untergruppe != Talent.UNTERGRUPPE_FERNKAMPF;
            }
        }

        public bool HatFernkampf
        {
            get
            {
                return Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF;
            }
        }

        [DependentProperty("ZuteilungPA"), DependentProperty("ZuteilungAT"), DependentProperty("TaW")]
        public bool IsZuteilungOkay
        {
            get
            {
                if (!IsZuteilbar)
                    return true;

                if (TaW.GetValueOrDefault() > 0 && (ZuteilungAT.GetValueOrDefault() < 0 || ZuteilungPA.GetValueOrDefault() < 0))
                    return false;

                int zuteilDiff = TaW.GetValueOrDefault() - ZuteilungAT.GetValueOrDefault() - ZuteilungPA.GetValueOrDefault();
                int zuteilDiff2 = ZuteilungAT.GetValueOrDefault() - ZuteilungPA.GetValueOrDefault();

                return zuteilDiff == 0 && zuteilDiff2 >= -5 && zuteilDiff2 <= 5;
            }
        }

        [DependentProperty("ZuteilungPA"), DependentProperty("ZuteilungAT"), DependentProperty("TaW")]
        public string ZuteilungHinweis
        {
            get
            {
                string hinweis = string.Empty;
                if (!IsZuteilbar)
                    return hinweis;

                int zuteilDiff = TaW.GetValueOrDefault() - ZuteilungAT.GetValueOrDefault() - ZuteilungPA.GetValueOrDefault();

                if (zuteilDiff > 0)
                    hinweis = string.Format("Noch {0} TaP zuteilen!", zuteilDiff);
                else if (zuteilDiff < 0)
                    hinweis = string.Format("{0} TaP zu viel zugeteilt!", zuteilDiff * -1);

                if ((ZuteilungAT.GetValueOrDefault() < 0 || ZuteilungPA.GetValueOrDefault() < 0) && TaW.GetValueOrDefault() >= 0)
                {
                    hinweis += string.IsNullOrEmpty(hinweis) ? string.Empty : Environment.NewLine;
                    hinweis += "Bei einem positiven TaW dürfen keine negativen Punkte verteilt werden!";
                }

                int zuteilDiff2 = ZuteilungAT.GetValueOrDefault() - ZuteilungPA.GetValueOrDefault();
                if (zuteilDiff2 > 5 || zuteilDiff2 < -5)
                {
                    hinweis += string.IsNullOrEmpty(hinweis) ? string.Empty : Environment.NewLine;
                    hinweis += "Es dürfen maximal 5 Punkte Unteschied zwischen AT- und PA-Zuteilung sein!";
                }

                return hinweis;
            }
        }

        [DependentProperty("ZuteilungAT"), DependentProperty("TaW")]
        public int AttackeBasisOhneMod
        {
            get
            {
                if (Held == null)
                    return 0;
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK)
                    return Held.AttackeBasisOhneMod + TaW.GetValueOrDefault();
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return FernkampfwertBasisOhneMod;
                return Held.AttackeBasisOhneMod + ZuteilungAT.GetValueOrDefault();
            }
        }

        [DependentProperty("ZuteilungAT"), DependentProperty("TaW")]
        public int AttackeOhneBE
        {
            get
            {
                if (Held == null)
                    return 0;
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK)
                    return Held.Attacke + TaW.GetValueOrDefault();
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return FernkampfwertOhneBE;
                return Held.Attacke + ZuteilungAT.GetValueOrDefault();
            }
        }

        [DependentProperty("AttackeBasisOhneMod")]
        public int AttackeOhneMod
        {
            get
            {
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK)
                    return AttackeBasisOhneMod + BerechneEffBehinderung();
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return FernkampfwertOhneMod;
                return AttackeBasisOhneMod - (int)Math.Floor(BerechneEffBehinderung() / 2.0);
            }
        }

        [DependentProperty("AttackeOhneBE")]
        public int Attacke
        {
            get
            {
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK)
                    return AttackeOhneBE + BerechneEffBehinderung();
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return Fernkampfwert;
                return AttackeOhneBE - (int)Math.Floor(BerechneEffBehinderung() / 2.0);
            }
        }

        [DependentProperty("ZuteilungPA")]
        public int ParadeBasisOhneMod
        {
            get
            {
                if (Held == null || Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK
                     || Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return 0;
                return Held.ParadeBasisOhneMod + ZuteilungPA.GetValueOrDefault();
            }
        }

        [DependentProperty("ZuteilungPA")]
        public int ParadeOhneBE
        {
            get
            {
                if (Held == null || Talent.Untergruppe == Talent.UNTERGRUPPE_ATTECHNIK
                     || Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return 0;
                return Held.Parade + ZuteilungPA.GetValueOrDefault();
            }
        }

        [DependentProperty("ParadeBasisOhneMod")]
        public int ParadeOhneMod
        {
            get
            {
                return ParadeBasisOhneMod - (int)Math.Ceiling(BerechneEffBehinderung() / 2.0);
            }
        }

        [DependentProperty("ParadeOhneBE")]
        public int Parade
        {
            get
            {
                return ParadeOhneBE - (int)Math.Ceiling(BerechneEffBehinderung() / 2.0);
            }
        }

        [DependentProperty("TaW")]
        public int FernkampfwertBasisOhneMod
        {
            get
            {
                if (Held == null)
                    return 0;
                if (Talent.Untergruppe == Talent.UNTERGRUPPE_FERNKAMPF)
                    return Held.FernkampfBasisOhneMod + TaW.GetValueOrDefault();
                return 0;
            }
        }

        [DependentProperty("TaW")]
        public int FernkampfwertOhneBE
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

        [DependentProperty("FernkampfwertBasisOhneMod")]
        public int FernkampfwertOhneMod
        {
            get
            {
                return FernkampfwertBasisOhneMod - BerechneEffBehinderung();
            }
        }

        [DependentProperty("FernkampfwertOhneBE")]
        public int Fernkampfwert
        {
            get
            {
                return FernkampfwertOhneBE - BerechneEffBehinderung();
            }
        }

        [DependsOnModifikator(typeof(Mod.IModAT))]
        public List<dynamic> ModifikatorenListeAT
        {
            get
            {
                if (Held == null)
                    return new List<dynamic>();
                List<dynamic> list = Held.ModifikatorenListe(typeof(Mod.IModATBasis), AttackeBasisOhneMod);
                list.AddRange(Held.ModifikatorenListe(typeof(Mod.IModAT), list.Count() == 0 ? AttackeBasisOhneMod : list.LastOrDefault().Wert));
                return list;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModPA))]
        public List<dynamic> ModifikatorenListePA
        {
            get
            {
                if (Held == null)
                    return new List<dynamic>();
                List<dynamic> list = Held.ModifikatorenListe(typeof(Mod.IModPABasis), ParadeBasisOhneMod);
                list.AddRange(Held.ModifikatorenListe(typeof(Mod.IModPA), list.Count() == 0 ? ParadeBasisOhneMod : list.LastOrDefault().Wert));
                return list;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModFK))]
        public List<dynamic> ModifikatorenListeFK
        {
            get
            {
                if (Held == null)
                    return new List<dynamic>();
                List<dynamic> list = Held.ModifikatorenListe(typeof(Mod.IModFKBasis), FernkampfwertBasisOhneMod);
                list.AddRange(Held.ModifikatorenListe(typeof(Mod.IModFK), list.Count() == 0 ? FernkampfwertBasisOhneMod : list.LastOrDefault().Wert));
                return list;
            }
        }

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
