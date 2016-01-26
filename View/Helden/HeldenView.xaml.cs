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
            //VM = new VM.HeldenViewModel();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.HeldenViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.HeldenViewModel))
                    return null;
                return DataContext as VM.HeldenViewModel;
            }
            set { DataContext = value; }
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.tabControl1.SelectedItem is TabItem)
            {
                TabItem tab = this.tabControl1.SelectedItem as TabItem;
                if (tab.Content == null)
                {
                    if (tab == _tabItemAllgemein)
                        tab.Content = new Helden.Controls.AllgemeinView();
                    else if (tab == _tabItemEigenschaften)
                        tab.Content = new Helden.Controls.EigenschaftenView();
                    else if (tab == _tabItemSonderfertigkeiten)
                        tab.Content = new Helden.Controls.SonderfertigkeitenView();
                    else if (tab == _tabItemTalente)
                        tab.Content = new Helden.Controls.TalentView();
                    else if (tab == _tabItemVorNachteile)
                        tab.Content = new Helden.Controls.VorNachteileView();
                    else if (tab == _tabItemZauber)
                        tab.Content = new Helden.Controls.ZauberView();
                    else if (tab == _tabItemInventar)
                        tab.Content = new Helden.Controls.InventarView();
                }
            }
        }
    }
}
