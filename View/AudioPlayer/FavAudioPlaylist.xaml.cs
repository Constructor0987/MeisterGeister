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
using System.Text.RegularExpressions;
using MeisterGeister.Model.Extensions;
//eigene
using VM = MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
//using MeisterGeister.Model;

namespace MeisterGeister.View.AudioPlayer
{

    /// <summary>
    /// Interaktionslogik für AudioZeile.xaml
    /// </summary>
    public partial class FavAudioPlaylist : Window
    {

        public VM.FavAudioPlaylistVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.FavAudioPlaylistVM))
                    return null;
                return DataContext as VM.FavAudioPlaylistVM;
            }
            set { DataContext = value; }
        }

        public FavAudioPlaylist()
        {
            InitializeComponent();
        }
        public FavAudioPlaylist(List<Audio_Playlist> favPlaylist)
            : this()
        {
            VM = new VM.FavAudioPlaylistVM(favPlaylist);
            InitializeComponent();
        }

        private void _favAudioPlaylist_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VM.AudioPlayerVM.FavPlayView = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.SelectedFavPlaylist = (sender as Button).Tag as Audio_Playlist;
            VM.AudioPlayerVM.SelectedMusikPlaylistItem = VM.AudioPlayerVM.MusikListItemListe.
                FirstOrDefault(t => t.VM.aPlaylist == VM.SelectedFavPlaylist);
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            lbEditorItemVM source = e.Data.GetData("lbiPlaylistVM") as lbEditorItemVM;
            if (source != null && source.APlaylist.Hintergrundmusik)
            {
                source.APlaylist.Favorite = true;
                Global.ContextAudio.Update<Audio_Playlist>(source.APlaylist);
                VM.AudioPlayerVM.UpdateFavorites();
            }
        }
    }
}
