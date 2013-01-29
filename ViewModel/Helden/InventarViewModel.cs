using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Weitere Usings
using System.Windows;
using System.Collections.ObjectModel;
//Eigene Usings
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Logik = MeisterGeister.ViewModel.Inventar.Logic;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister.ViewModel.Inventar {
    public class InventarViewModel : Base.ViewModelBase, MeisterGeister.ViewModel.Helden.Logic.IChangeListener {

        #region //FELDER

        //Intern
        const string TALENTNAHKAMPFWAFFEUNTERKATEGORIE = "Bewaffneter Nahkampf";
        const string TALENTNAHKAMPFWAFFEATTECHNIK = "Bewaffnete AT-Technik";
        const string TALENTFERNKAMPFWAFFEUNTERKATEGORIE = "Fernkampf";
        const string FILTERDEAKTIVIEREN = " Alle ";
        bool IsLoaded = false;

        //UI
        private Visibility isNahkampfwaffevorhanden = Visibility.Collapsed;
        private Visibility isFernkampfwaffevorhanden = Visibility.Collapsed;
        private Visibility isSchildVorhanden = Visibility.Collapsed;
        private Visibility isRuestungVorhanden = Visibility.Collapsed;
        private bool isAllSelected;
        private bool isNahkampfWaffeSelected;
        private bool isFernkampfwaffeSelected;
        private bool isSchildSelected;
        private bool isRuestungSelected;
        private int selectedFilterIndex = 0;

        private Model.Held selectedHeld;
        private Model.Talent selectedNahkampfwaffeTalent;
        private Model.Talent selectedFernkampfwaffeTalent;
        private Model.Waffe selectedNahkampfwaffe;
        private Model.Fernkampfwaffe selectedFernkampfwaffe;
        private Model.Schild selectedSchild;
        private Model.Rüstung selectedRuestung;

        private double aktuellesGewicht = 0;
        private double aktuellesGewichtInProzentZuTragkraft = 0;
        private double aktuellesGewichtProzentResultierendeBE = 0;
        private double aktuelleTragkraft = 0;

        //Entitylisten
        private List<Model.Talent> nahkampfWaffeTalentListe = new List<Model.Talent>();
        private List<Model.Talent> fernkampWaffeTalentListe = new List<Model.Talent>();
        private List<Model.Waffe> nahkampfwaffeListe = new List<Model.Waffe>();
        private List<Model.Fernkampfwaffe> fernkampfwaffeListe = new List<Model.Fernkampfwaffe>();
        private List<Model.Schild> schildListe = new List<Model.Schild>();
        private List<Model.Rüstung> ruestungListe = new List<Model.Rüstung>();

        //Zuordnungen
        private ObservableCollection<NahkampfItem> heldNahkampfWaffeImInventar = new ObservableCollection<NahkampfItem>();
        private ObservableCollection<FernkampfItem> heldFernkampfwaffeImInventar = new ObservableCollection<FernkampfItem>();
        private ObservableCollection<SchildItem> heldSchildImInventar = new ObservableCollection<SchildItem>();
        private ObservableCollection<RuestungItem> heldRuestungImInventar = new ObservableCollection<RuestungItem>();

        //Commands
        private Base.CommandBase onAddNahkampfwaffe;
        private Base.CommandBase onAddFernkampfwaffe;
        private Base.CommandBase onAddSchild;
        private Base.CommandBase onAddRuestung;

        #endregion

        #region //EIGENSCHAFTEN

        //UI
        public Visibility IsNahkampfwaffevorhanden {
            get { return isNahkampfwaffevorhanden; }
            set {
                isNahkampfwaffevorhanden = value;
                OnChanged("IsNahkampfwaffevorhanden");
            }
        }
        public Visibility IsFernkampfwaffevorhanden {
            get { return isFernkampfwaffevorhanden; }
            set {
                isFernkampfwaffevorhanden = value;
                OnChanged("IsFernkampfwaffevorhanden");
            }
        }
        public Visibility IsSchildVorhanden {
            get { return isSchildVorhanden; }
            set {
                isSchildVorhanden = value;
                OnChanged("IsSchildVorhanden");
            }
        }
        public Visibility IsRuestungVorhanden {
            get { return isRuestungVorhanden; }
            set {
                isRuestungVorhanden = value;
                OnChanged("IsRuestungVorhanden");
            }
        }
        public int SelectedFilterIndex {
            get { return selectedFilterIndex; }
            set {
                selectedFilterIndex = value;
                OnChanged("");
            }
        }

        public bool IsAllSelected {
            get { return isAllSelected; }
            set {
                isAllSelected = value;
                if (value && HeldNahkampfWaffeImInventar.Count() > 0) {
                    IsNahkampfwaffevorhanden = Visibility.Visible;
                }
                if (value && HeldFernkampfwaffeImInventar.Count() > 0) {
                    IsFernkampfwaffevorhanden = Visibility.Visible;
                }
                if (value && HeldSchildImInventar.Count() > 0) {
                    IsSchildVorhanden = Visibility.Visible;
                }

                if (value && HeldRuestungImInventar.Count() > 0) {
                    IsRuestungVorhanden = Visibility.Visible;
                }
                OnChanged("IsAllSelected");
            }
        }
        public bool IsNahkampfWaffeSelected {
            get { return isNahkampfWaffeSelected; }
            set {
                isNahkampfWaffeSelected = value;
                if (value) {
                    if (value && HeldNahkampfWaffeImInventar.Count() > 0) {
                        IsNahkampfwaffevorhanden = Visibility.Visible;
                    }
                    IsFernkampfwaffevorhanden = Visibility.Collapsed;
                    IsSchildVorhanden = Visibility.Collapsed;
                    IsRuestungVorhanden = Visibility.Collapsed;
                }

                OnChanged("IsNahkampfWaffeSelected");
            }
        }
        public bool IsFernkampfwaffeSelected {
            get { return isFernkampfwaffeSelected; }
            set {
                isFernkampfwaffeSelected = value;
                if (value) {
                    if (value && HeldFernkampfwaffeImInventar.Count() > 0) {
                        IsFernkampfwaffevorhanden = Visibility.Visible;
                    }
                    IsNahkampfwaffevorhanden = Visibility.Collapsed;
                    IsSchildVorhanden = Visibility.Collapsed;
                    IsRuestungVorhanden = Visibility.Collapsed;
                }

                OnChanged("IsFernkampfwaffeSelected");
            }
        }
        public bool IsSchildSelected {
            get { return isSchildSelected; }
            set {
                isSchildSelected = value;
                if (value) {
                    if (value && HeldSchildImInventar.Count() > 0) {
                        IsSchildVorhanden = Visibility.Visible;
                    }
                    IsFernkampfwaffevorhanden = Visibility.Collapsed;
                    IsNahkampfwaffevorhanden = Visibility.Collapsed;
                    IsRuestungVorhanden = Visibility.Collapsed;
                }
                OnChanged("IsSchildSelected");
            }
        }
        public bool IsRuestungSelected {
            get { return isRuestungSelected; }
            set {
                isRuestungSelected = value;
                if (value) {
                    if (value && HeldRuestungImInventar.Count() > 0) {
                        IsRuestungVorhanden = Visibility.Visible;
                    }
                    IsFernkampfwaffevorhanden = Visibility.Collapsed;
                    IsNahkampfwaffevorhanden = Visibility.Collapsed;
                    IsSchildVorhanden = Visibility.Collapsed;
                }
                OnChanged("IsRuestungSelected");
            }
        }

        public Model.Held SelectedHeld {
            get { return selectedHeld; }
            set {
                selectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }
        public Model.Talent SelectedNahkampfwaffeTalent {
            get { return selectedNahkampfwaffeTalent; }
            set {
                selectedNahkampfwaffeTalent = value;
                if (value.Talentname != FILTERDEAKTIVIEREN) {
                    NahkampfwaffeListe = Global.ContextInventar.WaffeListe.Where(w => w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                } else {
                    NahkampfwaffeListe = Global.ContextInventar.WaffeListe.ToList();
                }
                OnChanged("SelectedNahkampfwaffeTalent");
            }
        }
        public Model.Talent SelectedFernkampfwaffeTalent {
            get { return selectedFernkampfwaffeTalent; }
            set {
                selectedFernkampfwaffeTalent = value;
                if (value.Talentname != FILTERDEAKTIVIEREN) {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.Where(w => w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                } else {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.ToList();
                }
                OnChanged("SelectedFernkampfwaffeTalent");
            }
        }
        public Model.Waffe SelectedNahkampfwaffe {
            get { return selectedNahkampfwaffe; }
            set {
                selectedNahkampfwaffe = value;
                OnChanged("SelectedNahkampfwaffe");
            }
        }
        public Model.Fernkampfwaffe SelectedFernkampfwaffe {
            get { return selectedFernkampfwaffe; }
            set {
                selectedFernkampfwaffe = value;
                OnChanged("SelectedFernkampfwaffe");
            }
        }
        public Model.Schild SelectedSchild {
            get { return selectedSchild; }
            set {
                selectedSchild = value;
                OnChanged("SelectedSchild");
            }
        }
        public Model.Rüstung SelectedRuestung {
            get { return selectedRuestung; }
            set {
                selectedRuestung = value;
                OnChanged("SelectedRuestung");
            }
        }

        public double AktuellesGewicht {
            get { return aktuellesGewicht; }
            set {
                aktuellesGewicht = value;

                AktuellesGewichtInProzentZuTragkraft = ((AktuellesGewicht / AktuelleTragkraft) * 100);

                double val;
                if (AktuellesGewichtInProzentZuTragkraft / 50 - 2 > 0) {
                    val = Convert.ToInt32(Math.Floor(AktuellesGewichtInProzentZuTragkraft / 50 - 2 + 1));
                } else {
                    val = 0;
                }
                AktuellesGewichtProzentResultierendeBE = Convert.ToInt32(val);
                OnChanged("SelectedHeld");
                OnChanged("AktuellesGewicht");
            }
        }
        public double AktuelleTragkraft {
            get { return aktuelleTragkraft; }
            set {
                aktuelleTragkraft = value;
                OnChanged("AktuelleTragkraft");
            }
        }
        public double AktuellesGewichtInProzentZuTragkraft {
            get { return aktuellesGewichtInProzentZuTragkraft; }
            set {
                List<string> trimOnKomma = value.ToString().Split(',').ToList();
                double inShort = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                aktuellesGewichtInProzentZuTragkraft = inShort;
                OnChanged("AktuellesGewichtInProzentZuTragkraft");
            }
        }
        public double AktuellesGewichtProzentResultierendeBE {
            get { return aktuellesGewichtProzentResultierendeBE; }
            set {
                if (aktuellesGewichtProzentResultierendeBE != value) {
                    aktuellesGewichtProzentResultierendeBE = value;
                }
                OnChanged("AktuellesGewichtProzentResultierendeBE");
            }
        }

        //EntityListen
        public List<Model.Talent> NahkampfWaffeTalentListe {
            get { return nahkampfWaffeTalentListe; }
            set {
                nahkampfWaffeTalentListe = value;
                OnChanged("NahkampfWaffeTalentListe");
            }
        }
        public List<Model.Talent> FernkampWaffeTalentListe {
            get { return fernkampWaffeTalentListe; }
            set {
                fernkampWaffeTalentListe = value;
                OnChanged("FernkampWaffeTalentListe ");
            }
        }
        public List<Model.Waffe> NahkampfwaffeListe {
            get { return nahkampfwaffeListe; }
            set {
                nahkampfwaffeListe = value;
                OnChanged("NahkampfwaffeListe");
            }
        }
        public List<Model.Fernkampfwaffe> FernkampfwaffeListe {
            get { return fernkampfwaffeListe; }
            set {
                fernkampfwaffeListe = value;
                OnChanged("FernkampfwaffeListe");
            }
        }
        public List<Model.Schild> SchildListe {
            get { return schildListe; }
            set {
                schildListe = value;
                OnChanged("SchildListe");
            }
        }
        public List<Model.Rüstung> RuestungListe {
            get { return ruestungListe; }
            set {
                ruestungListe = value;
                OnChanged("RuestungListe");
            }
        }

        //Zuordnung
        public ObservableCollection<NahkampfItem> HeldNahkampfWaffeImInventar {
            get { return heldNahkampfWaffeImInventar; }
            set {
                heldNahkampfWaffeImInventar = value;
                if (heldNahkampfWaffeImInventar.Count() != 0) {
                    IsNahkampfwaffevorhanden = Visibility.Visible;
                } else {
                    IsNahkampfwaffevorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldNahkampfWaffeImInventar");
            }
        }
        public ObservableCollection<FernkampfItem> HeldFernkampfwaffeImInventar {
            get { return heldFernkampfwaffeImInventar; }
            set {
                heldFernkampfwaffeImInventar = value;
                if (heldFernkampfwaffeImInventar.Count() != 0) {
                    IsFernkampfwaffevorhanden = Visibility.Visible;
                } else {
                    IsFernkampfwaffevorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldFernkampfwaffeImInventar");
            }
        }
        public ObservableCollection<SchildItem> HeldSchildImInventar {
            get { return heldSchildImInventar; }
            set {
                heldSchildImInventar = value;
                if (heldSchildImInventar.Count() != 0) {
                    IsSchildVorhanden = Visibility.Visible;
                } else {
                    IsSchildVorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldSchildImInventar");
            }
        }
        public ObservableCollection<RuestungItem> HeldRuestungImInventar {
            get { return heldRuestungImInventar; }
            set {
                heldRuestungImInventar = value;
                if (heldRuestungImInventar.Count() != 0) {
                    IsRuestungVorhanden = Visibility.Visible;
                } else {
                    IsRuestungVorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldRuestungImInventar");
            }
        }
        //Commands
        public Base.CommandBase OnAddNahkampfwaffe {
            get { return onAddNahkampfwaffe; }
        }
        public Base.CommandBase OnAddFernwaffe {
            get { return onAddFernkampfwaffe; }
        }
        public Base.CommandBase OnAddSchild {
            get { return onAddSchild; }
        }
        public Base.CommandBase OnAddRuestung {
            get { return onAddRuestung; }
        }
        #endregion
        #region //KONSTRUKTOR

        public InventarViewModel() {
            onAddNahkampfwaffe = new Base.CommandBase(AddNahkampfwaffe, null);
            onAddFernkampfwaffe = new Base.CommandBase(AddFernkampfwaffe, null);
            onAddSchild = new Base.CommandBase(AddSchild, null);
            onAddRuestung = new Base.CommandBase(AddRuestung, null);

            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            SelectedFilterIndex = 0;
        }

        #endregion
        #region //INSTANZMETHODEN
        public void LoadDaten() {
            if (IsLoaded == false) {
                //Nahkampf
                NahkampfWaffeTalentListe.Add(new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });
                if (Global.ContextTalent != null)
                    NahkampfWaffeTalentListe.AddRange(Global.ContextTalent.TalentListe.Where(t =>
                        t.TalentgruppeID == 1
                        && (t.Untergruppe == TALENTNAHKAMPFWAFFEUNTERKATEGORIE
                        || t.Untergruppe == TALENTNAHKAMPFWAFFEATTECHNIK)
                        && !NahkampfWaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname));
                //NahkampfWaffeTalentListe = NahkampfWaffeTalentListe;                
                if (Global.ContextInventar != null)
                    NahkampfwaffeListe.AddRange(Global.ContextInventar.WaffeListe.Where(w => !NahkampfwaffeListe.Contains(w)).OrderBy(w => w.Name));
                if (NahkampfwaffeListe.Count > 0) {
                    IsNahkampfwaffevorhanden = Visibility.Visible;
                    OnChanged("IsNahkampfwaffevorhanden");
                }
                OnChanged("NahkampfwaffeListe");

                //Fernkampf
                FernkampWaffeTalentListe.Add(new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });
                if (Global.ContextTalent != null)
                    FernkampWaffeTalentListe.AddRange(Global.ContextTalent.TalentListe.Where(t => t.TalentgruppeID == 1 && t.Untergruppe == TALENTFERNKAMPFWAFFEUNTERKATEGORIE && !FernkampWaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname));
                //FernkampWaffeTalentListe = FernkampWaffeTalentListe;    
                if (Global.ContextInventar != null)
                    FernkampfwaffeListe.AddRange(Global.ContextInventar.FernkampfwaffeListe.Where(w => !FernkampfwaffeListe.Contains(w)).OrderBy(w => w.Name));
                OnChanged("FernkampfwaffeListe");
                if (FernkampfwaffeListe.Count > 0) {
                    IsFernkampfwaffevorhanden = Visibility.Visible;
                    OnChanged("IsFernkampfwaffevorhanden");
                }
                OnChanged("SchildListe");

                //Schild
                if (Global.ContextInventar != null)
                    SchildListe.AddRange(Global.ContextInventar.SchildListe.Where(w => !SchildListe.Contains(w)).OrderBy(w => w.Name));
                SchildListe = SchildListe;
                if (SchildListe.Count > 0) {
                    IsSchildVorhanden = Visibility.Visible;
                    OnChanged("IsSchildVorhanden");
                }
                OnChanged("SchildListe");

                //Rüstung
                if (Global.ContextInventar != null)
                    RuestungListe.AddRange(Global.ContextInventar.RuestungListe.Where(w => !RuestungListe.Contains(w)).OrderBy(w => w.Name));
                RuestungListe = RuestungListe;
                if (RuestungListe.Count > 0) {
                    IsRuestungVorhanden = Visibility.Visible;
                    OnChanged("IsRuestungVorhanden");
                }
                OnChanged("RuestungListe");                
                IsLoaded = true;
            }
        }
        private Model.Held_Ausrüstung CreateHeldZuAusruestung(Model.Held aHeld, Model.Ausrüstung aAusruestung) {
            Model.Held_Ausrüstung tmp = new Model.Held_Ausrüstung();
            tmp.Held = aHeld;
            tmp.HeldGUID = aHeld.HeldGUID;

            tmp.Ausrüstung = aAusruestung;
            tmp.Ausrüstung.AusrüstungGUID = aAusruestung.AusrüstungGUID;

            if (aAusruestung.Talente.Count() > 0) {
                tmp.Talent = aAusruestung.Talente.FirstOrDefault();
                tmp.TalentGUID = aAusruestung.Talente.FirstOrDefault().TalentGUID;
            }

            tmp.Angelegt = false;
            tmp.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.Name == "Rucksack").FirstOrDefault();
            tmp.Anzahl = 1;
            return tmp;
        }
        private NahkampfItem CreateItemVonNahkampfwaffe(Model.Waffe aNahkampfwaffe) {
            NahkampfItem tmpItem = new NahkampfItem(CreateHeldZuAusruestung(SelectedHeld, aNahkampfwaffe.Ausrüstung), aNahkampfwaffe);
            tmpItem.Trageort = "Rucksack";
            tmpItem.RemoveItem += (s, e) => { RemoveAusruestung(s); };
            return tmpItem;
        }
        private FernkampfItem CreateItemVonFernkampfwaffe(Model.Fernkampfwaffe aFernkampfwaffe) {
            FernkampfItem tmpItem = new FernkampfItem(CreateHeldZuAusruestung(SelectedHeld, aFernkampfwaffe.Ausrüstung), aFernkampfwaffe);
            tmpItem.RemoveItem += (s, e) => { RemoveAusruestung(s); };
            tmpItem.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.Name == "Rucksack").FirstOrDefault();
            return tmpItem;
        }
        private SchildItem CreateItemVonSchild(Model.Schild aSchild) {
            SchildItem tmpItem = new SchildItem(CreateHeldZuAusruestung(SelectedHeld, aSchild.Ausrüstung), aSchild);
            tmpItem.RemoveItem += (s, e) => { RemoveAusruestung(s); };
            tmpItem.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.Name == "Rucksack").FirstOrDefault();
            return tmpItem;
        }
        private RuestungItem CreateItemVonRuestung(Model.Rüstung aRuestung) {
            RuestungItem tmpItem = new RuestungItem(CreateHeldZuAusruestung(SelectedHeld, aRuestung.Ausrüstung), aRuestung);
            tmpItem.RemoveItem += (s, e) => { RemoveAusruestung(s); };
            tmpItem.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.Name == "Rucksack").FirstOrDefault();
            return tmpItem;
        }
        #endregion

        #region //EVENTS

        void SelectedHeldChanged() {
            //FIXME Hack
            if (!ListenToChangeEvents)
                return;

            SelectedHeld = Global.SelectedHeld;
            if (IsLoaded == false) {
                LoadDaten();
            }

            AktuellesGewicht = 0;
            if (SelectedHeld != null) {
                AktuelleTragkraft = SelectedHeld.Tragkraft;
                AktuellesGewicht = SelectedHeld.BerechneAusrüstungsGewicht();
            } else {
                AktuelleTragkraft = 0;
                AktuellesGewichtInProzentZuTragkraft = 0;
                AktuellesGewichtProzentResultierendeBE = 0;
            }

            //Nahkampf
            HeldNahkampfWaffeImInventar.Clear();
            if (SelectedHeld != null) {
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Waffe != null)) {
                    NahkampfItem value = new NahkampfItem(item, item.Ausrüstung.Waffe);
                    value.RemoveItem += (s, e) => { RemoveAusruestung(s); };
                    HeldNahkampfWaffeImInventar.Add(value);
                }
                if ((SelectedFilterIndex == 0 ||
                    SelectedFilterIndex == 1) &&
                    HeldNahkampfWaffeImInventar.Count() > 0) {
                    IsNahkampfwaffevorhanden = Visibility.Visible;
                } else {
                    IsNahkampfwaffevorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldNahkampfWaffeImInventar");
            } else {
                IsNahkampfwaffevorhanden = Visibility.Collapsed;
                OnChanged("HeldNahkampfWaffeImInventar");
            }

            //Schild
            HeldSchildImInventar.Clear();
            if (SelectedHeld != null) {
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Schild != null)) {
                    SchildItem value = new SchildItem(item, item.Ausrüstung.Schild);
                    value.RemoveItem += (s, e) => { RemoveAusruestung(s); };
                    HeldSchildImInventar.Add(value);
                }
                if ((SelectedFilterIndex == 0 ||
                    SelectedFilterIndex == 3) &&
                   HeldSchildImInventar.Count() > 0) {
                    IsSchildVorhanden = Visibility.Visible;
                } else {
                    IsSchildVorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldSchildImInventar");
            } else {
                IsSchildVorhanden = Visibility.Collapsed;
                OnChanged("HeldSchildImInventar");
            }

            //Fernkampfwaffe
            HeldFernkampfwaffeImInventar.Clear();
            if (SelectedHeld != null) {
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Fernkampfwaffe != null)) {
                    FernkampfItem value = new FernkampfItem(item, item.Ausrüstung.Fernkampfwaffe);
                    value.RemoveItem += (s, e) => { RemoveAusruestung(s); };
                    HeldFernkampfwaffeImInventar.Add(value);
                }
                if ((SelectedFilterIndex == 0 ||
                    SelectedFilterIndex == 2) && HeldFernkampfwaffeImInventar.Count() > 0) {
                    IsFernkampfwaffevorhanden = Visibility.Visible;
                } else {
                    IsFernkampfwaffevorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldFernkampfwaffeImInventar");
            } else {
                IsFernkampfwaffevorhanden = Visibility.Collapsed;
                OnChanged("HeldFernkampfwaffeImInventar");
            }

            //Rüstung
            HeldRuestungImInventar.Clear();
            if (SelectedHeld != null) {
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Rüstung != null)) {
                    RuestungItem value = new RuestungItem(item, item.Ausrüstung.Rüstung);
                    value.RemoveItem += (s, e) => { RemoveAusruestung(s); };
                    HeldRuestungImInventar.Add(value);
                }
                if ((SelectedFilterIndex == 0 ||
                    SelectedFilterIndex == 4) &&
                    HeldRuestungImInventar.Count() > 0) {
                    IsRuestungVorhanden = Visibility.Visible;
                } else {
                    IsRuestungVorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldRuestungImInventar");
            } else {
                IsRuestungVorhanden = Visibility.Collapsed;
                OnChanged("HeldRuestungImInventar");
            }

            if (SelectedHeld != null)
                SelectedHeld.BE = HeldRuestungImInventar.Sum(item => item.EntityR.BE);
        }

        #region //--ADD

        void AddNahkampfwaffe(object sender) {
            if (SelectedNahkampfwaffe != null && SelectedHeld != null) {
                foreach (var item in HeldNahkampfWaffeImInventar) {
                    if (item.EntityNW.WaffeGUID == SelectedNahkampfwaffe.WaffeGUID) {
                        item.EntityHA.Anzahl++;
                        OnChanged("HeldNahkampfWaffeImInventar");
                        AktuellesGewicht += SelectedNahkampfwaffe.Gewicht;
                        return;
                    }
                }
                NahkampfItem newItem = CreateItemVonNahkampfwaffe(SelectedNahkampfwaffe);
                HeldNahkampfWaffeImInventar.Add(newItem);
                OnChanged("HeldNahkampfWaffeImInventar");
                IsNahkampfwaffevorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                AktuellesGewicht += SelectedNahkampfwaffe.Gewicht;
            }
        }
        void AddFernkampfwaffe(object sender) {
            if (SelectedFernkampfwaffe != null && SelectedHeld != null) {
                foreach (var item in HeldFernkampfwaffeImInventar) {
                    if (item.EntityFW.FernkampfwaffeGUID == SelectedFernkampfwaffe.FernkampfwaffeGUID) {
                        item.EntityHA.Anzahl++;
                        OnChanged("HeldFernkampfwaffeImInventar");
                        AktuellesGewicht += SelectedFernkampfwaffe.Gewicht;
                        return;
                    }
                }
                FernkampfItem tmp = HeldFernkampfwaffeImInventar.Where(w => w.EntityHA.Ausrüstung.Fernkampfwaffe == SelectedFernkampfwaffe && w.EntityHA.HeldGUID == SelectedHeld.HeldGUID).FirstOrDefault();
                FernkampfItem newItem = CreateItemVonFernkampfwaffe(SelectedFernkampfwaffe);
                HeldFernkampfwaffeImInventar.Add(newItem);
                OnChanged("HeldFernkampfwaffeImInventar");
                IsFernkampfwaffevorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                AktuellesGewicht += SelectedFernkampfwaffe.Gewicht;
            }
        }
        void AddSchild(object sender) {
            if (SelectedSchild != null && SelectedHeld != null) {
                foreach (var item in HeldSchildImInventar) {
                    if (item.EntityS.SchildGUID == SelectedSchild.SchildGUID) {
                        item.EntityHA.Anzahl++;
                        OnChanged("HeldSchildImInventar");
                        AktuellesGewicht += SelectedSchild.Gewicht;
                        return;
                    }
                }
                SchildItem tmp = HeldSchildImInventar.Where(s => s.EntityHA.Ausrüstung.Schild == SelectedSchild && s.EntityHA.HeldGUID == SelectedHeld.HeldGUID).FirstOrDefault();
                SchildItem newItem = CreateItemVonSchild(SelectedSchild);
                HeldSchildImInventar.Add(newItem);
                OnChanged("HeldSchildImInventar");
                IsSchildVorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                AktuellesGewicht += SelectedSchild.Gewicht;
            }
        }
        void AddRuestung(object sender) {
            if (SelectedRuestung != null && SelectedHeld != null) {
                foreach (var item in HeldRuestungImInventar) {
                    if (item.EntityR.RüstungGUID == SelectedRuestung.RüstungGUID) {
                        item.EntityHA.Anzahl++;
                        OnChanged("HeldRuestungImInventar");
                        AktuellesGewicht += SelectedRuestung.Gewicht / 2;
                        break;
                    }
                }
                RuestungItem tmp = HeldRuestungImInventar.Where(s => s.EntityHA.Ausrüstung.Rüstung == SelectedRuestung && s.EntityHA.HeldGUID == SelectedHeld.HeldGUID).FirstOrDefault();
                RuestungItem newItem = CreateItemVonRuestung(SelectedRuestung);
                HeldRuestungImInventar.Add(newItem);
                SelectedHeld.BE += newItem.EntityR.BE;
                OnChanged("HeldRuestungImInventar");
                IsRuestungVorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                AktuellesGewicht += SelectedRuestung.Gewicht / 2;
            }
        }

        #endregion

        #region //--REMOVE

        void RemoveAusruestung(object sender) {
            if (sender != null && SelectedHeld != null) {

                if (sender is NahkampfItem) {
                    NahkampfItem item = HeldNahkampfWaffeImInventar.Where(value => value == (sender as NahkampfItem)).FirstOrDefault();
                    if (item != null) {
                        foreach (var invItem in HeldNahkampfWaffeImInventar) {
                            if (invItem.EntityNW.WaffeGUID == item.EntityNW.WaffeGUID) {
                                if (item.EntityHA.Anzahl <= 1)
                                    break;
                                item.EntityHA.Anzahl--;
                                OnChanged("HeldNahkampfWaffeImInventar");
                                AktuellesGewicht -= item.EntityNW.Gewicht;
                                return;
                            }
                        }
                        HeldNahkampfWaffeImInventar.Remove(item);
                        OnChanged("HeldNahkampfWaffeImInventar");
                        AktuellesGewicht -= item.EntityNW.Gewicht;
                        if (HeldNahkampfWaffeImInventar.Count() == 0) {
                            IsNahkampfwaffevorhanden = Visibility.Collapsed;
                        }
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);
                    }
                }

                if (sender is FernkampfItem) {
                    FernkampfItem item = HeldFernkampfwaffeImInventar.Where(value => value == (sender as FernkampfItem)).FirstOrDefault();
                    if (item != null) {
                        foreach (var invItem in HeldFernkampfwaffeImInventar) {
                            if (invItem.EntityFW.FernkampfwaffeGUID == item.EntityFW.FernkampfwaffeGUID) {
                                if (item.EntityHA.Anzahl <= 1)
                                    break;
                                item.EntityHA.Anzahl--;
                                OnChanged("HeldFernkampfwaffeImInventar");
                                AktuellesGewicht -= item.EntityFW.Gewicht;
                                return;
                            }
                        }
                        HeldFernkampfwaffeImInventar.Remove(item);
                        OnChanged("HeldFernkampfwaffeImInventar");
                        AktuellesGewicht -= item.EntityFW.Gewicht;
                        if (HeldFernkampfwaffeImInventar.Count() == 0) {
                            IsFernkampfwaffevorhanden = Visibility.Collapsed;
                        }
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);
                    }
                }

                if (sender is SchildItem) {
                    SchildItem item = HeldSchildImInventar.Where(value => value == (sender as SchildItem)).FirstOrDefault();
                    if (item != null) {
                        foreach (var invItem in HeldSchildImInventar) {
                            if (invItem.EntityS.SchildGUID == item.EntityS.SchildGUID) {
                                if (item.EntityHA.Anzahl <= 1)
                                    break;
                                item.EntityHA.Anzahl--;
                                OnChanged("HeldSchildImInventar");
                                AktuellesGewicht -= item.EntityS.Gewicht;
                                return;
                            }
                        }
                        HeldSchildImInventar.Remove(item);
                        OnChanged("HeldSchildImInventar");
                        AktuellesGewicht -= item.EntityS.Gewicht;
                        if (HeldSchildImInventar.Count() == 0) {
                            IsSchildVorhanden = Visibility.Collapsed;
                        }
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);

                    }
                }

                if (sender is RuestungItem) {
                    RuestungItem item = HeldRuestungImInventar.Where(value => value == (sender as RuestungItem)).FirstOrDefault();
                    if (item != null) {
                        foreach (var invItem in HeldRuestungImInventar) {
                            if (invItem.EntityR.RüstungGUID == item.EntityR.RüstungGUID) {
                                if (item.EntityHA.Anzahl <= 1)
                                    break;
                                item.EntityHA.Anzahl--;
                                OnChanged("HeldRuestungImInventar");
                                AktuellesGewicht -= item.EntityR.Gewicht / 2;
                                return;
                            }
                        }
                        HeldRuestungImInventar.Remove(item);
                        OnChanged("HeldRuestungImInventar");
                        AktuellesGewicht -= item.EntityR.Gewicht / 2;
                        SelectedHeld.BE -= item.EntityR.BE;
                        if (HeldRuestungImInventar.Count() == 0) {
                            IsRuestungVorhanden = Visibility.Collapsed;
                        }
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);

                    }
                }
            }
        }

        #endregion

        #endregion

        private bool listenToChangeEvents = true;

        public bool ListenToChangeEvents {
            get { return listenToChangeEvents; }
            set { listenToChangeEvents = value; SelectedHeldChanged(); }
        }


    }

    #region //SUBKLASSEN

    public class NahkampfItem : Base.ViewModelBase {

        #region //FELDER

        //UI
        private bool ausgeruestet;
        private string trageort;
        private string talente;
        private string tp;
        private string tpKK;
        private string wM;

        //Commands
        private Base.CommandBase onRemoveNahkampfwaffe;

        #endregion

        #region //EIGENSCHAFTEN

        //Intern
        public Model.Held_Ausrüstung EntityHA { get; set; }
        public Model.Waffe EntityNW { get; set; }

        //UI
        public bool Ausgeruestet {
            get { return ausgeruestet; }
            set {
                ausgeruestet = value;
                OnChanged("Ausgeruestet");
                EntityHA.Angelegt = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }
        public string Trageort {
            get { return trageort; }
            set {
                trageort = value;
                OnChanged("Trageort");
                //EntityHA.Trageort = new Model.Trageort() { ;                
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }
        public string Talente { get { return talente; } set { talente = value; OnChanged("Talente"); } }
        public string TP { get { return tp; } set { tp = value; OnChanged("TP"); } }
        public string TPKK { get { return tpKK; } set { tpKK = value; OnChanged("TPKK"); } }
        public string WM { get { return wM; } set { wM = value; OnChanged("WM"); } }

        //Commands
        public Base.CommandBase OnRemoveNahkampfwaffe {
            get { return onRemoveNahkampfwaffe; }
        }

        #endregion

        #region //KONSTRUKTOR

        public NahkampfItem(Model.Held_Ausrüstung aHA, Model.Waffe aNW) {
            bool isFirst = true;
            string talent = "";
            string tp = "";
            string tpkk = "";

            foreach (var value in aNW.Talent) {
                if (isFirst) {
                    talent += value.Talentname;
                    isFirst = false;
                } else {
                    talent += " / " + value.Talentname;
                }
            }
            tp = aNW.TPWürfelAnzahl + "W" + aNW.TPWürfel + "+" + aNW.TPBonus;
            tpkk = aNW.TPKKSchwelle + "/" + aNW.TPKKSchritt;
            //trageort = aHA.Ort;
            ausgeruestet = aHA.Angelegt;

            this.EntityHA = aHA;
            this.EntityNW = aNW;
            this.Talente = talent;
            this.TP = tp;
            this.TPKK = tpkk;
            this.WM = aNW.WMAT + "/" + aNW.WMPA;

            onRemoveNahkampfwaffe = new Base.CommandBase(RemoveNahkampfwaffe, null);
        }

        #endregion

        #region //EVENTS

        public event EventHandler RemoveItem;
        void RemoveNahkampfwaffe(object sender) {
            if (RemoveItem != null) {
                RemoveItem(this, new EventArgs());
            }

        }

        #endregion

    }

    public class SchildItem : Base.ViewModelBase {

        #region //FELDER

        //Intern
        private Model.Held_Ausrüstung entityHA;
        private Model.Schild entityS;

        //UI
        private bool ausgeruestet;
        private Model.Trageort trageort;
        private string wM;

        //Commands
        private Base.CommandBase onRemoveSchild;

        #endregion

        #region //EIGENSCHAFTEN

        //Intern
        public Model.Held_Ausrüstung EntityHA { get { return entityHA; } set { entityHA = value; OnChanged("EntityHA"); } }
        public Model.Schild EntityS { get { return entityS; } set { entityS = value; OnChanged("EntityS"); } }

        //UI
        public bool Ausgeruestet {
            get { return ausgeruestet; }
            set {
                ausgeruestet = value;
                OnChanged("Ausgeruestet");
                EntityHA.Angelegt = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }
        public Model.Trageort Trageort {
            get { return trageort; }
            set {
                trageort = value;
                OnChanged("Trageort");
                EntityHA.Trageort = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }
        public string WM { get { return wM; } set { wM = value; OnChanged("WM"); } }

        //Commands
        public Base.CommandBase OnRemoveSchild {
            get { return onRemoveSchild; }
        }

        #endregion

        #region //KONSTRUKTOR

        public SchildItem(Model.Held_Ausrüstung aHA, Model.Schild aS) {
            this.EntityHA = aHA;
            this.EntityS = aS;
            this.WM = aS.WMAT.ToString() + "/" + aS.WMPA.ToString();
            trageort = aHA.Trageort;
            ausgeruestet = aHA.Angelegt;

            onRemoveSchild = new Base.CommandBase(RemoveSchild, null);
        }

        #endregion

        #region //EVENTS

        public event EventHandler RemoveItem;
        void RemoveSchild(object sender) {
            if (RemoveItem != null) {
                RemoveItem(this, new EventArgs());
            }

        }

        #endregion

    }

    public class FernkampfItem : Base.ViewModelBase {

        #region //FELDER

        //Intern
        private Model.Held_Ausrüstung entityHA;
        private Model.Fernkampfwaffe entityFW;

        //UI
        private bool ausgeruestet;
        private Model.Trageort trageort;
        private string talente;
        private string name;
        private string tp;
        private string gewicht;
        private string preis;

        //Commands
        private Base.CommandBase onRemoveFernkampfwaffe;

        #endregion

        #region //EIGENSCHAFTEN

        //Intern
        public Model.Held_Ausrüstung EntityHA { get { return entityHA; } set { entityHA = value; OnChanged("EntityHA"); } }
        public Model.Fernkampfwaffe EntityFW { get { return entityFW; } set { entityFW = value; OnChanged("EntityFW"); } }

        //UI
        public bool Ausgeruestet {
            get { return ausgeruestet; }
            set {
                ausgeruestet = value;
                OnChanged("Ausgeruestet");
                EntityHA.Angelegt = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }
        public Model.Trageort Trageort {
            get { return trageort; }
            set {
                trageort = value;
                OnChanged("Trageort");
                EntityHA.Trageort = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }
        public string Talente { get { return talente; } set { talente = value; OnChanged("Talente"); } }
        public string Name { get { return name; } set { name = value; OnChanged("Name"); } }
        public string TP { get { return tp; } set { tp = value; OnChanged("TP"); } }
        public string Gewicht { get { return gewicht; } set { gewicht = value; OnChanged("Gewicht"); } }
        public string Preis { get { return preis; } set { preis = value; OnChanged("Preis"); } }

        //Commands
        public Base.CommandBase OnRemoveNahkampfwaffe {
            get { return onRemoveFernkampfwaffe; }
        }

        #endregion

        #region //KONSTRUKTOR

        public FernkampfItem(Model.Held_Ausrüstung aHA, Model.Fernkampfwaffe aFW) {
            bool isFirst = true;
            string talent = "";
            string name = "";
            string tp = "";
            string gewicht = "";

            foreach (var value in aFW.Talent) {
                if (isFirst) {
                    talent += value.Talentname;
                    isFirst = false;
                } else {
                    talent += " / " + value.Talentname;
                }
            }
            name = aFW.Name + ((aFW.Improvisiert) ? "**" : "");
            tp = aFW.TPWürfelAnzahl + "W" + aFW.TPWürfel + "+" + aFW.TPBonus + ((aFW.Verwundend) ? "*" : "");
            gewicht = aFW.Gewicht.ToString() ?? "0";
            trageort = aHA.Trageort;
            ausgeruestet = aHA.Angelegt;

            this.EntityHA = aHA;
            this.EntityFW = aFW;
            this.Talente = talent;
            this.Name = name;
            this.TP = tp;
            this.Gewicht = gewicht;
            onRemoveFernkampfwaffe = new Base.CommandBase(RemoveFernkampfwaffe, null);
        }

        #endregion

        #region //EVENTS

        public event EventHandler RemoveItem;
        void RemoveFernkampfwaffe(object sender) {
            if (RemoveItem != null) {
                RemoveItem(this, new EventArgs());
            }

        }

        #endregion

    }

    public class RuestungItem : Base.ViewModelBase {

        #region //FELDER

        //Intern
        private Model.Held_Ausrüstung entityHA;
        private Model.Rüstung entityR;

        //UI
        private bool ausgeruestet;
        private Model.Trageort trageort;

        //Commands
        private Base.CommandBase onRemoveRuestung;

        #endregion

        #region //EIGENSCHAFTEN

        //Intern
        public Model.Held_Ausrüstung EntityHA { get { return entityHA; } set { entityHA = value; OnChanged("EntityHA"); } }
        public Model.Rüstung EntityR { get { return entityR; } set { entityR = value; OnChanged("EntityR"); } }

        //UI
        public bool Ausgeruestet {
            get { return ausgeruestet; }
            set {
                ausgeruestet = value;
                OnChanged("Ausgeruestet");
                EntityHA.Angelegt = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }
        public Model.Trageort Trageort {
            get { return trageort; }
            set {
                trageort = value;
                OnChanged("Trageort");
                EntityHA.Trageort = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }

        //Commands
        public Base.CommandBase OnRemoveRuestung {
            get { return onRemoveRuestung; }
        }

        #endregion

        #region //KONSTRUKTOR

        public RuestungItem(Model.Held_Ausrüstung aHA, Model.Rüstung aR) {
            this.EntityHA = aHA;
            this.EntityR = aR;
            trageort = aHA.Trageort;
            ausgeruestet = aHA.Angelegt;

            onRemoveRuestung = new Base.CommandBase(RemoveRuestung, null);
        }

        #endregion

        #region //EVENTS

        public event EventHandler RemoveItem;
        void RemoveRuestung(object sender) {
            if (RemoveItem != null) {
                RemoveItem(this, new EventArgs());
            }

        }

        #endregion

    }

    #endregion

}

////////////////////////
//DSA Regelwerke
////////////////////////
//RüstungsBE + 

//Rüstung angelegt 1/2

//Tragkraft in Stein (1Stein = 40 Unze) = KK => implemented in OnHeldCHanged

//pro angefangene 50% über Tragkraft steigt BE um 1

//Strategisch prop Stunde marsch erschöpfung + 1 erleiden
//Übersteigt Gewicht doppelte Tragkraft muss KO-Runden 1 Runde gerastet werden, Sprint nicht mehr möglich

//Übersteigt 4x Tragkraft muss KO * 5 Schritt 1Min mit absetzen verschnauft werden

//+ BE durch Erschöpfung
//WdS (Bei Ausdauer)

////////////////////////
//CodeGenerated-Animation
////////////////////////
//public void AnimateLabelRotationInCode( object sender,
//  RoutedEventArgs e )
//{
//  DoubleAnimation oLabelAngleAnimation
//    = new DoubleAnimation();

//  oLabelAngleAnimation.From = 0;
//  oLabelAngleAnimation.To = 360;
//  oLabelAngleAnimation.Duration
//    = new Duration( new TimeSpan( 0, 0, 0, 0, 500 ) );
//  oLabelAngleAnimation.RepeatBehavior = new RepeatBehavior( 4 );

//  RotateTransform oTransform
//    = (RotateTransform) lblHello.RenderTransform;
//  oTransform.BeginAnimation( RotateTransform.AngleProperty,
//    oLabelAngleAnimation );
//}