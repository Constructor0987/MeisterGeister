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
using Microsoft.Win32.SafeHandles;
using System.Windows.Interop;

namespace MeisterGeister.View.Bodenplan
{
    /// <summary>
    /// Interaction logic for BattlegroundView.xaml
    /// </summary>
    public partial class BattlegroundView : UserControl
    {

        public class MyTimer
        {
            static int start = 0;
            static int stop = 0;
            public static void start_timer()
            {
                start = Environment.TickCount;
            }

            public static void stop_timer()
            {
                stop_timer("");
            }

            public static void stop_timer(string msg)
            {
                stop = Environment.TickCount;
                print(msg);
            }

            private static void print(string msg)
            {
                string output = "MyTimer(" + msg + "): " + (stop - start) + " Millisekunden";
                System.Diagnostics.Debug.WriteLine(output);
            }
        }


        private double _x1, _y1, _x2, _y2;
        public double _xMovingOld, _yMovingOld;
        private bool _zoomChanged = false;

        public BattlegroundView()
        {
            InitializeComponent();
            Global.CurrentKampf.BodenplanView = this;
            VM = new BattlegroundViewModel();
            
    //        ArenaGrid.Cursor = Cursors.Arrow;
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

        private ImageSource GetImageSource(string s)
        {
            ImageSource imgS = null;
            try
            {
                imgS = new BitmapImage(new Uri(s));
                return imgS;
            }
            catch 
            {
                return null;
            }
        }

         
     /// <summary> 
     /// The image resizing method. 
     /// </summary> 
     /// <param name="bitmapFrame"></param> 
     /// <param name="width"></param> 
     /// <param name="height"></param> 
     /// <returns></returns> 
     private static BitmapFrame Resize(BitmapFrame bitmapFrame, int width, int height) 
     { 
         double scaleWidth, scaleHeight; 
 
         // Resize proportionally to the width 
         if (height == 0) 
         { 
             scaleWidth = width; 
             scaleHeight = (((double)width / bitmapFrame.PixelWidth) * bitmapFrame.PixelHeight); 
         } 
         // Resize proportionally to the height 
         else if (width == 0) 
         { 
             scaleHeight = height; 
             scaleWidth = (((double)height / bitmapFrame.PixelHeight) * bitmapFrame.PixelWidth); 
         } 
         // Resize using the supplied width and height 
         else 
         { 
             scaleWidth = width; 
             scaleHeight = height; 
         } 
         // Create the scale transform 
         var scaleTransform = new ScaleTransform(scaleWidth/bitmapFrame.PixelWidth, scaleHeight/bitmapFrame.PixelHeight, 0, 0); 
 
 
         // Transform the bitmap frame 
         var transformedBitmap = new TransformedBitmap(bitmapFrame, scaleTransform); 
 
 
         return BitmapFrame.Create(transformedBitmap); 
     } 


        //Adds dynamicly created Picturebuttons for each picture in folder "Pictures".
        private void AddPictureButtons()
        {
            try
            {
                lstbxPictureButton.Items.Clear();
                //PictureButtonWrapPanel.Children.Clear(); // alte Bilder entfernen
                string appPath = Ressources.GetFullApplicationPath();
                String[] picurls = Ressources.GetPictureUrls();
                for (int i = 0; i < picurls.Count(); i++)
                {
                    if (!File.Exists(appPath + picurls[i])) continue;
                    String _buttonPrefix = "picbutton";
                    //var brush = new ImageBrush();
                    if (!File.Exists(appPath + picurls[i]) || GetImageSource(appPath + picurls[i]) == null) continue;

                    //BitmapImage bImg = new BitmapImage(new Uri(appPath + picurls[i]));

                    //var bitmap = BitmapFrame.Create(bImg);
                    //var resizedBitmapFrame = Resize(bitmap, 48, 48);
                    //Image img = new Image();
                    //img.Source = resizedBitmapFrame;
                    
                    //brush.ImageSource = isource; //new BitmapImage(new Uri(appPath + picurls[i]));
                    String[] pathsplit = picurls[i].Split('\\');
                    Button b = new Button() { Name = _buttonPrefix + i, ToolTip = picurls[i], Width=140, HorizontalContentAlignment= System.Windows.HorizontalAlignment.Left };
                    //Image img = new Image();
                    //img.Width = 48;
                    //img.Height= 48;
                    //img.Stretch = Stretch.Fill;
                    //img.Source = new BitmapImage(new Uri(appPath + picurls[i]));
                    b.Content = System.IO.Path.GetFileNameWithoutExtension(appPath + picurls[i]);
                    // resizedBitmapFrame;// bImg;// new Image() { Width = 48, Height = 48, Source = new BitmapImage(new Uri(appPath + picurls[i])) };
                   // b.Content = new TextBox() { Text = picurls[i] };// img;
                    //b.Content = brush;
                   // b.Background = new ImageBrush(new ImageSource( new Uri(appPath + picurls[i]));

                    b.Tag = Ressources.GetFullApplicationPath() + picurls[i];
                    b.Click += (object sender, RoutedEventArgs e) =>
                        {
                            try
                            {
                                var vm = DataContext as BattlegroundViewModel;
                                if (vm != null)
                                {
                                    int strlength = b.Name.Length - 1;
                                    int buttonNr = Convert.ToInt32(b.Name.Substring(_buttonPrefix.Length, b.Name.Length - _buttonPrefix.Length));
                                    var newpic = vm.CreateImageObject(Ressources.Decoder((string)b.Tag), new Point(_xMovingOld >= -500 ? _xMovingOld + 500 : 0, _yMovingOld >= 500 ? _yMovingOld - 500 : 0));
                                    vm.MoveLastObjectBehindCreatures();
                                    //vm.UpdateCreatureLevelToTop();
                                }
                            }
                            catch (Exception ex)
                            {
                                ViewHelper.ShowError("Fehler beim Generieren der Bilddatei." + Environment.NewLine + "Bitte überprüfen, ob die Datei existiert:" + Environment.NewLine +
                                  Ressources.Decoder(Ressources.GetFullApplicationPath() + (string)b.Tag), ex);
                            }
                        };

                    //Grid.SetRow(b, Convert.ToInt32(i / 3));
                    //Grid.SetColumn(b, i % 3);
                    lstbxPictureButton.Items.Add(b);
                   // PictureButtonWrapPanel.Children.Add(b);
                }
            }
            catch (Exception ex)
            {
                //ViewHelper.ShowError("Fehler beim Laden der Bilddatei." + Environment.NewLine + "Bitte überprüfen, ob alle Bilddateien existieren und lesbar sind:" + Environment.NewLine +
                //  Ressources.GetFullApplicationPath() , ex);
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
            try
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null) vm.SelectionChangedUpdateSliders();
            }
            catch
            { }
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
            try
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
                    string s = exeDirectory + "\\" + dir + "\\" + System.IO.Path.GetFileName(datei);
                    if (File.Exists(s)) File.Delete(s);
                    if (!File.Exists(s)) File.Copy(datei, s);
                }
                System.IO.Directory.SetCurrentDirectory(aktDir);
                AddPictureButtons();
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Hinzufügen eines neuen Bildes", ex); }
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

                    if (VM.IniWidthStart != Math.Round(VM.KampfWindow.Width / vm.ScaleKampfGrid))
                        VM.IniWidthStart = Math.Round(VM.KampfWindow.Width / vm.ScaleKampfGrid);
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
            VM.IniWidthStart = VM.KampfWindow.Width;
            VM.KampfWindow.Width = Math.Round(VM.IniWidthStart * vm.ScaleKampfGrid);
            VM.KampfWindow.Top = 0;

            int maxRight = Math.Max(
                System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Right, 
                System.Windows.Forms.Screen.AllScreens.Length > 1 ?
                    System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Right : 0);
            VM.KampfWindow.Left = maxRight - Math.Round(VM.IniWidthStart * vm.ScaleKampfGrid);

            VM.IsShowIniKampf = true;
            if (Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive &&
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Left >= System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
            {
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowState = System.Windows.WindowState.Normal;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowStyle = System.Windows.WindowStyle.None;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width = System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - Math.Round(VM.IniWidthStart * vm.ScaleKampfGrid);
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
                VM.KampfWindow.Width = Math.Round(vm.IniWidthStart * vm.ScaleKampfGrid);
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
        Nullable<Point> pKämpfer = null;
        MenuItem miOpen = null;
        public static RoutedCommand ThemeCommandCheck = new RoutedCommand();
        private void ArenaGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //STRG - Abfragen um return zu setzen
                if (Keyboard.IsKeyDown(Key.LeftCtrl)) return;
                
                if (VM != null)
                {
                    if (VM.IsPointerVisible)
                    {
                        VM.SetPointer(ArenaGridTop);
                        e.Handled = true;
                        return;
                    }
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
                                .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == VM.SelectedObject as IKämpfer);
                            if (mi != null)
                            {
                                if (menuitem.Name == "miKämpferZauber" || (miOpen != null && miOpen.Name == "miKämpferZauber"))
                                    mi.UmwandelnZauber.Execute(menuitem.CommandParameter);
                                if (menuitem.Name == "miKämpferFernkampf" || (miOpen != null && miOpen.Name == "miKämpferFernkampf"))
                                    mi.UmwandelnFernkampf.Execute(menuitem.CommandParameter);

                                if (menuitem.Name == "miKämpferAttacke" || (miOpen != null && miOpen.Name == "miKämpferAttacke"))
                                    mi.UmwandelnAttacke.Execute(menuitem.CommandParameter);

                                if (menuitem.Name == "miKämpferSonstiges" || (miOpen != null && miOpen.Name == "miKämpferSonstiges"))
                                    mi.UmwandelnSonstiges.Execute(menuitem.CommandParameter);
                            }
                            else
                            { }
                            if (miOpen != null && miOpen.IsSubmenuOpen)
                                miOpen.IsSubmenuOpen = false;
                            miOpen = null;
                            Global.CurrentKampf.SelectedManöver = mi;
                            Global.CurrentKampf.Kampf.SelectedManöverInfo = mi;
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
                        listboxItem.Refresh();
                        if (VM.SelectedObject != null)
                            VM.SelectedObject.IsMoving = false;
                        BattlegroundBaseObject o = ArenaGrid.ItemContainerGenerator.ItemFromContainer(listboxItem) as BattlegroundBaseObject;
                        VM.SelectedObject = o;

                        e.Handled = true;
                    }

                    //var button = ((DependencyObject)e.OriginalSource).FindAnchestor<Button>();
                    //if (button != null)
                    //{
                    //    ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe
                    //            .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde)
                    //            .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == VM.SelectedObject as IKämpfer);
                    //    if (mi == null)
                    //    {
                    //        mi = Global.CurrentKampf.Kampf.InitiativListe
                    //               .LastOrDefault(t => t.Manöver.Ausführender.Kämpfer == VM.SelectedObject as IKämpfer);
                    //        ZeitImKampf zik = Global.CurrentKampf.Kampf.AktuelleAktionszeit;
                    //        zik.InitiativPhase = zik.InitiativPhase - 1;

                    //        if (mi == null) return;
                    //        mi.Manöver.VerbleibendeDauer = 0;
                    //        //mi.Manöver.Dauer = mi.Start  .Aktionszeiten.Last(). .End = zik;
                    //        //(vm.SelectedObject as Wesen).ki.AngriffsManöver.Last().DauerInKampfaktionen
                    //        //   (vm.SelectedObject as Wesen).ki. = 1;
                    //        // (vm.SelectedObject as Wesen).ki.StandardAktionenSetzen(Global.CurrentKampf.Kampf.Kampfrunde);
                    //        Global.CurrentKampf.Kampf.SortedInitiativListe =
                    //            Global.CurrentKampf.Kampf.InitiativListe.Where(t => t.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase);
                    //    }
                    //    if (button.Name == "UmwandelnAttacke")
                    //        mi.UmwandelnAttacke.Execute(null);
                    //    if (button.Name == "UmwandelnFernkampf")
                    //        mi.UmwandelnFernkampf.Execute(null);
                    //    if (button.Name == "UmwandelnZauber")
                    //        mi.UmwandelnZauber.Execute(null);
                    //    if (button.Name == "UmwandelnSonstiges")
                    //        mi.UmwandelnSonstiges.Execute(null);

                    //    Global.CurrentKampf.SelectedManöver = mi;
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Pre-Loslassen der Linken Maustaste", ex); }
        }

        private void ArenaGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //if (!Keyboard.IsKeyDown(Key.LeftCtrl))
                //{
                //    if (VM.IsPointerVisible)
                //    {
                //        VM.SetPointer(ArenaGridTop);
                //        e.Handled = true;
                //    }
                //    else
                //    {
                //        var menuitem = ((DependencyObject)e.OriginalSource).FindAnchestor<MenuItem>();
                //        if (menuitem != null)
                //        {
                //            if (menuitem.HasItems)
                //            {
                //                menuitem.IsSubmenuOpen = !menuitem.IsSubmenuOpen;
                //                miOpen = menuitem.IsSubmenuOpen ? menuitem : null;
                //            }
                //            else
                //            {
                //                ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe
                //                    .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde)
                //                    .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == VM.SelectedObject as IKämpfer);
                //                if (mi != null)
                //                {
                //                    if (menuitem.Name == "miKämpferZauber" || (miOpen != null && miOpen.Name == "miKämpferZauber"))
                //                        mi.UmwandelnZauber.Execute(menuitem.CommandParameter);
                //                    if (menuitem.Name == "miKämpferFernkampf" || (miOpen != null && miOpen.Name == "miKämpferFernkampf"))
                //                        mi.UmwandelnFernkampf.Execute(menuitem.CommandParameter);

                //                    if (menuitem.Name == "miKämpferAttacke" || (miOpen != null && miOpen.Name == "miKämpferAttacke"))
                //                        mi.UmwandelnAttacke.Execute(menuitem.CommandParameter);

                //                    if (menuitem.Name == "miKämpferSonstiges" || (miOpen != null && miOpen.Name == "miKämpferSonstiges"))
                //                        mi.UmwandelnSonstiges.Execute(menuitem.CommandParameter);
                //                }
                //                else
                //                { }
                //                if (miOpen != null && miOpen.IsSubmenuOpen)
                //                    miOpen.IsSubmenuOpen = false;
                //                miOpen = null;
                //                Global.CurrentKampf.SelectedManöver = mi;
                //                Global.CurrentKampf.Kampf.SelectedManöverInfo = mi;
                //            }
                //            return;
                //        }

                //        if (miOpen != null && miOpen.IsSubmenuOpen)
                //            miOpen.IsSubmenuOpen = false;
                //        miOpen = null;

                //        var slider = ((DependencyObject)e.OriginalSource).FindAnchestor<Slider>();
                //        if (slider != null) return;

                //        var listboxItem = ((DependencyObject)e.OriginalSource).FindAnchestor<ListBoxItem>();
                //        if (listboxItem != null)
                //        {
                //            if (VM.SelectedObject != null)
                //                VM.SelectedObject.IsMoving = false;
                //            BattlegroundBaseObject o = ArenaGrid.ItemContainerGenerator.ItemFromContainer(listboxItem) as BattlegroundBaseObject;
                //            VM.SelectedObject = o;
                //            e.Handled = true;
                //        }                    
                //    }
                //}

                if (VM.FogFreimachen && Keyboard.IsKeyDown(Key.LeftCtrl) && VM.FogPixelData != null)
                {
                    VM.SelectedObject = null;
                    WriteableBitmap wbmap = VM.FogImage;
                    int newX = (int)VM.CurrentMousePositionX / 10;// (int)Mouse.GetPosition((IInputElement)sender).X;
                    int newY = (int)VM.CurrentMousePositionY / 10;// (int)Mouse.GetPosition((IInputElement)sender).Y;

                    int w = 10 * VM.FogFreeSize + 1;
                    int h = (10 * VM.FogFreeSize + 1) * 10 - 1;
                    int[] leererBereich = new int[w * h];

                    for (int i = 0; i < 100 * VM.FogFreeSize + 1; i++)
                        Array.ConstrainedCopy(leererBereich, 0, VM.FogPixelData, i * 1000, w * h);

                    //Bei VM.FogFreeSize = 1
                    // i = 0        0, 1000, 2000, 3000, 4000, ... 110*1000
                    // i = 1        1, 1001, 2001, 3001, 4001, ... 110*1000+1
                    // ...
                    // i = 11      11, 1011, 2011, 3011, 4011, ... 110*1000+11
                    int ber = 1000;
                    wbmap.WritePixels(new Int32Rect(newX, newY,
                        newX + 10 * VM.FogFreeSize + 1 > 1000 ? 1000 - newX : 10 * VM.FogFreeSize + 1,
                        newY + 10 * VM.FogFreeSize + 1 > 1000 ? 1000 - newY : 10 * VM.FogFreeSize + 1),
                        VM.FogPixelData, ber, 0); // widthInByte

                    VM.FogImage = wbmap;
                }
                else
                {
                    if (VM.SelectedObject != null) VM.SelectionChangedUpdateSliders();

                    if (VM.CreateLine)
                    {
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        VM.CreatingNewLine = true;
                        var line = VM.CreateNewPathLine(_x1, _y1);
                        line.IsNew = true; // TODO sollte in die Methode ^ mit rein
                        e.Handled = true;
                    }
                    else if (VM.CreateFilledLine)
                    {
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        VM.CreatingNewFilledLine = true;
                        var line = VM.CreateNewFilledLine(_x1, _y1);
                        line.IsNew = true; // TODO sollte in die Methode ^ mit rein
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Drücken der Linken Maustaste", ex); }
        }

        private void ArenaGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null)
                {
                    vm.IsMoving = false;
                    if (vm.SelectedObject != null) vm.SelectedObject.IsMoving = false;
                    //handling different possibilities based on Objects (like Pathline or different BattlegroundBaseObject)
                    if (vm.CreatingNewLine || vm.CreatingNewFilledLine)
                    {
                        vm.FinishCurrentPathLine();
                        e.Handled = true;
                        vm.CreatingNewLine = false;
                        vm.CreatingNewFilledLine = false;
                        vm.MoveLastObjectBehindCreatures();
                        //vm.UpdateCreatureLevelToTop();
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
               //         ArenaGrid.Cursor = Cursors.Arrow;
                    }
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Loslassen der Linken Maustaste", ex); }
        }
        
        private void ArenaGrid_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {                
                if (VM.useFog && Keyboard.IsKeyDown(Key.LeftCtrl))
                    VM.FogFreimachen = true;
                else
                    VM.FogFreimachen = false;
                if (VM.FogFreimachen && VM.FogPixelData != null)
                {
                    VM.SelectedObject = null;
                    VM.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
                    VM.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
                    if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                    {
                        WriteableBitmap wbmap = VM.FogImage;
                        int newX = (int)VM.CurrentMousePositionX / 10;
                        int newY = (int)VM.CurrentMousePositionY / 10;
                        int w = 10 * VM.FogFreeSize + 1;
                        int h = (10 * VM.FogFreeSize + 1) * 10;
                        int[] leererBereich = new int[w * h];
                        if (e.RightButton == MouseButtonState.Pressed)
                            leererBereich = Enumerable.Repeat(-0x01000000, w * h).ToArray();

                        for (int i = 0; i < 100 * VM.FogFreeSize + 1; i++)
                            Array.ConstrainedCopy(leererBereich, 0, VM.FogPixelData, i * 1000, w * h);// 100 * VM.FogFreeSize + 1);// w * h);

                        //Bei VM.FogFreeSize = 1
                        // i = 0        0, 1000, 2000, 3000, 4000, ... 110*1000
                        // i = 1        1, 1001, 2001, 3001, 4001, ... 110*1000+1
                        //...
                        // i = 11      11, 1011, 2011, 3011, 4011, ... 110*1000+11                        
                        int ber = 1000;
                        wbmap.WritePixels(new Int32Rect(newX, newY,
                                newX + 10 * VM.FogFreeSize + 1 > 1000 ? 1000 - newX : 10 * VM.FogFreeSize + 1,
                                newY + 10 * VM.FogFreeSize + 1 > 1000 ? 1000 - newY : 10 * VM.FogFreeSize + 1),
                                VM.FogPixelData, ber, 0);
                        VM.FogImage = wbmap;
                    }
                    return;
                }

                //cursor pixelchange?
                if (Math.Abs(e.GetPosition(ArenaGrid).X - VM.CurrentMousePositionX) > 2 ||
                    Math.Abs(e.GetPosition(ArenaGrid).Y - VM.CurrentMousePositionY) > 2)
                {
                    VM.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
                    VM.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
                    
                    //Set new Position for sticked Wesen
                    var tempstick = VM.BattlegroundObjects.Where(x => x is ViewModel.Kampf.Logic.Wesen && x.IsSticked).ToList();
                    if (tempstick.Count > 0)
                    {
                        foreach (var wesen in tempstick)
                        {
                            ((BattlegroundCreature)wesen).MoveObject(VM.CurrentMousePositionX, VM.CurrentMousePositionY, true);
                        }
                    }
                    else
                    if (VM.CreatingNewLine || VM.CreatingNewFilledLine)
                    {
                        _x2 = e.GetPosition(ArenaGrid).X;
                        _y2 = e.GetPosition(ArenaGrid).Y;
                        VM.MoveWhileDrawing(_x2, _y2, VM.Freizeichnen);
                    }
                    else if (e.LeftButton == MouseButtonState.Pressed && VM.SelectedObject != null)
                    {
                        VM.IsMoving = true;
                        VM.SelectedObject.IsMoving = true;
                        if (VM.SelectedObject is BattlegroundCreature) 
                            VM.InitDnD = true;
                        else
                            VM.MoveObject(_xMovingOld, _yMovingOld, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                    }
                    if (VM.InitDnD)
                    {
                        VM.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
                        VM.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
                        pKämpfer = e.GetPosition(null);
                        VM.InitDnD = false;

                      //  ArenaGrid.Cursor = Cursors.Hand;

                        // Initialisiere drag & drop Operation
                        DataObject dragData = new DataObject("BMKämpfer", VM.SelectedObject);
                        DragDrop.DoDragDrop(ArenaGrid, dragData, DragDropEffects.Move);
                        pKämpfer = null;
                    }

                    _xMovingOld = VM.CurrentMousePositionX;
                    _yMovingOld = VM.CurrentMousePositionY;
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Bewegen der Maus", ex); }
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        
        Cursor cKämpfer = null;
        private void ArenaGrid_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            try
            {                
                if (e.Effects == DragDropEffects.Copy || e.Effects == DragDropEffects.Move)
                {
                    if (cKämpfer == null && pKämpfer != null)
                    {
                        Point curMousePosScreen = GetMousePosition();

                        double factorX = (VM.CurrentMousePositionX - (VM.SelectedObject as BattlegroundCreature).CreatureX) / (VM.SelectedObject as BattlegroundCreature).CreatureWidth;
                        int minusX = (int)((VM.SelectedObject as BattlegroundCreature).CreatureWidth * (factorX * ArenaScrollViewer.Zoom));

                        double factorY = (VM.CurrentMousePositionY - (VM.SelectedObject as BattlegroundCreature).CreatureY) / (VM.SelectedObject as BattlegroundCreature).CreatureHeight;
                        int minusY = (int)((VM.SelectedObject as BattlegroundCreature).CreatureHeight * (factorY * ArenaScrollViewer.Zoom));

                        SetCursorPos((int)curMousePosScreen.X - minusX, (int)curMousePosScreen.Y - minusY);

                        VM.SelectedObject.MoveObject( VM.CurrentMousePositionX, VM.CurrentMousePositionY,true);
                        _xMovingOld = VM.CurrentMousePositionX;
                        _yMovingOld = VM.CurrentMousePositionY;

                        //Get DPI Scaling from MainProgramm
                        Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
                        double dx = m.M11;
                        double dy = m.M22;

                        Image img = new Image();
                        img.Width = (VM.SelectedObject as BattlegroundCreature).CreatureWidth * ArenaScrollViewer.Zoom * dx;
                        img.Height = (VM.SelectedObject as BattlegroundCreature).CreatureHeight * ArenaScrollViewer.Zoom * dy;
                        img.Stretch = Stretch.Fill;
                        string pic = (ArenaGrid.SelectedItem as IKämpfer).Bild ?? "pack://application:,,," + "/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png";
                        
                        if (!pic.StartsWith("/") && !File.Exists(pic))
                            pic = "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png";
                        img.Source = new BitmapImage(new Uri(pic.StartsWith("/")? "pack://application:,,," + pic: pic)); 
                        cKämpfer = CreateCursor(img, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                    }
                    if (cKämpfer != null)
                    {
                        e.UseDefaultCursors = false;
                        Mouse.SetCursor(cKämpfer);
                        VM.KämpferDnDTempPos = new Point (VM.CurrentMousePositionX, VM.CurrentMousePositionY );
                                                
                        VM.MoveObject(_xMovingOld, _yMovingOld, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                        _xMovingOld = VM.CurrentMousePositionX;
                        _yMovingOld = VM.CurrentMousePositionY;
                    }
                }
                else
                    e.UseDefaultCursors = true;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Feedback Fehler" + Environment.NewLine + "Beim Starten des Drag'n'Drop-Vorgangs ist ein Fehler aufgetreten.", ex);
                e.UseDefaultCursors = true;
            }
        }


        #region -- CURSOR DRAG & DROP --
        public static Cursor CreateCursor(UIElement element, double dX, double dY)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(new Point(), element.DesiredSize));

            RenderTargetBitmap rtb =
              new RenderTargetBitmap(
               (int)element.DesiredSize.Width,
               (int)element.DesiredSize.Height,
                96, 96, PixelFormats.Pbgra32);
            
            rtb.Render(element);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                using (var bmp = new System.Drawing.Bitmap(ms))
                {
                    //string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                  //  BitmapImage bmpi1 = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/mouse_cursornormal.png"));

                    //System.Drawing.Bitmap bmpMouseNormal = new System.Drawing.Bitmap(bmp.Width, bmp.Height);
                    //using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))// bmpMouseNormal))
                    //{
                    //    //using (MemoryStream outStream = new MemoryStream())
                    //    //{
                    //    //    PngBitmapEncoder enc = new PngBitmapEncoder();
                    //    //    enc.Frames.Add(BitmapFrame.Create(bmpi1));
                    //    //    enc.Save(outStream);
                    //    //    g.DrawImage(new System.Drawing.Bitmap(outStream), 0, 0);
                    //    //}
                    //    g.DrawImage(bmp, Convert.ToInt32(bmp.Width), Convert.ToInt32(bmp.Height));
                    //}
                    return InternalCreateCursor(bmp);                    
                }
            }
        }
        private static class NativeMethods
        {
            public struct IconInfo
            {
                public bool fIcon;
                public int xHotspot;
                public int yHotspot;
                public IntPtr hbmMask;
                public IntPtr hbmColor;
            }

            [DllImport("user32.dll")]
            public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

            [DllImport("user32.dll")]
            public static extern bool DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        }

        private void ArenaGrid_Drop(object sender, DragEventArgs e)
        {
            VM.SelectedObject.IsMoving = false;
            VM.IsMoving = false;
            ((BattlegroundCreature)VM.SelectedObject).CalculateSightArea();
            cKämpfer = null;
            //pKämpfer = null;
            
        }

        private void ArenaGrid_DragOver(object sender, DragEventArgs e)
        {
            VM.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
            VM.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
        }

        private class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeIconHandle()
                : base(true)
            {
            }
            override protected bool ReleaseHandle()
            {
                return NativeMethods.DestroyIcon(handle);
            }
        }
        private static Cursor InternalCreateCursor(System.Drawing.Bitmap bmp)
        {
            var iconInfo = new NativeMethods.IconInfo();
            NativeMethods.GetIconInfo(bmp.GetHicon(), ref iconInfo);

            iconInfo.xHotspot = 0;
            iconInfo.yHotspot = 0;
            iconInfo.fIcon = false;

            SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
            return CursorInteropHelper.Create(cursorHandle);
        }
        
        #endregion

        private void ArenaGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null)
                {
                    vm.IsMoving = false;
                    if (vm.SelectedObject != null) vm.SelectedObject.IsMoving = false;
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Pre-Loslassen der Linken Maustaste", ex); }
        }
        

        private void ArenaGrid_PreviewRightMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
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
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Pre-Loslassen der Rechten Maustaste", ex); }
        }
        
        #endregion
    }
}

