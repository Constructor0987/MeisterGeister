using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class EigenschaftenViewModel : Base.ViewModelBase, Logic.IChangeListener
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onMaxEnergie;
        public Base.CommandBase OnMaxEnergie
        {
            get { return onMaxEnergie; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        // Selection
        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }

        public System.Windows.Visibility HinweisMagieKarmaVisibility
        {
            get 
            {
                if (SelectedHeld == null || !(SelectedHeld.Magiebegabt || SelectedHeld.Geweiht))
                    return System.Windows.Visibility.Visible;
                return System.Windows.Visibility.Collapsed;
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public EigenschaftenViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };

            onMaxEnergie = new Base.CommandBase(SetEnergieMax, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            
        }

        public void NotifyRefresh()
        {
            OnChanged("SelectedHeld");
            OnChanged("HinweisMagieKarmaVisibility");
        }

        private void SetEnergieMax(object energieTyp)
        {
            if (energieTyp is MeisterGeister.View.General.EnergieEnum)
            {
                switch ((MeisterGeister.View.General.EnergieEnum)energieTyp)
                {
                    case MeisterGeister.View.General.EnergieEnum.Lebensenergie:
                        SelectedHeld.LebensenergieAktuell = SelectedHeld.LebensenergieMax;
                        break;
                    case MeisterGeister.View.General.EnergieEnum.Ausdauer:
                        SelectedHeld.AusdauerAktuell = SelectedHeld.AusdauerMax;
                        break;
                    case MeisterGeister.View.General.EnergieEnum.Astralenergie:
                        SelectedHeld.AstralenergieAktuell = SelectedHeld.AstralenergieMax;
                        break;
                    case MeisterGeister.View.General.EnergieEnum.Karmaenergie:
                        SelectedHeld.KarmaenergieAktuell = SelectedHeld.KarmaenergieMax;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region //---- EVENTS ----

        private void SelectedHeldChanged()
        {
            if (!ListenToChangeEvents)
                return;
            NotifyRefresh();
        }

        #endregion

        private bool listenToChangeEvents = true;

        public bool ListenToChangeEvents
        {
            get { return listenToChangeEvents; }
            set { listenToChangeEvents = value; SelectedHeldChanged(); }
        }
        
    }
    
}
