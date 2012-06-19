using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Base {
    public abstract class ViewModelBase : INotifyPropertyChanged {

        #region //----- FELDER ----
        
        private bool _hasValidationErrors = false;

        #endregion

        #region //---- EIGENSCHAFTEN -----
        
        public bool HasValidationErrors {
            get { return _hasValidationErrors; }
            set { _hasValidationErrors = value; }
        }

        #endregion

        #region //---- EVENTS ----
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnChanged(string propertyName) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        virtual public void Destroy() { }

        #endregion

    }
}