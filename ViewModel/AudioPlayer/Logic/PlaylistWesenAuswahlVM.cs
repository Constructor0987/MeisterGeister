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
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/magie.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/tot.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/nahkampf_01.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/fernkampf.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/schaden.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/schild.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/alchimie.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/alchimie.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/angeln.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/artefakt.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/audio2.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/beidhändig.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/foliant.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/halbschwert.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/jagd.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/kartenzeichnen.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/mechanik.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/muenze.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/munition.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/nahkampf_02.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/nahkampf_03.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/notiz.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/parierwaffe.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/ruestung.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/schmiede.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/sprache.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/ueberanstrengung.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/wuerfelbecher.png", UriKind.Relative)) });
            iconlist.Add(new Image() { Source = new BitmapImage(new Uri("/DSA%20MeisterGeister;component/Images/Icons/zauberzeichen.png", UriKind.Relative)) });

            return iconlist;
        }

        public void Init()
        {
            if (IconListe == null || IconListe.Count == 0)
                IconListe = new ObservableCollection<Image>(GetIcons());
            if (CurrentIcon == null)
                CurrentIcon = IconListe[0];
            if (CurrentIcon2 == null)
                CurrentIcon2 = IconListe[0];

            if (KategorieListe == null || KategorieListe.Count == 0)
                KategorieListe = new ObservableCollection<string>(GetKategorien());

            if (HatPlaylist)
            {
                PlaylistListe = new ObservableCollection<Audio_Playlist>(Global.ContextAudio.PlaylistListe.Where(t => !t.Hintergrundmusik));
                HeldenListe = new ObservableCollection<Held>(Global.ContextHeld.Liste<Held>());
                GegnerListe = new ObservableCollection<GegnerBase>(Global.ContextHeld.Liste<GegnerBase>());
                //daten 
                WesenPlaylistListe = new ObservableCollection<IWesenPlaylist>(CurrentPlaylist.Held_Audio_Playlist.AsEnumerable<IWesenPlaylist>().Union(CurrentPlaylist.GegnerBase_Audio_Playlist).AsEnumerable<IWesenPlaylist>());                         
            }
            else
            {
                PlaylistListe = new ObservableCollection<Audio_Playlist>(Global.ContextAudio.PlaylistListe.Where(t => !t.Hintergrundmusik));
                HeldenListe = new ObservableCollection<Held>(Global.ContextHeld.Liste<Held>());
                GegnerListe = new ObservableCollection<GegnerBase>(Global.ContextHeld.Liste<GegnerBase>());
                WesenOrientiertSelected = true;
                if (HatGegner)
                {
                    Gegner2Aktiv = true;
                    SelectedGegner2 = FilteredGegner2Liste.Cast<GegnerBase>().FirstOrDefault(t => t.Name == CurrentGegner.Name);
                }
                if (HatHeld)
                {
                    Held2Aktiv = true;
                    SelectedHeld2 = FilteredHelden2Liste.Cast<Held>().FirstOrDefault(t => t.Name == CurrentHeld.Name);
                }           
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

        private Base.CommandBase _onCopyWesenPlaylist2 = null;
        public Base.CommandBase OnCopyWesenPlaylist2
        {
            get
            {
                if (_onCopyWesenPlaylist2 == null)
                    _onCopyWesenPlaylist2 = new Base.CommandBase(CopyWesenPlaylist2, null);
                return _onCopyWesenPlaylist2;
            }
        }
        private void CopyWesenPlaylist2(object obj)
        {
            WesenSpeicher = WesenPlaylistListe2;
        }

        private Base.CommandBase _onPasteWesenPlaylist2 = null;
        public Base.CommandBase OnPasteWesenPlaylist2
        {
            get
            {
                if (_onPasteWesenPlaylist2 == null)
                    _onPasteWesenPlaylist2 = new Base.CommandBase(PasteWesenPlaylist2, null);
                return _onPasteWesenPlaylist2;
            }
        }
        private void PasteWesenPlaylist2(object obj)
        {
            foreach (IWesenPlaylist w in WesenSpeicher)
            {
                if (w.Held != null)
                {
                    if (SelectedHeld2.Held_Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID ==
                        (w as Held_Audio_Playlist).Audio_Playlist.Audio_PlaylistGUID) == null)
                    {
                        Held_Audio_Playlist wpl = Global.ContextAudio.New<Held_Audio_Playlist>();
                        wpl.Audio_Playlist = (w as Held_Audio_Playlist).Audio_Playlist;
                        wpl.Held = (w as Held_Audio_Playlist).Held;
                        wpl.Kategorie = (w as Held_Audio_Playlist).Kategorie;
                        wpl.Icon = (w as Held_Audio_Playlist).Icon;
                        SelectedHeld2.Held_Audio_Playlist.Add(wpl);
                        if (WesenPlaylistListe != null) WesenPlaylistListe.Add(wpl);
                        WesenPlaylistListe2.Add(wpl);
                    }
                }
                else if (w.GegnerBase != null)
                {
                    if (SelectedGegner2.GegnerBase_Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == 
                        (w as GegnerBase_Audio_Playlist).Audio_Playlist.Audio_PlaylistGUID) == null)
                    {
                        GegnerBase_Audio_Playlist wpl = Global.ContextAudio.New<GegnerBase_Audio_Playlist>();
                        wpl.Audio_Playlist = (w as GegnerBase_Audio_Playlist).Audio_Playlist;
                        wpl.GegnerBase = (w as GegnerBase_Audio_Playlist).GegnerBase;
                        wpl.Kategorie = (w as GegnerBase_Audio_Playlist).Kategorie;
                        wpl.Icon = (w as GegnerBase_Audio_Playlist).Icon;
                        SelectedGegner2.GegnerBase_Audio_Playlist.Add(wpl);
                        if (WesenPlaylistListe != null) WesenPlaylistListe.Add(wpl);
                        WesenPlaylistListe2.Add(wpl);
                    }
                }
            }
            Global.ContextAudio.Save();
        }
        
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
            speedbtnAudio.aPlaylistGuid = CurrentPlaylist.Audio_PlaylistGUID;
            speedbtnAudio.aPlaylist = CurrentPlaylist;
            speedbtnAudio.OnBtnClick(speedbtnAudio);
        }

        private Base.CommandBase _onPlaylistAnspielen2Click = null;
        public Base.CommandBase OnPlaylistAnspielen2Click
        {
            get
            {
                if (_onPlaylistAnspielen2Click == null)
                    _onPlaylistAnspielen2Click = new Base.CommandBase(PlaylistAnspielen2, null);
                return _onPlaylistAnspielen2Click;
            }
        }
        private void PlaylistAnspielen2(object obj)
        {
            if (obj == null || !(obj is IWesenPlaylist))
                return;

            speedbtnAudio.aPlaylistGuid = (obj as IWesenPlaylist).Audio_Playlist.Audio_PlaylistGUID;
            speedbtnAudio.aPlaylist = (obj as IWesenPlaylist).Audio_Playlist;
            speedbtnAudio.OnBtnClick(speedbtnAudio);
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

        private Base.CommandBase _onGegner2FilterLöschen = null;
        public Base.CommandBase OnGegner2FilterLöschen
        {
            get
            {
                if (_onGegner2FilterLöschen == null)
                    _onGegner2FilterLöschen = new Base.CommandBase(Gegner2FilterLöschen, null);
                return _onGegner2FilterLöschen;
            }
        }
        void Gegner2FilterLöschen(object obj)
        {
            Gegner2Filter = "";
        }

        private Base.CommandBase _onHelden2FilterLöschen = null;
        public Base.CommandBase OnHelden2FilterLöschen
        {
            get
            {
                if (_onHelden2FilterLöschen == null)
                    _onHelden2FilterLöschen = new Base.CommandBase(Helden2FilterLöschen, null);
                return _onHelden2FilterLöschen;
            }
        }
        void Helden2FilterLöschen(object obj)
        {
            Helden2Filter = "";
        }

        private Base.CommandBase _onPlaylist2FilterLöschen = null;
        public Base.CommandBase OnPlaylist2FilterLöschen
        {
            get
            {
                if (_onPlaylist2FilterLöschen == null)
                    _onPlaylist2FilterLöschen = new Base.CommandBase(Playlist2FilterLöschen, null);
                return _onPlaylist2FilterLöschen;
            }
        }
        void Playlist2FilterLöschen(object obj)
        {
            Playlist2Filter = "";
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
            string k = CurrentKategorie; 
            if (pl == null || (h == null && g == null) || k == null )
                return;
            IWesenPlaylist iwpl = null;
            if(h != null)
            {
                if (h.Held_Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID ==
                        pl.Audio_PlaylistGUID) == null)
                {
                    Held_Audio_Playlist wpl = Global.ContextAudio.New<Held_Audio_Playlist>();
                    wpl.Audio_PlaylistGUID = pl.Audio_PlaylistGUID;
                    wpl.Audio_Playlist = pl;
                    wpl.HeldGUID = h.HeldGUID;
                    wpl.Held = h;
                    wpl.Kategorie = k;
                    wpl.Icon = CurrentIcon.Source.ToString();
                    h.Held_Audio_Playlist.Add(wpl);
                    iwpl = wpl;
                }
            }
            else if(g !=  null)
            {
                if (g.GegnerBase_Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID ==
                        pl.Audio_PlaylistGUID) == null)
                {
                    GegnerBase_Audio_Playlist wpl = Global.ContextAudio.New<GegnerBase_Audio_Playlist>();
                    wpl.Audio_PlaylistGUID = pl.Audio_PlaylistGUID;
                    wpl.Audio_Playlist = pl;
                    wpl.GegnerBaseGUID = g.GegnerBaseGUID;
                    wpl.GegnerBase = g;
                    wpl.Kategorie = k;
                    wpl.Icon = CurrentIcon.Source.ToString();
                    g.GegnerBase_Audio_Playlist.Add(wpl);
                    iwpl = wpl;
                }
            }
            WesenPlaylistListe.Add(iwpl);
            if (WesenPlaylistListe2 != null) WesenPlaylistListe2.Add(iwpl);
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

            if (SelectedWesenPlaylist == null)
                return;
            if(SelectedWesenPlaylist.HatGegner)
                Global.ContextAudio.Delete<GegnerBase_Audio_Playlist>(SelectedWesenPlaylist as GegnerBase_Audio_Playlist);
            else if (SelectedWesenPlaylist.HatHeld)
                Global.ContextAudio.Delete<Held_Audio_Playlist>(SelectedWesenPlaylist as Held_Audio_Playlist);
            if (WesenPlaylistListe != null)
            {
                WesenPlaylistListe.Remove(SelectedWesenPlaylist);
                WesenPlaylistListe = WesenPlaylistListe;
            }
            if (WesenPlaylistListe2 != null)
            {
                WesenPlaylistListe2.Remove(SelectedWesenPlaylist);
                WesenPlaylistListe2 = WesenPlaylistListe2;
            }
            
            SelectedWesenPlaylist = null;
        }

        //Add und delete von Held_Audio_Playlist und GegnerBase_Audio_Playlist
        private Base.CommandBase _onAddWesenPlaylist2;
        public Base.CommandBase OnAddWesenPlaylist2
        {
            get
            {
                if (_onAddWesenPlaylist2 == null)
                    _onAddWesenPlaylist2 = new Base.CommandBase(AddWesenPlaylist2, null);
                return _onAddWesenPlaylist2;
            }
        }
        public void AddWesenPlaylist2(object obj)
        {
            Audio_Playlist pl = SelectedPlaylist2;

            Held h = (Held2Aktiv) ? SelectedHeld2 : null;
            GegnerBase g = (Gegner2Aktiv) ? SelectedGegner2 : null;

            string k = SelectedKategorie2 != null ? SelectedKategorie2 : CurrentKategorie2;
            if (pl == null || (h == null && g == null) || k == null)
                return;
            IWesenPlaylist iwpl = null;
            if (h != null)
            {
                if (h.Held_Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID ==
                        pl.Audio_PlaylistGUID) == null)
                {
                    Held_Audio_Playlist wpl = Global.ContextAudio.New<Held_Audio_Playlist>();
                    wpl.Audio_PlaylistGUID = pl.Audio_PlaylistGUID;
                    wpl.Audio_Playlist = pl;
                    wpl.HeldGUID = h.HeldGUID;
                    wpl.Held = h;
                    wpl.Kategorie = k;
                    wpl.Icon = CurrentIcon2.Source.ToString();
                    //TODO check ob schon vorhanden
                    h.Held_Audio_Playlist.Add(wpl);
                    iwpl = wpl;
                }
            }
            else if (g != null)
            {
                if (g.GegnerBase_Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID ==
                        pl.Audio_PlaylistGUID) == null)
                {
                    GegnerBase_Audio_Playlist wpl = Global.ContextAudio.New<GegnerBase_Audio_Playlist>();
                    wpl.Audio_PlaylistGUID = pl.Audio_PlaylistGUID;
                    wpl.Audio_Playlist = pl;
                    wpl.GegnerBaseGUID = g.GegnerBaseGUID;
                    wpl.GegnerBase = g;
                    wpl.Kategorie = k;
                    wpl.Icon = CurrentIcon2.Source.ToString();
                    //TODO check ob schon vorhanden
                    g.GegnerBase_Audio_Playlist.Add(wpl);
                    iwpl = wpl;
                }
            }
            if (iwpl != null)
            {
                if (WesenPlaylistListe != null) WesenPlaylistListe.Add(iwpl);
                if (WesenPlaylistListe2 != null) WesenPlaylistListe2.Add(iwpl);
                Global.ContextAudio.Save();
            }
            SelectedPlaylist2 = null;
            Init();
        }
        #endregion

        #region Properties
        
        private btnHotkeyVM _speedbtnAudio = new btnHotkeyVM();
        private btnHotkeyVM speedbtnAudio
        {
            get { return _speedbtnAudio; }
            set { Set(ref  _speedbtnAudio, value); }
        }

        private string _wesenFilter = "";
        public string WesenFilter
        {
            get { return _wesenFilter; }
            set
            {
                if (Set(ref _wesenFilter, value))
                    ApplyWesenFilter();
            }
        }
        
        private string _heldenFilter = "";
        public string HeldenFilter
        {
            get { return _heldenFilter; }
            set
            {
                if (Set(ref _heldenFilter, value))
                    ApplyHeldenFilter();
            }
        }

        private string _gegnerFilter = "";
        public string GegnerFilter
        {
            get { return _gegnerFilter; }
            set
            {
                if (Set(ref _gegnerFilter, value))
                    ApplyGegnerFilter();   
            }
        }

        private string _helden2Filter = "";
        public string Helden2Filter
        {
            get { return _helden2Filter; }
            set
            {
                if (Set(ref _helden2Filter, value))
                    ApplyHelden2Filter(); 
            }
        }

        private string _gegner2Filter = "";
        public string Gegner2Filter
        {
            get { return _gegner2Filter; }
            set
            {
                if (Set(ref _gegner2Filter, value))
                    ApplyGegner2Filter(); 
            }
        }

        private string _playlist2Filter = "";
        public string Playlist2Filter
        {
            get { return _playlist2Filter; }
            set
            {
                if (Set(ref _playlist2Filter, value))
                    ApplyPlaylist2Filter(); 
            }
        }

        void ApplyFilter()
        {
            if (FilteredGegnerListe != null) FilteredGegnerListe.Filter = (g => (g is GegnerBase) && (g as GegnerBase).Name.ToLower().ContainsIgnoreCase(_gegnerFilter.ToLower()));
            if (FilteredHeldenListe != null) FilteredHeldenListe.Filter = (g => (g is Held) && (g as Held).Name.ToLower().ContainsIgnoreCase(_heldenFilter.ToLower()));

            if (FilteredWesenListe != null) FilteredWesenListe.Filter = (g => (g is IKämpfer) && (g as IKämpfer).Name.ToLower().ContainsIgnoreCase(_gegnerFilter.ToLower()));
          //  FilteredPlaylistListe.Filter = (g => (g is Audio_Playlist) && (g as Audio_Playlist).Name.ContainsIgnoreCase(_filter));
        }

        void ApplyWesenFilter()
        {
            if (FilteredWesenListe != null) FilteredWesenListe.Filter = (g => (g is Held) && (g as Held).Name.ToLower().ContainsIgnoreCase(_heldenFilter.ToLower()));
        }
        
        void ApplyHeldenFilter()
        {
            if (FilteredHeldenListe != null) FilteredHeldenListe.Filter = (g => (g is Held) && (g as Held).Name.ToLower().ContainsIgnoreCase(_heldenFilter.ToLower()));
        }

        void ApplyGegnerFilter()
        {
            if (FilteredGegnerListe != null) FilteredGegnerListe.Filter = (g => (g is GegnerBase) && (g as GegnerBase).Name.ToLower().ContainsIgnoreCase(_gegnerFilter.ToLower()));            
        }

        void ApplyHelden2Filter()
        {
            if (FilteredHelden2Liste != null) FilteredHelden2Liste.Filter = (g => (g is Held) && (g as Held).Name.ToLower().ContainsIgnoreCase(_helden2Filter.ToLower()));
        }

        void ApplyGegner2Filter()
        {
            if (FilteredGegner2Liste != null) FilteredGegner2Liste.Filter = (g => (g is GegnerBase) && (g as GegnerBase).Name.ToLower().ContainsIgnoreCase(_gegner2Filter.ToLower()));            
        }

        void ApplyPlaylist2Filter()
        {
            if (FilteredPlaylist2Liste != null) FilteredPlaylist2Liste.Filter = (g => (g is Audio_Playlist) && (g as Audio_Playlist).Name.ToLower().ContainsIgnoreCase(_playlist2Filter.ToLower()));
        }
        

        //zum abspielen benötigt
        public AudioPlayerViewModel AudioVM;


        private ObservableCollection<IWesenPlaylist> _wesenSpeicher = null;
        /// <summary>
        /// Die WesenPlaylist im Zwischenspeicher.
        /// </summary>
        public ObservableCollection<IWesenPlaylist> WesenSpeicher
        {
            get { return _wesenSpeicher; }
            set { Set(ref _wesenSpeicher, value); }
        }


        private IWesenPlaylist _selectedWesenPlaylist;
        /// <summary>
        /// Die aktuell ausgewählte WesenPlaylist zum löschen und abspielen.
        /// </summary>
        public IWesenPlaylist SelectedWesenPlaylist
        {
            get { return _selectedWesenPlaylist; }
            set { Set(ref _selectedWesenPlaylist, value); }
        }

        private Image GetKategorieIcon(string s,  Image CurrentIcon)
        {
            Image i = null;
            if (s == "Allgemein")
                    i = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/speaker.png"));
                else
                if (s == "Zauberformel")
                    i = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/magie.png"));
                    else
                    if (s == "Nahkampf")
                        i = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/nahkampf_02.png"));
                        else
                        if (s == "Fernkampf")
                            i = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/fernkampf.png"));
                            else
                            if (s == "Tod")
                                i = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/tot.png"));
                                else
                                if (s == "Wunde")
                                    i = IconListe.FirstOrDefault(t => t.Source.ToString().Contains("/schaden.png"));
            return (i != null) ? i : CurrentIcon;
        }

        //private string _selectedKategorie;
        ///// <summary>
        ///// Die Kategorie für die Zuordnung zu den Icons
        ///// </summary>
        //public string SelectedKategorie
        //{
        //    get { return _selectedKategorie; }
        //    set
        //    {
        //        Set(ref _selectedKategorie, value);
        //        //CurrentIcon = GetKategorieIcon(value, CurrentIcon);
        //        //CurrentKategorie = value;                
        //    }
        //}

        private string _selectedKategorie2;
        /// <summary>
        /// Die Kategorie für die Zuordnung zu den Icons
        /// </summary>
        public string SelectedKategorie2
        {
            get { return _selectedKategorie2; }
            set
            {
                Set(ref _selectedKategorie2, value);
              //  CurrentIcon2 = GetKategorieIcon(value, CurrentIcon2);                                           
            }
        }

        private string _currentKategorie = "Allgemein";
        /// <summary>
        /// Die Kategorie für die neu angelegte Wesen-Playlist-Verknüpfung
        /// </summary>
        [DependentProperty("CurrentHeldenPlaylist"), DependentProperty("CurrentGegnerPlaylist")]
        public string CurrentKategorie
        {
            get { return _currentKategorie; }
            set
            {
                if (value != null && Set(ref _currentKategorie, value))
                {
                    //OnChanged();
                    if (CurrentHeldenPlaylist != null &&
                        CurrentHeldenPlaylist.Kategorie != CurrentKategorie)
                    {
                        //CurrentHeldenPlaylist.Kategorie = CurrentKategorie.ToString();
                        //Global.ContextAudio.Update<Held_Audio_Playlist>(CurrentHeldenPlaylist);
                    }
                    else
                        if (CurrentGegnerPlaylist != null &&
                        CurrentGegnerPlaylist.Kategorie != CurrentKategorie)
                        {
                     //       CurrentGegnerPlaylist.Kategorie = CurrentKategorie.ToString();
                     //       Global.ContextAudio.Update<GegnerBase_Audio_Playlist>(CurrentGegnerPlaylist);
                        }
                }
            }
        }

        private string _currentKategorie2 = "Allgemein";
        /// <summary>
        /// Die Kategorie für die neu angelegte Wesen-Playlist-Verknüpfung
        /// </summary>
        public string CurrentKategorie2
        {
            get { return _currentKategorie2; }
            set { Set(ref _currentKategorie2, value); }
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
                _currentGegner = null;
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
                _currentHeld = null;
            }
        }

        private bool _held2Aktiv;        
        public bool Held2Aktiv
        {
            get { return _held2Aktiv; }
            set 
            { 
                Set(ref _held2Aktiv, value);
                SelectedHeld2 = null;
                SelectedGegner2 = null;
            }
        }

        private bool _gegner2Aktiv;        
        public bool Gegner2Aktiv
        {
            get { return _gegner2Aktiv; }
            set 
            { 
                Set(ref _gegner2Aktiv, value);
                SelectedHeld2 = null;
                SelectedGegner2 = null;
            }
        }

        private bool _wesenOrientiertSelected;
        public bool WesenOrientiertSelected
        {
            get { return _wesenOrientiertSelected; }
            set { Set(ref _wesenOrientiertSelected, value); }
        }

        private Held _selectedHeld2;
        /// <summary>
        /// Für das Hinzufügen von neuen Einträgen aus einer Listbox
        /// </summary>
        public Held SelectedHeld2
        {
            get { return _selectedHeld2; }
            set 
            { 
                Set(ref _selectedHeld2, value);
                WesenPlaylistListe2 = (value == null) ? null : new ObservableCollection<IWesenPlaylist>(value.Held_Audio_Playlist.AsEnumerable<IWesenPlaylist>());
            }
        }

        private GegnerBase _selectedGegner2;
        /// <summary>
        /// Für das Hinzufügen von neuen Einträgen aus einer Listbox
        /// </summary>
        public GegnerBase SelectedGegner2
        {
            get { return _selectedGegner2; }
            set 
            { 
                Set(ref _selectedGegner2, value);
                WesenPlaylistListe2 = (value == null)? null: new ObservableCollection<IWesenPlaylist>(value.GegnerBase_Audio_Playlist.AsEnumerable<IWesenPlaylist>());
            }
        }


        private Audio_Playlist _selectedPlaylist2;
        /// <summary>
        /// Für das Hinzufügen von neuen Einträgen aus einer Listbox
        /// </summary>
        public Audio_Playlist SelectedPlaylist2
        {
            get { return _selectedPlaylist2; }
            set { Set(ref _selectedPlaylist2, value); }
        }

        private Audio_Playlist _selectedPlaylist;
        /// <summary>
        /// Für das Hinzufügen von neuen Einträgen aus einer Listbox
        /// </summary>
        public Audio_Playlist SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set { Set(ref _selectedPlaylist, value); }
        }

        private GegnerBase_Audio_Playlist _currentGegnerPlaylist;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentGegner")]
        public GegnerBase_Audio_Playlist CurrentGegnerPlaylist
        {
            get { return _currentGegnerPlaylist; }
            set 
            {
                if (value != null && Set(ref _currentGegnerPlaylist, value))
                {
                    CurrentHeldenPlaylist = null;
                    CurrentIcon = IconListe.FirstOrDefault(t => t.Source.ToString() == value.Icon.ToString().Right(t.Source.ToString().Length));
                    CurrentKategorie = KategorieListe.FirstOrDefault(t => t == value.Kategorie); 
                }
            }
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
                    CurrentKategorie = KategorieListe.FirstOrDefault(t => t == value.Kategorie); 
                }
            }
        }


        private IWesenPlaylist _currentWesenPlaylist2;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentHeld2"), DependentProperty("CurrentGegner2")]       
        public IWesenPlaylist CurrentWesenPlaylist2
        {
            get { return _currentWesenPlaylist2; }
            set {
                if (value != null && Set(ref _currentWesenPlaylist2, value))
                {
                    if (Held2Aktiv)
                    {
                        CurrentGegnerPlaylist2 = null;
                        CurrentHeldenPlaylist2 = value as Held_Audio_Playlist;
                        CurrentIcon2 = IconListe.FirstOrDefault(t => t.Source.ToString() == CurrentHeldenPlaylist2.Icon.ToString().Right(t.Source.ToString().Length));
                        CurrentKategorie2 = KategorieListe.FirstOrDefault(t => t == CurrentHeldenPlaylist2.Kategorie); 
                    }
                    else
                        if (Gegner2Aktiv)
                        {
                            CurrentHeldenPlaylist2 = null;
                            CurrentGegnerPlaylist2 = value as GegnerBase_Audio_Playlist;
                            CurrentIcon2 = IconListe.FirstOrDefault(t => t.Source.ToString() == CurrentGegnerPlaylist2.Icon.ToString().Right(t.Source.ToString().Length));
                            CurrentKategorie2 = KategorieListe.FirstOrDefault(t => t == CurrentGegnerPlaylist2.Kategorie); 
                        }
                }
            }
        }
        private GegnerBase_Audio_Playlist _currentGegnerPlaylist2;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentGegner2")]
        public GegnerBase_Audio_Playlist CurrentGegnerPlaylist2
        {
            get { return _currentGegnerPlaylist2; }
            set
            {
                if (value != null && Set(ref _currentGegnerPlaylist2, value))
                {
                    CurrentHeldenPlaylist2 = null;
                    CurrentIcon2 = IconListe.FirstOrDefault(t => t.Source.ToString() == value.Icon.ToString().Right(t.Source.ToString().Length));
                    CurrentKategorie2 = KategorieListe.FirstOrDefault(t => t == value.Kategorie);
                }
            }
        }

        private Held_Audio_Playlist _currentHeldenPlaylist2;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentHeld2")]
        public Held_Audio_Playlist CurrentHeldenPlaylist2
        {
            get { return _currentHeldenPlaylist2; }
            set {
                if (value != null && Set(ref _currentHeldenPlaylist2, value))
                {
                    CurrentGegnerPlaylist2 = null;
                    CurrentKategorie2 = KategorieListe.FirstOrDefault(t => t == value.Kategorie);                   
                    CurrentIcon2 = IconListe.FirstOrDefault(t => t.Source.ToString() == value.Icon.ToString().Right(t.Source.ToString().Length)); 
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

        private Image _currentIcon2;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        [DependentProperty("CurrentHeldenPlaylist2"), DependentProperty("CurrentGegnerPlaylist2")]
        public Image CurrentIcon2
        {
            get { return _currentIcon2; }
            set
            {
                if (value != null && Set(ref _currentIcon2, value))
                {
                    if (Held2Aktiv &&
                        (CurrentHeldenPlaylist2 != null &&
                            CurrentHeldenPlaylist2.Icon != CurrentIcon2.Source.ToString()))
                    {
                            CurrentHeldenPlaylist2.Icon = CurrentIcon2.Source.ToString();
                            Global.ContextAudio.Update<Held_Audio_Playlist>(CurrentHeldenPlaylist2);
                    }
                    else
                        if (Gegner2Aktiv &&
                        (CurrentGegnerPlaylist2 != null &&
                            CurrentGegnerPlaylist2.Icon != CurrentIcon2.Source.ToString()))
                    {
                        CurrentGegnerPlaylist2.Icon = CurrentIcon2.Source.ToString();
                        Global.ContextAudio.Update<GegnerBase_Audio_Playlist>(CurrentGegnerPlaylist2);
                    }
                }
            }
        }

        private Audio_Playlist _currentPlaylist2;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        public Audio_Playlist CurrentPlaylist2
        {
            get { return _currentPlaylist2; }
            set
            {
                Set(ref _currentPlaylist2, value);
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

        private GegnerBase _currentGegner2;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        public GegnerBase CurrentGegner2
        {
            get { return _currentGegner2; }
            set
            {
                if (value != null && Set(ref _currentGegner2, value))
                {
                    CurrentHeld2 = null;
                    CurrentPlaylist2 = null;
                }
            }
        }

        private Held _currentHeld2;
        /// <summary>
        /// Wenn gesetzt, werden diesem Objekt neue Einträge zugeordnet.
        /// </summary>
        public Held CurrentHeld2
        {
            get { return _currentHeld2; }
            set
            {
                if (value != null && Set(ref _currentHeld2, value))
                {
                    CurrentGegner2 = null;
                    CurrentPlaylist2 = null;
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
            set 
            { 
                Set(ref _iconListe, value);
                if (value != null && CurrentIcon ==  null)
                    CurrentIcon = value[0];
            }
        }

        private ObservableCollection<IWesenPlaylist> _wesenPlaylistListe;
        public ObservableCollection<IWesenPlaylist> WesenPlaylistListe
        {
            get { return _wesenPlaylistListe; }
            set {
                Set(ref _wesenPlaylistListe, value);

                _currentHeldenPlaylist = null;
                _currentGegnerPlaylist = null;
                HeldenPlaylistListe = new ObservableCollection<IWesenPlaylist>(value.Where(t => t.Held != null));
                GegnerPlaylistListe = new ObservableCollection<IWesenPlaylist>(value.Where(t => t.GegnerBase != null));
            }
        }

        private ObservableCollection<IWesenPlaylist> _wesenPlaylistListe2;
        public ObservableCollection<IWesenPlaylist> WesenPlaylistListe2
        {
            get { return _wesenPlaylistListe2; }
            set {
                Set(ref _wesenPlaylistListe2, value);
                
               // HeldenPlaylist2Liste = new ObservableCollection<IWesenPlaylist>(value.Where(t => t.Held != null));
              //  GegnerPlaylist2Liste = new ObservableCollection<IWesenPlaylist>(value.Where(t => t.GegnerBase != null));
            }
        }

        private ObservableCollection<IWesenPlaylist> _heldenPlaylist2Liste;
        public ObservableCollection<IWesenPlaylist> HeldenPlaylist2Liste
        {
            get { return _heldenPlaylist2Liste; }
            set { Set(ref _heldenPlaylist2Liste, value); }
        }

        private ObservableCollection<IWesenPlaylist> _gegnerPlaylist2Liste;
        public ObservableCollection<IWesenPlaylist> GegnerPlaylist2Liste
        {
            get { return _gegnerPlaylist2Liste; }
            set { Set(ref _gegnerPlaylist2Liste, value); }
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

        ///// <summary>
        ///// Alle Einträge. Muss eigentlich nicht gebunden werden.
        ///// </summary>
        //private ObservableCollection<Held> _helden2Liste;
        //public ObservableCollection<Held> Helden2Liste
        //{
        //    get { return _helden2Liste; }
        //    set
        //    {
        //        Set(ref _helden2Liste, value);
        //    }
        //}

        //private ObservableCollection<GegnerBase> _gegner2Liste;
        ///// <summary>
        ///// Alle Einträge. Muss eigentlich nicht gebunden werden.
        ///// </summary>
        //public ObservableCollection<GegnerBase> Gegner2Liste
        //{
        //    get { return _gegner2Liste; }
        //    set
        //    {
        //        Set(ref _gegner2Liste, value);
        //    }
        //}

        //private ObservableCollection<Audio_Playlist> _playlist2Liste;
        ///// <summary>
        ///// Alle Einträge. Muss eigentlich nicht gebunden werden.
        ///// </summary>
        //public ObservableCollection<Audio_Playlist> Playlist2Liste
        //{
        //    get { return _playlist2Liste; }
        //    set
        //    {
        //        Set(ref _playlist2Liste, value);
        //    }
        //}

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
                    if (_filteredWesenListe != null) _filteredWesenListe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
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
                    if (_filteredHeldenListe != null) 
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
                    if (_filteredGegnerListe != null) 
                        _filteredGegnerListe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredGegnerListe;
            }
            set
            {
                Set(ref _filteredGegnerListe, value);
            }
        }

        private ICollectionView _filteredHelden2Liste = null;
        /// <summary>
        /// Gefilterte und sortierte Darstellung für Auswahllisten.
        /// </summary>
        public ICollectionView FilteredHelden2Liste
        {
            get {
                if (_filteredHelden2Liste == null)
                {
                    _filteredHelden2Liste = CollectionViewSource.GetDefaultView(HeldenListe);
                    if (_filteredHelden2Liste != null) 
                        _filteredHelden2Liste.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredHelden2Liste; 
            }
            set
            {
                Set(ref _filteredHelden2Liste, value);
            }
        }

        private ICollectionView _filteredGegner2Liste = null;
        /// <summary>
        /// Gefilterte und sortierte Darstellung für Auswahllisten.
        /// </summary>
        public ICollectionView FilteredGegner2Liste
        {
            get
            {
                if (_filteredGegner2Liste == null)
                {
                    _filteredGegner2Liste = CollectionViewSource.GetDefaultView(GegnerListe);
                    if (_filteredGegner2Liste != null) 
                        _filteredGegner2Liste.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredGegner2Liste;
            }
            set
            {
                Set(ref _filteredGegner2Liste, value);
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
                    if (_filteredPlaylistListe != null) 
                        _filteredPlaylistListe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredPlaylistListe;
            }
            set
            {
                Set(ref _filteredPlaylistListe, value);
            }
        }

        private ICollectionView _filteredPlaylist2Liste = null;
        /// <summary>
        /// Gefilterte und sortierte Darstellung für Auswahllisten.
        /// </summary>
        public ICollectionView FilteredPlaylist2Liste
        {
            get
            {
                if (_filteredPlaylist2Liste == null)
                {
                    _filteredPlaylist2Liste = CollectionViewSource.GetDefaultView(PlaylistListe);
                    if (_filteredPlaylist2Liste != null) 
                        _filteredPlaylist2Liste.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                return _filteredPlaylist2Liste;
            }
            set
            {
                Set(ref _filteredPlaylist2Liste, value);
            }
        }
        #endregion

        #endregion
    }
}
