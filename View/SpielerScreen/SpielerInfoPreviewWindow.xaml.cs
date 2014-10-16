using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeisterGeister.View.SpielerScreen
{
    /// <summary>
    /// Interaktionslogik für SpielerInfoPreviewWindow.xaml
    /// </summary>
    public partial class SpielerInfoPreviewWindow : Window
    {

        private static SpielerInfoPreviewWindow _instance;

        private SpielerInfoPreviewWindow()
        {
            InitializeComponent();

            SetVisualBrush();
        }

        public static SpielerInfoPreviewWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpielerInfoPreviewWindow();
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

        public void SetVisualBrush()
        {
            double width = SpielerWindow.Instance.Width;
            double height = SpielerWindow.Instance.Height;
            Height = Width / width * height + 25;
            _rectangle.Fill = SpielerWindow.VisualBrush;
        }

        new public static void Show()
        {
            if (!Instance.IsVisible)
                ((Window)Instance).Show();
            else
                ((Window)Instance).Activate();
        }

        new public static void Hide()
        {
            if (_instance != null)
            {
                ((Window)Instance).Hide();
            }
        }

        new public static void Close()
        {
            if (_instance != null)
            {
                ((Window)Instance).Close();
                _instance = null;
                if (SpielerInfoPreviewWindowClosed != null)
                    SpielerInfoPreviewWindowClosed(null, new EventArgs());
            }
        }

        public static event EventHandler SpielerInfoPreviewWindowClosed;

        private void SpielerInfoPreviewWindow_Closed(object sender, EventArgs e)
        {
            _instance = null;
            if (SpielerInfoPreviewWindowClosed != null)
                SpielerInfoPreviewWindowClosed(null, new EventArgs());
        }

        private void ButtonKampf_Click(object sender, RoutedEventArgs e)
        {
            Global.CurrentSpielerScreen.ShowKampf();
        }

        private void ButtonSpielerInfo_Click(object sender, RoutedEventArgs e)
        {
            Global.CurrentSpielerScreen.SpielerInfoOpen();
        }

        private void ButtonBodenplan_Click(object sender, RoutedEventArgs e)
        {
            Global.CurrentSpielerScreen.ShowBodenplan();
        }

        private void ButtonSpielerInfoClose_Click(object sender, RoutedEventArgs e)
        {
            Global.CurrentSpielerScreen.SpielerInfoClose();
        }

        private void ButtonBildZeigen_Click(object sender, RoutedEventArgs e)
        {
            Global.CurrentSpielerScreen.ShowImage();
        }

        private void ButtonTextZeigen_Click(object sender, RoutedEventArgs e)
        {
            Global.CurrentSpielerScreen.ShowText();
        }
    }
}
