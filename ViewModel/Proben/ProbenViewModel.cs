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

                foreach (var item in ProbeErgebnisListe)
                {
                    if (item.Held != null)
                        item.Probe = item.Held.Held_Talent.Where(t => t.Talent == value).FirstOrDefault();
                    else
                        item.Probe = value;
                }

                OnChanged("SelectedProbe"); 
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
            RefreshProbeListe();
            RefreshProbeErgebnisListe();

            onWürfeln = new Base.CommandBase(Würfeln, null);
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

                ProbeErgebnisListe.Add(vm);
            }
        }

        private void RefreshProbeListe()
        {
            // Talente hinzufügen
            ProbeListe.AddRange(Global.ContextTalent.TalentListe.OrderBy(t => t.Name));
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void Würfeln(object obj)
        {
            // Alle Proben neu würfeln
            foreach (var item in ProbeErgebnisListe)
            {
                item.Würfeln(null);
            }
        }

        #endregion

        #region //---- EVENTS ----

        

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
