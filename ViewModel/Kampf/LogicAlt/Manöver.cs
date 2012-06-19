using System;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public class Manöver : KampfAktion
    {
        //nicht implementiert
        /*
         * Stichwortsammlung zum Thema Manöver und der Kampfrunde
         * 
         * Liste der Gegner im Kampf
         * Kampfmodifikatoren durch die Umgebung (Sicht, Überzahl usw.) in Listenform mit verschiedenen Modifikatorklassen, damit man z.B. den Überzahlmodifikator ausnehmen kann.
         * Mehrere GUI Prompts sollten möglich sein (Aufteilung des AT-Wertes etc.) oder eine andere Art Zusatzinformationen abzufragen
         * Auch vergleichende Proben kommen vor
         * Die Manöver sollten in Initiativereihenfolge ausgeführt werden nachdem sie in umgekehrter Reihhenfolge definiert wurden
         * -> Trennung von Definition und Ausführung
         * Zweithandattacken und umgewandelte Aktionen haben andere Ausführungszeitpunkte als normale Angriffe.
         * Parademanöver werden mit Ausnahme des Defensiven Kampfstils, der Klingenwand, der 2. Schild und Parierwaffenparade erst bei Bedarf angesagt.
         * 
         * Kann das Manöver ausgeführt werden? Waffe, Waffentalent, aktueller Stil -> static boolean
         * Wenn ja dann sollte es in der Liste im Kampftool auftauchen.
         * 
         * Die Methode AT-erfolgreich braucht für Gegenhalten einen optionalen 0,5 Modifikator für die TP
         * 
         * Speichern eines Kampfes:
         * Liste der Teilnehmer und der Seite auf der sie sind.
         * Für den Bodenplan
         *  Position
         * Für einen laufenden Kampf, der später fortgesetzt werden soll zusätzlich:
         *  Aktuelle Modifikatoren der Kämpfer
         *  unverbrauchte Aktionen
         *  Ansagen von Beginn der Runde
         *  Aktueller Kampfstil
         *  Initiativdurchgang
         *  Kampfrundennummer
         * komplette Aktionshistorie als Datenbasis
         * 
         * KR:
         * Benutzung von Def. Kampfstil, Klingenwand, 2. Schild und Waffenparade ansagen. (kleine Icons in der INI liste)
         * Umwandeln für jeden Kämpfer ohne Aufmerksamkeit.
         * Aktionen in umgekehrter Ini-Reihenfolge ansagen. Helden mit Aufmerksamkeit sagen hier ihre Umwandlungen an sobald sie agieren.
         * Wenn man dabei Ziel eines Angriffs ist, dann zählt dies für Helden mit Aufmerksamkeit als erste Aktion, also muss auch das Umwandeln angesagt werden.
         * Ausführung der Aktionen in Ini-Reihenfolge:
         * Angriff
         *  AT-würfeln (AT-Aktion(en) verbrauchen)
         *  Wenn Gegenhalten angesagt
         *   Gegenhalten würfeln (PA-verbrauchen)
         *  Wenn AT erfolgreich
         *   ist eine PA verfügbar? Hat das Ziel Kampfgespür und kann es eine AT umwandeln?
         *    PA-Manöver würfeln (PA-Aktion verbrauchen)
         *     PA erfolgreich ausführen
         *     oder PA mißlungen und AT erfolgreich ausführen 
         *  wenn AT nicht erfolgreich 
         *   Hat das Ziel Kampfgespür und möchte dieses umwandeln?
         * 
         * 
         * Für Kampfgespür und das Umwandeln jederzeit hätte ich gerne eine elegantere Lösung
         * 
         * 
         * Wunschliste an ein Kampftool:
         * Initiativliste ist gleichzeitig Kämpferliste - Kampfinfos zu jedem Kämpfer in der Detailsansicht, wenn man einen auswählt.
         * Modifikatoren für jeden Kämpfer in der Detailansicht
         * Modifikatoren wirken sich auf die Listensortierung und Kampfwerte aus
         * Kampfstil in der Detailansicht
         * 
         * Am anfang einer KR sollte nach dem Umwandeln gefragt werden ( AT/PA, AT/AT, PA/PA )
         * Anzeige der noch verfügbaren Aktionen
         * Ansage der Aktionen in umgekehrter Ini-Reihenfolge - Ohne implementierte Manöver genügt hier erstmal dafür verbrauchte aktionen, text und Ziel (manueller modus)
         * Ausführen der Aktionen in INI Reihenfolge - KampfAktion
         * 
         * 
         * Alle Kämpfer sind in der Liste
         * Neuer Kampf:
         * Für jeden Spieler nach Namen sortiert muss der INI-Wurf eingegeben werden.
         * 
        */
    }
}
