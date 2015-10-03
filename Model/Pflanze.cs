using MeisterGeister.Logic.Kalender.DsaTool;
using MeisterGeister.Logic.Literatur;
using MeisterGeister.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using ZooBotLogic = MeisterGeister.ViewModel.ZooBot.Logic;

namespace MeisterGeister.Model
{
    public partial class Pflanze : ILiteratur
    {
        public List<string> GebietsNamen
        {
            get
            {
                List<string> namen = new List<string>();
                for (int i = 0; i < this.Gebiet.Count; i++)
                    namen.Add(this.Gebiet.ElementAt(i).Name);
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

        public string Haltbarkeit
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Haltbarkeit == null? "":
                this.Pflanze_Ernte.ElementAt(0).Haltbarkeit + " " + this.Pflanze_Ernte.ElementAt(0).HaltbarkeitEinheit;
            }
        }

        public float? Gewicht
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Gewicht; }
        }

        public string BemerkungErnte
        {
            get { return this.Pflanze_Ernte.ElementAt(0).Bemerkung == null? "":
                this.Pflanze_Ernte.ElementAt(0).Bemerkung;
            }
        }

        public List<Gebiet> Gebiete
        {
            get
            {
                return Gebiet.ToList();
            }
        }
        
        public List<Pflanze_Verbreitung> VerbreitungsListe
        {
            get { return this.Pflanze_Verbreitung.ToList(); }
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

        /// <summary>
        /// Überprüfung der Erntezeit.
        /// </summary>
        /// <param name="month">1.0 bis 14.0</param>
        /// <param name="tagesgenau"></param>
        /// <returns></returns>
        public List<Pflanze_Ernte> GetErnte(double month, bool tagesgenau = true)
        {
            List<Pflanze_Ernte> l;
            l = new List<Pflanze_Ernte>();
            foreach (var ernte in Pflanze_Ernte)
            {
                double von = ernte.Von;
                double bis = ernte.Bis;
                if (bis % 1 == 0)
                    bis++;
                if (von <= bis)
                {
                    if (von <= month && month <= bis)
                        l.Add(ernte);
                }
                else
                {
                    if (!(bis < month && month < von))
                        l.Add(ernte);
                }
            }
            return l;
        }

        /// <summary>
        /// Tagesgenaue Überprüfung der Erntezeit.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Pflanze_Ernte> GetErnte(DSADateTime date)
        {
            if (date != null)
            {
                DSADateCalendarTwelve cal = new DSADateCalendarTwelve(date);
                double month = cal.Month;
                double daysInMonth = 30;
                if (month == 13)
                    daysInMonth = 5;
                double day = cal.Day;
                month += day / daysInMonth;
                return GetErnte(month);
            }
            return Pflanze_Ernte.ToList();
        }

        public bool GetInErnte(string monat)
        {
            if (monat.ToLower() == "komplettes jahr")
                return GetErnte(null).Count>0;
            double monatNr = 
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
            return GetErnte(monatNr).Count>0;
        }

        /// <summary>
        /// Prüfung nur anhand des ganzen Monats. Nicht tagesgenau.
        /// </summary>
        /// <param name="monat">1 bis 13</param>
        /// <returns></returns>
        public bool GetInErnte(double monatNr)
        {
            return GetErnte(monatNr, false).Count>0;
        }
                
        #region Import Export

        #endregion
    }
}
