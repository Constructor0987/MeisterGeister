using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;
using System.Collections.Specialized;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class Kampf : IDisposable
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
            set { _iniphase = value; }
        }

        private float _kampfrunde;
        public float Kampfrunde
        {
            get { return _kampfrunde; }
            private set { _kampfrunde = value; }
        }

        private ManöverInfo aktuelleAktion;
        public ManöverInfo AktuelleAktion
        {
            get { return aktuelleAktion; }
            set { 
                aktuelleAktion = value;
                //OnChanged("AktuelleAktion");
            }
        }

        public ManöverInfo Next()
        {
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
            //Alte Ansagen löschen
            InitiativListe.Clear();
            //Modifikatoren entfernen
            foreach (KämpferInfo ki in Kämpfer)
            {
                ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampfrunde);
                InitiativListe.Add(ki, new Manöver.KeineAktion(ki.Kämpfer), 0);
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
        }

        bool _umwandelnMöglich = true;
        public bool UmwandelnMöglich
        {
            get { return _umwandelnMöglich; }
            private set { _umwandelnMöglich = value; }
        }

        public void KampfBeginn()
        {
            //Globales Datum holen und mit Uhrzeit in Kampfbeginn abspeichern.
            //Die Kampfzeit wird für neue Modifikatoren in Erstellt abgelegt.
        }

        public void KampfEnde()
        {
            foreach (IKämpfer ki in Kämpfer)
                ki.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampf);
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
            //Anzeige neu darstellen?
        }

        public void Kämpfer_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Remove)
                InitiativListe.Remove((KämpferInfo)args.OldItems[0]);
            else if (args.Action == NotifyCollectionChangedAction.Add)
                InitiativListe.Add((KämpferInfo)args.NewItems[0], new Manöver.KeineAktion(((KämpferInfo)args.NewItems[0]).Kämpfer), 0);
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
            if (ki.Kämpfer.Kampfstil != Kampfstil.BeidhändigerKampf) //oder mehrhändig
            {
                foreach (var mi in InitiativListe.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion).ToList())
                    InitiativListe.Remove(mi);
            }
            if (ki.Kämpfer.Kampfstil != Kampfstil.Parierwaffenstil)
            {
                foreach (var mi in InitiativListe.Where(mi => mi.Manöver is Manöver.TodVonLinks).ToList())
                    InitiativListe.Remove(mi);
            }
            var geplanteAktionen = InitiativListe.Where(mi => mi.KämpferInfo == ki && mi.IsAktion).ToList().OrderBy(mi => mi.Initiative).ToList();
            while (geplanteAktionen.Count >= 1 && geplanteAktionen.Count > ki.Kämpfer.Angriffsaktionen)
            {
                var manöver = geplanteAktionen.FirstOrDefault();
                if(ki.Kämpfer.Angriffsaktionen == 0 && geplanteAktionen.Count==1)
                {
                    manöver.Manöver = new Manöver.KeineAktion(ki.Kämpfer);
                    break;
                }
                InitiativListe.Remove(manöver);
                geplanteAktionen.Remove(manöver);
            }
            if (ki.Kämpfer.Angriffsaktionen == 0 && geplanteAktionen.Count == 0)
                InitiativListe.Add(ki, new Manöver.KeineAktion(ki.Kämpfer), 0);
            int zusatzAktionen = geplanteAktionen.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion).Count();
            for (int i = geplanteAktionen.Count; i < ki.Kämpfer.Angriffsaktionen; i++)
            {
                if(i==0)
                    InitiativListe.Add(ki, new Manöver.Attacke(ki.Kämpfer), 0);
                else if (ki.Kämpfer.Kampfstil == Kampfstil.BeidhändigerKampf && i>=1)
                {
                    if (zusatzAktionen > 0 && i==2)
                        InitiativListe.Add(ki, new Manöver.Attacke(ki.Kämpfer), i * -4);
                    else
                    {
                        InitiativListe.Add(ki, new Manöver.ZusätzlicheAngriffsaktion(ki.Kämpfer), (zusatzAktionen + 1) * -4);
                        zusatzAktionen++;
                    }
                }
                else if (i >= 1 && ki.Kämpfer.Kampfstil == Kampfstil.Parierwaffenstil)
                {
                    if ( (ki.Kämpfer is Model.Gegner || (ki.Kämpfer as Model.Held).HatSonderfertigkeit("Tod von Links") )  && geplanteAktionen.Where(mi => mi.Manöver is Manöver.TodVonLinks).Count() == 0 )
                        InitiativListe.Add(ki, new Manöver.TodVonLinks(ki.Kämpfer), Math.Min(i * -4, -8));
                    else
                        InitiativListe.Add(ki, new Manöver.Attacke(ki.Kämpfer), Math.Min(i * -4, -8));
                }
                else if(ki.Kämpfer is Model.Gegner && ki.Kämpfer.Aktionen > 2)
                    InitiativListe.Add(ki, new Manöver.Attacke(ki.Kämpfer), i * -4);
                else
                    InitiativListe.Add(ki, new Manöver.Attacke(ki.Kämpfer), Math.Min(i * -4, -8));
            }
            
        }

        public void Dispose()
        {
            //TODO ??: ich finde diese Lösung noch nicht optimal. Das geht spätestens schief, wenn zwei Kämpfe gleichzeitig geführt werden.
            //Alle Gegenerinstanzen löschen
            Global.ContextHeld.DeleteAll<Model.Gegner>();
        }
    }
}
