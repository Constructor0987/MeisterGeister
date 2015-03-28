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


        #region //---- EIGENSCHAFTEN ----

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

        //private SliderVM _sliderVM = new SliderVM();
        //public SliderVM sliderVM
        //{
        //    get { return _sliderVM; }
        //    set
        //    {
        //        _sliderVM = value;
        //        OnChanged();
        //    }
        //}

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

        private List<string> _displayPath;
        private List<string> DisplayPath
        {
            get { return _displayPath; }
            set
            {
                List<string> mItemListe = new List<string>();
                foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe.OrderBy(t => t.Name))
                {
                    MenuItem mitem = new MenuItem();
                    mitem.Header = aPlaylist.Name;
                    mitem.Tag = aPlaylist.Audio_PlaylistGUID;

                    mItemListe.Add(aPlaylist.Name);
                }
                _displayPath = mItemListe;
            }
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
            get { return ((_aPlayTitel.Audio_Titel.Länge != null && _aPlayTitel.Audio_Titel.Länge.Value != 0) ? 
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


        //Commands  

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


        private Base.CommandBase _onAudioZeileAdd;
        public Base.CommandBase OnAudioZeileAdd
        {
            get { return _onAudioZeileAdd; }
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
                if (this.aPlayTitel.Reihenfolge < AktKlangPlaylist.Audio_Playlist_Titel.Count - 1)
                    MoveItem(this.aPlayTitel, +1);
                PlayerVM.FilteredLbEditorAudioZeilenListe = PlayerVM.FilteredLbEditorAudioZeilenListe.OrderBy(t => t.aPlayTitel.Reihenfolge).ToList();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken des Buttons 'btnMoveUp' ist ein Fehler aufgetreten", ex);
            }
        }
        
        private void MoveItem(Audio_Playlist_Titel aPlaylistTitel, int dif)
        {
            Audio_Playlist_Titel aPlaylistTitel_alt = AktKlangPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Reihenfolge == aPlaylistTitel.Reihenfolge + dif);
            aPlaylistTitel_alt.Reihenfolge = aPlaylistTitel_alt.Reihenfolge - dif;

            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel_alt);

            aPlaylistTitel.Reihenfolge = aPlaylistTitel.Reihenfolge + dif;
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel);

            OnChanged("AktKlangPlaylist");
            //object selected = audioZeile;
            //lb.Items.Remove(selected);
            //lb.Items.Insert(aPlaylistTitel.Reihenfolge, selected);
            //lb.ScrollIntoView(selected);
        }



        #endregion



        #region //---- KONSTRUKTOR ----

        public AudioZeileViewModel()
        {
            //Event-Handler zur DependentProperty-Notification
       //     PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            _onAudioZeileAdd = new Base.CommandBase(AudioZeileAdd, null); 
           // sliderVM.aPlaylistTitel = aPlayTitel;
        }

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


        private void AudioZeileAdd(object sender)
        {
            if (AudioZeileAddEvent != null)
                AudioZeileAddEvent(this, new EventArgs());
        }

        public event EventHandler AudioZeileAddEvent;

        #endregion
        
        public object DataContext { get; set; }
    }

}
