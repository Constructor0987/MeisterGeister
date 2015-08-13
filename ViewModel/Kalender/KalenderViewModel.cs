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
        }

        public override void RegisterEvents()
        {
        }

        public override void UnregisterEvents()
        {
        }

        private DSADateTime datum = new DSADateTime(DateTime.Now);
        public DSADateTime Datum
        {
            get { return datum; }
            set { Set(ref datum, value); }
        }

        private DSADateCalendar kalender = new DSADateCalendarTwelve();
        public DSADateCalendar Kalender
        {
            get { return kalender; }
            set { Set(ref kalender, value); }
        }

        private MeisterGeister.Logic.Kalender.Kalender kalenderTyp = MeisterGeister.Logic.Kalender.Kalender.BosparansFall;
        public MeisterGeister.Logic.Kalender.Kalender KalenderTyp
        {
            get { return kalenderTyp; }
            set { 
                Set(ref kalenderTyp, value);
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