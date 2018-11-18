using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;
using Microsoft.Win32.SafeHandles;

namespace MeisterGeister.View.Bodenplan
{
    /// <summary>
    /// Interaction logic for BattlegroundView.xaml
    /// </summary>
    public partial class BattlegroundView : UserControl
    {
        public double _xMovingOld, _yMovingOld;

        private double _visualisationHeight = 100;

        private double _visualisationWidth = 100;

        private double _x1, _y1;

        private bool _zoomChanged = false;

        private bool MouseIsOverScrViewer = false;

        public BattlegroundView()
        {
            InitializeComponent();
            Global.CurrentKampf.BodenplanView = this;
            VM = new BattlegroundViewModel();

            AddPictureButtons();
            AddFogOfWar();
            VM.KampfVM = Global.CurrentKampf;
        }

        public double VisualisationHeight
        {
            get { return _visualisationHeight; }
            private set { _visualisationHeight = value; }
        }

        public double VisualisationWidth
        {
            get { return _visualisationWidth; }
            private set { _visualisationWidth = value; }
        }

        public BattlegroundViewModel VM
        {
            get { return DataContext as BattlegroundViewModel; }
            set { DataContext = value; }
        }

        public void sl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 1)
            {
                ((Slider)sender).Value += ((((Slider)sender).Value < ((Slider)sender).Maximum - 4) ? 5 : ((((Slider)sender).Maximum - ((Slider)sender).Value)));
            }
            else
            {
                ((Slider)sender).Value += ((((Slider)sender).Value > ((Slider)sender).Minimum + 4) ? -5 : ((Slider)sender).Minimum);
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
            var scaleTransform = new ScaleTransform(scaleWidth / bitmapFrame.PixelWidth, scaleHeight / bitmapFrame.PixelHeight, 0, 0);

            // Transform the bitmap frame
            var transformedBitmap = new TransformedBitmap(bitmapFrame, scaleTransform);

            return BitmapFrame.Create(transformedBitmap);
        }

        private void AddFogOfWar()
        {
            //http://stackoverflow.com/questions/17767097/writeablebitmap-doesnt-change-pixel-color-in-wpf
            if (VM != null)
            {
                VM.AreanGrid = ArenaGridTop;
            }
        }

        /// <summary>
        /// Adds dynamicly created Picturebuttons for each picture in folder "Pictures".
        /// </summary>
        private void AddPictureButtons()
        {
            try
            {
                lstbxPictureButton.Items.Clear();
                var appPath = Ressources.GetFullApplicationPath();
                var picurls = Ressources.GetPictureUrls();
                for (var i = 0; i < picurls.Count(); i++)
                {
                    if (!File.Exists(appPath + picurls[i]))
                    {
                        continue;
                    }

                    var _buttonPrefix = "picbutton";
                    if (!File.Exists(appPath + picurls[i]) || GetImageSource(appPath + picurls[i]) == null)
                    {
                        continue;
                    }

                    var pathsplit = picurls[i].Split('\\');
                    var b = new Button() { Name = _buttonPrefix + i, ToolTip = picurls[i], Width = 140, HorizontalContentAlignment = HorizontalAlignment.Left };
                    b.Content = Path.GetFileNameWithoutExtension(appPath + picurls[i]);

                    b.Tag = Ressources.GetFullApplicationPath() + picurls[i];
                    b.Click += (object sender, RoutedEventArgs e) =>
                        {
                            try
                            {
                                var vm = DataContext as BattlegroundViewModel;
                                if (vm != null)
                                {
                                    var strlength = b.Name.Length - 1;
                                    var buttonNr = Convert.ToInt32(b.Name.Substring(_buttonPrefix.Length, b.Name.Length - _buttonPrefix.Length));
                                    ImageObject newpic = vm.CreateImageObject(Ressources.Decoder((string)b.Tag), new Point(_xMovingOld >= -500 ? _xMovingOld + 500 : 0, _yMovingOld >= 500 ? _yMovingOld - 500 : 0));
                                    vm.MoveLastObjectBehindCreatures();
                                }
                            }
                            catch (Exception ex)
                            {
                                ViewHelper.ShowError("Fehler beim Generieren der Bilddatei." + Environment.NewLine + "Bitte überprüfen, ob die Datei existiert:" + Environment.NewLine +
                                  Ressources.Decoder(Ressources.GetFullApplicationPath() + (string)b.Tag), ex);
                            }
                        };

                    lstbxPictureButton.Items.Add(b);
                }
            }
            catch (Exception)
            {
            }
        }

        private void ArenaGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var vm = DataContext as BattlegroundViewModel;
                if (vm != null)
                {
                    vm.SelectionChangedUpdateSliders();
                }
            }
            catch
            { }
        }

        private void ArenaScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
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
                    vm.LoadBattlegroundFromXML(dlg.FileName);
                }
            }
            AddPictureButtons(); //reload new pictures
            VM.SelectedObject = null;
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.ClearBattleground();
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

        private void ButtonBildhinzun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dir = "Daten\\Bodenplan";
                var allowedExtensions = new HashSet<string>(MeisterGeister.Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES, StringComparer.OrdinalIgnoreCase);
                var aktDir = System.IO.Directory.GetCurrentDirectory();
                var exeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                var dateien = new List<string>();
                dateien = ViewHelper.ChooseFiles("Bilder hinzufügen", "", true, new string[9] { "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff" });
                if (dateien == null)
                {
                    return;
                }

                foreach (var datei in dateien)
                {
                    var s = exeDirectory + "\\" + dir + "\\" + Path.GetFileName(datei);
                    if (File.Exists(s))
                    {
                        File.Delete(s);
                    }

                    if (!File.Exists(s))
                    {
                        File.Copy(datei, s);
                    }
                }
                System.IO.Directory.SetCurrentDirectory(aktDir);
                AddPictureButtons();
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Hinzufügen eines neuen Bildes", ex); }
        }

        private void ButtonEbeneHigher_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.ChangeEbeneHeight(true);
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

        private void ButtonEbeneLower_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.ChangeEbeneHeight(false);
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

        private void ButtonSticCreatues_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null)
            {
                vm.StickEnemies();
            }
        }

        private void ButtonStickHeroes_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as BattlegroundViewModel;
            if (vm != null && !vm.BattlegroundObjects.Where(x => x is Model.Gegner && x.IsSticked).Any())
            {
                vm.StickHeroes();
            }
        }

        private void CreateKampfWindow()
        {
            var vm = DataContext as BattlegroundViewModel;
            var infoView = new Kampf.KampfInfoView(Global.CurrentKampf);

            infoView.grdMain.LayoutTransform = new ScaleTransform(vm.ScaleKampfGrid, vm.ScaleKampfGrid);
            VM.KampfWindow = new Window
            {
                //SizeToContent auf Width setzt den Screen auf minimale Breite
                SizeToContent = SizeToContent.Width
            };
            VM.KampfWindow.Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                var vm2 = DataContext as BattlegroundViewModel;
                if (vm2 != null)
                {
                    vm2.IsShowIniKampf = false;
                }

                VM.KampfWindow = null;
            };
            infoView.scrViewer.MouseEnter += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = true; };
            infoView.scrViewer.MouseLeave += (object sender, MouseEventArgs e) => { MouseIsOverScrViewer = false; };
            infoView.scrViewer.ScrollChanged += (object sender, ScrollChangedEventArgs e) =>
            {
                if (!MouseIsOverScrViewer)
                {
                    if (((ScrollViewer)sender).ScrollableWidth > 0)
                    {
                        var anzInisDavor = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count(t => t.Kampfrunde < Global.CurrentKampf.Kampf.Kampfrunde);
                        var width1Ini = ((ScrollViewer)sender).ExtentWidth / Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count();
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
                        var anzInisDavor = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count(t => t.Kampfrunde < Global.CurrentKampf.Kampf.Kampfrunde);
                        var width1Ini = infoView.scrViewer.ExtentWidth / Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count();
                        infoView.scrViewer.ScrollToHorizontalOffset(width1Ini * anzInisDavor);
                    }

                    if (VM.IniWidthStart != Math.Round(VM.KampfWindow.Width / vm.ScaleKampfGrid))
                    {
                        VM.IniWidthStart = Math.Round(VM.KampfWindow.Width / vm.ScaleKampfGrid);
                    }
                }
            };

            if (VM.SpielerScreenWindow != null)
            {
                VM.SpielerScreenWindow.Topmost = false;
            }

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

            var maxRight = Math.Max(
                System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Right,
                System.Windows.Forms.Screen.AllScreens.Length > 1 ?
                    System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Right : 0);
            VM.KampfWindow.Left = maxRight - Math.Round(VM.IniWidthStart * vm.ScaleKampfGrid);

            VM.IsShowIniKampf = true;
            if (Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive &&
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Left >= System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
            {
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowState = WindowState.Normal;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowStyle = WindowStyle.None;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width = System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - Math.Round(VM.IniWidthStart * vm.ScaleKampfGrid);
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Height = System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Height;
            }
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

        private void MeisterArenaZoom_Click(object sender, RoutedEventArgs e)
        {
            if (Global.ContextHeld.HeldenGruppeListe.Count > 0)
            {
                ArenaScrollViewer.Zoom = 1;
                var xMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureX) - Global.ContextHeld.HeldenGruppeListe[0].CreatureWidth;
                var yMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureY) - Global.ContextHeld.HeldenGruppeListe[0].CreatureHeight;
                ArenaScrollViewer.TranslateX = -xMin;
                ArenaScrollViewer.TranslateY = -yMin;
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

        private void tbtnSpielerIniScreen_Click(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)e.OriginalSource).IsChecked == true)
            {
                CreateKampfWindow();
            }
            else
            {
                if (VM.KampfWindow != null)
                {
                    VM.KampfWindow.Close();
                }

                if (VM.SpielerScreenWindow != null)
                {
                    VM.SpielerScreenWindow.Topmost = false;
                }

                if (Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive &&
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Left >= System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
                {
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowState = System.Windows.WindowState.Maximized;
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowStyle = System.Windows.WindowStyle.None;
                }
            }
        }

        private void Thumb_Drag(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            if (thumb == null)
            {
                return;
            }

            var pathline = thumb.DataContext as PathLine;
            if (pathline == null)
            {
                return;
            }
        }

        private void ToggleFilledLinePathButton()
        {
            FilledPathLineButton.IsChecked = (!Convert.ToBoolean(FilledPathLineButton.IsChecked));
        }

        private void ToggleLinePathButton()
        {
            PathLineButton.IsChecked = (!Convert.ToBoolean(PathLineButton.IsChecked));
        }

        private void View_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM.KampfWindow != null)
            {
                VM.KampfWindow.Close();
            }

            VM.Dispose();
        }

        public class MyTimer
        {
            private static int _start = 0;
            private static int _stop = 0;

            public static void start_timer()
            {
                _start = Environment.TickCount;
            }

            public static void stop_timer()
            {
                stop_timer("");
            }

            public static void stop_timer(string msg)
            {
                _stop = Environment.TickCount;
                print(msg);
            }

            private static void print(string msg)
            {
                var output = "MyTimer(" + msg + "): " + (_stop - _start) + " Millisekunden";
                System.Diagnostics.Debug.WriteLine(output);
            }
        }

        #region --- Tastatur abfragen ---

        public static RoutedCommand ThemeCommandCheck = new RoutedCommand();
        private Cursor _kämpferCursor = null;
        private Point? _kämpferPoint = null;
        private bool _mouseClickedOnCreature = false;
        private MenuItem _openMenuItem = null;

        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private void ArenaGrid_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            try
            {
                if (e.Effects == DragDropEffects.Copy || e.Effects == DragDropEffects.Move)
                {
                    if (_kämpferCursor == null && _kämpferPoint != null)
                    {
                        Point curMousePosScreen = GetMousePosition();

                        var factorX = (VM.CurrentMousePositionX - (VM.SelectedObject as BattlegroundCreature).CreatureX) / (VM.SelectedObject as BattlegroundCreature).CreatureWidth;
                        var minusX = (int)((VM.SelectedObject as BattlegroundCreature).CreatureWidth * (factorX * ArenaScrollViewer.Zoom));

                        var factorY = (VM.CurrentMousePositionY - (VM.SelectedObject as BattlegroundCreature).CreatureY) / (VM.SelectedObject as BattlegroundCreature).CreatureHeight;
                        var minusY = (int)((VM.SelectedObject as BattlegroundCreature).CreatureHeight * (factorY * ArenaScrollViewer.Zoom));

                        SetCursorPos((int)curMousePosScreen.X - minusX, (int)curMousePosScreen.Y - minusY);

                        VM.SelectedObject.MoveObject(VM.CurrentMousePositionX, VM.CurrentMousePositionY, true);
                        _xMovingOld = VM.CurrentMousePositionX;
                        _yMovingOld = VM.CurrentMousePositionY;

                        //Get DPI Scaling from MainProgramm
                        Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
                        var dx = m.M11;
                        var dy = m.M22;

                        var img = new Image
                        {
                            Width = (VM.SelectedObject as BattlegroundCreature).CreatureWidth * ArenaScrollViewer.Zoom * dx,
                            Height = (VM.SelectedObject as BattlegroundCreature).CreatureHeight * ArenaScrollViewer.Zoom * dy,
                            Stretch = Stretch.Fill
                        };
                        try
                        {
                            var pic = (ArenaGrid.SelectedItem as IKämpfer).Bild ?? "pack://application:,,," + "/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png";

                            if (!pic.StartsWith("/") && !File.Exists(pic))
                            {
                                pic = "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png";
                            }
                            img.Source = new BitmapImage(new Uri(pic.StartsWith("/") ? "pack://application:,,," + pic : pic));
                            _kämpferCursor = CreateCursor(img, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                        }
                        catch (Exception ex)
                        {
                            ViewHelper.ShowError("IMAGE Fehler" + Environment.NewLine + "Beim Creieren des Icons von " + (VM.SelectedObject as BattlegroundCreature).ki.Kämpfer.Name +
                                " ist ein Fehler aufgetreten." + Environment.NewLine + "pic: " + (ArenaGrid.SelectedItem as IKämpfer).Bild, ex);
                            img.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png", UriKind.Absolute));
                            _kämpferCursor = CreateCursor(img, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                        }
                    }
                    if (_kämpferCursor != null)
                    {
                        e.UseDefaultCursors = false;
                        Mouse.SetCursor(_kämpferCursor);
                        VM.KämpferDnDTempPos = new Point(VM.CurrentMousePositionX, VM.CurrentMousePositionY);

                        //Maßstab Endposition setzen
                        VM.MoveWhileDrawing(
                            (VM.SelectedObject as BattlegroundCreature).MidCreatureX + (VM.SelectedObject as BattlegroundCreature).CreatureWidth / 2,
                            (VM.SelectedObject as BattlegroundCreature).MidCreatureY + (VM.SelectedObject as BattlegroundCreature).CreatureHeight / 2, false);

                        VM.MoveObject(_xMovingOld, _yMovingOld, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                        _xMovingOld = VM.CurrentMousePositionX;
                        _yMovingOld = VM.CurrentMousePositionY;
                    }
                }
                else
                {
                    e.UseDefaultCursors = true;
                }

                e.Handled = true;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Feedback Fehler" + Environment.NewLine + "Beim Starten des Drag'n'Drop-Vorgangs ist ein Fehler aufgetreten.", ex);
                e.UseDefaultCursors = true;
            }
        }

        private void ArenaGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (VM.FogFreimachen && Keyboard.IsKeyDown(Key.LeftCtrl) && VM.FogPixelData != null)
                {
                    VM.SelectedObject = null;
                    WriteableBitmap wbmap = VM.FogImage;
                    var newX = (int)VM.CurrentMousePositionX / 10;
                    var newY = (int)VM.CurrentMousePositionY / 10;

                    var w = 10 * VM.FogFreeSize + 1;
                    var h = (10 * VM.FogFreeSize + 1) * 10 - 1;
                    var leererBereich = new int[w * h];

                    for (var i = 0; i < 100 * VM.FogFreeSize + 1; i++)
                    {
                        Array.ConstrainedCopy(leererBereich, 0, VM.FogPixelData, i * 1000, w * h);
                    }

                    var ber = 1000;
                    wbmap.WritePixels(new Int32Rect(newX, newY,
                        newX + 10 * VM.FogFreeSize + 1 > 1000 ? 1000 - newX : 10 * VM.FogFreeSize + 1,
                        newY + 10 * VM.FogFreeSize + 1 > 1000 ? 1000 - newY : 10 * VM.FogFreeSize + 1),
                        VM.FogPixelData, ber, 0); // widthInByte

                    VM.FogImage = wbmap;
                }
                else
                {
                    if (VM.SelectedObject != null)
                    {
                        VM.SelectionChangedUpdateSliders();
                    }

                    if ((VM.SelectedObject != null && VM.SelectedObject.IsMoving) || VM.CreateLine || VM.CreateFilledLine)
                    {
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        VM.CreateNewTempPathLine(_x1, _y1);
                        VM.CreateNewTempTextLabel(_x1, _y1);
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
                _mouseClickedOnCreature = false;

                VM.IsMoving = false;
                if (VM.SelectedObject != null)
                {
                    VM.SelectedObject.IsMoving = false;
                }
                //handling different possibilities based on Objects (like Pathline or different BattlegroundBaseObject)
                if (VM.CreatingNewLine || VM.CreatingNewFilledLine)
                {
                    VM.FinishCurrentPathLine();
                    e.Handled = true;
                    VM.CreatingNewLine = false;
                    VM.CreatingNewFilledLine = false;
                    VM.MoveLastObjectBehindCreatures();
                }
                else if (VM.SelectedObject != null)
                {
                    if (VM.BattlegroundObjects.Where(x => x is Wesen && x.IsSticked).Any())
                    {
                        BattlegroundBaseObject currentcreature = VM.BattlegroundObjects.Where(x => x is Wesen && x.IsSticked).First();
                        currentcreature.IsSticked = false;

                        if (VM.BattlegroundObjects.Where(x => x is Model.Held && x.IsSticked).Any())
                        {
                            VM.CurrentlySelectedCreature = ((Model.Held)VM.BattlegroundObjects.Where(x => x is Model.Held && x.IsSticked).First()).Name;
                        }
                        else if (VM.BattlegroundObjects.Where(x => x is Model.Gegner && x.IsSticked).Any())
                        {
                            VM.CurrentlySelectedCreature = ((Model.Gegner)VM.BattlegroundObjects.Where(x => x is Model.Gegner && x.IsSticked).First()).Name;
                        }
                        else
                        {
                            VM.CurrentlySelectedCreature = "";
                        }
                    }
                    else if (VM.SelectedObject is Wesen)
                    {
                        var deltaX = VM.CurrentMousePositionX - ((Wesen)VM.SelectedObject).CreatureNameX;
                        var deltaY = VM.CurrentMousePositionY - ((Wesen)VM.SelectedObject).CreatureNameY;
                        deltaX = deltaX < 0 ? deltaX * -1 : deltaX;
                        deltaY = deltaY < 0 ? deltaY * -1 : deltaY;
                        if (deltaX <= 50 && deltaY <= 50)
                        {
                            if (((Wesen)VM.SelectedObject).Position == Position.Stehend)
                            {
                                ((Wesen)VM.SelectedObject).Position = Position.Kniend;
                            }
                            else if (((Wesen)VM.SelectedObject).Position == Position.Kniend)
                            {
                                ((Wesen)VM.SelectedObject).Position = Position.Liegend;
                            }
                            else if (((Wesen)VM.SelectedObject).Position == Position.Liegend)
                            {
                                ((Wesen)VM.SelectedObject).Position = Position.Reitend;
                            }
                            else if (((Wesen)VM.SelectedObject).Position == Position.Reitend)
                            {
                                ((Wesen)VM.SelectedObject).Position = Position.Fliegend;
                            }
                            else if (((Wesen)VM.SelectedObject).Position == Position.Fliegend)
                            {
                                ((Wesen)VM.SelectedObject).Position = Position.Schwebend;
                            }
                            else
                            {
                                ((Wesen)VM.SelectedObject).Position = Position.Stehend;
                            }
                        }
                        VM.UpdateCreaturesFromChangedKampferlist();

                        VM.FinishCurrentTempPathLine();
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Loslassen der linken Maustaste", ex); }
        }

        private void ArenaGrid_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                VM.FogFreimachen = (VM.useFog && Keyboard.IsKeyDown(Key.LeftCtrl));

                if (VM.FogFreimachen && VM.FogPixelData != null)
                {
                    VM.SelectedObject = null;
                    VM.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
                    VM.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
                    if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                    {
                        WriteableBitmap wbmap = VM.FogImage;
                        var newX = (int)VM.CurrentMousePositionX / 10;
                        var newY = (int)VM.CurrentMousePositionY / 10;
                        var w = 10 * VM.FogFreeSize + 1;
                        var h = (10 * VM.FogFreeSize + 1) * 10;
                        var leererBereich = new int[w * h];
                        if (e.RightButton == MouseButtonState.Pressed)
                        {
                            leererBereich = Enumerable.Repeat(-0x01000000, w * h).ToArray();
                        }

                        for (var i = 0; i < 100 * VM.FogFreeSize + 1; i++)
                        {
                            Array.ConstrainedCopy(leererBereich, 0, VM.FogPixelData, i * 1000, w * h);
                        }

                        var ber = 1000;
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
                    var tempstick = VM.BattlegroundObjects.Where(x => x is Wesen && x.IsSticked).ToList();
                    if (tempstick.Any())
                    {
                        foreach (BattlegroundBaseObject wesen in tempstick)
                        {
                            ((BattlegroundCreature)wesen).MoveObject(VM.CurrentMousePositionX, VM.CurrentMousePositionY, true);
                        }
                    }
                    else
                        if (VM.CreatingNewLine || VM.CreatingNewFilledLine)
                    {
                        VM.MoveWhileDrawing(VM.CurrentMousePositionX, VM.CurrentMousePositionY, VM.Freizeichnen);
                    }
                    else if (_mouseClickedOnCreature && e.LeftButton == MouseButtonState.Pressed && VM.SelectedObject != null)
                    {
                        VM.IsMoving = true;
                        VM.SelectedObject.IsMoving = true;
                        if (VM.SelectedObject is BattlegroundCreature)
                        {
                            VM.InitDnD = true;
                        }
                        else
                        {
                            VM.MoveObject(_xMovingOld, _yMovingOld, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                        }
                    }
                    if (VM.InitDnD)
                    {
                        var x = (VM.SelectedObject as BattlegroundCreature).MidCreatureX + (VM.SelectedObject as BattlegroundCreature).CreatureWidth / 2;
                        var y = (VM.SelectedObject as BattlegroundCreature).MidCreatureY + (VM.SelectedObject as BattlegroundCreature).CreatureHeight / 2;
                        VM.CreateNewTempPathLine(x, y);
                        VM.CreateNewTempTextLabel(x, y);

                        e.Handled = true;

                        _kämpferPoint = e.GetPosition(null);
                        VM.InitDnD = false;

                        // Initialisiere drag & drop Operation
                        var dragData = new DataObject("BMKämpfer", VM.SelectedObject);
                        DragDrop.DoDragDrop(ArenaGrid, dragData, DragDropEffects.Move);
                        _kämpferPoint = null;
                    }

                    _xMovingOld = VM.CurrentMousePositionX;
                    _yMovingOld = VM.CurrentMousePositionY;

                    if (_mouseClickedOnCreature && e.LeftButton == MouseButtonState.Released && VM.SelectedObject != null)
                    {
                        _mouseClickedOnCreature = false;
                        VM.FinishCurrentTempPathLine();
                    }
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Bewegen der Maus", ex); }
        }

        private void ArenaGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _mouseClickedOnCreature = false;
                //STRG - Abfragen um return zu setzen
                if (Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    return;
                }

                if (VM != null)
                {
                    if (VM.IsPointerVisible)
                    {
                        VM.SetPointer(ArenaGridTop);
                        e.Handled = true;
                        return;
                    }
                    MenuItem menuitem = ((DependencyObject)e.OriginalSource).FindAnchestor<MenuItem>();
                    if (menuitem != null)
                    {
                        if (menuitem.HasItems)
                        {
                            menuitem.IsSubmenuOpen = !menuitem.IsSubmenuOpen;
                            _openMenuItem = menuitem.IsSubmenuOpen ? menuitem : null;
                        }
                        else
                        {
                            ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe
                                .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde)
                                .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == VM.SelectedObject as IKämpfer);
                            if (mi != null)
                            {
                                if (menuitem.Name == "miKämpferZauber" || (_openMenuItem != null && _openMenuItem.Name == "miKämpferZauber"))
                                {
                                    mi.UmwandelnZauber.Execute(menuitem.CommandParameter);
                                }

                                if (menuitem.Name == "miKämpferFernkampf" || (_openMenuItem != null && _openMenuItem.Name == "miKämpferFernkampf"))
                                {
                                    mi.UmwandelnFernkampf.Execute(menuitem.CommandParameter);
                                }

                                if (menuitem.Name == "miKämpferAttacke" || (_openMenuItem != null && _openMenuItem.Name == "miKämpferAttacke"))
                                {
                                    mi.UmwandelnAttacke.Execute(menuitem.CommandParameter);
                                }

                                if (menuitem.Name == "miKämpferSonstiges" || (_openMenuItem != null && _openMenuItem.Name == "miKämpferSonstiges"))
                                {
                                    mi.UmwandelnSonstiges.Execute(menuitem.CommandParameter);
                                }
                            }
                            if (_openMenuItem != null && _openMenuItem.IsSubmenuOpen)
                            {
                                _openMenuItem.IsSubmenuOpen = false;
                            }

                            _openMenuItem = null;
                            Global.CurrentKampf.SelectedManöver = mi;
                            Global.CurrentKampf.Kampf.SelectedManöverInfo = mi;
                        }
                        return;
                    }

                    if (_openMenuItem != null && _openMenuItem.IsSubmenuOpen)
                    {
                        _openMenuItem.IsSubmenuOpen = false;
                    }

                    _openMenuItem = null;

                    Slider slider = ((DependencyObject)e.OriginalSource).FindAnchestor<Slider>();
                    if (slider != null)
                    {
                        return;
                    }

                    ListBoxItem listboxItem = ((DependencyObject)e.OriginalSource).FindAnchestor<ListBoxItem>();
                    if (listboxItem != null)
                    {
                        listboxItem.Refresh();
                        if (VM.SelectedObject != null)
                        {
                            VM.SelectedObject.IsMoving = false;
                        }

                        var o = ArenaGrid.ItemContainerGenerator.ItemFromContainer(listboxItem) as BattlegroundBaseObject;
                        VM.SelectedObject = o;
                        _mouseClickedOnCreature = true;
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fehler beim Pre-Loslassen der Linken Maustaste", ex);
                _mouseClickedOnCreature = true;
                e.Handled = true;
            }
        }

        private void ArenaGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                VM.IsMoving = false;
                if (VM.SelectedObject != null)
                {
                    VM.SelectedObject.IsMoving = false;
                }

                _mouseClickedOnCreature = false;
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
                        var newX = (int)vm.CurrentMousePositionX / 10;
                        var newY = (int)vm.CurrentMousePositionY / 10;

                        var w = 10 * vm.FogFreeSize + 1;
                        var h = (10 * vm.FogFreeSize + 1) * 10 - 1;
                        var leererBereich = Enumerable.Repeat(-0x01000000, w * h).ToArray();

                        for (var i = 0; i < 100 * vm.FogFreeSize + 1; i++)
                        {
                            Array.ConstrainedCopy(leererBereich, 0, vm.FogPixelData, i * 1000, w * h);
                        }

                        var ber = 1000;
                        wbmap.WritePixels(new Int32Rect(newX, newY,
                            newX + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newX : 10 * vm.FogFreeSize + 1,
                            newY + 10 * vm.FogFreeSize + 1 > 1000 ? 1000 - newY : 10 * vm.FogFreeSize + 1),
                            VM.FogPixelData, ber, 0); // widthInByte

                        VM.FogImage = wbmap;
                        return;
                    }

                    if (vm.SelectedObject == null)
                    {
                        return;
                    }

                    if (vm.SelectedObject is Wesen)
                    {
                        ((BattlegroundCreature)vm.SelectedObject).CalculateNewSightLineSektor(
                            new Point(e.GetPosition(ArenaGrid).X,
                            e.GetPosition(ArenaGrid).Y), VM.RechteckGrid);
                    }
                    else if (vm.SelectedObject is ImageObject)
                    {
                        ((ImageObject)vm.SelectedObject).CalculateNewDirection(
                            new Point(e.GetPosition(ArenaGrid).X,
                            e.GetPosition(ArenaGrid).Y));
                    }
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Pre-Loslassen der Rechten Maustaste", ex); }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int X;
            public int Y;
        };

        #region -- CURSOR DRAG & DROP --

        public static Cursor CreateCursor(UIElement element, double dX, double dY)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(new Point(), element.DesiredSize));

            var rtb =
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
                    return InternalCreateCursor(bmp);
                }
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

        private void ArenaGrid_DragOver(object sender, DragEventArgs e)
        {
            VM.CurrentMousePositionX = e.GetPosition(ArenaGrid).X;
            VM.CurrentMousePositionY = e.GetPosition(ArenaGrid).Y;
        }

        private void ArenaGrid_Drop(object sender, DragEventArgs e)
        {
            VM.SelectedObject.IsMoving = false;
            VM.IsMoving = false;
            ((BattlegroundCreature)VM.SelectedObject).CalculateSightArea();
            _kämpferCursor = null;
        }

        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

            [DllImport("user32.dll")]
            public static extern bool DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

            public struct IconInfo
            {
                public bool fIcon;
                public IntPtr hbmColor;
                public IntPtr hbmMask;
                public int xHotspot;
                public int yHotspot;
            }
        }

        private class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeIconHandle()
                : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                return NativeMethods.DestroyIcon(handle);
            }
        }

        #endregion -- CURSOR DRAG & DROP --

        #endregion --- Tastatur abfragen ---
    }
}
