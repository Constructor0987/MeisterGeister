using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.ViewModel.Karte
{
    public class PflanzenAnOrtViewModel : Base.ViewModelBase
    {
        public PflanzenAnOrtViewModel()
        {
            LadePflanzen();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
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


        private List<string> pflanzenTypen;
        public List<string> PflanzenTypen
        {
            get { return pflanzenTypen; }
            set { pflanzenTypen = value; }
        }

        private List<LandschaftsGruppeViewModel> landschaftsGruppen;
        public List<LandschaftsGruppeViewModel> LandschaftsGruppen
        {
            get { return landschaftsGruppen; }
            set { landschaftsGruppen = value; }
        }


        private string pflanzenTyp = String.Empty;
        public string PflanzenTyp
        {
            get { return pflanzenTyp; }
            set
            { Set(ref pflanzenTyp, value); }
        }

        ObservableCollection<Model.Pflanze> pflanzen;
        public ObservableCollection<Model.Pflanze> Pflanzen
        {
            get
            {
                if (pflanzen == null)
                    pflanzen = new ObservableCollection<Model.Pflanze>();
                return pflanzen;
            }
            protected set { Set(ref pflanzen, value); }
        }

        [DependentProperty("PflanzenTyp")]
        public List<Model.Pflanze> SichtbarePflanzen
        {
            get
            {
                return filterLandschaft(filterTyp(Pflanzen));
            }
        }

        private IEnumerable<Pflanze> filterTyp(IList<Pflanze> pflanzen)
        {
            if (PflanzenTyp == String.Empty)
                return pflanzen.ToList();
            else return pflanzen.Where(p => p.Pflanze_Typ.Where(pt => pt.Typ == PflanzenTyp).Count() > 0).ToList();
        }

        private List<Pflanze> filterLandschaft(IEnumerable<Pflanze> pflanzen)
        {
            HashSet<Landschaft> landschaften = new HashSet<Landschaft>();
            foreach (LandschaftsGruppeViewModel gruppe in LandschaftsGruppen)
            {
                foreach (LandschaftViewModel vm in gruppe.Landschaften.Where((l) => l.IsChecked))
                    landschaften.Add(vm.Landschaft);
            }
            return pflanzen.Where((p) => p.Landschaften.Any((l) => landschaften.Contains(l))).ToList();
        }


        void LadePflanzen()
        {
            Pflanzen.Clear();
            var gebiete = Global.ContextZooBot.GetGebiete(Global.Standort.Koordinaten, Tolerance);
            HashSet<Guid> pset = new HashSet<Guid>();
            HashSet<string> typen = new HashSet<string>();
            HashSet<Landschaft> landschaften = new HashSet<Landschaft>();
            HashSet<Landschaftsgruppe> gruppen = new HashSet<Landschaftsgruppe>();
            //Leerer Eintrag für alle Pflanzentypen
            typen.Add(String.Empty);
            foreach (var g in gebiete)
            {
                foreach (var p in g.Pflanze)
                {
                    if (pset.Contains(p.PflanzeGUID))
                        continue;

                    pset.Add(p.PflanzeGUID);
                    Pflanzen.Add(p);
                    foreach (var pt in p.Pflanze_Typ)
                        typen.Add(pt.Typ);
                    foreach (var l in p.Landschaften)
                    {
                        landschaften.Add(l);
                        foreach (var gr in l.Landschaftsgruppe)
                            gruppen.Add(gr);
                    }
                }
            }
            PflanzenTypen = typen.ToList();
            LandschaftsGruppen = new List<LandschaftsGruppeViewModel>();
            foreach (Landschaftsgruppe gruppe in gruppen)
            {
                var inGruppe = landschaften.Where((l) => l.Landschaftsgruppe.Contains(gruppe));
                LandschaftsGruppen.Add(new LandschaftsGruppeViewModel(inGruppe, gruppe));
            }
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            foreach (LandschaftsGruppeViewModel gruppe in LandschaftsGruppen)
            {
                gruppe.PropertyChanged += Gruppe_PropertyChanged;
            }
            Global.StandortChanged += Global_StandortChanged;
        }

        public override void UnregisterEvents()
        {
            Global.StandortChanged -= Global_StandortChanged;
            foreach (LandschaftsGruppeViewModel gruppe in LandschaftsGruppen)
            {
                gruppe.PropertyChanged -= Gruppe_PropertyChanged;
            }
            base.UnregisterEvents();
        }

        private void Gruppe_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                OnChanged("SichtbarePflanzen");
            }
        }

        private void Global_StandortChanged(object sender, EventArgs e)
        {
            LadePflanzen();
        }
    }

    public class LandschaftsGruppeViewModel : Base.ViewModelBase
    {
        public LandschaftsGruppeViewModel(IEnumerable<Landschaft> ls, Landschaftsgruppe gruppe)
        {
            Gruppe = gruppe;
            landschaften = new List<LandschaftViewModel>();
            foreach (Landschaft l in ls)
            {
                LandschaftViewModel vm = new LandschaftViewModel(l);
                landschaften.Add(vm);
            }
        }

        public Landschaftsgruppe Gruppe { get; private set; }


        public override void RegisterEvents()
        {
            base.RegisterEvents();
            foreach (LandschaftViewModel vm in landschaften)
            {
                vm.PropertyChanged += landschaft_PropertyChanged;
            }
        }

        public override void UnregisterEvents()
        {
            foreach (LandschaftViewModel vm in landschaften)
            {
                vm.PropertyChanged -= landschaft_PropertyChanged;
            }
            base.UnregisterEvents();
        }

        private void landschaft_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnChanged("IsChecked");
        }

        private List<LandschaftViewModel> landschaften;
        public List<LandschaftViewModel> Landschaften
        {
            get { return landschaften; }
            private set { Set(ref landschaften, value); }
        }


        public bool? IsChecked
        {
            get
            {
                bool foundChecked = false, foundUnchecked = false;
                foreach (LandschaftViewModel vm in landschaften)
                {
                    if (vm.IsChecked) foundChecked = true;
                    else foundUnchecked = true;
                }
                if (foundChecked && foundUnchecked) return null;
                else if (foundChecked) return true;
                else return false;
            }
            set
            {
                if (value != null)
                    foreach (LandschaftViewModel vm in landschaften)
                    {
                        vm.IsChecked = value.Value;
                    }
            }
        }

    }

    public class LandschaftViewModel : Base.ViewModelBase
    {
        public LandschaftViewModel(Landschaft landschaft)
        {
            Landschaft = landschaft;
        }
        public Landschaft Landschaft { get; private set; }

        private bool isChecked = true;
        public bool IsChecked
        {
            get { return isChecked; }
            set { Set(ref isChecked, value); }
        }

    }
}
