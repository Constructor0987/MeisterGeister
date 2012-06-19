using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Weitere Usings
using System.Windows.Input;

namespace MeisterGeister.ViewModel.Base {
    public class CommandBase : ICommand {

        #region //----- FELDER -----
                
        private readonly Action<object> _executeHandler;
        private readonly Func<object, bool> _canExecuteHandler;

        #endregion

        #region //---- KONSTRUKTOR -----
        
        public CommandBase(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute cannot be null");

            _executeHandler = execute;
            _canExecuteHandler = canExecute;
        }

        #endregion

        #region //---- EVENTS -----

        public event EventHandler CanExecuteChanged;

        public void Invalidate()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null)
                return true;
            return _canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }

        #endregion
    }
}

