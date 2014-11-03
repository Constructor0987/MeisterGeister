using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene usings
using MeisterGeister.ViewModel.Basar.Logic;
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister.ViewModel.Basar
{
    public class BasarViewModel : Base.ViewModelBase
    {
        #region //---- FELDER ----

        // Felder
        private double _rabattAufschlag = 0.0;
        private double _anzahl = 1.0;
        private string _suchText = string.Empty;

        private string _währungsText = "Silbertaler";

        private double _währungsFaktor = 1.0;
        private Model.Held _selectedHeld;
        
        // Listen
        private List<Model.Handelsgut> _handelsgutListe;
        private List<Model.Waffe> _waffeListe;
        private List<Model.Fernkampfwaffe> _fernkampfwaffeListe;
        private List<Model.Schild> _schildListe;
        private List<Model.Rüstung> _rüstungListe;
        private List<BasarItem> _basarItemListe;
        private List<BasarItem> _filteredBasarItemListe;

        //Commands
        private Base.CommandBase _onGoToBugForum;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public double RabattAufschlag
        {
            get { return _rabattAufschlag; }
            set
            {
                _rabattAufschlag = value;
                OnChanged("RabattAufschlag");

                // Änderung an BasarItems weiterreichen
                foreach (var item in BasarItemListe)
                    item.RabattAufschlag = _rabattAufschlag;
            }
        }

        public double Anzahl
        {
            get { return _anzahl; }
            set
            {
                if (_anzahl == value || value < 0.0)
                    return;
                _anzahl = value;
                OnChanged("Anzahl");

                // Änderung an BasarItems weiterreichen
                foreach (var item in BasarItemListe)
                    item.Anzahl = _anzahl;
            }
        }

        public double WährungsFaktor
        {
            get { return _währungsFaktor; }
            set
            {
                if (_währungsFaktor == value || value < 0.0)
                    return;
                _währungsFaktor = value;
                OnChanged("WährungsFaktor");

                // Änderung an BasarItems weiterreichen
                foreach (var item in BasarItemListe)
                    item.WährungsFaktor = _währungsFaktor;                
            }
        }
        
        public string WährungsText
        {
            get { return _währungsText; }
            set
            {
                _währungsText = value;
                OnChanged("WährungsText");
                MeisterGeister.Logic.Umrechner.Währung w = new MeisterGeister.Logic.Umrechner.Währung();                
                WährungsFaktor = w.FirstOrDefault(t => t.Key == _währungsText).Value;
                
                string wcode = // "-- Mittelreichische Münzen --"
                            (_währungsText == "Kreuzer") ? "K" :
                            (_währungsText == "Heller") ? "H" :
                            (_währungsText == "Silbertaler") ? "S" :
                            (_währungsText == "Dukaten") ? "D" :
                            (_währungsText == "Balihoer Rad (veraltet)") ? "BaR" :
                            (_währungsText == "Puniner Dublone (veraltet)") ? "PuD" :
                            (_währungsText == "Nickel (veraltet)") ? "Ni" :
                            (_währungsText == "Eslamo (veraltet)") ? "Es" :

                            //"-- Nostria und Andergast --"
                            (_währungsText == "Nostrische Krone") ? "NoKr" :
                            (_währungsText == "Andrataler") ? "An":

                            //"-- Al'Anfanische Münzen --"
                            (_währungsText == "Dirham") ? "Dir" :
                            (_währungsText == "Kleiner Oreal") ? "KOr" :
                            (_währungsText == "Oreal/Schilling") ? "Or" :
                            (_währungsText == "Dublone") ? "Dub" :

                            //"-- Amazonische Münzen --"
                            (_währungsText == "Amazonenkrone") ? "Kro" :

                            //"-- Brabaker Münzen --"
                            (_währungsText == "Brabaker Kreuzer") ? "Kr" :
                            (_währungsText == "Brabaker Krone") ? "Kro":
                            (_währungsText == "Brabaker Krone (außerhalb Brabaks)") ? "Kro" :

                            // "-- Münzen der Schwarzen Lande --") ? 0 :
                            (_währungsText == "Gulden (Glorania)") ? "Gul" :
                            (_währungsText == "Splitter (Xeraanien)") ? "Spl" :
                            (_währungsText == "Zholvari (Xeraanien)") ? "Zho" :
                            (_währungsText == "Borbaradstaler (Xeraanien)") ? "Bor" :

                           // (_währungsText == "-- Aranische Münzen --"
                            (_währungsText == "Rosenkreuzer") ? "Ros" :
                            (_währungsText == "Hallah") ? "Hal" :
                            (_währungsText == "Schekel") ? "Sch" :
                            (_währungsText == "Dinar") ? "Din" :

                            // "-- sonstige tulamidische Münzen --"
                            (_währungsText == "Selemer Kupferschilling (veraltet)") ? "SKu" :
                            (_währungsText == "Piaster (Rashdul)") ? "Pia" :
                            (_währungsText == "Alastren (Khunchom)") ? "Ala" :

                            // "-- Bornländische Münzen --"
                            (_währungsText == "Deut") ? "Deu":
                            (_währungsText == "Silbergroschen/Groschen") ? "Gro" :
                            (_währungsText == "Batzen") ? "Bat" :

                            // "-- Münzen des Kalifats --"
                            (_währungsText == "Muwlat") ? "Mu" :
                            (_währungsText == "Zechine") ? "Ze" :
                            (_währungsText == "Marawedi") ? "Ma" :
                            (_währungsText == "Shekel (veraltet)") ? "She" :
                            (_währungsText == "Denar (veraltet)") ? "De" :
                            (_währungsText == "Piaster (veraltet)") ? "Pia" :

                            // "-- Münzen im Großemirat Mengbilla --"
                            (_währungsText == "Ikossar") ? "Iko" :
                            (_währungsText == "Tesar") ? "Tes" :
                            (_währungsText == "Telar") ? "Tel" :
                            (_währungsText == "Dekat") ? "Dek" :
                            (_währungsText == "Mengbillaner Unze (veraltet)") ? "MUz" :

                            // "-- Horasische Münzen --"
                            (_währungsText == "Kusliker Rad (Horasdor)") ? "KRa" :
                            (_währungsText == "Krone (veraltet)") ? "Kro" :
                            (_währungsText == "Zehnt (veraltet)") ? "Z" :
                            (_währungsText == "Schilling (veraltet)") ? "Sch" :
                            (_währungsText == "Arivorer Silberdukaten (veraltet)") ? "Sid" :

                            // "-- Paavische Münzen --"
                            (_währungsText == "Gulden") ? "Gu" :

                            // "-- Trahelische Münzen --"
                            (_währungsText == "Trümmer") ? "Tr" :
                            (_währungsText == "Ch'ryskl") ? "Ch" :
                            (_währungsText == "Hedsch") ? "Hed" :
                            (_währungsText == "Suvar") ? "Suv" :

                            // "-- Vallusanische Münzen --"
                            (_währungsText == "Flindrich") ? "Fl" :
                            (_währungsText == "Stüber") ? "St" :
                            (_währungsText == "Witten") ? "Wi" :

                            // "-- Münzen der Zwerge --"
                            (_währungsText == "Atebrox (Zwergengroschen)") ? "Ate" :
                            (_währungsText == "Arganbrox (Zwergenschilling)") ? "Arg" :
                            (_währungsText == "Auromox (Zwergentaler)") ? "Aur" :

                            // "-- Weitere Münzen --"
                            (_währungsText == "Chorhoper Heller") ? "Ch" :
                            (_währungsText == "Syllaner Taler") ? "Syl" :
                            (_währungsText == "Minisepe") ? "Min" :

                            // "-- Myranische Münzen --"
                            (_währungsText == "Obulos") ? "Ob" :
                            (_währungsText == "Pekunos") ? "Pk" :
                            (_währungsText == "Argental") ? "Ag" :
                            (_währungsText == "Aureal") ? "Au" :
                            "S";
                // Änderung an BasarItems weiterreichen
                foreach (var item in BasarItemListe)
                {
                    item.WährungsCode = wcode;
                    item.WährungsText = _währungsText;
                }

                FilterListe();
            }
        }

        public string SuchText
        {
            get { return _suchText; }
            set
            {
                _suchText = value;
                OnChanged("SuchText");
                FilterListe();
            }
        }

        public Model.Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {
                _selectedHeld = value;
                OnChanged("SelectedHeld");
                OnChanged("HeldTalentwerte");
            }
        }

        public string HeldTalentwerte
        {
            get
            {
                string tawSchätzen = "-"; string tawHandel = "-"; string tawFeilschen = "-";
                if (SelectedHeld != null)
                {
                    if (SelectedHeld.HatTalent("Schätzen"))
                        tawSchätzen = SelectedHeld.Talentwert("Schätzen").ToString();
                    if (SelectedHeld.HatTalent("Handel"))
                        tawHandel = SelectedHeld.Talentwert("Handel").ToString();
                    if (SelectedHeld.HatTalent("Überreden"))
                        tawFeilschen = SelectedHeld.Talentwert("Überreden").ToString();
                }
                return string.Format("Schätzen {0}, Handel {1}, Überreden (Feilschen) {2}", tawSchätzen, tawHandel, tawFeilschen);
            }
        }

        #region //---- LISTEN ----

        public List<Model.Held> HeldListe
        {
            get { return Global.ContextHeld.HeldenGruppeListe; }
        }

        public List<Model.Handelsgut> HandelsgutListe
        {
            get { return _handelsgutListe; }
            set
            {
                _handelsgutListe = value;
                OnChanged("HandelsgutListe");
            }
        }

        public List<Model.Waffe> WaffeListe
        {
            get { return _waffeListe; }
            set
            {
                _waffeListe = value;
                OnChanged("WaffeListe");
            }
        }

        public List<Model.Fernkampfwaffe> FernkampfwaffeListe
        {
            get { return _fernkampfwaffeListe; }
            set
            {
                _fernkampfwaffeListe = value;
                OnChanged("FernkampfwaffeListe");
            }
        }

        public List<Model.Schild> SchildListe
        {
            get { return _schildListe; }
            set
            {
                _schildListe = value;
                OnChanged("SchildListe");
            }
        }

        public List<Model.Rüstung> RüstungListe
        {
            get { return _rüstungListe; }
            set
            {
                _rüstungListe = value;
                OnChanged("RüstungListe");
            }
        }

        public List<BasarItem> BasarItemListe
        {
            get { return _basarItemListe; }
            set
            {
                _basarItemListe = value;
                OnChanged("BasarItemListe");
            }
        }

        public List<BasarItem> FilteredBasarItemListe
        {
            get { return _filteredBasarItemListe; }
            set
            {
                _filteredBasarItemListe = value;
                OnChanged("FilteredBasarItemListe");
            }
        }

        #endregion

        //Commands
        public Base.CommandBase OnGoToBugForum
        {
            get { return _onGoToBugForum; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public BasarViewModel() : this(null, null) { }

        public BasarViewModel(Action<string> popup, Action<string, Exception> showError)
            : base(popup, showError)
        {
            _onGoToBugForum = new Base.CommandBase(GoToBugForum, null);

            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };

            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Refresh()
        {
            OnChanged("HeldListe");
        }

        public void Init()
        {
            HandelsgutListe = Global.ContextHandelsgut == null ? new List<Model.Handelsgut>() : Global.ContextHandelsgut.HandelsgüterListe;
            WaffeListe = Global.ContextInventar == null ? new List<Model.Waffe>() : Global.ContextInventar.WaffeListe;
            FernkampfwaffeListe = Global.ContextInventar == null ? new List<Model.Fernkampfwaffe>() : Global.ContextInventar.FernkampfwaffeListe;
            SchildListe = Global.ContextInventar == null ? new List<Model.Schild>() : Global.ContextInventar.SchildListe;
            RüstungListe = Global.ContextInventar == null ? new List<Model.Rüstung>() : Global.ContextInventar.RuestungListe;

            // Globale Listen der unterschiedlichen Handelsgütern in eine Gesamt-Liste zusammenführen
            List<BasarItem> itemList = new List<BasarItem>();

            // Handelsgüter einfügen
            foreach (var item in HandelsgutListe)
                itemList.Add(NewBasarItem(item));

            // Waffen einfügen
            foreach (var item in WaffeListe)
                itemList.Add(NewBasarItem(item));

            // Fernkampfwaffen einfügen
            foreach (var item in FernkampfwaffeListe)
                itemList.Add(NewBasarItem(item));

            // Schilde einfügen
            foreach (var item in SchildListe)
                itemList.Add(NewBasarItem(item));

            // Rüstungen einfügen
            foreach (var item in RüstungListe)
                itemList.Add(NewBasarItem(item));

            BasarItemListe = itemList;

            Refresh();

            FilterListe();
        }

        private BasarItem NewBasarItem(IHandelsgut item)
        {
            BasarItem basarItem = new BasarItem() { Item = item };
            basarItem.InventarAddEvent += (s, e) => { AddToInventar(s); };
            return basarItem;
        }

        void AddToInventar(object sender)
        {
            if (sender != null && sender is BasarItem && SelectedHeld != null)
            {
                BasarItem item = (BasarItem)sender;
                int anzahl = (int)Math.Ceiling(Anzahl);
                SelectedHeld.AddInventar(item.Item, anzahl);
                PopUp(string.Format("{0}x '{1}' zum Inventar von '{2}' hinzugefügt.", anzahl, item.Name, SelectedHeld.Name));
            }
        }

        /// <summary>
        /// Filtert die BasarItem-Liste auf Basis des SuchTextes.
        /// </summary>
        private void FilterListe()
        {
            string suchText = _suchText.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');

            if (suchText == string.Empty) // kein Suchwort
                FilteredBasarItemListe = BasarItemListe.AsParallel().OrderBy(n => n.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredBasarItemListe = BasarItemListe.AsParallel().Where(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredBasarItemListe = BasarItemListe.AsParallel().Where(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }

        #endregion

        #region //---- EVENTS ----

        void SelectedHeldChanged()
        {
            SelectedHeld = Global.SelectedHeld;
        }

        void GoToBugForum(object sender)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("http://forum.meistergeister.org/showthread.php?tid=191"));
        }

        #endregion

    }
}