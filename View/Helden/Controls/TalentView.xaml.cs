using System;
using System.Collections.Generic;
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
using VM = MeisterGeister.ViewModel.Helden;
using MeisterGeister.View.Kampf;


namespace MeisterGeister.View.Helden.Controls {
    /// <summary>
    /// Interaktionslogik für TalentView.xaml
    /// </summary>
    public partial class TalentView : UserControl {

        #region // ---- KONSTRUKTOR ----

        public TalentView() {
            this.InitializeComponent();
            VM = new VM.TalentViewModel(View.General.ViewHelper.Popup, View.General.ViewHelper.Confirm, View.General.ViewHelper.ShowProbeDialog, View.General.ViewHelper.ShowError);
        }

        #endregion

        #region // ---- EIGENSCHAFTEN ----

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.TalentViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.TalentViewModel))
                    return null;
                return DataContext as VM.TalentViewModel;
            }
            set { DataContext = value; }
        }

        #endregion

        #region // ---- EVENTS ----

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            try {
                VM.Init();
            } catch (Exception) {
            }
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }
        
        private void ContextMenuTalent_Opened(object sender, RoutedEventArgs e)
        {
            //if (_dataGridHeldTalente.SelectedItem == null) {
            //    _menuItemTalentLöschen.IsEnabled = false;
            //    _menuItemTalentWiki.IsEnabled = false;
            //    _menuItemTalentProben.IsEnabled = false;
            //} else {
            //    _menuItemTalentLöschen.IsEnabled = true;
            //    _menuItemTalentWiki.IsEnabled = true;
            //    _menuItemTalentProben.IsEnabled = true;
            //}
        }
        
        private void brdKlicked(object sender, System.Windows.Input.MouseButtonEventArgs e) 
        {
            int idxTalent = ((sender as Border).Parent as StackPanel).Children.IndexOf(sender as Border);
            CloseAllExpanderTalente();
            (spHeldTalent.Children[idxTalent] as Expander).IsExpanded = true;
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
        }

        #endregion // ---- EVENTS ----

        #region // ---- METHODEN ----

        private void CloseAllExpanderTalente() {
            expGesellschaft.IsExpanded = false;
            expHandwerk.IsExpanded = false;
            expKampf.IsExpanded = false;
            expKoerper.IsExpanded = false;
            expNatur.IsExpanded = false;
            expSprache.IsExpanded = false;
            expWissen.IsExpanded = false;
            expRituale.IsExpanded = false;
            expLiturgie.IsExpanded = false;
            expGaben.IsExpanded = false;
        }

        #endregion

    }
}