using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeisterGeister.ViewModel.Bodenplan;
using System.Windows.Controls.Primitives;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.View.General;
using System.IO;
using MeisterGeister.View.SpielerScreen;

namespace MeisterGeister.View.Bodenplan
{
    /// <summary>
    /// Interaction logic for BattlegroundView.xaml
    /// </summary>
    public partial class BattlegroundView : UserControl
    {

        private double _x1, _y1, _x2, _y2;
        public double _xMovingOld, _yMovingOld;
        private bool _zoomChanged = false;

        public BattlegroundView()
        {
            InitializeComponent();
            VM = new BattlegroundViewModel();
            
            ArenaGrid.Cursor = Cursors.Arrow;
            AddPictureButtons();
            AddFogOfWar();
        //    InitiateSpielerScaling();
            VM.KampfVM = Global.CurrentKampf;
        }

        //private void InitiateSpielerScaling()
        //{
        //    var scaler = PlayerArenaGridTop.LayoutTransform as ScaleTransform;
        //    if (scaler == null)
        //    {
        //        // Currently no zoom, so go instantly to normal zoom.
        //        PlayerArenaGridTop.LayoutTransform = new ScaleTransform(1, 1);
        //    }
        //}

        private void AddFogOfWar()
        {
            //http://stackoverflow.com/questions/17767097/writeablebitmap-doesnt-change-pixel-color-in-wpf
            if (VM != null) VM.AreanGrid = ArenaGridTop;
        }

        public BattlegroundViewModel VM
        {
            get { return DataContext as BattlegroundViewModel; }
            set { DataContext = value; }
        }

        //Adds dynamicly created Picturebuttons for each picture in folder "Pictures".
        private void AddPictureButtons()
        {
            PictureButtonWrapPanel.Children.Clear(); // alte Bilder entfernen

            String[] picurls = Ressources.GetPictureUrls();
            for (int i = 0; i < picurls.Count(); i++)
            {
                String _buttonPrefix = "picbutton";
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(picurls[i], UriKind.Relative));
                String[] pathsplit = picurls[i].Split('\\');
                Button b = new Button() { Width = 50, Height = 50, Name = _buttonPrefix+i, ToolTip = picurls[i]};
                //b.Content = brush;
                b.Background = brush;
                b.Tag = picurls[i];
                b.Click += (object sender, RoutedEventArgs e) =>
                    {
                        var vm = DataContext as BattlegroundViewModel;
                        if (vm != null)
                        {
                            int strlength = b.Name.Length-1;
                            int buttonNr = Convert.ToInt32(b.Name.Substring(_buttonPrefix.Length, b.Name.Length - _buttonPrefix.Length));
                            var newpic = vm.CreateImageObject(Ressources.Decoder(Ressources.GetFullApplicationPath() + (string)b.Tag), new Point(_xMovingOld >= -500 ? _xMovingOld + 500 : 0, _yMovingOld >=500? _yMovingOld - 500: 0));
                            vm.UpdateCreatureLevelToTop();
                        }
                    };

                Grid.SetRow(b,Convert.ToInt32(i/3));
                Grid.SetColumn(b,i%3);

                PictureButtonWrapPanel.Children.Add(b);
            }            
        }

        private void Thumb_Drag(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            if (thumb == null)
                return;

            var pathline = thumb.DataContext as PathLine;
            if (pathline == null)
                return;
        }

        private double _visualisationWidth = 100;
        public double VisualisationWidth
        {
            get { return _visualisationWidth; }
            private set { _visualisationWidth = value; }
        }

        private double _visualisationHeight = 100;
        public double VisualisationHeight
        {
            get { return _visualisationHeight; }
            private set { _visualisationHeight = value; }
        }
        
        //Set SelectedObject = null 
        private void UnselectObjects()
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                if (vm.SelectedObject != null)
                    vm.SelectedObject = null;
                //else
                //{
                //    vm.CreateLine = false;
                //    vm.CreateFilledLine = false;
                //}
            } 
        }

        private void ButtonEbeneHigher_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null) vm.ChangeEbeneHeight(true);
        }

        private void ButtonEbeneLower_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null) vm.ChangeEbeneHeight(false);
        }

        private void ToggleLinePathButton()
        {
           PathLineButton.IsChecked = (!Convert.ToBoolean(PathLineButton.IsChecked));
        }

        private void ToggleFilledLinePathButton()
        {
            FilledPathLineButton.IsChecked = (!Convert.ToBoolean(FilledPathLineButton.IsChecked));
        }

        private void ArenaGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if(vm!=null) vm.SelectionChangedUpdateSliders();
        }

        private void ArenaScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //ListBox lb = sender as ListBox;
            //if (lb == null)
            //    return;
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                if (_zoomChanged)
                {
                    _zoomChanged = false;
                    vm.CurrentMousePositionX = Mouse.GetPosition(ArenaGrid).X;
                    vm.CurrentMousePositionY = Mouse.GetPosition(ArenaGrid).Y;
                }
            }
        }

        private void ButtonEbeneHigherMax_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null) vm.MoveSelectedObjectToTop(true);
        }

        private void ButtonEbeneLowerMin_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null) vm.MoveSelectedObjectToTop(false);
        }

        private void Button_SaveXML_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Battleground_"+System.DateTime.Now.ToShortDateString(); // Default file name
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (.xml)|*.xml";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null) vm.SaveBattlegroundToXML(dlg.FileName);
            }
            
        }

        private void Button_LoadXML_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (.xml)|*.xml"; 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null) vm.LoadBattlegroundFromXML(dlg.FileName);
            }
            AddPictureButtons(); //reload new pictures
            
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null) vm.ClearBattleground();
        }

        private void ButtonStickHeroes_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null && !vm.BattlegroundObjects.Where(x => x is MeisterGeister.Model.Gegner && x.IsSticked).Any()) vm.StickHeroes();
        }

        private void ButtonSticCreatues_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null) vm.StickEnemies();
        }

        private void view_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM.KampfWindow != null)
                VM.KampfWindow.Close();
            VM.Dispose();
        }



        private void ButtonBildhinzun_Click(object sender, RoutedEventArgs e)
        {
            string dir = "Daten\\Bodenplan";
            var allowedExtensions = new HashSet<string>(MeisterGeister.Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES, StringComparer.OrdinalIgnoreCase);
            string aktDir = System.IO.Directory.GetCurrentDirectory();
            string exeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            List<string> dateien = new List<string>();
            dateien = ViewHelper.ChooseFiles("Bilder hinzufügen", "", true, new String[9] { "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff" });
            if (dateien == null) return;
            foreach (string datei in dateien)
            {
                string s = exeDirectory + "\\" + dir  + "\\" + System.IO.Path.GetFileName(datei);
                File.Copy(datei, s);
            }
            System.IO.Directory.SetCurrentDirectory(aktDir);
            AddPictureButtons();
        }

        public void sl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 1)
                ((Slider)sender).Value += ((((Slider)sender).Value < ((Slider)sender).Maximum - 4) ? 5 : ((((Slider)sender).Maximum - ((Slider)sender).Value)));
            else
                ((Slider)sender).Value += ((((Slider)sender).Value > ((Slider)sender).Minimum + 4) ? -5 : ((Slider)sender).Minimum);
        }

        public void slSmall_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double d = ((Slider)sender).Value;
            double dr = ((Slider)sender).Minimum + .009;
            double min = ((Slider)sender).Minimum;
            if (e.Delta > 1)
                ((Slider)sender).Value = Math.Round(((((Slider)sender).Value + .01 > ((Slider)sender).Maximum) ? ((Slider)sender).Maximum : ((Slider)sender).Value + .01), 2);
            else
                ((Slider)sender).Value = Math.Round(((((Slider)sender).Value - .01 > ((Slider)sender).Minimum) ? ((Slider)sender).Value - .01 : ((Slider)sender).Minimum), 2);
        }

        private void tbtnSpielerIniScreen_Click(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)e.OriginalSource).IsChecked == true)
            {
                CreateKampfWindow();  
            }
            else
            {
                if (VM.KampfWindow != null) VM.KampfWindow.Close();
                if (VM.SpielerScreenWindow != null) VM.SpielerScreenWindow.Topmost = false;

                if (Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive &&
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Left >= System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
                {
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowState = System.Windows.WindowState.Maximized;
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowStyle = System.Windows.WindowStyle.None;
                }

            }
        }
        
        private double WidthStart = 0;
        private bool MouseIsOverScrViewer = false;


        private void CreateKampfWindow()
        {
            var vm = DataContext as BattlegroundViewModel;
            Kampf.KampfInfoView infoView = new Kampf.KampfInfoView(Global.CurrentKampf);

            infoView.grdMain.LayoutTransform = new ScaleTransform(vm.ScaleKampfGrid, vm.ScaleKampfGrid);
            VM.KampfWindow = new Window();
            //SizeToContent auf Width setzt den Screen auf minimale Breite
            VM.KampfWindow.SizeToContent = SizeToContent.Width;
            VM.KampfWindow.Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                var vm2 = DataContext as BattlegroundViewModel;
                if (vm2 != null)
                    vm2.IsShowIniKampf = false;
                VM.KampfWindow = null;
            };
            infoView.scrViewer.MouseEnter += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = true; };
            infoView.scrViewer.MouseLeave += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = false;};
            infoView.scrViewer.ScrollChanged += (object sender, ScrollChangedEventArgs e) => 
            {
                if (!MouseIsOverScrViewer)
                {
                    if (((ScrollViewer)sender).ScrollableWidth > 0)
                    {
                        int anzInisDavor = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count(t => t.Kampfrunde < Global.CurrentKampf.Kampf.Kampfrunde);
                        double width1Ini = ((ScrollViewer)sender).ExtentWidth / Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count();
                        ((ScrollViewer)sender).ScrollToHorizontalOffset(width1Ini * anzInisDavor);
                    }
                }
            };
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

                    if (infoView.scrViewer.ScrollableWidth > 0)
                    {
                        int anzInisDavor = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count(t => t.Kampfrunde < Global.CurrentKampf.Kampf.Kampfrunde);
                        double width1Ini = infoView.scrViewer.ExtentWidth / Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count();
                        infoView.scrViewer.ScrollToHorizontalOffset(width1Ini * anzInisDavor);
                    }

                    if (WidthStart != Math.Round(VM.KampfWindow.Width / vm.ScaleKampfGrid))
                        WidthStart = Math.Round(VM.KampfWindow.Width / vm.ScaleKampfGrid);
                }
            };

            if (VM.SpielerScreenWindow != null) VM.SpielerScreenWindow.Topmost = false;
            VM.KampfWindow.Topmost = true;
            VM.KampfWindow.Content = infoView;
            VM.KampfWindow.Show();
            //SizeToContent muss auf Height gestellt werden, damit die Höhe an die Anz. Kämpfer angepasst wird
            VM.KampfWindow.SizeToContent = SizeToContent.Height;
            //SizeToContent muss wieder auf Manual gesetzt werden da das Window sonst immer größer wird
            VM.KampfWindow.SizeToContent = SizeToContent.Manual;
            VM.KampfWindow.MinWidth = 460;
            WidthStart = VM.KampfWindow.Width;
            VM.KampfWindow.Width = Math.Round(WidthStart * vm.ScaleKampfGrid);
            VM.KampfWindow.Top = 0;

            if (System.Windows.Forms.Screen.AllScreens.Length > 1)
                VM.KampfWindow.Left = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Right// + System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width
                      - Math.Round(WidthStart * vm.ScaleKampfGrid);
            VM.IsShowIniKampf = true;
            if (Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive &&
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Left >= System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
            {
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowState = System.Windows.WindowState.Normal;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowStyle = System.Windows.WindowStyle.None;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width = System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - Math.Round(WidthStart * vm.ScaleKampfGrid);
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Height = System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Height;
            }
        }
        
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null && vm.KampfWindow != null)
            {
                ((Kampf.KampfInfoView)vm.KampfWindow.Content).grdMain.LayoutTransform = new ScaleTransform(vm.ScaleKampfGrid, vm.ScaleKampfGrid);
                VM.KampfWindow.SizeToContent = SizeToContent.Height;
                //SizeToContent muss wieder auf Manual gesetzt werden da das Window sonst immer größer wird
                VM.KampfWindow.SizeToContent = SizeToContent.Manual;
                VM.KampfWindow.Width = Math.Round(WidthStart * vm.ScaleKampfGrid);
            }
        }
        

        private void MeisterArenaZoom_Click(object sender, RoutedEventArgs e)
        {
            if (Global.ContextHeld.HeldenGruppeListe.Count > 0)
            {
                ArenaScrollViewer.Zoom = 1;
                double xMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureX) - Global.ContextHeld.HeldenGruppeListe[0].CreatureWidth;
                double yMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureY) - Global.ContextHeld.HeldenGruppeListe[0].CreatureHeight;
                //double xMax = Global.ContextHeld.HeldenGruppeListe.Max(t => t.CreatureX) + Global.ContextHeld.HeldenGruppeListe[0].CreatureWidth;
                //double yMax = Global.ContextHeld.HeldenGruppeListe.Max(t => t.CreatureY) + Global.ContextHeld.HeldenGruppeListe[0].CreatureHeight;
                ArenaScrollViewer.TranslateX = -xMin;
                ArenaScrollViewer.TranslateY = -yMin;
            }
        }





        #region --- Tastatur abfragen ---

        MenuItem miOpen = null;
        public static RoutedCommand ThemeCommandCheck = new RoutedCommand();
        private void ArenaGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //STRG - Abfragen um return zu setzen
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) return;

            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                var menuitem = ((DependencyObject)e.OriginalSource).FindAnchestor<MenuItem>();
                if (menuitem != null)
                {
                    if (menuitem.HasItems)
                    {
                        menuitem.IsSubmenuOpen = !menuitem.IsSubmenuOpen;
                        miOpen = menuitem.IsSubmenuOpen ? menuitem : null;
                    }
                    else
                    {
                        ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe
                            .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde) 
                            .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == vm.SelectedObject as IKämpfer);
                        if (mi != null)
                        {
                            if (menuitem.Name == "miKämpferZauber" || (miOpen != null && miOpen.Name == "miKämpferZauber"))
                                mi.UmwandelnZauber.Execute(menuitem.CommandParameter);
                            if (menuitem.Name == "miKämpferFernkampf" || (miOpen != null && miOpen.Name == "miKämpferFernkampf"))
                                mi.UmwandelnFernkampf.Execute(menuitem.CommandParameter);
                        }
                        if (miOpen != null && miOpen.IsSubmenuOpen)
                            miOpen.IsSubmenuOpen = false;
                        miOpen = null;
                        Global.CurrentKampf.SelectedManöver = mi;
                    }
                    return;
                }

                if (miOpen != null && miOpen.IsSubmenuOpen)
                    miOpen.IsSubmenuOpen = false;
                miOpen = null;

                var slider = ((DependencyObject)e.OriginalSource).FindAnchestor<Slider>();
                if (slider != null) return;

                var listboxItem = ((DependencyObject)e.OriginalSource).FindAnchestor<ListBoxItem>();
                if (listboxItem != null)
                {
                    BattlegroundBaseObject o = ArenaGrid.ItemContainerGenerator.ItemFromContainer(listboxItem) as BattlegroundBaseObject;
                    vm.SelectedObject = o; //TODO: Zugriff muss aus dem anderen Thread ausgeführt werden.
                    vm.IsMoving = true;
                    e.Handled = true;
                }

                var button = ((DependencyObject)e.OriginalSource).FindAnchestor<Button>();
                if (button != null)
                {
                    ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe
                            .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde)
                            .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == vm.SelectedObject as IKämpfer);
                    if (mi == null)
                    {
                        mi = Global.CurrentKampf.Kampf.InitiativListe
                               .LastOrDefault(t => t.Manöver.Ausführender.Kämpfer == vm.SelectedObject as IKämpfer);
                        ZeitImKampf zik = Global.CurrentKampf.Kampf.AktuelleAktionszeit;
                        zik.InitiativPhase = zik.InitiativPhase - 1;

                        mi.Manöver.VerbleibendeDauer = 0; 
                        //mi.Manöver.Dauer = mi.Start  .Aktionszeiten.Last(). .End = zik;
                    //(vm.SelectedObject as Wesen).ki.AngriffsManöver.Last().DauerInKampfaktionen
                     //   (vm.SelectedObject as Wesen).ki. = 1;
                       // (vm.SelectedObject as Wesen).ki.StandardAktionenSetzen(Global.CurrentKampf.Kampf.Kampfrunde);
                        Global.CurrentKampf.Kampf.SortedInitiativListe =
                            Global.CurrentKampf.Kampf.InitiativListe.Where(t => t.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase);
                    }
                    if (button.Name == "UmwandelnAttacke")
                        mi.UmwandelnAttacke.Execute(null);
                    if (button.Name == "UmwandelnFernkampf")
                        mi.UmwandelnFernkampf.Execute(null);
                    if (button.Name == "UmwandelnZauber")
                        mi.UmwandelnZauber.Execute(null);
                    if (button.Name == "UmwandelnSonstiges")
                        mi.UmwandelnSonstiges.Execute(null);

                    Global.CurrentKampf.SelectedManöver = mi;
                    return;
                }
            }
        }

        private void ArenaGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                if (vm.FogFreimachen && Keyboard.IsKeyDown(Key.LeftCtrl) && VM.FogPixelData != null)
                {
                    vm.SelectedObject = null;
                    WriteableBitmap wbmap = VM.FogImage;
                    int newX = (int)vm.CurrentMousePositionX / 10;// (int)Mouse.GetPosition((IInputElement)sender).X;
                    int newY = (int)vm.CurrentMousePositionY / 10;// (int)Mouse.GetPosition((IInputElement)sender).Y;

                    int w = 10 * vm.FogFreeSize+1;
                    int h = (10 * vm.FogFreeSize +1) * 10-1;
                    int[] leererBereich = new int[w * h];

                    for (int i = 0; i < 100 * vm.FogFreeSize + 1; i++)
                        Array.ConstrainedCopy(leererBereich, 0, vm.FogPixelData, i * 1000, w * h);
                    
                    //Bei vm.FogFreeSize = 1
                    // i = 0        0, 1000, 2000, 3000, 4000, ... 110*1000
                    // i = 1        1, 1001, 2001, 3001, 4001, ... 110*1000+1
                    // ...
                    // i = 11      11, 1011, 2011, 3011, 4011, ... 110*1000+11
                    int ber = 1000;
                    wbmap.WritePixels(new Int32Rect(newX, newY,
                        newX + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newX : 10 * vm.FogFreeSize + 1,
                        newY + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newY : 10 * vm.FogFreeSize + 1),
                        VM.FogPixelData, ber, 0); // widthInByte

                    VM.FogImage = wbmap;
                }
                else
                {
                    if (vm.SelectedObject != null) vm.SelectionChangedUpdateSliders();

                    if (vm.CreateLine)
                    {
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        vm.CreatingNewLine = true;
                        var line = vm.CreateNewPathLine(_x1, _y1);
                        line.IsNew = true; // TODO sollte in die Methode ^ mit rein
                        e.Handled = true;
                    }
                    else if (vm.CreateFilledLine)
                    {
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        vm.CreatingNewFilledLine = true;
                        var line = vm.CreateNewFilledLine(_x1, _y1);
                        line.IsNew = true; // TODO sollte in die Methode ^ mit rein
                        e.Handled = true;
                    }
                }
            }
        }
        
        private void ArenaGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.IsMoving = false;
                //handling different possibilities based on Objects (like Pathline or different BattlegroundBaseObject)
                if (vm.CreatingNewLine || vm.CreatingNewFilledLine)
                {
                    vm.FinishCurrentPathLine();
                    e.Handled = true;
                    vm.CreatingNewLine = false;
                    vm.CreatingNewFilledLine = false;
                    vm.UpdateCreatureLevelToTop();
                }
                else if (vm.SelectedObject != null)
                {
                    if (vm.BattlegroundObjects.Where(x => x is ViewModel.Kampf.Logic.Wesen && x.IsSticked).Any())
                    {
                        var currentcreature = vm.BattlegroundObjects.Where(x => x is ViewModel.Kampf.Logic.Wesen && x.IsSticked).First();
                        currentcreature.IsSticked = false;

                        if (vm.BattlegroundObjects.Where(x => x is MeisterGeister.Model.Held && x.IsSticked).Any())
                        {
                            vm.CurrentlySelectedCreature = ((MeisterGeister.Model.Held)vm.BattlegroundObjects.Where(x => x is MeisterGeister.Model.Held && x.IsSticked).First()).Name;
                        }
                        else if (vm.BattlegroundObjects.Where(x => x is MeisterGeister.Model.Gegner && x.IsSticked).Any())
                        {
                            vm.CurrentlySelectedCreature = ((MeisterGeister.Model.Gegner)vm.BattlegroundObjects.Where(x => x is MeisterGeister.Model.Gegner && x.IsSticked).First()).Name;
                        }
                        else
                        {
                            vm.CurrentlySelectedCreature = "";
                        }

                    }
                    else if (vm.SelectedObject is Wesen)
                    {
                        double deltaX = vm.CurrentMousePositionX - ((Wesen)vm.SelectedObject).CreatureNameX;
                        double deltaY = vm.CurrentMousePositionY - ((Wesen)vm.SelectedObject).CreatureNameY;
                        deltaX = deltaX < 0 ? deltaX * -1 : deltaX;
                        deltaY = deltaY < 0 ? deltaY * -1 : deltaY;
                        if (deltaX <= 50 && deltaY <= 50)
                        {
                            if (((Wesen)vm.SelectedObject).Position == Position.Stehend) ((Wesen)vm.SelectedObject).Position = Position.Kniend;
                            else if (((Wesen)vm.SelectedObject).Position == Position.Kniend) ((Wesen)vm.SelectedObject).Position = Position.Liegend;
                            else if (((Wesen)vm.SelectedObject).Position == Position.Liegend) ((Wesen)vm.SelectedObject).Position = Position.Reitend;
                            else if (((Wesen)vm.SelectedObject).Position == Position.Reitend) ((Wesen)vm.SelectedObject).Position = Position.Fliegend;
                            else if (((Wesen)vm.SelectedObject).Position == Position.Fliegend) ((Wesen)vm.SelectedObject).Position = Position.Schwebend;
                            else ((Wesen)vm.SelectedObject).Position = Position.Stehend;
                        }
                        VM.UpdateCreaturesFromChangedKampferlist();
                    }
                    ArenaGrid.Cursor = Cursors.Arrow;
                }
            }
        }

        private void ArenaGrid_MouseMove(object sender, MouseEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                if (vm.useFog && Keyboard.IsKeyDown(Key.LeftCtrl))
                    vm.FogFreimachen = true;
                else
                    vm.FogFreimachen = false;
                if (vm.FogFreimachen && vm.FogPixelData != null)
                {
                    vm.SelectedObject = null;
                    vm.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
                    vm.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
                    if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                    {
                        WriteableBitmap wbmap = VM.FogImage;
                        int newX = (int)vm.CurrentMousePositionX / 10;
                        int newY = (int)vm.CurrentMousePositionY / 10;
                        int w = 10 * vm.FogFreeSize+1;
                        int h = (10 * vm.FogFreeSize+1)*10;
                        int[] leererBereich = new int[w * h];
                        if (e.RightButton == MouseButtonState.Pressed)
                            leererBereich  = Enumerable.Repeat(-0x01000000, w * h).ToArray();

                        for (int i = 0; i < 100 * vm.FogFreeSize + 1; i++)
                            Array.ConstrainedCopy(leererBereich, 0, vm.FogPixelData, i * 1000, w * h);// 100 * vm.FogFreeSize + 1);// w * h);
                        
                        //Bei vm.FogFreeSize = 1
                        // i = 0        0, 1000, 2000, 3000, 4000, ... 110*1000
                        // i = 1        1, 1001, 2001, 3001, 4001, ... 110*1000+1
                        //...
                        // i = 11      11, 1011, 2011, 3011, 4011, ... 110*1000+11                        
                        int ber = 1000;
                        wbmap.WritePixels(new Int32Rect(newX, newY,
                                newX + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newX : 10 * vm.FogFreeSize + 1,
                                newY + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newY : 10 * vm.FogFreeSize + 1),
                                VM.FogPixelData, ber, 0);
                            VM.FogImage = wbmap;                        
                    }
                    return;
                }

                //cursor pixelchange?
                if (Math.Round(e.GetPosition(ArenaGrid).X, 0) != vm.CurrentMousePositionX ||
                    Math.Round(e.GetPosition(ArenaGrid).Y, 0) != vm.CurrentMousePositionY)
                {
                    vm.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
                    vm.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
                    var listboxItem = ((DependencyObject)e.OriginalSource).FindAnchestor<ListBoxItem>();
                    //handling different possibilities based on Objects (like MoveObject or Hero, Create Line..)
                    var tempstick = vm.BattlegroundObjects.Where(x => x is ViewModel.Kampf.Logic.Wesen && x.IsSticked).ToList();
                    foreach (var wesen in tempstick)
                    {
                        ((BattlegroundCreature)wesen).MoveObject(vm.CurrentMousePositionX, vm.CurrentMousePositionY, true);
                    }
                    if (vm.CreatingNewLine || vm.CreatingNewFilledLine)
                    {
                        _x2 = e.GetPosition(ArenaGrid).X;
                        _y2 = e.GetPosition(ArenaGrid).Y;
                        vm.MoveWhileDrawing(_x2, _y2, vm.Freizeichnen);
                    }
                    else if (e.LeftButton == MouseButtonState.Pressed && vm.SelectedObject != null && vm.IsMoving)
                    {
                        ArenaGrid.Cursor = Cursors.Hand;
                        vm.MoveObject(_xMovingOld, _yMovingOld, vm.CurrentMousePositionX, vm.CurrentMousePositionY);
                    }
                    _xMovingOld = vm.CurrentMousePositionX;
                    _yMovingOld = vm.CurrentMousePositionY;
                }
            }
        }

        private void ArenaGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.IsMoving = false;
            }
        }
        

        private void ArenaGrid_PreviewRightMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ListBox lb = sender as ListBox;
            //if (lb == null)
            //    return;
            e.Handled = true;
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                if (vm.FogFreimachen && Keyboard.IsKeyDown(Key.LeftCtrl) && VM.FogPixelData != null)
                {
                    WriteableBitmap wbmap = VM.FogImage;
                    int newX = (int)vm.CurrentMousePositionX / 10;// (int)Mouse.GetPosition((IInputElement)sender).X;
                    int newY = (int)vm.CurrentMousePositionY / 10;// (int)Mouse.GetPosition((IInputElement)sender).Y;


                    int w = 10 * vm.FogFreeSize + 1;
                    int h = (10 * vm.FogFreeSize + 1) * 10 - 1;
                    int[] leererBereich = Enumerable.Repeat(-0x01000000, w * h).ToArray();

                    for (int i = 0; i < 100 * vm.FogFreeSize + 1; i++)
                        Array.ConstrainedCopy(leererBereich, 0, vm.FogPixelData, i * 1000, w * h);
                        
                    int ber = 1000;
                    wbmap.WritePixels(new Int32Rect(newX, newY,
                        newX + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newX : 10 * vm.FogFreeSize + 1,
                        newY + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newY : 10 * vm.FogFreeSize + 1),
                        VM.FogPixelData, ber, 0); // widthInByte

                    VM.FogImage = wbmap;
                    return;
                }


                if (vm.SelectedObject == null) return;
                if (vm.SelectedObject is ViewModel.Kampf.Logic.Wesen)
                {
                    ((BattlegroundCreature)vm.SelectedObject).CalculateNewSightLineSektor(new Point(e.GetPosition(ArenaGrid).X, e.GetPosition(ArenaGrid).Y), VM.RechteckGrid);
                }
                else if (vm.SelectedObject is ImageObject)
                {
                    ((ImageObject)vm.SelectedObject).CalculateNewDirection(new Point(e.GetPosition(ArenaGrid).X, e.GetPosition(ArenaGrid).Y));
                }
            }
        }

        //private void UserControl_KeyDown(object sender, KeyEventArgs e)
        //{
        //    var vm = DataContext as BattlegroundViewModel;
        //    if (vm != null)
        //    {
        //        vm.Freizeichnen = (e.Key == Key.LeftShift);
        //        if (e.Key == Key.Delete && vm.SelectedObject != null) vm.Delete();
        //        if (vm.Freizeichnen && (vm.CreatingNewLine || vm.CreatingNewFilledLine))
        //        {
        //            _x2 = _xMovingOld;
        //            _y2 = _yMovingOld;
        //            vm.MoveWhileDrawing(_x2, _y2, vm.Freizeichnen);
        //        }
        //    }
        //    if (e.Key == Key.Escape) UnselectObjects();
        //    if (e.Key == Key.D1) ToggleLinePathButton();
        //    if (e.Key == Key.D2) ToggleFilledLinePathButton();


        //}

        //private void UserControl_KeyUp(object sender, KeyEventArgs e)
        //{
        //    var vm = DataContext as BattlegroundViewModel;
        //    if (vm != null)
        //    {
        //        if (e.Key == Key.LeftCtrl && Keyboard.IsKeyUp(Key.LeftCtrl))
        //            VM.FogFreimachen = false;
        //        if (e.Key == Key.LeftShift) { vm.Freizeichnen = !vm.Freizeichnen; vm.LeftShiftPressed = !vm.LeftShiftPressed; }
        //    }
        //}

        #endregion
    }
}

