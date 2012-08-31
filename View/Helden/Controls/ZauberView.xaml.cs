﻿using System;
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
    /// Interaktionslogik für ZauberView.xaml
    /// </summary>
    public partial class ZauberView : UserControl
    {
        public ZauberView()
        {
            InitializeComponent();

            // TODO MT: ViewModel erstellen
        }

        //private DatabaseDSADataSet.Held_ZauberRow SelectedZauberRow
        //{
        //    get
        //    {
        //        DatabaseDSADataSet.Held_ZauberRow zauberRow = null;
        //        if (_dataGridHeldZauber.SelectedItem != null)
        //            zauberRow = (DatabaseDSADataSet.Held_ZauberRow)((System.Data.DataRowView)_dataGridHeldZauber.SelectedItem).Row;

        //        return zauberRow;
        //    }
        //}

        //private Model.Zauber SelectedZauber
        //{
        //    get { return new Zauber(SelectedZauberRow.ZauberRow); }
        //}

        private void ContextMenuZauber_Opened(object sender, RoutedEventArgs e)
        {
            if (_dataGridHeldZauber.SelectedItem == null)
            {
                _menuItemZauberLöschen.IsEnabled = false;
                _menuItemZauberWiki.IsEnabled = false;
            }
            else
            {
                _menuItemZauberLöschen.IsEnabled = true;
                _menuItemZauberWiki.IsEnabled = true;
            }
        }

        private void MenuItemZauberProben_Click(object sender, RoutedEventArgs e)
        {
            // TODO MT: Probe würfeln
            //if (ProbeWürfeln != null)
            //{
            //    ProbeWürfeln(SelectedZauberRow.ZauberRow.Name);
            //}
        }

        private void _comboBoxZauber_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                InsertZauber();
        }

        private void InsertZauber()
        {
            if (IsInitialized)
            {
                //if (_comboBoxZauber.SelectedItem != null && SelectedHeld.Id != Guid.Empty)
                //{
                //    var zauber = (System.Collections.Generic.KeyValuePair<string, int>)_comboBoxZauber.SelectedItem;
                //    string rep = _comboBoxRepräsentation.SelectedValue.ToString();
                //    if (SelectedHeld.AddZauber(zauber.Value, rep) == false)
                //        MessageBox.Show(string.Format("Der Zauber '{0}' in der Repräsentation '{1}' ist bereits vorhanden.", zauber.Key, rep), "Zauber hinzufügen");
                //    RefreshHeld(false);
                //    SetZauberAktivierbar();
                //}
            }
        }

        private void SetZauberAktivierbar()
        {
            //_comboBoxZauber.ItemsSource = Zauber.ZauberList;
            _comboBoxZauber.SelectedIndex = -1;
        }

        private void _comboBoxZauber_DropDownOpened(object sender, EventArgs e)
        {
            SetZauberAktivierbar();
        }

        private void _comboBoxZauber_DropDownClosed(object sender, EventArgs e)
        {
            InsertZauber();
        }

        private void _dataGridHeldZauber_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid && e.Key == Key.Delete)
            {
                var grid = (System.Windows.Controls.DataGrid)sender;

                if (grid.SelectedItems.Count > 0)
                {
                    //DatabaseDSADataSet.Held_ZauberRow zauber = SelectedZauberRow;
                    //if (MessageBox.Show(string.Format("Soll der Zauber '{0}' entfernt werden?", zauber.ZauberRow.Name), "Zauber entfernen", MessageBoxButton.YesNo,
                    //                    MessageBoxImage.Question, MessageBoxResult.Yes) != MessageBoxResult.Yes)
                    //{
                    //    e.Handled = true;
                    //}
                }
            }
        }

        private void MenuItemZauberLöschen_Click(object sender, RoutedEventArgs e)
        {
            DeleteHeldZauber();
        }

        private void DeleteHeldZauber()
        {
            //DatabaseDSADataSet.Held_ZauberRow zauber = SelectedZauberRow;
            //if (MessageBox.Show(string.Format("Soll der Zauber '{0}' entfernt werden?", zauber.ZauberRow.Name), "Zauber entfernen", MessageBoxButton.YesNo,
            //                            MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            //{
            //    SelectedHeld.DeleteZauber(zauber);
            //}
        }

        private void MenuItemZauberWiki_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedZauber.WikiLink);
        }  
    }
}
