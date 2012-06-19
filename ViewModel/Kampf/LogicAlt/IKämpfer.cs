// Eigene Usings
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public interface IKämpfer : System.ComponentModel.INotifyPropertyChanged
    {
        #region // Allgemeine Eigenschaften

        string Name { get; set; }

        string Kurzname { get; }

        KampfAktionListe AktionenLaufend { get; }

        bool AktuellerKämpfer { get; set; }

        /// <summary>
        /// 1 = Helden, 2 = Gegner
        /// </summary>
        int KampfPartei { get; set; }

        int Parade { get; set; }

        #endregion

        #region // GUI Eigenschaften

        System.Windows.Media.Brush Farbmarkierung { get; set; }

        #endregion

        #region // Initiative

        string InitiativeInfo { get; }

        string InitiativeInfoDetails { get; }

        int Initiative { get; }

        int InitiativeBasis { get; }

        int InitiativeMod { get; set; }

        uint InitiativeWurf { get; set; }

        WürfelEnum InitiativeZufall { get; }

        #endregion

        #region // Lebensenergie, Ausdauer, Astralenergie, Karmaenergie

        int LebensenergieAktuell { get; set; }

        int LebensenergieMax { get; }

        int AusdauerAktuell { get; set; }

        string LebensenergieStatus { get; }

        string LebensenergieStatusDetails { get; }

        string AusdauerStatus { get; }

        string AusdauerStatusDetails { get; }

        int AusdauerMax { get; }

        int AstralenergieAktuell { get; set; }

        int AstralenergieMax { get; }

        int KarmaenergieAktuell { get; set; }

        int KarmaenergieMax { get; }

        #endregion

        #region // Konstitution, Wundschwellen

        string Wundschwellen { get; }

        #endregion

        #region // Energiestatus

        bool Kampfunfähig { get; }

        bool Bewusstlos { get; }

        bool Tot { get; }

        #endregion

        #region // Eigenschafts-Modifikatoren

        int ModErschwernisEigenschaft { get; }

        int ModAttacke { get; }

        int ModParade { get; }

        string ModKampf_Text { get; }

        #endregion

        #region // Rüstungsschutz

        double BerechneRüstungsschutzGesamt();

        int RüstungsschutzGesamt { get; set; }

        int RüstungsschutzKopf { get; set; }

        int RüstungsschutzBrust { get; set; }

        int RüstungsschutzRücken { get; set; }

        int RüstungsschutzArmL { get; set; }

        int RüstungsschutzArmR { get; set; }

        int RüstungsschutzBauch { get; set; }

        int RüstungsschutzBeinL { get; set; }

        int RüstungsschutzBeinR { get; set; }

        int Behinderung { get; set; }

        #endregion

        #region // VorNachteile, Sonderfertigkeiten

        bool Magiebegabt { get; }

        bool Geweiht { get; }

        #endregion

        #region // Wunden

        /// <summary>
        /// Unlokalisierte Wunden.
        /// </summary>
        int Wunden { get; set; }

        int WundenKopf { get; set; }

        int WundenBrust { get; set; }

        int WundenArmL { get; set; }

        int WundenArmR { get; set; }

        int WundenBauch { get; set; }

        int WundenBeinL { get; set; }

        int WundenBeinR { get; set; }

        bool HatWunde { get; }

        #endregion

        #region // Kampf-Methoden

        void Schadenspunkte(int sp, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false);

        void SchadenspunkteAusdauer(int sp, bool leAbziehen, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false);

        void Trefferpunkte(int tp, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false);

        void TrefferpunkteAusdauer(int tp, bool leAbziehen, TrefferzoneEnum trefferzone, bool wundschwelleGesenkt = false, bool keineWunde = false);
        
        void Orientieren();

        #endregion
    }
}
