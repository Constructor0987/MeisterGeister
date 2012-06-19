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
        }

        public bool IsKampfInfoModus { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Maximized;            
        }
    }
}
