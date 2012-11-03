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
    /// Interaktionslogik für AudioTheme.xaml
    /// </summary>
    public partial class HitchPanel : UserControl
    {
        public HitchPanel()
        {
            InitializeComponent();
        }

        private void btnAngehakt_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage logo = new BitmapImage();
            if (btnAngehakt.IsChecked.Value)
            {
                logo.BeginInit();
                logo.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/ok.png");
                logo.EndInit();
                btnImgAngehakt.Source = logo;
            }
            else
                btnImgAngehakt.Source = null;

            btnHitchPanel.IsChecked = btnAngehakt.IsChecked;
        }
    }
}
