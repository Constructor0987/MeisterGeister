using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;

using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Manöver
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
            : this(ausführender, new Dictionary<IWaffe, IKämpfer>(1))
        {
        }

        protected Manöver(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : this(ausführender, new Dictionary<IWaffe, IKämpfer>() { { waffe, ziel } })
        {
        }

        protected Manöver(IKämpfer ausführender, IDictionary<IWaffe, IKämpfer> waffe_ziel)
        {
            Ansage = 0;
            Ausführender = ausführender;
            WaffeZiel = waffe_ziel;
        }

        //felder/parameter
        public virtual String Name
        {
            get { return "Manöver"; }
        }

        /// <summary>
        /// Dauer des Manövers
        /// </summary>
        /// hiermit bin ich noch sehr unzufrieden. wie mache ich denn sowas wie ALLE aktionen und Reaktionen oder freie aktionen
        public virtual int Dauer
        {
            get { return 1; }
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

        public virtual int Erschwernis
        {
            get { return Ansage + Grunderschwernis; }
        }

        //TODO: muss eine Probe für das UI anbieten
        //public virtual Probe Probe ...

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
            if (Dauer >= 1) //.Wert > 0 && Dauer.Einheit != Zeiteinheit.FreieAktion && Dauer.Einheit != Zeiteinheit.Keine)
                Ausführender.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitAktion);
        }
    }
}
