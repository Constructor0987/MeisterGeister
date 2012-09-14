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

        private System.Windows.Controls.TabItem _selectedTabItem = null;
        /// <summary>
        /// Der aktuell ausgewählte Tab.
        /// </summary>
        public System.Windows.Controls.TabItem SelectedTabItem
        {
            get { return _selectedTabItem; }
            set { _selectedTabItem = value; OnChanged("SelectedTabItem"); }
        }

        private int _selectedTabIndex = 0;
        public int SelectedTabIndex 
        {
            get { return _selectedTabIndex; }
            set { _selectedTabIndex = value < -1 ? -1 : value; OnChanged("SelectedTabIndex"); }
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

        public void NotifyRefresh()
        {
            OnChanged("SelectedHeld");
            
            // Prüfen, ob ein ausgeblendeter Tab ausgewält ist
            if (SelectedTabItem == null
                || SelectedTabItem.Visibility != System.Windows.Visibility.Visible)
                SelectedTabIndex--;
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
