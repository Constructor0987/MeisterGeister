using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    /// <summary>
    /// Modifikator
    ///   mehrere Subklassen (Alle Eigenschaften (mit und ohne Auswirkungen auf die Kampfwerte), INI, AT, PA, GS)
    ///   Dauer (kann null sein. Ist für Stärkungszauber etc.)
    /// </summary>
    public class Modifikator : IModifikator
    {
        //TODO JT: Modifikator ins Model
        //Guid ModifikatorGUID;
        //string Beschreibung; // sollte evtl in ToString rein
        //Wert1 (ZfP* oder Mod - jeweils von der SubKlasse zu interpretieren)
        //Wert2 (ZfW - jeweils von der SubKlasse zu interpretieren)
        //Wert3 (AsP - jeweils von der SubKlasse zu interpretieren)
        //Typ wird nur zum speichern in die DB gebraucht
        public virtual string Name
        {
            get { return "Modifikator"; }
        }

        public virtual string Literatur
        {
            get { return null; }
        }

        private DateTime erstellt = DateTime.Now;
        public virtual DateTime Erstellt
        {
            get { return erstellt; }
        }

        /*
         * MU, KL, IN, CH, KK, GE, FF, KO,
         * INI, INI-Basis, MaxLE, MaxAU, MaxAE, GS, AT, PA, FK, AT-Basis, PA-Basis, FK-Basis
         * alle Talentproben, alle Proben, einzelne Talentprobe, Eigenschaftsproben, Zauberproben
         * Talent-TaW, Zauber-TaW
         */
        //Dauer:
        /* bis zu festem Zeitpunkt, bis Ende der KR, bis zur nächsten Aktion, bis zur nächsten AT (passierschlag zählt nicht) oder PA oder Orientieren
         * x SR, x KR, x Tage, x Monate, x Jahre
         * bis zur Sonnenwende
         */
        /*
         * Beispiele: Axxeleratus: INI-Basis x 2, GS x 2, TP+2, gegnerparade+2
         */
    }
}
