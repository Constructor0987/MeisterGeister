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
                if (mp != null)
                    mp.Volume = value/100;
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

        private MediaPlayer _mp = new MediaPlayer();
        public MediaPlayer mp
        {
            get { return _mp; }
            set
            {
                _mp = value;
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


        //Commands
        //private Base.CommandBase _onlbEditorItemAdd;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        
        //Commands
        
        #endregion


        #region //---- KONSTRUKTOR ----

        public btnHotkeyVM()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            mp.MediaEnded += mp_failed_ended;
            mp.MediaFailed += mp_failed_ended;
            //_onlbEditorItemAdd = new Base.CommandBase(lbEditorItemAdd, null);
            _timerTeilAbspielen.Interval = TimeSpan.FromMilliseconds(20);
            _timerTeilAbspielen.Tick += new EventHandler(_timerTeilAbspielen_Tick);
        }
        #endregion

        #region //---- INSTANZMETHODEN ----
        
        public DispatcherTimer _timerTeilAbspielen = new DispatcherTimer();

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
                //Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.VM.aPlaylistGuid);

                int zuspielen = (new Random()).Next(0, aPlaylist.Audio_Playlist_Titel.Count );
                Audio_Playlist_Titel aPlayTitel = aPlaylist.Audio_Playlist_Titel.ToList().ElementAt(zuspielen);

                if (mp.Source != null)
                {
                    _timerTeilAbspielen_Tick(_timerTeilAbspielen, null);
                }
                mp.Open(new Uri(aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei));
                mp.Volume = (double)(volume / 100);
                if (aPlayTitel.TeilAbspielen)
                {
                    if (_timerTeilAbspielen.IsEnabled)
                        _timerTeilAbspielen.Stop();
                    mp.Position = TimeSpan.FromMilliseconds(0);
                    // Bis zu 1000ms warten um die Musikdatei auszulesen und die Laufzeit zu ermitteln
                    if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 1000))
                        mp.Position = TimeSpan.FromMilliseconds(aPlayTitel.TeilStart.Value);
                }

                mp.Play();
                if (aPlayTitel.TeilAbspielen)
                {
                    _timerTeilAbspielen.Tag = aPlayTitel.TeilEnde.Value;     
                    _timerTeilAbspielen.Start();
                }
            }
        }
        public void _timerTeilAbspielen_Tick(object sender, EventArgs e)
        {
            double Ende = (double)_timerTeilAbspielen.Tag;

            if (mp.Source == null ||
                mp.Position.TotalMilliseconds >= Ende ||
                mp.Position.TotalMilliseconds == 0)
            {
                if (mp.Source != null) 
                    mp.Stop();
                _timerTeilAbspielen.Stop();
            }
        }

        private void mp_failed_ended(object sender, EventArgs e)
        {
            try
            {
                ((MediaPlayer)sender).Stop();
                ((MediaPlayer)sender).Close();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Stoppen und Schließen des Media-Player nach einem Fehler ist ein Fehler aufgetreten.", ex);
            }
        }

        
        //private Base.CommandBase _onBtnLöschenLbEditor = null;
        //public Base.CommandBase OnBtnLöschenLbEditor
        //{
        //    get
        //    {
        //        if (_onBtnLöschenLbEditor == null)
        //            _onBtnLöschenLbEditor = new Base.CommandBase(BtnLöschenLbEditor, null);
        //        return _onBtnLöschenLbEditor;
        //    }
        //}
        //void BtnLöschenLbEditor(object obj)
        //{
        //}


        #endregion


    }

}
