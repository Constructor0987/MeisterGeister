using MeisterGeister.Model;
using MeisterGeister.Model.Service;
using MeisterGeister.View.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// Eigene Usings
using MeisterGeister.ViewModel.AudioPlayer;
using VM = MeisterGeister.ViewModel.AudioPlayer.Logic;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für PlaylistWesenAuswahlView.xaml
    /// </summary>
    public partial class PlaylistWesenAuswahlView : Window
    {
        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.PlaylistWesenAuswahlVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.PlaylistWesenAuswahlVM))
                    return null;
                return DataContext as VM.PlaylistWesenAuswahlVM;
            }
            set
            {
                DataContext = value;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        public PlaylistWesenAuswahlView(GegnerBase gegner)
            : this()
        {
            VM = new VM.PlaylistWesenAuswahlVM(null, null, gegner);
        }

        public PlaylistWesenAuswahlView(Held held) : this()
        {
            VM = new VM.PlaylistWesenAuswahlVM(null, held, null);
        }
        
        public PlaylistWesenAuswahlView(Audio_Playlist playlist)
            : this()
        {
            VM = new VM.PlaylistWesenAuswahlVM(playlist, null, null);
        }

        protected PlaylistWesenAuswahlView()
        {
            InitializeComponent();
            //WindowStartupLocation = WindowStartupLocation.Manual;
            //Left = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Location.X + 20);
            //Top = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Location.Y + 20);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
