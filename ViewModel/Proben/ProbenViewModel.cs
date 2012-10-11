using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.ViewModel.Proben
{
    public class ProbenViewModel : ViewModel.Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onWürfeln;
        public Base.CommandBase OnWürfeln
        {
            get { return onWürfeln; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        public List<Model.Held> HeldListe
        {
            get { return Global.ContextHeld.HeldenGruppeListe; }
        }

        private List<ProbeControlViewModel> _probeErgebnisListe = new List<ProbeControlViewModel>();
        public List<ProbeControlViewModel> ProbeErgebnisListe
        {
            get { return _probeErgebnisListe.OrderByDescending(vm => vm.Ergebnis.Übrig).
                ThenByDescending(vm => vm.Ergebnis.Qualität).
                ThenByDescending(vm => vm.Probe.Fertigkeitswert).ToList(); }
            set
            {
                _probeErgebnisListe = value;
                OnChanged("ProbeErgebnisListe");
            }
        }

        Probe _selectedProbe = null;
        public Probe SelectedProbe
        {
            get { return _selectedProbe; }
            set 
            { 
                _selectedProbe = value;
                RefreshProbeErgebnisListe();
                OnChanged("SelectedProbe");
            }
        }

        int _modifikator = 0;
        public int Modifikator
        {
            get { return _modifikator; }
            set
            {
                _modifikator = value;

                // Modifikator an ProbeControls weiterreichen
                foreach (ProbeControlViewModel er in ProbeErgebnisListe)
                    er.Modifikator = _modifikator;
                OnChanged("Modifikator");
                OnChanged("ProbeErgebnisListe");
            }
        }

        public string GruppenErgebnis
        {
            get
            {
                int tapSum = 0, vorSum = 0, i = 1;
                string art = "Übrig";
                foreach (ProbeControlViewModel er in ProbeErgebnisListe)
                {
                    if (er.Ergebnis.Übrig >= 0) //nur positive Ergebnisse addieren
                    {
                        tapSum += er.Ergebnis.Übrig;
                        vorSum += Convert.ToInt32(Math.Round((double)er.Ergebnis.Übrig / i, 0));
                        i++;
                    }
                }
                if (SelectedProbe is Model.Talent)
                    art = "TaP*";
                else if (SelectedProbe is Model.Zauber)
                    art = "ZfP*";

                return string.Format("Unabhängige Zusammenarbeit: {0} {1}\nMit fähigstem Held als Vorarbeiter: {2} {1}", tapSum, art, vorSum);
            }
        }

        // Listen
        List<Probe> _probeListe = new List<Probe>();
        public List<Probe> ProbeListe
        {
            get { return _probeListe; }
            set { _probeListe = value; OnChanged("ProbeListe"); }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public ProbenViewModel()
        {
            onWürfeln = new Base.CommandBase(Würfeln, null);

            Refresh();
            RefreshProbeErgebnisListe();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Refresh()
        {
            OnChanged("HeldListe");
            RefreshProbeListe();
        }

        private void RefreshProbeErgebnisListe()
        {
            _probeErgebnisListe.Clear();

            foreach (var item in HeldListe)
            {
                // TODO MT: Probem wenn der Held einen Zauber in mehreren Rep. hat
                // Solange wird erstmal der Zauber mit dem höchsten ZfW ausgewählt

                ProbeControlViewModel vm = new ProbeControlViewModel();
                vm.Held = item;
                if (SelectedProbe is Model.Talent)
                    vm.Probe = item.Held_Talent.Where(t => t.Talent == SelectedProbe).FirstOrDefault();
                else if (SelectedProbe is Model.Zauber)
                    vm.Probe = item.Held_Zauber.Where(z => z.Zauber == SelectedProbe).OrderByDescending(z => z.ZfW).FirstOrDefault();
                else if (SelectedProbe is Eigenschaft)
                    vm.Probe = item.Eigenschaft((SelectedProbe as Eigenschaft).Name);
                vm.Gewürfelt += ProbeControlGewürfelt;

                // nur einfügen, wenn der Held die Fähigkeit besitzt
                if (vm.Probe != null)
                    _probeErgebnisListe.Add(vm);
            }
            OnChanged("ProbeErgebnisListe");
        }

        private void RefreshProbeListe()
        {
            // Eigenschaften hinzufügen
            ProbeListe.AddRange(Eigenschaft.EigenschaftenListe);
            // Talente hinzufügen
            ProbeListe.AddRange(Global.ContextHeld.Liste<Model.Talent>().OrderBy(t => t.Name));
            // Zauber hinzufügen
            ProbeListe.AddRange(Global.ContextHeld.Liste<Model.Zauber>().OrderBy(z => z.Name));
        }

        private void Würfeln(object obj)
        {
            // Alle Proben neu würfeln
            foreach (var item in ProbeErgebnisListe)
                item.Würfeln();

            // Sound abspielen
            if (MeisterGeister.Logic.Settings.Einstellungen.WuerfelSoundAbspielen)
                MeisterGeister.Logic.General.AudioPlayer.PlayWürfel();

            OnChanged("ProbeErgebnisListe");
            OnChanged("GruppenErgebnis");
        }

        #endregion

        #region //---- EVENTS ----

        void ProbeControlGewürfelt(object sender, EventArgs e)
        {
            OnChanged("GruppenErgebnis");
        }

        #endregion
    }

    #region //---- SUBKLASSEN ----

    public class ProbeErgebnisItem : Base.ViewModelBase
    {
        private Model.Held _held = null;
        public Model.Held Held 
        { 
            get { return _held; } 
            set { _held = value; OnChanged("Held"); } 
        }

        public Probe Probe { get; set; }

        private ProbeControlViewModel _vm = null;
        public ProbeControlViewModel VM
        {
            get { return _vm; }
            set { _vm = value; OnChanged("VM"); }
        }
    }

    #endregion
}
