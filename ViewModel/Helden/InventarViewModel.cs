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
using E = MeisterGeister.Logic.Einstellung.Einstellungen;

namespace MeisterGeister.ViewModel.Inventar {
    public class InventarViewModel : Base.ViewModelBase
    {

        #region //FELDER

        //Intern
        const string TALENTNAHKAMPFWAFFEUNTERKATEGORIE = "Bewaffneter Nahkampf";
        const string TALENTNAHKAMPFWAFFEATTECHNIK = "Bewaffnete AT-Technik";
        const string TALENTFERNKAMPFWAFFEUNTERKATEGORIE = "Fernkampf";
        const string FILTERDEAKTIVIEREN = " Alle ";
        bool IsLoaded = false;
        private bool _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;

        //UI
        private Visibility isNahkampfwaffevorhanden = Visibility.Hidden;
        private Visibility isFernkampfwaffevorhanden = Visibility.Hidden;
        private Visibility isSchildVorhanden = Visibility.Hidden;
        private Visibility isRuestungVorhanden = Visibility.Hidden;
        private Visibility isSonstigesVorhanden = Visibility.Hidden;
        private Visibility isRuestungEinfachEingeben = Visibility.Hidden;
        private Visibility isBEEingebenVisibility = Visibility.Hidden;
        private Visibility isUeberlastungEingebenVisibility = Visibility.Hidden;

        private bool isAllSelected;
        private bool isNahkampfWaffeSelected;
        private bool isFernkampfwaffeSelected;
        private bool isSchildSelected;
        private bool isRuestungSelected;
        private bool isSonstigesSelected;
        private bool isRuestungBerechnungZonen = false;
        private bool isRuestungBerechnungEinfach = false;
        private bool isBehinderungEingeben = false;
        private bool isUeberlastungEingeben = false;

        private int selectedFilterIndex = 0;

        //private double aktuelleTragkraft = 0;

        private Model.Held selectedHeld;
        private Model.Talent selectedNahkampfwaffeTalent;
        private Model.Talent selectedFernkampfwaffeTalent;
        private Model.Waffe selectedNahkampfwaffe;
        private Model.Fernkampfwaffe selectedFernkampfwaffe;
        private Model.Schild selectedSchild;
        private Model.Rüstung selectedRuestung;

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
        private ObservableCollection<InventarItem> heldSonstigesImInventar = new ObservableCollection<InventarItem>();

        //Commands
        private Base.CommandBase onAddNahkampfwaffe;
        private Base.CommandBase onAddFernkampfwaffe;
        private Base.CommandBase onAddSchild;
        private Base.CommandBase onAddRuestung;

        #endregion

        #region //EIGENSCHAFTEN

        //UI      
        public bool IsReadOnly {
            get { return _isReadOnly; }
        }
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
        public Visibility IsSonstigesVorhanden
        {
            get { return isSonstigesVorhanden; }
            set
            {
                isSonstigesVorhanden = value;
                OnChanged("IsSonstigesVorhanden");
            }
        }
        public Visibility IsRuestungEinfachEingeben {
            get { return isRuestungEinfachEingeben; }
            set {
                isRuestungEinfachEingeben = value;
                OnChanged("IsRuestungEinfachEingeben");
            }
        }
        public Visibility IsBEEingebenVisibility {
            get { return isBEEingebenVisibility; }
            set {
                isBEEingebenVisibility = value;
                OnChanged("IsBEEingebenVisibility");
            }
        }
        public Visibility IsUeberlastungEingebenVisibility {
            get { return isUeberlastungEingebenVisibility; }
            set {
                isUeberlastungEingebenVisibility = value;
                OnChanged("IsUeberlastungEingebenVisibility");
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
                if (value && HeldSonstigesImInventar.Count() > 0)
                {
                    IsSonstigesVorhanden = Visibility.Visible;
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
                    IsSonstigesVorhanden = Visibility.Collapsed;
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
                    IsSonstigesVorhanden = Visibility.Collapsed;
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
                    IsSonstigesVorhanden = Visibility.Collapsed;
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
                    IsSonstigesVorhanden = Visibility.Collapsed;
                }
                OnChanged("IsRuestungSelected");
            }
        }
        public bool IsSonstigesSelected
        {
            get { return isSonstigesSelected; }
            set
            {
                isSonstigesSelected = value;
                if (value)
                {
                    if (value && HeldSonstigesImInventar.Count() > 0)
                    {
                        IsSonstigesVorhanden = Visibility.Visible;
                    }
                    IsFernkampfwaffevorhanden = Visibility.Collapsed;
                    IsNahkampfwaffevorhanden = Visibility.Collapsed;
                    IsSchildVorhanden = Visibility.Collapsed;
                    IsRuestungVorhanden = Visibility.Collapsed;
                }
                OnChanged("IsSonstigesSelected");
            }
        }
        public bool IsRuestungBerechnungEinfach {
            get { return !(isRuestungBerechnungEinfach || E.IsReadOnly); }
            set {
                isRuestungBerechnungEinfach = value;
                OnChanged("IsRuestungBerechnungEinfach");
            }
        }
        public bool IsRuestungBerechnungZonen {
            get { return isRuestungBerechnungZonen; }
            set {
                isRuestungBerechnungZonen = value;
                OnChanged("IsRuestungBerechnungZonen");
            }
        }
        public bool IsBehinderungEingeben {
            get { return !(isBehinderungEingeben || E.IsReadOnly); }
            set {
                isBehinderungEingeben = value;
                OnChanged("IsBehinderungEingeben");
            }
        }
        public bool IsUeberlastungEingeben {
            get { return !(isUeberlastungEingeben || E.IsReadOnly); }
            set {
                isUeberlastungEingeben = value;
                OnChanged("IsUeberlastungEingeben");
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
        //public double AktuelleTragkraft {
        //    get { return aktuelleTragkraft; }
        //    set {
        //        aktuelleTragkraft = value;
        //        OnChanged("AktuelleTragkraft");
        //    }
        //}
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
        public ObservableCollection<InventarItem> HeldSonstigesImInventar
        {
            get { return heldSonstigesImInventar; }
            set
            {
                heldSonstigesImInventar = value;
                if (heldSonstigesImInventar.Count() != 0)
                {
                    IsSonstigesVorhanden = Visibility.Visible;
                }
                else
                {
                    IsSonstigesVorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldSonstigesImInventar");
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
            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("BEBerechnung", ""));
            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("RSBerechnung", ""));
            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("UeberlastungBerechnung", ""));
            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("IsMitUeberlastung", ""));            

            onAddNahkampfwaffe = new Base.CommandBase(AddNahkampfwaffe, null);
            onAddFernkampfwaffe = new Base.CommandBase(AddFernkampfwaffe, null);
            onAddSchild = new Base.CommandBase(AddSchild, null);
            onAddRuestung = new Base.CommandBase(AddRuestung, null);

            SelectedFilterIndex = 0;

            if (IsLoaded == false) {
                IsAllSelected = true;
            }
        }
        #endregion

        #region //Public Methoden

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += SelectedHeldChanged;
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;
            E.EinstellungChanged += EinstellungenChangedHandler;
            SelectedHeldChanged(this, new EventArgs());
        }
        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Global.HeldSelectionChanged -= SelectedHeldChanged;
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged -= IsReadOnlyChanged;
            E.EinstellungChanged -= EinstellungenChangedHandler;
        }

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
        #endregion

        #region //Private Methoden
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
        private void IsReadOnlyChanged(object sender, EventArgs e) {
            _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }
        void EinstellungenChangedHandler(object sender, MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs e) {
            switch (e.PropertyName) {
                case "RSBerechnung":
                    switch (E.RSBerechnung) {
                        //Automatisch Zonen / Automatisch Einfach = 0,3                  
                        case 1: //Einfach                    
                            IsRuestungBerechnungEinfach = true;
                            IsRuestungBerechnungZonen = false;
                            IsRuestungEinfachEingeben = Visibility.Visible;
                            break;
                        case 2: //Zonen                    
                            IsRuestungBerechnungEinfach = false;
                            IsRuestungBerechnungZonen = true;
                            IsRuestungEinfachEingeben = Visibility.Hidden;
                            break;
                        default:
                            IsRuestungBerechnungEinfach = false;
                            IsRuestungBerechnungZonen = false;
                            IsRuestungEinfachEingeben = Visibility.Hidden;
                            if (SelectedHeld != null) {
                                SelectedHeld.BerechneRüstungswerte();
                            }
                            break;
                    }
                    break;
                case "BEBerechnung":
                    //Einstellung für BE-Berechnung
                    switch (E.BEBerechnung) {
                        //Automatisch
                        case 0:
                            IsBehinderungEingeben = false;
                            IsBEEingebenVisibility = Visibility.Hidden;
                            if (selectedHeld != null) {
                                SelectedHeld.BerechneBehinderung();
                            }
                            break;
                        //Eingegeben
                        case 1:
                            IsBehinderungEingeben = true;
                            IsBEEingebenVisibility = Visibility.Visible;
                            break;
                        default:
                            break;
                    }
                    break;
                case "UeberlastungBerechnung":
                    //Einstellung für Überlastung
                    switch (E.UeberlastungBerechnung) {
                        //Automatisch
                        case 0:
                            IsUeberlastungEingeben = false;
                            IsUeberlastungEingebenVisibility = Visibility.Hidden;
                            if (selectedHeld != null) {                                
                                SelectedHeld.BerechneUeberlastung();
                            }
                            break;
                        //Eingegeben
                        case 1:
                            IsUeberlastungEingeben = true;
                            IsUeberlastungEingebenVisibility = Visibility.Visible;
                            break;
                        default:
                            break;
                    }
                    break;
                case "IsMitUeberlastung":
                    switch (E.IsMitUeberlastung) {                        
                        case true:                            
                            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("UeberlastungBerechnung", ""));
                            break;
                        case false:
                            IsUeberlastungEingeben = false;
                            IsUeberlastungEingebenVisibility = Visibility.Hidden;                            
                            //if (selectedHeld != null) {
                            //    SelectedHeld.Ueberlastung = 0;
                            //}
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
        void SelectedHeldChanged(object sender, EventArgs e) {

            SelectedHeld = Global.SelectedHeld;
            if (IsLoaded == false) {
                LoadDaten();
            }           

            //Nahkampf
            HeldNahkampfWaffeImInventar.Clear();
            if (SelectedHeld != null) {
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Waffe != null).OrderBy(i => i.Ausrüstung.Name)) {
                    NahkampfItem value = new NahkampfItem(item, item.Ausrüstung.Waffe);
                    value.RemoveItem += (s, ev) => { RemoveAusruestung(s); };
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
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Schild != null).OrderBy(i => i.Ausrüstung.Name))
                {
                    SchildItem value = new SchildItem(item, item.Ausrüstung.Schild);
                    value.RemoveItem += (s, ev) => { RemoveAusruestung(s); };
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
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Fernkampfwaffe != null).OrderBy(i => i.Ausrüstung.Name))
                {
                    FernkampfItem value = new FernkampfItem(item, item.Ausrüstung.Fernkampfwaffe);
                    value.RemoveItem += (s, ev) => { RemoveAusruestung(s); };
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
                foreach (Model.Held_Ausrüstung item in Global.ContextInventar.HeldZuAusruestungListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Ausrüstung != null && hw.Ausrüstung.Rüstung != null).OrderBy(i => i.Ausrüstung.Name))
                {
                    RuestungItem value = new RuestungItem(item, item.Ausrüstung.Rüstung);
                    value.RemoveItem += (s, ev) => { RemoveAusruestung(s); };
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

            if (SelectedHeld != null && E.BEBerechnung == 0) {
                SelectedHeld.BerechneBehinderung();
            }

            //Sonstiges
            heldSonstigesImInventar.Clear();
            if (SelectedHeld != null)
            {
                foreach (Model.Held_Inventar item in Global.ContextInventar.HeldZuInventarListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Inventar != null).OrderBy(i => i.Inventar.Name))
                {
                    InventarItem value = new InventarItem(item, item.Inventar);
                    value.RemoveItem += (s, ev) => { RemoveAusruestung(s); };
                    HeldSonstigesImInventar.Add(value);
                }
                if ((SelectedFilterIndex == 0 ||
                    SelectedFilterIndex == 5) &&
                    HeldSonstigesImInventar.Count() > 0)
                {
                    IsSonstigesVorhanden = Visibility.Visible;
                }
                else
                {
                    IsSonstigesVorhanden = Visibility.Collapsed;
                }
                OnChanged("HeldSonstigesImInventar");
            }
            else
            {
                IsSonstigesVorhanden = Visibility.Collapsed;
                OnChanged("HeldSonstigesImInventar");
            }

            if (SelectedHeld != null && E.BEBerechnung == 0)
            {
                SelectedHeld.BerechneBehinderung();
            }
        }        

        //--ADD
        void AddNahkampfwaffe(object sender) {
            if (SelectedNahkampfwaffe != null && SelectedHeld != null && !IsReadOnly) {
                foreach (var item in HeldNahkampfWaffeImInventar) {
                    if (item.EntityNW.WaffeGUID == SelectedNahkampfwaffe.WaffeGUID) {
                        item.EntityHA.Anzahl++;
                        OnChanged("HeldNahkampfWaffeImInventar");
                        SelectedHeld.BerechneAusruestungsGewicht();
                        return;
                    }
                }
                NahkampfItem newItem = CreateItemVonNahkampfwaffe(SelectedNahkampfwaffe);
                HeldNahkampfWaffeImInventar.Add(newItem);
                OnChanged("HeldNahkampfWaffeImInventar");
                IsNahkampfwaffevorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }
        void AddFernkampfwaffe(object sender) {
            if (SelectedFernkampfwaffe != null && SelectedHeld != null && !IsReadOnly) {
                foreach (var item in HeldFernkampfwaffeImInventar) {
                    if (item.EntityFW.FernkampfwaffeGUID == SelectedFernkampfwaffe.FernkampfwaffeGUID) {
                        item.EntityHA.Anzahl++;
                        OnChanged("HeldFernkampfwaffeImInventar");
                        SelectedHeld.BerechneAusruestungsGewicht();
                        return;
                    }
                }
                FernkampfItem tmp = HeldFernkampfwaffeImInventar.Where(w => w.EntityHA.Ausrüstung.Fernkampfwaffe == SelectedFernkampfwaffe && w.EntityHA.HeldGUID == SelectedHeld.HeldGUID).FirstOrDefault();
                FernkampfItem newItem = CreateItemVonFernkampfwaffe(SelectedFernkampfwaffe);
                HeldFernkampfwaffeImInventar.Add(newItem);
                OnChanged("HeldFernkampfwaffeImInventar");
                IsFernkampfwaffevorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }
        void AddSchild(object sender) {
            if (SelectedSchild != null && SelectedHeld != null && !IsReadOnly) {
                foreach (var item in HeldSchildImInventar) {
                    if (item.EntityS.SchildGUID == SelectedSchild.SchildGUID) {
                        item.EntityHA.Anzahl++;
                        OnChanged("HeldSchildImInventar");
                        SelectedHeld.BerechneAusruestungsGewicht();
                        return;
                    }
                }
                SchildItem tmp = HeldSchildImInventar.Where(s => s.EntityHA.Ausrüstung.Schild == SelectedSchild && s.EntityHA.HeldGUID == SelectedHeld.HeldGUID).FirstOrDefault();
                SchildItem newItem = CreateItemVonSchild(SelectedSchild);
                HeldSchildImInventar.Add(newItem);
                OnChanged("HeldSchildImInventar");
                IsSchildVorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }
        void AddRuestung(object sender) {
            if (SelectedRuestung != null && SelectedHeld != null && !IsReadOnly) {
                foreach (var item in HeldRuestungImInventar) {
                    if (item.EntityR.RüstungGUID == SelectedRuestung.RüstungGUID) {
                        item.EntityHA.Anzahl++;

                        if (E.RSBerechnung == 0 ||
                            E.RSBerechnung == 3) {
                            SelectedHeld.BerechneRüstungswerte();
                        }

                        if (SelectedHeld != null && E.BEBerechnung == 0) {
                            SelectedHeld.BerechneBehinderung();
                        }
                        OnChanged("HeldRuestungImInventar");
                        SelectedHeld.BerechneAusruestungsGewicht();
                        return;
                    }
                }
                RuestungItem tmp = HeldRuestungImInventar.Where(s => s.EntityHA.Ausrüstung.Rüstung == SelectedRuestung && s.EntityHA.HeldGUID == SelectedHeld.HeldGUID).FirstOrDefault();
                RuestungItem newItem = CreateItemVonRuestung(SelectedRuestung);
                HeldRuestungImInventar.Add(newItem);

                if (E.RSBerechnung == 0 ||
                    E.RSBerechnung == 3) {
                    SelectedHeld.BerechneRüstungswerte();
                }

                if (SelectedHeld != null && E.BEBerechnung == 0) {
                    SelectedHeld.BerechneBehinderung();
                }
                OnChanged("HeldRuestungImInventar");
                IsRuestungVorhanden = Visibility.Visible;
                Global.ContextInventar.InsertHeldAusruestung(newItem.EntityHA);
                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }
        
        //--REMOVE
        void RemoveAusruestung(object sender) {
            if (sender != null && SelectedHeld != null && !IsReadOnly) {

                if (sender is NahkampfItem) {
                    NahkampfItem item = HeldNahkampfWaffeImInventar.Where(value => value == (sender as NahkampfItem)).FirstOrDefault();
                    if (item != null) {
                        foreach (var invItem in HeldNahkampfWaffeImInventar) {
                            if (invItem.EntityNW.WaffeGUID == item.EntityNW.WaffeGUID) {
                                if (item.EntityHA.Anzahl <= 1)
                                    break;
                                item.EntityHA.Anzahl--;
                                OnChanged("HeldNahkampfWaffeImInventar");
                                SelectedHeld.BerechneAusruestungsGewicht();
                                return;
                            }
                        }
                        HeldNahkampfWaffeImInventar.Remove(item);
                        OnChanged("HeldNahkampfWaffeImInventar");
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);                        
                        SelectedHeld.BerechneAusruestungsGewicht();
                        if (HeldNahkampfWaffeImInventar.Count() == 0) {
                            IsNahkampfwaffevorhanden = Visibility.Collapsed;
                        }
                        
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
                                SelectedHeld.BerechneAusruestungsGewicht();
                                return;
                            }
                        }
                        HeldFernkampfwaffeImInventar.Remove(item);
                        OnChanged("HeldFernkampfwaffeImInventar");
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);
                        SelectedHeld.BerechneAusruestungsGewicht();
                        if (HeldFernkampfwaffeImInventar.Count() == 0) {
                            IsFernkampfwaffevorhanden = Visibility.Collapsed;
                        }                        
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
                                SelectedHeld.BerechneAusruestungsGewicht();
                                return;
                            }
                        }
                        HeldSchildImInventar.Remove(item);
                        OnChanged("HeldSchildImInventar");
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);
                        SelectedHeld.BerechneAusruestungsGewicht();
                        if (HeldSchildImInventar.Count() == 0) {
                            IsSchildVorhanden = Visibility.Collapsed;
                        }                        
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
                                if (E.RSBerechnung == 0 ||
                                    E.RSBerechnung == 3) {
                                    SelectedHeld.BerechneRüstungswerte();
                                }

                                if (SelectedHeld != null && E.BEBerechnung == 0) {
                                    SelectedHeld.BerechneBehinderung();
                                }
                                OnChanged("HeldRuestungImInventar");
                                SelectedHeld.BerechneAusruestungsGewicht();
                                return;
                            }
                        }
                        HeldRuestungImInventar.Remove(item);
                        if (E.RSBerechnung == 0 ||
                            E.RSBerechnung == 3) {
                            SelectedHeld.BerechneRüstungswerte();
                        }
                        OnChanged("HeldRuestungImInventar");                        
                        Global.ContextInventar.HeldZuAusruestungListe.Remove(item.EntityHA);
                        Global.ContextInventar.RemoveAusruestungVonHeld(item.EntityHA);
                        SelectedHeld.BerechneAusruestungsGewicht();
                        if (SelectedHeld != null && E.BEBerechnung == 0) {
                            SelectedHeld.BerechneBehinderung();
                        }
                        if (HeldRuestungImInventar.Count() == 0) {
                            IsRuestungVorhanden = Visibility.Collapsed;
                        }                        
                    }
                }

                if (sender is InventarItem)
                {
                    InventarItem item = HeldSonstigesImInventar.Where(value => value == (sender as InventarItem)).FirstOrDefault();
                    if (item != null)
                    {
                        foreach (var invItem in HeldSonstigesImInventar)
                        {
                            if (invItem.EntityI.InventarGUID == item.EntityI.InventarGUID)
                            {
                                if (item.EntityHI.Anzahl <= 1)
                                    break;
                                item.EntityHI.Anzahl--;
                                OnChanged("HeldSonstigesImInventar");
                                SelectedHeld.BerechneAusruestungsGewicht();
                                return;
                            }
                        }
                        HeldSonstigesImInventar.Remove(item);
                        OnChanged("HeldSonstigesImInventar");
                        Global.ContextInventar.HeldZuInventarListe.Remove(item.EntityHI);
                        Global.ContextInventar.RemoveInventarVonHeld(item.EntityHI);
                        SelectedHeld.BerechneAusruestungsGewicht();
                        if (HeldSonstigesImInventar.Count() == 0)
                        {
                            isSonstigesVorhanden = Visibility.Collapsed;
                        }
                    }
                }
            }
        }        
        #endregion
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
            }
        }
        public string Talente { get { return talente; } set { talente = value; OnChanged("Talente"); } }
        public string TP { get { return tp; } set { tp = value; OnChanged("TP"); } }
        public string TPKK { get { return tpKK; } set { tpKK = value; OnChanged("TPKK"); } }
        public string WM { get { return wM; } set { wM = value; OnChanged("WM"); } }

        public List<Model.Held_Talent> Kampftalente
        {
            get
            {
                return EntityHA.Held.Kampftalente.Where(t => EntityNW.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).ToList(); ;
            }
        }

        public List<Kampf.Logic.KämpferNahkampfwaffe> KämpferWaffen
        {
            get
            {
                List<Kampf.Logic.KämpferNahkampfwaffe> waffen = new List<Kampf.Logic.KämpferNahkampfwaffe>();
                foreach (var talent in Kampftalente)
                    waffen.Add(new Kampf.Logic.KämpferNahkampfwaffe(EntityHA.Held, EntityNW, talent));
                return waffen;
            }
        }

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
            }
        }
        public string Talente { get { return talente; } set { talente = value; OnChanged("Talente"); } }
        public string Name { get { return name; } set { name = value; OnChanged("Name"); } }
        public string TP { get { return tp; } set { tp = value; OnChanged("TP"); } }
        public string Gewicht { get { return gewicht; } set { gewicht = value; OnChanged("Gewicht"); } }
        public string Preis { get { return preis; } set { preis = value; OnChanged("Preis"); } }

        public List<Model.Held_Talent> Kampftalente
        {
            get
            {
                return EntityHA.Held.Kampftalente.Where(t => EntityFW.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).ToList();
            }
        }

        public List<Kampf.Logic.KämpferFernkampfwaffe> KämpferWaffen
        {
            get
            {
                List<Kampf.Logic.KämpferFernkampfwaffe> waffen = new List<Kampf.Logic.KämpferFernkampfwaffe>();
                foreach (var talent in Kampftalente)
                    waffen.Add(new Kampf.Logic.KämpferFernkampfwaffe(EntityHA.Held, EntityFW, talent));
                return waffen;
            }
        }

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

    public class InventarItem : Base.ViewModelBase
    {

        #region //FELDER

        //Intern
        private Model.Held_Inventar entityHI;
        private Model.Inventar entityI;

        //UI
        private bool ausgeruestet;
        private Model.Trageort trageort;

        //Commands
        private Base.CommandBase onRemoveInventar;

        #endregion

        #region //EIGENSCHAFTEN

        //Intern
        public Model.Held_Inventar EntityHI { get { return entityHI; } set { entityHI = value; OnChanged("EntityHI"); } }
        public Model.Inventar EntityI { get { return entityI; } set { entityI = value; OnChanged("EntityI"); } }

        //UI
        public bool Ausgeruestet
        {
            get { return ausgeruestet; }
            set
            {
                ausgeruestet = value;
                OnChanged("Ausgeruestet");
                EntityHI.Angelegt = value;
                // TODO (Inventar)
                //Global.ContextInventar.UpdateHeldInventar(EntityHI);
            }
        }
        public Model.Trageort Trageort
        {
            get { return trageort; }
            set
            {
                trageort = value;
                OnChanged("Trageort");
                EntityHI.Trageort = value;
            }
        }

        //Commands
        public Base.CommandBase OnRemoveInventar
        {
            get { return onRemoveInventar; }
        }

        #endregion

        #region //KONSTRUKTOR

        public InventarItem(Model.Held_Inventar aHI, Model.Inventar aI)
        {
            this.EntityHI = aHI;
            this.EntityI = aI;
            trageort = aHI.Trageort;
            ausgeruestet = aHI.Angelegt;

            onRemoveInventar = new Base.CommandBase(RemoveInventar, null);
        }

        #endregion

        #region //EVENTS

        public event EventHandler RemoveItem;
        void RemoveInventar(object sender)
        {
            if (RemoveItem != null)
            {
                RemoveItem(this, new EventArgs());
            }

        }

        #endregion

    }

    #endregion

}