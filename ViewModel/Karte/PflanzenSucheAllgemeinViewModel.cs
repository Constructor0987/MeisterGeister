using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte
{
    public class PflanzenSucheAllgemeinViewModel : PflanzenSucheViewModel
    {
        public PflanzenSucheAllgemeinViewModel()
        {
            PropertyChanged += this_PropertyChanged;
            findeAnderePflanzen = new CommandBase(o => findePflanzen(), o => true);
        }

        private Base.CommandBase findeAnderePflanzen;
        public Base.CommandBase FindeAnderePflanzen
        {
            get { return findeAnderePflanzen; }
        }

        private void this_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Geländekunde" || e.PropertyName == "Ortskenntnis")
                updateTaW();
            else if (e.PropertyName == "TaP")
                findePflanzen();

        }

        private IEnumerable<IGrouping<Pflanze, KeyValuePair<Pflanze_Verbreitung, int>>> funde;
        public IEnumerable<IGrouping<Pflanze, KeyValuePair<Pflanze_Verbreitung, int>>> Funde
        {
            get { return funde; }
            private set { Set(ref funde, value); }
        }

        protected override int calculateTaW()
        {
            int taw = base.calculateTaW();
            if (Ortskenntnis)
                taw += 7;
            return taw;
        }

        [DependentProperty("LandschaftsGruppen")]
        public List<LandschaftsGruppeViewModel> SichtbareLandschaftsGruppen
        {
            get
            {
                return base.LandschaftsGruppen.Where(l => !l.IsEmpty).ToList();
            }
        }

        [DependentProperty("SichtbarePflanzen")]
        public override bool CanExecuteSuche
        {
            get
            {
                return base.CanExecuteSuche && SichtbarePflanzen.Count > 0;
            }
        }

        public override void ExecuteSuche()
        {
            Probe probe = getProbe();
            ProbenErgebnis e = ShowProbeDialog(probe, Global.SelectedHeld);
            if (e == null)
                return;

            TaP = e.Übrig;
        }

        private void findePflanzen()
        {
            Dictionary<Pflanze_Verbreitung, int> gefunden = new Dictionary<Pflanze_Verbreitung, int>();
            var selected = LandschaftsGruppen.SelectMany(g => g.Landschaften).Distinct().Where(l => l.IsChecked);
            int punkte = TaP;

            IEnumerable<Pflanze_Verbreitung> möglichkeiten =
                möglichePflanzen(LandschaftsGruppen.SelectMany(g => g.Landschaften).Distinct()
                .Where(l => l.IsChecked).Select(l => l.Landschaft)).ToList();
            var unendlich = möglichkeiten.Where(pv => pv.Suchschwierigkeit <= 0);
            möglichkeiten = möglichkeiten.Except(unendlich).Where(pv => pv.Suchschwierigkeit <= (int)Math.Ceiling(effektiveTaP(punkte, pv) / 2.0));
            while (möglichkeiten.Count() > 0)
            {
                Pflanze_Verbreitung fund = möglichkeiten.ElementAt(Würfel.Wurf(möglichkeiten.Count()) - 1);
                if (gefunden.ContainsKey(fund))
                    gefunden[fund]++;
                else
                    gefunden.Add(fund, 1);
                punkte -= fund.Suchschwierigkeit;
                möglichkeiten = möglichkeiten.Where(pv => pv.Suchschwierigkeit <= (int)Math.Ceiling(effektiveTaP(punkte, pv) / 2.0));
            }

            foreach (Pflanze_Verbreitung pv in unendlich)
                //0 Steht für unendlich
                gefunden.Add(pv, 0);

            Funde = gefunden.GroupBy(kvp => kvp.Key.Pflanze);
        }

        private int effektiveTaP(int tap, Pflanze_Verbreitung v)
        {
            if (v.Landschaft.GeländekundeAktiv)
                return tap + 3;
            else return tap;
        }

        private IEnumerable<Pflanze_Verbreitung> möglichePflanzen(IEnumerable<Landschaft> landschaften)
        {
            var pflanzen_verbreitungen = SichtbarePflanzen.SelectMany(p => p.Pflanze_Verbreitung);
            pflanzen_verbreitungen = pflanzen_verbreitungen.Where(pv => landschaften.Contains(pv.Landschaft));
            return pflanzen_verbreitungen;
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            foreach (LandschaftsGruppeViewModel gruppe in LandschaftsGruppen)
            {
                if (gruppe != null)
                    gruppe.PropertyChanged += Gruppe_PropertyChanged;
            }
        }

        public override void UnregisterEvents()
        {
            foreach (LandschaftsGruppeViewModel gruppe in LandschaftsGruppen)
            {
                if (gruppe != null)
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

        protected override IEnumerable<Pflanze> filterLandschaft(IEnumerable<Pflanze> pflanzen)
        {
            var landschaften = LandschaftsGruppen.SelectMany(g => g.Landschaften).Distinct().Where(l => l.IsChecked).Select(l => l.Landschaft).ToList();
            return pflanzen.Where(p => p.Pflanze_Verbreitung.Any(v => landschaften.Contains(v.Landschaft)));
        }
    }
}
