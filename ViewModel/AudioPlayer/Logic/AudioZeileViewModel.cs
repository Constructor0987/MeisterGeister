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
using MeisterGeister.ViewModel.Basar.Logic;
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using MeisterGeister.Logic.Umrechner;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.AudioPlayer;
using System.Windows.Input;
using System.IO;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class AudioZeileViewModel  : Base.ViewModelBase
    {

        #region //---- FELDER ----
        public AudioPlayerViewModel PlayerVM;
        private string _suchtext = string.Empty;

        bool _checked = true;
        Audio_Playlist_Titel _aPlayTitel = new Audio_Playlist_Titel();
        public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;

        #endregion

        public DoubleCollection SliderTicks
        {
            get
            {
                return new DoubleCollection 
                    {0, 100, 200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000, 3000, 4000, 5000, 7500, 8000, 9000, 10000, 15000, 
                     20000, 25000, 30000, 40000, 50000, 60000, 90000, 120000, 180000, 240000, 300000, 450000, 600000, 900000}; 
            }            
        }

        private void audiozeileLbi_MouseMove(object sender, MouseEventArgs e)
        {
            AudioZeile aZeile = ((sender as ListBoxItem).Parent as Grid).Parent as AudioZeile;
            PlayerVM.audioZeileMouseOverDropped =  PlayerVM.LbEditorAudioZeilenListe.IndexOf(this);//aZeile);
            if (PlayerVM.pointerZeileDragDrop == null)
                return;

            Point mousePos = e.GetPosition(null);
            Vector diff = ((Point)PlayerVM.pointerZeileDragDrop) - mousePos;

            Point mp = Mouse.GetPosition(aZeile);
            PlayerVM.audioZeileMouseOverDropped = PlayerVM.LbEditorAudioZeilenListe.IndexOf(this);//aZeile);
            //Abfrage bei gedrückter Maustaste, wenn im Vorderen Bereich und nicht auf der ProgressBar (um Teilabspielen zu editieren)
            if (e.LeftButton == MouseButtonState.Pressed &&
                (mp.X < 35 + 10 + aZeile.pbarTitel.ActualWidth) && mp.X > 0 &&
                ((mp.Y > 0 && mp.Y < aZeile.lbiEditorRow.ActualHeight / 2 - aZeile.pbarTitel.ActualHeight / 2) ||
                 (mp.Y > aZeile.lbiEditorRow.ActualHeight / 2 + aZeile.pbarTitel.ActualHeight / 2)))
            {
                // Initialisiere drag & drop Operation
                DataObject dragData = new DataObject("meineAudioZeile", aZeile);
                DragDrop.DoDragDrop(aZeile, dragData, DragDropEffects.Copy);
                PlayerVM.pointerZeileDragDrop = null;
            }
        }



        #region //---- EIGENSCHAFTEN ----

        [DependentProperty("PlayerVM"), DependentProperty("Reihenfolge"), DependentProperty("AktPlaylistTitel")]
        public bool IstErsteZeile
        {
            get { return aPlayTitel.Reihenfolge == 0; }
            set { OnChanged(); }
        }

        [DependentProperty("PlayerVM"), DependentProperty("Reihenfolge")]
        public bool IstLetzteZeile
        {
            get { return PlayerVM == null? false: (PlayerVM.AktKlangPlaylist.Audio_Playlist_Titel.Count == aPlayTitel.Reihenfolge + 1); }
            set { OnChanged(); }
        }


        public bool aPlayTitelVolumeChange
        {
            get { return _aPlayTitel.VolumeChange; }
            set { OnChanged(); }            
        }

        private Base.CommandBase _onVolumeChange = null;
        public Base.CommandBase OnVolumeChange
        {
            get
            {
                if (_onVolumeChange == null)
                    _onVolumeChange = new Base.CommandBase(VolumeChange, null);
                return _onVolumeChange;
            }
        }
        void VolumeChange(object obj)
        {
            aPlayTitel.VolumeChange = !aPlayTitel.VolumeChange;
            PlayerVM.IsVolumeChangeChecked = !(PlayerVM.AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.VolumeChange) != PlayerVM.AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.Aktiv));
            OnChanged("aPlayTitelVolumeChange");
        }

        public bool aPlayTitelPauseChange
        {
            get { return aPlayTitel.PauseChange; }
            set { OnChanged(); }
        }

        private Base.CommandBase _onPauseChange = null;
        public Base.CommandBase OnPauseChange
        {
            get
            {
                if (_onPauseChange == null)
                    _onPauseChange = new Base.CommandBase(PauseChange, null);
                return _onPauseChange;
            }
        }
        void PauseChange(object obj)
        {
            aPlayTitel.PauseChange = !aPlayTitel.PauseChange;
            PlayerVM.IsPausenZeitChangeChecked = !(PlayerVM.AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.PauseChange) != PlayerVM.AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.Aktiv));
            OnChanged("aPlayTitelPauseChange");
        }


        public int PauseMinIncValue
        {
            get
            {
                return (aPlayTitel.PauseMin >= 10000) ? 5000 :
                         (aPlayTitel.PauseMin >= 2000) ? 1000 : 200;
            }
        }

        public int PauseMaxIncValue
        {
            get
            {
                return (aPlayTitel.PauseMax >= 10000) ? 5000 :
                         (aPlayTitel.PauseMax >= 2000) ? 1000 : 200;
            }
        }

        private int _aPlayTitelPauseMin = 100;
        public int aPlayTitelPauseMin
        {
            get { return _aPlayTitelPauseMin; }
            set
            {
                _aPlayTitelPauseMin = value;
                aPlayTitel.PauseMin = value;
                if (aPlayTitelPauseMax < value)
                    aPlayTitelPauseMax = value;
                OnChanged();
                OnChanged("PauseMinIncValue");
            }
        }

        private int _aPlayTitelPauseMax = 100;
        public int aPlayTitelPauseMax
        {
            get { return _aPlayTitelPauseMax; }
            set
            {
                _aPlayTitelPauseMax = value;
                aPlayTitel.PauseMax = value;
                if (aPlayTitelPauseMin > value)
                    aPlayTitelPauseMin = value;
                OnChanged();
                OnChanged("PauseMaxIncValue");
            }
        }

        private bool _fileNotExist = false;
        public bool FileNotExist
        {
            get { return _fileNotExist; }
            set
            {
                _fileNotExist = value;
                OnChanged();
            }
        }

        private bool _filePlayable = true;
        public bool FilePlayable
        {
            get { return _filePlayable; }
            set
            {
                _filePlayable = value;
                OnChanged();
            }
        }

        private AudioZeileViewModel _aZeile = null;
        public AudioZeileViewModel AZeile
        {
            get { return _aZeile; }
            set
            {
                _aZeile = value;
                if (value.aPlayTitel.Länge != 0 && value.TitelMaximum != value.aPlayTitel.Länge)
                    value.TitelMaximum = value.aPlayTitel.Länge;
                OnChanged();
            }
        }

        public long aPlayTitelPause
        {
            get { return aPlayTitel.Pause; }
            set
            {
                aPlayTitel.Pause = value;
                OnChanged();
                OnChanged("ZeigeAPlayTitelPause");
            }
        }
                      

        private Audio_Playlist _aktKlangPlaylist = null;
        public Audio_Playlist AktKlangPlaylist
        {
            get { return _aktKlangPlaylist; }
            set
            {
                _aktKlangPlaylist = value;
                OnChanged();
            }
        }
                      
        
        public ObservableCollection<MenuItem> DisplayPath
        {
            get {           
                ObservableCollection<MenuItem> mItemListe = new ObservableCollection<MenuItem>();
                foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe.OrderBy(t => t.Name))
                {
                    MenuItem mitem = new MenuItem();
                    mitem.Header = aPlaylist.Name;
                    mitem.Tag = aPlaylist;
                    mItemListe.Add(mitem);
                }
                return mItemListe;
            }
            set { OnChanged(); }
        }
        
        public string ZeigeAPlayTitelPause
        {
            get
            {
                return (aPlayTitel.Pause < 1000 ? aPlayTitel.Pause + " ms" : aPlayTitel.Pause < 60000 ?
                    Math.Round((double)aPlayTitel.Pause / 1000, 2).ToString() + " sek." :
                    aPlayTitel.Pause / 60000 + " min.");
            }
        }

        public string ToolTipPlaySpeed
        {
            get
            {
                return ( "Abspielgeschwindigkeit: " +
                  (aPlayTitel.Speed == .1 ? "sehr langsam" :
                   aPlayTitel.Speed == .5 ? "langsam" :
                   aPlayTitel.Speed == .75 ? "gedrosselt" :
                   aPlayTitel.Speed == 1 ? "normal" :
                   aPlayTitel.Speed == 2 ? "erhöht" :
                   aPlayTitel.Speed == 3 ? "schnell" :
                   aPlayTitel.Speed == 4 ? "sehr schnell" :
                   aPlayTitel.Speed == 5 ? "ultra schnell" : "nicht definiert"));
            }
        }
        
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        private double _titelMinimum = 0;
        public double TitelMinimum
        {
            get { return _titelMinimum; }
            set 
            { 
                _titelMinimum = value;
                OnChanged();
            }
        }

        private double _titelMaximum = 100000;
        public double TitelMaximum
        {
            get { return _titelMaximum; }
            set 
            {
                _titelMaximum = (value == 0)? 100000: value;
                OnChanged();
            }
        }

        double _progress = 0;
        public double Progress
        {
            get { return _progress; }
            set 
            {
                _progress = value;
                OnChanged();
            }
        }

        [DependentProperty("aPlayTitel")]
        public double aPlayTitelLänge
        {
            get
            {
                return (_aPlayTitel.Audio_Titel != null && (_aPlayTitel.Audio_Titel.Länge != null && _aPlayTitel.Audio_Titel.Länge.Value != 0) ? 
                _aPlayTitel.Audio_Titel.Länge.Value : 100000); }
            set { OnChanged(); }
        }

        [DependentProperty("aPlayTitel")]
        public double aPlayTitelTeilStart
        {
            get { return (_aPlayTitel.TeilStart != null ? _aPlayTitel.TeilStart.Value: 0);}
            set { OnChanged(); }        
        }

        [DependentProperty("aPlayTitel")]
        public bool aPlayTitelTeilAbspielen
        {
            get { return _aPlayTitel.TeilAbspielen;}
            set
            {
                OnChanged();
                OnChanged("Audio_Playlist");
            }
        }
        
        [DependentProperty("aPlayTitel")]
        public double aPlayTitelTeilEnde
        {
            get { return (_aPlayTitel.TeilEnde != null ? _aPlayTitel.TeilEnde.Value : 100000); }
            set { OnChanged(); }
        }


        public Audio_Playlist_Titel aPlayTitel
        {
            get { return _aPlayTitel; }
            set
            {
                _aPlayTitel = value;
                OnChanged();
            }
        }

        void OnSliderMouseUp(object obj)
        {
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlayTitel);
        }

        #endregion

        #region //---- Commands ----

        private Base.CommandBase _onKopierenZuPlaylist;
        public Base.CommandBase OnKopierenZuPlaylist
        {
            get
            {
                if (_onKopierenZuPlaylist == null)
                    _onKopierenZuPlaylist = new Base.CommandBase(KopierenZuPlaylist, null);
                return _onKopierenZuPlaylist;
            }
        }
        void KopierenZuPlaylist(object obj)
        {
            Audio_Playlist zielPlaylist = (((MenuItem)obj).Tag) as Audio_Playlist;
            if (zielPlaylist != null)
            {
                List<string> gedroppteDateien = new List<string>();
                gedroppteDateien.Add(aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei);

                PlayerVM._DateienAufnehmen(gedroppteDateien, null, this, zielPlaylist, 0, false);
                Global.ContextAudio.Update<Audio_Playlist>(zielPlaylist);
            }
            else
            {
                ViewHelper.Popup("Die Playliste konnte nicht gefunden werden." + Environment.NewLine + Environment.NewLine + "Vorgang abgebrochen");
            }
        }

        private Base.CommandBase _onDateiPfadÄndern;
        public Base.CommandBase OnDateiPfadÄndern
        {
            get
            {
                if (_onDateiPfadÄndern == null)
                    _onDateiPfadÄndern = new Base.CommandBase(DateiPfadÄndern, null);
                return _onDateiPfadÄndern;
            }
        }
        void DateiPfadÄndern(object obj)
        {
            // Datei-Bezug ändern 
            Audio_Titel aTitel = PlayerVM.setTitelStdPfad(aPlayTitel.Audio_Titel);

            FileInfo fi = new FileInfo(aTitel.Pfad + @"\" + aTitel.Datei);
            string aktDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = fi.DirectoryName;
            string datei = ViewHelper.ChooseFile("Datei auswählen", fi.Name, false, PlayerVM.validExt);
            Environment.CurrentDirectory = aktDir;

            if (datei != null)
            {
                aTitel.Pfad = System.IO.Path.GetDirectoryName(datei);
                aTitel.Datei = System.IO.Path.GetFileName(datei);

                string s = System.IO.Path.GetFileNameWithoutExtension(datei);
                aTitel.Name = s.Length > 100 ? s.Substring(0, 99) : s;
                aPlayTitel.Audio_Titel = PlayerVM.setTitelStdPfad(aTitel);
                Global.ContextAudio.Update<Audio_Titel>(aPlayTitel.Audio_Titel);
            }
        }


        private Base.CommandBase _onDateiPfadÖffnen;
        public Base.CommandBase  OnDateiPfadÖffnen
        {
            get
            {
                if (_onDateiPfadÖffnen == null)
                    _onDateiPfadÖffnen = new Base.CommandBase(DateiPfadÖffnen, null);
                return _onDateiPfadÖffnen;
            }
        }
        void DateiPfadÖffnen(object obj)
        {
            if (Directory.Exists(aPlayTitel.Audio_Titel.Pfad) &&
                                        File.Exists(aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei))
            {
                FileInfo fi2 = new FileInfo(aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei);
                System.Diagnostics.Process.Start("explorer.exe", "/e,/select," + fi2.DirectoryName + "\\" + @"""" + fi2.Name + @"""");
            }
            else
                ViewHelper.Popup("Die Datei bzw. das Verzeichnis konnte nicht gefunden werden");
        }

        private Base.CommandBase _onTitelDuplizieren;
        public Base.CommandBase  OnTitelDuplizieren
        {
            get
            {
                if (_onTitelDuplizieren == null)
                    _onTitelDuplizieren = new Base.CommandBase(TitelDuplizieren, null);
                return _onTitelDuplizieren;
            }
        }
        void TitelDuplizieren(object obj)
        {
            try
            {
                List<string> gedroppteDateien = new List<string>();
                gedroppteDateien.Add(aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei);

                UpdateReihenfolge();

                PlayerVM._DateienAufnehmen(gedroppteDateien, null, this, _aPlayTitel.Audio_Playlist, this.aPlayTitel.Reihenfolge, false);
                Global.ContextAudio.Update<Audio_Playlist>(_aPlayTitel.Audio_Playlist);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fehler" + Environment.NewLine + "Beim Ausführen der Duplizierung in eine andere Playliste ist ein Fehler aufgetreten", ex);
            }
        }


        private Base.CommandBase _onMouseUpCommand;
        public Base.CommandBase  OnMouseUpCommand
        {
            get
            {
                if (_onMouseUpCommand == null)
                    _onMouseUpCommand = new Base.CommandBase(MouseUpCommand, null);
                return _onMouseUpCommand;
            }
        }
        void MouseUpCommand(object obj)
        {
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlayTitel);
            Global.ContextAudio.Update<Audio_Titel>(aPlayTitel.Audio_Titel);
        }
        
        private Base.CommandBase _onPlayTitelTeilAbspielen;
        public Base.CommandBase  OnPlayTitelTeilAbspielen
        {
            get
            {
                if (_onPlayTitelTeilAbspielen == null)
                    _onPlayTitelTeilAbspielen = new Base.CommandBase(PlayTitelTeilAbspielen, null);
                return _onPlayTitelTeilAbspielen;
            }
        }
        void PlayTitelTeilAbspielen(object obj)
        {
            if (aPlayTitel.TeilAbspielen)
            {
                aPlayTitel.TeilStart = 0;
                aPlayTitel.TeilEnde = aPlayTitel.Audio_Titel.Länge;                
            }
            aPlayTitelTeilAbspielen = aPlayTitel.TeilAbspielen;
        }
        

        private Base.CommandBase _onBtnAktiv;
        public Base.CommandBase OnBtnAktiv
        {
            get
            {
                if (_onBtnAktiv == null)
                    _onBtnAktiv = new Base.CommandBase(BtnAktiv, null);
                return _onBtnAktiv;
            }
        }
        void BtnAktiv(object obj)
        {
            if (aPlayTitel.Aktiv)
            {
                if (grpobj != null && 
                    !grpobj.NochZuSpielen.Contains(aPlayTitel.Audio_TitelGUID))
                {
                    for (int i = 0; i <= aPlayTitel.Rating; i++)
                        grpobj.NochZuSpielen.Add(aPlayTitel.Audio_TitelGUID);
                }
                PlayerVM.AllTitelAktiv = PlayerVM.AllTitelAktiv;
            }
            else
            {
                if (grpobj != null) grpobj.NochZuSpielen.RemoveAll(t => t.Equals(aPlayTitel.Audio_TitelGUID));
                PlayerVM.AllTitelAktiv = false;
            }
        }


        private Base.CommandBase _onBtnGewichtung;
        public Base.CommandBase OnBtnGewichtung
        {
            get
            {
                if (_onBtnGewichtung == null)
                    _onBtnGewichtung = new Base.CommandBase(BtnGewichtung, null);
                return _onBtnGewichtung;
            }
        }
        void BtnGewichtung(object obj)
        {
            aPlayTitel.Rating = aPlayTitel.Rating < 5 ? aPlayTitel.Rating = aPlayTitel.Rating + 1 : aPlayTitel.Rating = 0;
            OnChanged("Rating");
        }

        private Base.CommandBase _onAudioZeileRemove;
        public Base.CommandBase OnAudioZeileRemove
        {
            get
            {
                if (_onAudioZeileRemove == null)
                    _onAudioZeileRemove = new Base.CommandBase(AudioZeileRemove, null);        
                return _onAudioZeileRemove;
            }
        }
        void AudioZeileRemove(object obj)
        {
            Global.ContextAudio.RemoveTitelFromPlaylist(aPlayTitel);
            PlayerVM.LbEditorAudioZeilenListe.Remove(this);
            PlayerVM.LadeFilteredAudioZeilen();
            PlayerVM._chkAnzDateienInDir(PlayerVM.AktKlangPlaylist);
        }

        private Base.CommandBase _onReihenfolgeMoveUp;
        public Base.CommandBase OnReihenfolgeMoveUp
        {
            get
            {
                if (_onReihenfolgeMoveUp == null)
                    _onReihenfolgeMoveUp = new Base.CommandBase(ReihenfolgeMoveUp, null);
                return _onReihenfolgeMoveUp;
            }
        }
        void ReihenfolgeMoveUp(object obj)
        {
            try
            {
                UpdateReihenfolge();
                if (this.aPlayTitel.Reihenfolge > 0)
                    MoveItem(this.aPlayTitel, -1);
                PlayerVM.FilteredLbEditorAudioZeilenListe = PlayerVM.FilteredLbEditorAudioZeilenListe.OrderBy(t => t.aPlayTitel.Reihenfolge).ToList();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken des Buttons 'btnMoveUp' ist ein Fehler aufgetreten", ex);
            }
        }

        private Base.CommandBase _onReihenfolgeMoveDown;
        public Base.CommandBase OnReihenfolgeMoveDown
        {
            get
            {
                if (_onReihenfolgeMoveDown == null)
                    _onReihenfolgeMoveDown = new Base.CommandBase(ReihenfolgeMoveDown, null);
                return _onReihenfolgeMoveDown;
            }
        }
        void ReihenfolgeMoveDown(object obj)
        {
            try
            {
                UpdateReihenfolge();
                if (this.aPlayTitel.Reihenfolge < AktKlangPlaylist.Audio_Playlist_Titel.Count - 1)
                    MoveItem(this.aPlayTitel, +1);
                PlayerVM.FilteredLbEditorAudioZeilenListe = PlayerVM.FilteredLbEditorAudioZeilenListe.OrderBy(t => t.aPlayTitel.Reihenfolge).ToList();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken des Buttons 'btnMoveUp' ist ein Fehler aufgetreten", ex);
            }
        }
        
        #endregion
        
        #region //---- KONSTRUKTOR ----

        public AudioZeileViewModel()
        {
            //Event-Handler zur DependentProperty-Notification
       //     PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

         //   _onAudioZeileAdd = new Base.CommandBase(AudioZeileAdd, null); 
           // sliderVM.aPlaylistTitel = aPlayTitel;
        }


        


        //private Base.CommandBase _onAudioZeileAdd;
        //public Base.CommandBase OnAudioZeileAdd
        //{
        //    get { return _onAudioZeileAdd; }
        //}

        #endregion

        #region //---- INSTANZMETHODEN ----
        
        /// <summary>
        /// Prüft, ob 'suchWort' im Namen, der Kategorie oder in den Tags vorkommt.
        /// </summary>
        /// <param name="suchWort"></param>
        /// <returns></returns>
        public bool Contains(string suchWort)
        {
            return _aPlayTitel.Audio_Titel.Name.Contains(suchWort);
        }

        /// <summary>
        /// Prüft, ob die 'suchWorte' im Namen, der Kategorie oder in den Tags vorkommt.
        /// Es wird dabei eine UND-Prüfung durchgeführt.
        /// </summary>
        /// <param name="suchWorte"></param>
        /// <returns></returns>
        public bool Contains(string[] suchWorte)
        {
            foreach (string wort in suchWorte)
            {
                if (!Contains(wort))
                    return false;
            }
            return true;
        }
        
        private void UpdateReihenfolge()
        {
            if (PlayerVM.AktKlangPlaylist.Audio_Playlist_Titel.Count > 1 &&
                PlayerVM.AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.Reihenfolge == 0) > 1)
                PlayerVM.sortPlaylist(PlayerVM.AktKlangPlaylist, -1);
        }

        private void AudioZeileAdd(object sender)
        {
            if (AudioZeileAddEvent != null)
                AudioZeileAddEvent(this, new EventArgs());
        }

        public event EventHandler AudioZeileAddEvent;


        private void MoveItem(Audio_Playlist_Titel aPlaylistTitel, int dif)
        {
            Audio_Playlist_Titel aPlaylistTitel_alt = AktKlangPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Reihenfolge == aPlaylistTitel.Reihenfolge + dif);
            aPlaylistTitel_alt.Reihenfolge = aPlaylistTitel_alt.Reihenfolge - dif;

            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel_alt);

            aPlaylistTitel.Reihenfolge = aPlaylistTitel.Reihenfolge + dif;
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel);

            OnChanged("AktKlangPlaylist");
        }


        #endregion
        
        public object DataContext { get; set; }
    }

}
