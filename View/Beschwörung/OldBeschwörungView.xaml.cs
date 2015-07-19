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
using System.Windows.Navigation;
using System.Windows.Shapes;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Beschwörung;
//Weitere Usings
using System.Diagnostics;

namespace MeisterGeister.View.Beschwörung
{
    /// <summary>
    /// Interaktionslogik für BeschwörungView.xaml
    /// </summary>
    public partial class OldBeschwörungView : UserControl
    {
        public OldBeschwörungView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.OldBeschwörungViewModel).LoadDaten();
            }
            catch (Exception ex)
            {
                View.Windows.MsgWindow errWin = new View.Windows.MsgWindow("Beschwörung-Tool", "Beim Laden des Beschwörung-Tools ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {

        }
    }
}
