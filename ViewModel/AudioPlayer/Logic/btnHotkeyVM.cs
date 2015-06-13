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

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class btnHotkeyVM : Base.ViewModelBase
    {
        //INotifyPropertyChanged
        #region //---- FELDER ----

        //public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel hkey;

        private double _volume = .5;
        public double volume 
        {
            get {return _volume;}
            set
            {
                _volume = value;
                TitelPlayList.ForEach(delegate(TitelPlay titelPlay)
                {
                    if (titelPlay.mp != null)
                        titelPlay.mp.Volume = value / 100;
                });
                OnChanged();
            }
        }

        private int _taste = -1;
        public int taste
        {
            get { return _taste; }
            set 
            { 
                _taste = value;
                tasteChar = Convert.ToChar(value);
                OnChanged();
            }
        }

        private char _tasteChar;
        public char tasteChar
        {
            get { return _tasteChar; }
            set
            { 
                _tasteChar = value;
                OnChanged();
            }
        }

        private Guid _aPlaylistGuid = Guid.Empty;
        public Guid aPlaylistGuid
        {
            get { return _aPlaylistGuid; }
            set
            {
                _aPlaylistGuid = value;
                OnChanged();
            }
        }



        private List<MediaPlayer> _mpList = new List<MediaPlayer>();
        public List<MediaPlayer> mpList
        {
            get { return _mpList; }
            set
            {
                _mpList = value;
                OnChanged();
            }
        }
        
        private Audio_Playlist _aPlaylist = null;
        public Audio_Playlist aPlaylist
        {
            get { return _aPlaylist; }
            set
            {
                _aPlaylist = value;
                OnChanged();
            }
        }

        private List<Audio_Playlist_Titel> _playableTitelList = null;
        public List<Audio_Playlist_Titel> PlayableTitelList
        {
            get 
            {
                if (_playableTitelList == null && aPlaylist != null)
                {
                    _playableTitelList = new List<Audio_Playlist_Titel>();
                    _playableTitelList.AddRange(aPlaylist.Audio_Playlist_Titel);
                }
                return _playableTitelList; 
            }
            set
            {
                _playableTitelList = value;
                OnChanged();
            }
        }
        
        #endregion

        #region //---- EIGENSCHAFTEN ----

        
        //Commands
        
        #endregion


        #region //---- KONSTRUKTOR ----

        public btnHotkeyVM()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            //_onlbEditorItemAdd = new Base.CommandBase(lbEditorItemAdd, null);                 
        }
        #endregion

        #region //---- INSTANZMETHODEN ----
        public AudioPlayerViewModel AudioVM;
        public List<DispatcherTimer> _timerTeilAbspielenList = new  List<DispatcherTimer>();

        private bool checkTitel(Audio_Titel titel)
        {
            if (//DriveInfo.GetDrives().FirstOrDefault(t => t.Name == titel.Pfad.Substring(0,3)) != null &&
                !Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei))
            {
                titel = AudioVM.setTitelStdPfad(titel);
                if (File.Exists(titel.Pfad + "\\" + titel.Datei))
                    Global.ContextAudio.Update<Audio_Titel>(titel);
            }

            if (Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei) ||
                    !Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)))
                return false;
            else
                return true;
        }

        public List<TitelPlay> TitelPlayList = new List<TitelPlay>();
        public class TitelPlay
        {
            public MediaPlayer mp;
            public DispatcherTimer dis;
        }

        private Base.CommandBase _onBtn = null;
        public Base.CommandBase OnBtn
        {
            get
            {
                if (_onBtn == null)
                    _onBtn = new Base.CommandBase(OnBtnClick, null);
                return _onBtn;
            }
        }
        public void OnBtnClick(object obj)
        {
            if (aPlaylistGuid != null)
            {
                PlayableTitelList.AddRange(aPlaylist.Audio_Playlist_Titel);
                if (PlayableTitelList.Count == 0) return;

                int zuspielen = (new Random()).Next(0, PlayableTitelList.Count);// aPlaylist.Audio_Playlist_Titel.Count );
                Audio_Playlist_Titel aPlayTitel = PlayableTitelList.ToList().ElementAt(zuspielen);

                while (!checkTitel(aPlayTitel.Audio_Titel))
                {
                    PlayableTitelList.Remove(aPlayTitel);
                    if (PlayableTitelList.Count == 0) return;

                    zuspielen = (new Random()).Next(0, PlayableTitelList.Count);
                    aPlayTitel = PlayableTitelList.ToList().ElementAt(zuspielen);
                }
                
                MediaPlayer mp = new MediaPlayer();
                mp.MediaEnded += mp_failed_ended;
                mp.MediaFailed += mp_failed_ended;

                TitelPlay titelPlay = new TitelPlay();
                TitelPlayList.Add(titelPlay);
                titelPlay.mp = mp;

                mpList.Add(mp);

                mp.Open(new Uri(aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei));
                mp.Volume = (double)(volume / 100);
                if (aPlayTitel.TeilAbspielen)
                {
                    mp.Position = TimeSpan.FromMilliseconds(0);
                    // Bis zu 1000ms warten um die Musikdatei auszulesen und die Laufzeit zu ermitteln
                    if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 1000))
                        mp.Position = TimeSpan.FromMilliseconds(aPlayTitel.TeilStart.Value);
                }

                mp.Play();
                if (aPlayTitel.TeilAbspielen)
                {
                    DispatcherTimer _timerTeilAbspielen = new DispatcherTimer();
                    _timerTeilAbspielen.Interval = TimeSpan.FromMilliseconds(20);
                    _timerTeilAbspielen.Tick += new EventHandler(_timerTeilAbspielen_Tick);       

                    _timerTeilAbspielenList.Add(_timerTeilAbspielen);
                    _timerTeilAbspielen.Tag = aPlayTitel.TeilEnde.Value;

                    titelPlay.dis = _timerTeilAbspielen;
                    _timerTeilAbspielen.Start();
                }
            }
        }
        public void _timerTeilAbspielen_Tick(object sender, EventArgs e)
        {
            double Ende = (double)(sender as DispatcherTimer).Tag;

            TitelPlay titelPlay = TitelPlayList.FirstOrDefault(t => t.dis == (sender as DispatcherTimer));
            if (titelPlay == null)
            {
                (sender as DispatcherTimer).Stop();
                return;
            }

            if (titelPlay.mp.Source == null ||
                titelPlay.mp.Position.TotalMilliseconds >= Ende)
            {
                if (titelPlay.mp.Source != null)
                    titelPlay.mp.Stop();
                (sender as DispatcherTimer).Stop();
            }
        }

        private void mp_failed_ended(object sender, EventArgs e)
        {
            try
            {
                TitelPlay titelPlay = TitelPlayList.FirstOrDefault(t => t.mp == (sender as MediaPlayer));
                if (titelPlay != null)
                    TitelPlayList.Remove(titelPlay);

                ((MediaPlayer)sender).Stop();
                ((MediaPlayer)sender).Close();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Stoppen und Schließen des Media-Player nach einem Fehler ist ein Fehler aufgetreten.", ex);
            }
        }


        #endregion


    }

}
