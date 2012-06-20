using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Helden.Controls
{
    public class VorNachteileViewModel : Base.ViewModelBase
    {
        #region //---- FELDER ----

        // Felder

        // Listen

        // Commands


        #endregion

        #region //---- EIGENSCHAFTEN ----

        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }

        // Listen


        // Commands


        #endregion

        #region //---- KONSTRUKTOR ----

        public VorNachteileViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
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
