using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeisterGeister.Daten;
using System.Collections.Generic;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.View.Arena;
using MeisterGeister.View.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Kampf.Logic;
using VM = MeisterGeister.ViewModel.Kampf;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MeisterGeister.View.AudioPlayer;

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
        }

        private void userControl_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as VM.KampfViewModel;
            
            if (vm != null && vm.BodenplanViewModel != null)
            {
                vm.BodenplanViewModel.Freizeichnen = (e.Key == Key.LeftShift);
                if (e.Key == Key.Delete && vm.BodenplanViewModel.SelectedObject != null) vm.BodenplanViewModel.Delete();
                if (vm.BodenplanViewModel.Freizeichnen && (vm.BodenplanViewModel.CreatingNewLine || vm.BodenplanViewModel.CreatingNewFilledLine))
                {
                    double _x2 = vm.BodenplanViewModel.CurrentMousePositionX;
                    double _y2 = vm.BodenplanViewModel.CurrentMousePositionY;
                    vm.BodenplanViewModel.MoveWhileDrawing(_x2, _y2, vm.BodenplanViewModel.Freizeichnen);
                }
            }
            if (e.Key == Key.Escape) vm.BodenplanViewModel.SelectedObject = null;
            //if (e.Key == Key.D1) vm.BodenplanViewModel.CreateLine = (!Convert.ToBoolean(vm.BodenplanViewModel.CreateLine));
            //if (e.Key == Key.D2) vm.BodenplanViewModel.ToggleFilledLinePathButton();
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            var vm = DataContext as VM.KampfViewModel;
            if (vm != null && vm.BodenplanViewModel != null)
            {
                if (e.Key == Key.LeftCtrl && Keyboard.IsKeyUp(Key.LeftCtrl))
                    vm.BodenplanViewModel.FogFreimachen = false;
                if (e.Key == Key.LeftShift) 
                { 
                    vm.BodenplanViewModel.Freizeichnen = !vm.BodenplanViewModel.Freizeichnen;
                    vm.BodenplanViewModel.LeftShiftPressed = !vm.BodenplanViewModel.LeftShiftPressed;
                }
            }
        }


        //private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        //{
        //    if (_treeInitiative.SelectedItem == null)
        //    {
        //        _menuItemKämpferInitiaveWürfeln.Visibility = System.Windows.Visibility.Collapsed;
        //        _menuItemKämpferOrientieren.Visibility = System.Windows.Visibility.Collapsed;
        //        _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Collapsed;
        //        _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Collapsed;
        //        _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        _menuItemKämpferInitiaveWürfeln.Visibility = System.Windows.Visibility.Visible;
        //        _menuItemKämpferOrientieren.Visibility = System.Windows.Visibility.Visible;
        //        _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Visible;
        //        _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Visible;
        //        _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Visible;
        //    }
        //}

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
        
         */

        //private void btnAudioSpeedButtonWesenZuweisen_Click(object sender, RoutedEventArgs e)
        //{
        //    MeisterGeister.Model.Held h = (VM.SelectedKämpferInfo.Kämpfer is MeisterGeister.Model.Held)? 
        //        (VM.SelectedKämpferInfo.Kämpfer as MeisterGeister.Model.Held): null;
        //    MeisterGeister.Model.GegnerBase g = (VM.SelectedKämpferInfo.Kämpfer is MeisterGeister.Model.GegnerBase)? 
        //        (VM.SelectedKämpferInfo.Kämpfer as MeisterGeister.Model.GegnerBase):
        //        (VM.SelectedKämpferInfo.Kämpfer is MeisterGeister.Model.Gegner) ?
        //        (VM.SelectedKämpferInfo.Kämpfer as MeisterGeister.Model.Gegner).GegnerBase: null;
        //    if (h != null)
        //    { 
        //        PlaylistWesenAuswahlView wesenAuswahlView = new PlaylistWesenAuswahlView(h);
        //        wesenAuswahlView.ShowDialog();
        //    } else
        //        if (g != null)
        //        { 
        //            PlaylistWesenAuswahlView wesenAuswahlView = new PlaylistWesenAuswahlView(g);
        //            wesenAuswahlView.ShowDialog();
        //        }
        //}
    }
}
