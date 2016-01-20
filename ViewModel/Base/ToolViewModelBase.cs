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
        public virtual string Name { get { return Tool.Name; } }

        /// <summary>
        /// Pfad des Tool-Icons.
        /// </summary>
        public virtual string Icon { get { return Tool.Icon; } }

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
