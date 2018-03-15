using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf;
using MeisterGeister.ViewModel.SpielerScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeisterGeister.View.SpielerScreen
{
    /// <summary>
    /// Interaktionslogik für SpielerWindow.xaml
    /// </summary>
    public partial class SpielerWindow : Window
    {
        private static SpielerWindow _instance;

        private SpielerWindow()
        {
            InitializeComponent();

            int xPoint = 0, yPoint = 0;
            if (VM != null)
            {
                xPoint = VM.SpielerScreen.Bounds.Location.X + 20;
                yPoint = VM.SpielerScreen.Bounds.Location.Y + 20;
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
                    this.Topmost = false;
                    //var vm = DataContext as BattlegroundViewModel;
                    //Kampf.KampfInfoView infoView = new Kampf.KampfInfoView(Global.CurrentKampf);
                    //infoView.grdMain.LayoutTransform = new ScaleTransform(vm.ScaleKampfGrid, vm.ScaleKampfGrid);
                    
                    //vm.KampfIniInfoView = infoView;

                   // ((SpielerWindow)Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow).grdIniBodenplan.DataContext = VM.KampfIniInfoView;

                //    grdIniBodenplan.DataContext = vm.KampfIniInfoView;// VMBoden.KampfIniInfoView;
                    //Kampf.KampfInfoView infoView = new Kampf.KampfInfoView(Global.CurrentKampf);
                    //brdIni.DataContext = infoView;
                }
            }
        }

        //////////////////////////////////////////////////////////////


        private void Thumb_Drag(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            if (thumb == null)
                return;

            var pathline = thumb.DataContext as PathLine;
            if (pathline == null)
                return;
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


        private static bool _bodenplanActive = false;
        public static bool BodenplanActive
        {
            get
            {
                if (_bodenplanActive == false && _instance != null)
                    _bodenplanActive = true;
                return _bodenplanActive;
            }
        }
        
        //////////////////////////////////////////////////////////////

        public static SpielerWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpielerWindow();
                    _visualBrush = new VisualBrush(_instance);
                    if (SpielerWindowInstantiated != null)
                        SpielerWindowInstantiated(_instance, new EventArgs());
                }
                return _instance;
            }
        }

        private ViewModel.SpielerScreen.SpielerScreenControlViewModel VM
        {
            get
            {
                return DataContext as SpielerScreenControlViewModel;
            }
        }

        public static bool IsInstantiated
        {
            get
            {
                return _instance != null;
            }
        }

        private static VisualBrush _visualBrush = null;
        public static VisualBrush VisualBrush
        {
            get
            {
                if (_visualBrush == null && _instance != null)
                    _visualBrush = new VisualBrush(SpielerWindow.Instance);
                return _visualBrush;
            }
        }

        public static event EventHandler SpielerWindowInstantiated;

        new public static void Show()
        {
            ((Window)Instance).Show();
        }

        new public static void Hide()
        {
            if (_instance != null)
            {
                ((Window)Instance).Hide();
                ClearContent();
            }
        }

        new public static void Close()
        {
            if (_instance != null)
            {
                ((Window)Instance).Close();
                _instance = null;
                _visualBrush = null;
                if (SpielerWindowClosed != null)
                    SpielerWindowClosed(null, new EventArgs());
                if (Global.CurrentKampf != null &&
                    Global.CurrentKampf.BodenplanWindow != null)
                    Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive = false;
            }
        }

        public static event EventHandler SpielerWindowClosed;

        internal static void ReOpen()
        {
            Close();
            Show();
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
            RichTextBox txtBlock = new RichTextBox();
            FlowDocument flowDoc = new FlowDocument();

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
            RichTextBox txtBlock = new RichTextBox();
            FlowDocument flowDoc = new FlowDocument();

            txtBlock.Document = flowDoc;
            txtBlock.Background = (ImageBrush)App.Current.FindResource("BackgroundPergamentQuer");
            txtBlock.BorderBrush = Brushes.Transparent;
            txtBlock.Margin = new Thickness(40);
            txtBlock.Padding = new Thickness(20);

            try
            {
                // Umwandlung versuchen
                if (text.StartsWith("<FlowDocument"))
                    txtBlock.Document = System.Windows.Markup.XamlReader.Parse(text) as FlowDocument;
                else
                {
                    FlowDocument tmpDoc = new FlowDocument(new Paragraph(new Run(text)));
                    txtBlock.Document = tmpDoc;
                }
            }
            catch (System.Windows.Markup.XamlParseException)
            {
                FlowDocument tmpDoc = new FlowDocument(new Paragraph(new Run(text)));
                txtBlock.Document = tmpDoc;
            }

            SetContent(txtBlock);
        }

        public static void SetImage(string pfad, Stretch stretch = Stretch.Uniform)
        {
            try
            {
                Image img = new Image();
                img.Stretch = stretch;

                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.UriSource = new Uri(pfad, UriKind.Relative);
                bmi.EndInit();

                bmi.Freeze();		// freeze image source, used to move it across the thread
                img.Source = bmi;

                SpielerPointer pointer = new SpielerPointer();

                Binding pointerMarginBinding = new Binding("PointerMarginSpieler");
                pointerMarginBinding.Source = SpielerScreenControlViewModel.Instance;
                pointer.SetBinding(UserControl.MarginProperty, pointerMarginBinding);

                Grid grid = new Grid();
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
                Image img = new Image();

                Binding myBinding = new Binding("CurrentSlideShowImage");
                myBinding.Source = vm;
                img.SetBinding(Image.SourceProperty, myBinding);

                Instance.Content = img;

                Show();
            }
            catch { }
        }

        public static void SetKampfInfoView()
        {
            if (Global.CurrentKampf != null)
            {
                Kampf.KampfInfoView infoView = new Kampf.KampfInfoView(Global.CurrentKampf);
                SetContent(infoView);
            }
        }

        public static void SetBodenplanView()
        {
                
        }

        private static void btn_Click(object sender, RoutedEventArgs e)
        {
            Rectangle rec =  Instance.FindName("rectGrid") as Rectangle;
            rec.Margin = new Thickness(100, 0, -100, 0);
        }

        private static void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rec = ((sender) as Rectangle);
            double x = rec.Margin.Left;
            rec.Margin = new Thickness(x+100, 0, -x-100, 0);
        }

        public bool IsKampfInfoModus { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.Screen.AllScreens.Length > 1)// Global.CurrentKampf.Left < System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width)
            {
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                this.Left = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width;
                
                this.WindowState = System.Windows.WindowState.Maximized;
                this.WindowStyle = System.Windows.WindowStyle.None;
            } 
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            }
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
                SpielerWindowClosed(null, new EventArgs());
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

    }
}
