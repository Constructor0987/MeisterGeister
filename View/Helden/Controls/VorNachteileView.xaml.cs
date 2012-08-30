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
using MeisterGeister.Model;
using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für VorNachteileView.xaml
    /// </summary>
    public partial class VorNachteileView : UserControl
    {
        public VorNachteileView()
        {
            InitializeComponent();

            //VM an View Registrieren
            this.DataContext = new VM.VorNachteileViewModel(); 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.VorNachteileViewModel).Init();
            }
            catch (Exception)
            {
            }
        }

        private void ContextMenuVorNachteile_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHeldVorNachteile.SelectedItem == null)
            {
                _menuItemVorNachteilLöschen.IsEnabled = false;
                _menuItemVorNachteilWiki.IsEnabled = false;
            }
            else
            {
                _menuItemVorNachteilLöschen.IsEnabled = true;
                _menuItemVorNachteilWiki.IsEnabled = true;
            }
        }

        private void ListBoxHeldVorNachteile_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_listBoxHeldVorNachteile.SelectedItem != null)
            {
                switch (e.Key)
                {
                    // VorNachteil löschen
                    case Key.Delete:
                        // TODO MT: Lösch-Frage einbauen
                        (this.DataContext as VM.VorNachteileViewModel).OnDeleteVorNachteil.Execute(null);
                        break;
                    default:
                        break;
                }
            }
        }

        //private DatabaseDSADataSet.Held_VorNachteilRow SelectedVorNachteil
        //{
        //    get
        //    {
        //        DatabaseDSADataSet.Held_VorNachteilRow vorNachRow = null;
        //        if (_listBoxHeldVorNachteile.SelectedItem != null)
        //            vorNachRow = (DatabaseDSADataSet.Held_VorNachteilRow)((System.Data.DataRowView)_listBoxHeldVorNachteile.SelectedItem).Row;

        //        return vorNachRow;
        //    }
        //}

        private void MenuItemVorNachteilWiki_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + (this.DataContext as VM.VorNachteileViewModel).SelectedHeldVorNachteil.VorNachteil.Name);
        }

        private void SetVorNachteileAktivierbar()
        {
            // TODO MT: Bereits vorhandene VorNachteile aus Auswahl entfernen
            _comboBoxVorteil.ItemsSource = (this.DataContext as VM.VorNachteileViewModel).VorteilAuswahlListe;
            _comboBoxVorteil.SelectedIndex = -1;
            _comboBoxNachteil.ItemsSource = (this.DataContext as VM.VorNachteileViewModel).NachteilAuswahlListe;
            _comboBoxNachteil.SelectedIndex = -1;
        }

        private void _comboBoxVorNachteil_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender == _comboBoxVorteil)
                    InsertVorNachteil("Vorteil");
                else
                    InsertVorNachteil("Nachteil");
            }
        }

        private void _comboBoxVorNachteil_DropDownClosed(object sender, EventArgs e)
        {
            if (sender == _comboBoxVorteil)
                InsertVorNachteil("Vorteil");
            else
                InsertVorNachteil("Nachteil");
        }

        private void InsertVorNachteil(string typ)
        {
            //if (IsInitialized)
            //{
            //    ComboBox comboVorNach;
            //    if (typ == "Vorteil")
            //        comboVorNach = _comboBoxVorteil;
            //    else
            //        comboVorNach = _comboBoxNachteil;

            //    if (comboVorNach.SelectedItem != null && SelectedHeld.Id != Guid.Empty)
            //    {
            //        var vorNach = (System.Collections.Generic.KeyValuePair<string, int>)comboVorNach.SelectedItem;
            //        if (SelectedHeld.AddVorNachteil(vorNach.Value) == false)
            //        {
            //            MessageBox.Show(string.Format("Der Vor-/Nachteil '{0}' ist bereits vorhanden.", vorNach.Key), "Vor-/Nachteil hinzufügen");
            //        }
            //        RefreshHeld();
            //        SetVorNachteileAktivierbar();
            //    }
            //}
        }

        private void _comboBoxVorNachteil_DropDownOpened(object sender, EventArgs e)
        {
            SetVorNachteileAktivierbar();
        }



    }
}
