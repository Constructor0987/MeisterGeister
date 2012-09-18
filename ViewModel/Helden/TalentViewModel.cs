using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Weitere Usings
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;
using System.Collections.ObjectModel;

namespace MeisterGeister.ViewModel.Helden {
    public class TalentViewModel : Base.ViewModelBase, Logic.IChangeListener {
        #region //FELDER
        //Intern
        bool initFlag = false;
        //Listen
        private List<Model.Talent> talentAuswahlListe = new List<Model.Talent>();
        private ObservableCollection<TalentListeItem> kampfTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> koerperTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> gesellschaftTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> naturTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> wissenTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> spracheTalentListe = new ObservableCollection<TalentListeItem>();
        private ObservableCollection<TalentListeItem> handwerkTalentListe = new ObservableCollection<TalentListeItem>();
        //Selection
        private M.Held selectedHeld;
        private M.Talent talentAuswahl;
        private TalentListeItem kampfTalent;
        private TalentListeItem koerperTalent;
        private TalentListeItem gesellschaftTalent;
        private TalentListeItem naturTalent;
        private TalentListeItem wissenTalent;
        private TalentListeItem spracheTalent;
        private TalentListeItem handwerkTalent;
        //Command
        public Base.CommandBase onAddTalent;
        public Base.CommandBase onRemoveTalent;        
        #endregion
        #region //EIGENSCHAFTEN
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
        //Selection
        public Model.Held SelectedHeld { get { return selectedHeld; } set { selectedHeld = value; OnChanged("SelectedHeld"); } }
        public M.Talent TalentAuswahl { get { return talentAuswahl; } set { talentAuswahl = value; OnChanged("TalentAuswahl"); } }
        public TalentListeItem KampfTalent { get { return kampfTalent; } set { ClearTalentauswahl(1); kampfTalent = value; OnChanged("KampfTalent"); } }
        public TalentListeItem KoerperTalent { get { return koerperTalent; } set { ClearTalentauswahl(2); koerperTalent = value; OnChanged("KoerperTalent"); } }
        public TalentListeItem GesellschaftTalent { get { return gesellschaftTalent; } set { ClearTalentauswahl(3); gesellschaftTalent = value; OnChanged("GesellschaftTalent"); } }
        public TalentListeItem NaturTalent { get { return naturTalent; } set { ClearTalentauswahl(4); naturTalent = value; OnChanged("NaturTalent"); } }
        public TalentListeItem WissenTalent { get { return wissenTalent; } set { ClearTalentauswahl(5); wissenTalent = value; OnChanged("WissenTalent"); } }
        public TalentListeItem SpracheTalent { get { return spracheTalent; } set { ClearTalentauswahl(6); spracheTalent = value; OnChanged("SpracheTalent"); } }
        public TalentListeItem HandwerkTalent { get { return handwerkTalent; } set { ClearTalentauswahl(7); handwerkTalent = value; OnChanged("HandwerkTalent"); } }
        //Command
        public Base.CommandBase OnAddTalent_Click { get { return onAddTalent; } }
        public Base.CommandBase OnRemoveTalent_Click { get { return onRemoveTalent; } }
        #endregion
        #region //KONSTRUKTOR
        public TalentViewModel() {
            onAddTalent = new Base.CommandBase(AddTalent, null);
            onRemoveTalent = new Base.CommandBase(RemoveTalent, null);            
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            SelectedHeld = Global.SelectedHeld;
        }
        #endregion
        #region //METHODEN
        public void Init() {
            if (initFlag == false) {
                //Clear
                KampftalentListe.Clear();
                KoerperTalentListe.Clear();
                GesellschaftTalentListe.Clear();
                NaturTalentListe.Clear();
                WissenTalentListe.Clear();
                HandwerkTalentListe.Clear();
                SpracheTalentListe.Clear();

                //All-Add für Talentauswahl
                TalentauswahlListe = Global.ContextTalent.TalentListe.OrderBy(item => item.Talentname).ToList();
                //Heldentalente in Listen laden
                if (Global.SelectedHeld != null) {
                    SelectedHeldChanged();
                }
                initFlag = true;
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
        }
        private void ClearTalentauswahl(int aExcept) {
            if (aExcept != 1) {
                KampfTalent = null;
            }
            if (aExcept != 2) {
                KoerperTalent = null;
            }
            if (aExcept != 3) {
                GesellschaftTalent = null;
            }
            if (aExcept != 4) {
                NaturTalent = null;
            }
            if (aExcept != 5) {
                WissenTalent = null;
            }
            if (aExcept != 6) {
                SpracheTalent = null;
            }
            if (aExcept != 7) {
                HandwerkTalent = null;
            }
        }
        private TalentListeItem GetSelectedTalent() {
            if (KampfTalent != null) {
                return KampfTalent;
            }
            if (KoerperTalent != null) {
                return KoerperTalent;
            }
            if (GesellschaftTalent != null) {
                return GesellschaftTalent;
            }
            if (NaturTalent != null) {
                return NaturTalent;
            }
            if (WissenTalent != null) {
                return WissenTalent;
            }
            if (SpracheTalent != null) {
                return SpracheTalent;
            }
            if (HandwerkTalent != null) {
                return HandwerkTalent;
            }
            return null;
        }
        #endregion
        #region //EVENTS
        //Global
        void SelectedHeldChanged() {
            if (!ListenToChangeEvents)
                return;
            SelectedHeld = Global.SelectedHeld;
            ReInit();
            if (SelectedHeld != null) {
                foreach (var item in SelectedHeld.Held_Talent) {
                    switch (Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault().TalentgruppeID) {
                        case 1:
                            KampftalentListe.Add(new TalentListeItem() {
                                HeldTalent = item,
                                Talent = Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault()
                            });
                            break;
                        case 2:
                            KoerperTalentListe.Add(new TalentListeItem() {
                                HeldTalent = item,
                                Talent = Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault()
                            });
                            break;
                        case 3:
                            GesellschaftTalentListe.Add(new TalentListeItem() {
                                HeldTalent = item,
                                Talent = Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault()
                            });
                            break;
                        case 4:
                            NaturTalentListe.Add(new TalentListeItem() {
                                HeldTalent = item,
                                Talent = Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault()
                            });
                            break;
                        case 5:
                            WissenTalentListe.Add(new TalentListeItem() {
                                HeldTalent = item,
                                Talent = Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault()
                            });
                            break;
                        case 6:
                            HandwerkTalentListe.Add(new TalentListeItem() {
                                HeldTalent = item,
                                Talent = Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault()
                            });
                            break;
                        case 7:
                            SpracheTalentListe.Add(new TalentListeItem() {
                                HeldTalent = item,
                                Talent = Global.ContextTalent.TalentListe.Where(val => val.Talentname == item.Talentname).FirstOrDefault()
                            });
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        //UI
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
                TalentAuswahl = null;
            }
        }
        private void RemoveTalent(object obj) {
            TalentListeItem removeTalent = GetSelectedTalent();
            if (removeTalent != null) {
                SelectedHeld.Held_Talent.Remove(removeTalent.HeldTalent);
                SelectedHeldChanged();                
            }
        }        
        #endregion

        private bool listenToChangeEvents = true;

        public bool ListenToChangeEvents
        {
            get { return listenToChangeEvents; }
            set { listenToChangeEvents = value; SelectedHeldChanged(); }
        }
        
    }
    #region SUBKLASSEN
    public class TalentListeItem : Base.ViewModelBase {
        #region //FELDER
        #endregion
        #region //EIGENSCHAFTEN
        public Model.Talent Talent { get; set; }
        public Model.Held_Talent HeldTalent { get; set; }
        public string TaW { get; set; }
        #endregion
        #region //KONSTRUKTOR
        #endregion
        #region //EVENTS
        #endregion
    }
    #endregion
}
