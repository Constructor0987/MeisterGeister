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
    public class lbiPlaylistIconVM  : Base.ViewModelBase
    {

        #region //---- FELDER ----
        public AudioPlayerViewModel PlayerVM;
        private string _suchtext = string.Empty;

        bool _checked = true;
        Audio_Playlist _aPlaylist = new Audio_Playlist();
        public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;

        #endregion
        

        #region //---- EIGENSCHAFTEN ----

        [DependentProperty("PlayerVM"), DependentProperty("Reihenfolge"), DependentProperty("AktPlaylistTitel")]
        public bool IstErsteZeile
        {
            get { return _aPlaylist.Reihenfolge == 0; }
            set { OnChanged(); }
        }

        [DependentProperty("PlayerVM"), DependentProperty("Reihenfolge")]
        public bool IstLetzteZeile
        {
            get { return PlayerVM == null || PlayerVM.AktKlangPlaylist == null? 
                false : (PlayerVM.FilteredEditorListBoxItemListe.Count == _aPlaylist.Reihenfolge + 1); }
            set { OnChanged(); }
        }



        public Audio_Playlist aPlaylist
        {
            get { return _aPlaylist; }
            set
            {
                _aPlaylist = value;
                OnChanged();
            }
        }

        #endregion

        #region //---- Commands ----
        
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
            Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);
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
                if (this.aPlaylist.Reihenfolge > 0)
                    MoveItem(this.aPlaylist, -1);
                PlayerVM.FilteredEditorListBoxItemListe = PlayerVM.FilteredEditorListBoxItemListe.OrderBy(t => t.APlaylist.Reihenfolge).ToList();
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
                if (this.aPlaylist.Reihenfolge < PlayerVM.FilteredEditorListBoxItemListe.Count - 1)
                    MoveItem(this.aPlaylist, +1);
                PlayerVM.FilteredEditorListBoxItemListe = PlayerVM.FilteredEditorListBoxItemListe.OrderBy(t => t.APlaylist.Reihenfolge).ToList();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken des Buttons 'btnMoveUp' ist ein Fehler aufgetreten", ex);
            }
        }
        
        #endregion
        
        #region //---- KONSTRUKTOR ----

        public lbiPlaylistIconVM()
        {
            

        }        

        #endregion

        #region //---- INSTANZMETHODEN ----
        

        private void UpdateReihenfolge()
        {
            if (PlayerVM.EditorListBoxItemListe.Count > 1 &&
                PlayerVM.EditorListBoxItemListe.Count(t => t.APlaylist.Reihenfolge == 0) > 1)
                PlayerVM.sortPlaylist(PlayerVM.EditorListBoxItemListe, -1);
        }
        
        private void MoveItem(Audio_Playlist aPlaylist, int dif)
        {
            Audio_Playlist aPlaylist_alt = PlayerVM.EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist.Reihenfolge == aPlaylist.Reihenfolge + dif).APlaylist;
            aPlaylist_alt.Reihenfolge = aPlaylist_alt.Reihenfolge - dif;

            Global.ContextAudio.Update<Audio_Playlist>(aPlaylist_alt);

            aPlaylist.Reihenfolge = aPlaylist.Reihenfolge + dif;
            Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);

            OnChanged("EditorListBoxItemListe");
        }


        #endregion
        
        public object DataContext { get; set; }
    }

}
