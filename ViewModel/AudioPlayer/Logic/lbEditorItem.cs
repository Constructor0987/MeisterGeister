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
    public class lbEditorItem : Base.ViewModelBase
    {
        //INotifyPropertyChanged
        #region //---- FELDER ----
        public ListBox ParentIs;
        public AudioPlayerViewModel PlayerVM = null;
        
        //private BitmapImage _listBoxItemIcon;
        private lbEditorItem _item = null;
        private string _suchtext = string.Empty;

        private bool _changed = false;
        private double _totalTimePlylist = 0;
        private double _vol_PlaylistMod = 0;
        private DateTime _lastVolUpdate = DateTime.Now;
        private uint _sollBtnGedrueckt = 0;
        private Audio_Theme _aTheme = null;
        private Audio_Playlist _aPlaylist = null;
        private List<Audio_Playlist_Titel> _aPlaylistTitel;
        private int _objGruppe;
        private UInt16 _anzVolChange = 0;
        private UInt16 _anzPauseChange;
        private string _playlistName = "";
        private bool _istMusik = true;
        private List<AudioPlayerViewModel.KlangZeile> _listZeile = new List<AudioPlayerViewModel.KlangZeile>();
        private bool _wirdAbgespielt = false;
        private List<Guid> _nochZuSpielen = new List<Guid>();
        private List<UInt16> _gespielt = new List<UInt16>();
        private Nullable<double> _force_Volume = null;

        private bool _visuell = true;
        public DispatcherTimer wartezeitTimer = new DispatcherTimer();


        //Commands

        #endregion

        #region //---- EIGENSCHAFTEN ----

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
        
        public lbEditorItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                _suchtext = Name.ToLower() + (Kategorie != null ? Kategorie.ToLower() : "");
                OnChanged();
            }
        }
        
        public bool Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }

        public double TotalTimePlylist
        {
            get { return _totalTimePlylist; }
            set { _totalTimePlylist = value; }
        }

        public double Vol_PlaylistMod
        {
            get { return _vol_PlaylistMod; }
            set { _vol_PlaylistMod = value; }
        }

        public DateTime LastVolUpdate
        {
            get { return _lastVolUpdate; }
            set { _lastVolUpdate = value; }
        }

        public uint SollBtnGedrueckt
        {
            get { return _sollBtnGedrueckt; }
            set { _sollBtnGedrueckt = value; }
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

        public List<Audio_Playlist_Titel> APlaylistTitel
        {
            get { return _aPlaylistTitel; }
            set { _aPlaylistTitel = value; }
        }

        public int ObjGruppe
        {
            get { return _objGruppe; }
            set { _objGruppe = value; }
        }

        public UInt16 AnzVolChange
        {
            get { return _anzVolChange; }
            set { _anzVolChange = value; }
        }

        public UInt16 AnzPauseChange
        {
            get { return _anzPauseChange; }
            set { _anzPauseChange = value; }
        }

        public string PPlaylistName
        {
            get { return _playlistName; }
            set { _playlistName = value; }
        }

        public bool IstMusik
        {
            get { return _istMusik; }
            set { _istMusik = value; }
        }

        public List<AudioPlayerViewModel.KlangZeile> ListZeile
        {
            get { return _listZeile; }
            set { _listZeile = value; }
        }

        public bool WirdAbgespielt
        {
            get { return _wirdAbgespielt; }
            set { _wirdAbgespielt = value; }
        }

        public List<Guid> NochZuSpielen
        {
            get { return _nochZuSpielen; }
            set { _nochZuSpielen = value; }
        }

        public List<UInt16> Gespielt
        {
            get { return _gespielt; }
            set { _gespielt = value; }
        }

        public Nullable<double> Force_Volume
        {
            get { return _force_Volume; }
            set { _force_Volume = value; }
        }

        public bool visuell
        {
            get { return _visuell; }
            set { _visuell = value; }
        }

        public Type ItemType
        {
            get { return APlaylist == null ? null : APlaylist.GetType(); }
        }

        public string Name
        {
            get 
            {
                if (ATheme == null)
                    return APlaylist == null || APlaylist.Name == null ? string.Empty : APlaylist.Name;
                else
                    return ATheme.Name;
            }
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

        //Commands
        private Base.CommandBase _onlbEditorItemAdd;
        public Base.CommandBase OnlbEditorAdd
        {
            get { return _onlbEditorItemAdd; }
        }


        #endregion


        #region //---- KONSTRUKTOR ----

        public lbEditorItem()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            _onlbEditorItemAdd = new Base.CommandBase(lbEditorItemAdd, null);
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
                if (datei != null)
                {
                    Global.SetIsBusy(true, string.Format("Die Playlist wird exportiert ..."));

                    datei = datei.Replace("--", "-");
                    while (datei.EndsWith("-.xml") || datei.EndsWith(" .xml"))
                        datei = datei.Substring(0, datei.Length - 5) + ".xml";

                    File.Delete(datei);
                    this.APlaylist.Export(datei, this.APlaylist.Audio_PlaylistGUID);

                    Global.SetIsBusy(false);
                    ViewHelper.Popup("Die Playlist-Daten wurden erfolgreich gesichert.");
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren der Playlist ist ein Fehler aufgetreten.", ex);
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
                    Global.SetIsBusy(true, string.Format("Playlist '" + APlaylist.Name + "' wird gelöscht..."));
                    List<lbEditorItem> lbPlaylist = PlayerVM.EditorListBoxItemListe;
                    List<lbEditorItem> lbFilteredPlaylist = PlayerVM.FilteredEditorListBoxItemListe;

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

                    PlayerVM.EditorListBoxItemListe = PlayerVM.lbiPlaylistListNeuErstellen();// lbPlaylist;
                    PlayerVM.FilterEditorPlaylistTitelListe();
                    PlayerVM.MusikListItemListe = PlayerVM.mZeileEditorMusikNeuErstellen();
                    //PlayerVM.FilteredEditorListBoxItemListe = lbFilteredPlaylist;
                    if (PlayerVM.AktKlangPlaylist == null && PlayerVM.FilteredEditorListBoxItemListe.Count > 0)
                        PlayerVM.SelectedEditorItem = PlayerVM.FilteredEditorListBoxItemListe[0];
                    ViewHelper.Popup("Die Playlist wurde erfolgreich gelöscht.");
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

                        List<lbEditorItem> lbTheme = PlayerVM.EditorThemeListBoxItemListe;
                        List<lbEditorItem> lbFilteredTheme = PlayerVM.FilteredEditorThemeListBoxItemListe;

                        if (PlayerVM.SelectedEditorThemeItem == lbFilteredTheme.First(t => t.ATheme == ATheme))
                            PlayerVM.SelectedEditorThemeItem = null;
                        lbFilteredTheme.Remove(lbFilteredTheme.First(t => t.ATheme == ATheme));
                        lbTheme.Remove(lbTheme.First(t => t.ATheme == ATheme));

                        if (!Global.ContextAudio.Delete<Audio_Theme>(ATheme))
                            Global.ContextAudio.ThemeListe.Remove(ATheme);

                        PlayerVM.EditorThemeListBoxItemListe = PlayerVM.lbiThemeListNeuErstellen();
                        PlayerVM.FilterThemeEditorPlaylistListe();                      
                        PlayerVM.FilterErwPlayerThemeListe();
                        if (PlayerVM.AktKlangTheme == null && PlayerVM.FilteredEditorThemeListBoxItemListe.Count > 0)
                            PlayerVM.SelectedEditorThemeItem = PlayerVM.FilteredEditorThemeListBoxItemListe[0];
                        
                        Global.SetIsBusy(false);
                        ViewHelper.Popup("Das Theme wurde erfolgreich gelöscht.");
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

        /*
        private void InventarAdd(object sender)
        {
            if (InventarAddEvent != null)
                InventarAddEvent(this, new EventArgs());
        }

        public event EventHandler InventarAddEvent;

        #endregion

        #region //---- INotifyPropertyChanged Member ----

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        */

        public event EventHandler lbEditorItemAddEvent;
        private void lbEditorItemAdd(object sender)
        {
            if (lbEditorItemAddEvent != null)
                lbEditorItemAddEvent(this, new EventArgs());
        }


        #endregion



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

    }

}
