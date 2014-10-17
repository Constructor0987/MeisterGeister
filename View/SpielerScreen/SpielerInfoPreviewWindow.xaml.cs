using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
            Height = height * 0.2 + 39 + 26; // Fenstergröße entspricht 20% des 2. Bildschirm
            Width = width * 0.2;
            if (Width < 370)
            {
                Height = (height * 0.2) * 370 / Width + 39 + 26;
                Width = 370;
            }
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

        private void CheckBoxWindowFixed_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (_checkBoxWindowFixed.IsChecked == true)
            {
                SetVisualBrush();
                ResizeMode = System.Windows.ResizeMode.NoResize;
            }
            else
                ResizeMode = System.Windows.ResizeMode.CanResize;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            WindowAspectRatio.Register((Window)sender);
        }
    }

    #region // Klasse für proportionale Vergrößern des Fensters

    internal class WindowAspectRatio
    {
        private double _ratio;

        private WindowAspectRatio(Window window)
        {
            _ratio = window.Width / window.Height;
            ((HwndSource)HwndSource.FromVisual(window)).AddHook(DragHook);
        }

        public static void Register(Window window)
        {
            new WindowAspectRatio(window);
        }

        internal enum WM
        {
            WINDOWPOSCHANGING = 0x0046,
        }

        [Flags()]
        public enum SWP
        {
            NoMove = 0x2,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        private IntPtr DragHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled)
        {
            if ((WM)msg == WM.WINDOWPOSCHANGING)
            {
                WINDOWPOS position = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));

                if ((position.flags & (int)SWP.NoMove) != 0 ||
                    HwndSource.FromHwnd(hwnd).RootVisual == null) return IntPtr.Zero;

                position.cx = (int)(position.cy * _ratio);

                Marshal.StructureToPtr(position, lParam, true);
                handeled = true;
            }

            return IntPtr.Zero;
        }
    }

    #endregion
}
