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
using System.Windows.Data;
using System.ComponentModel;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.Inventar
{
    public class InventarViewModel : Base.ViewModelBase
    {

        #region //FELDER

        //Intern
        const string TALENTNAHKAMPFWAFFEUNTERKATEGORIE = "Bewaffneter Nahkampf";
        const string TALENTNAHKAMPFWAFFEATTECHNIK = "Bewaffnete AT-Technik";
        const string TALENTFERNKAMPFWAFFEUNTERKATEGORIE = "Fernkampf";
        const string FILTERDEAKTIVIEREN = " Alle ";
        private bool _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;

        //UI
        private bool isNahkampfwaffevorhanden = false;
        private bool isFernkampfwaffevorhanden = false;
        private bool isSchildVorhanden = false;
        private bool isRuestungVorhanden = false;
        private bool isSonstigesVorhanden = false;
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
        private Model.Held_Ausrüstung selectedAusrüstung;

        //Entitylisten
        private List<Model.Talent> nahkampfWaffeTalentListe = new List<Model.Talent>();
        private List<Model.Talent> fernkampWaffeTalentListe = new List<Model.Talent>();
        private List<Model.Waffe> nahkampfwaffeListe = new List<Model.Waffe>();
        private List<Model.Fernkampfwaffe> fernkampfwaffeListe = new List<Model.Fernkampfwaffe>();
        private List<Model.Schild> schildListe = new List<Model.Schild>();
        private List<Model.Rüstung> ruestungListe = new List<Model.Rüstung>();
        private List<Model.Trageort> trageortListe = new List<Model.Trageort>();

        //Zuordnungen
        private ExtendedObservableCollection<Model.Held_Ausrüstung> heldAusrüstungen = new ExtendedObservableCollection<Model.Held_Ausrüstung>();

        private ICollectionView heldNahkampfWaffeImInventar;
        private ICollectionView heldFernkampfwaffeImInventar;
        private ICollectionView heldSchildImInventar;
        private ICollectionView heldRuestungImInventar;

        private ExtendedObservableCollection<InventarItem> heldSonstigesImInventar = new ExtendedObservableCollection<InventarItem>();

        //Commands
        private Base.CommandBase onAddNahkampfwaffe;
        private Base.CommandBase onAddFernkampfwaffe;
        private Base.CommandBase onAddSchild;
        private Base.CommandBase onAddRuestung;

        #endregion

        #region //EIGENSCHAFTEN

        //UI      
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }
        public Visibility IsRuestungEinfachEingeben
        {
            get { return isRuestungEinfachEingeben; }
            set
            {
                Set(ref isRuestungEinfachEingeben, value);
            }
        }
        public Visibility IsBEEingebenVisibility
        {
            get { return isBEEingebenVisibility; }
            set
            {
                Set(ref isBEEingebenVisibility, value);
            }
        }
        public Visibility IsUeberlastungEingebenVisibility
        {
            get { return isUeberlastungEingebenVisibility; }
            set
            {
                Set(ref isUeberlastungEingebenVisibility, value);
            }
        }

        public int SelectedFilterIndex
        {
            get { return selectedFilterIndex; }
            set
            {
                Set(ref selectedFilterIndex, value);
            }
        }

        public bool IsAllSelected
        {
            get { return isAllSelected; }
            set
            {
                Set(ref isAllSelected, value);
            }
        }
        public bool IsNahkampfWaffeSelected
        {
            get { return isNahkampfWaffeSelected; }
            set
            {
                Set(ref isNahkampfWaffeSelected, value);
            }
        }
        public bool IsFernkampfwaffeSelected
        {
            get { return isFernkampfwaffeSelected; }
            set
            {
                Set(ref isFernkampfwaffeSelected, value);
            }
        }
        public bool IsSchildSelected
        {
            get { return isSchildSelected; }
            set
            {
                Set(ref isSchildSelected, value);
            }
        }
        public bool IsRuestungSelected
        {
            get { return isRuestungSelected; }
            set
            {
                Set(ref isRuestungSelected, value);
            }
        }
        public bool IsSonstigesSelected
        {
            get { return isSonstigesSelected; }
            set
            {
                Set(ref isSonstigesSelected, value);
            }
        }
        public bool IsRuestungBerechnungEinfach
        {
            get { return !(isRuestungBerechnungEinfach || E.IsReadOnly); }
            set
            {
                Set(ref isRuestungBerechnungEinfach, value);
            }
        }
        public bool IsRuestungBerechnungZonen
        {
            get { return isRuestungBerechnungZonen; }
            set
            {
                Set(ref isRuestungBerechnungZonen, value);
            }
        }
        public bool IsBehinderungEingeben
        {
            get { return !(isBehinderungEingeben || E.IsReadOnly); }
            set
            {
                Set(ref isBehinderungEingeben, value);
            }
        }
        public bool IsUeberlastungEingeben
        {
            get { return !(isUeberlastungEingeben || E.IsReadOnly); }
            set
            {
                Set(ref isUeberlastungEingeben, value);
            }
        }

        public Model.Held SelectedHeld
        {
            get { return selectedHeld; }
            set
            {
                Set(ref selectedHeld, value);
            }
        }
        public Model.Talent SelectedNahkampfwaffeTalent
        {
            get { return selectedNahkampfwaffeTalent; }
            set
            {
                selectedNahkampfwaffeTalent = value;
                if (value.Talentname != FILTERDEAKTIVIEREN)
                {
                    NahkampfwaffeListe = Global.ContextInventar.WaffeListe.Where(w => w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                }
                else
                {
                    NahkampfwaffeListe = Global.ContextInventar.WaffeListe;
                }
                OnChanged("SelectedNahkampfwaffeTalent");
            }
        }
        public Model.Talent SelectedFernkampfwaffeTalent
        {
            get { return selectedFernkampfwaffeTalent; }
            set
            {
                selectedFernkampfwaffeTalent = value;
                if (value.Talentname != FILTERDEAKTIVIEREN)
                {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.Where(w => w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                }
                else
                {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.ToList();
                }
                OnChanged("SelectedFernkampfwaffeTalent");
            }
        }
        public Model.Waffe SelectedNahkampfwaffe
        {
            get { return selectedNahkampfwaffe; }
            set
            {
                Set(ref selectedNahkampfwaffe, value);
            }
        }
        public Model.Fernkampfwaffe SelectedFernkampfwaffe
        {
            get { return selectedFernkampfwaffe; }
            set
            {
                Set(ref selectedFernkampfwaffe, value);
            }
        }
        public Model.Schild SelectedSchild
        {
            get { return selectedSchild; }
            set
            {
                Set(ref selectedSchild, value);
            }
        }
        public Model.Rüstung SelectedRuestung
        {
            get { return selectedRuestung; }
            set
            {
                Set(ref selectedRuestung, value);
            }
        }

        public Model.Held_Ausrüstung SelectedAusrüstung
        {
            get { return selectedAusrüstung; }
            set { Set(ref selectedAusrüstung, value); }
        }

        //public double AktuelleTragkraft {
        //    get { return aktuelleTragkraft; }
        //    set {
        //        aktuelleTragkraft = value;
        //        OnChanged("AktuelleTragkraft");
        //    }
        //}
        //EntityListen
        public List<Model.Talent> NahkampfWaffeTalentListe
        {
            get { return nahkampfWaffeTalentListe; }
            set
            {
                Set(ref nahkampfWaffeTalentListe, value);
            }
        }
        public List<Model.Talent> FernkampWaffeTalentListe
        {
            get { return fernkampWaffeTalentListe; }
            set
            {
                Set(ref fernkampWaffeTalentListe, value);
            }
        }
        public List<Model.Waffe> NahkampfwaffeListe
        {
            get { return nahkampfwaffeListe; }
            set
            {
                Set(ref nahkampfwaffeListe, value);
            }
        }
        public List<Model.Fernkampfwaffe> FernkampfwaffeListe
        {
            get { return fernkampfwaffeListe; }
            set
            {
                Set(ref fernkampfwaffeListe, value);
            }
        }
        public List<Model.Schild> SchildListe
        {
            get { return schildListe; }
            set
            {
                Set(ref schildListe, value);
            }
        }
        public List<Model.Rüstung> RuestungListe
        {
            get { return ruestungListe; }
            set
            {
                Set(ref ruestungListe, value);
            }
        }
        public List<Model.Trageort> TrageortListe
        {
            get { return trageortListe; }
            set
            {
                Set(ref trageortListe, value);
            }
        }
        //Zuordnung
        public ICollectionView HeldNahkampfWaffeImInventar
        {
            get { return heldNahkampfWaffeImInventar; }
        }
        public ICollectionView HeldFernkampfwaffeImInventar
        {
            get { return heldFernkampfwaffeImInventar; }
        }
        public ICollectionView HeldSchildImInventar
        {
            get { return heldSchildImInventar; }
        }
        public ICollectionView HeldRuestungImInventar
        {
            get { return heldRuestungImInventar; }
        }
        public ExtendedObservableCollection<InventarItem> HeldSonstigesImInventar
        {
            get { return heldSonstigesImInventar; }
            set
            {
                heldSonstigesImInventar = value;
                OnChanged("HeldSonstigesImInventar");
            }
        }
        //Commands
        public Base.CommandBase OnAddNahkampfwaffe
        {
            get { return onAddNahkampfwaffe; }
        }
        public Base.CommandBase OnAddFernwaffe
        {
            get { return onAddFernkampfwaffe; }
        }
        public Base.CommandBase OnAddSchild
        {
            get { return onAddSchild; }
        }
        public Base.CommandBase OnAddRuestung
        {
            get { return onAddRuestung; }
        }
        #endregion

        #region //KONSTRUKTOR
        public InventarViewModel()
        {
            heldNahkampfWaffeImInventar = new CollectionViewSource { Source = heldAusrüstungen }.View;
            heldNahkampfWaffeImInventar.Filter = (a) => ((Model.Held_Ausrüstung)a).Waffe != null;

            heldFernkampfwaffeImInventar = new CollectionViewSource { Source = heldAusrüstungen }.View;
            heldFernkampfwaffeImInventar.Filter = (a) => ((Model.Held_Ausrüstung)a).Fernkampfwaffe != null;

            heldSchildImInventar = new CollectionViewSource { Source = heldAusrüstungen }.View;
            heldSchildImInventar.Filter = (a) => ((Model.Held_Ausrüstung)a).Schild != null;

            heldRuestungImInventar = new CollectionViewSource { Source = heldAusrüstungen }.View;
            heldRuestungImInventar.Filter = (a) => ((Model.Held_Ausrüstung)a).Rüstung != null;

            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("BEBerechnung", ""));
            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("RSBerechnung", ""));
            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("UeberlastungBerechnung", ""));
            EinstellungenChangedHandler(null, new MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs("IsMitUeberlastung", ""));

            onAddNahkampfwaffe = new Base.CommandBase(AddNahkampfwaffe, null);
            onAddFernkampfwaffe = new Base.CommandBase(AddFernkampfwaffe, null);
            onAddSchild = new Base.CommandBase(AddSchild, null);
            onAddRuestung = new Base.CommandBase(AddRuestung, null);

            SelectedFilterIndex = 0;

            LoadDaten();
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

        private void LoadDaten()
        {
            //Nahkampf
            NahkampfWaffeTalentListe = Global.ContextTalent.TalentListe.Where(t =>
                    t.TalentgruppeID == 1
                    && (t.Untergruppe == TALENTNAHKAMPFWAFFEUNTERKATEGORIE
                    || t.Untergruppe == TALENTNAHKAMPFWAFFEATTECHNIK)
                    && !NahkampfWaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname).ToList();
            NahkampfWaffeTalentListe.Insert(0, new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });

            NahkampfwaffeListe = Global.ContextInventar.WaffeListe;

            //Fernkampf
                FernkampWaffeTalentListe = Global.ContextTalent.TalentListe.Where(t => 
                    t.TalentgruppeID == 1
                    && t.Untergruppe == TALENTFERNKAMPFWAFFEUNTERKATEGORIE
                    && !FernkampWaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname).ToList();
            FernkampWaffeTalentListe.Insert(0, new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });

            FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe;

            //Schild
            SchildListe = Global.ContextInventar.SchildListe;

            //Rüstung
            RuestungListe = Global.ContextInventar.RuestungListe;

            //Trageorte
            TrageortListe = Global.ContextInventar.TrageortListe;
        }
        #endregion

        #region //EVENTS
        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }
        void EinstellungenChangedHandler(object sender, MeisterGeister.Logic.Einstellung.EinstellungChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "RSBerechnung":
                    switch (E.RSBerechnung)
                    {
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
                            if (SelectedHeld != null)
                            {
                                SelectedHeld.BerechneRüstungswerte();
                            }
                            break;
                    }
                    break;
                case "BEBerechnung":
                    //Einstellung für BE-Berechnung
                    switch (E.BEBerechnung)
                    {
                        //Automatisch
                        case 0:
                            IsBehinderungEingeben = false;
                            IsBEEingebenVisibility = Visibility.Hidden;
                            if (selectedHeld != null)
                            {
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
                    switch (E.UeberlastungBerechnung)
                    {
                        //Automatisch
                        case 0:
                            IsUeberlastungEingeben = false;
                            IsUeberlastungEingebenVisibility = Visibility.Hidden;
                            if (selectedHeld != null)
                            {
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
                    switch (E.IsMitUeberlastung)
                    {
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
        void SelectedHeldChanged(object sender, EventArgs e)
        {
            SelectedHeld = Global.SelectedHeld;
            heldAusrüstungen.Clear();
            heldSonstigesImInventar.Clear();

            if (SelectedHeld != null)
            {
                heldAusrüstungen.AddRange(SelectedHeld.Held_Ausrüstung);
                
                //Sonstiges / Held_Inventar
                foreach (Model.Held_Inventar item in Global.ContextInventar.HeldZuInventarListe.Where(hw => hw.HeldGUID == Global.SelectedHeldGUID && hw.Inventar != null).OrderBy(i => i.Inventar.Name))
                {
                    InventarItem value = new InventarItem(item, item.Inventar);
                    value.RemoveItem += (s, ev) => { RemoveAusruestung(s); };
                    HeldSonstigesImInventar.Add(value);
                }
                if (E.BEBerechnung == 0)
                    SelectedHeld.BerechneBehinderung();
            }
        }

        //--ADD
        void AddNahkampfwaffe(object sender)
        {
            if (SelectedNahkampfwaffe != null && SelectedHeld != null && !IsReadOnly)
            {
                var ha = SelectedHeld.AddAusrüstung(SelectedNahkampfwaffe.Ausrüstung);
                heldAusrüstungen.Add(ha);
                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }
        void AddFernkampfwaffe(object sender)
        {
            if (SelectedFernkampfwaffe != null && SelectedHeld != null && !IsReadOnly)
            {
                var ha = SelectedHeld.AddAusrüstung(SelectedFernkampfwaffe.Ausrüstung);
                heldAusrüstungen.Add(ha);
                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }
        void AddSchild(object sender)
        {
            if (SelectedSchild != null && SelectedHeld != null && !IsReadOnly)
            {
                var ha = SelectedHeld.AddAusrüstung(SelectedSchild.Ausrüstung);
                heldAusrüstungen.Add(ha);
                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }
        void AddRuestung(object sender)
        {
            if (SelectedRuestung != null && SelectedHeld != null && !IsReadOnly)
            {
                var ha = SelectedHeld.AddAusrüstung(SelectedRuestung.Ausrüstung);
                heldAusrüstungen.Add(ha);

                if (E.RSBerechnung == 0 || E.RSBerechnung == 3)
                    SelectedHeld.BerechneRüstungswerte();
                if (E.BEBerechnung == 0)
                    SelectedHeld.BerechneBehinderung();

                SelectedHeld.BerechneAusruestungsGewicht();
            }
        }

        //--REMOVE
        void RemoveAusruestung(object sender)
        {
            if (SelectedHeld != null && !IsReadOnly)
            {
                if ((sender as InventarItem) != null)
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
                            isSonstigesVorhanden = false;
                        }
                    }
                }
                else if (SelectedAusrüstung != null)
                {
                    //SelectedHeldAusrüstung löschen
                    heldAusrüstungen.Remove(SelectedAusrüstung);
                    SelectedHeld.RemoveAusrüstung(SelectedAusrüstung);
                }
            }
        }
        #endregion
    }

    #region //SUBKLASSEN

    public class AusruestungsItem : Base.ViewModelBase
    {
        private bool ausgeruestet;
        public virtual bool Ausgeruestet
        {
            get { return ausgeruestet; }
            set
            {
                Set(ref ausgeruestet, value);
                EntityHA.Angelegt = value;
                Global.ContextInventar.UpdateHeldAusruestung(EntityHA);
            }
        }

        public virtual Model.Trageort Trageort
        {
            get { return EntityHA.Trageort; }
            set
            {
                EntityHA.Trageort = value;
                OnChanged("Trageort");
            }
        }

        public Model.Held_Ausrüstung EntityHA { get; set; }

        public AusruestungsItem(bool angelegt)
        {
            ausgeruestet = angelegt;
            onRemove = new Base.CommandBase((o) => Remove(), null);
        }

        private Base.CommandBase onRemove;
        public Base.CommandBase OnRemove
        {
            get { return onRemove; }
        }

        public event EventHandler RemoveItem;
        public void Remove()
        {
            EventHandler handler = RemoveItem;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }

        }
    }

    public class InventarItem : AusruestungsItem
    {

        #region //FELDER

        //Intern
        private Model.Held_Inventar entityHI;
        private Model.Inventar entityI;

        #endregion

        #region //EIGENSCHAFTEN

        //Intern
        public Model.Held_Inventar EntityHI
        {
            get { return entityHI; }
            set { Set(ref entityHI, value); }
        }
        public Model.Inventar EntityI
        {
            get { return entityI; }
            set { Set(ref entityI, value); }
        }

        //UI
        public override bool Ausgeruestet
        {
            get { return base.Ausgeruestet; }
            set
            {
                EntityHI.Angelegt = value;
                base.Ausgeruestet = value;
                Global.ContextInventar.UpdateHeldInventar(EntityHI);
            }
        }

        public override Model.Trageort Trageort
        {
            get
            {
                return EntityHI.Trageort;
            }
            set
            {
                EntityHI.Trageort = value;
                OnChanged("Trageort");
            }
        }


        #endregion

        #region //KONSTRUKTOR

        public InventarItem(Model.Held_Inventar aHI, Model.Inventar aI) : base(aHI.Angelegt)
        {
            this.EntityHI = aHI;
            this.EntityI = aI;
        }

        #endregion
    }

    #endregion

}