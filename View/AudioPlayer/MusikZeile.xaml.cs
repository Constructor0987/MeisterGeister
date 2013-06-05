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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für MusikZeile.xaml
    /// </summary>
    public partial class MusikZeile : ListBoxItem
    {
        public MusikZeile()
        {
            InitializeComponent();
        }

        private void tbtnCheck_Checked(object sender, RoutedEventArgs e)
        {
            btnImgOK.Visibility = Visibility.Visible;
        }

        private void tbtnCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            btnImgOK.Visibility = Visibility.Hidden;
        }
        
    }
}
