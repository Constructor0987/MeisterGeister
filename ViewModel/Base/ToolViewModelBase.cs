using MeisterGeister.Logic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Base
{
    public abstract class ToolViewModelBase : ViewModelBase
    {
        #region Konstruktoren

        /// <summary>
        /// ViewModel mit Callbacks.
        /// </summary>
        /// <param name="popup">Ein OK-Popup-Dialog. (Nachricht)</param>
        /// <param name="confirm">Bestätigung einer Ja-Nein-Frage. (Fenstertitel, Frage)</param>
        /// <param name="confirmYesNoCancel">Bestätigen eines YesNoCancel-Dialoges (cancel=0, no=1, yes=2). (Fenstertitel, Frage)</param>
        /// <param name="chooseFile">Wahl einer Datei. (Fenstertitel, Dateiname, zum speichern, Dateierweiterung ...)</param>
        /// <param name="showProbeDialog">Zeigt einen Probe-Dialog an (Probe, Held).</param>
        protected ToolViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Func<string, bool, string> chooseDirectory, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError, Func<string, string, int, int, int, int?> inputIntDialog)
            : base(popup, confirm, confirmYesNoCancel, chooseFile, chooseDirectory, showProbeDialog, showError, inputIntDialog)
        {
        }

        protected ToolViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, chooseFile, null, null, showError, null) { }
        protected ToolViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Func<string, bool, string> chooseDirectory, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, chooseFile, chooseDirectory, null, showError, null) { }
        protected ToolViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, null, null, showProbeDialog, showError, null) { }
        protected ToolViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Action<string, Exception> showError) : this(popup, confirm, confirmYesNoCancel, null, null, null, showError, null) {}
        protected ToolViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError) : this(popup, confirm, null, null, null, showProbeDialog, showError, null) { }
        protected ToolViewModelBase(Action<string> popup, Func<string, string, bool> confirm, Action<string, Exception> showError) : this(popup, confirm, null, null, null, showError) { }
        protected ToolViewModelBase(Action<string> popup, Action<string, Exception> showError) : this(popup, null, null, null, null, showError) { }
        protected ToolViewModelBase(Func<string, string, bool> confirm, Action<string, Exception> showError) : this(null, confirm, null, null, null, showError) { }
        protected ToolViewModelBase(Func<string, string, bool> confirm, Action<string, Exception> showError, Func<string, string, int, int, int, int?> inputIntDialog) : this(null, confirm, null, null, null, null, showError, inputIntDialog) { }
        protected ToolViewModelBase(Action<string, Exception> showError) : this(null, null, null, null, null, showError) {}
        protected ToolViewModelBase() : this(null, null, null, null, null, null) { }
        protected ToolViewModelBase(Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog) : this(null, null, null, showProbeDialog, null) { }
        
        #endregion

        #region //---- EIGENSCHAFTEN ----

        /// <summary>
        /// Name des Tools.
        /// </summary>
        public virtual string Name
        {
            get
            {
                if (Tool == null)
                    return null;
                return Tool.Name;
            }
        }

        /// <summary>
        /// Pfad des Tool-Icons.
        /// </summary>
        public virtual string Icon
        {
            get
            {
                if (Tool == null)
                    return null; 
                return Tool.Icon;
            }
        }

        /// <summary>
        /// Tool
        /// </summary>
        public virtual Tool Tool { get; set; }

        bool isSelected = false;
        /// <summary>
        /// Ist das Tool im TabControl selektiert?
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { Set(ref isSelected, value); }
        }


        #endregion

        #region //----- DESTRUKTOR -----
        #if DEBUG
        /// <summary>
        /// Zwecks überprüfung, ob die ViewModels vom Garbage-Collector
        /// entfernt werden.
        /// </summary>
        ~ToolViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.Name, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
        #endif
        #endregion

        #region //----- COMMANDS ----

        private Base.CommandBase _closeCommand = null;
        public Base.CommandBase CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new Base.CommandBase(param => this.OnRequestClose(), null);
                return _closeCommand;
            }
        }

        #endregion

        #region //---- EVENTS ----

        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            MarshallEvent(RequestClose, EventArgs.Empty);
        }

        #endregion
    }
}
