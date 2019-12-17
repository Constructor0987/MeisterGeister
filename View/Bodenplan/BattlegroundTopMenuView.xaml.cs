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
            VM = new BattlegroundViewModel
            {
                KampfVM = Global.CurrentKampf
            };
        }

        public BattlegroundViewModel VM
        {
            get { return DataContext as BattlegroundViewModel; }
            set { DataContext = value; }
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

        private void Button_SaveXML_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Battleground_" + System.DateTime.Now.ToShortDateString(), // Default file name
                DefaultExt = ".xml",
                Filter = "XML Files (.xml)|*.xml"
            };
            var result = dlg.ShowDialog();

            if (result == true)
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null)
                {
                    vm.SaveBattlegroundToXML(dlg.FileName);
                }
            }
        }

        private void Button_LoadXML_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "XML Files (.xml)|*.xml"
            };
            var result = dlg.ShowDialog();

            if (result == true)
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null)
                {
                    Global.CurrentKampf.Kampf.Kämpfer.Clear();
                    Global.CurrentKampf.BodenplanViewModel.RemoveCreatureAll();
                    vm.LoadBattlegroundFromXML(dlg.FileName);
                    vm.UpdateCreatureLevelToTop();
                }
            }
        }

        private void Button_Load_lastKR_XML_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewHelper.Confirm("Laden der letzten KR des letzten Kampfes",
                "Wollen Sie den momentanen Kampf verwerfen und die letzte KR des letzten Kampfes laden?"))
                return;
            
            string bodenplanPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + 
                @"\Daten\Bodenplan\Battleground_Letzte_KR.xml";
            if (Directory.Exists(Path.GetDirectoryName(bodenplanPath)) && File.Exists(bodenplanPath))
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null)
                {
                    Global.CurrentKampf.Kampf.Kämpfer.Clear();
                    Global.CurrentKampf.BodenplanViewModel.RemoveCreatureAll();
                    vm.LoadBattlegroundFromXML(bodenplanPath);
                    vm.UpdateCreatureLevelToTop();
                }
            }
            else
                ViewHelper.Popup("Die temporäre Datei "+Environment.NewLine+ bodenplanPath + Environment.NewLine+ " konnte nicht gefunden werden");
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
                if (VM.KampfWindow != null)
            {
                VM.KampfWindow.Close();
            }
        }

        private void CreateKampfWindow()
        {
            var infoView = new Kampf.KampfInfoView(Global.CurrentKampf);

            infoView.grdMain.LayoutTransform = new ScaleTransform(VM.ScaleKampfGrid, VM.ScaleKampfGrid);
            VM.KampfWindow = new Window();

            //SizeToContent auf Width setzt den Screen auf minimale Breite
            if (Global.CurrentKampf.Kampf.Kampfrunde <= 1)
            {
                VM.KampfWindow.SizeToContent = SizeToContent.Width;
            }
            else
            {
                VM.KampfWindow.Width = VM.IniWidthStart != 0 ? VM.IniWidthStart : 426 * VM.ScaleKampfGrid;
            }

            VM.KampfWindow.Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                if (VM != null)
                {
                    VM.IsShowIniKampf = false;
                }

                VM.KampfWindow = null;
            };
            infoView.scrViewer.MouseEnter += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = true; };
            infoView.scrViewer.MouseLeave += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = false; };
            VM.KampfWindow.SizeChanged += (object sender, SizeChangedEventArgs e) =>
            {
                if ((System.Windows.Forms.Screen.AllScreens.Length > 1 &&
                     VM.KampfWindow.Left > System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width +
                        System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width * .5) ||
                    (System.Windows.Forms.Screen.AllScreens.Length == 1 &&
                     VM.KampfWindow.Left > System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width * .5))
                {
                    VM.KampfWindow.Left = (System.Windows.Forms.Screen.AllScreens.Length > 1) ?
                        System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width +
                        System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - VM.KampfWindow.Width +
                        (e.PreviousSize.Width - e.NewSize.Width) :

                        System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width - VM.KampfWindow.Width;
                    if (System.Windows.Forms.Screen.AllScreens.Length > 1 &&
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive)
                    {
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width =
                            System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - VM.KampfWindow.Width;
                    }
                }
                VM.SetIniWindowWidth();

                if (VM.IniWidthStart != Math.Round(VM.KampfWindow.Width / VM.ScaleKampfGrid))
                {
                    VM.IniWidthStart = Math.Round(VM.KampfWindow.Width / VM.ScaleKampfGrid);
                }
            };
            if (VM.SpielerScreenWindow != null)
            {
                VM.SpielerScreenWindow.Topmost = false;
            }

            VM.KampfWindow.Topmost = true;
            VM.KampfWindow.Content = infoView;
            VM.KampfWindow.Show();
            // SizeToContent muss auf Height gestellt werden, damit die Höhe an die Anz. Kämpfer
            // angepasst wird
            VM.KampfWindow.SizeToContent = SizeToContent.Height;

            // SizeToContent muss wieder auf Manual gesetzt werden da das Window sonst immer größer wird
            VM.KampfWindow.SizeToContent = SizeToContent.Manual;
            VM.KampfWindow.WindowStyle = WindowStyle.None;

            VM.SetIniWindowPosition();

            VM.IsShowIniKampf = true;
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
                    if (VM.SpielerScreenActive)
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
