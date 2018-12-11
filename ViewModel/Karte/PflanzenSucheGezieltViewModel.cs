using MeisterGeister.Logic.Einstellung;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeisterGeister.ViewModel.Karte
{    
    public class PflanzenSucheGezieltViewModel : PflanzenSucheViewModel
    {
        private Pflanze_Verbreitung suche = null;
        public Pflanze_Verbreitung Suche
        {
            get { return suche; }
            set
            {
                Set(ref suche, value);
                checkGeländekunde();
            }
        }

        public override List<LandschaftsGruppeViewModel> LandschaftsGruppen
        {
            get { return base.LandschaftsGruppen; }
            set
            {
                //Dummy-Value für alle Gruppen:
                value.Insert(0, nullGruppe);
                base.LandschaftsGruppen = value;
            }
        }

        private static readonly LandschaftsGruppeViewModel nullGruppe = new LandschaftsGruppeViewModel(null, null, null);
        private LandschaftsGruppeViewModel filterGruppe = nullGruppe;
        public LandschaftsGruppeViewModel FilterGruppe
        {
            get { return filterGruppe; }
            set { Set(ref filterGruppe, value); }
        }


        public override void ExecuteSuche()
        {
            Probe probe = getProbe();
            probe.Modifikator = Modifikator;

            ProbenErgebnis e = ShowProbeDialog(probe, Global.SelectedHeld);
            if (e == null)
                return;

            TaP = e.Übrig;
        }

        [DependentProperty("Suche")]
        [DependentProperty("Ortskenntnis")]
        [DependentProperty("Geländekunde")]
        public int Modifikator
        {
            get
            {
                if (Suche == null)
                    return 0;
                return Math.Max(0, Suche.Suchschwierigkeit - (Ortskenntnis ? 7 : 0) - (Geländekunde ? 3 : 0));
            }
        }


        [DependentProperty("Suche")]
        [DependentProperty("TaP")]
        public int Funde
        {
            get
            {
                if (TaP == 0 || Suche == null)
                    return 0;
                else if (Suche.Suchschwierigkeit <= 0)
                    //Die Regeln decken diesen Fall leider nicht ab
                    //Damit man nicht unendlich Pflanen findet, oder ein negativer Betrag rauskommt findet man TaP + 1 Pflanzen
                    //So war das im alten ZooBot-Tool implementiert.
                    return TaP + 1;
                else
                    return 1 + TaP / (int)Math.Round(Suche.Suchschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
            }
        }


        public bool PflanzenwissenIntegration
        {
            get { return Einstellungen.PflanzenwissenIntegrieren; }
        }

        private void checkGeländekunde()
        {
            if (Suche != null)
            {
                Geländekunde = Suche.Landschaft.GeländekundeAktiv;
            }
        }

        protected override void OnHeldChanged()
        {
            base.OnHeldChanged();
            checkGeländekunde();
        }

        protected override IEnumerable<Pflanze> filterLandschaft(IEnumerable<Pflanze> pflanzen)
        {
            if (FilterGruppe == null || FilterGruppe.Gruppe == null)
                return pflanzen;
            return pflanzen.Where(p => p.Pflanze_Verbreitung.Any(v => FilterGruppe.Gruppe.Landschaft.Contains(v.Landschaft)));
        }

            
    }
}
