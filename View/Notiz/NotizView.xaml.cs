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
                VM.LoadDaten();
            }
            catch (Exception) { }
        }

        public VM.Notiz.NotizViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.Notiz.NotizViewModel))
                    return null;
                return DataContext as VM.Notiz.NotizViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object oldNotizObject = null;
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                    oldNotizObject = e.RemovedItems[0];

                // aktuelles FlowDocument in RichtTextBox anzeigen
                Model.Notizen notiz = VM.SelectedNotiz.EntityNotiz;
                if (notiz != null)
                {
                    if (oldNotizObject != null)
                    {
                        // Scroll-Position speichern
                        ViewModel.Notiz.NotizViewModel.NotizItem oldNotizItem = (ViewModel.Notiz.NotizViewModel.NotizItem)oldNotizObject;
                        oldNotizItem.VerticalOffset = RTBNotiz.RTBBox.VerticalOffset;
                    }
                    RTBNotiz.Document = notiz.Document;
                    RTBNotiz.RTBBox.ScrollToVerticalOffset(VM.SelectedNotiz.VerticalOffset);
                }
            }
            catch (Exception) { }
        }

        private void RTBNotiz_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                // Notizen in Datenbank speichern
                Global.ContextNotizen.SaveNotizen();
            }
            catch (Exception) { }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Notiz-Document freigeben
            try
            {
                RTBNotiz.Document = new FlowDocument();
            }
            catch (Exception) { }
        }
        
    }
}