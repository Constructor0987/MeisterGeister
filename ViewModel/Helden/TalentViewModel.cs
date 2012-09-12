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
    public class TalentViewModel : Base.ViewModelBase {
        #region //FELDER
        //Test
        private List<string> text = new List<string>();
        public List<string> Text { get { return text; } set { value = text; OnChanged("Text"); } }

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
        #endregion
        #region //EIGENSCHAFTEN
        //Listen
        public List<Model.Talent> TalentauswahlListe { get { return talentAuswahlListe; } set { talentAuswahlListe = value; OnChanged("TalentauswahlListe"); } }
        public ObservableCollection<TalentListeItem> KampftalentListe { get { return kampfTalentListe; } set { kampfTalentListe = value; OnChanged("KampftalentListe"); } }
        public ObservableCollection<TalentListeItem> KoerperTalentListe { get { return koerperTalentListe; } set { koerperTalentListe = value; OnChanged("KoerperTalentListe"); } }
        public ObservableCollection<TalentListeItem> GesellschaftTalentListe { get { return gesellschaftTalentListe; } set { gesellschaftTalentListe = value; OnChanged("GesellschaftTalentListe"); } }
        public ObservableCollection<TalentListeItem> NaturTalentListe { get { return naturTalentListe; } set { naturTalentListe = value; OnChanged("NaturTalentListe"); } }
        public ObservableCollection<TalentListeItem> WissenTalentListe { get { return wissenTalentListe; } set { wissenTalentListe = value; OnChanged("WissenTalentListe"); } }
        public ObservableCollection<TalentListeItem> SpracheTalentListe { get { return spracheTalentListe; } set { spracheTalentListe = value; OnChanged("SpracheTalentListe"); } }
        public ObservableCollection<TalentListeItem> HandwerkTalentListe { get { return handwerkTalentListe; } set { handwerkTalentListe = value; OnChanged("HandwerkTalentListe"); } }
        //Selection
        public Model.Held SelectedHeld { get { return selectedHeld; } set { selectedHeld = value; OnChanged("SelectedHeld"); } }
        public M.Talent TalentAuswahl { get { return talentAuswahl; } set { talentAuswahl = value; OnChanged("TalentAuswahl"); } }
        public TalentListeItem KampfTalent { get { return kampfTalent; } set { kampfTalent = value; OnChanged("KampfTalent"); } }
        public TalentListeItem KoerperTalent { get { return koerperTalent; } set { koerperTalent = value; OnChanged("KoerperTalent"); } }
        public TalentListeItem GesellschaftTalent { get { return gesellschaftTalent; } set { gesellschaftTalent = value; OnChanged("GesellschaftTalent"); } }
        public TalentListeItem NaturTalent { get { return naturTalent; } set { naturTalent = value; OnChanged("NaturTalent"); } }
        public TalentListeItem WissenTalent { get { return wissenTalent; } set { wissenTalent = value; OnChanged("WissenTalent"); } }
        public TalentListeItem SpracheTalent { get { return spracheTalent; } set { spracheTalent = value; OnChanged("SpracheTalent"); } }
        public TalentListeItem HandwerkTalent { get { return handwerkTalent; } set { handwerkTalent = value; OnChanged("HandwerkTalent"); } }
        //Command
        public Base.CommandBase OnAddTalent;
        #endregion
        #region //KONSTRUKTOR
        public TalentViewModel() {
            //Auf Event HeldWechsel aus Global registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            SelectedHeld = Global.SelectedHeld;
        }
        #endregion
        #region //METHODEN
        bool initFlag = false;
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
        #endregion
        #region //EVENTS
        //Event
        void SelectedHeldChanged() {
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
        #endregion
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
