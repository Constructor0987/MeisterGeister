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

        private ObservableCollection<ProbeControlViewModel> _probeItemListe = new ObservableCollection<ProbeControlViewModel>();
        public ObservableCollection<ProbeControlViewModel> ProbeItemListe
        {
            get { return _probeItemListe; }
            set
            {
                _probeItemListe = value;
                OnChanged("ProbeItemListe");
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public ProbenViewModel()
        {
            // Talentauswahl zum Testen
            Model.Talent talent = Global.ContextTalent.TalentListe.Where(t => t.Talentname == "Sinnenschärfe").FirstOrDefault();

            foreach (var item in HeldListe)
            {
                ProbeControlViewModel vm = new ProbeControlViewModel()
                {
                    Held = item,
                    Probe = talent
                };

                ProbeItemListe.Add(vm);

                //ProbeItemListe.Add(new ProbeItem() {
                //    //Held = HeldListe.FirstOrDefault(),
                //    //Probe = talent, 
                //    VM = vm
                //});
            }

            ProbeItemListe.Add(new ProbeControlViewModel() {Held = HeldListe[0], Probe = new Probe()});

            OnChanged("ProbeItemListe");

            onWürfeln = new Base.CommandBase(Würfeln, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void Würfeln(object obj)
        {
            // Alle Proben neu würfeln
            foreach (var item in ProbeItemListe)
            {
                item.Würfeln(null);
            }
        }

        #endregion

        #region //---- EVENTS ----

        

        #endregion
    }

    #region //---- SUBKLASSEN ----

    public class ProbeItem : Base.ViewModelBase
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
