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
using System.Reflection;


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

        private void ListBoxTalent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _listBoxGaben.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxGesellschaft.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxHandwerk.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxKampf.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxKoerper.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxLiturgien.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxNatur.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxRituale.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxSprachen.SelectionChanged -= ListBoxTalent_SelectionChanged;
            _listBoxWissen.SelectionChanged -= ListBoxTalent_SelectionChanged;

            _listBoxGaben.SelectedItem = null;
            _listBoxGesellschaft.SelectedItem = null;
            _listBoxHandwerk.SelectedItem = null;
            _listBoxKampf.SelectedItem = null;
            _listBoxKoerper.SelectedItem = null;
            _listBoxLiturgien.SelectedItem = null;
            _listBoxNatur.SelectedItem = null;
            _listBoxRituale.SelectedItem = null;
            _listBoxSprachen.SelectedItem = null;
            _listBoxWissen.SelectedItem = null;

            (sender as ListBox).SelectedItem = e.AddedItems.Count > 0 ? e.AddedItems[0] : null;

            _listBoxGaben.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxGesellschaft.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxHandwerk.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxKampf.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxKoerper.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxLiturgien.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxNatur.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxRituale.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxSprachen.SelectionChanged += ListBoxTalent_SelectionChanged;
            _listBoxWissen.SelectionChanged += ListBoxTalent_SelectionChanged;
        }
        
        private void brdKlicked(object sender, System.Windows.Input.MouseButtonEventArgs e) 
        {
            int idxTalent = ((sender as Border).Parent as StackPanel).Children.IndexOf(sender as Border) - 1;
            if (idxTalent == -1)
                SetExpanderTalente(true);
            else
            {
                SetExpanderTalente(false);
                if (spHeldTalent.Children.Count > idxTalent)
                    (spHeldTalent.Children[idxTalent] as Expander).IsExpanded = true;
            }
        }

        #endregion // ---- EVENTS ----

        #region // ---- METHODEN ----

        private void SetExpanderTalente(bool isExpanded)
        {
            expGesellschaft.IsExpanded = isExpanded;
            expHandwerk.IsExpanded = isExpanded;
            expKampf.IsExpanded = isExpanded;
            expKoerper.IsExpanded = isExpanded;
            expNatur.IsExpanded = isExpanded;
            expSprache.IsExpanded = isExpanded;
            expWissen.IsExpanded = isExpanded;
            expRituale.IsExpanded = isExpanded;
            expLiturgie.IsExpanded = isExpanded;
            expGaben.IsExpanded = isExpanded;
        }
    
        #endregion

    }
}