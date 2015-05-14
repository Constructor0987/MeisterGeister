using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
//eigene
using MeisterGeister.ViewModel.AudioPlayer;
using VM = MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model;
using MeisterGeister.View.General;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für lbEditorItem.xaml
    /// </summary>
    public partial class lbEditorItem : ListBoxItem
    {
        public bool _animateOnMouseEvent = true;


        public VM.lbEditorItemVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.lbEditorItemVM))
                    return null;
                return DataContext as VM.lbEditorItemVM;
            }
            set { DataContext = value; }
        }

        public lbEditorItem()
        {
            InitializeComponent();
            
        }

        private void lbiEditorPlaylist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VM.PlayerVM.SelectedEditorItem = this.VM;
            if (VM.PlayerVM.rbEditorEditPlaylist) return;
            if (e.LeftButton == MouseButtonState.Pressed)
                VM.PlayerVM.DnDZielObject = e.GetPosition(null);
        }

        private void lbiEditorPlaylist_MouseMove(object sender, MouseEventArgs e)
        {
            if (VM.PlayerVM == null || VM.PlayerVM.rbEditorEditPlaylist) return;
            if (VM.PlayerVM.DnDZielObject == null || //lbEditor.Tag 
                VM.PlayerVM.DnDZielObject is AudioZeile ||
                ((sender) as StackPanel).Tag == null)
                return;

            Point mousePos = e.GetPosition(null);
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Audio_Playlist aPlaylist = (Audio_Playlist)((sender) as StackPanel).Tag;
                // Initialisiere drag & drop Operation
                DataObject dragData = new DataObject("meinListBoxItemIcon", aPlaylist);
                VM.PlayerVM.DnDZielObject = aPlaylist;
                DragDrop.DoDragDrop(sender as StackPanel, dragData, DragDropEffects.All);
                if (VM != null) VM.PlayerVM.DnDZielObject = null;
            }
        }

        private void lbitemEditor_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meineAudioZeile"))
            {
                Audio_Playlist aplaylist = (Audio_Playlist)((StackPanel)sender).Tag;
                AudioZeileItemAblegen(e.Data.GetData("meineAudioZeile") as AudioZeile, null, aplaylist, e, sender);
            }
            else
                if (e.Data.GetDataPresent("meinListBoxItemIcon"))
                    VM.PlayerVM._dndZeilenCursor = null;
        }


        // ****************

        private void AudioZeileItemAblegen(AudioZeile aZeile, VM.AudioZeileVM aZeileVM, Audio_Playlist aPlaylist, DragEventArgs e, object sender)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                bool kopieren = Keyboard.Modifiers == ModifierKeys.Control ? true : false;

                List<string> gedroppteDateien = new List<string>();
                if (aZeile != null)
                    gedroppteDateien.Add(aZeile.VM.aPlayTitel.Audio_Titel.Pfad + '\\' + aZeile.VM.aPlayTitel.Audio_Titel.Datei);
                else
                    gedroppteDateien.Add(aZeileVM.aPlayTitel.Audio_Titel.Pfad + '\\' + aZeileVM.aPlayTitel.Audio_Titel.Datei);

                VM.PlayerVM._DateienAufnehmen(gedroppteDateien, aZeile, aZeileVM, aPlaylist, VM.PlayerVM.audioZeileMouseOverDropped - 1, true);

                if (aPlaylist == VM.PlayerVM.AktKlangPlaylist && Keyboard.Modifiers != ModifierKeys.Control) // Verschieben in akt. Playliste
                {
                    Audio_Playlist_Titel aplytitel1 =
                        aZeile != null ? (Audio_Playlist_Titel)aZeile.Tag : 
                        aZeileVM.aPlayTitel;
                    
                    int oldReihenfolge = aplytitel1.Reihenfolge;
                    Audio_Playlist_Titel aplytitel2 = aPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Reihenfolge == VM.PlayerVM.audioZeileMouseOverDropped);// Global.ContextAudio.PlaylistTitelListe

                    if (!(sender is ListBox) && aplytitel2.Reihenfolge == aplytitel1.Reihenfolge && aplytitel1.Audio_TitelGUID == aplytitel2.Audio_TitelGUID)
                        aplytitel1.Reihenfolge = aPlaylist.Audio_Playlist_Titel.Count;
                    else
                        aplytitel1.Reihenfolge = aplytitel2.Reihenfolge != aplytitel1.Reihenfolge ? aplytitel2.Reihenfolge : aplytitel2.Reihenfolge;

                    VM.PlayerVM.sortPlaylist(aPlaylist, oldReihenfolge < aplytitel1.Reihenfolge ? oldReihenfolge : aplytitel1.Reihenfolge);

                    VM.PlayerVM.LbEditorAudioZeilenSelected = VM.PlayerVM.LbEditorAudioZeilenListe[VM.PlayerVM.LbEditorAudioZeilenListe.Count - 1];
                  //  lbEditorListe.SelectedIndex = lbEditorListe.Items.Count - 1;
                  //  lbEditorListe.Items.MoveCurrentToPosition(PlayerVM.audioZeileMouseOverDropped);
                }

                if (aPlaylist != VM.PlayerVM.AktKlangPlaylist && !kopieren)             //Verschieben = Löschen in akt. Playliste
                {
                    Audio_Titel aTitel =
                        aZeile != null ?
                        Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Audio_TitelGUID == ((Audio_Playlist_Titel)aZeile.Tag).Audio_Titel.Audio_TitelGUID) :
                        Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Audio_TitelGUID == aZeileVM.aPlayTitel.Audio_TitelGUID);

                    Global.ContextAudio.RemoveTitelFromPlaylist(VM.PlayerVM.AktKlangPlaylist, aTitel);

                    if (aZeileVM != null) VM.PlayerVM.LbEditorAudioZeilenListe.Remove(aZeileVM);
                    VM.lbEditorItemVM lbi = VM.PlayerVM.SelectedEditorItem;
                    VM.PlayerVM.SelectedEditorItem = null;
                    VM.PlayerVM.SelectedEditorItem = lbi;
                    VM.PlayerVM.LbEditorAudioZeilenListe = VM.PlayerVM.LbEditorAudioZeilenListe;
                    VM.PlayerVM.FilterEditorPlaylistTitelListe();
                }
                VM.PlayerVM.audioZeileMouseOverDropped = 0;
                VM.PlayerVM._dndZeilenCursor = null;
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei der Drag&Drop-Funktion der AudioZeile ist ein Fehler aufgetreten.", ex);
            }
        }
       
    }
}
