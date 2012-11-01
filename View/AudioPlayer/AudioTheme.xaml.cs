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
    public partial class AudioTheme : UserControl
    {
        public AudioTheme()
        {
            InitializeComponent();
        }

        private void btnAudioTheme_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            if ( Convert.ToInt32(imgPlay.Tag) == 0)
            {
                logo.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png");
                imgPlay.Visibility = Visibility.Visible;
            }
            else
            {
                logo.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause_grau.png");
                imgPlay.Visibility = Visibility.Visible;
            }
            logo.EndInit();
            imgPlay.Source = logo;
        }

        private void btnAudioTheme_MouseLeave(object sender, MouseEventArgs e)
        {
            if ( Convert.ToInt32(imgPlay.Tag) == 0)
                imgPlay.Visibility = Visibility.Hidden;                
            else
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png");
                logo.EndInit();
                imgPlay.Source = logo;
            }
        }

        private void btnAudioTheme_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == sender)
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                if (Convert.ToInt32(imgPlay.Tag) == 0)
                {
                    imgPlay.Tag = 1;
                    logo.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png");
                }
                else
                {
                    imgPlay.Tag = 0;
                    logo.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png");
                }
                logo.EndInit();
                imgPlay.Source = logo;
                //btnAudioTheme_MouseEnter(sender, null);
            }
        }

        private void pbarActBGTitel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

    }
}
