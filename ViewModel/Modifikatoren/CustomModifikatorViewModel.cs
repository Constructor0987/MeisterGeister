using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister.ViewModel.Modifikatoren
{
    public class CustomModifikatorViewModel
    {
        private CustomModifikatorFactory factory;
        
        //Basisdaten
        // Liste von möglichen Modifikatoren (gruppiert nach Typ)
        // Liste von Rechenfunktionen
        // Liste von Zaubernamen
        // Liste von Talentnamen

        //Liste von eingebrachten Modifikatoren
        // je Modifikator:
        // Rechenfunktion und Wert
        // Beim EndetMitZeitpunkt-Modifikator muss später noch ein DSA-Datum hinzugefügt werden. Das setzt aber einen überarbeiteten Kalender voraus.

        //Auswirkungstext
        //Fehler
        //Name (editierbar)
        //Literaturangabe (editierbar)
        //0-n Zaubername
        //0-n Talentname

        //Aktionen
        // Modifikator hinzufügen
        //  mit check ob schon vorhanden?
        // Talentname hinzufügen
        // Talentname löschen
        // Zaubername hinzufügen
        // Zaubername löschen
        // Modifikator löschen
        // Anwenden (ist danach nicht mehr editierbar)
    }
}
