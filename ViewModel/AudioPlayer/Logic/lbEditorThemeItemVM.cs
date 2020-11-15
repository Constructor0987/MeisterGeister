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
using System.Xml;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class lbEditorThemeItemVM : Base.ToolViewModelBase
    {
        #region //---- FELDER ----
        
        public AudioPlayerViewModel PlayerVM = null;        
        private lbEditorThemeItem _item = null;
        private string _suchtext = string.Empty;
        private Audio_Theme _aTheme = null;
        private Audio_Playlist _aPlaylist = null;


        #endregion

        #region //---- EIGENSCHAFTEN ----

        [DependentProperty("Reihenfolge")]
        public bool IstErsteZeile
        {
            get
            {
                return PlayerVM.FilteredEditorListBoxItemListe.Count > 0 ?
                  PlayerVM.FilteredEditorListBoxItemListe.OrderBy(t => t.APlaylist.Reihenfolge).First().APlaylist == APlaylist :
                  true;
            }
            set { OnChanged(); }
        }

        [DependentProperty("Reihenfolge")]
        public bool IstLetzteZeile
        {
            get
            {
                return (APlaylist == null || PlayerVM.FilteredEditorListBoxItemListe.Count < 1) ?
                    true :
                    PlayerVM.FilteredEditorListBoxItemListe.OrderBy(t => t.APlaylist.Reihenfolge).Last().APlaylist == APlaylist;
            }
            set { OnChanged(); }
        }

        [DependentProperty("APlaylist"), DependentProperty("AktKlangPlaylist")]
        public string ListBoxItemIconBild
        {
            get
            {
                if (APlaylist != null)
                    return (APlaylist.Hintergrundmusik ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png" :
                                                         "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png");
                else
                    return "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/copy.png";
            }
        }

        public lbEditorThemeItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                _suchtext = (this.ATheme != null? ATheme.Name.ToLower(): this.APlaylist.Name.ToLower()) + (Kategorie != null ? Kategorie.ToLower() : "");
                OnChanged();
            }
        }

        public bool PlaylistAZ
        {
            get { return PlayerVM.PlaylistAZ; }            
        }

        private bool _mouseOnSubObject = false;
        public bool MouseOnSubObject
        {
            get { return _mouseOnSubObject; }
            set { _mouseOnSubObject = value; }
        }

        public Audio_Playlist APlaylist
        {
            get { return _aPlaylist; }
            set { _aPlaylist = value; }
        }

        public Audio_Theme ATheme
        {
            get { return _aTheme; }
            set { _aTheme = value; }
        }
        
        public Type ItemType
        {
            get { return APlaylist == null ? null : APlaylist.GetType(); }
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public string Kategorie
        {
            get
            {
                if (ATheme == null)
                    return APlaylist == null || APlaylist.Kategorie == null ? string.Empty : APlaylist.Kategorie;
                else
                    return ATheme.Kategorie;
            }
        }
        
        #endregion
        
        #region //---- KONSTRUKTOR ----

        public lbEditorThemeItemVM()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            _onlbEditorThemeItemAdd = new Base.CommandBase(lbEditorThemeItemAdd, null);
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
            return _suchtext.Contains(suchWort);
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

        public event EventHandler lbEditorThemeItemAddEvent;
        private void lbEditorThemeItemAdd(object sender)
        {
            if (lbEditorThemeItemAddEvent != null)
                lbEditorThemeItemAddEvent(this, new EventArgs());
        }

        #endregion

        #region //---- COMMANDS ----

        private Base.CommandBase _onlbEditorThemeItemAdd;
        public Base.CommandBase OnlbEditorThemeAdd
        {
            get { return _onlbEditorThemeItemAdd; }
        }


        private Base.CommandBase _onBtnExportLbEditor = null;
        public Base.CommandBase OnBtnExportLbEditor
        {
            get
            {
                if (_onBtnExportLbEditor == null)
                    _onBtnExportLbEditor = new Base.CommandBase(BtnExportLbEditor, null);
                return _onBtnExportLbEditor;
            }
        }
        void BtnExportLbEditor(object obj)
        {
            try
            {
                string datei = ViewHelper.ChooseFile("Playliste exportieren", "Playlist_" + this.APlaylist.Name.Replace("/", "_") + ".xml", true, "xml");
                if (string.IsNullOrEmpty(datei)) return;

                Global.SetIsBusy(true, string.Format("Die Playlist wird exportiert ..."));

                datei = datei.Replace("--", "-");
                while (datei.EndsWith("-.xml") || datei.EndsWith(" .xml"))
                    datei = datei.Substring(0, datei.Length - 5) + ".xml";

                File.Delete(datei);
                this.APlaylist.Export(datei, this.APlaylist.Audio_PlaylistGUID);

                Global.SetIsBusy(false);
                ViewHelper.Popup("Die Playlist-Daten wurden erfolgreich gesichert.");
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren der Playlist ist ein Fehler aufgetreten.", ex);
            }
        }


        private Base.CommandBase _onReihenfolgePListMoveUp;
        public Base.CommandBase OnReihenfolgePListMoveUp
        {
            get
            {
                if (_onReihenfolgePListMoveUp == null)
                    _onReihenfolgePListMoveUp = new Base.CommandBase(ReihenfolgePListMoveUp, null);
                return _onReihenfolgePListMoveUp;
            }
        }
        void ReihenfolgePListMoveUp(object obj)
        {
            try
            {
                if (PlayerVM.EditorListBoxItemListe.Count > 1 &&
                    PlayerVM.EditorListBoxItemListe.Count(t => t.APlaylist.Reihenfolge == 0) > 1)
                    PlayerVM.sortPlaylist(PlayerVM.EditorListBoxItemListe, -1);

                if (APlaylist.Reihenfolge > 0)
                    PlayerVM.MoveLbEditorItem(APlaylist, -1);

                PlayerVM.FilteredEditorListBoxItemListe = PlayerVM.FilteredEditorListBoxItemListe.OrderBy(t => t.APlaylist.Reihenfolge).ToList();
                MouseOnSubObject = false;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken des Buttons 'btnMoveUp' ist ein Fehler aufgetreten", ex);
            }
        }

        private Base.CommandBase _onBtnLöschenLbEditor = null;
        public Base.CommandBase OnBtnLöschenLbEditor
        {
            get
            {
                if (_onBtnLöschenLbEditor == null)
                    _onBtnLöschenLbEditor = new Base.CommandBase(BtnLöschenLbEditor, null);
                return _onBtnLöschenLbEditor;
            }
        }
        void BtnLöschenLbEditor(object obj)
        {
            try
            {
                if (ViewHelper.ConfirmYesNoCancel("Löschen der Playlist", "Wollen Sie wirklich die ausgewählte Playlist  '" + APlaylist.Name + "'  löschen.") == 2)
                {
                    bool aPlaylistWarHintergrund = APlaylist.Hintergrundmusik;
                    Global.SetIsBusy(true, string.Format("Playlist '" + APlaylist.Name + "' wird gelöscht..."));
                    List<lbEditorItemVM> lbPlaylist = PlayerVM.EditorListBoxItemListe;
                    List<lbEditorItemVM> lbFilteredPlaylist = PlayerVM.FilteredEditorListBoxItemListe;

                    if (PlayerVM.AktKlangPlaylist == APlaylist)
                        PlayerVM.AktKlangPlaylist = null;

                    List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(APlaylist);
                    titel.ForEach(delegate(Audio_Titel aTitel)
                    {
                        Global.ContextAudio.RemoveTitelFromPlaylist(APlaylist, aTitel);
                    });

                    if (PlayerVM.SelectedEditorItem == lbFilteredPlaylist.FirstOrDefault(t => t.APlaylist == APlaylist))
                        PlayerVM.SelectedEditorItem = null;
                    lbFilteredPlaylist.Remove(lbFilteredPlaylist.First(t => t.APlaylist == APlaylist));
                    lbPlaylist.Remove(lbPlaylist.First(t => t.APlaylist == APlaylist));

                    Global.ContextAudio.Delete<Audio_Playlist>(APlaylist);
                    Global.SetIsBusy(false);

                    PlayerVM.EditorListBoxItemListe = PlayerVM.lbiPlaylistListNeuErstellen();

                    if (aPlaylistWarHintergrund)
                        PlayerVM.MusikListItemListe = PlayerVM.mZeileErwPlayerMusikNeuErstellen();
                    else
                        PlayerVM.ErwPlayerGeräuscheListItemListe = PlayerVM.mZeileErwPlayerGeräuscheNeuErstellen();
                    
                    if (PlayerVM.AktKlangPlaylist == null && PlayerVM.FilteredEditorListBoxItemListe.Count > 0)
                        PlayerVM.SelectedEditorItem = PlayerVM.FilteredEditorListBoxItemListe[0];
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Löschen der Playlist ist ein Fehler aufgetreten.", ex);
            }
        }
        

        private Base.CommandBase _onBtnLöschenLbEditorTheme = null;
        public Base.CommandBase OnBtnLöschenLbEditorTheme
        {
            get
            {
                if (_onBtnLöschenLbEditorTheme == null)
                    _onBtnLöschenLbEditorTheme = new Base.CommandBase(BtnLöschenLbEditorTheme, null);
                return _onBtnLöschenLbEditorTheme;
            }
        }
        void BtnLöschenLbEditorTheme(object obj)
        {
            try
            {
                if (ATheme != null)
                {
                    if (ViewHelper.ConfirmYesNoCancel("Löschen des Themes", "Wollen Sie wirklich das ausgewählte Theme  '" + ATheme.Name + "'  löschen.") == 2)
                    {
                        Global.SetIsBusy(true, string.Format("Theme '" + ATheme.Name + "' wird gelöscht..."));

                        List<lbEditorThemeItemVM> lbTheme = PlayerVM.EditorThemeListBoxItemListe;
                        List<lbEditorThemeItemVM> lbFilteredTheme = PlayerVM.FilteredEditorThemeListBoxItemListe;

                        if (PlayerVM.SelectedEditorThemeItem == lbFilteredTheme.First(t => t.ATheme == ATheme))
                            PlayerVM.SelectedEditorThemeItem = null;
                        lbFilteredTheme.Remove(lbFilteredTheme.First(t => t.ATheme == ATheme));
                        lbTheme.Remove(lbTheme.First(t => t.ATheme == ATheme));

                        List<grdThemeButton> grdThBtnList = PlayerVM.ErwPlayerThemeListe.FindAll(t => t.VM.Theme.Audio_ThemeGUID != ATheme.Audio_ThemeGUID);
                        PlayerVM.ErwPlayerThemeListe = grdThBtnList;
                        
                        if (!Global.ContextAudio.Delete<Audio_Theme>(ATheme))
                            Global.ContextAudio.ThemeListe.Remove(ATheme);

                        PlayerVM.EditorThemeListBoxItemListe = PlayerVM.lbiThemeListNeuErstellen();
                        //PlayerVM.ErwPlayerThemeListe = PlayerVM.ThemeErwPlayerListeNeuErstellen();
                        PlayerVM.FilterThemeEditorPlaylistListe();                      
                        PlayerVM.FilterErwPlayerThemeListe();
                        if (PlayerVM.AktKlangTheme == null && PlayerVM.FilteredEditorThemeListBoxItemListe.Count > 0)
                            PlayerVM.SelectedEditorThemeItem = PlayerVM.FilteredEditorThemeListBoxItemListe[0];
                        
                        Global.SetIsBusy(false);
                    }
                }
                else
                    ViewHelper.ShowError("Das ausgewählte Theme konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", new Exception());
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Löschen des Themes ist ein Fehler aufgetreten.", ex);
            }
        }


        private Base.CommandBase _onBtnExportLbEditorTheme = null;
        public Base.CommandBase OnBtnExportLbEditorTheme
        {
            get
            {
                if (_onBtnExportLbEditorTheme == null)
                    _onBtnExportLbEditorTheme = new Base.CommandBase(BtnExportLbEditorTheme, null);
                return _onBtnExportLbEditorTheme;
            }
        }
        void BtnExportLbEditorTheme(object obj)
        {
            try
            {                
                if (ATheme != null)
                {
                    string pfaddatei = ViewHelper.ChooseFile("Theme exportieren", "Theme_" + ATheme.Name.Replace("/", "_") + ".xml", true, "xml");
                    if (string.IsNullOrEmpty(pfaddatei)) return;

                    pfaddatei = validateString(pfaddatei);
                    ExportTheme(ATheme, pfaddatei);
                    ViewHelper.Popup("Die Themeliste wurde erfolgreich gesichert." + Environment.NewLine + Environment.NewLine +
                        "!!! Bitte beachten Sie, dass Die PLAYLISTEN SEPARAT gesichert werden müssen !!!");
                    Global.SetIsBusy(false);
                }
                else
                    ViewHelper.ShowError("Das ausgewählte Theme konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", new Exception());
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren des Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        #endregion
        
        #region //---- Kalkulationen & Export ----
        
        private string validateString(string s)
        {
            while (s.Contains("--"))
                s = s.Replace("--", "-");

            s = s.TrimEnd(new char[] { ' ', '-', '/' });
            s = s.TrimStart(new char[] { ' ', '-', '/' });

            while (s.EndsWith("-.xml") || s.EndsWith(" .xml"))
                s = s.Substring(0, s.Length - 5) + ".xml";

            return s;
        }

        private void ExportTheme(Audio_Theme aTheme, string pfaddatei)
        {
            if (aTheme != null)
            {
                if (pfaddatei != null)
                {
                    Global.SetIsBusy(true, string.Format("Das Theme '" + aTheme.Name + "'  wird exportiert ..."));

                    File.Delete(pfaddatei);
                    XmlTextWriter textWriter = new XmlTextWriter(pfaddatei, null);
                    textWriter.WriteStartDocument();

                    textWriter.WriteComment("Theme-Export vom " + DateTime.Now.ToShortDateString());
                    textWriter.WriteComment("Theme-Name: " + aTheme.Name);

                    int i = 1;
                    textWriter.WriteStartElement("Themename", aTheme.Name);

                    textWriter.WriteStartAttribute("IstNurGeräuschTheme");
                    textWriter.WriteValue(aTheme.NurGeräusche);
                    textWriter.WriteEndAttribute();                    
                    textWriter.WriteStartAttribute("HUEScene");
                    textWriter.WriteValue(aTheme.HUE_Scene);
                    textWriter.WriteEndAttribute();

                    foreach (Audio_Playlist aPlaylist in aTheme.Audio_Playlist)
                    {
                        textWriter.WriteStartElement("Playlist" + i);
                        textWriter.WriteStartAttribute("Name");
                        textWriter.WriteValue(aPlaylist.Name);
                        textWriter.WriteEndAttribute();

                        textWriter.WriteStartAttribute("Audio_PlaylistGUID");
                        textWriter.WriteValue(aPlaylist.Audio_PlaylistGUID.ToString());
                        textWriter.WriteEndAttribute();
                        textWriter.WriteEndElement();
                        i++;
                    }
                    int t_pos = 1;
                    foreach (Audio_Theme aUTheme in aTheme.Audio_Theme1)
                    {
                        textWriter.WriteStartElement("Theme" + t_pos);
                        textWriter.WriteStartAttribute("Name");
                        textWriter.WriteValue(aUTheme.Name);
                        textWriter.WriteEndAttribute();

                        textWriter.WriteStartAttribute("Audio_ThemeGUID");
                        textWriter.WriteValue(aUTheme.Audio_ThemeGUID.ToString());
                        textWriter.WriteEndAttribute();
                        textWriter.WriteEndElement();
                        t_pos++;
                    }
                    textWriter.WriteEndDocument();
                    textWriter.Close();
                }
            }
        }
        #endregion
    }

}
