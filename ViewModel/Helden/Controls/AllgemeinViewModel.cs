using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Helden.Controls {
    public class AllgemeinViewModel : Base.ViewModelBase {
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
        #endregion
        #region //KONSTRUKTOR
        public AllgemeinViewModel() {
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            Global.HeldSelectionChanging += (s, ev) => { SelectedHeldChanging(); };

            //TODO DW: Wenn Heldenliste wieder bedienbar, entfernen
            if (Global.SelectedHeld == null) {
                Global.SelectedHeld = new M.Held() { Name = "Mein Held" };                
            }
        }
        #endregion
        #region //METHODEN
        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args) {
            if (new string[] { "Name", "BildLink", "Rasse", "Kultur", "Profession", "AktiveHeldengruppe" }.Contains(args.PropertyName))
                hasChanges = true;
        }
        #endregion
        #region //EVENTS
        //Event
        void SelectedHeldChanged() {
            SelectedHeld = Global.SelectedHeld;
            if (SelectedHeld != null)
                SelectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
            if (RefreshNotiz != null) {
                RefreshNotiz(this, new EventArgs());
            }
        }
        void SelectedHeldChanging() {
            if (Global.SelectedHeld != null) {
                if (hasChanges)
                    Global.ContextHeld.Update<Model.Held>(SelectedHeld);
                hasChanges = false;
                Global.SelectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
            }
        }
        #endregion
    }
}
