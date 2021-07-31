using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf;
using MeisterGeister.ViewModel.Karte;
using MeisterGeister.ViewModel.SpielerScreen;

namespace MeisterGeister.View.SpielerScreen
{
    /// <summary>
    /// Interaktionslogik für SpielerWindow.xaml
    /// </summary>
    public partial class SpielerWindow : Window
    {
        public static event EventHandler SpielerWindowInstantiated;

        public static event EventHandler SpielerWindowClosed;

        public static bool BodenplanActive
        {
            get
            {
                if (_bodenplanActive == false && _instance != null)
                {
                    _bodenplanActive = true;
                }

                return _bodenplanActive;
            }
        }

        public static SpielerWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpielerWindow();
                    _visualBrush = new VisualBrush(_instance);
                    if (SpielerWindowInstantiated != null)
                    {
                        SpielerWindowInstantiated(_instance, new EventArgs());
                    }
                }
                return _instance;
            }
        }

        public static bool IsInstantiated
        {
            get
            {
                return _instance != null;
            }
        }

        public static VisualBrush VisualBrush
        {
            get
            {
                if (_visualBrush == null && _instance != null)
                {
                    _visualBrush = new VisualBrush(SpielerWindow.Instance);
                }

                return _visualBrush;
            }
        }

        public BattlegroundViewModel VMBoden
        {
            get { return DataContext as BattlegroundViewModel; }
            set { DataContext = value; }
        }

        public KampfViewModel KampfVM
        {
            get { return (DataContext as BattlegroundViewModel).kampf; }
        }

        public bool IsKampfInfoModus { get; set; }

        public DispatcherTimer _timerVideoUpdate = new DispatcherTimer();

        public static new void Show()
        {
            ((Window)Instance).Show();
        }

        public static new void Hide()
        {
            if (_instance != null)
            {
                ((Window)Instance).Hide();
                ClearContent();
            }
        }

        public static new void Close()
        {
            if (_instance != null)
            {
                ((Window)Instance).Close();
                _instance = null;
                _visualBrush = null;
                if (SpielerWindowClosed != null)
                {
                    SpielerWindowClosed(null, new EventArgs());
                }

                if (Global.CurrentKampf != null &&
                    Global.CurrentKampf.BodenplanView != null)
                {
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive = false;
                }
            }
        }

        public static void ClearContent()
        {
            Instance.Content = null;
        }

        public static void SetContent(object info)
        {            
            Instance.Content = info;
            Instance.IsKampfInfoModus = (info is Kampf.KampfInfoView);

            Show();
        }

        public static void SetTextFromClipboard()
        {
            var txtBlock = new RichTextBox();
            var flowDoc = new FlowDocument();

            txtBlock.Document = flowDoc;
            txtBlock.Background = (ImageBrush)App.Current.FindResource("BackgroundPergamentQuer");
            txtBlock.BorderBrush = Brushes.Transparent;
            txtBlock.Margin = new Thickness(40);
            txtBlock.Padding = new Thickness(20);

            txtBlock.Paste();

            SetContent(txtBlock);
        }

        public static void SetText(string text)
        {
            var txtBlock = new RichTextBox();
            var flowDoc = new FlowDocument();

            txtBlock.Document = flowDoc;
            txtBlock.Background = (ImageBrush)App.Current.FindResource("BackgroundPergamentQuer");
            txtBlock.BorderBrush = Brushes.Transparent;
            txtBlock.Margin = new Thickness(40);
            txtBlock.Padding = new Thickness(20);

            try
            {
                // Umwandlung versuchen
                if (text.StartsWith("<FlowDocument"))
                {
                    txtBlock.Document = System.Windows.Markup.XamlReader.Parse(text) as FlowDocument;
                }
                else
                {
                    var tmpDoc = new FlowDocument(new Paragraph(new Run(text)));
                    txtBlock.Document = tmpDoc;
                }
            }
            catch (System.Windows.Markup.XamlParseException)
            {
                var tmpDoc = new FlowDocument(new Paragraph(new Run(text)));
                txtBlock.Document = tmpDoc;
            }

            SetContent(txtBlock);
        }

        public static void SetImage(string pfad, Stretch stretch = Stretch.Uniform)
        {
            try
            {
                var img = new Image
                {
                    Stretch = stretch
                };

                if (pfad != null && pfad.StartsWith("/DSA MeisterGeister;component"))
                {
                    string bildS = Global.SelectedHeld.Bild.Replace(
                        "/DSA MeisterGeister;component", "pack://application:,,,/DSA MeisterGeister;component");
                    img.Source = new BitmapImage(new Uri(bildS));
                }
                else
                {
                    var bmi = new BitmapImage();
                    bmi.BeginInit();
                    bmi.UriSource = new Uri(pfad, UriKind.Relative);
                    bmi.EndInit();

                    bmi.Freeze();       // freeze image source, used to move it across the thread
                    img.Source = bmi;
                }
                var pointer = new SpielerPointer();

                var pointerMarginBinding = new Binding("PointerMarginSpieler")
                {
                    Source = SpielerScreenControlViewModel.Instance
                };
                pointer.SetBinding(UserControl.MarginProperty, pointerMarginBinding);

                var grid = new Grid();
                grid.Children.Add(img);
                grid.Children.Add(pointer);
                grid.DataContext = SpielerScreenControlViewModel.Instance;

                SetContent(grid);
            }
            catch { }
        }

        public static void SetSlideShow(ViewModel.SpielerScreen.SpielerScreenControlViewModel vm)
        {
            Instance.DataContext = vm;

            try
            {
                var img = new Image();

                var myBinding = new Binding("CurrentSlideShowImage")
                {
                    Source = vm
                };
                img.SetBinding(Image.SourceProperty, myBinding);

                Instance.Content = img;

                Show();
            }
            catch { }
        }

        public static void SetKampfInfoView(System.Windows.Forms.Screen SpielerScr = null)
        {
            if (Global.CurrentKampf != null)
            {
                var infoView = new Kampf.KampfInfoView(Global.CurrentKampf);

                infoView.LayoutTransform = new ScaleTransform(2,2);
                SetContent(infoView);
                infoView.UpdateLayout();
                double höheKämpferlist = Global.CurrentKampf.Kampf.KämpferIListImKampf.Count() * 30  +40+50 + 
                     infoView.grdMain.RowDefinitions[0].ActualHeight;
                double transFactor = SpielerScr == null? 2: 
                    Math.Min(2,  SpielerScr.WorkingArea.Height / (höheKämpferlist * 2));
                if (transFactor < 2)
                    infoView.LayoutTransform = new ScaleTransform(transFactor, transFactor);
            }
        }

        internal static void ReOpen()
        {
            Close();
            Show();
        }

        private static SpielerWindow _instance;

        private static bool _bodenplanActive = false;

        private static VisualBrush _visualBrush = null;

        private SpielerWindow()
        {
            InitializeComponent();

            int xPoint = 0, yPoint = 0;
            if (VM != null)
            {
                xPoint = VM.SpielerScreen.Bounds.Location.X + 20;
                yPoint = VM.SpielerScreen.Bounds.Location.Y + 20;
            }
            else
            {
                System.Windows.Forms.Screen SpielerScreen = null; 
                foreach (System.Windows.Forms.Screen objActualScreen in System.Windows.Forms.Screen.AllScreens.ToList())
                {
                    if (!objActualScreen.Primary)
                        SpielerScreen = objActualScreen;
                }
                if (SpielerScreen != null)
                {
                    xPoint = SpielerScreen.Bounds.Location.X + 20;
                    yPoint = SpielerScreen.Bounds.Location.Y + 20;
                }
            }
            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = Convert.ToDouble(xPoint);
            Top = Convert.ToDouble(yPoint);

            if (Global.CurrentKampf != null &&
                Global.CurrentKampf.BodenplanViewModel != null &&
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive)
            {
                VMBoden = Global.CurrentKampf.BodenplanViewModel;
                grdStandard.Visibility = Visibility.Collapsed;
                grdBodenplan.Visibility = Visibility.Visible;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow = this;
                if (VMBoden.IsShowIniKampf)
                {
                    Topmost = false;
                }
            }
        }

        //////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////
        private ViewModel.SpielerScreen.SpielerScreenControlViewModel VM
        {
            get
            {
                return DataContext as SpielerScreenControlViewModel;
            }
        }

        private static void btn_Click(object sender, RoutedEventArgs e)
        {
            var rec = Instance.FindName("rectGrid") as Rectangle;
            rec.Margin = new Thickness(100, 0, -100, 0);
        }

        private static void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var rec = ((sender) as Rectangle);
            var x = rec.Margin.Left;
            rec.Margin = new Thickness(x + 100, 0, -x - 100, 0);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.Screen.AllScreens.Length > 1)// Global.CurrentKampf.Left < System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
            {
                WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                Left = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width;

                WindowState = System.Windows.WindowState.Maximized;
                WindowStyle = System.Windows.WindowStyle.None;
            }
            else
            {
                WindowState = System.Windows.WindowState.Normal;
                WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            }

            _timerVideoUpdate.Interval = TimeSpan.FromMilliseconds(200);
            _timerVideoUpdate.Tick += new EventHandler(_timerVideoUpdate_Tick);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            { // Vollbildmodus verlassen
                WindowState = System.Windows.WindowState.Normal;
                WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _instance = null;
            _visualBrush = null;
            if (SpielerWindowClosed != null)
            {
                SpielerWindowClosed(null, new EventArgs());
            }

            _timerVideoUpdate.Stop();
            if (VMBoden != null && //= Global.CurrentKampf.BodenplanViewModel
                grdStandard.Visibility == Visibility.Collapsed &&
                grdBodenplan.Visibility == Visibility.Visible)
            {
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive = false;
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow = null;
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Toogle Vollbildmodus
            if (WindowStyle == System.Windows.WindowStyle.SingleBorderWindow)
            {
                WindowState = System.Windows.WindowState.Maximized;
                WindowStyle = System.Windows.WindowStyle.None;
            }
            else
            {
                WindowState = System.Windows.WindowState.Normal;
                WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            }
        }

        private void MP4BattlemapBackgroundSpieler_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = TimeSpan.FromSeconds(Global.CurrentKampf.BodenplanViewModel.BackgroundMp4MinPosition);
        }

        private void MP4BattlemapBackgroundSpieler_MediaOpened(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = TimeSpan.FromSeconds(Global.CurrentKampf.BodenplanViewModel.BackgroundMp4MinPosition);
            ((MediaElement)sender).SpeedRatio = Global.CurrentKampf.BodenplanView.VideoObject1.SpeedRatio;
            _timerVideoUpdate.Start();
        }

        public void _timerVideoUpdate_Tick(object sender, EventArgs e)
        {            
            if (MP4BattlemapBackgroundSpieler.Position >= TimeSpan.FromSeconds(Global.CurrentKampf.BodenplanViewModel.BackgroundMp4MaxPosition))
                MP4BattlemapBackgroundSpieler.Position = TimeSpan.FromSeconds(Global.CurrentKampf.BodenplanViewModel.BackgroundMp4MinPosition);
            MP4BattlemapBackgroundSpieler.SpeedRatio = Global.CurrentKampf.BodenplanView.VideoObject1.SpeedRatio;
        }
    }
}
