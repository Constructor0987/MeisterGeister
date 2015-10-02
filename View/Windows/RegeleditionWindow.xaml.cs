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

namespace MeisterGeister.View.Windows
{
    /// <summary>
    /// Interaktionslogik für RegeleditionWindow.xaml
    /// </summary>
    public partial class RegeleditionWindow : Window
    {
        public RegeleditionWindow()
        {
            InitializeComponent();
        }

        private void buttonDSA5_Click(object sender, RoutedEventArgs e)
        {
            Global.Regeledition = "DSA 5";
            DialogResult = true;
            Close();
        }

        private void buttonDSA4_1_Click(object sender, RoutedEventArgs e)
        {
            Global.Regeledition = "DSA 4.1";
            DialogResult = true;
            Close();
        }
    }
}
