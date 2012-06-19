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
using VM = MeisterGeister.ViewModel.Zauberzeichen;
//Weitere Usings
using System.Diagnostics;

namespace MeisterGeister.View.Zauberzeichen
{
    /// <summary>
    /// Interaktionslogik für ZauberzeichenView.xaml
    /// </summary>
    public partial class ZauberzeichenView : UserControl
    {
        public ZauberzeichenView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.ZauberzeichenViewModel).LoadDaten();
            }
            catch (Exception ex)
            {
                View.Windows.MsgWindow errWin = new View.Windows.MsgWindow("Zauberzeichen-Tool", "Beim Laden des Zauberzeichen-Tools ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }
    }
}
