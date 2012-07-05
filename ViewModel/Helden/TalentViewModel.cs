using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Weitere Usings
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Helden {
    public class TalentViewModel : Base.ViewModelBase {
        #region //FELDER
        //Listen
        private List<Model.Talent> talentAuswahlListe = new List<Model.Talent>();
        private List<TalentListeItem> kampfTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> koerperTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> gesellschaftTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> naturTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> wissenTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> spracheTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> handwerkTalentListe = new List<TalentListeItem>();
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
        public List<TalentListeItem> KampftalentListe { get { return kampfTalentListe; } set { kampfTalentListe = value; OnChanged("KampftalentListe"); } }
        public List<TalentListeItem> KoerperTalentListe { get { return koerperTalentListe; } set { koerperTalentListe = value; OnChanged("KoerperTalentListe"); } }
        public List<TalentListeItem> GesellschaftTalentListe { get { return gesellschaftTalentListe; } set { gesellschaftTalentListe = value; OnChanged("GesellschaftTalentListe"); } }
        public List<TalentListeItem> NaturTalentListe { get { return naturTalentListe; } set { naturTalentListe = value; OnChanged("NaturTalentListe"); } }
        public List<TalentListeItem> WissenTalentListe { get { return wissenTalentListe; } set { wissenTalentListe = value; OnChanged("WissenTalentListe"); } }
        public List<TalentListeItem> SpracheTalentListe { get { return spracheTalentListe; } set { spracheTalentListe = value; OnChanged("SpracheTalentListe"); } }
        public List<TalentListeItem> HandwerkTalentListe { get { return handwerkTalentListe; } set { handwerkTalentListe = value; OnChanged("HandwerkTalentListe"); } }
        //Selection
        public Model.Held SelectedHeld { get { return selectedHeld; } set { selectedHeld = value; OnChanged("SelectedHeld"); } }
        public M.Talent TalentAuswahl{ get { return talentAuswahl; } set { talentAuswahl = value; OnChanged("TalentAuswahl"); } }
        public TalentListeItem KampfTalent{ get { return kampfTalent; } set { kampfTalent = value; OnChanged("KampfTalent"); } }
        public TalentListeItem KoerperTalent{ get { return koerperTalent; } set { koerperTalent = value; OnChanged("KoerperTalent"); } }
        public TalentListeItem GesellschaftTalent{ get { return gesellschaftTalent; } set { gesellschaftTalent = value; OnChanged("GesellschaftTalent"); } }
        public TalentListeItem NaturTalent{ get { return naturTalent; } set { naturTalent = value; OnChanged("NaturTalent"); } }
        public TalentListeItem WissenTalent{ get { return wissenTalent; } set { wissenTalent = value; OnChanged("WissenTalent"); } }
        public TalentListeItem SpracheTalent{ get { return spracheTalent; } set { spracheTalent = value; OnChanged("SpracheTalent"); } }
        public TalentListeItem HandwerkTalent{ get { return handwerkTalent; } set { handwerkTalent = value; OnChanged("HandwerkTalent"); } }
        #endregion
        #region //KONSTRUKTOR
        public TalentViewModel() {
            //Auf Event HeldWechsel aus Global registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            SelectedHeld = Global.SelectedHeld;
        }
        #endregion
        #region //METHODEN
        public void Init() {
            //Clear
            KampftalentListe.Clear();
            KoerperTalentListe.Clear();
            GesellschaftTalentListe.Clear();
            NaturTalentListe.Clear();
            WissenTalentListe.Clear();
            HandwerkTalentListe.Clear();
            SpracheTalentListe.Clear();

            if (SelectedHeld == null) {
                //TODO DW: Wenn Heldenliste wieder bedienbar, entfernen                
                SelectedHeld = Global.ContextHeld.HeldenListe.FirstOrDefault();
            }
            //All-Add für Talentauswahl
            TalentauswahlListe = Global.ContextTalent.TalentListe.ToList();
            //Heldentalente in Listen laden
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
        #region //EVENTS
        //Event
        void SelectedHeldChanged() {
            SelectedHeld = Global.SelectedHeld;
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
