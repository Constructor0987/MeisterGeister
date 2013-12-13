using System;
using System.Windows.Input;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    // Serves as an abstraction of Actions performed by the user via interaction with the UI (Button Click,...)
    class BattlegroundCommand:ICommand
    {
        public Action Action { get; set; }

        public void Execute(object parameter)
        {
            if (Action != null)
                Action();
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        private bool _isEnabled=true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public BattlegroundCommand(Action action)
        {
            Action = action;
        }
    }
}
