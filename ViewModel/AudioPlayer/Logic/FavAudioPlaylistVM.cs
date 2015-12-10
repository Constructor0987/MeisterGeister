using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MeisterGeister.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Media;
//Eigene usings
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.AudioPlayer;
using System.Windows.Input;
using System.IO;
using System.Globalization;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{    
    public class FavAudioPlaylistVM  : Base.ViewModelBase
    {
        #region //---- FELDER & EIGENSCHAFTEN  + Get/Set ----
        public object DataContext { get; set; }

        private List<Audio_Playlist> _favPlaylistListe = new List<Audio_Playlist>();
        public List<Audio_Playlist> FavPlaylistListe
        {
            get { return _favPlaylistListe; }
            set { Set(ref _favPlaylistListe, value); }
        }

        private AudioPlayerViewModel _audioPlayerVM = null;
        public AudioPlayerViewModel AudioPlayerVM
        {
            get { return _audioPlayerVM; }
            set { Set(ref _audioPlayerVM, value); }
        }

        private Audio_Playlist _selectedFavPlaylist = null;
        public Audio_Playlist SelectedFavPlaylist
        {
            get { return _selectedFavPlaylist; }
            set { Set(ref _selectedFavPlaylist, value); }
        }

        private string _contentName;
        public string ContentName
        {
            get { return _contentName; }
            set { Set(ref _contentName, value); }
        }
        
        private double _windowYMiddle;
        public double WindowYMiddle
        {
            get { return _windowYMiddle; }
            set { Set(ref _windowYMiddle, value); }
        }

        
        private double _favWidth;
        public double FavWidth
        {
            get { return _favWidth; }
            set { Set(ref _favWidth, value); }
        }
        #endregion

        #region //---- KONSTRUKTOR ----

        public FavAudioPlaylistVM(List<Audio_Playlist> FavPlaylist)
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;            
            FavPlaylistListe = FavPlaylist;
        }
        
                
        #endregion

        #region //---- COMMAND ----
         
        #endregion

        #region //---- INSTANZMETHODEN ----
                
        
        #endregion
        
    }

}
