using System;
using System.ComponentModel;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Settings;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public abstract class Wesen : INotifyPropertyChanged
    {
        #region // Allgemeine Eigenschaften

        public abstract string Name { get; set; }

        public string Kurzname
        {
            get { return Name; }
        }

        private bool _aktuellerKämpfer = false;
        public bool AktuellerKämpfer
        {
            get { return _aktuellerKämpfer; }
            set { _aktuellerKämpfer = value; OnPropertyChanged("AktuellerKämpfer"); }
        }

        private KampfAktionListe _aktionen = new KampfAktionListe();
        public KampfAktionListe AktionenLaufend
        {
            get { return _aktionen; }
        }

        public int AktionenVerbleibend
        {
            get
            {
                if (_aktionen != null)
                {
                    int rest = 2;
                    foreach (KampfAktion ak in _aktionen)
                    {
                        if (!ak.Vorbei)
                            rest -= ak.RestAktionenDauer;
                    }
                    return Math.Max(0, rest);
                }
                return 2;
            }
        }

        public abstract int Parade { get; set; }

        /// <summary>
        /// 1 = Helden, 2 = Gegner
        /// </summary>
        public int KampfPartei { get; set; }

        #endregion

        #region // GUI Eigenschaften

        private System.Windows.Media.Brush _farbmarkierung = System.Windows.Media.Brushes.Transparent;
        public System.Windows.Media.Brush Farbmarkierung
        {
            get
            {
                return _farbmarkierung;
            }
            set
            {
                _farbmarkierung = value;
                OnPropertyChanged("Farbmarkierung");
            }
        }

        #endregion

        #region // Initiative

        private uint _initiativeWurf;
        public uint InitiativeWurf
        {
            get { return _initiativeWurf; }
            set { _initiativeWurf = value; OnPropertyChanged(string.Empty); }
        }

        private int _initiativeBasis;
        public virtual int InitiativeBasis
        {
            get { return _initiativeBasis; }
            set { _initiativeBasis = value; OnPropertyChanged(string.Empty); }
        }

        private int _initiativeMod;
        /// <summary>
        /// Patzer, etc.
        /// </summary>
        public int InitiativeMod
        {
            get { return _initiativeMod; }
            set { _initiativeMod = value; OnPropertyChanged(string.Empty); }
        }

        public int Initiative
        {
            get
            {
                // Basiswert
                int ini = InitiativeBasis;

                // Behinderung
                ini -= Behinderung;

                // Waffe
                //TODO ??: Waffen INI

                // Würfel-Wurf
                ini += (int)InitiativeWurf;

                // Wunden
                ini -= Wunden * 2; // Unlokalisierte Wunden
                ini -= WundenKopf * 2;
                ini -= WundenBauch;
                ini -= WundenBeinL * 2;
                ini -= WundenBeinR * 2;

                // Modifikatoren (Patzer, etc.)
                ini += InitiativeMod;

                return ini;
            }
        }

        public abstract WürfelEnum InitiativeZufall { get; }

        #endregion

        #region // Lebensenergie, Ausdauer, Astralenergie, Karmaenergie

        public abstract int LebensenergieAktuell { get; set; }

        public abstract int LebensenergieMax { get; set; }

        public abstract int AusdauerAktuell { get; set; }

        public abstract int AusdauerMax { get; set; }

        public abstract int AstralenergieAktuell { get; set; }

        public abstract int KarmaenergieAktuell { get; set; }

        #endregion

        #region // Konstitution, Wundschwellen

        public virtual int KO { get; set; }

        public int ModKO
        {
            get
            {
                int mod = 0;

                // Wunden
                mod -= WundenBrust;
                mod -= WundenBauch;

                return mod;
            }
        }

        public int ModErschwernisKO
        {
            get
            {
                int mod = ModKO;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public abstract int Wundschwelle { get; }
        public abstract int Wundschwelle2 { get; }
        public abstract int Wundschwelle3 { get; }

        #endregion

        #region // Energiestatus

        public bool Kampfunfähig
        {
            get
            {
                return KampfunfähigLebensenergie || KampfunfähigAusdauer || Bewusstlos || Tot;
            }
        }

        public bool KampfunfähigLebensenergie
        {
            get
            {
                if (LebensenergieMax <= 5)
                    return false;
                return LebensenergieAktuell <= 5;
            }
        }

        public bool KampfunfähigAusdauer
        {
            get
            {
                if (AusdauerMax == 0)
                    return false;
                return AusdauerAktuell <= 0;
            }
        }

        public bool Bewusstlos
        {
            get
            {
                return LebensenergieAktuell <= 0;
            }
        }

        public bool Tot
        {
            get
            {
                return LebensenergieAktuell < KO * -1;
            }
        }

        public bool LebensenergieUnterHälfte
        {
            get
            {
                return LebensenergieAktuell < LebensenergieMax / 2;
            }
        }

        public bool LebensenergieUnterDrittel
        {
            get
            {
                return LebensenergieAktuell < LebensenergieMax / 3;
            }
        }

        public bool LebensenergieUnterViertel
        {
            get
            {
                return LebensenergieAktuell < LebensenergieMax / 4;
            }
        }

        public bool AusdauerUnterDrittel
        {
            get
            {
                if (AusdauerMax == 0)
                    return false;
                return AusdauerAktuell < AusdauerMax / 3;
            }
        }

        public bool AusdauerUnterViertel
        {
            get
            {
                if (AusdauerMax == 0)
                    return false;
                return AusdauerAktuell < AusdauerMax / 4;
            }
        }

        public string LebensenergieStatusDetails
        {
            get
            {
                string info = string.Empty;
                if (Tot)
                    info = "Tot";
                else if (Bewusstlos)
                {
                    info = "Bewusstlos";
                    info += Environment.NewLine + string.Format("tot in W6 x KO ({0}) Kampfrunden ({0} bis {1} KR = {2} bis {3} sec)",
                        KO, 6 * KO, 3 * KO, 18 * KO);
                }
                else if (KampfunfähigLebensenergie)
                {
                    info = "Kampfunfähig";
                    info += Environment.NewLine + "keine Aktionen möglich, außer mit GS 1 bewegen";
                }
                else if (LebensenergieUnterViertel)
                {
                    info = "< 1/4";
                    info += Environment.NewLine + "Optional: Eigenschaftsproben +3; Talent-/Zauberproben +9; GS -3";
                }
                else if (LebensenergieUnterDrittel)
                {
                    info = "< 1/3";
                    info += Environment.NewLine + "Optional: Eigenschaftsproben +2; Talent-/Zauberproben +6; GS -2";
                }
                else if (LebensenergieUnterHälfte)
                {
                    info = "< 1/2";
                    info += Environment.NewLine + "Optional: Eigenschaftsproben +1; Talent-/Zauberproben +3; GS -1";
                }
                info += Environment.NewLine + "(\"Auswirkungen niedriger Lebensenergie\" siehe WdS 56f.)";
                return info;
            }
        }

        public string LebensenergieStatus
        {
            get
            {
                if (Tot)
                    return "Tot";
                else if (Bewusstlos)
                    return "Bewusstlos";
                else if (KampfunfähigLebensenergie)
                    return "Kampfunfähig";
                else if (LebensenergieUnterViertel)
                    return "< 1/4";
                else if (LebensenergieUnterDrittel)
                    return "< 1/3";
                else if (LebensenergieUnterHälfte)
                    return "< 1/2";
                return string.Empty;
            }
        }

        public string AusdauerStatusDetails
        {
            get
            {
                string info = string.Empty;
                if (KampfunfähigAusdauer)
                {
                    info = "Kampfunfähig";
                    info += Environment.NewLine + "keine Aktionen möglich, außer Atem Holen";
                }
                else if (AusdauerUnterViertel)
                {
                    info = "< 1/4";
                    info += Environment.NewLine + "Optional: Eigenschaftsproben +2; Talent-/Zauberproben +6";
                }
                else if (AusdauerUnterDrittel)
                {
                    info = "< 1/3";
                    info += Environment.NewLine + "Optional: Eigenschaftsproben +1; Talent-/Zauberproben +3";
                }
                info += Environment.NewLine + "(\"Auswirkungen niedriger Ausdauer\" siehe WdS 83)";
                return info;
            }
        }

        public string AusdauerStatus
        {
            get
            {
                if (KampfunfähigAusdauer)
                    return "Kampfunfähig";
                else if (AusdauerUnterViertel)
                    return "< 1/4";
                else if (AusdauerUnterDrittel)
                    return "< 1/3";
                return string.Empty;
            }
        }

        #endregion

        #region // Eigenschafts-Modifikatoren

        /// <summary>
        /// Erschwernis auf Eigenschaften. Diese senken die Eigenschaften nicht, sondern stellen nur Modifikatoren auf Proben dar.
        /// </summary>
        public int ModErschwernisEigenschaft
        {
            get
            {
                int mod = 0;

                mod += ModEigenschaftNiedrigeLe;

                mod += ModEigenschaftNiedrigeAu;

                return mod;
            }
        }

        public int ModEigenschaftNiedrigeAu
        {
            get
            {
                int mod = 0;

                // Ausdauer (WdS 83)
                if (Regeln.NiedrigeAU)
                {
                    if (AusdauerUnterViertel)
                        mod += 2;
                    else if (AusdauerUnterDrittel)
                        mod += 1;
                }
                return mod;
            }
        }

        public int ModDreierProbeNiedrigeAu
        {
            get { return ModEigenschaftNiedrigeAu * 3; }
        }

        public int ModEigenschaftNiedrigeLe
        {
            get
            {
                int mod = 0;

                // Lebensenergie (WdS 56f.)
                if (Regeln.NiedrigeLE)
                {
                    if (LebensenergieUnterViertel)
                        mod += 3;
                    else if (LebensenergieUnterDrittel)
                        mod += 2;
                    else if (LebensenergieUnterHälfte)
                        mod += 1;
                }
                return mod;
            }
        }

        public int ModDreierProbeNiedrigeLe
        {
            get { return ModEigenschaftNiedrigeLe * 3; }
        }

        public int ModDreierProbeNiedrige
        {
            get { return ModEigenschaftNiedrigeLe + ModEigenschaftNiedrigeAu; }
        }

        public int ModAttacke
        {
            get
            {
                int mod = 0;

                // Niedrige LE und AU
                mod += ModErschwernisEigenschaft;

                // Wunden
                mod += Wunden * 2; // Unlokalisierte Wunden
                mod += WundenBrust;
                mod += WundenArmL * 2; //TODO ??: Wenn Arm benutzt
                mod += WundenArmR * 2; //TODO ??: Wenn Arm benutzt
                mod += WundenBauch;
                mod += WundenBeinL * 2;
                mod += WundenBeinR * 2;

                return mod;
            }
        }

        public int ModParade
        {
            get
            {
                int mod = 0;

                // Niedrige LE und AU
                mod += ModErschwernisEigenschaft;

                // Wunden
                mod += Wunden * 2; // Unlokalisierte Wunden
                mod += WundenBrust;
                mod += WundenArmL * 2; //TODO ??: Wenn Arm benutzt
                mod += WundenArmR * 2; //TODO ??: Wenn Arm benutzt
                mod += WundenBauch;
                mod += WundenBeinL * 2;
                mod += WundenBeinR * 2;

                return mod;
            }
        }

        public int ModFernkampf
        {
            get
            {
                int mod = 0;

                // Niedrige LE und AU
                mod += ModErschwernisEigenschaft;

                // Unlokalisierte Wunden
                mod += Wunden * 2;

                return mod;
            }
        }

        public string ModKampf_Text
        {
            get
            {
                return string.Format("AT {0} / PA {1}", (ModAttacke > 0 ? "+" + ModAttacke.ToString() : ModAttacke.ToString()),
                    (ModParade > 0 ? "+" + ModParade.ToString() : ModParade.ToString()));
            }
        }

        public string ModKampf_TextDetail
        {
            get
            {
                string txt = string.Empty;

                if (ModEigenschaftNiedrigeLe != 0)
                    txt += string.Format("Niedrige LE: AT/PA/FK/Eigenschaften +{0}\n", ModEigenschaftNiedrigeLe);
                if (ModEigenschaftNiedrigeAu != 0)
                    txt += string.Format("Niedrige AU: AT/PA/FK/Eigenschaften +{0}\n", ModEigenschaftNiedrigeLe);
                if (Wunden != 0)
                    txt += string.Format("Wunden: AT/PA/FK +{0}\n", Wunden * 2);
                if (WundenBrust != 0)
                    txt += string.Format("Wunden (Brust): AT/PA +{0}\n", WundenBrust);
                if (WundenArmL != 0)
                    txt += string.Format("Wunden (ArmL): AT/PA +{0} (wenn Arm benutzt)\n", WundenArmL * 2);
                if (WundenArmR != 0)
                    txt += string.Format("Wunden (ArmR): AT/PA +{0} (wenn Arm benutzt)\n", WundenArmL * 2);
                if (WundenBauch != 0)
                    txt += string.Format("Wunden (Bauch): AT/PA +{0}\n", WundenBauch);
                if (WundenBeinL != 0)
                    txt += string.Format("Wunden (BeinL): AT/PA +{0}\n", WundenBeinL * 2);
                if (WundenBeinR != 0)
                    txt += string.Format("Wunden (BeinR): AT/PA +{0}\n", WundenBeinR * 2);
                if (txt == string.Empty)
                    txt = "Kampfmodifikatoren";

                return txt;
            }
        }

        #endregion

        #region // Rüstungsschutz

        public double BerechneRüstungsschutzGesamt()
        {
            double gRS = 0.0;

            gRS = (RüstungsschutzKopf * 2
                + RüstungsschutzBrust * 4
                + RüstungsschutzRücken * 4
                + RüstungsschutzBauch * 4
                + RüstungsschutzArmL + RüstungsschutzArmR
                + RüstungsschutzBeinL * 2 + RüstungsschutzBeinR * 2) / 20.0;

            return gRS;
        }

        /// <summary>
        /// Setzt den Rüstungsschutz in allen Zonen auf 'rs'.
        /// </summary>
        /// <param name="rs"></param>
        public void SetRüstungsschutz(int rs)
        {
            RüstungsschutzKopf = rs;
            RüstungsschutzBrust = rs;
            RüstungsschutzRücken = rs;
            RüstungsschutzArmL = rs;
            RüstungsschutzArmR = rs;
            RüstungsschutzBauch = rs;
            RüstungsschutzBeinL = rs;
            RüstungsschutzBeinR = rs;
        }

        public int GetRüstungsschutzZone(TrefferzoneEnum trefferzone)
        {
            int rs = 0;
            switch (trefferzone)
            {
                case TrefferzoneEnum.Zufall:
                    rs = RüstungsschutzGesamt;
                    break;
                case TrefferzoneEnum.Unlokalisiert:
                    rs = RüstungsschutzGesamt;
                    break;
                case TrefferzoneEnum.Kopf:
                    rs = RüstungsschutzKopf;
                    break;
                case TrefferzoneEnum.Brust:
                    rs = RüstungsschutzBrust;
                    break;
                case TrefferzoneEnum.Rücken:
                    rs = RüstungsschutzRücken;
                    break;
                case TrefferzoneEnum.ArmL:
                    rs = RüstungsschutzArmL;
                    break;
                case TrefferzoneEnum.ArmR:
                    rs = RüstungsschutzArmR;
                    break;
                case TrefferzoneEnum.Bauch:
                    rs = RüstungsschutzBauch;
                    break;
                case TrefferzoneEnum.BeinL:
                    rs = RüstungsschutzBeinL;
                    break;
                case TrefferzoneEnum.BeinR:
                    rs = RüstungsschutzBeinR;
                    break;
                default:
                    rs = RüstungsschutzGesamt;
                    break;
            }
            return rs;
        }

        public abstract int RüstungsschutzGesamt { get; set; }

        public abstract int RüstungsschutzKopf { get; set; }

        public abstract int RüstungsschutzBrust { get; set; }

        public abstract int RüstungsschutzRücken { get; set; }

        public abstract int RüstungsschutzArmL { get; set; }

        public abstract int RüstungsschutzArmR { get; set; }

        public abstract int RüstungsschutzBauch { get; set; }

        public abstract int RüstungsschutzBeinL { get; set; }

        public abstract int RüstungsschutzBeinR { get; set; }

        public abstract int Behinderung { get; set; }

        #endregion

        #region // VorNachteile, Sonderfertigkeiten

        public abstract bool Magiebegabt { get; }

        public abstract bool Geweiht { get; }

        public abstract bool HatAufmerksamkeit { get; }
        public abstract bool HatVoraussetzungenAufmerksamkeit { get; }

        #endregion

        #region // Wunden

        private int _wunden;
        /// <summary>
        /// Unlokalisierte Wunden.
        /// </summary>
        public virtual int Wunden
        {
            get { return _wunden; }
            set { _wunden = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        private int _wundenKopf;
        public virtual int WundenKopf
        {
            get { return _wundenKopf; }
            set { _wundenKopf = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        private int _wundenBrust;
        public virtual int WundenBrust
        {
            get { return _wundenBrust; }
            set { _wundenBrust = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        private int _wundenArmL;
        public virtual int WundenArmL
        {
            get { return _wundenArmL; }
            set { _wundenArmL = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        private int _wundenArmR;
        public virtual int WundenArmR
        {
            get { return _wundenArmR; }
            set { _wundenArmR = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        private int _wundenBauch;
        public virtual int WundenBauch
        {
            get { return _wundenBauch; }
            set { _wundenBauch = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        private int _wundenBeinL;
        public virtual int WundenBeinL
        {
            get { return _wundenBeinL; }
            set { _wundenBeinL = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        private int _wundenBeinR;
        public virtual int WundenBeinR
        {
            get { return _wundenBeinR; }
            set { _wundenBeinR = value < 0 ? 0 : value; OnPropertyChanged(string.Empty); }
        }

        public bool HatWunde
        {
            get
            {
                return Wunden > 0 || WundenKopf > 0 || WundenBrust > 0 || WundenArmL > 0 || WundenArmR > 0
                    || WundenBauch > 0 || WundenBeinL > 0 || WundenBeinR > 0;
            }
        }

        #endregion

        #region // Kampf-Methoden

        public void Orientieren()
        {
            ProbenErgebnis pErgebnis = new ProbenErgebnis();
            pErgebnis.Gelungen = true;

            if (!(HatAufmerksamkeit && HatVoraussetzungenAufmerksamkeit))
            {
                if (ProbeWürfelnEvent != null)
                {
                    IProbe probe = new Eigenschaft("IN");
                    pErgebnis = ProbeWürfelnEvent(this, probe, "Orientieren");
                }
            }

            if (pErgebnis.Gelungen)
            {
                // INI auf maximales Würfelergebnis setzen
                InitiativeWurf = Convert.ToUInt32(InitiativeZufall);
            }
        }

        public void Schadenspunkte(int sp, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false)
        {
            LebensenergieAktuell -= sp;

            // Wunden
            if (keineWunde == false)
            {
                int wsMod = wundschwelleGesenkt ? 2 : 0;
                int wundenAnzahl = 0;
                if (sp > Wundschwelle3 - wsMod)
                { // 3 Wunden
                    wundenAnzahl = 3;
                }
                else if (sp > Wundschwelle2 - wsMod)
                { // 2 Wunden
                    wundenAnzahl = 2;
                }
                else if (sp > Wundschwelle - wsMod)
                { // 1 Wunde
                    wundenAnzahl++;
                }
                switch (trefferzone)
                {
                    case TrefferzoneEnum.Zufall:
                        Wunden += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.Unlokalisiert:
                        Wunden += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.Kopf:
                        WundenKopf += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.Brust:
                    case TrefferzoneEnum.Rücken:
                        WundenBrust += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.ArmL:
                        WundenArmL += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.ArmR:
                        WundenArmR += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.Bauch:
                        WundenBauch += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.BeinL:
                        WundenBeinL += wundenAnzahl;
                        break;
                    case TrefferzoneEnum.BeinR:
                        WundenBeinR += wundenAnzahl;
                        break;
                    default:
                        Wunden += wundenAnzahl;
                        break;
                }
            }
        }

        public void SchadenspunkteAusdauer(int sp, bool leAbziehen, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false)
        {
            AusdauerAktuell -= sp;
            if (leAbziehen)
                Schadenspunkte(Convert.ToInt32(Math.Round(sp / 2.0, 0, MidpointRounding.AwayFromZero)), trefferzone, wundschwelleGesenkt, keineWunde);
        }

        public void Trefferpunkte(int tp, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false)
        {
            int rs = GetRüstungsschutzZone(trefferzone);
            Schadenspunkte(Math.Max(0, tp - rs), trefferzone, wundschwelleGesenkt, keineWunde);
        }

        public void TrefferpunkteAusdauer(int tp, bool leAbziehen, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false)
        {
            int rs = GetRüstungsschutzZone(trefferzone);
            SchadenspunkteAusdauer(Math.Max(0, tp - rs), leAbziehen, trefferzone, wundschwelleGesenkt, keineWunde);
        }

        #endregion

        #region // Events

        public event ProbeWürfeln ProbeWürfelnEvent;

        #endregion

        #region // INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                if (propertyName == string.Empty)
                { // Keine spezielle Eigenschaft -> Für alle ChangeEvent werfen
                    Type type = this.GetType();
                    foreach (var props in type.GetProperties())
                        PropertyChanged(this, new PropertyChangedEventArgs(props.Name));
                }
                else
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}