using MeisterGeister.Model;
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

using Base = MeisterGeister.ViewModel.Base;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für boxThemeTheme.xaml
    /// </summary>
    public partial class boxThemeTheme : UserControl
    {
        public Audio_Playlist aPlaylist;
        public Audio_Theme aTheme;   

        private void OnPlaylistNameUpdated(object sender, DataTransferEventArgs e)
        {
            txblkName.Text = aPlaylist.Name;
            imgIcon.Source = new BitmapImage(new Uri( 
                (aPlaylist.Hintergrundmusik) ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png" : 
                "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png")); 
        }
                    
        public boxThemeTheme()
        {
            InitializeComponent();
        }
    }
}

