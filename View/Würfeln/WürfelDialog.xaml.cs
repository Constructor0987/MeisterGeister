using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MeisterGeister.Logic.General;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Würfeln;

namespace MeisterGeister.View.Würfeln
{
    /// <summary>
    /// Interaktionslogik für WürfelDialog.xaml
    /// </summary>
    public partial class WürfelDialog : Window
    {
        public WürfelDialog()
        {
            InitializeComponent();
            VM = new VM.WürfelDialogViewModel("1W6");
        }

        /// <summary>
        /// Ein Würfel Dialog-Fenster.
        /// </summary>
        /// <param name="würfel">Würfel-Text (z.B. 2W+3, W20).</param>
        /// <param name="infoText">Info-Text, der angezeigt wird und den Würfelwurf beschreibt.</param>
        public WürfelDialog(string würfel, string infoText = "")
        {
            InitializeComponent();

            VM = new VM.WürfelDialogViewModel(würfel);
            VM.InfoText = infoText;
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.WürfelDialogViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.WürfelDialogViewModel))
                    return null;
                return DataContext as VM.WürfelDialogViewModel;
            }
            set { DataContext = value; }
        }

        public int Ergebnis
        {
            get
            {
                return VM == null ? 0
                    : VM.Ergebnis;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Interop.ComponentDispatcher.IsThreadModal)
                DialogResult = true;
            else
                Close();
        }
    }
}
