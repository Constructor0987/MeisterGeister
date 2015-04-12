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
using System.Timers;
using ComboBox = System.Windows.Controls.ComboBox;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Xml;
using System.IO;
using System.Windows.Markup;
using System.ComponentModel;
using System.Windows.Interop;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions; 

// Eigene Usings
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.Logic.General;
using MeisterGeister.View.General;
using MeisterGeister.View.Windows;
using Global = MeisterGeister.Global;
using MeisterGeister.Model;
using MeisterGeister.View.AudioPlayer;
using VM = MeisterGeister.ViewModel.AudioPlayer;
using MeisterGeister.ViewModel.Base;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using System.Windows.Interactivity;

namespace MeisterGeister.View.AudioPlayer {
	/// <summary>
	/// Interaktionslogik für AudioPlayerView.xaml
	/// </summary>
	/// 
     

    public class MouseWheelGesture : MouseGesture
    {
        public WheelDirection Direction { get; set; }

        public static MouseWheelGesture Up
        {
            get { return new MouseWheelGesture { Direction = WheelDirection.Up }; }
        }

        public static MouseWheelGesture Down
        {
            get { return new MouseWheelGesture { Direction = WheelDirection.Down }; }
        }

        public static MouseWheelGesture CrtlDown
        { get { return new MouseWheelGesture(ModifierKeys.Control) { Direction = WheelDirection.Down }; } }

        public MouseWheelGesture()
            : base(MouseAction.WheelClick)
        { }

        public MouseWheelGesture(ModifierKeys modifiers) :
            base(MouseAction.WheelClick, modifiers)
        { }


        public override bool Matches(object targetElement, InputEventArgs inputEventArg)
        {
            if (!base.Matches(targetElement, inputEventArg)) return false;
            if (!(inputEventArg is MouseWheelEventArgs)) return false;

            var args = (MouseWheelEventArgs)inputEventArg;
            switch (Direction)
            {
                case WheelDirection.None: return args.Delta == 0;
                case WheelDirection.Up: return args.Delta > 0;
                case WheelDirection.Down: return args.Delta < 0;
                default: return false;
            }
        }

        public enum WheelDirection
        {
            None, Up, Down,
        }
    }


    public partial class AudioPlayerView : UserControl
    {
        TabItemControl AudioTIC = null;

        //private MusikView _bgPlayer = new MusikView();
        private int _bgPlayeraktiv;
        public int BGPlayeraktiv
        {
            get { return VM.BGPlayeraktiv; }
            set
            {
                VM.BGPlayeraktiv = value;
                _bgPlayeraktiv = value;
            }
        }

        private VM.AudioPlayerViewModel.MusikView _bgPlayer;
        public VM.AudioPlayerViewModel.MusikView BGPlayer
        {
            get { return VM.BGPlayer; }
            set
            {
                VM.BGPlayer = value;
                _bgPlayer = value;                
            }
        }
        
        //private List<GruppenObjekt> VM._GrpObjecte = new List<GruppenObjekt>();

        private Audio_Playlist _aktKlangPlaylist;
        public Audio_Playlist AktKlangPlaylist
        {
            get { return VM.AktKlangPlaylist; }
            set
            {
                VM.AktKlangPlaylist = value;
                _aktKlangPlaylist = value;
            }
        }
        
        private Audio_Theme _aktKlangTheme;
        public Audio_Theme AktKlangTheme
        {
            get { return VM.AktKlangTheme; }
            set
            {
                VM.AktKlangTheme = value;
                _aktKlangTheme = value;
            }
        }

       // DispatcherTimer _debugTreeview = new DispatcherTimer();
        
        delegate void UpdateUI();

        //public class ScrollIntoViewForListBox : Behavior<ListBox>
        //{
        //    /// <summary>
        //    ///  When Beahvior is attached
        //    /// </summary>
        //    protected override void OnAttached()
        //    {
        //        base.OnAttached();
        //        this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        //    }

        //    /// <summary>
        //    /// On Selection Changed
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    void AssociatedObject_SelectionChanged(object sender,
        //                                           SelectionChangedEventArgs e)
        //    {
        //        if (sender is ListBox)
        //        {
        //            ListBox listBox = (sender as ListBox);
        //            if (listBox.SelectedItem != null)
        //            {
        //                listBox.Dispatcher.BeginInvoke(
        //                    (Action)(() =>
        //                    {
        //                        listBox.UpdateLayout();
        //                        if (listBox.SelectedItem !=
        //                            null)
        //                            listBox.ScrollIntoView(
        //                                listBox.SelectedItem);
        //                    }));
        //            }
        //        }
        //    }
        //    /// <summary>
        //    /// When behavior is detached
        //    /// </summary>
        //    protected override void OnDetaching()
        //    {
        //        base.OnDetaching();
        //        this.AssociatedObject.SelectionChanged -=
        //            AssociatedObject_SelectionChanged;

        //    }
        //}
        
        public AudioPlayerView()
        {
            InitializeComponent();
            VM = new VM.AudioPlayerViewModel();

            VM.setStdPfad();
            VM.fadingTime = MeisterGeister.Logic.Einstellung.Einstellungen.Fading;
            slPlaylistVolume.Value = Einstellungen.GeneralGeräuscheVolume;
            slBGVolume.Value = Einstellungen.GeneralMusikVolume;
            slHotkey.Value = Einstellungen.GeneralHotkeyVolume;

            BGPlayer = new ViewModel.AudioPlayer.AudioPlayerViewModel.MusikView();
            BGPlayer.BG.Add(new MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.Musik());
            BGPlayer.BG.Add(new MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.Musik());

            VM.AktualisiereHotKeys();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.AudioPlayerViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.AudioPlayerViewModel))
                    return null;
                return DataContext as VM.AudioPlayerViewModel;
            }
            set { DataContext = value; }
        }



        private void AudioZeile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                VM.pointerZeileDragDrop = e.GetPosition(null);
        }

        //private void lbiPlaylist_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        //{
        //    try
        //    {
        //        if (VM.rbEditorEditPlaylist) return;

        //        if (e.Effects == DragDropEffects.Copy || e.Effects == DragDropEffects.Move)
        //        {
        //            if (VM._dndZeilenCursor == null && (VM.pointerZeileDragDrop != null || VM.DnDZielObject != null))
        //            {
                        
        //                VM._dndZeilenCursor = CreateCursor(e.Source as UIElement, false);
        //            }

        //            if (VM._dndZeilenCursor != null)
        //                e.UseDefaultCursors = false;
        //        }
        //        else
        //            e.UseDefaultCursors = true;

        //        e.Handled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Feedback Fehler" + Environment.NewLine + "Beim Starten des Drag'n'Drop-Vorgangs ist ein Fehler aufgetreten.", ex);
        //        e.UseDefaultCursors = true;
        //    }
        //}       

        private void AudioZeile_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            try
            {
                if (!VM.rbEditorEditPlaylist) return;

                if (e.Effects == DragDropEffects.Copy || e.Effects == DragDropEffects.Move)
                {
                    if (VM._dndZeilenCursor == null && (VM.pointerZeileDragDrop != null || VM.DnDZielObject != null))
                    {
                        VM._dndZeilenCursor = CreateCursor(((AudioZeile)(e.Source)).grdDnD as UIElement, false);
                        VM._dndZeilenCursorPlus = CreateCursor(((AudioZeile)(e.Source)).grdDnD as UIElement, true);
                    }

                    if (VM._dndZeilenCursor != null)
                    {
                        e.UseDefaultCursors = false;
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                            Mouse.SetCursor(VM._dndZeilenCursorPlus);
                        else
                            Mouse.SetCursor(VM._dndZeilenCursor);
                    }
                }
                else
                    e.UseDefaultCursors = true;

                e.Handled = true;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Feedback Fehler" + Environment.NewLine + "Beim Starten des Drag'n'Drop-Vorgangs ist ein Fehler aufgetreten.", ex);
                e.UseDefaultCursors = true;
            }
        }


        private void AudioZeile_MouseMove(object sender, MouseEventArgs e)
        {
            AudioZeile aZeile = sender as AudioZeile;
            VM.audioZeileMouseOverDropped = lbEditorListe.Items.IndexOf(aZeile.VM);
            if (VM.pointerZeileDragDrop == null )
                return;

            Point mousePos = e.GetPosition(null);
            Vector diff = ((Point)VM.pointerZeileDragDrop) - mousePos;

            Point mp = Mouse.GetPosition(aZeile);
            VM.audioZeileMouseOverDropped = lbEditorListe.Items.IndexOf(aZeile.VM);
            //Abfrage bei gedrückter Maustaste, wenn im Vorderen Bereich und nicht auf der ProgressBar (um Teilabspielen zu editieren)
            if (e.LeftButton == MouseButtonState.Pressed &&
                (mp.X < +aZeile.pbarTitel.ActualWidth) && mp.X > 0 && //35 + 10
                ((mp.Y > 0 && mp.Y < aZeile.lbiEditorRow.ActualHeight / 2 - aZeile.pbarTitel.ActualHeight / 2) ||
                 (mp.Y > aZeile.lbiEditorRow.ActualHeight / 2 + aZeile.pbarTitel.ActualHeight / 2)))
            {
                // Initialisiere drag & drop Operation
                DataObject dragData = new DataObject("meineAudioZeile", aZeile);
                DragDrop.DoDragDrop(aZeile, dragData, DragDropEffects.Copy);
                VM.pointerZeileDragDrop = null;
            }
        }
                

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.Refresh();
        }
        
        public static RoutedCommand AudioTabClose = new RoutedCommand();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AudioTIC == null)
                {
                    AudioTIC = ((TabItemControl)((AudioPlayerView)e.Source).Parent);
                    AudioTIC.CommandBindings.Add(new CommandBinding(AudioTabClose, VM.OnAudioTabClose));
                    AudioTIC._buttonClose.Command = AudioTabClose;
                }                
                rbEditorMusik.Focus();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Laden des Fensters ist ein Fehler aufgetreten.", ex);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (VM.ShowHotkeyPanel)
                {
                    string s = Convert.ToString(e.Key);
                    if (e.Key >= Key.D0 && e.Key <= Key.D9)
                        s = s.Remove(0, s.Length - 1);

                    btnHotkey hkey = VM.hotkeyListUsed.FindAll(t => t.VM.aPlaylistGuid != (Guid.Empty)).
                        FirstOrDefault(t => Convert.ToChar(t.VM.taste).ToString() == s);
                    if (hkey != null)
                        hkey.VM.OnBtnClick(hkey.btn);
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswerten des Tastenklicks ist ein Fehler aufgetreten.", ex);
            }
        }
        
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Dispatcher.HasShutdownStarted)
                {
                    if (BGPlayer != null)
                    {
                        VM.stopFadingIn = true;
                        if (BGPlayer.BG[0].mPlayer != null && !BGPlayer.BG[0].FadingOutStarted)
                        {
                            BGPlayer.BG[0].FadingOutStarted = true;
                            VM.BGFadingOut(BGPlayer.BG[0], true, true);
                        }
                        if (BGPlayer.BG[1].mPlayer != null && !BGPlayer.BG[1].FadingOutStarted)
                        {
                            BGPlayer.BG[1].FadingOutStarted = true;
                            VM.BGFadingOut(BGPlayer.BG[1], true, true);
                        }
                    }

                    for (int i = 0; i < VM._GrpObjecte.Count; i++)
                        VM.AlleKlangSongsAus(VM._GrpObjecte[i], true, false, true);

                    VM.KlangProgBarTimer.Stop();
                    VM.MusikProgBarTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Schließen des Fensters ist ein Fehler aufgetreten.", ex);
            }
        }
       
        private void cmboxTopHotkey_DropDownClosed(object sender, EventArgs e)
        {
            VM.UpdateHotkeyUsed();
            VM.ShowHotkeyPanel = false;
            rbEditorMusik.Focus();
        }
                
        private void pbarBGSong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (BGPlayer.AktPlaylist != null && BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
                {
                    Point pts = e.GetPosition((sender as ProgressBar));
                    double total = (sender as ProgressBar).Maximum;
                    double res = ((pts.X * 100) / ((double)(sender as ProgressBar).ActualWidth)) / 100;
                    BGPlayer.BG[BGPlayeraktiv].mPlayer.Position = TimeSpan.FromMilliseconds(total * res);
                }
            }
            catch (Exception) { }
        }

        private void tbtnKlangPauseX_Checked(object sender, RoutedEventArgs e)
        {
            try
            {                
                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);// .tbtnKlangPause == ((ToggleButton)sender));
                if (grpobj == null)
                    return;
                if (tiEditor.IsSelected)
                    grpobj.sollBtnGedrueckt++;

                grpobj.wirdAbgespielt = true;

                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    grpobj._listZeile[i].istPause = false;

                    if (grpobj._listZeile[i].istLaufend && grpobj._listZeile[i].aPlaylistTitel.Aktiv)
                    {
                        if (grpobj.aPlaylist.Hintergrundmusik)// .istMusik)
                        {
                            grpobj._listZeile[i].FadingOutStarted = false;
                            VM.FadingIn(grpobj._listZeile[i], grpobj._listZeile[i]._mplayer, grpobj._listZeile[i].Aktuell_Volume / 100);
                            // Sichtbares Abspielen im Editor -> Scrollen
                            //if (grpobj.visuell)
                            //    grpobj.lbEditorListe.ScrollIntoView(grpobj._listZeile[i].audioZeile);
                            //grpobj.sviewer.ScrollToVerticalOffset(i * grpobj._listZeile[i].audioZeile.ActualHeight);
                        }
                    }
                    else
                        grpobj._listZeile[i].istStandby =
                            (!grpobj._listZeile[i].istPause && !grpobj._listZeile[i].istLaufend && grpobj._listZeile[i].aPlaylistTitel.Aktiv) ? true : false;
                }
                VM.CheckPlayStandbySongs(grpobj);

                if (!grpobj.aPlaylist.Hintergrundmusik && grpobj.aPlaylist.Fading)
                    VM.FadingInGeräusch(grpobj);

                if (grpobj.visuell)
                {
                    //((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/stop.png"));
                    //((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(3, 4, 2, 2);
                }
                //***Problem: Absturz wenn LW / Ordner / Datei nicht vorhanden ?!?
                //Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FindAll(t => t.Audio_PlaylistGUID.Equals(grpobj.aPlaylist.Audio_PlaylistGUID)).FirstOrDefault();
                //grpobj.totalTimePlylist = -1;
                //     if (aPlaylist != null && tcAudioPlayer.SelectedItem == tiEditor)
                //         GetTotalLength(aPlaylist);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Anwählen der Pause-Funktion für die Playlist ist ein Fehler aufgetreten", ex);
            }
        }

        private void tbtnKlangPauseX_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);// .tbtnKlangPause == ((ToggleButton)sender));
                if (grpobj == null)
                    return;
                if (tiEditor.IsSelected && grpobj.sollBtnGedrueckt > 0)
                    grpobj.sollBtnGedrueckt--;
                grpobj.wirdAbgespielt = false;

                if (!grpobj.aPlaylist.Hintergrundmusik && grpobj.aPlaylist.Fading)// && !_timerFadingOutGeräusche.IsEnabled)
                    VM.FadingOutGeräusch(true, true, grpobj);

                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    if (!grpobj.aPlaylist.Hintergrundmusik)
                    {
                        if (grpobj._listZeile[i]._mplayer != null && !grpobj.aPlaylist.Fading)
                        {
                            grpobj._listZeile[i].istPause = true;
                            grpobj._listZeile[i]._mplayer.Pause();
                            grpobj._listZeile[i].istStandby = false;
                            grpobj._listZeile[i].istLaufend = false;
                        }
                    }
                    else
                    {
                        grpobj._listZeile[i].istPause = true;
                        if (!grpobj._listZeile[i].FadingOutStarted && grpobj._listZeile[i].istLaufend)
                        {
                            grpobj._listZeile[i].FadingOutStarted = true;
                            VM.FadingOut(grpobj._listZeile[i], true, true);

                            grpobj._listZeile[i].istLaufend = false;
                            grpobj._listZeile[i].audioZeileVM.TitelMinimum = 0;// .audioZeile.pbarTitel.Value = 0;
                            grpobj._listZeile[i].istStandby = true;
                        }
                    }
                }
                VM.CheckPlayStandbySongs(grpobj);
                if (grpobj.visuell)
                {
                    //((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                    //((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(1, 2, 0, 0);
                }

                //Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FindAll(t => t.Audio_PlaylistGUID.Equals(grpobj.aPlaylist.Audio_PlaylistGUID)).FirstOrDefault();
                grpobj.totalTimePlylist = -1;
                //if (aPlaylist != null)
                //    GetTotalLength(aPlaylist);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Abwählen der Pause-Funktion für die Playlist ist ein Fehler aufgetreten", ex);
            }
        }

        public void slVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ((Slider)sender).Value += (e.Delta > 1) ? 3 : -3;
        }

        public void slBGVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            slBGVolume.Value += (e.Delta > 1) ? 3 : -3;
        }        
        
        
        private void cmbxThemeTheme_DropDownClosed(object sender, EventArgs e)
        {
            grdEditorThemeWPnlUTheme.Children.Remove((ComboBox)sender);
        }

        
        private void lbEditor_Drop(object sender, DragEventArgs e)
        {
            if (VM.audioZeileMouseOverDropped == -1)
                return;
            if (e.Data.GetDataPresent("meineAudioZeile"))
                AudioZeileItemAblegen(e.Data.GetData("meineAudioZeile") as AudioZeile, null, AktKlangPlaylist, e, sender);
            else
            {
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    {
                        List<string> gedroppteDateien = new List<string>((string[])e.Data.GetData(DataFormats.FileDrop, false));
                        VM._DateienAufnehmen(gedroppteDateien, null,  null, AktKlangPlaylist, 0, false);
                    }
                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    //Audio_Playlist aPlaylist = AktKlangPlaylist;
                    //AktKlangPlaylist = null;
                    //AktKlangPlaylist = aPlaylist;
                    
                    Mouse.OverrideCursor = null;
                }
                catch (Exception ex)
                {
                    Mouse.OverrideCursor = null;
                    ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ablegen der Dateien in der Playlist ist ein Fehler aufgetreten.", ex);
                }
            }
        }

        private void audioZeile_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent("meineAudioZeile"))
            {
                int drag = lbEditorListe.Items.IndexOf(e.Data.GetData("meineAudioZeile") as AudioZeile);
                
                object o = sender;
                if (o is AudioZeile)
                {
                    while (!(o is AudioZeile)) 
                    {
                        if (o is Grid) 
                            o = (o as Grid).Parent;
                        else
                            if (o is ListBoxItem)
                                o = (o as ListBoxItem).Parent;
                    }
                }
                int drop = lbEditorListe.Items.IndexOf((AudioZeile)o);                    
                if (drag != drop)
                    VM.audioZeileMouseOverDropped = drop;
                else
                    VM.audioZeileMouseOverDropped = -1;
            }
        }

        private void brdEditorTheme_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meinListBoxItemIcon"))
            {
                VM.ThemeItemIconAblegen(e.Data.GetData("meinListBoxItemIcon") as Audio_Playlist);
                VM._dndZeilenCursor = null;
            }
        }


        private void lbitemEditor_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meineAudioZeile"))
            {
                Audio_Playlist aplaylist =(Audio_Playlist)((StackPanel)sender).Tag;
                AudioZeileItemAblegen(e.Data.GetData("meineAudioZeile") as AudioZeile, null, aplaylist, e, sender);
            }
            else
                if (e.Data.GetDataPresent("meinListBoxItemIcon"))
                    VM._dndZeilenCursor = null;
        }

        private void AudioZeileItemAblegen(AudioZeile aZeile, AudioZeileViewModel aZeileVM, Audio_Playlist aPlaylist, DragEventArgs e, object sender)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                bool kopieren = Keyboard.Modifiers == ModifierKeys.Control? true: false;

                List<string> gedroppteDateien = new List<string>();
                if (aZeile != null)
                    gedroppteDateien.Add(aZeile.VM.aPlayTitel.Audio_Titel.Pfad + '\\' + aZeile.VM.aPlayTitel.Audio_Titel.Datei); 
                else
                    gedroppteDateien.Add(aZeileVM.aPlayTitel.Audio_Titel.Pfad + '\\' + aZeileVM.aPlayTitel.Audio_Titel.Datei); 
                    //(string)aZeile.chkTitel.Tag);

                VM._DateienAufnehmen(gedroppteDateien, aZeile, aZeileVM, aPlaylist, VM.audioZeileMouseOverDropped - 1, true);
                
                if (aPlaylist == AktKlangPlaylist && Keyboard.Modifiers != ModifierKeys.Control) // Verschieben in akt. Playliste
                {
                    Audio_Playlist_Titel aplytitel1 =
                        aZeile != null ? (Audio_Playlist_Titel)aZeile.Tag : //aPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)aZeile.Tag) :
                        aZeileVM.aPlayTitel;
                        //aPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == aZeileVM.aPlayTitel.Audio_TitelGUID);
                        // (Guid)aZeile.Tag);
                    int oldReihenfolge = aplytitel1.Reihenfolge;
                    Audio_Playlist_Titel aplytitel2 = aPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Reihenfolge == VM.audioZeileMouseOverDropped);// Global.ContextAudio.PlaylistTitelListe
                    
                    if (!(sender is ListBox) && aplytitel2.Reihenfolge == aplytitel1.Reihenfolge && aplytitel1.Audio_TitelGUID == aplytitel2.Audio_TitelGUID)
                        aplytitel1.Reihenfolge = aPlaylist.Audio_Playlist_Titel.Count;
                    else
                        aplytitel1.Reihenfolge = aplytitel2.Reihenfolge != aplytitel1.Reihenfolge ? aplytitel2.Reihenfolge : aplytitel2.Reihenfolge;

                    VM.sortPlaylist(aPlaylist, oldReihenfolge < aplytitel1.Reihenfolge ? oldReihenfolge : aplytitel1.Reihenfolge);

                    lbEditorListe.SelectedIndex = lbEditorListe.Items.Count - 1;
                    lbEditorListe.Items.MoveCurrentToPosition(VM.audioZeileMouseOverDropped);
                }
               
                if (aPlaylist != AktKlangPlaylist && !kopieren)             //Verschieben = Löschen in akt. Playliste
                {
                    Audio_Titel aTitel =
                        aZeile != null?  Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Audio_TitelGUID  == (Guid)aZeile.Tag):
                        Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Audio_TitelGUID == aZeileVM.aPlayTitel.Audio_TitelGUID);
                        // (Guid)aZeile.Tag);
                    Global.ContextAudio.RemoveTitelFromPlaylist(AktKlangPlaylist, aTitel);

                    if (aZeileVM != null) VM.LbEditorAudioZeilenListe.Remove(aZeileVM);
                    VM.LbEditorAudioZeilenListe = VM.LbEditorAudioZeilenListe;
                    VM.FilterEditorPlaylistTitelListe();
                }
                VM.audioZeileMouseOverDropped = 0;
                VM._dndZeilenCursor = null;
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei der Drag&Drop-Funktion der AudioZeile ist ein Fehler aufgetreten.", ex);
            }
        }

        // *************************************************************************
        //Drag'n'Drop AudioZeile - Mouse-Cursor ändert sich zur AudioZeile während des DnD Vorgangs
        private static class NativeMethods
        {
            public struct IconInfo
            {
                public bool fIcon;
                public int xHotspot;
                public int yHotspot;
                public IntPtr hbmMask;
                public IntPtr hbmColor;
            }

            [DllImport("user32.dll")]
            public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

            [DllImport("user32.dll")]
            public static extern bool DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        }

        private class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeIconHandle()
                : base(true)
            {
            }
            override protected bool ReleaseHandle()
            {
                return NativeMethods.DestroyIcon(handle);
            }
        }

        private static Cursor InternalCreateCursor(System.Drawing.Bitmap bmp)
        {
            var iconInfo = new NativeMethods.IconInfo();
            NativeMethods.GetIconInfo(bmp.GetHicon(), ref iconInfo);

            iconInfo.xHotspot = 0;
            iconInfo.yHotspot = 0;
            iconInfo.fIcon = false;

            SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
            return CursorInteropHelper.Create(cursorHandle);
        }

        public static Cursor CreateCursor(UIElement element, bool andPlus)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(new Point(), element.DesiredSize));

            RenderTargetBitmap rtb =
              new RenderTargetBitmap(
                (int)element.DesiredSize.Width,
                (int)element.DesiredSize.Height,
                96, 96, PixelFormats.Pbgra32);

            rtb.Render(element);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                using (var bmp = new System.Drawing.Bitmap(ms))
                {
                    string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    if (!andPlus)
                    {
                        BitmapImage bmpi1 = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/mouse_cursornormal.png"));

                        System.Drawing.Bitmap bmpMouseNormal = new System.Drawing.Bitmap(Convert.ToInt32(bmpi1.Width) + bmp.Width, Convert.ToInt32(bmpi1.Height) + bmp.Height);
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpMouseNormal))
                        {
                            using (MemoryStream outStream = new MemoryStream())
                            {
                                PngBitmapEncoder enc = new PngBitmapEncoder();
                                enc.Frames.Add(BitmapFrame.Create(bmpi1));
                                enc.Save(outStream);
                                g.DrawImage(new System.Drawing.Bitmap(outStream), 0, 0);
                            }
                            g.DrawImage(bmp, Convert.ToInt32(bmpi1.Width), Convert.ToInt32(bmpi1.Height));
                        }
                        return InternalCreateCursor(bmpMouseNormal);
                    }
                    else
                    {
                        BitmapImage bmpi2 = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/mouse_cursorplus.png"));

                        System.Drawing.Bitmap bmpMousePlus = new System.Drawing.Bitmap(Convert.ToInt32(bmpi2.Width) + bmp.Width, Convert.ToInt32(bmpi2.Height) + bmp.Height);
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpMousePlus))
                        {
                            using (MemoryStream outStream = new MemoryStream())
                            {
                                PngBitmapEncoder enc = new PngBitmapEncoder();
                                enc.Frames.Add(BitmapFrame.Create(bmpi2));
                                enc.Save(outStream);
                                g.DrawImage(new System.Drawing.Bitmap(outStream), 0, 0);
                            }
                            g.DrawImage(bmp, Convert.ToInt32(bmpi2.Width), Convert.ToInt32(bmpi2.Height));
                        }
                        return InternalCreateCursor(bmpMousePlus);
                    }
                }
            }
        }

        private void lbiEditorPlaylist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VM.rbEditorEditPlaylist) return;
            if (e.LeftButton == MouseButtonState.Pressed)// && !(lbEditor.Tag is ListboxItemIcon))
            {
               // lbEditor.Tag = e.GetPosition(null);
                VM.DnDZielObject = e.GetPosition(null);
            }
        }




        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
        }

        //private void lb_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count > 0)
        //    {
        //        ((ListBox)sender).ScrollIntoView((MusikZeile)e.AddedItems[0]);
        //        ((ListBox)sender).UpdateLayout();
        //        //Audio_Playlist aPlaylist = ((MusikZeile)e.AddedItems[0]).VM.aPlaylist;
        //        //if (((MusikZeile)lbPListMusik.SelectedItem).VM.aPlaylist.Audio_PlaylistGUID != VM.SelectedMusikItem.VM.aPlaylist.Audio_PlaylistGUID)
        //        //    VM.SelectedMusikItem = VM.ErwPlayerMusikListItemListe.FirstOrDefault(t => t.VM.aPlaylist == aPlaylist);
        //         //   lbPListMusik.SelectedItem = VM.SelectedMusikItem;
        //        //if (lbPListMusik.SelectedItem == null ||
        //        //    ((MusikZeile)lbPListMusik.SelectedItem).VM.aPlaylist != aPlaylist)
        //        //    lbPListMusik.SelectedItem = VM.ErwPlayerMusikListItemListe.FirstOrDefault(t => t.VM.aPlaylist == aPlaylist);
        //    }
        //}

        //private void lb_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count > 0)
        //    {
        //        ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
        //        ((ListBox)sender).UpdateLayout();
        //       // Audio_Playlist aPlaylist = ((MusikZeile)e.AddedItems[0]).VM.aPlaylist;
        //        //if (lbMusik.SelectedItem == null ||
        //        //    ((MusikZeile)lbMusik.SelectedItem).VM.aPlaylist.Audio_PlaylistGUID != ((MusikZeile)e.AddedItems[0]).VM.aPlaylist.Audio_PlaylistGUID)
        //        //    VM.SelectedMusikItem = VM.MusikListItemListe.FirstOrDefault(t => t.VM.aPlaylist == aPlaylist);
        //          //  lbMusik.SelectedItem = VM.MusikListItemListe.FirstOrDefault(t => t.VM.aPlaylist == aPlaylist);
        //    }
        //}

        double HOffset = 0;
        private void Slider_MouseMove(object sender, MouseEventArgs e)
        {
            ((ToolTip)(sender as Slider).ToolTip).IsOpen = e.LeftButton == MouseButtonState.Pressed;
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                HOffset = e.GetPosition(sender as Slider).X;
                ((ToolTip)(sender as Slider).ToolTip).HorizontalOffset = 0;
            }
            if (e.LeftButton == MouseButtonState.Pressed &&
                ((ToolTip)(sender as Slider).ToolTip).IsOpen)
                ((ToolTip)(sender as Slider).ToolTip).HorizontalOffset = e.GetPosition(sender as Slider).X - HOffset;
        }

        private void Slider_MouseLeave(object sender, MouseEventArgs e)
        {
            ((ToolTip)(sender as Slider).ToolTip).IsOpen = false;
        }



        //private void lbMusiktitellist_MouseMove(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        if (e != null && e.Source != null && e.Source is TabItem
        //            && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
        //        {
        //            TabItem item = (TabItem)(e.Source);
        //            DragDrop.DoDragDrop(item, item, DragDropEffects.All);
        //            VM.DnDZielObject = null;
        //           // lbEditor.Tag = null;
        //        }
        //    }
        //    catch (Exception) { }
        //}

        private void grdEditorX_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Drag-Mode über den Editor ist ein Fehler aufgetreten.", ex);
            }
        }



        private void lbiEditorPlaylist_MouseMove(object sender, MouseEventArgs e)
        {
            if (VM == null || VM.rbEditorEditPlaylist) return;
            if (VM.DnDZielObject == null || //lbEditor.Tag 
                VM.DnDZielObject is AudioZeile ||
                ((sender) as StackPanel).Tag == null)
                return;

            Point mousePos = e.GetPosition(null);
            //Vector diff = (lbEditor.Tag is Point ? (Point)lbEditor.Tag : (Point){0;0}) - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed)// &&
            //(Mouse.GetPosition((AudioZeile)sender).X > 35 + 10 + ((AudioZeile)sender)._audioZeile.pbarTitel.ActualWidth))
            {

                Audio_Playlist aPlaylist = (Audio_Playlist)((sender) as StackPanel).Tag;
                   // Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)((sender) as StackPanel).Tag);
                //                VM.EditorListBoxItemListe[0].
                // Initialisiere drag & drop Operation
                DataObject dragData = new DataObject("meinListBoxItemIcon", aPlaylist);// lbi);// ListboxItemIcon);
                VM.DnDZielObject = aPlaylist;
                DragDrop.DoDragDrop(sender as StackPanel, dragData, DragDropEffects.All); //ListboxItemIcon
                VM.DnDZielObject = null;
            }
        }

        private void sldPlaylistWartezeit_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double aktWert = ((Slider)sender).Value;
            if (e.Delta < 0)
            {
                if (aktWert != ((Slider)sender).Minimum)
                    ((Slider)sender).Value = ((Slider)sender).Ticks[((Slider)sender).Ticks.IndexOf(aktWert) - 1];
            }
            else
            {
                if (aktWert != ((Slider)sender).Maximum)
                    ((Slider)sender).Value = ((Slider)sender).Ticks[((Slider)sender).Ticks.IndexOf(aktWert) + 1];
            }
        }
    }
}


        //private double GetMusikdateiLength(string musikdatei)
        //{
        //    double max = -2;
        //    MediaPlayer mp = new MediaPlayer();

        //    if (Directory.Exists(System.IO.Path.GetDirectoryName(musikdatei)) &&
        //            File.Exists(musikdatei))
        //    {
        //        mp.Volume = 0;
        //        mp.Open(new Uri(musikdatei));
        //        mp.Play();
        //        if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 4000))
        //        {
        //            mp.Pause();
        //            max = mp.NaturalDuration.TimeSpan.TotalMilliseconds;
        //        }
        //        mp.Stop();
        //        mp.Close();
        //        return max;
        //    }
        //    else
        //        return -1;
        //}

        //private void chkKlangZeileCMenuNurTeil_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile = (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile)((ContextMenu)((CheckBox)sender).Parent).Tag;

        //        double max = GetMusikdateiLength(kZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + kZeile.aPlaylistTitel.Audio_Titel.Datei);
        //        if (max > 0)
        //        {
        //            kZeile.aPlaylistTitel.TeilAbspielen = true;
        //            kZeile.audioZeileVM.TitelMinimum = 0;// .rsldTeilSong.Minimum = 0;
        //            kZeile.audioZeileVM.TitelMaximum = max;//.rsldTeilSong.Maximum = max;
        //            //kZeile.audioZeile.rsldTeilSong.UpdateLayout();

        //            //kZeile.audioZeile.rsldTeilSong.LowerValue = 0;
        //            //kZeile.audioZeile.rsldTeilSong.UpperValue = max;
        //            //kZeile.audioZeile.rsldTeilSong.UpdateLayout();

        //            //kZeile.audioZeile.rsldTeilSong.Visibility = Visibility.Visible;

        //            ((ContextMenu)((CheckBox)sender).Parent).IsOpen = false;
        //            Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.aPlaylistTitel);
        //        }
        //        else
        //        {
        //            ((CheckBox)sender).Unchecked -= chkKlangZeileCMenuNurTeil_Unchecked;
        //            ((CheckBox)sender).IsChecked = false;
        //            ((CheckBox)sender).Unchecked += chkKlangZeileCMenuNurTeil_Unchecked;
        //            string meldung;
        //            if (max == -1)
        //                meldung = "Die Länge der Musikdatei konnte nicht ermittelt werden. Überprüfen Sie den Pfad und wiederholen Sie den Vorgang.";
        //            else
        //                meldung = "Die Laufzeit von 4sek. um die Länge der Musikdatei zu ermitteln wurde überschritten. Wiederholen Sie den Vorgang zu einem späteren Zeitpunkt.";

        //            ViewHelper.ShowError("Datenfehler" + Environment.NewLine + meldung, new Exception());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ((CheckBox)sender).Unchecked -= chkKlangZeileCMenuNurTeil_Unchecked;
        //        ((CheckBox)sender).IsChecked = false;
        //        ((CheckBox)sender).Unchecked += chkKlangZeileCMenuNurTeil_Unchecked;
        //        ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Der Slider für einen Teil des Titel abzuspielen, konnte nicht aktiviert werden.", ex);
        //    }
        //}

        //private void chkKlangZeileCMenuNurTeil_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile = (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile)((ContextMenu)((CheckBox)sender).Parent).Tag;
        //    kZeile.aPlaylistTitel.TeilAbspielen = false;
        //    //kZeile.audioZeile.rsldTeilSong.Visibility = Visibility.Hidden;
        //    ((ContextMenu)((CheckBox)sender).Parent).IsOpen = false;

        //    Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.aPlaylistTitel);
        //}
        

        //private Audio_Playlist UpdatePlaylist(Audio_Playlist AktPlaylist, string NeuerPlaylistName)
        //{
        //    if (AktPlaylist == null)
        //    {
        //        Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
        //        playlist.Name = NeuerPlaylistName;
        //        AktPlaylist = playlist;
        //    }
        //    else
        //    {
        //        List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.FindAll(t => t.Audio_PlaylistGUID.Equals(AktPlaylist.Audio_PlaylistGUID)).ToList();
        //        if (playlistliste.Count == 0)
        //        {
        //            Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
        //            playlist.Name = NeuerPlaylistName;
        //            playlist.Hintergrundmusik = false;
        //            playlist.MaxSongsParallel = 1;

        //            //zur datenbank hinzufügen
        //            if (!Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
        //                ViewHelper.ShowError("Datenbank-Fehler" + Environment.NewLine + "Das hinzufügen der Playlist in die Datenbank ist fehlgeschlagen.", new Exception());
        //        }
        //        else
        //        {
        //            playlistliste[0].Name = NeuerPlaylistName;
        //            Global.ContextAudio.Update<Audio_Playlist>(playlistliste[0]);
        //        }
        //    }
        //    return AktPlaylist;
        //}

        //private void tboxTopKategorieX_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Audio_Playlist aPlyLst = Global.ContextAudio.PlaylistListe.Find(t => t.Audio_PlaylistGUID == ((Guid)((TextBox)e.Source).Tag));
        //        if (aPlyLst != null)
        //        {
        //            aPlyLst.Kategorie = ((TextBox)e.Source).Text;
        //            Global.ContextAudio.Update<Audio_Playlist>(aPlyLst);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Verlassen des Kategorie-Feldes ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void cmboxTopHotkey_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        // a = 65
        //        int istkey = Convert.ToInt32(e.Key) + 21;
        //        if (e.Key >= Key.D0 && e.Key <= Key.D9)
        //            istkey = Convert.ToInt32(Convert.ToString(e.Key).Remove(0, 1)) + 48;

        //        foreach (Border brdItem in ((ComboBox)sender).Items)
        //        {
        //            if (Convert.ToInt32(brdItem.Tag) == istkey)
        //            {
        //                ((ComboBox)sender).SelectedItem = brdItem;
        //                break;
        //            }
        //        } 
        //        rbEditorMusik.Focus();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Drücken der Auswahltaste im Combobox-Feld ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void chbxThemeNurGeräusche_Checked(object sender, RoutedEventArgs e)
        //{
        //    ((Audio_Theme)((CheckBox)sender).Tag).NurGeräusche = ((CheckBox)sender).IsChecked.Value;
        //    Global.ContextAudio.Update<Audio_Theme>(((Audio_Theme)((CheckBox)sender).Tag));
        //}

        //private void btnBGFilter_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (((TextBox)((Grid)((Button)(e.Source)).Parent).Children.OfType<TextBox>().First()).Text == "")
        //            ((TextBox)((Grid)((Button)(e.Source)).Parent).Children.OfType<TextBox>().First()).Text = " ";
        //        ((TextBox)((Grid)((Button)(e.Source)).Parent).Children.OfType<TextBox>().First()).Text = "";
        //    }
        //    catch (Exception) { }
        //}

        //private void btneditorFilter_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        btnBGFilter_Click(sender, e);
        //        lbEditor.ScrollIntoView(lbEditor.SelectedItem);
        //    }
        //    catch (Exception) { }
        //}

        //public void _btnStdPfad_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t._listZeile.Exists(t2 => t2.istLaufend)).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj)
        //        {
        //            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
        //        });

        //        var dialog = new System.Windows.Forms.FolderBrowserDialog();

        //        dialog.SelectedPath = MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis;
        //        System.Windows.Forms.DialogResult result = dialog.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Eingabefehler" + Environment.NewLine + "Das Auswählen des Standard-Verzeichnisses hat eine Exeption ausgelöst.", ex);
        //    }
        //}
        
        //private void wartezeitTimer_Tick(object sender, EventArgs e)
        //{
        //    MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt)((DispatcherTimer)sender).Tag;
        //    grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
        //}

        //private void lbItembtnLöschenTheme_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
        //        Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == g);
        //        if (aTheme != null)
        //        {
        //            if (ViewHelper.ConfirmYesNoCancel("Löschen des Themes", "Wollen Sie wirklich das ausgewählte Theme  '" + aTheme.Name + "'  löschen.") == 2)
        //            {
        //                Global.SetIsBusy(true, string.Format("Theme '" + aTheme.Name + "' wird gelöscht..."));

        //                //if (AktKlangTheme != null && (wpnlPListThemes.Tag as List<Guid>).Contains(AktKlangTheme.Audio_ThemeGUID))
        //                //{
        //                //    for (int i = 0; i < wpnlPListThemes.Children.Count; i++)
        //                //        if ((Guid)((grdThemeButton)wpnlPListThemes.Children[i]).Tag == AktKlangTheme.Audio_ThemeGUID)
        //                //            ((grdThemeButton)wpnlPListThemes.Children[i]).tbtnTheme.IsChecked = false;
        //                //}

        //                if (!Global.ContextAudio.Delete<Audio_Theme>(aTheme))
        //                    Global.ContextAudio.ThemeListe.Remove(aTheme);
        //                lbEditorTheme.SelectedIndex = -1;
        //                AktKlangTheme = null;

        //                //wpnlEditorThemeGeräusch.Children.Clear();
        //                //wpnlEditorThemeMusik.Children.Clear();
        //                //wpnlEditorTopThemesThemes.Children.Clear();
        //                tboxEditorName.Text = VM.GetNeuenNamen("Neues Theme", 1);

        //                Global.SetIsBusy(false);
        //                ViewHelper.Popup("Das Theme wurde erfolgreich gelöscht.");
        //            }
        //        }
        //        else
        //            ViewHelper.ShowError("Das ausgewählte Theme konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", new Exception());
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Löschen des Themes ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void tcAudioPlayer_SizeChanged(object sender, RoutedEventArgs e)
        //{
        //    if (IsInitialized)
        //        exErwPlayerTheme.MaxHeight = tcAudioPlayer.ActualHeight * 2 / 3;
        //}


        
        //private void imgStdIconDelete_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    ListboxItemBtn lbiBtn = (ListboxItemBtn)((StackPanel)((Grid)((Image)sender).Parent).Parent).Parent;

        //    string s = "";
        //    for (int i = 0; i < VM.stdPfad.Count; i++)
        //        if (VM.stdPfad[i] != lbiBtn.lblStdPfad.Content.ToString())
        //            s += VM.stdPfad[i] + "|";
        //    if (s.Length > 0)
        //        s = s.Substring(0, s.Length - 1);
        //    else
        //        s = "C:";

        //    Logic.Einstellung.Einstellungen.AudioVerzeichnis = s;
        //    Logic.Einstellung.Einstellungen.UpdateEinstellungen();

        //    ((ListBox)lbiBtn.Parent).Items.Remove(lbiBtn);
        //    VM.setStdPfad();
        //}

        //public void Close_Click(object sender, RoutedEventArgs e)
        //{
        //    VM.KlangProgBarTimer.Stop();
        //    VM.MusikProgBarTimer.Stop();
        //    for (int posObjGruppe = 0; posObjGruppe < VM._GrpObjecte.Count; posObjGruppe++)
        //    {
        //        List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> KlangZeilenLaufend = VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);

        //        if (KlangZeilenLaufend.Count != 0)
        //            for (int durchlauf = 0; durchlauf < KlangZeilenLaufend.Count; durchlauf++)
        //            {
        //                if (KlangZeilenLaufend[durchlauf]._mplayer != null)
        //                {
        //                    KlangZeilenLaufend[durchlauf]._mplayer.Stop();
        //                    KlangZeilenLaufend[durchlauf]._mplayer.Close();
        //                }
        //            }
        //    }
        //    if (BGPlayer != null && BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
        //    {
        //        BGPlayer.BG[BGPlayeraktiv].mPlayer.Stop();
        //        BGPlayer.BG[BGPlayeraktiv].mPlayer.Close();
        //    }

        //    foreach (DispatcherTimer dispTmr in VM.lstKlangPlayEndetimer)
        //        if (dispTmr != null) dispTmr.Stop();

        //    VM.AlleKlangSongsAus(null, true, false, true);

        //    VM._GrpObjecte.Clear();
        //    VM.lstKlangPlayEndetimer.Clear();
        //}

        //private void CloseTab(object source, RoutedEventArgs args)
        //{
        //    TabItem tabItem = (TabItem)args.Source;
        //    if (tabItem != null)
        //    {
        //        TabControl tabControl = (TabControl)tabItem.Parent;
        //        if (tabControl != null)
        //            tabControl.Items.Remove(tabItem);
        //    }
        //}

        //public string SongInfo(String musikfile)
        //{
        //    String Filename = "";

        //    if (musikfile != null)
        //        Filename = musikfile;
        //    Filename = Filename + DateTime.Now.ToString();
        //    return Filename;
        //}

        
        

        //private void lbMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        //if (lbPListMusik.Items.Count != lbMusik.Items.Count)
        //        //    AktualisierePListPlaylist();
        //        if (lbPListMusik.SelectedIndex != lbMusik.SelectedIndex)
        //            lbPListMusik.SelectedIndex = lbMusik.SelectedIndex;

        //        if (e != null)
        //        {
        //            ListBox selListBox = ((ListBox)(e.Source));
        //            if (selListBox.SelectedItems.Count != 0)
        //            {
        //                try
        //                {
        //                    if (BGPlayer != null && VM.MusikProgBarTimer != null)
        //                    {
        //                        VM.MusikProgBarTimer.Stop();
        //                        btnBGAbspielen.Tag = true;
        //                        //btnBGAbspielen_Click(btnBGAbspielen, null);
        //                    }

        //                    Audio_Playlist playlistliste = Global.ContextAudio.PlaylistListe.FindAll(t => t.Audio_PlaylistGUID.Equals(((MusikZeile)lbMusik.SelectedItem).Tag)).FirstOrDefault();
        //                    if (playlistliste != null)
        //                    {
        //                        lbMusik.Tag = selListBox.SelectedIndex;                                
        //                        //lbMusiktitellist.Items.Clear();

        //                        BGPlayer.NochZuSpielen.Clear();
        //                        BGPlayer.Gespielt.Clear();
        //                        //BGPlayer.AktPlaylist = playlistliste;
        //                        BGPlayer.AktTitel.Clear();

        //                        btnBGShuffle.IsChecked = BGPlayer.AktPlaylist.Shuffle;
        //                        /*btnBGRepeat.IsChecked = BGPlayer.AktPlaylist.Repeat;
        //                        ((Image)btnBGRepeat.Content).Source = new BitmapImage(new Uri(
        //                            (btnBGRepeat.IsChecked == null) ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat1.png" :
        //                            (btnBGRepeat.IsChecked.Value)?    "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat.png":
        //                                                              "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat0.png"));
        //                        btnBGRepeat.ToolTip = (btnBGRepeat.IsChecked == null) ? "Einzelstück wiederholen" :
        //                            btnBGRepeat.IsChecked.Value ? "Playliste wiederholen" : "Keine Wiederholung";

        //                        btnBGShuffle.IsChecked = BGPlayer.AktPlaylist.Shuffle;
        //                        btnShuffle_Click(btnBGShuffle, null);*/

        //                        if (playlistliste.Audio_Playlist_Titel.Count > 0)
        //                        {
        //                            //foreach (Audio_Playlist_Titel playlisttitel in playlistliste.Audio_Playlist_Titel.OrderBy(t => t.Reihenfolge))
        //                            //{
        //                            //    BGPlayer.AktTitel.Add(playlisttitel.Audio_Titel);
        //                            //    ListBoxItem lbitem = new ListBoxItem();
        //                            //    lbitem.Tag = playlisttitel.Audio_Titel.Audio_TitelGUID;
        //                            //    lbitem.Content = playlisttitel.Audio_Titel.Name;
        //                            //    lbitem.ToolTip = playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;

        //                            //    if (!playlistliste.Audio_Playlist_Titel.First(t => t.Audio_TitelGUID == playlisttitel.Audio_Titel.Audio_TitelGUID).Aktiv)
        //                            //    {
        //                            //        lbitem.FontStyle = FontStyles.Italic;
        //                            //        lbitem.Foreground = Brushes.DarkSlateGray;
        //                            //        lbitem.ToolTip = "Audio-Titel inaktiv." + Environment.NewLine + "Im Playlist-Editor anhaken zum Aktivieren" +
        //                            //                         Environment.NewLine + "Anklicken um den Titel abzuspielen";
        //                            //    }
        //                            //    lbMusiktitellist.Items.Add(lbitem);
        //                            //}
        //                            VM.RenewMusikNochZuSpielen();

        //                            btnBGAbspielen.IsEnabled = true;
        //                            btnBGAbspielen.Tag = false;
        //                            btnBGNext.IsEnabled = true;
        //                            //((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Maximum = pbarBGSong.Maximum;
        //                            //((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Value = pbarBGSong.Value;
        //                            //((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Visibility = Visibility.Visible;

        //                            VM.SpieleNeuenMusikTitel(Guid.Empty);
        //                            if (playlistliste.Audio_Playlist_Titel.Count == 0)
        //                            {
        //                                btnBGAbspielen.Tag = true;
        //                                //btnBGAbspielen_Click(btnBGAbspielen, null); //new RoutedEventArgs());
        //                            }

        //                            BGPlayer.totalLength = -1;
        //                        }
        //                        else
        //                        {
        //                            grdSongInfo.Visibility = Visibility.Hidden;
        //                            btnBGNext.IsEnabled = false;
        //                            btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //                            btnBGAbspielen.IsEnabled = false;
        //                        }
        //                        //VM.BGPlayer = BGPlayer;
        //                        //VM.LoadMusiklistListe();
        //                        //VM.UpdateBG();
        //                    }
        //                    else
        //                    {
        //                        lbMusik.Tag = -1;
        //                        grdSongInfo.Visibility = Visibility.Hidden;
        //                        btnBGNext.IsEnabled = false;
        //                        btnBGAbspielen.IsEnabled = false;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Die Playliste konnte nicht geöffnet werden oder die Playliste ist leer", ex);
        //                }
        //            }
        //            else
        //            {
        //                if (selListBox.SelectedItems.Count == 0)
        //                {
        //                    btnBGNext.IsEnabled = false;
        //                    btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //                    btnBGAbspielen.IsEnabled = false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Beim Auswählen der Playlist ist ein Fehler aufgetreten", ex);
        //    }
        //}

        //private void btnBGSpeaker_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (Convert.ToInt32(btnBGSpeaker.Tag) != -1)
        //        {
        //            btnBGSpeaker.Tag = -1;
        //            imgbtnBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
        //            if (BGPlayer.BG[0].mPlayer != null) BGPlayer.BG[0].mPlayer.IsMuted = false;
        //            if (BGPlayer.BG[1].mPlayer != null) BGPlayer.BG[1].mPlayer.IsMuted = false;
        //        }
        //        else
        //        {
        //            btnBGSpeaker.Tag = slBGVolume.Value;
        //            imgbtnBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker-mute.png"));
        //            if (BGPlayer.BG[0].mPlayer != null) BGPlayer.BG[0].mPlayer.IsMuted = true;
        //            if (BGPlayer.BG[1].mPlayer != null) BGPlayer.BG[1].mPlayer.IsMuted = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Klicken des Lautsprecher-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnBGAbspielen_Click(object sender, RoutedEventArgs e)
        //{                                   //btnBGAbspielen.Tag = 0 --> Button hat Play-Icon, kein Sound spielt
        //    try
        //    {                
        //        if (!Convert.ToBoolean(btnBGAbspielen.Tag) && !BGPlayer.BG[BGPlayeraktiv].isPaused ||
        //            BGPlayer.BG[BGPlayeraktiv].isPaused &&
        //            (BGPlayer.BG[BGPlayeraktiv].aPlaylist == null ||
        //             BGPlayer.BG[BGPlayeraktiv].aPlaylist.Audio_PlaylistGUID == (Guid)((MusikZeile)lbMusik.SelectedItem).Tag))
        //        {
        //            btnBGAbspielen.Tag = !Convert.ToBoolean(btnBGAbspielen.Tag);

        //            if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null && BGPlayer.BG[BGPlayeraktiv].mPlayer.Source != null)
        //            {
        //                grdSongInfo.Visibility = Visibility.Visible;
        //                if (!BGPlayer.BG[BGPlayeraktiv].isPaused)
        //                {
        //                    lbMusiktitellist.SelectionChanged -= lbMusiktitellist_SelectionChanged;
        //                    lbMusiktitellist.SelectedIndex = Convert.ToInt16(lbMusiktitellist.Tag);
        //                    lbMusiktitellist.SelectionChanged += lbMusiktitellist_SelectionChanged;
        //                }
        //                BGPlayer.BG[BGPlayeraktiv].aPlaylist = BGPlayer.AktPlaylist;
        //                BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = false;

        //                BGPlayer.BG[BGPlayeraktiv].isPaused = false;
        //                FadingIn(BGPlayer.BG[BGPlayeraktiv], BGPlayer.BG[BGPlayeraktiv].mPlayer, Convert.ToDouble(BGPlayer.AktPlaylistTitel.Volume / 100));
        //                btnBGAbspielen.Tag = true;
        //                imgbtnBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
        //            }
        //            else
        //                SpieleNeuenMusikTitel(Guid.Empty);

        //            btnBGStoppen.IsEnabled = true;
        //            btnBGPrev.IsEnabled = true;
        //        }
        //        else
        //        {
        //            if (Convert.ToInt16(lbMusik.Tag) == lbMusik.SelectedIndex)          // Auswahl Playliste nicht geändert
        //            {
        //                if (Convert.ToBoolean(btnBGAbspielen.Tag))
        //                {
        //                    if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
        //                    {
        //                        BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
        //                        BGFadingOut(BGPlayer.BG[BGPlayeraktiv], false, true);
        //                        BGPlayer.BG[BGPlayeraktiv].aPlaylist = null;
        //                    }
        //                }
        //                else
        //                {
        //                    BGPlayer.BG[BGPlayeraktiv].isPaused = false;
        //                    FadingIn(BGPlayer.BG[BGPlayeraktiv], BGPlayer.BG[BGPlayeraktiv].mPlayer, Convert.ToDouble(BGPlayer.AktPlaylistTitel.Volume / 100));
        //                }
        //            }
        //            //imgbtnBGAbspielen.Source = Convert.ToBoolean(btnBGAbspielen.Tag) ?
        //            //    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")) :
        //            //    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));

        //            btnBGAbspielen.Tag = !Convert.ToBoolean(btnBGAbspielen.Tag);
        //        }
        //        //(lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Play-/Pause-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnBGStoppen_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        BGPlayer.BG[BGPlayeraktiv].isPaused = false;
        //        if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
        //        {
        //            if (!BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
        //            {
        //                BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
        //                VM.BGFadingOut(BGPlayer.BG[BGPlayeraktiv], true, true);
        //            }
        //            lbMusiktitellist.Tag = lbMusiktitellist.SelectedIndex;
        //        }

        //        if (lbMusiktitellist.SelectedIndex >= 0)
        //        {
        //            BGPlayer.Gespielt.Add((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
        //            if (BGPlayer.Gespielt.Count > 50)
        //                BGPlayer.Gespielt.RemoveAt(0);
        //        }
        //        btnBGPrev.IsEnabled = false;

        //        VM.MusikProgBarTimer.Stop();
        //        btnBGAbspielen.Tag = false;
        //        grdSongInfo.Visibility = Visibility.Hidden;
        //        lbMusiktitellist.SelectedIndex = -1;
        //        imgbtnBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
        //        if (lbPListMusik.SelectedItem != null)
        //            ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Visibility = Visibility.Collapsed;
        //        btnBGStoppen.IsEnabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Stop-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void lbMusik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        lbMusik_SelectionChanged(sender, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Doppelklicken des Maus-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnBGNext_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (lbMusiktitellist.SelectedIndex == -1)
        //            VM.SpieleNeuenMusikTitel(Guid.Empty);
        //        else
        //        {
        //            lbMusiktitellist.Tag = lbMusiktitellist.SelectedIndex;
        //            VM.SpieleNeuenMusikTitel(Guid.Empty);
        //        }
        //        if (lbPListMusik.SelectedItem != null)
        //            (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Next-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnBGPrev_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (BGPlayer.Gespielt.Count == 0)
        //            VM.SpieleNeuenMusikTitel((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
        //        else
        //        {
        //            Guid vorher = BGPlayer.Gespielt.ElementAt(BGPlayer.Gespielt.Count - 1);
        //            btnBGPrev.Tag = true;
        //            VM.SpieleNeuenMusikTitel(vorher);
        //            btnBGPrev.Tag = null;
        //            if (BGPlayer.Gespielt.Count > 0)
        //                BGPlayer.Gespielt.RemoveAt(BGPlayer.Gespielt.Count - 1);
        //            lbMusiktitellist.Tag = -1;
        //        }
        //        (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Zurück-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private bool ChecklbEditorPossible(ListboxItemIcon vorher)
        //{
        //    Guid GuidVorher = vorher == null ? Guid.Empty : (Guid)vorher.Tag;
        //    Guid GuidSoll = (Guid)((ListboxItemIcon)lbEditor.Items[lbEditor.SelectedIndex]).Tag;
        //    bool found = false;

        //    if (tcAudioPlayer.SelectedItem == tiEditor && GuidVorher != GuidSoll)
        //    {
        //        bool hasHintergrund = false;

        //        // Check doppelte Playlists
        //        if (grdEditorListe.Tag != null &&
        //            ((Guid)grdEditorListe.Tag) == GuidSoll)
        //        {
        //            ViewHelper.Popup("Doppelte Playlist im Theme" + Environment.NewLine + "Die Playlist ist bereits in dem Theme aufgelistet und kann nicht nochmals benutzt werden.");
        //            lbEditor.SelectedIndex = -1;
        //            found = true;
        //        }
                
        //        // Check zwei Musik-Playlists
        //        if (!found && grdEditorListe.Tag != null)
        //        {
        //            Audio_Playlist aplyLst = Global.ContextAudio.PlaylistListe.First(t => t.Audio_PlaylistGUID == (Guid)grdEditorListe.Tag);
        //            if (aplyLst != null && aplyLst.Hintergrundmusik)
        //                hasHintergrund = true;
        //        }

        //        if (grdEditorListe.Tag != null && hasHintergrund &&
        //            Global.ContextAudio.PlaylistListe.First(t => t.Audio_PlaylistGUID == GuidSoll).Hintergrundmusik)
        //        {
        //            ViewHelper.Popup("Hintergrund-Playlist Error" + Environment.NewLine + "Das Theme enthält schon eine Hintergrund-Playlist. Pro Theme kann nur eine Hintergrund-Playlist abgespielt werden.");
        //            lbEditor.SelectedIndex = -1;
        //            found = true;
        //        }
        //    }
        //    return !found;
        //}

        //private void lbEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (!IsInitialized)
        //        return;
        //    try
        //    {
        //        if ((tcAudioPlayer.SelectedItem == tiEditor) && (rbEditorEditTheme.IsChecked.Value))
        //        {
        //            lbEditor.SelectionChanged -= new SelectionChangedEventHandler(lbEditor_SelectionChanged);
        //            if (e.RemovedItems.Count != 0)
        //                lbEditor.SelectedItem = e.RemovedItems[0];
        //            else
        //                lbEditor.SelectedIndex = -1;
        //            lbEditor.SelectionChanged += new SelectionChangedEventHandler(lbEditor_SelectionChanged);
        //            return;
        //        }

        //        if (lbEditor.SelectedIndex != -1 &&
        //            (tcAudioPlayer.SelectedItem == tiEditor && VM.rbEditorEditPlaylist.Value ||
        //             ChecklbEditorPossible((ListboxItemIcon)(e.RemovedItems.Count == 0 ? null : e.RemovedItems[0]))))
        //        {
        //            Mouse.OverrideCursor = Cursors.Wait;
        //            try
        //            {
        //                lbEditorListe.Items.Clear();
        //                int was = lbEditor.SelectedIndex;
        //                lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
        //                lbEditor.SelectedIndex = was;
        //                lbEditor.SelectionChanged += lbEditor_SelectionChanged;

        //                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //                if (grpobj == null)
        //                {
        //                    tiPlus_MouseUp(false, null);
        //                    grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //                }                        

        //                if (lbEditor.SelectedIndex == -1)
        //                {
        //                    lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
        //                    lbEditor.SelectedIndex = was;
        //                    lbEditor.SelectionChanged += lbEditor_SelectionChanged;
        //                }
        //                List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.
        //                   Where(t => t.Audio_PlaylistGUID.Equals(((ListboxItemIcon)lbEditor.Items[lbEditor.SelectedIndex]).Tag)).ToList();

        //                if (playlistliste.Count == 1)
        //                {
        //                    List<Audio_Playlist_Titel> lstPlylistTitel = playlistliste[0].Audio_Playlist_Titel.OrderBy(t2 => t2.Reihenfolge).ToList();

        //                    if (rbEditorEditTheme.IsChecked.Value)
        //                    {
        //                        if (rbEditorEditTheme.IsEnabled == true && AktKlangTheme == null)
        //                        {
        //                            NeueKlangThemeInDB(tboxEditorName.Text);
        //                            tboxEditorName.Focus();
        //                        }
        //                    }

        //                    if (e != null && grpobj.aPlaylist != null && rbEditorEditTheme.IsChecked.Value && AktKlangTheme != null &&
        //                        AktKlangTheme.Audio_Playlist.Contains(grpobj.aPlaylist))
        //                    {
        //                        AktKlangTheme.Audio_Playlist.Remove(grpobj.aPlaylist);
        //                        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        //                    }

        //                    //Letzte visuell TabItem befreien von Zeilen
        //                    PlaylisteLeeren(VM._GrpObjecte.FirstOrDefault(t => t.visuell));

        //                    AktKlangPlaylist = playlistliste[0];

        //                    tbtnPlaylistWartezeit.IsChecked = AktKlangPlaylist.WarteZeitAktiv;
        //                    sldPlaylistWartezeit.Value = AktKlangPlaylist.WarteZeit;
        //                    tboxPlaylistWartezeitMax.Text = AktKlangPlaylist.WarteZeitMax.ToString();

        //                    grdEditorMain.Focus();
        //                    grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
        //                    grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;
                                                        
        //                    grpobj.istMusik = AktKlangPlaylist.Hintergrundmusik;

        //                    grpobj.playlistName = AktKlangPlaylist.Name;
                                
        //                    if (lstPlylistTitel.Count > 0)
        //                    {
        //                        UInt16 x = 0;
        //                        foreach (Audio_Playlist_Titel playlisttitel in lstPlylistTitel)
        //                        {
        //                            if (!System.IO.Path.IsPathRooted(playlisttitel.Audio_Titel.Pfad + "\\" + (playlisttitel.Audio_Titel.Datei == null ? "" : playlisttitel.Audio_Titel.Datei)) &&
        //                                !File.Exists(playlisttitel.Audio_Titel.Pfad + "\\" + (playlisttitel.Audio_Titel.Datei == null ? "" : playlisttitel.Audio_Titel.Datei)))
        //                            {
        //                                playlisttitel.Audio_Titel = setTitelStdPfad(playlisttitel.Audio_Titel);
        //                                if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei)) &&
        //                                    File.Exists(playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei))
        //                                    Global.ContextAudio.Update<Audio_Titel>(playlisttitel.Audio_Titel);
        //                            }

        //                            KlangNewRow(playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei, grpobj, x, playlisttitel);
        //                            if (playlisttitel.Aktiv &&
        //                                !grpobj.NochZuSpielen.Contains(playlisttitel.Audio_TitelGUID))
        //                            {
        //                                for (int i = 0; i <= playlisttitel.Rating; i++)
        //                                    grpobj.NochZuSpielen.Add(playlisttitel.Audio_TitelGUID);
        //                            }
        //                            if (playlisttitel.Reihenfolge != x)
        //                                playlisttitel.Reihenfolge = x;
        //                            x++;
        //                        }
        //                        if (AktKlangPlaylist.Hintergrundmusik)
        //                            ZeigeZeileKlangSpalten(grpobj, false);
        //                    }
        //                    grpobj.aPlaylist = AktKlangPlaylist;
        //                    UpdatePlaylistLänge(AktKlangPlaylist);
        //                    CheckAlleAngehakt(grpobj);                        

        //                    if (grpobj.wirdAbgespielt)
        //                    {
        //                        if (grpobj.istMusik)
        //                            grpobj.wirdAbgespielt = false;
        //                        ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
        //                        ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(1, 2, 0, 0);
        //                    }

        //                    if (!AktKlangPlaylist.Hintergrundmusik)                            
        //                    {
        //                        hotkey hKey = hotkeyListe.FirstOrDefault(t => t.aPlaylistGuid == AktKlangPlaylist.Audio_PlaylistGUID);
        //                        grpobj.btnTopHotkeySet.Content = (hKey == null) ? "nicht definiert" : Convert.ToChar(hKey.taste).ToString();

        //                        grpobj.btnHotkeyEntfernen.Visibility = hKey == null ? Visibility.Collapsed : Visibility.Visible;
        //                        grpobj.cmboxTopHotkey.SelectedIndex = -1;

        //                        grpobj.intbxSongsParallel.MaxValue = grpobj.aPlaylist.Audio_Playlist_Titel.Count;
        //                        //.gboxTopSongsParallel      intbxSongsParallel1.MaxValue = 
                                
        //                    }

        //                    //Neue Playlist in Theme hinzufügen
        //                    if (tcAudioPlayer.SelectedItem == tiEditor && rbEditorEditTheme.IsChecked.Value &&
        //                        !AktKlangTheme.Audio_Playlist.Contains(AktKlangPlaylist))
        //                    {
        //                        AktKlangTheme.Audio_Playlist.Add(AktKlangPlaylist);
        //                        if (Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == AktKlangTheme.Audio_ThemeGUID) == null)
        //                            NeueKlangThemeInDB(AktKlangTheme.Name);
        //                        else
        //                        {
        //                            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        //                            Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        //                        }
        //                        lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
        //                        lbEditor.SelectedIndex = was;
        //                        lbEditor.SelectionChanged += lbEditor_SelectionChanged;
        //                    }
        //                    if (((TabItem)tcAudioPlayer.SelectedItem) == tiEditor && lbEditorTheme.Tag == null)   //lbEditorTheme.Tag == null --> lbEditorTheme geklickt
        //                        CheckBtnGleicherPfad(VM._GrpObjecte.FirstOrDefault(t => t.visuell));

        //                    if (((TabItem)tcAudioPlayer.SelectedItem) == tiEditor)
        //                    {
        //                        tboxEditorName.Text = AktKlangPlaylist.Name;
        //                        GetTotalLength(AktKlangPlaylist);
        //                    }
        //                }
        //            }
        //            finally
        //            {
        //                Mouse.OverrideCursor = null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Auswahlfehler" + Environment.NewLine + "Beim Ändern der Selektierung des 'lbEditor' ist ein Fehler aufgetreten", ex);
        //    }
        //}


        //private void lbEditorTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (lbEditorTheme.SelectedIndex != -1)
        //        {
        //            try
        //            {
        //                Mouse.OverrideCursor = Cursors.Wait;
        //                {
        //                    try
        //                    {
        //                        AktKlangPlaylist = null;
        //                        Guid ThemeGuid = (Guid)((ListboxItemIcon)lbEditorTheme.Items[lbEditorTheme.SelectedIndex]).Tag;
        //                        Audio_Theme atheme = Global.ContextAudio.LoadThemesByGUID(ThemeGuid);

        //                        if (atheme != null)
        //                        {
        //                            //wpnlEditorThemeGeräusch.Children.Clear();
        //                            //wpnlEditorThemeMusik.Children.Clear();
        //                            wpnlEditorTopThemesThemes.Children.Clear();

        //                            AktKlangTheme = atheme;

        //                            foreach (Audio_Playlist aPlaylist in atheme.Audio_Playlist)
        //                            {
        //                                foreach (lbEditorItem lbi in lbEditor.Items)
        //                                {
        //                                    if (lbi.APlaylist == aPlaylist)
        //                                        VM.ThemeItemIconAblegen(aPlaylist);//lbi);
        //                                }
        //                            }

        //                            // Erstelle Untergeordnete Themes
        //                            foreach (Audio_Theme aUnterTheme in atheme.Audio_Theme1.
        //                                Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
        //                            {
        //                                boxThemeTheme bxTheme = new boxThemeTheme();
        //                                bxTheme.txblkName.Text = aUnterTheme.Name;
        //                                bxTheme.Tag = aUnterTheme.Audio_ThemeGUID;
        //                                bxTheme.btnRemove.Tag = aUnterTheme.Audio_ThemeGUID;
        //                                bxTheme.btnRemove.Click += bxThemeBtnClose_Click;
        //                                wpnlEditorTopThemesThemes.Children.Add(bxTheme);
        //                            }
        //                            tboxThemeKategorie.Text = atheme.Kategorie;
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Die Playlist-Liste konnte nicht eindeutig in der Datenbank detektiert werden.", ex);

        //                        for (int i = 0; i <= grdEditorPlaylistInfo.Children.Count - 1; i++)
        //                            if ((grdEditorPlaylistInfo.Children[i] as Control) != null &&
        //                                (grdEditorPlaylistInfo.Children[i] as Control).Name != "btnKlangSave") grdEditorPlaylistInfo.Children[i].Visibility = Visibility.Hidden;
        //                    }
        //                }
        //            }
        //            finally
        //            {
        //                Mouse.OverrideCursor = null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Wechseln der Listbox im Theme-Mode ist ein Fehler aufgetreten.", ex);
        //    }
        //}


        //private void chkStdPfadVerfügbar()
        //{
        //    VM.setStdPfad();
        //    /*   foreach (ListboxItemBtn lbb in lbStandardPfade.Items)
        //       {
        //           lbb.lblStdPfad.IsEnabled = Directory.Exists(lbb.lblStdPfad.Content.ToString());
        //           if (!lbb.lblStdPfad.IsEnabled)
        //               stdPfad.Remove(stdPfad.First(t => t == (string)lbb.lblStdPfad.Content));
        //       }*/
        //}

        //private void miListPlaylisttitel_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    if (AktKlangPlaylist == null) return;
        //    if (((MenuItem)sender).Items.Count != Global.ContextAudio.PlaylistListe.Count)
        //    {
        //        ((MenuItem)sender).Items.Clear();
        //        foreach (Audio_Playlist aPlay in Global.ContextAudio.PlaylistListe.FindAll(t2 => t2.Audio_PlaylistGUID != AktKlangPlaylist.Audio_PlaylistGUID).OrderBy(t => t.Name))
        //        {
        //            MenuItem mi = new MenuItem();
        //            mi.Header = aPlay.Name;
        //            mi.Tag = aPlay.Audio_PlaylistGUID;
        //            mi.Click += miCopyTitelToPlaylist_Click;
        //            ((MenuItem)sender).Items.Add(mi);
        //        }
        //    }
        //}

        //private void miCopyTitelToPlaylist_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile = 
        //            (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile)((ContextMenu)(((MenuItem)(((MenuItem)sender).Parent)).Parent)).Tag;
        //        Guid playGUID = (Guid)(((MenuItem)sender).Tag);
        //        Audio_Playlist zielPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == playGUID);

        //        if (zielPlaylist != null)
        //        {
        //            List<string> gedroppteDateien = new List<string>();
        //            gedroppteDateien.Add((string)kZeile.audioZeile.chkTitel.Tag);
        //            _DateienAufnehmen(gedroppteDateien, kZeile.audioZeile, zielPlaylist, 0, false);
        //            Global.ContextAudio.Update<Audio_Playlist>(zielPlaylist);
        //        }
        //        else
        //        {
        //            ViewHelper.Popup("Die Playliste konnte nicht gefunden werden." + Environment.NewLine + Environment.NewLine + "Vorgang abgebrochen");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Fehler" + Environment.NewLine + "Beim Ausführen der Duplizierung in eine andere Playliste ist ein Fehler aufgetreten", ex);
        //    }
        //}

        

        

        //private Int16 GetPosObjGruppe(int objGruppe)
        //{
        //    Int16 posObjGruppe = Convert.ToInt16(VM._GrpObjecte.FindIndex(t => t.objGruppe == objGruppe));
        //    return posObjGruppe;
        //}

        //public UIElement DeepCopy(UIElement element, string oldValue, string newValue)
        //{
        //    string shapestring;
        //    shapestring = XamlWriter.Save(element);
        //    if (oldValue != null)
        //        shapestring = shapestring.Replace(oldValue, newValue);

        //    StringReader stringReader = new StringReader(shapestring);
        //    XmlReader xmlTextReader = new XmlTextReader(stringReader);
        //    UIElement DeepCopyobject = (UIElement)XamlReader.Load(xmlTextReader);
        //    return DeepCopyobject;
        //}

        //private void ZeigeZeileKlangSpalten(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj, bool sichtbar)
        //{
        //    grpobj._listZeile.ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile)
        //    {
        //        if (kZeile.audioZeile.lbiEditorRow != null)
        //        {
        //            if (!sichtbar)
        //            {
        //                if (kZeile.audioZeile.grdEditorRow.ColumnDefinitions[4].Width != new GridLength(0))
        //                {
        //                    for (int i = kZeile.audioZeile.grdEditorRow.ColumnDefinitions.Count - 1; i >= 4; i--)
        //                        kZeile.audioZeile.grdEditorRow.ColumnDefinitions[i].Width = new GridLength(0);
        //                    kZeile.audioZeile.brdTrennstrich.Visibility = Visibility.Collapsed;
        //                }
        //            }
        //            else
        //            {
        //                //if (kZeile.audioZeile.grdEditorRow.ColumnDefinitions[2].Width != new GridLength(grpobj.grdEditorTop.ColumnDefinitions[3].Width.Value))
        //                {
        //                    for (int i = kZeile.audioZeile.grdEditorRow.ColumnDefinitions.Count - 1; i >= 4; i--)
        //                        kZeile.audioZeile.grdEditorRow.ColumnDefinitions[i].Width = new GridLength(grpobj.grdEditorTop.ColumnDefinitions[i + 1].Width.Value);

        //                    kZeile.audioZeile.grdEditorRow.ColumnDefinitions[2].MinWidth = grpobj.grdEditorTop.ColumnDefinitions[3].MinWidth;
        //                    kZeile.audioZeile.brdTrennstrich.Visibility = Visibility.Visible;
        //                }
        //            }
        //        }
        //    });
        //}


        //private void rsldTeilZeile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (VM._GrpObjecte.Count > 0 && VM._GrpObjecte[0] != null)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile = VM._GrpObjecte.FirstOrDefault(t => t.visuell)._listZeile
        //                .FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeile.rsldTeilSong == (SliderRange)sender);

        //            //wenn gerade abspielt wird, entsprechend des Trigger jumped_to_start zurücksetzen
        //            if (kZeile._mplayer != null && kZeile._mplayer.NaturalDuration.HasTimeSpan &&
        //                kZeile._mplayer.Position.TotalMilliseconds < kZeile.audioZeile.rsldTeilSong.LowerValue)
        //                kZeile.jumped_to_start = false;
        //            kZeile.aPlaylistTitel.TeilStart = kZeile.audioZeile.rsldTeilSong.LowerValue;
        //            kZeile.aPlaylistTitel.TeilEnde = kZeile.audioZeile.rsldTeilSong.UpperValue;

        //            Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.aPlaylistTitel);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void chkTitel0_0_Click(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel audioZeileVM, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
        //        foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt chkgrpObj in VM._GrpObjecte.FindAll(t => t.visuell))
        //        {
        //            if (chkgrpObj._listZeile.FindAll(t => t.audioZeileVM != null).FirstOrDefault(tt => tt.audioZeileVM == audioZeileVM) != null)
        //            {
        //                grpobj = chkgrpObj;
        //                break;
        //            }
        //        }
        //        if (grpobj == null)
        //            return;

        //        int zeile = grpobj._listZeile.IndexOf(
        //            grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeileVM == audioZeileVM));

        //        if (e != null && e.Source != null)
        //        {
        //            if (!(sender as CheckBox).IsChecked.Value && grpobj._listZeile[zeile].istLaufend)
        //            {
        //                grpobj._listZeile[zeile]._mplayer.Pause();
        //                grpobj._listZeile[zeile]._mplayer.Position = TimeSpan.FromMilliseconds(0);
        //                grpobj._listZeile[zeile].istLaufend = false;

        //            }
        //        }
        //        if ((grpobj.visuell && grpobj.wirdAbgespielt || !grpobj.visuell) ||
        //            (grpobj.visuell && !grpobj.wirdAbgespielt && grpobj.tbtnKlangPause.IsChecked.Value))
        //            VM.abspielProzess(grpobj, (sender as CheckBox).IsChecked.Value, grpobj.wirdAbgespielt, grpobj._listZeile[zeile], e);

        //        if (grpobj.aPlaylist != null)
        //        {
        //            Audio_Playlist_Titel playlisttitel =
        //                grpobj.aPlaylist.Audio_Playlist_Titel.Where(t => t.Audio_TitelGUID ==
        //                    grpobj._listZeile[zeile].aPlaylistTitel.Audio_TitelGUID).FirstOrDefault(
        //                    t => t.Aktiv != (sender as CheckBox).IsChecked.Value);

        //            if (playlisttitel != null)
        //                playlisttitel.Aktiv = (sender as CheckBox).IsChecked.Value;

        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e != null && e.Source != null) VM.SetzeChangeBit(grpobj.aPlaylist);
        //        }
        //        if (grpobj.visuell)
        //        {
        //            VM.CheckAlleAngehakt(grpobj);
        //            if (grpobj.wirdAbgespielt) VM.CheckPlayStandbySongs(grpobj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Auswahlfehler" + Environment.NewLine + "Beim der Prozedure 'chkTitel' ist ein Fehler aufgetreten", ex);
        //    }
        //}

        
        //private void NeueKlangThemeInDB(string titel)
        //{
        //    string themeName = VM.GetNeuenNamen(titel == "" ? "Neues Theme" : titel, 1);
        //    Audio_Theme themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(themeName));

        //    if (themelist == null)
        //    {
        //        Audio_Theme theme = Global.ContextAudio.New<Audio_Theme>();
        //        theme.Name = themeName;
        //        theme.Hintergrund_VolMod = 50;
        //        theme.Klang_VolMod = 50;

        //        //zur datenbank hinzufügen
        //        if (Global.ContextAudio.Insert<Audio_Theme>(theme))               //erfolgreich hinzugefügt
        //        {
        //            Global.ContextAudio.Update<Audio_Theme>(theme);
        //            AktKlangTheme = theme;
        //            for (int i = 0; i <= lbEditorTheme.Items.Count - 1; i++)
        //                if ((lbEditorTheme.Items[i] as ListboxItemIcon).lbText.Content.ToString() == theme.Name)
        //                    lbEditorTheme.SelectedIndex = i;
        //            lbEditorTheme.ScrollIntoView(lbEditorTheme.SelectedItem);
        //        }
        //    }
        //    else
        //    {
        //        ViewHelper.ShowError("Datenbankfehler" + Environment.NewLine + "Theme evtl. schon vorhanden. Bitte wiederholen Sie den Vorgang und wählen einen anderen Titel", new Exception());
        //    }
        //}

        //private void tboxThemeKategorie_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangTheme != null)
        //        {
        //            AktKlangTheme.Kategorie = tboxThemeKategorie.Text;
        //            Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Verlassen des Kategorie-Feldes ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnHotkeysLöschen_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (ViewHelper.ConfirmYesNoCancel("Löschen aller hotkeyListe", "Klicken Sie 'Ja' um alle hotkeyListe aus sämtlichen Plalyisten zu entfernen") == 2)
        //        {
        //            foreach (Audio_Playlist aPlayList in Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null))
        //            {
        //                aPlayList.Key = null;
        //                Global.ContextAudio.Update<Audio_Playlist>(aPlayList);
        //            }
        //            VM.AktualisiereHotKeys();

        //            if (AktKlangPlaylist != null)
        //            {
        //                int war = lbEditor.SelectedIndex;
        //                lbEditor.SelectedIndex = -1;
        //                lbEditor.SelectedIndex = war;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Löschen der Hotkey-Tasten ist ein Fehler aufgetreten.", ex);
        //    }
        //}
        
        //private void slHotkey_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (IsInitialized)
        //        {
        //            ((Slider)sender).ToolTip = Math.Round(e.NewValue) + " %";
        //            VM.hotkeyListe.FindAll(t => t.mp != null).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.hotkey hkey)
        //            {
        //                hkey.mp.Volume = e.NewValue / 100;
        //            });

        //            if (Einstellungen.GeneralHotkeyVolume != (int)Math.Round(((Slider)sender).Value))
        //                Einstellungen.SetEinstellung<int>("GeneralHotkeyVolume", (int)Math.Round(((Slider)sender).Value));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ändern der aktiven Hotkey-Geräuschen ist ein Fehler aufgetreten.", ex);
        //    }
        //}


        //private void cmboxTopHotkey_DropDownOpened(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        VM.notPlayHotkey = true;
        //    /*    ((ComboBox)sender).Items.Clear();
        //        for (int i = 0; i < VM.hotkeyListe.Count; i++)
        //        {
        //            if (VM.hotkeyListe[i].aPlaylistGuid == Guid.Empty)
        //            {
        //                Border brdHotkey = new Border();
        //                brdHotkey.Height = 30;
        //                brdHotkey.Width = 30;
        //                brdHotkey.CornerRadius = new CornerRadius(5);
        //                brdHotkey.Background = Brushes.LightGray;
        //                Label lbl = new Label();
        //                lbl.FontSize = 18;
        //                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
        //                lbl.Padding = new Thickness(5, 2, 5, 5);
        //                lbl.Margin = new Thickness(3, 0, 0, 0);
        //                lbl.FontStyle = FontStyles.Italic;
        //                lbl.Content = Convert.ToChar(VM.hotkeyListe[i].taste);
        //                brdHotkey.Child = lbl;
        //                brdHotkey.Tag = VM.hotkeyListe[i].taste;

        //                ((ComboBox)sender).Items.Add(brdHotkey);
        //            }
        //        }
        //        ((ComboBox)sender).Focus();*/
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Öffnen der Dropdown-Liste ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void cmboxTopHotkey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangPlaylist == null)
        //            return;
        //        if (((ComboBox)sender).SelectedIndex != -1)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //            grpobj.cmboxTopHotkey.Visibility = Visibility.Collapsed;
        //            grpobj.btnTopHotkeySet.Visibility = Visibility.Visible;

        //            if (VM.hotkeyListe.FirstOrDefault(t => t.aPlaylistGuid == AktKlangPlaylist.Audio_PlaylistGUID) != null)
        //                grpobj.btnHotkeyEntfernen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.hotkey hkey = 
        //                VM.hotkeyListe.FirstOrDefault(t => t.taste == Convert.ToInt32(((Border)e.AddedItems[0]).Tag));
        //            if (hkey != null && AktKlangPlaylist != null)
        //            {
        //                hkey.aPlaylistGuid = AktKlangPlaylist.Audio_PlaylistGUID;

        //                Button btnHotKey = new Button();
        //                btnHotKey.Background = Brushes.LightGray;
        //                btnHotKey.Content = Convert.ToChar(hkey.taste);
        //                Audio_Playlist aplylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.aPlaylistGuid);
        //                btnHotKey.ToolTip = aplylist != null ? aplylist.Name : btnHotKey.Content;
        //                btnHotKey.Margin = new Thickness(5, 0, 5, 0);
        //                btnHotKey.Height = 20;
        //                btnHotKey.Width = btnHotKey.Height;
        //                btnHotKey.Tag = hkey;
        //               // btnHotKey.CommandBindings.Add(new CommandBinding(MyCommand, ClickHotkeyButton));

        //                spnlHotkeys.Children.Add(btnHotKey);
        //                grpobj.btnTopHotkeySet.Content = btnHotKey.Content.ToString();
        //            }
        //            if ((ListboxItemIcon)lbEditor.SelectedItem != null)
        //            {
        //                ((ListboxItemIcon)lbEditor.SelectedItem).Focus();
        //                lbEditor.ScrollIntoView(lbEditor.SelectedItem);
        //            }

        //            grpobj.aPlaylist.Key = Convert.ToChar(hkey.taste).ToString();
        //            Global.ContextAudio.Update<Audio_Playlist>(grpobj.aPlaylist);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        VM.notPlayHotkey = false;
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Wechseln der Hotkey-Taste ist ein Fehler aufgetreten.", ex);
        //    }
        //}


        //private void btnHotkeyEntfernenX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        btnHotkey hkey = VM.hotkeyListe.FirstOrDefault(t => t.VM.aPlaylistGuid == grpobj.aPlaylist.Audio_PlaylistGUID);
        //        //MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.hotkey 
        //        if (hkey == null)
        //            return;

        //        foreach (btnHotkey btnHotKey in VM.hotkeyListUsed)//spnlHotkeys.Children.OfType<Button>())
        //        {
        //            if (((btnHotkey)btnHotKey.Tag).VM.taste == hkey.VM.taste)
        //            { //MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.hotkey
        //                VM.hotkeyListUsed.Remove(btnHotKey);
        //                //spnlHotkeys.Children.Remove(btnHotKey);
        //                grpobj.btnTopHotkeySet.Content = "nicht definiert";
        //                grpobj.cmboxTopHotkey.SelectedIndex = -1;
        //                hkey.VM.aPlaylistGuid = Guid.Empty;

        //                grpobj.aPlaylist.Key = null;
        //                grpobj.aPlaylist.Modifiers = null;
        //                Global.ContextAudio.Update<Audio_Playlist>(grpobj.aPlaylist);
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Entfernen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void UpdateHotkeys()
        //{
        //    spnlHotkeys.Children.RemoveRange(0, spnlHotkeys.Children.Count);
        //    foreach (Audio_Playlist aPlayList in Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null))
        //    {
        //        hotkey hkey = hotkeyListe.FirstOrDefault(t => t.taste == Convert.ToInt32(Convert.ToChar(aPlayList.Key)));
        //        if (hkey != null)
        //        {
        //            hkey.aPlaylistGuid = aPlayList.Audio_PlaylistGUID;

        //            Button btnHotKey = new Button();
        //            btnHotKey.Background = Brushes.LightGray;
        //            btnHotKey.Content = Convert.ToChar(hkey.taste);
        //            btnHotKey.ToolTip = aPlayList.Name;
        //            btnHotKey.Margin = new Thickness(5, 0, 5, 0);
        //            btnHotKey.Height = 20;
        //            btnHotKey.Width = btnHotKey.Height;
        //            btnHotKey.Tag = hkey;
        //            btnHotKey.Click += btnHotKey_Click;

        //            spnlHotkeys.Children.Add(btnHotKey);
        //        }
        //    }
        //}

        //private void btnHotKey_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.hotkey hkey = 
        //            (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.hotkey)((Button)sender).Tag;
        //        if (hkey.aPlaylistGuid != null)
        //        {
        //            Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.aPlaylistGuid);

        //            int zuspielen = (new Random()).Next(0, aPlaylist.Audio_Playlist_Titel.Count - 1);

        //            Audio_Titel aTitel = aPlaylist.Audio_Playlist_Titel.ToList().ElementAt(zuspielen).Audio_Titel;

        //            hkey.mp.MediaEnded += mp_failed_ended;
        //            hkey.mp.MediaFailed += mp_failed_ended;
        //            hkey.mp.Open(new Uri(aTitel.Pfad + "\\" + aTitel.Datei));
        //            if (hkey.mp.Volume != slHotkey.Value / 100)
        //                hkey.mp.Volume = slHotkey.Value / 100; // Slider des PListModifikator
        //            hkey.mp.Play();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswählen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        

        //private void tbThemeThemeNormSize_Click(object sender, RoutedEventArgs e)
        //{
        //    imgThemeThemeNorm.Visibility = tbThemeThemeNormSize.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
        //    //AktualisierePlaylistThemes();
        //}


        //private void AktualisierePlaylistThemes()
        //{
            //List<Audio_Theme> aThemes = Global.ContextAudio.ThemeListe;
            //int cnt = 0;
            //bool neu;
            //wpnlPListThemes.ItemHeight = tbThemeThemeNormSize.IsChecked.Value ? 25 : 50;
            //foreach (Audio_Theme aTheme in aThemes.FindAll(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            //{
            //    if (!chkPListFilter(tbThemesFilterAll.Text, aTheme.Name) && (aTheme.Kategorie == null ? true : !chkPListFilter(tbThemesFilterAll.Text, aTheme.Kategorie)))
            //        continue;
            //    cnt++;
            //    neu = wpnlPListThemes.Children.Count < cnt;

            //    grdThemeButton grdThButton = neu ? new grdThemeButton() : (grdThemeButton)wpnlPListThemes.Children[cnt - 1];
            //    grdThButton.Tag = aTheme.Audio_ThemeGUID;
            //    grdThButton.Height = tbThemeThemeNormSize.IsChecked.Value ? 22 : 42;
            //    grdThButton.tbtnTheme.Tag = aTheme.Audio_ThemeGUID;
            //    //grdThButton.lblTheme.Content = aTheme.Name;
            //    grdThButton.brdKategorie.Visibility = (aTheme.Kategorie != null && aTheme.Kategorie != "" && !tbThemeThemeNormSize.IsChecked.Value) ?
            //        Visibility.Visible : Visibility.Collapsed;
            //    grdThButton.lblKategorie.Content = aTheme.Kategorie;
            //    grdThButton.chkbxPlus.Visibility = tbThemeThemeNormSize.IsChecked.Value ? Visibility.Hidden : Visibility.Visible;
            //    grdThButton.chkbxPlus.Tag = aTheme;
            //    grdThButton.chkbxPlus.IsChecked = aTheme.NurGeräusche;

            //    Audio_Playlist aPListHintergrund = aTheme.Audio_Playlist.FirstOrDefault(t => t.Hintergrundmusik);
            //    string ttip = aPListHintergrund != null ? "Hintergrund-Musik:   " + aPListHintergrund.Name + Environment.NewLine : "";

            //    Int16 i = 1;
            //    List<Audio_Playlist> aPListGeräusche = aTheme.Audio_Playlist.Where(t => !t.Hintergrundmusik).ToList();
            //    foreach (Audio_Playlist aPList in aPListGeräusche.OrderBy(t => t.Name))
            //    {
            //        ttip += "Geräusch " + i + ":   " + aPList.Name + Environment.NewLine;
            //        i++;
            //    }
            //    i = 1;
            //    foreach (Audio_Theme aUnterTheme in aTheme.Audio_Theme1.
            //        Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            //    {
            //        ttip += "Sub-Theme " + i + ":   " + aUnterTheme.Name + Environment.NewLine;
            //        i++;
            //    }
            //    grdThButton.tbtnTheme.ToolTip = ttip;

            //    if (neu)
            //    {
            //        grdThButton.tbtnTheme.Checked += tbtnTheme_Checked;
            //        grdThButton.tbtnTheme.Unchecked += tbtnTheme_UnChecked;
            //        grdThButton.chkbxPlus.Checked += chbxThemeNurGeräusche_Checked;
            //        grdThButton.chkbxPlus.Unchecked += chbxThemeNurGeräusche_Checked;
            //        wpnlPListThemes.Children.Add(grdThButton);
            //    }
            //}
            //wpnlPListThemes.Children.RemoveRange(cnt, wpnlPListThemes.Children.Count - cnt);
        //}
               
     
        //private void btnNeuePlaylist_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string NeuePlaylist = VM.GetNeuenNamen("NeuePlayliste", 0);
                
        //        Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
        //        playlist.Name = NeuePlaylist.ToString();
        //        if (rbEditorKlang.IsChecked.Value)
        //            playlist.Hintergrundmusik = false;
        //        else
        //            playlist.Hintergrundmusik = true;

        //        //zur datenbank hinzufügen
        //        if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
        //        {
        //            AktKlangPlaylist = playlist;
        //            playlist.MaxSongsParallel = 1;

        //            lbEditor.SelectedIndex = -1;
        //            for (int i = 0; i <= lbEditor.Items.Count - 1; i++)
        //                if ((lbEditor.Items[i] as ListboxItemIcon).lbText.Content.ToString() == playlist.Name)
        //                    lbEditor.SelectedIndex = i;
        //        }
        //        lbEditor.ScrollIntoView(lbEditor.SelectedItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Erstellen einer neuen Playlist ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void tiMusik_Loaded(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (lbMusik.Items.Count == 0)
        //            AktualisiereMusikPlaylist();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Wechseln auf das TabItem ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void lbMusiktitellist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if ((lbMusiktitellist.SelectedIndex >= 0) &&
        //           (((ListBoxItem)lbMusiktitellist.SelectedItem).Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString()))         // Red))
        //        {
        //            if (lbMusik.SelectedIndex == -1)
        //            {
        //                lbMusik.SelectionChanged -= lbMusik_SelectionChanged;
        //                lbMusik.SelectedIndex = Convert.ToInt16(lbMusik.Tag);
        //                lbMusik.SelectionChanged += lbMusik_SelectionChanged;
        //            }
        //            if (e != null && e.RemovedItems.Count == 0 && BGPlayer.AktPlaylist.Repeat != null)
        //                VM.RenewMusikNochZuSpielen();
        //            chkbxPlayRange.IsChecked = false;
        //            rsldTeilSong.Visibility = Visibility.Hidden;
        //            rsldTeilSong.LowerValue = 0;
        //            rsldTeilSong.UpperValue = 100000;

        //            ListBoxItem lbItem = (ListBoxItem)lbMusiktitellist.SelectedItem;

        //            Audio_Titel titel = null;
        //            Audio_Playlist_Titel aPlayListtitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)lbItem.Tag);
        //            if (aPlayListtitel != null)
        //                titel = BGPlayer.AktTitel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)lbItem.Tag);

        //            if (titel == null)
        //            {
        //                lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
        //                lbItem.ToolTip = "Keine Titel-Informationen gefunden";

        //                lbMusik_SelectionChanged(lbMusiktitellist, e);
        //                lbMusiktitellist.Tag = -1;
        //                btnBGPrev.IsEnabled = false;
        //            }
        //            else
        //            {
        //                if (!File.Exists(titel.Pfad + "\\" + titel.Datei))
        //                {
        //                    titel = VM.setTitelStdPfad(titel);
        //                    if (File.Exists(titel.Pfad + "\\" + titel.Datei))
        //                        Global.ContextAudio.Update<Audio_Titel>(titel);
        //                }

        //                if (Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei) ||
        //                    !Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)))
        //                {
        //                    lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
        //                    lbItem.ToolTip = "Datei nicht gefunden. -> " + titel.Pfad + "\\" + titel.Datei;
        //                    lbMusiktitellist.Tag = -1;
        //                    btnBGPrev.IsEnabled = false;
        //                    VM.SpieleNeuenMusikTitel(Guid.Empty);
        //                }
        //                else
        //                {
        //                    if (Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)) &&
        //                        File.Exists(titel.Pfad + "\\" + titel.Datei))
        //                    {
        //                        grdSongInfo.Visibility = Visibility.Visible;

        //                        //lblBgTimeMax.Content = "--:--";
        //                        lblBgTitel.Content = "";
        //                        lblBgAlbum.Content = "";
        //                        lblBgArtist.Content = "";
        //                        lblBgJahr.Content = "";
        //                        lblBgGenre.Content = "";

        //                        BGPlayer.AktPlaylistTitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(BGPlayer.AktPlaylist, titel).First();
        //                        chkbxPlayRange.IsChecked = BGPlayer.AktPlaylistTitel.TeilAbspielen;

        //                        if (BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
        //                            BGPlayeraktiv = BGPlayeraktiv == 0 ? 1 : 0;

        //                        if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null && BGPlayer.BG[BGPlayeraktiv].mPlayer.Position.TotalMilliseconds > 0 &&
        //                            !BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
        //                        {
        //                            if (lbMusiktitellist.SelectedIndex != -1)
        //                            {
        //                                if (!BGPlayer.BG[BGPlayeraktiv].FadingOutStarted && lbMusiktitellist.SelectedIndex != -1)
        //                                {
        //                                    BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
        //                                    VM.BGFadingOut(BGPlayer.BG[BGPlayeraktiv], false, false);
        //                                }
        //                                BGPlayeraktiv = (BGPlayeraktiv == 0) ? 1 : 0;
        //                            }
        //                        }
        //                        BGPlayer.BG[BGPlayeraktiv].isPaused = false;

        //                        BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals((Guid)lbItem.Tag));
        //                        BGPlayer.BG[BGPlayeraktiv].mPlayer =
        //                            VM.PlayFile(false, null, -1, BGPlayer.BG[BGPlayeraktiv].mPlayer, titel.Pfad + "\\" + titel.Datei, slBGVolume.Value, true);

        //                        btnBGPrev.IsEnabled = true;
        //                        btnBGStoppen.IsEnabled = true;

        //                        if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
        //                        {
        //                            btnBGAbspielen.Tag = true;
        //                            imgbtnBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
        //                            pbarBGSong.Value = 0;
        //                            if (((MusikZeile)lbPListMusik.SelectedItem) != null)
        //                                ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Value = pbarBGSong.Value;

        //                            if (BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.HasTimeSpan)
        //                            {
        //                                pbarBGSong.Maximum = BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
        //                                rsldTeilSong.Minimum = 0;
        //                                rsldTeilSong.Maximum = BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
        //                                if (BGPlayer.AktPlaylistTitel.TeilAbspielen)
        //                                {
        //                                    rsldTeilSong.LowerValue = BGPlayer.AktPlaylistTitel.TeilStart.Value;
        //                                    rsldTeilSong.UpperValue = BGPlayer.AktPlaylistTitel.TeilEnde.Value;
        //                                    rsldTeilSong.Visibility = Visibility.Visible;
        //                                }
        //                                else
        //                                {
        //                                    rsldTeilSong.LowerValue = 0;
        //                                    rsldTeilSong.UpperValue = pbarBGSong.Maximum;
        //                                }
        //                                if (((MusikZeile)lbPListMusik.SelectedItem) != null)
        //                                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Maximum = pbarBGSong.Maximum;
        //                                //lblBgTimeMax.Content = BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
        //                            }
        //                            btnBGNext.IsEnabled = true;
        //                            btnBGAbspielen.IsEnabled = true;
        //                            starsUpdate();
        //                            grdSongInfo.Visibility = Visibility.Visible;

        //                            VM.MusikProgBarTimer.Tag = -1;
        //                            VM.MusikProgBarTimer.Start();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        grdSongInfo.Visibility = Visibility.Hidden;
        //                        lbMusiktitellist.SelectedIndex = -1;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Nach Auswählen ist ein unvorhergesehner Fehler aufgetreten", ex);
        //    }
        //}
        
        //private void chkVolMove0_0_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
        //        foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt chkgrpObj in VM._GrpObjecte)
        //        {
        //            if (chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkVolMove == (CheckBox)sender) != null)
        //            {
        //                grpobj = chkgrpObj;
        //                break;
        //            }
        //        }
        //        if (grpobj == null)
        //            return;
        //        int zeile = grpobj._listZeile.IndexOf(
        //            grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkVolMove == (Control)sender));

        //        grpobj.anzVolChange = Convert.ToUInt16(
        //            grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkVolMove.IsChecked == true).Count);

        //        if (grpobj.anzPauseChange == grpobj._listZeile.Count)
        //            grpobj.chkbxTopVolChange.IsChecked = true;
        //        else
        //            grpobj.chkbxTopVolChange.IsChecked = false;

        //        grpobj._listZeile[zeile].aPlaylistTitel.VolumeChange = ((CheckBox)sender).IsChecked.Value;
        //        CheckAlleAngehakt(grpobj);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Auswahlfehler" + Environment.NewLine + "Beim Anklicken der Checkbox 'chkVolMove' ist ein Fehler aufgetreten", ex);
        //    }
        //}

        //private void sldKlangPause0_0_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
        //        foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt chkgrpObj in VM._GrpObjecte)
        //        {
        //            if (chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.sldKlangPause == (Slider)sender) != null)
        //            {
        //                grpobj = chkgrpObj;
        //                break;
        //            }
        //        }
        //        if (grpobj == null)
        //            return;
        //        int zeile = grpobj._listZeile.IndexOf(
        //            grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeile.sldKlangPause == (Control)sender));

        //        grpobj._listZeile[zeile].aPlaylistTitel.Pause = Convert.ToInt32(Math.Round(((Slider)sender).Value));

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) VM.SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Verlassen des Sliders 'sldKlangPause' ist ein Fehler aufgetreten", ex);
        //    }
        //}

        //private void chkKlangPauseMove0_0_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
        //        foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt chkgrpObj in VM._GrpObjecte)
        //        {
        //            if (chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (CheckBox)sender) != null)
        //            {
        //                grpobj = chkgrpObj;
        //                break;
        //            }
        //        }
        //        if (grpobj == null)
        //            return;
        //        int zeile = grpobj._listZeile.IndexOf(
        //            grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (Control)sender));

        //        grpobj.anzPauseChange = Convert.ToUInt16(
        //            grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkKlangPauseMove.IsChecked == true).Count);

        //        grpobj._listZeile[zeile].aPlaylistTitel.PauseChange = ((CheckBox)sender).IsChecked.Value;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);

        //        CheckAlleAngehakt(grpobj);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken der CheckBox 'chkKlangPauseMove' ist ein Fehler aufgetreten", ex);
        //    }
        //}
                

        //private void imgTrash0_0_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (VM.isDeleting)
        //            return;
        //        VM.isDeleting = true;

        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile = (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile)((Image)sender).Tag;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);

        //        if (kZeile.aPlaylistTitel.Audio_Playlist == null || kZeile.aPlaylistTitel.Audio_Titel == null)     // sehr schnell geklickt - Daten schon gelöscht
        //            return;

        //        //Stopp abspielen
        //        if (kZeile.audioZeileVM.Checked && kZeile.istLaufend) //.chkTitel.IsChecked.Value 
        //        {
        //            kZeile.audioZeileVM.Checked = false;// .audioZeile.chkTitel.IsChecked = false;
        //            chkTitel0_0_Click(kZeile.audioZeileVM, new RoutedEventArgs()); //.chkTitel
        //        }

        //        int zeile = grpobj._listZeile.IndexOf(
        //            grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeile.imgTrash == (Image)sender));

        //        grpobj.lbEditorListe.Items.Remove(kZeile.audioZeile);
        //        grpobj._listZeile.Remove(kZeile);

        //        Global.ContextAudio.RemoveTitelFromPlaylist(kZeile.aPlaylistTitel.Audio_Playlist, kZeile.aPlaylistTitel.Audio_Titel);

        //        sortPlaylist(grpobj.aPlaylist, zeile);
        //        grpobj.Gespielt.Clear();

        //        if (kZeile.aPlaylistTitel != null)
        //            grpobj.NochZuSpielen.RemoveAll(t => t.Equals(kZeile.aPlaylistTitel.Audio_TitelGUID));

        //        VM.CheckBtnGleicherPfad(AktKlangPlaylist);  // grpobj);
        //        GC.GetTotalMemory(true);
        //    }
        //    finally
        //    {
        //        VM.isDeleting = false;
        //    }
        //    //	catch (Exception ex)
        //    //	{
        //    //		ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Löschvorgang der Zeile ist in der Prozedure 'imgTrash' ist ein Fehler aufgetreten", ex);
        //    //       VM.isDeleting = false;
        //    //	}
        //}

        //private void rbEditorEditPlaylist_Checked(object sender, RoutedEventArgs e)
        //{
            
        //    AktualisiereEditorPlaylist();
        //    lbEditor.ToolTip = null;
        //    //if (lbEditor.Items.Count > 0 && (lbEditor.Items[0] as ListboxItemIcon)._animateOnMouseEvent)
        //    //{
        //    //    foreach (ListboxItemIcon lbi in lbEditor.Items)
        //    //    {
        //    //        lbi.PreviewMouseLeftButtonDown -= lbiEditorPlaylist_PreviewMouseLeftButtonDown;
        //    //        lbi.PreviewMouseMove -= lbiEditorPlaylist_MouseMove;
        //    //        lbi._animateOnMouseEvent = true;
        //    //    }
        //    //}
        //    grdEditorList.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Auto);
        //    exKlangPList.IsExpanded = true;
        //    grdEditorListe.Visibility = Visibility.Visible;
        //    brdEditorListe.Visibility = Visibility.Visible;
        //    rbEditorEditTheme.Tag = true;


        //    if (((RadioButton)sender).Name == "rbEditorEditPList")
        //        lbEditorTheme.SelectedIndex = -1;

        //    wpnlEditorTopThemesThemes.Children.RemoveRange(1, wpnlEditorTopThemesThemes.Children.Count);

        //    lblEditorListName.Content = "Playlist-Name";
        //    //tboxEditorName.Text = lbEditor.SelectedIndex == -1 ? GetNeuenNamen("NeuePlayliste", 0) : ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content.ToString();

        //    //tiPlus_MouseUp(false, null); 
        //}


        //private void rbTopIstKlangPlaylist_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (AktKlangPlaylist != null)
        //        {
        //            if (BGPlayer.AktPlaylist == AktKlangPlaylist && lbMusik.SelectedIndex != -1)
        //            {
        //                btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //                lbMusik.SelectedIndex = -1;
        //                lbMusik.Tag = -1;
        //                lbMusiktitellist.Items.Clear();
        //            }

        //            grpobj.istMusik = false;
        //            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);

        //            //ZeigeZeileKlangSpalten(grpobj, true);

        //            for (UInt16 i = 0; i < grpobj._listZeile.Count; i++)
        //            {
        //                if (grpobj._listZeile[i].audioZeile.chkTitel.IsChecked == true)
        //                    grpobj._listZeile[i].istStandby = true;
        //            }

        //            //AktualisiereEditorPlaylist();

        //            ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
        //            ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.ToolTip = "Geräusche-Playlist";
        //        }
        //        //else
        //        //	ZeigeKlangTop(grpobj, true);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void rbTopIstMusikPlaylist_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (AktKlangPlaylist != null)
        //        {
        //            // *****************  In Playlist-Player herausnehmen  **********************
        //            if (lbPListGeräusche.Items.Count != 0)
        //            {
        //                Int16 i = 0;
        //                while (i < lbPListGeräusche.Items.Count && (Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag != AktKlangPlaylist.Audio_PlaylistGUID)
        //                    i++;

        //                if (i < lbPListGeräusche.Items.Count && (Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
        //                {
        //                    if (((MusikZeile)lbPListGeräusche.Items[i]).tbtnCheck.IsChecked.Value)
        //                        ((MusikZeile)lbPListGeräusche.Items[i]).tbtnCheck.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
        //                    lbPListGeräusche.Items.RemoveAt(i);
        //                }
        //            }
        //            // *************************************************************************

        //            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        //            //AktualisierePListPlaylist();

        //            VM.AlleKlangSongsAus(grpobj, false, false, false);

        //            if (grpobj.wirdAbgespielt)
        //            {
        //                grpobj.wirdAbgespielt = false;
        //                ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
        //                ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(1, 2, 0, 0);
        //            }

        //            //VM.ZeigeZeileKlangSpalten(grpobj, false);

        //            AktKlangPlaylist.MaxSongsParallel = 1;
        //            grpobj.istMusik = true;

        //            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        //            //AktualisiereEditorPlaylist();

        //            ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png"));
        //            ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.ToolTip = "Musik-Playlist";

        //        }
        //        //else
        //        //ZeigeKlangTop(grpobj, false);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void tboxEditorName_KeyUp(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Key == Key.Return)
        //        {
        //            tboxEditorName.Text = validateString(tboxEditorName.Text);
                    
        //            if (rbEditorEditTheme.IsChecked.Value)
        //            {
        //                if (AktKlangTheme == null)
        //                {
        //                    List<Audio_Theme> klTheme = Global.ContextAudio.ThemeListe.FindAll(t => t.Name.Equals(tboxEditorName.Text)).ToList();
        //                    if (klTheme.Count == 1)
        //                    {
        //                        AktKlangTheme = klTheme[0];
        //                        ((TabItem)lbEditorTheme.SelectedItem).Content = tboxEditorName.Text;
        //                        tboxEditorName.Text = AktKlangTheme.Name;
        //                    }
        //                    else
        //                        NeueKlangThemeInDB(tboxEditorName.Text);
        //                }
        //                AktKlangTheme.Name = tboxEditorName.Text;
        //                ((ListboxItemIcon)lbEditorTheme.SelectedItem).lbText.Content = AktKlangTheme.Name;
        //                Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        //                ((TextBox)(sender)).Background = null;
        //            }
        //            else
        //            {
        //                CreateNewAktKlangPlaylist();
                        
        //            }
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void CreateNewAktKlangPlaylist()
        //{
        //    if (AktKlangPlaylist == null || lbEditor.SelectedItem == null)
        //    {
        //        List<Audio_Playlist> klPlaylist = Global.ContextAudio.PlaylistListe.FindAll(t => t.Name.Equals(tboxEditorName.Text)).ToList();
        //        if (klPlaylist.Count == 1)
        //        {
        //            AktKlangPlaylist = klPlaylist[0];
        //            tboxEditorName.Text = AktKlangPlaylist.Name;
        //        }
        //        else
        //        {
        //            VM.NeueKlangPlaylistInDB(tboxEditorName.Text);
        //            for (int i = 0; i < lbEditor.Items.Count; i++)
        //                if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
        //                {
        //                    //lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
        //                    //lbEditor.SelectedIndex = i;
        //                    //lbEditor.SelectionChanged += lbEditor_SelectionChanged;
        //                    lbEditor.ScrollIntoView(lbEditor.SelectedItem);
        //                }
        //        }
        //    }
        //    AktKlangPlaylist.Name = tboxEditorName.Text;

        //    ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content = AktKlangPlaylist.Name;
        //    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        //    tboxEditorName.Background = null;
        //}

        //private void btnKlangNeuTheme_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        NeueKlangThemeInDB("");
        //        tboxEditorName.Focus();
        //    }
        //    catch (Exception) { }
        //}

        //private void tcEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        string s = "";

        //        if (VM._GrpObjecte.Count == 0)
        //            tiPlus_MouseUp(false, null);  //1. Auswahl
        //        else
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //            if (grpobj == null)
        //                return;
        //            List<Audio_Playlist> playlistliste = null;

        //            s = tboxEditorName.Text;

        //            if (playlistliste != null && playlistliste.Count != 0 && grpobj._listZeile.Count > 0)
        //            {
        //                List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]);
        //                AktKlangPlaylist = playlistliste[0];

        //                if (AktKlangPlaylist.Hintergrundmusik)
        //                    grpobj.rbTopIstMusikPlaylist.IsChecked = true;
        //                else
        //                    grpobj.rbTopIstKlangPlaylist.IsChecked = true;

        //                tboxEditorName.Text = AktKlangTheme.Name;
        //                grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
        //                grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;

        //                //if (titelliste.Count > 0)
        //                //    ZeigeZeileKlangSpalten(grpobj, !playlistliste[0].Hintergrundmusik);
        //            }
        //            else
        //            {
        //                //lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
        //                //lbEditor.SelectedIndex = -1;
        //                //lbEditor.SelectionChanged += lbEditor_SelectionChanged;
        //                tboxEditorName.Text = s;

        //                if (playlistliste != null && grpobj._listZeile.Count > 0)
        //                {
        //                    grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
        //                    grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;
        //                }
        //                else
        //                {
        //                    grpobj.tbTopKlangKategorie.Text = "";
        //                    grpobj.tbTopKlangKategorie.Tag = null;
        //                }
        //            }

        //            SelektiereKlangZeile(s);
        //        }
        //        if (tcAudioPlayer.SelectedItem == tiEditor && lbEditorTheme.Tag == null)
        //            VM.CheckBtnGleicherPfad(AktKlangPlaylist);  // VM._GrpObjecte.FirstOrDefault(t => t.visuell));
        //    }
        //    catch (Exception) { }
        //}

        //private void SelektiereKlangZeile(string klangGUID)
        //{
        //    int i = 0;
        //    while (i <= lbEditor.Items.Count - 1)
        //    {
        //        if (((ListboxItemIcon)lbEditor.Items[i]).lbText.Content.ToString() == klangGUID)
        //        {
        //            lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
        //            lbEditor.SelectedIndex = i;
        //            lbEditor.SelectionChanged += lbEditor_SelectionChanged;
        //            break;
        //        }
        //        i++;
        //    }
        //}

        //private void ZeigeKlangGerneral(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj, bool sichtbar)
        //{
        //    if (sichtbar)
        //    {
        //        if (grpobj != null)
        //        {
        //            grpobj.gboxTopTyp.Visibility = Visibility.Visible;
        //            grpobj.btnTopKlangOpen.Visibility = Visibility.Visible;
        //        }
        //    }
        //    else
        //    {
        //        if (grpobj != null)
        //        {
        //            //grpobj.tbTopKlangKategorie.Visibility = Visibility.Visible;
        //            grpobj.gboxTopTyp.Visibility = Visibility.Hidden;
        //            grpobj.btnTopKlangOpen.Visibility = Visibility.Hidden;
        //        }
        //        lbEditor.SelectedIndex = -1;
        //    }
        //}

        //public void tiPlus_MouseUp(bool virtuell, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        VM.tiErstellt++;
        //        Int16 objGruppe = Convert.ToInt16(VM.tiErstellt);
        //        //AktKlangPlaylist = null;               

        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = new MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt();

        //        grpobj.objGruppe = VM.tiErstellt;
        //        grpobj.wartezeitTimer.Tick += new EventHandler(wartezeitTimer_Tick);

        //        if (!virtuell)
        //        {
        //            tboxEditorName.Text = VM.GetNeuenNamen("NeuePlayliste", 0);

        //            tbtnKlangPause1.Tag = VM.tiErstellt;
        //            chkbxTopAktiv1.Tag = objGruppe;
        //            chkbxTopVolChange1.Tag = objGruppe;
        //            chkbxTopPauseChange1.Tag = objGruppe;

        //            grpobj.playlistName = tboxEditorName.Text;
        //            grpobj.aPlaylist = new Audio_Playlist();
        //            grpobj.aPlaylist.Name = tboxEditorName.Text;

        //            //grpobj.sviewer = sviewer1;
        //            grpobj.grdEditor = grdEditor1;
        //            grpobj.grdEditorTop = grdEditorTop1;
        //            //grpobj.wpnl = wpnl1;
        //            grpobj.lbEditorListe = lbEditorListe;
        //            grpobj.tbtnKlangPause = tbtnKlangPause1;

        //            grpobj.tbTopFilter = tbEditorTopFilter1;
        //            grpobj.btnTopFilter = btnKlangTopFilter1;
        //            grpobj.brdTopKlangKategorie = brdTopKlangKategorie1;
        //            grpobj.tbTopKlangKategorie = tboxTopKlangKategorie1;

        //            grpobj.gboxTopSongsParallel = gboxTopSongsParallel1;
        //        //    grpobj.intbxSongsParallel = intbxSongsParallel1;

        //            grpobj.gboxTopMusikSort = gboxTopMusikSort1;
        //            grpobj.btnTopMusikAbisZ = btnTopMusikAbisZ1;
        //            grpobj.btnTopMusik1bis9 = btnTopMusik1bis91;

        //            grpobj.gboxTopTyp = gboxTopTyp1;
        //            grpobj.rbTopIstKlangPlaylist = rbIstTopKlangPlaylist1;
        //            grpobj.rbTopIstMusikPlaylist = rbIstTopMusikPlaylist1;

        //            grpobj.chkbxTopAktiv = chkbxTopAktiv1;
        //            grpobj.spnlTopGeräuschIcon = spnlTopGeräuschIcon1;
        //            grpobj.btnTopVolMin = btnTopVolMin1;
        //            grpobj.btnTopVolDown = btnTopVolDown1;
        //            grpobj.btnTopVolUp = btnTopVolUp1;
        //            grpobj.btnTopVolMax = btnTopVolMax1;
        //            grpobj.chkbxTopVolChange = chkbxTopVolChange1;
        //            grpobj.btnTopPauseMin = btnTopPauseMin1;
        //            grpobj.btnTopPauseDown = btnTopPauseDown1;
        //            grpobj.btnTopPauseUp = btnTopPauseUp1;
        //            grpobj.btnTopPauseMax = btnTopPauseMax1;
        //            grpobj.chkbxTopPauseChange = chkbxTopPauseChange1;
        //            grpobj.btnTopVolMinMinus = btnTopVolMinMinus1;
        //            grpobj.btnTopVolMinPlus = btnTopVolMinPlus1;
        //            grpobj.btnTopVolMaxMinus = btnTopVolMaxMinus1;
        //            grpobj.btnTopVolMaxPlus = btnTopVolMaxPlus1;
        //            grpobj.brdTrennstrich = brdTrennstrich1;
        //            grpobj.btnTopPauseMinMinus = btnTopPauseMinMinus1;
        //            grpobj.btnTopPauseMinPlus = btnTopPauseMinPlus1;
        //            grpobj.btnTopPauseMaxMinus = btnTopPauseMaxMinus1;
        //            grpobj.btnTopPauseMaxPlus = btnTopPauseMaxPlus1;

        //            grpobj.btnTopHotkeySet = btnTopHotkeySet1;
        //            grpobj.spnlTopHotkey = spnlTopHotkey1;
        //            grpobj.cmboxTopHotkey = cmboxTopHotkey1;
        //            grpobj.btnHotkeyEntfernen = btnHotkeyEntfernen1;
        //            grpobj.btnTopKlangOpen = btnTopKlangOpen1;
        //            grpobj.btnKlangUpdateFiles = btnKlangUpdateFiles1;
        //        }
        //        else  //if (virtuell)
        //        {
        //            ToggleButton tbtn = new ToggleButton();
        //            tbtn.Name = "tbtnKlangPause" + objGruppe;
        //            tbtn.Tag = VM.tiErstellt;
        //            tbtn.Checked += tbtnKlangPauseX_Checked;
        //            tbtn.Unchecked += tbtnKlangPauseX_Unchecked;
        //            grpobj.tbtnKlangPause = tbtn;
        //        }
        //        //VM._GrpObjecte.Add(grpobj);
        //        //**********************************************************************************************************

        //        if (!virtuell)
        //        {
        //            // AktKlangPlaylist = grpobj.aPlaylist;
        //            //lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
        //            //lbEditor.SelectedIndex = -1;
        //            //lbEditor.SelectionChanged += lbEditor_SelectionChanged;

        //            //if (tcAudioPlayer.SelectedItem == tiEditor)
        //            //    tboxEditorName.Text = lbEditor.SelectedIndex == -1 ? tboxEditorName.Text : ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content.ToString();
        //            //grpobj.tbTopKlangKategorie.Text = "";
        //            //grpobj.tbTopKlangKategorie.Tag = VM._GrpObjecte[VM._GrpObjecte.Count - 1].aPlaylist.Audio_PlaylistGUID;
        //            ZeigeKlangGerneral(grpobj, true);

        //        //    grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void tboxVolMin0_X_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    try
        //    {
        //        foreach (var item in e.Text)
        //            e.Handled = !char.IsDigit(item);
        //    }
        //    catch (Exception) { }
        //}

        //private void tboxVolMin0_X_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangPlaylist != null)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //            if (grpobj == null)
        //                return;
        //            int zeile = grpobj._listZeile.IndexOf(
        //                grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxVolMin == (Control)sender));

        //            if (Convert.ToInt32(((TextBox)(sender)).Text) > grpobj._listZeile[zeile].volMax_wert)
        //                ((TextBox)(sender)).Text = grpobj._listZeile[zeile].volMax_wert.ToString();
        //            grpobj._listZeile[zeile].aPlaylistTitel.VolumeMin = Convert.ToInt16(((TextBox)(sender)).Text);
        //            if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxVolMax.Text) < grpobj._listZeile[zeile].aPlaylistTitel.VolumeMin)
        //                grpobj._listZeile[zeile].audioZeile.tboxVolMax.Text = Convert.ToString(grpobj._listZeile[zeile].aPlaylistTitel.VolumeMin);

        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void tboxVolMax0_X_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangPlaylist != null)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //            if (grpobj == null)
        //                return;
        //            int zeile = grpobj._listZeile.IndexOf(
        //                grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxVolMax == (Control)sender));

        //            if (Convert.ToInt32(((TextBox)(sender)).Text) < grpobj._listZeile[zeile].volMin_wert)
        //                ((TextBox)(sender)).Text = grpobj._listZeile[zeile].volMin_wert.ToString();
        //            grpobj._listZeile[zeile].aPlaylistTitel.VolumeMax = Convert.ToInt16(((TextBox)(sender)).Text);

        //            if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxVolMin.Text) > grpobj._listZeile[zeile].aPlaylistTitel.VolumeMax)
        //                grpobj._listZeile[zeile].audioZeile.tboxVolMin.Text = Convert.ToString(grpobj._listZeile[zeile].aPlaylistTitel.VolumeMax);

        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void tboxPauseMin0_X_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangPlaylist != null)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //            if (grpobj == null)
        //                return;
        //            int zeile = grpobj._listZeile.IndexOf(
        //                grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxPauseMin == (Control)sender));

        //            if (Convert.ToInt32(((TextBox)(sender)).Text) > grpobj._listZeile[zeile].pauseMax_wert)
        //                ((TextBox)(sender)).Text = grpobj._listZeile[zeile].pauseMax_wert.ToString();
        //            grpobj._listZeile[zeile].aPlaylistTitel.PauseMin = Convert.ToInt16(((TextBox)(sender)).Text);

        //            if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text) > grpobj._listZeile[zeile].aPlaylistTitel.PauseMax)
        //                grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text = Convert.ToString(grpobj._listZeile[zeile].aPlaylistTitel.PauseMax);

        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void tboxPauseMax0_X_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangPlaylist != null)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //            if (grpobj == null)
        //                return;
        //            int zeile = grpobj._listZeile.IndexOf(
        //                grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxPauseMax == (Control)sender));

        //            if (Convert.ToInt32(((TextBox)(sender)).Text) < grpobj._listZeile[zeile].pauseMin_wert)
        //                ((TextBox)(sender)).Text = grpobj._listZeile[zeile].pauseMin_wert.ToString();
        //            grpobj._listZeile[zeile].aPlaylistTitel.PauseMax = Convert.ToInt16(((TextBox)(sender)).Text);

        //            if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text) < grpobj._listZeile[zeile].aPlaylistTitel.PauseMin)
        //                grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text = Convert.ToString(grpobj._listZeile[zeile].aPlaylistTitel.PauseMin);

        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void _btnVolMinMinus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //            (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //        int sollWert = klZeile.volMin_wert - VolSprung;

        //        if (sollWert <= klZeile.audioZeile.sldKlangVol.Maximum)
        //            klZeile.volMin_wert = sollWert < 0 ? 0 : sollWert;
        //        else
        //            klZeile.volMin_wert = Convert.ToInt16(klZeile.audioZeile.sldKlangVol.Minimum);
        //        klZeile.audioZeile.tboxVolMin.Text = Convert.ToString(klZeile.volMin_wert);
        //        klZeile.aPlaylistTitel.VolumeMin = klZeile.volMin_wert;
        //        klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
        //            (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}


        //private void _btnVolMaxMinus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //            (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //        int sollWert = klZeile.volMax_wert - VolSprung;
        //        int max = Convert.ToInt16(klZeile.audioZeile.sldKlangVol.Maximum);

        //        if (sollWert <= max)
        //            klZeile.volMax_wert = sollWert < 0 ? 0 : sollWert;
        //        else
        //            klZeile.volMax_wert = max;
        //        klZeile.audioZeile.tboxVolMax.Text = Convert.ToString(klZeile.volMax_wert);
        //        if (klZeile.volMax_wert < Convert.ToInt16(klZeile.audioZeile.tboxVolMin.Text))
        //        {
        //            klZeile.audioZeile.tboxVolMin.Text = klZeile.audioZeile.tboxVolMax.Text;
        //            klZeile.volMin_wert = klZeile.volMax_wert;
        //        }
        //        klZeile.aPlaylistTitel.VolumeMax = klZeile.volMax_wert;
        //        klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
        //            (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}

        //private void _btnVolMinPlus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //           (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //        int sollWert = klZeile.volMin_wert + VolSprung;
        //        int max = Convert.ToInt16(klZeile.audioZeile.sldKlangVol.Maximum);

        //        if (sollWert >= klZeile.audioZeile.sldKlangVol.Minimum)
        //            klZeile.volMin_wert = sollWert > max ? max : sollWert;
        //        else
        //            klZeile.volMin_wert = max;
        //        klZeile.audioZeile.tboxVolMin.Text = Convert.ToString(klZeile.volMin_wert);
        //        if (klZeile.volMin_wert > Convert.ToInt16(klZeile.audioZeile.tboxVolMax.Text))
        //        {
        //            klZeile.audioZeile.tboxVolMax.Text = klZeile.audioZeile.tboxVolMin.Text;
        //            klZeile.volMax_wert = klZeile.volMin_wert;
        //        }
        //        klZeile.aPlaylistTitel.VolumeMin = klZeile.volMin_wert;
        //        klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
        //            (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}

        //private void _btnVolMaxPlus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //            (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //        int sollWert = klZeile.volMax_wert + VolSprung;
        //        int max = Convert.ToInt32(klZeile.audioZeile.sldKlangVol.Maximum);

        //        klZeile.volMax_wert = sollWert < max ? sollWert : max;

        //        klZeile.audioZeile.tboxVolMax.Text = Convert.ToString(klZeile.volMax_wert);
        //        klZeile.aPlaylistTitel.VolumeMax = klZeile.volMax_wert;
        //        klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
        //            (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}

        //private void _btnPauseMinMinus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //            (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //  /*      int sollWert = klZeile.pauseMin_wert - PauseSprung;

        //        if (sollWert <= klZeile.audioZeile.sldKlangPause.Maximum)
        //            klZeile.pauseMin_wert = sollWert <= 0 ? 0 : sollWert;
        //        else
        //            klZeile.pauseMin_wert = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Minimum);

        //        klZeile.audioZeile.tboxPauseMin.Text = Convert.ToString(klZeile.pauseMin_wert);*/
        //        klZeile.aPlaylistTitel.PauseMin = Convert.ToInt32(klZeile.audioZeile.tboxPauseMin.Text); //klZeile.pauseMin_wert;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}

        //private void _btnPauseMaxMinus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //            (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //        /*int sollWert = klZeile.pauseMax_wert - PauseSprung;
        //        int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

        //        if (sollWert <= max)
        //            klZeile.pauseMax_wert = sollWert < 0 ? 0 : sollWert;
        //        else
        //            klZeile.pauseMax_wert = max;

        //        klZeile.audioZeile.tboxPauseMax.Text = Convert.ToString(klZeile.pauseMax_wert);
        //        if (klZeile.pauseMax_wert < Convert.ToInt16(klZeile.audioZeile.tboxPauseMin.Text))
        //        {
        //            klZeile.audioZeile.tboxPauseMin.Text = klZeile.audioZeile.tboxPauseMax.Text;
        //            klZeile.pauseMin_wert = klZeile.pauseMax_wert;
        //        }*/
        //        klZeile.aPlaylistTitel.PauseMax = Convert.ToInt32(klZeile.audioZeile.tboxPauseMax.Text);// klZeile.pauseMax_wert;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}

        //private void _btnPauseMinPlus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //           (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //      /*  int sollWert = klZeile.pauseMin_wert + PauseSprung;
        //        int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

        //        if (sollWert >= Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Minimum))
        //            klZeile.pauseMin_wert = sollWert > max ? max : sollWert;
        //        else
        //            klZeile.pauseMin_wert = max;
        //        klZeile.audioZeile.tboxPauseMin.Text = Convert.ToString(klZeile.pauseMin_wert);
        //        if (klZeile.pauseMin_wert > Convert.ToInt16(klZeile.audioZeile.tboxPauseMax.Text))
        //        {
        //            klZeile.audioZeile.tboxPauseMax.Text = klZeile.audioZeile.tboxPauseMin.Text;
        //            klZeile.pauseMax_wert = klZeile.pauseMin_wert;
        //        }*/
        //        klZeile.aPlaylistTitel.PauseMin = Convert.ToInt32(klZeile.audioZeile.tboxPauseMin.Text); //klZeile.pauseMin_wert;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}

        //private void _btnPauseMaxPlus0_X_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
        //            (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

        //     /*   int sollWert = klZeile.pauseMax_wert + PauseSprung;
        //        int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

        //        klZeile.pauseMax_wert = sollWert < max ? sollWert : max;
        //        klZeile.audioZeile.tboxPauseMax.Text = Convert.ToString(klZeile.pauseMax_wert);*/
              
        //        klZeile.aPlaylistTitel.PauseMax = Convert.ToInt32(klZeile.audioZeile.tboxPauseMax.Text);// klZeile.pauseMax_wert;

        //        //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //        if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
        //    }
        //    catch (Exception) { }
        //}

        //private void tboxklangsongparallelX_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangPlaylist != null)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
        //            if (grpobj == null)
        //                return;
        //            try
        //            {
        //                if (Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) >= 0 &&
        //                    Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) != AktKlangPlaylist.MaxSongsParallel)
        //                {
        //                    if (Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) > AktKlangPlaylist.Audio_Playlist_Titel.Count)
        //                        grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.Audio_Playlist_Titel.Count.ToString();
        //                    AktKlangPlaylist.MaxSongsParallel = Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text);

        //                    if (grpobj.wirdAbgespielt)
        //                        CheckPlayStandbySongs(grpobj);

        //                    try { Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist); }
        //                    catch (Exception ex)
        //                    {
        //                        ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Die Datenbank konnte nicht aktualisiert werden", ex);
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ViewHelper.ShowError("Eingabefehler" + Environment.NewLine + "Ungültige Eingabe. Bitte geben Sie nur Ganzzahlwert ein.", ex);
        //                grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.MaxSongsParallel;
        //                grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception) { }
        //}
        
        //private void btnSongParPlusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int dif = Convert.ToInt32(((Button)sender).Tag);
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        int momentan = Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text);
        //        int max = Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Tag);

        //        if ((dif > 0 && dif + momentan <= max) ||
        //           ((dif < 0 && dif + momentan >= 0)))
        //        {
        //            grpobj.tbTopKlangSongsParallel.Text = (Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) + dif).ToString();
        //            grpobj.aPlaylist.MaxSongsParallel = Convert.ToUInt16(grpobj.tbTopKlangSongsParallel.Text);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void sldKlangVol0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (VM._GrpObjecte[0] != null)
        //        {
        //            UInt16 seite = 0;
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = null;
        //            while (klZeile == null)
        //            {
        //                klZeile = VM._GrpObjecte[seite]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(((Slider)e.Source).Tag));
        //                seite++;
        //            }
        //            klZeile.Aktuell_Volume = e.NewValue;
        //            klZeile.audioZeile.sldKlangVol.ToolTip = Convert.ToInt16(Math.Round(e.NewValue)) + " %";

        //            klZeile.aPlaylistTitel.Volume = Convert.ToInt16(klZeile.Aktuell_Volume);

        //            if (VM._GrpObjecte[seite - 1].visuell && klZeile._mplayer != null)
        //                klZeile._mplayer.Volume = e.NewValue / 100;

        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e.Source != null) SetzeChangeBit(VM._GrpObjecte[seite - 1].aPlaylist);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void sldPlaySpeed0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (VM._GrpObjecte.Count > 0 && VM._GrpObjecte[0] != null)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
        //            if (grpobj == null)
        //                return;

        //            int zeile = grpobj._listZeile.IndexOf(
        //                grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeile.sldPlaySpeed == (Slider)sender));

        //            if (zeile >= 0)
        //            {
        //                double speed = grpobj._listZeile[zeile].audioZeileVM.aPlayTitel.Speed;// .sldPlaySpeed.Value;
        //                if (grpobj._listZeile[zeile]._mplayer != null)
        //                    grpobj._listZeile[zeile]._mplayer.SpeedRatio = speed;

        //                grpobj._listZeile[zeile].aPlaylistTitel.Speed = speed;

        //                string geschw = "Abspielgeschwindigkeit: ";

        //                geschw += speed == .1 ? "sehr langsam" :
        //                          speed == .5 ? "langsam" :
        //                          speed == .75 ? "gedrosselt" :
        //                          speed == 1 ? "normal" :
        //                          speed == 2 ? "erhöht" :
        //                          speed == 3 ? "schnell" :
        //                          speed == 4 ? "sehr schnell" :
        //                          speed == 5 ? "ultra schnell" : "nicht definiert";
        //                //grpobj._listZeile[zeile].audioZeile.sldPlaySpeed.ToolTip = geschw;
        //                grpobj._listZeile[zeile].playspeed = speed;
        //            }
        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e.Source != null) VM.SetzeChangeBit(grpobj.aPlaylist);
        //        }
        //    }
        //    catch
        //    {
        //        ViewHelper.ShowError("Datenbankfehler" + Environment.NewLine + "Ändern der Geschwindigkeit des Titel konnte nicht durchgeführt werden.", new Exception());
        //    }
        //}


        //private void sldKlangPause0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (VM._GrpObjecte.Count > 0 && VM._GrpObjecte[0] != null)
        //        {
        //            UInt16 seite = 0;
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = null;
        //            while (klZeile == null)
        //            {
        //                klZeile = VM._GrpObjecte[seite]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(((Slider)e.Source).Tag));
        //                seite++;
        //            }
        //            klZeile.audioZeile.sldKlangPause.ToolTip = (e.NewValue < 1000) ? e.NewValue + " ms" : (e.NewValue < 60000) ? e.NewValue / 1000 + " sek." : e.NewValue / 60000 + " min.";
        //            klZeile.aPlaylistTitel.Pause = Convert.ToInt32(e.NewValue);

        //            //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
        //            if (e.Source != null) SetzeChangeBit(VM._GrpObjecte[seite].aPlaylist);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void btnAllVolUp_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        double d = Convert.ToDouble(((sender) as Button).Tag);

        //        grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.lbiEditorRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(t => t.audioZeile.sldKlangVol.Value += d);
        //    }
        //    catch (Exception) { }
        //}

        //private void btnAllPauseUp_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        double d = Convert.ToDouble(((sender) as Button).Tag);

        //        grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.lbiEditorRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile)
        //            {
        //                if (d == 1 && klZeile.audioZeile.sldKlangPause.Value != klZeile.audioZeile.sldKlangPause.Maximum)
        //                    klZeile.audioZeile.sldKlangPause.Value = klZeile.audioZeile.sldKlangPause.Ticks.Where(t => t > klZeile.audioZeile.sldKlangPause.Value).Min();
        //                else
        //                    if (d == -1 && klZeile.audioZeile.sldKlangPause.Value != 0)
        //                        klZeile.audioZeile.sldKlangPause.Value = klZeile.audioZeile.sldKlangPause.Ticks.Where(t => t < klZeile.audioZeile.sldKlangPause.Value).Max();
        //                    else
        //                        if (d == 2)
        //                            klZeile.audioZeile.sldKlangPause.Value = klZeile.audioZeile.sldKlangPause.Maximum;
        //                        else
        //                            if (d == -2)
        //                                klZeile.audioZeile.sldKlangPause.Value = 0;
        //            });
        //    }
        //    catch (Exception) { }
        //}

        //private void _imgOk_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (AktKlangPlaylist != null)
        //        {
        //            AktKlangPlaylist.Name = Convert.ToString(((Image)sender).Tag);
        //            for (int i = 0; i <= lbEditor.Items.Count - 1; i++)
        //                if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
        //                {
        //                    ((ListboxItemIcon)lbEditor.Items[i]).lbText.Content = AktKlangPlaylist.Name;
        //                    break;
        //                }
        //            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void tboxEditorName_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (rbEditorEditTheme.IsChecked.Value)
        //        {
        //            if (((TextBox)(sender)).Background != null && AktKlangTheme != null)
        //            {
        //                ((TextBox)(sender)).Text = AktKlangTheme.Name;
        //                ((TextBox)(sender)).Background = null;
        //            }
        //        }
        //        else
        //        {
        //            if (((TextBox)(sender)).Background != null && AktKlangPlaylist != null)
        //            {
        //                ((TextBox)(sender)).Text = AktKlangPlaylist.Name;
        //                ((TextBox)(sender)).Background = null;
        //            }
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void tiEditor_GotFocus(object sender, RoutedEventArgs e)
        //{
            //try
            //{
            //    if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)          //nur wenn TabItem gewechselt wurde
            //    {
            //        tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;  // = 0

            //        rbEditorEditTheme.IsEnabled = true;
            //        if (lbEditor.Items.Count == 0)                  //true
            //            AktualisiereEditorPlaylist();

            //        if (VM._GrpObjecte.Count > 0)
            //            foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile in VM._GrpObjecte.FirstOrDefault(t => t.visuell)._listZeile)
            //                UpdateKlangZeileRatingVisuell(kZeile);
            //        if (lbEditor.SelectedIndex != -1)
            //            lbEditor.ScrollIntoView(lbEditor.SelectedItem);
            //    }
            //}
            //catch (Exception) { }
        //}

        //private void tiMusik_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)          //nur wenn TabItem gewechselt wurde
        //    {
        //        tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;
        //        //AktualisiereMusikPlaylist();
        //    }
        //}

        //private void tiPList_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)      //nur wenn TabItem gewechselt wurde
        //        {
        //            tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;
        //            //AktualisierePListPlaylist();
        //            if (lbPListMusik.SelectedIndex == -1 && lbMusik.SelectedIndex != -1)
        //            {
        //                lbPListMusik.SelectionChanged -= lbPListMusik_SelectionChanged;
        //                lbPListMusik.SelectedIndex = lbMusik.SelectedIndex;
        //                lbPListMusik.SelectionChanged += lbPListMusik_SelectionChanged;
        //            }
        //           // AktualisierePlaylistThemes();
        //        }
        //    }
        //    catch (Exception ex) { }
        //}

        //private void btnClick(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        ((((Button)(sender)).Parent as Grid).Parent as Window).Close();
        //    }
        //    catch (Exception) { }
        //}

        //private void hyperlink_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        System.Diagnostics.Process.Start(((Hyperlink)sender).NavigateUri.AbsoluteUri);
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopVolMinMinusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnVolMinMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopVolMinPlusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnVolMinPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopVolMaxMinusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnVolMaxMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopVolMaxPlusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnVolMaxPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopPauseMinMinusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnPauseMinMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopPauseMinPlusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnPauseMinPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopPauseMaxMinusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnPauseMaxMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void btnTopPauseMaxPlusX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
        //            FindAll(t => t.audioZeile != null).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
        //            FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
        //            ForEach(t => _btnPauseMaxPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
        //    }
        //    catch (Exception) { }
        //}

        //private void chkbxTopAktivX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        bool soll = ((CheckBox)(e.Source)).IsChecked.Value;
        //        UInt16 objGruppe = Convert.ToUInt16(((CheckBox)sender).Tag);
        //        int posObjGruppe = Convert.ToInt16(VM._GrpObjecte.FindIndex(t => t.objGruppe == objGruppe));

        //        VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked.Value != soll).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile)
        //        {
        //            klZeile.audioZeile.chkTitel.IsChecked = soll;
        //            klZeile.aPlaylistTitel.Aktiv = soll;
        //            klZeile.audioZeile.chkTitel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        //        });

        //        if (VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).Count == VM._GrpObjecte[posObjGruppe]._listZeile.Count &&
        //            ((CheckBox)(e.Source)).IsChecked == true)
        //        {
        //            //Zufallsaktivierung der Zeilen
        //            List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> klZeileAktiv;
        //            klZeileAktiv = VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.playable == true).FindAll(t => t.aPlaylistTitel.Aktiv);

        //            while (klZeileAktiv.Count > 0)
        //            {
        //                int zeileIndex = VM._GrpObjecte[posObjGruppe]._listZeile.IndexOf(klZeileAktiv[(new Random()).Next(0, klZeileAktiv.Count)]);
        //                chkTitel0_0_Click(VM._GrpObjecte[posObjGruppe]._listZeile[zeileIndex].audioZeile.chkTitel, null);

        //                klZeileAktiv = klZeileAktiv.FindAll(t => t.aPlaylistTitel.Aktiv != true);
        //                klZeileAktiv = klZeileAktiv.FindAll(t => t.istLaufend != true);
        //                klZeileAktiv = klZeileAktiv.FindAll(t => t.istStandby != true);
        //            }
        //        }
        //        ((CheckBox)(e.Source)).IsChecked = soll;
        //    }
        //    catch (Exception) { }
        //}

        //private void chkbxTopVolChangeX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Int16 objGruppe = Convert.ToInt16(((CheckBox)sender).Tag);
        //        int posObjGruppe = Convert.ToInt16(VM._GrpObjecte.FindIndex(t => t.objGruppe == objGruppe));
        //        bool changeto = ((CheckBox)sender).IsChecked.Value;

        //        VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked == true).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile)
        //        {
        //            klZeile.audioZeile.chkVolMove.IsChecked = changeto;
        //            klZeile.aPlaylistTitel.VolumeChange = changeto;
        //        });

        //        VM._GrpObjecte[posObjGruppe].anzVolChange = Convert.ToUInt16(
        //            VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkVolMove.IsChecked == true).Count);
        //        VM.CheckAlleAngehakt(VM._GrpObjecte[posObjGruppe]);
        //    }
        //    catch (Exception) { }
        //}


        //private void chkbxTopPauseChangeX_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Int16 objGruppe = Convert.ToInt16(((CheckBox)sender).Tag);
        //        int posObjGruppe = Convert.ToInt16(VM._GrpObjecte.FindIndex(t => t.objGruppe == objGruppe));
        //        bool changeto = ((CheckBox)sender).IsChecked.Value;

        //        VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked == true).
        //            FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile)
        //        {
        //            klZeile.audioZeile.chkKlangPauseMove.IsChecked = changeto;
        //            klZeile.aPlaylistTitel.PauseChange = changeto;
        //        });

        //        VM._GrpObjecte[posObjGruppe].anzPauseChange = Convert.ToUInt16(
        //            VM._GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkKlangPauseMove.IsChecked == true).Count);
        //        VM.CheckAlleAngehakt(VM._GrpObjecte[posObjGruppe]);
        //    }
        //    catch (Exception) { }
        //}


        //private void chkbxPlayRange_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (((CheckBox)sender).IsChecked.Value)
        //            rsldTeilSong.Visibility = Visibility.Visible;
        //        else
        //            rsldTeilSong.Visibility = Visibility.Hidden;

        //        BGPlayer.AktPlaylistTitel.TeilAbspielen = chkbxPlayRange.IsChecked.Value;
        //        if (!BGPlayer.AktPlaylistTitel.TeilAbspielen)
        //        {
        //            BGPlayer.AktPlaylistTitel.TeilStart = null;
        //            BGPlayer.AktPlaylistTitel.TeilEnde = null;
        //        }
        //        else
        //        {
        //            BGPlayer.AktPlaylistTitel.TeilStart = rsldTeilSong.LowerValue;
        //            BGPlayer.AktPlaylistTitel.TeilEnde = rsldTeilSong.UpperValue;
        //        }
        //    }
        //    catch (Exception) { }
        //}
        
        


        //public void btnEditorGewichtung_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile = grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.btnGewichtung == (Button)sender);

        //        kZeile.aPlaylistTitel.Rating = (Convert.ToInt32(kZeile.audioZeile.btnGewichtung.Tag));
        //        Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.audiotitel);

        //        if (grpobj.NochZuSpielen.FindAll(t => t == kZeile.aPlaylistTitel.Audio_TitelGUID).Count <= kZeile.aPlaylistTitel.Rating)
        //        {
        //            //Von x Sterne auf x+1 - also eins hinzufügen
        //            grpobj.NochZuSpielen.Add(kZeile.aPlaylistTitel.Audio_TitelGUID);

        //            if (BGPlayer.AktPlaylistTitel != null &&
        //                BGPlayer.AktPlaylistTitel.Audio_PlaylistGUID == kZeile.aPlaylistTitel.Audio_TitelGUID)
        //            {
        //                BGPlayer.NochZuSpielen.Add(kZeile.aPlaylistTitel.Audio_TitelGUID);
        //                starsUpdate();
        //            }
        //        }
        //        else
        //        {
        //            //Von 5 Sterne auf 0 zurückgesetzt - also alle löschen und eins hinzufügen
        //            grpobj.NochZuSpielen.RemoveAll(t => t == kZeile.aPlaylistTitel.Audio_TitelGUID);
        //            if (grpobj.NochZuSpielen.FindAll(t => t == kZeile.aPlaylistTitel.Audio_TitelGUID).Count <= kZeile.aPlaylistTitel.Rating)
        //                grpobj.NochZuSpielen.Add(kZeile.aPlaylistTitel.Audio_TitelGUID);

        //            if (BGPlayer.AktPlaylistTitel != null &&
        //                BGPlayer.AktPlaylistTitel.Audio_PlaylistGUID == kZeile.aPlaylistTitel.Audio_TitelGUID)
        //            {
        //                BGPlayer.NochZuSpielen.RemoveAll(t => t == kZeile.aPlaylistTitel.Audio_TitelGUID);
        //                BGPlayer.NochZuSpielen.Add(kZeile.aPlaylistTitel.Audio_TitelGUID);
        //                starsUpdate();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Speicherfehler" + Environment.NewLine + "Die Gewichtung des Titels konnte nicht vorgenommen werden.", ex);
        //    }
        //}

        //public void UpdateKlangZeileRatingVisuell(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile)
        //{
        //    if (kZeile.aPlaylistTitel.Rating != Convert.ToInt32(kZeile.audioZeile.btnGewichtung.Tag))
        //    {
        //        kZeile.audioZeile.btnGewichtung.Click -= btnEditorGewichtung_Click;
        //        while (kZeile.aPlaylistTitel.Rating != Convert.ToInt32(kZeile.audioZeile.btnGewichtung.Tag))
        //            kZeile.audioZeile.btnGewichtung.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //        kZeile.audioZeile.btnGewichtung.Click += btnEditorGewichtung_Click;
        //    }
        //}

        //public void imgBGStern_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (Convert.ToInt16(((Image)sender).Tag) == 1 && BGPlayer.AktPlaylistTitel.Rating == 1)
        //            BGPlayer.AktPlaylistTitel.Rating = 0;
        //        else
        //            BGPlayer.AktPlaylistTitel.Rating = Convert.ToInt16(((Image)sender).Tag);

        //        starsUpdate();
        //    }
        //    catch (Exception) { }
        //}

        //public void starsUpdate()
        //{
        //    imgBGStern0.Source = (BGPlayer.AktPlaylistTitel.Rating >= 1) ?
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
        //    imgBGStern1.Source = (BGPlayer.AktPlaylistTitel.Rating >= 2) ?
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
        //    imgBGStern2.Source = (BGPlayer.AktPlaylistTitel.Rating >= 3) ?
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
        //    imgBGStern3.Source = (BGPlayer.AktPlaylistTitel.Rating >= 4) ?
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
        //    imgBGStern4.Source = (BGPlayer.AktPlaylistTitel.Rating == 5) ?
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
        //}
        
        //public void tiUebersicht_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        AktKlangPlaylist = null;
        //        AktKlangTheme = null;
        //    }
        //    catch (Exception) { }
        //}

        //public void tiUebersicht_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (rbEditorEditTheme.IsChecked.Value)
        //            rbEditorEditPlaylist_Checked(null, null);
        //    }
        //    catch (Exception) { }
        //}

        //public void checkPlayableFilesInGrpobj(Audio_Playlist aPlaylist)
        //{
        //    BackgroundWorker bkwChkPlayable = new BackgroundWorker();
        //    bkwChkPlayable.WorkerReportsProgress = true;
        //    bkwChkPlayable.WorkerSupportsCancellation = true;
        //    bkwChkPlayable.DoWork += new DoWorkEventHandler(bkwChkPlayable_DoWork);
        //    bkwChkPlayable.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwChkPlayable_RunWorkerCompleted);
        //    bkwChkPlayable.RunWorkerAsync(aPlaylist);
        //}

        //// ***Problem***  hier könnte das Problem liegen, dass die Titel Deaktiviert werden, obwohl sie da sind
        //private void bkwChkPlayable_DoWork(object sender, DoWorkEventArgs args)
        //{
        //    string old_Pfad;
        //    string old_Datei;
        //    MediaPlayer mp = new MediaPlayer();

        //    try
        //    {
        //        Audio_Playlist plylst = (Audio_Playlist)args.Argument;
        //        for (int i = 0; i < plylst.Audio_Playlist_Titel.Count; i++)
        //        {
        //            Audio_Titel aTitel = plylst.Audio_Playlist_Titel.ElementAt(i).Audio_Titel;

        //            VM._GrpObjecte.FindAll(t => t.aPlaylist == plylst).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj)
        //            {
        //                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpObj._listZeile.First(t => t.aPlaylistTitel.Audio_TitelGUID == plylst.Audio_Playlist_Titel.ElementAt(i).Audio_TitelGUID);


        //                //***Problem : Vielleicht verursacht das Speichern der Daten eine Execption ?!?
        //                old_Pfad = klZeile.aPlaylistTitel.Audio_Titel.Pfad;
        //                old_Datei = klZeile.aPlaylistTitel.Audio_Titel.Datei;
        //                Audio_Titel neuaTitel = VM.setTitelStdPfad(klZeile.aPlaylistTitel.Audio_Titel);
        //                if (neuaTitel.Pfad != old_Pfad ||
        //                    neuaTitel.Datei != old_Datei)
        //                {
        //                    klZeile.aPlaylistTitel.Audio_Titel = neuaTitel;
        //                    //Global.ContextAudio.Update<Audio_Titel>(klZeile.aPlaylistTitel.Audio_Titel);
        //                }

        //                //***Problem : IsPathRooted wurde vorerst für Testzwecke entfernt (Problem DropBox Ordner erzeugt x von y Dateien nicht abspielbar ***//
        //                klZeile.playable = (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
        //                                    File.Exists(aTitel.Pfad + "\\" + aTitel.Datei));
        //            });
        //        }
        //        args.Result = (Audio_Playlist)args.Argument;
        //    }
        //    catch (Exception)
        //    { }
        //}

        //private void bkwChkPlayable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        //{
        //    try
        //    {
        //        for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
        //        {
        //            if ((Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag == (Guid)(((Audio_Playlist)args.Result).Audio_PlaylistGUID))
        //            {
        //                VM._GrpObjecte.FindAll(t => t.aPlaylist == ((Audio_Playlist)args.Result)).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj)
        //                {
        //                    if (grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.aPlaylistTitel.Aktiv).Count != grpobj._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count)
        //                    {
        //                        ((MusikZeile)lbPListGeräusche.Items[i]).VM.TeilAbspielbar = true;
        //                        ((MusikZeile)lbPListGeräusche.Items[i]).VM.AnzahlTeilAbspielbar = grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.aPlaylistTitel.Aktiv).Count;
        //                        ((MusikZeile)lbPListGeräusche.Items[i]).VM.AnzahlGesamt = grpobj._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count;
        //                        //((MusikZeile)lbPListGeräusche.Items[i]).spnlMusikZeile.ToolTip =
        //                        //    grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.aPlaylistTitel.Aktiv).Count + " von " + grpobj._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count + " Titel abspielbar";
        //                    }
        //                    else
        //                    {
        //                        ((MusikZeile)lbPListGeräusche.Items[i]).VM.TeilAbspielbar = false;
        //                        //((MusikZeile)lbPListGeräusche.Items[i]).spnlMusikZeile.Background = null;
        //                        //((MusikZeile)lbPListGeräusche.Items[i]).spnlMusikZeile.ToolTip = null;
        //                    }
        //                });
        //            }
        //        }
        //        (sender as BackgroundWorker).Dispose();
        //    }
        //    catch (Exception)
        //    {
        //        (sender as BackgroundWorker).Dispose();
        //    }
        //}

        //private void tbEditorThemeFilter_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 0; i < lbEditorTheme.Items.Count; i++)
        //            ((ListboxItemIcon)lbEditorTheme.Items[i]).Visibility = ((ListboxItemIcon)lbEditorTheme.Items[i]).lbText.Content.ToString().ToLower().Contains(tbEditorThemeFilter.Text.ToLower()) ?
        //                Visibility.Visible : Visibility.Collapsed;
        //    }
        //    catch (Exception) { }
        //}

        //private void tbEditorTopFilterX_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
        //        if (grpobj == null)
        //            return;
        //        grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.lbiEditorRow != null).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile)
        //        {
        //            klZeile.audioZeile.Visibility = (klZeile.audioZeile.chkTitel.Content.ToString().ToLower().Contains(((TextBox)(e.Source)).Text.ToLower())) ? Visibility.Visible : Visibility.Collapsed;
        //        });
        //    }
        //    catch (Exception) { }

        //}

        //private void tbKBGFilter_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 0; i < lbMusik.Items.Count; i++)
        //        {
        //            ((MusikZeile)lbMusik.Items[i]).Visibility =
        //                ((MusikZeile)lbMusik.Items[i]).tblkTitel.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ||
        //                ((MusikZeile)lbMusik.Items[i]).tboxKategorie.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ?
        //                  Visibility.Visible : Visibility.Collapsed;
        //        }
        //    }
        //    catch (Exception) { }
        //}

        // TODO in MVVM
        //private void tbtnMusikZeileBtnCheck_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MusikZeile mZeile = null;
        //        foreach (MusikZeile zeile in lbPListGeräusche.Items)
        //        {
        //            if (zeile.tbtnCheck == (ToggleButton)sender)
        //            {
        //                mZeile = zeile;
        //                break;
        //            }
        //        }

        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = ((ToggleButton)sender).Tag == null ? null : VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.objGruppe == Convert.ToInt32(((ToggleButton)sender).Tag));
        //        if (grpobj == null)
        //            grpobj = VM._GrpObjecte.FindAll(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == ((Guid)(mZeile.Tag)));

        //        if (grpobj == null)
        //        {
        //            tiPlus_MouseUp(true, null);   // Neue Geräusch Playlist im Theme ausgewählt
        //            ((ToggleButton)sender).Tag = VM._GrpObjecte[VM._GrpObjecte.Count - 1].objGruppe;
        //            string sTitel = ((TextBlock)((StackPanel)((ToggleButton)e.Source).Parent).FindName("tblkTitel")).Text;

        //            int plylstItemPos = 0;
        //            while (((ListboxItemIcon)lbEditor.Items[plylstItemPos]).lbText.Content.ToString() != sTitel)
        //                plylstItemPos++;

        //            grpobj = VM._GrpObjecte.FirstOrDefault(t => t.objGruppe == VM.tiErstellt);

        //            //Get Playlist
        //            grpobj.aPlaylist = Global.ContextAudio.PlaylistListe.
        //                    FirstOrDefault(t => t.Audio_PlaylistGUID.Equals(((ListboxItemIcon)lbEditor.Items[plylstItemPos]).Tag));

        //            if (grpobj.aPlaylist != null)
        //            {
        //                //AktKlangPlaylist = grpobj.aPlaylist;
        //                grpobj.visuell = false;
        //                grpobj.istMusik = grpobj.aPlaylist.Hintergrundmusik;
        //                grpobj.playlistName = grpobj.aPlaylist.Name;

        //                foreach (Audio_Playlist_Titel aPlaylistTitel in grpobj.aPlaylist.Audio_Playlist_Titel)
        //                {
        //                    /*if (!File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei == null ? "" : aPlaylistTitel.Audio_Titel.Datei))
        //                    {
        //                        aPlaylistTitel.Audio_Titel = setTitelStdPfad(aPlaylistTitel.Audio_Titel);
        //                        if (File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei))
        //                            Global.ContextAudio.Update<Audio_Titel>(aPlaylistTitel.Audio_Titel);
        //                    }*/

        //                    VM.KlangNewRow(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei, grpobj, 0, aPlaylistTitel);

        //                    if (aPlaylistTitel.Aktiv &&
        //                        !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
        //                    {
        //                        for (int i = 0; i <= aPlaylistTitel.Rating; i++)
        //                            grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
        //                    }
        //                }
        //            }
        //        }

        //        if (mZeile.chkbxForceVol.IsChecked.Value)
        //            grpobj.force_Volume = mZeile.sldForceVolume.Value / 100;
        //        else
        //            grpobj.force_Volume = null;

        //        //grpobj.Vol_PlaylistMod = Convert.ToUInt16(slPlaylistVolume.Value);

        //        if ((!btnPListPListAbspielen.IsEnabled) ||
        //            (Convert.ToBoolean(btnPListPListAbspielen.Tag) &&
        //             !grpobj.wirdAbgespielt &&
        //             VM._GrpObjecte.FindAll(t => t.wirdAbgespielt).FindAll(t => !t.visuell).FindAll(t => t.tiEditor == null).Count != 0))    //Abspielen
        //        {
        //            grpobj.wirdAbgespielt = true;
        //            if (grpobj.visuell)
        //            {
        //                ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/stop.png"));
        //                ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(3, 4, 2, 2);
        //            }
        //            btnPListPListAbspielen.Tag = true;
        //            if (!grpobj.aPlaylist.Fading)
        //                mZeile.recProzent.Width = 65;

        //            //WARTEZEIT DER PLAYLISTE EINBAUEN
        //            grpobj.wartezeitTimer.Tag = grpobj;
        //            if (!grpobj.aPlaylist.WarteZeitAktiv)
        //                wartezeitTimer_Tick(grpobj.wartezeitTimer, new EventArgs());
        //            else
        //            {
        //                if (grpobj.wartezeitTimer.IsEnabled)
        //                    grpobj.wartezeitTimer.Stop();

        //                grpobj.wartezeitTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)grpobj.aPlaylist.WarteZeit);
        //                grpobj.wartezeitTimer.Start();
        //            }                                                          
                    
        //            foreach (MusikZeile _mZeile in lbPListGeräusche.Items)
        //            {
        //                if (_mZeile.tbtnCheck.IsChecked.Value && _mZeile.tbtnCheck != ((ToggleButton)sender))
        //                {
        //                    MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObjAlleAnderen = VM._GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(_mZeile.tbtnCheck.Tag));
        //                    if (grpObjAlleAnderen != null && grpObjAlleAnderen.wirdAbgespielt != grpobj.wirdAbgespielt)
        //                    {
        //                        grpObjAlleAnderen.wirdAbgespielt = !grpobj.wirdAbgespielt;
        //                        grpObjAlleAnderen.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
        //                        if (grpObjAlleAnderen.tbtnKlangPause.Content != null)
        //                        {
        //                            ((Image)grpObjAlleAnderen.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
        //                            ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(3, 4, 2, 2);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        checkPListPlaybtnGeräusche();
        //        checkPlayableFilesInGrpobj(grpobj.aPlaylist);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anwählen der Geräusche-Playlist ist ein Fehler aufgetreten", ex);
        //    }
        //}

        //***
        //private void tbtnMusikZeileBtnCheck_UnChecked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MusikZeile mZeile = null;
        //        foreach (MusikZeile zeile in lbPListGeräusche.Items)
        //        {
        //            if (zeile.tbtnCheck == (ToggleButton)sender)
        //            {
        //                mZeile = zeile;
        //                break;
        //            }
        //        }
        //        btnPListPListAbspielen.IsEnabled = false;
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = ((ToggleButton)sender).Tag == null ? null : VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.objGruppe == Convert.ToInt32(((ToggleButton)sender).Tag));
        //        if (grpobj == null)
        //            grpobj = VM._GrpObjecte.FindAll(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == ((Guid)(mZeile.Tag)));

        //        ((StackPanel)((Grid)((StackPanel)((ToggleButton)e.Source).Parent).Parent).Parent).Background = null;
        //        ((StackPanel)((Grid)((StackPanel)((ToggleButton)e.Source).Parent).Parent).Parent).ToolTip = null;


        //        if (grpobj.wartezeitTimer.IsEnabled)
        //                grpobj.wartezeitTimer.Stop();
        //        grpobj.wartezeitTimer.Tag = null;

        //        if (grpobj != null && grpobj.wirdAbgespielt)
        //        {
        //            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
        //            if (!grpobj.aPlaylist.Fading)
        //                mZeile.recProzent.Width = 0;

        //            if (VM._GrpObjecte.FindAll(t => t.wirdAbgespielt).Count == 0)
        //                btnPListPListAbspielen.Tag = false;
        //        }
        //        btnPListPListAbspielen.IsEnabled = true;
        //        checkPListPlaybtnGeräusche();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Abwählen der Geräusche-Playlist ist ein Fehler aufgetreten", ex);
        //    }
        //}

        //private void checkPListPlaybtnGeräusche()
        //{
        //    bool found = false;
        //    foreach (MusikZeile mZeile in lbPListGeräusche.Items)
        //    {
        //        if (mZeile.tbtnCheck.IsChecked.Value)
        //        {
        //            found = true;
        //            break;
        //        }
        //    }
        //    btnPListPListAbspielen.IsEnabled = found;

        //    btnimgPListPListAbspielen.Source = !Convert.ToBoolean(btnPListPListAbspielen.Tag) ?
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")) :
        //        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
        //}

        //private void AktualisierePListPlaylist()
        //{
        //    UInt16 posMusik = 0;
        //    UInt16 posGeräusche = 0;
        //    bool neu = true;
        //    List<Audio_Playlist> aPlyList = Global.ContextAudio.PlaylistListe;

        //    Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik).OrderBy(t => t.Name).ToList().ForEach(delegate(Audio_Playlist playlistliste)
        //    {
        //        neu = (posMusik + 1 > lbPListMusik.Items.Count) ? true : false;

        //        MusikZeile mZeile = neu ? new MusikZeile() : ((MusikZeile)(lbPListMusik.Items[posMusik]));
        //        if (neu)
        //        {
        //            mZeile.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
        //            mZeile.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
        //            mZeile.Cursor = Cursors.Hand;
        //            mZeile.tboxKategorie.LostFocus += tboxTopKategorieX_LostFocus;
        //            mZeile.pbarSong.MouseLeftButtonDown += pbarBGSong_MouseLeftButtonDown;
        //            mZeile.tbtnCheck.Visibility = Visibility.Collapsed;
        //            lbPListMusik.Items.Add(mZeile);
        //        }
   //*     //        ((Grid)mZeile.grdForceVol.Parent).RowDefinitions[1].Height = tbThemePListNormSize.IsChecked.Value ? new GridLength(0) : GridLength.Auto;
        //        mZeile.ToolTip = (tbThemePListNormSize.IsChecked.Value && mZeile.tboxKategorie.Text != "") ? mZeile.tboxKategorie.Text : null;
        //        mZeile.Tag = playlistliste.Audio_PlaylistGUID;
        //        mZeile.tblkTitel.Text = playlistliste.Name;
        //        mZeile.tblkLänge.Text = (playlistliste.Länge != 0) ? TimeSpan.FromMilliseconds(playlistliste.Länge).ToString(@"hh\:mm\:ss") : "";
        //        mZeile.tboxKategorie.Tag = mZeile.Tag;
        //        mZeile.tboxKategorie.Text = playlistliste.Kategorie;
        //        posMusik++;
        //    });

        //    Global.ContextAudio.PlaylistListe.FindAll(t => !t.Hintergrundmusik).OrderBy(t => t.Name).ToList().ForEach(delegate(Audio_Playlist playlistliste)
        //    {
        //        neu = (posGeräusche + 1 > lbPListGeräusche.Items.Count) ? true : false;

        //        MusikZeile mZeile = neu ? new MusikZeile() : ((MusikZeile)(lbPListGeräusche.Items[posGeräusche]));

        //        if (neu)
        //        {
        //            mZeile.grdForceVol.ColumnDefinitions[0].Width = new GridLength(90);
        //            mZeile.grdForceVol.ColumnDefinitions[1].Width = new GridLength(70);
        //            mZeile.tbtnCheck.Visibility = Visibility.Visible;
        //            mZeile.tbtnCheck.Tag = null;
        //            mZeile.tbtnCheck.Checked += tbtnMusikZeileBtnCheck_Checked;
        //            mZeile.tbtnCheck.Unchecked += tbtnMusikZeileBtnCheck_UnChecked;
        //            mZeile.tboxKategorie.LostFocus += tboxTopKategorieX_LostFocus;
        //            mZeile.chkbxForceVol.Click += chkbxForceVol_Click;
        //            mZeile.sldForceVolume.ValueChanged += sldForceVolume_ValueChanged;
        //            lbPListGeräusche.Items.Add(mZeile);
        //        }
                
        //        Grid.SetRow(mZeile.grdForceVol, tbThemePListNormSize.IsChecked.Value ? 0 : 1);
        //        Grid.SetColumn(mZeile.grdForceVol, tbThemePListNormSize.IsChecked.Value ? 2 : 0);
        //        mZeile.tblkLänge.Visibility = tbThemePListNormSize.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
        //        mZeile.grdForceVol.ColumnDefinitions[0].Width = tbThemePListNormSize.IsChecked.Value ? new GridLength(20) : new GridLength(90);
        //        mZeile.chkbxForceVol.Content = tbThemePListNormSize.IsChecked.Value ? null : "Lautstärke";
        //        mZeile.chkbxForceVol.Margin = tbThemePListNormSize.IsChecked.Value ? new Thickness(0, 1, 0, 0) : new Thickness(15, 1, 0, 0);
        //        mZeile.brdKategorie.Visibility = tbThemePListNormSize.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
        //        ((Grid)mZeile.grdForceVol.Parent).ColumnDefinitions[1].Width = tbThemePListNormSize.IsChecked.Value ? new GridLength(100) : new GridLength(70);
        //        mZeile.ToolTip = (tbThemePListNormSize.IsChecked.Value && mZeile.tboxKategorie.Text != "") ? mZeile.tboxKategorie.Text : null;

        //        mZeile.Tag = playlistliste.Audio_PlaylistGUID;
        //        mZeile.chkbxForceVol.Tag = playlistliste;
        //        mZeile.sldForceVolume.Tag = playlistliste;
        //        mZeile.tblkLänge.Text = (playlistliste.Länge != 0) ? TimeSpan.FromMilliseconds(playlistliste.Länge).ToString(@"hh\:mm\:ss") : "";
        //        mZeile.tblkTitel.Text = playlistliste.Name;
        //        mZeile.tboxKategorie.Tag = mZeile.Tag;
        //        mZeile.tboxKategorie.Text = playlistliste.Kategorie;
        //        mZeile.chkbxForceVol.IsChecked = (playlistliste.DoForce) ? true : false;
        //        mZeile.sldForceVolume.Value = (!playlistliste.DoForce && playlistliste.ForceVolume == 0) ? 50 : playlistliste.ForceVolume;
        //        posGeräusche++;
        //    });

        //    if (lbPListMusik.Items.Count > posMusik && lbPListMusik.Items.Count != 0)
        //    {
        //        for (int i = posMusik; i < lbPListMusik.Items.Count; i++)
        //            lbPListMusik.Items.RemoveAt(i);
        //    }
        //    if (lbPListGeräusche.Items.Count > posGeräusche && lbPListGeräusche.Items.Count != 0)
        //    {
        //        for (int i = posGeräusche; i < lbPListGeräusche.Items.Count; i++)
        //            lbPListGeräusche.Items.RemoveAt(i);
        //    }
        //}

        //private void chkbxForceVol_Click(object sender, RoutedEventArgs e)
        //{
        //    MusikZeile mZeile = (MusikZeile)((StackPanel)((Grid)((Grid)((CheckBox)sender).Parent).Parent).Parent).Parent;

        //    Audio_Playlist aPlaylist = (Audio_Playlist)((CheckBox)sender).Tag;

        //    aPlaylist.DoForce = (mZeile.chkbxForceVol.IsChecked == true) ? true : false;
        //    Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);

        //    MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj = mZeile.tbtnCheck.Tag == null ? null : VM._GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));

        //    if (grpObj != null)
        //    {
        //        if (mZeile.chkbxForceVol.IsChecked.Value)
        //            grpObj.force_Volume = mZeile.sldForceVolume.Value / 100;
        //        else
        //            grpObj.force_Volume = null;
        //    }
        //}

        //private void sldForceVolume_ValueChanged(object sender, RoutedEventArgs e)
        //{
        //    MusikZeile mZeile = (MusikZeile)((StackPanel)((Grid)((Grid)((Slider)sender).Parent).Parent).Parent).Parent;
        //    Audio_Playlist aPlaylist = (Audio_Playlist)((Slider)sender).Tag;


        //    aPlaylist.ForceVolume = Convert.ToInt32(mZeile.sldForceVolume.Value);
        //    Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);

        //    MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj = mZeile.tbtnCheck.Tag == null ? null : VM._GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));

        //    if (grpObj != null)
        //    {
        //        if (mZeile.chkbxForceVol.IsChecked.Value)
        //            grpObj.force_Volume = mZeile.sldForceVolume.Value / 100;
        //        else
        //            grpObj.force_Volume = null;
        //    }
        //    ((Slider)sender).ToolTip = Math.Round(((Slider)sender).Value) + " %";
        //}


        //private void tbPListGeräusche_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        string[] split = tbPListGeräuscheName.Text.ToLower().Split(new Char[] { ' ', ',' });

        //        for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
        //        {
        //            ((MusikZeile)lbPListGeräusche.Items[i]).Visibility = Visibility.Visible;

        //            foreach (string s in split)
        //            {
        //                if (s != "")
        //                    if (!((MusikZeile)lbPListGeräusche.Items[i]).tblkTitel.Text.ToLower().Contains(s) ||
        //                        ((MusikZeile)lbPListMusik.Items[i]).tboxKategorie.Text == "")
        //                        ((MusikZeile)lbPListGeräusche.Items[i]).Visibility = Visibility.Collapsed;
        //            }
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void tbThemesFilterAll_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    AktualisierePlaylistThemes();
        //}

        //private void tbPListFilterAll_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        //btnPListAktFilter.Visibility = Visibility.Hidden;
        //        //for (int i = 0; i < lbPListMusik.Items.Count; i++)
        //        //{
        //        //    if ((chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListMusik.Items[i])).tboxKategorie.Text) ||
        //        //        chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListMusik.Items[i])).tblkTitel.Text)) &&
        //        //        (chkPListFilter(tbPListMusikName.Text, ((MusikZeile)(lbPListMusik.Items[i])).tboxKategorie.Text) ||
        //        //        chkPListFilter(tbPListMusikName.Text, ((MusikZeile)(lbPListMusik.Items[i])).tblkTitel.Text)))
        //        //        ((MusikZeile)(lbPListMusik.Items[i])).Visibility = Visibility.Visible;
        //        //    else
        //        //        ((MusikZeile)(lbPListMusik.Items[i])).Visibility = Visibility.Collapsed;
        //        //}

        //        //for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
        //        //{
        //        //    if ((chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tboxKategorie.Text) ||
        //        //        chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tblkTitel.Text)) &&
        //        //        (chkPListFilter(tbPListGeräuscheName.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tboxKategorie.Text) ||
        //        //        chkPListFilter(tbPListGeräuscheName.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tblkTitel.Text)))
        //        //        ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Visible;
        //        //    else
        //        //        ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Collapsed;
        //        //}
        //    }
        //    catch (Exception) { }
        //}

        //private void btnThemeGeräuscheFilterAktiv_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        tbPListGeräuscheName.Text = "";
        //        for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
        //        {
        //            if (!((MusikZeile)lbPListGeräusche.Items[i]).tbtnCheck.IsChecked.Value)
        //                ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Collapsed;
        //        }
        //        lbPListGeräusche.ScrollIntoView(lbPListGeräusche.Items[0]);
        //        btnPListAktFilter.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception) { }
        //}

        //private void btnPListAktFilter_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //lbTest.Visibility = lbTest.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        //        //lbTest.Items.Clear();

        //        for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
        //            ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Visible;

        //        tbPListFilterAll_TextChanged(tbPListGeräuscheName, null);
        //        lbPListGeräusche.ScrollIntoView(lbPListGeräusche.Items[0]);
        //        btnPListAktFilter.Visibility = Visibility.Hidden;
        //    }
        //    catch (Exception) { }
        //}

        //private void lbPListMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RemovedItems.Count == 1)
        //            (e.RemovedItems[0] as MusikZeile).pbarSong.Visibility = Visibility.Collapsed;
        //        lbMusik.SelectedIndex = lbPListMusik.SelectedIndex;
        //    }
        //    catch (Exception) { }
        //}

        
        //private bool chkPListFilter(string filter, string text)
        //{
        //    bool result = true;

        //    foreach (string s in filter.ToLower().Split(new Char[] { ' ', ',' }))
        //        if (s != "" && (!text.ToLower().Contains(s) || text == ""))
        //            result = false;
        //    return result;
        //}

        //private void slPlaylistVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (IsInitialized)
        //        {
        //            //slPlaylistVolume.ToolTip = Math.Round(slPlaylistVolume.Value) + "% der Playlisten-Lautstärke";
        //            //if (VM.GeräuscheIsMuted)// Convert.ToDouble(btnPListPListSpeaker.Tag) != -1)
        //            //    btnPListPListSpeaker.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //            foreach (MusikZeile mZeile in lbPListGeräusche.Items)
        //            {
        //                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj = (mZeile.tbtnCheck.Tag == null) ? null : VM._GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
        //                if (grpObj != null)
        //                    grpObj.Vol_PlaylistMod = Convert.ToInt32(slPlaylistVolume.Value);
        //            }

        //            if (Einstellungen.GeneralGeräuscheVolume != (int)Math.Round(((Slider)sender).Value))
        //                Einstellungen.SetEinstellung<int>("GeneralGeräuscheVolume", (int)Math.Round(((Slider)sender).Value));
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void slBGVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (IsInitialized && BGPlayer != null && BGPlayer.BG[BGPlayeraktiv].mPlayer != null &&
        //            (VM.FadingIn_Started == null || VM.FadingIn_Started.Source == null) &&
        //            !BGPlayer.BG[BGPlayeraktiv].FadingOutStarted &&
        //            BGPlayer.BG[BGPlayeraktiv].mPlayer.Volume != e.NewValue / 100)
        //            BGPlayer.BG[BGPlayeraktiv].mPlayer.Volume = e.NewValue / 100;
        //        if (Convert.ToDouble(btnBGSpeaker.Tag) != -1)
        //            btnBGSpeaker.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //        //slBGVolume.ToolTip = Math.Round(slBGVolume.Value) + " %";

        //        //if (IsInitialized && Einstellungen.GeneralMusikVolume != (int)Math.Round(((Slider)sender).Value))
        //        //    Einstellungen.SetEinstellung<int>("GeneralMusikVolume", (int)Math.Round(((Slider)sender).Value));
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ändern der Hintergrund Lautstärke ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnKlangUpdateFiles_Click(object sender, RoutedEventArgs e)
        //{
        //    string titelRef = "";
        //    try
        //    {
        //        Global.SetIsBusy(true, string.Format("Neue Dateien werden integriert..."));
        //        List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);

        //        titelRef = ((Button)sender).Tag.ToString();
        //        ((Button)sender).Visibility = Visibility.Visible;

        //        List<string> allFiles = new List<string>();
        //        foreach (string datei in Directory.GetFiles(titelRef, "*.mp3", SearchOption.AllDirectories))
        //            if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
        //                allFiles.Add(datei);

        //        foreach (string datei in Directory.GetFiles(titelRef, "*.wav", SearchOption.AllDirectories))
        //            if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
        //                allFiles.Add(datei);

        //        foreach (string datei in Directory.GetFiles(titelRef, "*.ogg", SearchOption.AllDirectories))
        //            if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
        //                allFiles.Add(datei);

        //        foreach (string datei in Directory.GetFiles(titelRef, "*.wma", SearchOption.AllDirectories))
        //            if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
        //                allFiles.Add(datei);

        //        if (allFiles.Count > 0)
        //        {
        //            if (ViewHelper.ConfirmYesNoCancel("Hinzufügen von Musiktitel aus dem Verzeichnis", "Es wurden insgesamt " + allFiles.Count +
        //                " Dateien gefunden, die noch nicht in der Playliste eingetragen sind." + Environment.NewLine +
        //                "Sollen diese integriert werden?") == 2)
        //            {
        //                Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine + titelRef + "werden eingebunden"));
        //                grdEditorListe.Visibility = Visibility.Hidden;
        //                foreach (string newFile in allFiles)
        //                {
        //                    Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine +
        //                        titelRef + "werden eingebunden" +
        //                        Environment.NewLine + System.IO.Path.GetFileName(newFile)));
        //                    VM.KlangDateiHinzu(newFile, null, AktKlangPlaylist, 0);
        //                }
        //                grdEditorListe.Visibility = Visibility.Visible;
        //            }
        //        }
        //        ((Button)sender).Visibility = Visibility.Hidden;
        //        VM._chkAnzDateienInDir(AktKlangPlaylist);  // VM._GrpObjecte.FirstOrDefault(t => t.aPlaylist == AktKlangPlaylist));
        //        Global.SetIsBusy(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        grdEditorListe.Visibility = Visibility.Visible;
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Ungültiger Pfad" + Environment.NewLine + "Bitte überprüfen Sie das Verzeichnis:" + Environment.NewLine + titelRef, ex);
        //    }
        //}

        //private void btnPListPListSpeaker_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (Convert.ToInt32((sender as Button).Tag) != -1)
        //        {
        //            (sender as Button).Tag = -1;
        //            btnimgPListPListSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
        //        }
        //        else
        //        {
        //            (sender as Button).Tag = slPlaylistVolume.Value;
        //            btnimgPListPListSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker-mute.png"));
        //        }

        //        foreach (MusikZeile mZeile in lbPListGeräusche.Items)
        //        {
        //            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj = (mZeile.tbtnCheck.Tag == null) ? null : VM._GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
        //            if (grpObj != null && mZeile.tbtnCheck.IsChecked.Value)
        //                grpObj._listZeile.ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile)
        //                {
        //                    if (kZeile._mplayer != null)
        //                        kZeile._mplayer.IsMuted = Convert.ToInt32(btnPListPListSpeaker.Tag) != -1 ? true : false;
        //                });
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void btnAudioDatenImport_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int mrRes;
        //        if (Global.ContextAudio.PlaylistListe.Count == 0 && Global.ContextAudio.ThemeListe.Count == 0)
        //            mrRes = 2;
        //        else
        //            mrRes = ViewHelper.ConfirmYesNoCancel("Löschen bestehender Daten", "Soll die aktuelle Datenbank erweitert werden?" + Environment.NewLine + Environment.NewLine + "Wählen sie 'Ja' damit die Datenbank erweitert wird." +
        //                Environment.NewLine + "Wählen Sie 'Nein' um die bestehende Datenbank zu ersetzten. Achtung! Alle Daten gehen verloren.");
        //        if (mrRes == 2 || mrRes == 1)
        //        {
        //            Global.SetIsBusy(true);


        //            int mrImpVar = (ViewHelper.ConfirmYesNoCancel("Komplette Sicherung", "Aus der Historie heraus, gibt es zwei Varianten einer Komplettsicherung." + Environment.NewLine +
        //                    Environment.NewLine + "Die Sicherung der Musikdaten werden in neuerer Version auf verschiedene Dateien aufgeteilt." + Environment.NewLine +
        //                    "Wenn Sie eine einzige XML-Datei als Komplettsicherung gespeichert haben, deutet das auf die vorherige Methode hin." + Environment.NewLine +
        //                    Environment.NewLine + "Wollen Sie das Importieren des neuen Prozesses durchführen?"));
        //            if (mrImpVar == 0)
        //            {
        //                Global.SetIsBusy(false);
        //                return;
        //            }

        //            //Importieren aller Playlisten und danach aller Themelisten
        //            if (mrImpVar == 2)
        //            {
        //                System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
        //                folderDlg.SelectedPath = Environment.CurrentDirectory;
        //                folderDlg.Description = "Wählen Sie ein Verzeichnis das alle Dateien der Sicherung enthält";
        //                List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();

        //                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                {
        //                    if (mrRes == 1)
        //                    {
        //                        btnAudioDatenDelete.Tag = true;
        //                        btnAudioDatenDelete.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //                        btnAudioDatenDelete.Tag = null;
        //                    }
        //                    Global.SetIsBusy(true, string.Format("Alle Playlisten werden importiert ..."));
        //                    string pfad = folderDlg.SelectedPath;

        //                    DirectoryInfo d = new DirectoryInfo(pfad);
        //                    List<string> listXML = new List<string>();
        //                    foreach (FileInfo f in d.GetFiles("*.xml"))
        //                        listXML.Add(f.DirectoryName + "\\" + f.Name);

        //                    btnAudioDatenImport.Tag = listXML;
        //                    btnPlaylistImport.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //                    btnKlangThemeImport.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //                    btnAudioDatenImport.Tag = null;
        //                }
        //            }
        //            else
        //            {
        //                if (ViewHelper.ConfirmYesNoCancel("Unsicherer Verlauf", "ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
        //                   "Leider konnte dieser Prozess noch NICHT ZUVERLÄSSIG programmiert werden." + Environment.NewLine +
        //                   Environment.NewLine + "Es muss damit gerechtnet werden, das die exportierte Datei" + Environment.NewLine + "NICHT MEHR IMPORTIERT werden kann!" +
        //                   Environment.NewLine + Environment.NewLine + "Soll der Vorgang trotzdem fortgesetzt werden?") != 2)
        //                    return;

        //                string pfad = ViewHelper.ChooseFile("Audio-Daten importieren", "", false, "xml");
        //                if (pfad != null)
        //                {
        //                    try
        //                    {
        //                        string tmpFile = Directory.GetCurrentDirectory() + @"\AudioDB_temp.xml";
        //                        VM._datenloeschen(mrRes, false, tmpFile);

        //                        Global.SetIsBusy(true, string.Format("Neue Daten werden importiert ..."));

        //                        if (Audio_Playlist.Import(pfad, "") != null)
        //                        {
        //                            Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
        //                            Global.ContextAudio.Save();
        //                        }

        //                        Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
        //                        VM.AktualisiereHotKeys();
        //                        Global.SetIsBusy(true, string.Format("Temporäre Daten werden gelöscht ..."));
        //                        if (mrRes == 1)
        //                            File.Delete(tmpFile);
        //                        Global.SetIsBusy(false);
        //                        VM.rbEditorEditPlaylist = false;
        //                        //tcAudioPlayer.Tag = -1;

        //                        //tiEditor_GotFocus(sender, null);
        //                        VM.rbEditorEditPlaylist = true;
        //                        //(wpnlPListThemes.Tag as List<Guid>).Clear();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Global.SetIsBusy(false);
        //                        ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
        //                    }
        //                }
        //            }
        //            Global.SetIsBusy(false);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //private void btnAudioDatenDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int mrRes = btnAudioDatenDelete.Tag != null ? 2 :
        //            ViewHelper.ConfirmYesNoCancel("Löschen ALLER bestehender Audio-Daten", "Soll die komplette Audio-Datenbank gelöscht werden?" +
        //                Environment.NewLine + Environment.NewLine + "Achtung! Alle Daten gehen unwiderruflich verloren.");
        //        if (mrRes == 2)
        //        {
        //            VM._datenloeschen(mrRes, true, "");

        //            Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
        //            Global.ContextAudio.UpdateList<Audio_Titel>();
        //            Global.ContextAudio.UpdateList<Audio_Playlist>();
        //            Global.ContextAudio.UpdateList<Audio_Playlist_Titel>();
        //            Global.ContextAudio.UpdateList<Audio_Theme>();
        //            Global.ContextAudio.UpdateList<Audio_Theme_Playlist>();

        //            Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
        //            VM.AktualisiereHotKeys();
        //            Global.SetIsBusy(false);

        //            VM.rbEditorEditPlaylist = false;
        //            //tcAudioPlayer.Tag = -1;

        //            //tiEditor_GotFocus(sender, null);
        //            VM.rbEditorEditPlaylist = true;
        //            //(wpnlPListThemes.Tag as List<Guid>).Clear();
        //            lbEditorListe.Items.Clear();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
        //    }
        //}


        //private void btnPlaylistImport_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        List<string> dateien;
        //        if (btnAudioDatenImport.Tag != null)
        //            dateien = btnAudioDatenImport.Tag as List<string>;
        //        else
        //            dateien = ViewHelper.ChooseFiles("Playlist(en) importieren", "", true, new string[3]{"xml","wpl","m3u8"});
        //        if (dateien != null)
        //        {
        //            try
        //            {
        //                bool _nicht_first = false;
        //                int was = lbEditor.SelectedIndex;
                        
        //                foreach (string pfad in dateien)
        //                {
        //                    Global.SetIsBusy(true, string.Format("Neue Playlist  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + "'  wird importiert ..."));
        //                    (App.Current.MainWindow as View.MainView).UpdateLayout();

        //                    if (pfad.EndsWith(".xml"))
        //                    {
        //                        if (AktKlangPlaylist == null) AktKlangPlaylist = new Audio_Playlist();
        //                        if ( Audio_Playlist.Import(pfad, "Audio_Playlist", _nicht_first) != null)
        //                            AktKlangPlaylist = Global.ContextAudio.Liste<Audio_Playlist>()[0];
        //                    }
        //                    else
        //                    {
        //                        FileInfo fi = new FileInfo(pfad);
        //                        AktKlangPlaylist = NeueKlangPlaylistInDB(fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length));

        //                        _DateienAufnehmen(new List<string>() { pfad }, null, AktKlangPlaylist, 0, false);
        //                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        //                    }
        //                    Global.ContextAudio.Save();
        //                    _nicht_first = true;
        //                }
        //                Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
        //                tbEditorPlaylistFilter.Text = "";
        //                VM.AktualisiereHotKeys();
                        
        //                if (AktKlangPlaylist != null)
        //                {
        //                    for (int i = 0; i < lbEditor.Items.Count; i++)
        //                        if (AktKlangPlaylist != null && (Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
        //                            lbEditor.SelectedIndex = i;
        //                }
        //                lbEditor.ScrollIntoView(lbEditor.SelectedItem);
        //                Global.SetIsBusy(false);
        //            }
        //            catch (Exception ex)
        //            {
        //                Global.SetIsBusy(false);
        //                ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
        //                VM.AktualisiereHotKeys();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
        //    }
        //}

        //private void lbItembtnLöschenPlaylist_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
        //        Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == g);
        //        if (aPlaylist != null)
        //        {
        //            if (ViewHelper.ConfirmYesNoCancel("Löschen der Playlist", "Wollen Sie wirklich die ausgewählte Playlist  '" + aPlaylist.Name + "'  löschen.") == 2)
        //            {
        //                Global.SetIsBusy(true, string.Format("Playlist '" + aPlaylist.Name + "' wird gelöscht..."));
        //                if (AktKlangPlaylist != null && AktKlangPlaylist.Name == aPlaylist.Name)
        //                {
        //                    for (UInt16 i = 0; i <= lbEditor.Items.Count - 1; i++)
        //                    {
        //                        if (((ListboxItemIcon)lbEditor.Items[i]).lbText.Content.ToString() == aPlaylist.Name)
        //                        {
        //                            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
        //                            if (grpobj == null || grpobj.objGruppe == -1)
        //                                return;

        //                            foreach (MusikZeile mZeile in lbPListGeräusche.Items)
        //                            {
        //                                if (grpobj != null && mZeile.tbtnCheck.IsChecked.Value)
        //                                    grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
        //                            }
        //                            if (grpobj != null)
        //                            {
        //                                VM.PlaylisteLeeren(grpobj);

        //                                grpobj.tbTopKlangKategorie.Text = "";
        //                                grpobj.tbTopKlangKategorie.Tag = null;
        //                                tboxEditorName.Text = VM.GetNeuenNamen("NeuePlayliste", 0);
        //                            }
        //                        }
        //                    }
        //                    AktKlangPlaylist = null;
        //                }

        //                if (BGPlayer.AktPlaylist != null && BGPlayer.AktPlaylist.Name == aPlaylist.Name)
        //                {
        //                    VM.MusikProgBarTimer.Stop();
        //                    if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
        //                    {
        //                        BGPlayer.BG[BGPlayeraktiv].mPlayer.Stop();
        //                        BGPlayer.BG[BGPlayeraktiv].mPlayer.Close();
        //                        btnBGAbspielen.IsEnabled = false;
        //                        BGPlayer.AktPlaylist = null;
        //                        lbMusiktitellist.Items.Clear();
        //                        BGPlayer.AktTitel.Clear();
        //                    }
        //                    grdSongInfo.Visibility = Visibility.Hidden;
        //                }
        //                List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(aPlaylist);
        //                titel.ForEach(delegate(Audio_Titel aTitel)
        //                {
        //                    Global.ContextAudio.RemoveTitelFromPlaylist(aPlaylist, aTitel);
        //                });

        //                if (aPlaylist.Key != null)
        //                {
        //                    foreach (btnHotkey btnHotkey in VM.hotkeyListUsed)// spnlHotkeys.Children)
        //                        if (((btnHotkey)btnHotkey.Tag).VM.taste == Convert.ToInt32(Convert.ToChar(aPlaylist.Key))) //MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.hotkey
        //                        {
        //                            VM.hotkeyListUsed.Remove(btnHotkey);
        //                            //spnlHotkeys.Children.Remove(btnHotkey);
        //                            break;
        //                        }
        //                }
        //                Global.ContextAudio.Delete<Audio_Playlist>(aPlaylist);

        //                int oldIndex = lbEditor.SelectedIndex;
        //                lbEditor.SelectedIndex = -1;
        //                lbEditor.SelectedIndex = oldIndex > 0? oldIndex-1: oldIndex;
        //                Global.SetIsBusy(false);
        //                ViewHelper.Popup("Die Playlist wurde erfolgreich gelöscht.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Löschen der Playlist ist ein Fehler aufgetreten.", ex);
        //    }
        //}

       


        //private void lbItembtnExportPlaylist_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;

        //        Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == g);
        //        if (aPlaylist != null)
        //        {
        //            string datei = ViewHelper.ChooseFile("Playliste exportieren", "Playlist_" + aPlaylist.Name.Replace("/", "_") + ".xml", true, "xml");
        //            if (datei != null)
        //            {
        //                Global.SetIsBusy(true, string.Format("Die Playlist wird exportiert ..."));

        //                datei = validateString(datei);
                        
        //                File.Delete(datei);
        //                aPlaylist.Export(datei, g);

        //                Global.SetIsBusy(false);
        //                ViewHelper.Popup("Die Playlist-Daten wurden erfolgreich gesichert.");
        //            }
        //        }
        //        else
        //            ViewHelper.ShowError("Die ausgewählte Playlist konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", new Exception());

        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren der Playlist ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnAudioDatenExport_Click(object sender, RoutedEventArgs e)
        //{
        //    Global.ContextAudio.Save();
        //    try
        //    {
        //        if (ViewHelper.ConfirmYesNoCancel("Komplette Sicherung", "Eine komplette Sicherung der Audiodaten wird durchgeführt, " +
        //                Environment.NewLine + "bestehend aus der Sicherung aller Playlisten und aller vorhandene Themelisten." + Environment.NewLine +
        //               Environment.NewLine + "Wollen Sie den Prozess durchführen?") != 2)
        //            return;

        //        btnAudioDatenExport.Tag = true;
        //        btnPlaylistExportALL.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        //        btnAudioDatenExport.Tag = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Bei dem Speichern der kompletten Audiodatenbank ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void AlleThemesExportieren(string dlgFolder)
        //{
        //    Global.SetIsBusy(true, string.Format("Alle Themes werden exportiert ..."));

        //    foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe.
        //                        Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")))
        //    {
        //        Global.SetIsBusy(true, string.Format("Theme '" + aTheme.Name + "' wird exportiert"));
        //        string pfaddatei = dlgFolder + "\\Theme_" + aTheme.Name.Replace("/", "_") + ".xml";

        //        pfaddatei = validateString(pfaddatei);

        //        VM.ExportTheme(aTheme, pfaddatei);
        //    }

        //    Global.SetIsBusy(true, string.Format("Theme Export beendet ..."));
        //}

        //private void btnThemeExportAll_Click(object sender, RoutedEventArgs e)
        //{
        //    Global.ContextAudio.Save();
        //    try
        //    {
        //        System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
        //        folderDlg.SelectedPath = Environment.CurrentDirectory;
        //        folderDlg.Description = "Wählen Sie ein Verzeichnis aus in das alle Dateien gespeichert werden sollen." + Environment.NewLine;
        //        if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            AlleThemesExportieren(folderDlg.SelectedPath);

        //            ViewHelper.Popup("Der Export wurde erfolgreich beendet" + Environment.NewLine + "Alle Themelisten wurden folgednes Verzeichnis exportiert" +
        //                Environment.NewLine + Environment.NewLine + folderDlg.SelectedPath + Environment.NewLine +
        //                Environment.NewLine + "!!! Bitte beachten Sie, dass Die PLAYLISTEN SEPARAT gesichert werden müssen !!!");
        //            Global.SetIsBusy(false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Bei dem Speichern der kompletten Audio-Themes ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnPlaylistExportAll_Click(object sender, RoutedEventArgs e)
        //{
        //    Global.ContextAudio.Save();
        //    try
        //    {
        //        System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
        //        folderDlg.SelectedPath = Environment.CurrentDirectory;
        //        folderDlg.Description = "Wählen Sie ein Verzeichnis aus in das alle Dateien gespeichert werden sollen";
        //        List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();
        //        if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            Global.SetIsBusy(true, string.Format("Export aller Playlisten wird vorbereitet..."));
        //            string pfad = folderDlg.SelectedPath;
                    
        //            Audio_Playlist.Export(Global.ContextAudio.PlaylistListe, pfad);

        //            if (btnAudioDatenExport.Tag != null && Convert.ToBoolean(btnAudioDatenExport.Tag))
        //            {
        //                AlleThemesExportieren(pfad);
        //                ViewHelper.Popup("Der Export aller Audio-Daten wurde erfolgreich beendet." + Environment.NewLine + Environment.NewLine +
        //                    "Alle Audio-Playlisten und Themes wurden in folgendes Verzeichnis exportiert." + Environment.NewLine +
        //                    Environment.NewLine + Environment.NewLine + pfad + Environment.NewLine);
        //            }
        //            else
        //                ViewHelper.Popup("Der Export wurde erfolgreich beendet." + Environment.NewLine + Environment.NewLine +
        //                    "Alle Playlisten wurden in folgendes Verzeichnis exportiert" + Environment.NewLine +
        //                    Environment.NewLine + Environment.NewLine + pfad + Environment.NewLine);

        //            Global.SetIsBusy(false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Bei dem Speichern der kompletten Audio-Playlisten ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private List<Guid> CheckUnterTheme(Guid wpnlGuid, List<Guid> schonAktiveThemes)
        //{
        //    foreach (Audio_Theme aUnterTheme in Global.ContextAudio.LoadThemesByGUID(wpnlGuid).Audio_Theme1)
        //    {
        //        if (!schonAktiveThemes.Contains(aUnterTheme.Audio_ThemeGUID))
        //        {
        //            schonAktiveThemes.Add(aUnterTheme.Audio_ThemeGUID);
        //            CheckUnterTheme(aUnterTheme.Audio_ThemeGUID, schonAktiveThemes);
        //        }
        //    }
        //    return schonAktiveThemes;
        //}
        //private List<Guid> CheckUnterThemeInLbi(Guid lbiGuid, List<Guid> schonAktiveThemes)
        //{
        //    foreach (Audio_Theme aUnterTheme in Global.ContextAudio.LoadThemesByGUID(lbiGuid).Audio_Theme1)
        //    {
        //        if (aUnterTheme.Audio_ThemeGUID == AktKlangTheme.Audio_ThemeGUID &&
        //            !schonAktiveThemes.Contains(lbiGuid))
        //        {
        //            schonAktiveThemes.Add(lbiGuid);
        //            CheckUnterTheme(lbiGuid, schonAktiveThemes);
        //        }
        //    }
        //    return schonAktiveThemes;
        //}

        //private void btnTopThemeAddTheme_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        ComboBox cmbx = new ComboBox();
        //        List<Guid> schonAktiveThemes = new List<Guid>();

        //        //Guid-Liste der schon verwendeten Themes erstellen
        //        schonAktiveThemes.Add(AktKlangTheme.Audio_ThemeGUID);
        //        foreach (boxThemeTheme boxItemTheme in wpnlEditorTopThemesThemes.Children)
        //        {
        //            schonAktiveThemes.Add((Guid)boxItemTheme.Tag);
        //            schonAktiveThemes = CheckUnterTheme((Guid)boxItemTheme.Tag, schonAktiveThemes);
        //        }
        //        //Themes, die das Aktuelle Theme enthalten auch auf die Guid-Liste
        //        foreach (ListboxItemIcon lbi in lbEditorTheme.Items)
        //            schonAktiveThemes = CheckUnterThemeInLbi((Guid)lbi.Tag, schonAktiveThemes);

        //        //Alle nicht vorhandenen Guids anzeigen
        //        for (int i = 0; i < lbEditorTheme.Items.Count; i++)
        //        {
        //            if (!schonAktiveThemes.Contains((Guid)((ListboxItemIcon)lbEditorTheme.Items[i]).Tag))
        //            {
        //                ListboxItemIcon lbi = new ListboxItemIcon();
        //                lbi.imgIcon = ((ListboxItemIcon)lbEditorTheme.Items[i]).imgIcon;
        //                lbi.lbText.Content = ((ListboxItemIcon)lbEditorTheme.Items[i]).lbText.Content;
        //                lbi.Tag = ((ListboxItemIcon)lbEditorTheme.Items[i]);
        //                cmbx.Items.Add(lbi);
        //            }
        //        }
        //        cmbx.SelectionChanged += cmbxThemeTheme_SelectionChanged;
        //        cmbx.DropDownClosed += cmbxThemeTheme_DropDownClosed;
        //        grdEditorThemeWPnlUTheme.Children.Add(cmbx);
        //        Grid.SetColumn(cmbx, 0);
        //        cmbx.IsDropDownOpen = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Beim Erstellen des DropDown-Feldes für die Themeliste ist ein Fehler aufgetreten.", ex);
        //    }
        //}


        //private void cmbxThemeTheme_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Guid ThemeGuid = (Guid)((ListboxItemIcon)((ListboxItemIcon)((ComboBox)sender).SelectedItem).Tag).Tag;

        //        AktKlangTheme.Audio_Theme1.Add(Global.ContextAudio.ThemeListe.First(t => t.Audio_ThemeGUID == ThemeGuid));
        //        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);

        //        boxThemeTheme bxTheme = new boxThemeTheme();
        //        bxTheme.txblkName.Text = ((ListboxItemIcon)((ComboBox)sender).SelectedItem).lbText.Content.ToString();
        //        bxTheme.Tag = ThemeGuid;
        //        bxTheme.btnRemove.Tag = ThemeGuid;
        //        bxTheme.btnRemove.Click += bxThemeBtnClose_Click;

        //        wpnlEditorTopThemesThemes.Children.Add(bxTheme);
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Bei der Auswahl des untergeordneten Themes ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void bxThemeBtnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Guid ThemeORPlaylistGuid = (Guid)((Button)sender).Tag;

        //        Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == ThemeORPlaylistGuid);
        //        if (aPlaylist != null)
        //        {
        //            AktKlangTheme.Audio_Playlist.Remove(aPlaylist);
        //        }
        //        else
        //        {
        //            Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == ThemeORPlaylistGuid);
        //            if (aTheme != null)
        //            {
        //                AktKlangTheme.Audio_Theme1.Remove(Global.ContextAudio.ThemeListe.First(t => t.Audio_ThemeGUID == ThemeORPlaylistGuid));
        //                wpnlEditorTopThemesThemes.Children.Remove((boxThemeTheme)((GroupBox)((Grid)((Button)sender).Parent).Parent).Parent);
        //            }
        //        }
        //        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Beim Lösen des untergeordenten Themes aus dem Theme ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        
        //private void bxEditorThemeBtnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Guid ThemeGuid = (Guid)((Button)sender).Tag;

        //        Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == (ThemeGuid));
        //        if (aPlaylist != null)
        //        {
        //            if (aPlaylist.Hintergrundmusik)
        //            {
        //                //wpnlEditorThemeMusik.Children.Remove((boxThemeTheme)((GroupBox)((Grid)((Button)sender).Parent).Parent).Parent);
        //                if (rbEditorMusik.IsChecked.Value)
        //                {
        //                    foreach (ListboxItemIcon lbi in lbEditor.Items)
        //                        if (ThemeGuid == (Guid)lbi.Tag)
        //                        {
        //                            lbi.Visibility = Visibility.Visible;
        //                            break;
        //                        }
        //                }
        //            }
        //            else
        //            {
        //                //wpnlEditorThemeGeräusch.Children.Remove((boxThemeTheme)((GroupBox)((Grid)((Button)sender).Parent).Parent).Parent);
        //                if (rbEditorKlang.IsChecked.Value)
        //                    foreach (ListboxItemIcon lbi in lbEditor.Items)
        //                    {
        //                        if (ThemeGuid == (Guid)lbi.Tag)
        //                        {
        //                            lbi.Visibility = Visibility.Visible;
        //                            break;
        //                        }
        //                    }
        //            }
        //            AktKlangTheme.Audio_Playlist.Remove(aPlaylist);

        //        }
        //        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Fehler beim Entfernen des Playlist/Theme-Buttons im Editor", ex);
        //    }
        //}

        //private void ThemeItemIconAblegen(Audio_Playlist aPlaylist)// lbEditorItem lbi)//ListboxItemIcon lbi)
        //{
        //     try
        //     {
        //        AktKlangTheme.Audio_Playlist.Add(aPlaylist);
        //        VM.SelectedEditorThemeItem = VM.SelectedEditorThemeItem;

               // boxThemeTheme bxTheme = new boxThemeTheme();
               // bxTheme.aPlaylist = aPlaylist;// lbi.APlaylist;
               // bxTheme.aTheme = AktKlangTheme;
               // bxTheme.imgIcon.Source = new BitmapImage(new Uri(lbi.ListBoxItemIconBild));// .imgIcon.Source;
               // bxTheme.txblkName.Text = aPlaylist.Name;// lbi.APlaylist.Name;// lbi.lbText.Content.ToString();
               // bxTheme.txblkName.IsEnabled = false;
               // bxTheme.Margin = new Thickness(5);
               // bxTheme.Tag = lbi;
                

               //// bxTheme.btnClose.Tag = lbi.Tag;
               //// bxTheme.btnClose.Click += bxEditorThemeBtnClose_Click;
                
               // //Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == ((Guid)lbi.Tag));
               // //if (aPlaylist != null)
               // //{
               //     if (AktKlangTheme == null)
               //         NeueKlangThemeInDB(tboxEditorName.Text);
               //     if (lbi.APlaylist.Hintergrundmusik)
               //     {
               //         //if (wpnlEditorThemeMusik.Children.Count == 0)
               //         {
               //             //wpnlEditorThemeMusik.Children.Add(bxTheme);
               //             //((ListboxItemIcon)lbEditor.Items[lbEditor.Items.IndexOf(lbi)]).Visibility = Visibility.Collapsed;
               //             if (!AktKlangTheme.Audio_Playlist.Contains(lbi.APlaylist))
               //             {
               //                 AktKlangTheme.Audio_Playlist.Add(lbi.APlaylist);
               //                 Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
               //             }
               //         }
               //         //else
               //         //{
               //         //    ViewHelper.ShowError("Es ist bereits eine Musik-Playliste in dem Theme eingetragen." + Environment.NewLine +
               //         //        "Entfernen Sie zunächst die aktuelle Musik-Playliste des Themes um eine neue zu definieren.", new Exception());
               //         //    return;
               //         //}
               //     }
               //     else
               //     {
               //         //wpnlEditorThemeGeräusch.Children.Add(bxTheme);
               //         //((lbEditorItem)lbEditor.Items[lbEditor.Items.IndexOf(lbi)]).Visibility = Visibility.Collapsed;
               //         if (!AktKlangTheme.Audio_Playlist.Contains(lbi.APlaylist))
               //         {
               //             AktKlangTheme.Audio_Playlist.Add(lbi.APlaylist);
               //             Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
               //         }
               //     }
               // /*}
               // else
               // {
               //     Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == ((Guid)lbi.Tag));
               //     if (aTheme != null)
               //     {
               //         wpnlEditorTopThemesThemes.Children.Add(bxTheme);
               //         AktKlangTheme.Audio_Theme1.Add(aTheme);
               //         Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
               //     }
               // }*/
            //}
            //catch (Exception ex)
            //{
            //    ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Einfügen deiner Playliste/Themes in ein Theme ist ein Fehler aufgetreten", ex);
            //}
       // }

        //private void MoveItem(ListBox lb, AudioZeile audioZeile, Audio_Playlist_Titel aPlaylistTitel, int dif)
        //{
        //    Audio_Playlist_Titel aPlaylistTitel_alt = AktKlangPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Reihenfolge == aPlaylistTitel.Reihenfolge + dif);
        //    aPlaylistTitel_alt.Reihenfolge = aPlaylistTitel_alt.Reihenfolge - dif;
        //    Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel_alt);

        //    aPlaylistTitel.Reihenfolge = aPlaylistTitel.Reihenfolge + dif;
        //    Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel);

        //    object selected = audioZeile;
        //    lb.Items.Remove(selected);
        //    lb.Items.Insert(aPlaylistTitel.Reihenfolge, selected);
        //    lb.ScrollIntoView(selected);
        //}
        

        //private void btnBGRepeat_Click(object sender, RoutedEventArgs e)
        //{    
        //    ((Image)btnBGRepeat.Content).Source = new BitmapImage(new Uri(
        //        (btnBGRepeat.IsChecked == null) ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat1.png" :
        //        (btnBGRepeat.IsChecked.Value) ?   "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat.png" :
        //                                          "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat0.png"));
        //    btnBGRepeat.ToolTip = (btnBGRepeat.IsChecked == null) ? "Einzelstück wiederholen" : btnBGRepeat.IsChecked.Value ? "Playliste wiederholen" : "Keine Wiederholung";

        //    if (BGPlayer.AktPlaylist == null) return;
            
        //    BGPlayer.AktPlaylist.Repeat = btnBGRepeat.IsChecked;
        //    Global.ContextAudio.Update<Audio_Playlist>(BGPlayer.AktPlaylist);
        //}

        
        //private void btnEditRepeat_Click(object sender, RoutedEventArgs e)
        //{
        //    ((Image)btnEditRepeat.Content).Source = new BitmapImage(new Uri(
        //        (btnEditRepeat.IsChecked == null) ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat1.png" :
        //        (btnEditRepeat.IsChecked.Value) ?   "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat.png" :
        //                                            "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/repeat0.png"));
        //    btnEditRepeat.ToolTip = (btnEditRepeat.IsChecked == null) ? "Einzelstück wiederholen" :
        //        btnEditRepeat.IsChecked.Value ? "Playliste wiederholen" : "Keine Wiederholung";

        //    AktKlangPlaylist.Repeat = btnEditRepeat.IsChecked;
        //    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        //}

        //private void lbItembtnExportTheme_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
        //        Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == g);

        //        if (aTheme != null)
        //        {
        //            string pfaddatei = ViewHelper.ChooseFile("Theme exportieren", "Theme_" + aTheme.Name.Replace("/", "_") + ".xml", true, "xml");

        //            pfaddatei = validateString(pfaddatei);

        //            VM.ExportTheme(aTheme, pfaddatei);
        //            ViewHelper.Popup("Die Themeliste wurde erfolgreich gesichert." + Environment.NewLine + Environment.NewLine +
        //                "!!! Bitte beachten Sie, dass Die PLAYLISTEN SEPARAT gesichert werden müssen !!!");
        //            Global.SetIsBusy(false);
        //        }
        //        else
        //            ViewHelper.ShowError("Das ausgewählte Theme konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", new Exception());
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren des Themes ist ein Fehler aufgetreten.", ex);
        //    }
        //}

        //private void btnKlangThemeImport_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        List<string> dateien;
        //        if (btnAudioDatenImport.Tag != null)
        //            dateien = btnAudioDatenImport.Tag as List<string>;
        //        else
        //            dateien = ViewHelper.ChooseFiles("Theme importieren", "", false, "xml");
        //        if (dateien != null)
        //        {
        //            try
        //            {
        //                bool _nicht_first = false;
        //                foreach (string pfad in dateien)
        //                {
        //                    Global.SetIsBusy(true, string.Format("Neues Theme  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + "'  wird importiert ..."));

        //                    XmlTextReader textReader = new XmlTextReader(pfad);
        //                    textReader.Read();
        //                    while (textReader.NodeType == XmlNodeType.XmlDeclaration ||
        //                        (textReader.NodeType == XmlNodeType.Element && textReader.Name == "Audio_Theme"))
        //                        textReader.Read();
        //                    if (textReader.Name == "Audio_Playlist")
        //                        continue;
        //                    if (textReader.NodeType != XmlNodeType.Comment)
        //                    {
        //                        textReader.Read();
        //                        XmlDocument doc = new XmlDocument();
        //                        XmlNode node = doc.ReadNode(textReader);
        //                        if (node.Attributes.Count > 0 && node.Attributes["Audio_ThemeGUID"] != null &&
        //                            node.Attributes["Audio_ThemeGUID"].Value == "00000000-0000-0000-0000-00000000a11e")
        //                            break;

        //                        if (ViewHelper.ConfirmYesNoCancel("Unsicherer Verlauf", "Theme-Import:  " + System.IO.Path.GetFileNameWithoutExtension(pfad) + Environment.NewLine + Environment.NewLine +
        //                            "ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
        //                           "Leider konnte dieser Prozess noch NICHT ZUVERLÄSSIG programmiert werden." + Environment.NewLine +
        //                           Environment.NewLine + "Es muss damit gerechtnet werden, das die exportierte Datei" + Environment.NewLine + "NICHT MEHR IMPORTIERT werden kann!" +
        //                           Environment.NewLine + Environment.NewLine + "Soll der Vorgang trotzdem fortgesetzt werden?") != 2)
        //                        {
        //                            Global.SetIsBusy(false);
        //                            return;
        //                        }

        //                        Audio_Playlist.Import(pfad, "Audio_Theme", _nicht_first);
        //                        _nicht_first = true;
        //                        Global.ContextAudio.Save();
        //                    }
        //                    else
        //                    {
        //                        string thName = "";
        //                        List<string> aPlayListsName = new List<string>();
        //                        List<string> aPlayListsGuid = new List<string>();
        //                        List<string> aThemesName = new List<string>();
        //                        List<string> aThemesGuid = new List<string>();
        //                        List<string> lstNotInclude = new List<string>();
        //                        bool nurGeräusch = false;
        //                        while (textReader.Read())
        //                        {
        //                            XmlDocument doc = new XmlDocument();

        //                            if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "Themename")
        //                            {
        //                                thName = textReader.NamespaceURI;

        //                                for (int i = 0; i < textReader.AttributeCount; i++)
        //                                {
        //                                    if (textReader.Name == "IstNurGeräuschTheme")
        //                                    {
        //                                        nurGeräusch = Convert.ToBoolean(textReader.Value);
        //                                        break;
        //                                    }
        //                                    textReader.MoveToNextAttribute();
        //                                }

        //                                textReader.Read();
        //                                while (textReader.NodeType == XmlNodeType.Element &&
        //                                    textReader.Name.StartsWith("Playlist") || textReader.Name.StartsWith("Theme"))
        //                                {
        //                                    XmlNode node = doc.ReadNode(textReader);
        //                                    if (node.Attributes.Count > 0 && node.Attributes["Name"] != null &&
        //                                        (node.Attributes["Audio_PlaylistGUID"] != null || node.Attributes["Audio_ThemeGUID"] != null))
        //                                    {
        //                                        if (node.Name.StartsWith("Playlist"))
        //                                        {
        //                                            // Playlisten einlesen
        //                                            aPlayListsName.Add(node.Attributes["Name"].Value);
        //                                            aPlayListsGuid.Add(node.Attributes["Audio_PlaylistGUID"].Value);
        //                                        }
        //                                        else
        //                                            if (node.Name.StartsWith("Theme"))
        //                                            {
        //                                                // UnterThemes einlesen
        //                                                if (node.Attributes["Audio_ThemeGUID"].Value.ToUpper() != "00000000-0000-0000-0000-00000000A11E")
        //                                                {
        //                                                    aThemesName.Add(node.Attributes["Name"].Value);
        //                                                    aThemesGuid.Add(node.Attributes["Audio_ThemeGUID"].Value);
        //                                                }
        //                                            }
        //                                    }
        //                                    if (textReader.NodeType == XmlNodeType.EndElement)
        //                                        break;
        //                                }

        //                                // Theme erstellen
        //                                if (thName != "")
        //                                {
        //                                    if (Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name == thName) != null)
        //                                    {
        //                                        int resp = ViewHelper.ConfirmYesNoCancel("Doppelter Theme-Name", "Ein Theme mit dem Namen '" + thName + "' ist schon vorhanden." + Environment.NewLine +
        //                                            Environment.NewLine + "Soll das vorhandene Theme überschrieben werden");
        //                                        if (resp == 2)
        //                                        {
        //                                            AktKlangTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name == thName);
        //                                            AktKlangTheme.Audio_Playlist.Clear();
        //                                            AktKlangTheme.Audio_Theme1.Clear();
        //                                            AktKlangTheme.Audio_Theme2.Clear();
        //                                        }
        //                                        else
        //                                            if (resp == 0)
        //                                            {
        //                                                Global.SetIsBusy(false);
        //                                                return;
        //                                            }
        //                                    }
        //                                    else
        //                                        VM.NeuesKlangThemeInDB(thName);

        //                                    AktKlangTheme.NurGeräusche = nurGeräusch;
        //                                    foreach (string aPlyLstGuid in aPlayListsGuid)
        //                                    {
        //                                        Audio_Playlist aPlayList = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID.ToString() == aPlyLstGuid);
        //                                        if (aPlayList == null)
        //                                            aPlayList = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Name == aPlayListsName[aPlayListsGuid.IndexOf(aPlyLstGuid)]);

        //                                        if (aPlayList != null)
        //                                            AktKlangTheme.Audio_Playlist.Add(aPlayList);
        //                                        else
        //                                            lstNotInclude.Add(aPlayListsName[aPlayListsGuid.IndexOf(aPlyLstGuid)]);
        //                                    }

        //                                    foreach (string aThemeGuid in aThemesGuid)
        //                                    {
        //                                        Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID.ToString() == aThemeGuid);
        //                                        if (aTheme == null)
        //                                            aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name != aThemesName[aThemesGuid.IndexOf(aThemeGuid)]);

        //                                        if (aTheme != null)
        //                                            AktKlangTheme.Audio_Theme1.Add(aTheme);
        //                                        else
        //                                            lstNotInclude.Add(aThemesName[aThemesGuid.IndexOf(aThemeGuid)]);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        Global.ContextAudio.Save();
        //                        if (lstNotInclude.Count > 0)
        //                        {
        //                            string text = "";
        //                            foreach (string s in lstNotInclude)
        //                                text += s + Environment.NewLine;

        //                            ViewHelper.Popup("ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
        //                                Environment.NewLine + "Import nur teilweise durchgeführt" + Environment.NewLine + Environment.NewLine +
        //                                "Von dem Theme -" + thName + "- konnten folgende Playlisten/Unterthemes leider nicht gefunden werden." +
        //                                Environment.NewLine + Environment.NewLine + text + Environment.NewLine +
        //                                "Bitte stellen Sie sicher, dass die Playlisten/Unterthemes integriert sind, bevor die Themes importiert werden.");
        //                        }
        //                    }
        //                }
        //                Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
        //                Global.ContextAudio.Save();

        //                Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
        //                VM.AktualisiereHotKeys();

        //                Global.SetIsBusy(false);
        //                lbEditor.SelectedIndex = -1;

        //                VM.rbEditorEditPlaylist = false;
        //                //tcAudioPlayer.Tag = -1;

        //                //tiEditor_GotFocus(sender, null);
        //                VM.rbEditorEditPlaylist = true;
        //                //(wpnlPListThemes.Tag as List<Guid>).Clear();

        //            }
        //            catch (Exception ex)
        //            {
        //                Global.SetIsBusy(false);
        //                ViewHelper.ShowError("Beim Import des Themes ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.SetIsBusy(false);
        //        ViewHelper.ShowError("Beim Importieren der Theme-Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
        //    }
        //}

        
        //private void tbtnErwPlayerDebug_Click(object sender, RoutedEventArgs e)
        //{
        //    if (tbtnErwPlayerDebug.IsChecked.Value)
        //        _debugTreeview.Start();
        //    else
        //        _debugTreeview.Stop();
        //}
    
        //private void _debugTreeview_Tick(object sender, EventArgs e)
        //{
        //    tvDebug.Items.Clear();
        //    foreach(GruppenObjekt grpobj in VM._GrpObjecte)
        //    {
        //        if (grpobj.wirdAbgespielt ||
        //            grpobj._listZeile.FirstOrDefault(t => t.istLaufend) != null ||
        //            grpobj._listZeile.FirstOrDefault(t => t.FadingOutStarted) != null)
        //        {
        //            TreeViewItem tvItem = null;
        //            foreach (TreeViewItem tvi in tvDebug.Items)
        //                if ((Guid)tvi.Tag == grpobj.aPlaylist.Audio_PlaylistGUID)
        //                    tvItem = tvi;
        //            if (tvItem == null)
        //            {
        //                tvItem = new TreeViewItem();
        //                tvDebug.Items.Add(tvItem);
        //            }
        //            tvItem.Tag = grpobj.aPlaylist.Audio_PlaylistGUID;
        //            tvItem.Header = grpobj.aPlaylist.Name;

        //            updateTVTitel(tvItem);
        //        }
        //    }
        //}

        //private void updateTVTitel(TreeViewItem tvItem)
        //{
        //    GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == (Guid)tvItem.Tag);
        //    if (grpobj == null) return;

        //    tvItem.Items.Clear();

        //    foreach (KlangZeile kZeile in grpobj._listZeile.FindAll(t => t._mplayer != null).FindAll(tt => tt.istLaufend))
        //    {
        //        TreeViewItem tvi = new TreeViewItem();
        //        tvi.Header = Math.Round(kZeile._mplayer.Volume,4) * 100 + "% Total-Volumen  von " + kZeile.aPlaylistTitel.Volume + "% bei 100% Regelerstellung    " + kZeile.aPlaylistTitel.Audio_Titel.Name;
        //        tvItem.Items.Add(tvi);
        //    }
        //    tvItem.ExpandSubtree();            
        //}


        //private void rsldTeilSongMin_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    BGPlayer.AktPlaylistTitel.TeilStart = ((Slider)sender).Value;
        //}

        //private void rsldTeilSongMax_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    BGPlayer.AktPlaylistTitel.TeilEnde = ((Slider)sender).Value;
        //}
