using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Helden {
    public class AllgemeinViewModel : Base.ViewModelBase, Logic.IChangeListener {
        #region //FELDER
        //EasyTypes
        private bool hasChanges = false;
        //Selection
        private Model.Held selectedHeld = new M.Held();
        #endregion
        
        #region //EIGENSCHAFTEN
        
        //Selection
        public Model.Held SelectedHeld { get { return Global.SelectedHeld; } set { Global.SelectedHeld = value; OnChanged("SelectedHeld"); } }
        
        //Events
        public event EventHandler RefreshNotiz;

        public bool IsReadOnly
        {
            get { return MeisterGeister.Logic.Settings.Einstellungen.IsReadOnly; }
        }

        #endregion

        #region //KONSTRUKTOR

        public AllgemeinViewModel() {
            AttachEvents();
        }

        #endregion

        #region //METHODEN

        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args) {
            if (new string[] { "Name", "BildLink", "Rasse", "Kultur", "Profession", "AktiveHeldengruppe" }.Contains(args.PropertyName))
                hasChanges = true;
        }

        private void AttachEvents()
        {
            Global.HeldSelectionChanged += SelectedHeldChanged;
            Global.HeldSelectionChanging += SelectedHeldChanging;
            MeisterGeister.Logic.Settings.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;
            SelectedHeldChanged(null, null);
        }

        private void DetachEvents()
        {
            SelectedHeldChanging(null, null);
            Global.HeldSelectionChanged -= SelectedHeldChanged;
            Global.HeldSelectionChanging -= SelectedHeldChanging;
            MeisterGeister.Logic.Settings.Einstellungen.IsReadOnlyChanged -= IsReadOnlyChanged;
        }

        #endregion

        #region //EVENTS
        
        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            OnChanged("IsReadOnly");
        }

        //Event
        void SelectedHeldChanged(object sender, EventArgs args)
        {
            SelectedHeld = Global.SelectedHeld;
            if (SelectedHeld != null)
                SelectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
            if (RefreshNotiz != null) {
                RefreshNotiz(this, new EventArgs());
            }
        }
        void SelectedHeldChanging(object sender, EventArgs args)
        {
            if (Global.SelectedHeld != null) {
                if (hasChanges)
                    Global.ContextHeld.Update<Model.Held>(SelectedHeld);
                hasChanges = false;
                Global.SelectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
            }
        }
        #endregion

        private bool listenToChangeEvents = true;

        public bool ListenToChangeEvents
        {
            get { return listenToChangeEvents; }
            set { 
                listenToChangeEvents = value;
                if (listenToChangeEvents)
                    AttachEvents();
                else
                    DetachEvents();
            }
        }
        
    }
}
