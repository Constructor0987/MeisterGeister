using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
// Eigene Usings
using System.Collections.ObjectModel;
using MeisterGeister.ViewModel.AudioPlayer.Logic;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IKämpfer : INotifyPropertyChanged, IHasWunden, IHasZonenRs
    {
        //Allgemein
        string Name { get; }
        string Initialen { get; }
        string Spieler { get; }

        #region Bodenplan
        string Bild { get; }
        Position Position { get; set; }
        #endregion


        //Notwendige Eigenschaften
        int Körperkraft { get; }
        int Gewandtheit { get; }
        int Konstitution { get; }
        int KonstitutionOhneWunden { get; }
        double Geschwindigkeit { get; }

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

        bool Kampfunfähig { get; }

        //Kampf relevantes
        //int INI { get; }
        int InitiativeBasis { get; }
        int InitiativeWurf { get; }
        int Initiative(bool dialog = false);
        int InitiativeMax();
        int? Orientieren(bool dialog = false);
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
        
        ////Aktuellen Kampfstil abbilden. Halbschwert, Beidhändiger Kampf, Schildkampf, ...
        //Kampfstil Kampfstil { get; set; }

        Modifikatoren.ModifikatorenListe Modifikatoren { get; }

        //Umwandlung von Aktionen und Reaktionen muss noch modeliert werden,
        //mögliche überlegung ist eine manöverliste, welche den  Regelkomformen und den nicht Regelkomformen Modus abdeckt, hiermit wird auch die Umwandlug von Aktionen/Reaktionen mit 
        //entsprechender erschwernis geregelt. Ini-listen mod wird dann vermutlich über eine Methode laufen.
        List<Manöver.Manöver> Manöver { get; }

        /// <summary>
        /// Alle zur Zeit verwendeten Waffen mit denen Nahkampfangriffe ausgeführt werden können.
        /// Sortiert nach AT-Wert.
        /// </summary>
        IList<INahkampfwaffe> Angriffswaffen { get; }

        /// <summary>
        /// Alle zur Zeit verwendeten Waffen mit denen Paraden durchgeführt werden können.
        /// Sortiert nach PA-Wert.
        /// </summary>
        IList<INahkampfwaffe> Paradewaffen { get; }

        /// <summary>
        /// Alle zur Zeit verwendeten Waffen mit denen Fernkampfangriffe ausgeführt werden können.
        /// Sortiert nach FK-Wert.
        /// </summary>
        IList<IFernkampfwaffe> Fernkampfwaffen { get; }

        System.Windows.Media.Color Farbmarkierung { get; set; }
        string HinweisText { get; set; }
    }
}
