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

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class grdThemeBtnVM : Base.ToolViewModelBase
    {
        //INotifyPropertyChanged
        #region //---- FELDER ----

        private Audio_Theme _theme = null;
        public Audio_Theme Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                OnChanged();
            }
        }

        private bool _forceVolumeVis = true;
        public bool ForceVolumeVis
        {
            get { return _forceVolumeVis; }
            set
            {
                _forceVolumeVis = value;
                OnChanged();
            }
        }

        private bool _großeAnsicht = true;
        public bool GroßeAnsicht
        {
            get { return _großeAnsicht; }
            set
            {
                _großeAnsicht = value;
                OnChanged();
                ForceVolumeVis = GroßeAnsicht;
            }
        }


        //Commands
        //private Base.CommandBase _onlbEditorItemAdd;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        
        //Commands
        
        #endregion


        #region //---- KONSTRUKTOR ----

        public grdThemeBtnVM()
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        [DependentProperty("Theme")]
        public string ThemeTooltip
        {
            get
            {
                Audio_Playlist aPListHintergrund = Theme.Audio_Playlist.FirstOrDefault(t => t.Hintergrundmusik);
                string ttip = aPListHintergrund != null ? "Hintergrund-Musik:   " + aPListHintergrund.Name + Environment.NewLine : "";
                Int16 i = 1;
                List<Audio_Playlist> aPListGeräusche = Theme.Audio_Playlist.Where(t => !t.Hintergrundmusik).ToList();
                foreach (Audio_Playlist aPList in aPListGeräusche.OrderBy(t => t.Name))
                {
                    ttip += "Geräusch " + i + ":   " + aPList.Name + Environment.NewLine;
                    i++;
                }
                i = 1;
                foreach (Audio_Theme aUnterTheme in Theme.Audio_Theme1.
                    Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
                {
                    ttip += "Sub-Theme " + i + ":   " + aUnterTheme.Name + Environment.NewLine;
                    i++;
                }
                return ttip;        
            }
        }


        //Commands
        

        /*
        private void tbtnTheme_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Audio_Theme aTheme = Global.ContextAudio.LoadThemesByGUID((Guid)((ToggleButton)sender).Tag);

                ((ToggleButton)sender).FontWeight = FontWeights.Bold;
                if (!aTheme.NurGeräusche)
                {
                    foreach (grdThemeButton grdTbtn in wpnlPListThemes.Children)
                    {
                        if (grdTbtn.tbtnTheme.IsChecked.Value && grdTbtn.tbtnTheme != ((ToggleButton)sender) &&
                            !grdTbtn.chkbxPlus.IsChecked.Value)// grdTbtn.chkbxPlus.IsChecked.Value)
                        {
                            grdTbtn.tbtnTheme.IsChecked = false;
                            (wpnlPListThemes.Tag as List<Guid>).Remove((Guid)grdTbtn.Tag);
                        }
                    }

                    bool foundHintergrund = false;
                    foreach (Audio_Playlist aPlaylist in aTheme.Audio_Playlist)
                    {
                        if (aPlaylist.Hintergrundmusik)
                        {
                            foundHintergrund = true;
                            foreach (MusikZeile mZeile in lbPListMusik.Items)
                            {
                                if ((Guid)mZeile.Tag == aPlaylist.Audio_PlaylistGUID)
                                {
                                    mZeile.IsSelected = true;
                                    lbPListMusik.ScrollIntoView(mZeile);
                                    break;
                                }
                            }
                        }
                    }
                    if (!foundHintergrund)
                    {
                        foreach (MusikZeile mZeile in lbPListMusik.Items)
                        {
                            if (mZeile.IsSelected)
                            {
                                mZeile.IsSelected = false;
                                break;
                            }
                        }
                    }
                }

                foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                {
                    if (aTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                        mZeile.tbtnCheck.IsChecked = true;
                }

                //Auswählen der Geräusche-Playlisst der untergeorgenten Themes
                CheckUnterThemes(aTheme);

                (wpnlPListThemes.Tag as List<Guid>).Add((Guid)((ToggleButton)sender).Tag);

                FilterGeräuscheAktiv();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswählen des Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        private void tbtnTheme_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Audio_Theme aTheme = Global.ContextAudio.LoadThemesByGUID((Guid)((ToggleButton)sender).Tag);

                ((ToggleButton)sender).FontWeight = FontWeights.Normal;
                if (((List<Guid>)wpnlPListThemes.Tag).Count != 0)
                {
                    if (!aTheme.NurGeräusche)
                        foreach (MusikZeile aZeile in lbPListMusik.Items) aZeile.IsSelected = false;

                    foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                    {
                        if (aTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                            mZeile.tbtnCheck.IsChecked = false;
                    }
                    if (!aTheme.NurGeräusche)
                        (wpnlPListThemes.Tag as List<Guid>).Remove(aTheme.Audio_ThemeGUID);
                }
                FilterGeräuscheAktiv();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Abwählen des Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        private void CheckUnterThemes(Audio_Theme aTheme)
        {
            foreach (Audio_Theme aUnterTheme in aTheme.Audio_Theme1.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")))
            {
                foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                {
                    if (aUnterTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                        mZeile.tbtnCheck.IsChecked = true;
                }
                if (aUnterTheme.Audio_Theme1.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).ToList().Count > 0)
                    CheckUnterThemes(aUnterTheme);
            }
        }


        private void FilterGeräuscheAktiv()
        {
            tbPListGeräuscheName.Text = "";
            btnPListAktFilter.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            bool found = false;
            foreach (MusikZeile mZeile in lbPListGeräusche.Items)
            {
                if (mZeile.tbtnCheck.IsChecked.Value)
                {
                    found = true;
                    break;
                }
            }
            if (found)
                btnThemeGeräuscheFilterAktiv.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
*/
          
        #endregion


    }

}
