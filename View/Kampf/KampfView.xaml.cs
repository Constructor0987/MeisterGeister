using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeisterGeister.Daten;
using System.Collections.Generic;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Settings;
using MeisterGeister.View.Arena;
using MeisterGeister.View.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Kampf.Logic;
using VM = MeisterGeister.ViewModel.Kampf;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für KampfView.xaml
    /// </summary>
    public partial class KampfView : UserControl
    {
        public KampfView()
        {
            InitializeComponent();
            VM = new VM.KampfViewModel(ShowBodenplanWindow, View.General.ViewHelper.ShowGegnerView, View.General.ViewHelper.Confirm);

            View.General.EnumItemsSource tpValues = (View.General.EnumItemsSource)Resources["TrefferzonenValues"];
            tpValues.Remove("Gesamt");
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.KampfViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.KampfViewModel))
                    return null;
                return DataContext as VM.KampfViewModel;
            }
            set { DataContext = value; }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_treeInitiative.SelectedItem == null)
            {
                _menuItemKämpferInitiaveWürfeln.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferOrientieren.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                _menuItemKämpferInitiaveWürfeln.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferOrientieren.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private BattlegroundWindow battlegroundWindow = null;

        private void ShowBodenplanWindow(MeisterGeister.ViewModel.Kampf.KampfViewModel vm)
        {
            if (battlegroundWindow == null)
            {
                battlegroundWindow = new BattlegroundWindow();

                battlegroundWindow.VM.KampfVM = vm;
                battlegroundWindow.Show();
            }
            else
            {
                battlegroundWindow.Activate();
            }

            //// TODO ??: In Command verschieben
            //if (VM.BodenplanWindow != null)
            //{
            //    VM.BodenplanWindow.Activate();
            //}
            //else
            //{
            //    ArenaWindow arenaWindow = new ArenaWindow(_cbArena.IsChecked == true ? VM : null);
            //    VM.BodenplanWindow = arenaWindow;
            //    arenaWindow.Width = 1200;
            //    arenaWindow.Height = 800;
            //    arenaWindow.Closed += ArenaWindow_Closed;
            //    arenaWindow.Show();
            //}
        }

        /*
        private void ButtonArena_Click(object sender, RoutedEventArgs e)
        {
            //// TODO ??: In Command verschieben
            if (VM.BodenplanWindow != null)
            {
                VM.BodenplanWindow.Activate();
            }
            else
            {
                BattlegroundWindow bw = new BattlegroundWindow();
                //VM.BodenplanWindow = bw;
                ((BattlegroundViewModel) bw.battlegroundView1.DataContext).KampfVM = VM;
                bw.Closed += Bodenplan_Closed;
                bw.Show();
            }

            //// TODO ??: In Command verschieben
            //if (VM.BodenplanWindow != null)
            //{
            //    VM.BodenplanWindow.Activate();
            //}
            //else
            //{
            //    ArenaWindow arenaWindow = new ArenaWindow(_cbArena.IsChecked == true ? VM : null);
            //    VM.BodenplanWindow = arenaWindow;
            //    arenaWindow.Width = 1200;
            //    arenaWindow.Height = 800;
            //    arenaWindow.Closed += ArenaWindow_Closed;
            //    arenaWindow.Show();
            //}
        }
        
        void Bodenplan_Closed(object sender, EventArgs e)
        {
            VM.BodenplanWindow = null;
        }
         */


        private void ButtonSpielerInfo_Click(object sender, RoutedEventArgs e)
        {
            ShowSpielerInfo();
        }

        private void ButtonSpielerInfoClose_Click(object sender, RoutedEventArgs e)
        {
            MainView.CloseSpielerFenster();
        }

        private void ShowSpielerInfo()
        {
            KampfInfoView infoView = new KampfInfoView(VM);
            MainView.ShowSpielerInfo(infoView);
        }

        private void InitiativeListe_TreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            var parent = ItemsControl.ItemsControlFromItemContainer(e.OriginalSource as TreeViewItem);
            VM.KämpferSelected = parent is TreeView;
        }

        void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }

        static T VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
        {
            DependencyObject returnVal = source;

            while (returnVal != null && !(returnVal is T))
            {
                DependencyObject tempReturnVal = null;
                if (returnVal is Visual || returnVal is Visual3D)
                {
                    tempReturnVal = VisualTreeHelper.GetParent(returnVal);
                }
                if (tempReturnVal == null)
                {
                    returnVal = LogicalTreeHelper.GetParent(returnVal);
                }
                else returnVal = tempReturnVal;
            }

            return returnVal as T;
        }

    }

    public delegate void ProbeWürfelnEventHandler(string talentname);
}
