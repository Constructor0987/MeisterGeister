using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using MeisterGeister.Model.Extensions;
using System.Windows.Threading;
using System.Windows.Controls;
using System.IO;
// Eigene Usings
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.ViewModel.AudioPlayer;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using System.Windows.Media.Imaging;
using MeisterGeister.View.General;
using System.Windows.Media;
using System.Threading;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.Logic.Extensions;
using MeisterGeister.ViewModel.Kampf.Logic;
using System.Windows.Data;
using System.Windows;
using System.Collections.ObjectModel;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class PlaylistWesenAuswahlVM : Base.ViewModelBase
    {

        #region Konstruktur, Init und Refresh

        /// <summary>
        /// Nur mit einem Parameter ungleich NULL aufrufen.
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="held"></param>
        /// <param name="gegner"></param>
        public PlaylistWesenAuswahlVM(Audio_Playlist playlist, Held held, GegnerBase gegner)
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            //am ende ist nur einer gesetzt
            CurrentPlaylist = playlist;
            CurrentHeld = held;
            CurrentGegner = gegner;

            Init();
        }

        public void Init()
        {
            //ObservableCollection<Held_Audio_Playlist> heldPlaylists
            //ObservableCollection<GegnerBase_Audio_Playlist> gegnerPlaylists

            if (HatPlaylist)
            {
                HeldenListe = new ObservableCollection<Held>(Global.ContextHeld.Liste<Held>());
                GegnerListe = new ObservableCollection<GegnerBase>(Global.ContextHeld.Liste<GegnerBase>());
                //daten 
                //heldPlaylists = new ObservableCollection<Held_Audio_Playlist>(CurrentPlaylist.Held_Audio_Playlist);
                //gegnerPlaylists = new ObservableCollection<GegnerBase_Audio_Playlist>(CurrentPlaylist.GegnerBase_Audio_Playlist);
            }
            else
            {
                PlaylistListe = new ObservableCollection<Audio_Playlist>(Global.ContextHeld.Liste<Audio_Playlist>());
                //daten 
                if(HatHeld)
                    ;
                    //heldPlaylists = new ObservableCollection<Held_Audio_Playlist>(CurrentHeld.Held_Audio_Playlist);
                    
                if (HatGegner)
                    ;
                    //gegnerPlaylists = new ObservableCollection<GegnerBase_Audio_Playlist>(CurrentGegner.GegnerBase_Audio_Playlist);
            }
        }

        public void Refresh()
        {
            //der held, gegner oder die playlist könnte mittlerweile gelöscht sein?
        }
        #endregion

        #region Commands
        //Commands

        private Base.CommandBase _onFilterLöschen = null;
        public Base.CommandBase OnFilterLöschen
        {
            get
            {
                if (_onFilterLöschen == null)
                    _onFilterLöschen = new Base.CommandBase(FilterLöschen, null);
                return _onFilterLöschen;
            }
        }
        void FilterLöschen(object obj)
        {
            Filter = "";
        }

        //TODO Add und delete von Held_Audio_Playlist und GegnerBase_Audio_Playlist
        #endregion

        #region Properties
        private string _filter = "";
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (Set(ref _filter, value))
                {
                    ApplyFilter();
                }
            }
        }

        void ApplyFilter()
        {
            FilteredGegnerListe.Filter = (g => (g is GegnerBase) && (g as GegnerBase).Name.ContainsIgnoreCase(_filter));
            FilteredHeldenListe.Filter = (g => (g is Held) && (g as Held).Name.ContainsIgnoreCase(_filter));
            FilteredPlaylistListe.Filter = (g => (g is Audio_Playlist) && (g as Audio_Playlist).Name.ContainsIgnoreCase(_filter));
        }


        public AudioPlayerViewModel AudioVM;

        private string _selectedKategorie = "Tod";
        /// <summary>
        /// Die Kategorie für die neu angelegte Wesen-Playlist-Verknüpfung
        /// </summary>
        public string SelectedKategorie
        {
            get { return _selectedKategorie; }
            set
            {
                Set(ref _selectedKategorie, value);
            }
        }

        private Held _selectedHeld;
        /// <summary>
        /// Für das Hinzufügen von neuen Einträgen aus einer Listbox
        /// </summary>
        public Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {
                Set(ref _selectedHeld, value);
            }
        }

        private GegnerBase _selectedGegner;
        /// <summary>
        /// Für das Hinzufügen von neuen Einträgen aus einer Listbox
        /// </summary>
        public GegnerBase SelectedGegner
        {
            get { return _selectedGegner; }
            set
            {
                Set(ref _selectedGegner, value);
            }
        }

        private Audio_Playlist _selectedPlaylist;
        /// <summary>
        /// Für das Hinzufügen von neuen Einträgen aus einer Listbox
        /// </summary>
        public Audio_Playlist SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set
            {
                Set(ref _selectedPlaylist, value);
            }
        }

        private Audio_Playlist _currentPlaylist;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        public Audio_Playlist CurrentPlaylist
        {
            get { return _currentPlaylist; }
            set
            {
                if (value != null && Set(ref _currentPlaylist, value))
                {
                    CurrentHeld = null;
                    CurrentGegner = null;
                }
            }
        }

        private GegnerBase _currentGegner;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        public GegnerBase CurrentGegner
        {
            get { return _currentGegner; }
            set
            {
                if (value != null && Set(ref _currentGegner, value))
                {
                    CurrentHeld = null;
                    CurrentPlaylist = null;
                }
            }
        }

        private Held _currentHeld;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        public Held CurrentHeld
        {
            get { return _currentHeld; }
            set
            {
                if (value != null && Set(ref _currentHeld, value))
                {
                    CurrentGegner = null;
                    CurrentPlaylist = null;
                }
            }
        }
        
        [DependentProperty("CurrentHeld")]
        /// <summary>
        /// Für UI Anpassungen anhand des gesetzten Objektes
        /// </summary>
        public bool HatHeld
        {
            get { return CurrentHeld != null; }
        }
        [DependentProperty("CurrentGegner")]
        /// <summary>
        /// Für UI Anpassungen anhand des gesetzten Objektes
        /// </summary>
        public bool HatGegner
        {
            get { return CurrentGegner != null; }
        }
        [DependentProperty("CurrentPlaylist")]
        /// <summary>
        /// Für UI Anpassungen anhand des gesetzten Objektes
        /// </summary>
        public bool HatPlaylist
        {
            get { return CurrentPlaylist != null; }
        }

        #region Collections
        private ObservableCollection<Held> _heldenListe;
        /// <summary>
        /// Alle Einträge. Muss eigentlich nicht gebunden werden.
        /// </summary>
        public ObservableCollection<Held> HeldenListe
        {
            get { return _heldenListe; }
            set
            {
                Set(ref _heldenListe, value);
            }
        }

        private ObservableCollection<GegnerBase> _gegnerListe;
        /// <summary>
        /// Alle Einträge. Muss eigentlich nicht gebunden werden.
        /// </summary>
        public ObservableCollection<GegnerBase> GegnerListe
        {
            get { return _gegnerListe; }
            set
            {
                Set(ref _gegnerListe, value);
            }
        }

        private ObservableCollection<Audio_Playlist> _playlistListe;
        /// <summary>
        /// Alle Einträge. Muss eigentlich nicht gebunden werden.
        /// </summary>
        public ObservableCollection<Audio_Playlist> PlaylistListe
        {
            get { return _playlistListe; }
            set
            {
                Set(ref _playlistListe, value);
            }
        }

        private ICollectionView _filteredHeldenListe = null;
        /// <summary>
        /// Gefilterte und sortierte Darstellung für Auswahllisten.
        /// </summary>
        public ICollectionView FilteredHeldenListe
        {
            get {
                if (_filteredHeldenListe == null)
                {
                    _filteredHeldenListe = CollectionViewSource.GetDefaultView(HeldenListe);
                    _filteredHeldenListe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredHeldenListe; 
            }
            set
            {
                Set(ref _filteredHeldenListe, value);
            }
        }

        private ICollectionView _filteredGegnerListe = null;
        /// <summary>
        /// Gefilterte und sortierte Darstellung für Auswahllisten.
        /// </summary>
        public ICollectionView FilteredGegnerListe
        {
            get
            {
                if (_filteredGegnerListe == null)
                {
                    _filteredGegnerListe = CollectionViewSource.GetDefaultView(GegnerListe);
                    _filteredGegnerListe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredGegnerListe;
            }
            set
            {
                Set(ref _filteredGegnerListe, value);
            }
        }

        private ICollectionView _filteredPlaylistListe = null;
        /// <summary>
        /// Gefilterte und sortierte Darstellung für Auswahllisten.
        /// </summary>
        public ICollectionView FilteredPlaylistListe
        {
            get
            {
                if (_filteredPlaylistListe == null)
                {
                    _filteredPlaylistListe = CollectionViewSource.GetDefaultView(PlaylistListe);
                    _filteredPlaylistListe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredPlaylistListe;
            }
            set
            {
                Set(ref _filteredPlaylistListe, value);
            }
        }
        #endregion

        #endregion
    }
}
