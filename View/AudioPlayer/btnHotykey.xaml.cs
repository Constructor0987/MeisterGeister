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
using System.IO;

//eigene 
using MeisterGeister.Model;
using VM = MeisterGeister.ViewModel.AudioPlayer.Logic;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für btnHotkey.xaml
    /// </summary>
        

    public partial class btnHotkey : UserControl
    {
        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.btnHotkeyVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.btnHotkeyVM))
                    return null;
                return DataContext as VM.btnHotkeyVM;
            }
            set { DataContext = value; }
        }
                
        public btnHotkey()
        {
            InitializeComponent();
            VM = new VM.btnHotkeyVM();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).Tag as Audio_Playlist).Key = null;
            Global.ContextAudio.Update<Audio_Playlist>((sender as MenuItem).Tag as Audio_Playlist);
            this.Visibility = Visibility.Collapsed;
        }
    }
}
