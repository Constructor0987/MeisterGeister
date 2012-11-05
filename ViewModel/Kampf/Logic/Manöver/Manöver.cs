using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;

using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Extensions;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    //TODO JT: überarbeiten
    //Probe für ein Manöver wird am Anfang des Manövers ausgeführt.
    //Wenn die VerbleibendeDauer auf 0 geht wird es angewandt.
    public class Manöver : INotifyPropertyChanged
    {
        protected static Object syncRoot;
        protected static volatile List<Type> _manöverListe = null;
        public static List<Type> ManöverListe
        {
            get {
                if (Manöver._manöverListe == null) {
                    lock (Manöver.syncRoot) {
                        if (Manöver._manöverListe == null)
                        {
                            Assembly ass = Assembly.GetAssembly(typeof(Manöver));
                            Manöver._manöverListe = ass.GetTypes().Where(t => t.IsSubclassOf(typeof(Manöver))).ToList(); ;
                        }
                    }
                }
                return Manöver._manöverListe;
            }
        }

        protected Manöver(IKämpfer ausführender)
            : this(ausführender, new Dictionary<IWaffe, IKämpfer>(1), 1)
        {
        }

        protected Manöver(IKämpfer ausführender, double dauer)
            : this(ausführender, new Dictionary<IWaffe, IKämpfer>(1), dauer)
        {
        }

        protected Manöver(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : this(ausführender, new Dictionary<IWaffe, IKämpfer>() { { waffe, ziel } }, 1)
        {
        }

        protected Manöver(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel, double dauer)
            : this(ausführender, new Dictionary<IWaffe, IKämpfer>() { { waffe, ziel } }, dauer)
        {
        }

        protected Manöver(IKämpfer ausführender, IDictionary<IWaffe, IKämpfer> waffe_ziel)
            : this (ausführender, waffe_ziel, 1)
        {
        }

        protected Manöver(IKämpfer ausführender, IDictionary<IWaffe, IKämpfer> waffe_ziel, double dauer)
        {
            Ansage = 0;
            Ausführender = ausführender;
            WaffeZiel = waffe_ziel;
            Dauer = dauer;
            VerbleibendeDauer = Dauer;
        }

        //felder/parameter
        public virtual String Name
        {
            get { return "Manöver"; }
        }

        public virtual String Literatur
        {
            get { return "WdS 59"; }
        }

        /*
         * ausführender, ziele, variable erschwernis (aufgeteilt in ansage und grunderschwernis,
         * so dass man den aufschlag bei misslingen für die nächste aktion erstellen kann)
         */
        public virtual IKämpfer Ausführender
        {
            get;
            private set;
        }

        public virtual IDictionary<IWaffe, IKämpfer> WaffeZiel
        {
            get;
            private set;
        }

        public virtual IEnumerable<IKämpfer> Ziele
        {
            get
            {
                return WaffeZiel.Values.Distinct();
            }
        }

        public virtual IEnumerable<IWaffe> Waffen
        {
            get
            {
                return WaffeZiel.Keys.Distinct();
            }
        }

        public virtual int Grunderschwernis
        {
            get { return 0; }
        }

        public virtual int Ansage
        {
            get;
            set;
        }

        private double dauer = 1;
        public virtual double Dauer
        {
            get { return dauer; }
            set { 
                dauer = value;
                OnChanged("Dauer");
            }
        }

        private double verbleibendeDauer = 1;
        /// <summary>
        /// Die Restdauer der Aktion in Aktionen.
        /// </summary>
        public double VerbleibendeDauer
        {
            get { return verbleibendeDauer; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value == verbleibendeDauer)
                    return;
                verbleibendeDauer = value;
                if(value == 0)
                    Ausführender.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitAktion);
                OnChanged("VerbleibendeDauer");
            }
        }

        public virtual int Erschwernis
        {
            get { return Ansage + Grunderschwernis; }
        }

        //TODO: muss eine Probe für das UI anbieten
        public virtual Probe Probe
        {
            get { return null; }
        }

        /// <summary>
        /// Ausführen einer Aktion des Manövers.
        /// Verbraucht Aktion(en) des Kämpfers.
        /// </summary>
        /// <returns></returns>
        public virtual Probe Ausführen()
        {
            AktionenVerbrauchen();
            VerbleibendeDauer--;
            RaiseOnAusführung();
            if(VerbleibendeDauer > 0)
                return null;
            return Probe;
        }

        /// <summary>
        /// Die Anzahl der Angriffsaktionen, die verbraucht werden.
        /// </suAusführender
        public virtual int Angriffsaktionen
        {
            get { return 1; }
        }

        /// <summary>
        /// Die Anzahl der Abwehraktionen, die verbraucht werden.
        /// </suAusführender
        public virtual int Abwehraktionen
        {
            get { return 0; }
        }

        /// <summary>
        /// Die Anzahl der freien Aktionen, die verbraucht werden.
        /// </suAusführender
        public virtual int FreieAktionen
        {
            get { return 0; }
        }

        protected virtual void AktionenVerbrauchen()
        {
            if (Ausführender == null)
                return;
            Ausführender.VerbrauchteAngriffsaktionen += Angriffsaktionen;
            Ausführender.VerbrauchteAbwehraktionen += Abwehraktionen;
            Ausführender.VerbrauchteFreieAktionen += FreieAktionen;
        }

        //eine weitere methode für den erfolg
        /// <summary>
        /// Die Auswirkungen des Manövers auf das Ziel anwenden.
        /// </summary>
        /// <param name="ziel"></param>
        public virtual void Erfolg(IKämpfer ziel)
        {
            OnAktion();
        }

        //und eine für den misserfolg
        /// <summary>
        /// Legt den MisslungenModifikator auf dem Ausführenden an.
        /// </summary>
        public virtual void Misserfolg()
        {
            // Erschwernis als Malus bis zur nächsten Aktion.
            int malus = Erschwernis;
            // Klingentänzer haben nur die halbe Ansage als Malus
            if (Ausführender is Model.Held)
            {
                Model.Held h = Ausführender as Model.Held;
                if (h.HatSonderfertigkeitUndVoraussetzungen("Klingentänzer"))
                    malus = (int)Math.Round(malus / 2.0, MidpointRounding.AwayFromZero);
            }
            //TODO: Das gleiche auch für Gegner in Form einer Kampfregel?
            if (malus > 0)
            {
                //TODO JT: Klasse MisslungenModifikator anlegen
                //Ausführender.Modifikatoren.Add(new MisslungenModifikator(malus));
            }
            OnAktion();
        }

        //sollte man sowas als Event machen?
        protected virtual void OnAktion()
        {
   
        }

        public delegate void OnAusführungEventHandler(object sender);
        public event OnAusführungEventHandler OnAusführung;
        protected void RaiseOnAusführung()
        {
            if (OnAusführung != null)
                OnAusführung(this);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
