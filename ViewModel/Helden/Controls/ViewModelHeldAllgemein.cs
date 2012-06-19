using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Helden.Controls {
    public class ViewModelHeldAllgemein : Base.ViewModelBase {

        #region //FELDER
        private Model.Held selectedHeld = new M.Held();
        private bool hasChanges = false;
        #endregion

        #region //EIGENSCHAFTEN
        public Model.Held SelectedHeld {
            get { return selectedHeld; }
            set {
                selectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }

        #endregion

        public ViewModelHeldAllgemein() {
            //Commands Initialisiert
            //EventHanler HeldSelectionChanged
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            Global.HeldSelectionChanging += (s, ev) => { SelectedHeldChanging(); };
        }

        void SelectedHeldChanged() {
            SelectedHeld = Global.SelectedHeld;
            if (SelectedHeld != null)
                SelectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
            if (RefreshNotiz != null) {
                RefreshNotiz(this, new EventArgs());
            }
        }
        void SelectedHeldChanging()
        {
            if (Global.SelectedHeld != null)
            {
                if(hasChanges)
                    Global.ContextHeld.Update<Model.Held>(SelectedHeld);
                hasChanges = false;
                Global.SelectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
            }
        }

        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (new string[] { "Name", "BildLink", "Rasse", "Kultur", "Profession", "AktiveHeldengruppe" }.Contains(args.PropertyName))
                hasChanges = true;
        }

        #region //EVENTS
            public event EventHandler RefreshNotiz;
        #endregion
    }
}
