using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Base;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

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

        /// <summary>
        /// Wird gefeuert während das Manöver ausgeführt wird
        /// </summary>
        public event EventHandler Ausführung;

        /// <summary>
        /// Wird gefeuert nachdem das Manöver gewürfelt wurde Zum Zeitpunkt dieses Events wurden noch
        /// keine Auswirkungen auf
        /// </summary>
        public event EventHandler<ManöverEventArgs> Ausgeführt;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public string Literatur
        {
            get; protected set;
        }

        public ManöverTyp Typ
        {
            get; protected set;
        }

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

        /// <summary>
        /// Bei Reaktionen stehen hier die Manöver, die pariert werden
        /// </summary>
        public IEnumerable<Manöver> InitialManöver
        {
            get { return _initialManöver; }

            set
            {
                foreach (Manöver m in _initialManöver)
                {
                    unregisterInitialManöver(m);
                }
                _initialManöver = value;
                foreach (Manöver m in _initialManöver)
                {
                    registerInitialManöver(m);
                }
            }
        }

        public int Grunderschwernis
        {
            get; protected set;
        }

        public int Ansage
        {
            get; protected set;
        }

        public virtual int Dauer
        {
            get { return _dauer; }
            set { Set(ref _dauer, value); }
        }

        /// <summary>
        /// Die Restdauer der Aktion in Aktionen.
        /// </summary>
        public int VerbleibendeDauer
        {
            get { return _verbleibendeDauer; }

            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (value == _verbleibendeDauer)
                {
                    return;
                }

                _verbleibendeDauer = value;
                if (value == 0 && Ausführender != null)
                {
                    Ausführender.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitAktion); //TODO das gilt zB nicht für freie aktionen.
                }

                OnChanged(nameof(VerbleibendeDauer));
            }
        }

        /// <summary>
        /// Gibt an ob dieses Manöver schon ausgeführt wurde.
        /// </summary>
        public bool IsAusgeführt
        {
            get { return _isAusgeführt; }

            set
            {
                if (_isAusgeführt == value)
                {
                    return;
                }

                _isAusgeführt = value;
                if (_isAusgeführt && MeisterGeister.Logic.Einstellung.Einstellungen.AngriffAutomatischWürfeln)
                {
                    AktionAusführen();
                }

                OnChanged(nameof(IsAusgeführt));
            }
        }

        public int Erschwernis
        {
            get { return Ansage + Grunderschwernis; }
        }

        /// <summary> Die Anzahl der Angriffsaktionen, die verbraucht werden. </suAusführender
        public int Angriffsaktionen
        {
            get; protected set;
        }

        /// <summary> Die Anzahl der Abwehraktionen, die verbraucht werden. </suAusführender
        public int Abwehraktionen
        {
            get; protected set;
        }

        /// <summary> Die Anzahl der freien Aktionen, die verbraucht werden. </suAusführender
        public int FreieAktionen
        {
            get; protected set;
        }

        /// <summary>
        /// Zieht von der Ausführungsdauer eine Aktion ab und kümmert sich bei Bedarf darum dass z.B.
        /// Proben gewürfelt oder die Aktion ausgeführt wird
        /// </summary>
        public void Aktion()
        {
            VerbleibendeDauer--;
            if (VerbleibendeDauer <= 0)
            {
                IsAusgeführt = true;
            }
        }

        /// <summary>
        /// Führt die Aktion aus. Wird in der letzen Aktion des Manövers aufgerufen
        /// </summary>
        /// <returns></returns>
        public virtual void AktionAusführen()
        {
            VerbleibendeDauer = 0;
            OnAusführung();
            IsAusgeführt = true;
        }

        /// <summary>
        /// Führt die Reaktion aus. Wird im Zuge der Ausführung des InitialManövers aufgerufen und
        /// kann dieses Manöver abbrechen (z.B. Parade pariert Angriff)
        /// </summary>
        public virtual void ReaktionAusführen(ManöverEventArgs e)
        {
            OnAusführung();
            IsAusgeführt = true;
        }

        protected virtual void registerInitialManöver(Manöver initialManöver)
        {
            initialManöver.Ausgeführt += InitialManöver_Ausgeführt;
        }

        protected virtual void unregisterInitialManöver(Manöver initialManöver)
        {
            initialManöver.Ausgeführt -= InitialManöver_Ausgeführt;
        }

        protected bool OnAusgeführt(Probe probe)
        {
            var e = new ManöverEventArgs(probe);
            Ausgeführt?.Invoke(this, e);
            return e.Abgebrochen;
        }

        private string _name;

        private IEnumerable<Manöver> _initialManöver;
        private int _dauer = 1;

        private int _verbleibendeDauer = 1;

        private bool _isAusgeführt = false;

        private void InitialManöver_Ausgeführt(object sender, ManöverEventArgs e)
        {
            ReaktionAusführen(e);
        }

        private void OnAusführung()
        {
            Ausführung?.Invoke(this, EventArgs.Empty);
        }
    }

    public abstract class Manöver<TWaffe> : Manöver where TWaffe : IWaffe
    {
        public ReadOnlyDictionary<string, ManöverModifikator<TWaffe>> Mods
        {
            get { return readonlymods; }
        }

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

        public bool InitPase { get; set; } = false;


        public IEnumerable<TWaffe> Waffen
        {
            get
            {
                return WaffeZiel.Keys.Distinct();
            }
        }

        protected Dictionary<string, ManöverModifikator<TWaffe>> mods;

        protected Manöver(KämpferInfo ausführender) : this(ausführender, 1)
        {
        }

        protected Manöver(KämpferInfo ausführender, int dauer) : base(ausführender)
        {            
            InitPase = true;
            Dauer = VerbleibendeDauer = dauer;
            Init();
            InitMods(Global.CurrentKampf.BodenplanViewModel.miWaffeSelected);
            SetDefaultModValues();
            InitPase = false;
        }

        protected virtual void Init()
        {
            WaffeZiel = new Dictionary<TWaffe, KämpferInfo>();
            Name = "Manöver";
            Literatur = "WdS 59";
            Ansage = Grunderschwernis = 0;
        }

        protected virtual void InitMods(IWaffe waffe)
        {
            mods = new Dictionary<string, ManöverModifikator<TWaffe>>();            
            readonlymods = new ReadOnlyDictionary<string, ManöverModifikator<TWaffe>>(mods);
        }

        protected virtual void SetDefaultModValues()
        {
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
            var malus = Erschwernis;
            // Klingentänzer haben nur die halbe Ansage als Malus
            if (Ausführender.Kämpfer is Model.Held)
            {
                var h = Ausführender.Kämpfer as Model.Held;
                if (h.HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Klingentänzer))
                {
                    malus = (int)Math.Round(malus / 2.0, MidpointRounding.AwayFromZero);
                }
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
        /// <param name="e_init">
        /// Die ManöverEventArgs des Initialmanövers. Kann benutzt werden um das InitialManöver
        /// abzubrechen o.Ä.
        /// </param>
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

        private ReadOnlyDictionary<string, ManöverModifikator<TWaffe>> readonlymods;
        private int _getGesamt;
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
}
