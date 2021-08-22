using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene usings
using MeisterGeister.ViewModel.Basar.Logic;
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using MeisterGeister.ViewModel.SpielerScreen;
using MeisterGeister.Logic.Umrechner;
using MeisterGeister.View.General;
using MeisterGeister.View.SpielerScreen;

namespace MeisterGeister.ViewModel.Basar
{
    public class BasarViewModel : Base.ToolViewModelBase
    {
        #region //---- FELDER ----

        // Felder
        private double _rabattAufschlag = 0.0;
        private double _anzahl = 1.0;
        private string _suchText = string.Empty;
        private bool _nurVorhandeneWaren;

        private string _währungsText = "Silbertaler";

        private double _währungsFaktor = 1.0;
        
        // Listen
        private List<Model.Handelsgut> _handelsgutListe;
        private List<Model.Waffe> _waffeListe;
        private List<Model.Fernkampfwaffe> _fernkampfwaffeListe;
        private List<Model.Schild> _schildListe;
        private List<Model.Rüstung> _rüstungListe;
        private List<BasarItem> _basarItemListe;
        private List<BasarItem> _filteredBasarItemListe;
        private Währung _währungen;

        //Commands
        private Base.CommandBase _onGoToBugForum;
        private Base.CommandBase _onBtnChangeItemPicture;


        private List<System.Windows.Forms.Screen> _screenList = System.Windows.Forms.Screen.AllScreens.ToList();
        public List<System.Windows.Forms.Screen> ScreenList
        {
            get { return _screenList; }
        }

        private System.Windows.Forms.Screen _spielerScreen = null;
        public System.Windows.Forms.Screen SpielerScreen
        {
            get
            {
                if (_spielerScreen == null)
                {
                    if (ScreenList.Count <= 1)
                        _spielerScreen = ScreenList.FirstOrDefault();
                    else
                    {
                        foreach (System.Windows.Forms.Screen objActualScreen in ScreenList)
                        {
                            if (!objActualScreen.Primary)
                                _spielerScreen = objActualScreen;
                        }
                    }
                }
                return _spielerScreen;
            }
        }


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
                Währungsinformationen winfo  = Währungen.FirstOrDefault(t => t.Key == _währungsText).Value;
                WährungsFaktor = winfo.WährungsFaktor;
                
                // Änderung an BasarItems weiterreichen
                foreach (var item in BasarItemListe)
                {
                    item.WährungsCode = winfo.WährungAbkürzung;
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

        public string Regionen
        {
            get { return string.Join(", ", Global.MomentaneRegion); }
        }
        
        public bool NurVorhandeneWaren
        {
            get { return _nurVorhandeneWaren; }
            set
            {
                _nurVorhandeneWaren = value;
                OnChanged("NurVorhandeneWaren");
                FilterListe();
            }
        }

        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged();
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
                Set(ref _filteredBasarItemListe, value);
                //_filteredBasarItemListe = value;
                //OnChanged("FilteredBasarItemListe");
            }
        }

        public Währung Währungen
        {
            get { return _währungen; }
            set
            {
                _währungen = value;
                OnChanged("Währungen");
            }
        }

        
        #endregion

        //Commands
        public Base.CommandBase OnGoToBugForum
        {
            get { return _onGoToBugForum; }
        }

        public Base.CommandBase OnBtnChangeItemPicture
        {
            get { return _onBtnChangeItemPicture; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public BasarViewModel() : this(null, null) { }

        public BasarViewModel(Action<string> popup, Action<string, Exception> showError)
            : base(popup, showError)
        {
            _onGoToBugForum = new Base.CommandBase(GoToBugForum, null);
            _onBtnChangeItemPicture = new Base.CommandBase(ChangeItemPicture, null);

            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += SelectedHeldChanged;
            OnChanged("SelectedHeld");
        }
        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Global.HeldSelectionChanged -= SelectedHeldChanged;
        }

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
            Währungen = new Währung();
            FillBasarListe();
        }

        public void FillBasarListe()
        { 
            List<BasarItem> itemList = new List<BasarItem>();

            // Handelsgüter einfügen
            foreach (var item in HandelsgutListe)
                itemList.Add(NewBasarItem(item));

            // Waffen einfügen
            foreach (var item in WaffeListe)
                itemList.Add(NewBasarItem(item));    //(item as Model.Waffe).Verbreitung

            // Fernkampfwaffen einfügen
            foreach (var item in FernkampfwaffeListe)//(item as Model.Fernkampfwaffe).Verbreitung
                itemList.Add(NewBasarItem(item));

            // Schilde einfügen
            foreach (var item in SchildListe)//(item as Model.Schild).Verbreitung
                itemList.Add(NewBasarItem(item));

            // Rüstungen einfügen
            foreach (var item in RüstungListe)//(item as Model.Rüstung).Verbreitung
                itemList.Add(NewBasarItem(item));

            // Globale Listen der unterschiedlichen Handelsgütern in eine Gesamt-Liste zusammenführen         
            BasarItemListe = itemList;
            Refresh();
            FilterListe();
        }
        
        private BasarItem NewBasarItem(IHandelsgut item)
        {
            BasarItem basarItem = new BasarItem() { Item = item };
            basarItem.ImGebietVorhanden = item is Model.Handelsgut;
                  
            if (item as Model.Waffe != null)
            {
                if ((item as Model.Waffe).Verbreitung == null)
                    basarItem.ImGebietVorhanden = true;
                else
                // überall, alle
                if ((item as Model.Waffe).Verbreitung.Contains("überall") ||
                    ((item as Model.Waffe).Verbreitung.Contains("alle") &&
                     !(item as Model.Waffe).Verbreitung.Contains("ausser")))
                    basarItem.ImGebietVorhanden = true;
                else
                // alle ausser, alle außer
                if ((item as Model.Waffe).Verbreitung.Contains("ausser") ||
                    (item as Model.Waffe).Verbreitung.Contains("alle außer"))
                {
                    if (item.Name == "Beil")
                    { }
                    basarItem.ImGebietVorhanden = true;
                    Global.MomentaneRegion.ForEach(delegate (string r)
                        { if (basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = !(item as Model.Waffe).Verbreitung.Contains(r); });
                }
                else
                //Eingetragene Regionen = Vorhanden
                Global.MomentaneRegion.ForEach(delegate (string r)
                { if (!basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = ((item as Model.Waffe).Verbreitung.Contains(r)); });
            }

            if (item as Model.Fernkampfwaffe != null)
            {
                if ((item as Model.Fernkampfwaffe).Verbreitung == null)
                    basarItem.ImGebietVorhanden = true;
                else
                // überall, alle
                if ((item as Model.Fernkampfwaffe).Verbreitung.Contains("überall") ||
                    ((item as Model.Fernkampfwaffe).Verbreitung.Contains("alle") &&
                     !(item as Model.Fernkampfwaffe).Verbreitung.Contains("ausser")))
                    basarItem.ImGebietVorhanden = true;
                else
                // alle ausser, alle außer
                if ((item as Model.Fernkampfwaffe).Verbreitung.Contains("ausser") ||
                    (item as Model.Fernkampfwaffe).Verbreitung.Contains("alle außer"))
                {
                    basarItem.ImGebietVorhanden = true;
                    Global.MomentaneRegion.ForEach(delegate (string r)
                    { if (basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = !(item as Model.Fernkampfwaffe).Verbreitung.Contains(r); });
                }
                else
                    //Eingetragene Regionen = Vorhanden
                    Global.MomentaneRegion.ForEach(delegate (string r)
                    { if (!basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = ((item as Model.Fernkampfwaffe).Verbreitung.Contains(r)); });
            }

            if (item as Model.Schild != null)
            {
                if ((item as Model.Schild).Verbreitung == null)
                    basarItem.ImGebietVorhanden = true;
                else
                // überall, alle
                if ((item as Model.Schild).Verbreitung.Contains("überall") ||
                    ((item as Model.Schild).Verbreitung.Contains("alle") &&
                     !(item as Model.Schild).Verbreitung.Contains("ausser")))
                    basarItem.ImGebietVorhanden = true;
                else
                // alle ausser, alle außer
                if ((item as Model.Schild).Verbreitung.Contains("ausser") ||
                    (item as Model.Schild).Verbreitung.Contains("alle außer"))
                {
                    basarItem.ImGebietVorhanden = true;
                    Global.MomentaneRegion.ForEach(delegate (string r)
                    { if (basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = !(item as Model.Schild).Verbreitung.Contains(r); });
                }
                else
                    //Eingetragene Regionen = Vorhanden
                    Global.MomentaneRegion.ForEach(delegate (string r)
                    { if (!basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = ((item as Model.Schild).Verbreitung.Contains(r)); });
            }

            if (item as Model.Rüstung != null)
            {
                if ((item as Model.Rüstung).Verbreitung == null)
                    basarItem.ImGebietVorhanden = true;
                else
                // überall, alle
                if ((item as Model.Rüstung).Verbreitung.Contains("überall") ||
                    ((item as Model.Rüstung).Verbreitung.Contains("alle") &&
                     !(item as Model.Rüstung).Verbreitung.Contains("ausser")))
                    basarItem.ImGebietVorhanden = true;
                else
                // ausser, alle außer
                if ((item as Model.Rüstung).Verbreitung.Contains("ausser") ||
                    (item as Model.Rüstung).Verbreitung.Contains("alle außer"))
                {
                    basarItem.ImGebietVorhanden = true;
                    Global.MomentaneRegion.ForEach(delegate (string r)
                    { if (basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = !(item as Model.Rüstung).Verbreitung.Contains(r); });
                }
                else
                    //Eingetragene Regionen = Vorhanden
                    Global.MomentaneRegion.ForEach(delegate (string r)
                    { if (!basarItem.ImGebietVorhanden) basarItem.ImGebietVorhanden = ((item as Model.Rüstung).Verbreitung.Contains(r)); });
            }
            

            basarItem.InventarAddEvent += (s, e) => { AddToInventar(s); };
            basarItem.FilterKategorieEvent += (s, e) => { FilterKategorie(s); };
            return basarItem;
        }

        private void FilterKategorie(object sender)
        {
            if (sender != null && sender is BasarItem)
            {
                SuchText = ((BasarItem)sender).Kategorie;
            }
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
            FilteredBasarItemListe = (NurVorhandeneWaren) ?
                BasarItemListe.AsParallel().Where(t => t.ImGebietVorhanden).ToList():
                BasarItemListe;

            string suchText = _suchText.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');
            
            if (suchText == string.Empty) // kein Suchwort
                FilteredBasarItemListe = FilteredBasarItemListe.OrderBy(n => n.Name).ToList(); //AsParallel().
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredBasarItemListe = FilteredBasarItemListe.Where(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredBasarItemListe = FilteredBasarItemListe.Where(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }

        #endregion

        #region //---- EVENTS ----

        void ChangeItemPicture(object sender)
        {
            Logic.BasarItem bi = sender as Logic.BasarItem;
            if (bi != null)
            {
                string oldPfad = bi.Item.Pfad;
                string newPfad = ViewHelper.ChooseFile("Wähle ein Bild", oldPfad ?? "", false,
                    new string[] { "bmp", "gif", "jpg", "jpeg", "jpe", "jtif", "png", "tif", "tiff" });
                if (bi.Item is Model.Handelsgut)
                {
                    (bi.Item as Model.Handelsgut).Pfad = string.IsNullOrEmpty(newPfad) ? null : newPfad;
                    Global.ContextHandelsgut.Update<Model.Handelsgut>(bi.Item as Model.Handelsgut);
                }
                else
                if (bi.Item is Model.Waffe)
                {
                    (bi.Item as Model.Waffe).Pfad = string.IsNullOrEmpty(newPfad) ? null : newPfad;
                    Global.ContextHandelsgut.Update<Model.Waffe>(bi.Item as Model.Waffe);
                }
                else
                if (bi.Item is Model.Fernkampfwaffe)
                {
                    (bi.Item as Model.Fernkampfwaffe).Pfad = string.IsNullOrEmpty(newPfad) ? null : newPfad;
                    Global.ContextHandelsgut.Update<Model.Fernkampfwaffe>(bi.Item as Model.Fernkampfwaffe);
                }
                else
                if (bi.Item is Model.Schild)
                {
                    (bi.Item as Model.Schild).Pfad = string.IsNullOrEmpty(newPfad) ? null : newPfad;
                    Global.ContextHandelsgut.Update<Model.Schild>(bi.Item as Model.Schild);
                }
                else
                if (bi.Item is Model.Rüstung)
                {
                    (bi.Item as Model.Rüstung).Pfad = string.IsNullOrEmpty(newPfad) ? null : newPfad;
                    Global.ContextHandelsgut.Update<Model.Rüstung>(bi.Item as Model.Rüstung);
                }
            }
        }
        void SelectedHeldChanged(object sender, EventArgs e)
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
