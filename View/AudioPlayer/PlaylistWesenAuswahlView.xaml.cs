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
            set { DataContext = value; }
        }

        public PlaylistWesenAuswahlView(GegnerBase gegner)
            : this()
        {
            VM = new VM.PlaylistWesenAuswahlVM(null, null, gegner);
        }

        public PlaylistWesenAuswahlView(Held held)
            : this()
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
        }

        private void PListPlaylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.FilteredPlaylist2Liste.Contains(VM.CurrentPlaylist))
                VM.SelectedPlaylist2 = VM.CurrentPlaylist;
        }

        private void WesenPlaylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.FilteredPlaylistListe.Contains(VM.SelectedPlaylist2))
                VM.CurrentPlaylist = VM.SelectedPlaylist2;
        }

        private void WesenHeld_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.FilteredHeldenListe.Contains(VM.SelectedHeld2))
                VM.SelectedHeld = VM.SelectedHeld2;
        }

        private void WesenGegner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.FilteredGegnerListe.Contains(VM.SelectedGegner2))
                VM.SelectedGegner = VM.SelectedGegner2;
        }

        private void PListGegner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.FilteredGegner2Liste.Contains(VM.SelectedGegner))
                VM.SelectedGegner2 = VM.SelectedGegner;

        }

        private void PListHeld_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.FilteredHelden2Liste.Contains(VM.SelectedHeld))
                VM.SelectedHeld2 = VM.SelectedHeld;
        }
    }
}
