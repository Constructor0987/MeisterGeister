using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.Model;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.ViewModel.Kampf.Logic.Manöver;
using MeisterGeister.ViewModel.SpielerScreen;
using Microsoft.Win32.SafeHandles;

namespace MeisterGeister.View.Bodenplan
{
    /// <summary>
    /// Interaction logic for BattlegroundView.xaml
    /// </summary>
    public partial class BattlegroundView : UserControl
    {
        public double _xMovingOld, _yMovingOld;

        public BattlegroundView()
        {
            InitializeComponent();
            Global.CurrentKampf.BodenplanView = this;
            VM = new BattlegroundViewModel();
            
            AddPictureButtons();
            AddFogOfWar();

        //    VM.BackgroundColor = Color.FromArgb(255, 55, 88, 55);
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

        private double _visualisationHeight = 100;

        private double _visualisationWidth = 100;

        private double _x1, _y1;

        private bool _zoomChanged = false;

        private bool MouseIsOverScrViewer = false;

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
                    var b = new Button()
                    {
                        Name = _buttonPrefix + i,
                        ToolTip = picurls[i],
                        Width = 140,
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Content = System.IO.Path.GetFileNameWithoutExtension(appPath + picurls[i]),
                        Tag = Ressources.GetFullApplicationPath() + picurls[i]
                    };

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
                if (VM != null)
                {
                    VM.SelectionChangedUpdateSliders();
                }
            }
            catch
            { }
        }

        private void ArenaScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (VM != null)
            {
                if (_zoomChanged)
                {
                    _zoomChanged = false;
                    VM.CurrentMousePositionX = Mouse.GetPosition(ArenaGrid).X;
                    VM.CurrentMousePositionY = Mouse.GetPosition(ArenaGrid).Y;
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
                if (VM != null)
                {
                    VM.LoadBattlegroundFromXML(dlg.FileName);
                }
            }
            AddPictureButtons(); //reload new pictures
            VM.SelectedObject = null;
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.ClearBattleground();
            }
        }

        private void Button_SaveXML_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Battleground_" + DateTime.Now.ToShortDateString(), // Default file name
                DefaultExt = ".xml",
                Filter = "XML Files (.xml)|*.xml"
            };
            var result = dlg.ShowDialog();

            if (result == true)
            {
                if (VM != null)
                {
                    VM.SaveBattlegroundToXML(dlg.FileName);
                }
            }
        }

        private void ButtonBildhinzun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dir = "Daten\\Bodenplan";
                var allowedExtensions = new HashSet<string>(Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES, StringComparer.OrdinalIgnoreCase);
                var aktDir = Directory.GetCurrentDirectory();
                var exeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                var dateien = new List<string>();
                dateien = ViewHelper.ChooseFiles("Bilder hinzufügen", "", true, new string[9] { "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff" });
                if (dateien == null)
                {
                    return;
                }

                foreach (var datei in dateien)
                {
                    var s = exeDirectory + "\\" + dir + "\\" + System.IO.Path.GetFileName(datei);
                    if (File.Exists(s))
                    {
                        File.Delete(s);
                    }
                    File.Copy(datei, s);
                }
                Directory.SetCurrentDirectory(aktDir);
                AddPictureButtons();
                VM.LoadImagesFromDir(String.IsNullOrEmpty(Ressources.GetFullApplicationPath()) ? "Daten\\Bodenplan" : Ressources.GetFullApplicationPath() + "Daten\\Bodenplan");
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Hinzufügen eines neuen Bildes", ex); }
        }

        private void ButtonEbeneHigher_Click(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.ChangeEbeneHeight(true);
            }
        }

        private void ButtonEbeneHigherMax_Click(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.MoveSelectedObjectToTop(true);
            }
        }

        private void ButtonEbeneLower_Click(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.ChangeEbeneHeight(false);
            }
        }

        private void ButtonEbeneLowerMin_Click(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.MoveSelectedObjectToTop(false);
            }
        }

        private void ButtonSticCreatues_Click(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.StickEnemies();
            }
        }

        private void ButtonStickHeroes_Click(object sender, RoutedEventArgs e)
        {
            if (VM != null && !VM.BattlegroundObjects.Where(x => x is Model.Gegner && x.IsSticked).Any())
            {
                VM.StickHeroes();
            }
        }

        private void CreateKampfWindow()
        {
            var infoView = new Kampf.KampfInfoView(Global.CurrentKampf);

            infoView.grdMain.LayoutTransform = new ScaleTransform(VM.ScaleKampfGrid, VM.ScaleKampfGrid);
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

                    if (VM.IniWidthStart != Math.Round(VM.KampfWindow.Width / VM.ScaleKampfGrid))
                    {
                        VM.IniWidthStart = Math.Round(VM.KampfWindow.Width / VM.ScaleKampfGrid);
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
            VM.KampfWindow.Width = Math.Round(VM.IniWidthStart * VM.ScaleKampfGrid);
            VM.KampfWindow.Top = 0;

            var maxRight = Math.Max(
                System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Right,
                System.Windows.Forms.Screen.AllScreens.Length > 1 ?
                    System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Right : 0);
            VM.KampfWindow.Left = maxRight - Math.Round(VM.IniWidthStart * VM.ScaleKampfGrid);

            VM.IsShowIniKampf = true;
            if (Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive &&
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Left >= System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
            {
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowState = WindowState.Normal;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowStyle = WindowStyle.None;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width = System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - Math.Round(VM.IniWidthStart * VM.ScaleKampfGrid);
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
            if (Global.ContextHeld.HeldenGruppeListe.Any())
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
            if (VM != null && VM.KampfWindow != null)
            {
                ((Kampf.KampfInfoView)VM.KampfWindow.Content).grdMain.LayoutTransform = new ScaleTransform(VM.ScaleKampfGrid, VM.ScaleKampfGrid);
                VM.KampfWindow.SizeToContent = SizeToContent.Height;
                //SizeToContent muss wieder auf Manual gesetzt werden da das Window sonst immer größer wird
                VM.KampfWindow.SizeToContent = SizeToContent.Manual;
                VM.KampfWindow.Width = Math.Round(VM.IniWidthStart * VM.ScaleKampfGrid);
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
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowState = WindowState.Maximized;
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.WindowStyle = WindowStyle.None;
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

        private void View_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM != null && VM.KampfWindow != null)
            {
                VM.KampfWindow.Close();
            }
            if (VM != null && VM.IsShowIniKampf)
            {
                VM.KampfWindow.Tag = true;
                VM.KampfWindow.Close();
            }
            VM.Dispose();
        }

        #region --- Tastatur abfragen ---

        public static RoutedCommand ThemeCommandCheck = new RoutedCommand();

        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int X;
            public int Y;
        };

        private Cursor _kämpferCursor = null;
        private Point? _kämpferPoint = null;
        private bool _mouseClickedOnCreature = false;
        private MenuItem _openMenuItem = null;

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
                        //TODO: Muss noch angepasst werden wegen (VM.SelectedObject as BattlegroundCreature).TokenOversizeMod 

                        Point curMousePosScreen = GetMousePosition();
                        var factorX = (VM.CurrentMousePositionX - (VM.SelectedObject as BattlegroundCreature).CreatureXPic) / 
                            ((VM.SelectedObject as BattlegroundCreature).CreatureWidthPic* (VM.SelectedObject as BattlegroundCreature).TokenOversizeMod);
                        var minusX = (int)((VM.SelectedObject as BattlegroundCreature).CreatureWidthPic * (factorX * ArenaScrollViewer.Zoom));

                        var factorY = (VM.CurrentMousePositionY - (VM.SelectedObject as BattlegroundCreature).CreatureYPic) / 
                            ((VM.SelectedObject as BattlegroundCreature).CreatureHeightPic * (VM.SelectedObject as BattlegroundCreature).TokenOversizeMod);
                        var minusY = (int)((VM.SelectedObject as BattlegroundCreature).CreatureHeightPic * (factorY * ArenaScrollViewer.Zoom));
                        
                        SetCursorPos((int)curMousePosScreen.X - minusX, (int)curMousePosScreen.Y - minusY);
                        
                        VM.MoveObject(VM.SelectedObject.ZDisplayX, VM.SelectedObject.ZDisplayY, VM.CurrentMousePositionX, VM.CurrentMousePositionY);
                        _xMovingOld = VM.CurrentMousePositionX;
                        _yMovingOld = VM.CurrentMousePositionY;
                        VM.SelectedObject.MoveObject(VM.CurrentMousePositionX, VM.CurrentMousePositionY, true);

                        //Get DPI Scaling from MainProgramm
                        Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
                        var dx = m.M11;
                        var dy = m.M22;

                        var img = new Image
                        {
                            Width = (VM.SelectedObject as BattlegroundCreature).CreatureWidthPic * ArenaScrollViewer.Zoom * dx,
                            Height = (VM.SelectedObject as BattlegroundCreature).CreatureHeightPic * ArenaScrollViewer.Zoom * dy,
                            Stretch = Stretch.Fill
                        };
                        try
                        {
                            RotateTransform aRotateTransform = new RotateTransform();
                            aRotateTransform.CenterX = (VM.SelectedObject as BattlegroundCreature).RotateImageCenterXY;
                            aRotateTransform.CenterY = (VM.SelectedObject as BattlegroundCreature).RotateImageCenterXY;
                            aRotateTransform.Angle = (VM.SelectedObject as BattlegroundCreature).RotateImageDegrees;
                            img.RenderTransform = aRotateTransform;

                            if ((VM.SelectedObject as BattlegroundCreature).ki != null &&
                                (VM.SelectedObject as BattlegroundCreature).ki.Kämpfer.Bild != (VM.SelectedObject as BattlegroundCreature).CreatureImage.Tag?.ToString())
                                (VM.SelectedObject as BattlegroundCreature).CreatureImage = (VM.SelectedObject as BattlegroundCreature).SetCreatrueImage(img);
                            img.Source = (VM.SelectedObject as BattlegroundCreature).CreatureImage.Source;

                            _kämpferCursor = CreateCursor(img);
                        }
                        catch (Exception ex)
                        {
                            ViewHelper.ShowError("IMAGE Fehler" + Environment.NewLine + "Beim Creieren des Icons von " + (VM.SelectedObject as BattlegroundCreature).ki.Kämpfer.Name +
                                " ist ein Fehler aufgetreten." + Environment.NewLine + "pic: " + (ArenaGrid.SelectedItem as IKämpfer).Bild, ex);
                            img.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png", UriKind.Absolute));
                            _kämpferCursor = CreateCursor(img);
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
                if (VM.FogFreimachen && Keyboard.Modifiers == ModifierKeys.Control && VM.FogPixelData != null)
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

                    if (VM.SelectedObject != null && VM.SelectedObject.IsMoving && !(Keyboard.Modifiers == ModifierKeys.Control))
                    {
                        VM.BewegungZuvor = 0;
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        VM.CreateNewTempPathLine(_x1, _y1);
                        VM.CreateNewTempTextLabel(_x1, _y1);
                        e.Handled = true;
                    }
                    else if (VM.CreateLine)
                    {
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        VM.CreatingNewLine = true;
                        VM.CreateNewPathLine(_x1, _y1);
                        Global.CurrentKampf.LabelInfo =
                            "Shift - Taste drücken oder gedrückt halten für angrenzende Linie";
                        e.Handled = true;

                    }
                    else if (VM.CreateFilledLine)
                    {
                        _x1 = e.GetPosition(ArenaGrid).X;
                        _y1 = e.GetPosition(ArenaGrid).Y;
                        VM.CreatingNewFilledLine = true;
                        VM.CreateNewFilledLine(_x1, _y1);
                        Global.CurrentKampf.LabelInfo =
                            "Shift - Taste drücken oder gedrückt halten für Flächenzeichnung";
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
                    Global.CurrentKampf.LabelInfo = null;
                }
                else if (VM.SelectedObject != null)
                {
                    if (VM.BattlegroundObjects.Where(x => x is Wesen && x.IsSticked).Any())
                    {
                        BattlegroundBaseObject currentCreature = VM.BattlegroundObjects.Where(x => x is Wesen && x.IsSticked).First();
                        currentCreature.IsSticked = false;

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
                if (VM.LinealAktiv && !VM.InitLineal)
                {
                    VM.AlterPathLine(e.GetPosition(ArenaGrid).X, e.GetPosition(ArenaGrid).Y);
                    return;
                }

                if (VM.SpielerKastenAktiv && !VM.InitSpielerKasten)
                {
                    VM.AlterRectangle(e.GetPosition(ArenaGrid).X, e.GetPosition(ArenaGrid).Y);
                    return;
                }

                VM.FogFreimachen = (VM.useFog && Keyboard.Modifiers == ModifierKeys.Control);

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
                        VM.BewegungZuvor = 0;
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
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    return;
                }

                if (VM != null)
                {
                    if (VM.SpielerKastenAktiv)
                    {
                        if (VM.InitSpielerKasten)
                        {
                            //Kasten zeichnen Beginn
                            VM.BewegungZuvor = 0;
                            var x = VM.CurrentMousePositionX;
                            var y = VM.CurrentMousePositionY;
                            VM.CreateNewTempRectangle(x, y);

                            VM.InitSpielerKasten = false;
                            e.Handled = true;
                        }
                        else
                        {
                            //Kasten zeichnen Beginn beenden
                            if (VM.SelectedTempObject != null)
                            {
                                Rect r = new Rect(
                                    (VM.SelectedTempObject as RectangleObject).RectPositionX, (VM.SelectedTempObject as RectangleObject).RectPositionY,
                                    (VM.SelectedTempObject as RectangleObject).RectWidth, (VM.SelectedTempObject as RectangleObject).RectHeight);
                                VM.InitSpielerKasten = true;
                                VM.SetSpielerZoom(r);
                            }
                            VM.FinishCurrentTempRectangle();
                            VM.SpielerKastenAktiv = false;
                            e.Handled = true;
                        }
                        return;
                    }
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
                                .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde &&
                                            z.Manöver.Ausführender.Kämpfer == VM.SelectedObject as IKämpfer)
                                .FirstOrDefault(t => t.Aktionszeiten.Contains(Global.CurrentKampf.Kampf.AktuelleAktionszeit));
                            if (mi == null)
                                mi = Global.CurrentKampf.Kampf.InitiativListe
                                .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde)
                                .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == VM.SelectedObject as IKämpfer);

                            if (mi != null)
                            {
                                VM.miWaffeSelected = menuitem.CommandParameter as IWaffe;
                                if (menuitem.Name == "miKämpferZauber" || (_openMenuItem != null && _openMenuItem.Name == "miKämpferZauber"))
                                {
                                    mi.UmwandelnZauber.Execute(menuitem.CommandParameter);
                                }

                                if (menuitem.Name == "miKämpferFernkampf" || (_openMenuItem != null && _openMenuItem.Name == "miKämpferFernkampf"))
                                {

                                    if (VM.miWaffeSelected == null)
                                        if (((ListBox)sender).SelectedItem as Held != null)
                                            VM.miWaffeSelected = (((ListBox)sender).SelectedItem as Held).Fernkampfwaffen.FirstOrDefault();
                                        else
                                        {
                                            if (Global.CurrentKampf.SelectedKämpfer != null)
                                                VM.miWaffeSelected = Global.CurrentKampf.SelectedKämpfer.Kämpfer.Fernkampfwaffen.FirstOrDefault();
                                            else if (menuitem.DataContext.GetType() == typeof(Gegner))
                                                VM.miWaffeSelected = ((Gegner)menuitem.DataContext).Fernkampfwaffen.FirstOrDefault();
                                        }

                                    mi.UmwandelnFernkampf.Execute(VM.miWaffeSelected);
                                }

                                if (menuitem.Name == "miKämpferAttacke" || (_openMenuItem != null && _openMenuItem.Name == "miKämpferAttacke"))
                                {
                                    if (VM.miWaffeSelected == null)
                                        VM.miWaffeSelected =(((ListBox)sender).SelectedItem is Held)?                                            
                                            (((ListBox)sender).SelectedItem as Held).Nahkampfwaffen.FirstOrDefault():                              
                                            null;
                                    mi.UmwandelnAttacke.Execute(VM.miWaffeSelected);
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

                        //Duplicator geklickjt ?


                        ImageDuplicator id = ((DependencyObject)e.OriginalSource).FindAnchestor<ImageDuplicator>();

                        if (id != null)
                        {

                            e.Handled = true;
                            return;
                        }

                        //else
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
                if (VM != null)
                {                    
                    if (VM.FogFreimachen && Keyboard.Modifiers == ModifierKeys.Control && VM.FogPixelData != null)
                    {
                        WriteableBitmap wbmap = VM.FogImage;
                        var newX = (int)VM.CurrentMousePositionX / 10;
                        var newY = (int)VM.CurrentMousePositionY / 10;

                        var w = 10 * VM.FogFreeSize + 1;
                        var h = (10 * VM.FogFreeSize + 1) * 10 - 1;
                        var leererBereich = Enumerable.Repeat(-0x01000000, w * h).ToArray();

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
                        return;
                    }

                    if (VM.LinealAktiv)
                    {
                        if (VM.InitLineal)
                        {
                            VM.BewegungZuvor = 0;
                            var x = VM.CurrentMousePositionX;
                            var y = VM.CurrentMousePositionY;
                            VM.CreateNewTempLinealLine(x, y);
                            VM.CreateNewTempLinealLabel(x, y);

                            VM.InitLineal = false;
                            e.Handled = true;
                        }
                        else
                        {
                            VM.InitLineal = true;
                            e.Handled = true;
                        }
                        return;
                    }
                    else if (VM.SelectedObject == null)
                    {
                        return;
                    }
                    else if (VM.SelectedObject is Wesen)
                    {
                        //Beim Öffnen eines Kontext-Menüs Sichfeld nicht ändern
                   if ((Mouse.DirectlyOver is Border && (Mouse.DirectlyOver as Border).Name == "brdCreaturePic") ||
                            (Mouse.DirectlyOver is System.Windows.Shapes.Rectangle) && 
                            ((Mouse.DirectlyOver as System.Windows.Shapes.Rectangle).DataContext is Wesen) ||
                            (Mouse.DirectlyOver is Image) && ((Mouse.DirectlyOver as Image).DataContext is Wesen))
                        { return; }
                        ((BattlegroundCreature)VM.SelectedObject).CalculateNewSightLineSektor(
                            new Point(e.GetPosition(ArenaGrid).X,
                            e.GetPosition(ArenaGrid).Y), VM.RechteckGrid);
                    }
                    else if (VM.SelectedObject is ImageObject)
                    {
                        ((ImageObject)VM.SelectedObject).CalculateNewDirection(
                            new Point(e.GetPosition(ArenaGrid).X,
                            e.GetPosition(ArenaGrid).Y));
                    }
                }
            }
            catch (Exception ex)
            { ViewHelper.ShowError("Fehler beim Pre-Loslassen der Rechten Maustaste", ex); }
        }

        #region -- CURSOR DRAG & DROP --

        public static Cursor CreateCursor(UIElement element)
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

            //Mehrfachstrecken der Kämpfer
            if (VM.SelectedObject != null && VM.SelectedObject.IsMoving && Keyboard.Modifiers == ModifierKeys.Control)
            {
                var pathLine = (PathLine)VM.SelectedTempObject;
                Point startPoint = pathLine.GetStartPoint;
                Point endPoint = pathLine.GetEndPoint;
                VM.BewegungZuvor += VM.BerechneLänge(startPoint, endPoint);

                VM.CreateNewTempPathLine(endPoint.X, endPoint.Y);
                VM.CreateNewTempTextLabel(endPoint.X, endPoint.Y);
            }
        }

        private void ArenaGrid_Drop(object sender, DragEventArgs e)
        {
            if (VM.SelectedObject != null)
            {
                VM.SelectedObject.IsMoving = false;
                if (VM.SelectedObject is BattlegroundCreature)
                {
                    BattlegroundBaseObject bgObj = VM.SelectedObject;
                    ((BattlegroundCreature)VM.SelectedObject).CalculateSightArea();
                }
            }
            VM.IsMoving = false;
            _kämpferCursor = null;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    List<string> gedroppteDateien = new List<string>();
                    foreach (var s in (string[])e.Data.GetData(DataFormats.FileDrop, false))
                    {
                        if (File.Exists(s))
                        {
                            try
                            {
                                var strlength = ("picbutton" + (VM.BattlegroundObjects.Count )).Length - 1;
                                var buttonNr = VM.BattlegroundObjects.Count;
                                ImageObject newpic = VM.CreateImageObject(Ressources.Decoder((string)s),
                                    new Point(e.GetPosition(ArenaGrid).X, e.GetPosition(ArenaGrid).Y));
                                newpic.ImagePositionX = e.GetPosition(ArenaGrid).X;
                                newpic.ImagePositionY = e.GetPosition(ArenaGrid).Y;
                                VM.MoveLastObjectBehindCreatures();
                            }
                            catch (Exception ex)
                            {
                                ViewHelper.ShowError("Fehler beim Generieren der Bilddatei." + Environment.NewLine + "Bitte überprüfen, ob die Datei existiert:" + Environment.NewLine +
                                  Ressources.Decoder(Ressources.GetFullApplicationPath() + (string)s), ex);
                            }
                        }
                    }
                }
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ablegen der Dateien in der Playlist ist ein Fehler aufgetreten.", ex);
            }
        }

        private void _listBoxDirectory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((e.Source as ListBox).SelectedItem == null) return;
            ImageItem iItem = e.AddedItems[0] as ImageItem;
            try
            {
                if (iItem != null)
                {
                    Point p = new Point(
                        VM.CurrentMousePositionX,
                        VM.CurrentMousePositionY);
                    VM.CreateImageObject(iItem.Pfad, p);
                }
                (e.Source as ListBox).SelectedItem = null;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fehler beim Generieren der Bilddatei." + Environment.NewLine + "Bitte überprüfen, ob die Datei existiert:" + Environment.NewLine +
                  Ressources.Decoder(Ressources.GetFullApplicationPath() + iItem != null? iItem.Pfad: "Unbekannter Pfad"), ex);
            }
        }

        public void VideoObject1_MediaOpened(object sender, RoutedEventArgs e)
        {
            if(VM.BackgroundMP4LoadedBehavior == MediaState.Pause)
            {
                ((MediaElement)sender).Position = new TimeSpan(0);

                VM.BackgroundMP4LoadedBehavior = MediaState.Play;
                VM.BackgroundMp4Mute = true;
                while (((MediaElement)sender).Position <= new TimeSpan(0,0,0,0,50))
                {  }
            }
            VM.BackgroundMP4LoadedBehavior = MediaState.Pause;
            
            if (VM.BackgroundMp4Length !=
                Convert.ToInt32(((MediaElement)sender).NaturalDuration != Duration.Automatic ?
                Convert.ToInt32(((MediaElement)sender).NaturalDuration.TimeSpan.TotalSeconds) :
                999))
                VM.BackgroundMp4Length =
                    Convert.ToInt32(((MediaElement)sender).NaturalDuration != Duration.Automatic?
                    Convert.ToInt32(((MediaElement)sender).NaturalDuration.TimeSpan.TotalSeconds):
                    999);
            if (!VM.IsLoading)
            {
                VM.BackgroundMp4MinPosition = 0;
                VM.BackgroundMp4MaxPosition = VM.BackgroundMp4Length;
            }
            VM.IsLoading = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        public Nullable<Position> tempP;
        private void MenuItemIstImKampf_Click(object sender, RoutedEventArgs e)
        {
            bool value = ((MenuItem)sender).IsChecked;
            if (!value)
                Global.CurrentKampf.Kampf.KämpferIListImKampf.Remove(((MenuItem)sender).Tag as KämpferInfo);
            else
                Global.CurrentKampf.Kampf.KämpferIListImKampf.Add(((MenuItem)sender).Tag as KämpferInfo);
        }

        private void sldAkt_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VM.UpdateIniKampfView();
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
                public int xHotspot;
                public int yHotspot;
                public IntPtr hbmMask;
                public IntPtr hbmColor;
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
