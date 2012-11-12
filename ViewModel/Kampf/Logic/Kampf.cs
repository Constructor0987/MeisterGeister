using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
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
            Kämpfer.PropertyChanged += Kämpfer_PropertyChanged;
            Kämpfer.CollectionChanged += Kämpfer_CollectionChanged;
            InitiativListe = new InitiativListe(this);
            InitiativListe.CollectionChanged += InitiativListe_CollectionChanged;
            INIPhase = 0;
            Kampfrunde = 0;
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

        private float _iniphase;
        public float INIPhase
        {
            get { return _iniphase; }
            set { _iniphase = value; OnChanged("INIPhase"); }
        }

        public delegate void NeueKampfrundeEventHandler(object sender, int kampfrunde);
        public event NeueKampfrundeEventHandler OnNeueKampfrunde;

        private int _kampfrunde;
        public int Kampfrunde
        {
            get { return _kampfrunde; }
            private set { 
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

        private ManöverInfo aktuelleAktion;
        public ManöverInfo AktuelleAktion
        {
            get { return aktuelleAktion; }
            set { 
                aktuelleAktion = value;
                OnChanged("AktuelleAktion");
                // NotifyChanged IsAktuell
                foreach (ManöverInfo mi in InitiativListe)
                {
                    mi.OnChanged("IsAktuell");
                    if (mi.KämpferInfo != null)
                        mi.KämpferInfo.OnChanged("IsAktuell");
                }

            }
        }

        public ManöverInfo Next()
        {
            if (Kampfrunde < 1)
                KampfBeginn();
            if (AktuelleAktion != null && !AktuelleAktion.Ausgeführt)
            {
                if (AktuelleAktion.Manöver != null)
                {
                    var probe = AktuelleAktion.Manöver.Ausführen();
                    if (probe != null)
                    {
                        //TODO JT: Probe anzeigen und Erfolg oder Misserfolg auswerten.
                        //Das ist keine gute idee. Eine Probe und die Parade muss auch schon angezeigt werden, wenn das Manöver nur ausgewählt wird.
                        //Hier muss noch mehr am Konzept gearbeitet werden.
                    }
                }
                UmwandelnMöglich = false;
            }

            foreach (ManöverInfo mi in InitiativListe)
            {
                if (!(mi.Manöver is Manöver.KeineAktion) && !mi.Ausgeführt)
                {
                    AktuelleAktion = mi;
                    INIPhase = mi.Initiative;
                    return mi;
                }
            }
            NeueKampfrunde();
            return null;
        }

        public void NeueKampfrunde()
        {
            //Alle Manöver, die noch nicht ausgeführt wurden
            var nichtAusgeführt = InitiativListe.Where(mi => mi.Ausgeführt == false);
            //TODO JT: entweder warnen
            foreach (var mi in nichtAusgeführt)
            {
                //oder einfach als ausgeführt setzen
                if (mi.Manöver != null)
                {
                    var probe = mi.Manöver.Ausführen();
                    //Siehe kommentar in next() ... das design ist mist.
                    //Wenn die probe ignoriert wird passiert auch nichts weiter, auss dass die Verbleibenden Aktionen sinken.
                }
                else
                    mi.Ausgeführt = true;
            }
            //Alte Ansagen löschen
            InitiativListe.LöscheBeendeteManöver();
            //Modifikatoren entfernen
            foreach (KämpferInfo ki in Kämpfer)
            {
                ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampfrunde);
                StandardAktionenSetzen(ki);
                ki.VerbrauchteAbwehraktionen = 0;
                ki.VerbrauchteAngriffsaktionen = 0;
                ki.VerbrauchteFreieAktionen = 0;
                //Im UI sollten kämpfer ohne Ansage leicht an der Farbe erkennbar sein
                //Kämpfer mit Aufmerksamkeit oder Kampfgespür müssen nicht markiert werden (höchstens mit einer leichten tönung)
            }
            Kampfrunde++;
            if (InitiativListe.Count > 0)
                INIPhase = InitiativListe[0].Initiative;
            else
                INIPhase = 0;
            //im UI markieren, dass man nun bis zur ersten Aktions-Ansage umwandeln kann
            UmwandelnMöglich = true;
            //eventuell in die Property?
            if (OnNeueKampfrunde != null)
                OnNeueKampfrunde(this, _kampfrunde);
        }

        bool _umwandelnMöglich = true;
        public bool UmwandelnMöglich
        {
            get { return _umwandelnMöglich; }
            private set { _umwandelnMöglich = value; OnChanged("UmwandelnMöglich"); }
        }

        public void KampfNeuStarten()
        {
            NeueKampfrunde(); // KR abschließen
            KampfEnde();

            // INI neu ermitteln
            foreach (var kämpferInfo in Kämpfer)
            {
                if (kämpferInfo != null)
                    kämpferInfo.Initiative = kämpferInfo.Kämpfer.Initiative();
            }
            Kampfrunde = 0;
        }

        public void KampfBeginn()
        {
            //TODO JT: Globales Datum holen und mit Uhrzeit in Kampfbeginn abspeichern.
            //TODO: Die Kampfzeit wird für neue Modifikatoren in Erstellt abgelegt.
            Kampfrunde = 1;
            UmwandelnMöglich = true;
            if (OnNeueKampfrunde != null)
                OnNeueKampfrunde(this, _kampfrunde);
        }

        public void KampfEnde()
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

        public void Kämpfer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Sort")
            {
                //INI Liste sortieren
                InitiativListe.Sort();
            }
            else if (args.PropertyName == "Angriffsaktionen")
            {
                KämpferInfo ki = null;
                if (sender is KämpferInfo)
                    ki = sender as KämpferInfo;
                if(ki != null)
                    StandardAktionenSetzen(ki);
            }
        }

        public void Kämpfer_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Remove)
                InitiativListe.Remove((KämpferInfo)args.OldItems[0]);
            else if (args.Action == NotifyCollectionChangedAction.Add)
                StandardAktionenSetzen((KämpferInfo)args.NewItems[0]);
                //InitiativListe.Add((KämpferInfo)args.NewItems[0], new Manöver.KeineAktion(((KämpferInfo)args.NewItems[0]).Kämpfer), 0);
        }

        public void InitiativListe_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {

            /* ich glaube das taugt nichts, frei editierbar ist besser.
            if (args.Action == NotifyCollectionChangedAction.Add && !(args.NewItems[0] is Manöver.KeineAktion))
                UmwandelnMöglich = false;
             * */
        }

        private void StandardAktionenSetzen()
        {
            foreach (KämpferInfo ki in Kämpfer)
            {
                StandardAktionenSetzen(ki);
            }
        }

        private void StandardAktionenSetzen(KämpferInfo ki)
        {
            //löschen von Manövern, für die der falsche Kampfstil gewählt ist.
            if (ki.Kampfstil != Kampfstil.BeidhändigerKampf) //oder mehrhändig
            {
                foreach (var mi in InitiativListe.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion).ToList())
                    InitiativListe.Remove(mi);
            }
            if (ki.Kampfstil != Kampfstil.Parierwaffenstil)
            {
                foreach (var mi in InitiativListe.Where(mi => mi.Manöver is Manöver.TodVonLinks).ToList())
                    InitiativListe.Remove(mi);
            }
            var geplanteAktionen = InitiativListe.Where(mi => mi.KämpferInfo == ki && mi.IsAktion).ToList().OrderBy(mi => mi.Initiative).ToList();
            while (geplanteAktionen.Count >= 1 && geplanteAktionen.Count > ki.Angriffsaktionen)
            {
                var manöver = geplanteAktionen.FirstOrDefault();
                //Das letzte Manöver wird in KeineAktion umgewandelt um den Kämpfer weiterhin in der Liste zu haben.
                if(ki.Angriffsaktionen == 0 && geplanteAktionen.Count==1)
                {
                    manöver.Manöver = new Manöver.KeineAktion(ki);
                    break;
                }
                //alle anderen Manöver, die zuviel sind, löschen.
                InitiativListe.Remove(manöver);
                geplanteAktionen.Remove(manöver);
            }
            //wenn die Liste ganz leer ist, füge KeineAktion hinzu
            if (ki.Angriffsaktionen == 0 && InitiativListe.Where(mi => mi.KämpferInfo == ki).Count() == 0)
                InitiativListe.Add(ki, new Manöver.KeineAktion(ki), 0);

            //TODO JT: die Formulierung der Bedingungen mehr an die Voraussetzungen von ZusätzlicheAngriffsaktion und TodVonLinks anpassen
            //sonst wird das Ergänzen bald recht schwierig
            int zusatzAktionen = geplanteAktionen.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion).Count();
            for (int i = geplanteAktionen.Count; i < ki.Angriffsaktionen; i++)
            {
                if(i==0)
                {
                    var m = InitiativListe.Where(mi => mi.KämpferInfo == ki && mi.Manöver is Manöver.KeineAktion).FirstOrDefault();
                    if (m == null)
                        InitiativListe.Add(ki, new Manöver.Attacke(ki), 0);
                    else
                        m.Manöver = new Manöver.Attacke(ki); 
                }
                else if (ki.Kampfstil == Kampfstil.BeidhändigerKampf && i>=1)
                {
                    //normale Aktionen und zusatzaktionen getrennt zählen, dann ist es einfach
                    //wenn noch keine Angriffsaktion da ist, dann Attacke bei i*-8
                    //wenn schon eine Angriffsaktion in der Liste ist, und i<Aktionen, dann zusatzaktion bei erste Angriffsaktion-4. sonst Attacke bei -8
                    if (zusatzAktionen > 0 && i==2)
                        InitiativListe.Add(ki, new Manöver.Attacke(ki), i * -4);
                    else
                    {
                        InitiativListe.Add(ki, new Manöver.ZusätzlicheAngriffsaktion(ki), (zusatzAktionen + 1) * -4);
                        zusatzAktionen++;
                    }
                }
                else if (i >= 1 && ki.Kampfstil == Kampfstil.Parierwaffenstil)
                {
                    if ( (ki.Kämpfer is Model.Gegner || (ki.Kämpfer as Model.Held).HatSonderfertigkeit("Tod von Links") )  && geplanteAktionen.Where(mi => mi.Manöver is Manöver.TodVonLinks).Count() == 0 )
                        InitiativListe.Add(ki, new Manöver.TodVonLinks(ki), Math.Min(i * -4, -8));
                    else
                        InitiativListe.Add(ki, new Manöver.Attacke(ki), Math.Min(i * -4, -8));
                }
                else if(ki.Kämpfer is Model.Gegner && ki.Aktionen > 2)
                    InitiativListe.Add(ki, new Manöver.Attacke(ki), i * -4);
                else
                    InitiativListe.Add(ki, new Manöver.Attacke(ki), Math.Min(i * -4, -8));
            }
            
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
            if(Kämpfer[k] == null)
                return;
            int rs = 0;
            if((optionen & TrefferpunkteOptions.IgnoriertRüstung) != TrefferpunkteOptions.IgnoriertRüstung)
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
            }
        }

        public void Dispose()
        {
            //TODO ??: ich finde diese Lösung noch nicht optimal. Das geht spätestens schief, wenn zwei Kämpfe gleichzeitig geführt werden.
            //Alle Gegenerinstanzen löschen
            Global.ContextHeld.DeleteAll<Model.Gegner>();
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
