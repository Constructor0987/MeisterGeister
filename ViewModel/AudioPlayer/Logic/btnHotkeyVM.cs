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
using MeisterGeister.Model;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using System.Windows.Media.Imaging;
using MeisterGeister.View.General;
using System.Windows.Media;
using System.Threading;
using MeisterGeister.Logic.Einstellung;
using VM = MeisterGeister.ViewModel.AudioPlayer;
using Un4seen.Bass;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class btnHotkeyVM : Base.ToolViewModelBase
    {

        #region //---- Klassen ----
        public class AudioData
        {
            public int audioStream = 0;

            public void Stop()
            {
                if (audioStream != 0)
                    Bass.BASS_ChannelStop(audioStream);
            }

            public bool isStopped()
            {
                return Bass.BASS_ChannelIsActive(audioStream) == BASSActive.BASS_ACTIVE_STOPPED;
            }
            public bool isPlaying()
            {
                return Bass.BASS_ChannelIsActive(audioStream) == BASSActive.BASS_ACTIVE_PLAYING;
            }
            public bool isPaused()
            {
                return Bass.BASS_ChannelIsActive(audioStream) == BASSActive.BASS_ACTIVE_PAUSED;
            }

            private SYNCPROC _mySync;
            public void Play()
            {
                if (_mySync == null)
                    _mySync = new SYNCPROC(EndSync);
                Bass.BASS_ChannelSetSync(audioStream, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_MIXTIME, 0, _mySync, IntPtr.Zero);

                Bass.BASS_ChannelPlay(audioStream, false);
            }
            private void EndSync(int handle, int channel, int data, IntPtr user)
            {
                //     Stop();
                //     Close();
            }

            public void Pause()
            {
                Bass.BASS_ChannelPause(audioStream);
            }
            public void Close()
            {
                if (!Bass.BASS_StreamFree(audioStream))
                {
                    BASSError berr = Bass.BASS_ErrorGetCode();
                    if (berr != 0)
                    { }
                }
            }

            public void setSpeed(double speed)
            {
                Bass.BASS_ChannelSetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_MUSIC_SPEED, Convert.ToSingle(speed));
            }

            private float lastVolume = 0;
            private bool muted;
            /// <summary>
            /// Setzt den Song auf MUTE oder setzt das MUTING des Songs zurück
            /// </summary>
            /// <param name="mute"></param>
            /// <returns></returns>
            public bool mute(bool mute)
            {
                if (mute == muted)
                    return true;

                bool rtn; // returnvalue

                if (mute) // mute
                {
                    lastVolume = getVolume(); // save current volume
                    rtn = setVolume(0.0f); // set volume to 0.0f (= mute)
                    muted = true; // set mute-state
                }
                else // unmute
                {
                    rtn = setVolume(lastVolume); // restore volume
                    muted = false; // set mute-state
                }

                return rtn; // returnvalue
            }

            /// <summary>
            /// Gibt das aktuelle Volume zurück
            /// </summary>
            /// <returns></returns>
            public float getVolume()
            {
                float vol = 0f;
                return (Bass.BASS_ChannelGetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_VOL, ref vol)) ? vol : 0f;
            }
            /// <summary>
            /// Setzt das Volume des Songs
            /// </summary>
            /// <param name="vol"></param>
            /// <returns></returns>
            public bool setVolume(double vol)
            {
                return Bass.BASS_ChannelSetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_VOL, Convert.ToSingle(vol));
            }
            /// <summary>
            /// Gibt die Länge des Songs in Millisekunden zurück
            /// </summary>
            /// <returns></returns>
            public double getLength()
            {
                return Bass.BASS_ChannelBytes2Seconds(audioStream, Bass.BASS_ChannelGetLength(audioStream) * 1000);
            }
            /// <summary>
            /// Gibt die aktuelle Position in Millisekunden im Song zurück
            /// </summary>
            /// <returns></returns>
            public double getPosition()
            {
                return Bass.BASS_ChannelBytes2Seconds(audioStream, Bass.BASS_ChannelGetPosition(audioStream) * 1000);
            }
            /// <summary>
            /// Setzt die aktuelle Position des Songs
            /// </summary>
            /// <param name="milliSec"></param>
            /// <returns></returns>
            public bool setPosition(double milliSec)
            {
                return Bass.BASS_ChannelSetPosition(audioStream, milliSec / 1000);
            }
            /// <summary>
            /// Gibt den Absoluten Dateinamen des Songs zurück
            /// </summary>
            /// <returns></returns>
            public string getFilename()
            {
                return (audioStream != 0) ? Bass.BASS_ChannelGetInfo(audioStream).filename : null;
            }

            public bool setFilename(string file)
            {
                audioStream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);

                return (audioStream != 0);
            }

        }
        #endregion


        //INotifyPropertyChanged
        #region //---- FELDER ----

        public List<string> stdPfad = new List<string>();

        private bool _aktiv = false;
        public bool Aktiv
        {
            get { return _aktiv; }
            set { Set(ref _aktiv, value); }
    }

        private double _volume = Einstellungen.GeneralHotkeyVolume;
        public double volume 
        {
            get { return Einstellungen.GeneralHotkeyVolume; }
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

        private List<AudioData> _mpList = new List<AudioData>();
        public List<AudioData> mpList
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
                if (PlayableTitelList != null) PlayableTitelList.RemoveRange(0, PlayableTitelList.Count);
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

         //   InitBASS();    
        }
        #endregion

        #region //---- INSTANZMETHODEN ----

        public void InitBASS()
        {
            int i = Bass.BASS_PluginLoad("basswma.dll");
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                BASS_INFO info = new BASS_INFO();
                Bass.BASS_GetInfo(info);
                Console.WriteLine(info.ToString());
                Dictionary<int, string> lst = Bass.BASS_PluginLoadDirectory(Environment.CurrentDirectory + @"\Audio\BASS\Plugin\");
            }
        }
        public List<DispatcherTimer> _timerTeilAbspielenList = new  List<DispatcherTimer>();

        private bool checkTitel(Audio_Titel titel)
        {
            if (titel == null) return false;
            if (!Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei))
            {
                titel = setTitelStdPfad(titel);
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
            public AudioData mp;
            public DispatcherTimer dis;
        }

        public double lastPos = 0;

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

                int zuspielen = (new Random()).Next(0, PlayableTitelList.Count);
                Audio_Playlist_Titel aPlayTitel = PlayableTitelList.ToList().ElementAt(zuspielen);

                while (!checkTitel(aPlayTitel.Audio_Titel))
                {
                    PlayableTitelList.Remove(aPlayTitel);
                    if (PlayableTitelList.Count == 0) return;

                    zuspielen = (new Random()).Next(0, PlayableTitelList.Count);
                    aPlayTitel = PlayableTitelList.ToList().ElementAt(zuspielen);
                }

                AudioData mp = new AudioData();
               // mp.MediaEnded += mp_failed_ended;
               // mp.MediaFailed += mp_failed_ended;

                TitelPlay titelPlay = new TitelPlay();
                TitelPlayList.Add(titelPlay);
                titelPlay.mp = mp;

                mpList.Add(mp);
                
                mp.setFilename(aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei);
                mp.setVolume((double)(volume / 100));
                if (aPlayTitel.TeilAbspielen)
                {
                    mp.setPosition(aPlayTitel.TeilStart.Value);
                    //mp.setPosition(0); 
                    //MyTimer.start_timer();
                    //// Bis zu 1000ms warten um die Musikdatei auszulesen und die Laufzeit zu ermitteln
                    //if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 1000))
                    //    mp.Position = TimeSpan.FromMilliseconds(aPlayTitel.TeilStart.Value);
                    //MyTimer.stop_timer("Hotkey_OnBtnClick");
                }

                mp.Play();
                Aktiv = true;
               // if (aPlayTitel.TeilAbspielen)
                {
                    DispatcherTimer _timerTeilAbspielen = new DispatcherTimer();
                    _timerTeilAbspielen.Interval = TimeSpan.FromMilliseconds(20);
                    _timerTeilAbspielen.Tick += new EventHandler(_timerTeilAbspielen_Tick);       

                    _timerTeilAbspielenList.Add(_timerTeilAbspielen);
                    _timerTeilAbspielen.Tag = (aPlayTitel.TeilAbspielen) ? 
                        aPlayTitel.TeilEnde.Value : aPlayTitel.Länge != 0 ? 
                            aPlayTitel.Länge == mp.getLength() ? 
                                aPlayTitel.Länge : mp.getLength() : mp.getLength();

                    titelPlay.dis = _timerTeilAbspielen;
                    _timerTeilAbspielen.Start();
                }                
            }
        }

        double lastpos = 0;
        public void _timerTeilAbspielen_Tick(object sender, EventArgs e)
        {
            double Ende = (double)(sender as DispatcherTimer).Tag;

            TitelPlay titelPlay = TitelPlayList.FirstOrDefault(t => t.dis == (sender as DispatcherTimer));
            if (titelPlay == null)
            {
                (sender as DispatcherTimer).Stop();
                return;
            }
            double aktPos = titelPlay.mp.getPosition();
            if (titelPlay.mp == null ||
                aktPos >= Ende ||
                lastpos == aktPos && aktPos != 0)
            {
                if (titelPlay.mp != null)
                {
                    titelPlay.mp.Stop();
                    titelPlay.mp.Close();
                }
                (sender as DispatcherTimer).Stop();
                Aktiv = false;
            }
            else
                lastpos = titelPlay.mp.getPosition();
        }

        private void mp_failed_ended(object sender, EventArgs e)
        {
            try
            {
                TitelPlay titelPlay = TitelPlayList.FirstOrDefault(t => t.mp == (sender as AudioData));
                if (titelPlay != null)
                    TitelPlayList.Remove(titelPlay);

                ((AudioData)sender).Stop();
                ((AudioData)sender).Close();
                Aktiv = false;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Stoppen und Schließen des Media-Player nach einem Fehler ist ein Fehler aufgetreten.", ex);
            }
        }


        public void setStdPfad()
        {
            char[] charsToTrim = { '\\' };
            if (stdPfad.Count > 0) stdPfad.RemoveRange(0, stdPfad.Count);
            stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
        }

        public Audio_Titel setTitelStdPfad(Audio_Titel aTitel)
        {
            if (stdPfad.Count == 0)
                setStdPfad();
            char[] charsToTrim = { '\\' };
            //Check Titel -> Pfad vorhanden ansonsten Standard-Pfad hinzufügen
            if (File.Exists(aTitel.Pfad + "\\" + aTitel.Datei))
            {
                foreach (string pfad in stdPfad)
                {
                    if (pfad == aTitel.Pfad)
                        return aTitel;

                    if (aTitel.Pfad != null && (aTitel.Pfad + "\\" + aTitel.Datei).Contains(pfad))
                    {
                        aTitel.Datei = (aTitel.Pfad.EndsWith("\\") ? aTitel.Pfad + aTitel.Datei : aTitel.Pfad + "\\" + aTitel.Datei).
                            Substring(pfad.EndsWith("\\") ? pfad.Length : pfad.Length + 1);
                        aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                        return aTitel;
                    }
                }
                // Pfad noch kein Standard-Pfad
                if (ViewHelper.Confirm("Audio-Pfad ist kein Standard-Pfad", "Der Pfad der Audio-Datei konnte nicht unter den Standard-Pfaden gefunden werden." +
                    Environment.NewLine + "In dieser Konstellation ist es nicht zulässig, den Titel abzuspielen." + Environment.NewLine +
                    "Soll der Pfad mit in die Standard-Pfade integriert werden?" + Environment.NewLine + Environment.NewLine + "Neuer Pfad:     " + aTitel.Pfad))
                {
                    MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis =
                        MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis + "|" + aTitel.Pfad;
                    setStdPfad();
                }
                return aTitel;
            }

            //Pfad+Titel nicht gefunden -> Check Titel in einem anderen Standard-Pfad
            foreach (string pfad in stdPfad)
            {
                if (aTitel.Datei == null && aTitel.Pfad != null)
                {
                    aTitel.Datei = aTitel.Pfad;
                    aTitel.Pfad = "";
                }
                if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + aTitel.Datei))
                {
                    aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                    return aTitel;
                }

                if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + System.IO.Path.GetFileName(aTitel.Datei)))
                {
                    aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                    aTitel.Datei = System.IO.Path.GetFileName(aTitel.Datei);
                    return aTitel;
                }
            }

            if (Einstellungen.AudioInAnderemPfadSuchen)
            {
                //ab hier: kein Std.-Pfad ist gültig -> Check in jedem Std.-Pfad mit Suche incl. Unterverzeichnisse nach dem Dateinamen
                string gesuchteDatei = System.IO.Path.GetFileName(stdPfad[0].TrimEnd(charsToTrim) + "\\" + aTitel.Datei);
                foreach (string pfad in stdPfad)
                {
                    if (pfad != "C:\\" && Directory.Exists(pfad))
                    {
                        string[] pfad_datei = Directory.GetFiles(pfad.TrimEnd(charsToTrim), gesuchteDatei, SearchOption.AllDirectories);
                        if (pfad_datei.Length > 0)
                        {
                            aTitel.Pfad = System.IO.Path.GetDirectoryName(pfad_datei[0]);
                            aTitel.Datei = System.IO.Path.GetFileName(pfad_datei[0]);
                            aTitel = setTitelStdPfad(aTitel);
                            return aTitel;
                        }
                    }
                }
            }

            if (aTitel.Pfad == null) aTitel.Pfad = "";
            if (aTitel.Pfad == "" || aTitel.Datei == null)
            {
                string pfadDatei = aTitel.Pfad != null || aTitel.Pfad != "" ? aTitel.Pfad : "";
                if (pfadDatei != "" && !pfadDatei.EndsWith("\\"))
                    pfadDatei = pfadDatei + "\\";
                if (aTitel.Datei != null)
                    pfadDatei = pfadDatei + aTitel.Datei;

                aTitel.Pfad = System.IO.Path.GetDirectoryName(pfadDatei);
                aTitel.Datei = System.IO.Path.GetFileName(pfadDatei);
            }
            return aTitel;
        }

        #endregion
    }
}
