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

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class btnHotkeyVM : Base.ToolViewModelBase
    {
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
        }
        #endregion

        #region //---- INSTANZMETHODEN ----
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
            public MediaPlayer mp;
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
                    MyTimer.start_timer();
                    // Bis zu 1000ms warten um die Musikdatei auszulesen und die Laufzeit zu ermitteln
                    if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 1000))
                        mp.Position = TimeSpan.FromMilliseconds(aPlayTitel.TeilStart.Value);
                    MyTimer.stop_timer("Hotkey_OnBtnClick");
                }

                mp.Play();
                Aktiv = true;
                if (aPlayTitel.TeilAbspielen)
                {
                    DispatcherTimer _timerTeilAbspielen = new DispatcherTimer();
                    _timerTeilAbspielen.Interval = TimeSpan.FromMilliseconds(20);
                    _timerTeilAbspielen.Tick += new EventHandler(_timerTeilAbspielen_Tick);       

                    _timerTeilAbspielenList.Add(_timerTeilAbspielen);
                    _timerTeilAbspielen.Tag = (aPlayTitel.TeilAbspielen)? aPlayTitel.TeilEnde.Value: aPlayTitel.Länge != 0? aPlayTitel.Länge: 100000;

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
                Aktiv = false;
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
