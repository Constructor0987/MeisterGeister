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
    public class TalentViewModel : Base.ViewModelBase, Logic.IChangeListener {
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
        private ObservableCollection<TalentListeItem> kampfTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> koerperTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> gesellschaftTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> naturTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> wissenTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> spracheTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> handwerkTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> gabenTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> ritualeTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> liturgienTalentListe = new ObservableCollection<TalentListeItem>();                
        #endregion
        #region //EIGENSCHAFTEN
        //Logic
        public bool ListenToChangeEvents {
            get { return listenToChangeEvents; }
            set { listenToChangeEvents = value; SelectedHeldChanged(); }
        }
        //Listen
        public List<Model.Talent> TalentauswahlListe {
            get { return talentAuswahlListe; }
            set {
                if (value != null) { talentAuswahlListe = value; OnChanged("TalentauswahlListe"); }
            }
        }
        public ObservableCollection<TalentListeItem> KampftalentListe {
            get { return kampfTalentListe; }
            set {
                if (value != null) {
                    kampfTalentListe = value; OnChanged("KampftalentListe");
                }
            }
        }
        public ObservableCollection<TalentListeItem> KoerperTalentListe {
            get { return koerperTalentListe; }
            set {
                if (value != null) { koerperTalentListe = value; OnChanged("KoerperTalentListe"); }
            }
        }
        public ObservableCollection<TalentListeItem> GesellschaftTalentListe {
            get { return gesellschaftTalentListe; }
            set {
                if (value != null) { gesellschaftTalentListe = value; OnChanged("GesellschaftTalentListe"); }
            }
        }
        public ObservableCollection<TalentListeItem> NaturTalentListe {
            get { return naturTalentListe; }
            set {
                if (value != null) { naturTalentListe = value; OnChanged("NaturTalentListe"); }
            }
        }
        public ObservableCollection<TalentListeItem> WissenTalentListe {
            get { return wissenTalentListe; }
            set { if (value != null) { wissenTalentListe = value; OnChanged("WissenTalentListe"); } }
        }
        public ObservableCollection<TalentListeItem> SpracheTalentListe {
            get { return spracheTalentListe; }
            set { if (value != null) { spracheTalentListe = value; OnChanged("SpracheTalentListe"); } }
        }
        public ObservableCollection<TalentListeItem> HandwerkTalentListe {
            get { return handwerkTalentListe; }
            set {
                if (value != null) { handwerkTalentListe = value; OnChanged("HandwerkTalentListe"); }
            }
        }
        public ObservableCollection<TalentListeItem> GabenTalentListe {
            get { return gabenTalentListe; }
            set {
                if (value != null) { gabenTalentListe = value; OnChanged("GabenTalentListe "); }
            }
        }
        public ObservableCollection<TalentListeItem> RitualeTalentListe {
            get { return ritualeTalentListe; }
            set {
                if (value != null) { ritualeTalentListe = value; OnChanged("RitualeTalentListe"); }
            }
        }
        public ObservableCollection<TalentListeItem> LiturgienTalentListe {
            get { return liturgienTalentListe; }
            set {
                if (value != null) { liturgienTalentListe = value; OnChanged("LiturgienTalentListe"); }
            }
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

            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            SelectedHeld = Global.SelectedHeld;
        }
        #endregion
        #region //METHODEN
        public void Init() {
            if (Global.SelectedHeld != null) {
                SelectedHeldChanged();
            }
        }
        private void ReInit() {
            KampftalentListe.Clear();
            KoerperTalentListe.Clear();
            GesellschaftTalentListe.Clear();
            NaturTalentListe.Clear();
            WissenTalentListe.Clear();
            SpracheTalentListe.Clear();
            HandwerkTalentListe.Clear();
            GabenTalentListe.Clear();
            RitualeTalentListe.Clear();
            LiturgienTalentListe.Clear();
        }
        #endregion
        #region //EVENTS
        void SelectedHeldChanged() {
            if (!ListenToChangeEvents)
                return;

            Global.SetIsBusy(true, "Talente werden geladen...");
            SelectedHeld = Global.SelectedHeld;
            ReInit();
            if (SelectedHeld != null) {
                foreach (var item in SelectedHeld.Held_Talent) {
                    switch (Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault().TalentgruppeID) {
                        case 1:
                            TalentListeItem tmpKampf = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpKampf.RemoveItem += (s, e) => { RemoveTalent(s); };
                            KampftalentListe.Add(tmpKampf);
                            break;
                        case 2:
                            TalentListeItem tmpKoerper = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpKoerper.RemoveItem += (s, e) => { RemoveTalent(s); };
                            KoerperTalentListe.Add(tmpKoerper);
                            break;
                        case 3:
                            TalentListeItem tmpGesellschaft = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpGesellschaft.RemoveItem += (s, e) => { RemoveTalent(s); };
                            GesellschaftTalentListe.Add(tmpGesellschaft);
                            break;
                        case 4:
                            TalentListeItem tmpNatur = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpNatur.RemoveItem += (s, e) => { RemoveTalent(s); };
                            NaturTalentListe.Add(tmpNatur);
                            break;
                        case 5:
                            TalentListeItem tmpWissen = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpWissen.RemoveItem += (s, e) => { RemoveTalent(s); };
                            WissenTalentListe.Add(tmpWissen);
                            break;
                        case 6:
                            TalentListeItem tmpHandwerk = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpHandwerk.RemoveItem += (s, e) => { RemoveTalent(s); };
                            HandwerkTalentListe.Add(tmpHandwerk);
                            break;
                        case 7:                            
                            TalentListeItem tmpSprache = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpSprache.RemoveItem += (s, e) => { RemoveTalent(s); };
                            SpracheTalentListe.Add(tmpSprache);
                            break;
                        case 8:
                            //Meta ID = 10
                            break;
                        case 9:
                            TalentListeItem tmpGaben = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpGaben.RemoveItem += (s, e) => { RemoveTalent(s); };
                            GabenTalentListe.Add(tmpGaben);
                            break;
                        case 10:
                            TalentListeItem tmpRitual = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpRitual.RemoveItem += (s, e) => { RemoveTalent(s); };
                            RitualeTalentListe.Add(tmpRitual);
                            break;
                        case 11:
                            TalentListeItem tmpLiturgie = new TalentListeItem() {
                                HeldTalent = item,
                                Talent = item.Talent
                            };
                            tmpLiturgie.RemoveItem += (s, e) => { RemoveTalent(s); };
                            LiturgienTalentListe.Add(tmpLiturgie);
                            break;
                        default:
                            break;
                    }
                }
                //Load Talentauswahl VS HeldTalentListe
                if (SelectedHeld != null) {
                    TalentauswahlListe = Global.ContextTalent.TalentListe.Where(item => SelectedHeld.Held_Talent.Where(t => t.Talent == item).Count() <= 0 && item.TalentgruppeID != 8).OrderBy(item => item.Talentname).ToList();
                }
            }
            Global.SetIsBusy(false);
        }        
        private void AddTalent(object obj) {
            if (TalentAuswahl != null) {
                M.Held_Talent newTalent = new M.Held_Talent() {
                    Held = SelectedHeld,
                    HeldGUID = SelectedHeld.HeldGUID,
                    Talent = TalentAuswahl,
                    Talentname = TalentAuswahl.Talentname,
                    TaW = 0,
                    ZuteilungAT = 0,
                    ZuteilungPA = 0
                };
                SelectedHeld.Held_Talent.Add(newTalent);
                SelectedHeldChanged();
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

            if (h != null
                && Confirm("Talent löschen", String.Format("Soll das Talent '{0}' wirklich vom Helden entfernt werden?", h.Talent.Talentname))
                && Global.ContextHeld.Delete<Model.Held_Talent>(h))
            {
                SelectedHeldChanged();
                TalentauswahlListe.Add(h.Talent);
                TalentAuswahl = null;
            }

        }
        private void OpenWiki(object sender)
        {
            if (SelectedTalentListeItem != null)
                System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedTalentListeItem.Talent.GetWikiLink());
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
