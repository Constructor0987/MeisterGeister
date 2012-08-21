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


namespace MeisterGeister.View.Helden.Controls
{
	/// <summary>
	/// Interaktionslogik für TalentView.xaml
	/// </summary>
	public partial class TalentView : UserControl {
        
        #region//KONSTRUKTOR
        public TalentView()
		{
			this.InitializeComponent();
            this.DataContext = new VM.TalentViewModel();
		}
#endregion
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            try
            {
                (this.DataContext as VM.TalentViewModel).Init();
            }
            catch (Exception)
            {
            }
		}
        private void RefreshHeld() {
            // Talente-Sortierung aktualisieren
            //if (_dataGridHeldTalente.Items is System.Windows.Data.CollectionView) {
            //    System.Windows.Data.CollectionViewSource csv = (System.Windows.Data.CollectionViewSource)FindResource("heldHeld_TalentViewSource");
            //    if (csv != null && csv.View != null) {
            //        csv.View.SortDescriptions.Clear();
            //        csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("TalentgruppeID", System.ComponentModel.ListSortDirection.Ascending));
            //        csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Talentname", System.ComponentModel.ListSortDirection.Ascending));
            //        _dataGridHeldTalente.ItemsSource = csv.View;
            //    }
            //}
        }


        private Model.Talent SelectedTalentRow {
            get;
            set;
            //get {
            //    Model.Talent talentRow = null;
            //    if (_dataGridHeldTalente.SelectedItem != null)
            //        talentRow = (Model.Talent)((System.Data.DataRowView)_dataGridHeldTalente.SelectedItem).Row;

            //    return talentRow;
            //}
        }

        private Model.Talent SelectedTalent {
            get;
            set;
            //get { return new Talent(SelectedTalentRow.TalentRow); }
        }

        private bool DeleteHeldTalent() {
            bool deleted = false;
            //Model.Talent talent = SelectedTalentRow;
            //if (MessageBox.Show(string.Format("Soll das Talent '{0}' entfernt werden?", talent.TalentRow.Talentname), "Talent entfernen", MessageBoxButton.YesNo,
            //                    MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes) {
            //    Global.SelectedHeld.DeleteTalent(talent);
            //    deleted = true;
            //}
            return deleted;
        }

        private void ContextMenuTalente_Opened(object sender, RoutedEventArgs e) {
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

        private void MenuItemTalentLöschen_Click(object sender, RoutedEventArgs e) {
            DeleteHeldTalent();
        }

        private void MenuItemTalentWiki_Click(object sender, RoutedEventArgs e) {
            //System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedTalent.WikiLink);
        }

        public event ProbeWürfelnEventHandler ProbeWürfeln;
        private void MenuItemTalentProben_Click(object sender, RoutedEventArgs e) {
            if (ProbeWürfeln != null) {
                //ProbeWürfeln(SelectedTalentRow.TalentRow.Talentname);
            }
        }

        private void SetTalenteAktivierbar() {
            //_comboBoxTalentAktivieren.ItemsSource = SelectedHeld.TalenteAktivierbar;
            _comboBoxTalentAktivieren.SelectedIndex = -1;
        }

        private void _comboBoxTalentAktivieren_DropDownOpened(object sender, EventArgs e) {
            SetTalenteAktivierbar();
        }

        private void InsertTalent() {
            if (IsInitialized) {
                //if (_comboBoxTalentAktivieren.SelectedItem != null && _comboBoxTalentAktivieren.SelectedItem is string && SelectedHeld.Id != Guid.Empty) {
                //    string talent = _comboBoxTalentAktivieren.SelectedItem.ToString();
                //    //if (Global.SelectedHeld.AddTalent(talent) == false)
                //    //    MessageBox.Show(string.Format("Das Talent '{0}' ist bereits vorhanden.", talent.ToString()), "Talent hinzufügen");
                //    RefreshHeld();
                //    SetTalenteAktivierbar();
                //}
            }
        }

        private void _dataGridHeldTalente_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (sender is System.Windows.Controls.DataGrid && e.Key == Key.Delete) {
                var grid = (System.Windows.Controls.DataGrid)sender;

                if (grid.SelectedItems.Count > 0) {
                    e.Handled = !DeleteHeldTalent();
                }
            }
        }
        private void _comboBoxTalentAktivieren_DropDownClosed(object sender, EventArgs e) {
            InsertTalent();
        }
        private void _comboBoxTalentAktivieren_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Enter)
                InsertTalent();
        }
	}
}