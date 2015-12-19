using MeisterGeister.Logic.Kalender;
using MeisterGeister.Logic.Literatur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Model
{
    public partial class Pflanze_Ernte
    {
        public bool Verfügbar
        {
            get
            {
                double monat = (int)Datum.Aktuell.Monat + (Datum.Aktuell.Tag - 1) / (Datum.Aktuell.Monat == Monat.NamenloseTage ? 5.0 : 30.0);
                if (Von < Bis)
                    return monat < Bis && monat >= Von;
                else
                    return monat >= Von || monat < Bis;
            }
        }

        public Monat VonMonat
        {
            get { return (Monat)Von; }
        }
        public Monat BisMonat
        {
            get
            {
                //Da die Eigenschaft 'Bis' das exklusive Ende der Erntezeit darstellt
                //gehen wir hier einen Monat zurück um die darstellung an der GUI verständlicher zu gestalten
                double monat = Bis - 1;
                if (monat < 0)
                    monat += 13;
                return (Monat)monat;
            }
        }

        public bool GanzesJahrVerfügbar
        {
            get { return Von == Bis; }
        }
    }
}
