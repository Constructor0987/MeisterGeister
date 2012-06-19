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
// Eigene Usings
using VM = MeisterGeister.ViewModel;
using MeisterGeister.View.Windows;
// Weitere Usings

namespace MeisterGeister.View.Basar
{
    /// <summary>
    /// Interaktionslogik für BasarView.xaml
    /// </summary>
    public partial class BasarView : UserControl
    {
        public BasarView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.Basar.BasarViewModel).LoadDaten();
            }
            catch (Exception ex) 
            {
                MsgWindow errWin = new MsgWindow("Basar-Tool", "Beim Laden des Basar-Tools ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }

    }
}
