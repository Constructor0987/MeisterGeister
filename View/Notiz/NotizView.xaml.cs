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
using System.IO;
//Eigene Usings
using VM = MeisterGeister.ViewModel;
//Weitere Usings
using System.Diagnostics;

namespace MeisterGeister.View.Notiz
{
    public partial class NotizView : UserControl
    {
        public NotizView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.Notiz.NotizViewModel).LoadDaten();
            }
            catch (Exception) { }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // aktuelles FlowDocument in RichtTextBox anzeigen
                Model.Notizen notiz = (this.DataContext as VM.Notiz.NotizViewModel).SelectedNotiz.EntityNotiz;
                if (notiz != null)
                    RTBNotiz.Document = notiz.Document;
            }
            catch (Exception) { }
        }

        private void RTBNotiz_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                // Notizen in Datenbank speichern
                Global.ContextNotizen.Save();
            }
            catch (Exception) { }
        }
        
    }
}