using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;

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

        private ObservableCollection<ProbeControlViewModel> _probeErgebnisListe = new ObservableCollection<ProbeControlViewModel>();
        public ObservableCollection<ProbeControlViewModel> ProbeErgebnisListe
        {
            get { return _probeErgebnisListe; }
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

        int _mod = 0;
        public int Mod
        {
            get { return _mod; }
            set
            {
                _mod = value;
                OnChanged("Mod");
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
            ProbeErgebnisListe.Clear();

            foreach (var item in HeldListe)
            {
                ProbeControlViewModel vm = new ProbeControlViewModel()
                {
                    Held = item,
                    Probe = item.Held_Talent.Where(t => t.Talent == SelectedProbe).FirstOrDefault()
                };

                vm.Gewürfelt += ProbeControlGewürfelt;

                // nur einfügen, wenn der Held die Fähigkeit besitzt
                if (vm.Probe != null)
                    ProbeErgebnisListe.Add(vm);
            }
        }

        private void RefreshProbeListe()
        {
            // Talente hinzufügen
            ProbeListe.AddRange(Global.ContextTalent.TalentListe.OrderBy(t => t.Name));
        }

        private void Würfeln(object obj)
        {
            // Alle Proben neu würfeln
            foreach (var item in ProbeErgebnisListe)
                item.Würfeln(null);

            // Sound abspielen
            if (MeisterGeister.Logic.Settings.Einstellungen.WuerfelSoundAbspielen)
                MeisterGeister.Logic.General.AudioPlayer.PlayWürfel();

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
