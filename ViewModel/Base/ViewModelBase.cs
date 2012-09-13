using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Base {
    public abstract class ViewModelBase : INotifyPropertyChanged {

        #region //----- FELDER ----
        
        private bool _hasValidationErrors = false;

        private Action<string> popup;
        private Action<string, Exception> showError;
        private Func<string, string, bool> confirm;
        private Func<string, string, int> confirmYesNoCancel;
        private Func<string, string, bool, string[], string> chooseFile;
        #endregion

        #region Konstruktoren

        /// <summary>
        /// ViewModel mit Callbacks.
        /// </summary>
        /// <param name="popup">Ein OK-Popup-Dialog. (Nachricht)</param>
        /// <param name="confirm">Bestätigung einer Ja-Nein-Frage. (Fenstertitel, Frage)</param>
        /// <param name="confirmYesNoCancel">Bestätigen eines YesNoCancel-Dialoges (cancel=0, no=1, yes=2). (Fenstertitel, Frage)</param>
        /// <param name="chooseFile">Wahl einer Datei. (Fenstertitel, Dateierweiterung, Dateiname, zum speichern)</param>
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, string[], string> chooseFile, Action<string, Exception> showError)
        {
            this.popup = popup;
            this.confirm = confirm;
            this.confirmYesNoCancel = confirmYesNoCancel;
            this.chooseFile = chooseFile;
            this.showError = showError;
        }

        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, null, showError) {}
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Action<string, Exception> showError) : this(popup, confirm, null, null, showError) { }
        protected ViewModelBase(Action<string> popup, Action<string, Exception> showError) : this(popup, null, null, null, showError) { }
        protected ViewModelBase(Func<string, string, bool> confirm, Action<string, Exception> showError) : this(null, confirm, null, null, showError) { }
        protected ViewModelBase(Action<string, Exception> showError) : this(null, null, null, null, showError) {}
        protected ViewModelBase() : this(null, null, null, null, null) { }

        #endregion

        #region Methoden

        /// <summary>
        /// Um Informationen anzuzeigen.
        /// Entspricht einer Messagebox mit dem Informationssymbol und einem OK-Button.
        /// </summary>
        /// <param name="text">anzuzeigender Text</param>
        public void PopUp(string text)
        {
            if (popup != null)
                popup(text);
        }

        /// <summary>
        /// Um eine Bestätigung vom Benutzer zu holen.
        /// Entspricht einer Messagebox mit dem Warnungssymbol sowie einem Ja- und Nein-Button.
        /// </summary>
        /// <param name="header">Fenstertitel</param>
        /// <param name="text">anzuzeigender Text</param>
        /// <returns>true bei Klick auf Ja, sonst false</returns>
        public bool Confirm(string header, string text)
        {
            if (confirm != null)
                return confirm(header, text);
            return false;
        }
        
        /// <summary>
        /// Um eine Bestätigung vom Benutzer zu holen.
        /// Entspricht einer Messagebox mit dem Warnungssymbol sowie einem Ja-, Nein- und Abbrechen-Button.
        /// </summary>
        /// <param name="header">Fenstertitel</param>
        /// <param name="text">anzuzeigender Text</param>
        /// <returns>Abbrechen=0, Nein=1, Ja=2</returns>
        public int ConfirmYesNoCancel(string header, string text)
        {
            if (confirmYesNoCancel != null)
                return confirmYesNoCancel(header, text);
            return 0;
        }

        /// <summary>
        /// Zum Anzeigen eines Fehlers.
        /// </summary>
        /// <param name="text">anzuzeigender Text</param>
        /// <param name="ex">auslösende Exception</param>
        public void ShowError(string text, Exception ex)
        {
            if (showError != null)
                showError(text, ex);
        }

        /// <summary>
        /// Zur Auswahl einer Datei.
        /// </summary>
        /// <param name="header">Fenstertitel</param>
        /// <param name="filename">vorbesetzter Dateiname</param>
        /// <param name="save">true für einen SaveDialog, false für einen OpenDialog</param>
        /// <param name="extensions">Dateierweiterungen</param>
        /// <returns>Den ausgewählten Dateipfad oder null</returns>
        public string ChooseFile(string header, string filename, bool save, params string[] extensions)
        {
            if (chooseFile != null)
                return chooseFile(header, filename, save, extensions);
            return null;
        }

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