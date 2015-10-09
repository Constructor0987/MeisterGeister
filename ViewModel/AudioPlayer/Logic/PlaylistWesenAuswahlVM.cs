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
            //TODO braucht noch das AudioVM oder eine andere Möglichkeit zum abspielen der Playlist, damit man beim editieren der zuordnung mal kurz reinhören kann.

            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            //am ende ist nur einer gesetzt
            CurrentPlaylist = playlist;
            CurrentHeld = held;
            CurrentGegner = gegner;

            Init();
        }

        private List<string> GetKategorien()
        {
            List<string> s = new List<string>();
            s.Add("Allgemein");
            s.Add("Zauberformel");
            s.Add("Tod");
            s.Add("Nahkampf");
            s.Add("Fernkampf");
            s.Add("Wunde");
            return s;
        }
        private List<Image> GetIcons()
        {
            List<Image> iconlist = new List<Image>();
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/General/speaker.png", UriKind.Relative)) });            
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/alchimie.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/alchimie.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/angeln.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/artefakt.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/audio2.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/beidhändig.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/fernkampf.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/foliant.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/halbschwert.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/jagd.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/kartenzeichnen.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/magie.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/mechanik.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/muenze.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/munition.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/nahkampf_01.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/nahkampf_02.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/nahkampf_03.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/notiz.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/parierwaffe.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/ruestung.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/schaden.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/schild.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/schmiede.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/sprache.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/tot.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/ueberanstrengung.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/wuerfelbecher.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/zauberzeichen.png", UriKind.Relative)) });

            return iconlist;
        }

        public void Init()
        {
            if (IconListe == null || IconListe.Count == 0)
            {
                IconListe = new ObservableCollection<Image>(GetIcons());
            }
            if (KategorieListe == null || KategorieListe.Count == 0)
            {
                KategorieListe = new ObservableCollection<string>(GetKategorien());
            }
            if (HatPlaylist)
            {
                PlaylistListe = new ObservableCollection<Audio_Playlist>(Global.ContextAudio.PlaylistListe.Where(t => !t.Hintergrundmusik));
                HeldenListe = new ObservableCollection<Held>(Global.ContextHeld.Liste<Held>());
                GegnerListe = new ObservableCollection<GegnerBase>(Global.ContextHeld.Liste<GegnerBase>());

                //daten 
                WesenPlaylistListe = new ObservableCollection<IWesenPlaylist>(CurrentPlaylist.Held_Audio_Playlist.AsEnumerable<IWesenPlaylist>().Union(CurrentPlaylist.GegnerBase_Audio_Playlist).AsEnumerable<IWesenPlaylist>() );
            }
            else
            {
                PlaylistListe = new ObservableCollection<Audio_Playlist>(Global.ContextHeld.Liste<Audio_Playlist>());
                //daten 
                if(HatHeld)
                    WesenPlaylistListe = new ObservableCollection<IWesenPlaylist>(CurrentPlaylist.Held_Audio_Playlist.AsEnumerable<IWesenPlaylist>() );
                else if (HatGegner)
                    WesenPlaylistListe = new ObservableCollection<IWesenPlaylist>(CurrentPlaylist.GegnerBase_Audio_Playlist.AsEnumerable<IWesenPlaylist>());
            }
        }

        public void Refresh()
        {
            //der held, gegner oder die playlist könnte mittlerweile gelöscht sein?
            //Nein, denn das Fenser wird mit ShowDialog modal aufgerufen.
        }
        #endregion

        #region Commands
        //Commands

        private Base.CommandBase _onPlaylistAnspielenClick = null;
        public Base.CommandBase OnPlaylistAnspielenClick
        {
            get
            {
                if (_onPlaylistAnspielenClick == null)
                    _onPlaylistAnspielenClick = new Base.CommandBase(PlaylistAnspielen, null);
                return _onPlaylistAnspielenClick;
            }
        }
        private void PlaylistAnspielen(object obj)
        {
            btnHotkeyVM hotkey = new btnHotkeyVM();
            hotkey.aPlaylist = CurrentPlaylist;
            hotkey.OnBtnClick(hotkey);
        }

        private Base.CommandBase _onWesenFilterLöschen = null;
        public Base.CommandBase OnWesenFilterLöschen
        {
            get
            {
                if (_onWesenFilterLöschen == null)
                    _onWesenFilterLöschen = new Base.CommandBase(WesenFilterLöschen, null);
                return _onWesenFilterLöschen;
            }
        }
        void WesenFilterLöschen(object obj)
        {
            WesenFilter = "";
        }

        private Base.CommandBase _onGegnerFilterLöschen = null;
        public Base.CommandBase OnGegnerFilterLöschen
        {
            get
            {
                if (_onGegnerFilterLöschen == null)
                    _onGegnerFilterLöschen = new Base.CommandBase(GegnerFilterLöschen, null);
                return _onGegnerFilterLöschen;
            }
        }
        void GegnerFilterLöschen(object obj)
        {
            GegnerFilter = "";
        }

        private Base.CommandBase _onHeldenFilterLöschen = null;
        public Base.CommandBase OnHeldenFilterLöschen
        {
            get
            {
                if (_onHeldenFilterLöschen == null)
                    _onHeldenFilterLöschen = new Base.CommandBase(HeldenFilterLöschen, null);
                return _onHeldenFilterLöschen;
            }
        }
        void HeldenFilterLöschen(object obj)
        {
            HeldenFilter = "";
        }


        //Add und delete von Held_Audio_Playlist und GegnerBase_Audio_Playlist
        private Base.CommandBase _onAddWesenPlaylist;
        public Base.CommandBase OnAddWesenPlaylist
        {
            get
            {
                if (_onAddWesenPlaylist == null)
                    _onAddWesenPlaylist = new Base.CommandBase(AddWesenPlaylist, null);
                return _onAddWesenPlaylist;
            }
        }
        public void AddWesenPlaylist(object obj)
        {
            Audio_Playlist pl = CurrentPlaylist;
            if (pl == null)
                pl = SelectedPlaylist;
            Held h = CurrentHeld;
            if (h == null)
                h = SelectedHeld;
            GegnerBase g = CurrentGegner;
            if (g == null)
                g = SelectedGegner;
            string k = SelectedKategorie != null? SelectedKategorie: CurrentKategorie;
            if (pl == null || (h == null && g == null) || k == null )
                return;
            IWesenPlaylist iwpl = null;
            if(h != null)
            {
                Held_Audio_Playlist wpl = Global.ContextAudio.New<Held_Audio_Playlist>();
                wpl.Audio_PlaylistGUID = pl.Audio_PlaylistGUID;
                wpl.Audio_Playlist = pl;
                wpl.HeldGUID = h.HeldGUID;
                wpl.Held = h;
                wpl.Kategorie = k;
                wpl.Icon = CurrentIcon.Source.ToString();
                //TODO check ob schon vorhanden
                h.Held_Audio_Playlist.Add(wpl);
                iwpl = wpl;
            }
            else if(g !=  null)
            {
                GegnerBase_Audio_Playlist wpl = Global.ContextAudio.New<GegnerBase_Audio_Playlist>();
                wpl.Audio_PlaylistGUID = pl.Audio_PlaylistGUID;
                wpl.Audio_Playlist = pl;
                wpl.GegnerBaseGUID = g.GegnerBaseGUID;
                wpl.GegnerBase = g;
                wpl.Kategorie = k;
                wpl.Icon = CurrentIcon.Source.ToString();
                //TODO check ob schon vorhanden
                g.GegnerBase_Audio_Playlist.Add(wpl);
                iwpl = wpl;
            }
            WesenPlaylistListe.Add(iwpl);
            Global.ContextAudio.Save();
            SelectedHeld = null;
            SelectedGegner = null;
            Init();
        }

        private Base.CommandBase _onDeleteWesenPlaylist;
        public Base.CommandBase OnDeleteWesenPlaylist
        {
            get
            {
                if (_onDeleteWesenPlaylist == null)
                    _onDeleteWesenPlaylist = new Base.CommandBase(DeleteWesenPlaylist, null);
                return _onDeleteWesenPlaylist;
            }
        }
        void DeleteWesenPlaylist(object obj)
        {
            var value = obj;
            if (obj == null)
                return;
            SelectedWesenPlaylist = (obj as IWesenPlaylist);

            //SelectedWesenPlaylist =
            //    CurrentHeld != null ? WesenPlaylistListe.FirstOrDefault(t => t.Held == CurrentHeld) :
            //    CurrentGegner != null ? WesenPlaylistListe.FirstOrDefault(t => t.GegnerBase == CurrentGegner) : null;
            
            if (SelectedWesenPlaylist == null)
                return;
            if(SelectedWesenPlaylist.HatGegner)
                Global.ContextAudio.Delete<GegnerBase_Audio_Playlist>(SelectedWesenPlaylist as GegnerBase_Audio_Playlist);
            else if(SelectedWesenPlaylist.HatHeld)
                Global.ContextAudio.Delete<Held_Audio_Playlist>(SelectedWesenPlaylist as Held_Audio_Playlist);
            WesenPlaylistListe.Remove(SelectedWesenPlaylist);
            SelectedWesenPlaylist = null;
            Init();
        }
        #endregion

        #region Properties
        private string _wesenFilter = "";
        public string WesenFilter
        {
            get { return _wesenFilter; }
            set
            {
                if (Set(ref _wesenFilter, value))
                {
                    ApplyWesenFilter();
                }
            }
        }
        
        private string _heldenFilter = "";
        public string HeldenFilter
        {
            get { return _heldenFilter; }
            set
            {
                if (Set(ref _heldenFilter, value))
                {
                    ApplyHeldenFilter();
                }
            }
        }

        private string _gegnerFilter = "";
        public string GegnerFilter
        {
            get { return _gegnerFilter; }
            set
            {
                if (Set(ref _gegnerFilter, value))
                {
                    ApplyGegnerFilter();
                }
            }
        }

        void ApplyFilter()
        {
            FilteredGegnerListe.Filter = (g => (g is GegnerBase) && (g as GegnerBase).Name.ContainsIgnoreCase(_gegnerFilter));
            FilteredHeldenListe.Filter = (g => (g is Held) && (g as Held).Name.ContainsIgnoreCase(_heldenFilter));
            
            FilteredWesenListe.Filter = (g => (g is IKämpfer) && (g as IKämpfer).Name.ContainsIgnoreCase(_gegnerFilter));
          //  FilteredPlaylistListe.Filter = (g => (g is Audio_Playlist) && (g as Audio_Playlist).Name.ContainsIgnoreCase(_filter));
        }

        void ApplyWesenFilter()
        {
            FilteredWesenListe.Filter = (g => (g is Held) && (g as Held).Name.ContainsIgnoreCase(_heldenFilter));
        }
        
        void ApplyHeldenFilter()
        {
            FilteredHeldenListe.Filter = (g => (g is Held) && (g as Held).Name.ContainsIgnoreCase(_heldenFilter));
        }

        void ApplyGegnerFilter()
        {
            FilteredGegnerListe.Filter = (g => (g is GegnerBase) && (g as GegnerBase).Name.ContainsIgnoreCase(_gegnerFilter));            
        }


        //zum abspielen benötigt
        public AudioPlayerViewModel AudioVM;

        private IWesenPlaylist _selectedWesenPlaylist;
        /// <summary>
        /// Die aktuell ausgewählte WesenPlaylist zum löschen und abspielen.
        /// </summary>
        public IWesenPlaylist SelectedWesenPlaylist
        {
            get { return _selectedWesenPlaylist; }
            set
            {
                Set(ref _selectedWesenPlaylist, value);
            }
        }

        private string _selectedKategorie;
        /// <summary>
        /// Die Kategorie für die Zuordnung zu den Icons
        /// </summary>
        public string SelectedKategorie
        {
            get { return _selectedKategorie; }
            set
            {
                Set(ref _selectedKategorie, value);

                if (value == "Allgemein")
                    CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/speaker.png"));
                else
                    if (value == "Zauberformel")
                        CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/magie.png"));
                    else
                        if (value == "Nahkampf")
                            CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/nahkampf_02.png"));
                        else
                            if (value == "Fernkampf")
                                CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/fernkampf.png"));
                            else
                                if (value == "Tod")
                                    CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/tot.png"));
                                else
                                    if (value == "Wunde")
                                        CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/schaden.png"));                                    
            }
        }

        private string _currentKategorie = "Allgemein";
        /// <summary>
        /// Die Kategorie für die neu angelegte Wesen-Playlist-Verknüpfung
        /// </summary>
        public string CurrentKategorie
        {
            get { return _currentKategorie; }
            set
            {
                Set(ref _currentKategorie, value);
                //if (CurrentHeldenPlaylist != null && CurrentHeldenPlaylist.Kategorie != value)
                //{
                //    CurrentHeldenPlaylist.Kategorie = CurrentKategorie.ToString();
                //    Global.ContextAudio.Update<Held_Audio_Playlist>(CurrentHeldenPlaylist);
                //} else
                //    if (CurrentGegnerPlaylist != null && CurrentGegnerPlaylist.Kategorie != value)
                //    {
                //        CurrentGegnerPlaylist.Kategorie = CurrentKategorie.ToString();
                //        Global.ContextAudio.Update<GegnerBase_Audio_Playlist>(CurrentGegnerPlaylist);
                //    }
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

        private GegnerBase_Audio_Playlist _currentGegnerPlaylist;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentGegner")]
        public GegnerBase_Audio_Playlist CurrentGegnerPlaylist
        {
            get { return _currentGegnerPlaylist; }
            set { 
                if (value != null && Set(ref _currentGegnerPlaylist, value))
                    CurrentHeldenPlaylist = null; }
        }

        private Held_Audio_Playlist _currentHeldenPlaylist;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentHeld")]
        public Held_Audio_Playlist CurrentHeldenPlaylist
        {
            get { return _currentHeldenPlaylist; }
            set {
                if (value != null && Set(ref _currentHeldenPlaylist, value))
                {
                    CurrentGegnerPlaylist = null;
                    CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString() == value.Icon.ToString().Right(t.Source.ToString().Length));                   
                }
            }
        }

        private Image _currentIcon;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentHeldenPlaylist"), DependentProperty("CurrentGegnerPlaylist")]
        public Image CurrentIcon
        {
            get { return _currentIcon; }
            set
            {
                if (value != null && Set(ref _currentIcon, value))
                {
                    if (CurrentHeldenPlaylist != null &&
                        CurrentHeldenPlaylist.Icon != CurrentIcon.Source.ToString())
                    {                        
                        CurrentHeldenPlaylist.Icon = CurrentIcon.Source.ToString();
                        Global.ContextAudio.Update<Held_Audio_Playlist>(CurrentHeldenPlaylist);
                    } else
                        if (CurrentGegnerPlaylist != null &&
                        CurrentGegnerPlaylist.Icon != CurrentIcon.Source.ToString())
                        {
                            CurrentGegnerPlaylist.Icon = CurrentIcon.Source.ToString();
                            Global.ContextAudio.Update<GegnerBase_Audio_Playlist>(CurrentGegnerPlaylist);
                        }
                }
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
                Init();
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
        /// <summary>
        /// Für UI Anpassungen anhand des gesetzten Objektes
        /// </summary>
        /// 
        [DependentProperty("CurrentPlaylist")]
        public bool HatPlaylist
        {
            get {return CurrentPlaylist != null; }
        }


        #region Collections

        private ObservableCollection<string> _kategorieListe;
        public ObservableCollection<string> KategorieListe
        {
            get { return _kategorieListe; }
            set { Set(ref _kategorieListe, value); }
        }


        private ObservableCollection<Image> _iconListe;
        public ObservableCollection<Image> IconListe
        {
            get { return _iconListe; }
            set { Set(ref _iconListe, value); }
        }

        private ObservableCollection<IWesenPlaylist> _wesenPlaylistListe;
        public ObservableCollection<IWesenPlaylist> WesenPlaylistListe
        {
            get { return _wesenPlaylistListe; }
            set {
                Set(ref _wesenPlaylistListe, value);
                HeldenPlaylistListe = new ObservableCollection<IWesenPlaylist>(value.Where(t => t.Held != null));
                GegnerPlaylistListe = new ObservableCollection<IWesenPlaylist>(value.Where(t => t.GegnerBase != null));
            }
        }

        private ObservableCollection<IWesenPlaylist> _heldenPlaylistListe;
        public ObservableCollection<IWesenPlaylist> HeldenPlaylistListe
        {
            get { return _heldenPlaylistListe; }
            set { Set(ref _heldenPlaylistListe, value); }
        }

        private ObservableCollection<IWesenPlaylist> _gegnerPlaylistListe;
        public ObservableCollection<IWesenPlaylist> GegnerPlaylistListe
        {
            get { return _gegnerPlaylistListe; }
            set { Set(ref _gegnerPlaylistListe, value);  }
        }
        

        /// <summary>
        /// Alle Einträge. Muss eigentlich nicht gebunden werden.
        /// </summary>
        private ObservableCollection<IWesenPlaylist> _wesenListe;
        public ObservableCollection<IWesenPlaylist> WesenListe
        {
            get { return _wesenListe; }
            set
            {
                Set(ref _wesenListe, value);
            }
        }

        /// <summary>
        /// Alle Einträge. Muss eigentlich nicht gebunden werden.
        /// </summary>
        private ObservableCollection<Held> _heldenListe;
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

        private ICollectionView _filteredWesenListe = null;
        /// <summary>
        /// Gefilterte und sortierte Darstellung für Auswahllisten.
        /// </summary>
        public ICollectionView FilteredWesenListe
        {
            get
            {
                if (_filteredWesenListe == null)
                {
                    _filteredWesenListe = CollectionViewSource.GetDefaultView(WesenListe);
                    _filteredWesenListe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredWesenListe;
            }
            set
            {
                Set(ref _filteredWesenListe, value);
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
                    //HeldenPlaylistListe.ToList().ForEach(delegate (IWesenPlaylist iw)
                    //{
                    //    _filteredHeldenListe.
                    //    if (_filteredHeldenListe.Contains(iw)) _filteredHeldenListe -= iw;
                    //});

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
