using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class Kampf
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
            //Anzeige neu darstellen?
        }

        public void Kämpfer_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs args)
        {
            if (args.Action == System.ComponentModel.CollectionChangeAction.Remove)
                InitiativListe.Remove((KämpferInfo)args.Element);
            else if (args.Action == System.ComponentModel.CollectionChangeAction.Add)
                InitiativListe.Add((KämpferInfo)args.Element, new Manöver.KeineAktion(((KämpferInfo)args.Element).Kämpfer), 0);
        }

        public void InitiativListe_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs args)
        {
            if (args.Action == System.ComponentModel.CollectionChangeAction.Add && !(args.Element is Manöver.KeineAktion))
                UmwandelnMöglich = false;
        }
    }
}
