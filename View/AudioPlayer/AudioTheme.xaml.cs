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
            //inaktiv
            if ( Convert.ToInt32(imgPlay.Tag) == 0)
            {
                //sofort spielen?
                imgPlay.Source = (Convert.ToBoolean((this as AudioTheme).Tag))?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png")):
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/feder.png"));
                imgPlay.Visibility = Visibility.Visible;
            }
            //aktiv
            else
            {
                //sofort spielen?
                imgPlay.Source = (Convert.ToBoolean((this as AudioTheme).Tag))?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png")):
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/feder.png"));
                imgPlay.Visibility = Visibility.Visible;
            }
        }

        private void btnAudioTheme_MouseLeave(object sender, MouseEventArgs e)
        {
            //aktiv
            if (Convert.ToInt32(imgPlay.Tag) == 1)
            {
                //sofort spielen?
                imgPlay.Source = (Convert.ToBoolean((this as AudioTheme).Tag))?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")):
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/feder.png"));
            }
            //inaktiv
            else
                imgPlay.Visibility = Visibility.Hidden;
        }

        private void btnAudioTheme_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == sender)
            {
                //inaktiv
                if (Convert.ToInt32(imgPlay.Tag) == 0)
                {
                    //schalte aktiv
                    imgPlay.Tag = 1;
                    //sofort spielen?
                    imgPlay.Source = (Convert.ToBoolean((this as AudioTheme).Tag))?
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")):
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/feder.png"));
                }
                //aktiv
                else
                {
                    //schalte inaktiv
                    imgPlay.Tag = 0;
                    //sofort spielen?
                    imgPlay.Source = (Convert.ToBoolean((this as AudioTheme).Tag))?
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png")):
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/feder.png"));
                }
            }
        }

        private void sldVolHintergrund_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).ToolTip = Math.Round(e.NewValue) + "%";
        }


        public void slVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ((Slider)sender).Value += (e.Delta > 1) ? 3 : -3;
        }

    }
}
