using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte
{
    public abstract class PflanzenSucheViewModel : ViewModelBase
    {
        public PflanzenSucheViewModel() : base(ViewHelper.ShowProbeDialog)
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            Suchen = new CommandBase(o => ExecuteSuche(), o => CanExecuteSuche);
            updateTaW();
        }

        private double tolerance = 0.2;
        public double Tolerance
        {
            get { return tolerance; }
            set
            {
                Set(ref tolerance, value);
                LadePflanzen();
            }
        }

        private List<LandschaftsGruppeViewModel> landschaftsGruppen = new List<LandschaftsGruppeViewModel>();
        public virtual List<LandschaftsGruppeViewModel> LandschaftsGruppen
        {
            get { return landschaftsGruppen; }
            set { Set(ref landschaftsGruppen, value); }
        }

        private Suchmonat suchmonat = Suchmonat.AktuellerMonat;
        public Suchmonat Suchmonat
        {
            get { return suchmonat; }
            set { Set(ref suchmonat, value); }
        }

        private List<string> pflanzenTypen = new List<string>();
        public List<string> PflanzenTypen
        {
            get { return pflanzenTypen; }
            set { Set(ref pflanzenTypen, value); }
        }

        private List<bool> pflanzenBekannt = new List<bool>();
        public List<bool> PflanzenBekannt
        {
            get { return pflanzenBekannt; }
            set { Set(ref pflanzenBekannt, value); }
        }

        private string pflanzenTyp = String.Empty;
        public string PflanzenTyp
        {
            get { return pflanzenTyp; }
            set { Set(ref pflanzenTyp, value); }
        }

        private bool geländekunde;
        public bool Geländekunde
        {
            get { return geländekunde; }
            set { Set(ref geländekunde, value); }
        }

        private int taW = 0;
        public int TaW
        {
            get { return taW; }
            private set { Set(ref taW, value); }
        }

        private int taP;
        public int TaP
        {
            get { return taP; }
            set { Set(ref taP, value); }
        }
        
        private bool langeSuchen;
        public bool LangeSuchen
        {
            get { return langeSuchen; }
            set
            {
                Set(ref langeSuchen, value);
                updateTaW();
            }
        }

        private bool filterBekannt = false;
        public bool FilterBekannt
        {
            get { return filterBekannt; }
            set { Set(ref filterBekannt, value); }
        }
        
        
        protected void updateTaW()
        {
            TaW = calculateTaW();
        }

        protected virtual int calculateTaW()
        {
            if (Global.SelectedHeld == null)
                return 0;
            else
            {
                int pflanzenkunde, sinnenschärfe, wildnisleben;

                if (Global.SelectedHeld.GetHeldTalent("Pflanzenkunde", true, out pflanzenkunde) == null)
                {
                    return 0;
                }
                Global.SelectedHeld.GetHeldTalent("Sinnenschärfe", true, out sinnenschärfe);
                Global.SelectedHeld.GetHeldTalent("Wildnisleben", true, out wildnisleben);

                double mod = LangeSuchen ? 1.5 : 1;
                return (int)Math.Round(mod * (pflanzenkunde + sinnenschärfe + wildnisleben) / 3.0, MidpointRounding.AwayFromZero);
            }
        }




        private Landschaft überall, fastÜberall;

        private List<LandschaftsGruppeViewModel> sichtbareGruppen;

        private void LadePflanzen()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            //cache leeren
            LandschaftViewModels.Clear();

            var typen = getPflanzen().SelectMany(p => p.Pflanze_Typ).Select(t => t.Typ).Distinct();
             //   getPflanzen().ToList().FindAll(a => a.PflanzeHeldBekannt).SelectMany(p => p.Pflanze_Typ).Select(t => t.Typ).Distinct();
            List<Landschaft> landschaften = getPflanzen().SelectMany(p => p.Landschaften).Distinct().ToList();

            //überall = landschaften.Where(l => l.Name == "überall").Single();
            //fastÜberall = landschaften.Where(l => l.Name.StartsWith("überall ")).Single();
            //landschaften.Remove(überall);
            //landschaften.Remove(fastÜberall);

            var gruppen = landschaften.SelectMany(l => l.Landschaftsgruppe).Distinct();
            var gruppenVM = gruppen.Select(g => new LandschaftsGruppeViewModel(g.Landschaft.Intersect(landschaften), g, LandschaftViewModels));
            gruppenVM = gruppenVM.Where(vm => vm.Landschaften.Count > 0).OrderBy(vm => vm.Gruppe.Name);

            List<string> temp = typen.ToList();

            temp.Insert(0, String.Empty);
            PflanzenTypen = temp;

            var neueLandschaften = landschaftenInGruppe(gruppenVM);
            var aktiv = landschaftenInGruppe(LandschaftsGruppen).Where(l => l.IsChecked).Select(l=>l.Landschaft.LandschaftGUID);
            foreach(LandschaftViewModel l in neueLandschaften.Where(l=>aktiv.Contains(l.Landschaft.LandschaftGUID)))
            {
                l.IsChecked = true;
            }

            LandschaftsGruppen = gruppenVM.ToList();
            OnChanged("SichtbarePflanzen");
        }

        private IEnumerable<LandschaftViewModel> landschaftenInGruppe(IEnumerable<LandschaftsGruppeViewModel> gruppen)
        {
            return gruppen.SelectMany(g => g.Landschaften).Distinct();
        }

        private Dictionary<Guid, LandschaftViewModel> landschaftViewModels = new Dictionary<Guid, LandschaftViewModel>();
        public Dictionary<Guid, LandschaftViewModel> LandschaftViewModels
        {
            get { return landschaftViewModels; }
            protected set { Set(ref landschaftViewModels, value); }
        }

        private List<Pflanze> sichtbarePflanzen = null;

        [DependentProperty("PflanzenTyp")]
        [DependentProperty("Suchmonat")]
        [DependentProperty("FilterGruppe")]
        [DependentProperty("FilterBekannt")]
        public List<Model.Pflanze> SichtbarePflanzen
        {
            get
            {
                sichtbarePflanzen = filterLandschaft(filterTyp(filterMonat(getPflanzen()))).OrderBy(p => p.Name).ToList();
                if (FilterBekannt)
                    sichtbarePflanzen = sichtbarePflanzen.FindAll(t => t.PflanzeHeldBekannt);
                invalidateVerbreitung();
                return sichtbarePflanzen;
            }
        }

        private IEnumerable<Pflanze> filterMonat(IEnumerable<Pflanze> pflanzen)
        {
            if (Suchmonat == Suchmonat.GanzesJahr)
                return pflanzen;
            else
            {
                return pflanzen.Where(p => p.Pflanze_Typ.Any(t => t.Typ == "Gefährliche Pflanze") || p.Pflanze_Ernte.Any(e => e.Verfügbar));
            }
        }

        private IEnumerable<Pflanze> filterTyp(IEnumerable<Pflanze> pflanzen)
        {
            if (PflanzenTyp == String.Empty)
                return pflanzen;
            else return pflanzen.Where(p => p.Pflanze_Typ.Any(pt => pt.Typ == PflanzenTyp));
        }

        protected abstract IEnumerable<Pflanze> filterLandschaft(IEnumerable<Pflanze> pflanzen);

        protected IEnumerable<Gebiet> getGebiete()
        {
            return Global.ContextZooBot.GetGebiete(Global.HeldenPosition, Tolerance);
        }

        protected IEnumerable<Pflanze> getPflanzen()
        {
            return getGebiete().SelectMany(g => g.Pflanze).Distinct();
        }

        private void invalidateVerbreitung()
        {
            OnChanged("VerbreitungSehrHäufig");
            OnChanged("VerbreitungHäufig");
            OnChanged("VerbreitungGelegentlich");
            OnChanged("VerbreitungSelten");
            OnChanged("VerbreitungSehrSelten");
        }

        public bool VerbreitungSehrHäufig
        {
            get { return sichtbarePflanzen.SelectMany(p => p.VerbreitungSehrHäufig).Count() > 0; }
        }
        public bool VerbreitungHäufig
        {
            get { return sichtbarePflanzen.SelectMany(p => p.VerbreitungHäufig).Count() > 0; }
        }
        public bool VerbreitungGelegentlich
        {
            get { return sichtbarePflanzen.SelectMany(p => p.VerbreitungGelegentlich).Count() > 0; }
        }
        public bool VerbreitungSelten
        {
            get { return sichtbarePflanzen.SelectMany(p => p.VerbreitungSelten).Count() > 0; }
        }
        public bool VerbreitungSehrSelten
        {
            get { return sichtbarePflanzen.SelectMany(p => p.VerbreitungSehrSelten).Count() > 0; }
        }

        private Base.CommandBase suchen;
        public Base.CommandBase Suchen
        {
            get;
            private set;
        }

        public virtual bool CanExecuteSuche
        {
            get { return Global.SelectedHeld != null; }
        }

        
        public abstract void ExecuteSuche();

        private bool ortskenntnis;
        public bool Ortskenntnis
        {
            get { return ortskenntnis; }
            set { Set(ref ortskenntnis, value); }
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            LadePflanzen();
            PropertyChanged += PflanzenSucheViewModel_PropertyChanged;            
            Global.HeldSelectionChanged += Global_HeldSelectionChanged;
            Global.StandortChanged += Global_StandortChanged;
            updateTaW();
            OnChanged("HeldListe");
            OnChanged("SelectedHeld");
        }

        public override void UnregisterEvents()
        {
            Global.StandortChanged -= Global_StandortChanged;
            Global.HeldSelectionChanged -= Global_HeldSelectionChanged;
            PropertyChanged -= PflanzenSucheViewModel_PropertyChanged;
            base.UnregisterEvents();
        }

        private void PflanzenSucheViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CanExecuteSuche")
                Suchen.Invalidate();
        }

        private void Global_HeldSelectionChanged(object sender, EventArgs e)
        {
            OnChanged("SelectedHeld");
            updateTaW();
            Suchen.Invalidate();
            OnHeldChanged();
            LadePflanzen();
        }

        private void Global_StandortChanged(object sender, EventArgs e)
        {
            UnregisterEvents();
            RegisterEvents();
        }


        protected virtual void OnHeldChanged()
        {
            OnChanged("CanExecuteSuche");
            OnChanged("SelectedHeld");
        }
        
        protected Probe getProbe()
        {
            Probe probe = new Probe();
            probe.Fertigkeitswert = TaW;
            probe.Probenname = "Kräuter suchen";
            probe.WerteNamen = "(MU/IN/FF)";
            probe.Werte = new int[] { Global.SelectedHeld.MU ?? 10, Global.SelectedHeld.IN ?? 10, Global.SelectedHeld.FF ?? 10 };
            return probe;
        }
    }

    public enum Suchmonat
    {
        [Description("Ganzes Jahr")]
        GanzesJahr,
        [Description("Aktueller Monat")]
        AktuellerMonat
    }
}
