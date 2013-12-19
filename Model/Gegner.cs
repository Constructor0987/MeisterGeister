using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.Model.Extensions;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Helden.Logic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MeisterGeister.Model
{
    public partial class Gegner : KampfLogic.Wesen, KampfLogic.IKämpfer, KampfLogic.IGegnerBase, Extensions.IInitializable, KampfLogic.IHasZonenRs, IHasWunden, MeisterGeister.Logic.Literatur.ILiteratur
    {
        public Gegner() : base()
        {
            GegnerGUID = Guid.NewGuid();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        public Gegner(GegnerBase gegnerBase) : this()
        {
            GegnerBaseGUID = gegnerBase.GegnerBaseGUID;
            GegnerBase = gegnerBase;
            AE = gegnerBase.AE;
            AstralenergieAktuell = AstralenergieMax;
            AU = gegnerBase.AU;
            AusdauerAktuell = AusdauerMax;
            LE = gegnerBase.LE;
            LebensenergieAktuell = LebensenergieMax;
            KE = gegnerBase.KE;
            KarmaenergieAktuell = KarmaenergieMax;
            INIBasis = gegnerBase.INIBasis;
            KO = gegnerBase.KO;
            MRGeist = gegnerBase.MRGeist;
            MRKörper = gegnerBase.MRKörper;
            GS = gegnerBase.GS;
            GS2 = gegnerBase.GS2;
            GS3 = gegnerBase.GS3;
            Aktionen = gegnerBase.Aktionen;
            RSArmL = gegnerBase.RSArmL;
            RSArmR = gegnerBase.RSArmR;
            RSBauch = gegnerBase.RSBauch;
            RSBeinL = gegnerBase.RSBeinL;
            RSBeinR = gegnerBase.RSBeinR;
            RSBrust = gegnerBase.RSBrust;
            RSKopf = gegnerBase.RSKopf;
            RSRücken = gegnerBase.RSRücken;
            AT = gegnerBase.AT;
            PA = gegnerBase.PA;
            FK = gegnerBase.FK;
            Name = gegnerBase.Name;
            Bild = gegnerBase.Bild;
            Bemerkung = gegnerBase.Bemerkung;
        }

        #region IInitializable
        public void Initialize()
        {
            //Angriffsaktionen = Aktionen - Abwehraktionen;
        }
        #endregion

        private static Regex klammern = new Regex("\\((\\d+)\\)", RegexOptions.RightToLeft);
        [DependentProperty("Name")]
        public string Kurzname
        {
            get
            {
                string kurzname = Name;
                if (kurzname == null)
                    return null;
                if (kurzname.Length <= 18)
                    return kurzname;
                var m = klammern.Match(kurzname);
                if(m != null)
                {
                    kurzname = klammern.Replace(kurzname, "");
                    kurzname = kurzname.Substring(0, Math.Min(12, kurzname.Length)) + ".. " + m.Value;
                }
                return kurzname;
            }
        }

        #region IKämpfer

        public string Spieler { get { return "Meister"; } }

        private int _initiativeWurf = 0;
        public int InitiativeWurf
        {
            get { return _initiativeWurf; }
        }
        public int Initiative(bool dialog = false)
        {
            // TODO ??: Dialog MVVM-konform aufrufen
            if (dialog)
            {
                int wurf = View.General.ViewHelper.ShowWürfelDialog(INIZufall, "Iinitiative Würfel-Wurf");
                if (wurf != 0)
                    _initiativeWurf = wurf;
            }
            else
                _initiativeWurf = Logic.General.Würfel.Parse(INIZufall);
            return INIBasis - BE.GetValueOrDefault() + InitiativeWurf;
        }

        public int InitiativeMax()
        {
            _initiativeWurf = Logic.General.Würfel.Parse(INIZufall, false);
            return INIBasis - BE.GetValueOrDefault() + InitiativeWurf;
        }

        public int? Orientieren(bool dialog = false)
        {
            // TODO ??: Was ist mit Aufmerksamkeit und Kriegskunst bei Gegnern?
            // TODO ??: Gegner haben keine IN. Wert kann derzeit im Proben-Dialog nicht geändert werden.
            ProbenErgebnis ergebnis;
            if (dialog) // TODO ??: Dialog MVVM-konform aufrufen
                ergebnis = View.General.ViewHelper.ShowProbeDialog(new Eigenschaft("IN", 10), null);
            else
                ergebnis = new Eigenschaft("IN", 10).Würfeln();
            if (ergebnis.Gelungen)
                return InitiativeMax();
            return null;
        }

        [DependentProperty("INIBasis")]
        public int InitiativeBasis
        {
            get { return INIBasis; }
        }

        public string Position
        {
            get { throw new NotImplementedException(); }
        }

        [DependentProperty("KO")]
        public int Körperkraft
        {
            get { return KO; }
        }

        [DependentProperty("KO")]
        public int Gewandtheit
        {
            get { return KO; }
        }

        [DependentProperty("KO")]
        public int Konstitution
        {
            get { return KO; }
        }

        //return GS abhängig vom Modus (fliegend, am boden, galopp, etc.)
        [DependentProperty("GS")]
        public double Geschwindigkeit
        {
            get { return GS; }
        }

        [DependentProperty("LE")]
        public int LebensenergieMax
        {
            get { return LE;  }
        }

        [DependentProperty("LEAktuell")]
        public int LebensenergieAktuell
        {
            get
            {
                return LEAktuell;
            }
            set
            {
                LEAktuell = value;
            }
        }

        [DependentProperty("LebensenergieAktuell"), DependentProperty("LebensenergieMax"), DependentProperty("Konstitution")]
        public string LebensenergieStatus
        {
            get
            {
                return GetLebensenergieStatus();
            }
        }
        
        [DependentProperty("AU")]
        public int AusdauerMax
        {
            get { return AU; }
        }

        [DependentProperty("AUAktuell")]
        public int AusdauerAktuell
        {
            get
            {
                return AUAktuell;
            }
            set
            {
                AUAktuell = value;
            }
        }

        [DependentProperty("AusdauerAktuell"), DependentProperty("AusdauerMax")]
        public string AusdauerStatus
        {
            get
            {
                return GetAusdauerStatus();
            }
        }

        [DependentProperty("AE")]
        public int AstralenergieMax
        {
            get { return AE; }
        }

        [DependentProperty("AEAktuell")]
        public int AstralenergieAktuell
        {
            get
            {
                return AEAktuell;
            }
            set
            {
                AEAktuell = value;
            }
        }

        public int KarmaenergieMax
        {
            get { return 0; }
        }

        public int KarmaenergieAktuell
        {
            get
            {
                return 0;
            }
            set {}
        }

        int? KampfLogic.IKämpfer.AT
        {
            get {
                //TODO JT: stattdessen selectedangriff verwenden
                return AT;
            }
        }

        int? KampfLogic.IKämpfer.PA
        {
            get {
                // TODO JT stattdessen selectedangriff verwenden
                return PA; 
            }
        }

        [DependentProperty("MRKörper")]
        public int MR
        {
            get { return MRKörper ?? 0; }
        }

        private Rüstungsschutz _rs = null;
        public IRüstungsschutz RS
        {
            get
            {
                if (_rs == null)
                    _rs = new Rüstungsschutz((Model.Gegner)this);
                return _rs;
            }
        }

        [DependentProperty("PA")]
        public int? Ausweichen
        {
            get { return ((KampfLogic.IKämpfer)this).PA; }
        }

        private int? _be;
        public int? BE
        {
            get { return _be; }
            set { _be = value; OnChanged("BE"); }
        }

        [DependentProperty("Konstitution"), DependentProperty("LebensenergieMax")]
        public int Wundschwelle
        {
            get {
                if (Konstitution == 0)
                    return (int)Math.Round(LebensenergieMax / 6.0, MidpointRounding.AwayFromZero); //ZBA 10, inoffizielle Errata Wiki Aventurica
                else
                    return (int)Math.Round(Konstitution / 2.0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle2
        {
            get {
                if (Konstitution == 0)
                    return (int)Math.Round(LebensenergieMax / 3.0, MidpointRounding.AwayFromZero);
                else
                    return Konstitution; 
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle3
        {
            get {
                if (Konstitution == 0)
                    return (int)Math.Round(LebensenergieMax / 2.0, MidpointRounding.AwayFromZero);
                else
                    return (int)Math.Round(Konstitution * 1.5, MidpointRounding.AwayFromZero); 
            }
        }

        private Wunden kämpferWunden = null;
        public IWunden WundenByZone
        {
            get
            {
                if (kämpferWunden == null)
                    kämpferWunden = new KampfLogic.Wunden((Model.Gegner)this);
                return kämpferWunden;
            }
        }

        IWunden IKämpfer.Wunden
        {
            get
            {
                return WundenByZone;
            }
        }

        public List<KampfLogic.Manöver.Manöver> Manöver
        {
            get { throw new NotImplementedException(); }
        }

        public IList<KampfLogic.IWaffe> Angriffswaffen
        {
            get { return GegnerBase.GegnerBase_Angriff.Select(ga => (KampfLogic.IWaffe)ga).ToList(); }
        }

        [DependsOnModifikator(typeof(Mod.IModifikator))]
        public IList<Gegner_Angriff> Angriffe
        {
            get 
            {
                IList<Gegner_Angriff> angriffe = new List<Gegner_Angriff>();

                // Basis-Kampfwerte einfügen
                if (AT != 0 || PA != 0)
                    angriffe.Add(new Gegner_Angriff("Basis-Kampfwerte", AT, PA, this));
                if (FK != 0)
                    angriffe.Add(new Gegner_Angriff("Basis-Fernkampf", FK, 0, this));

                foreach (var item in GegnerBase.GegnerBase_Angriff)
                    angriffe.Add(new Gegner_Angriff(item, this));
                return angriffe;
            }
        }

        public bool Magiebegabt
        {
            get { return AstralenergieMax > 0; }
        }

        public bool Geweiht
        {
            get { return KarmaenergieMax > 0; }
        }

        private System.Windows.Media.Color _farbmarkierung = System.Windows.Media.Color.FromArgb(0,0,0,0);
        public System.Windows.Media.Color Farbmarkierung
        {
            get { return _farbmarkierung; }
            set { _farbmarkierung = value; OnChanged("Farbmarkierung"); }
        }

        private string _hinweisText = string.Empty;
        public string HinweisText
        {
            get { return _hinweisText; }
            set { _hinweisText = value; OnChanged("HinweisText"); }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        #region IGegnerBase
        public string INIZufall
        {
            get
            {
                return GegnerBase.INIZufall;
            }
        }

        public int? GW
        {
            get
            {
                return GegnerBase.GW;
            }
        }

        public int? Jagd
        {
            get
            {
                return GegnerBase.Jagd;
            }
        }

        public int? Beschwörung
        {
            get
            {
                return GegnerBase.Beschwörung;
            }
        }

        public int? Kontrolle
        {
            get
            {
                return GegnerBase.Kontrolle;
            }
        }

        public int? Beschwörungskosten
        {
            get
            {
                return GegnerBase.Beschwörungskosten;
            }
        }

        public string Tags
        {
            get
            {
                return GegnerBase.Tags;
            }
        }

        public string Literatur
        {
            get
            {
                return GegnerBase.Literatur;
            }
        }

        public string Setting
        {
            get
            {
                return GegnerBase.Setting;
            }
        }
        #endregion
    }

    #region Gegner_Angriff

    public class Gegner_Angriff : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        public Gegner_Angriff(GegnerBase_Angriff angriff, Gegner gegner)
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            Base_Angriff = angriff;
            Gegner = gegner;
        }

        public Gegner_Angriff(string name, int at, int pa, Gegner gegner)
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            _name = name;
            _at = at;
            _pa = pa;
            Gegner = gegner;
        }

        public GegnerBase_Angriff Base_Angriff { get; set; }
        private Gegner Gegner { get; set; }

        /// <summary>
        /// Gibt an, ob es sich bei diesem Angriff um Basisangriffwerte handelt (globale Kampfwerte).
        /// </summary>
        public bool IsBasis
        {
            get
            {
                return Name.StartsWith("Basis");
            }
        }

        public bool IsBasisNot { get { return !IsBasis; } }

        public bool IsFernkampf
        {
            get
            {
                return Name.Contains("Fernkampf") ||
                    (Reichweite > 0 && PA == 0);
            }
        }

        public bool IsNahkampf { get { return !IsFernkampf; } }

        private string _name;
        public string Name { get { return Base_Angriff == null ? _name : Base_Angriff.Name; } }

        private int _at;
        public int AT { get { return Base_Angriff == null ? _at : Base_Angriff.AT; } }

        private int _pa;
        public int PA { get { return Base_Angriff == null ? _pa : Base_Angriff.PA; } }

        public string Bemerkung { get { return Base_Angriff == null ? null : Base_Angriff.Bemerkung; } }

        public string DK { get { return Base_Angriff == null ? null : Base_Angriff.DK; } }

        public int? Reichweite { get { return Base_Angriff == null ? null : Base_Angriff.Reichweite; } }

        public string TP 
        { 
            get 
            {
                return Base_Angriff == null ? null : string.Format("{0}W{1}+{2}", Base_Angriff.TPWürfelAnzahl, Base_Angriff.TPWürfel, Base_Angriff.TPBonus);
            }
        }

        /// <summary>
        /// Grund-AT-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty("AT")]
        [DependsOnModifikator(typeof(Mod.IModATBasis))]
        [DependsOnModifikator(typeof(Mod.IModAT))]
        public int Attacke
        {
            get
            {
                int v = AT;
                if (Gegner.Modifikatoren != null)
                {
                    Gegner.Modifikatoren.Where(m => m is Mod.IModATBasis).Select(m => (Mod.IModATBasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyATBasisMod(v));
                    Gegner.Modifikatoren.Where(m => m is Mod.IModAT).Select(m => (Mod.IModAT)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyATMod(v));
                }
                return v;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModATBasis))]
        [DependsOnModifikator(typeof(Mod.IModAT))]
        public List<dynamic> ModifikatorenListeAT
        {
            get
            {
                List<dynamic> list = Gegner.ModifikatorenListe(typeof(Mod.IModATBasis), AT);
                list.AddRange(Gegner.ModifikatorenListe(typeof(Mod.IModAT), list.Count() == 0 ? AT : list.LastOrDefault().Wert));
                return list;
            }
        }

        /// <summary>
        /// Grund-PA-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty("PA")]
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        [DependsOnModifikator(typeof(Mod.IModPA))]
        public int Parade
        {
            get
            {
                int v = PA;
                if (Gegner.Modifikatoren != null)
                {
                    Gegner.Modifikatoren.Where(m => m is Mod.IModPABasis).Select(m => (Mod.IModPABasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyPABasisMod(v));
                    Gegner.Modifikatoren.Where(m => m is Mod.IModPA).Select(m => (Mod.IModPA)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyPAMod(v));
                }
                return v;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        [DependsOnModifikator(typeof(Mod.IModPA))]
        public List<dynamic> ModifikatorenListePA
        {
            get
            {
                List<dynamic> list = Gegner.ModifikatorenListe(typeof(Mod.IModPABasis), PA);
                list.AddRange(Gegner.ModifikatorenListe(typeof(Mod.IModPA), list.Count() == 0 ? PA : list.LastOrDefault().Wert));
                return list;
            }
        }

    }

    #endregion
}

