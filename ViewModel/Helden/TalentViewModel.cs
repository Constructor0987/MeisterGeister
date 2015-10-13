using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Weitere Usings
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;
using System.Collections.ObjectModel;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Helden {
    public class TalentViewModel : Base.ViewModelBase {
        #region //FELDER
        private bool listenToChangeEvents = true;
        public Base.CommandBase onAddTalent;
        public Base.CommandBase onRemoveTalent;
        private Base.CommandBase onOpenWiki;
        private Base.CommandBase onWürfelProbe;
        private Base.CommandBase onWürfelGruppenProbe;
        private M.Held selectedHeld;
        private M.Talent talentAuswahl;        
        private List<Model.Talent> talentAuswahlListe = new List<Model.Talent>();
        private List<TalentListeItem> _kampfTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _koerperTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _gesellschaftTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _naturTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _wissenTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _spracheTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _handwerkTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _gabenTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _ritualeTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> _liturgienTalentListe = new List<TalentListeItem>();                
        #endregion
        #region //EIGENSCHAFTEN
        //Logic
        private bool _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }
        //Listen
        public List<Model.Talent> TalentauswahlListe {
            get { return talentAuswahlListe; }
            set {
                if (value != null) { talentAuswahlListe = value; OnChanged("TalentauswahlListe"); }
            }
        }
        public List<TalentListeItem> KampfTalentListe
        {
            get { return _kampfTalentListe == null ? new List<TalentListeItem>() : _kampfTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _kampfTalentListe = value; OnChanged("KampfTalentListe"); }
        }
        public List<TalentListeItem> KoerperTalentListe 
        {
            get { return _koerperTalentListe == null ? new List<TalentListeItem>() : _koerperTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _koerperTalentListe = value; OnChanged("KoerperTalentListe"); }
        }
        public List<TalentListeItem> GesellschaftTalentListe
        {
            get { return _gesellschaftTalentListe == null ? new List<TalentListeItem>() : _gesellschaftTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _gesellschaftTalentListe = value; OnChanged("GesellschaftTalentListe"); }
        }
        public List<TalentListeItem> NaturTalentListe
        {
            get { return _naturTalentListe == null ? new List<TalentListeItem>() : _naturTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _naturTalentListe = value; OnChanged("NaturTalentListe"); }
        }
        public List<TalentListeItem> WissenTalentListe
        {
            get { return _wissenTalentListe == null ? new List<TalentListeItem>() : _wissenTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _wissenTalentListe = value; OnChanged("WissenTalentListe"); }
        }
        public List<TalentListeItem> SpracheTalentListe
        {
            get { return _spracheTalentListe == null ? new List<TalentListeItem>() : _spracheTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _spracheTalentListe = value; OnChanged("SpracheTalentListe"); }
        }
        public List<TalentListeItem> HandwerkTalentListe
        {
            get { return _handwerkTalentListe == null ? new List<TalentListeItem>() : _handwerkTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _handwerkTalentListe = value; OnChanged("HandwerkTalentListe"); }
        }
        public List<TalentListeItem> GabenTalentListe
        {
            get { return _gabenTalentListe == null ? new List<TalentListeItem>() : _gabenTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _gabenTalentListe = value; OnChanged("GabenTalentListe "); }
        }
        public List<TalentListeItem> RitualeTalentListe
        {
            get { return _ritualeTalentListe == null ? new List<TalentListeItem>() : _ritualeTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _ritualeTalentListe = value; OnChanged("RitualeTalentListe"); }
        }
        public List<TalentListeItem> LiturgienTalentListe
        {
            get { return _liturgienTalentListe == null ? new List<TalentListeItem>() : _liturgienTalentListe.OrderBy(t => t.Talent.Talentname).ToList(); }
            set { _liturgienTalentListe = value; OnChanged("LiturgienTalentListe"); }
        }
        //Selection
        public Model.Held SelectedHeld { get { return selectedHeld; } set { selectedHeld = value; OnChanged("SelectedHeld"); } }
        public M.Talent TalentAuswahl { get { return talentAuswahl; } set { talentAuswahl = value; OnChanged("TalentAuswahl"); } }
        TalentListeItem _selectedTalentListeItem = null;
        public TalentListeItem SelectedTalentListeItem
        {
            get { return _selectedTalentListeItem; }
            set { _selectedTalentListeItem = value; OnChanged("SelectedTalentListeItem"); }
        }
        public Base.CommandBase OnAddTalent_Click { get { return onAddTalent; } }
        public Base.CommandBase OnRemove { get { return onRemoveTalent; } }
        public Base.CommandBase OnOpenWiki
        {
            get { return onOpenWiki; }
        }
        public Base.CommandBase OnWürfelProbe
        {
            get { return onWürfelProbe; }
        }
        public Base.CommandBase OnWürfelGruppenProbe
        {
            get { return onWürfelGruppenProbe; }
        }
        #endregion
        #region //KONSTRUKTOR
        public TalentViewModel(Action<string> popup, Func<string, string, bool> confirm, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError)
            : base(popup, confirm, showProbeDialog, showError)
        {
            onAddTalent = new Base.CommandBase(AddTalent, null);
            onRemoveTalent = new Base.CommandBase(RemoveTalent, null);
            onOpenWiki = new Base.CommandBase(OpenWiki, null);
            onWürfelProbe = new Base.CommandBase(WürfelProbe, null);
            onWürfelGruppenProbe = new Base.CommandBase(WürfelGruppenProbe, null);
        }
        #endregion
        #region //METHODEN

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += SelectedHeldChanged;
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;
            SelectedHeldChanged(this, new EventArgs());
        }
        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Global.HeldSelectionChanged -= SelectedHeldChanged;
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged -= IsReadOnlyChanged;
        }
        
        private void ReInit() {
            _kampfTalentListe.Clear();
            _koerperTalentListe.Clear();
            _gesellschaftTalentListe.Clear();
            _naturTalentListe.Clear();
            _wissenTalentListe.Clear();
            _spracheTalentListe.Clear();
            _handwerkTalentListe.Clear();
            _gabenTalentListe.Clear();
            _ritualeTalentListe.Clear();
            _liturgienTalentListe.Clear();
        }
        #endregion
        #region //EVENTS
        
        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }

        void SelectedHeldChanged(object sender, EventArgs args) {
            SelectedHeld = Global.SelectedHeld;
            ReInit();
            if (SelectedHeld != null) 
            {
                foreach (var item in SelectedHeld.Held_Talent) {
                    if (item.Talent == null)
                        break;
                    switch (item.Talent.TalentgruppeID) {
                        case M.Talent.GRUPPE_KAMPF:
                            TalentListeItem tmpKampf = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpKampf.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _kampfTalentListe.Add(tmpKampf);
                            break;
                        case 2:
                            TalentListeItem tmpKoerper = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpKoerper.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _koerperTalentListe.Add(tmpKoerper);
                            break;
                        case 3:
                            TalentListeItem tmpGesellschaft = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpGesellschaft.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _gesellschaftTalentListe.Add(tmpGesellschaft);
                            break;
                        case 4:
                            TalentListeItem tmpNatur = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpNatur.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _naturTalentListe.Add(tmpNatur);
                            break;
                        case 5:
                            TalentListeItem tmpWissen = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpWissen.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _wissenTalentListe.Add(tmpWissen);
                            break;
                        case 6:
                            TalentListeItem tmpHandwerk = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpHandwerk.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _handwerkTalentListe.Add(tmpHandwerk);
                            break;
                        case 7:                            
                            TalentListeItem tmpSprache = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpSprache.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _spracheTalentListe.Add(tmpSprache);
                            break;
                        case M.Talent.GRUPPE_META:
                            //Meta ID = 10
                            break;
                        case 9:
                            TalentListeItem tmpGaben = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpGaben.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _gabenTalentListe.Add(tmpGaben);
                            break;
                        case 10:
                            TalentListeItem tmpRitual = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpRitual.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _ritualeTalentListe.Add(tmpRitual);
                            break;
                        case 11:
                            TalentListeItem tmpLiturgie = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpLiturgie.RemoveItem += (s, e) => { RemoveTalent(s); };
                            _liturgienTalentListe.Add(tmpLiturgie);
                            break;
                        default:
                            break;
                    }
                }

                //Load Talentauswahl VS HeldTalentListe
                if (SelectedHeld != null) {
                    TalentauswahlListe = Global.ContextTalent.TalentListe.Where(item => SelectedHeld.Held_Talent.Where(t => t.Talent == item).Count() <= 0 && item.TalentgruppeID != 8 && item.TalentgruppeID != 0).OrderBy(item => item.Talentname).ToList();
                }
            }

            // Refresh Listen
            OnChanged("KampfTalentListe");
            OnChanged("KoerperTalentListe");
            OnChanged("GesellschaftTalentListe");
            OnChanged("NaturTalentListe");
            OnChanged("WissenTalentListe");
            OnChanged("SpracheTalentListe");
            OnChanged("HandwerkTalentListe");
            OnChanged("GabenTalentListe");
            OnChanged("RitualeTalentListe");
            OnChanged("LiturgienTalentListe");
        }        
        private void AddTalent(object obj) {
            if (TalentAuswahl != null && SelectedHeld != null && !IsReadOnly)
            {
                int taw = 0;
                if (Global.DSA5 && TalentAuswahl.TalentgruppeID == 1)
                    taw = 6; // Kampftechniken starten auf Wert 6
                SelectedHeld.AddTalent(TalentAuswahl, taw);
                SelectedHeldChanged(this, new EventArgs());
                TalentauswahlListe.Remove(TalentAuswahl);
                TalentAuswahl = null;
            }
        }
        void RemoveTalent(object sender) {
            M.Held_Talent h = null;
            if (sender != null) // Aufruf durch 'TalentListeItem'
                h = ((sender as TalentListeItem).HeldTalent);
            else if (SelectedTalentListeItem != null) // Aufruf durch ContextMenu
                h = SelectedTalentListeItem.HeldTalent;

            if (h != null && !IsReadOnly
                && Confirm("Talent löschen", String.Format("Soll das Talent '{0}' wirklich vom Helden entfernt werden?", h.Talent.Talentname)))
            {
                SelectedHeld.DeleteTalent(h);
                SelectedHeldChanged(this, new EventArgs());
                TalentauswahlListe.Add(h.Talent);
                TalentAuswahl = null;
            }

        }
        private void OpenWiki(object sender)
        {
            if (SelectedTalentListeItem != null)
                WikiAventurica.OpenBrowser(SelectedTalentListeItem.Talent);
        }

        private void WürfelGruppenProbe(object obj)
        {
            if (SelectedTalentListeItem != null)
                Global.WürfelGruppenProbe(SelectedTalentListeItem.HeldTalent);
        }

        private void WürfelProbe(object obj)
        {
            if (SelectedTalentListeItem != null)
                ShowProbeDialog(SelectedTalentListeItem.HeldTalent, SelectedHeld);
        }
        #endregion
    }
    #region SUBKLASSEN
    public class TalentListeItem : Base.ViewModelBase {
        #region //FELDER
        private Base.CommandBase onRemove;
        #endregion
        #region //EIGENSCHAFTEN
        public Model.Talent Talent { get; set; }
        public Model.Held_Talent HeldTalent { get; set; }
        public Base.CommandBase OnRemove { get { return onRemove; } }
        #endregion
        #region //KONSTRUKTOR
        public TalentListeItem() {
            onRemove = new Base.CommandBase(Remove, null);
        }
        #endregion
        #region //EVENTS
        public event EventHandler RemoveItem;
        void Remove(object sender) {
            if (RemoveItem != null) {
                RemoveItem(this, new EventArgs());
            }

        }
        #endregion
    }
    #endregion
}
