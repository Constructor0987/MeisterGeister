using MeisterGeister.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using ZooBotLogic = MeisterGeister.ViewModel.ZooBot.Logic;

namespace MeisterGeister.Model
{
    public partial class Pflanze 
    {
        public List<string> GebietsNamen
        {
            get
            {
                List<string> namen = new List<string>();
                for (int i = 0; i < this.Pflanze_Gebiet.Count; i++)
                    namen.Add(this.Pflanze_Gebiet.ElementAt(i).Gebiet.Name);
                return namen;
            }
        }

        public List<string> LandschaftsNamen
        {
            get
            {
                List<string> namen = new List<string>();
                for (int i = 0; i < this.Landschaften.Count; i++)
                    namen.Add(this.Landschaften.ElementAt(i).Name);
                return namen;
            }
        }

        public List<Pflanze_Ernte> PflanzenErnte
        {
            get { return this.Pflanze_Ernte.Where(t => t.PflanzeGUID == this.PflanzeGUID).ToList(); }
        }

        public string Grundmenge
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Grundmenge; }
        }

        public string Pflanzenteil
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Pflanzenteil; }
        }

        public string Name
        {
            get { return this.Handelsgut.Name; }
        }

        public string Haltbarkeit
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Haltbarkeit == null? "":
                this.Pflanze_Ernte.ElementAt(0).Haltbarkeit + " " + this.Pflanze_Ernte.ElementAt(0).HaltbarkeitEinheit;
            }
        }

        public int? Gewicht
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Gewicht; }
        }

        public string Bemerkung
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Bemerkung == null? "":
                this.Pflanze_Ernte.ElementAt(0).Bemerkung;
            }
        }

        public List<Gebiet> Gebiete
        {
            get
            {
                List<Gebiet> gebieteListe = new List<Gebiet>();

                foreach (Pflanze_Gebiet pGebiet in this.Pflanze_Gebiet)
                    gebieteListe.Add(pGebiet.Gebiet);
                return gebieteListe;
            }
        }
        
        public List<Pflanze_Verbreitung> VerbreitungsListe
        {
            get { return this.Pflanze_Verbreitung.Where(t => t.PflanzeGUID == this.PflanzeGUID).ToList(); }
        }

        public List<Landschaft> Landschaften
        {
            get
            {
                List<Landschaft> landschaftenListe = new List<Landschaft>();

                foreach (Pflanze_Verbreitung verbreitung in VerbreitungsListe)
                    landschaftenListe.Add(verbreitung.Landschaft);

                return landschaftenListe;
            }
        }
                    

        public enum EVorkommen
        {
            SEHRHAEUFIG = 1,
            HAEUFIG = 2,
            GELEGENTLICH = 4,
            SELTEN = 8,
            SEHRSELTEN = 16,
            KEINE = 100
        }

        public Pflanze()
        {
            PflanzeGUID = Guid.NewGuid();
        }
     
        /// <summary>
        /// Gibt die Verbeitung zurück:
        /// SEHRHAEUFIG = 1,
        /// HAEUFIG = 2,
        /// GELEGENTLICH = 4,
        /// SELTEN = 8,
        /// SEHRSELTEN = 16,
        /// KEINE = 100
        /// </summary>
        /// <param name="landschaft"></param>
        /// <returns></returns>       
        public Single GetVorkommen(Guid landschaft)
        {
            Pflanze_Verbreitung pVerbreitung = this.Pflanze_Verbreitung
                .Where(t => t.PflanzeGUID == this.PflanzeGUID)
                .Where(t2 => t2.LandschaftGUID == landschaft).FirstOrDefault();
            return (pVerbreitung != null) ? pVerbreitung.Verbreitung : 100;
        }

        public bool GetInErnte(string monat)
        {
            int monatNr = 
                monat.ToLower() == "komplettes jahr" ? this.Pflanze_Ernte.ElementAt(0).Von :
                monat.ToLower() == "praios" ? 1 :
                monat.ToLower() == "rondra" ? 2 :
                monat.ToLower() == "efferd" ? 3 :
                monat.ToLower() == "travia" ? 4 :
                monat.ToLower() == "boron" ? 5 :
                monat.ToLower() == "hesinde" ? 6 :
                monat.ToLower() == "firun" ? 7 :
                monat.ToLower() == "tsa" ? 8 :
                monat.ToLower() == "phex" ? 9 :
                monat.ToLower() == "peraine" ? 10 :
                monat.ToLower() == "ingerimm" ? 11 :
                monat.ToLower() == "rahja" ? 12 :
                monat.ToLower() == "namenlosetage" ? 13 : 0;
            return this.Pflanze_Ernte.Where(t => t.Von >= monatNr).Where(t2 => t2.Bis <= monatNr) != null;
        }
                
        #region Import Export

        #endregion
    }
}
