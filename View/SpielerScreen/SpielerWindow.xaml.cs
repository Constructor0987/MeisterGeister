using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaktionslogik für SpielerWindow.xaml
    /// </summary>
    public partial class SpielerWindow : Window
    {
        public SpielerWindow()
        {
            InitializeComponent();

            int xPoint = 0, yPoint = 0;
            foreach (System.Windows.Forms.Screen objActualScreen in ScreenList)
            {
                if (!objActualScreen.Primary)
                {
                    xPoint = objActualScreen.Bounds.Location.X + 20;
                    yPoint = objActualScreen.Bounds.Location.Y + 20;
                }
            }
            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = Convert.ToDouble(xPoint);
            Top = Convert.ToDouble(yPoint);
        }

        List<System.Windows.Forms.Screen> ScreenList = System.Windows.Forms.Screen.AllScreens.ToList();

        public bool IsKampfInfoModus { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ScreenList.Count > 1)
            {
                WindowState = System.Windows.WindowState.Maximized;
                WindowStyle = System.Windows.WindowStyle.None;
            }
            else
            { // bei nur einem Screen nicht im Vollbildmodus starten
                WindowState = System.Windows.WindowState.Normal;
                WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
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
    }
}
