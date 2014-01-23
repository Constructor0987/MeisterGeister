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

        private bool _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public EigenschaftenViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;

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
            if (SelectedHeld != null && energieTyp is View.General.EnergieEnum)
            {
                switch ((View.General.EnergieEnum)energieTyp)
                {
                    case View.General.EnergieEnum.Lebensenergie:
                        SelectedHeld.LebensenergieAktuell = SelectedHeld.LebensenergieMax;
                        break;
                    case View.General.EnergieEnum.Ausdauer:
                        SelectedHeld.AusdauerAktuell = SelectedHeld.AusdauerMax;
                        break;
                    case View.General.EnergieEnum.Astralenergie:
                        SelectedHeld.AstralenergieAktuell = SelectedHeld.AstralenergieMax;
                        break;
                    case View.General.EnergieEnum.Karmaenergie:
                        SelectedHeld.KarmaenergieAktuell = SelectedHeld.KarmaenergieMax;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region //---- EVENTS ----

        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }

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
