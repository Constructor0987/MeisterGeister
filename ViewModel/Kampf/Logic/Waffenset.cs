using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    /// <summary>
    /// Das Waffenset stellt die Kombination von Kampfstil, verwendeter Ausrüstung und des Waffentalentes.
    /// </summary>
    public class Waffenset
    {
        //TODO ??: Waffensets Konzeptionell erwarbeiten & für Inventar als Waffenset berücksichtigen
        //z.B. Ausrüstung: Reiterschild & Langschwert -- referenz auf Ausrüstung
        //z.B. Waffentalent: Schwert mit zuordnung zu "Hand"->(n)
        //z.B. Kampfstil: Schildkampf, Mercanario

        //WaffensetId (wenn es in die DB kommt)
        //Name
        //HeldGUID

        //WaffenloserStil
        //Kampfstil

        //Liste von Held_Ausrüstung


        /*

        Folgende Änderungen an der Datenbank sind nötig um Waffensets zu realisieren:

        Tabelle Held_Ausrüstung:
            Der Trageort ist zur Zeit teil des Primärschlüssels, wesshalb man ihn auch noch nicht ändern kann.
            Hier muss eine eigene GUID als Primärschlüssel eingefügt werden.
            Die Anzahl wird entfernt. Es kann sein dass ein Held 2 Kurzschwerter hat. Eins davon kann sich z.B. in der Hand befinden. Das andere dagegen im Wagen.
            Wenn ein Held mehrere Items hat wird einfach der Eintrag mehrfach in der Tabelle hinzugefügt.
            Der BF bezieht sich nicht auf alle Ausrüstungsgegenstände, sondern nur auf Waffen und Schilde. Diese Spalte gehört nicht in Held_Ausrüstung, sondern muss der entsprechenden Waffe/Schild zugeordnet werden.
            Waffen/Schilde sind aktuell nur mit dem Basiswerten in der Datenbank vertreten. Wir sollten das irgendwie ändern so dass wir auch Werte modifizieren können.
            Der BF ist allerdings nur ein Wert den man modifizieren können soll. Wenn man persönliche Waffen schmiedet wäre es auch sinnvoll die TP, INI und W/M ändern zu können.
            Vielleicht orientieren wir uns hier an Gegner und GegnerBase. Oder wir fügen in den Tabellen Waffe und Schild ein Flag hinzu,
            welches angibt ob es sich um eine persönliche Waffe handelt um dort dann die Werte ändern zu können, ohne es für alle Waffen dieses Typs zu ändern.
            Einen solchen Tabelleneintrag könnte man dann anlegen wenn eine Waffe zum Inventar hinzugefügt wird.



        Waffen:
            Es soll auch möglich sein meisterlich geschmiedete Waffen zu haben. (Andere TP, BF, INI, W/M)



        */
    }
}