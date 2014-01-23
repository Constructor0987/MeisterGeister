using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.NSCGenerator
{
    class NamenGenerator
    {
        #region //SUBKLASSEN

        public class Name
        {
            public string VorName = string.Empty;
            public string NachName = string.Empty;
            public string Geschlecht = string.Empty;

            public bool KeineNachsilbeVorname = false;
            public bool KeineNachsilbeNachname = false;
            public bool KeineVorsilbe = false;

            public bool Nachsilbe = false;
            public bool Vorsilbe = false;
            public string namensHerkunft = string.Empty;

            public override string ToString()
            {
                string name = VorName;
                if (NachName != string.Empty)
                    name += " " + NachName;
                if (Geschlecht != null && Geschlecht != string.Empty)
                    name += " (" + Geschlecht + ")";
                return name;
            }
        }

        #endregion

        #region //FELDER
        // intern
        private static readonly Random ZahlenGenerator = new Random();
        //UI
        //Entitylisten
        //Zuordnungen
        //Commands
        #endregion

        #region //KONSTRUKTOR

        public NamenGenerator(){
        /*                       "Svellttal Name", //fehlen in DB siehe WDH 
                   //für jede kultur sind namenseigenheiten zu implementieren!! siehe WDH
                   "Amazonen Namen", //fehlen in DB
                   "Novadische Namen",//fehlen in DB*/
        }
        #endregion

        #region //INSTANZMETHODEN

        public Name generateNameByKultur(string geschlecht, Model.Kultur kultur)
        {
            //TODO (Manuel) adaptiere rasse an namensliste
            Name n = new Name();
            //geschlecht bestimmen
            if (n.Geschlecht == "m/w" || n.Geschlecht == null || n.Geschlecht == string.Empty)
                if (new Würfel(2).Würfeln() == 1) n.Geschlecht = "m";
                else n.Geschlecht = "w";
            // wenn Rasse auch gewählt, dann Name nach Rasse auswählen!! (z.B. Zwerg, Elf, etc.)

            n.namensHerkunft = Global.ContextNsc.getNamenHerkunftByKultur(kultur);

            n.VorName = Global.ContextNsc.getVornameByNamensHerkunft(Global.ContextNsc.getNamenHerkunftByKultur(kultur), n.Geschlecht);
            n.NachName = Global.ContextNsc.getNachnameByNamensHerkunft(n.namensHerkunft, n.Geschlecht);

            return n;
        }

        public Name generateNameByRasse(string geschlecht, Model.Rasse rasse)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region //EVENTS
        #endregion


    }
}
