using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Weitere Usings
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Helden.Controls {
    public class TalentViewModel : Base.ViewModelBase {

        #region //FELDER
        //Listen
        private List<Model.Talent> talentAuswahlListe = new List<Model.Talent>();
        private List<TalentListeItem> kampfTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> koerperTalentListe = new List<TalentListeItem>();//kö,ge,na,wi,sp,ha
        private List<TalentListeItem> gesellschaftTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> naturTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> wissenTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> spracheTalentListe = new List<TalentListeItem>();
        private List<TalentListeItem> handwerkTalentListe = new List<TalentListeItem>();
        //Selection
        private Model.Held selectedHeld = Global.SelectedHeld;
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
