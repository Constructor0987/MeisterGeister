using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
// Eigene Usings
using System.Collections.ObjectModel;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IKämpfer : INotifyPropertyChanged, IHasWunden, IHasZonenRs
    {
        //Allgemein
        string Name { get; }

        #region Bodenplan
        string Bild { get; }
        string Position { get; }
        #endregion

        //Notwendige Eigenschaften
        int Körperkraft { get; }
        int Gewandtheit { get; }
        int Konstitution { get; }
        int Geschwindigkeit { get; }

        //
        int LebensenergieMax { get; }
        int LebensenergieAktuell { get; set; }
        int AusdauerMax { get; }
        int AusdauerAktuell { get; set; }
        int AstralenergieMax { get; }
        int AstralenergieAktuell { get; set; }
        int KarmaenergieMax { get; }
        int KarmaenergieAktuell { get; set; }

        bool Magiebegabt { get; }
        bool Geweiht { get; }

        //Energie-Stati
        string LebensenergieStatus { get; }
        string LebensenergieStatusDetails { get; }
        string AusdauerStatus { get; }
        string AusdauerStatusDetails { get; }

        //Kampf relevantes
        //int INI { get; }
        int InitiativeBasis { get; }
        int Initiative();
        int InitiativeMax();
        //int Orientieren();
        int? AT { get; } //Hauptwaffe Standardattackewert
        int? PA { get; }

        int MRGeist { get; }
        int MR { get; }

        IRüstungsschutz RS { get; }
        int? Ausweichen { get; }
        int? BE { get; }

        int Wundschwelle { get; }
        int Wundschwelle2 { get; }
        int Wundschwelle3 { get; }
        new IWunden Wunden { get; }
        IWunden WundenByZone { get; }
        
        //int Aktionen { get; } //maximale anzahl an Aktionen, die aufgeteilt werden können
        //int FreieAktionen { get; set; }
        ////Zuteilung der Aktionen in Angriffsaktionen und Abwehraktionen
        //int Angriffsaktionen { get; set; }
        //int Abwehraktionen { get; set; }

        //int VerbrauchteAngriffsaktionen { get; set; }
        //int VerbrauchteAbwehraktionen { get; set; }
        //int VerbrauchteFreieAktionen { get; set; }
        
        ////Aktuellen Kampfstil abbilden. Halbschwert, Mercanario, Beidhändiger Kampf, Schildkampf, ...
        //Kampfstil Kampfstil { get; set; }
        //WaffenloserKampfstil WaffenloserKampfstil { get; set; }

        ObservableCollection<Modifikatoren.IModifikator> Modifikatoren { get; }

        //Umwandlung von Aktionen und Reaktionen muss noch modeliert werden,
        //mögliche überlegung ist eine manöverliste, welche den  Regelkomformen und den nicht Regelkomformen Modus abdeckt, hiermit wird auch die Umwandlug von Aktionen/Reaktionen mit 
        //entsprechender erschwernis geregelt. Ini-listen mod wird dann vermutlich über eine Methode laufen.
        List<Manöver.Manöver> Manöver { get; }

        /// <summary>
        /// Alle zur Zeit verwendeten Waffen.
        /// </summary>
        IList<IWaffe> Angriffswaffen { get; }
    }
}
