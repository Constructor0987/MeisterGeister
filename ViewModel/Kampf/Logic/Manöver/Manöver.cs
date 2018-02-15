using System;
using System.Collections.Generic;
using System.Linq;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.General;
using System.Collections.ObjectModel;
using MeisterGeister.ViewModel.Base;
using MeisterGeister.View.General;
using MeisterGeister.Model;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    //TODO JT: überarbeiten
    //Probe für ein Manöver wird am Anfang des Manövers ausgeführt.
    //Wenn die VerbleibendeDauer auf 0 geht wird es angewandt.
    public abstract class Manöver : ViewModelBase
    {
        public Manöver(KämpferInfo ausführender)
        {
            Ausführender = ausführender;
        }

        //felder/parameter
        private string name;
        public String Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        public String Literatur
        {
            get; protected set;
        }

        public ManöverTyp Typ
        {
            get; protected set;
        }

        /*
         * ausführender, ziele, variable erschwernis (aufgeteilt in ansage und grunderschwernis,
         * so dass man den aufschlag bei misslingen für die nächste aktion erstellen kann)
         */
        public KämpferInfo Ausführender
        {
            get;
            private set;
        }

        /// <summary>
        /// Bei Aktionen stehen hier die Ziele des Manövers (meißt nur einer)
        /// </summary>
        public abstract IEnumerable<KämpferInfo> Ziele
        {
            get;
        }

        private IEnumerable<Manöver> initialManöver;
        /// <summary>
        /// Bei Reaktionen stehen hier die Manöver, die pariert werden
        /// </summary>
        public IEnumerable<Manöver> InitialManöver
        {
            get { return initialManöver; }
            set
            {
                foreach(Manöver m in initialManöver)
                {
                    unregisterInitialManöver(m);
                }
                initialManöver = value;
                foreach(Manöver m in initialManöver)
                {
                    registerInitialManöver(m);
                }
            }
        }

        protected virtual void registerInitialManöver(Manöver initialManöver)
        {
            initialManöver.Ausgeführt += InitialManöver_Ausgeführt;
        }

        protected virtual void unregisterInitialManöver(Manöver initialManöver)
        {
            initialManöver.Ausgeführt -= InitialManöver_Ausgeführt;
        }

        private void InitialManöver_Ausgeführt(object sender, ManöverEventArgs e)
        {
            ReaktionAusführen(e);
        }

        public int Grunderschwernis
        {
            get; protected set;
        }

        public int Ansage
        {
            get; protected set;
        }

        private int dauer = 1;
        public virtual int Dauer
        {
            get { return dauer; }
            set { Set(ref dauer, value); }
        }

        private int verbleibendeDauer = 1;
        /// <summary>
        /// Die Restdauer der Aktion in Aktionen.
        /// </summary>
        public int VerbleibendeDauer
        {
            get { return verbleibendeDauer; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value == verbleibendeDauer)
                    return;
                verbleibendeDauer = value;
                if (value == 0 && Ausführender != null)
                    Ausführender.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitAktion); //TODO das gilt zB nicht für freie aktionen.
                OnChanged("VerbleibendeDauer");
            }
        }

        private bool isAusgeführt = false;
        /// <summary>
        /// Gibt an ob dieses Manöver schon ausgeführt wurde.
        /// </summary>
        public bool IsAusgeführt
        {
            get { return isAusgeführt; }
            set
            {
                if (isAusgeführt == value)
                    return;
                isAusgeführt = value;
                if (isAusgeführt)
                    AktionAusführen();
                OnChanged("IsAusgeführt");
            }
        }

        public int Erschwernis
        {
            get { return Ansage + Grunderschwernis; }
        }

        /// <summary>
        /// Zieht von der Ausführungsdauer eine Aktion ab und kümmert sich bei Bedarf darum dass z.B. Proben gewürfelt oder die Aktion ausgeführt wird
        /// </summary>
        public void Aktion()
        {
            VerbleibendeDauer--;
            if (VerbleibendeDauer <= 0)
                IsAusgeführt = true;
        }

        /// <summary>
        /// Führt die Aktion aus. Wird in der letzen Aktion des Manövers aufgerufen
        /// </summary>
        /// <returns></returns>
        public virtual void AktionAusführen()
        {
            //AktionenVerbrauchen();
            VerbleibendeDauer = 0;
            OnAusführung();
            IsAusgeführt = true;
            
        }

        /// <summary>
        /// Führt die Reaktion aus. Wird im Zuge der Ausführung des InitialManövers aufgerufen und kann dieses Manöver abbrechen (z.B. Parade pariert Angriff)
        /// </summary>
        public virtual void ReaktionAusführen(ManöverEventArgs e)
        {
            OnAusführung();
            IsAusgeführt = true;
        }

        private void OnAusführung()
        {
            EventHandler handler = Ausführung;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected bool OnAusgeführt(Probe probe)
        {
            ManöverEventArgs e = new ManöverEventArgs(probe);
            EventHandler<ManöverEventArgs> handler = Ausgeführt;
            if (handler != null)
            {
                handler(this, e);
            }
            return e.Abgebrochen;
        }

        /// <summary>
        /// Wird gefeuert während das Manöver ausgeführt wird
        /// </summary>
        public event EventHandler Ausführung;
        /// <summary>
        /// Wird gefeuert nachdem das Manöver gewürfelt wurde
        /// Zum Zeitpunkt dieses Events wurden noch keine Auswirkungen auf 
        /// </summary>
        public event EventHandler<ManöverEventArgs> Ausgeführt;

        /// <summary>
        /// Die Anzahl der Angriffsaktionen, die verbraucht werden.
        /// </suAusführender
        public int Angriffsaktionen
        {
            get; protected set;
        }

        /// <summary>
        /// Die Anzahl der Abwehraktionen, die verbraucht werden.
        /// </suAusführender
        public int Abwehraktionen
        {
            get; protected set;
        }

        /// <summary>
        /// Die Anzahl der freien Aktionen, die verbraucht werden.
        /// </suAusführender
        public int FreieAktionen
        {
            get; protected set;
        }

        //protected virtual void AktionenVerbrauchen()
        //{
        //    if (Ausführender == null)
        //        return;
        //    Ausführender.VerbrauchteAngriffsaktionen += Angriffsaktionen;
        //    Ausführender.VerbrauchteAbwehraktionen += Abwehraktionen;
        //    Ausführender.VerbrauchteFreieAktionen += FreieAktionen;
        //}
    }

    public abstract class Manöver<TWaffe> : Manöver where TWaffe : IWaffe
    {
        protected Manöver(KämpferInfo ausführender) : this(ausführender, 1)
        {
        }

        protected Manöver(KämpferInfo ausführender, int dauer) : base(ausführender)
        {
            Dauer = VerbleibendeDauer = dauer;
            Init();
            InitMods();
            SetDefaultModValues();
        }

        protected virtual void Init()
        {
            WaffeZiel = new Dictionary<TWaffe, KämpferInfo>();
            Name = "Manöver";
            Literatur = "WdS 59";
            Ansage = Grunderschwernis = 0;
        }

        protected Dictionary<string, ManöverModifikator<TWaffe>> mods;
        private ReadOnlyDictionary<string, ManöverModifikator<TWaffe>> readonlymods;

        protected virtual void InitMods()
        {
            mods = new Dictionary<string, ManöverModifikator<TWaffe>>();
            readonlymods = new ReadOnlyDictionary<string, ManöverModifikator<TWaffe>>(mods);
        }

        protected virtual void SetDefaultModValues()
        {

        }

        public ReadOnlyDictionary<string, ManöverModifikator<TWaffe>> Mods
        {
            get { return readonlymods; }
        }

        private int _getGesamt;
        public int GetGesamt
        {
            get { return _getGesamt; }
            set { Set(ref _getGesamt, value); }
        }        

        public IDictionary<TWaffe, KämpferInfo> WaffeZiel
        {
            get;
            private set;
        }

        public override IEnumerable<KämpferInfo> Ziele
        {
            get
            {
                return WaffeZiel.Values.Distinct();
            }
        }

        public IEnumerable<TWaffe> Waffen
        {
            get
            {
                return WaffeZiel.Keys.Distinct();
            }
        }

        /// <summary>
        /// Die Auswirkungen des Manövers auf das Ziel anwenden.
        /// </summary>
        /// <param name="ziel"></param>
        protected abstract void Erfolg(Probe p, KämpferInfo ziel, TWaffe waffe, ManöverEventArgs e_init);

        protected virtual void GlücklicherErfolg(Probe p, KämpferInfo ziel, TWaffe waffe, ManöverEventArgs e_init)
        {
            Erfolg(p, ziel, waffe, e_init);
        }

        protected virtual void KritischerErfolg(Probe p, KämpferInfo ziel, TWaffe waffe, ManöverEventArgs e_init)
        {
            GlücklicherErfolg(p, ziel, waffe, e_init);
        }

        //und eine für den misserfolg
        /// <summary>
        /// Legt den MisslungenModifikator auf dem Ausführenden an.
        /// </summary>
        protected virtual void Misserfolg(Probe p, KämpferInfo ziel, TWaffe waffe, ManöverEventArgs e_init)
        {
            // Erschwernis als Malus bis zur nächsten Aktion.
            int malus = Erschwernis;
            // Klingentänzer haben nur die halbe Ansage als Malus
            if (Ausführender.Kämpfer is Model.Held)
            {
                Model.Held h = Ausführender.Kämpfer as Model.Held;
                if (h.HatSonderfertigkeitUndVoraussetzungen("Klingentänzer"))
                    malus = (int)Math.Round(malus / 2.0, MidpointRounding.AwayFromZero);
            }
            //TODO: Das gleiche auch für Gegner in Form einer Kampfregel?
            if (malus > 0)
            {
                //TODO JT: Klasse MisslungenModifikator anlegen
                //Ausführender.Kämpfer.Modifikatoren.Add(new MisslungenModifikator(malus));
            }
        }

        protected virtual void Patzer(Probe p, KämpferInfo ziel, TWaffe waffe, ManöverEventArgs e_init)
        {
            Misserfolg(p, ziel, waffe, e_init);
        }

        protected virtual void FatalerPatzer(Probe p, KämpferInfo ziel, TWaffe waffe, ManöverEventArgs e_init)
        {
            Patzer(p, ziel, waffe, e_init);
        }

        /// <summary>
        /// Wertet die Probe aus, die im Zuge des Manövers geworfen wurde
        /// </summary>
        /// <param name="p">Die Probe, die beim Manöver geworfen wurde</param>
        /// <param name="ziel">Das Ziel des Manövers</param>
        /// <param name="waffe">Die Waffe mit der das Manöver ausgeführt wird</param>
        /// <param name="e_init">Die ManöverEventArgs des Initialmanövers. Kann benutzt werden um das InitialManöver abzubrechen o.Ä.</param>
        protected void ProbeAuswerten(Probe p, KämpferInfo ziel, TWaffe waffe, ManöverEventArgs e_init)
        {
            switch (p.Ergebnis.Ergebnis)
            {
                case ErgebnisTyp.GELUNGEN:
                    Erfolg(p, ziel, waffe, e_init);
                    break;
                case ErgebnisTyp.GLÜCKLICH:
                    GlücklicherErfolg(p, ziel, waffe, e_init);
                    break;
                case ErgebnisTyp.MEISTERHAFT:
                    KritischerErfolg(p, ziel, waffe, e_init);
                    break;

                case ErgebnisTyp.MISSLUNGEN:
                    Misserfolg(p, ziel, waffe, e_init);
                    break;
                case ErgebnisTyp.PATZER:
                    Patzer(p, ziel, waffe, e_init);
                    break;
                case ErgebnisTyp.FATALER_PATZER:
                    FatalerPatzer(p, ziel, waffe, e_init);
                    break;
            }
        }
    }

    public class ManöverEventArgs : EventArgs
    {
        public ManöverEventArgs(Probe probe)
        {
            Probe = probe;
        }

        public bool Abgebrochen { get; set; }
        public Probe Probe { get; private set; }
    }

    public enum ManöverTyp
    {
        Aktion,
        Reaktion
    }
}
