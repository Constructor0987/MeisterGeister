using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MeisterGeister.View.Bodenplan;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class Kampf : IDisposable, INotifyPropertyChanged
    {
        /*
         * Die Klasse hält und kontrolliert alle globalen Kampfinformationen und -Aktionen
         * neue KR(), nächster Kämpfer(), ...
         * aktueller Kämpfer, aktuelle KR, aktuelle INI ...
         * 
         * Liste der angesagten Aktionen pro KR, Liste der ausgeführten Aktionen pro KR
         * 
         * Eine Liste der Kämpfer
         * Zur Unterstützung einer beliebigen Anzahl an Parteien gehört zu jedem Teilnehmer eine Teamnummer
         * 
         * Soll die Initiative im Kämpfer oder im Kampf gehalten werden?
         * Im Kampf, so könnte man es mit abspeichern.
         * 
         * Automatische Erkennung des Kampfendes anhand der Teams und des Zustandes der Kämpfer (kampfunfähig, geflohen, tot, bewusstlos)
         */
        public Kampf()
        {
            Kämpfer = new KämpferInfoListe(this);
            Kämpfer.CollectionChanged += Kämpfer_CollectionChanged;
            InitiativListe = new InitiativListe(this);
            InitiativListe.CollectionChanged += InitiativListe_CollectionChanged;
            //INIPhase = 0;
            Kampfrunde = 0;
        }

        //public Arena.Arena Bodenplan { get; set; }
        public BattlegroundWindow Bodenplan { get; set; }

        private ObservableCollection<string> _kampfLog = new ObservableCollection<string>();
        public ObservableCollection<string> KampfLog
        {
            get { return _kampfLog; }
        }

        private InitiativListe _initiativListe;
        public InitiativListe InitiativListe
        {
            get { return _initiativListe; }
            private set { _initiativListe = value; }
        }

        private KämpferInfoListe _kämpfer;
        public KämpferInfoListe Kämpfer
        {
            get { return _kämpfer; }
            private set { _kämpfer = value; }
        }

        public int INIPhase
        {
            get { return AktuelleAktion == null ? 0 : (int)AktuelleAktion.Start.InitiativPhase; }
        }

        private int _kampfrunde;
        public int Kampfrunde
        {
            get { return _kampfrunde; }
            private set
            {
                _kampfrunde = value;
                Kampfzeit = new TimeSpan(0, 0, 3 * Math.Max(value - 1, 0));
                OnChanged("Kampfrunde");
            }
        }

        private TimeSpan _kampfzeit = new TimeSpan();
        public TimeSpan Kampfzeit
        {
            get { return _kampfzeit; }
            private set
            {
                _kampfzeit = value;
                OnChanged("Kampfzeit");
            }
        }

        private ZeitImKampf aktuelleAktionszeit;
        public ZeitImKampf AktuelleAktionszeit
        {
            get { return aktuelleAktionszeit; }
            private set
            {
                aktuelleAktionszeit = value;
                OnChanged("AktuelleAktionszeit");
                AktuelleAktion = InitiativListe.First(mi => mi.Aktionszeiten.Contains(value));
            }
        }


        private ManöverInfo aktuelleAktion;
        public ManöverInfo AktuelleAktion
        {
            get { return aktuelleAktion; }
            private set
            {
                aktuelleAktion = value;
                OnChanged("AktuelleAktion");
            }
        }

        /// <summary>
        /// Speichert einen Text im Kampf-Log.
        /// </summary>
        /// <param name="msg">Zu speichernder Log-Text.</param>
        public void Log(string msg)
        {
            KampfLog.Insert(0, string.Format("{0}.{1}: {2}", Kampfrunde, INIPhase, msg));
        }

        public ManöverInfo Next()
        {
            if (AktuelleAktion != null && !AktuelleAktion.Ausgeführt)
            {
                //Was muss in ManöverInfo und was ins Manöver?

                //Die aktuelle Aktion wurde noch nicht ausgeführt
                //es wurde kein Ergebnis zugewiesen.
                //if (!AktuelleAktion.Manöver.ErgebnisseAkzeptiert)
                //{
                //    //das eingetragene bzw. vorgeschlagene ProbenErgebnis anwenden
                //    AktuelleAktion.Manöver.ErgebnisseAkzeptiert = true;
                //}
                //else
                //{
                //    //Next darf nicht weitergehen, wenn die letzte Aktion des Manövers dran ist und die Auswirkungen noch nicht bestätigt wurden (neues feld im Manöver oder ManöverInfo?)
                //    //if(AktuelleAktion.Manöver.VerbleibendeDauer == 1)
                //}
                //aktion wurde in dieser Runde bearbeitet, aktionen werden verbraucht
                AktuelleAktion.Manöver.Aktion();
                //irgendwie müssen die Auswirkungen noch zum tragen kommen und bestätigt werden
                //AktuelleAktion.Manöver.AuswirkungenAkzeptiert

                //if (AktuelleAktion.Manöver != null)
                //{
                //    var probe = AktuelleAktion.Manöver.Ausführen(); //dies darf vielleicht keine Methode sein. Es sollte ein property sein, die gebunden werden kann.
                //    if (probe != null)
                //    {
                //        var pe = probe.Würfeln();
                //        //TODO JT: Probe anzeigen und Erfolg oder Misserfolg auswerten.
                //        //Das ist keine gute idee. Eine Probe und die Parade muss auch schon angezeigt werden, wenn das Manöver nur ausgewählt wird.
                //        //Hier muss noch mehr am Konzept gearbeitet werden.
                //    }
                //}
                //UmwandelnMöglich = false;
            }

            ZeitImKampf next;
            if (AktuelleAktion != null)
            {
                next = InitiativListe.SelectMany(mi => mi.Aktionszeiten).Where(zeit => zeit > AktuelleAktionszeit).OrderBy(zeit => zeit).First();
            }
            else
            {
                NeueKampfrunde();
                next = InitiativListe.SelectMany(mi => mi.Aktionszeiten).Where(zeit => zeit.Kampfrunde == Kampfrunde).OrderBy(zeit => zeit).First();
            }
            AktuelleAktionszeit = next;
            return AktuelleAktion;
        }

        public void NeueKampfrunde()
        {
            ////Alle Manöver, die noch nicht ausgeführt wurden
            //var nichtAusgeführt = InitiativListe.Where(mi => mi.Ausgeführt == false);
            ////TODO JT: entweder warnen
            //foreach (var mi in nichtAusgeführt)
            //{
            //    //oder einfach als ausgeführt setzen
            //    if (mi.Manöver != null)
            //    {
            //        var probe = mi.Manöver.Ausführen();
            //        //Siehe kommentar in next() ... das design ist mist.
            //        //Wenn die probe ignoriert wird passiert auch nichts weiter, ausser dass die Verbleibenden Aktionen sinken.
            //    }
            //    else
            //        mi.Ausgeführt = true;
            //}
            //Alte Ansagen löschen
            //InitiativListe.LöscheBeendeteManöver();

            Kampfrunde++;

            foreach (KämpferInfo ki in Kämpfer)
            {
                //Modifikatoren entfernen
                ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampfrunde);

                //sollte in KämpferInfo rein
                ki.VerbrauchteAbwehraktionen = 0;
                ki.VerbrauchteAngriffsaktionen = 0;
                ki.VerbrauchteFreieAktionen = 0;

                ki.AktionenBerechnen();
                ki.StandardAktionenSetzen(Kampfrunde);

                ki.VerbrauchteAbwehraktionen = 0;
                ki.VerbrauchteAngriffsaktionen = 0;
                ki.VerbrauchteFreieAktionen = 0;
                //Im UI sollten kämpfer ohne Ansage leicht an der Farbe erkennbar sein
                //Kämpfer mit Aufmerksamkeit oder Kampfgespür müssen nicht markiert werden (höchstens mit einer leichten tönung)
            }

            //if (InitiativListe.Count > 0)
            //    INIPhase = InitiativListe[0].InitiativeStart;
            //else
            //    INIPhase = 0;
            //im UI markieren, dass man nun bis zur ersten Aktions-Ansage umwandeln kann
            //UmwandelnMöglich = true;
            //eventuell in die Property?

            Log(string.Format("Kampfrunde {0} gestartet", Kampfrunde));
        }

        public void KampfNeuStarten()
        {
            KampfEnde();
            AktuelleAktionszeit = default(ZeitImKampf);
            AktuelleAktion = null;
            InitiativListe.Clear();
            Kampfrunde = 0;
            NeueKampfrunde(); // KR abschließen

            // INI neu ermitteln
            foreach (var kämpferInfo in Kämpfer)
            {
                if (kämpferInfo != null)
                    kämpferInfo.Initiative = kämpferInfo.Kämpfer.Initiative();
            }

        }

        private void KampfEnde()
        {
            foreach (var ki in Kämpfer)
                ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampf);
        }

        public void Orientieren(IKämpfer k)
        {
            Orientieren(Kämpfer[k]);
        }

        public void Orientieren(KämpferInfo ki)
        {
            ki.Initiative = ki.Kämpfer.InitiativeMax();
            ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitAktion);
        }

        public void Kämpfer_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Remove)
                InitiativListe.Remove((KämpferInfo)args.OldItems[0]);
            else if (args.Action == NotifyCollectionChangedAction.Add)
                ((KämpferInfo)args.NewItems[0]).StandardAktionenSetzen(Kampfrunde);
            //InitiativListe.Add((KämpferInfo)args.NewItems[0], new Manöver.KeineAktion(((KämpferInfo)args.NewItems[0]).Kämpfer), 0);
        }

        public void InitiativListe_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {

            /* ich glaube das taugt nichts, frei editierbar ist besser.
            if (args.Action == NotifyCollectionChangedAction.Add && !(args.NewItems[0] is Manöver.KeineAktion))
                UmwandelnMöglich = false;
             * */
        }

        /// <summary>
        /// Trefferpunkte auf einen Kämpfer. Kümmert sich um Wunden und TP(A).
        /// </summary>
        /// <param name="k">Kämpfer</param>
        /// <param name="tp">Trefferpunkte</param>
        /// <param name="zone">Trefferzone</param>
        /// <param name="verwundend">WS-2</param>
        /// <param name="alsSP">Rüstung wird ignoriert</param>
        /// <param name="alsTPA">als Ausdauerschaden</param>
        public void Trefferpunkte(IKämpfer k, int tp, Trefferzone zone = Trefferzone.Unlokalisiert, TrefferpunkteOptions optionen = TrefferpunkteOptions.Default)
        {
            if (Kämpfer[k] == null)
                return;

            if (zone == Trefferzone.Zufall)
                zone = TrefferzonenHelper.ZufallsZone();
            int rs = 0;
            if ((optionen & TrefferpunkteOptions.IgnoriertRüstung) != TrefferpunkteOptions.IgnoriertRüstung)
                rs = k.RS[zone];
            int spa = 0;
            int sp = Math.Max(tp - rs, 0);
            if ((optionen & TrefferpunkteOptions.Ausdauerschaden) == TrefferpunkteOptions.Ausdauerschaden)
            {
                spa = sp;
                if ((optionen & TrefferpunkteOptions.AusdauerschadenMachtKeineEchtenSchadenspunkte) == TrefferpunkteOptions.AusdauerschadenMachtKeineEchtenSchadenspunkte)
                    sp = 0;
                else
                    sp = (int)Math.Round(spa / 2.0, MidpointRounding.AwayFromZero);
            }
            k.LebensenergieAktuell -= sp;
            k.AusdauerAktuell -= spa;

            Log(string.Format("Treffer bei '{0}': {1} SP ({2} TP, RS {3}) in Zone {4}", k.Name, sp, tp, rs, zone));

            if ((optionen & TrefferpunkteOptions.KeineWunden) != TrefferpunkteOptions.KeineWunden)
            {
                int wsmod = 0 - ((optionen & TrefferpunkteOptions.VerringerteWundschwelle) == TrefferpunkteOptions.VerringerteWundschwelle ? 2 : 0) + ((optionen & TrefferpunkteOptions.Ausdauerschaden) == TrefferpunkteOptions.Ausdauerschaden ? 2 : 0);
                int wunden = 0;
                if (sp > k.Wundschwelle3 + wsmod)
                    wunden = 3;
                else if (sp > k.Wundschwelle2 + wsmod)
                    wunden = 2;
                else if (sp > k.Wundschwelle + wsmod)
                    wunden = 1;
                k.Wunden[zone] += wunden;

                if (wunden > 0)
                    Log(string.Format("Wunde(n) bei '{0}': {1} Wunde(n) in Zone {2}", k.Name, wunden, zone));
            }
        }

        public void Dispose()
        {
            //TODO ??: ich finde diese Lösung noch nicht optimal. Das geht schief, wenn man speichern und laden möchte.
            //Alle Gegenerinstanzen löschen
            foreach (var k in Kämpfer.Where(ki => ki.Kämpfer is Model.Gegner).Select(ki => ki.Kämpfer).ToList())
            {
                Kämpfer.Remove(k);
            }
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

    [Flags]
    public enum TrefferpunkteOptions
    {
        Default = 0x00,
        Ausdauerschaden = 0x01,
        IgnoriertRüstung = 0x02,
        VerringerteWundschwelle = 0x04,
        KeineWunden = 0x08,
        AusdauerschadenMachtKeineEchtenSchadenspunkte = 0x10
    }
}
