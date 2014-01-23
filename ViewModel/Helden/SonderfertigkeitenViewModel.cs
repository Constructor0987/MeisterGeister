using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class SonderfertigkeitenViewModel : Base.ViewModelBase, Logic.IChangeListener
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onDeleteSonderfertigkeit;
        public Base.CommandBase OnDeleteSonderfertigkeit
        {
            get { return onDeleteSonderfertigkeit; }
        }

        private Base.CommandBase onAddSonderfertigkeit;
        public Base.CommandBase OnAddSonderfertigkeit
        {
            get { return onAddSonderfertigkeit; }
        }

        private Base.CommandBase onShowAddMultiSonderfertigkeit = null;
        public Base.CommandBase OnShowAddMultiSonderfertigkeit
        {
            get
            {
                if (onShowAddMultiSonderfertigkeit == null)
                    onShowAddMultiSonderfertigkeit = new Base.CommandBase(ShowAddMultiSonderfertigkeit, null);
                return onShowAddMultiSonderfertigkeit;
            }
        }

        private Base.CommandBase onAddMultiSonderfertigkeit = null;
        public Base.CommandBase OnAddMultiSonderfertigkeit
        {
            get
            {
                if (onAddMultiSonderfertigkeit == null)
                    onAddMultiSonderfertigkeit = new Base.CommandBase(AddMultiSonderfertigkeit, null);
                return onAddMultiSonderfertigkeit;
            }
        }

        private Base.CommandBase onOpenWiki;
        public Base.CommandBase OnOpenWiki
        {
            get { return onOpenWiki; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        private string _suchText = string.Empty;
        public string SuchText
        {
            get { return _suchText; }
            set
            {
                _suchText = value;
                OnChanged("SuchText");
                RefreshFilterListe();
            }
        }

        public string AktiveSettings
        {
            get { return Model.Setting.AktiveSettingsToString(); }
        }

        // Selection
        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }
        Model.Held_Sonderfertigkeit _selectedHeldSonderfertigkeit = null;
        public Model.Held_Sonderfertigkeit SelectedHeldSonderfertigkeit
        {
            get { return _selectedHeldSonderfertigkeit; }
            set { _selectedHeldSonderfertigkeit = value; OnChanged("SelectedHeldSonderfertigkeit"); } 
        }

        Model.Sonderfertigkeit _selectedAddSonderfertigkeit = null;
        public Model.Sonderfertigkeit SelectedAddSonderfertigkeit
        {
            get { return _selectedAddSonderfertigkeit; }
            set { _selectedAddSonderfertigkeit = value; OnChanged("SelectedAddSonderfertigkeit"); }
        }

        // Listen
        public List<Model.Held_Sonderfertigkeit> SonderfertigkeitListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.Held_Sonderfertigkeit.ToList(); }
        }

        public List<Model.Sonderfertigkeit> SonderfertigkeitAuswahlListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.SonderfertigkeitenWählbar; }
        }

        private List<SonderfertigkeitItem> _sonderfertigkeitMultiAddListe = new List<SonderfertigkeitItem>();
        public List<SonderfertigkeitItem> SonderfertigkeitMultiAddListe
        {
            get 
            {
                return _sonderfertigkeitMultiAddListe;
            }
            set
            {
                _sonderfertigkeitMultiAddListe = value;
                OnChanged("SonderfertigkeitMultiAddListe");
                RefreshFilterListe();
            }
        }

        private List<SonderfertigkeitItem> _filteredSonderfertigkeitMultiAddListe;
        public List<SonderfertigkeitItem> FilteredSonderfertigkeitMultiAddListe
        {
            get
            {
                return _filteredSonderfertigkeitMultiAddListe;
            }
            set
            {
                _filteredSonderfertigkeitMultiAddListe = value;
                OnChanged("FilteredSonderfertigkeitMultiAddListe");
            }
        }

        private void RefreshSonderfertigkeitMultiAddListe()
        {
            var globalList = Global.ContextHeld.SonderfertigkeitListe.OrderBy(sf => sf.Name);
            List<SonderfertigkeitItem> li = new List<SonderfertigkeitItem>(globalList.Count());
            foreach (var item in globalList)
                li.Add(new SonderfertigkeitItem(item, SelectedHeld.HatSonderfertigkeit(item)));
            SonderfertigkeitMultiAddListe = li;
        }

        private void RefreshFilterListe()
        {
            string suchText = _suchText.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');

            if (suchText == string.Empty) // kein Suchwort
                FilteredSonderfertigkeitMultiAddListe = SonderfertigkeitMultiAddListe.AsParallel().OrderBy(n => n.SF.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredSonderfertigkeitMultiAddListe = SonderfertigkeitMultiAddListe.AsParallel().Where(s => s.Contains(suchWorte[0])).OrderBy(n => n.SF.Name).ToList();
            else // mehrere Suchwörter
                FilteredSonderfertigkeitMultiAddListe = SonderfertigkeitMultiAddListe.AsParallel().Where(s => s.Contains(suchWorte)).OrderBy(n => n.SF.Name).ToList();
        }

        private bool _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        private Visibility _isMultiAdd = Visibility.Collapsed;
        public Visibility IsMultiAdd
        {
            get { return _isMultiAdd; }
            set
            {
                _isMultiAdd = value;
                OnChanged("IsMultiAdd");
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public SonderfertigkeitenViewModel(Func<string, string, bool> confirm, Action<string, Exception> showError) : base(confirm, showError)
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;

            onDeleteSonderfertigkeit = new Base.CommandBase(DeleteSonderfertigkeit, null);
            onAddSonderfertigkeit = new Base.CommandBase(AddSonderfertigkeit, null);
            onOpenWiki = new Base.CommandBase(OpenWiki, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            
        }

        public void NotifyRefresh()
        {
            OnChanged("SelectedHeld");
            OnChanged("SonderfertigkeitListe");
            OnChanged("SonderfertigkeitAuswahlListe");
            if (IsMultiAdd == Visibility.Visible)
                RefreshSonderfertigkeitMultiAddListe();
        }

        private void DeleteSonderfertigkeit(object sender)
        {
            Model.Held_Sonderfertigkeit h = SelectedHeldSonderfertigkeit;
            if (h != null && !IsReadOnly
                && Confirm("Sonderfertigkeit löschen", String.Format("Soll die Sonderfertigkeit {0} wirklich vom Helden entfernt werden?", h.Sonderfertigkeit.Name)))
            {
                SelectedHeld.DeleteSonderfertigkeit(h);
                SelectedHeldSonderfertigkeit = null;
                NotifyRefresh();
            }
        }

        private void AddSonderfertigkeit(object sender)
        {
            if (SelectedHeld != null && SelectedAddSonderfertigkeit != null && !IsReadOnly)
            {
                if (
                    (SelectedAddSonderfertigkeit.HatWert ?? false)
                    || (!SelectedHeld.HatSonderfertigkeitUndVoraussetzungen(SelectedAddSonderfertigkeit))
                    )
                    SelectedHeld.AddSonderfertigkeit(SelectedAddSonderfertigkeit, null);

                NotifyRefresh();
            }
        }

        private void ShowAddMultiSonderfertigkeit(object obj)
        {
            if (SelectedHeld != null && !IsReadOnly)
            {
                if (IsMultiAdd == Visibility.Visible)
                    IsMultiAdd = Visibility.Collapsed;
                else
                {
                    IsMultiAdd = Visibility.Visible;
                    RefreshSonderfertigkeitMultiAddListe();
                }
            }
        }

        private void AddMultiSonderfertigkeit(object obj)
        {
            if (SelectedHeld != null && !IsReadOnly)
            {
                // Sonderfertigkeiten hinzufügen
                foreach (var item in SonderfertigkeitMultiAddListe.Where(sf => sf.IsChecked && sf.IsWählbar))
                    SelectedHeld.AddSonderfertigkeit(item.SF);

                IsMultiAdd = Visibility.Collapsed;
                NotifyRefresh();
            }
        }

        private void OpenWiki(object sender)
        {
            if (SelectedHeldSonderfertigkeit != null)
                WikiAventurica.OpenBrowser(SelectedHeldSonderfertigkeit.Sonderfertigkeit.Name);
        }

        #endregion

        #region //---- EVENTS ----

        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }

        private void SelectedHeldChanged()
        {
            if (!ListenToChangeEvents)
                return;
            NotifyRefresh();
        }

        #endregion

        private bool listenToChangeEvents = true;

        public bool ListenToChangeEvents
        {
            get { return listenToChangeEvents; }
            set { listenToChangeEvents = value; SelectedHeldChanged(); }
        }

    }

    #region Subklassen

    public class SonderfertigkeitItem
    {
        public SonderfertigkeitItem(Model.Sonderfertigkeit sf, bool hatHeld)
        {
            SF = sf;
            IsWählbar = !hatHeld;
            IsChecked = hatHeld;
            if (SF != null)
            {
                _suchtext = SF.Name.ToLower() + SF.Typ.ToLower();
                SetImagePath();
            }
        }

        private string _suchtext = string.Empty;

        public Model.Sonderfertigkeit SF { get; set; }

        public bool IsWählbar { get; set; }

        public bool IsChecked { get; set; }

        private string _imagePath = string.Empty;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        private void SetImagePath()
        {
            switch (SF.Typ)
            {
                case "Kampf":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/nahkampf_01.png";
                    break;
                case "Magisch":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/magie.png";
                    break;
                case "Magisch (Ritual)":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/zauberzeichen.png";
                    break;
                case "Petromantie":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/zauberzeichen.png";
                    break;
                case "Klerikal":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/audio2.png";
                    break;
                case "Klerikal (Liturgie)":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/audio2.png";
                    break;
                default:
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/helden.png";
                    break;
            }
        }

        /// <summary>
        /// Prüft, ob 'suchWort' im Namen, der Kategorie oder in den Tags vorkommt.
        /// </summary>
        /// <param name="suchWort"></param>
        /// <returns></returns>
        public bool Contains(string suchWort)
        {
            return _suchtext.Contains(suchWort);
        }

        /// <summary>
        /// Prüft, ob die 'suchWorte' im Namen, der Kategorie oder in den Tags vorkommt.
        /// Es wird dabei eine UND-Prüfung durchgeführt.
        /// </summary>
        /// <param name="suchWorte"></param>
        /// <returns></returns>
        public bool Contains(string[] suchWorte)
        {
            foreach (string wort in suchWorte)
            {
                if (!Contains(wort))
                    return false;
            }
            return true;
        }
    }

    #endregion
    
}
