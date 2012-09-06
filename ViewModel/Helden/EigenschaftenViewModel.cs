using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class EigenschaftenViewModel : Base.ViewModelBase
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

        #endregion

        #region //---- EVENTS ----

        private void SelectedHeldChanged()
        {
            NotifyRefresh();
        }

        #endregion

    }
    
}
