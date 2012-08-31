using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MeisterGeister.Daten;
using System.Linq;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
using System.Collections.Generic;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Helden;
using MeisterGeister.Logic.General;
using MeisterGeister.View.Windows;
using MeisterGeister.View.Kampf;
using System.IO;

namespace MeisterGeister.View.Helden
{
    /// <summary>
    /// Interaktionslogik für HeldenView.xaml
    /// </summary>
    public partial class HeldenView : System.Windows.Controls.UserControl
    {
        public HeldenView()
        {
            InitializeComponent();

            //VM an View Registrieren
            this.DataContext = new VM.HeldenViewModel(); 
        }

        public event System.ComponentModel.PropertyChangedEventHandler HeldChanged;

        void SelectedHeld_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "This")
            {
                // TODO MT: In EigenschaftenView integrieren
                //_energieControlAstralenergie.SetEnergie();
                //_energieControlAusdauer.SetEnergie();
                //_energieControlKarmaenergie.SetEnergie();
                //_energieControlLebensenergie.SetEnergie();

                // Changed Event weitergeben
                if (HeldChanged != null)
                    HeldChanged(sender, e);
            }
        }

        //TODO DW: Trennen in Controls
        private void RefreshHeld(bool refreshRepräsentationen = true)
        {
            // TODO MT: In EigenschaftenView integrieren
            //_labelAstralenergie.Visibility = System.Windows.Visibility.Visible;
            //_labelKarmaenergie.Visibility = System.Windows.Visibility.Visible;
            //_imageAeKeHinweis.Visibility = System.Windows.Visibility.Collapsed;

            Held held = SelectedHeld;

            //if (!held.Magiebegabt && !held.Geweiht)
            //    _imageAeKeHinweis.Visibility = System.Windows.Visibility.Visible;

            // Magiebegabung und Astralenergie
            if (!held.Magiebegabt)
            {
                //_labelAstralenergie.Visibility = System.Windows.Visibility.Collapsed;
                if (tabControl1.SelectedItem == _tabItemZauber)
                    tabControl1.SelectedItem = _tabItemTalente;
            }
            else
            {
                // TODO MT: In ZauberView integrieren
                //if (refreshRepräsentationen)
                //    _comboBoxRepräsentation.SelectedValue = held.RepräsentationStandard();

                //// Zauber-Sortierung aktualisieren
                //if (_dataGridHeldZauber.Items is System.Windows.Data.CollectionView)
                //{
                //    System.Windows.Data.CollectionViewSource csv = (System.Windows.Data.CollectionViewSource)FindResource("heldHeld_ZauberViewSource");
                //    if (csv != null && csv.View != null)
                //    {
                //        csv.View.SortDescriptions.Clear();
                //        csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Zaubername", System.ComponentModel.ListSortDirection.Ascending));
                //        csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Repräsentation", System.ComponentModel.ListSortDirection.Ascending));
                //        _dataGridHeldZauber.ItemsSource = csv.View;
                //    }
                //}
            }

            // TODO MT: In EigenschaftenView integrieren
            // Geweiht und Karmaenergie
            //if (!held.Geweiht)
            //    _labelKarmaenergie.Visibility = System.Windows.Visibility.Collapsed;

            
        }

        public Held SelectedHeld
        {
            get
            {
                Held held = null;
                if(Global.SelectedHeldGUID != Guid.Empty)
                    held = new Held(Global.SelectedHeldGUID);
                return held;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ListBoxHelden_SelectionChanged(sender, null);
        }

        public event ProbeWürfelnEventHandler ProbeWürfeln;
        
    }
}
