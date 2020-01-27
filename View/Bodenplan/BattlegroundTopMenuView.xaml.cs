using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MeisterGeister.View.General;
using MeisterGeister.View.SpielerScreen;
using MeisterGeister.ViewModel.Bodenplan;

namespace MeisterGeister.View.Bodenplan
{
    /// <summary>
    /// Interaction logic for BattlegroundTopMenuView.xaml
    /// </summary>
    public partial class BattlegroundTopMenuView : UserControl
    {
        public BattlegroundTopMenuView()
        {
            InitializeComponent();
        }

        private BattlegroundViewModel BattlegroundVM
        {
            get { return Global.CurrentKampf.BodenplanViewModel; }
        }

        public double VisualisationWidth
        {
            get { return _visualisationWidth; }
            private set { _visualisationWidth = value; }
        }

        public double VisualisationHeight
        {
            get { return _visualisationHeight; }
            private set { _visualisationHeight = value; }
        }

        public void sl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 1)
            {
                ((Slider)sender).Value += ((((Slider)sender).Value < ((Slider)sender).Maximum - 4) ? 5 : ((((Slider)sender).Maximum - ((Slider)sender).Value)));
            }
            else
            {
                ((Slider)sender).Value = ((((Slider)sender).Value >= ((Slider)sender).Minimum + 4) ? ((Slider)sender).Value - 5 : ((Slider)sender).Minimum);
            }
        }

        public void slSmall_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var d = ((Slider)sender).Value;
            var dr = ((Slider)sender).Minimum + .009;
            var min = ((Slider)sender).Minimum;
            if (e.Delta > 1)
            {
                ((Slider)sender).Value = Math.Round(((((Slider)sender).Value + .01 > ((Slider)sender).Maximum) ? ((Slider)sender).Maximum : ((Slider)sender).Value + .01), 2);
            }
            else
            {
                ((Slider)sender).Value = Math.Round(((((Slider)sender).Value - .01 > ((Slider)sender).Minimum) ? ((Slider)sender).Value - .01 : ((Slider)sender).Minimum), 2);
            }
        }

        private double _x1, _y1, _x2, _y2;
        private double _xMovingOld, _yMovingOld;
        private bool _zoomChanged = false;
        private double _visualisationWidth = 100;
        private double _visualisationHeight = 100;
        private bool MouseIsOverScrViewer = false;

        private void ButtonEbeneHigher_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.ChangeEbeneHeight(true);
            }
        }

        private void ButtonEbeneLower_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.ChangeEbeneHeight(false);
            }
        }

        private void ButtonEbeneHigherMax_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.MoveSelectedObjectToTop(true);
            }
        }

        private void ButtonEbeneLowerMin_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.MoveSelectedObjectToTop(false);
            }
        }


            private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.ClearBattleground();
            }
        }

        private void ButtonStickHeroes_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null && !vm.BattlegroundObjects.Where(x => x is MeisterGeister.Model.Gegner && x.IsSticked).Any())
            {
                vm.StickHeroes();
            }
        }

        private void ButtonSticCreatues_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.StickEnemies();
            }
        }

        private void tbtnSpielerIniScreen_Click(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)e.OriginalSource).IsChecked == true)
            {
                CreateKampfWindow();
            }
            else
                if (BattlegroundVM.KampfWindow != null)
            {
                //Setze den Tag um das Fenster zu schließen, bei == null bleibt es offen (Fensterwechsel)
                BattlegroundVM.KampfWindow.Tag = true;
                BattlegroundVM.KampfWindow.Close();
            }
        }

        private void CreateKampfWindow()
        {
            var infoView = new Kampf.KampfInfoView(Global.CurrentKampf);

            infoView.grdMain.LayoutTransform = new ScaleTransform(BattlegroundVM.ScaleKampfGrid, BattlegroundVM.ScaleKampfGrid);
            BattlegroundVM.KampfWindow = new Window();

            //SizeToContent auf Width setzt den Screen auf minimale Breite
            if (Global.CurrentKampf.Kampf.Kampfrunde <= 1)
            {
                BattlegroundVM.KampfWindow.SizeToContent = SizeToContent.Width;
            }
            else
            {
                BattlegroundVM.KampfWindow.Width = 
                    BattlegroundVM.IniWidthStart != 0 ?
                    BattlegroundVM.IniWidthStart : 
                    426 * BattlegroundVM.ScaleKampfGrid;
            }

            BattlegroundVM.KampfWindow.Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                if (BattlegroundVM.KampfWindow != null && 
                    BattlegroundVM.KampfWindow.Tag == null)
                {
                    e.Cancel = true;
                    return;
                }
                if (BattlegroundVM != null)
                {
                    BattlegroundVM.IsShowIniKampf = false;
                }
                BattlegroundVM.KampfWindow = null;
            };
            infoView.scrViewer.MouseEnter += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = true; };
            infoView.scrViewer.MouseLeave += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = false; };
            BattlegroundVM.KampfWindow.SizeChanged += (object sender, SizeChangedEventArgs e) =>
            {
                if ((System.Windows.Forms.Screen.AllScreens.Length > 1 &&
                     BattlegroundVM.KampfWindow.Left > System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width +
                        System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width * .5) ||
                    (System.Windows.Forms.Screen.AllScreens.Length == 1 &&
                     BattlegroundVM.KampfWindow.Left > System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width * .5))
                {
                    BattlegroundVM.KampfWindow.Left = (System.Windows.Forms.Screen.AllScreens.Length > 1) ?
                        System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width +
                        System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - BattlegroundVM.KampfWindow.Width +
                        (e.PreviousSize.Width - e.NewSize.Width) :

                        System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width - BattlegroundVM.KampfWindow.Width;
                    if (System.Windows.Forms.Screen.AllScreens.Length > 1 &&
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive)
                    {
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width =
                            System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - BattlegroundVM.KampfWindow.Width;
                    }
                }
                BattlegroundVM.SetIniWindowWidth();

                if (BattlegroundVM.IniWidthStart != Math.Round(BattlegroundVM.KampfWindow.Width / BattlegroundVM.ScaleKampfGrid))
                {
                    BattlegroundVM.IniWidthStart = Math.Round(BattlegroundVM.KampfWindow.Width / BattlegroundVM.ScaleKampfGrid);
                }
            };
            if (BattlegroundVM.SpielerScreenWindow != null)
            {
                BattlegroundVM.SpielerScreenWindow.Topmost = false;
            }

            BattlegroundVM.KampfWindow.Topmost = true;
            BattlegroundVM.KampfWindow.Content = infoView;
            BattlegroundVM.KampfWindow.Show();
            // SizeToContent muss auf Height gestellt werden, damit die Höhe an die Anz. Kämpfer
            // angepasst wird
            BattlegroundVM.KampfWindow.SizeToContent = SizeToContent.Height;

            // SizeToContent muss wieder auf Manual gesetzt werden da das Window sonst immer größer wird
            BattlegroundVM.KampfWindow.SizeToContent = SizeToContent.Manual;
            BattlegroundVM.KampfWindow.WindowStyle = WindowStyle.None;

            BattlegroundVM.SetIniWindowPosition();

            BattlegroundVM.IsShowIniKampf = true;
        }

        private void tbtnSpielerScreenActive_Click(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)e.OriginalSource).IsChecked == true)
            {
                if (Global.CurrentKampf != null && Global.CurrentKampf.BodenplanViewModel != null)
                {
                    SpielerWindow.Close();
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive = true;
                    SpielerWindow.Show();
                }
            }
            else
            {
                if (Global.CurrentKampf != null && Global.CurrentKampf.BodenplanViewModel != null)
                {
                    if (BattlegroundVM.SpielerScreenActive)
                    {
                        tbtnSpielerScreenActive.RaiseEvent(new RoutedEventArgs(ToggleButton.ClickEvent));
                    }

                    SpielerWindow.Close();
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive = false;
                }
            }
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft-WindowsAPICodePack-Shell
            //using Microsoft.WindowsAPICodePack.Shell;

            //Microsoft.WindowsAPICodePack.Shell.dll
            //Image img = new Image();
            //img.Source = ShellFile.FromFilePath(@"C:\Temp\_LE_D\einer jedem Frau Recht.png").Thumbnail.BitmapSource;
        }
    }
}
