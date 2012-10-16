using System;
using System.Windows;
using MeisterGeister.Logic.General;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Proben;

namespace MeisterGeister.View.Proben
{
    /// <summary>
    /// Interaktionslogik für ProbeDialog.xaml
    /// </summary>
    public partial class ProbeDialog : Window
    {
        #region //---- KONSTRUKTOREN ----

        public ProbeDialog()
        {
            InitializeComponent();
            VM = new VM.ProbeDialogViewModel();
            VM.RequestClose += VM_RequestClose;
        }

        public ProbeDialog(Probe probe, Model.Held held = null)
        {
            InitializeComponent();
            VM = new VM.ProbeDialogViewModel(probe, held);
            VM.RequestClose += VM_RequestClose;
        }

        #endregion //---- KONSTRUKTOREN ----

        #region //---- EIGENSCHAFTEN & FELDER ----

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.ProbeDialogViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ProbeDialogViewModel))
                    return null;
                return DataContext as VM.ProbeDialogViewModel;
            }
            set { DataContext = value; }
        }

        public ProbenErgebnis Ergebnis
        {
            get
            {
                return VM == null ? new ProbenErgebnis()
                    : VM.Ergebnis;
            }
        }

        #endregion //---- EIGENSCHAFTEN & FELDER ----

        #region //---- EVENT HANDLER ----

        private void VM_RequestClose(object sender, EventArgs e)
        {
            DialogResult = VM.DialogResult;
            Close();
        }

        #endregion //---- EVENT HANDLER ----

    }
}
