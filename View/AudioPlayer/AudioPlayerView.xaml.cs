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
using System.Windows.Interactivity;
using System.Collections.ObjectModel;

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
using Un4seen.Bass;
//using Meta.Vlc.Wpf;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für AudioPlayerView.xaml
    /// </summary>
    /// 

    public class MyTimer
    {
        static int start = 0;
        static int stop = 0;
        public static void start_timer()
        {
            start = Environment.TickCount;
        }

        public static void stop_timer()
        {
            stop_timer("");
        }

        public static void stop_timer(string msg)
        {
            stop = Environment.TickCount;
            print(msg);
        }

        private static void print(string msg)
        {
            string output = "MyTimer(" + msg + "): " + (stop - start) + " Millisekunden";
            System.Diagnostics.Debug.WriteLine(output);
        }
    }

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

    #region   //--- Drag n Drop ITEM ----
    public class DragAndDropListBox<T> : ListBox where T : class
    {
        private P FindVisualParent<P>(DependencyObject child)
            where P : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            P parent = parentObject as P;
            if (parent != null)
                return parent;

            return FindVisualParent<P>(parentObject);
        }

        public DragAndDropListBox()
        {
            var style = new Style(typeof(ListBoxItem));

            style.Setters.Add(
                new EventSetter(
                    ListBoxItem.PreviewMouseLeftButtonDownEvent,
                    new MouseButtonEventHandler(ListBoxItem_PreviewMouseLeftButtonDown)));
            this.ItemContainerStyle = style;
        }
        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Move(lbEditorItemVM source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                var items = this.DataContext as IList<lbEditorItemVM>;
                if (items != null)
                {
                    items.Insert(targetIndex + 1, source);
                    items.RemoveAt(sourceIndex);
                }
            }
            else
            {
                var items = this.DataContext as IList<lbEditorItemVM>;
                if (items != null)
                {
                    int removeIndex = sourceIndex + 1;
                    if (items.Count + 1 > removeIndex)
                    {
                        items.Insert(targetIndex, source);
                        items.RemoveAt(removeIndex);
                    }
                }
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public Item(string name)
        {
            this.Name = name;
        }
    }
    public class ItemDragAndDropListBox : DragAndDropListBox<Item> { }

    #endregion

    public partial class AudioPlayerView : UserControl
    {
        private Point _dragStartPoint;
        TabItemControl AudioTIC = null;
        
        public VM.AudioPlayerViewModel.MusikView BGPlayer
        {
            get { return VM.BGPlayer; }
            set { VM.BGPlayer = value; }
        }

        public Audio_Playlist AktKlangPlaylist
        {
            get { return VM.AktKlangPlaylist; }
            set
            {
                VM.AktKlangPlaylist = value;
            }
        }

        public Audio_Theme AktKlangTheme
        {
            get { return VM.AktKlangTheme; }
            set
            {
                VM.AktKlangTheme = value;
            }
        }

        delegate void UpdateUI();

        public AudioPlayerView()
        {
            InitializeComponent();

            slPlaylistVolume.Value = Einstellungen.GeneralGeräuscheVolume;
            slBGVolume.Value = Einstellungen.GeneralMusikVolume;
            slHotkey.Value = Einstellungen.GeneralHotkeyVolume;

            DataContextChanged += AudioPlayerView_DataContextChanged;
        }

        void AudioPlayerView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {               
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

            VM.audioZeileMouseOverDropped = lbEditorListe.Items.IndexOf((sender as AudioZeile).VM);
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
            if (VM.pointerZeileDragDrop == null)
                return;

            Point mousePos = e.GetPosition(null);
            Vector diff = ((Point)VM.pointerZeileDragDrop) - mousePos;

            Point mp = Mouse.GetPosition(aZeile);
            VM.audioZeileMouseOverDropped = lbEditorListe.Items.IndexOf(aZeile.VM);
            //Abfrage bei gedrückter Maustaste, wenn im Vorderen Bereich und nicht auf der ProgressBar (um Teilabspielen zu editieren)
            if (e.LeftButton == MouseButtonState.Pressed &&
                (mp.X < +aZeile.pbarTitel.ActualWidth) && mp.X > 0 &&
                ((mp.Y > 0 && mp.Y < aZeile.lbiEditorRow.ActualHeight / 2 - aZeile.pbarTitel.ActualHeight / 2) ||
                 (mp.Y > aZeile.lbiEditorRow.ActualHeight / 2 + aZeile.pbarTitel.ActualHeight / 2)))
            {
                // Initialisiere drag & drop Operation
                DataObject dragData;
                if (lbEditorListe.SelectedItems.Count > 1)
                { 
                    dragData = new DataObject("meineAudioZeilenListe", lbEditorListe.SelectedItems);
                }
                else
                    dragData = new DataObject("meineAudioZeile", aZeile);
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
                    if (AudioTIC != null)
                    {
                        AudioTIC.CommandBindings.Add(new CommandBinding(AudioTabClose, VM.OnAudioTabClose));
                        AudioTIC._buttonClose.Command = AudioTabClose;
                    }
                }

                if (Einstellungen.ShowPlaylistFavorite && !MeisterGeister.ViewModel.MainViewModel.Instance.ShowFavPlaylist)
                    MeisterGeister.ViewModel.MainViewModel.Instance.ShowFavPlaylist = true;

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
                        if (BGPlayer.BG[0].aData.audioStream != 0 && !BGPlayer.BG[0].FadingOutStarted)
                        {
                            BGPlayer.BG[0].FadingOutStarted = true;
                            VM.BGFadingOut(BGPlayer.BG[0], true, true);
                        }
                        if (BGPlayer.BG[1].aData.audioStream != 0 && !BGPlayer.BG[1].FadingOutStarted)
                        {
                            BGPlayer.BG[1].FadingOutStarted = true;
                            VM.BGFadingOut(BGPlayer.BG[1], true, true);
                        }
                    }

                    for (int i = 0; i < VM._GrpObjecte.Count; i++)
                        VM.AlleKlangSongsAus(VM._GrpObjecte[i], true, true, false, true);

                    VM.KlangProgBarTimer.Stop();
                    VM.MusikProgBarTimer.Stop();
                    VM.workerGetLength.Dispose();
                }
                if (VM.workerGetLength.IsBusy)
                    VM.workerGetLength.Dispose();
                if (VM.ChkAnzDateien._bkworker.IsBusy)
                    VM.ChkAnzDateien._bkworker.Dispose();

                MeisterGeister.ViewModel.MainViewModel.Instance.ShowFavPlaylist = false;
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
                if (BGPlayer.AktPlaylist != null && VM.MusikAktiv.aData.audioStream != 0) // BGPlayer.BG[BGPlayeraktiv]
                {
                    Point pts = e.GetPosition((sender as ProgressBar));
                    double total = (sender as ProgressBar).Maximum;
                    double res = ((pts.X * 100) / ((double)(sender as ProgressBar).ActualWidth)) / 100;
                    Bass.BASS_ChannelSetPosition(VM.MusikAktiv.aData.audioStream, total * res / 1000);
                    //VM.MusikAktiv.mPlayer.Position = TimeSpan.FromMilliseconds(total * res); //BGPlayer.BG[BGPlayeraktiv]
                }
            }
            catch (Exception) { }
        }

        private void tbtnKlangPauseX_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
                if (VM.SelectedEditorItem != null && grpobj == null)
                {
                    lbEditorItemVM lbi = VM.SelectedEditorItem;
                  //  VM.SelectedEditorItem = null;
                    VM.SelectedEditorItem = lbi;
                    grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
                }
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
                        if (grpobj.aPlaylist.Hintergrundmusik)
                        {
                            grpobj._listZeile[i].FadingOutStarted = false;
                            VM.FadingIn(null, grpobj._listZeile[i], grpobj._listZeile[i].aData, grpobj._listZeile[i].Aktuell_Volume / 100);
                        }
                    }
                    else
                        grpobj._listZeile[i].istStandby =
                            (!grpobj._listZeile[i].istPause && !grpobj._listZeile[i].istLaufend && grpobj._listZeile[i].aPlaylistTitel.Aktiv) ? true : false;
                }
                VM.CheckPlayStandbySongs(grpobj);

                if (!grpobj.aPlaylist.Hintergrundmusik && grpobj.aPlaylist.Fading)
                    VM.FadingInGeräusch(grpobj);
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
                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = VM._GrpObjecte.FirstOrDefault(t => t.visuell);
                if (grpobj == null)
                    return;
                if (tiEditor.IsSelected && grpobj.sollBtnGedrueckt > 0)
                    grpobj.sollBtnGedrueckt--;
                grpobj.wirdAbgespielt = false;

                if (!grpobj.aPlaylist.Hintergrundmusik && grpobj.aPlaylist.Fading)
                    VM.FadingOutGeräusch(true, true, grpobj);

                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    if (!grpobj.aPlaylist.Hintergrundmusik)
                    {
                        if (grpobj._listZeile[i].aData != null && !grpobj.aPlaylist.Fading)
                        {
                            grpobj._listZeile[i].istPause = true;
                            grpobj._listZeile[i].aData.Pause();
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
                            VM.FadingOut(grpobj._listZeile[i], grpobj, true, true);

                            grpobj._listZeile[i].istLaufend = false;
                            grpobj._listZeile[i].audioZeileVM.TitelMinimum = 0;
                            grpobj._listZeile[i].istStandby = true;
                        }
                    }
                }
                VM.CheckPlayStandbySongs(grpobj);
                grpobj.totalTimePlylist = -1;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Abwählen der Pause-Funktion für die Playlist ist ein Fehler aufgetreten", ex);
            }
        }

        public void slVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 1)
            {
                ((Slider)sender).Value += ((((Slider)sender).Value < ((Slider)sender).Maximum-2) ? 3 : ((((Slider)sender).Maximum - ((Slider)sender).Value)));
            }
            else
            { ((Slider)sender).Value += ((((Slider)sender).Value > 2) ? -3 : 0); }
        }

        private void cmbxThemeTheme_DropDownClosed(object sender, EventArgs e)
        {
            grdEditorThemeWPnlUTheme.Children.Remove((ComboBox)sender);
        }

        private P FindVisualParent<P>(DependencyObject child)
            where P : DependencyObject
        {
            try
            {
                var parentObject = VisualTreeHelper.GetParent(child);
                if (parentObject == null)
                    return null;

                P parent = parentObject as P;
                if (parent != null)
                    return parent;
                return FindVisualParent<P>(parentObject);
            }
            catch (Exception)
            {
                //ViewHelper.ShowError("Datenbankfehler" + Environment.NewLine + "Beim Analysieren der Playlist '" + 
                //    child.GetValue(System.Windows.Documents.Run.TextProperty) + "' ist ein Fehler aufgetreten.", ex);
                return null;
            }
        }

        private void lbEditorPlaylist_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var lbi = (e.OriginalSource is Visual) ? FindVisualParent<lbEditorItem>((DependencyObject)e.OriginalSource) : null;
            if (lbi != null)
            {
                if (VM.pointerPlaylistDragDrop == null ||
                    VM.PlaylistAZ)
                    return;

                var lb = sender as ListBox;
                VM.lbiPlaylistMouseOverDropped = VM.FilteredEditorListBoxItemListe.IndexOf(lbi.VM);
                Point point = e.GetPosition(null);
                Vector diff = ((Point)VM.pointerPlaylistDragDrop) - point;

                if (e.LeftButton != MouseButtonState.Pressed)
                    VM.lbiEditorPlaylistStartDnD = lbi;

                if (VM.lbiEditorPlaylistStartDnD != null &&
                    e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance/2 ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance/2))
                {
                    if (lbi != null && VM.lbiEditorPlaylistStartDnD.VM != null)
                    {
                        VM.PlaylistListeNichtUpdaten = true;
                        DataObject dragData = new DataObject("lbiPlaylistVM", VM.lbiEditorPlaylistStartDnD.VM);
                        DragDrop.DoDragDrop(VM.lbiEditorPlaylistStartDnD, dragData, DragDropEffects.Move);
                        VM.lbiEditorPlaylistStartDnD = null;
                    }
                }
            }
            else
                VM.PlaylistListeNichtUpdaten = false;
        }
        
        private void lbEditorTheme_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var lbi = (e.OriginalSource is Visual) ? FindVisualParent<lbEditorThemeItem>((DependencyObject)e.OriginalSource) : null;
            if (lbi != null)
            {
                if (VM.pointerPlaylistDragDrop == null)
                    return;

                var lb = sender as ListBox;
                VM.lbiPlaylistMouseOverDropped = VM.FilteredEditorThemeListBoxItemListe.IndexOf(lbi.VM);
                Point point = e.GetPosition(null);
                Vector diff = ((Point)VM.pointerPlaylistDragDrop) - point;

                if (e.LeftButton != MouseButtonState.Pressed)
                    VM.lbiEditorThemeStartDnD = lbi;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance/2 ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance/2))
                {
                    if (lbi != null && VM.lbiEditorThemeStartDnD != null && VM.lbiEditorThemeStartDnD.VM != null)
                    {
                        VM.PlaylistListeNichtUpdaten = true;
                        DataObject dragData = new DataObject("lbiThemeVM", VM.lbiEditorThemeStartDnD.VM);
                        DragDrop.DoDragDrop(VM.lbiEditorThemeStartDnD, dragData, DragDropEffects.Move);
                        VM.lbiEditorThemeStartDnD = null;
                    }
                }
            }
            else
                VM.PlaylistListeNichtUpdaten = false;
        }
        

        private void lbEditor_Drop(object sender, DragEventArgs e)
        {
            if (sender == null) return;
            if (sender is lbEditorItem)
            {
                if (VM.lbiPlaylistMouseOverDropped == -1)
                    return;
                VM.PlaylistListeNichtUpdaten = true;
                lbEditorItemVM source = e.Data.GetData("lbiPlaylistVM") as lbEditorItemVM;
                lbEditorItemVM target = ((lbEditorItem)(sender)).DataContext as lbEditorItemVM;

                int sourceIndex = lbEditor.Items.IndexOf(source);
                int targetIndex = lbEditor.Items.IndexOf(target);

                VM.MoveLbEditorItem(source.APlaylist, targetIndex - sourceIndex);
                VM.FilteredEditorListBoxItemListe = VM.FilteredEditorListBoxItemListe.OrderBy(t => t.APlaylist.Reihenfolge).ToList();

                //Listen im Musik-Player & Erw.Player aktualisieren
                if (source.APlaylist.Hintergrundmusik || target.APlaylist.Hintergrundmusik)
                    VM.FilteredErwPlayerMusikListItemListe = VM.FilteredErwPlayerMusikListItemListe.OrderBy(t => t.VM.aPlaylist.Reihenfolge).ToList();
                else
                    VM.FilteredErwPlayerGeräuscheListItemListe = VM.FilteredErwPlayerGeräuscheListItemListe.OrderBy(t => t.VM.aPlaylist.Reihenfolge).ToList();
            }
            else
            {
                if (e.Data.GetDataPresent("meineAudioZeile"))
                {
                    //Prozess beenden wenn unglütige Ablage oder ViewModel bereits entfernt
                    Audio_Playlist zielPlaylist = VM.DropZielPlaylist != null ?
                        VM.DropZielPlaylist :
                        (e.Data.GetData("meineAudioZeile") as AudioZeile).VM == null? null:
                            (e.Data.GetData("meineAudioZeile") as AudioZeile).VM.aPlayTitel.Audio_Playlist;

                    if (VM.audioZeileMouseOverDropped == -1 ||
                        (e.Data.GetData("meineAudioZeile") as AudioZeile).VM == null)
                        return;
                    AZeileAblegen(e.Data.GetData("meineAudioZeile") as AudioZeile,
                        (e.Data.GetData("meineAudioZeile") as AudioZeile).VM,
                        zielPlaylist,
                        e,
                        sender);
                    VM.DropZielPlaylist = null;
                }
                else
                    if (e.Data.GetDataPresent("meineAudioZeilenListe"))
                {
                    //Prozess beenden wenn unglütige Ablage oder ViewModel bereits entfernt
                    System.Collections.IList lstItems = (System.Collections.IList)e.Data.GetData("meineAudioZeilenListe");
                    
                        //var coll = lstItems.Cast<List<AudioZeile>>();
                    //lstItems = ((System.Collections.IList)e.Data.GetData("meineAudioZeilenListe")).Cast<AudioZeile>();

                    Audio_Playlist zielPlaylist = VM.DropZielPlaylist != null ?
                        VM.DropZielPlaylist : VM.AktKlangPlaylist;
                        //(e.Data.GetData("meineAudioZeilenListe") as AudioZeile).VM.aPlayTitel.Audio_Playlist;

                        
                    if (VM.audioZeileMouseOverDropped == -1 ||
                        (lstItems == null || lstItems.Count == 0)) //e.Data.GetData("meineAudioZeilenListe") as ItemCollection) == null)
                        return;

                    foreach (AudioZeileVM aZeileVM in lstItems)// e.Data.GetData("meineAudioZeilenListe") as  ItemCollection)
                        AZeileAblegen(null, aZeileVM,
                            zielPlaylist,
                            e,
                            sender);
                    VM.DropZielPlaylist = null;
                    lbEditorListe.SelectedItem = null;
                }
                else
                {
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        if (e.Data.GetDataPresent(DataFormats.FileDrop))
                        {
                            List<string> gedroppteDateien = new List<string>();
                            foreach (var s in (string[])e.Data.GetData(DataFormats.FileDrop, false))
                            {
                                if (Directory.Exists(s))
                                    gedroppteDateien.AddRange(Directory.GetFiles(s, "*", SearchOption.AllDirectories));
                                else
                                    gedroppteDateien.Add(s);
                            }
                            VM._DateienAufnehmen(gedroppteDateien, null, null, VM.AktKlangPlaylist, 0, false);
                        }
                        Global.ContextAudio.Update<Audio_Playlist>(VM.AktKlangPlaylist);
                        Mouse.OverrideCursor = null;
                    }
                    catch (Exception ex)
                    {
                        Mouse.OverrideCursor = null;
                        ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ablegen der Dateien in der Playlist ist ein Fehler aufgetreten.", ex);
                    }
                    return;
                }
            }
            e.Data.SetData("");
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
            //if (e.Data.GetDataPresent("meinListBoxItemIcon"))
            //{
            //    VM.ThemeItemIconAblegen(e.Data.GetData("meinListBoxItemIcon") as Audio_Playlist);
            //    VM._dndZeilenCursor = null;
            //}
            if (e.Data.GetDataPresent("lbiPlaylistVM"))
            {
                lbEditorItemVM source = e.Data.GetData("lbiPlaylistVM") as lbEditorItemVM;
                VM.ThemeItemIconAblegen(source.APlaylist);
                VM._dndZeilenCursor = null;
            }
        }


        private void lbitemEditor_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meineAudioZeile"))
            {
                Audio_Playlist aplaylist = (Audio_Playlist)((StackPanel)sender).Tag;
                AZeileAblegen(e.Data.GetData("meineAudioZeile") as AudioZeile, null, aplaylist, e, sender);
            }
            else
            if (e.Data.GetDataPresent("meineAudioZeilenList"))
            {
                Audio_Playlist aplaylist = (Audio_Playlist)((StackPanel)sender).Tag;
                AZeileAblegen(e.Data.GetData("meineAudioZeilenList") as AudioZeile, null, aplaylist, e, sender);
            }
                
            else
                if (e.Data.GetDataPresent("lbiPlaylistVM"))
                    VM._dndZeilenCursor = null;
        }

        private void AZeileAblegen(AudioZeile aZeile, AudioZeileVM aZeileVM, Audio_Playlist ZielPlaylist, DragEventArgs e, object sender)
        {
            try
            {
                //Audio_Playlist aplaylist = (Audio_Playlist)((StackPanel)sender).Tag;
                Mouse.OverrideCursor = Cursors.Wait;
                bool kopieren = Keyboard.Modifiers == ModifierKeys.Control ? true : false;

                List<string> gedroppteDateien = new List<string>();
                if (aZeile != null)
                    gedroppteDateien.Add(aZeile.VM.aPlayTitel.Audio_Titel.Pfad + '\\' + aZeile.VM.aPlayTitel.Audio_Titel.Datei);
                else
                    gedroppteDateien.Add(aZeileVM.aPlayTitel.Audio_Titel.Pfad + '\\' + aZeileVM.aPlayTitel.Audio_Titel.Datei);

                if (kopieren)
                    VM._DateienAufnehmen(gedroppteDateien, aZeile, aZeileVM, ZielPlaylist, VM.audioZeileMouseOverDropped - 1, true);
                else
                {
                    if (ZielPlaylist == VM.AktKlangPlaylist)   // Verschieben innerhalb der Playliste
                    {
                        int quelle = VM.FilteredLbEditorAudioZeilenListe.IndexOf(aZeileVM);
                        int ziel = VM.audioZeileMouseOverDropped;
                        VM.MoveAudioZeileItem(aZeile != null ? aZeile.Tag as Audio_Playlist_Titel : aZeileVM.aPlayTitel, ziel - quelle);
                        VM.FilteredLbEditorAudioZeilenListe = VM.FilteredLbEditorAudioZeilenListe.OrderBy(t => t.aPlayTitel.Reihenfolge).ToList();
                    }

                    if (!kopieren &&
                        ZielPlaylist != VM.AktKlangPlaylist)   //Verschieben = Löschen in akt. Playliste
                    {
                        VM._DateienAufnehmen(gedroppteDateien, aZeile, aZeileVM, ZielPlaylist, VM.audioZeileMouseOverDropped - 1, true);
                        Audio_Titel aTitel =
                            aZeile != null ?
                            Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Audio_TitelGUID == ((Audio_Playlist_Titel)aZeile.Tag).Audio_Titel.Audio_TitelGUID) :
                            Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Audio_TitelGUID == aZeileVM.aPlayTitel.Audio_TitelGUID);
                        Global.ContextAudio.RemoveTitelFromPlaylist(VM.AktKlangPlaylist, aTitel);

                        if (aZeileVM != null) VM.LbEditorAudioZeilenListe.Remove(aZeileVM);
                        lbEditorItemVM lbi = VM.SelectedEditorItem;
                        VM.SelectedEditorItem = null;
                        VM.SelectedEditorItem = lbi;
                        VM.LbEditorAudioZeilenListe = VM.LbEditorAudioZeilenListe;
                        VM.FilterEditorPlaylistListe();
                    }
                }

                if (VM != null)
                {
                    VM.audioZeileMouseOverDropped = 0;
                    VM._dndZeilenCursor = null;
                }
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
            _dragStartPoint = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed)
                VM.pointerPlaylistDragDrop = e.GetPosition(null);

            if (VM.rbEditorEditPlaylist) return;
            if (e.LeftButton == MouseButtonState.Pressed)
                VM.DnDZielObject = e.GetPosition(null);
        }

        private void lbiEditorTheme_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed)
                VM.pointerPlaylistDragDrop = e.GetPosition(null);
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
                if ((e.RemovedItems.Count > 0  &&  e.RemovedItems[0] != e.AddedItems[0]) ||
                    (VM.SelectedEditorItem != null && VM.SelectedEditorItem == e.AddedItems[0]))
                    (sender as ListBox).Focus();
            }
            if (tbtnKlangPause1.IsChecked.Value)
            {
                tbtnKlangPause1.IsChecked = false;
                tbtnKlangPause1.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
            }
        }

        private void lbErwPlayerMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 &&
               (lbMusik.SelectedItem == null || lbMusik.SelectedIndex != lbErwPlayerMusik.SelectedIndex))
                lbMusik.SelectedIndex = VM.FilteredMusikPlaylistItemListe.FindIndex(t =>
                    t.VM.aPlaylist.Audio_PlaylistGUID == VM.SelectedMusikPlaylistItem.VM.aPlaylist.Audio_PlaylistGUID);            
        }

        
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
            if (BGPlayer.AktPlaylistTitel != null)
                Global.ContextAudio.Update<Audio_Playlist_Titel>(BGPlayer.AktPlaylistTitel);
        }

        private void grdEditorX_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Drag-Mode über den Editor ist ein Fehler aufgetreten.", ex);
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

        private void tcAudioPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tiMusik.IsSelected && lbMusik.SelectedItem != null)
            {
                int i = lbMusik.SelectedIndex;
                if (i != -1) lbMusik.ScrollIntoView(lbMusik.Items[i]);
            }
            if (tiErwPlayer.IsSelected && lbErwPlayerMusik.SelectedItem != null)
            {
                int i = lbErwPlayerMusik.SelectedIndex;
                if (i != -1) lbErwPlayerMusik.ScrollIntoView(lbErwPlayerMusik.Items[i]);
            }            
        }

        private void MusikTitelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = lbMusikTitelList.SelectedIndex;
            if (i != -1) lbMusikTitelList.ScrollIntoView(lbMusikTitelList.Items[i]);
        }

        private void btnAudioSpeedButtonWesenZuweisen_Click(object sender, RoutedEventArgs e)
        {
            VM.CurrentPlaylist =
                (VM.AktKlangPlaylist != null && !VM.AktKlangPlaylist.Hintergrundmusik) ? VM.AktKlangPlaylist :
                (VM.AktKlangPlaylist == null || VM.AktKlangPlaylist.Hintergrundmusik) ? Global.ContextAudio.PlaylistListe.FirstOrDefault(t => !t.Hintergrundmusik) : null;
            PlaylistWesenAuswahlView wesenAuswahlView = new PlaylistWesenAuswahlView(VM.CurrentPlaylist);
            wesenAuswahlView.ShowDialog();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (VM.SelectedEditorItem != null)
                VM.SelectedEditorItem.Name = ((TextBox)e.OriginalSource).Text;
        }

        #region -- Key to Char ---
        // ReSharper disable InconsistentNaming
        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }
        // ReSharper restore InconsistentNaming

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)] 
        StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        public static char GetCharFromKey(Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            var keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            var stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
                default:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
            }
            return ch;
        }


        #endregion

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            char[] invalidFileKonstante = new char[] {'\\'};//,'/',':','*','?','"','<','>','|'};

            if (invalidFileKonstante.Contains(GetCharFromKey(e.Key)))
            {
                e.Handled = true;
            }
        }
    }
}


