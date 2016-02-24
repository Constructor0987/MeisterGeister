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
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging {

        #region //----- FELDER ----
        
        private bool _hasValidationErrors = false;

        private Action<string> popup;
        public Action<string> PopupDelegate
        {
            get { return popup; }
            set { popup = value; }
        }
        private Action<string, Exception> showError;
        public Action<string, Exception> ShowErrorDelegate
        {
            get { return showError; }
            set { showError = value; }
        }
        private Func<string, string, bool> confirm;
        public Func<string, string, bool> ConfirmDelegate
        {
            get { return confirm; }
            set { confirm = value; }
        }
        private Func<string, string, int> confirmYesNoCancel;
        public Func<string, string, int> ConfirmYesNoCancelDelegate
        {
            get { return confirmYesNoCancel; }
            set { confirmYesNoCancel = value; }
        }
        private Func<string, string, bool, bool, string[], string> chooseFile;
        public Func<string, string, bool, bool, string[], string> ChooseFileDelegate
        {
            get { return chooseFile; }
            set { chooseFile = value; }
        }
        private Func<string, bool, string> chooseDirectory;
        public Func<string, bool, string> ChooseDirectoryDelegate
        {
            get { return chooseDirectory; }
            set { chooseDirectory = value; }
        }
        private Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog;
        public Func<Probe, Model.Held, ProbenErgebnis> ShowProbeDialogDelegate
        {
            get { return showProbeDialog; }
            set { showProbeDialog = value; }
        }
        private Func<string, string, int, int, int, int?> inputIntDialog;
        public Func<string, string, int, int, int, int?> InputIntDialogDelegate
        {
            get { return inputIntDialog; }
            set { inputIntDialog = value; }
        }

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
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Func<string, bool, string> chooseDirectory, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError, Func<string, string, int, int, int, int?> inputIntDialog)
        {
            this.Dispatcher = Dispatcher.CurrentDispatcher;
            this.popup = popup;
            this.confirm = confirm;
            this.confirmYesNoCancel = confirmYesNoCancel;
            this.chooseFile = chooseFile;
            this.chooseDirectory = chooseDirectory;
            this.showError = showError;
            this.showProbeDialog = showProbeDialog;
            this.inputIntDialog = inputIntDialog;
        }

        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, chooseFile, null, null, showError, null) { }
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Func<string, bool, string> chooseDirectory, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, chooseFile, chooseDirectory, null, showError, null) { }
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, null, null, showProbeDialog, showError, null) { }
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, null, null, null, showError, null) {}
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError) : this(popup, confirm, null, null, null, showProbeDialog, showError, null) { }
        protected ViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Action<string, Exception> showError) : this(popup, confirm, null, null, null, showError) { }
        protected ViewModelBase(Action<string> popup, Action<string, Exception> showError) : this(popup, null, null, null, null, showError) { }
        protected ViewModelBase(Func<string, string, bool> confirm, Action<string, Exception> showError) : this(null, confirm, null, null, null, showError) { }
        protected ViewModelBase(Func<string, string, bool> confirm, Action<string, Exception> showError, Func<string, string, int, int, int, int?> inputIntDialog) : this(null, confirm, null, null, null, null, showError, inputIntDialog) { }
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
            this.OnChanging(propertyName);
            storage = value;
            this.OnChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Setzt die Dialog-Delegate-Functions auf die von MeisterGeister.View.General.ViewHelper.
        /// </summary>
        public void SetFromViewHelper()
        {
            this.popup = MeisterGeister.View.General.ViewHelper.Popup;
            this.confirm = MeisterGeister.View.General.ViewHelper.Confirm;
            this.confirmYesNoCancel = MeisterGeister.View.General.ViewHelper.ConfirmYesNoCancel;
            this.chooseFile = MeisterGeister.View.General.ViewHelper.ChooseFile;
            this.chooseDirectory = MeisterGeister.View.General.ViewHelper.ChooseDirectory;
            this.showError = MeisterGeister.View.General.ViewHelper.ShowError;
            this.showProbeDialog = MeisterGeister.View.General.ViewHelper.ShowProbeDialog;
            this.inputIntDialog = MeisterGeister.View.General.ViewHelper.InputIntDialog;
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
        /// Auswahl eines Verzeichnisses.
        /// </summary>
        /// <param name="path">Vorausgewähltes Verzeichnis.</param>
        /// <param name="askRelativePath">true, falls der User gefragt werden soll, ob der Pfad relativ oder absolut angegeben werden soll</param>
        /// <returns></returns>
        public string ChooseDirectory(string path, bool askRelativePath)
        {
            if (chooseDirectory != null)
                return chooseDirectory(path, askRelativePath);
            return null;
        }

        public int? InputIntDialog(string caption, string msg, int init, int min = int.MinValue, int max = int.MaxValue)
        {
            if (inputIntDialog != null)
                return inputIntDialog(caption, msg, init, min, max);
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

        /// <summary>
        /// Dies ist die richtige Stelle um Events anzumelden
        /// </summary>
        public virtual void RegisterEvents()
        {

        }

        /// <summary>
        /// Dies ist die richtige Stelle um Events abzumelden
        /// </summary>
        public virtual void UnregisterEvents()
        {

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
            _propertyChanged.Raise(this, new PropertyChangedEventArgs(propertyName));
        }
        WeakEvent<PropertyChangedEventHandler> _propertyChanged = new WeakEvent<PropertyChangedEventHandler>();
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged.Add(value); }
            remove { _propertyChanged.Remove(value); }
        }

        /// <summary>
        /// Führt ein Event in dem bei Konstruktion gespeicherten Thread (meist UI-Thread) aus.
        /// Funktioniert nicht mit PropertyChanged. WPF macht das bei PropertyChanged automatisch.
        /// </summary>
        /// <param name="eventhandler"></param>
        /// <param name="args"></param>
        protected void MarshallEvent(EventHandler eventhandler, EventArgs args)
        {
            if(Dispatcher.Thread == System.Threading.Thread.CurrentThread)
            {
                if (eventhandler != null)
                    eventhandler(this, args);
            }
            else
            {
                Dispatcher.BeginInvoke((Action<EventHandler, EventArgs>)MarshallEvent, DispatcherPriority.Normal, eventhandler, args);
            }
        }

        virtual public void Destroy() { }

        /// <summary>
        /// Notifies listeners that a property value ís changing.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnChanging([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);
            _propertyChanging.Raise(this, new PropertyChangingEventArgs(propertyName));
        }

        WeakEvent<PropertyChangingEventHandler> _propertyChanging = new WeakEvent<PropertyChangingEventHandler>();
        public event PropertyChangingEventHandler PropertyChanging
        {
            add { _propertyChanging.Add(value); }
            remove { _propertyChanging.Remove(value); }
        }
        #endregion
    }
}