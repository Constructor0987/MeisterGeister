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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für AudioTheme.xaml
    /// </summary>
    public partial class HitchPanel : UserControl
    {
        public HitchPanel()
        {
            InitializeComponent();
        }

        private void btnAngehakt_Checked(object sender, RoutedEventArgs e)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/ok.png");
            logo.EndInit();
            btnImgAngehakt.Source = logo;
            btnHitchPanel.IsChecked = true;
        }

        private void btnAngehakt_Unchecked(object sender, RoutedEventArgs e)
        {
            btnImgAngehakt.Source = null;
            btnHitchPanel.IsChecked = false;
        }

        private void btnHitchPanel_Checked(object sender, RoutedEventArgs e)
        {
            btnAngehakt.IsChecked = true;
        }

        private void btnHitchPanel_Unchecked(object sender, RoutedEventArgs e)
        {
            btnAngehakt.IsChecked = false;
        }
    }
}
