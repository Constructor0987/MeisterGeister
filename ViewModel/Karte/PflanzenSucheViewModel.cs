using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte
{
    public class PflanzenSucheViewModel : Base.ViewModelBase
    {
        public PflanzenSucheViewModel() : base(ViewHelper.ShowProbeDialog)
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            sucheGezielt = new Base.CommandBase((o) => GezielteSuche(), (o) => Global.SelectedHeld != null);
            updateTaW();
        }

        private Base.CommandBase sucheGezielt;
        public Base.CommandBase SucheGezielt
        {
            get { return sucheGezielt; }
        }

        private Model.Pflanze_Verbreitung pflanze;
        public Model.Pflanze_Verbreitung Pflanze
        {
            get { return pflanze; }
            set
            {
                Set(ref pflanze, value);
                checkGeländekunde();
            }
        }

        [DependentProperty("Schwierigkeit")]
        [DependentProperty("Ortskenntnis")]
        [DependentProperty("Geländekunde")]
        public int Modifikator
        {
            get
            {
                if (Pflanze == null)
                    return 0;
                return Math.Max(0, Schwierigkeit - (Ortskenntnis ? 7 : 0) - (Geländekunde ? 3 : 0));
            }
        }

        [DependentProperty("Pflanze")]
        public int Schwierigkeit
        {
            get
            {
                if (Pflanze == null)
                    return 0;
                return Pflanze.Verbreitung + Pflanze.Pflanze.Bestimmung;
            }
        }

        private bool ortskenntnis;
        public bool Ortskenntnis
        {
            get { return ortskenntnis; }
            set { Set(ref ortskenntnis, value); }
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


        [DependentProperty("Schwierigkeit")]
        [DependentProperty("TaP")]
        public int Funde
        {
            get
            {
                if (TaP == 0 || Schwierigkeit == 0)
                    return 0;
                else
                    return 1 + TaP / (int)Math.Round(Schwierigkeit / 2.0, MidpointRounding.AwayFromZero);
            }
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

        private void updateTaW()
        {
            if (Global.SelectedHeld == null)
                TaW = 0;
            else
            {
                int pflanzenkunde, sinnenschärfe, wildnisleben;

                if(Global.SelectedHeld.GetHeldTalent("Pflanzenkunde", true, out pflanzenkunde) == null)
                {
                    TaW = 0;
                    return;
                }
                Global.SelectedHeld.GetHeldTalent("Sinnenschärfe", true, out sinnenschärfe);
                Global.SelectedHeld.GetHeldTalent("Wildnisleben", true, out wildnisleben);

                double mod = LangeSuchen ? 1.5 : 1;
                TaW = (int)Math.Round(mod * (pflanzenkunde + sinnenschärfe + wildnisleben) / 3.0, MidpointRounding.AwayFromZero);
            }
        }

        private void checkGeländekunde()
        {
            if (Global.SelectedHeld != null && Pflanze != null)
            {
                Geländekunde = Global.SelectedHeld.HatSonderfertigkeitUndVoraussetzungen(String.Format("Geländekunde ({0})", Pflanze.Landschaft.Kundig));
            }
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += Global_HeldSelectionChanged;
            updateTaW();
        }

        public override void UnregisterEvents()
        {
            Global.HeldSelectionChanged -= Global_HeldSelectionChanged;
            base.UnregisterEvents();
        }

        private void Global_HeldSelectionChanged(object sender, EventArgs e)
        {
            updateTaW();
            sucheGezielt.Invalidate();
            checkGeländekunde();
        }

        private void GezielteSuche()
        {
            Probe probe = new Probe();

            probe.Fertigkeitswert = TaW;
            probe.Probenname = "Kräuter suchen";
            probe.Modifikator = Modifikator;
            probe.WerteNamen = "(MU/IN/FF)";
            probe.Werte = new int[] { Global.SelectedHeld.MU ?? 10, Global.SelectedHeld.IN ?? 10, Global.SelectedHeld.FF ?? 10 };

            ProbenErgebnis e = ShowProbeDialog(probe, Global.SelectedHeld);
            if (e == null)
                return;

            TaP = e.Übrig;
        }

        private void UngezielteSuche()
        {

        }
    }
}
