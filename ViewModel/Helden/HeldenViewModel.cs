using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Helden
{
    public class HeldenViewModel : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

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

        #endregion

        #region //---- KONSTRUKTOR ----

        public HeldenViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            
        }

        #endregion

        #region //---- EVENTS ----

        private void SelectedHeldChanged()
        {
            OnChanged("SelectedHeld");
        }

        #endregion

    }
}
