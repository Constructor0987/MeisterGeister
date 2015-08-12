using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene usings
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using System.Collections.ObjectModel;
//using MeisterGeister.Logic.Kalender;
using MeisterGeister.Logic.Kalender.DsaTool;

namespace MeisterGeister.ViewModel.Kalender
{
    public class KalenderViewModel : Base.ToolViewModelBase
    {
        public KalenderViewModel()
        {
            Icon = "";
            Name = "Kalender";
            Global.StandortChanged += Global_StandortChanged;
            //
            
        }

        void Global_StandortChanged(object sender, EventArgs e)
        {
            Standort = Global.Standort;
        }

        private DgSuche.Ortsmarke standort = null;
        public DgSuche.Ortsmarke Standort
        {
            get { return standort; }
            set { 
                standort = value;
                OnChanged("Standort");
            }
        }

        private DSADateTime datum = new DSADateTime(DateTime.Now);
        public DSADateTime Datum
        {
            get { return datum; }
            set { 
                datum = value;
                OnChanged("Datum");
            }
        }

        private DSADateCalendar kalender = new DSADateCalendarTwelve();
        public DSADateCalendar Kalender
        {
            get { return kalender; }
            set { 
                kalender = value;
                OnChanged("Kalender");
            }
        }

        private MeisterGeister.Logic.Kalender.Kalender kalenderTyp = MeisterGeister.Logic.Kalender.Kalender.BosparansFall;
        public MeisterGeister.Logic.Kalender.Kalender KalenderTyp
        {
            get { return kalenderTyp; }
            set { 
                kalenderTyp = value;
                OnChanged("KalenderTyp");
                Kalender = MeisterGeister.Logic.Kalender.Zeitrechnung.KalenderDictionary[kalenderTyp];
            }
        }

        private void DatumAufHeute()
        {
            Datum = new DSADateTime(DateTime.Now);
        }

        private void TagVor()
        {
            Datum.addDays(1);
        }

        private void TagZurück()
        {
            Datum.addDays(-1);
        }

        public int Mondphase
        {
            get { return Datum.getMoonday(); }
        }

        List<object> EventListe;

        //Wetter:
        //Wüste? Küste? Berggipfel?
        //Ewiges Eis, Ehernes Schwert, Hoher Norden, Tundra/Taiga, Bornland/Thorwal, Streitende Königreiche bis Weiden, Zentrales Mittelreich, Nördliches Horasreich/Almada/Aranien, Höhen des Raschtulswalls, Südliches Horasreich/Reich der Ersten Sonne, Khôm, Echsensümpfe/Meridiana, Altoum/Gewürzinseln/Südmeer
        //Jahreszeit (abhängig von Position und Datum)
    }
}