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

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für SonderfertigkeitenView.xaml
    /// </summary>
    public partial class SonderfertigkeitenView : UserControl
    {
        public SonderfertigkeitenView()
        {
            InitializeComponent();

            // TODO MT: ViewModel erstellen
        }

        private void _listBoxHeldSonderfertigkeiten_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_listBoxHeldSonderfertigkeiten.SelectedItem != null)
            {
                switch (e.Key)
                {
                    // Sonderfertigkeit löschen
                    case Key.Delete:
                        DeleteHeldSonderfertigkeit();
                        break;
                    default:
                        break;
                }
            }
        }

        //private DatabaseDSADataSet.Held_SonderfertigkeitRow SelectedSonderfertigkeit
        //{
        //    get
        //    {
        //        DatabaseDSADataSet.Held_SonderfertigkeitRow sfRow = null;
        //        if (_listBoxHeldSonderfertigkeiten.SelectedItem != null)
        //            sfRow = (DatabaseDSADataSet.Held_SonderfertigkeitRow)((System.Data.DataRowView)_listBoxHeldSonderfertigkeiten.SelectedItem).Row;
        //        return sfRow;
        //    }
        //}

        private void ListBoxSonderfertigkeiten_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxHeldSonderfertigkeiten.SelectedItem = null;
        }

        private void ContextMenuSonderfertigkeiten_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHeldSonderfertigkeiten.SelectedItem == null)
            {
                _menuItemSonderfertigkeitLöschen.IsEnabled = false;
                _menuItemSonderfertigkeitlWiki.IsEnabled = false;
            }
            else
            {
                _menuItemSonderfertigkeitLöschen.IsEnabled = true;
                _menuItemSonderfertigkeitlWiki.IsEnabled = true;
            }
        }

        private void MenuItemSonderfertigkeitLöschen_Click(object sender, RoutedEventArgs e)
        {
            DeleteHeldSonderfertigkeit();
        }

        private void DeleteHeldSonderfertigkeit()
        {
            //DatabaseDSADataSet.Held_SonderfertigkeitRow sf = SelectedSonderfertigkeit;
            //if (MessageBox.Show(string.Format("Soll die Sonderfertigkeit '{0}' entfernt werden?", sf.SonderfertigkeitRow.Name), "Sonderfertigkeit entfernen", MessageBoxButton.YesNo,
            //                    MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            //{
            //    {
            //        SelectedHeld.DeleteSonderfertigkeit(sf);
            //        _listBoxHeldSonderfertigkeiten.SelectedIndex = -1;
            //    }
            //}
        }

        private void MenuItemSonderfertigkeitWiki_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedSonderfertigkeit.SonderfertigkeitRow.Name);
        }

        private void _comboBoxSonderfertigkeit_DropDownOpened(object sender, EventArgs e)
        {
            SetSonderfertigkeitenAktivierbar();
        }

        private void SetSonderfertigkeitenAktivierbar()
        {
            //_comboBoxSonderfertigkeit.ItemsSource = SelectedHeld.SonderfertigkeitenErlernbar;
            _comboBoxSonderfertigkeit.SelectedIndex = -1;
        }


        private void _comboBoxSonderfertigkeit_DropDownClosed(object sender, EventArgs e)
        {
            InsertSonderfertigkeit();
        }

        private void _comboBoxSonderfertigkeit_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                InsertSonderfertigkeit();
        }

        private void InsertSonderfertigkeit()
        {
            if (IsInitialized)
            {
                //if (_comboBoxSonderfertigkeit.SelectedItem != null && SelectedHeld.Id != Guid.Empty)
                //{
                //    var sf = (System.Collections.Generic.KeyValuePair<string, int>)_comboBoxSonderfertigkeit.SelectedItem;
                //    if (SelectedHeld.AddSonderfertigkeit(sf.Value) == false)
                //        MessageBox.Show(string.Format("Die Sonderfertigkeit '{0}' ist bereits vorhanden.", sf.Key), "Sonderfertigkeit hinzufügen");
                //    RefreshHeld();
                //    SetSonderfertigkeitenAktivierbar();
                //}
            }
        }
    }
}
