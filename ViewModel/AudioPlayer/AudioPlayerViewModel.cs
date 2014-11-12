using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MeisterGeister.Model;
using System.Windows.Data;

namespace MeisterGeister.ViewModel.AudioPlayer
{
    class ZeileVM : INotifyPropertyChanged
    {

        // property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        #region //---- FELDER ----

        private double _volume = 50;
        double _XPos = 10;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public double XPos
        {
            get { return _XPos; }
            set
            {
                _XPos = value;
                OnPropertyChanged("XPos");
            }
        }

        
        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public double AktLautstärke
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged("ZeileVMVolume");
            }
        }
        
        #endregion

    }

    public class MultiBooleanToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                                Type targetType,
                                object parameter,
                                System.Globalization.CultureInfo culture)
        {
            bool visible = true;
            foreach (object value in values)
                if (value is bool)
                    visible = visible && (bool)value;

            if (visible)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Hidden;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    
    public class AudioPlayerViewModel : Base.ViewModelBase
    {
        #region //---- FELDER & EIGENSCHAFTEN ----

        private Audio_Playlist _aktKlangPlaylist;
        public Audio_Playlist AktKlangPlaylist
        {
            get { return _aktKlangPlaylist; }
            set
            {
                _aktKlangPlaylist = value;
                OnChanged();
            }
        }

        private List<Audio_Playlist> _playlistListe;

        public List<Audio_Playlist> PlaylistListe
        {
            get { return _playlistListe; }
            set
            {
                _playlistListe = value;
                OnChanged("PlaylistListe");
            }
        }

        

        #endregion

        private Base.CommandBase _onWarteZeitMinPlus = null;
        public Base.CommandBase OnWarteZeitMinPlus
        {
            get
            {
                if (_onWarteZeitMinPlus == null)
                    _onWarteZeitMinPlus = new Base.CommandBase(WarteZeitMinPlus, null);
                return _onWarteZeitMinPlus;
            }
        }

        private void WarteZeitMinPlus(object obj)
        {
            // TODO: WarteZeitMin erhöhen
            AktKlangPlaylist.WarteZeitMin = 200;
        }


        #region //---- KONSTRUKTOR ----

        public AudioPlayerViewModel()
        {

        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Refresh()
        {
           // OnChanged("HeldListe");
        }

        #endregion

        #region //---- EVENTS ----

        #endregion


        private ObservableCollection<TitelInfo> _titelListe = new ObservableCollection<TitelInfo>();

        /// <summary>
        /// Titel der aktuellen Playlist.
        /// </summary>
        public ObservableCollection<TitelInfo> TitelListe
        {
            get { return _titelListe; }
            set
            {
                _titelListe = value;
                OnChanged("TitelListe");
            }
        }

        private Audio_Playlist _currentPlaylist;

        public Audio_Playlist CurrentPlaylist
        {
            get { return _currentPlaylist; }
            set { 
                _currentPlaylist = value;
                TitelListe.Clear();
                if (value != null)
                    value.Audio_Playlist_Titel.Select(pt => new TitelInfo(pt)).ToList().ForEach(ti => TitelListe.Add(ti));
                OnChanged("CurrentPlaylist");
                OnChanged("TitelListe");
            }
        }

        public class TitelInfo : Base.ViewModelBase
        {
            private Audio_Playlist_Titel _playlistTitel;

            public Audio_Playlist_Titel PlaylistTitel
            {
                get { return _playlistTitel; }
            }

            public TitelInfo(Audio_Playlist_Titel aPlaylistTitel)
            {
                _playlistTitel = aPlaylistTitel;
            }

            public event EventHandler OnRemoveTitel;
            void RemoveTitel(object sender)
            {
                Global.ContextAudio.RemoveTitelFromPlaylist(PlaylistTitel);
                if (OnRemoveTitel != null)
                {
                    OnRemoveTitel(this, new EventArgs());
                }
            }

        }

      /*  private Base.CommandBase _onAddTitel;
        public Base.CommandBase OnAddTitel
        {
            get { return _onAddTitel; }
        }*/

        /// <summary>
        /// Neuer Titel vom FileChooser aus. Wird zur aktuellen Playlist hinzugefügt.
        /// </summary>
        /// <param name="sender"></param>
        private void AddTitel(object sender)
        {
            string pfad = "";
            //präsentiere einen FileChooser und werte das Ergebnis aus.
            //pfad = ergebnis vom filechooser
            //danach ertellen wir einen neuen Titel
            Audio_Titel aTitel = CreateTitel(pfad);
            //und fügen ihn der Playlist hinzu
            Audio_Playlist_Titel aPlaylistTitel = AddTitelToPlaylist(CurrentPlaylist, aTitel);
            //Und auch der TitelListe, die ja die titel der aktuellen playlist darstellt
            TitelListe.Add(new TitelInfo(aPlaylistTitel));
        }

        private Audio_Titel CreateTitel(string pfad)
        {
            Audio_Titel tmp = Global.ContextAudio.New<Audio_Titel>();
            //TODO JB: hier aus Meta informationen den Namen auslesen
            tmp.Name = "Neuer Titel";
            tmp.Pfad = pfad;
            return tmp;
        }

        private Audio_Playlist_Titel AddTitelToPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel) {
            Audio_Playlist_Titel tmp;
            Global.ContextAudio.AddTitelToPlaylist(aPlaylist, aTitel, out tmp);
            return tmp;
        }

        
    }
}
