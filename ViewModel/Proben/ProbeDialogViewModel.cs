using System;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Proben
{
    public class ProbeDialogViewModel : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onOkay;
        public Base.CommandBase OnOkay
        {
            get
            {
                if (onOkay == null)
                    onOkay = new Base.CommandBase(Okay, null);
                return onOkay;
            }
        }

        private void Okay(object obj)
        {
            DialogResult = true;
            if (RequestClose != null)
                RequestClose(this, new EventArgs());
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        private bool? _dialogResult;
        public bool? DialogResult 
        {
            get { return _dialogResult; }
            set { _dialogResult = value; OnChanged("DialogResult"); }
        }

        public ProbenErgebnis Ergebnis
        {
            get { return ProbeControlViewModel.Ergebnis; }
            set { ProbeControlViewModel.Ergebnis = value; OnChanged("Ergebnis"); }
        }

        private ProbeControlViewModel _probeControlViewModel = new ProbeControlViewModel();
        public ProbeControlViewModel ProbeControlViewModel
        {
            get { return _probeControlViewModel; }
            set { _probeControlViewModel = value; OnChanged("ProbeControlViewModel"); }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public ProbeDialogViewModel() { }

        public ProbeDialogViewModel(Probe probe, Model.Held held)
        {
            ProbeControlViewModel.Held = held;
            ProbeControlViewModel.Probe = probe;
            ProbeControlViewModel.Würfeln();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        

        #endregion

        #region //---- EVENTS ----

        public EventHandler RequestClose;

        #endregion

    }
}
