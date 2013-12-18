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
using System.Globalization;
using System.Collections.ObjectModel;
// Eigene Usings
using MeisterGeister.Logic.Settings;
using MeisterGeister.Logic.General;
using MeisterGeister.View.General;
using MeisterGeister.View.Windows;
using Global = MeisterGeister.Global;
using MeisterGeister.Model;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.ViewModel.AudioPlayer;

using MeisterGeister.ViewModel.Base;



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

public class KlangZeile
{
	public UInt16 ID_Zeile;
	public MediaPlayer _mplayer = null; 
	public Audio_Playlist_Titel audiotitel = new Audio_Playlist_Titel();
    public int mediaHashCode = 0;
	public bool FadingOutStarted = false;
	public bool istPause = false;
	public bool istLaufend = false;
    public bool istWartezeit = false;
	public bool istStandby = false;
	public bool playable = true;
	public int pauseMax_wert = 9000;
	public int pauseMin_wert = 0;
	public int volMax_wert = 100;
	public int volMin_wert = 0;
	public int volZiel = 50;
	public int Vol_jump = 1;            // Volumesprung bei variabler Lautstärkenänderung
	public double Aktuell_Volume = 50;
	public double playspeed = 1;
	public UInt16 UpdateZyklusVol = 3;  //Sekunden neuen Zielwert ermitteln
	public DateTime dtLiedLastCheck = DateTime.MinValue;

	public AudioZeile audioZeile = null;    	   
	public KlangZeile(UInt16 id)
	{
		ID_Zeile = id;
	}
}

public class GruppenObjekt
{
    public bool changed = false;
	public double totalTimePlylist = 0;
	public double Vol_ThemeMod = 100;        // Multiplikator(/100) auf den Aktuell_Volume (werte von 0 bis 200)
	public double Vol_PlaylistMod = 100;
	public DateTime LastVolUpdate = DateTime.Now;
	//public int seite;
	public uint sollBtnGedrueckt = 0;
	//public Guid Audio_Playlist_GUID;
	public Audio_Playlist aPlaylist = null;
	public List<Audio_Playlist_Titel> aPlaylistTitel;
	public int objGruppe;
	public UInt16 anzVolChange = 0;
	public UInt16 anzPauseChange = 0;
	public string playlistName = "";
	public bool istMusik = true;
	public List<KlangZeile> _listZeile = new List<KlangZeile>();
	public bool wirdAbgespielt = false;
	public List<Guid> NochZuSpielen = new List<Guid>();
    public List<UInt16> Gespielt = new List<UInt16>();
    public Nullable<double> force_Volume = null;

    public bool visuell = true;
	public TabItem tiEditor = null;
	public TCButtons ticKlang = null;
	public ScrollViewer sviewer;
	public Grid grdEditor = null;
	public Grid grdEditorTop = null;
	public ToggleButton tbtnKlangPause = null;
	public WrapPanel wpnl = null;

	public TextBox tbTopFilter = null;
	public Button btnTopFilter = null;
	public TextBox tbTopKlangKategorie = null;
	public Border brdTopKlangKategorie = null;

	public GroupBox gboxTopSongsParallel = null;
	public TextBox tbTopKlangSongsParallel = null;
	public Button btnTopSongParPlus = null;
	public Button btnTopSongParMinus = null;

	public GroupBox gboxTopTyp = null;
	public RadioButton rbTopIstMusikPlaylist = null;
	public RadioButton rbTopIstKlangPlaylist = null;

	public CheckBox chkbxTopAktiv = null;
	public StackPanel spnlTopGeräuschIcon = null;
	public Button btnTopVolMin = null;
	public Button btnTopVolDown = null;
	public Button btnTopVolUp = null;
	public Button btnTopVolMax = null;
	public CheckBox chkbxTopVolChange = null;
	public Button btnTopPauseMin = null;
	public Button btnTopPauseDown = null;
	public Button btnTopPauseUp = null;
	public Button btnTopPauseMax = null;
	public CheckBox chkbxTopPauseChange = null;
	public Button btnTopVolMinMinus = null;
	public Button btnTopVolMinPlus = null;
	public Button btnTopVolMaxMinus = null;
	public Button btnTopVolMaxPlus = null;
	public Border brdTrennstrich = null;
	public Button btnTopPauseMinMinus = null;
	public Button btnTopPauseMinPlus = null;
	public Button btnTopPauseMaxMinus = null;
	public Button btnTopPauseMaxPlus = null;

	public Button btnTopHotkeySet = null;
	public StackPanel spnlTopHotkey = null;
	public ComboBox cmboxTopHotkey = null;
	public Button btnHotkeyEntfernen = null;

	public Button btnTopKlangOpen = null;
	public Button btnKlangUpdateFiles = null;
    public Button btnTopChPfad = null;
}

public class Musik
{
    public bool FadingOutStarted = false;
	public bool isPaused = false;
    public Audio_Playlist aPlaylist = null;
	public MediaPlayer mPlayer = null;
}

public class MusikView
{
	public List<Musik> BG = new List<Musik>();
	public string[] s = new string[2];
	public int aktiv = 0;
	public double totalLength;
	public List<Guid> NochZuSpielen = new List<Guid>();
	public List<Guid> Gespielt = new List<Guid>();
	public Audio_Playlist AktPlaylist;
	public Audio_Playlist_Titel AktPlaylistTitel;
	public List<Audio_Titel> AktTitel = new List<Audio_Titel>();
}

public class hotkey
{
	public int taste = -1;
	public Guid aPlaylistGuid = Guid.Empty;
	public MediaPlayer mp = new MediaPlayer();	
}

public class chkeckAnzDateien
{
    public BackgroundWorker _bkworker = new BackgroundWorker();
    public GruppenObjekt grpobj = null;
    public Audio_Playlist aPlaylist = null;
    public List<Audio_Titel> titelliste;
    public string titelRef = null;
    public string[] allFilesMP3;
    public string[] allFilesWAV;
    public string[] allFilesOGG;
    public string[] allFilesWMA;
    public int allFiles;
    public int gefundenFilesMP3;
    public int gefundenFilesWAV;
    public int gefundenFilesOGG;
    public int gefundenFilesWMA;
}

namespace MeisterGeister.View.AudioPlayer {
	/// <summary>
	/// Interaktionslogik für AudioPlayerView.xaml
	/// </summary>
	/// 

    public partial class AudioPlayerView : UserControl
    {
        chkeckAnzDateien _chkAnzDateien = new chkeckAnzDateien();
        //int[] _anzAllFilesInDir;// = int[4](0,0,0,0);   //MP3 , WAV , OGG , WMA
        
        private double Zeitüberlauf = 1000;   // in ms
        public string stdPfad = "C:\\";
        private UInt16 tiErstellt = 0;
        private UInt16 rowErstellt = 0;
        private double fadingIntervall = 10;
        public double fadingTime = 600;    // * fadingIntervall = Übergang in ms
        private bool FadingIn_Started = false;
        private bool stopFadingIn = false;
        private bool notPlayHotkey = false;

        private int SliderTeile = 25;
        private Int16 PauseSprung = 200;
        private Int16 VolSprung = 5;
        //MediaPlayer mpFadingInBezug;
       
        private List<hotkey> hotkeys = new List<hotkey>();

        private List<Audio_Playlist_Titel> plyTitelToSave = new List<Audio_Playlist_Titel>();

        private MusikView _BGPlayer = new MusikView();
        private List<GruppenObjekt> _GrpObjecte = new List<GruppenObjekt>();

        private int tcEditor_vorher, tcEditor_vorherTag;    

        private Audio_Playlist AktKlangPlaylist;
        private Audio_Playlist aPlaylistLengthCheck;
        private Audio_Theme AktKlangTheme = null;
        
        System.Timers.Timer BGSongTimer = new System.Timers.Timer();
        DispatcherTimer KlangPlayEndetimer;
        DispatcherTimer plyTitelToSaveTimer = new DispatcherTimer();
        

        DispatcherTimer KlangProgBarTimer = new DispatcherTimer();
        DispatcherTimer MusikProgBarTimer = new DispatcherTimer();
        
        ZeileVM _zeile = new ZeileVM();

        delegate void UpdateUI();

        public AudioPlayerView()
        {
            InitializeComponent();

            _BGPlayer.BG.Add(new Musik());
            _BGPlayer.BG.Add(new Musik());
            
            _rbtnGleichSpielen.IsChecked = Logic.Settings.Einstellungen.AudioDirektAbspielen;
            stdPfad = MeisterGeister.Logic.Settings.Einstellungen.AudioVerzeichnis;
            _tbStdPfad.Text = stdPfad;
            fadingTime = MeisterGeister.Logic.Settings.Einstellungen.Fading;

            DataContext = _zeile;

            AktualisiereHotKeys();
            plyTitelToSaveTimer.Tick += new EventHandler(plyTitelToSaveTimer_Tick);
            plyTitelToSaveTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            KlangProgBarTimer.Tick += new EventHandler(KlangProgBarTimer_Tick);
            KlangProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            KlangProgBarTimer.Tag = 0;

            MusikProgBarTimer.Tick += new EventHandler(MusikProgBarTimer_Tick);
            MusikProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

   /*     public void Dispose()
        {
            KlangProgBarTimer.Stop();
            MusikProgBarTimer.Stop();
            for (int posObjGruppe = 0; posObjGruppe < _GrpObjecte.Count; posObjGruppe++)
            {
                List<KlangZeile> KlangZeilenLaufend = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);
                if (KlangZeilenLaufend.Count != 0)
                    for (int durchlauf = 0; durchlauf < KlangZeilenLaufend.Count; durchlauf++)
                    {
                        if (KlangZeilenLaufend[durchlauf]._mplayer != null)
                        {
                            KlangZeilenLaufend[durchlauf]._mplayer.Stop();
                            KlangZeilenLaufend[durchlauf]._mplayer.Close();
                        }
                    }
            }
            if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
            {
                _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Stop();
                _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Close();
            }
        }*/

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!notPlayHotkey && gbxHotkeys.Visibility == Visibility.Visible)
                {
                    string s = Convert.ToString(e.Key);
                    if (e.Key >= Key.D0 && e.Key <= Key.D9)
                        s = s.Remove(0, s.Length - 1);

                    hotkey hkey = hotkeys.FindAll(t => t.aPlaylistGuid != Guid.Empty).
                        FirstOrDefault(t => Convert.ToChar(t.taste).ToString() == s);
                    if (hkey != null)
                    {
                        foreach (Button btnHotKey in spnlHotkeys.Children.OfType<Button>())
                        {
                            if (((hotkey)btnHotKey.Tag).taste == hkey.taste) 
                            {
                                btnHotKey.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                break;
                            }
                        }
                    }
                }
                if (notPlayHotkey)
                {
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    if (grpobj != null && grpobj.cmboxTopHotkey.Visibility == Visibility.Collapsed)
                        notPlayHotkey = false;
                }
            }
            catch (Exception ex) {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswerten des Tastenklicks ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _tbStdPfad.Text = MeisterGeister.Logic.Settings.Einstellungen.AudioVerzeichnis;
                _rbtnGleichSpielen.IsChecked = MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen;
                _sldFading.Value = MeisterGeister.Logic.Settings.Einstellungen.Fading;
                _sldFading.ToolTip = Math.Round(_sldFading.Value / 100, 1) + " Sekunden In-/Out-Fading";
                tcAudioPlayer.Tag = -1;
                tiEditor_GotFocus(sender, null);
                rbEditorEditPList.IsChecked = true;
                wpnlPListThemes.Tag = Guid.Empty;
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Laden des Fensters ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Dispatcher.HasShutdownStarted)
                {
                    if (_BGPlayer != null)
                    {
                        stopFadingIn = true;
                        if (_BGPlayer.BG[0].mPlayer != null && !_BGPlayer.BG[0].FadingOutStarted)
                        {
                            _BGPlayer.BG[0].FadingOutStarted = true;
                            BGFadingOut(_BGPlayer.BG[0], true, true);
                        }
                        if (_BGPlayer.BG[1].mPlayer != null && !_BGPlayer.BG[1].FadingOutStarted)
                        {
                            _BGPlayer.BG[1].FadingOutStarted = true;
                            BGFadingOut(_BGPlayer.BG[1], true, true);
                        }
                    }

                    for (int i = 0; i < _GrpObjecte.Count; i++)
                        AlleKlangSongsAus(_GrpObjecte[i], true, false);

                    KlangProgBarTimer.Stop();
                    plyTitelToSaveTimer.Stop();
                    MusikProgBarTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Schließen des Fensters ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void plyTitelToSaveTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                plyTitelToSave.ForEach(delegate(Audio_Playlist_Titel plyTitel)
                {
                    Global.ContextAudio.Update<Audio_Playlist_Titel>(plyTitel);
                    plyTitelToSave.Remove(plyTitel);
                });
                if (plyTitelToSave.Count == 0)
                    plyTitelToSaveTimer.Stop();
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Zyklischen Sichern der Playtitel ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }


        private void CloseTab(object source, RoutedEventArgs args)
        {
            TabItem tabItem = (TabItem)args.Source;
            if (tabItem != null)
            {
                TabControl tabControl = (TabControl)tabItem.Parent;
                if (tabControl != null)
                    tabControl.Items.Remove(tabItem);
            }
        }


        public string SongInfo(String musikfile)
        {
            String Filename = "";

            if (musikfile != null)
                Filename = musikfile;
            Filename = Filename + DateTime.Now.ToString();
            return Filename;
        }

        public MediaPlayer PlayFile(bool notMusikPlayer, KlangZeile klZeile, int posObjGruppe, MediaPlayer _player, String url, double vol, bool fading)
        {
            try
            {
                if (_player == null)
                {
                    _player = new MediaPlayer();
                    if (notMusikPlayer)
                    {
                        _player.MediaEnded += Player_Ended;  
                        _player.MediaFailed += Player_KlangMediaFailed; 
                    }
                    else
                    {   
                        _player.MediaFailed += Player_MusikMediaFailed;
                    }
                }

                try
                {
                 //   bool neuAbspielen = false;
                    if (_player.Source == null || _player.Source.LocalPath.ToString() != url)
                    {
                  //      neuAbspielen = true;
                        _player.IsMuted = (notMusikPlayer) ? 
                            (!_GrpObjecte[posObjGruppe].visuell && Convert.ToInt32(btnPListPListSpeaker.Tag) != -1 ? true : false) :
                            (Convert.ToInt16(btnBGSpeaker.Tag) != -1) ? true : false;

                        _player.Volume = 0;
                        if (posObjGruppe != -1)
                            _player.SpeedRatio = klZeile.playspeed;
                        _player.Open(new Uri(url));
                    };

                    if (fading)
                    {
                        if (!notMusikPlayer)
                            _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = false;
                        else
                            klZeile.FadingOutStarted = false;
                        FadingIn(_player, (!notMusikPlayer) ? vol / 100 : (vol * (_GrpObjecte[posObjGruppe].Vol_ThemeMod / 100)) / 100);
                    }
                    else
                    {
                        _player.Volume = (!notMusikPlayer) ? vol / 100 :
                            (_GrpObjecte[posObjGruppe].force_Volume != null) ? _GrpObjecte[posObjGruppe].force_Volume.Value :             //Playlist-ForceVolume
                            (vol / 100 * 
                             _GrpObjecte[posObjGruppe].Vol_ThemeMod / 100 *
                             _GrpObjecte[posObjGruppe].Vol_PlaylistMod / 100);

             /*           if (!neuAbspielen)
                            _player.Position = (klZeile.audiotitel.TeilAbspielen) ?
                                TimeSpan.FromMilliseconds(klZeile.audiotitel.TeilStart.Value) :
                                TimeSpan.FromMilliseconds(0);
                        */
                        MyTimer.stop_timer(Convert.ToString(_player.Volume)); //MUSS vorerst drinnnen bleiben, damit das Volume richtig wiedergegben wird (Zeit)?!? 
                        _player.Play();
                    }
                }
                catch (Exception ex2)
                {
                    ListBoxItem lbItem = (ListBoxItem)lbMusiktitellist.SelectedItem;
                    lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));   // Brushes.Yellow
                    lbItem.ToolTip = "Datei konnte nicht geöffnet werden (Datei abspielbar / Codec installiert?)" + ex2;
                    SpieleNeuenMusikTitel(Guid.Empty);
                    return null;
                }

                return _player;
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Audio Fehler", "Der Audio Player hat einen Fehler verursacht.", ex);
                errWin.ShowDialog();
                errWin.Close();
                return null;
            }
        }

        private void CheckPlayStandbySongs(GruppenObjekt grpobj) 
        {
            int titel = -1;
            try
            {
                int laufende = grpobj._listZeile.FindAll(t => t.istLaufend).Count;
                
                List<KlangZeile> klZeilenStandbyNichtPause = grpobj._listZeile.FindAll(t => t.istStandby).FindAll(t => t.istPause == false);
                int standbyNichtPausePlayable = klZeilenStandbyNichtPause.Count;
                
                if ((laufende == 0 && standbyNichtPausePlayable != 0) ||
                   (laufende != 0 && standbyNichtPausePlayable != 0 && grpobj.aPlaylist.MaxSongsParallel > laufende))
                {
                    int neueSongs = (laufende == 0) ? grpobj.aPlaylist.MaxSongsParallel :
                        grpobj.aPlaylist.MaxSongsParallel - laufende;

                    if (neueSongs < 0)
                        neueSongs = 0;

                    if (neueSongs == 0 && grpobj.aPlaylist.MaxSongsParallel == 0)
                        neueSongs = 1;
                    
                    for (int i = 0; i < neueSongs; i++)
                    {
                        if (standbyNichtPausePlayable >= 1)
                        {
                            if (grpobj.NochZuSpielen.Count == 0)
                            {
                                for (int x = 0; x < standbyNichtPausePlayable; x++)
                                {
                                    if (((klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background != null) &&
                                        klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 255, 0)).ToString() &&       // Yellow
                                        klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString()) ||         // Red))     
                                        (klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background == null) &&
                                        klZeilenStandbyNichtPause[x].audiotitel.Aktiv)
                                    {
                                        for (int t = 0; t <= klZeilenStandbyNichtPause[x].audiotitel.Rating; t++)
                                            grpobj.NochZuSpielen.Add(klZeilenStandbyNichtPause[x].audiotitel.Audio_TitelGUID);
                                    }
                                }
                            }

                            if (grpobj.istMusik)
                            {                                
                                if (grpobj.NochZuSpielen.Count > 0)
                                {
                                    int neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                    Guid zuspielendeGuid = grpobj.NochZuSpielen[neuPos];
                                    int posZeile = grpobj._listZeile.FindIndex(t => t.audiotitel.Audio_TitelGUID == zuspielendeGuid);
                                    grpobj._listZeile[posZeile].istStandby = false;
                    
                                    // Titel anstarten
                                    if (!grpobj.visuell)
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].audiotitel.Aktiv, grpobj._listZeile[posZeile], null);
                                    else
                                    {
                                        if (grpobj._listZeile[posZeile].audioZeile.chkTitel != null)
                                        {
                                            chkTitel0_0_Click(grpobj._listZeile[posZeile].audioZeile.chkTitel, new RoutedEventArgs());
                                            grpobj.sviewer.ScrollToVerticalOffset(posZeile * grpobj._listZeile[posZeile].audioZeile.ActualHeight);
                                        }
                                        else
                                        {
                                            grpobj._listZeile[posZeile].istStandby = true;
                                        }                                        
                                    }
                                    standbyNichtPausePlayable--;
                                    if (neuPos < grpobj.NochZuSpielen.Count)
                                    {
                                        klZeilenStandbyNichtPause.Remove(grpobj._listZeile[posZeile]);
                                        grpobj.NochZuSpielen.RemoveAll(t => t.Equals(zuspielendeGuid));
                                    }
                                }
                            }
                            else
                            {
                                if (grpobj.NochZuSpielen.Count > 0 &&
                                    grpobj._listZeile.FindAll(t => t.istStandby).Count > 0)
                                {
                                    int neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);

                                    while (!grpobj._listZeile.First(t => t.audiotitel.Audio_TitelGUID == grpobj.NochZuSpielen[neuPos]).istStandby)
                                        neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                    Guid zuspielendeGuid = grpobj.NochZuSpielen[neuPos];
                                    int posZeile = grpobj._listZeile.FindIndex(t => t.audiotitel.Audio_TitelGUID == zuspielendeGuid);
                                    
                                    grpobj._listZeile[posZeile].istStandby = false;
                                    if (!grpobj.visuell)
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].audiotitel.Aktiv, grpobj._listZeile[posZeile], null);
                                    else
                                    {
                                        // Titel anstarten
                                        if (grpobj._listZeile[posZeile].audioZeile.chkTitel != null)
                                            chkTitel0_0_Click(grpobj._listZeile[posZeile].audioZeile.chkTitel, new RoutedEventArgs());
                                        else
                                        {
                                            grpobj._listZeile[posZeile].istStandby = true;
                                        }
                                    }
                                    standbyNichtPausePlayable--;

                                    //  Die NOCHZUSPIELENDEN Titel werden bei Geräuschen NICHT rausgelöscht!
                                    /*
                                    if (neuPos < grpobj.NochZuSpielen.Count)
                                    {
                                        back = 13;
                                        klZeilenStandbyNichtPause.Remove(grpobj._listZeile[posZeile]);
                                        back = 14;
                                        grpobj.NochZuSpielen.RemoveAll(t => t.Equals(zuspielendeGuid));
                                        back = 15;
                                    }
                                     * */
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Beim Überprüfen der StandyBy-Songs ist eine Fehler aufgetreten: Datenfehler", "titel=" + titel, ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        void KlangPlayEndetimer_Tick(object sender, EventArgs e)
        {
            int posit = -1;
            //int posObjGruppe = -1;
            int zeile = -1;
            int neu = -1;
            double wertPlus = -1;
            int IndexPlus = -1;
            try
            {
                (sender as DispatcherTimer).Stop();
                UInt16 sollID_Zeile = Convert.ToUInt16((sender as DispatcherTimer).Tag);

                //posObjGruppe = -1;
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt _chkGrpObj in _GrpObjecte)
                {
                    if (_chkGrpObj._listZeile.FirstOrDefault(t => t.ID_Zeile == sollID_Zeile) != null)
                    {
                        grpobj = _chkGrpObj;
                        break;
                    }
                }
                if (grpobj != null)
                {
                    zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FirstOrDefault(t => t.ID_Zeile == sollID_Zeile));

                    grpobj._listZeile[zeile].istWartezeit = false;
                    if (grpobj.visuell)
                    {
                        grpobj._listZeile[zeile].audioZeile.pbarTitel.Value = 0;
                        if (grpobj._listZeile[zeile].audiotitel.PauseChange)
                        {
                            if (Convert.ToUInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text) >
                                Convert.ToUInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text))
                                grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text = grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text;

                            neu = (new Random()).Next(Convert.ToUInt16(grpobj._listZeile[zeile].audiotitel.PauseMin),
                                                      Convert.ToUInt16(grpobj._listZeile[zeile].audiotitel.PauseMax));

                            wertPlus = grpobj._listZeile[zeile].audioZeile.sldKlangPause.Ticks.First(t => t >= neu);
                            IndexPlus = grpobj._listZeile[zeile].audioZeile.sldKlangPause.Ticks.IndexOf(wertPlus);

                            grpobj._listZeile[zeile].audioZeile.sldKlangPause.Value =
                                (neu - grpobj._listZeile[zeile].audioZeile.sldKlangPause.Ticks[IndexPlus - 1] < wertPlus - neu) ?
                                grpobj._listZeile[zeile].audioZeile.sldKlangPause.Ticks[IndexPlus - 1] : wertPlus;
                        }
                    }

                    // Song aus der Liste der laufenden Songs herausnehmen
                    grpobj._listZeile[zeile].istLaufend = false;

                    // Song in die Liste der Standby-Songs aufnehmen wenn nur ein Song in Liste
                    if (grpobj._listZeile.FindAll(t => t.istStandby).Count == 0)
                    {
                        grpobj._listZeile[zeile].istStandby = true;
                        CheckPlayStandbySongs(grpobj);// posObjGruppe);
                    }
                    else
                    {
                        posit = 4;
                        // Song in die Liste der Standby-Songs aufnehmen wenn nur mehere Songs verfügbar
                   //FALSCH     // somit wird nicht 2x der gleiche Song gespielt
                        CheckPlayStandbySongs(grpobj);
                        grpobj._listZeile[zeile].istStandby = true;
                        posit = 5;
                    }
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Playlist Fehler", "Fehler beim Überprüfen der Endewartezeit" + Environment.NewLine +
                 "Zeile=" + zeile + "   wertPlus" + wertPlus + "   Neu=" + neu + "   IndexPlus=" + IndexPlus + " Posit=" + posit, ex);

                errWin.ShowDialog();
                errWin.Close();
            }
        }
        
        void Player_MusikMediaFailed(object sender, ExceptionEventArgs e)
        {
            try
            {
                (sender as MediaPlayer).Stop();
                (sender as MediaPlayer).Close();
                (sender as MediaPlayer).MediaFailed -= Player_KlangMediaFailed;
                MusikProgBarTimer.Stop();
                ListBoxItem lbItem = (ListBoxItem)lbMusiktitellist.SelectedItem;
                lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));      // Yellow
                lbItem.ToolTip = "Datei kann nicht abgespielt werden. Ungültiger Name? Vermeiden Sie Sonderzeichen( #&'! ) im Zusammenhang mit Netzlaufwerken.";
                SpieleNeuenMusikTitel(Guid.Empty);
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswerten des Musikfehlers ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        void Player_Ended(object sender, EventArgs e)
        {
            try
            {
                int posObjGruppe = 0;

                while (posObjGruppe < _GrpObjecte.Count &&
                    !_GrpObjecte[posObjGruppe]._listZeile.Exists(t => t.mediaHashCode.Equals((sender as MediaPlayer).GetHashCode())))
                    posObjGruppe++;
                if (posObjGruppe < _GrpObjecte.Count)
                {
                    int zeile = _GrpObjecte[posObjGruppe]._listZeile.FindIndex(t => t.mediaHashCode.Equals((sender as MediaPlayer).GetHashCode()));

                    int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
                    if (objGruppe == -1)
                        return;

                    if (!_GrpObjecte[posObjGruppe].istMusik &&                             // Direkt wieder anstarten wenn der Titel die einigste Möglichkeit ist
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Pause == 0 &&
                        _GrpObjecte[posObjGruppe].aPlaylist.MaxSongsParallel == _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend).Count &&
                        _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istStandby).Count == 0)
                        _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Position = (!_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.TeilAbspielen) ? 
                            TimeSpan.FromMilliseconds(0) : TimeSpan.FromMilliseconds(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.TeilStart.Value);
                    else
                    {
                        _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Stop();
                        KlangPlayEndetimer = new DispatcherTimer();
                        
                        //Neue Wartezeit fest oder per RANDOM bestimmen                        
                        KlangPlayEndetimer.Interval = (_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseChange)?
                            TimeSpan.FromMilliseconds(
                                (new Random()).Next(_GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert , 
                                _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert)):
                            TimeSpan.FromMilliseconds(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Pause);
                            
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].istWartezeit = true;
                        KlangPlayEndetimer.Tag = _GrpObjecte[posObjGruppe]._listZeile[zeile].ID_Zeile;
                        KlangPlayEndetimer.Tick += new EventHandler(KlangPlayEndetimer_Tick);
                        KlangPlayEndetimer.Start();
                        
                        if (!_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.TeilAbspielen)
                            _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Close(); 
                        
                        if (_GrpObjecte[posObjGruppe].visuell)
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Value = _GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Maximum;
                                          
                    }
                }
                App.CloseSplashScreen();
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Playlist Fehler", "Nach dem Beenden des Musiktitels ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }


        void Player_KlangMediaFailed(object sender, ExceptionEventArgs e)
        {
            try
            {
                char[] Separator = new char[] { '_' };

                int mediahash = (sender as MediaPlayer).GetHashCode();
                int posObjGruppe = 0;
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpobj in _GrpObjecte)
                    if (chkgrpobj._listZeile.Exists(t => t.mediaHashCode.Equals(mediahash)))
                    {
                        grpobj = chkgrpobj;
                        break;
                    }

               /* while (posObjGruppe < _GrpObjecte.Count &&
                       !grpobj._listZeile.Exists(t => t.mediaHashCode.Equals(mediahash)))
                    posObjGruppe++;*/

                if (grpobj != null) //posObjGruppe < _GrpObjecte.Count)
                {
                    int zeile = grpobj._listZeile.FindIndex(t => t.mediaHashCode.Equals(mediahash));

                    int objGruppe = grpobj.objGruppe;
                    if (objGruppe == -1)
                        return;

                    string file = grpobj._listZeile[zeile].audiotitel.Audio_Titel.Pfad.Substring(1,1) == ":" ?
                        grpobj._listZeile[zeile].audiotitel.Audio_Titel.Pfad :
                        stdPfad + "\\" + grpobj._listZeile[zeile].audiotitel.Audio_Titel.Pfad;

                    if (!File.Exists(file))
                    {
                        if (grpobj.visuell)
                        {
                            grpobj._listZeile[zeile].audioZeile.lbiEditorRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));       // Red
                            grpobj._listZeile[zeile].audioZeile.lbiEditorRow.ToolTip = "Datei nicht gefunden. -> " + file;
                        }
                    }
                    else
                    {
                        if (grpobj.visuell)
                        {
                            grpobj._listZeile[zeile].audioZeile.lbiEditorRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));
                            grpobj._listZeile[zeile].audioZeile.lbiEditorRow.ToolTip = "Datei kann nicht abgespielt werden (evtl. Inkompatibler Typ oder Geschwindigkeits-Problem).";
                        }
                    }
                    grpobj.NochZuSpielen.RemoveAll(t => t.Equals(grpobj._listZeile[zeile].audiotitel.Audio_TitelGUID));
                    if (grpobj._listZeile[zeile]._mplayer != null)
                    {
                        grpobj._listZeile[zeile]._mplayer.Stop();
                        grpobj._listZeile[zeile]._mplayer.Close();
                        grpobj._listZeile[zeile]._mplayer.MediaEnded -= Player_Ended;
                        grpobj._listZeile[zeile]._mplayer.MediaFailed -= Player_KlangMediaFailed;
                        grpobj._listZeile[zeile]._mplayer = null;
                    }

                    grpobj._listZeile[zeile].istPause = false;
                    grpobj._listZeile[zeile].istLaufend = false;
                    grpobj._listZeile[zeile].istStandby = false;
                    grpobj._listZeile[zeile].playable = false;

                    foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                    {
                        if ((Guid)mZeile.Tag == grpobj.aPlaylist.Audio_PlaylistGUID)
                        {
                            mZeile.spnlMusikZeile.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 80));   // Brushes.Yellow
                            mZeile.spnlMusikZeile.ToolTip = grpobj._listZeile.FindAll(t => t.playable).Count + " von " + grpobj._listZeile.FindAll(t => t.audiotitel.Aktiv).Count + " Titel abspielbar";
                        }
                    }

                    CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);
                }
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswetrten des Klang-Playerfehlers ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void slBGVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (IsInitialized && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null && 
                    !FadingIn_Started && !_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
                    _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Volume = e.NewValue / 100;
                if (Convert.ToDouble(btnBGSpeaker.Tag) != -1)
                    btnBGSpeaker.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                ((Slider)sender).ToolTip = Math.Round(((Slider)sender).Value) + " %";
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Ändern der Hintergrund Lautstärke ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void lbBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbPListMusik.Items.Count != lbBackground.Items.Count)
                    AktualisierePListPlaylist();
                if (lbPListMusik.SelectedIndex != lbBackground.SelectedIndex)
                    lbPListMusik.SelectedIndex = lbBackground.SelectedIndex;

                if (e != null)
                {
                    ListBox selListBox = ((ListBox)(e.Source));
                    if (selListBox.SelectedItems.Count != 0)
                    {
                        try
                        {
                            if (MusikProgBarTimer != null &&
                                MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen)
                            {
                                MusikProgBarTimer.Stop();
                                btnBGAbspielen.Tag = true;
                                btnBGAbspielen_Click(btnBGAbspielen, new RoutedEventArgs());
                            }

                            Audio_Playlist playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(((MusikZeile)lbBackground.SelectedItem).Tag)).FirstOrDefault();
                            if (playlistliste != null)
                            {
                                _BGPlayer.NochZuSpielen.Clear();
                                _BGPlayer.Gespielt.Clear();
                                lbBackground.Tag = selListBox.SelectedIndex;
                                _BGPlayer.AktPlaylist = playlistliste;
                                lbMusiktitellist.Items.Clear();
                                _BGPlayer.AktTitel.Clear();

                                List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(playlistliste);
                                foreach (Audio_Titel aTitel in titel.OrderBy(t => t.Name))
                                {
                                    _BGPlayer.AktTitel.Add(aTitel);
                                    ListBoxItem lbitem = new ListBoxItem();
                                    lbitem.Tag = aTitel.Audio_TitelGUID;
                                    lbitem.Content = aTitel.Name;
                                    lbitem.ToolTip = (System.IO.Path.GetDirectoryName(aTitel.Pfad).Substring(1, 1) != ":") ?
                                        "[Standard-Verzeichnis]" + @"\" + aTitel.Pfad : aTitel.Pfad;

                                    if (!playlistliste.Audio_Playlist_Titel.First(t => t.Audio_TitelGUID == aTitel.Audio_TitelGUID).Aktiv)
                                    {
                                        lbitem.FontStyle = FontStyles.Italic;
                                        lbitem.Foreground = Brushes.DarkSlateGray;
                                        lbitem.ToolTip = "Audio-Titel inaktiv." + Environment.NewLine + "Im Playlist-Editor anhaken zum Aktivieren" +
                                                         Environment.NewLine + "Anklicken um den Titel abzuspielen";
                                    }
                                    lbMusiktitellist.Items.Add(lbitem);
                                }
                                if (titel.Count > 0)
                                {
                                    btnBGAbspielen.IsEnabled = true;
                                    btnPListMusikAbspielen.IsEnabled = true;
                                    btnBGAbspielen.Tag = false;
                                    btnBGNext.IsEnabled = true;
                                    imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
                                    btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;
                                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Maximum = pbarBGSong.Maximum;
                                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Value = pbarBGSong.Value;
                                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Visibility = Visibility.Visible;

                                    if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen)
                                    {
                                        SpieleNeuenMusikTitel(Guid.Empty);
                                        if (titel.Count == 0)
                                        {
                                            btnBGAbspielen.Tag = true;
                                            btnBGAbspielen_Click(btnBGAbspielen, new RoutedEventArgs());
                                        }
                                    }

                                    if (_BGPlayer.totalLength != -1)
                                    {
                                        aPlaylistLengthCheck = _BGPlayer.AktPlaylist;
                                        _BGPlayer.totalLength = -1;
                                        if (aPlaylistLengthCheck != null)
                                            GetTotalLength();
                                    }
                                }
                                else
                                {
                                    grdSongInfo.Visibility = Visibility.Hidden;

                                    btnBGNext.IsEnabled = false;
                                    imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
                                    btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;

                                    btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                                    btnBGAbspielen.IsEnabled = false;
                                    btnPListMusikAbspielen.IsEnabled = false;
                                    btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
                                }
                            }
                            else
                            {
                                lbBackground.Tag = -1;
                                grdSongInfo.Visibility = Visibility.Hidden;

                                btnBGNext.IsEnabled = false;
                                imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
                                btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;

                                btnBGAbspielen.IsEnabled = false;
                                btnPListMusikAbspielen.IsEnabled = false;
                                btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
                            }
                        }
                        catch (Exception ex)
                        {
                            var errWin = new MsgWindow("Playlist Fehler", "Die Playliste konnte nicht geöffnet werden oder die Playliste ist leer", ex);
                            errWin.ShowDialog();
                            errWin.Close();
                        }
                    }
                    else
                    {
                        if (selListBox.SelectedItems.Count == 0)
                        {
                            btnBGNext.IsEnabled = false;
                            imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
                            btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;

                            btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                            btnBGAbspielen.IsEnabled = false;
                            btnPListMusikAbspielen.IsEnabled = false;
                            btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Playlist Fehler", "Beim Auswählen der Playlist ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void MusikSongInfo(Visibility sichtbar)
        {
            try
            {
                for (int i = 0; i <= grdSongInfo.Children.Count - 1; i++)
                    grdSongInfo.Children[i].Visibility = sichtbar;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Visualisieren der Musik-Infos ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void RenewMusikNochZuSpielen()
        {
            for (int i = 0; i < lbMusiktitellist.Items.Count; i++)
            {
                if (((ListBoxItem)lbMusiktitellist.Items[i]).Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString() &&     // Red
                    ((ListBoxItem)lbMusiktitellist.Items[i]).Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 255, 0)).ToString())     // Yellow
                {
                    Audio_Playlist_Titel aktPlaylistTitel = _BGPlayer.AktPlaylist.Audio_Playlist_Titel.First(
                        t => t.Audio_Titel.Name == ((ListBoxItem)lbMusiktitellist.Items[i]).Content.ToString());
                    if (aktPlaylistTitel.Aktiv)
                    {
                        for (int bew = 0; bew <= aktPlaylistTitel.Rating; bew++)
                            _BGPlayer.NochZuSpielen.Add(aktPlaylistTitel.Audio_TitelGUID);
                    }
                }
            }
        }

        private void SpieleNeuenMusikTitel(Guid Index)
        {
            if (_BGPlayer.NochZuSpielen.Count == 0)
                RenewMusikNochZuSpielen();

            //FadingOut des aktuellen BG-Titels
            if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds > 0)
            {
                if (lbMusiktitellist.SelectedIndex != -1)
                    _BGPlayer.Gespielt.Add((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
                MusikProgBarTimer.Stop();
            }


            if (_BGPlayer.NochZuSpielen.Count != 0)  // kein abspielbarer Titel gefunden
            {
                if (lbMusiktitellist.Items.Count == 1)
                {
                    lbMusiktitellist.SelectedIndex = -1;
                    lbMusiktitellist.SelectedIndex = 0;
                }
                else
                {
                    chkbxPlayRange.IsChecked = false;
                    rsldTeilSong.Visibility = Visibility.Hidden;
                    rsldTeilSong.LowerValue = 0;
                    rsldTeilSong.UpperValue = 100000;
                    if (Index != Guid.Empty)
                    {
                        if ((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag == Index)
                            lbMusiktitellist.SelectedIndex = -1;
                        Int16 i = 0;
                        while (((Guid)((ListBoxItem)lbMusiktitellist.Items[i]).Tag) != Index)
                            i++;
                        lbMusiktitellist.SelectedIndex = i;
                        lbMusiktitellist.ScrollIntoView(lbMusiktitellist.SelectedItem);
                    }
                    else
                    {
                        // Shuffle-Modus
                        if (btnShuffle.IsChecked.Value)
                        {

                            Guid u = _BGPlayer.NochZuSpielen[(new Random()).Next(0, _BGPlayer.NochZuSpielen.Count)];

                            Int16 i = 0;
                            while ((Guid)((ListBoxItem)lbMusiktitellist.Items[i]).Tag != u)
                                i++;
                            _BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(u));
                            lbMusiktitellist.SelectedIndex = i;
                            if (lbMusiktitellist.SelectedItem != null)
                                lbMusiktitellist.ScrollIntoView(lbMusiktitellist.SelectedItem);
                        }
                        else
                        {
                            int i = lbMusiktitellist.SelectedIndex;
                            int startIndex = i;
                            if (i == lbMusiktitellist.Items.Count - 1)
                                i = 0;
                            else
                                i++;
                            while (((ListBoxItem)lbMusiktitellist.Items[i]).Background.ToString() == new SolidColorBrush(Color.FromArgb(100, 255, 255, 0)).ToString() ||  // Yellow
                                    ((ListBoxItem)lbMusiktitellist.Items[i]).Background.ToString() == new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString() &&   // Red
                                    i != startIndex)
                            {
                                if (i != lbMusiktitellist.Items.Count - 1)
                                    i++;
                                else
                                    i = 0;
                            }

                            _BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(i));
                            if (i != startIndex)
                            {
                                lbMusiktitellist.SelectedIndex = i;
                                lbMusiktitellist.ScrollIntoView(lbMusiktitellist.SelectedItem);
                            }
                        }
                    }
                }
            }
            else
            {
                lbBackground.Tag = -1;
                lbBackground.SelectedIndex = -1;
            }
        }

        private void btnBGSpeaker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(btnBGSpeaker.Tag) != -1)
                {
                    btnBGSpeaker.Tag = -1;
                    btnImgBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
                    if (_BGPlayer.BG[0].mPlayer != null) _BGPlayer.BG[0].mPlayer.IsMuted = false;
                    if (_BGPlayer.BG[1].mPlayer != null) _BGPlayer.BG[1].mPlayer.IsMuted = false;
                }
                else
                {
                    btnBGSpeaker.Tag = slBGVolume.Value;
                    btnImgBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker-mute.png"));
                    if (_BGPlayer.BG[0].mPlayer != null) _BGPlayer.BG[0].mPlayer.IsMuted = true;
                    if (_BGPlayer.BG[1].mPlayer != null) _BGPlayer.BG[1].mPlayer.IsMuted = true;
                }
                btnImgPListMusikSpeaker.Source = btnImgBGSpeaker.Source;
                slPlaylistMusikVolume.Value = slBGVolume.Value;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Klicken des Lautsprecher-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnBGAbspielen_Click(object sender, RoutedEventArgs e)
        {                                   //btnBGAbspielen.Tag = 0 --> Button hat Play-Icon, kein Sound spielt
            try
            {
                if (!Convert.ToBoolean(btnBGAbspielen.Tag) && !_BGPlayer.BG[_BGPlayer.aktiv].isPaused ||
                    _BGPlayer.BG[_BGPlayer.aktiv].isPaused &&
                    (_BGPlayer.BG[_BGPlayer.aktiv].aPlaylist == null ||
                     _BGPlayer.BG[_BGPlayer.aktiv].aPlaylist.Audio_PlaylistGUID == (Guid)((MusikZeile)lbBackground.SelectedItem).Tag))
                {
                    btnBGAbspielen.Tag = !Convert.ToBoolean(btnBGAbspielen.Tag);

                    if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Source != null)
                    {
                        grdSongInfo.Visibility = Visibility.Visible;
                        if (!_BGPlayer.BG[_BGPlayer.aktiv].isPaused)
                        {
                            lbMusiktitellist.SelectionChanged -= lbMusiktitellist_SelectionChanged;
                            lbMusiktitellist.SelectedIndex = Convert.ToInt16(lbMusiktitellist.Tag);
                            lbMusiktitellist.SelectionChanged += lbMusiktitellist_SelectionChanged;
                        }
                        _BGPlayer.BG[_BGPlayer.aktiv].aPlaylist = _BGPlayer.AktPlaylist;
                        
                        _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = false;
                        FadingIn_Started = false;                           

                        _BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;
                        FadingIn(_BGPlayer.BG[_BGPlayer.aktiv].mPlayer, Convert.ToDouble(_BGPlayer.AktPlaylistTitel.Volume) / 100);
                        btnBGAbspielen.Tag = true;
                        btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
                    }
                    else
                        SpieleNeuenMusikTitel(Guid.Empty);
                                        
                    btnBGStoppen.IsEnabled = true;
                    btnBGPrev.IsEnabled = true;
                    imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
                    btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
                    btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;
                    btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
                }
                else
                {
                    if (Convert.ToInt16(lbBackground.Tag) == lbBackground.SelectedIndex )          // Auswahl Playliste nicht geändert
                    {
                        FadingIn_Started = false;
                              
                        _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
                        BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], false, true);
                        _BGPlayer.BG[_BGPlayer.aktiv].aPlaylist = null;
                    }
                   
                    btnBGAbspielen.Tag = true;
                    btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                }
                btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
                (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Play-/Pause-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnBGStoppen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;
                if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                {
                    if (!_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
                    {
                        _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
                        BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], true, true);
                    }
                    lbMusiktitellist.Tag = lbMusiktitellist.SelectedIndex;
                }

                if (lbMusiktitellist.SelectedIndex >= 0)
                    _BGPlayer.Gespielt.Add((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
                btnBGPrev.IsEnabled = false;
                imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
                btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;

                MusikProgBarTimer.Stop();
                btnBGAbspielen.Tag = false;
                grdSongInfo.Visibility = Visibility.Hidden;
                lbMusiktitellist.SelectedIndex = -1;
                btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                if (lbPListMusik.SelectedItem != null)
                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Visibility = Visibility.Collapsed;
                btnBGStoppen.IsEnabled = false;

                btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;
                btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
                btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Stop-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void lbBackground_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lbBackground_SelectionChanged(sender, null);
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Doppelklicken des Maus-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnBGNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (lbMusiktitellist.SelectedIndex == -1)
                    SpieleNeuenMusikTitel(Guid.Empty);
                else
                {
                    _BGPlayer.Gespielt.Add((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
                    lbMusiktitellist.Tag = lbMusiktitellist.SelectedIndex;
                    if (btnBGRepeat.IsChecked.Value)
                        SpieleNeuenMusikTitel((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
                    else
                        SpieleNeuenMusikTitel(Guid.Empty);
                }
                btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
                if (lbPListMusik.SelectedItem != null)
                    (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Next-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnBGPrev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_BGPlayer.Gespielt.Count == 0)
                    SpieleNeuenMusikTitel((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
                else
                {
                    Guid vorher = _BGPlayer.Gespielt.ElementAt(_BGPlayer.Gespielt.Count - 1);

                    SpieleNeuenMusikTitel(vorher);
                    if (_BGPlayer.Gespielt.Count > 0)
                        _BGPlayer.Gespielt.RemoveAt(_BGPlayer.Gespielt.Count - 1);
                    lbMusiktitellist.Tag = -1;
                }
                btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
                (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Zurück-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private bool ChecklbEditorPossible(ListboxItemIcon vorher)
        {
            Guid GuidVorher = vorher == null ? Guid.Empty : (Guid)vorher.Tag;
            Guid GuidSoll = (Guid)((ListboxItemIcon)lbEditor.Items[lbEditor.SelectedIndex]).Tag;
            bool found = false;

            if (tcAudioPlayer.SelectedItem == tiEditor && GuidVorher != GuidSoll)
            {
                bool hasHintergrund = false;

                for (int i = 0; i < tcEditor.Items.Count - 2; i++)
                {
                    if (((TCButtons)tcEditor.Items[i]).Visibility == Visibility.Collapsed)
                        continue;
                    // Check doppelte Playlists
                    if (((TCButtons)tcEditor.Items[i]).Tag != null &&
                        ((TCButtons)tcEditor.Items[i]).Visibility == Visibility.Visible &&
                        ((Guid)((TCButtons)tcEditor.Items[i]).Tag) == GuidSoll)
                    {
                        string messageBoxText = "Die Playlist ist bereits in dem Theme aufgelistet und kann nicht nochmals benutzt werden.";
                        string caption = "Doppelte Playlist im Theme";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;

                        MessageBox.Show(messageBoxText, caption, button, icon);
                        lbEditor.SelectedIndex = -1;
                        found = true;
                        break;
                    }

                    // Check zwei Musik-Playlists
                    if (((TCButtons)tcEditor.Items[i]).Tag != null)
                    {
                        Audio_Playlist aplyLst = Global.ContextAudio.PlaylistListe.First(t => t.Audio_PlaylistGUID == (Guid)((TCButtons)tcEditor.Items[i]).Tag);
                        if (aplyLst != null && aplyLst.Hintergrundmusik)
                            hasHintergrund = true;
                    }

                    if (((TCButtons)tcEditor.Items[i]).Tag != null && hasHintergrund &&
                        ((TCButtons)tcEditor.Items[i]).Visibility == Visibility.Visible &&
                        Global.ContextAudio.PlaylistListe.First(t => t.Audio_PlaylistGUID == GuidSoll).Hintergrundmusik)
                    {
                        string messageBoxText = "Das Theme enthält schon eine Hintergrund-Playlist. Pro Theme kann nur eine Hintergrund-Playlist abgespielt werden.";
                        string caption = "Hintergrund-Playlist Error";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;

                        MessageBox.Show(messageBoxText, caption, button, icon);
                        lbEditor.SelectedIndex = -1;
                        found = true;
                        break;
                    }
                }
            }
            return !found;
        }

        private void lbEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbEditor.SelectedIndex != -1 &&
                    (tcAudioPlayer.SelectedItem == tiEditor && rbEditorEditPList.IsChecked.Value ||
                     ChecklbEditorPossible((ListboxItemIcon)(e.RemovedItems.Count == 0 ? null : e.RemovedItems[0]))))
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    try
                    {
                        int was = lbEditor.SelectedIndex;

                        if (tcEditor.SelectedItem == null ||
                            (tcEditor.SelectedItem.GetType() == typeof(TCButtons) && ((TCButtons)tcEditor.SelectedItem).Visibility != Visibility.Visible) ||
                            (tcEditor.SelectedItem.GetType() == typeof(TabItem)))
                        {
                            tiPlus_MouseUp(tiPlus, null);
                            lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                            lbEditor.SelectedIndex = was;
                            lbEditor.SelectionChanged += lbEditor_SelectionChanged;
                        }
                        GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.Items.GetItemAt(Convert.ToInt32(tcEditor.Tag)));
                       
                        List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.
                            Where(t => t.Audio_PlaylistGUID.Equals(((ListboxItemIcon)lbEditor.Items[lbEditor.SelectedIndex]).Tag)).ToList();

                        for (int i = 0; i <= grdEditorPlaylistInfo.Children.Count - 1; i++)
                            if (((grdEditorPlaylistInfo.Children[i] is UIElement) && (grdEditorPlaylistInfo.Children[i] as UIElement) is Border) ||
                                (grdEditorPlaylistInfo.Children[i] as Control).Name != "btnKlangSave") grdEditorPlaylistInfo.Children[i].Visibility = Visibility.Visible;

                        grpobj.grdEditor.Visibility = Visibility.Hidden;

                        if (playlistliste.Count == 1)
                        {
                            ((TCButtons)tcEditor.SelectedItem).Tag = ((ListboxItemIcon)lbEditor.Items[lbEditor.SelectedIndex]).Tag;
                            List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]).OrderBy(t => t.Name).ToList();

                            if (rbEditorEditTheme.IsChecked.Value)
                            {
                                rbEditorEditTheme_Checked(null, null);
                                if (rbEditorEditTheme.IsEnabled == true && AktKlangTheme == null)
                                {
                                    NeueKlangThemeInDB(tboxKlangThemeName.Text);
                                    tboxKlangThemeName.Focus();
                                }
                            }

                            if (e != null && grpobj.aPlaylist != null && rbEditorEditTheme.IsChecked.Value && AktKlangTheme != null &&
                                AktKlangTheme.Audio_Playlist.Contains(grpobj.aPlaylist))
                            {
                                AktKlangTheme.Audio_Playlist.Remove(grpobj.aPlaylist);
                                Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                            }
                            
                            //Letzte visuell TabItem befreien von Zeilen
                            PlaylisteLeeren(_GrpObjecte.LastOrDefault(t => t.visuell));
                            
                            AktKlangPlaylist = playlistliste[0];

                            if (AktKlangPlaylist.Hintergrundmusik)
                                grpobj.rbTopIstMusikPlaylist.IsChecked = true;
                            else
                                grpobj.rbTopIstKlangPlaylist.IsChecked = true;

                            ((TCButtons)tcEditor.SelectedItem)._tbText.Text = AktKlangPlaylist.Name;
                            ((TCButtons)tcEditor.SelectedItem).Focus();

                            grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
                            grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;

                            grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallel_TextChanged;
                            grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;
                            grpobj.tbTopKlangSongsParallel.Text = "1";
                            grpobj.istMusik = AktKlangPlaylist.Hintergrundmusik;
                            
                            grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallel_TextChanged;

                            if (titelliste.Count > 0)
                            {
                                grpobj.playlistName = AktKlangPlaylist.Name;
                                for (UInt16 x = 0; x <= AktKlangPlaylist.Audio_Playlist_Titel.Count - 1; x++)
                                {
                                    Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titelliste[x])[0];
                                    //*************** TEMP *********************
                                    if (playlisttitel.Audio_Titel.Pfad.StartsWith(stdPfad))
                                    {
                                        playlisttitel.Audio_Titel.Pfad = playlisttitel.Audio_Titel.Pfad.Substring(stdPfad.Length);
                                        if (playlisttitel.Audio_Titel.Pfad.StartsWith(@"\")) playlisttitel.Audio_Titel.Pfad = playlisttitel.Audio_Titel.Pfad.Substring(1);
                                        Global.ContextAudio.Update<Audio_Titel>(playlisttitel.Audio_Titel);
                                    }
                                    //******************************************

                                    KlangNewRow(playlisttitel.Audio_Titel.Pfad, grpobj, x, playlisttitel);

                                    //      if (AktKlangPlaylist.Hintergrundmusik)
                                    {
                                        if (playlisttitel.Aktiv &&
                                            !grpobj.NochZuSpielen.Contains(grpobj._listZeile[x].audiotitel.Audio_TitelGUID))
                                        {
                                            for (int i = 0; i <= grpobj._listZeile[x].audiotitel.Rating; i++)
                                                grpobj.NochZuSpielen.Add(grpobj._listZeile[x].audiotitel.Audio_TitelGUID);
                                        }
                                    }
                                }
                                if (AktKlangPlaylist.Hintergrundmusik)
                                    ZeigeZeileKlangSpalten(grpobj, false);

                                grpobj.aPlaylist = AktKlangPlaylist;
                                grpobj.grdEditor.Visibility = Visibility.Visible;
                                grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();

                                CheckAlleAngehakt(grpobj);
                                CheckChPfadHatBezug(grpobj);
                            }

                            grpobj.grdEditor.Visibility = Visibility.Visible;

                            if (grpobj.wirdAbgespielt)
                            {
                                AlleKlangSongsAus(grpobj, false, false);

                                if (grpobj.istMusik)
                                    grpobj.wirdAbgespielt = false;
                                ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                            }

                            if (AktKlangPlaylist.Hintergrundmusik)
                            {
                                ZeigeKlangSongsParallel(grpobj, false);
                                ZeigeKlangTop(grpobj, false);
                                grpobj.spnlTopHotkey.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                ZeigeKlangSongsParallel(grpobj, true);
                                ZeigeKlangTop(grpobj, true);
                                grpobj.spnlTopHotkey.Visibility = Visibility.Visible;

                                hotkey hKey = hotkeys.FirstOrDefault(t => t.aPlaylistGuid == AktKlangPlaylist.Audio_PlaylistGUID);
                                grpobj.btnTopHotkeySet.Content = (hKey == null) ? "nicht definiert" : Convert.ToChar(hKey.taste).ToString();

                                grpobj.btnHotkeyEntfernen.Visibility = hKey == null ? Visibility.Collapsed : Visibility.Visible;
                                grpobj.cmboxTopHotkey.SelectedIndex = -1;
                            }

                            //Neue Playlist in Theme hinzufügen
                            if (tcAudioPlayer.SelectedItem == tiEditor && rbEditorEditTheme.IsChecked.Value &&
                                !AktKlangTheme.Audio_Playlist.Contains(AktKlangPlaylist))
                            {
                                AktKlangTheme.Audio_Playlist.Add(AktKlangPlaylist);
                                if (Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == AktKlangTheme.Audio_ThemeGUID) == null)
                                    NeueKlangThemeInDB(AktKlangTheme.Name);
                                else
                                {
                                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                                    Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                                }
                                lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                                lbEditor.SelectedIndex = was;
                                lbEditor.SelectionChanged += lbEditor_SelectionChanged;
                            }
                            tbEditorTopFilterX.Text = "";

                            if (((TabItem)tcAudioPlayer.SelectedItem) == tiEditor && lbEditorTheme.Tag == null)   //lbEditorTheme.Tag == null --> lbEditorTheme geklickt
                                CheckBtnGleicherPfad(_GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem));
                        }
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Auswahlfehler", "Beim Ändern der Selektierung des 'lbEditor' ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }


        private void lbEditorTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbEditorTheme.SelectedIndex != -1)
                {
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        {
                            try
                            {
                                AktKlangPlaylist = null;
                                Guid ThemeGuid = (Guid)((ListboxItemIcon)lbEditorTheme.Items[lbEditorTheme.SelectedIndex]).Tag;
                                Audio_Theme atheme = Global.ContextAudio.LoadThemesByGUID(ThemeGuid);

                                if (atheme != null)
                                {
                                    AktKlangTheme = atheme;

                                    // Bestehende Playlist Schließen
                                    for (int i = tcEditor.Items.Count - 3; i >= 0; i--)
                                    {
                                        lbEditorTheme.Tag = true;
                                        if (((TCButtons)tcEditor.Items[i]).Visibility == Visibility.Visible)
                                        {
                                            AlleKlangSongsAus(_GrpObjecte.FirstOrDefault(t => t.ticKlang == ((TCButtons)tcEditor.Items[i])), false, false);
                                            ((TCButtons)tcEditor.Items[i])._buttonClose.IsEnabled = false;
                                            ((TCButtons)tcEditor.Items[i])._buttonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                        }
                                        lbEditorTheme.Tag = null;
                                    }

                                    if (wpnlEditorTopThemesThemes.Children.Count > 0)
                                    {
                                        wpnlEditorTopThemesThemes.Children.RemoveRange(0, wpnlEditorTopThemesThemes.Children.Count);
                                        expEditorTopThemeTheme.Visibility = Visibility.Collapsed;
                                    }

                                    // Erstelle Untergeordnete Themes
                                    foreach (Audio_Theme aUnterTheme in atheme.Audio_Theme1.
                                        Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
                                    {
                                        boxThemeTheme bxTheme = new boxThemeTheme();
                                        bxTheme.txblkName.Text = aUnterTheme.Name;
                                        bxTheme.Tag = aUnterTheme.Audio_ThemeGUID;
                                        bxTheme.btnClose.Tag = aUnterTheme.Audio_ThemeGUID;
                                        bxTheme.btnClose.Click += bxThemeBtnClose_Click;
                                        wpnlEditorTopThemesThemes.Children.Add(bxTheme);
                                    }
                                    expEditorTopThemeTheme.Visibility = wpnlEditorTopThemesThemes.Children.Count == 0 ? Visibility.Collapsed : Visibility.Visible;

                                    //Komplette Playlist anzeigen
                                    if (!rbEditorKlang.IsChecked.Value)
                                    {
                                        rbEditorKlang.IsChecked = true;
                                        rbEditorKlang.RaiseEvent(new RoutedEventArgs(RadioButton.ClickEvent));
                                    }
                                    if (!rbEditorMusik.IsChecked.Value)
                                    {
                                        rbEditorMusik.IsChecked = true;
                                        rbEditorMusik.RaiseEvent(new RoutedEventArgs(RadioButton.ClickEvent));
                                    }

                                    // Erstelle Playlists aus dem Theme
                                    int alt_index = lbEditor.SelectedIndex;
                                    int plylst_added = 0;
                                    foreach (Audio_Playlist aplylst in atheme.Audio_Playlist.OrderBy(t => t.Name))
                                    {
                                        for (int i = 0; i < lbEditor.Items.Count; i++)
                                        {
                                            if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == aplylst.Audio_PlaylistGUID)
                                            {
                                                if (plylst_added > 0 || tcEditor.SelectedIndex == -1) tiPlus_MouseUp(tiPlus, null);
                                                lbEditorTheme.Tag = true;
                                                lbEditor.SelectedIndex = i;
                                                lbEditorTheme.Tag = null;
                                                plylst_added++;
                                                break;
                                            }
                                        }
                                    }
                                    AktKlangPlaylist = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem).aPlaylist;
                                    CheckBtnGleicherPfad(_GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem));
                                    tboxKlangThemeName.Text = atheme.Name;
                                }
                            }
                            catch (Exception ex)
                            {
                                var errWin = new MsgWindow("Datenfehler", "Die Playlist-Liste konnte nicht eindeutig in der Datenbank detektiert werden.", ex);
                                errWin.ShowDialog();
                                errWin.Close();

                                for (int i = 0; i <= grdEditorPlaylistInfo.Children.Count - 1; i++)
                                    if ((grdEditorPlaylistInfo.Children[i] as Control) != null &&
                                        (grdEditorPlaylistInfo.Children[i] as Control).Name != "btnKlangSave") grdEditorPlaylistInfo.Children[i].Visibility = Visibility.Hidden;
                            }
                        }
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }
                }
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Wechseln der Listbox im Theme-Mode ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }
        
        private void btnTopChPfad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
                if (titelliste.Count > 0)
                {
                    Global.SetIsBusy(true);
                    Mouse.OverrideCursor = Cursors.AppStarting;
                    string titelRef = titelliste[0].Pfad.LastIndexOf(@"\") != -1 ? titelliste[0].Pfad.Substring(0, titelliste[0].Pfad.LastIndexOf(@"\")) : titelliste[0].Pfad;
                    titelRef = (titelRef.Substring(1, 1) != ":") ? stdPfad + @"\" + titelRef : titelRef;

                    titelliste.ForEach(delegate(Audio_Titel aTitel)
                    {
                        string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;

                        while (!vergleich.StartsWith(titelRef))
                        {
                            if (titelRef.Contains(@"\"))
                                titelRef = titelRef.Substring(0, titelRef.LastIndexOf(@"\"));
                            else
                            {
                                titelRef = stdPfad;
                                break;
                            }
                        }
                    });

                    System.Windows.Forms.FolderBrowserDialog browse = new System.Windows.Forms.FolderBrowserDialog();
                    browse.Description = "Ändern des Bezug-Pfades für die Playlist '" + AktKlangPlaylist.Name + "'" + Environment.NewLine +
                        "Aktuelles Bezugs-Verzeichnis: " + Environment.NewLine + titelRef;
                    browse.SelectedPath = (Directory.Exists(titelRef)) ? titelRef : (Directory.Exists(stdPfad)) ? stdPfad : "C:\\";

                    browse.ShowNewFolderButton = false;
                    if (browse.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        int found = 0;
                        string ausweichPfad = browse.SelectedPath;
                        List<string> dateiGefundenInSub = new List<string>();
                        titelliste.ForEach(delegate(Audio_Titel aTitel)
                        {
                            string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;
                            if (File.Exists(browse.SelectedPath + vergleich.Substring(titelRef.Length)))
                                found++;
                            else
                            {
                                if (File.Exists(ausweichPfad + vergleich.Substring(titelRef.Length)))
                                    dateiGefundenInSub.Add(ausweichPfad + vergleich.Substring(titelRef.Length));
                                else
                                {
                                    MyTimer.start_timer();
                                    string inSub = Directory.GetFiles(browse.SelectedPath, System.IO.Path.GetFileName(vergleich), SearchOption.AllDirectories).FirstOrDefault();
                                    MyTimer.stop_timer();
                                    if (inSub != null)
                                    {
                                        dateiGefundenInSub.Add(inSub);
                                        ausweichPfad = System.IO.Path.GetDirectoryName(inSub);
                                    }
                                }
                            }
                        });

                        Mouse.OverrideCursor = null;

                        MessageBoxResult mbResult = MessageBox.Show("Ändern des Bezugs-Verzeichnisses von" + Environment.NewLine + "                  " + 
                            titelRef + Environment.NewLine +
                        "Auf das ausgwählte Verzeichnis" + Environment.NewLine + "                  " + browse.SelectedPath + Environment.NewLine + Environment.NewLine + Environment.NewLine +
                        "Von " + titelliste.Count + " Audiodateien konnten " + found + " Übereinstimmungen in dem ausgewählten Verzeichnis gefunden werden." + Environment.NewLine + Environment.NewLine +
                        (dateiGefundenInSub != null ?
                        dateiGefundenInSub.Count + " Dateien konnten in den Unterverzeichnissen gefunden werden" : "")
                        + Environment.NewLine + Environment.NewLine +
                        "Sollen ALLE gefundenen Audio-Pfade geändert werden?" + Environment.NewLine + Environment.NewLine +
                        "Nicht gefundene Dateien werden nicht verändert", "Änderungseinstellung", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

                        if (mbResult != MessageBoxResult.Cancel)
                        {
                            titelliste.ForEach(delegate(Audio_Titel aTitel)
                            {
                                string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;
                                if (mbResult == MessageBoxResult.OK)
                                {
                                    if (File.Exists(browse.SelectedPath + vergleich.Substring(titelRef.Length)))
                                        aTitel.Pfad = browse.SelectedPath + vergleich.Substring(titelRef.Length);
                                    else
                                    {
                                        dateiGefundenInSub.ForEach(delegate(string pathDatei)
                                        {
                                            if (System.IO.Path.GetFileName(pathDatei) == System.IO.Path.GetFileName(vergleich))
                                                aTitel.Pfad = pathDatei;
                                        });
                                    }
                                    Global.ContextAudio.Update<Audio_Titel>(aTitel);

                                    KlangZeile kZeile =_GrpObjecte.FirstOrDefault(t => t.aPlaylist == AktKlangPlaylist). //   [GetPosObjGruppe(GetObjGruppe(Convert.ToInt16(tcEditor.Tag)))].
                                        _listZeile.FirstOrDefault(t => t.audiotitel.Audio_TitelGUID == aTitel.Audio_TitelGUID);

                                    kZeile.audioZeile.chkTitel.ToolTip = aTitel.Pfad.StartsWith(stdPfad) ? 
                                        "[Standard-Verzeichnis]" + aTitel.Pfad.Substring(stdPfad.Length) : aTitel.Pfad;
                                        
                                    kZeile.audioZeile.chkTitel.Tag = (System.IO.Path.GetDirectoryName(aTitel.Pfad).Substring(1, 1) != ":") ?
                                                stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;

                                    kZeile.audioZeile.lbiEditorRow.Background = null;                                                                        
                                    kZeile.playable = true;
                                    kZeile.istLaufend = false;
                                }
                            });
                        }
                    }
                }
            }
            finally
            {
                Global.SetIsBusy(false);
                Mouse.OverrideCursor = null;
            }
        }
        
        private void lblKlangZeileCMenuÄndern_MouseDown(object sender, MouseButtonEventArgs e)
        {
            KlangZeile kZeile = (KlangZeile)((ContextMenu)((Label)sender).Parent).Tag;

            switch (Convert.ToInt16(((Label)sender).Tag))
            {
                case 1:                                                     // Geräusch/Musiktitel löschen
                    imgTrash0_0_MouseUp(kZeile.audioZeile.imgTrash, e);
                    break;
                case 2:                                                     // Datei-Bezug ändern                    
                    string vergleich = (kZeile.audiotitel.Audio_Titel.Pfad.Substring(1, 1) != ":") ? 
                        stdPfad + @"\" + kZeile.audiotitel.Audio_Titel.Pfad : kZeile.audiotitel.Audio_Titel.Pfad;

                     Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.CheckFileExists = true;
                    dlg.Multiselect = false;
                    dlg.Filter = "Musikdatei |" + System.IO.Path.GetFileName(vergleich); // Filter Dateien pro extension
                    dlg.InitialDirectory = System.IO.Path.GetDirectoryName(vergleich);
                    
                    Nullable<bool> result = dlg.ShowDialog();
                    if (result == true)
                    {
                        kZeile.audiotitel.Audio_Titel.Pfad = dlg.FileName;
                        kZeile.audioZeile.chkTitel.Content = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                        kZeile.audioZeile.chkTitel.ToolTip =
                            (System.IO.Path.GetDirectoryName(dlg.FileName).Substring(1, 1) != ":") ?
                            "[Standard-Verzeichnis]" + @"\" + dlg.FileName : dlg.FileName;
                        Global.ContextAudio.Update<Audio_Titel>(kZeile.audiotitel.Audio_Titel);
                    }               
                    break;
            }
        }

        private void _chkAnzDateienInDir(GruppenObjekt grpobj)
        {
            Mouse.OverrideCursor = Cursors.AppStarting;
            if (_chkAnzDateien._bkworker.IsBusy)
            {
                _chkAnzDateien._bkworker.CancelAsync();
                _chkAnzDateien._bkworker.Dispose();
            }
            _chkAnzDateien.grpobj = grpobj;
            _chkAnzDateien.aPlaylist = AktKlangPlaylist;
            _chkAnzDateien._bkworker = new BackgroundWorker();
            _chkAnzDateien._bkworker.WorkerReportsProgress = true;
            _chkAnzDateien._bkworker.WorkerSupportsCancellation = true;
            _chkAnzDateien._bkworker.DoWork += new DoWorkEventHandler(_bkworkerCHKAnzDateien_DoWork);
            _chkAnzDateien._bkworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkworkerCHKAnzDateien_RunWorkerCompleted);
            _chkAnzDateien._bkworker.RunWorkerAsync();
        }
        
        private void _bkworkerCHKAnzDateien_DoWork(object sender, DoWorkEventArgs e)
        {
            _chkAnzDateien.titelliste = Global.ContextAudio.LoadTitelByPlaylist(_chkAnzDateien.aPlaylist);

            if (_chkAnzDateien.titelliste != null && _chkAnzDateien.titelliste.Count > 0)
            {
                _chkAnzDateien.titelRef = _chkAnzDateien.titelliste[0].Pfad.LastIndexOf(@"\") != -1 ?
                    _chkAnzDateien.titelliste[0].Pfad.Substring(0, _chkAnzDateien.titelliste[0].Pfad.LastIndexOf(@"\")) : _chkAnzDateien.titelliste[0].Pfad;
                _chkAnzDateien.titelRef = (_chkAnzDateien.titelRef.Substring(1, 1) != ":") ? stdPfad + @"\" + _chkAnzDateien.titelRef : _chkAnzDateien.titelRef;

                _chkAnzDateien.titelliste.ForEach(delegate(Audio_Titel aTitel)
                {
                    string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;

                    while (!vergleich.StartsWith(_chkAnzDateien.titelRef))
                    {
                        if (_chkAnzDateien.titelRef.Contains(@"\"))
                            _chkAnzDateien.titelRef = _chkAnzDateien.titelRef.Substring(0, _chkAnzDateien.titelRef.LastIndexOf(@"\"));
                        else break;
                    }
                });
                if (Directory.Exists(_chkAnzDateien.titelRef))
                {
                    if (_chkAnzDateien.grpobj.aPlaylist != AktKlangPlaylist) return;
                    _chkAnzDateien.allFilesMP3 = Directory.GetFiles(_chkAnzDateien.titelRef, "*.mp3", SearchOption.AllDirectories);
                    if (_chkAnzDateien.grpobj.aPlaylist != AktKlangPlaylist) return;
                    _chkAnzDateien.allFilesWAV = Directory.GetFiles(_chkAnzDateien.titelRef, "*.wav", SearchOption.AllDirectories);
                    if (_chkAnzDateien.grpobj.aPlaylist != AktKlangPlaylist) return;
                    _chkAnzDateien.allFilesOGG = Directory.GetFiles(_chkAnzDateien.titelRef, "*.ogg", SearchOption.AllDirectories);
                    if (_chkAnzDateien.grpobj.aPlaylist != AktKlangPlaylist) return;
                    _chkAnzDateien.allFilesWMA = Directory.GetFiles(_chkAnzDateien.titelRef, "*.wma", SearchOption.AllDirectories);
                    if (_chkAnzDateien.grpobj.aPlaylist != AktKlangPlaylist) return;
                    _chkAnzDateien.allFiles = _chkAnzDateien.allFilesMP3.Length + _chkAnzDateien.allFilesOGG.Length + _chkAnzDateien.allFilesWAV.Length + _chkAnzDateien.allFilesWMA.Length;

                    _chkAnzDateien.gefundenFilesMP3 = _chkAnzDateien.allFilesMP3.Length;
                    _chkAnzDateien.gefundenFilesWAV = _chkAnzDateien.allFilesWAV.Length;
                    _chkAnzDateien.gefundenFilesOGG = _chkAnzDateien.allFilesOGG.Length;
                    _chkAnzDateien.gefundenFilesWMA = _chkAnzDateien.allFilesWMA.Length;
                    foreach (Audio_Titel aTitel in _chkAnzDateien.titelliste.FindAll(t => t.Pfad.ToLower().EndsWith(".mp3")))
                        if (_chkAnzDateien.allFilesMP3.Contains((aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad))
                            _chkAnzDateien.gefundenFilesMP3--;

                    foreach (Audio_Titel aTitel in _chkAnzDateien.titelliste.FindAll(t => t.Pfad.ToLower().EndsWith(".wav")))
                        if (!_chkAnzDateien.allFilesWAV.Contains((aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad))
                            _chkAnzDateien.gefundenFilesWAV--;

                    foreach (Audio_Titel aTitel in _chkAnzDateien.titelliste.FindAll(t => t.Pfad.ToLower().EndsWith(".ogg")))
                        if (!_chkAnzDateien.allFilesOGG.Contains((aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad))
                            _chkAnzDateien.gefundenFilesOGG--;

                    foreach (Audio_Titel aTitel in _chkAnzDateien.titelliste.FindAll(t => t.Pfad.ToLower().EndsWith(".wma")))
                        if (!_chkAnzDateien.allFilesWMA.Contains((aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad))
                            _chkAnzDateien.gefundenFilesWMA--;
                }
            }            
        }

        private void _bkworkerCHKAnzDateien_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = null;
                if (e.Error == null)
                {
                    if (_chkAnzDateien.grpobj.aPlaylist == AktKlangPlaylist &&
                        Directory.Exists(_chkAnzDateien.titelRef) &&
                        _chkAnzDateien.aPlaylist == AktKlangPlaylist &&
                        _chkAnzDateien.allFiles != _chkAnzDateien.titelliste.Count)
                    {
                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.Tag = _chkAnzDateien.titelRef;
                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.IsEnabled = true;

                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.ToolTip = _chkAnzDateien.titelRef + Environment.NewLine + "Update der Titel im o.g. Verzeichnis" + Environment.NewLine +
                            _chkAnzDateien.titelliste.Count + " Dateien sind in der Playlist vorhanden." + Environment.NewLine +
                            _chkAnzDateien.allFiles + " Sound-Dateien wurden incl. den Unterverzeichnisse gefunden" + Environment.NewLine + Environment.NewLine +
                            _chkAnzDateien.gefundenFilesMP3 + " neue MP3-Dateien von " + _chkAnzDateien.allFilesMP3.Length + " gefunden." + Environment.NewLine +
                            _chkAnzDateien.gefundenFilesOGG + " neue OGG-Dateien von " + _chkAnzDateien.allFilesOGG.Length + " gefunden." + Environment.NewLine +
                            _chkAnzDateien.gefundenFilesWAV + " neue WAV-Dateien von " + _chkAnzDateien.allFilesWAV.Length + " gefunden." + Environment.NewLine +
                            _chkAnzDateien.gefundenFilesWMA + " neue WMA-Dateien von " + _chkAnzDateien.allFilesWMA.Length + " gefunden." + Environment.NewLine + Environment.NewLine +
                            "Klicken um alle Dateien neu gefundenen Dateien zu integrieren.";
                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.Visibility = Visibility.Visible;
                    }
                    else
                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;
                }
                else
                    _chkAnzDateien.grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                (sender as BackgroundWorker).Dispose();
                _chkAnzDateien.grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;
            }
        }
        
        private void CheckBtnGleicherPfad(GruppenObjekt grpobj)
        {
            if (grpobj == null) return;
            MyTimer.start_timer();
            grpobj.btnKlangUpdateFiles.Tag = null;
            grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;

            List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            if (titelliste.Count > 0)
            {
                string titelRef = titelliste[0].Pfad.LastIndexOf(@"\") != -1 ? titelliste[0].Pfad.Substring(0, titelliste[0].Pfad.LastIndexOf(@"\")) : titelliste[0].Pfad;
                titelRef = (titelRef.Substring(1, 1) != ":") ? stdPfad + @"\" + titelRef : titelRef;

                titelliste.ForEach(delegate(Audio_Titel aTitel)
                {
                    string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;

                    while (!vergleich.StartsWith(titelRef))
                    {
                        if (titelRef.Contains(@"\"))
                            titelRef = titelRef.Substring(0, titelRef.LastIndexOf(@"\"));
                        else break;
                    }
                });

                if (CheckAlleTitelGleicherPfad(titelRef, titelliste))
                {
                    MyTimer.stop_timer();
                    _chkAnzDateienInDir(grpobj);
                }
            }
            MyTimer.stop_timer();
        }

        private bool CheckAlleTitelGleicherPfad(string pfad, List<Audio_Titel> lstTitel)
        {
            bool pfadeGleich = true;
            lstTitel.ForEach(delegate(Audio_Titel aTitel)
            {
                string file = aTitel.Pfad;
                if (file.Substring(1, 1) != ":")
                {
                    if (stdPfad.EndsWith(@"\"))
                        file = stdPfad + file;
                    else
                        file = stdPfad + @"\" + file;
                }
                if (!file.StartsWith(pfad)) pfadeGleich = false;
            });
            return pfadeGleich;
        }

        private void CheckAlleAngehakt(GruppenObjekt grpobj)
        {
            grpobj.chkbxTopAktiv.IsChecked = (grpobj._listZeile.Count != 0 &&
                grpobj._listZeile.Count == grpobj._listZeile.Count(t => t.audioZeile.chkTitel.IsChecked == true)) ?
                true : false;

            grpobj.chkbxTopVolChange.IsChecked = (grpobj._listZeile.Count != 0 &&
                 grpobj._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked == true).Count ==
                 grpobj.anzVolChange) ? true : false;

            grpobj.chkbxTopPauseChange.IsChecked = (grpobj._listZeile.Count != 0 &&
                grpobj._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked == true).Count ==
                grpobj.anzPauseChange) ? true : false;
        }

        private void AlleKlangSongsAus(GruppenObjekt grpobj, bool checkboxAus, bool ZeileLoeschen)
        {
            if (grpobj == null || !grpobj.wirdAbgespielt)
                return;

            grpobj._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).ForEach(delegate(KlangZeile kZeile)
            {
                if (checkboxAus)
                {
                    kZeile.audioZeile.chkTitel.Click -= chkTitel0_0_Click;
                    kZeile.audioZeile.chkTitel.IsChecked = false;
                }
            });
            grpobj._listZeile.FindAll(t => t.istLaufend).ForEach(delegate(KlangZeile kZeile)
            {
                if (kZeile._mplayer != null)
                {

                    if (!grpobj.istMusik)
                    {
                        kZeile._mplayer.MediaEnded -= Player_Ended;
                        kZeile._mplayer.Stop();
                        if (ZeileLoeschen)
                            kZeile._mplayer.Close();
                        if (!ZeileLoeschen)
                            kZeile._mplayer.MediaEnded += Player_Ended;
                    }
                    else
                    {
                        if (!kZeile.FadingOutStarted)
                        {
                            kZeile.FadingOutStarted = true;
                            FadingOut(kZeile, true, true);
                        }
                    }
                }
                kZeile.istLaufend = false;
                kZeile.istPause = false;

                kZeile.audioZeile.pbarTitel.Maximum = 100;
                kZeile.audioZeile.pbarTitel.Value = 0;

                if (ZeileLoeschen)
                {
                    grpobj.wpnl.Children.Clear();
                    grpobj._listZeile.Clear();
                }
            });
            grpobj.wirdAbgespielt = false;
            GC.GetTotalMemory(true);                //GC update (Memory wird aktualisiert)
        }

        private Int16 GetPosObjGruppe(int objGruppe)
        {
            Int16 posObjGruppe = Convert.ToInt16(_GrpObjecte.FindIndex(t => t.objGruppe == objGruppe));
            return posObjGruppe;
        }
       /* private Int16 GetPosObjGruppe(int erstellt)
        {
            Int16 posObjGruppe = Convert.ToInt16(_GrpObjecte.FindIndex(t => t.objGruppe == erstellt));
            return posObjGruppe;
        }*/

        private void PlaylisteLeeren(GruppenObjekt grpobj)
        {
            if (grpobj == null)
                return;

            if (AktKlangPlaylist != null)
            {
                if (_BGPlayer.AktPlaylist == AktKlangPlaylist && grpobj.wirdAbgespielt)
                {
                    btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    lbBackground.SelectedIndex = -1;
                }
                AlleKlangSongsAus(grpobj, true, true);

                ZeigeKlangSongsParallel(grpobj, false);

                grpobj.wirdAbgespielt = (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen);
            }
            if (grpobj._listZeile.Count > 0)
            {
                grpobj.wpnl.Children.RemoveRange(0, grpobj._listZeile.Count);
                grpobj._listZeile.RemoveRange(0, grpobj._listZeile.Count);
            }
            grpobj.istMusik = true;
            grpobj.anzVolChange = 0;
            grpobj.anzPauseChange = 0;
            grpobj.NochZuSpielen.Clear();
            grpobj.Gespielt.Clear();
        }

        public UIElement DeepCopy(UIElement element, string oldValue, string newValue)
        {
            string shapestring;
            shapestring = XamlWriter.Save(element);
            if (oldValue != null)
                shapestring = shapestring.Replace(oldValue, newValue);

            StringReader stringReader = new StringReader(shapestring);
            XmlReader xmlTextReader = new XmlTextReader(stringReader);
            UIElement DeepCopyobject = (UIElement)XamlReader.Load(xmlTextReader);
            return DeepCopyobject;
        }

        private void ZeigeKlangTop(GruppenObjekt grpobj, bool sichtbar)
        {
            grpobj.spnlTopGeräuschIcon.Visibility = sichtbar ? Visibility.Visible : Visibility.Hidden;
        }

        private void ZeigeKlangSongsParallel(GruppenObjekt grpobj, bool sichtbar)
        {
            if (sichtbar && grpobj != null)
            {
                grpobj.gboxTopSongsParallel.Visibility = Visibility.Visible;
                grpobj.tbTopKlangSongsParallel.Visibility = Visibility.Visible;
                grpobj.btnTopSongParMinus.Visibility = Visibility.Visible;
                grpobj.btnTopSongParPlus.Visibility = Visibility.Visible;

                grpobj.tbTopKlangSongsParallel.Text = Convert.ToString(grpobj.aPlaylist != null?grpobj.aPlaylist.MaxSongsParallel: 1);
                grpobj.tbtnKlangPause.Visibility = Visibility.Visible;
                if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen && ((TabItem)tcEditor.SelectedItem).Name == "tiEditor")
                {
                    grpobj.sollBtnGedrueckt++;
                    if (grpobj.wirdAbgespielt)
                        tbtnKlangPause_Unchecked(grpobj.tbtnKlangPause, new RoutedEventArgs());
                    else
                        tbtnKlangPause_Checked(grpobj.tbtnKlangPause, new RoutedEventArgs());
                    //btnKlangPauseX_Click(grpobj.tbtnKlangPause, new RoutedEventArgs());
                }
            }
            else
            {
                if (grpobj == null)
                    lbEditor.SelectedIndex = -1;
                else
                {
                    grpobj.gboxTopSongsParallel.Visibility = Visibility.Hidden;
                    grpobj.tbTopKlangSongsParallel.Visibility = Visibility.Hidden;
                    grpobj.btnTopSongParPlus.Visibility = Visibility.Hidden;
                    grpobj.btnTopSongParMinus.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ZeigeZeileKlangSpalten(GruppenObjekt grpobj, bool sichtbar)
        {
            grpobj._listZeile.ForEach(delegate(KlangZeile kZeile)
            {
                if (kZeile.audioZeile.lbiEditorRow != null)
                {
                    if (!sichtbar)
                    {
                        if (kZeile.audioZeile.grdEditorRow.ColumnDefinitions[3].Width != new GridLength(0))
                        {
                            for (int i = kZeile.audioZeile.grdEditorRow.ColumnDefinitions.Count - 1; i >= 3; i--)
                                kZeile.audioZeile.grdEditorRow.ColumnDefinitions[i].Width = new GridLength(0);
                            kZeile.audioZeile.brdTrennstrich.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        if (kZeile.audioZeile.grdEditorRow.ColumnDefinitions[3].Width != new GridLength(grpobj.grdEditorTop.ColumnDefinitions[1].Width.Value))
                        {
                            for (int i = kZeile.audioZeile.grdEditorRow.ColumnDefinitions.Count - 1; i >= 3; i--)
                                kZeile.audioZeile.grdEditorRow.ColumnDefinitions[i].Width = new GridLength(grpobj.grdEditorTop.ColumnDefinitions[i + 1].Width.Value);

                            kZeile.audioZeile.grdEditorRow.ColumnDefinitions[2].MinWidth = grpobj.grdEditorTop.ColumnDefinitions[2].MinWidth;
                            kZeile.audioZeile.brdTrennstrich.Visibility = Visibility.Visible;
                        }
                    }
                }
            });
        }


        private void rsldTeilZeile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_GrpObjecte.Count > 0 && _GrpObjecte[0] != null)
                {
                    //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                    KlangZeile kZeile = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile.FirstOrDefault(t => t.audioZeile.rsldTeilSong == (SliderRange)sender);

                    kZeile.audiotitel.TeilStart = kZeile.audioZeile.rsldTeilSong.LowerValue;
                    kZeile.audiotitel.TeilEnde = kZeile.audioZeile.rsldTeilSong.UpperValue;

                    Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.audiotitel);
                }
            }
            catch (Exception) {}
        }

        private double GetMusikdateiLength(string musikdatei)
        {
            double max = -2;
            MediaPlayer mp = new MediaPlayer();

            if (Directory.Exists(System.IO.Path.GetDirectoryName(musikdatei)) &&
                    File.Exists(musikdatei))
            {
                mp.Volume = 0;
                mp.Open(new Uri(musikdatei));
                mp.Play();
                if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 4000))
                {
                    mp.Pause();
                    max = mp.NaturalDuration.TimeSpan.TotalMilliseconds;
                }
                mp.Stop();
                mp.Close();
                return max;
            }
            else
                return -1;
        }

        private void chkKlangZeileCMenuNurTeil_Checked(object sender, RoutedEventArgs e)
        {
            try
            {                
                KlangZeile kZeile = (KlangZeile)((ContextMenu)((CheckBox)sender).Parent).Tag;

                MyTimer.start_timer();
                double max = GetMusikdateiLength(GetFullPath(kZeile.audiotitel.Audio_Titel.Pfad));                
                MyTimer.stop_timer();

                if (max > 0)
                {
                    kZeile.audiotitel.TeilAbspielen = true;
                    kZeile.audioZeile.rsldTeilSong.Minimum = 0;
                    kZeile.audioZeile.rsldTeilSong.Maximum = max;
                    kZeile.audioZeile.rsldTeilSong.UpdateLayout();

                    kZeile.audioZeile.rsldTeilSong.LowerValue = 0;
                    kZeile.audioZeile.rsldTeilSong.UpperValue = max;
                    kZeile.audioZeile.rsldTeilSong.UpdateLayout();

                    kZeile.audioZeile.rsldTeilSong.Visibility = Visibility.Visible;

                    ((ContextMenu)((CheckBox)sender).Parent).IsOpen = false;
                    Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.audiotitel);
                }
                else
                {
                    ((CheckBox)sender).Unchecked -= chkKlangZeileCMenuNurTeil_Unchecked;
                    ((CheckBox)sender).IsChecked = false;
                    ((CheckBox)sender).Unchecked += chkKlangZeileCMenuNurTeil_Unchecked;
                    string meldung;
                    if (max == -1)
                        meldung = "Die Länge der Musikdatei konnte nicht ermittelt werden. Überprüfen Sie den Pfad und wiederholen Sie den Vorgang.";
                    else
                        meldung = "Die Laufzeit von 4sek. um die Länge der Musikdatei zu ermitteln wurde überschritten. Wiederholen Sie den Vorgang zu einem späteren Zeitpunkt.";
                    
                    var errWin = new MsgWindow("Datenfehler", meldung);                    
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
            catch (Exception ex)
            {
                ((CheckBox)sender).Unchecked -= chkKlangZeileCMenuNurTeil_Unchecked;
                ((CheckBox)sender).IsChecked = false;
                ((CheckBox)sender).Unchecked += chkKlangZeileCMenuNurTeil_Unchecked; 
                var errWin = new MsgWindow("Datenfehler", "Der Slider für einen Teil des Titel abzuspielen, konnte nicht aktiviert werden.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void chkKlangZeileCMenuNurTeil_Unchecked(object sender, RoutedEventArgs e)
        {
            KlangZeile kZeile = (KlangZeile)((ContextMenu)((CheckBox)sender).Parent).Tag;
            kZeile.audiotitel.TeilAbspielen = false;
            kZeile.audioZeile.rsldTeilSong.Visibility = Visibility.Hidden;
            ((ContextMenu)((CheckBox)sender).Parent).IsOpen = false;

            Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.audiotitel);
        }


        private void KlangNewRow(string songdatei, GruppenObjekt grpobj, UInt16 row, Audio_Playlist_Titel playlisttitel)
        {
            bool neuerstellen = true;
            if (grpobj == null)
                return;
            int objGruppe = grpobj.objGruppe;

            if (grpobj.visuell && ((ListBoxItem)this.FindName("lbiEditorRow" + objGruppe + "_" + row)) != null)
                neuerstellen = false;

            KlangZeile klZeile = new KlangZeile(rowErstellt);
            
            klZeile.audiotitel = playlisttitel;
            klZeile._mplayer = new MediaPlayer();
            
            //Mögliche Variante zu MediaPlayer
            //MediaElement mp = new MediaElement();
            //mp.Source = new Uri(@"Resources\1.mp3", UriKind.RelativeOrAbsolute);
            //mp.LoadedBehavior = MediaState.Manual;
            
            klZeile._mplayer.MediaEnded += Player_Ended;
            klZeile._mplayer.MediaFailed += Player_KlangMediaFailed;
            klZeile.mediaHashCode = klZeile._mplayer.GetHashCode();

            if (grpobj.visuell)
            {
                klZeile.audioZeile = new AudioZeile();
                klZeile.audioZeile.BeginInit();

                klZeile.audioZeile.Name = "audioZeile" + objGruppe + "_" + row;
                klZeile.audioZeile.Tag = klZeile.ID_Zeile;

                klZeile.audioZeile.lbiEditorRow.Tag = rowErstellt;

                //*************************************************************************************************
                //Papierkorb
                klZeile.audioZeile.imgTrash.Tag = klZeile;
                klZeile.audioZeile.chkTitel.Content = System.IO.Path.GetFileNameWithoutExtension(songdatei);

                if (playlisttitel.Aktiv)
                    klZeile.audioZeile.chkTitel.IsChecked = playlisttitel.Aktiv;

                Label lbl1 = new Label();
                lbl1.Content = "Datei entfernen";
                lbl1.Tag = 1;
                lbl1.Padding = new Thickness(3);
                lbl1.MouseDown += lblKlangZeileCMenuÄndern_MouseDown;

                Label lbl2 = new Label();
                lbl2.Content = "Dateipfad ändern";
                lbl2.Tag = 2;
                lbl1.Padding = new Thickness(3);
                lbl2.MouseDown += lblKlangZeileCMenuÄndern_MouseDown;

                CheckBox chk3 = new CheckBox();
                chk3.Content = "Nur einen Teil abspielen";
                chk3.Tag = 3;
                chk3.VerticalAlignment = VerticalAlignment.Center;
                if (klZeile.audiotitel.TeilAbspielen)
                    chk3.IsChecked = true;
                chk3.Checked += chkKlangZeileCMenuNurTeil_Checked;
                chk3.Unchecked += chkKlangZeileCMenuNurTeil_Unchecked;

                ContextMenu cMenu = new System.Windows.Controls.ContextMenu();
                cMenu.Tag = klZeile;
                cMenu.Items.Add(lbl1);
                cMenu.Items.Add(lbl2);
                cMenu.Items.Add(chk3);

                klZeile.audioZeile.chkTitel.Tag = (System.IO.Path.GetDirectoryName(songdatei).StartsWith(stdPfad)) ?
                    songdatei.Substring(stdPfad.Length) : songdatei;
                string s = System.IO.Path.GetDirectoryName(songdatei);

                klZeile.audioZeile.chkTitel.ToolTip =
                    ((s.Length == 0 || s.Length >= 2 && s.Substring(1, 1) != ":") ?
                    "[Standard-Verzeichnis]" + @"\" + songdatei : songdatei);
                klZeile.audioZeile.chkTitel.ContextMenu = cMenu;
                klZeile.audioZeile.pbarTitel.ContextMenu = cMenu;

                // Schieberegler Lautstärke
                klZeile.audioZeile.sldKlangVol.Minimum = 0;
                klZeile.audioZeile.sldKlangVol.Maximum = 100;
                klZeile.audioZeile.sldKlangVol.Value = playlisttitel.Volume;
                klZeile.audioZeile.sldKlangVol.Tag = rowErstellt;
                klZeile.audioZeile.sldKlangVol.ToolTip = Math.Round(klZeile.audioZeile.sldKlangVol.Value) + " %";
                //Checkbox Automatisch veränderbare Lautstärke
                klZeile.audioZeile.chkVolMove.IsChecked = playlisttitel.VolumeChange;

                klZeile.audioZeile.tboxVolMin.Text = Convert.ToString(playlisttitel.VolumeMin);
                klZeile.audioZeile.tboxVolMin.Tag = row;
                klZeile.audioZeile.tboxVolMax.Text = Convert.ToString(playlisttitel.VolumeMax);
                klZeile.audioZeile.tboxVolMax.Tag = row;

                klZeile.audioZeile.sldKlangPause.Value = playlisttitel.Pause;
                klZeile.audioZeile.sldKlangPause.Tag = rowErstellt;
                klZeile.audioZeile.sldKlangPause.ToolTip = (playlisttitel.Pause < 1000) ? playlisttitel.Pause + " ms" : playlisttitel.Pause / 1000 + " sek.";
                // Checkbox veränderbare Zwischenpause
                klZeile.audioZeile.chkKlangPauseMove.IsChecked = playlisttitel.PauseChange;
                klZeile.audioZeile.chkKlangPauseMove.Tag = row;
                klZeile.audioZeile.tboxPauseMin.Text = Convert.ToString(playlisttitel.PauseMin);
                klZeile.audioZeile.tboxPauseMin.Tag = row;
                klZeile.audioZeile.tboxPauseMax.Text = Convert.ToString(playlisttitel.PauseMax);
                klZeile.audioZeile.tboxPauseMax.Tag = row;
                klZeile.audioZeile.sldPlaySpeed.Value = playlisttitel.Speed;

                string geschw = "Abspielgeschwindigkeit: ";
                geschw += klZeile.audioZeile.sldPlaySpeed.Value == .1 ? "sehr langsam" :
                          klZeile.audioZeile.sldPlaySpeed.Value == .5 ? "langsam" :
                          klZeile.audioZeile.sldPlaySpeed.Value == .75 ? "gedrosselt" :
                          klZeile.audioZeile.sldPlaySpeed.Value == 1 ? "normal" :
                          klZeile.audioZeile.sldPlaySpeed.Value == 2 ? "erhöht" :
                          klZeile.audioZeile.sldPlaySpeed.Value == 3 ? "schnell" :
                          klZeile.audioZeile.sldPlaySpeed.Value == 4 ? "sehr schnell" :
                          klZeile.audioZeile.sldPlaySpeed.Value == 5 ? "utlra schnell" : "nicht definiert";
                klZeile.audioZeile.sldPlaySpeed.ToolTip = geschw;
                klZeile.audioZeile.sldPlaySpeed.Tag = row;

                klZeile.audioZeile.grdEditorRow.Name += objGruppe + "_" + row;
                klZeile.audioZeile.lbiEditorRow.Name += objGruppe + "_" + row;
                klZeile.audioZeile.imgTrash.Name += objGruppe + "_" + row;
                klZeile.audioZeile.chkTitel.Name += objGruppe + "_" + row;
                klZeile.audioZeile.sldKlangVol.Name += objGruppe + "_" + row;
                klZeile.audioZeile.chkVolMove.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnVolMinMinus.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnVolMinPlus.Name += objGruppe + "_" + row;
                klZeile.audioZeile.tboxVolMin.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnVolMaxMinus.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnVolMaxPlus.Name += objGruppe + "_" + row;
                klZeile.audioZeile.tboxVolMax.Name += objGruppe + "_" + row;
                klZeile.audioZeile.sldKlangPause.Name += objGruppe + "_" + row;
                klZeile.audioZeile.chkKlangPauseMove.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnPauseMinMinus.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnPauseMinPlus.Name += objGruppe + "_" + row;
                klZeile.audioZeile.tboxPauseMin.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnPauseMaxMinus.Name += objGruppe + "_" + row;
                klZeile.audioZeile._btnPauseMaxPlus.Name += objGruppe + "_" + row;
                klZeile.audioZeile.tboxPauseMin.Name += objGruppe + "_" + row;
                klZeile.audioZeile.tboxPauseMax.Name += objGruppe + "_" + row;
                klZeile.audioZeile.sldPlaySpeed.Name += objGruppe + "_" + row;

                klZeile.audioZeile.lblDauer.Content = playlisttitel.Audio_Titel.Länge != null ?
                    TimeSpan.FromMilliseconds(playlisttitel.Audio_Titel.Länge.Value).ToString(@"mm\:ss") : "--:--";

                for (int i = 0; i < klZeile.audiotitel.Rating; i++)
                    klZeile.audioZeile.btnGewichtung.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                //klZeile.audioZeile.btnGewichtung.Tag = klZeile.audiotitel.Rating;

                if (neuerstellen)
                {
                    klZeile.audioZeile.imgTrash.MouseUp += imgTrash0_0_MouseUp;
                    klZeile.audioZeile.chkTitel.Click += chkTitel0_0_Click;
                    klZeile.audioZeile.sldKlangVol.ValueChanged += sldKlangVol0_X_ValueChanged;
                    klZeile.audioZeile.chkVolMove.Click += chkVolMove0_0_Click;
                    klZeile.audioZeile._btnVolMinMinus.Click += _btnVolMinMinus0_X_Click;
                    klZeile.audioZeile._btnVolMinPlus.Click += _btnVolMinPlus0_X_Click;
                    klZeile.audioZeile.tboxVolMin.PreviewTextInput += tboxVolMin0_X_PreviewTextInput;
                    klZeile.audioZeile.tboxVolMin.LostFocus += tboxVolMin0_X_LostFocus;
                    klZeile.audioZeile._btnVolMaxMinus.Click += _btnVolMaxMinus0_X_Click;
                    klZeile.audioZeile._btnVolMaxPlus.Click += _btnVolMaxPlus0_X_Click;
                    klZeile.audioZeile.tboxVolMax.PreviewTextInput += tboxVolMin0_X_PreviewTextInput;
                    klZeile.audioZeile.tboxVolMax.LostFocus += tboxVolMax0_X_LostFocus;
                    klZeile.audioZeile.sldKlangPause.ValueChanged += sldKlangPause0_X_ValueChanged;
                    klZeile.audioZeile.chkKlangPauseMove.Click += chkKlangPauseMove0_0_Click;
                    klZeile.audioZeile._btnPauseMinMinus.Click += _btnPauseMinMinus0_X_Click;
                    klZeile.audioZeile._btnPauseMinPlus.Click += _btnPauseMinPlus0_X_Click;
                    klZeile.audioZeile.tboxPauseMin.PreviewTextInput += tboxVolMin0_X_PreviewTextInput;
                    klZeile.audioZeile._btnPauseMaxMinus.Click += _btnPauseMaxMinus0_X_Click;
                    klZeile.audioZeile._btnPauseMaxPlus.Click += _btnPauseMaxPlus0_X_Click;
                    klZeile.audioZeile.tboxPauseMin.LostFocus += tboxPauseMin0_X_LostFocus;
                    klZeile.audioZeile.tboxPauseMax.PreviewTextInput += tboxVolMin0_X_PreviewTextInput;
                    klZeile.audioZeile.tboxPauseMax.LostFocus += tboxPauseMax0_X_LostFocus;
                    klZeile.audioZeile.sldPlaySpeed.ValueChanged += sldPlaySpeed0_X_ValueChanged;
                    klZeile.audioZeile.rsldTeilSong.PreviewMouseLeftButtonUp += rsldTeilZeile_PreviewMouseLeftButtonUp;
                    klZeile.audioZeile.btnGewichtung.Click += btnEditorGewichtung_Click;
                }

                klZeile.audioZeile.EndInit();
                grpobj.wpnl.Children.Add(klZeile.audioZeile);
                //*************************************************************************************************
            }

            klZeile.pauseMin_wert = Convert.ToInt16(playlisttitel.PauseMin); // klZeile.audioZeile.tboxPauseMin.Text);
            klZeile.pauseMax_wert = Convert.ToInt16(playlisttitel.PauseMax); //klZeile.audioZeile.tboxPauseMax.Text);
            klZeile.volMin_wert = Convert.ToInt16(playlisttitel.VolumeMin); //klZeile.audioZeile.tboxVolMin.Text);
            klZeile.volMax_wert = (Convert.ToInt16(playlisttitel.VolumeMax) >= klZeile.volMin_wert) ?  //klZeile.audioZeile.tboxVolMax.Text
                Convert.ToInt16(playlisttitel.VolumeMax) : klZeile.volMin_wert;  //klZeile.audioZeile.tboxVolMax.Text) 
            klZeile.Aktuell_Volume = playlisttitel.Volume;
            klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

            klZeile.playspeed = playlisttitel.Speed; // klZeile.audioZeile.sldPlaySpeed.Value;


            if (playlisttitel.Aktiv && !grpobj.istMusik)
                klZeile.istStandby = true;
            else
                klZeile.istStandby = false;

            if (!grpobj.wirdAbgespielt)
                klZeile.istPause = true;

            klZeile.playable = playlisttitel.Aktiv;// klZeile.audioZeile.chkTitel.IsChecked.Value;

            grpobj._listZeile.Add(klZeile);
            if (grpobj.visuell && klZeile.audiotitel.TeilAbspielen)
            {
                klZeile.audioZeile.rsldTeilSong.Visibility = Visibility.Visible;     
                klZeile.audioZeile.rsldTeilSong.Minimum = 0;
                klZeile.audioZeile.rsldTeilSong.Maximum = klZeile.audiotitel.Länge;
                klZeile.audioZeile.rsldTeilSong.UpdateLayout();
                
                klZeile.audioZeile.rsldTeilSong.LowerValue = klZeile.audiotitel.TeilStart.Value; 
                klZeile.audioZeile.rsldTeilSong.UpperValue = klZeile.audiotitel.TeilEnde.Value;
                klZeile.audioZeile.rsldTeilSong.brdLine.Margin = new Thickness(
                    (klZeile.audioZeile.rsldTeilSong.ActualWidth - 17) / (klZeile.audioZeile.rsldTeilSong.LowerSlider.Maximum / klZeile.audioZeile.rsldTeilSong.LowerSlider.Value), 0,
                    klZeile.audioZeile.rsldTeilSong.ActualWidth - 17 - (klZeile.audioZeile.rsldTeilSong.ActualWidth - 17) / (klZeile.audioZeile.rsldTeilSong.LowerSlider.Maximum / klZeile.audioZeile.rsldTeilSong.UpperSlider.Value), 0);
            }
            if (playlisttitel.VolumeChange) grpobj.anzVolChange++; // klZeile.audioZeile.chkVolMove.IsChecked == true)
            if (playlisttitel.PauseChange) grpobj.anzPauseChange++; // klZeile.audioZeile.chkKlangPauseMove.IsChecked == true)
            rowErstellt++;
        }

        private void abspielProzess(GruppenObjekt grpObj, bool checkStatus, KlangZeile klZeile, RoutedEventArgs e)
        {
            grpObj.wirdAbgespielt = false;
            string file = klZeile.audiotitel.Audio_Titel.Pfad; // (sender as CheckBox).Tag.ToString();
            if (file.Substring(1, 1) != ":")
            {
                if (stdPfad.EndsWith(@"\"))
                    file = stdPfad + file;
                else
                    file = stdPfad + @"\" + file;
            }
            try
            {
                if (e == null || e.Source == null) //!klZeile.istPause &&
                {
                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(file)) || !File.Exists(file))
                    {
                        if (grpObj.visuell)
                        {
                            klZeile.audioZeile.lbiEditorRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));       // Red
                            klZeile.audioZeile.lbiEditorRow.ToolTip = "Datei nicht gefunden. -> " +
                            klZeile.audiotitel.Audio_Titel.Pfad;
                        }
                        klZeile.playable = false;
                        klZeile.istLaufend = false;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.audiotitel.Audio_TitelGUID));// .ID_Zeile));

                        foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                        {
                            if ((Guid)mZeile.Tag == grpObj.aPlaylist.Audio_PlaylistGUID)// ._listZeile[zeile].audiotitel.Audio_TitelGUID)
                            {
                                mZeile.spnlMusikZeile.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 80));   // Brushes.Yellow
                                mZeile.spnlMusikZeile.ToolTip = grpObj._listZeile.FindAll(t => t.playable).Count + " von " + grpObj._listZeile.FindAll(t => t.audiotitel.Aktiv).Count + " Titel abspielbar";
                            }
                        }
                        //CheckPlayStandbySongs(grpObj); //GetPosObjGruppe(grpObj.objGruppe));
                    }
                    else
                    {
                        klZeile.playable = true;
                        if (grpObj.visuell) klZeile.audioZeile.lbiEditorRow.Background = null;
                        //okay if abfrage unten
                        if (checkStatus)
                        {
                            if (grpObj.aPlaylist.MaxSongsParallel == 0 && grpObj.aPlaylist.Audio_Playlist_Titel.Count > 0)
                            {
                                grpObj.aPlaylist.MaxSongsParallel = 1;
                                Global.ContextAudio.Update<Audio_Playlist>(grpObj.aPlaylist);
                            }
                            if (grpObj.aPlaylist.MaxSongsParallel > grpObj._listZeile.FindAll(t => t.istLaufend == true).Count)
                            {
                                if (grpObj.istMusik)
                                    klZeile.FadingOutStarted = false;
                                grpObj.wirdAbgespielt = true;

                                klZeile._mplayer =
                                    PlayFile(true, klZeile, GetPosObjGruppe(grpObj.objGruppe), klZeile._mplayer, file,
                                        klZeile.Aktuell_Volume, grpObj.istMusik);

                                klZeile.mediaHashCode = klZeile._mplayer.GetHashCode();

                                if (grpObj.visuell)
                                {
                                    if (klZeile._mplayer.NaturalDuration.HasTimeSpan)
                                        klZeile.audioZeile.pbarTitel.Maximum =
                                            klZeile._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                    else
                                        klZeile.audioZeile.pbarTitel.Maximum = 100000;
                                }

                                if (e != null && e.Source != null) klZeile.istStandby = false;

                                klZeile.istLaufend = true;
                                klZeile.istPause = false;
                            }
                            else
                            {
                                klZeile.istStandby = true;
                            }
                        }
                        else
                        {
                            if (klZeile._mplayer != null)
                            {
                                if (grpObj.istMusik)
                                {
                                    if (!klZeile.FadingOutStarted)
                                    {
                                        klZeile.FadingOutStarted = true;
                                        FadingOut(klZeile, true, true);
                                    }
                                }
                                else
                                {
                                    klZeile._mplayer.Stop();
                                    if (!klZeile.audiotitel.TeilAbspielen)
                                        klZeile._mplayer.Close();
                                }
                                klZeile.istStandby = false;
                                klZeile.istLaufend = false;
                                klZeile.istPause = false;
                            }
                            klZeile.audioZeile.pbarTitel.Maximum = 100;
                            klZeile.audioZeile.pbarTitel.Value = 0;

                            if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen)
                                CheckPlayStandbySongs(grpObj); //GetPosObjGruppe(grpObj.objGruppe));
                        }
                        if (grpObj._listZeile.FindAll(t => t.istLaufend).Count > 0)
                        {
                            KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(100);
                            KlangProgBarTimer.IsEnabled = true;
                            KlangProgBarTimer.Start();
                        }
                        else
                        {
                            for (int i = 0; i < _GrpObjecte.Count; i++)
                            {
                                if (_GrpObjecte[i]._listZeile.FindAll(t => t.istLaufend).Count > 0)
                                {
                                    KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(100);
                                    KlangProgBarTimer.IsEnabled = true;
                                    KlangProgBarTimer.Start();
                                    break;
                                }
                                else
                                {
                                    KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(1000);
                                }
                            }
                        }
                    }
                }
                else
                {
                    klZeile.audiotitel.Aktiv = checkStatus;
                    klZeile.istStandby = checkStatus;
                    if (checkStatus)
                    {
                        klZeile.istLaufend = false;
                        if (!grpObj.NochZuSpielen.Contains(klZeile.audiotitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= klZeile.audiotitel.Rating; i++)
                                grpObj.NochZuSpielen.Add(klZeile.audiotitel.Audio_TitelGUID);
                        }
                    }
                    else
                    {
                        klZeile._mplayer.Stop();
                        if (!klZeile.audiotitel.TeilAbspielen)
                            klZeile._mplayer.Close();
                                
                        klZeile.istLaufend = false;
                        klZeile.istPause = false;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.audiotitel.Audio_TitelGUID));
                    }
                } 
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Datenfehler",  "Der AbspielProzess konnte nicht ordnungsgemäß durchgeführt werden.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void chkTitel0_0_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte.FindAll(t => t.visuell))
                {
                    if (chkgrpObj._listZeile.FirstOrDefault(t => t.audioZeile.chkTitel == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }                 
                if (grpobj == null)
                    return;

                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FirstOrDefault(t => t.audioZeile.chkTitel == (Control)sender));

                if (e != null && e.Source != null)
                {
                 /*   grpobj._listZeile[zeile].audiotitel.Aktiv = (sender as CheckBox).IsChecked.Value;
                    
                    grpobj._listZeile[zeile].istStandby = (sender as CheckBox).IsChecked.Value;
                    grpobj._listZeile[zeile].istPause = (sender as CheckBox).IsChecked.Value;
                  * */
                    if (!(sender as CheckBox).IsChecked.Value && grpobj._listZeile[zeile].istLaufend)
                    {
                        grpobj._listZeile[zeile]._mplayer.Pause();
                        grpobj._listZeile[zeile]._mplayer.Position = TimeSpan.FromMilliseconds(0);
                        grpobj._listZeile[zeile].istLaufend = false;
                        
                    }
                }
                abspielProzess(grpobj, (sender as CheckBox).IsChecked.Value, grpobj._listZeile[zeile], e);

                if (grpobj.aPlaylist != null)
                {
                    Audio_Playlist_Titel playlisttitel =
                        grpobj.aPlaylist.Audio_Playlist_Titel.Where(t => t.Audio_TitelGUID ==
                            grpobj._listZeile[zeile].audiotitel.Audio_TitelGUID).FirstOrDefault(
                            t => t.Aktiv != (sender as CheckBox).IsChecked.Value);

                    if (playlisttitel != null)
                    {
                        playlisttitel.Aktiv = (sender as CheckBox).IsChecked.Value;
                        if (tcAudioPlayer.SelectedItem == tiEditor)         // Nur Speichern wenn im Tab-Editor
                        {
                            plyTitelToSave.Add(playlisttitel);
                            if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
                        }
                    }
                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e != null && e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
                }
                if (grpobj.visuell)
                {
                    CheckAlleAngehakt(grpobj);
                    CheckPlayStandbySongs(grpobj);
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Auswahlfehler", "Beim der Prozedure 'chkTitel' ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        public void SetzeChangeBit(Audio_Playlist playlist_bezug)
        {
            foreach (GruppenObjekt grpObj in _GrpObjecte.FindAll(t => t.aPlaylist.Audio_PlaylistGUID == playlist_bezug.Audio_PlaylistGUID)
                    .FindAll(t => !t.visuell))
                grpObj.changed = true;

        }

        private void NeueKlangPlaylistInDB()
        {
            string NeuePlaylist = ((TCButtons)tcEditor.SelectedItem)._tbText.Text;

            Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
            playlist.MaxSongsParallel = 1;
            playlist.Name = NeuePlaylist.ToString();
            playlist.Hintergrundmusik = false;

            //zur datenbank hinzufügen
            if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                AktKlangPlaylist = playlist;

            AktualisiereKlangPlaylist();
        }

        private void NeueKlangThemeInDB(string titel)
        {
            string themeName = GetNeuenThemeNamen(titel == "" ? "Neues Theme" : titel);
            Audio_Theme themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(themeName));

            if (themelist == null)
            {
                Audio_Theme theme = Global.ContextAudio.New<Audio_Theme>();
                theme.Name = themeName;
                theme.Hintergrund_VolMod = 50;
                theme.Klang_VolMod = 50;

                //zur datenbank hinzufügen
                if (Global.ContextAudio.Insert<Audio_Theme>(theme))               //erfolgreich hinzugefügt
                {
                    Global.ContextAudio.Update<Audio_Theme>(theme);
                    AktKlangTheme = theme;
                    AktualisiereKlangThemes();
                    for (int i = 0; i <= lbEditorTheme.Items.Count - 1; i++)
                        if ((lbEditorTheme.Items[i] as ListboxItemIcon).lbText.Content.ToString() == theme.Name)
                            lbEditorTheme.SelectedIndex = i;
                    lbEditorTheme.ScrollIntoView(lbEditorTheme.SelectedItem);
                }
            }
            else
            {
                var errWin = new MsgWindow("Datenbankfehler", "Theme evtl. schon vorhanden. Bitte wiederholen Sie den Vorgang und wählen einen anderen Titel");
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private Guid KlangDateiHinzu(string datei)
        {
            string temp_datei = datei;
            if (datei.ToLower().StartsWith(stdPfad.ToLower()))
            {
                temp_datei = datei.Substring(stdPfad.Length);
                if (temp_datei.StartsWith(@"\")) temp_datei = temp_datei.Substring(1);
            }
            
            Audio_Titel titel = Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Pfad == temp_datei);
            if (titel == null)
            {
                titel = Global.ContextAudio.New<Audio_Titel>();     //erstelle ein leeres Titel-Objekt

                //eigenschaften setzen
                string s = System.IO.Path.GetFileNameWithoutExtension(datei);
                titel.Name = s.Length > 100 ? s.Substring(0, 99) : s;
                if (datei.ToLower().StartsWith(stdPfad.ToLower()))
                {
                    titel.Pfad = datei.Substring(stdPfad.Length);
                    if (titel.Pfad.StartsWith(@"\")) titel.Pfad = titel.Pfad.Substring(1);
                }
                else
                    titel.Pfad = datei;
                if (titel.Pfad.Length > 200)
                {
                    MessageBox.Show("Die Datei '" + titel.Pfad + "' kann nicht interiert werden, da der Pfad zu komplex ist (Länge)." + Environment.NewLine + Environment.NewLine +
                        "Bitte kopieren Sie die Datei in einen weniger komplexen Bereich.", "Dateistruktur zu groß", MessageBoxButton.OK, MessageBoxImage.Error);
                    titel.Pfad = titel.Pfad.Substring(0, 199);
                }

                //zur datenbank hinzufügen
                Global.ContextAudio.Insert<Audio_Titel>(titel);
            }

            Global.ContextAudio.AddTitelToPlaylist(AktKlangPlaylist, titel);
            GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);

            grpobj.tbTopKlangSongsParallel.Tag = grpobj._listZeile.Count + 1;

            Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel).Last();
            if (playlisttitel != null)
            {
                playlisttitel.VolumeChange = false;
                playlisttitel.Volume = 50;
                playlisttitel.VolumeMin = 0;
                playlisttitel.VolumeMax = 100;

                playlisttitel.PauseChange = false;
                playlisttitel.Pause = 1000;
                playlisttitel.PauseMin = 100;
                playlisttitel.PauseMax = 10000;
                playlisttitel.Speed = 1;

                KlangNewRow(datei, grpobj, Convert.ToUInt16(grpobj._listZeile.Count), playlisttitel);
                
                if (playlisttitel.Aktiv)
                {
                    for (int i = 0; i < playlisttitel.Rating; i++)
                        grpobj.NochZuSpielen.Add(grpobj._listZeile[grpobj._listZeile.Count].audiotitel.Audio_TitelGUID);
                }

                if (AktKlangPlaylist.Hintergrundmusik)
                    ZeigeZeileKlangSpalten(grpobj, false);

                Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel);
            }

            if (AktKlangPlaylist.MaxSongsParallel == 0)
            {
                grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;
                grpobj.tbTopKlangSongsParallel.Text = "1";  //ruft TextChange-Ereibnis auf, und aktualisiert die Playlist
            }
            
            return titel.Audio_TitelGUID;
        }

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
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Drag-Mode über den Editor ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void grdEditorX_Drop(object sender, DragEventArgs e)
        {
            bool hinzugefuegt = false;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    if (AktKlangPlaylist == null)
                        NeueKlangPlaylistInDB();

                    string[] gedroppteDateien = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                    string[] extension = new String[4] { ".mp3", ".wav", ".ogg", ".wma" };

                    foreach (string droppedFilePath in gedroppteDateien.OrderBy(t => t))
                    {
                        if (AktKlangPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => GetFullPath(t.Audio_Titel.Pfad) == droppedFilePath) != null)
                            continue;
                        if (Array.IndexOf(extension, droppedFilePath.Substring(droppedFilePath.Length - 4)) != -1 )
                        {
                            KlangDateiHinzu(droppedFilePath);
                            hinzugefuegt = true;

                            Guid g = Guid.Empty;
                            Audio_Playlist altAktKlangPlaylist = AktKlangPlaylist;

                            // check gleiche Playlists im Hintergrund und aktualisieren
                            _GrpObjecte.FindAll(t => t.aPlaylist.Audio_PlaylistGUID == AktKlangPlaylist.Audio_PlaylistGUID).FindAll(t => t.aPlaylist != AktKlangPlaylist)
                                .ForEach(delegate(GruppenObjekt grpObj)
                            {
                                int altIndex = tcEditor.SelectedIndex;
                                AktKlangPlaylist = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(grpObj.aPlaylist.Audio_PlaylistGUID)).FirstOrDefault();
                                tcEditor.SelectedItem = _GrpObjecte.FirstOrDefault(t => t.ticKlang == grpObj.ticKlang).ticKlang;// grpObj.seite;
                                g = KlangDateiHinzu(droppedFilePath);
                                AktKlangPlaylist = altAktKlangPlaylist;
                                tcEditor.SelectedIndex = altIndex;
                                hinzugefuegt = true;
                            });

                            //Aktualisieren der Hintergrund-Playlist
                            if (_BGPlayer.AktPlaylist == AktKlangPlaylist)
                            {
                                ListBoxItem lbitem = new ListBoxItem();
                                lbitem.Name = "titel" + lbMusiktitellist.Items.Count;
                                lbitem.Tag = g;
                                lbitem.Content = System.IO.Path.GetFileNameWithoutExtension(droppedFilePath);

                                lbMusiktitellist.Items.Add(lbitem);
                            }
                        }
                    }
                }
                if (hinzugefuegt)
                {
                    if (lbEditor.SelectedIndex == -1)
                    {
                        for (int i = 0; i < lbEditor.Items.Count; i++)
                            if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
                                lbEditor.SelectedIndex = i;
                    }
                    GruppenObjekt grpobj = _GrpObjecte.First(t => t.ticKlang == tcEditor.SelectedItem);

                    if (grpobj.rbTopIstKlangPlaylist.IsChecked == true)
                        AktKlangPlaylist.Hintergrundmusik = false;
                    else
                        AktKlangPlaylist.Hintergrundmusik = true;

                    if (AktKlangPlaylist.Hintergrundmusik)
                    {
                        ZeigeKlangSongsParallel(grpobj, false);
                        ZeigeKlangTop(grpobj, false);
                    }
                    else
                    {
                        ZeigeKlangSongsParallel(grpobj, true);
                        ZeigeKlangTop(grpobj, true);
                    }
                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    CheckAlleAngehakt(grpobj);
                    CheckChPfadHatBezug(grpobj);
                    grpobj.playlistName = AktKlangPlaylist.Name;
                    grpobj.grdEditor.Visibility = Visibility.Visible;
                    grpobj.aPlaylist = AktKlangPlaylist;
                }
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Ablegen der Dateien in der Playlist ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }
        
        private void CheckChPfadHatBezug(GruppenObjekt grpobj)
        {
            bool hatBezug = false;
            List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            if (titelliste.Count > 0)
            {
                hatBezug = true;
                string titelRef = titelliste[0].Pfad.LastIndexOf(@"\") != -1 ? titelliste[0].Pfad.Substring(0, titelliste[0].Pfad.LastIndexOf(@"\")) : titelliste[0].Pfad;
                titelRef = (titelRef.Substring(1, 1) != ":") ? stdPfad + @"\" + titelRef : titelRef;

                titelliste.ForEach(delegate(Audio_Titel aTitel)
                {
                    string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;

                    while (!vergleich.StartsWith(titelRef))
                    {
                        if (titelRef.Contains(@"\"))
                            titelRef = titelRef.Substring(0, titelRef.LastIndexOf(@"\"));
                        else
                        {
                            hatBezug = false;
                            break;
                        }
                    }
                });
            }
            grpobj.btnTopChPfad.Visibility = hatBezug ? Visibility.Visible : Visibility.Hidden;
        }

        private Audio_Playlist UpdatePlaylist(Audio_Playlist AktPlaylist, string NeuerPlaylistName)
        {
            if (AktPlaylist == null)
            {
                Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                playlist.Name = NeuerPlaylistName;
                AktPlaylist = playlist;
            }
            else
            {
                List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(AktPlaylist.Audio_PlaylistGUID)).ToList(); 
                if (playlistliste.Count == 0)
                {
                    Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                    playlist.Name = NeuerPlaylistName;
                    playlist.Hintergrundmusik = false;
                    playlist.MaxSongsParallel = 1;

                    //zur datenbank hinzufügen
                    if (!Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                    {
                        var errWin = new MsgWindow("Datenbank-Fehler", "Das hinzufügen der Playlist in die Datenbank ist fehlgeschlagen.");
                        errWin.ShowDialog();
                        errWin.Close();
                        //List<Audio_Titel> titelMitNeuImNamen = Global.ContextAudio.TitelListe.Where(t => t.Name.StartsWith("Neu")).ToList();
                    }
                }
                else
                {
                    playlistliste[0].Name = NeuerPlaylistName;
                    Global.ContextAudio.Update<Audio_Playlist>(playlistliste[0]);
                }
            }
            return AktPlaylist;            
        }

        private void tboxTopKategorie_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Audio_Playlist aPlyLst = Global.ContextAudio.PlaylistListe.Find(t => t.Audio_PlaylistGUID == ((Guid)((TextBox)e.Source).Tag));
                if (aPlyLst != null)
                {
                    aPlyLst.Kategorie = ((TextBox)e.Source).Text;
                    Global.ContextAudio.Update<Audio_Playlist>(aPlyLst);
                }
                tbLostFocus(null, null);
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Verlassen des Kategorie-Feldes ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void addHotkey(int i)
        {
            hotkey hkey = new hotkey();
            hkey.taste = i;
            hkey.aPlaylistGuid = Guid.Empty;

            hotkeys.Add(hkey);
        }

        private void AktualisiereHotKeys()
        {
            hotkeys.RemoveRange(0, hotkeys.Count);
            for (int i = 48; i <= 57; i++)
                addHotkey(i);

            for (int i = 65; i < 91; i++)
                addHotkey(i);

            UpdateHotkeys();
        }

        private void UpdateHotkeys()
        {
            spnlHotkeys.Children.RemoveRange(0, spnlHotkeys.Children.Count);
            foreach (Audio_Playlist aPlayList in Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null))
            {
                hotkey hkey = hotkeys.FirstOrDefault(t => t.taste == Convert.ToInt32(Convert.ToChar(aPlayList.Key)));
                if (hkey != null)
                {
                    hkey.aPlaylistGuid = aPlayList.Audio_PlaylistGUID;

                    Button btnHotKey = new Button();
                    btnHotKey.Background = Brushes.LightGray;
                    btnHotKey.Content = Convert.ToChar(hkey.taste);
                    btnHotKey.ToolTip = aPlayList.Name;
                    btnHotKey.Margin = new Thickness(5, 0, 5, 0);
                    btnHotKey.Height = 20;
                    btnHotKey.Width = btnHotKey.Height;
                    btnHotKey.Tag = hkey;
                    btnHotKey.Click += btnHotKey_Click;

                    spnlHotkeys.Children.Add(btnHotKey);
                }
                btnHotkeyStop.IsEnabled = (spnlHotkeys.Children.Count > 0) ? true : false;
                gbxHotkeys.Visibility = (spnlHotkeys.Children.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void slHotkey_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                ((Slider)sender).ToolTip = e.NewValue + " %";
                hotkeys.FindAll(t => t.mp != null).ForEach(delegate(hotkey hkey)
                {
                    hkey.mp.Volume = e.NewValue / 100;
                });
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Ändern der aktiven Hotkey-Geräuschen ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnTopHotkeySet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Button)sender).Visibility = Visibility.Collapsed;

                _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem).cmboxTopHotkey.Visibility = Visibility.Visible;
                _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem).cmboxTopHotkey.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Setzen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }
        private void cmboxTopHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // a = 65
                int istkey = Convert.ToInt32(e.Key)+21;
                //string s = Convert.ToString(e.Key);
                if (e.Key >= Key.D0 && e.Key <= Key.D9)
                    istkey = Convert.ToInt32(Convert.ToString(e.Key).Remove(0, 1))+48;
                    //s = s.Remove(0, s.Length - 1);

                foreach (Border brdItem in ((ComboBox)sender).Items)
                {
                    
                    if (Convert.ToInt32(brdItem.Tag) == istkey)
                    {
                        ((ComboBox)sender).SelectedItem = brdItem;
                        break;
                    }
                }
                gbxHotkeys.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                gbxHotkeys.Visibility = Visibility.Visible;
                var errWin = new MsgWindow("Allgemeiner Fehler", "Drücken der Auswahltaste im Combobox-Feld ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void cmboxTopHotkey_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                notPlayHotkey = true;
                ((ComboBox)sender).Items.Clear();
                for (int i = 0; i < hotkeys.Count; i++)
                {
                    if (hotkeys[i].aPlaylistGuid == Guid.Empty)
                    {
                        Border brdHotkey = new Border();
                        brdHotkey.Height = 30;
                        brdHotkey.Width = 30;
                        brdHotkey.CornerRadius = new CornerRadius(5);
                        brdHotkey.Background = Brushes.LightGray;
                        Label lbl = new Label();
                        lbl.FontSize = 18;
                        lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                        lbl.Padding = new Thickness(5, 2, 5, 5);
                        lbl.Margin = new Thickness(3, 0, 0, 0);
                        lbl.FontStyle = FontStyles.Italic;
                        lbl.Content = Convert.ToChar(hotkeys[i].taste);
                        brdHotkey.Child = lbl;
                        brdHotkey.Tag = hotkeys[i].taste;

                        ((ComboBox)sender).Items.Add(brdHotkey);
                    }
                }
                ((ComboBox)sender).Focus();
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Öffnen der Dropdown-Liste ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void cmboxTopHotkey_DropDownClosed(object sender, EventArgs e)
        {
            ((ComboBox)sender).Visibility = Visibility.Collapsed;
            GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
            grpobj.btnHotkeyEntfernen.Visibility = Visibility.Visible;
            grpobj.btnTopHotkeySet.Visibility = Visibility.Visible;
            notPlayHotkey = false;
            ((ListboxItemIcon)lbEditor.SelectedItem).Focus();
        }

        private void cmboxTopHotkey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(AktKlangPlaylist == null)
                    return;
                if (((ComboBox)sender).SelectedIndex != -1)
                {
                    //Int16 posobjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    grpobj.cmboxTopHotkey.Visibility = Visibility.Collapsed;
                    grpobj.btnHotkeyEntfernen.Visibility = Visibility.Visible;
                    grpobj.btnTopHotkeySet.Visibility = Visibility.Visible;

                    if (hotkeys.FirstOrDefault(t => t.aPlaylistGuid == AktKlangPlaylist.Audio_PlaylistGUID) != null)
                        grpobj.btnHotkeyEntfernen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                    hotkey hkey = hotkeys.FirstOrDefault(t => t.taste == Convert.ToInt32(((Border)e.AddedItems[0]).Tag));
                    if (hkey != null && AktKlangPlaylist != null)
                    {
                        hkey.aPlaylistGuid = AktKlangPlaylist.Audio_PlaylistGUID;

                        Button btnHotKey = new Button();
                        btnHotKey.Background = Brushes.LightGray;
                        btnHotKey.Content = Convert.ToChar(hkey.taste);
                        Audio_Playlist aplylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.aPlaylistGuid);
                        btnHotKey.ToolTip = aplylist != null ? aplylist.Name : btnHotKey.Content;
                        btnHotKey.Margin = new Thickness(5, 0, 5, 0);
                        btnHotKey.Height = 20;
                        btnHotKey.Width = btnHotKey.Height;
                        btnHotKey.Tag = hkey;
                        btnHotKey.Click += btnHotKey_Click;

                        spnlHotkeys.Children.Add(btnHotKey);
                        grpobj.btnTopHotkeySet.Content = btnHotKey.Content.ToString();
                    }
                    if ((ListboxItemIcon)lbEditor.SelectedItem != null)
                    {
                        ((ListboxItemIcon)lbEditor.SelectedItem).Focus();
                        lbEditor.ScrollIntoView(lbEditor.SelectedItem);
                    }
                    btnHotkeyStop.IsEnabled = (spnlHotkeys.Children.Count > 0) ? true : false;
                    gbxHotkeys.Visibility = (spnlHotkeys.Children.Count > 0) ? Visibility.Visible : Visibility.Collapsed;

                    grpobj.aPlaylist.Key = Convert.ToChar(hkey.taste).ToString();
                    Global.ContextAudio.Update<Audio_Playlist>(grpobj.aPlaylist);
                }
            }
            catch (Exception ex)
            {
                notPlayHotkey = false;
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Wechseln der Hotkey-Taste ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnHotkeyStop_Click(object sender, RoutedEventArgs e)
        {
            hotkeys.FindAll(t => t.mp != null).ForEach(delegate (hotkey hkey)
            {
                hkey.mp.Stop();
            });
        }

        private void btnHotkeyEntfernen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                hotkey hkey = hotkeys.FirstOrDefault(t => t.aPlaylistGuid == grpobj.aPlaylist.Audio_PlaylistGUID);
                if (hkey == null)
                    return;

                foreach (Button btnHotKey in spnlHotkeys.Children.OfType<Button>())
                {
                    if (((hotkey)btnHotKey.Tag).taste == hkey.taste)
                    {
                        spnlHotkeys.Children.Remove(btnHotKey);
                        //int posobjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                        grpobj.btnHotkeyEntfernen.Visibility = Visibility.Collapsed;
                        grpobj.btnTopHotkeySet.Content = "nicht definiert";
                        grpobj.cmboxTopHotkey.SelectedIndex = -1;
                        hkey.aPlaylistGuid = Guid.Empty;

                        grpobj.aPlaylist.Key = null;
                        grpobj.aPlaylist.Modifiers = null;
                        Global.ContextAudio.Update<Audio_Playlist>(grpobj.aPlaylist);
                        break;
                    }
                }
                if (spnlHotkeys.Children.Count == 0)
                {
                    btnHotkeyStop.IsEnabled = false;
                    gbxHotkeys.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Entfernen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnHotKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hotkey hkey = (hotkey)((Button)sender).Tag;
                if (hkey.aPlaylistGuid != null)
                {
                    Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.aPlaylistGuid);

                  //  Global.ContextAudio.LoadPlaylist_TitelByPlaylist(aPlaylist, 
                  //  List<Audio_Playlist_Titel> aTitel = Global.ContextAudio.LoadTitelByPlaylist(aPlaylist).Where(t => t.Audio_Playlist_Titel.
                    
                    
                    int zuspielen = (new Random()).Next(0, aPlaylist.Audio_Playlist_Titel.Count - 1);

                    string file = aPlaylist.Audio_Playlist_Titel.ToList().ElementAt(zuspielen).Audio_Titel.Pfad;
                    if (file.Substring(1, 1) != ":")
                        file = (stdPfad.EndsWith(@"\")) ? stdPfad + file : stdPfad + @"\" + file;
                    hkey.mp.Volume = slHotkey.Value / 100; //slPlaylistVolume.Value / 100);   // Slider des PListModifikator
                    hkey.mp.MediaEnded += mp_failed_ended;
                    hkey.mp.MediaFailed += mp_failed_ended;
                    hkey.mp.Open(new Uri(file));
                    hkey.mp.Play();
                }
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void mp_failed_ended(object sender, EventArgs e)
        {
            try
            {
                ((MediaPlayer)sender).Stop();
                ((MediaPlayer)sender).Close();
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Stoppen und Schließen des Media-Player nach einem Fehler ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void AktualisiereMusikPlaylist()
        {
            UInt16 pos = 0;
            foreach (Audio_Playlist plyList in Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik).OrderBy(t => t.Name))
            {
                List<Audio_Titel> s = Global.ContextAudio.LoadTitelByPlaylist(plyList);

                if (pos + 1 > lbBackground.Items.Count)                         // Zeile noch nicht vorhanden
                {
                    MusikZeile mZeile = new MusikZeile();
                    mZeile.Cursor = Cursors.Hand;
                    mZeile.Tag = plyList.Audio_PlaylistGUID;
                    mZeile.tblkTitel.Text = plyList.Name;
                    mZeile.tblkLänge.Text = (plyList.Länge != 0) ? TimeSpan.FromMilliseconds(plyList.Länge).ToString(@"hh\:mm\:ss") : "";
                    mZeile.tboxKategorie.Tag = mZeile.Tag;
                    mZeile.tboxKategorie.Text = plyList.Kategorie;
                    mZeile.tboxKategorie.GotFocus += tbGotFocus;
                    mZeile.tboxKategorie.LostFocus += tboxTopKategorie_LostFocus;

                    lbBackground.Items.Add(mZeile);
                }
                else                                                            // Abändern der Zeile
                {
                    ((MusikZeile)(lbBackground.Items[pos])).Tag = plyList.Audio_PlaylistGUID;
                    if (((MusikZeile)(lbBackground.Items[pos])).tblkTitel.Text != plyList.Name)
                        ((MusikZeile)(lbBackground.Items[pos])).tblkTitel.Text = plyList.Name;
                    if (((MusikZeile)(lbBackground.Items[pos])).tboxKategorie.Text != plyList.Kategorie)
                        ((MusikZeile)(lbBackground.Items[pos])).tboxKategorie.Text = plyList.Kategorie;
                }
                pos++;
            }
            if (lbBackground.Items.Count != 0)
            {
                while (Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik).Count > pos)
                    lbBackground.Items.RemoveAt(lbBackground.Items.Count - 1);
            }
        }

        private void AktualisiereKlangPlaylist()
        {
            lbEditor.Items.Clear();
            foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe.OrderBy(t => t.Name))
            {
                ListboxItemIcon lbitem = new ListboxItemIcon();
                if ((aPlaylist.Hintergrundmusik) && rbEditorMusik.IsChecked == true)
                {
                    lbitem.Tag = aPlaylist.Audio_PlaylistGUID;
                    lbitem.lbText.Content = aPlaylist.Name;
                    lbitem.imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png"));
                    lbitem.imgIcon.ToolTip = "Musik-Playlist";
                    lbEditor.Items.Add(lbitem);
                }
                if ((aPlaylist.Hintergrundmusik == false) && rbEditorKlang.IsChecked == true)
                {
                    lbitem.Tag = aPlaylist.Audio_PlaylistGUID;
                    lbitem.lbText.Content = aPlaylist.Name;
                    lbitem.imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
                    lbitem.imgIcon.ToolTip = "Geräusche-Playlist";
                    lbEditor.Items.Add(lbitem);
                }
                lbitem.btnExport.Click += new RoutedEventHandler(lbItembtnExportPlaylist_Click);
                lbitem.btnLöschen.Click += new RoutedEventHandler(lbItembtnLöschenPlaylist_Click);

                lbitem.imgIcon.Source = (aPlaylist.Hintergrundmusik) ?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png")) :
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
            }
        }

        private void AktualisiereKlangThemes()
        {
            List<Audio_Theme> aThemes = Global.ContextAudio.ThemeListe;
            int posistionThemeItem = 0;
            foreach (Audio_Theme aTheme in aThemes.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                ListboxItemIcon lbitem = null;
                if (lbEditorTheme.Items.Count <= posistionThemeItem)
                    lbitem = new ListboxItemIcon();
                else
                    lbitem = (ListboxItemIcon)lbEditorTheme.Items[posistionThemeItem];

                lbitem.Tag = aTheme.Audio_ThemeGUID;
                lbitem.lbText.Content = aTheme.Name;
                lbitem.imgIcon.Tag = "1";
                
                
                if (lbEditorTheme.Items.Count <= posistionThemeItem)
                {
                    lbitem.btnExport.Click += new RoutedEventHandler(lbItembtnExportTheme_Click);
                    lbitem.btnLöschen.Click += new RoutedEventHandler(lbItembtnLöschenTheme_Click);                
                    lbEditorTheme.Items.Add(lbitem);
                }
                posistionThemeItem++;
            }
            int cnt = lbEditorTheme.Items.Count;
            for (int i = posistionThemeItem; i < cnt; i++)
                lbEditorTheme.Items.RemoveAt(lbEditorTheme.Items.Count-1);
        }

        private void AktualisierePlaylistThemes()
        {
            List<Audio_Theme> aThemes = Global.ContextAudio.ThemeListe;
            int cnt = 0;
            bool neu;
            foreach (Audio_Theme aTheme in aThemes.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                cnt++;
                neu = wpnlPListThemes.Children.Count < cnt;
                ToggleButton tbtnTheme = neu? new ToggleButton(): (ToggleButton)wpnlPListThemes.Children[cnt-1];
                tbtnTheme.Tag = aTheme.Audio_ThemeGUID;
                tbtnTheme.Content = aTheme.Name;
                
                Audio_Playlist aPListHintergrund = aTheme.Audio_Playlist.FirstOrDefault(t => t.Hintergrundmusik);
                string ttip = aPListHintergrund != null ? "Hintergrund-Musik:   " + aPListHintergrund.Name + Environment.NewLine : "";
                
                Int16 i = 1;
                List<Audio_Playlist> aPListGeräusche = aTheme.Audio_Playlist.Where(t => !t.Hintergrundmusik).ToList();
                foreach (Audio_Playlist aPList in aPListGeräusche.OrderBy(t => t.Name))
                {
                    ttip += "Geräusch " + i + ":   " + aPList.Name + Environment.NewLine;
                    i++;
                }                
                i = 1;
                foreach (Audio_Theme aUnterTheme in aTheme.Audio_Theme1.
                    Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
                {
                    ttip += "Sub-Theme " + i + ":   " + aUnterTheme.Name + Environment.NewLine;
                    i++;
                }
                tbtnTheme.ToolTip = ttip;

                if (neu)
                {
                    tbtnTheme.Margin = new Thickness(5);
                    tbtnTheme.Focusable = false;
                    tbtnTheme.Checked += tbtnTheme_Checked;
                    tbtnTheme.Unchecked += tbtnTheme_UnChecked;
                    wpnlPListThemes.Children.Add(tbtnTheme);
                }
            }
            wpnlPListThemes.Children.RemoveRange(aThemes.Count, wpnlPListThemes.Children.Count-aThemes.Count);
        }

        private void tbtnTheme_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ToggleButton)sender).FontWeight = FontWeights.Bold;
                foreach (ToggleButton tbtn in wpnlPListThemes.Children)
                {
                    wpnlPListThemes.Tag = Guid.Empty;
                    if (tbtn.IsChecked.Value && tbtn != ((ToggleButton)sender))
                        tbtn.IsChecked = false;
                }
                wpnlPListThemes.Tag = Guid.Empty;

                Audio_Theme aTheme = Global.ContextAudio.LoadThemesByGUID((Guid)((ToggleButton)sender).Tag);
 
                bool foundHintergrund = false;
                foreach (Audio_Playlist aPlaylist in aTheme.Audio_Playlist)
                {
                    if (aPlaylist.Hintergrundmusik)
                    {
                        foundHintergrund = true;
                        foreach (MusikZeile aZeile in lbPListMusik.Items)
                        {
                            if ((Guid)aZeile.Tag == aPlaylist.Audio_PlaylistGUID)
                            {
                                aZeile.IsSelected = true;
                                lbPListMusik.ScrollIntoView(aZeile);
                                break;
                            }
                        }
                    }
                }
                if (!foundHintergrund)
                {
                    foreach (MusikZeile aZeile in lbPListMusik.Items)
                    {
                        if (aZeile.IsSelected)
                        {
                            aZeile.IsSelected = false;
                            break;
                        }
                    }
                }
                foreach (MusikZeile aZeile in lbPListGeräusche.Items)
                {
                    aZeile.tbtnCheck.IsChecked = (aTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)aZeile.Tag) == null) ? false : true;
                }

                //Auswählen der Geräusche-Playlisst der untergeorgenten Themes
                CheckUnterThemes(aTheme);
                
                wpnlPListThemes.Tag = ((ToggleButton)sender).Tag;
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Themes ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void CheckUnterThemes(Audio_Theme aTheme)
        {
            foreach (Audio_Theme aUnterTheme in aTheme.Audio_Theme1.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")))
            {                    
                foreach (MusikZeile aZeile in lbPListGeräusche.Items)
                {
                    if (aUnterTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)aZeile.Tag) != null)
                        aZeile.tbtnCheck.IsChecked = true;
                }
                if (aUnterTheme.Audio_Theme1.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).ToList().Count > 0)
                    CheckUnterThemes(aUnterTheme);
            }
        }

        private void tbtnTheme_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ToggleButton)sender).FontWeight = FontWeights.Normal;
                if ((Guid)wpnlPListThemes.Tag != Guid.Empty)
                {
                    foreach (MusikZeile aZeile in lbPListMusik.Items) aZeile.IsSelected = false;
                    foreach (MusikZeile aZeile in lbPListGeräusche.Items) aZeile.tbtnCheck.IsChecked = false;
                    wpnlPListThemes.Tag = Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Abwählen des Themes ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnNeuePlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string NeuePlaylist = "NeuePlayliste";
                int ver = 0;
                string[] str_tiHeader = new string[tcEditor.Items.Count - 2];

                for (int i = 0; i < tcEditor.Items.Count - 2; i++)
                    str_tiHeader[i] = (i == 0) ? ((TabItem)tcEditor.Items[i]).Header.ToString() : ((TCButtons)tcEditor.Items[i])._tbText.Text;

                Audio_Playlist playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuePlaylist));
                while (playlistlist != null)
                {
                    NeuePlaylist = "NeuePlayliste-" + ver;
                    ver++;
                    playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuePlaylist));
                }

                if (playlistlist == null)
                {
                    rbEditorKlang_Click(rbEditorKlang, null);
                    Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                    playlist.Name = NeuePlaylist.ToString();
                    if (rbEditorKlang.IsChecked.Value)
                        playlist.Hintergrundmusik = false;
                    else
                        playlist.Hintergrundmusik = true;

                    //zur datenbank hinzufügen
                    if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                    {
                        AktKlangPlaylist = playlist;
                        playlist.MaxSongsParallel = 1;
                                                
                        AktualisiereKlangPlaylist();
                        for (int i = 0; i <= lbEditor.Items.Count - 1; i++)
                            if ((lbEditor.Items[i] as ListboxItemIcon).lbText.Content.ToString() == playlist.Name)
                                lbEditor.SelectedIndex = i;
                    }
                }
                else
                {
                    var errWin = new MsgWindow("Datenbankfehler", "Playlist schon vorhanden. Bitte wiederholen Sie den Vorgang und wählen einen anderen Titel");
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Erstellen einer neuen Playlist ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void tiMusik_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbBackground.Items.Count == 0)
                    AktualisiereMusikPlaylist();
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Wechseln auf das TabItem ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void lbMusiktitellist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((lbMusiktitellist.SelectedIndex >= 0) &&
                   (((ListBoxItem)lbMusiktitellist.SelectedItem).Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString()))         // Red))
                {
                    if (lbBackground.SelectedIndex == -1)
                    {
                        lbBackground.SelectionChanged -= lbBackground_SelectionChanged;
                        lbBackground.SelectedIndex = Convert.ToInt16(lbBackground.Tag);
                        lbBackground.SelectionChanged += lbBackground_SelectionChanged;
                    }
                    chkbxPlayRange.IsChecked = false;
                    rsldTeilSong.Visibility = Visibility.Hidden;
                    rsldTeilSong.LowerValue = 0;
                    rsldTeilSong.UpperValue = 100000;

                    ListBoxItem lbItem = (ListBoxItem)lbMusiktitellist.SelectedItem;
                    string st = lbItem.Tag.ToString();

                    Audio_Titel titel = null;
                    Audio_Playlist_Titel aPlayListtitel = _BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)lbItem.Tag);
                    if (aPlayListtitel != null)
                        titel = _BGPlayer.AktTitel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)lbItem.Tag);

                    if (titel == null)
                    {
                        lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                        lbItem.ToolTip = "Keine Titel-Informationen gefunden";

                        lbBackground_SelectionChanged(lbMusiktitellist, e);
                        lbMusiktitellist.Tag = -1;
                        btnBGPrev.IsEnabled = false;
                        imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
                        btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
                    }
                    else
                    {
                        string file = titel.Pfad;
                        if (file.Substring(1, 1) != ":")
                        {
                            if (stdPfad.EndsWith(@"\"))
                                file = stdPfad + file;
                            else
                                file = stdPfad + @"\" + file;
                        }

                        if (Directory.Exists(System.IO.Path.GetDirectoryName(file)) && !File.Exists(file) ||
                            !Directory.Exists(System.IO.Path.GetDirectoryName(file)))
                        {
                            lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                            lbItem.ToolTip = "Datei nicht gefunden. -> " + file;
                            lbMusiktitellist.Tag = -1;
                            btnBGPrev.IsEnabled = false;
                            imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
                            btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
                            SpieleNeuenMusikTitel(Guid.Empty);
                        }
                        else
                        {
                            if (Directory.Exists(System.IO.Path.GetDirectoryName(file)) && File.Exists(file))
                            {
                                grdSongInfo.Visibility = Visibility.Visible;

                                lblBgTimeMax.Content = "--:--";
                                lblBgTitel.Content = "";
                                lblBgAlbum.Content = "";
                                lblBgArtist.Content = "";
                                lblBgJahr.Content = "";
                                lblBgGenre.Content = "";

                                _BGPlayer.AktPlaylistTitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(_BGPlayer.AktPlaylist, titel).First();
                                chkbxPlayRange.IsChecked = _BGPlayer.AktPlaylistTitel.TeilAbspielen;

                                if (_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
                                    _BGPlayer.aktiv = _BGPlayer.aktiv == 0 ? 1 : 0;

                                if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds > 0 &&
                                    !_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
                                {

                                    FadingIn_Started = false;
                                    if (lbMusiktitellist.SelectedIndex != -1)
                                    {
                                       // _BGPlayer.BG[_BGPlayer.aktiv == 0 ? 1 : 0].FadingOutStarted = false;
                                        if (!_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted && lbMusiktitellist.SelectedIndex != -1)
                                        {
                                            _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
                                            BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], false, false);
                                        }
                                        _BGPlayer.aktiv = (_BGPlayer.aktiv == 0) ? 1 : 0;
                                    //    _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
                                    //    BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], false, false);
                                    }
                                }
                                _BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;

                                _BGPlayer.BG[_BGPlayer.aktiv].mPlayer = PlayFile(false, null, -1, _BGPlayer.BG[_BGPlayer.aktiv].mPlayer, file, slBGVolume.Value, true);
                                btnBGPrev.IsEnabled = true;
                                imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
                                btnBGStoppen.IsEnabled = true;

                                btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
                                btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
                                btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;

                                if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                                {
                                    btnBGAbspielen.Tag = true;
                                    btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
                                    pbarBGSong.Value = 0;
                                    if (((MusikZeile)lbPListMusik.SelectedItem) != null)
                                        ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Value = pbarBGSong.Value;

                                    if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.HasTimeSpan)
                                    {
                                        pbarBGSong.Maximum = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                        rsldTeilSong.Minimum = 0;
                                        rsldTeilSong.Maximum = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                        if (_BGPlayer.AktPlaylistTitel.TeilAbspielen)
                                        {
                                            rsldTeilSong.LowerValue = _BGPlayer.AktPlaylistTitel.TeilStart.Value;
                                            rsldTeilSong.UpperValue = _BGPlayer.AktPlaylistTitel.TeilEnde.Value;
                                            rsldTeilSong.Visibility = Visibility.Visible;
                                        }
                                        else
                                        {
                                            rsldTeilSong.LowerValue = 0;
                                            rsldTeilSong.UpperValue = pbarBGSong.Maximum;
                                        }
                                        if (((MusikZeile)lbPListMusik.SelectedItem) != null)
                                            ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Maximum = pbarBGSong.Maximum;
                                        lblBgTimeMax.Content = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                                    }
                                    btnBGNext.IsEnabled = true;
                                    imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
                                    btnBGAbspielen.IsEnabled = true;
                                    btnPListMusikAbspielen.IsEnabled = true;
                                    btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;
                                    starsUpdate();
                                    grdSongInfo.Visibility = Visibility.Visible;

                                    ListBoxItem lbi = (ListBoxItem)lbBackground.SelectedItem;
                                    MusikProgBarTimer.Tag = -1;
                                    MusikProgBarTimer.Start();
                                }
                            }
                            else
                            {
                                grdSongInfo.Visibility = Visibility.Hidden;
                                lbMusiktitellist.SelectedIndex = -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Playlist Fehler", "Nach Auswählen ist ein unvorhergesehner Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private static String ConvertByteToString(byte[] bytes, int pos1, int pos2)
        {
            //pos2 muß größer oder gleich pos1 sein und
            //pos2 darf Länge des Arrays nicht überschreiten
            if ((pos1 > pos2) || (pos2 > bytes.Length - 1))
                throw new ArgumentException("Aruments of range");

            //Länge des zu betrachtenden Ausschnittes
            int length = pos2 - pos1 + 1;

            //neues Char-Array anlegen der Länge length
            Char[] chars = new Char[length];

            //packe alle Bytes von pos1 bis pos2 als
            //Char konvertiert in Array chars
            for (int i = 0; i < length; i++)
                chars[i] = Convert.ToChar(bytes[i + pos1]);

            //konvertiere Char-Array in String und gebe es zurück
            String s = new String(chars);
            s = s.Replace("\0", "");

            return s; // neu String(s);
        }

        private void KlangProgBarTimer_Tick(object sender, EventArgs e)
        {
            MyTimer.start_timer();
            bool found = false;
            KlangProgBarTimer.Tag = (KlangProgBarTimer.Tag.ToString() == "0") ? "1" : "0";
            try
            {
                for (int posObjGruppe = 0; posObjGruppe < _GrpObjecte.Count; posObjGruppe++)
                {
                    List<KlangZeile> KlangZeilenLaufend = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);

                    if (KlangZeilenLaufend.Count != 0)
                    {
                        if (KlangProgBarTimer.Interval == TimeSpan.FromMilliseconds(1000))
                            KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(100);
                        for (int durchlauf = 0; durchlauf < KlangZeilenLaufend.Count; durchlauf++)
                        {
                            if (KlangZeilenLaufend[durchlauf].istPause)
                                continue;

                            int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
                            if (objGruppe == -1)
                                break;
                            found = true;

                            if (_GrpObjecte[posObjGruppe].visuell && 
                                KlangZeilenLaufend[durchlauf].audioZeile != null &&
                                KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel != null &&
                                KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Tag != null)
                            {
                                //keine Informationen nach 1 Sekunde vom MediaPlayer über Track -> Gelb -> nächstes Lied
                                if (((TimeSpan)(DateTime.Now - KlangZeilenLaufend[durchlauf].dtLiedLastCheck)).TotalMilliseconds > Zeitüberlauf)
                                {
                                    if (!KlangZeilenLaufend[durchlauf]._mplayer.HasAudio)
                                    {
                                        KlangZeilenLaufend[durchlauf].audioZeile.lbiEditorRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));       // Yellow
                                        KlangZeilenLaufend[durchlauf].audioZeile.lbiEditorRow.ToolTip = "Datei kann nicht abgespielt werden (Zeitüberlauf)";
                                        KlangZeilenLaufend[durchlauf]._mplayer.Stop();
                                        KlangZeilenLaufend[durchlauf]._mplayer.Close();
                                        KlangZeilenLaufend[durchlauf].playable = false;
                                        KlangZeilenLaufend[durchlauf].istStandby = true;
                                        KlangZeilenLaufend[durchlauf].istLaufend = false;
                                        KlangZeilenLaufend[durchlauf].istPause = false;
                                        CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);
                                    }
                                    else
                                        KlangZeilenLaufend[durchlauf].audioZeile.lbiEditorRow.Background = null;
                                }
                            }
                            if (KlangZeilenLaufend[durchlauf]._mplayer != null &&
                                KlangZeilenLaufend[durchlauf]._mplayer.HasAudio == false &&
                                KlangZeilenLaufend[durchlauf]._mplayer.BufferingProgress == 1)
                                KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.Now;
                            else
                                KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.MinValue;

                            if (KlangZeilenLaufend[durchlauf].audiotitel.Aktiv && (KlangProgBarTimer.Tag.ToString() == "0") && 
                                KlangZeilenLaufend[durchlauf]._mplayer != null)
                            {
                                // Volume anpassen
                                if ((_GrpObjecte[posObjGruppe].force_Volume == null) &&
                                    KlangZeilenLaufend[durchlauf].audiotitel.VolumeChange && !KlangZeilenLaufend[durchlauf].FadingOutStarted) // .audioZeile.chkVolMove.IsChecked == true 
                                {
                                    if ((((TimeSpan)(DateTime.Now - _GrpObjecte[posObjGruppe].LastVolUpdate)).Seconds > KlangZeilenLaufend[durchlauf].UpdateZyklusVol) &&
                                        Math.Abs(KlangZeilenLaufend[durchlauf]._mplayer.Volume * 100 - KlangZeilenLaufend[durchlauf].volZiel) <= KlangZeilenLaufend[durchlauf].Vol_jump)
                                    {
                                        KlangZeilenLaufend[durchlauf].volZiel =
                                            (new Random()).Next(0, KlangZeilenLaufend[durchlauf].volMax_wert - KlangZeilenLaufend[durchlauf].volMin_wert) +
                                            KlangZeilenLaufend[durchlauf].volMin_wert;
                                    }
                                    _zeile.AktLautstärke = (KlangZeilenLaufend[durchlauf].volZiel < _zeile.AktLautstärke) ? _zeile.AktLautstärke -= 1 : _zeile.AktLautstärke += 1;

                                    //Ausserhalb der Einstellwerte oder weit von dem Ziel-Vol entfernt => schneller Sprung ansonsten 1er Schritt
                                    int speed = (KlangZeilenLaufend[durchlauf].Aktuell_Volume < (double)KlangZeilenLaufend[durchlauf].volMin_wert ||
                                        KlangZeilenLaufend[durchlauf].Aktuell_Volume > (double)KlangZeilenLaufend[durchlauf].volMax_wert) ||
                                        (Math.Abs((double)KlangZeilenLaufend[durchlauf].volZiel - KlangZeilenLaufend[durchlauf].Aktuell_Volume) > 6) ?
                                        KlangZeilenLaufend[durchlauf].Vol_jump : 1;

                                    KlangZeilenLaufend[durchlauf].Aktuell_Volume = (KlangZeilenLaufend[durchlauf].volZiel < KlangZeilenLaufend[durchlauf].Aktuell_Volume) ?
                                        KlangZeilenLaufend[durchlauf].Aktuell_Volume -= speed :
                                        KlangZeilenLaufend[durchlauf].Aktuell_Volume += speed;

                                    if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null &&
                                        _GrpObjecte[posObjGruppe].ticKlang.Visibility == Visibility.Visible)
                                        KlangZeilenLaufend[durchlauf].audioZeile.sldKlangVol.Value = KlangZeilenLaufend[durchlauf].Aktuell_Volume;
                                }
                                double sollWert = (KlangZeilenLaufend[durchlauf].Aktuell_Volume / 100) * (_GrpObjecte[posObjGruppe].Vol_ThemeMod) / 100 * _GrpObjecte[posObjGruppe].Vol_PlaylistMod / 100;
                                if (!FadingIn_Started && !KlangZeilenLaufend[durchlauf].FadingOutStarted && _GrpObjecte[posObjGruppe].force_Volume != null)
                                    KlangZeilenLaufend[durchlauf]._mplayer.Volume = _GrpObjecte[posObjGruppe].force_Volume.Value;
                                else
                                    if (!FadingIn_Started && !KlangZeilenLaufend[durchlauf].FadingOutStarted && sollWert != KlangZeilenLaufend[durchlauf]._mplayer.Volume)
                                        KlangZeilenLaufend[durchlauf]._mplayer.Volume = sollWert;

                                //einmaliges ermitteln des Endzeitpunkts
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null &&
                                    KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum == 100000 && KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan &&
                                    KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel != null)
                                {
                                    if (KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge != KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
                                        Global.ContextAudio.Update<Audio_Titel>(KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel);

                                    KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge = KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                    KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum = (double)KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge;

                                    //aktualisiere Endzeitpunkt Minutenposition
                                    KlangZeilenLaufend[durchlauf].audioZeile.lblDauer.Content = KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.ToString().Substring(3, 5);
                                }

                                //aktualisiere ProgressBar - bei Wartezeit auf Maximum
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null &&
                                    _GrpObjecte[posObjGruppe].ticKlang.Visibility == Visibility.Visible &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)
                                    KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value =
                                        (!KlangZeilenLaufend[durchlauf].istWartezeit) ? KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds :
                                        KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum;
                                
                                //Startposition überprüfen
                                // Ist notwendig wenn HasTimeSpan zu Beginn nicht erkant wurde
                                if (!KlangZeilenLaufend[durchlauf].istWartezeit && !KlangZeilenLaufend[durchlauf].istStandby &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan && KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds < KlangZeilenLaufend[durchlauf].audiotitel.TeilStart)
                                    KlangZeilenLaufend[durchlauf]._mplayer.Position = TimeSpan.FromMilliseconds(KlangZeilenLaufend[durchlauf].audiotitel.TeilStart.Value);
                                       
                                //Bei Musikplaylists die Endposition vor Fading überprüfen
                                if (_GrpObjecte[posObjGruppe].istMusik && !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)
                                    if ((KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
                                         KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde) 
                                        ||                                      
                                        (!KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
                                        KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
                                        )
                                {
                                    _GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));

                                    if (!KlangZeilenLaufend[durchlauf].FadingOutStarted)
                                    {
                                        KlangZeilenLaufend[durchlauf].FadingOutStarted = true;
                                        FadingOut(KlangZeilenLaufend[durchlauf], true, false);
                                    } 
                                    if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null)
                                        KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value = KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde.Value;

                                    KlangZeilenLaufend[durchlauf].istLaufend = false;
                                    //KlangZeilenLaufend[durchlauf].istStandby = true;
                                    KlangZeilenLaufend[durchlauf].istPause = false;

                                    bool nurEinLiedAktiv = (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audiotitel.Aktiv).Count == 1) ? true : false;
                                    KlangZeilenLaufend[durchlauf].istStandby = nurEinLiedAktiv;
                                    
                                    CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);
                                    
                                    if (!nurEinLiedAktiv)
                                        KlangZeilenLaufend[durchlauf].istStandby = true;

                                    if (KlangZeilenLaufend[durchlauf].audioZeile != null)
                                        KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value = 0;
                                }
                            }
                        }
                    }
                }

                if (!found)
                {
                    KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(1000);
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Durchlauf der Prozesdur 'KlangProgBarTimer' ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
            MyTimer.stop_timer();
        }

        private void MusikProgBarTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Source != null)
                {
                    if (lblBgTitel.Content.ToString() == "")
                    {
                        FileInfo file = new FileInfo(_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Source.LocalPath);
                        Stream str = file.OpenRead();
                        byte[] bytes = new byte[128];
                        str.Seek(-128, SeekOrigin.End);
                        int numBytesToRead = 128;
                        int numBytesRead = 0;
                        while (numBytesToRead > 0)
                        {
                            int n = str.Read(bytes, numBytesRead, numBytesToRead);

                            if (n == 0)
                                break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                        str.Close();

                        String tag = ConvertByteToString(bytes, 0, 2);
                        if (tag != "TAG")
                        {
                            lblBgTitel.Content = System.IO.Path.GetFileNameWithoutExtension(_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Source.LocalPath);
                            lblBgArtist.Content = "---";
                            lblBgAlbum.Content = "---";
                            lblBgJahr.Content = "---";
                            lblBgGenre.Content = "---";
                        }
                        else
                        {
                            string[] _genres = {
						"Blues","Classic Rock","Country","Dance","Disco","Funk","Grunge","Hip-Hop","Jazz","Metal",
						"New Age","Oldies","Other","Pop","R&B","Rap","Reggae","Rock","Techno","Industrial",
						"Alternative","Ska","Death Metal","Pranks","Soundtrack","Euro-Techno","Ambient","Trip-Hop",
						"Vocal","Jazz+Funk","Fusion","Trance","Classical","Instrumental","Acid","House",
						"Game","Sound Clip","Gospel","Noise","Alternative Rock","Bass","Soul","Punk","Space",
						"Meditative","Instrumental Pop","Instrumental Rock","Ethnic","Gothic",
						"Darkwave","Techno-Industrial","Electronic","Pop-Folk","Eurodance","Dream",
						"Southern Rock","Comedy","Cult","Gangsta","Top 40","Christian Rap","Pop/Funk","Jungle",
						"Native American","Cabaret","New Wave","Psychadelic","Rave","Showtunes","Trailer","Lo-Fi",
						"Tribal","Acid Punk","Acid Jazz","Polka","Retro","Musical","Rock & Roll","Hard Rock","Folk",
						"Folk/Rock","National Folk","Swing","Fast-Fusion","Bebob","Latin","Revival","Celtic","Bluegrass",
						"Avantgarde","Gothic Rock","Progressive Rock","Psychedelic Rock","Symphonic Rock","Slow Rock",
						"Big Band","Chorus","Easy Listening","Acoustic","Humour","Speech","Chanson","Opera","Chamber Music",
						"Sonata","Symphony","Booty Bass","Primus","Porn Groove","Satire","Slow Jam","Club",
						"Tango","Samba","Folklore","Ballad","Power Ballad","Rhytmic Soul","Freestyle","Duet",
						"Punk Rock","Drum Solo","Acapella","Euro-House","Dance Hall","Goa","Drum & Bass","Club-House",
						"Hardcore","Terror","Indie","BritPop","Negerpunk","Polsk Punk","Beat","Christian Gangsta Rap",
						"Heavy Metal","Black Metal","Crossover","Contemporary Christian",
						"Christian Rock","Merengue","Salsa","Trash Metal","Anime","JPop","SynthPop"};

                            string titel = ConvertByteToString(bytes, 3, 32);
                            lblBgTitel.Content = titel;
                            lblBgTitel.Content = titel != "" ? titel : System.IO.Path.GetFileNameWithoutExtension(_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Source.LocalPath);
                            lblBgArtist.Content = ConvertByteToString(bytes, 33, 62);
                            lblBgAlbum.Content = ConvertByteToString(bytes, 63, 92);
                            lblBgJahr.Content = ConvertByteToString(bytes, 93, 96);
                            int z = Convert.ToInt32(bytes[127]);
                            if (z <= _genres.Length - 1)
                                lblBgGenre.Content = _genres[z];
                        }
                    }

                    if (lblBgTimeMax.Content.ToString() == "--:--")
                    {
                        if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.HasTimeSpan)
                        {
                            pbarBGSong.Maximum = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                            if (((MusikZeile)lbPListMusik.SelectedItem) != null)
                                ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Maximum = pbarBGSong.Maximum;
                            rsldTeilSong.Minimum = 0;
                            rsldTeilSong.Maximum = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;

                            if (_BGPlayer.AktPlaylistTitel.TeilAbspielen)
                            {
                                rsldTeilSong.LowerValue = _BGPlayer.AktPlaylistTitel.TeilStart.Value;
                                rsldTeilSong.UpperValue = _BGPlayer.AktPlaylistTitel.TeilEnde.Value;
                                rsldTeilSong.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                rsldTeilSong.LowerValue = 0;
                                rsldTeilSong.UpperValue = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                            }
                            lblBgTimeMax.Content = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");

                            if (_BGPlayer.AktPlaylistTitel.Audio_Titel.Länge != _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds)
                            {
                                _BGPlayer.AktPlaylistTitel.Audio_Titel.Länge = (double)_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                Global.ContextAudio.Update<Audio_Titel>(_BGPlayer.AktPlaylistTitel.Audio_Titel);
                            }
                        }
                    }

                    pbarBGSong.Value = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds;
                    if (((MusikZeile)lbPListMusik.SelectedItem) != null)
                        ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Value = pbarBGSong.Value;
                    lblBgTimeActual.Content = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.ToString(@"mm\:ss");

                    if (chkbxPlayRange.IsChecked.Value && pbarBGSong.Value < rsldTeilSong.LowerValue)
                        _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position = TimeSpan.FromMilliseconds(rsldTeilSong.LowerValue);

                    //Bei Musikplaylists die Endposition vor Fading überprüfen
                    if ((_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.HasTimeSpan &&
                         _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds) ||
                         (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= rsldTeilSong.UpperValue &&
                         chkbxPlayRange.IsChecked.Value))
                    {
                        if (btnBGRepeat.IsChecked.Value)
                            SpieleNeuenMusikTitel((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
                        else
                            SpieleNeuenMusikTitel(Guid.Empty);
                    }
                    else
                        MusikProgBarTimer.Tag = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds;
                }
            }
            catch (Exception ex)
            {
                plyTitelToSaveTimer.Stop();
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Zyklischen Check der Hintergrundmusik ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void chkVolMove0_0_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    if (chkgrpObj._listZeile.FirstOrDefault(t => t.audioZeile.chkVolMove == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;                        
                        break;
                    }
                }
                if (grpobj == null)
                    return;
                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FirstOrDefault(t => t.audioZeile.chkVolMove == (Control)sender));

                grpobj.anzVolChange = Convert.ToUInt16(
                    grpobj._listZeile.FindAll(t => t.audioZeile.chkVolMove.IsChecked == true).Count);

                if (grpobj.anzPauseChange == grpobj._listZeile.Count)
                    grpobj.chkbxTopVolChange.IsChecked = true;
                else
                    grpobj.chkbxTopVolChange.IsChecked = false;

                grpobj._listZeile[zeile].audiotitel.VolumeChange = ((CheckBox)sender).IsChecked.Value;
                AlleKlangzeilenSpeichern(GetPosObjGruppe(grpobj.objGruppe));

                CheckAlleAngehakt(grpobj);
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Auswahlfehler", "Beim Anklicken der Checkbox 'chkVolMove' ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void sldKlangPause0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    if (chkgrpObj._listZeile.FirstOrDefault(t => t.audioZeile.sldKlangPause == (Slider)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;
                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FirstOrDefault(t => t.audioZeile.sldKlangPause == (Control)sender));

                grpobj._listZeile[zeile].audiotitel.Pause = Convert.ToInt32(Math.Round(((Slider)sender).Value));
                plyTitelToSave.Add(grpobj._listZeile[zeile].audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Verlassen des Sliders 'sldKlangPause' ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void chkKlangPauseMove0_0_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    if (chkgrpObj._listZeile.FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;
                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (Control)sender));

                grpobj.anzPauseChange = Convert.ToUInt16(
                    grpobj._listZeile.FindAll(t => t.audioZeile.chkKlangPauseMove.IsChecked == true).Count);

                grpobj._listZeile[zeile].audiotitel.PauseChange = ((CheckBox)sender).IsChecked.Value;

                plyTitelToSave.Add(grpobj._listZeile[zeile].audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);

                CheckAlleAngehakt(grpobj);
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Anklicken der CheckBox 'chkKlangPauseMove' ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void rsldTeilSong_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _BGPlayer.AktPlaylistTitel.TeilStart = rsldTeilSong.LowerValue;
                _BGPlayer.AktPlaylistTitel.TeilEnde = rsldTeilSong.UpperValue;
                plyTitelToSave.Add(_BGPlayer.AktPlaylistTitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
            }
            catch (Exception) { }
        }

        private void imgTrash0_0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                KlangZeile kZeile = (KlangZeile)((Image)sender).Tag;        
                if (kZeile.audioZeile.chkTitel.IsChecked.Value && kZeile.istLaufend)
                {
                    kZeile.audioZeile.chkTitel.IsChecked = false;
                    chkTitel0_0_Click(kZeile.audioZeile.chkTitel, new RoutedEventArgs());
                }            
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FirstOrDefault(t => t.audioZeile.imgTrash == (Image)sender));
                
                grpobj.Gespielt.FindAll(t => t > zeile).ForEach(t => t--);

                Global.ContextAudio.RemoveTitelFromPlaylist(kZeile.audiotitel.Audio_Playlist, kZeile.audiotitel.Audio_Titel);  

                if (kZeile.audiotitel == null)
                    ZeigeKlangSongsParallel(grpobj, false);
                else
                    grpobj.NochZuSpielen.RemoveAll(t => t.Equals(kZeile.audiotitel.Audio_TitelGUID));
                grpobj.wpnl.Children.RemoveAt(zeile);
                grpobj._listZeile.RemoveAt(zeile);

                if (grpobj._listZeile.Count > 0)
                {
                    if (Convert.ToInt16(grpobj.tbTopKlangSongsParallel.Text) > grpobj._listZeile.Count)
                    {
                        grpobj.tbTopKlangSongsParallel.Tag = grpobj._listZeile.Count;
                        grpobj.tbTopKlangSongsParallel.Text = grpobj._listZeile.Count.ToString();
                    }
                    CheckChPfadHatBezug(grpobj);
                }
                else
                {
                    ZeigeKlangSongsParallel(grpobj, false);
                    grpobj.btnTopChPfad.Visibility = Visibility.Hidden;
                }

                CheckBtnGleicherPfad(grpobj);   
                GC.GetTotalMemory(true);
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Löschvorgang der Zeile ist in der Prozedure 'imgTrash' ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void rbEditorKlang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((TCButtons)tcEditor.SelectedItem) != null)
                {
                    int klangzeile = lbEditor.SelectedIndex;

                    string klangname = klangzeile != -1 ? ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content.ToString() :
                        (((TCButtons)tcEditor.SelectedItem) != null) ? ((TCButtons)tcEditor.SelectedItem)._tbText.Text : "";

                    if (((TabItem)tcAudioPlayer.SelectedItem) == tiMusik)
                        AktualisiereMusikPlaylist();
                    else
                        if (((TabItem)tcAudioPlayer.SelectedItem) == tiEditor)
                            AktualisiereKlangPlaylist();

                    tbEditorPlaylistFilter.Text = "";
                    if (klangname != "") SelektiereKlangZeile(klangname);
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Editor-Klang-Buttons ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void rbEditorEditTheme_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsInitialized)
                {
                    tcEditor.ToolTip = !rbEditorEditPList.IsChecked.Value? "Playlists im Theme." + Environment.NewLine + "\"Plus\"-Icon zum hinzufügen von Themes, " + Environment.NewLine +
                        "\"X\"-Icon zum entfernen der Playlist aus dem Theme": null;
                    exKlangTheme.Visibility = rbEditorEditTheme.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    brdKlang.Visibility = exKlangTheme.Visibility;
                    tiPlus.Visibility = exKlangTheme.Visibility;
                    
                    if (rbEditorEditTheme.Tag != null && (Boolean)rbEditorEditTheme.Tag)
                    {
                        // Bestehende Playlist Schließen
                        for (int i = tcEditor.Items.Count - 3; i >= 0; i--)
                        {
                            lbEditorTheme.Tag = true;
                            if (((TCButtons)tcEditor.Items[i]).Visibility == Visibility.Visible)
                            {
                                AlleKlangSongsAus(_GrpObjecte.FirstOrDefault(t => t.ticKlang == ((TCButtons)tcEditor.Items[i])), false, false);
                                ((TCButtons)tcEditor.Items[i])._buttonClose.IsEnabled = false;
                                ((TCButtons)tcEditor.Items[i])._buttonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            }
                            lbEditorTheme.Tag = null;
                        }

                        tboxKlangThemeName.Text = GetNeuenThemeNamen("Neues Theme");
                        lbEditor.SelectedIndex = -1;
                        
                        AktualisiereKlangThemes();
                    }

                    for (int i = 0; i <= tcEditor.Items.Count - 3; i++)
                        if (tcEditor.Items[i].GetType() == typeof(TCButtons))
                            ((TCButtons)tcEditor.Items[i])._buttonClose.Visibility = rbEditorEditPList.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;

                    rbEditorEditTheme.Tag = false;
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Auswählen des Editor-Theme-Butttons ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private string GetNeuenThemeNamen(string titel)
        {
            string NeuesTheme = titel;
            int ver = 0;
            Audio_Theme themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(NeuesTheme));
            while (themelist != null)
            {
                NeuesTheme = titel + "-" + ver;
                ver++;
                themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(NeuesTheme));
            }
            return NeuesTheme;
        }

        private void rbEditorEditTheme_UnChecked(object sender, RoutedEventArgs e)
        {
            rbEditorEditTheme.Tag = true;
            if (((RadioButton)sender).Name == "rbEditorEditPList")
                lbEditorTheme.SelectedIndex = -1;
            if (wpnlEditorTopThemesThemes.Children.Count > 0)
            {
                wpnlEditorTopThemesThemes.Children.RemoveRange(0, wpnlEditorTopThemesThemes.Children.Count);
                expEditorTopThemeTheme.Visibility = Visibility.Collapsed;
            }
        }

        private void rbTopIstKlangPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Int16 posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                grpobj.spnlTopHotkey.Visibility = Visibility.Visible;
                if (AktKlangPlaylist != null)
                {
                    if (_BGPlayer.AktPlaylist == AktKlangPlaylist && lbBackground.SelectedIndex != -1)
                    {
                        btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        lbBackground.SelectedIndex = -1;
                        lbBackground.Tag = -1;
                        lbMusiktitellist.Items.Clear();
                    }

                    grpobj.istMusik = false;
                    AktKlangPlaylist.Hintergrundmusik = false;
                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);

                    ZeigeKlangSongsParallel(grpobj, true);
                    ZeigeKlangTop(grpobj, true);
                    ZeigeZeileKlangSpalten(grpobj, true);

                    for (UInt16 i = 0; i < grpobj._listZeile.Count; i++)
                    {
                        if (grpobj._listZeile[i].audioZeile.chkTitel.IsChecked == true)
                            grpobj._listZeile[i].istStandby = true;
                    }

                    AktualisiereKlangPlaylist();
                    if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen && ((TabItem)tcEditor.SelectedItem).Name == "tiEditor")
                        CheckPlayStandbySongs(grpobj);
                }
                else
                    ZeigeKlangTop(grpobj, true);
            }
            catch (Exception) { }
        }

        private void rbTopIstMusikPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
               // Int16 posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                grpobj.spnlTopHotkey.Visibility = Visibility.Collapsed;
                if (AktKlangPlaylist != null)
                {
                    // *****************  In Playlist-Player herausnehmen  **********************
                    if (lbPListGeräusche.Items.Count != 0)
                    {
                        Int16 i = 0;
                        while (i < lbPListGeräusche.Items.Count && (Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag != AktKlangPlaylist.Audio_PlaylistGUID)
                            i++;

                        if (i < lbPListGeräusche.Items.Count && (Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
                        {
                            if (((MusikZeile)lbPListGeräusche.Items[i]).tbtnCheck.IsChecked.Value)
                                ((MusikZeile)lbPListGeräusche.Items[i]).tbtnCheck.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
                            lbPListGeräusche.Items.RemoveAt(i);
                        }
                    }
                    // *************************************************************************

                    AktKlangPlaylist.Hintergrundmusik = true;
                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    AktualisierePListPlaylist();

                    AlleKlangSongsAus(grpobj, false, false);

                    if (grpobj.wirdAbgespielt)
                    {
                        grpobj.wirdAbgespielt = false;
                        ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                    }

                    ZeigeKlangSongsParallel(grpobj, false);
                    ZeigeKlangTop(grpobj, false);
                    ZeigeZeileKlangSpalten(grpobj, false);

                    AktKlangPlaylist.MaxSongsParallel = 1;
                    grpobj.istMusik = true;

                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    AktualisiereKlangPlaylist();
                }
                else
                    ZeigeKlangTop(grpobj, false);
            }
            catch (Exception) { }
        }

        private void tboxKlangTheme_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    if (AktKlangTheme == null)
                    {
                        List<Audio_Theme> klTheme = Global.ContextAudio.ThemeListe.Where(t => t.Name.Equals(tboxKlangThemeName.Text)).ToList();
                        if (klTheme.Count == 1)
                        {
                            AktKlangTheme = klTheme[0];
                            ((TabItem)lbEditorTheme.SelectedItem).Content = tboxKlangThemeName.Text;
                            tboxKlangThemeName.Text = AktKlangTheme.Name;
                        }
                        else
                        {
                            NeueKlangThemeInDB(tboxKlangThemeName.Text);
                        }
                    }
                    AktKlangTheme.Name = tboxKlangThemeName.Text;
                    ((ListboxItemIcon)lbEditorTheme.SelectedItem).lbText.Content = AktKlangTheme.Name;
                    Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                    ((TextBox)(sender)).Background = null;
                }
                else
                {
                    ((TextBox)(sender)).Background = Brushes.LightSalmon;
                }
            }
            catch (Exception) { }
        }

        private void btnKlangNeuTheme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NeueKlangThemeInDB("");
                tboxKlangThemeName.Focus();
            }
            catch (Exception) { }
        }

        private void pbarBGSong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_BGPlayer.AktPlaylist != null && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                {
                    Point pts = e.GetPosition((sender as ProgressBar));
                    double total = (sender as ProgressBar).Maximum;
                    double res = ((pts.X * 100) / ((double)(sender as ProgressBar).ActualWidth)) / 100;
                    _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position = TimeSpan.FromMilliseconds(total * res);
                }
            }
            catch (Exception) { }
        }

        private void tcEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                tcEditor.Tag = tcEditor.SelectedIndex;
                if (tcEditor.SelectedIndex == tcEditor.Items.Count - 1)
                {
                    if (tcEditor.Items.Count == 1)
                        tcEditor.SelectedIndex = -1;
                    else
                        tcEditor.SelectedIndex = tcEditor.Items.Count - 2;
                }
                string s = "";
                Int16 posObjGruppe;

                if (tcEditor.SelectedIndex == -1)
                {
                    ZeigeKlangSongsParallel(null, false);
                    if (tiEditor.IsSelected && lbEditor.SelectedIndex == -1)
                        tcEditor.SelectedItem = null;
                }
                else
                {
                    if (((TabItem)tcEditor.Items[0]).Header.ToString() == "")
                    {
                        int ver = 0;
                        s = "NeuePlayliste";
                        while (Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(s)).ToList().Count != 0) 
                        {
                            s = "NeuePlayliste-" + ver;
                            ver++;
                        }
                        ((TabItem)tcEditor.SelectedItem).Header = s;
                        tboxKlangThemeName.Text = AktKlangTheme.Name;

                        GruppenObjekt grpobj = new GruppenObjekt();
                        grpobj.btnTopKlangOpen.Focus();

                        //grpobj.seite = tcEditor.SelectedIndex;
                        grpobj.tiEditor = ((TabItem)tcEditor.SelectedItem);
                        grpobj.objGruppe = 0;
                        grpobj.playlistName = s;
                        grpobj.aPlaylist = new Audio_Playlist();
                        grpobj.aPlaylist.Name = s;

                        grpobj.sviewer = sviewerX;
                        grpobj.grdEditor = grdEditorX;
                        grpobj.grdEditorTop = grdEditorTopX;
                        grpobj.tbtnKlangPause = tbtnKlangPauseX;
                        grpobj.brdTopKlangKategorie = brdTopKlangKategorieX;
                        grpobj.tbTopKlangKategorie = tboxTopKlangKategorieX;

                        grpobj.gboxTopSongsParallel = gboxTopSongsParallelX;
                        grpobj.tbTopKlangSongsParallel = tboxTopklangsongparallelX;
                        grpobj.btnTopSongParPlus = btnTopSongParPlusX;
                        grpobj.btnTopSongParMinus = btnTopSongParMinusX;

                        grpobj.gboxTopTyp = gboxTopTypX;
                        grpobj.rbTopIstKlangPlaylist = rbIstTopKlangPlaylistX;
                        grpobj.rbTopIstMusikPlaylist = rbIstTopMusikPlaylistX;

                        grpobj.chkbxTopAktiv = chkbxTopAktivX;
                        grpobj.spnlTopGeräuschIcon = spnlTopGeräuschIconX;
                        grpobj.btnTopVolMin = btnTopVolMinX;
                        grpobj.btnTopVolDown = btnTopVolDownX;
                        grpobj.btnTopVolUp = btnTopVolUpX;
                        grpobj.btnTopVolMax = btnTopVolMaxX;
                        grpobj.chkbxTopVolChange = chkbxTopVolChangeX;
                        grpobj.btnTopPauseMin = btnTopPauseMinX;
                        grpobj.btnTopPauseDown = btnTopPauseDownX;
                        grpobj.btnTopPauseUp = btnTopPauseUpX;
                        grpobj.btnTopPauseMax = btnTopPauseMaxX;
                        grpobj.chkbxTopPauseChange = chkbxTopPauseChangeX;
                        grpobj.btnTopVolMinMinus = btnTopVolMinMinusX;
                        grpobj.btnTopVolMinPlus = btnTopVolMinPlusX;
                        grpobj.btnTopVolMaxMinus = btnTopVolMaxMinusX;
                        grpobj.btnTopVolMaxPlus = btnTopVolMaxPlusX;
                        grpobj.brdTrennstrich = brdTrennstrichX;
                        grpobj.btnTopPauseMinMinus = btnTopPauseMinMinusX;
                        grpobj.btnTopPauseMinPlus = btnTopPauseMinPlusX;
                        grpobj.btnTopPauseMaxMinus = btnTopPauseMaxMinusX;
                        grpobj.btnTopPauseMaxPlus = btnTopPauseMaxPlusX;
                        _GrpObjecte.Add(grpobj);
                        posObjGruppe = GetPosObjGruppe(grpobj.objGruppe);
                        grpobj.tbTopKlangKategorie.Tag = grpobj.aPlaylist.Audio_PlaylistGUID;
                    }
                    else
                    {                       
                        if (tcEditor.SelectedIndex >= 0)
                        {
                            //Int16 objGruppe = GetObjGruppe(tcEditor.SelectedIndex);
                            GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                            if (grpobj == null)
                                return;
                            //posObjGruppe = GetPosObjGruppe(objGruppe); 
                            List<Audio_Playlist> playlistliste = null;

                            if (tcEditor.SelectedItem.GetType() == typeof(TCButtons))
                            {
                                s = ((TCButtons)tcEditor.SelectedItem)._tbText.Text.ToString();
                                playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(((TCButtons)tcEditor.SelectedItem).Tag)).ToList();
                            }
                            else
                                s = ((TabItem)tcEditor.SelectedItem).Header.ToString();

                            if (playlistliste != null && playlistliste.Count != 0 && grpobj._listZeile.Count > 0)
                            {
                                List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]);
                                AktKlangPlaylist = playlistliste[0];

                                if (AktKlangPlaylist.Hintergrundmusik)
                                    grpobj.rbTopIstMusikPlaylist.IsChecked = true;
                                else
                                    grpobj.rbTopIstKlangPlaylist.IsChecked = true;
                                tboxKlangThemeName.Text = AktKlangTheme.Name;
                                grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
                                grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;

                                if (titelliste.Count > 0)
                                {
                                    grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallel_TextChanged;
                                    grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;

                                    grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
                                    grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallel_TextChanged;

                                    ZeigeKlangSongsParallel(grpobj, !playlistliste[0].Hintergrundmusik);
                                    ZeigeKlangTop(grpobj, !playlistliste[0].Hintergrundmusik);
                                    ZeigeZeileKlangSpalten(grpobj, !playlistliste[0].Hintergrundmusik);
                                }
                            }
                            else
                            {
                                lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                                lbEditor.SelectedIndex = -1;
                                lbEditor.SelectionChanged += lbEditor_SelectionChanged;

                                grpobj.rbTopIstKlangPlaylist.IsChecked = rbEditorKlang.IsChecked;
                                grpobj.rbTopIstMusikPlaylist.IsChecked = rbEditorMusik.IsChecked;
                                tboxKlangThemeName.Text = s;
                                ZeigeKlangSongsParallel(grpobj, false);

                                grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallel_TextChanged;
                                if (playlistliste != null && grpobj._listZeile.Count > 0)
                                {
                                    grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;
                                    grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
                                    grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
                                    grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;
                                }
                                else
                                {
                                    grpobj.tbTopKlangSongsParallel.Tag = null;
                                    grpobj.tbTopKlangSongsParallel.Text = "1";
                                    grpobj.tbTopKlangKategorie.Text = "";
                                    grpobj.tbTopKlangKategorie.Tag = null;
                                }
                                grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallel_TextChanged;
                            }
                        }
                        SelektiereKlangZeile(s);
                    }
                }
                if (tcAudioPlayer.SelectedItem == tiEditor && lbEditorTheme.Tag == null) 
                    CheckBtnGleicherPfad(_GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem));
            }
            catch (Exception) { }
        }

        private void SelektiereKlangZeile(string klangGUID)
        {
            int i = 0;
            while (i <= lbEditor.Items.Count - 1)
            {
                if (((ListboxItemIcon)lbEditor.Items[i]).lbText.Content.ToString() == klangGUID)
                {
                    lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                    lbEditor.SelectedIndex = i;
                    lbEditor.SelectionChanged += lbEditor_SelectionChanged;
                    break;
                }
                i++;
            }
        }

        private void tiEditorPlaylistClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang._buttonClose == ((Button)sender));
               /* Int16 posObjGruppe = GetPosObjGruppe(_GrpObjecte.FirstOrDefault(t => t.ticKlang._buttonClose == ((Button)sender)).objGruppe);
                if (posObjGruppe == -1)
                    return;
                int seite = _GrpObjecte[posObjGruppe].seite;
                */
                if (((Button)sender).IsEnabled &&
                    grpobj.aPlaylist != null && rbEditorEditTheme.IsChecked.Value &&
                    AktKlangTheme.Audio_Playlist.Contains(grpobj.aPlaylist))
                {
                    AktKlangTheme.Audio_Playlist.Remove(grpobj.aPlaylist);
                    Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                }

                AlleKlangSongsAus(grpobj, true, false);

                PlaylisteLeeren(grpobj);
                _GrpObjecte.Remove(_GrpObjecte.FirstOrDefault(t => t.ticKlang._buttonClose == ((Button)sender))); 

                if (tcEditor.SelectedIndex == tcEditor.Items.Count - 2)
                {
                    int switchToItem = tcEditor.SelectedIndex - 1;
                    while (switchToItem > 0 && ((TCButtons)tcEditor.Items[switchToItem]).Visibility == Visibility.Collapsed)
                        switchToItem--;
                    tcEditor.SelectedIndex = switchToItem;
                }
                if (tcEditor.SelectedIndex == -1 || ((TCButtons)tcEditor.SelectedItem).Visibility == Visibility.Collapsed)
                {
                    ZeigeKlangGerneral(null, false);
                    tiPlus_MouseUp(null, null);
                    ((TCButtons)tcEditor.SelectedItem).Focus();

                    if (!grpobj.rbTopIstMusikPlaylist.IsChecked.Value && !grpobj.rbTopIstKlangPlaylist.IsChecked.Value)
                        grpobj.rbTopIstKlangPlaylist.IsChecked = true;
                }
                else
                {
                    //seite = tcEditor.SelectedIndex;
                    grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                  //  posObjGruppe = GetPosObjGruppe(GetObjGruppe(seite));
                 //   if (posObjGruppe >= 0)
                    ZeigeKlangGerneral(grpobj, true);
                }
                tcEditor.Tag = tcEditor.SelectedIndex;

                if (AktKlangPlaylist != null)
                {
                    if (AktKlangPlaylist.Hintergrundmusik)
                        ZeigeKlangSongsParallel(grpobj, false);
                    else
                        ZeigeKlangSongsParallel(grpobj, true);
                }
                else
                    ZeigeKlangSongsParallel(null, false);
            }
            catch (Exception) { }
        }

        private void ZeigeKlangGerneral(GruppenObjekt grpobj, bool sichtbar)
        {
            if (sichtbar)
            {
                ZeigeKlangSongsParallel(grpobj, true);
                if (grpobj != null)
                {
                    grpobj.gboxTopTyp.Visibility = Visibility.Visible;
                    grpobj.btnTopKlangOpen.Visibility = Visibility.Visible;
                }
            }
            else
            {
                ZeigeKlangSongsParallel(grpobj, false);
                if (grpobj != null)
                {
                    grpobj.tbTopKlangKategorie.Visibility = Visibility.Visible;
                    grpobj.gboxTopTyp.Visibility = Visibility.Hidden;
                    grpobj.btnTopKlangOpen.Visibility = Visibility.Hidden;
                }
                lbEditor.SelectedIndex = -1;
            }
        }

        private void tiPlus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                tiErstellt++;
                Int16 objGruppe = Convert.ToInt16(tiErstellt);
                AktKlangPlaylist = null;
                bool virtuell = tiPlus.Tag != null ? true : false;

                GruppenObjekt grpobj = new GruppenObjekt();
                
                //grpobj.seite = tcEditor.Items.Count - 3 +1;
                grpobj.objGruppe = tiErstellt;

                if (!virtuell)
                {
                    Grid GridTmp = (Grid)DeepCopy(GridX, "X", objGruppe.ToString());
                    TCButtons tabItem = new TCButtons();

                    tabItem.Visibility = Visibility.Visible;
                    tabItem.Height = 19;
                    tabItem.Name = "tiEditor" + objGruppe;

                    string NeuePlaylist = "NeuePlayliste";
                    int ver = 0;
                    string[] str_tiHeader = new string[tcEditor.Items.Count - 2];

                    for (int i = 0; i < tcEditor.Items.Count - 2; i++)
                        str_tiHeader[i] = ((TCButtons)tcEditor.Items[i])._tbText.Text;


                    while (Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(NeuePlaylist)).ToList().Count != 0 ||
                        str_tiHeader.Contains(NeuePlaylist))
                    {
                        NeuePlaylist = "NeuePlayliste-" + ver;
                        ver++;
                    }
                    tabItem._tbEditText.GotFocus += tbGotFocus;
                    tabItem._tbEditText.LostFocus += tbLostFocus;
                    tabItem._imgOk.MouseDown += _imgOk_MouseDown;
                    tabItem._tbText.Text = NeuePlaylist;
                    
                    tabItem._buttonClose.Click += tiEditorPlaylistClose_Click;

                    tcEditor.Items.Insert(tcEditor.Items.Count - 2, tabItem);
                    tabItem.Content = GridTmp;

                    ScrollViewer scrViewer = (ScrollViewer)GridTmp.FindName("sviewer" + objGruppe);
                    scrViewer.ToolTip = sviewerX.ToolTip;
                    scrViewer.Drop += grdEditorX_Drop;
                    scrViewer.DragEnter += grdEditorX_DragEnter;

                    Grid grdEditor = (Grid)scrViewer.FindName("grdEditor" + objGruppe);
                    WrapPanel wpnl = (WrapPanel)grdEditor.FindName("wpnl" + objGruppe);

                    Grid grdEditorTop = (Grid)grdEditor.FindName("grdEditorTop" + objGruppe);
                    ToggleButton tbtn = (ToggleButton)grdEditorTop.FindName("tbtnKlangPause" + objGruppe);
                    tbtn.Tag = tiErstellt;  //objGruppe
                    tbtn.Checked += tbtnKlangPause_Checked;
                    tbtn.Unchecked += tbtnKlangPause_Unchecked;
                    //tbtn.Click += btnKlangPauseX_Click;
                    
                    TextBox tbTopFilter = (TextBox)grdEditorTop.FindName("tbEditorTopFilter" + objGruppe);
                    tbTopFilter.TextChanged += tbEditorTopFilter_TextChanged;
                    tbTopFilter.LostFocus += tbLostFocus;
                    tbTopFilter.GotFocus += tbGotFocus;

                    Border brdTopKlangKategorie = (Border)grdEditorTop.FindName("brdTopKlangKategorie" + objGruppe);
                    TextBox tbTopKlangKategorie = (TextBox)grdEditorTop.FindName("tboxTopKlangKategorie" + objGruppe);
                    tbTopKlangKategorie.LostFocus += tboxTopKategorie_LostFocus;
                    tbTopKlangKategorie.GotFocus += tbGotFocus;

                    GroupBox gboxTopSongsParallel = (GroupBox)grdEditorTop.FindName("gboxTopSongsParallel" + objGruppe);
                    TextBox tboxTopklangsongparallel = (TextBox)grdEditorTop.FindName("tboxTopklangsongparallel" + objGruppe);
                    tboxTopklangsongparallel.TextChanged += tboxklangsongparallel_TextChanged;
                    tboxTopklangsongparallel.LostFocus += tbLostFocus;
                    tboxTopklangsongparallel.GotFocus += tbGotFocus;

                    Button btnTopSongParPlus = (Button)grdEditorTop.FindName("btnTopSongParPlus" + objGruppe);
                    btnTopSongParPlus.Click += btnSongParPlus_Click;
                    Button btnTopSongParMinus = (Button)grdEditorTop.FindName("btnTopSongParMinus" + objGruppe);
                    btnTopSongParMinus.Click += btnSongParPlus_Click;

                    GroupBox gboxTopTyp = (GroupBox)grdEditorTop.FindName("gboxTopTyp" + objGruppe);
                    RadioButton rbIstTopMusikPlaylist = (RadioButton)grdEditorTop.FindName("rbIstTopMusikPlaylist" + objGruppe);
                    rbIstTopMusikPlaylist.Click += rbTopIstMusikPlaylist_Click;
                    RadioButton rbIstTopKlangPlaylist = (RadioButton)grdEditorTop.FindName("rbIstTopKlangPlaylist" + objGruppe);
                    rbIstTopKlangPlaylist.Click += rbTopIstKlangPlaylist_Click;

                    Button btnTopFilter = (Button)grdEditorTop.FindName("btnKlangTopFilter" + objGruppe);
                    btnTopFilter.Click += btnBGFilter_Click;

                    CheckBox chbxTopAkt = (CheckBox)grdEditorTop.FindName("chkbxTopAktiv" + objGruppe);
                    chbxTopAkt.Tag = objGruppe;
                    chbxTopAkt.Click += chkbxTopAktivX_Click;

                    StackPanel spnlTopGeräuschIcon = (StackPanel)grdEditorTop.FindName("spnlTopGeräuschIcon" + objGruppe);

                    Button btnTopVolMin = (Button)grdEditorTop.FindName("btnTopVolMin" + objGruppe);
                    btnTopVolMin.Click += btnAllVolUp_Click;
                    Button btnTopVolDown = (Button)grdEditorTop.FindName("btnTopVolDown" + objGruppe);
                    btnTopVolDown.Click += btnAllVolUp_Click;
                    Button btnTopVolUp = (Button)grdEditorTop.FindName("btnTopVolUp" + objGruppe);
                    btnTopVolUp.Click += btnAllVolUp_Click;
                    Button btnTopVolMax = (Button)grdEditorTop.FindName("btnTopVolMax" + objGruppe);
                    btnTopVolMax.Click += btnAllVolUp_Click;

                    CheckBox chbxTopVolCh = (CheckBox)grdEditorTop.FindName("chkbxTopVolChange" + objGruppe);
                    chbxTopVolCh.Tag = objGruppe;
                    chbxTopVolCh.Click += chkbxTopVolChangeX_Click;

                    Button btnTopPauseMin = (Button)grdEditorTop.FindName("btnTopPauseMin" + objGruppe);
                    btnTopPauseMin.Click += btnAllPauseUp_Click;
                    Button btnTopPauseDown = (Button)grdEditorTop.FindName("btnTopPauseDown" + objGruppe);
                    btnTopPauseDown.Click += btnAllPauseUp_Click;
                    Button btnTopPauseUp = (Button)grdEditorTop.FindName("btnTopPauseUp" + objGruppe);
                    btnTopPauseUp.Click += btnAllPauseUp_Click;
                    Button btnTopPauseMax = (Button)grdEditorTop.FindName("btnTopPauseMax" + objGruppe);
                    btnTopPauseMax.Click += btnAllPauseUp_Click;

                    CheckBox chbxTopPauseCh = (CheckBox)grdEditorTop.FindName("chkbxTopPauseChange" + objGruppe);
                    chbxTopPauseCh.Tag = objGruppe;
                    chbxTopPauseCh.Click += chkbxTopPauseChangeX_Click;

                    Button btnTopVolMinMinus = (Button)grdEditorTop.FindName("btnTopVolMinMinus" + objGruppe);
                    btnTopVolMinMinus.Click += btnTopVolMinMinusX_Click;
                    Button btnTopVolMinPlus = (Button)grdEditorTop.FindName("btnTopVolMinPlus" + objGruppe);
                    btnTopVolMinPlus.Click += btnTopVolMinPlusX_Click;
                    Button btnTopVolMaxMinus = (Button)grdEditorTop.FindName("btnTopVolMaxMinus" + objGruppe);
                    btnTopVolMaxMinus.Click += btnTopVolMaxMinusX_Click;
                    Button btnTopVolMaxPlus = (Button)grdEditorTop.FindName("btnTopVolMaxPlus" + objGruppe);
                    btnTopVolMaxPlus.Click += btnTopVolMaxPlusX_Click;

                    Border brdTrennstrich = (Border)grdEditorTop.FindName("brdTrennstrich" + objGruppe);

                    Button btnTopPauseMinMinus = (Button)grdEditorTop.FindName("btnTopPauseMinMinus" + objGruppe);
                    btnTopPauseMinMinus.Click += btnTopPauseMinMinusX_Click;
                    Button btnTopPauseMinPlus = (Button)grdEditorTop.FindName("btnTopPauseMinPlus" + objGruppe);
                    btnTopPauseMinPlus.Click += btnTopPauseMinPlusX_Click;
                    Button btnTopPauseMaxMinus = (Button)grdEditorTop.FindName("btnTopPauseMaxMinus" + objGruppe);
                    btnTopPauseMaxMinus.Click += btnTopPauseMaxMinusX_Click;
                    Button btnTopPauseMaxPlus = (Button)grdEditorTop.FindName("btnTopPauseMaxPlus" + objGruppe);
                    btnTopPauseMaxPlus.Click += btnTopPauseMaxPlusX_Click;

                    Button btnTopKlangOpen = (Button)grdEditorTop.FindName("btnTopKlangOpen" + objGruppe);
                    btnTopKlangOpen.Click += btnTopKlangOpen_Click;

                    Button btnTopHotkeySet = (Button)grdEditor.FindName("btnTopHotkeySet" + objGruppe);
                    btnTopHotkeySet.Click += btnTopHotkeySet_Click;
                    StackPanel spnlTopHotkey = (StackPanel)grdEditorTop.FindName("spnlTopHotkey" + objGruppe);
                    ComboBox cmboxTopHotkey = (ComboBox)grdEditorTop.FindName("cmboxTopHotkey" + objGruppe);
                    cmboxTopHotkey.DropDownOpened += cmboxTopHotkey_DropDownOpened;
                    cmboxTopHotkey.SelectionChanged += cmboxTopHotkey_SelectionChanged;
                    cmboxTopHotkey.DropDownClosed += cmboxTopHotkey_DropDownClosed;
                    cmboxTopHotkey.KeyDown += cmboxTopHotkey_KeyDown;
                    Button btnHotkeyEntfernen = (Button)grdEditorTop.FindName("btnHotkeyEntfernen" + objGruppe);
                    btnHotkeyEntfernen.Click += btnHotkeyEntfernen_Click;

                    Button btnTopChPfad = (Button)grdEditorTop.FindName("btnTopChPfad" + objGruppe);
                    btnTopChPfad.Click += btnTopChPfad_Click;

                    Button btnKlangUpdateFiles = (Button)grdEditorTop.FindName("btnKlangUpdateFiles" + objGruppe);
                    btnKlangUpdateFiles.Click += btnKlangUpdateFiles_Click;


                    //**********************************************************************************************************


                    grpobj.ticKlang = tabItem;
                    grpobj.playlistName = NeuePlaylist;
                    grpobj.aPlaylist = new Audio_Playlist();
                    grpobj.aPlaylist.Name = NeuePlaylist;

                    grpobj.sviewer = scrViewer;
                    grpobj.grdEditor = grdEditor;
                    grpobj.grdEditorTop = grdEditorTop;
                    grpobj.wpnl = wpnl;
                    grpobj.tbtnKlangPause = tbtn;

                    grpobj.tbTopFilter = tbTopFilter;
                    grpobj.btnTopFilter = btnTopFilter;
                    grpobj.brdTopKlangKategorie = brdTopKlangKategorie;
                    grpobj.tbTopKlangKategorie = tbTopKlangKategorie;

                    grpobj.gboxTopSongsParallel = gboxTopSongsParallel;
                    grpobj.tbTopKlangSongsParallel = tboxTopklangsongparallel;
                    grpobj.btnTopSongParPlus = btnTopSongParPlus;
                    grpobj.btnTopSongParMinus = btnTopSongParMinus;

                    grpobj.gboxTopTyp = gboxTopTyp;
                    grpobj.rbTopIstKlangPlaylist = rbIstTopKlangPlaylist;
                    grpobj.rbTopIstMusikPlaylist = rbIstTopMusikPlaylist;

                    grpobj.chkbxTopAktiv = chbxTopAkt;
                    grpobj.spnlTopGeräuschIcon = spnlTopGeräuschIcon;
                    grpobj.btnTopVolMin = btnTopVolMin;
                    grpobj.btnTopVolDown = btnTopVolDown;
                    grpobj.btnTopVolUp = btnTopVolUp;
                    grpobj.btnTopVolMax = btnTopVolMax;
                    grpobj.chkbxTopVolChange = chbxTopVolCh;
                    grpobj.btnTopPauseMin = btnTopPauseMin;
                    grpobj.btnTopPauseDown = btnTopPauseDown;
                    grpobj.btnTopPauseUp = btnTopPauseUp;
                    grpobj.btnTopPauseMax = btnTopPauseMax;
                    grpobj.chkbxTopPauseChange = chbxTopPauseCh;
                    grpobj.btnTopVolMinMinus = btnTopVolMinMinus;
                    grpobj.btnTopVolMinPlus = btnTopVolMinPlus;
                    grpobj.btnTopVolMaxMinus = btnTopVolMaxMinus;
                    grpobj.btnTopVolMaxPlus = btnTopVolMaxPlus;
                    grpobj.brdTrennstrich = brdTrennstrich;
                    grpobj.btnTopPauseMinMinus = btnTopPauseMinMinus;
                    grpobj.btnTopPauseMinPlus = btnTopPauseMinPlus;
                    grpobj.btnTopPauseMaxMinus = btnTopPauseMaxMinus;
                    grpobj.btnTopPauseMaxPlus = btnTopPauseMaxPlus;

                    grpobj.btnTopHotkeySet = btnTopHotkeySet;
                    grpobj.spnlTopHotkey = spnlTopHotkey;
                    grpobj.cmboxTopHotkey = cmboxTopHotkey;
                    grpobj.btnHotkeyEntfernen = btnHotkeyEntfernen;
                    grpobj.btnTopKlangOpen = btnTopKlangOpen;
                    grpobj.btnTopChPfad = btnTopChPfad;
                    grpobj.btnKlangUpdateFiles = btnKlangUpdateFiles;

                    tcEditor.SelectedIndex = tcEditor.Items.Count - 3;
                }
                else   //if (virtuell)
                {
                    ToggleButton tbtn = new ToggleButton();
                    tbtn.Name = "tbtnKlangPause" + objGruppe;
                    tbtn.Tag = tiErstellt;  //objGruppe
                    tbtn.Checked += tbtnKlangPause_Checked;
                    tbtn.Unchecked += tbtnKlangPause_Unchecked;
                    //tbtn.Click += btnKlangPauseX_Click;
                    grpobj.tbtnKlangPause = tbtn;
                }

                _GrpObjecte.Add(grpobj);
                //**********************************************************************************************************

                if (!virtuell)
                {
                    lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                    lbEditor.SelectedIndex = -1;
                    lbEditor.SelectionChanged += lbEditor_SelectionChanged;

                    if (tcAudioPlayer.SelectedItem == tiEditor)
                        tboxKlangThemeName.Text = AktKlangTheme != null ? AktKlangTheme.Name : "";
                    grpobj.tbTopKlangKategorie.Text = "";
                    grpobj.tbTopKlangKategorie.Tag = _GrpObjecte[_GrpObjecte.Count - 1].aPlaylist.Audio_PlaylistGUID;
                    ZeigeKlangGerneral(grpobj, true);
                    ZeigeKlangSongsParallel(grpobj, false);

                    grpobj.tbTopKlangSongsParallel.Tag = null;
                    grpobj.tbTopKlangSongsParallel.Text = "1";

                    grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;
                    if (rbEditorEditTheme.IsChecked.Value)
                        grpobj.ticKlang._buttonClose.Visibility = Visibility.Visible;
                }
            }
            catch (Exception) { }
        }

        private void tboxVolMin0_X_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                foreach (var item in e.Text)
                    e.Handled = !char.IsDigit(item);
            }
            catch (Exception) { }
        }

        private void tboxVolMin0_X_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist != null)
                {
                    //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FirstOrDefault(t => t.audioZeile.tboxVolMin == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) > grpobj._listZeile[zeile].volMax_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].volMax_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.VolumeMin = Convert.ToInt16(((TextBox)(sender)).Text);
                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxVolMax.Text) < grpobj._listZeile[zeile].audiotitel.VolumeMin)
                        grpobj._listZeile[zeile].audioZeile.tboxVolMax.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.VolumeMin);

                    plyTitelToSave.Add(grpobj._listZeile[zeile].audiotitel);
                    if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
                }
            }
            catch (Exception) { }
        }

        private void tboxVolMax0_X_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist != null)
                {
                    //Int16 objGruppe = GetObjGruppe(tcEditor.SelectedIndex);
                    //int posObjGruppe = GetPosObjGruppe(objGruppe);
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FirstOrDefault(t => t.audioZeile.tboxVolMax == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) < grpobj._listZeile[zeile].volMin_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].volMin_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.VolumeMax = Convert.ToInt16(((TextBox)(sender)).Text);

                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxVolMin.Text) > grpobj._listZeile[zeile].audiotitel.VolumeMax)
                        grpobj._listZeile[zeile].audioZeile.tboxVolMin.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.VolumeMax);

                    plyTitelToSave.Add(grpobj._listZeile[zeile].audiotitel);
                    if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
                }
            }
            catch (Exception) { }
        }

        private void tboxPauseMin0_X_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist != null)
                {
                   // Int16 objGruppe = GetObjGruppe(tcEditor.SelectedIndex);
                   // int posObjGruppe = GetPosObjGruppe(objGruppe);

                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FirstOrDefault(t => t.audioZeile.tboxPauseMin == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) > grpobj._listZeile[zeile].pauseMax_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].pauseMax_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.PauseMin = Convert.ToInt16(((TextBox)(sender)).Text);

                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text) > grpobj._listZeile[zeile].audiotitel.PauseMax)
                        grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.PauseMax);

                    plyTitelToSave.Add(grpobj._listZeile[zeile].audiotitel);
                    if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
                }
            }
            catch (Exception) { }
        }

        private void tboxPauseMax0_X_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist != null)
                {
                   // Int16 objGruppe = GetObjGruppe(tcEditor.SelectedIndex);
                   // int posObjGruppe = GetPosObjGruppe(objGruppe);
                    
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FirstOrDefault(t => t.audioZeile.tboxPauseMax == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) < grpobj._listZeile[zeile].pauseMin_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].pauseMin_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.PauseMax = Convert.ToInt16(((TextBox)(sender)).Text);

                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text) < grpobj._listZeile[zeile].audiotitel.PauseMin)
                        grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.PauseMin);

                    plyTitelToSave.Add(grpobj._listZeile[zeile].audiotitel);
                    if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
                }
            }
            catch (Exception) { }
        }

        private void _btnVolMinMinus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));

                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                    (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.volMin_wert - VolSprung;

                if (sollWert <= klZeile.audioZeile.sldKlangVol.Maximum)
                    klZeile.volMin_wert = sollWert < 0 ? 0 : sollWert;
                else
                    klZeile.volMin_wert = Convert.ToInt16(klZeile.audioZeile.sldKlangVol.Minimum);
                klZeile.audioZeile.tboxVolMin.Text = Convert.ToString(klZeile.volMin_wert);
                klZeile.audiotitel.VolumeMin = klZeile.volMin_wert;
                klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                    (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }


        private void _btnVolMaxMinus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));

                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                    (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.volMax_wert - VolSprung;
                int max = Convert.ToInt16(klZeile.audioZeile.sldKlangVol.Maximum);

                if (sollWert <= max)
                    klZeile.volMax_wert = sollWert < 0 ? 0 : sollWert;
                else
                    klZeile.volMax_wert = max;
                klZeile.audioZeile.tboxVolMax.Text = Convert.ToString(klZeile.volMax_wert);
                if (klZeile.volMax_wert < Convert.ToInt16(klZeile.audioZeile.tboxVolMin.Text))
                {
                    klZeile.audioZeile.tboxVolMin.Text = klZeile.audioZeile.tboxVolMax.Text;
                    klZeile.volMin_wert = klZeile.volMax_wert;
                }
                klZeile.audiotitel.VolumeMax = klZeile.volMax_wert;
                klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                    (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnVolMinPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                   (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.volMin_wert + VolSprung;
                int max = Convert.ToInt16(klZeile.audioZeile.sldKlangVol.Maximum);

                if (sollWert >= klZeile.audioZeile.sldKlangVol.Minimum)
                    klZeile.volMin_wert = sollWert > max ? max : sollWert;
                else
                    klZeile.volMin_wert = max;
                klZeile.audioZeile.tboxVolMin.Text = Convert.ToString(klZeile.volMin_wert);
                if (klZeile.volMin_wert > Convert.ToInt16(klZeile.audioZeile.tboxVolMax.Text))
                {
                    klZeile.audioZeile.tboxVolMax.Text = klZeile.audioZeile.tboxVolMin.Text;
                    klZeile.volMax_wert = klZeile.volMin_wert;
                }
                klZeile.audiotitel.VolumeMin = klZeile.volMin_wert;
                klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                    (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnVolMaxPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                    (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.volMax_wert + VolSprung;
                int max = Convert.ToInt32(klZeile.audioZeile.sldKlangVol.Maximum);

                klZeile.volMax_wert = sollWert < max ? sollWert : max;

                klZeile.audioZeile.tboxVolMax.Text = Convert.ToString(klZeile.volMax_wert);
                klZeile.audiotitel.VolumeMax = klZeile.volMax_wert;
                klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                    (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMinMinus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                    (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.pauseMin_wert - PauseSprung;

                if (sollWert <= klZeile.audioZeile.sldKlangPause.Maximum)
                    klZeile.pauseMin_wert = sollWert <= 0 ? 0 : sollWert;
                else
                    klZeile.pauseMin_wert = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Minimum);

                klZeile.audioZeile.tboxPauseMin.Text = Convert.ToString(klZeile.pauseMin_wert);
                klZeile.audiotitel.PauseMin = klZeile.pauseMin_wert;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMaxMinus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                    (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.pauseMax_wert - PauseSprung;
                int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

                if (sollWert <= max)
                    klZeile.pauseMax_wert = sollWert < 0 ? 0 : sollWert;
                else
                    klZeile.pauseMax_wert = max;

                klZeile.audioZeile.tboxPauseMax.Text = Convert.ToString(klZeile.pauseMax_wert);
                if (klZeile.pauseMax_wert < Convert.ToInt16(klZeile.audioZeile.tboxPauseMin.Text))
                {
                    klZeile.audioZeile.tboxPauseMin.Text = klZeile.audioZeile.tboxPauseMax.Text;
                    klZeile.pauseMin_wert = klZeile.pauseMax_wert;
                }
                klZeile.audiotitel.PauseMax = klZeile.pauseMax_wert;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMinPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                   (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.pauseMin_wert + PauseSprung;
                int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

                if (sollWert >= Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Minimum))
                    klZeile.pauseMin_wert = sollWert > max ? max : sollWert;
                else
                    klZeile.pauseMin_wert = max;
                klZeile.audioZeile.tboxPauseMin.Text = Convert.ToString(klZeile.pauseMin_wert);
                if (klZeile.pauseMin_wert > Convert.ToInt16(klZeile.audioZeile.tboxPauseMax.Text))
                {
                    klZeile.audioZeile.tboxPauseMax.Text = klZeile.audioZeile.tboxPauseMin.Text;
                    klZeile.pauseMax_wert = klZeile.pauseMin_wert;
                }
                klZeile.audiotitel.PauseMin = klZeile.pauseMin_wert;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMaxPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                    (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.pauseMax_wert + PauseSprung;
                int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

                klZeile.pauseMax_wert = sollWert < max ? sollWert : max;
                klZeile.audioZeile.tboxPauseMax.Text = Convert.ToString(klZeile.pauseMax_wert);
                klZeile.audiotitel.PauseMax = klZeile.pauseMax_wert;

                plyTitelToSave.Add(klZeile.audiotitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void tbtnKlangPause_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.tbtnKlangPause == ((ToggleButton)sender));
                if (grpobj == null)
                    return;
                if (tiEditor.IsSelected)
                    grpobj.sollBtnGedrueckt++;

                grpobj.wirdAbgespielt = true;

                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    grpobj._listZeile[i].istPause = false;

                    if (grpobj._listZeile[i].istLaufend && grpobj._listZeile[i].audiotitel.Aktiv)
                    {
                        grpobj._listZeile[i].FadingOutStarted = false;
                        FadingIn_Started = false;
                        if (grpobj.ticKlang.Visibility == Visibility.Collapsed)
                            FadingIn(grpobj._listZeile[i]._mplayer,
                                    (grpobj._listZeile[i].Aktuell_Volume / 100) * (grpobj.Vol_ThemeMod) / 100);
                        else
                        {
                            FadingIn(grpobj._listZeile[i]._mplayer, grpobj._listZeile[i].Aktuell_Volume / 100);
                            grpobj.sviewer.ScrollToVerticalOffset(i * grpobj._listZeile[i].audioZeile.ActualHeight);
                        }
                    }
                    else
                        if (!grpobj._listZeile[i].istPause && !grpobj._listZeile[i].istLaufend &&
                            grpobj._listZeile[i].audiotitel.Aktiv) // .audioZeile.chkTitel.IsChecked.Value)
                            grpobj._listZeile[i].istStandby = true;
                        else
                            grpobj._listZeile[i].istStandby = false;
                }
                CheckPlayStandbySongs(grpobj);
                if (grpobj.visuell)
                    ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));

                if (grpobj.totalTimePlylist != -1)
                {
                    aPlaylistLengthCheck = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(grpobj.aPlaylist.Audio_PlaylistGUID)).FirstOrDefault();
                    grpobj.totalTimePlylist = -1;
                    if (aPlaylistLengthCheck != null)
                        GetTotalLength();
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Felhler", "Beim Anwählen der Pause-Funktion für die Playlist ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void tbtnKlangPause_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.tbtnKlangPause == ((ToggleButton)sender));
                if (grpobj == null)
                    return;
                if (tiEditor.IsSelected)
                    grpobj.sollBtnGedrueckt--;
                grpobj.wirdAbgespielt = false;

                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    grpobj._listZeile[i].istPause = true;

                    if (!grpobj.istMusik)
                    {
                        if (grpobj._listZeile[i]._mplayer != null)
                        {
                            grpobj._listZeile[i]._mplayer.Pause();
                            grpobj._listZeile[i].istStandby = false;
                            grpobj._listZeile[i].istLaufend = false;
                        }
                    }
                    else
                    {
                        if (!grpobj._listZeile[i].FadingOutStarted && grpobj._listZeile[i].istLaufend)
                        {
                            grpobj._listZeile[i].FadingOutStarted = true;
                            FadingOut(grpobj._listZeile[i], false, true);
                        //    grpobj._listZeile[i].istLaufend = false;
                            grpobj._listZeile[i].istStandby = true;
                        }
                    }
                }
                CheckPlayStandbySongs(grpobj);
                if (grpobj.visuell)
                    ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
   
                if (grpobj.totalTimePlylist != -1)
                {
                    aPlaylistLengthCheck = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(grpobj.aPlaylist.Audio_PlaylistGUID)).FirstOrDefault();
                    grpobj.totalTimePlylist = -1;
                    if (aPlaylistLengthCheck != null)
                        GetTotalLength();
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgemeiner Felhler", "Beim Abwählen der Pause-Funktion für die Playlist ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

      /*  private void btnKlangPauseX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.tbtnKlangPause == ((ToggleButton)sender));
                if (grpobj == null)
                    return;
                if (tiEditor.IsSelected)
                {
                    if (grpobj.wirdAbgespielt)
                        grpobj.sollBtnGedrueckt--;
                    else
                        grpobj.sollBtnGedrueckt++;
                }
                grpobj.wirdAbgespielt = !grpobj.wirdAbgespielt;

                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    grpobj._listZeile[i].istPause = !grpobj.wirdAbgespielt;

                    if (!grpobj.wirdAbgespielt)
                    {
                        if (!grpobj.istMusik)
                        {
                            if (grpobj._listZeile[i]._mplayer != null)
                            {
                                grpobj._listZeile[i]._mplayer.Pause();
                                grpobj._listZeile[i].istStandby = false;
                                grpobj._listZeile[i].istLaufend = false;
                            }
                        }
                        else
                        {
                            if (!grpobj._listZeile[i].FadingOutStarted)
                            {
                                grpobj._listZeile[i].FadingOutStarted = true;
                                FadingOut(grpobj._listZeile[i], false, true);
                            }
                        }
                    }
                    else
                        if (!grpobj._listZeile[i].istPause && grpobj._listZeile[i].istLaufend &&
                            grpobj._listZeile[i].audiotitel.Aktiv) //  ._listZeile[i].audioZeile.chkTitel.IsChecked.Value)
                        {
                            grpobj._listZeile[i].FadingOutStarted = false;
                            FadingIn_Started = false;
                            if (grpobj.ticKlang.Visibility == Visibility.Collapsed)
                                FadingIn(grpobj._listZeile[i]._mplayer,
                                        (grpobj._listZeile[i].Aktuell_Volume / 100) * (grpobj.Vol_ThemeMod) / 100);
                            else
                            {
                                FadingIn(grpobj._listZeile[i]._mplayer, grpobj._listZeile[i].Aktuell_Volume / 100);
                                grpobj.sviewer.ScrollToVerticalOffset(i * grpobj._listZeile[i].audioZeile.ActualHeight);
                            }
                        }
                        else
                            if (!grpobj._listZeile[i].istPause && !grpobj._listZeile[i].istLaufend &&
                                grpobj._listZeile[i].audiotitel.Aktiv) // .audioZeile.chkTitel.IsChecked.Value)
                                grpobj._listZeile[i].istStandby = true;
                            else
                                grpobj._listZeile[i].istStandby = false;
                }
                CheckPlayStandbySongs(grpobj);
                if (grpobj.visuell)
                {
                    if (!grpobj.wirdAbgespielt)
                        grpobj.btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                    else
                        grpobj.btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
                }

                if (grpobj.totalTimePlylist != -1)
                {
                    aPlaylistLengthCheck = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(grpobj.aPlaylist.Audio_PlaylistGUID)).FirstOrDefault();
                    grpobj.totalTimePlylist = -1;
                    if (aPlaylistLengthCheck != null)
                        GetTotalLength();
                }
            }
            catch (Exception) { }
        }*/

        private void tboxklangsongparallel_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist != null && tcEditor.SelectedIndex >= 0)
                {
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    if (grpobj == null)
                        return;
                    try
                    {
                        if (Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) >= 0 &&
                            Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) != AktKlangPlaylist.MaxSongsParallel)
                        {
                            if (Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) > AktKlangPlaylist.Audio_Playlist_Titel.Count)
                                grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.Audio_Playlist_Titel.Count.ToString();
                            AktKlangPlaylist.MaxSongsParallel = Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text);
                            
                            if (grpobj.wirdAbgespielt)
                                CheckPlayStandbySongs(grpobj);

                            try { Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist); }
                            catch (Exception ex)
                            {
                                var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                                errWin.ShowDialog();
                                errWin.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var errWin = new MsgWindow("Eingabefehler", "Ungültige Eingabe. Bitte geben Sie nur Ganzzahlwert ein.", ex);
                        errWin.ShowDialog();
                        errWin.Close();
                        grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.MaxSongsParallel;
                        grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnSongParPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int dif = Convert.ToInt32(((Button)sender).Tag);
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                int momentan = Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text);
                int max = Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Tag);

                if ((dif > 0 && dif + momentan <= max) ||
                   ((dif < 0 && dif + momentan >= 0)))
                {
                    grpobj.tbTopKlangSongsParallel.Text = (Convert.ToInt32(grpobj.tbTopKlangSongsParallel.Text) + dif).ToString();
                    grpobj.aPlaylist.MaxSongsParallel = Convert.ToUInt16(grpobj.tbTopKlangSongsParallel.Text);
                }
            }
            catch (Exception) { }
        }

        public class AutoGreyableImage : Image
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AutoGreyableImage"/> class.
            /// </summary>
            static AutoGreyableImage()
            {
                // Override the metadata of the IsEnabled property.
                IsEnabledProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnAutoGreyScaleImageIsEnabledPropertyChanged)));
            }
            /// <summary>
            /// Called when [auto grey scale image is enabled property changed].
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
            private static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
            {
                var autoGreyScaleImg = source as AutoGreyableImage;
                var isEnable = Convert.ToBoolean(args.NewValue);
                if (autoGreyScaleImg != null)
                {
                    if (!isEnable)
                    {
                        // Get the source bitmap
                        var bitmapImage = new BitmapImage(new Uri(autoGreyScaleImg.Source.ToString()));
                        // Convert it to Gray
                        autoGreyScaleImg.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);
                        // Create Opacity Mask for greyscale image as FormatConvertedBitmap does not keep transparency info
                        autoGreyScaleImg.OpacityMask = new ImageBrush(bitmapImage);
                    }
                    else
                    {
                        // Set the Source property to the original value.
                        autoGreyScaleImg.Source = ((FormatConvertedBitmap)autoGreyScaleImg.Source).Source;
                        // Reset the Opcity Mask
                        autoGreyScaleImg.OpacityMask = null;
                    }
                }
            }
        }
        
        private void sldKlangVol0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (_GrpObjecte[0] != null)
                {
                    UInt16 seite = 0;
                    KlangZeile klZeile = null;
                    while (klZeile == null)
                    {
                        klZeile = _GrpObjecte[seite]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(((Slider)e.Source).Tag));
                        seite++;
                    }
                    klZeile.Aktuell_Volume = Convert.ToInt16(Math.Round(e.NewValue));
                    klZeile.audioZeile.sldKlangVol.ToolTip = klZeile.Aktuell_Volume + " %";

                    klZeile.audiotitel.Volume = Convert.ToInt16(klZeile.Aktuell_Volume);

                    if (tcAudioPlayer.SelectedItem == tcEditor)         // Nur Speichern wenn im Tab-Editor
                    {
                        plyTitelToSave.Add(klZeile.audiotitel);
                        if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
                    }
                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(_GrpObjecte[seite].aPlaylist);
                }
            }
            catch (Exception) { }
        }

        private void sldPlaySpeed0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (_GrpObjecte.Count > 0 && _GrpObjecte[0] != null)
                {
                    //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                    GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                    if (grpobj == null)
                        return;

                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FirstOrDefault(t => t.audioZeile.sldPlaySpeed == (Slider)sender));

                    if (zeile >= 0)
                    {
                        double speed = grpobj._listZeile[zeile].audioZeile.sldPlaySpeed.Value;
                        if (grpobj._listZeile[zeile]._mplayer != null)
                            grpobj._listZeile[zeile]._mplayer.SpeedRatio = speed;

                        grpobj._listZeile[zeile].audiotitel.Speed = speed;

                        if (tcAudioPlayer.SelectedItem == tcEditor)         // Nur Speichern wenn im Tab-Editor
                        {
                            plyTitelToSave.Add(grpobj._listZeile[zeile].audiotitel);
                            if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
                        }

                        string geschw = "Abspielgeschwindigkeit: ";

                        geschw += speed == .1 ? "sehr langsam" :
                                  speed == .5 ? "langsam" :
                                  speed == .75 ? "gedrosselt" :
                                  speed == 1 ? "normal" :
                                  speed == 2 ? "erhöht" :
                                  speed == 3 ? "schnell" :
                                  speed == 4 ? "sehr schnell" :
                                  speed == 5 ? "ultra schnell" : "nicht definiert";
                        grpobj._listZeile[zeile].audioZeile.sldPlaySpeed.ToolTip = geschw;
                        grpobj._listZeile[zeile].playspeed = speed;
                    }
                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
                }
            }
            catch
            {
                var errWin = new MsgWindow("Datenbankfehler", "Ändern der Geschwindigkeit des Titel konnte nicht durchgeführt werden.");
                errWin.ShowDialog();
                errWin.Close();
            }
        }


        private void sldKlangPause0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (_GrpObjecte.Count > 0 && _GrpObjecte[0] != null)
                {
                    UInt16 seite = 0;
                    KlangZeile klZeile = null;
                    while (klZeile == null)
                    {
                        klZeile = _GrpObjecte[seite]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(((Slider)e.Source).Tag));
                        seite++;
                    }
                    klZeile.audioZeile.sldKlangPause.ToolTip = (e.NewValue < 1000) ? e.NewValue + " ms" : e.NewValue / 1000 + " sek.";
                    klZeile.audiotitel.Pause = Convert.ToInt32(e.NewValue);

                    if (tcAudioPlayer.SelectedItem == tcEditor)         // Nur Speichern wenn im Tab-Editor
                    {
                        plyTitelToSave.Add(klZeile.audiotitel);
                        if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
                    }
                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(_GrpObjecte[seite].aPlaylist);
                }
            }
            catch (Exception) { }
        }

        private void btnAllVolUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                double d = Convert.ToDouble(((sender) as Button).Tag);

                grpobj._listZeile.FindAll(t => t.audioZeile.lbiEditorRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(t => t.audioZeile.sldKlangVol.Value += d);
            }
            catch (Exception) { }
        }

        private void btnAllPauseUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                double d = Convert.ToDouble(((sender) as Button).Tag);

                grpobj._listZeile.FindAll(t => t.audioZeile.lbiEditorRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
                    {
                        if (d == 1 && klZeile.audioZeile.sldKlangPause.Value != klZeile.audioZeile.sldKlangPause.Maximum)
                            klZeile.audioZeile.sldKlangPause.Value = klZeile.audioZeile.sldKlangPause.Ticks.Where(t => t > klZeile.audioZeile.sldKlangPause.Value).Min();
                        else
                            if (d == -1 && klZeile.audioZeile.sldKlangPause.Value != 0)
                                klZeile.audioZeile.sldKlangPause.Value = klZeile.audioZeile.sldKlangPause.Ticks.Where(t => t < klZeile.audioZeile.sldKlangPause.Value).Max();
                            else
                                if (d == 2)
                                    klZeile.audioZeile.sldKlangPause.Value = klZeile.audioZeile.sldKlangPause.Maximum;
                                else
                                    if (d == -2)
                                        klZeile.audioZeile.sldKlangPause.Value = 0;
                    });
            }
            catch (Exception) { }
        }

        private void _imgOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist != null)
                {
                    AktKlangPlaylist.Name = Convert.ToString(((Image)sender).Tag);
                    for (int i = 0; i <= lbEditor.Items.Count - 1; i++)
                        if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
                        {
                            ((ListboxItemIcon)lbEditor.Items[i]).lbText.Content = AktKlangPlaylist.Name;
                            break;
                        }
                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                }
            }
            catch (Exception) { }
        }

        private void tboxKlangTheme_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((TextBox)(sender)).Background != null && AktKlangPlaylist != null)
                {
                    ((TextBox)(sender)).Text = AktKlangTheme.Name;
                    ((TextBox)(sender)).Background = null;
                }
                tbLostFocus(null, null);
            }
            catch (Exception) { }
        }


        private void tiEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)          //nur wenn TabItem gewechselt wurde
                {
                    tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;

                    rbEditorEditTheme.IsEnabled = true;
                    if (lbEditor.Items.Count == 0)
                        AktualisiereKlangPlaylist();

                    if (tcEditor.SelectedItem != null)
                    {
                        if (tcEditor.SelectedItem.GetType() == typeof(TabItem))
                        {
                            tiPlus_MouseUp(null, null);
                            ((TCButtons)tcEditor.SelectedItem).Focus();
                        }
                        else
                            if (tcEditor.SelectedItem.GetType() == typeof(TCButtons) &&
                                ((TCButtons)tcEditor.Items[Convert.ToInt32(tcEditor.Tag)]).Visibility == Visibility.Visible)
                            {
                                tcEditor.SelectedItem = ((TCButtons)tcEditor.Items[Convert.ToInt32(tcEditor.Tag)]);

                                foreach (ListboxItemIcon lbi in lbEditor.Items)
                                {
                                    if (lbi.lbText.Content.ToString() == ((TCButtons)tcEditor.SelectedItem)._tbText.Text)
                                    {
                                        lbEditor.SelectedItem = lbi;
                                        lbEditor.ScrollIntoView(lbEditor.SelectedItem);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                bool found = false;
                                for (int i = 0; i < tcEditor.Items.Count; i++)
                                {
                                    if (((TCButtons)tcEditor.Items[i]).Visibility == Visibility.Visible)
                                    {
                                        found = true;
                                        tcEditor.Tag = i;
                                        tcEditor.SelectedItem = ((TCButtons)tcEditor.Items[i]);
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    tcEditor.SelectedIndex = -1;
                                    ZeigeKlangGerneral(null, false);
                                    tiPlus_MouseUp(null, null);
                                }
                            }
                    }

                    foreach (KlangZeile kZeile in _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcAudioPlayer.SelectedItem)._listZeile) // [GetPosObjGruppe(GetObjGruppe(tcAudioPlayer.SelectedIndex))]._listZeile)
                        UpdateKlangZeileRatingVisuell(kZeile);
                }
            }
            catch (Exception) { }
        }

        private void tiMusik_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)          //nur wenn TabItem gewechselt wurde
            {
                tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;
                AktualisiereMusikPlaylist();
            }
        }

        private void tiPList_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)      //nur wenn TabItem gewechselt wurde
                {
                    tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;
                    slPlaylistMusikVolume.Value = slBGVolume.Value;
                    AktualisierePListPlaylist();
                    if (lbPListMusik.SelectedIndex == -1 && lbBackground.SelectedIndex != -1)
                    {
                        lbPListMusik.SelectionChanged -= lbPListMusik_SelectionChanged;
                        lbPListMusik.SelectedIndex = lbBackground.SelectedIndex;
                        lbPListMusik.SelectionChanged += lbPListMusik_SelectionChanged;
                    }
                    AktualisierePlaylistThemes();
                    if ((Guid)wpnlPListThemes.Tag != Guid.Empty)
                    {
                        foreach (ToggleButton tbtn in wpnlPListThemes.Children)
                            if ((Guid)tbtn.Tag == (Guid)wpnlPListThemes.Tag)
                                tbtn.IsChecked = true;
                    }
                }
            }
            catch (Exception) { }
        }
        
        private void btnTopKlangOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string bezugsDir = Directory.Exists(stdPfad) ? stdPfad : "C:\\";
                if (AktKlangPlaylist != null)
                {
                    List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
                    if (titelliste.Count > 0)
                    {
                        bezugsDir = titelliste[0].Pfad.LastIndexOf(@"\") != -1 ? titelliste[0].Pfad.Substring(0, titelliste[0].Pfad.LastIndexOf(@"\")) : titelliste[0].Pfad;
                        bezugsDir = (bezugsDir.Substring(1, 1) != ":") ? stdPfad + @"\" + bezugsDir : bezugsDir;

                        titelliste.ForEach(delegate(Audio_Titel aTitel)
                        {
                            string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ? stdPfad + @"\" + aTitel.Pfad : aTitel.Pfad;

                            while (!vergleich.StartsWith(bezugsDir))
                            {
                                if (bezugsDir.Contains(@"\"))
                                    bezugsDir = bezugsDir.Substring(0, bezugsDir.LastIndexOf(@"\"));
                                else break;
                            }
                        });
                    }
                }

                // Konfiguren des Öffnen Ddialogs
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.CheckFileExists = true;
                dlg.Multiselect = true;
                dlg.DefaultExt = ".mp3;.wav;.wma;.ogg"; // Extensionen
                dlg.Filter = "Alle Musikdateien |*.mp3;*.wav;*.wma;*.ogg|MP3-Dateien|*.mp3|Wave-Dateien|*.wav|Windows Media Player-Dateien|*.wma|OGG-Dateien|*.ogg"; // Filter Dateien pro extension
                dlg.InitialDirectory = bezugsDir;

                // Zeige File-Öffnen Dialog
                Nullable<bool> result = dlg.ShowDialog();

                // Öffnen bestätigt
                if (result == true)
                {
                    // Öffne das Dokument
                    string filename = dlg.FileName;
                    bool hinzugefuegt = false;
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        if (dlg.FileNames.Length != 0)
                        {
                            if (AktKlangPlaylist == null)
                                NeueKlangPlaylistInDB();

                            string[] extension = new String[4] { ".mp3", ".wav", ".wma", ".ogg" };

                            foreach (string dateihinzu in dlg.FileNames)
                            {
                                if (Array.IndexOf(extension, dateihinzu.Substring(dateihinzu.Length - 4)) != -1)
                                {
                                    KlangDateiHinzu(dateihinzu);
                                    hinzugefuegt = true;
                                }
                            }
                            if (hinzugefuegt)
                            {
                                if (lbEditor.SelectedIndex == -1)
                                {
                                    for (int i = 0; i < lbEditor.Items.Count; i++)
                                        if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
                                            lbEditor.SelectedIndex = i;
                                }

                                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                                if (grpobj == null)
                                    return;

                                if (grpobj.rbTopIstKlangPlaylist.IsChecked == true)
                                    AktKlangPlaylist.Hintergrundmusik = false;
                                else
                                    AktKlangPlaylist.Hintergrundmusik = true;

                                if (AktKlangPlaylist.Hintergrundmusik)
                                {
                                    ZeigeKlangSongsParallel(grpobj, false);
                                    ZeigeKlangTop(grpobj, false);
                                }
                                else
                                {
                                    ZeigeKlangSongsParallel(grpobj, true);
                                    ZeigeKlangTop(grpobj, true);
                                }
                                CheckAlleAngehakt(grpobj);
                                CheckChPfadHatBezug(grpobj);
                                grpobj.grdEditor.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                        Global.ContextAudio.Save();
                        int i = tcEditor.SelectedIndex;
                        tcEditor.SelectedIndex = -1;
                        tcEditor.SelectedIndex = i;
                        CheckBtnGleicherPfad(_GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem));//  GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex)));
                    }
                }
            }
            catch (Exception) { }
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CustomMessage("Codec Add-On Hinweis", "OGG-Dateien integrieren",
                    "OGG-Dateien können nach dem Installieren eines entsprechenden AddOns bzw. " + Environment.NewLine +
                    "Codec-Packs ebenfalls wiedergegeben werden." + Environment.NewLine + Environment.NewLine +
                    "Ein entsprechendes Codec bietet das 'Media Player Codec Pack' und " + Environment.NewLine +
                    "kann unter folgender Adresse heruntergeladen werden:" + Environment.NewLine + Environment.NewLine,
                    "http://www.mediaplayercodecpack.com/");
            }
            catch (Exception) { }
        }


        private void btnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ((((Button)(sender)).Parent as Grid).Parent as Window).Close();
            }
            catch (Exception) { }
        }

        private void CustomMessage(string s_titel, string s_top, string s_mitte, string s_url)
        {
            Window w = new Window();
            Grid grd = new Grid();
            Border brd = new Border();
            TextBlock txTop = new TextBlock();
            TextBlock tx = new TextBlock();
            Paragraph parx = new Paragraph();
            Button btn = new Button();
            ScrollViewer scr = new ScrollViewer();

            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            rowDef1.Height = new GridLength(55);
            rowDef2.Height = new GridLength(1, GridUnitType.Star);
            rowDef3.Height = new GridLength(65);

            grd.RowDefinitions.Add(rowDef1);
            grd.RowDefinitions.Add(rowDef2);
            grd.RowDefinitions.Add(rowDef3);

            brd.Background = Brushes.Lavender;
            grd.Children.Add(brd);
            Grid.SetRow(brd, 0);
            txTop.Text = s_top;
            txTop.FontSize = 16;
            txTop.Margin = new Thickness(10);
            grd.Children.Add(txTop);
            Grid.SetRow(txTop, 0);

            grd.Children.Add(scr);
            Grid.SetRow(scr, 1);

            Run run1 = new Run(s_mitte);
            run1.FontSize = 12;
            run1.TextDecorations = null;
            run1.FontWeight = FontWeights.Normal;

            tx.TextWrapping = TextWrapping.Wrap;
            tx.Margin = new Thickness(10);
            tx.Inlines.Add(run1);

            if (s_url != null)
            {
                Run run2 = new Run(s_url);
                Hyperlink hyperl = new Hyperlink(run2);
                hyperl.NavigateUri = new Uri(s_url);
                hyperl.Click += hyperlink_Click;
                tx.Inlines.Add(hyperl);
            }

            scr.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scr.Content = tx;

            btn.Content = "OK";
            btn.MinWidth = 50;
            btn.Margin = new Thickness(10);
            btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            btn.Click += btnClick;

            grd.Children.Add(btn);
            Grid.SetRow(btn, 2);

            w.Content = grd;
            w.Title = s_titel;
            w.MinHeight = 300;
            w.MinWidth = 250;
            w.Width = 600;
            w.Height = 300;
            w.Show();
        }

        private void hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(((Hyperlink)sender).NavigateUri.AbsoluteUri);
            }
            catch (Exception) { }
        }

        private void btnTopVolMinMinusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnVolMinMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void btnTopVolMinPlusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnVolMinPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void btnTopVolMaxMinusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnVolMaxMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void btnTopVolMaxPlusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnVolMaxPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void btnTopPauseMinMinusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnPauseMinMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void btnTopPauseMinPlusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnPauseMinPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void btnTopPauseMaxMinusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnPauseMaxMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void btnTopPauseMaxPlusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem)._listZeile. // [GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex))]._listZeile.
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
                    FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
                    ForEach(t => _btnPauseMaxPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
            }
            catch (Exception) { }
        }

        private void chkbxTopAktivX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool soll = ((CheckBox)(e.Source)).IsChecked.Value;
                UInt16 objGruppe = Convert.ToUInt16(((CheckBox)sender).Tag);
                int posObjGruppe = GetPosObjGruppe(objGruppe);

                _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked.Value != soll).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.audioZeile.chkTitel.IsChecked = soll;
                    klZeile.audiotitel.Aktiv = soll;
                    klZeile.audioZeile.chkTitel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                        
                    plyTitelToSave.Add(klZeile.audiotitel);
                });

                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                if (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).Count == _GrpObjecte[posObjGruppe]._listZeile.Count &&
                    ((CheckBox)(e.Source)).IsChecked == true)
                {
                    //Zufallsaktivierung der Zeilen
                    List<KlangZeile> klZeileAktiv;
                    klZeileAktiv = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.playable == true).FindAll(t => t.audiotitel.Aktiv);

                    while (klZeileAktiv.Count > 0)
                    {
                        int zeileIndex = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(klZeileAktiv[(new Random()).Next(0, klZeileAktiv.Count)]);
                        chkTitel0_0_Click(_GrpObjecte[posObjGruppe]._listZeile[zeileIndex].audioZeile.chkTitel, null);

                        klZeileAktiv = klZeileAktiv.FindAll(t => t.istLaufend != true);
                        klZeileAktiv = klZeileAktiv.FindAll(t => t.istStandby != true);
                    }
                }
                ((CheckBox)(e.Source)).IsChecked = soll;
            }
            catch (Exception) { }
        }

        private void chkbxTopVolChangeX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int16 objGruppe = Convert.ToInt16(((CheckBox)sender).Tag);
                int posObjGruppe = GetPosObjGruppe(objGruppe);
                bool changeto = ((CheckBox)sender).IsChecked.Value;

                _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked == true).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.audioZeile.chkVolMove.IsChecked = changeto;
                    klZeile.audiotitel.VolumeChange = changeto;
                    plyTitelToSave.Add(klZeile.audiotitel);
                });
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                _GrpObjecte[posObjGruppe].anzVolChange = Convert.ToUInt16(
                    _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkVolMove.IsChecked == true).Count);
                CheckAlleAngehakt(_GrpObjecte[posObjGruppe]);
            }
            catch (Exception) { }
        }


        private void chkbxTopPauseChangeX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int16 objGruppe = Convert.ToInt16(((CheckBox)sender).Tag);
                int posObjGruppe = GetPosObjGruppe(objGruppe);
                bool changeto = ((CheckBox)sender).IsChecked.Value;

                _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked == true).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.audioZeile.chkKlangPauseMove.IsChecked = changeto;
                    klZeile.audiotitel.PauseChange = changeto;               
                    plyTitelToSave.Add(klZeile.audiotitel);
                });                
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

                _GrpObjecte[posObjGruppe].anzPauseChange = Convert.ToUInt16(
                    _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkKlangPauseMove.IsChecked == true).Count);
                CheckAlleAngehakt(_GrpObjecte[posObjGruppe]);
            }
            catch (Exception) { }
        }


        private void chkbxPlayRange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((CheckBox)sender).IsChecked.Value)
                    rsldTeilSong.Visibility = Visibility.Visible;
                else
                    rsldTeilSong.Visibility = Visibility.Hidden;

                _BGPlayer.AktPlaylistTitel.TeilAbspielen = chkbxPlayRange.IsChecked.Value;
                if (!_BGPlayer.AktPlaylistTitel.TeilAbspielen)
                {
                    _BGPlayer.AktPlaylistTitel.TeilStart = null;
                    _BGPlayer.AktPlaylistTitel.TeilEnde = null;
                }
                else
                {
                    _BGPlayer.AktPlaylistTitel.TeilStart = rsldTeilSong.LowerValue;
                    _BGPlayer.AktPlaylistTitel.TeilEnde = rsldTeilSong.UpperValue;
                }
                plyTitelToSave.Add(_BGPlayer.AktPlaylistTitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
            }
            catch (Exception) { }
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnShuffleImg.Source = ((ToggleButton)sender).IsChecked == true ?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/shuffle.png")) :
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/no_shuffle.png"));
            }
            catch (Exception) { }
        }
        
        private void AlleKlangzeilenSpeichern(int posObjGruppe)
        {
            for (int i = 0; i < _GrpObjecte[posObjGruppe]._listZeile.Count; i++)
                    plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[i].audiotitel);
            
            if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
        }

        public void _rbtnGleichSpielen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsInitialized)
                    MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen = (bool)_rbtnGleichSpielen.IsChecked;
            }
            catch (Exception) { }
        }
        
        public void BGFadingOut(Musik BG, bool playerStoppen, bool sofort)
        {
            DispatcherTimer _timer = new DispatcherTimer();

            double stVol = BG.mPlayer.Volume;
            double aktVol = 0;
            _timer.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

            DateTime fadOutStart = DateTime.MinValue;
            double _vergangeneZeit;

            _timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                if (fadOutStart == DateTime.MinValue)
                    fadOutStart = DateTime.Now; 
                
                _vergangeneZeit = DateTime.Now.Subtract(fadOutStart).TotalMilliseconds;

                if (!sofort && _vergangeneZeit > 5000)  //maximale Wartezeit zum FadingOut
                    sofort = true;

                if (FadingIn_Started || sofort)
                {
                    sofort = true;
                    if (BG.FadingOutStarted)                                //solange Volume runterregeln bis der Titel extern das Fadingstoppt
                    {
                        if (BG.mPlayer != null)
                        {
                            aktVol = (fadingTime != 0) ? stVol - stVol * (_vergangeneZeit / (fadingTime * fadingIntervall)) : 0;
                            aktVol = aktVol < 0 ? 0 : aktVol;

                            if (BG.mPlayer.Volume != aktVol)
                                BG.mPlayer.Volume = aktVol;

                            if (BG.mPlayer.Volume == 0)
                            {
                                if (playerStoppen && BG.FadingOutStarted)   //bei Volume 0 Fadingauf false und MediaPlayer freigeben
                                {
                                    BG.mPlayer.Stop();
                                    BG.mPlayer.Close();
                                }
                                if (!playerStoppen && BG.FadingOutStarted)
                                {
                                    BG.mPlayer.Pause();
                                    BG.mPlayer.Position = TimeSpan.FromMilliseconds(0);
                                    BG.isPaused = true;
                                }

                                BG.FadingOutStarted = false;
                                _timer.Stop();
                            }
                        }
                        else
                        {
                            BG.FadingOutStarted = false;
                            _timer.Stop();
                        }
                    }
                    else
                        _timer.Stop();
                }
            });
            _timer.Start();
        }

        public void FadingOut(KlangZeile klZeile, bool playerStoppen, bool sofort)
        {
            DispatcherTimer _timer = new DispatcherTimer();

            double stVol = (klZeile._mplayer != null) ? klZeile._mplayer.Volume : .5;
            double aktVol = 0;

            _timer.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

            DateTime fadOutStart = DateTime.MinValue;
            double _vergangeneZeit;

            _timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                if (fadOutStart == DateTime.MinValue)
                    fadOutStart = DateTime.Now;
                _vergangeneZeit = DateTime.Now.Subtract(fadOutStart).TotalMilliseconds;

                if (!sofort && _vergangeneZeit > 5000)                                 //maximale Wartezeit zum FadingOut
                    sofort = true;

                if (FadingIn_Started || sofort)
                {
                    sofort = true;
                    if (klZeile.FadingOutStarted)                                   //solange Volume runterregeln bis der Titel extern das Fadingstoppt
                    {
                        if (klZeile._mplayer != null)
                        {
                            aktVol = (fadingTime != 0) ? stVol - stVol * (_vergangeneZeit / (fadingTime * fadingIntervall)) : 0;
                            aktVol = aktVol < 0 ? 0 : aktVol;

                            if (klZeile._mplayer.Volume != aktVol)
                                klZeile._mplayer.Volume = aktVol;
                                                        
                            if (klZeile._mplayer.Volume == 0)
                            {
                                if (playerStoppen && klZeile.FadingOutStarted)     //bei Volume 0 Fadingauf false und MediaPlayer freigeben
                                {
                                    klZeile._mplayer.Stop();
                                    klZeile._mplayer.Close();
                                }
                                if (!playerStoppen && klZeile.FadingOutStarted)
                                {
                                    klZeile._mplayer.Pause();
                                    klZeile._mplayer.Position = TimeSpan.FromMilliseconds(0);
                                }
                                klZeile.FadingOutStarted = false;
                                _timer.Stop();
                            }
                        }
                        else
                        {
                            klZeile.FadingOutStarted = false;
                            _timer.Stop();
                        }
                    }
                    else
                        _timer.Stop();
                }
            });
            _timer.Start();
        }

        public void FadingIn(MediaPlayer mplayer, double zielVol)
        {
            DispatcherTimer _timer = new DispatcherTimer();

            double aktVol = 0;
            _timer.Interval = TimeSpan.FromMilliseconds(fadingIntervall);
            DateTime fadInStart = DateTime.MinValue;

            _timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                if (fadInStart == DateTime.MinValue)
                    fadInStart = DateTime.Now;
                FadingIn_Started = true;
                double _vergangeneZeit = DateTime.Now.Subtract(fadInStart).TotalMilliseconds;

                aktVol = (fadingTime != 0) ? zielVol * (_vergangeneZeit / (fadingTime * fadingIntervall)) : zielVol;

                if (slBGVolume.Value / 100 != zielVol)        // wenn während des Fading die Lautstärke verändert wird
                    zielVol = slBGVolume.Value / 100;

                if (mplayer != null)
                    mplayer.Volume = aktVol;
                
                if (stopFadingIn || mplayer.Volume >= zielVol)
                {
                    _timer.Stop();
                    FadingIn_Started = false;
                }
            });
            mplayer.Volume = 0;
            mplayer.Play();
            _timer.Start();
        }


        public void btnEditorGewichtung_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                KlangZeile kZeile = grpobj._listZeile.FirstOrDefault(t => t.audioZeile.btnGewichtung == (Button)sender);

                kZeile.audiotitel.Rating = (Convert.ToInt32(kZeile.audioZeile.btnGewichtung.Tag));
                Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.audiotitel);

                if (grpobj.NochZuSpielen.FindAll(t => t == kZeile.audiotitel.Audio_TitelGUID).Count <= kZeile.audiotitel.Rating)
                {
                    //Von x Sterne auf x+1 - also eins hinzufügen
                    grpobj.NochZuSpielen.Add(kZeile.audiotitel.Audio_TitelGUID);

                    if (_BGPlayer.AktPlaylistTitel != null &&
                        _BGPlayer.AktPlaylistTitel.Audio_PlaylistGUID == kZeile.audiotitel.Audio_TitelGUID)
                    {
                        _BGPlayer.NochZuSpielen.Add(kZeile.audiotitel.Audio_TitelGUID);
                        starsUpdate();
                    }
                }
                else
                {
                    //Von 5 Sterne auf 0 zurückgesetzt - also alle löschen und eins hinzufügen
                    grpobj.NochZuSpielen.RemoveAll(t => t == kZeile.audiotitel.Audio_TitelGUID);
                    if (grpobj.NochZuSpielen.FindAll(t => t == kZeile.audiotitel.Audio_TitelGUID).Count <= kZeile.audiotitel.Rating)
                        grpobj.NochZuSpielen.Add(kZeile.audiotitel.Audio_TitelGUID);

                    if (_BGPlayer.AktPlaylistTitel != null &&
                        _BGPlayer.AktPlaylistTitel.Audio_PlaylistGUID == kZeile.audiotitel.Audio_TitelGUID)
                    {
                        _BGPlayer.NochZuSpielen.RemoveAll(t => t == kZeile.audiotitel.Audio_TitelGUID);
                        _BGPlayer.NochZuSpielen.Add(kZeile.audiotitel.Audio_TitelGUID);
                        starsUpdate();
                    }
                }

            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Speicherfehler", "Die Gewichtung des Titels konnte nicht vorgenommen werden.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        public void UpdateKlangZeileRatingVisuell(KlangZeile kZeile)
        {            
            if (kZeile.audiotitel.Rating != Convert.ToInt32(kZeile.audioZeile.btnGewichtung.Tag))
            {
                kZeile.audioZeile.btnGewichtung.Click -= btnEditorGewichtung_Click;
                while (kZeile.audiotitel.Rating != Convert.ToInt32(kZeile.audioZeile.btnGewichtung.Tag))
                    kZeile.audioZeile.btnGewichtung.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                kZeile.audioZeile.btnGewichtung.Click += btnEditorGewichtung_Click;
            }
        }

        public void imgBGStern_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Convert.ToInt16(((Image)sender).Tag) == 1 && _BGPlayer.AktPlaylistTitel.Rating == 1)
                    _BGPlayer.AktPlaylistTitel.Rating = 0;
                else
                    _BGPlayer.AktPlaylistTitel.Rating = Convert.ToInt16(((Image)sender).Tag);

                plyTitelToSave.Add(_BGPlayer.AktPlaylistTitel);
                if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
                starsUpdate();
            }
            catch (Exception) { }
        }

        public void starsUpdate()
        {
            imgBGStern0.Source = (_BGPlayer.AktPlaylistTitel.Rating >= 1) ?
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
            imgBGStern1.Source = (_BGPlayer.AktPlaylistTitel.Rating >= 2) ?
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
            imgBGStern2.Source = (_BGPlayer.AktPlaylistTitel.Rating >= 3) ?
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
            imgBGStern3.Source = (_BGPlayer.AktPlaylistTitel.Rating >= 4) ?
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
            imgBGStern4.Source = (_BGPlayer.AktPlaylistTitel.Rating == 5) ?
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu.png")) :
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/neu_grau.png"));
        }

        public void tiUebersicht_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tcEditor_vorher = tcEditor.SelectedIndex;
                tcEditor_vorherTag = Convert.ToInt16(tcEditor.Tag);

                AktKlangPlaylist = null;
                AktKlangTheme = null;
            }
            catch (Exception) { }
        }

        public void tiUebersicht_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tcEditor.SelectedIndex = tcEditor_vorher;
                tcEditor.Tag = tcEditor_vorherTag;

                if (rbEditorEditTheme.IsChecked.Value)
                    rbEditorEditTheme_UnChecked(null, null);
            }
            catch (Exception) { }
        }

        public void _sldFading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                fadingTime = _sldFading.Value;
                _sldFading.ToolTip = Math.Round(e.NewValue / 100, 1) + " Sekunden In-/Out-Fading";
                if (IsInitialized)
                    MeisterGeister.Logic.Settings.Einstellungen.Fading = Convert.ToInt32(e.NewValue);
            }
            catch (Exception) { }
        }

        public void GetTotalLength()
        {
            if (aPlaylistLengthCheck.Länge == 0)
            {
                double gesLänge = 0;
                for (int i = 0; i < aPlaylistLengthCheck.Audio_Playlist_Titel.Count; i++)
                    if (aPlaylistLengthCheck.Audio_Playlist_Titel.ElementAt<Audio_Playlist_Titel>(i).Audio_Titel.Länge.HasValue)
                        gesLänge += aPlaylistLengthCheck.Audio_Playlist_Titel.ElementAt(i).Audio_Titel.Länge.Value;
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MediaPlayer mp = new MediaPlayer();
            string file;
            TimeSpan totalLength = TimeSpan.FromMilliseconds(0);

            try
            {
                Audio_Playlist plylst = aPlaylistLengthCheck;
                for (int i = 0; i < plylst.Audio_Playlist_Titel.Count; i++)
                {
                    file = plylst.Audio_Playlist_Titel.ElementAt(i).Audio_Titel.Pfad;
                    if (file.Substring(1, 1) != ":")
                        file = (stdPfad.EndsWith(@"\")) ? (stdPfad + file) : (stdPfad + @"\" + file);

                    if (Directory.Exists(System.IO.Path.GetDirectoryName(file)) && File.Exists(file))
                    {
                        mp.Volume = 0;
                        mp.Open(new Uri(file));
                        mp.Play();
                        if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 4000))
                        {
                            mp.Pause();
                            if (plylst != aPlaylistLengthCheck)
                            {
                                totalLength = TimeSpan.FromMilliseconds(0);
                                mp.Close();
                                break;
                            }
                            totalLength += mp.NaturalDuration.TimeSpan;
                            if (aPlaylistLengthCheck.Audio_Playlist_Titel.Count >= i + 1)
                                aPlaylistLengthCheck.Audio_Playlist_Titel.ElementAt(i).Länge = mp.NaturalDuration.TimeSpan.TotalMilliseconds;
                        }
                        mp.Stop();
                        mp.Close();
                    }
                }
                aPlaylistLengthCheck.Länge = totalLength.TotalMilliseconds;
            }
            catch (Exception)
            { }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null && aPlaylistLengthCheck.Länge != 0)
                {
                    Global.ContextAudio.Update<Audio_Playlist>(aPlaylistLengthCheck);
                    if (_BGPlayer.AktPlaylist == aPlaylistLengthCheck)
                    {
                        _BGPlayer.totalLength = aPlaylistLengthCheck.Länge;
                        ((MusikZeile)lbBackground.SelectedItem).tblkLänge.Text = TimeSpan.FromMilliseconds(aPlaylistLengthCheck.Länge).ToString(@"hh\:mm\:ss");
                        ((MusikZeile)lbPListMusik.SelectedItem).tblkLänge.Text = ((MusikZeile)lbBackground.SelectedItem).tblkLänge.Text;

                        for (int i = 0; i < lbPListMusik.Items.Count; i++)
                        {
                            if ((Guid)((MusikZeile)lbPListMusik.Items[i]).Tag == (Guid)(aPlaylistLengthCheck.Audio_PlaylistGUID))
                            {
                                ((MusikZeile)lbPListMusik.Items[i]).tblkLänge.Text = TimeSpan.FromMilliseconds(aPlaylistLengthCheck.Länge).ToString(@"hh\:mm\:ss");
                                break;
                            }
                        }
                        GruppenObjekt grpObj = _GrpObjecte.FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == (Guid)(aPlaylistLengthCheck.Audio_PlaylistGUID));
                        if (grpObj != null)
                            grpObj.totalTimePlylist = aPlaylistLengthCheck.Länge;

                    }
                    else
                    {
                        for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                        {
                            if ((Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag == (Guid)(aPlaylistLengthCheck.Audio_PlaylistGUID))
                            {
                                ((MusikZeile)lbPListGeräusche.Items[i]).tblkLänge.Text = TimeSpan.FromMilliseconds(aPlaylistLengthCheck.Länge).ToString(@"hh\:mm\:ss");
                                break;
                            }
                        }
                        GruppenObjekt grpObj = _GrpObjecte.FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == (Guid)(aPlaylistLengthCheck.Audio_PlaylistGUID));
                        if (grpObj != null)
                            grpObj.totalTimePlylist = aPlaylistLengthCheck.Länge;
                    }
                }
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                (sender as BackgroundWorker).Dispose();
            }
        }

        private void tbEditorPlaylistFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lbEditor.Items.Count; i++)
                    ((ListboxItemIcon)lbEditor.Items[i]).Visibility = ((ListboxItemIcon)lbEditor.Items[i]).lbText.Content.ToString().ToLower().Contains(tbEditorPlaylistFilter.Text.ToLower()) ?
                        Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception) { }
        }

        private void tbEditorThemeFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lbEditorTheme.Items.Count; i++)
                    ((ListboxItemIcon)lbEditorTheme.Items[i]).Visibility = ((ListboxItemIcon)lbEditorTheme.Items[i]).lbText.Content.ToString().ToLower().Contains(tbEditorThemeFilter.Text.ToLower()) ?
                        Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception) { }
        }

        private void tbEditorTopFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcEditor.SelectedIndex));
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                if (grpobj == null)
                    return;
                grpobj._listZeile.FindAll(t => t.audioZeile.lbiEditorRow != null).ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.audioZeile.Visibility = (klZeile.audioZeile.chkTitel.Content.ToString().ToLower().Contains(((TextBox)(e.Source)).Text.ToLower())) ? Visibility.Visible : Visibility.Collapsed;
                });
            }
            catch (Exception) { }

        }

        private void tbKBGFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lbBackground.Items.Count; i++)
                {
                    ((MusikZeile)lbBackground.Items[i]).Visibility =
                        ((MusikZeile)lbBackground.Items[i]).tblkTitel.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ||
                        ((MusikZeile)lbBackground.Items[i]).tboxKategorie.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ?
                          Visibility.Visible : Visibility.Collapsed;
                }
            }
            catch (Exception) { }
        }

        private void btnBGFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((TextBox)((Grid)((Button)(e.Source)).Parent).Children.OfType<TextBox>().First()).Text == "")
                    ((TextBox)((Grid)((Button)(e.Source)).Parent).Children.OfType<TextBox>().First()).Text = " ";
                ((TextBox)((Grid)((Button)(e.Source)).Parent).Children.OfType<TextBox>().First()).Text = "";
            }
            catch (Exception) { }
        }


        
        public void _btnStdPfad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _GrpObjecte.FindAll(t => t._listZeile.Exists(t2 => t2.istLaufend)).ForEach(delegate(GruppenObjekt grpobj)
                {
                    grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                });

                var dialog = new System.Windows.Forms.FolderBrowserDialog();

                dialog.SelectedPath = MeisterGeister.Logic.Settings.Einstellungen.AudioVerzeichnis;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                    _tbStdPfad.Text = dialog.SelectedPath; 
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Eingabefehler", "Das Auswählen des Standard-Verzeichnisses hat eine Exeption ausgelöst.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }


        public void _tbStdPfad_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (IsInitialized)
                {
                    _btnStdPfad.Tag = _tbStdPfad.Text;
                    MeisterGeister.Logic.Settings.Einstellungen.SetEinstellung("AudioVerzeichnis", _tbStdPfad.Text);
                    stdPfad = _tbStdPfad.Text;
                }
            }
            catch (Exception) { }
        }

        public void slVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                ((Slider)sender).Value += (e.Delta > 1) ? 3 : -3;
            }
            catch (Exception) { }
        }

        public void sldPause_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                ((Slider)sender).Value = (e.Delta > 1) ?
                    ((((Slider)sender).Value != ((Slider)sender).Maximum) ?
                        ((Slider)sender).Ticks.First(t => t > ((Slider)sender).Value) :
                        ((Slider)sender).Value = ((Slider)sender).Maximum) :
                    (e.Delta < 1 && ((Slider)sender).Value != ((Slider)sender).Minimum) ?
                        ((Slider)sender).Ticks[((Slider)sender).Ticks.IndexOf(((Slider)sender).Value) - 1] : 0;
            }
            catch (Exception) { }
        }
        
        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e != null && e.Source != null && e.Source is TCButtons
                    && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
                {
                    TCButtons item = (TCButtons)(e.Source);
                    DragDrop.DoDragDrop(item, item, DragDropEffects.All);
                }
            }
            catch (Exception) { }
        }
        
        private void tbtnMusikZeileBtnCheck_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                //btnPListPListAbspielen.IsEnabled = false;

                MusikZeile mZeile = null;
                foreach (MusikZeile zeile in lbPListGeräusche.Items)
                {
                    if (zeile.tbtnCheck == (ToggleButton)sender)
                    {
                        mZeile = zeile;
                        break;
                    }
                }

                GruppenObjekt grpobj = ((ToggleButton)sender).Tag == null ? null : _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.objGruppe == Convert.ToInt32(((ToggleButton)sender).Tag));
                
                if (grpobj == null)
                    grpobj = _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));

                Guid war = AktKlangPlaylist != null ? AktKlangPlaylist.Audio_PlaylistGUID : Guid.Empty;
                int warTag = Convert.ToInt32(tcEditor.Tag);
                if (grpobj == null)
                {
                    tcEditor.SelectedIndex = tcEditor.Items.Count - 2;
                    tiPlus.Tag = false;
                    tiPlus_MouseUp(null, null);
                    tiPlus.Tag = null;
                    ((ToggleButton)sender).Tag = _GrpObjecte[_GrpObjecte.Count - 1].objGruppe;
                    string sTitel = ((TextBlock)((StackPanel)((ToggleButton)e.Source).Parent).FindName("tblkTitel")).Text;

                    int plylstItemPos = 0;
                    while (((ListboxItemIcon)lbEditor.Items[plylstItemPos]).lbText.Content.ToString() != sTitel)
                        plylstItemPos++;


                 //   lbEditor.SelectedIndex = plylstItemPos;
                 //   ((TCButtons)tcEditor.SelectedItem).Visibility = Visibility.Collapsed;

                    grpobj = _GrpObjecte.FirstOrDefault(t => t.objGruppe == tiErstellt);

                    //Get Playlist
                    grpobj.aPlaylist = Global.ContextAudio.PlaylistListe.
                            FirstOrDefault(t => t.Audio_PlaylistGUID.Equals(((ListboxItemIcon)lbEditor.Items[plylstItemPos]).Tag));

                    if (grpobj.aPlaylist != null)
                    {
                        AktKlangPlaylist = grpobj.aPlaylist;
                        grpobj.visuell = false;
                        grpobj.istMusik = AktKlangPlaylist.Hintergrundmusik;
                        grpobj.playlistName = AktKlangPlaylist.Name;

                        foreach (Audio_Playlist_Titel aPlaylistTitel in AktKlangPlaylist.Audio_Playlist_Titel)
                        {
                            KlangNewRow(aPlaylistTitel.Audio_Titel.Pfad, grpobj, 0, aPlaylistTitel);

                            if (aPlaylistTitel.Aktiv &&
                                !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
                            {
                                for (int i = 0; i <= aPlaylistTitel.Rating; i++)
                                    grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                            }
                        }
                    }

                 //   grpObj.ticKlang._tbText.Text = grpObj.playlistName + "Collapsed";
                }
                grpobj.Vol_PlaylistMod = Convert.ToUInt16(slPlaylistVolume.Value);
                grpobj.Vol_ThemeMod = 100;
                //grpobj.ticKlang._buttonExport.Visibility = Visibility.Visible;

                if ((!btnPListPListAbspielen.IsEnabled && Logic.Settings.Einstellungen.AudioDirektAbspielen) ||
                    (Convert.ToBoolean(btnPListPListAbspielen.Tag) && 
                     !grpobj.wirdAbgespielt && 
                     (Logic.Settings.Einstellungen.AudioDirektAbspielen ||
                     _GrpObjecte.FindAll(t => t.wirdAbgespielt).FindAll(t => t.tiEditor == null).Count != 0)))    //Abspielen
                {
                    int old_tcEditorTag = Convert.ToInt16(tcEditor.Tag);
                    tcEditor.Tag = tcEditor.Items.IndexOf(grpobj.ticKlang); //.seite;
                    grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                    if (grpobj.visuell)
                        ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
                    btnPListPListAbspielen.Tag = true;
                    tcEditor.Tag = old_tcEditorTag;

                    if (grpobj._listZeile.FindAll(t => t.playable).Count != grpobj._listZeile.FindAll(t => t.audiotitel.Aktiv).Count)
                    {
                        mZeile.spnlMusikZeile.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 80));   // Brushes.Yellow
                        mZeile.spnlMusikZeile.ToolTip = grpobj._listZeile.FindAll(t => t.playable).Count + " von " + grpobj._listZeile.FindAll(t => t.audiotitel.Aktiv).Count + " Titel abspielbar";
                    }

                    foreach (MusikZeile _mZeile in lbPListGeräusche.Items)
                    {
                        if (_mZeile.tbtnCheck.IsChecked.Value && _mZeile.tbtnCheck != ((ToggleButton)sender))
                        {
                            GruppenObjekt grpObjAlleAnderen = _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(_mZeile.tbtnCheck.Tag));
                            if (grpObjAlleAnderen != null && grpObjAlleAnderen.wirdAbgespielt != grpobj.wirdAbgespielt)
                            {
                                old_tcEditorTag = Convert.ToInt16(tcEditor.Tag);
                                tcEditor.Tag = tcEditor.Items.IndexOf(grpObjAlleAnderen.ticKlang);// .seite;
                                grpObjAlleAnderen.wirdAbgespielt = !grpobj.wirdAbgespielt;
                                grpObjAlleAnderen.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                                ((Image)grpObjAlleAnderen.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
                                tcEditor.Tag = old_tcEditorTag;
                            }
                        }
                    }
                }
           //     foreach (MusikZeile mZ in lbPListGeräusche.Items)
          //          if (mZ.tbtnCheck == ((ToggleButton)sender))
          //              mZ.grdForceVol.ColumnDefinitions[0].Width = new GridLength(110);

               // btnPListPListAbspielen.IsEnabled = true;

                checkPListPlaybtnGeräusche();
                tcEditor.Tag = warTag;
                tcEditor.SelectedItem = tcEditor.Items[Convert.ToInt32(tcEditor.Tag)];
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgmeiner Fehler", "Beim Anwählen der Geräusche-Playlist ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void tbtnMusikZeileBtnCheck_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                btnPListPListAbspielen.IsEnabled = false;
                GruppenObjekt grpobj = ((ToggleButton)sender).Tag == null ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(((ToggleButton)sender).Tag));               
                
                ((StackPanel)((Grid)((StackPanel)((ToggleButton)e.Source).Parent).Parent).Parent).Background = null;
                ((StackPanel)((Grid)((StackPanel)((ToggleButton)e.Source).Parent).Parent).Parent).ToolTip = null;
                
             //   Guid war = AktKlangPlaylist.Audio_PlaylistGUID;
             //   int warTag = Convert.ToInt32(tcEditor.Tag);
                if (grpobj.wirdAbgespielt)
                {
             //       tcEditor.Tag = tcEditor.Items.IndexOf(grpobj.ticKlang); //.seite;
                    grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
                   // grpobj.btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                    
                    if (_GrpObjecte.FindAll(t => t.wirdAbgespielt).Count == 0)
                        btnPListPListAbspielen.Tag = false;

                    if (grpobj.changed)
                    {
                        _GrpObjecte.Remove(grpobj);
                       // grpobj.ticKlang._buttonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        ((ToggleButton)sender).Tag = null;
                    }
                }
                foreach (MusikZeile mZ in lbPListGeräusche.Items)
                    if (mZ.tbtnCheck == ((ToggleButton)sender))
                    {
                     //   mZ.grdForceVol.ColumnDefinitions[0].Width = new GridLength(0);
                        if (mZ.chkbxForceVol.IsChecked.Value)
                        {
                            mZ.chkbxForceVol.IsChecked = false;
                            mZ.chkbxForceVol.RaiseEvent(new RoutedEventArgs(CheckBox.ClickEvent));
                        }
                    }
                btnPListPListAbspielen.IsEnabled = true;
                checkPListPlaybtnGeräusche();
          //      tcEditor.Tag = warTag;
                tcEditor.SelectedItem = tcEditor.Items[Convert.ToInt32(tcEditor.Tag)];
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Allgmeiner Fehler", "Beim Abwählen der Geräusche-Playlist ist ein Fehler aufgetreten", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void checkPListPlaybtnGeräusche()
        {
            bool found = false;
            foreach(MusikZeile mZeile in lbPListGeräusche.Items)
            {
                if (mZeile.tbtnCheck.IsChecked.Value)
                {
                    found = true;
                    break;
                }
            }
            btnPListPListAbspielen.IsEnabled = found;

            btnimgPListPListAbspielen.Source = !Convert.ToBoolean(btnPListPListAbspielen.Tag) ?
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")) :
                new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
        }

        private void AktualisierePListPlaylist()
        {
            UInt16 posMusik = 0;
            UInt16 posGeräusche = 0;
            Global.ContextAudio.PlaylistListe.OrderBy(t => t.Name).ToList().ForEach(delegate(Audio_Playlist playlistliste) 
            {
                MusikZeile mZeile = new MusikZeile();
                mZeile.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                mZeile.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                mZeile.Tag = playlistliste.Audio_PlaylistGUID;
                mZeile.Cursor = Cursors.Hand;

                mZeile.tbtnCheck.Tag = null;
                mZeile.tbtnCheck.Checked += tbtnMusikZeileBtnCheck_Checked;
                mZeile.tbtnCheck.Unchecked += tbtnMusikZeileBtnCheck_UnChecked;
                mZeile.tblkTitel.Text = playlistliste.Name;
                mZeile.tblkLänge.Text = (playlistliste.Länge != 0) ? TimeSpan.FromMilliseconds(playlistliste.Länge).ToString(@"hh\:mm\:ss") : "";
                mZeile.tboxKategorie.Tag = mZeile.Tag;
                mZeile.tboxKategorie.Text = playlistliste.Kategorie;
                mZeile.tboxKategorie.GotFocus += tbGotFocus;
                mZeile.tboxKategorie.LostFocus += tboxTopKategorie_LostFocus;

                if (playlistliste.Hintergrundmusik)
                {
                    mZeile.pbarSong.MouseLeftButtonDown += pbarBGSong_MouseLeftButtonDown;
                    mZeile.tbtnCheck.Visibility = Visibility.Collapsed;
                    if (posMusik + 1 > lbPListMusik.Items.Count)
                        lbPListMusik.Items.Add(mZeile);
                    else
                    {
                        ((MusikZeile)(lbPListMusik.Items[posMusik])).Tag = mZeile.Tag;
                        if (((MusikZeile)(lbPListMusik.Items[posMusik])).tblkTitel.Text != mZeile.tblkTitel.Text)
                            ((MusikZeile)(lbPListMusik.Items[posMusik])).tblkTitel.Text = mZeile.tblkTitel.Text;
                        if (((MusikZeile)(lbPListMusik.Items[posMusik])).tboxKategorie.Text != mZeile.tboxKategorie.Text)
                            ((MusikZeile)(lbPListMusik.Items[posMusik])).tboxKategorie.Text = mZeile.tboxKategorie.Text;
                    }
                    posMusik++;
                }
                else
                {
                    mZeile.tbtnCheck.Visibility = Visibility.Visible;
                    mZeile.grdForceVol.ColumnDefinitions[0].Width = new GridLength(90);
                    mZeile.grdForceVol.ColumnDefinitions[1].Width = new GridLength(70);
                    mZeile.chkbxForceVol.Tag = playlistliste;
                    mZeile.chkbxForceVol.Click += chkbxForceVol_Click;
                    mZeile.sldForceVolume.Tag = playlistliste;
                    mZeile.sldForceVolume.ValueChanged += sldForceVolume_ValueChanged;

                    if (posGeräusche + 1 > lbPListGeräusche.Items.Count)
                        lbPListGeräusche.Items.Add(mZeile);
                    else
                    {
                        ((MusikZeile)lbPListGeräusche.Items[posGeräusche]).Name = mZeile.Name;
                        ((MusikZeile)(lbPListGeräusche.Items[posGeräusche])).Tag = mZeile.Tag;
                        if (((MusikZeile)(lbPListGeräusche.Items[posGeräusche])).tblkTitel.Text != mZeile.tblkTitel.Text)
                            ((MusikZeile)(lbPListGeräusche.Items[posGeräusche])).tblkTitel.Text = mZeile.tblkTitel.Text;
                    }
                    posGeräusche++;
                }
            });

            if (lbPListMusik.Items.Count > posMusik && lbPListMusik.Items.Count != 0)
            {
                for (int i = posMusik; i < lbPListMusik.Items.Count; i++)
                    lbPListMusik.Items.RemoveAt(i);
            }
            if (lbPListGeräusche.Items.Count > posGeräusche && lbPListGeräusche.Items.Count != 0)
            {
                for (int i = posGeräusche; i < lbPListGeräusche.Items.Count; i++)
                    lbPListGeräusche.Items.RemoveAt(i);
            }
        }

        private void chkbxForceVol_Click(object sender, RoutedEventArgs e)
        {
            MusikZeile mZeile = (MusikZeile)((StackPanel)((Grid)((Grid)((CheckBox)sender).Parent).Parent).Parent).Parent;

            Audio_Playlist aPlaylist = (Audio_Playlist)((CheckBox)sender).Tag;
                
            GruppenObjekt grpObj = mZeile.tbtnCheck.Tag == null ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));

            if (grpObj != null)
            {
                if (mZeile.chkbxForceVol.IsChecked.Value)
                    grpObj.force_Volume = mZeile.sldForceVolume.Value / 100;
                else
                    grpObj.force_Volume = null;
            }
        }

        private void sldForceVolume_ValueChanged(object sender, RoutedEventArgs e)
        {
            MusikZeile mZeile = (MusikZeile)((StackPanel)((Grid)((Grid)((Slider)sender).Parent).Parent).Parent).Parent;
            Audio_Playlist aPlaylist = (Audio_Playlist)((Slider)sender).Tag;

            GruppenObjekt grpObj = mZeile.tbtnCheck.Tag == null ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));

            if (grpObj != null)
            {
                if (mZeile.chkbxForceVol.IsChecked.Value)
                    grpObj.force_Volume = mZeile.sldForceVolume.Value / 100;
                else
                    grpObj.force_Volume = null;
            }
            ((Slider)sender).ToolTip = Math.Round(((Slider)sender).Value) + " %";
        }


        private void tbPListGeräusche_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string[] split = tbPListGeräusche.Text.ToLower().Split(new Char[] { ' ', ',' });

                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                {
                    ((MusikZeile)lbPListGeräusche.Items[i]).Visibility = Visibility.Visible;

                    foreach (string s in split)
                    {
                        if (s != "")
                            if (!((MusikZeile)lbPListGeräusche.Items[i]).tblkTitel.Text.ToLower().Contains(s) ||
                                ((MusikZeile)lbPListMusik.Items[i]).tboxKategorie.Text == "")
                                ((MusikZeile)lbPListGeräusche.Items[i]).Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception) { }
        }


        private void tbPListKategorie_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                btnPListAktFilter.Visibility = Visibility.Hidden;
                for (int i = 0; i < lbPListMusik.Items.Count; i++)
                {
                    ((MusikZeile)(lbPListMusik.Items[i])).Visibility = Visibility.Visible;

                    if (!chkPListFilter(tbPListKategorie.Text, ((MusikZeile)(lbPListMusik.Items[i])).tboxKategorie.Text) ||
                        !chkPListFilter(tbPListMusik.Text, ((MusikZeile)(lbPListMusik.Items[i])).tblkTitel.Text))
                        ((MusikZeile)(lbPListMusik.Items[i])).Visibility = Visibility.Collapsed;
                }

                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                {
                    ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Visible;

                    if (!chkPListFilter(tbPListKategorie.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tboxKategorie.Text) ||
                        !chkPListFilter(tbPListGeräusche.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tblkTitel.Text))
                        ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception) { }
        }

        private void btnFilterAktiv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbPListGeräusche.Text = "";
                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                {
                    //((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Visible;

                    if (!((MusikZeile)lbPListGeräusche.Items[i]).tbtnCheck.IsChecked.Value)
                        ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Collapsed;
                }
                lbPListGeräusche.ScrollIntoView(lbPListGeräusche.Items[0]);
                btnPListAktFilter.Visibility = Visibility.Visible;
            }
            catch (Exception) { }
        }

        private void btnPListAktFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                    ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Visible;

                tbPListKategorie_TextChanged(tbPListGeräusche, null);
                lbPListGeräusche.ScrollIntoView(lbPListGeräusche.Items[0]);
                btnPListAktFilter.Visibility = Visibility.Hidden;
            }
            catch (Exception) { }
        }

        private void lbPListMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.RemovedItems.Count == 1)
                    (e.RemovedItems[0] as MusikZeile).pbarSong.Visibility = Visibility.Collapsed;
                lbBackground.SelectedIndex = lbPListMusik.SelectedIndex;
                btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
                btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;
                btnImgPListMusikAbspielen.IsEnabled = btnImgBGAbspielen.IsEnabled;
                btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
            }
            catch (Exception) { }
        }

        private bool chkPListFilter(string filter, string text)
        {
            bool result = true;

            foreach (string s in filter.ToLower().Split(new Char[] { ' ', ',' }))
                if (s != "")
                    if (!text.ToLower().Contains(s) || text == "")
                        result = false;
            return result;
        }

        private void slPlaylistVolume_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IsInitialized)
                {
                    ((Slider)sender).ToolTip = Math.Round(((Slider)sender).Value) + " %";
                    if (Convert.ToDouble(btnPListPListSpeaker.Tag) != -1)
                        btnPListPListSpeaker.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                    {
                        GruppenObjekt grpObj = (mZeile.tbtnCheck.Tag == null) ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
                        if (grpObj != null)
                        {
                            grpObj.Vol_PlaylistMod = Convert.ToInt32(((Slider)sender).Value);
                            grpObj.Vol_ThemeMod = 100;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void slPlaylistMusikVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (IsInitialized)
                {
                    ((Slider)sender).ToolTip = Math.Round(((Slider)sender).Value) + " %";
                    slBGVolume.Value = ((Slider)sender).Value;
                }
            }
            catch (Exception) { }
        }

        private void btnKlangUpdateFiles_Click(object sender, RoutedEventArgs e)
        {
            string titelRef = "";
            try
            {
                Global.SetIsBusy(true, string.Format("Neue Dateien werden integriert..."));
                List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);

                titelRef = ((Button)sender).Tag.ToString();
                if (CheckAlleTitelGleicherPfad(titelRef, titelliste))
                {
                    ((Button)sender).Visibility = Visibility.Visible;

                    string[] allFilesMP3 = Directory.GetFiles(titelRef, "*.mp3", SearchOption.AllDirectories);
                    string[] allFilesWAV = Directory.GetFiles(titelRef, "*.wav", SearchOption.AllDirectories);
                    string[] allFilesOGG = Directory.GetFiles(titelRef, "*.ogg", SearchOption.AllDirectories);
                    string[] allFilesWMA = Directory.GetFiles(titelRef, "*.wma", SearchOption.AllDirectories);

                    string[] allFiles = new string[allFilesMP3.Length + allFilesOGG.Length + allFilesWAV.Length + allFilesWMA.Length];

                    MessageBoxResult msgRes = new MessageBoxResult();
                    if (allFilesMP3.Length > 0)
                    {
                        msgRes = MessageBox.Show("Sollen " + allFilesMP3.Length + " MP3-Dateien integriert werden?", "Hinzufügen von Musiktitel aus dem Verzecihnis", MessageBoxButton.YesNoCancel,MessageBoxImage.Question, MessageBoxResult.Yes);
                        if (msgRes == MessageBoxResult.Yes)                    
                            allFilesMP3.CopyTo(allFiles, 0);
                    }
                    if (msgRes != MessageBoxResult.Cancel)
                    {
                        if (allFilesWAV.Length > 0)
                        {
                            msgRes = MessageBox.Show("Sollen " + allFilesWAV.Length + " WAV-Dateien integriert werden?", "Hinzufügen von Musiktitel aus dem Verzecihnis", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                            if (msgRes == MessageBoxResult.Yes)                    
                                allFilesWAV.CopyTo(allFiles, allFiles.Length);// allFilesMP3.Length);
                        }
                    }
                    if (msgRes != MessageBoxResult.Cancel)
                    {
                        if (allFilesOGG.Length > 0)
                        {
                            msgRes = MessageBox.Show("Sollen " + allFilesOGG.Length + " OGG-Dateien integriert werden?", "Hinzufügen von Musiktitel aus dem Verzecihnis", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                            if (msgRes == MessageBoxResult.Yes)
                                allFilesOGG.CopyTo(allFiles, allFiles.Length);// allFilesMP3.Length + allFilesWAV.Length);
                        }
                    }
                    if (msgRes != MessageBoxResult.Cancel)
                    {                           
                        if (allFilesWMA.Length > 0)
                        {
                            msgRes = MessageBox.Show("Sollen " + allFilesWMA.Length + " WMA-Dateien integriert werden?", "Hinzufügen von Musiktitel aus dem Verzecihnis", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                            if (msgRes == MessageBoxResult.Yes)                    
                                allFilesWMA.CopyTo(allFiles, allFiles.Length);// allFilesMP3.Length + allFilesWAV.Length + allFilesOGG.Length);
                        }
                    }

                    List<string> newfiles = new List<string>();
                    int durchlauf = 0;
                    string prefix = (titelliste[0].Pfad.Substring(1, 1) != ":") ?
                        stdPfad + @"\" : "";

                    while (durchlauf < allFiles.Length)
                    {
                        if (allFiles[durchlauf] != null &&
                            titelliste.FirstOrDefault(t => prefix + t.Pfad == allFiles[durchlauf]) == null) newfiles.Add(allFiles[durchlauf]);
                        durchlauf++;
                    }

                    Global.SetIsBusy(true, string.Format(newfiles.Count + " Titel im Verzeichnis: " + Environment.NewLine + titelRef + "werden eingebunden"));

                    foreach (string newFile in newfiles)
                    {
                        Global.SetIsBusy(true, string.Format(newfiles.Count + " Titel im Verzeichnis: " + Environment.NewLine +
                            titelRef + "werden eingebunden" +
                            Environment.NewLine + System.IO.Path.GetFileName(newFile)));
                        Guid g = KlangDateiHinzu(newFile);
                        if (AktKlangPlaylist == _BGPlayer.AktPlaylist)
                        {

                        }
                    }
                    ((Button)sender).Visibility = Visibility.Hidden;
                    _chkAnzDateienInDir(_GrpObjecte.FirstOrDefault(t => t.aPlaylist == AktKlangPlaylist));
                    Global.SetIsBusy(false);
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                var errWin = new MsgWindow("Ungültiger Pfad", "Bitte überprüfen Sie das Verzeichnis:" + Environment.NewLine + titelRef, ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnPListPListAbspielen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int warTag = Convert.ToInt32(tcEditor.Tag);
                GruppenObjekt grpobj = null;
                foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                {
                    grpobj = (mZeile.tbtnCheck.Tag == null)? null: _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
                    if (grpobj != null && mZeile.tbtnCheck.IsChecked.Value)
                    {
                        tcEditor.Tag = tcEditor.Items.IndexOf(grpobj.ticKlang); //.seite;
                        if (Convert.ToBoolean(btnPListPListAbspielen.Tag))//grpobj.wirdAbgespielt)
                            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
                        else
                            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                    }
                    else
                    {
                        int i = 1;
                        i++;
                        tcEditor.Tag = Convert.ToInt16(tcEditor.Tag) + 1 - i;
                    }
                }
                btnPListPListAbspielen.Tag = !Convert.ToBoolean(btnPListPListAbspielen.Tag);
                btnimgPListPListAbspielen.Source = !Convert.ToBoolean(btnPListPListAbspielen.Tag) ?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")) :
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));

                if (grpobj != null)
                {
                    grpobj.wirdAbgespielt = Convert.ToBoolean(btnPListPListAbspielen.Tag);

                    if (grpobj.totalTimePlylist != -1)
                    {
                        aPlaylistLengthCheck = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(grpobj.aPlaylist.Audio_PlaylistGUID)).FirstOrDefault();
                        grpobj.totalTimePlylist = -1;
                        if (aPlaylistLengthCheck != null)
                            GetTotalLength();
                    }
                }
                tcEditor.Tag = warTag;
            }
            catch (Exception) { }
        }

        private void btnPListPListSpeaker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32((sender as Button).Tag) != -1)
                {
                    (sender as Button).Tag = -1;
                    btnimgPListPListSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
                }
                else
                {
                    (sender as Button).Tag = slPlaylistVolume.Value;
                    btnimgPListPListSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker-mute.png"));
                }

                foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                {
                    GruppenObjekt grpObj = (mZeile.tbtnCheck.Tag == null) ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
                    if (grpObj != null && mZeile.tbtnCheck.IsChecked.Value)
                        grpObj._listZeile.ForEach(delegate(KlangZeile kZeile)
                        {
                            if (kZeile._mplayer != null)
                                kZeile._mplayer.IsMuted = Convert.ToInt32(btnPListPListSpeaker.Tag) != -1 ? true : false;
                        });
                }
            }
            catch (Exception) { }
        }

        private void tbGotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                gbxHotkeys.Visibility = Visibility.Collapsed;
            }
            catch (Exception) { }
        }

        private void tbLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (spnlHotkeys.Children.Count > 0)
                {
                    gbxHotkeys.Visibility = Visibility.Visible;
                    btnHotkeyStop.IsEnabled = true;
                }
            }
            catch (Exception) { }
        }

        private void _datenloeschen(MessageBoxResult mrRes, bool allesloeschen, string saveTMPdatei)
        {
            hotkeys.Clear();
            spnlHotkeys.Children.Clear();

            if (mrRes == MessageBoxResult.No)
            {
                Global.SetIsBusy(true, string.Format("Bestehende Daten werden gesichert..." + Environment.NewLine + saveTMPdatei));
                this.UpdateLayout();
                if (Global.ContextAudio.PlaylistListe.Count > 0)
                    Global.ContextAudio.PlaylistListe[0].Export(saveTMPdatei, Global.ContextAudio.PlaylistListe[0].Audio_PlaylistGUID);
            }
            if (mrRes == MessageBoxResult.No || allesloeschen)
            {
                Global.SetIsBusy(true, string.Format("Laufende Songs werden beendet..."));
                foreach (GruppenObjekt grpobj in _GrpObjecte.FindAll(t => t.visuell))
                    AlleKlangSongsAus(grpobj, false, false);

                foreach (MusikZeile aZeile in lbPListGeräusche.Items) aZeile.tbtnCheck.IsChecked = false;

                if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                {
                    if (btnImgBGStoppen.IsEnabled)
                        btnImgBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                    _BGPlayer.NochZuSpielen.Clear();
                    _BGPlayer.Gespielt.Clear();
                    lbBackground.Tag = -1;
                    _BGPlayer.AktPlaylist = null;
                    lbBackground.Items.Clear();
                    lbMusiktitellist.Items.Clear();
                    _BGPlayer.AktTitel.Clear();
                }
                
                //1.
                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.PlaylistTitelListe.Count + 
                    " Titel in " + Global.ContextAudio.PlaylistListe.Count + " Playlisten)..."));
                int i = 0;
                foreach (Audio_Playlist_Titel aPlyTitel in Global.ContextAudio.PlaylistTitelListe)
                {
                    if (i == 10)
                    {
                        Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.PlaylistTitelListe.Count +
                            " Titel in " + Global.ContextAudio.PlaylistListe.Count + " Playlisten)..."));                    
                        i = 0;
                    }
                    Global.ContextAudio.RemoveTitelFromPlaylist(aPlyTitel);
                    i++;
                }

                //2.
                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.PlaylistListe.Count + 
                    " Playlisten )..."));
                foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe)
                    Global.ContextAudio.Delete<Audio_Playlist>(aPlaylist);
                
                //3. ??
                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.ThemeListe.Count + " Themes)..."));
                foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe)
                {
                    foreach (Audio_Theme aTheme1 in aTheme.Audio_Theme1)
                        aTheme.Audio_Theme2.Remove(aTheme1);

                    Global.ContextAudio.Delete<Audio_Theme>(aTheme);
                }

                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.TitelListe.Count + " Titel)..."));
                foreach (Audio_Titel aTitel in Global.ContextAudio.TitelListe)
                    Global.ContextAudio.RemoveTitel(aTitel);
                
                Global.SetIsBusy(true, string.Format("Grundzustand wird hergestellt..."));
            }
            FadingIn_Started = false;
            stopFadingIn = false;

            hotkeys = new List<hotkey>();
            plyTitelToSave = new List<Audio_Playlist_Titel>();
            _BGPlayer = new MusikView();
            _GrpObjecte = new List<GruppenObjekt>();
            tcEditor_vorher = 0;
            tcEditor_vorherTag = 0;
            AktKlangPlaylist = null;
            aPlaylistLengthCheck = null;
            AktKlangTheme = null;
            BGSongTimer.Close();
            if (KlangPlayEndetimer != null)
                KlangPlayEndetimer.Stop();
            plyTitelToSaveTimer.Stop();
            KlangProgBarTimer.Stop();
            MusikProgBarTimer.Stop();
            lbBackground.Items.Clear();

            _BGPlayer.BG.Clear();
            _BGPlayer.BG.Add(new Musik());
            _BGPlayer.BG.Add(new Musik());

            _rbtnGleichSpielen.IsChecked = Logic.Settings.Einstellungen.AudioDirektAbspielen;
            stdPfad = MeisterGeister.Logic.Settings.Einstellungen.AudioVerzeichnis;
            _tbStdPfad.Text = stdPfad;
            fadingTime = MeisterGeister.Logic.Settings.Einstellungen.Fading;

            DataContext = _zeile;
        }

        private void btnAudioDatenImport_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                MessageBoxResult mrRes = MessageBox.Show("Soll die aktuelle Datenbank erweitert werden?" + Environment.NewLine + Environment.NewLine + "Wählen sie 'Ja' damit die Datenbank erweitert wird." +
                    Environment.NewLine + "Wählen Sie 'Nein' um die bestehende Datenbank zu ersetzten. Achtung! Alle Daten gehen verloren.",
                    "Löschen bestehender Daten",MessageBoxButton.YesNoCancel,MessageBoxImage.Warning,MessageBoxResult.Yes);
                if (mrRes == MessageBoxResult.Yes || mrRes == MessageBoxResult.No)
                { 
                    Global.SetIsBusy(true);
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.CheckFileExists = true;
                    dlg.DefaultExt = ".xml";
                    dlg.Filter = "XML-Datei | *.xml";
                    dlg.InitialDirectory = Directory.Exists(stdPfad) ? stdPfad : "C:\\";
                    
                    Nullable<bool> result = dlg.ShowDialog();
                    if (result == true)
                    {
                        string pfad = dlg.FileName;
                        try
                        {
                            string tmpFile = Directory.GetCurrentDirectory() + @"\AudioDB_temp.xml";
                            _datenloeschen(mrRes, false, tmpFile);

                            Global.SetIsBusy(true, string.Format("Neue Daten werden importiert ..."));

                            tcEditor.SelectionChanged -= tcEditor_SelectionChanged;                            
                            tcEditor.SelectedIndex = 0;
                            
                            for (int i = tcEditor.Items.Count-1; i >= 0 ;i--)
                                if (tcEditor.Items[i] is TCButtons)
                                    tcEditor.Items.RemoveAt(i);

                            if (Audio_Playlist.Import(pfad, "") != null)
                            {
                                Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                                Global.ContextAudio.Save();
                            }

                            Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                            AktualisiereMusikPlaylist();
                            AktualisiereKlangPlaylist();
                            AktualisiereHotKeys();    
                            Global.SetIsBusy(true, string.Format("Temporäre Daten werden gelöscht ..."));
                            if (mrRes == MessageBoxResult.No)
                                File.Delete(tmpFile);
                            Global.SetIsBusy(false);
                            rbEditorEditPList.IsChecked = false;
                            tcEditor.SelectedItem = tiPlus;
                            tcEditor.SelectionChanged += tcEditor_SelectionChanged;
                            tcAudioPlayer.Tag = -1;
                            
                            tiEditor_GotFocus(sender, null);
                            rbEditorKlang_Click(rbEditorKlang, null);
                            rbEditorEditPList.IsChecked = true;
                            wpnlPListThemes.Tag = Guid.Empty;       
                     

                            //MessageBox.Show("Die Audio-Daten wurden importiert.");                            
                        }
                        catch (Exception ex)
                        {
                            Global.SetIsBusy(false);
                            MessageBox.Show("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." + 
                                Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    Global.SetIsBusy(false);
                }
            }
            catch (Exception) { }        
        }

        private void btnAudioDatenDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult mrRes = MessageBox.Show("Soll die komplette Audio-Datenbank gelöscht werden?" + Environment.NewLine + Environment.NewLine + "Achtung! Alle Daten gehen " +
                    "unwiderruflich verloren.", "Löschen ALLER bestehender Audio-Daten", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Yes);
                if (mrRes == MessageBoxResult.Yes)
                {
                    tcEditor.SelectionChanged -= tcEditor_SelectionChanged;
                    for (int i = tcEditor.Items.Count - 1; i >= 0; i--)
                        if (tcEditor.Items[i] is TCButtons)
                            tcEditor.Items.RemoveAt(i);

                    _datenloeschen(mrRes, true, "");

                    Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                    Global.ContextAudio.UpdateList<Audio_Titel>();
                    Global.ContextAudio.UpdateList<Audio_Playlist>();
                    Global.ContextAudio.UpdateList<Audio_Playlist_Titel>();
                    Global.ContextAudio.UpdateList<Audio_Theme>();
                    Global.ContextAudio.UpdateList<Audio_Theme_Playlist>();
                    //Global.ContextAudio.Save();

                    Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                    AktualisiereMusikPlaylist();
                    AktualisiereKlangPlaylist();
                    AktualisiereHotKeys();   
                    Global.SetIsBusy(false);

                    rbEditorEditPList.IsChecked = false;
                    tcEditor.SelectedItem = tiPlus;
                    tcEditor.SelectionChanged += tcEditor_SelectionChanged;
                    tcAudioPlayer.Tag = -1;

                    tiEditor_GotFocus(sender, null);
                    rbEditorKlang_Click(rbEditorKlang, null);
                    rbEditorEditPList.IsChecked = true;
                    wpnlPListThemes.Tag = Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                MessageBox.Show("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                    Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnKlangThemeImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.CheckFileExists = true;
                dlg.Multiselect = true;
                dlg.DefaultExt = ".xml";
                dlg.Filter = "XML-Datei | *.xml";
                dlg.InitialDirectory = Directory.Exists(stdPfad) ? stdPfad : "C:\\";

                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        bool _nicht_first = false;
                        foreach (string pfad in dlg.FileNames)
                        {
                            Global.SetIsBusy(true, string.Format("Neues Theme  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + "'  wird importiert ..."));

                            Audio_Playlist.Import(pfad, "Audio_Theme ", _nicht_first);
                            Global.ContextAudio.Save();
                            _nicht_first = true;
                        }                
                        Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                        Global.ContextAudio.Save();

                        Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                        AktualisiereMusikPlaylist();
                        AktualisiereKlangPlaylist();
                        AktualisiereHotKeys();    

                        Global.SetIsBusy(false);
                           lbEditor.SelectedIndex = -1;

                           rbEditorEditPList.IsChecked = false;
                           tcEditor.SelectedItem = tiPlus;
                           tcEditor.SelectionChanged += tcEditor_SelectionChanged;
                           tcAudioPlayer.Tag = -1;
                            
                           tiEditor_GotFocus(sender, null);
                           rbEditorKlang_Click(rbEditorKlang, null);
                           rbEditorEditPList.IsChecked = true;
                           wpnlPListThemes.Tag = Guid.Empty;    

                           // MessageBox.Show("Die Theme-Daten wurden importiert.");
                    }
                    catch (Exception ex)
                    {
                        Global.SetIsBusy(false);
                        MessageBox.Show("Beim Import des Themes ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                            Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } 
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                MessageBox.Show("Beim Importieren der Theme-Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                    Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void btnPlaylistImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.CheckFileExists = true; 
                dlg.Multiselect = true;
                dlg.DefaultExt = ".xml";
                dlg.Filter = "XML-Datei | *.xml";
                dlg.InitialDirectory = Directory.Exists(stdPfad) ? stdPfad : "C:\\";

                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        bool _nicht_first = false;
                        foreach (string pfad in dlg.FileNames)
                        {
                            Global.SetIsBusy(true, string.Format("Neue Playlist  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + "'  wird importiert ..."));

                            Audio_Playlist.Import(pfad, "Audio_Playlist ", _nicht_first);
                            Global.ContextAudio.Save();
                            _nicht_first = true;
                        }
                        Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                        AktualisiereMusikPlaylist();
                        AktualisiereKlangPlaylist();
                        AktualisiereHotKeys();    

                        Global.SetIsBusy(false);

                        //MessageBox.Show("Der Vorgang wurde erfolgreich abgeschlossen");
                    }
                    catch (Exception ex)
                    {
                        Global.SetIsBusy(false);
                        MessageBox.Show("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                            Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        AktualisiereKlangPlaylist();
                        AktualisiereHotKeys();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                MessageBox.Show("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                    Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lbItembtnLöschenTheme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
                Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == g);
                if (aTheme != null)
                {
                    string messageBoxText = "Wollen Sie wirklich das ausgewählte Theme  '" + aTheme.Name + "'  löschen.";
                    string caption = "Löschen des Themes";
                    MessageBoxButton button = MessageBoxButton.YesNoCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;

                    if (MessageBox.Show(messageBoxText, caption, button, icon) == MessageBoxResult.Yes)
                    {
                        Global.SetIsBusy(true, string.Format("Theme '" + aTheme.Name + "' wird gelöscht..."));

                        if (AktKlangTheme != null && (Guid)wpnlPListThemes.Tag == AktKlangTheme.Audio_ThemeGUID)
                        {
                            for (int i = 0; i < wpnlPListThemes.Children.Count; i++)
                                if ((Guid)((ToggleButton)wpnlPListThemes.Children[i]).Tag == AktKlangTheme.Audio_ThemeGUID)
                                    ((ToggleButton)wpnlPListThemes.Children[i]).IsChecked = false;
                        }

                        if (!Global.ContextAudio.Delete<Audio_Theme>(aTheme))
                            Global.ContextAudio.ThemeListe.Remove(aTheme);
                        lbEditorTheme.SelectedIndex = -1;
                        expEditorTopThemeTheme.Visibility = Visibility.Collapsed;
                        AktualisiereKlangThemes();


                        rbEditorEditTheme.Tag = true;
                        rbEditorEditTheme_Checked(null, null);

                        AktKlangTheme = null;
                        rbEditorEditTheme_Checked(null, null);

                        Global.SetIsBusy(false);
                        MessageBox.Show("Das Theme wurde erfolgreich gelöscht.");
                    }
                }
                else
                    MessageBox.Show("Das ausgewählte Theme konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                            Environment.NewLine, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Löschen des Themes ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void lbItembtnLöschenPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
                Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == g);
                if (aPlaylist != null)
                {
                    string messageBoxText = "Wollen Sie wirklich die ausgewählte Playlist  '" + aPlaylist.Name + "'  löschen.";
                    string caption = "Löschen der Playlist";
                    MessageBoxButton button = MessageBoxButton.YesNoCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;

                    if (MessageBox.Show(messageBoxText, caption, button, icon) == MessageBoxResult.Yes)
                    {
                        Global.SetIsBusy(true, string.Format("Playlist '" + aPlaylist.Name + "' wird gelöscht..."));
                        if (AktKlangPlaylist != null && AktKlangPlaylist.Name == aPlaylist.Name)
                        {
                            for (UInt16 i = 0; i <= lbEditor.Items.Count - 1; i++)
                            {
                                if (((ListboxItemIcon)lbEditor.Items[i]).lbText.Content.ToString() == aPlaylist.Name)
                                {
                                    GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.ticKlang == tcEditor.SelectedItem);
                                    if (grpobj == null || grpobj.objGruppe == -1)
                                        return;

                                    foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                                    {
                                        grpobj = (mZeile.tbtnCheck.Tag == null) ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
                                        if (grpobj != null && mZeile.tbtnCheck.IsChecked.Value)
                                        {
                                            tcEditor.Tag = tcEditor.Items.IndexOf(grpobj.ticKlang); //.seite;
                                            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
                                        }
                                    }
                                    if (grpobj != null)
                                    {
                                        PlaylisteLeeren(grpobj);

                                        grpobj.tbTopKlangKategorie.Text = "";
                                        grpobj.tbTopKlangKategorie.Tag = null;
                                        ((TCButtons)tcEditor.SelectedItem)._tbText.Text = "NeuePlayliste";

                                        grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallel_TextChanged;
                                        grpobj.tbTopKlangSongsParallel.Tag = null;
                                        grpobj.tbTopKlangSongsParallel.Text = "1";
                                        grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallel_TextChanged;
                                    }
                                    ZeigeKlangSongsParallel(grpobj, false);
                                }
                            }
                            AktKlangPlaylist = null;
                        }

                        if (_BGPlayer.AktPlaylist != null && _BGPlayer.AktPlaylist.Name == aPlaylist.Name)
                        {
                            MusikProgBarTimer.Stop();
                            if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                            {
                                _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Stop();
                                _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Close();
                                btnBGAbspielen.IsEnabled = false;
                                btnPListMusikAbspielen.IsEnabled = false;
                                _BGPlayer.AktPlaylist = null;
                                lbMusiktitellist.Items.Clear();
                                _BGPlayer.AktTitel.Clear();
                            }
                            grdSongInfo.Visibility = Visibility.Hidden;
                        }
                        List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(aPlaylist);
                        titel.ForEach(delegate(Audio_Titel aTitel)
                        {
                            Global.ContextAudio.RemoveTitelFromPlaylist(aPlaylist, aTitel);
                        });

                        if (aPlaylist.Key != null)
                        {
                            foreach (Button btnHotkey in spnlHotkeys.Children)
                                if (((hotkey)btnHotkey.Tag).taste == Convert.ToInt32(Convert.ToChar(aPlaylist.Key)))
                                {
                                    spnlHotkeys.Children.Remove(btnHotkey);
                                    break;
                                }
                        }
                        Global.ContextAudio.Delete<Audio_Playlist>(aPlaylist);

                        AktualisiereKlangPlaylist();
                        tbEditorPlaylistFilter_TextChanged(null, null);
                        Global.SetIsBusy(false);
                        MessageBox.Show("Die Playlist wurde erfolgreich gelöscht.");
                    }
                }
                else
                    MessageBox.Show("Die ausgewählte Playlist konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                            Environment.NewLine, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Löschen der Playlist ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void lbItembtnExportTheme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
                Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == g);
                if (aTheme != null)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.CheckFileExists = false;
                    dlg.DefaultExt = ".xml";
                    dlg.Filter = "XML-Datei | *.xml";
                    dlg.InitialDirectory = Directory.Exists(stdPfad) ? stdPfad : "C:\\";
                    dlg.FileName = "Theme_" + aTheme.Name + ".xml";

                    Nullable<bool> result = dlg.ShowDialog();
                    if (result == true)
                    {
                        Global.SetIsBusy(true, string.Format("Das Theme wird exportiert ..."));

                        File.Delete(dlg.FileName);
                        aTheme.Export(dlg.FileName, Guid.Empty);    
 
                        MessageBox.Show("Die Theme-Daten wurden erfolgreich gesichert.");
                    }                    
                    Global.SetIsBusy(false);
                }
                else
                    MessageBox.Show("Das ausgewählte Theme konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                        Environment.NewLine, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Exportieren des Themes ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void lbItembtnExportPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
                Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == g);
                if (aPlaylist != null)
                {

                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.CheckFileExists = false;
                    dlg.DefaultExt = ".xml";
                    dlg.Filter = "XML-Datei | *.xml";
                    dlg.InitialDirectory = Directory.Exists(stdPfad) ? stdPfad : "C:\\";
                    dlg.FileName = "Playlist_" + aPlaylist.Name + ".xml";

                    Nullable<bool> result = dlg.ShowDialog();
                    if (result == true)
                    {
                        Global.SetIsBusy(true, string.Format("Die Playlist wird exportiert ..."));
                        File.Delete(dlg.FileName);
                        aPlaylist.Export(dlg.FileName, g);

                        Global.SetIsBusy(false);
                        MessageBox.Show("Die Playlist-Daten wurden erfolgreich gesichert.");
                    }
                }
                else
                    MessageBox.Show("Die ausgewählte Playlist konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang." +
                        Environment.NewLine, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                var errWin = new MsgWindow("Allgemeiner Fehler", "Beim Exportieren der Playlist ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        private void btnAudioDatenExport_Click(object sender, RoutedEventArgs e)
        {
            Global.ContextAudio.Save();
            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.CheckFileExists = false;
                dlg.DefaultExt = ".xml";
                dlg.Filter = "XML-Datei | *.xml";
                dlg.InitialDirectory = Directory.Exists(stdPfad) ? stdPfad : "C:\\";

                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    Global.SetIsBusy(true, string.Format("Bestehende Daten werden exportiert ..."));
                    string pfad = dlg.FileName;
                    
                    
                    try
                    {
                        File.Delete(pfad);
                        Audio_Theme.ExportAll(pfad);

                        Global.SetIsBusy(false);
                    }
                    catch (Exception ex)
                    {
                        Global.SetIsBusy(false);
                        MessageBox.Show("Beim Export ist ein Fehler aufgetreten." + Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    MessageBox.Show("Die Audio-Daten wurden in \'" + pfad + "\' gespeichert.");         
                }
            }
            catch (Exception) { }
        }

        private List<Guid> CheckUnterTheme(Guid wpnlGuid, List<Guid> schonAktiveThemes)
        {
            foreach (Audio_Theme aUnterTheme in Global.ContextAudio.LoadThemesByGUID(wpnlGuid).Audio_Theme1)
            {
                if (!schonAktiveThemes.Contains(aUnterTheme.Audio_ThemeGUID))
                {
                    schonAktiveThemes.Add(aUnterTheme.Audio_ThemeGUID);
                    CheckUnterTheme(aUnterTheme.Audio_ThemeGUID, schonAktiveThemes);
                }
            }
            return schonAktiveThemes;
        }
        private List<Guid> CheckUnterThemeInLbi(Guid lbiGuid, List<Guid> schonAktiveThemes)
        {
            foreach (Audio_Theme aUnterTheme in Global.ContextAudio.LoadThemesByGUID(lbiGuid).Audio_Theme1)
            {
                if (aUnterTheme.Audio_ThemeGUID == AktKlangTheme.Audio_ThemeGUID &&
                    !schonAktiveThemes.Contains(lbiGuid))
                {
                    schonAktiveThemes.Add(lbiGuid);
                    CheckUnterTheme(lbiGuid, schonAktiveThemes);
                }
            }
            return schonAktiveThemes;
        }
        
        private void btnTopThemeAddTheme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox cmbx = new ComboBox();
                List<Guid> schonAktiveThemes = new List<Guid>();

                //Guid-Liste der schon verwendeten Themes erstellen
                schonAktiveThemes.Add(AktKlangTheme.Audio_ThemeGUID);
                foreach (boxThemeTheme boxItemTheme in wpnlEditorTopThemesThemes.Children)
                {
                    schonAktiveThemes.Add((Guid)boxItemTheme.Tag);
                    schonAktiveThemes = CheckUnterTheme((Guid)boxItemTheme.Tag, schonAktiveThemes);
                }
                //Themes, die das Aktuelle Theme enthalten auch auf die Guid-Liste
                foreach (ListboxItemIcon lbi in lbEditorTheme.Items)
                    schonAktiveThemes = CheckUnterThemeInLbi((Guid)lbi.Tag, schonAktiveThemes);
                
                //Alle nicht vorhandenen Guids anzeigen
                for (int i = 0; i < lbEditorTheme.Items.Count; i++)
                {
                    if (!schonAktiveThemes.Contains((Guid)((ListboxItemIcon)lbEditorTheme.Items[i]).Tag))
                    {
                        ListboxItemIcon lbi = new ListboxItemIcon();
                        lbi.imgIcon = ((ListboxItemIcon)lbEditorTheme.Items[i]).imgIcon;
                        lbi.lbText.Content = ((ListboxItemIcon)lbEditorTheme.Items[i]).lbText.Content;
                        lbi.Tag = ((ListboxItemIcon)lbEditorTheme.Items[i]);
                        cmbx.Items.Add(lbi);
                    }
                }

                cmbx.SelectionChanged += cmbxThemeTheme_SelectionChanged;
                cmbx.DropDownClosed += cmbxThemeTheme_DropDownClosed;
                grdEditorPlaylistInfo.Children.Add(cmbx);
                Grid.SetColumn(cmbx, 3);
                cmbx.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                MessageBox.Show("Beim Erstellen des DropDown-Feldes für die Themeliste ist ein Fehler aufgetreten." +
                    Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbxThemeTheme_DropDownClosed(object sender, EventArgs e)
        {
            grdEditorPlaylistInfo.Children.Remove((ComboBox)sender);
        }

        private void cmbxThemeTheme_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid ThemeGuid = (Guid)((ListboxItemIcon)((ListboxItemIcon)((ComboBox)sender).SelectedItem).Tag).Tag;

                AktKlangTheme.Audio_Theme1.Add(Global.ContextAudio.ThemeListe.First(t => t.Audio_ThemeGUID == ThemeGuid));
                Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);

                boxThemeTheme bxTheme = new boxThemeTheme();
                bxTheme.txblkName.Text = ((ListboxItemIcon)((ComboBox)sender).SelectedItem).lbText.Content.ToString();
                bxTheme.Tag = ThemeGuid;
                bxTheme.btnClose.Tag = ThemeGuid;
                bxTheme.btnClose.Click += bxThemeBtnClose_Click;

                wpnlEditorTopThemesThemes.Children.Add(bxTheme);
                expEditorTopThemeTheme.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                MessageBox.Show("Bei der Auswahl des untergeordneten Themes ist ein Fehler aufgetreten." +
                    Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bxThemeBtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid ThemeGuid = (Guid)((Button)sender).Tag;

                AktKlangTheme.Audio_Theme1.Remove(Global.ContextAudio.ThemeListe.First(t => t.Audio_ThemeGUID == ThemeGuid));
                Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);

                wpnlEditorTopThemesThemes.Children.Remove((boxThemeTheme)((GroupBox)((Grid)((Button)sender).Parent).Parent).Parent);
                expEditorTopThemeTheme.Visibility = wpnlEditorTopThemesThemes.Children.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                MessageBox.Show("Beim Lösen des untergeordenten Themes aus dem Theme ist ein Fehler aufgetreten." +
                    Environment.NewLine + ex, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetFullPath(string titel)
        {
            titel = (titel.Substring(1, 1) != ":")? stdPfad + @"\" + titel : titel;
            return titel;
        }

        private string GetShortPath(string titel)
        {
            titel = titel.StartsWith(stdPfad) ? titel.Substring(stdPfad.Length): titel;
            return titel;
        }
    }
}