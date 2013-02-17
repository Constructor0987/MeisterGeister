using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Base
{
    public abstract class ToolViewModelBase : ViewModelBase
    {
        #region //---- EIGENSCHAFTEN ----

        /// <summary>
        /// Name des Tools.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Pfad des Tool-Icons.
        /// </summary>
        public string Icon { get; protected set; }

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
            if (RequestClose != null) RequestClose(this, EventArgs.Empty);
        }

        #endregion
    }
}
