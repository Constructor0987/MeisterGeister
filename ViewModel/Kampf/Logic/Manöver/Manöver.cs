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
using ImpromptuInterface;
using System.Collections.ObjectModel;
using MeisterGeister.ViewModel.Base;
using System.Runtime.CompilerServices;

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
        public String Name
        {
            get; protected set;
        }

        public String Literatur
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

        public abstract IEnumerable<KämpferInfo> Ziele
        {
            get;
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
            set
            {
                dauer = value;
                OnChanged("Dauer");
            }
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

        public int Erschwernis
        {
            get { return Ansage + Grunderschwernis; }
        }

        protected abstract IEnumerable<Probe> ProbenAnlegen();

        private List<Probe> proben = null;
        protected List<Probe> Proben
        {
            get
            {
                if (proben == null)
                {
                    proben = new List<Probe>();
                    proben.AddRange(ProbenAnlegen());
                }
                return proben;
            }
        }

        //private bool ergebnisseAkzeptiert = false;
        ///// <summary>
        ///// Akzeptiert die eingetragenen ProbenErgebnisse.
        ///// Wenn nicht genug Ergebnisse eingetragen wurden, dann werden diese zufällig bestimmt.
        ///// </summary>
        //public bool ErgebnisseAkzeptiert
        //{
        //    get { return ergebnisseAkzeptiert; }
        //    set
        //    {
        //        ergebnisseAkzeptiert = value;
        //        for (int i = 0; i < ProbenAnzahl; i++)
        //        {
        //            if (ProbenErgebnisse[i].Ergebnis == ErgebnisTyp.KEIN_ERGEBNIS)
        //                ProbenErgebnisse[i] = Proben[i].Würfeln();
        //        }
        //        OnChanged("ErgebnisseAkzeptiert");
        //    }
        //}

        //private bool auswirkungenAngewendet = false;
        //public bool AuswirkungenAngewendet
        //{
        //    get { return auswirkungenAngewendet; }
        //    set
        //    {
        //        auswirkungenAngewendet = value;
        //        OnChanged("AuswirkungenAngewendet");
        //    }
        //}

        /// <summary>
        /// Zieht von der Ausführungsdauer eine Aktion ab und kümmert sich bei Bedarf darum dass z.B. Proben gewürfelt oder die Aktion ausgeführt wird
        /// </summary>
        public void Aktion()
        {
            VerbleibendeDauer--;
            if (VerbleibendeDauer <= 0)
                Ausführen();
        }

        /// <summary>
        /// Ausführen einer Aktion des Manövers.
        /// Verbraucht Aktion(en) des Kämpfers.
        /// </summary>
        /// <returns></returns>
        public List<Probe> Ausführen()
        {
            //AktionenVerbrauchen();
            VerbleibendeDauer = 0;
            OnAusführung();
            if (VerbleibendeDauer > 0) //TODO Inkorrekt. Ich glaube die Probe wird am Anfang ausgeführt.
                return null;
            return Proben;
        }

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

        //eine weitere methode für den erfolg
        /// <summary>
        /// Die Auswirkungen des Manövers auf das Ziel anwenden.
        /// </summary>
        /// <param name="ziel"></param>
        protected abstract void Erfolg(IKämpfer ziel);

        protected virtual void GlücklicherErfolg(IKämpfer ziel)
        {
            Erfolg(ziel);
        }

        protected virtual void KritischerErfolg(IKämpfer ziel)
        {
            GlücklicherErfolg(ziel);
        }

        //und eine für den misserfolg
        /// <summary>
        /// Legt den MisslungenModifikator auf dem Ausführenden an.
        /// </summary>
        protected virtual void Misserfolg()
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

        protected virtual void Patzer()
        {
            Misserfolg();
        }

        protected virtual void FatalerPatzer()
        {
            Patzer();
        }

        public event EventHandler<ManöverEventArgs> Ausführung;
        protected void OnAusführung()
        {
            EventHandler<ManöverEventArgs> handler = Ausführung;
            if (handler != null)
            {
                ManöverEventArgs args = new ManöverEventArgs(Proben);
                handler(this, args);
                if (!args.Abgebrochen)
                    ProbenAuswerten();
            }
        }

        protected virtual void ProbenAuswerten()
        {
            foreach (Probe p in Proben)
                ProbeAuswerten(p);
        }

        protected virtual void ProbeAuswerten(Probe p)
        {
            switch (p.Ergebnis.Ergebnis)
            {
                case ErgebnisTyp.GELUNGEN:
                    foreach (KämpferInfo info in Ziele)
                        Erfolg(info.Kämpfer);
                    break;
                case ErgebnisTyp.GLÜCKLICH:
                    foreach (KämpferInfo info in Ziele)
                        GlücklicherErfolg(info.Kämpfer);
                    break;
                case ErgebnisTyp.MEISTERHAFT:
                    foreach (KämpferInfo info in Ziele)
                        KritischerErfolg(info.Kämpfer);
                    break;

                case ErgebnisTyp.MISSLUNGEN:
                    Misserfolg();
                    break;
                case ErgebnisTyp.PATZER:
                    Patzer();
                    break;
                case ErgebnisTyp.FATALER_PATZER:
                    FatalerPatzer();
                    break;
            }
        }
    }

    public interface IManöver<out TWaffe> where TWaffe : IWaffe
    {

    }

    public abstract class Manöver<TWaffe> : Manöver, IManöver<TWaffe> where TWaffe : IWaffe
    {
        protected Manöver(KämpferInfo ausführender)
            : this(ausführender, new Dictionary<TWaffe, KämpferInfo>(1), 1)
        {
        }

        protected Manöver(KämpferInfo ausführender, int dauer)
            : this(ausführender, new Dictionary<TWaffe, KämpferInfo>(1), dauer)
        {

        }

        protected Manöver(KämpferInfo ausführender, TWaffe waffe, KämpferInfo ziel)
            : this(ausführender, new Dictionary<TWaffe, KämpferInfo>() { { waffe, ziel } }, 1)
        {
        }

        protected Manöver(KämpferInfo ausführender, TWaffe waffe, KämpferInfo ziel, int dauer)
            : this(ausführender, new Dictionary<TWaffe, KämpferInfo>() { { waffe, ziel } }, dauer)
        {
        }

        protected Manöver(KämpferInfo ausführender, IDictionary<TWaffe, KämpferInfo> waffe_ziel)
            : this(ausführender, waffe_ziel, 1)
        {
        }

        protected Manöver(KämpferInfo ausführender, IDictionary<TWaffe, KämpferInfo> waffe_ziel, int dauer)
            : base(ausführender)
        {
            WaffeZiel = waffe_ziel;
            Dauer = VerbleibendeDauer = dauer;
            Init();
            InitMods();
        }

        protected virtual void Init()
        {
            Name = "Manöver";
            Literatur = "WdS 59";
            Ansage = 0;
        }

        protected Dictionary<string, ManöverModifikator<TWaffe>> mods;
        private ReadOnlyDictionary<string, ManöverModifikator<TWaffe>> readonlymods;

        protected virtual void InitMods()
        {
            mods = new Dictionary<string, ManöverModifikator<TWaffe>>();
            readonlymods = new ReadOnlyDictionary<string, ManöverModifikator<TWaffe>>(mods);
        }

        public ReadOnlyDictionary<string, ManöverModifikator<TWaffe>> Mods
        {
            get { return readonlymods; }
        }


        public virtual IDictionary<TWaffe, KämpferInfo> WaffeZiel
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
    }

    public class ManöverEventArgs : EventArgs
    {
        public ManöverEventArgs(List<Probe> proben)
        {
            Proben = proben;
        }

        public bool Abgebrochen { get; set; }
        public List<Probe> Proben { get; private set; }
    }
}
