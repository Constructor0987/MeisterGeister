using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Würfeln
{
    
    public class WürfelDialogViewModel : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onWürfeln;
        public Base.CommandBase OnWürfeln
        {
            get { return onWürfeln; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        private int _ergebnis;
        public int Ergebnis
        {
            get { return _ergebnis; }
            set { _ergebnis = value; OnChanged("Ergebnis"); }
        }

        private string _würfel;
        public string Würfel
        {
            get { return _würfel; }
            set { _würfel = value; OnChanged("Würfel"); }
        }

        private string _infoText;
        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; OnChanged("InfoText"); }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public WürfelDialogViewModel(string würfel)
        {
            Würfel = würfel;
            Würfeln(null);

            onWürfeln = new Base.CommandBase(Würfeln, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void Würfeln(object obj)
        {
            Ergebnis = Logic.General.Würfel.Parse(Würfel, true);
        }

        #endregion

        #region //---- EVENTS ----


        #endregion
        
    }
}
