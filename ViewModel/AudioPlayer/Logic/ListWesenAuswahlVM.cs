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
using MeisterGeister.ViewModel.Kampf.Logic;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class ListWesenAuswahlVM : Base.ViewModelBase
    {


        //INotifyPropertyChanged
        #region //---- FELDER ----

        //private List<Audio_Playlist> _playlistListen;
        //public List<Audio_Playlist> PlaylistListen
        //{
        //    get { return _playlistListen; }
        //    set
        //    {
        //        _playlistListen = value;
        //        OnChanged();
        //    }
        //}
                
        private string _filter = "";
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnChanged();
                Refresh();
            }
        }
        
        #endregion

        #region //---- EIGENSCHAFTEN ----
        
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
        
        #endregion


        #region //---- KONSTRUKTOR ----

        public ListWesenAuswahlVM()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            //Init();
        }
        #endregion

        #region //---- INSTANZMETHODEN ----

        public AudioPlayerViewModel AudioVM;

        private bool _hatAudioPlaylistGUID;
        public bool HatAudioPlaylistGUID
        {
            get { return _hatAudioPlaylistGUID; }
            set
            {

                _hatAudioPlaylistGUID = value;

            }
        }

        private Audio_Playlist _aktPlaylist;
        public Audio_Playlist AktPlaylist
        {
            get{ return _aktPlaylist; }
            set
            {
                _aktPlaylist = value;
                if (HotGegnerListe != null && HotHeldenListe != null)
                    Refresh();
            }
        }


        //public class HotkeyWesen
        //{
        //    public Held held { get; set; }
        //    public GegnerBase gegnerbase { get; set; }
        //    public bool hatPlaylistGUID { get; set; }
        //}
        //private List<HotkeyWesen> _hotkeyWesenList;
        //public List<HotkeyWesen> HotkeyWesenList
        //{ get; set; }


        private List<Held> _hotHeldenListe;
        public List<Held> HotHeldenListe
        {
            get { return _hotHeldenListe; }
            set
            {
                _hotHeldenListe = value;
                OnChanged("HotHeldenListe");
            }
        }

        private List<GegnerBase> _hotGegnerListe;
        public List<GegnerBase> HotGegnerListe
        {
            get { return _hotGegnerListe; }
            set
            {
                _hotGegnerListe = value;
                OnChanged("HotGegnerListe");
            }
        }

        private List<Held> _filteredHotHeldenListe;
        public List<Held> FilteredHotHeldenListe
        {
            get { return _filteredHotHeldenListe; }
            set
            {
                _filteredHotHeldenListe = value;
                OnChanged("FilteredHotHeldenListe");
            }
        }

        private List<GegnerBase> _filteredHotGegnerListe;
        public List<GegnerBase> FilteredHotGegnerListe
        {
            get { return _filteredHotGegnerListe; }
            set
            {
                _filteredHotGegnerListe = value;
                OnChanged("FilteredHotGegnerListe");
            }
        }

        
        public void Init()
        {
            HotHeldenListe = Global.ContextHeld.Liste<Held>().OrderBy(h => h.Name).ToList();
            HotGegnerListe = Global.ContextHeld.Liste<GegnerBase>().OrderBy(h => h.Name).ToList();

            HotHeldenListe.FindAll(t => t.Audio_HotkeyWesen.Count > 0).ForEach(delegate(Held held)
            { held.Audio_HotkeyWesen.FirstOrDefault(t => t.Audio_PListGUID == AktPlaylist.Audio_PlaylistGUID).HatPlaylist = true; });

            HotGegnerListe.Where(t => t.Audio_HotkeyWesen.Count > 0).ToList().ForEach(delegate(GegnerBase gegner)
            { gegner.Audio_HotkeyWesen.FirstOrDefault(t => t.Audio_PListGUID == AktPlaylist.Audio_PlaylistGUID).HatPlaylist = true; });
            
            Refresh();
        }

        public void Refresh()
        {            
            List<Held> tempHeldenListe = new List<Held>();
            List<GegnerBase> tempGegnerListe = new List<GegnerBase>();

            tempHeldenListe = HotHeldenListe.Where(t => t.Name.ToLower().Contains(Filter.ToLower())).ToList();
            tempGegnerListe = HotGegnerListe.Where(t => t.Name.ToLower().Contains(Filter.ToLower())).ToList();

            //tempHeldenListe.FindAll(t => t.Audio_HotkeyWesen.Count > 0).ForEach(delegate(Held aHotHeld)
            //{
            //    aHotHeld.Audio_HotkeyWesen.ToList().ForEach(delegate(Audio_HotkeyWesen aHotWesen)
            //    { aHotWesen.HatPlaylist = (aHotWesen.Audio_PListGUID == AktPlaylist.Audio_PlaylistGUID); });
            //});

            //tempGegnerListe.FindAll(t => t.Audio_HotkeyWesen.Count > 0).ForEach(delegate(GegnerBase aHotGegner)
            //{
            //    aHotGegner.Audio_HotkeyWesen.ToList().ForEach(delegate(Audio_HotkeyWesen aHotWesen)
            //    {
            //        aHotWesen.HatPlaylist = (aHotWesen.Audio_PListGUID == AktPlaylist.Audio_PlaylistGUID);
            //    });
            //});            

            FilteredHotHeldenListe = tempHeldenListe;
            FilteredHotGegnerListe = tempGegnerListe;            
        }

        #endregion
    }
}
