using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

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
            InitiativListe = new InitiativListe(this);
            InitiativListe.PropertyChanged += InitiativListe_PropertyChanged;
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

        public void NeueKampfrunde()
        {
            //Modifikatoren entfernen
            foreach (KämpferInfo ki in Kämpfer)
            {
                ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampfrunde);
                //Alte Ansagen löschen
                //Im UI sollten kämpfer ohne Ansage leicht an der Farbe erkennbar sein
                //Kämpfer mit Aufmerksamkeit oder Kampfgespür müssen nicht markiert werden (höchstens mit einer leichten tönung)
            }
            Kampfrunde++;
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
            if (args.PropertyName == "List")
            {
                //INI Liste abgleichen
            }
            if (args.PropertyName == "Sort")
            {
                //INI Liste sortieren
            }
            //Anzeige neu darstellen?
        }

        public void InitiativListe_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
        }
    }
}
