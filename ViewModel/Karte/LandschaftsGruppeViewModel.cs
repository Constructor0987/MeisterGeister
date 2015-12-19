using MeisterGeister.Logic.Kalender;
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
    public class LandschaftsGruppeViewModel : Base.ViewModelBase
    {
        public LandschaftsGruppeViewModel(IEnumerable<Landschaft> ls, Landschaftsgruppe gruppe, Dictionary<Guid, LandschaftViewModel> lvmCache)
        {
            Gruppe = gruppe;
            landschaften = new List<LandschaftViewModel>();
            if (ls != null)
            {
                if (gruppe.Name == "Sonstiges")
                    IsExpanded = true;
                foreach (Landschaft l in ls)
                {
                    LandschaftViewModel vm = null;
                    if (lvmCache.ContainsKey(l.LandschaftGUID))
                        vm = lvmCache[l.LandschaftGUID];
                    else
                    {
                        vm = new LandschaftViewModel(l);
                        lvmCache.Add(l.LandschaftGUID, vm);
                    }
                    landschaften.Add(vm);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LandschaftsGruppeViewModel))
                return false;
            LandschaftsGruppeViewModel other = (LandschaftsGruppeViewModel)obj;
            if (other.Gruppe == null && Gruppe == null)
                return true;
            else if (other.Gruppe == null || Gruppe == null)
                return false;
            else return other.Gruppe.LandschaftsgruppeID == Gruppe.LandschaftsgruppeID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Landschaftsgruppe Gruppe { get; private set; }
        public bool IsEmpty
        {
            get
            {
                return !Landschaften.Any(l => !l.Landschaft.Name.Contains("überall"));
            }
        }


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

        [DependentProperty("Landschaften")]
        public List<LandschaftViewModel> SichtbareLandschaften
        {
            get { return Landschaften.Where(l => !l.Landschaft.Name.Contains("überall")).ToList(); }
        }


        public bool? IsChecked
        {
            get
            {
                bool foundChecked = false, foundUnchecked = false;
                foreach (LandschaftViewModel vm in SichtbareLandschaften)
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
                    foreach (LandschaftViewModel vm in SichtbareLandschaften)
                    {
                        vm.IsChecked = value.Value;
                    }
            }
        }

        private bool isExpanded = false;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { Set(ref isExpanded, value); }
        }

    }

    public class LandschaftViewModel : Base.ViewModelBase
    {
        public LandschaftViewModel(Landschaft landschaft)
        {
            Landschaft = landschaft;
        }
        public Landschaft Landschaft { get; private set; }

        private bool isChecked = false;
        public bool IsChecked
        {
            get { return isChecked; }
            set { Set(ref isChecked, value); }
        }

    }
}
