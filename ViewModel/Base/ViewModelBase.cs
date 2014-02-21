using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using MeisterGeister.Logic.General;
using System.Windows.Threading;
using System.Runtime.CompilerServices;

namespace MeisterGeister.ViewModel.Base {
    public abstract class ViewModelBase : INotifyPropertyChanged {

        #region //----- FELDER ----
        
        private bool _hasValidationErrors = false;

        private Action<string> popup;
        private Action<string, Exception> showError;
        private Func<string, string, bool> confirm;
        private Func<string, string, int> confirmYesNoCancel;
        private Func<string, string, bool, bool, string[], string> chooseFile;
        private Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog;

        public virtual Dispatcher Dispatcher { get; protected set; }
        #endregion

        #region Konstruktoren

        /// <summary>
        /// ViewModel mit Callbacks.
        /// </summary>
        /// <param name="popup">Ein OK-Popup-Dialog. (Nachricht)</param>
        /// <param name="confirm">Bestätigung einer Ja-Nein-Frage. (Fenstertitel, Frage)</param>
        /// <param name="confirmYesNoCancel">Bestätigen eines YesNoCancel-Dialoges (cancel=0, no=1, yes=2). (Fenstertitel, Frage)</param>
        /// <param name="chooseFile">Wahl einer Datei. (Fenstertitel, Dateiname, zum speichern, Dateierweiterung ...)</param>
        /// <param name="showProbeDialog">Zeigt einen Probe-Dialog an (Probe, Held).</param>
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError)
        {
            this.Dispatcher = Dispatcher.CurrentDispatcher;
            this.popup = popup;
            this.confirm = confirm;
            this.confirmYesNoCancel = confirmYesNoCancel;
            this.chooseFile = chooseFile;
            this.showError = showError;
            this.showProbeDialog = showProbeDialog;
        }

        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, chooseFile, null, showError) { }
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, null, showProbeDialog, showError) { }
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, null, null, showError) {}
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError) : this(popup, confirm, null, null, showProbeDialog, showError) { }
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Action<string, Exception> showError) : this(popup, confirm, null, null, null, showError) { }
        protected ViewModelBase(Action<string> popup, Action<string, Exception> showError) : this(popup, null, null, null, null, showError) { }
        protected ViewModelBase(Func<string, string, bool> confirm, Action<string, Exception> showError) : this(null, confirm, null, null, null, showError) { }
        protected ViewModelBase(Action<string, Exception> showError) : this(null, null, null, null, null, showError) {}
        protected ViewModelBase() : this(null, null, null, null, null, null) { }
        protected ViewModelBase(Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog) : this(null, null, null, showProbeDialog, null) { }

        #endregion

        #region Methoden

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool Set<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnChanged(propertyName);
            return true;
        }

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
        /// <param name="askRelativePath">true, falls der User gefragt werden soll, ob der Pfad relativ oder absolut angegeben werden soll</param>
        /// <param name="extensions">erlaubte Dateierweiterungen</param>
        public string ChooseFile(string header, string filename, bool save, bool askRelativePath, params string[] extensions)
        {
            if (chooseFile != null)
                return chooseFile(header, filename, save, askRelativePath, extensions);
            return null;
        }

        /// <summary>
        /// Zur Anzeige eines Probe-Dialogs.
        /// </summary>
        /// <param name="probe">Probe</param>
        /// <param name="held">Held der die Probe ausführt</param>
        /// <returns>Ergebnis des Proben-Wurfs</returns>
        public ProbenErgebnis ShowProbeDialog(Probe probe, Model.Held held)
        {
            if (showProbeDialog != null)
                return showProbeDialog(probe, held);
            return null;
        }

        /// <summary>
        /// Gibt zurück, ob eine Exception gewurfen werden soll, oder Debug.Fail() 
        /// genutz wird, falls ein nicht vorhandener Property-Name als String an
        /// OnChanged bzw. VerifyPropertyName übergeben wird.
        /// Standartrückgabewert ist false. Unterklassen können für Unit-Tests diesen
        /// Getter überschreiben, um true zurückzugeben.
        /// <returns>true, falls eine Exeption geworfen werden soll.</returns>
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// Verifiziert als String übergebene Properties
        /// </summary>
        /// <param name="propertyString">String/Name des zu verifizierenden Property</param>
        [Conditional("DEBUG_PROPERTIES")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyString)
        {
            if (TypeDescriptor.GetProperties(this)[propertyString] == null)
            {
                string msg = "Invalid property name: " + propertyString;
                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        #endregion

        #region //---- EIGENSCHAFTEN -----

        public bool HasValidationErrors {
            get { return _hasValidationErrors; }
            set { _hasValidationErrors = value; }
        }

        #endregion

        #region //---- EVENTS ----
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnChanged([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        virtual public void Destroy() { }

        #endregion

    }
}