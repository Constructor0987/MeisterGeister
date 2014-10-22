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
 

// Eigene Usings
using MeisterGeister.Logic.Einstellung;
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

#region Klassen-Definitionen

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
    public bool jumped_to_start = false;
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
	public double Vol_PlaylistMod = 0;
	public DateTime LastVolUpdate = DateTime.Now;
	public uint sollBtnGedrueckt = 0;
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
	public Grid grdEditor = null;
	public Grid grdEditorTop = null;
	public ToggleButton tbtnKlangPause = null;
    public ListBox lbEditorListe = null;

	public TextBox tbTopFilter = null;
	public Button btnTopFilter = null;
	public TextBox tbTopKlangKategorie = null;
	public Border brdTopKlangKategorie = null;

	public GroupBox gboxTopSongsParallel = null;
	public TextBox tbTopKlangSongsParallel = null;
	public Button btnTopSongParPlus = null;
	public Button btnTopSongParMinus = null;

    public GroupBox gboxTopMusikSort = null;
    public ToggleButton btnTopMusikAbisZ = null;
    public ToggleButton btnTopMusik1bis9 = null;

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
    public List<string> allFilesMP3 = new List<string>();
    public List<string> allFilesWAV = new List<string>();
    public List<string> allFilesOGG = new List<string>();
    public List<string> allFilesWMA = new List<string>();
}

#endregion

namespace MeisterGeister.View.AudioPlayer {
	/// <summary>
	/// Interaktionslogik für AudioPlayerView.xaml
	/// </summary>
	/// 

    public partial class AudioPlayerView : UserControl
    {
        TabItemControl AudioTIC = null;
        chkeckAnzDateien _chkAnzDateien = new chkeckAnzDateien();

        private string[] validExt = new String[6] { "mp3", "wav", "wma", "ogg", "m3u8", "wpl" };

        private double Zeitüberlauf = 1000;   // in ms
        public List<string> stdPfad = new List<string>();
        private UInt16 tiErstellt = 0;
        private UInt16 rowErstellt = 0;
        private double fadingIntervall = 10;
        public double fadingTime = 600;    // * fadingIntervall = Übergang in ms

        private MediaPlayer FadingIn_Started = new MediaPlayer();
        private MediaPlayer FadingOut_Started = new MediaPlayer();
        private bool stopFadingIn = false;
        private bool notPlayHotkey = false;
        private bool isDeleting = false;
        private bool AudioInAnderemPfadSuchen = Einstellungen.AudioInAnderemPfadSuchen;
        private bool AudioSpieldauerBerechnen = Einstellungen.AudioSpieldauerBerechnen;

        private int SliderTeile = 25;
        private Int16 PauseSprung = 200;
        private Int16 VolSprung = 5;

        private Nullable<Point> pointerZeileDragDrop = null;
        private int audioZeileMouseOverDropped = 0;

        private List<hotkey> hotkeys = new List<hotkey>();

        private MusikView _BGPlayer = new MusikView();
        private List<GruppenObjekt> _GrpObjecte = new List<GruppenObjekt>();

        public Audio_Playlist AktKlangPlaylist;
        private Audio_Theme AktKlangTheme = null;

        System.Timers.Timer BGSongTimer = new System.Timers.Timer();
        private List<DispatcherTimer> lstKlangPlayEndetimer = new List<DispatcherTimer>();
        DispatcherTimer KlangPlayEndetimer;

        DispatcherTimer _timerFadingIn = new DispatcherTimer();
        DispatcherTimer _timerFadingOut = new DispatcherTimer();
        DispatcherTimer _timerFadingOutGeräusche = new DispatcherTimer();
        DispatcherTimer _timerFadingInGeräusche = new DispatcherTimer();
        DispatcherTimer _timerBGFadingOut = new DispatcherTimer();
        DispatcherTimer KlangProgBarTimer = new DispatcherTimer();
        DispatcherTimer MusikProgBarTimer = new DispatcherTimer();

        DispatcherTimer _debugTreeview = new DispatcherTimer();

        ZeileVM _zeile = new ZeileVM();

        delegate void UpdateUI();

        
        public AudioPlayerView()
        {
            InitializeComponent();

#if !DEBUG
            tiDebug.Visibility = Visibility.Collapsed;
#endif

            //_zeile.XPos = 10;
            //XPos = 10;
            this.DataContext = this;
            
            _BGPlayer.BG.Add(new Musik());
            _BGPlayer.BG.Add(new Musik());

            setStdPfad();
            fadingTime = MeisterGeister.Logic.Einstellung.Einstellungen.Fading;
            slPlaylistVolume.Value = Einstellungen.GeneralGeräuscheVolume;
            slBGVolume.Value = Einstellungen.GeneralMusikVolume;

            DataContext = _zeile;
            AktualisiereHotKeys();
                        
            _timerFadingIn.Tick += new EventHandler(_timerFadingIn_Tick);
            _timerFadingOut.Tick += new EventHandler(_timerFadingOut_Tick);
            _timerFadingOutGeräusche.Tick += new EventHandler(_timerFadingOutGeräusche_Tick);
            _timerFadingInGeräusche.Tick += new EventHandler(_timerFadingInGeräusche_Tick);
            _timerBGFadingOut.Tick += new EventHandler(_timerBGFadingOut_Tick);


            _debugTreeview.Tick += new EventHandler(_debugTreeview_Tick);
            _debugTreeview.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _debugTreeview.Tag = 0;

            KlangProgBarTimer.Tick += new EventHandler(KlangProgBarTimer_Tick);
            KlangProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            KlangProgBarTimer.Tag = 0;

            MusikProgBarTimer.Tick += new EventHandler(MusikProgBarTimer_Tick);
            MusikProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            //myTextBlock.BeginAnimation();
            //_box.BeginAnimation(tbBGFilter,)
        }


        private void imgStdIconDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListboxItemBtn lbiBtn = (ListboxItemBtn)((StackPanel)((Grid)((Image)sender).Parent).Parent).Parent;

            string s = "";
            for (int i = 0; i < stdPfad.Count; i++)
                if (stdPfad[i] != lbiBtn.lblStdPfad.Content.ToString())
                    s += stdPfad[i] + "|";
            if (s.Length > 0)
                s = s.Substring(0, s.Length - 1);
            else
                s = "C:";

            Logic.Einstellung.Einstellungen.AudioVerzeichnis = s;
            Logic.Einstellung.Einstellungen.UpdateEinstellungen();

            ((ListBox)lbiBtn.Parent).Items.Remove(lbiBtn);
            setStdPfad();
        }


        public void Close_Click(object sender, RoutedEventArgs e)
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

            foreach (DispatcherTimer dispTmr in lstKlangPlayEndetimer)
                if (dispTmr != null) dispTmr.Stop();
            _GrpObjecte.Clear();
            lstKlangPlayEndetimer.Clear();
        }

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
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (grpobj != null && grpobj.cmboxTopHotkey.Visibility == Visibility.Collapsed)
                        notPlayHotkey = false;
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswerten des Tastenklicks ist ein Fehler aufgetreten.", ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AudioTIC == null)
                {
                    AudioTIC = ((TabItemControl)((AudioPlayerView)e.Source).Parent);
                    AudioTIC._buttonClose.Click += Close_Click;
                }
                tiEditor_GotFocus(sender, null);
                rbEditorEditPList.IsChecked = true;
                wpnlPListThemes.Tag = new List<Guid>();

                if (_GrpObjecte.FirstOrDefault(t => t.visuell) == null)
                    tiPlus_MouseUp(false, null);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Laden des Fensters ist ein Fehler aufgetreten.", ex);
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
                        AlleKlangSongsAus(_GrpObjecte[i], true, false, true);

                    KlangProgBarTimer.Stop();
                    MusikProgBarTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Schließen des Fensters ist ein Fehler aufgetreten.", ex);
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
                        _player.MediaFailed += Player_MusikMediaFailed;
                }

                try
                {
                    if (klZeile != null)
                        klZeile.jumped_to_start = false;

                    if (_player.Source == null || _player.Source.LocalPath.ToString() != url)
                    {
                        _player.IsMuted = (notMusikPlayer) ?
                            (!_GrpObjecte[posObjGruppe].visuell && Convert.ToInt32(btnPListPListSpeaker.Tag) != -1 ? true : false) :
                            (Convert.ToInt16(btnBGSpeaker.Tag) != -1) ? true : false;

                        if (posObjGruppe != -1)
                            _player.SpeedRatio = klZeile.playspeed;
                        _player.Open(new Uri(url));
                    };

                    if (fading)   // Musik-Playlist
                    {
                        _player.Volume = 0;
                        if (!notMusikPlayer)
                        {
                            _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = false;
                            FadingOut_Started = null;
                        }
                        else
                        {
                            klZeile.FadingOutStarted = false;
                            FadingOut_Started = null;
                        }
                        FadingIn(klZeile, _player, (!notMusikPlayer) ? vol / 100 : vol / 100);
                    }
                    else
                    {
                        if (_player.NaturalDuration.HasTimeSpan &&
                            (klZeile.audiotitel.TeilAbspielen ||
                            (_player.NaturalDuration.TimeSpan == _player.Position)))
                        {
                            _player.Position = TimeSpan.FromMilliseconds(Math.Round(klZeile.audiotitel.TeilStart.Value, 0, MidpointRounding.ToEven));
                            klZeile.jumped_to_start = true;
                        }
                        else
                            _player.Position = TimeSpan.FromMilliseconds(0);

                        if (_timerFadingOut.IsEnabled || !_GrpObjecte[posObjGruppe].aPlaylist.Fading)
                            _player.Volume =
                                ((!notMusikPlayer || _GrpObjecte[posObjGruppe].visuell) ?
                                    vol / 100 :
                                        (_GrpObjecte[posObjGruppe].force_Volume != null) ?
                                        _GrpObjecte[posObjGruppe].force_Volume.Value :                      //Playlist-ForceVolume
                                        (vol / 100) * (_GrpObjecte[posObjGruppe].Vol_PlaylistMod / 100));
                        else
                            _player.Volume = 0;                                                             //Fading In Geräusche Start

                        //MyTimer.stop_timer(Convert.ToString(_player.Volume)); //MUSS vorerst drinnnen bleiben, damit das Volume richtig wiedergegben wird (Zeit)?!? 
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
                ViewHelper.ShowError("Audio Fehler" + Environment.NewLine + "Der Audio Player hat einen Fehler verursacht.", ex);
                return null;
            }
        }

        private void KlangProgBarTimer_Tick(object sender, EventArgs e)
        {
            bool found = false;
            UInt16 loops = 0;
            KlangProgBarTimer.Tag = (KlangProgBarTimer.Tag.ToString() == "0") ? "1" : "0";
            try
            {
                for (int posObjGruppe = 0; posObjGruppe < _GrpObjecte.Count; posObjGruppe++)
                {
                    List<KlangZeile> KlangZeilenLaufend = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);

                    if (KlangZeilenLaufend != null && KlangZeilenLaufend.Count != 0)
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
                            loops++;
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
                                if ((!_timerFadingInGeräusche.IsEnabled && !_timerFadingOutGeräusche.IsEnabled) ||
                                    !_GrpObjecte[posObjGruppe].aPlaylist.Fading)
                                {
                                    // Volume anpassen
                                    if ((_GrpObjecte[posObjGruppe].force_Volume == null) &&
                                        KlangZeilenLaufend[durchlauf].audiotitel.VolumeChange && !KlangZeilenLaufend[durchlauf].FadingOutStarted)
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
                                            _GrpObjecte[posObjGruppe].visuell)// .ticKlang.Visibility == Visibility.Visible)
                                            KlangZeilenLaufend[durchlauf].audioZeile.sldKlangVol.Value = KlangZeilenLaufend[durchlauf].Aktuell_Volume;
                                    }
                                    double sollWert = (KlangZeilenLaufend[durchlauf].Aktuell_Volume / 100) *
                                        (!_GrpObjecte[posObjGruppe].aPlaylist.Fading? 1: (!_GrpObjecte[posObjGruppe].istMusik ? _GrpObjecte[posObjGruppe].Vol_PlaylistMod / 100 : 1));

                                    //Forcing des VOLUME
                                    if ((FadingIn_Started == null || FadingIn_Started.Source == null) &&
                                        !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                        _GrpObjecte[posObjGruppe].force_Volume != null)
                                        KlangZeilenLaufend[durchlauf]._mplayer.Volume = _GrpObjecte[posObjGruppe].force_Volume.Value;
                                    else
                                        if ((FadingIn_Started == null || FadingIn_Started.Source == null) &&
                                            !KlangZeilenLaufend[durchlauf].FadingOutStarted && sollWert != KlangZeilenLaufend[durchlauf]._mplayer.Volume)
                                            KlangZeilenLaufend[durchlauf]._mplayer.Volume = sollWert;
                                }

                                //einmaliges ermitteln des Endzeitpunkts
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null &&
                                    KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum == 100000 && KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan &&
                                    KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel != null)
                                {
                                    if (KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge != KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
                                    {
                                        KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge = KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                        Global.ContextAudio.Update<Audio_Titel>(KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel);
                                        UpdatePlaylistLänge(_GrpObjecte[posObjGruppe].aPlaylist);
                                    }
                                    KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum = (double)KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge;

                                    //aktualisiere Endzeitpunkt Minutenposition
                                    KlangZeilenLaufend[durchlauf].audioZeile.lblDauer.Content = KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.ToString().Substring(3, 5);
                                }

                                //aktualisiere ProgressBar - bei Wartezeit auf Maximum
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null &&
                                    _GrpObjecte[posObjGruppe].visuell && //.ticKlang.Visibility == Visibility.Visible &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)
                                    KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value =
                                        (!KlangZeilenLaufend[durchlauf].istWartezeit) ? KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds :
                                        KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum;

                                //Startposition überprüfen
                                // Ist notwendig wenn HasTimeSpan zu Beginn nicht erkant wurde
                                if (!KlangZeilenLaufend[durchlauf].jumped_to_start && !KlangZeilenLaufend[durchlauf].istWartezeit && !KlangZeilenLaufend[durchlauf].istStandby &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan && KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.Position < TimeSpan.FromMilliseconds(Math.Round(KlangZeilenLaufend[durchlauf].audiotitel.TeilStart.Value, 0, MidpointRounding.ToEven)))
                                {
                                    KlangZeilenLaufend[durchlauf].jumped_to_start = true;
                                    KlangZeilenLaufend[durchlauf]._mplayer.Position = TimeSpan.FromMilliseconds(Math.Round(KlangZeilenLaufend[durchlauf].audiotitel.TeilStart.Value, 0, MidpointRounding.ToEven));
                                }

                                // Endposition von Geräuschen überprüfen bei vorzeitigem Ende
                                if (!_GrpObjecte[posObjGruppe].istMusik && KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)
                                    if (KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
                                        KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds >= KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde)
                                    {
                                        _GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));
                                        if (_GrpObjecte[posObjGruppe].Gespielt.Count > 50)
                                            _GrpObjecte[posObjGruppe].Gespielt.RemoveAt(0);

                                        bool nurEinLiedAktiv = (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audiotitel.Aktiv).Count == 1) ? true : false;
                                        if (nurEinLiedAktiv)
                                        {
                                            KlangZeilenLaufend[durchlauf]._mplayer.Position =
                                                (KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen) ?
                                                    TimeSpan.FromMilliseconds(Math.Round(KlangZeilenLaufend[durchlauf].audiotitel.TeilStart.Value, 0, MidpointRounding.ToEven)) :
                                                    TimeSpan.FromMilliseconds(0);
                                        }
                                        else
                                        {
                                            KlangZeilenLaufend[durchlauf].jumped_to_start = false;
                                            KlangZeilenLaufend[durchlauf]._mplayer.Stop();
                                            KlangZeilenLaufend[durchlauf]._mplayer.Close();

                                            if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null)
                                                KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value = KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde.Value;

                                            KlangZeilenLaufend[durchlauf].istLaufend = false;
                                            KlangZeilenLaufend[durchlauf].istPause = false;
                                            KlangZeilenLaufend[durchlauf].istStandby = false;

                                            CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);

                                            KlangZeilenLaufend[durchlauf].istStandby = true;

                                            if (KlangZeilenLaufend[durchlauf].audioZeile != null)
                                                KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value = 0;
                                        }
                                    }
                                //Musik Endposition (incl vor Fading) überprüfen
                                if (_GrpObjecte[posObjGruppe].istMusik && !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)

                                    //Bei TeilAbspielen und Ende erreicht
                                    if ((KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
                                         KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde)
                                        ||
                                        (!KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
                                        KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
                                        )
                                    {
                                        _GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));
                                        if (_GrpObjecte[posObjGruppe].Gespielt.Count > 50)
                                            _GrpObjecte[posObjGruppe].Gespielt.RemoveAt(0);

                                        if (!KlangZeilenLaufend[durchlauf].FadingOutStarted)
                                        {
                                            KlangZeilenLaufend[durchlauf].FadingOutStarted = true;
                                            FadingOut(KlangZeilenLaufend[durchlauf], true, false);
                                        }

                                        if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeile != null)
                                            KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value = KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde.Value;

                                        KlangZeilenLaufend[durchlauf].istLaufend = false;
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
            catch (Exception)
            {
                // ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Durchlauf der Prozedur 'KlangProgBarTimer' ist ein Fehler aufgetreten", ex);
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
                titel = 0;
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
                                    if (klZeilenStandbyNichtPause[x].audioZeile == null ||
                                        (((klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background != null) &&
                                        klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 255, 0)).ToString() &&       // Yellow
                                        klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString()) ||         // Red))     
                                        (klZeilenStandbyNichtPause[x].audioZeile.lbiEditorRow.Background == null)) &&
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
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].audiotitel.Aktiv, grpobj.wirdAbgespielt, grpobj._listZeile[posZeile], null);
                                    else
                                    {
                                        if (grpobj._listZeile[posZeile].audioZeile.chkTitel != null)
                                        {
                                            chkTitel0_0_Click(grpobj._listZeile[posZeile].audioZeile.chkTitel, new RoutedEventArgs());
                                            grpobj.lbEditorListe.ScrollIntoView(grpobj._listZeile[posZeile].audioZeile);
                                            //grpobj.sviewer.ScrollToVerticalOffset(posZeile * grpobj._listZeile[posZeile].audioZeile.ActualHeight);
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

                                    if (grpobj.NochZuSpielen.Count > 1)
                                    {
                                        while (!grpobj._listZeile.First(t => t.audiotitel.Audio_TitelGUID == grpobj.NochZuSpielen[neuPos]).istStandby &&
                                               (grpobj._listZeile.FindAll(t => t.istStandby).Count != 0))
                                            neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                    }

                                    Guid zuspielendeGuid = grpobj.NochZuSpielen[neuPos];
                                    int posZeile = grpobj._listZeile.FindIndex(t => t.audiotitel.Audio_TitelGUID == zuspielendeGuid);

                                    grpobj._listZeile[posZeile].istStandby = false;
                                    if (!grpobj.visuell)
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].audiotitel.Aktiv, grpobj.wirdAbgespielt, grpobj._listZeile[posZeile], null);
                                    else
                                    {
                                        // Titel anstarten
                                        if (grpobj._listZeile[posZeile].audioZeile.chkTitel != null)
                                        {
                                            chkTitel0_0_Click(grpobj._listZeile[posZeile].audioZeile.chkTitel, new RoutedEventArgs());

                                            //if (grpobj.aPlaylist.Fading)
                                            //    FadingInGeräusch(grpobj);
                                        }
                                        else
                                        {
                                            grpobj._listZeile[posZeile].istStandby = true;
                                        }
                                    }
                                    standbyNichtPausePlayable--;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Beim Überprüfen der StandyBy-Songs ist eine Fehler aufgetreten: Datenfehler" + Environment.NewLine + "titel=" + titel, ex);
            }
        }

        void KlangPlayEndetimer_Tick(object sender, EventArgs e)
        {
            int posit = -1;
            int zeile = -1;
            int neu = -1;
            double wertPlus = -1;
            int IndexPlus = -1;
            try
            {
                (sender as DispatcherTimer).Stop();

                UInt16 sollID_Zeile = Convert.ToUInt16((sender as DispatcherTimer).Tag);

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
                            if (IndexPlus != 0)
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
                ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Fehler beim Überprüfen der Endewartezeit" + Environment.NewLine +
                 "Zeile=" + zeile + "   wertPlus" + wertPlus + "   Neu=" + neu + "   IndexPlus=" + IndexPlus + " Posit=" + posit, ex);
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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswerten des Musikfehlers ist ein Fehler aufgetreten.", ex);
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

                    if (!_GrpObjecte[posObjGruppe].wirdAbgespielt)
                    {
                        if (((FadingOutGeräusche)_timerFadingOutGeräusche.Tag).gruppen.FirstOrDefault(t => t.grpobj == _GrpObjecte[posObjGruppe]) != null)
                        {
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].istPause = false;
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = true;
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].istLaufend = false;
                        }
                        else
                            return;
                    }

                    int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
                    if (objGruppe == -1)
                        return;

                    if (!_GrpObjecte[posObjGruppe].istMusik &&           // Direkt wieder anstarten wenn der Titel die einigste Möglichkeit ist
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Pause == 0 &&
                        _GrpObjecte[posObjGruppe].aPlaylist.MaxSongsParallel == _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend).Count &&
                        _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istStandby).Count == 0)
                    {
                        _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Position = (!_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.TeilAbspielen) ?
                            TimeSpan.FromMilliseconds(0) : TimeSpan.FromMilliseconds(Math.Round(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.TeilStart.Value, 0, MidpointRounding.ToEven));

                        if (_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseChange)
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Pause =
                                (new Random()).Next(_GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert,
                                    _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert);
                    }
                    else
                    {
                        _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Stop();
                        KlangPlayEndetimer = new DispatcherTimer();

                        //Neue Wartezeit fest oder per RANDOM bestimmen                        
                        KlangPlayEndetimer.Interval = (_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseChange) ?
                            TimeSpan.FromMilliseconds(
                                (new Random()).Next(_GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert,
                                _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert)) :
                            TimeSpan.FromMilliseconds(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Pause);

                        _GrpObjecte[posObjGruppe]._listZeile[zeile].istWartezeit = true;
                        KlangPlayEndetimer.Tag = _GrpObjecte[posObjGruppe]._listZeile[zeile].ID_Zeile;
                        KlangPlayEndetimer.Tick += new EventHandler(KlangPlayEndetimer_Tick);
                        KlangPlayEndetimer.Start();
                        lstKlangPlayEndetimer.Add(KlangPlayEndetimer);

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
                ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Nach dem Beenden des Musiktitels ist ein Fehler aufgetreten", ex);
            }
        }


        void Player_KlangMediaFailed(object sender, ExceptionEventArgs e)
        {
            try
            {
                char[] Separator = new char[] { '_' };

                int mediahash = (sender as MediaPlayer).GetHashCode();
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpobj in _GrpObjecte)
                    if (chkgrpobj._listZeile.Exists(t => t.mediaHashCode.Equals(mediahash)))
                    {
                        grpobj = chkgrpobj;
                        break;
                    }

                if (grpobj != null)
                {
                    int zeile = grpobj._listZeile.FindIndex(t => t.mediaHashCode.Equals(mediahash));

                    int objGruppe = grpobj.objGruppe;
                    if (objGruppe == -1)
                        return;

                    if (!File.Exists(grpobj._listZeile[zeile].audiotitel.Audio_Titel.Pfad + "\\" + grpobj._listZeile[zeile].audiotitel.Audio_Titel.Datei))
                    {
                        if (grpobj.visuell)
                        {
                            grpobj._listZeile[zeile].audioZeile.lbiEditorRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));       // Red
                            grpobj._listZeile[zeile].audioZeile.lbiEditorRow.ToolTip = "Datei nicht gefunden. -> " +
                                grpobj._listZeile[zeile].audiotitel.Audio_Titel.Pfad + "\\" + grpobj._listZeile[zeile].audiotitel.Audio_Titel.Datei;
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
                            mZeile.spnlMusikZeile.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 80));   // Yellow
                            mZeile.spnlMusikZeile.ToolTip = grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.audiotitel.Aktiv).Count + " von " + grpobj._listZeile.FindAll(t => t.audiotitel.Aktiv).Count + " Titel abspielbar";
                        }
                    }
                    CheckPlayStandbySongs(grpobj);
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswetrten des Klang-Playerfehlers ist ein Fehler aufgetreten.", ex);
            }
        }


        private void lbMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbPListMusik.Items.Count != lbMusik.Items.Count)
                    AktualisierePListPlaylist();
                if (lbPListMusik.SelectedIndex != lbMusik.SelectedIndex)
                    lbPListMusik.SelectedIndex = lbMusik.SelectedIndex;

                if (e != null)
                {
                    ListBox selListBox = ((ListBox)(e.Source));
                    if (selListBox.SelectedItems.Count != 0)
                    {
                        try
                        {
                            if (MusikProgBarTimer != null)
                            {
                                MusikProgBarTimer.Stop();
                                btnBGAbspielen.Tag = true;
                                btnBGAbspielen_Click(btnBGAbspielen, new RoutedEventArgs());
                            }

                            Audio_Playlist playlistliste = Global.ContextAudio.PlaylistListe.FindAll(t => t.Audio_PlaylistGUID.Equals(((MusikZeile)lbMusik.SelectedItem).Tag)).FirstOrDefault();
                            if (playlistliste != null)
                            {
                                lbMusik.Tag = selListBox.SelectedIndex;
                                lbMusiktitellist.Items.Clear();
                                _BGPlayer.NochZuSpielen.Clear();
                                _BGPlayer.Gespielt.Clear();
                                _BGPlayer.AktPlaylist = playlistliste;

                                _BGPlayer.AktTitel.Clear();
                                btnBGRepeat.IsChecked = _BGPlayer.AktPlaylist.Repeat;

                                btnBGShuffle.IsChecked = _BGPlayer.AktPlaylist.Shuffle;
                                btnShuffle_Click(btnBGShuffle, null);

                                if (playlistliste.Audio_Playlist_Titel.Count > 0)
                                {
                                    foreach (Audio_Playlist_Titel playlisttitel in playlistliste.Audio_Playlist_Titel.OrderBy(t => t.Reihenfolge))
                                    {
                                        _BGPlayer.AktTitel.Add(playlisttitel.Audio_Titel);
                                        ListBoxItem lbitem = new ListBoxItem();
                                        lbitem.Tag = playlisttitel.Audio_Titel.Audio_TitelGUID;
                                        lbitem.Content = playlisttitel.Audio_Titel.Name;
                                        lbitem.ToolTip = playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;

                                        if (!playlistliste.Audio_Playlist_Titel.First(t => t.Audio_TitelGUID == playlisttitel.Audio_Titel.Audio_TitelGUID).Aktiv)
                                        {
                                            lbitem.FontStyle = FontStyles.Italic;
                                            lbitem.Foreground = Brushes.DarkSlateGray;
                                            lbitem.ToolTip = "Audio-Titel inaktiv." + Environment.NewLine + "Im Playlist-Editor anhaken zum Aktivieren" +
                                                             Environment.NewLine + "Anklicken um den Titel abzuspielen";
                                        }
                                        lbMusiktitellist.Items.Add(lbitem);
                                    }
                                    RenewMusikNochZuSpielen();

                                    btnBGAbspielen.IsEnabled = true;
                                    btnBGAbspielen.Tag = false;
                                    btnBGNext.IsEnabled = true;
                                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Maximum = pbarBGSong.Maximum;
                                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Value = pbarBGSong.Value;
                                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Visibility = Visibility.Visible;

                                    SpieleNeuenMusikTitel(Guid.Empty);
                                    if (playlistliste.Audio_Playlist_Titel.Count == 0)
                                    {
                                        btnBGAbspielen.Tag = true;
                                        btnBGAbspielen_Click(btnBGAbspielen, new RoutedEventArgs());
                                    }

                                    _BGPlayer.totalLength = -1;
                                }
                                else
                                {
                                    grdSongInfo.Visibility = Visibility.Hidden;
                                    btnBGNext.IsEnabled = false;
                                    btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                    btnBGAbspielen.IsEnabled = false;
                                }
                            }
                            else
                            {
                                lbMusik.Tag = -1;
                                grdSongInfo.Visibility = Visibility.Hidden;
                                btnBGNext.IsEnabled = false;
                                btnBGAbspielen.IsEnabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Die Playliste konnte nicht geöffnet werden oder die Playliste ist leer", ex);
                        }
                    }
                    else
                    {
                        if (selListBox.SelectedItems.Count == 0)
                        {
                            btnBGNext.IsEnabled = false;
                            btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            btnBGAbspielen.IsEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Beim Auswählen der Playlist ist ein Fehler aufgetreten", ex);
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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Visualisieren der Musik-Infos ist ein Fehler aufgetreten.", ex);
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
            if (_BGPlayer.NochZuSpielen.Count == 0 && _BGPlayer.AktPlaylist.Repeat)
                RenewMusikNochZuSpielen();

            if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds > 0)
            {
                if (lbMusiktitellist.SelectedIndex != -1 && btnBGPrev.Tag == null)
                {
                    _BGPlayer.Gespielt.Add((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);

                    if (_BGPlayer.Gespielt.Count > 50)
                        _BGPlayer.Gespielt.RemoveAt(0);
                }
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
                        if (_BGPlayer.AktPlaylist.Shuffle)
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

                            _BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals((Guid)((ListBoxItem)lbMusiktitellist.Items[i]).Tag));
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
                //lbMusik.Tag = -1;
                //	lbMusik.SelectedIndex = -1;
                lbMusiktitellist.SelectedIndex = -1;
            }
        }

        private void btnBGSpeaker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(btnBGSpeaker.Tag) != -1)
                {
                    btnBGSpeaker.Tag = -1;
                    imgbtnBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
                    if (_BGPlayer.BG[0].mPlayer != null) _BGPlayer.BG[0].mPlayer.IsMuted = false;
                    if (_BGPlayer.BG[1].mPlayer != null) _BGPlayer.BG[1].mPlayer.IsMuted = false;
                }
                else
                {
                    btnBGSpeaker.Tag = slBGVolume.Value;
                    imgbtnBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker-mute.png"));
                    if (_BGPlayer.BG[0].mPlayer != null) _BGPlayer.BG[0].mPlayer.IsMuted = true;
                    if (_BGPlayer.BG[1].mPlayer != null) _BGPlayer.BG[1].mPlayer.IsMuted = true;
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Klicken des Lautsprecher-Buttons ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnBGAbspielen_Click(object sender, RoutedEventArgs e)
        {                                   //btnBGAbspielen.Tag = 0 --> Button hat Play-Icon, kein Sound spielt
            try
            {
                if (!Convert.ToBoolean(btnBGAbspielen.Tag) && !_BGPlayer.BG[_BGPlayer.aktiv].isPaused ||
                    _BGPlayer.BG[_BGPlayer.aktiv].isPaused &&
                    (_BGPlayer.BG[_BGPlayer.aktiv].aPlaylist == null ||
                     _BGPlayer.BG[_BGPlayer.aktiv].aPlaylist.Audio_PlaylistGUID == (Guid)((MusikZeile)lbMusik.SelectedItem).Tag))
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

                        _BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;
                        FadingIn(_BGPlayer.BG[_BGPlayer.aktiv], _BGPlayer.BG[_BGPlayer.aktiv].mPlayer, Convert.ToDouble(_BGPlayer.AktPlaylistTitel.Volume) / 100);
                        btnBGAbspielen.Tag = true;
                        imgbtnBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
                    }
                    else
                        SpieleNeuenMusikTitel(Guid.Empty);

                    btnBGStoppen.IsEnabled = true;
                    btnBGPrev.IsEnabled = true;
                }
                else
                {
                    if (Convert.ToInt16(lbMusik.Tag) == lbMusik.SelectedIndex)          // Auswahl Playliste nicht geändert
                    {
                        if (Convert.ToBoolean(btnBGAbspielen.Tag))
                        {
                            if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                            {
                                _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
                                BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], false, true);
                                _BGPlayer.BG[_BGPlayer.aktiv].aPlaylist = null;
                            }
                        }
                        else
                        {
                            _BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;
                            FadingIn(_BGPlayer.BG[_BGPlayer.aktiv], _BGPlayer.BG[_BGPlayer.aktiv].mPlayer, Convert.ToDouble(_BGPlayer.AktPlaylistTitel.Volume) / 100);
                        }
                    }
                    imgbtnBGAbspielen.Source = Convert.ToBoolean(btnBGAbspielen.Tag) ?
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")) :
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));

                    btnBGAbspielen.Tag = !Convert.ToBoolean(btnBGAbspielen.Tag);
                }
                (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Play-/Pause-Buttons ist ein Fehler aufgetreten.", ex);
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
                {
                    _BGPlayer.Gespielt.Add((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
                    if (_BGPlayer.Gespielt.Count > 50)
                        _BGPlayer.Gespielt.RemoveAt(0);
                }
                btnBGPrev.IsEnabled = false;

                MusikProgBarTimer.Stop();
                btnBGAbspielen.Tag = false;
                grdSongInfo.Visibility = Visibility.Hidden;
                lbMusiktitellist.SelectedIndex = -1;
                imgbtnBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                if (lbPListMusik.SelectedItem != null)
                    ((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Visibility = Visibility.Collapsed;
                btnBGStoppen.IsEnabled = false;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Stop-Buttons ist ein Fehler aufgetreten.", ex);
            }
        }

        private void lbMusik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lbMusik_SelectionChanged(sender, null);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Doppelklicken des Maus-Buttons ist ein Fehler aufgetreten.", ex);
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
                    lbMusiktitellist.Tag = lbMusiktitellist.SelectedIndex;
                    SpieleNeuenMusikTitel(Guid.Empty);
                }
                if (lbPListMusik.SelectedItem != null)
                    (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Next-Buttons ist ein Fehler aufgetreten.", ex);
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
                    btnBGPrev.Tag = true;
                    SpieleNeuenMusikTitel(vorher);
                    btnBGPrev.Tag = null;
                    if (_BGPlayer.Gespielt.Count > 0)
                        _BGPlayer.Gespielt.RemoveAt(_BGPlayer.Gespielt.Count - 1);
                    lbMusiktitellist.Tag = -1;
                }
                (lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Zurück-Buttons ist ein Fehler aufgetreten.", ex);
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

                // Check doppelte Playlists
                if (grdEditorListe.Tag != null &&
                    ((Guid)grdEditorListe.Tag) == GuidSoll)
                {
                    ViewHelper.Popup("Doppelte Playlist im Theme" + Environment.NewLine + "Die Playlist ist bereits in dem Theme aufgelistet und kann nicht nochmals benutzt werden.");
                    lbEditor.SelectedIndex = -1;
                    found = true;
                }

                // Check zwei Musik-Playlists
                if (!found && grdEditorListe.Tag != null)
                {
                    Audio_Playlist aplyLst = Global.ContextAudio.PlaylistListe.First(t => t.Audio_PlaylistGUID == (Guid)grdEditorListe.Tag);
                    if (aplyLst != null && aplyLst.Hintergrundmusik)
                        hasHintergrund = true;
                }

                if (grdEditorListe.Tag != null && hasHintergrund &&
                    Global.ContextAudio.PlaylistListe.First(t => t.Audio_PlaylistGUID == GuidSoll).Hintergrundmusik)
                {
                    ViewHelper.Popup("Hintergrund-Playlist Error" + Environment.NewLine + "Das Theme enthält schon eine Hintergrund-Playlist. Pro Theme kann nur eine Hintergrund-Playlist abgespielt werden.");
                    lbEditor.SelectedIndex = -1;
                    found = true;
                }
            }
            return !found;
        }

        private void lbEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsInitialized)
                return;
            try
            {
                if ((tcAudioPlayer.SelectedItem == tiEditor) && (rbEditorEditTheme.IsChecked.Value))
                {
                    lbEditor.SelectionChanged -= new SelectionChangedEventHandler(lbEditor_SelectionChanged);
                    if (e.RemovedItems.Count != 0)
                        lbEditor.SelectedItem = e.RemovedItems[0];
                    else
                        lbEditor.SelectedIndex = -1;
                    lbEditor.SelectionChanged += new SelectionChangedEventHandler(lbEditor_SelectionChanged);
                    return;
                }

                if (lbEditor.SelectedIndex != -1 &&
                    (tcAudioPlayer.SelectedItem == tiEditor && rbEditorEditPList.IsChecked.Value ||
                     ChecklbEditorPossible((ListboxItemIcon)(e.RemovedItems.Count == 0 ? null : e.RemovedItems[0]))))
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    try
                    {
                        lbEditorListe.Items.Clear();
                        int was = lbEditor.SelectedIndex;
                        lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                        lbEditor.SelectedIndex = was;
                        lbEditor.SelectionChanged += lbEditor_SelectionChanged;

                        GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                        if (grpobj == null)
                        {
                            tiPlus_MouseUp(false, null);
                            grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                        }                        

                        if (lbEditor.SelectedIndex == -1)
                        {
                            lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                            lbEditor.SelectedIndex = was;
                            lbEditor.SelectionChanged += lbEditor_SelectionChanged;
                        }
                        List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.
                           Where(t => t.Audio_PlaylistGUID.Equals(((ListboxItemIcon)lbEditor.Items[lbEditor.SelectedIndex]).Tag)).ToList();

                        if (playlistliste.Count == 1)
                        {
                            List<Audio_Playlist_Titel> lstPlylistTitel = playlistliste[0].Audio_Playlist_Titel.OrderBy(t2 => t2.Reihenfolge).ToList();

                            if (rbEditorEditTheme.IsChecked.Value)
                            {
                                if (rbEditorEditTheme.IsEnabled == true && AktKlangTheme == null)
                                {
                                    NeueKlangThemeInDB(tboxEditorName.Text);
                                    tboxEditorName.Focus();
                                }
                            }

                            if (e != null && grpobj.aPlaylist != null && rbEditorEditTheme.IsChecked.Value && AktKlangTheme != null &&
                                AktKlangTheme.Audio_Playlist.Contains(grpobj.aPlaylist))
                            {
                                AktKlangTheme.Audio_Playlist.Remove(grpobj.aPlaylist);
                                Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                            }

                            //Letzte visuell TabItem befreien von Zeilen
                            PlaylisteLeeren(_GrpObjecte.FirstOrDefault(t => t.visuell));

                            AktKlangPlaylist = playlistliste[0];

                            if (AktKlangPlaylist.Hintergrundmusik)
                                grpobj.rbTopIstMusikPlaylist.IsChecked = true;
                            else
                                grpobj.rbTopIstKlangPlaylist.IsChecked = true;

                            rbEditorFading.IsChecked = AktKlangPlaylist.Fading;

                            grdEditorMain.Focus();
                            grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
                            grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;

                            grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallelX_TextChanged;
                            grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;
                            grpobj.tbTopKlangSongsParallel.Text = "1";
                            grpobj.istMusik = AktKlangPlaylist.Hintergrundmusik;

                            grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallelX_TextChanged;

                            grpobj.playlistName = AktKlangPlaylist.Name;
                                
                            if (lstPlylistTitel.Count > 0)
                            {
                                UInt16 x = 0;
                                foreach (Audio_Playlist_Titel playlisttitel in lstPlylistTitel)
                                {
                                    if (!System.IO.Path.IsPathRooted(playlisttitel.Audio_Titel.Pfad + "\\" + (playlisttitel.Audio_Titel.Datei == null ? "" : playlisttitel.Audio_Titel.Datei)) &&
                                        !File.Exists(playlisttitel.Audio_Titel.Pfad + "\\" + (playlisttitel.Audio_Titel.Datei == null ? "" : playlisttitel.Audio_Titel.Datei)))
                                    {
                                        playlisttitel.Audio_Titel = setTitelStdPfad(playlisttitel.Audio_Titel);
                                        if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei)) &&
                                            File.Exists(playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei))
                                            Global.ContextAudio.Update<Audio_Titel>(playlisttitel.Audio_Titel);
                                    }

                                    KlangNewRow(playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei, grpobj, x, playlisttitel);
                                    if (playlisttitel.Aktiv &&
                                        !grpobj.NochZuSpielen.Contains(playlisttitel.Audio_TitelGUID))
                                    {
                                        for (int i = 0; i <= playlisttitel.Rating; i++)
                                            grpobj.NochZuSpielen.Add(playlisttitel.Audio_TitelGUID);
                                    }
                                    if (playlisttitel.Reihenfolge != x)
                                        playlisttitel.Reihenfolge = x;
                                    x++;
                                }
                                if (AktKlangPlaylist.Hintergrundmusik)
                                    ZeigeZeileKlangSpalten(grpobj, false);
                            }
                            grpobj.aPlaylist = AktKlangPlaylist;
                            UpdatePlaylistLänge(AktKlangPlaylist);
                            grpobj.grdEditor.Visibility = Visibility.Visible;
                            grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();

                            CheckAlleAngehakt(grpobj);
                            

                            grpobj.grdEditor.Visibility = Visibility.Visible;

                            if (grpobj.wirdAbgespielt)
                            {
                                if (grpobj.istMusik)
                                    grpobj.wirdAbgespielt = false;
                                ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                                ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(1, 2, 0, 0);
                            }

                            if (AktKlangPlaylist.Hintergrundmusik)
                            {
                                ZeigeKlangSongsParallel(grpobj, false);
                                //ZeigeKlangTop(grpobj, false);
                                grpobj.spnlTopHotkey.Visibility = Visibility.Collapsed;

                                btnEditRepeat.IsChecked = AktKlangPlaylist.Repeat;
                                btnEditShuffle.IsChecked = AktKlangPlaylist.Shuffle;
                                //btnEditRepeat.Visibility = Visibility.Visible;
                                //btnEditShuffle.Visibility = Visibility.Visible;
                                btnShuffle_Click(btnEditShuffle, null);
                            }
                            else
                            {
                                ZeigeKlangSongsParallel(grpobj, true);
                                //ZeigeKlangTop(grpobj, true);
                                grpobj.spnlTopHotkey.Visibility = Visibility.Visible;

                                hotkey hKey = hotkeys.FirstOrDefault(t => t.aPlaylistGuid == AktKlangPlaylist.Audio_PlaylistGUID);
                                grpobj.btnTopHotkeySet.Content = (hKey == null) ? "nicht definiert" : Convert.ToChar(hKey.taste).ToString();

                                grpobj.btnHotkeyEntfernen.Visibility = hKey == null ? Visibility.Collapsed : Visibility.Visible;
                                grpobj.cmboxTopHotkey.SelectedIndex = -1;

                                //btnEditRepeat.Visibility = Visibility.Collapsed;
                                //btnEditShuffle.Visibility = Visibility.Collapsed;
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
                            if (((TabItem)tcAudioPlayer.SelectedItem) == tiEditor && lbEditorTheme.Tag == null)   //lbEditorTheme.Tag == null --> lbEditorTheme geklickt
                                CheckBtnGleicherPfad(_GrpObjecte.FirstOrDefault(t => t.visuell));

                            if (((TabItem)tcAudioPlayer.SelectedItem) == tiEditor)
                            {
                                tboxEditorName.Text = AktKlangPlaylist.Name;

                                GetTotalLength(AktKlangPlaylist);
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
                ViewHelper.ShowError("Auswahlfehler" + Environment.NewLine + "Beim Ändern der Selektierung des 'lbEditor' ist ein Fehler aufgetreten", ex);
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
                                    wpnlEditorThemeGeräusch.Children.Clear();
                                    wpnlEditorThemeMusik.Children.Clear();
                                    wpnlEditorTopThemesThemes.Children.Clear();

                                    AktKlangTheme = atheme;
                                    AktualisiereEditorPlaylist();

                                    foreach (Audio_Playlist aPlaylist in atheme.Audio_Playlist)
                                    {
                                        foreach (ListboxItemIcon lbi in lbEditor.Items)
                                        {
                                            if (((Guid)lbi.Tag) == aPlaylist.Audio_PlaylistGUID)
                                                ThemeItemIconAblegen(lbi);
                                        }
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
                                    tboxEditorName.Text = atheme.Name;

                                    tboxThemeKategorie.Text = atheme.Kategorie;
                                }
                            }
                            catch (Exception ex)
                            {
                                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Die Playlist-Liste konnte nicht eindeutig in der Datenbank detektiert werden.", ex);

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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Wechseln der Listbox im Theme-Mode ist ein Fehler aufgetreten.", ex);
            }
        }

        private void setStdPfad()
        {
            char[] charsToTrim = { '\\' };
            if (stdPfad.Count > 0) stdPfad.RemoveRange(0, stdPfad.Count);
            stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
        }

        private void chkStdPfadVerfügbar()
        {
            setStdPfad();
            /*   foreach (ListboxItemBtn lbb in lbStandardPfade.Items)
               {
                   lbb.lblStdPfad.IsEnabled = Directory.Exists(lbb.lblStdPfad.Content.ToString());
                   if (!lbb.lblStdPfad.IsEnabled)
                       stdPfad.Remove(stdPfad.First(t => t == (string)lbb.lblStdPfad.Content));
               }*/
        }


        private Audio_Titel setTitelStdPfad(Audio_Titel aTitel)
        {
            char[] charsToTrim = { '\\' };
            //Check Titel -> Pfad vorhanden ansonsten Standard-Pfad hinzufügen
            if (//System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                File.Exists(aTitel.Pfad + "\\" + aTitel.Datei))
            {
                foreach (string pfad in stdPfad)
                {
                    if (pfad == aTitel.Pfad)
                        return aTitel;

                    if (aTitel.Pfad != null && (aTitel.Pfad + "\\" + aTitel.Datei).Contains(pfad))
                    {
                        aTitel.Datei = (aTitel.Pfad.EndsWith("\\") ? aTitel.Pfad + aTitel.Datei : aTitel.Pfad + "\\" + aTitel.Datei).
                            Substring(pfad.EndsWith("\\") ? pfad.Length : pfad.Length + 1);
                        aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                        return aTitel;
                    }
                }
                // Pfad noch kein Standard-Pfad
                if (ViewHelper.Confirm("Audio-Pfad ist kein Standard-Pfad", "Der Pfad der Audio-Datei konnte nicht unter den Standard-Pfaden gefunden werden." +
                    Environment.NewLine + "In dieser Konstellation ist es nicht zulässig, den Titel abzuspielen." + Environment.NewLine +
                    "Soll der Pfad mit in die Standard-Pfade integriert werden?" + Environment.NewLine + Environment.NewLine + "Neuer Pfad:     " + aTitel.Pfad))
                {
                    MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis =
                        MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis + "|" + aTitel.Pfad;
                    setStdPfad();
                }
                return aTitel;
            }

            //Pfad+Titel nicht gefunden -> Check Titel in einem anderen Standard-Pfad
            foreach (string pfad in stdPfad)
            {
                if (aTitel.Datei == null && aTitel.Pfad != null)
                {
                    aTitel.Datei = aTitel.Pfad;
                    aTitel.Pfad = "";
                }
                if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + aTitel.Datei))
                {
                    aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                    return aTitel;
                }

                if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + System.IO.Path.GetFileName(aTitel.Datei)))
                {
                    aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                    aTitel.Datei = System.IO.Path.GetFileName(aTitel.Datei);
                    return aTitel;
                }
            }

            if (AudioInAnderemPfadSuchen)
            {
                //ab hier: kein Std.-Pfad ist gültig -> Check in jedem Std.-Pfad mit Suche incl. Unterverzeichnisse nach dem Dateinamen
                string gesuchteDatei = System.IO.Path.GetFileName(stdPfad[0].TrimEnd(charsToTrim) + "\\" + aTitel.Datei);
                foreach (string pfad in stdPfad)
                {
                    if (pfad != "C:\\" && Directory.Exists(pfad))
                    {
                        string[] pfad_datei = Directory.GetFiles(pfad.TrimEnd(charsToTrim), gesuchteDatei, SearchOption.AllDirectories);
                        if (pfad_datei.Length > 0)
                        {
                            aTitel.Pfad = System.IO.Path.GetDirectoryName(pfad_datei[0]);
                            aTitel.Datei = System.IO.Path.GetFileName(pfad_datei[0]);
                            aTitel = setTitelStdPfad(aTitel);
                            return aTitel;
                        }
                    }
                }
            }

            if (aTitel.Pfad == null) aTitel.Pfad = "";
            if (aTitel.Pfad == "" || aTitel.Datei == null)
            {
                string pfadDatei = aTitel.Pfad != null || aTitel.Pfad != "" ? aTitel.Pfad : "";
                if (pfadDatei != "" && !pfadDatei.EndsWith("\\"))
                    pfadDatei = pfadDatei + "\\";
                if (aTitel.Datei != null)
                    pfadDatei = pfadDatei + aTitel.Datei;

                aTitel.Pfad = System.IO.Path.GetDirectoryName(pfadDatei);
                aTitel.Datei = System.IO.Path.GetFileName(pfadDatei);
            }

            return aTitel;
        }

        private void miListPlaylisttitel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (AktKlangPlaylist == null) return;
            if (((MenuItem)sender).Items.Count != Global.ContextAudio.PlaylistListe.Count)
            {
                ((MenuItem)sender).Items.Clear();
                foreach (Audio_Playlist aPlay in Global.ContextAudio.PlaylistListe.FindAll(t2 => t2.Audio_PlaylistGUID != AktKlangPlaylist.Audio_PlaylistGUID).OrderBy(t => t.Name))
                {
                    MenuItem mi = new MenuItem();
                    mi.Header = aPlay.Name;
                    mi.Tag = aPlay.Audio_PlaylistGUID;
                    mi.Click += miCopyTitelToPlaylist_Click;
                    ((MenuItem)sender).Items.Add(mi);
                }
            }
        }

        private void miCopyTitelToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                KlangZeile kZeile = (KlangZeile)((ContextMenu)(((MenuItem)(((MenuItem)sender).Parent)).Parent)).Tag;
                Guid playGUID = (Guid)(((MenuItem)sender).Tag);
                Audio_Playlist zielPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == playGUID);

                if (zielPlaylist != null)
                {
                    List<string> gedroppteDateien = new List<string>();
                    gedroppteDateien.Add((string)kZeile.audioZeile.chkTitel.Tag);
                    _DateienAufnehmen(gedroppteDateien, kZeile.audioZeile, zielPlaylist, 0, false);
                    Global.ContextAudio.Update<Audio_Playlist>(zielPlaylist);
                }
                else
                {
                    ViewHelper.Popup("Die Playliste konnte nicht gefunden werden." + Environment.NewLine + Environment.NewLine + "Vorgang abgebrochen");
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fehler" + Environment.NewLine + "Beim Ausführen der Duplizierung in eine andere Playliste ist ein Fehler aufgetreten", ex);
            }
        }

        private void lblKlangZeileCMenuÄndern_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i = Convert.ToInt16(((Label)sender).Tag);
            try
            {
                KlangZeile kZeile = (KlangZeile)((ContextMenu)((Label)sender).Parent).Tag;

                switch (i)
                {
                    case 1:                                                     // Geräusch/Musiktitel löschen
                        imgTrash0_0_MouseUp(kZeile.audioZeile.imgTrash, e);
                        break;
                    case 2:                                                     // Datei-Bezug ändern                         
                        kZeile.audiotitel.Audio_Titel = setTitelStdPfad(kZeile.audiotitel.Audio_Titel);
                        Global.ContextAudio.Update<Audio_Titel>(kZeile.audiotitel.Audio_Titel);

                        FileInfo fi = new FileInfo(kZeile.audiotitel.Audio_Titel.Pfad + @"\" + kZeile.audiotitel.Audio_Titel.Datei);
                        string aktDir = Environment.CurrentDirectory;
                        Environment.CurrentDirectory = fi.DirectoryName;
                        string datei = ViewHelper.ChooseFile("Datei auswählen", fi.Name, false, validExt);
                           //fi.Extension.Substring(1)+",.mp3");
                        Environment.CurrentDirectory = aktDir;

                        if (datei != null)
                        {
                            kZeile.audiotitel.Audio_Titel.Pfad = System.IO.Path.GetDirectoryName(datei);
                            kZeile.audiotitel.Audio_Titel.Datei = System.IO.Path.GetFileName(datei);
                            kZeile.audioZeile.chkTitel.Content = System.IO.Path.GetFileNameWithoutExtension(datei);
                            kZeile.audioZeile.chkTitel.ToolTip = datei;
                            Global.ContextAudio.Update<Audio_Titel>(kZeile.audiotitel.Audio_Titel);
                        }
                        break;
                    case 4:                                                 // Titel duplizieren    
                        AudioZeileItemAblegen(kZeile.audioZeile, AktKlangPlaylist, null);
                        break;
                    case 5:                                                 // Dateipfad öffnen
                        if (Directory.Exists(kZeile.audiotitel.Audio_Titel.Pfad) &&
                            File.Exists(kZeile.audiotitel.Audio_Titel.Pfad + "\\" + kZeile.audiotitel.Audio_Titel.Datei))
                        {
                            FileInfo fi2 = new FileInfo(kZeile.audiotitel.Audio_Titel.Pfad + "\\" + kZeile.audiotitel.Audio_Titel.Datei);                            
                            System.Diagnostics.Process.Start("explorer.exe", "/e,/select," + fi2.DirectoryName + "\\" + @"""" + fi2.Name + @""""); 
                        }
                        else
                            ViewHelper.Popup("Die Datei bzw. das Verzeichnis konnte nicht gefunden werden");
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fehler" + Environment.NewLine + "Beim Ausführen der Kontextfunktion " + i + " ist ein Fehler aufgetreten", ex);
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
                _chkAnzDateien.allFilesMP3.Clear();
                _chkAnzDateien.allFilesWAV.Clear();
                _chkAnzDateien.allFilesOGG.Clear();
                _chkAnzDateien.allFilesWMA.Clear();

                /*   if (!System.IO.Path.IsPathRooted(_chkAnzDateien.titelliste[0].Pfad) ||
                       !File.Exists(_chkAnzDateien.titelliste[0].Pfad + "\\" + _chkAnzDateien.titelliste[0].Datei))
                   {
                       _chkAnzDateien.titelliste[0] = setTitelStdPfad(_chkAnzDateien.titelliste[0]);
                       if (System.IO.Path.IsPathRooted(_chkAnzDateien.titelliste[0].Pfad) &&
                           File.Exists(_chkAnzDateien.titelliste[0].Pfad + "\\" + _chkAnzDateien.titelliste[0].Datei))
                               Global.ContextAudio.Update<Audio_Titel>(_chkAnzDateien.titelliste[0]);
                   }*/

                if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(_chkAnzDateien.titelliste[0].Pfad + "\\" + _chkAnzDateien.titelliste[0].Datei)))
                {
                    _chkAnzDateien.titelRef = System.IO.Path.GetDirectoryName(_chkAnzDateien.titelliste[0].Pfad + "\\" + _chkAnzDateien.titelliste[0].Datei);

                    _chkAnzDateien.titelliste.ForEach(delegate(Audio_Titel aTitel)
                    {
                        string vergleich = System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei);

                        while (!vergleich.StartsWith(_chkAnzDateien.titelRef))
                        {
                            if (_chkAnzDateien.titelRef.Contains(@"\"))
                                _chkAnzDateien.titelRef = _chkAnzDateien.titelRef.Substring(0, _chkAnzDateien.titelRef.LastIndexOf(@"\"));
                            else break;
                        }
                    });
                    if (Directory.Exists(_chkAnzDateien.titelRef))
                    {
                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.mp3", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesMP3.Add(datei);

                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.wav", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesWAV.Add(datei);

                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.ogg", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesOGG.Add(datei);

                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.wma", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesWMA.Add(datei);

                        if (_chkAnzDateien.grpobj.aPlaylist != AktKlangPlaylist) return;
                    }
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
                    int all = _chkAnzDateien.allFilesMP3.Count + _chkAnzDateien.allFilesOGG.Count +
                              _chkAnzDateien.allFilesWAV.Count + _chkAnzDateien.allFilesWMA.Count;
                    if (_chkAnzDateien.grpobj.aPlaylist == AktKlangPlaylist &&
                        System.IO.Path.IsPathRooted(_chkAnzDateien.titelRef) &&
                        Directory.Exists(_chkAnzDateien.titelRef) &&
                        _chkAnzDateien.aPlaylist == AktKlangPlaylist &&
                        all > 0)
                    {
                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.Tag = _chkAnzDateien.titelRef;
                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.IsEnabled = true;

                        _chkAnzDateien.grpobj.btnKlangUpdateFiles.ToolTip = _chkAnzDateien.titelRef + Environment.NewLine + "Update der Titel im o.g. Verzeichnis" + Environment.NewLine +
                            _chkAnzDateien.titelliste.Count + " Dateien sind in der Playlist vorhanden." + Environment.NewLine +
                            all + " neue Sound-Dateien wurden incl. den Unterverzeichnisse gefunden" + Environment.NewLine + Environment.NewLine +
                            _chkAnzDateien.allFilesMP3.Count + " neue MP3-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesOGG.Count + " neue OGG-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesWAV.Count + " neue WAV-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesWMA.Count + " neue WMA-Dateien gefunden." + Environment.NewLine + Environment.NewLine +
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
            grpobj.btnKlangUpdateFiles.Tag = null;
            grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;

            List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            if (titelliste.Count > 0)
            {
                /* if (!System.IO.Path.IsPathRooted(titelliste[0].Pfad + "\\" + titelliste[0].Datei) ||
                     !File.Exists(titelliste[0].Pfad + "\\" + titelliste[0].Datei))
                 {
                     titelliste[0] = setTitelStdPfad(titelliste[0]);
                     if (System.IO.Path.IsPathRooted(titelliste[0].Pfad + "\\" + titelliste[0].Datei) &&
                         File.Exists(titelliste[0].Pfad + "\\" + titelliste[0].Datei))
                         Global.ContextAudio.Update<Audio_Titel>(titelliste[0]);
                 }*/

                if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(titelliste[0].Pfad + "\\" + titelliste[0].Datei)))
                {
                    string titelRef = System.IO.Path.GetDirectoryName(titelliste[0].Pfad + "\\" + titelliste[0].Datei);

                    titelliste.ForEach(delegate(Audio_Titel aTitel)
                    {
                        string vergleich = System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei);

                        while (!vergleich.StartsWith(titelRef))
                        {
                            if (titelRef.Contains(@"\"))
                                titelRef = titelRef.Substring(0, titelRef.LastIndexOf(@"\"));
                            else break;
                        }
                    });
                    _chkAnzDateienInDir(grpobj);
                }
            }
        }

        private void CheckAlleAngehakt(GruppenObjekt grpobj)
        {
            grpobj.chkbxTopAktiv.IsChecked = (grpobj._listZeile.Count != 0 &&
                grpobj._listZeile.Count == grpobj._listZeile.FindAll(t => t.audioZeile != null).Count(t => t.audioZeile.chkTitel.IsChecked == true)) ?
                true : false;

            grpobj.chkbxTopVolChange.IsChecked = (grpobj._listZeile.Count != 0 &&
                 grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked == true).Count ==
                 grpobj.anzVolChange) ? true : false;

            grpobj.chkbxTopPauseChange.IsChecked = (grpobj._listZeile.Count != 0 &&
                grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked == true).Count ==
                grpobj.anzPauseChange) ? true : false;
        }

        private void AlleKlangSongsAus(GruppenObjekt grpobj, bool checkboxAus, bool ZeileLoeschen, bool sofortAus)
        {
            if (grpobj == null || !grpobj.wirdAbgespielt)
                return;

            grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).ForEach(delegate(KlangZeile kZeile)
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

                    if (!grpobj.istMusik || sofortAus)
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
                    grpobj.lbEditorListe.Items.Clear();// .wpnl.Children.Clear();
                    grpobj._listZeile.Clear();
                }
            });
            grpobj.tbtnKlangPause.IsChecked = false;
            grpobj.wirdAbgespielt = false;

            GC.GetTotalMemory(true);                //GC update (Memory wird aktualisiert)
        }

        private Int16 GetPosObjGruppe(int objGruppe)
        {
            Int16 posObjGruppe = Convert.ToInt16(_GrpObjecte.FindIndex(t => t.objGruppe == objGruppe));
            return posObjGruppe;
        }

        private void PlaylisteLeeren(GruppenObjekt grpobj)
        {
            if (grpobj == null)
                return;

            if (AktKlangPlaylist != null)
            {
                if (_BGPlayer.AktPlaylist == AktKlangPlaylist && grpobj.wirdAbgespielt)
                {
                    btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    lbMusik.SelectedIndex = -1;
                }
                AlleKlangSongsAus(grpobj, true, true, true);

                ZeigeKlangSongsParallel(grpobj, false);

                grpobj.wirdAbgespielt = false;
            }
            if (grpobj._listZeile.Count > 0)
            {
                grpobj.lbEditorListe.Items.Clear();
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

        private void ZeigeKlangSongsParallel(GruppenObjekt grpobj, bool sichtbar)
        {
            if (sichtbar && grpobj != null)
            {
                grpobj.tbTopKlangSongsParallel.Text = Convert.ToString(grpobj.aPlaylist != null ? grpobj.aPlaylist.MaxSongsParallel : 1);
            }
            else
            {
                if (grpobj == null)
                    lbEditor.SelectedIndex = -1;
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
                        if (kZeile.audioZeile.grdEditorRow.ColumnDefinitions[4].Width != new GridLength(0))
                        {
                            for (int i = kZeile.audioZeile.grdEditorRow.ColumnDefinitions.Count - 1; i >= 4; i--)
                                kZeile.audioZeile.grdEditorRow.ColumnDefinitions[i].Width = new GridLength(0);
                            kZeile.audioZeile.brdTrennstrich.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        //if (kZeile.audioZeile.grdEditorRow.ColumnDefinitions[2].Width != new GridLength(grpobj.grdEditorTop.ColumnDefinitions[3].Width.Value))
                        {
                            for (int i = kZeile.audioZeile.grdEditorRow.ColumnDefinitions.Count - 1; i >= 4; i--)
                                kZeile.audioZeile.grdEditorRow.ColumnDefinitions[i].Width = new GridLength(grpobj.grdEditorTop.ColumnDefinitions[i + 1].Width.Value);

                            kZeile.audioZeile.grdEditorRow.ColumnDefinitions[2].MinWidth = grpobj.grdEditorTop.ColumnDefinitions[3].MinWidth;
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
                    KlangZeile kZeile = _GrpObjecte.FirstOrDefault(t => t.visuell)._listZeile
                        .FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.rsldTeilSong == (SliderRange)sender);

                    //wenn gerade abspielt wird, entsprechend des Trigger jumped_to_start zurücksetzen
                    if (kZeile._mplayer != null && kZeile._mplayer.NaturalDuration.HasTimeSpan &&
                        kZeile._mplayer.Position.TotalMilliseconds < kZeile.audioZeile.rsldTeilSong.LowerValue)
                        kZeile.jumped_to_start = false;
                    kZeile.audiotitel.TeilStart = kZeile.audioZeile.rsldTeilSong.LowerValue;
                    kZeile.audiotitel.TeilEnde = kZeile.audioZeile.rsldTeilSong.UpperValue;

                    Global.ContextAudio.Update<Audio_Playlist_Titel>(kZeile.audiotitel);
                }
            }
            catch (Exception) { }
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

                double max = GetMusikdateiLength(kZeile.audiotitel.Audio_Titel.Pfad + "\\" + kZeile.audiotitel.Audio_Titel.Datei);
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

                    ViewHelper.ShowError("Datenfehler" + Environment.NewLine + meldung, new Exception());
                }
            }
            catch (Exception ex)
            {
                ((CheckBox)sender).Unchecked -= chkKlangZeileCMenuNurTeil_Unchecked;
                ((CheckBox)sender).IsChecked = false;
                ((CheckBox)sender).Unchecked += chkKlangZeileCMenuNurTeil_Unchecked;
                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Der Slider für einen Teil des Titel abzuspielen, konnte nicht aktiviert werden.", ex);
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
                klZeile.audioZeile.Tag = klZeile.audiotitel.Audio_TitelGUID;// klZeile.ID_Zeile;

                klZeile.audioZeile.lbiEditorRow.Tag = rowErstellt;

                //*************************************************************************************************
                //Papierkorb
                klZeile.audioZeile.imgTrash.Tag = klZeile;
                klZeile.audioZeile.chkTitel.Content = System.IO.Path.GetFileNameWithoutExtension(songdatei);

                klZeile.audioZeile.btnMoveUp.Tag = klZeile;
                klZeile.audioZeile.btnMoveDown.Tag = klZeile;

                if (playlisttitel.Aktiv)
                    klZeile.audioZeile.chkTitel.IsChecked = playlisttitel.Aktiv;

                Label lbl4 = new Label();
                lbl4.Content = "Datei duplizieren";
                lbl4.Tag = 4;
                lbl4.Padding = new Thickness(3);
                lbl4.MouseDown += lblKlangZeileCMenuÄndern_MouseDown;

                MenuItem lbl6 = new MenuItem();
                lbl6.Header = "Datei kopieren nach...";
                lbl6.Padding = new Thickness(3);
                lbl6.MouseEnter += miListPlaylisttitel_MouseEnter;

                Label lbl1 = new Label();
                lbl1.Content = "Datei entfernen";
                lbl1.Tag = 1;
                lbl1.Padding = new Thickness(3);
                lbl1.MouseDown += lblKlangZeileCMenuÄndern_MouseDown;

                Label lbl2 = new Label();
                lbl2.Content = "Dateipfad ändern";
                lbl2.Tag = 2;
                lbl2.Padding = new Thickness(3);
                lbl2.MouseDown += lblKlangZeileCMenuÄndern_MouseDown;

                Label lbl5 = new Label();
                lbl5.Content = "Dateipfad öffnen";
                lbl5.Tag = 5;
                lbl5.Padding = new Thickness(3);
                lbl5.MouseDown += lblKlangZeileCMenuÄndern_MouseDown;

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
                cMenu.Items.Add(lbl4);          //Datei duplizieren
                cMenu.Items.Add(lbl6);          //Datei kopieren nach...
                cMenu.Items.Add(lbl1);          //Datei entfernen
                cMenu.Items.Add(new Separator());
                cMenu.Items.Add(chk3);          //Nur einen Teil abspielen
                cMenu.Items.Add(new Separator());
                cMenu.Items.Add(lbl2);          //Dateipfad ändern
                cMenu.Items.Add(lbl5);          //Dateipfad öffnen

                if (!File.Exists(klZeile.audiotitel.Audio_Titel.Pfad + "\\" + (klZeile.audiotitel.Audio_Titel.Datei == null ? "" : klZeile.audiotitel.Audio_Titel.Datei)))
                {
                    klZeile.audiotitel.Audio_Titel = setTitelStdPfad(klZeile.audiotitel.Audio_Titel);
                    if (File.Exists(klZeile.audiotitel.Audio_Titel.Pfad + "\\" + klZeile.audiotitel.Audio_Titel.Datei))
                        Global.ContextAudio.Update<Audio_Titel>(klZeile.audiotitel.Audio_Titel);
                }
                klZeile.audioZeile.chkTitel.Tag = klZeile.audiotitel.Audio_Titel.Pfad + "\\" + klZeile.audiotitel.Audio_Titel.Datei;

                klZeile.audioZeile.chkTitel.ToolTip = klZeile.audiotitel.Audio_Titel.Pfad + "\\" + klZeile.audiotitel.Audio_Titel.Datei;

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

                /*klZeile.audioZeile.grdEditorRow.Name += objGruppe + "_" + row;
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
                */
                klZeile.audioZeile.lblDauer.Content = playlisttitel.Audio_Titel.Länge != null ?
                    TimeSpan.FromMilliseconds(playlisttitel.Audio_Titel.Länge.Value).ToString(@"mm\:ss") : "--:--";

                for (int i = 0; i < klZeile.audiotitel.Rating; i++)
                    klZeile.audioZeile.btnGewichtung.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                if (neuerstellen)
                {
                    klZeile.audioZeile.PreviewMouseLeftButtonDown += audiozeile_PreviewMouseLeftButtonDown;
                    klZeile.audioZeile.lbiEditorRow.MouseMove += audiozeileLbi_MouseMove;
                    klZeile.audioZeile.grdEditorRow.Drop += audiozeile_Drop;
                    klZeile.audioZeile.GiveFeedback += audiozeile_GiveFeedback;

                    klZeile.audioZeile.MouseEnter += audioZeile_MouseEnter;
                    klZeile.audioZeile.MouseLeave += audioZeile_MouseLeave;
                    klZeile.audioZeile.imgTrash.MouseUp += imgTrash0_0_MouseUp;
                    klZeile.audioZeile.btnMoveUp.Click += btnMoveUp_Click;
                    klZeile.audioZeile.btnMoveDown.Click += btnMoveDown_Click;
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

                    klZeile.audioZeile.tboxVolMin.GotFocus += tbGotFocus;
                    klZeile.audioZeile.tboxVolMax.GotFocus += tbGotFocus;
                    klZeile.audioZeile.tboxPauseMin.GotFocus += tbGotFocus;
                    klZeile.audioZeile.tboxPauseMax.GotFocus += tbGotFocus;

                    klZeile.audioZeile.tboxVolMin.LostFocus += tbLostFocus;
                    klZeile.audioZeile.tboxVolMax.LostFocus += tbLostFocus;
                    klZeile.audioZeile.tboxPauseMin.LostFocus += tbLostFocus;
                    klZeile.audioZeile.tboxPauseMax.LostFocus += tbLostFocus;
                }
                klZeile.audioZeile.EndInit();
                grpobj.lbEditorListe.Items.Add(klZeile.audioZeile);
                //*************************************************************************************************
            }

            klZeile.pauseMin_wert = Convert.ToInt16(playlisttitel.PauseMin);
            klZeile.pauseMax_wert = Convert.ToInt16(playlisttitel.PauseMax);
            klZeile.volMin_wert = Convert.ToInt16(playlisttitel.VolumeMin);
            klZeile.volMax_wert = (Convert.ToInt16(playlisttitel.VolumeMax) >= klZeile.volMin_wert) ?
                Convert.ToInt16(playlisttitel.VolumeMax) : klZeile.volMin_wert;
            klZeile.Aktuell_Volume = playlisttitel.Volume;
            klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

            klZeile.playspeed = playlisttitel.Speed;


            if (playlisttitel.Aktiv && !grpobj.istMusik)
                klZeile.istStandby = true;
            else
                klZeile.istStandby = false;

            if (!grpobj.wirdAbgespielt)
                klZeile.istPause = true;

            klZeile.playable = playlisttitel.Aktiv;

            grpobj._listZeile.Add(klZeile);
            if (grpobj.visuell && klZeile.audiotitel.TeilAbspielen)
            {
                klZeile.audioZeile.rsldTeilSong.Visibility = Visibility.Visible;
                klZeile.audioZeile.rsldTeilSong.Minimum = 0;
                klZeile.audioZeile.rsldTeilSong.Maximum = klZeile.audiotitel.Audio_Titel.Länge > 0 ? klZeile.audiotitel.Audio_Titel.Länge.Value : 10000;
                klZeile.audioZeile.rsldTeilSong.UpdateLayout();

                klZeile.audioZeile.rsldTeilSong.LowerValue = klZeile.audiotitel.TeilStart.Value;
                klZeile.audioZeile.rsldTeilSong.UpperValue = klZeile.audiotitel.TeilEnde.Value;
                klZeile.audioZeile.rsldTeilSong.brdLine.Margin = new Thickness(
                    (klZeile.audioZeile.rsldTeilSong.ActualWidth - 17) / (klZeile.audioZeile.rsldTeilSong.LowerSlider.Maximum / klZeile.audioZeile.rsldTeilSong.LowerSlider.Value), 0,
                    klZeile.audioZeile.rsldTeilSong.ActualWidth - 17 - (klZeile.audioZeile.rsldTeilSong.ActualWidth - 17) / (klZeile.audioZeile.rsldTeilSong.LowerSlider.Maximum / klZeile.audioZeile.rsldTeilSong.UpperSlider.Value), 0);
            }
            if (playlisttitel.VolumeChange) grpobj.anzVolChange++;
            if (playlisttitel.PauseChange) grpobj.anzPauseChange++;
            rowErstellt++;
        }

        private void abspielProzess(GruppenObjekt grpObj, bool checkStatus, bool sollStandby, KlangZeile klZeile, RoutedEventArgs e)
        {
            Audio_Titel aTitel = setTitelStdPfad(klZeile.audiotitel.Audio_Titel);
            if (aTitel.Pfad != klZeile.audiotitel.Audio_Titel.Pfad ||
                aTitel.Datei != klZeile.audiotitel.Audio_Titel.Datei)
            {
                klZeile.audiotitel.Audio_Titel = aTitel;
                Global.ContextAudio.Update<Audio_Titel>(klZeile.audiotitel.Audio_Titel);
            }
            try
            {
                if (e == null || e.Source == null)
                {
                    if (!Directory.Exists(klZeile.audiotitel.Audio_Titel.Pfad) ||
                        !File.Exists(klZeile.audiotitel.Audio_Titel.Pfad + "\\" + klZeile.audiotitel.Audio_Titel.Datei))
                    {
                        if (grpObj.visuell)
                        {
                            klZeile.audioZeile.lbiEditorRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));       // Red
                            klZeile.audioZeile.lbiEditorRow.ToolTip = "Datei nicht gefunden. -> " +
                            klZeile.audiotitel.Audio_Titel.Pfad + "\\" + klZeile.audiotitel.Audio_Titel.Datei;
                        }
                        klZeile.playable = false;
                        klZeile.istLaufend = false;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.audiotitel.Audio_TitelGUID));
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
                                {
                                    klZeile.FadingOutStarted = false;
                                    FadingOut_Started = null;
                                }
                                //if (!grpObj.wirdAbgespielt) ;
                                //    grpObj.wirdAbgespielt = true;

                                klZeile._mplayer =
                                    PlayFile(true, klZeile, GetPosObjGruppe(grpObj.objGruppe), klZeile._mplayer,
                                        klZeile.audiotitel.Audio_Titel.Pfad + "\\" + klZeile.audiotitel.Audio_Titel.Datei,
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

                            CheckPlayStandbySongs(grpObj);
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
                        if (klZeile._mplayer != null)
                        {
                            klZeile._mplayer.Stop();
                            if (!klZeile.audiotitel.TeilAbspielen)
                                klZeile._mplayer.Close();
                        }
                        klZeile.istLaufend = false;
                        klZeile.istPause = false;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.audiotitel.Audio_TitelGUID));
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Der AbspielProzess konnte nicht ordnungsgemäß durchgeführt werden.", ex);
            }
        }

        private void chkTitel0_0_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte.FindAll(t => t.visuell))
                {
                    if (chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkTitel == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;

                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkTitel == (Control)sender));

                if (e != null && e.Source != null)
                {
                    if (!(sender as CheckBox).IsChecked.Value && grpobj._listZeile[zeile].istLaufend)
                    {
                        grpobj._listZeile[zeile]._mplayer.Pause();
                        grpobj._listZeile[zeile]._mplayer.Position = TimeSpan.FromMilliseconds(0);
                        grpobj._listZeile[zeile].istLaufend = false;

                    }
                }
                if ((grpobj.visuell && grpobj.wirdAbgespielt || !grpobj.visuell) ||
                    (grpobj.visuell && !grpobj.wirdAbgespielt && grpobj.tbtnKlangPause.IsChecked.Value))
                    abspielProzess(grpobj, (sender as CheckBox).IsChecked.Value, grpobj.wirdAbgespielt, grpobj._listZeile[zeile], e);

                if (grpobj.aPlaylist != null)
                {
                    Audio_Playlist_Titel playlisttitel =
                        grpobj.aPlaylist.Audio_Playlist_Titel.Where(t => t.Audio_TitelGUID ==
                            grpobj._listZeile[zeile].audiotitel.Audio_TitelGUID).FirstOrDefault(
                            t => t.Aktiv != (sender as CheckBox).IsChecked.Value);

                    if (playlisttitel != null)
                        playlisttitel.Aktiv = (sender as CheckBox).IsChecked.Value;

                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e != null && e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
                }
                if (grpobj.visuell)
                {
                    CheckAlleAngehakt(grpobj);
                    if (grpobj.wirdAbgespielt) CheckPlayStandbySongs(grpobj);
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Auswahlfehler" + Environment.NewLine + "Beim der Prozedure 'chkTitel' ist ein Fehler aufgetreten", ex);
            }
        }

        public void SetzeChangeBit(Audio_Playlist playlist_bezug)
        {
            foreach (GruppenObjekt grpObj in _GrpObjecte.FindAll(t => t.aPlaylist.Audio_PlaylistGUID == playlist_bezug.Audio_PlaylistGUID)
                    .FindAll(t => !t.visuell))
                grpObj.changed = true;

        }

        private Audio_Playlist NeueKlangPlaylistInDB(string playlistname)
        {

            Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
            playlist.MaxSongsParallel = 1;
            playlist.Name = playlistname;
            playlist.Hintergrundmusik = false;
            playlist.Repeat = btnEditRepeat.IsChecked.Value;
            playlist.Shuffle = btnEditShuffle.IsChecked.Value;

            //zur datenbank hinzufügen
            if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                AktKlangPlaylist = playlist;

            AktualisiereEditorPlaylist();
            return playlist;
        }

        private void NeueKlangThemeInDB(string titel)
        {
            string themeName = GetNeuenNamen(titel == "" ? "Neues Theme" : titel, 1);
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
                    AktualisiereEditorThemes();
                    for (int i = 0; i <= lbEditorTheme.Items.Count - 1; i++)
                        if ((lbEditorTheme.Items[i] as ListboxItemIcon).lbText.Content.ToString() == theme.Name)
                            lbEditorTheme.SelectedIndex = i;
                    lbEditorTheme.ScrollIntoView(lbEditorTheme.SelectedItem);
                }
            }
            else
            {
                ViewHelper.ShowError("Datenbankfehler" + Environment.NewLine + "Theme evtl. schon vorhanden. Bitte wiederholen Sie den Vorgang und wählen einen anderen Titel", new Exception());
            }
        }

        private bool KlangDateiHinzu(string pfad_datei, AudioZeile aZeile, Audio_Playlist aPlaylist, int position)
        {
            bool titelGuidInPlaylist = false;
            bool titelNeuHinzugefügt = false;
            bool found = false;
            string pfad_max = System.IO.Path.GetDirectoryName(pfad_datei);
            string[] temp_pfad_datei = new string[] { pfad_max, System.IO.Path.GetFileName(pfad_datei) };

            foreach (string pfad in stdPfad)
            {
                if (pfad_max.ToLower().StartsWith(pfad.ToLower()))
                {
                    if (pfad_max != pfad)
                        temp_pfad_datei[1] = pfad_max.Substring(pfad.Length + 1) + "\\" + temp_pfad_datei[1];
                    temp_pfad_datei[0] = pfad_max.Substring(0, pfad.Length);
                    found = true;
                }
            }

            if (!found && temp_pfad_datei[0].Length <= 200)
            {
                // Pfad noch kein Standard-Pfad -> Neuer Standard-Pfad wird so groß als möglich!
                if (ViewHelper.Confirm("Audio-Pfad ist kein Standard-Pfad", "Der Pfad der Audio-Datei konnte nicht unter den Standard-Pfaden gefunden werden." +
                    Environment.NewLine + "In dieser Konstellation ist es nicht zulässig, den Titel abzuspielen." + Environment.NewLine +
                    "Soll der Pfad mit in die Standard-Pfade integriert werden?"))
                {
                    MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis =
                        MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis + "|" + temp_pfad_datei[0];
                    setStdPfad();
                }
                else
                    return false;
            }
            Audio_Titel titel = Global.ContextAudio.TitelListe.FindAll(t => t.Pfad == temp_pfad_datei[0]).FirstOrDefault(tt => tt.Datei == temp_pfad_datei[1]);

            if (titel != null && aPlaylist != null)
                titelGuidInPlaylist = aPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == titel.Audio_TitelGUID) == null ? false : true;

            int confYNC = -1;

            if (aZeile != null && aZeile.chkTitel.ContextMenu.IsVisible)
                confYNC = 2;
            else
                if (titel != null && titelGuidInPlaylist && Keyboard.Modifiers == ModifierKeys.Control)
                    confYNC = ViewHelper.ConfirmYesNoCancel("Titel schon vorhanden", "Den Titel '" + titel.Name + "' gibt es schon in der Playliste." +
                        Environment.NewLine + Environment.NewLine + "Soll der Titel mehrfach in der Playliste aufgeführt werden?");

            if (titel == null || confYNC == 2)          //nicht in DB oder Neuen Titel in die Playliste ODER Verschieben
            {
                titelNeuHinzugefügt = true;
                titel = Global.ContextAudio.New<Audio_Titel>();     //erstelle ein leeres Titel-Objekt

                //eigenschaften setzen
                string s = System.IO.Path.GetFileNameWithoutExtension(temp_pfad_datei[1]);
                titel.Name = s.Length > 100 ? s.Substring(0, 99) : s;

                titel.Pfad = temp_pfad_datei[0];
                if (titel.Pfad.Length > 200)
                {
                    ViewHelper.ShowError("Dateistruktur zu groß" + Environment.NewLine + "Die Datei '" + titel.Pfad + "\\" + titel.Datei +
                        "' kann nicht interiert werden, da der Pfad zu komplex ist (Länge)." + Environment.NewLine + Environment.NewLine +
                        "Bitte kopieren Sie die Datei in einen weniger komplexen Bereich.", new Exception());
                    titel.Pfad = titel.Pfad.Substring(0, 199);
                }
                titel.Datei = temp_pfad_datei[1];

                //zur datenbank hinzufügen
                Global.ContextAudio.Insert<Audio_Titel>(titel);
            }
            if (titel != null && !titelGuidInPlaylist)
                titelNeuHinzugefügt = true;

            if (titelNeuHinzugefügt)
            {
                Global.ContextAudio.AddTitelToPlaylist(aPlaylist, titel);

                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                if (aPlaylist == AktKlangPlaylist)
                    grpobj.tbTopKlangSongsParallel.Tag = grpobj._listZeile.Count + 1;

                Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(aPlaylist, titel).Last();
                if (playlisttitel != null)
                {
                    //AudioZeile per Drag&Drop hinzugefügt
                    if (aZeile != null)
                    {
                        Audio_Playlist_Titel aplaytitelDnD = AktKlangPlaylist.Audio_Playlist_Titel.First(t => t.Audio_TitelGUID == (Guid)aZeile.Tag);
                        playlisttitel.Audio_Titel.Länge = aplaytitelDnD.Audio_Titel.Länge;
                        playlisttitel.Aktiv = aplaytitelDnD.Aktiv;
                        playlisttitel.Länge = aplaytitelDnD.Länge;
                        playlisttitel.Pause = aplaytitelDnD.Pause;
                        playlisttitel.PauseChange = aplaytitelDnD.PauseChange;
                        playlisttitel.PauseMax = aplaytitelDnD.PauseMax;
                        playlisttitel.PauseMin = aplaytitelDnD.PauseMin;
                        playlisttitel.Rating = aplaytitelDnD.Rating;
                        playlisttitel.Speed = aplaytitelDnD.Speed;
                        playlisttitel.TeilAbspielen = aplaytitelDnD.TeilAbspielen;
                        playlisttitel.TeilEnde = aplaytitelDnD.TeilEnde;
                        playlisttitel.TeilStart = aplaytitelDnD.TeilStart;
                        playlisttitel.Volume = aplaytitelDnD.Volume;
                        playlisttitel.VolumeChange = aplaytitelDnD.VolumeChange;
                        playlisttitel.VolumeMax = aplaytitelDnD.VolumeMax;
                        playlisttitel.VolumeMin = aplaytitelDnD.VolumeMin;
                        playlisttitel.Reihenfolge = position;
                    }
                    else
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
                        playlisttitel.Reihenfolge = AktKlangPlaylist.Audio_Playlist_Titel.Count - 1;
                    }

                    if (aPlaylist == AktKlangPlaylist)
                    {
                        KlangNewRow(pfad_datei, grpobj, Convert.ToUInt16(grpobj._listZeile.Count), playlisttitel);

                        if (playlisttitel.Aktiv)
                        {
                            for (int i = 0; i <= playlisttitel.Rating; i++)
                                grpobj.NochZuSpielen.Add(grpobj._listZeile[grpobj._listZeile.Count - 1].audiotitel.Audio_TitelGUID);
                        }

                        if (AktKlangPlaylist.Hintergrundmusik)
                            ZeigeZeileKlangSpalten(grpobj, false);
                    }

                    Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel);
                }

                if (aPlaylist == AktKlangPlaylist && AktKlangPlaylist.MaxSongsParallel == 0)
                {
                    grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;
                    grpobj.tbTopKlangSongsParallel.Text = "1";  //ruft TextChange-Ereibnis auf, und aktualisiert die Playlist
                }
            }
            return titelNeuHinzugefügt;
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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Drag-Mode über den Editor ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnTopKlangOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string bezugsDir = stdPfad[0];
                if (AktKlangPlaylist != null)
                {
                    List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
                    if (titelliste.Count > 0)
                    {
                        titelliste[0] = setTitelStdPfad(titelliste[0]);

                        bezugsDir = (titelliste[0].Pfad + titelliste[0].Datei).LastIndexOf(@"\") != -1 ?
                            (titelliste[0].Pfad + titelliste[0].Datei).Substring(0, titelliste[0].Pfad.LastIndexOf(@"\")) :
                            titelliste[0].Pfad + titelliste[0].Datei;

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
                
                List<string> files = ViewHelper.ChooseFiles("Musiktitel auswählen", "", true, validExt);//new String[6] { "mp3", "wav", "wma", "ogg", "m3u8", "wpl" });

                // Öffnen bestätigt
                if (files != null && files.Count > 0)
                {
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        _DateienAufnehmen(files, null, AktKlangPlaylist, 0, false);
                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                        tcEditor_SelectionChanged(grdEditorListe, null);
                        CheckBtnGleicherPfad(_GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell));
                        lbEditor.ScrollIntoView(lbEditor.SelectedItem);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Beim Einfügen der Datei(en) ist ein Fehler aufgetreten.", ex);
            }
        }

        private List<string> getWinampFilesFromPlaylist(string filename)
        {
            List<string> lstFiles = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        if (line.Substring(1, 2) == ":\\")
                            lstFiles.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Lesefehler" + Environment.NewLine + "Beim Auslesen der Playliste '" + filename + "' ist ein Fehler aufgetreten.", ex);
            }
            return lstFiles;
        }

        private List<string> getMPlayerFilesFromPlaylist(string filename)
        {
            List<string> lstFiles = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine().Trim();

                        if (line.StartsWith("<media src="))
                        {
                            line = line.Substring(line.IndexOf("\"") + 1);
                            line = line.Substring(0, line.IndexOf("\""));
                            lstFiles.Add(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Lesefehler" + Environment.NewLine + "Beim Auslesen der Playliste '" + filename + "' ist ein Fehler aufgetreten.", ex);
            }

            return lstFiles;
        }


        private void _DateienAufnehmen(List<string> files, AudioZeile aZeile, Audio_Playlist aPlaylist, int position, bool jetztUpdaten)
        {
            string[] extension = new String[4] { ".mp3", ".wav", ".wma", ".ogg" };
            bool hinzugefuegt = false;

            if (aPlaylist == null)
                aPlaylist = NeueKlangPlaylistInDB(tboxEditorName.Text);

            foreach (string dateihinzu in files)
            {
                if (Array.IndexOf(extension, dateihinzu.ToLower().Substring(dateihinzu.Length - 4)) != -1)
                {
                    KlangDateiHinzu(dateihinzu, aZeile, aPlaylist, position);
                    hinzugefuegt = true;
                }
                else
                {
                    if (dateihinzu.ToLower().EndsWith(".m3u8"))
                    {
                        _DateienAufnehmen(getWinampFilesFromPlaylist(dateihinzu), null, aPlaylist, 0, true);
                        hinzugefuegt = true;
                    }
                    else
                    {
                        if (dateihinzu.ToLower().EndsWith(".wpl"))
                        {
                            _DateienAufnehmen(getMPlayerFilesFromPlaylist(dateihinzu), null, aPlaylist, 0, true);
                            hinzugefuegt = true;
                        }
                    }
                }
            }
            if (hinzugefuegt)
            {
                if (aPlaylist == AktKlangPlaylist)
                {
                    if (lbEditor.SelectedIndex == -1)
                    {
                        for (int i = 0; i < lbEditor.Items.Count; i++)
                            if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == aPlaylist.Audio_PlaylistGUID)
                                lbEditor.SelectedIndex = i;
                    }

                    GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
                    if (grpobj == null)
                        return;

                    if (grpobj.rbTopIstKlangPlaylist.IsChecked == true)
                        aPlaylist.Hintergrundmusik = false;
                    else
                        aPlaylist.Hintergrundmusik = true;

                    if (jetztUpdaten)
                        Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);

                    if (aPlaylist.Hintergrundmusik)
                    {
                        ZeigeKlangSongsParallel(grpobj, false);
                        //ZeigeKlangTop(grpobj, false);
                    }
                    else
                    {
                        ZeigeKlangSongsParallel(grpobj, true);
                        //ZeigeKlangTop(grpobj, true);
                    }

                    CheckAlleAngehakt(grpobj);
                    grpobj.playlistName = aPlaylist.Name;
                    grpobj.grdEditor.Visibility = Visibility.Visible;
                    grpobj.aPlaylist = aPlaylist;
                }
                else
                    if (jetztUpdaten)
                        Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);
            }
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
                List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.FindAll(t => t.Audio_PlaylistGUID.Equals(AktPlaylist.Audio_PlaylistGUID)).ToList();
                if (playlistliste.Count == 0)
                {
                    Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                    playlist.Name = NeuerPlaylistName;
                    playlist.Hintergrundmusik = false;
                    playlist.MaxSongsParallel = 1;

                    //zur datenbank hinzufügen
                    if (!Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                        ViewHelper.ShowError("Datenbank-Fehler" + Environment.NewLine + "Das hinzufügen der Playlist in die Datenbank ist fehlgeschlagen.", new Exception());
                }
                else
                {
                    playlistliste[0].Name = NeuerPlaylistName;
                    Global.ContextAudio.Update<Audio_Playlist>(playlistliste[0]);
                }
            }
            return AktPlaylist;
        }

        private void tboxTopKategorieX_LostFocus(object sender, RoutedEventArgs e)
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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Verlassen des Kategorie-Feldes ist ein Fehler aufgetreten.", ex);
            }
        }

        private void tboxThemeKategorie_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AktKlangTheme != null)
                {
                    AktKlangTheme.Kategorie = tboxThemeKategorie.Text;
                    Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                }

                tbLostFocus(null, null);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Verlassen des Kategorie-Feldes ist ein Fehler aufgetreten.", ex);
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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ändern der aktiven Hotkey-Geräuschen ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnTopHotkeySet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Button)sender).Visibility = Visibility.Collapsed;

                _GrpObjecte.FirstOrDefault(t => t.visuell).cmboxTopHotkey.Visibility = Visibility.Visible;
                _GrpObjecte.FirstOrDefault(t => t.visuell).cmboxTopHotkey.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Setzen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
            }
        }
        private void cmboxTopHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // a = 65
                int istkey = Convert.ToInt32(e.Key) + 21;
                if (e.Key >= Key.D0 && e.Key <= Key.D9)
                    istkey = Convert.ToInt32(Convert.ToString(e.Key).Remove(0, 1)) + 48;

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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Drücken der Auswahltaste im Combobox-Feld ist ein Fehler aufgetreten.", ex);
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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Öffnen der Dropdown-Liste ist ein Fehler aufgetreten.", ex);
            }
        }

        private void cmboxTopHotkey_DropDownClosed(object sender, EventArgs e)
        {
            ((ComboBox)sender).Visibility = Visibility.Collapsed;
            GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
            grpobj.btnHotkeyEntfernen.Visibility = Visibility.Visible;
            grpobj.btnTopHotkeySet.Visibility = Visibility.Visible;
            notPlayHotkey = false;
            ((ListboxItemIcon)lbEditor.SelectedItem).Focus();
        }

        private void cmboxTopHotkey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist == null)
                    return;
                if (((ComboBox)sender).SelectedIndex != -1)
                {
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Wechseln der Hotkey-Taste ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnHotkeyStop_Click(object sender, RoutedEventArgs e)
        {
            hotkeys.FindAll(t => t.mp != null).ForEach(delegate(hotkey hkey)
            {
                hkey.mp.Stop();
            });
        }

        private void btnHotkeyEntfernenX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                hotkey hkey = hotkeys.FirstOrDefault(t => t.aPlaylistGuid == grpobj.aPlaylist.Audio_PlaylistGUID);
                if (hkey == null)
                    return;

                foreach (Button btnHotKey in spnlHotkeys.Children.OfType<Button>())
                {
                    if (((hotkey)btnHotKey.Tag).taste == hkey.taste)
                    {
                        spnlHotkeys.Children.Remove(btnHotKey);
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
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Entfernen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
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

                    int zuspielen = (new Random()).Next(0, aPlaylist.Audio_Playlist_Titel.Count - 1);

                    Audio_Titel aTitel = aPlaylist.Audio_Playlist_Titel.ToList().ElementAt(zuspielen).Audio_Titel;

                    hkey.mp.MediaEnded += mp_failed_ended;
                    hkey.mp.MediaFailed += mp_failed_ended;
                    hkey.mp.Open(new Uri(aTitel.Pfad + "\\" + aTitel.Datei));
                    if (hkey.mp.Volume != slHotkey.Value / 100)
                        hkey.mp.Volume = slHotkey.Value / 100; // Slider des PListModifikator
                    hkey.mp.Play();
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswählen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
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
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Stoppen und Schließen des Media-Player nach einem Fehler ist ein Fehler aufgetreten.", ex);
            }
        }

        private void AktualisiereMusikPlaylist()
        {
            UInt16 pos = 0;
            foreach (Audio_Playlist plyList in Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik).OrderBy(t => t.Name))
            {
                List<Audio_Titel> s = Global.ContextAudio.LoadTitelByPlaylist(plyList);

                if (pos + 1 > lbMusik.Items.Count)                         // Zeile noch nicht vorhanden
                {
                    MusikZeile mZeile = new MusikZeile();
                    mZeile.Cursor = Cursors.Hand;
                    mZeile.Tag = plyList.Audio_PlaylistGUID;
                    mZeile.tblkTitel.Text = plyList.Name;
                    mZeile.tblkLänge.Text = (plyList.Länge != 0) ? TimeSpan.FromMilliseconds(plyList.Länge).ToString(@"hh\:mm\:ss") : "";
                    mZeile.tboxKategorie.Tag = mZeile.Tag;
                    mZeile.tboxKategorie.Text = plyList.Kategorie;
                    mZeile.tboxKategorie.GotFocus += tbGotFocus;
                    mZeile.tboxKategorie.LostFocus += tboxTopKategorieX_LostFocus;

                    lbMusik.Items.Add(mZeile);
                }
                else                                                            // Abändern der Zeile
                {
                    ((MusikZeile)(lbMusik.Items[pos])).Tag = plyList.Audio_PlaylistGUID;
                    if (((MusikZeile)(lbMusik.Items[pos])).tblkTitel.Text != plyList.Name)
                        ((MusikZeile)(lbMusik.Items[pos])).tblkTitel.Text = plyList.Name;
                    if (((MusikZeile)(lbMusik.Items[pos])).tboxKategorie.Text != plyList.Kategorie)
                        ((MusikZeile)(lbMusik.Items[pos])).tboxKategorie.Text = plyList.Kategorie;
                    if (((MusikZeile)(lbMusik.Items[pos])).tblkLänge.Text != ((plyList.Länge != 0) ? TimeSpan.FromMilliseconds(plyList.Länge).ToString(@"hh\:mm\:ss") : ""))
                        ((MusikZeile)(lbMusik.Items[pos])).tblkLänge.Text = (plyList.Länge != 0) ? TimeSpan.FromMilliseconds(plyList.Länge).ToString(@"hh\:mm\:ss") : "";
                }
                pos++;
            }
            if (lbMusik.Items.Count != 0)
            {
                while (Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik).Count > lbMusik.Items.Count)
                    lbMusik.Items.RemoveAt(lbMusik.Items.Count - 1);
            }
        }

        private void AktualisiereEditorPlaylist()
        {
            int cnt = 0;
            bool neu = false;
            bool sollVis;
            foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe.OrderBy(t => t.Name))
            {
                sollVis = true;
                cnt++;
                neu = lbEditor.Items.Count < cnt;
                ListboxItemIcon lbitem = neu ? new ListboxItemIcon() : (ListboxItemIcon)lbEditor.Items.GetItemAt(cnt - 1);

                //Im Theme-Editor 
                if (rbEditorEditTheme.IsChecked.Value)
                {
                    lbitem._animateOnMouseEvent = false;

                    //alle Playlists verstecken die im Theme verwendet werden
                    if (AktKlangTheme != null &&
                        AktKlangTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID) != null)
                        sollVis = false;
                }
                else
                    // Im Playlist-Editor
                    lbitem._animateOnMouseEvent = true;

                lbitem.Tag = aPlaylist.Audio_PlaylistGUID;
                lbitem.lbText.Content = aPlaylist.Name;
                if ((aPlaylist.Hintergrundmusik) && rbEditorMusik.IsChecked == true)
                {
                    lbitem.imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png"));
                    lbitem.imgIcon.ToolTip = "Musik-Playlist";
                }
                else
                    if ((!aPlaylist.Hintergrundmusik) && rbEditorKlang.IsChecked == true)
                    {
                        lbitem.imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
                        lbitem.imgIcon.ToolTip = "Geräusche-Playlist";
                    }
                    else
                        sollVis = false;
                lbitem.Visibility = sollVis ? Visibility.Visible : Visibility.Collapsed;

                lbitem.ToolTip = rbEditorEditPList.IsChecked.Value ?
                    "Playliste zum Editieren auswählen" + Environment.NewLine + "MusikZeilen mit gedrückter Strg-Taste per Drag&Drop einfügen" :
                    "Playliste per Drag&Drop in das Theme einfügen";

                if (neu)
                {
                    lbEditor.Items.Add(lbitem);
                    lbitem.AllowDrop = true;
                    lbitem.Drop += new DragEventHandler(lbitemEditor_Drop);
                    //lbitem.GiveFeedback += audiozeile_GiveFeedback;
                    lbitem.btnExport.Click += new RoutedEventHandler(lbItembtnExportPlaylist_Click);
                    lbitem.btnLöschen.Click += new RoutedEventHandler(lbItembtnLöschenPlaylist_Click);
                }

                lbitem.imgIcon.Source = (aPlaylist.Hintergrundmusik) ?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png")) :
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
            }
            while (lbEditor.Items.Count > cnt)
                lbEditor.Items.RemoveAt(cnt == 0 ? cnt : cnt - 1);
        }

        private void AktualisiereEditorThemes()
        {
            List<Audio_Theme> aThemes = Global.ContextAudio.ThemeListe;
            int posistionThemeItem = 0;
            foreach (Audio_Theme aTheme in aThemes.FindAll(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                ListboxItemIcon lbitem = null;
                if (lbEditorTheme.Items.Count <= posistionThemeItem)
                    lbitem = new ListboxItemIcon();
                else
                    lbitem = (ListboxItemIcon)lbEditorTheme.Items[posistionThemeItem];

                lbitem.Tag = aTheme.Audio_ThemeGUID;
                lbitem.lbText.Content = aTheme.Name;
                tboxThemeKategorie.Text = aTheme.Kategorie;
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
                lbEditorTheme.Items.RemoveAt(lbEditorTheme.Items.Count - 1);
        }

        private void tbThemeThemeNormSize_Click(object sender, RoutedEventArgs e)
        {
            imgThemeThemeNorm.Visibility = tbThemeThemeNormSize.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
            AktualisierePlaylistThemes();
        }

        private void tbThemePListNormSize_Click(object sender, RoutedEventArgs e)
        {
            imgThemePListNorm.Visibility = tbThemePListNormSize.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
            AktualisierePListPlaylist();
        }

        private void AktualisierePlaylistThemes()
        {
            List<Audio_Theme> aThemes = Global.ContextAudio.ThemeListe;
            int cnt = 0;
            bool neu;
            wpnlPListThemes.ItemHeight = tbThemeThemeNormSize.IsChecked.Value ? 25 : 50;
            foreach (Audio_Theme aTheme in aThemes.FindAll(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                if (!chkPListFilter(tbThemesFilterAll.Text, aTheme.Name) && (aTheme.Kategorie == null ? true : !chkPListFilter(tbThemesFilterAll.Text, aTheme.Kategorie)))
                    continue;
                cnt++;
                neu = wpnlPListThemes.Children.Count < cnt;

                grdThemeButton grdThButton = neu ? new grdThemeButton() : (grdThemeButton)wpnlPListThemes.Children[cnt - 1];
                grdThButton.Tag = aTheme.Audio_ThemeGUID;
                grdThButton.Height = tbThemeThemeNormSize.IsChecked.Value ? 22 : 42;
                grdThButton.tbtnTheme.Tag = aTheme.Audio_ThemeGUID;
                grdThButton.lblTheme.Content = aTheme.Name;
                grdThButton.brdKategorie.Visibility = (aTheme.Kategorie != null && aTheme.Kategorie != "" && !tbThemeThemeNormSize.IsChecked.Value) ?
                    Visibility.Visible : Visibility.Collapsed;
                grdThButton.lblKategorie.Content = aTheme.Kategorie;
                grdThButton.chkbxPlus.Visibility = tbThemeThemeNormSize.IsChecked.Value ? Visibility.Hidden : Visibility.Visible;
                grdThButton.chkbxPlus.Tag = aTheme;
                grdThButton.chkbxPlus.IsChecked = aTheme.NurGeräusche;

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
                grdThButton.tbtnTheme.ToolTip = ttip;

                if (neu)
                {
                    grdThButton.tbtnTheme.Checked += tbtnTheme_Checked;
                    grdThButton.tbtnTheme.Unchecked += tbtnTheme_UnChecked;
                    grdThButton.chkbxPlus.Checked += chbxThemeNurGeräusche_Checked;
                    grdThButton.chkbxPlus.Unchecked += chbxThemeNurGeräusche_Checked;
                    wpnlPListThemes.Children.Add(grdThButton);
                }
            }
            wpnlPListThemes.Children.RemoveRange(cnt, wpnlPListThemes.Children.Count - cnt);
        }

        private void chbxThemeNurGeräusche_Checked(object sender, RoutedEventArgs e)
        {
            ((Audio_Theme)((CheckBox)sender).Tag).NurGeräusche = ((CheckBox)sender).IsChecked.Value;
            Global.ContextAudio.Update<Audio_Theme>(((Audio_Theme)((CheckBox)sender).Tag));
        }

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
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Abwählen des Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnThemeGeräuscheAus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                    mZeile.tbtnCheck.IsChecked = false;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Abwählen der Geräusche-Playlisten ist ein Fehler aufgetreten.", ex);
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

        private void btnNeuePlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string NeuePlaylist = GetNeuenNamen("NeuePlayliste", 0);
                
                rbEditorKlang_Click(rbEditorKlang, null);
                Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                playlist.Name = NeuePlaylist.ToString();
                if (rbEditorKlang.IsChecked.Value)
                    playlist.Hintergrundmusik = false;
                else
                    playlist.Hintergrundmusik = true;

                playlist.Repeat = btnEditRepeat.IsChecked.Value;
                playlist.Shuffle = btnEditShuffle.IsChecked.Value;

                //zur datenbank hinzufügen
                if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                {
                    AktKlangPlaylist = playlist;
                    playlist.MaxSongsParallel = 1;

                    AktualisiereEditorPlaylist();
                    lbEditor.SelectedIndex = -1;
                    for (int i = 0; i <= lbEditor.Items.Count - 1; i++)
                        if ((lbEditor.Items[i] as ListboxItemIcon).lbText.Content.ToString() == playlist.Name)
                            lbEditor.SelectedIndex = i;
                }
                lbEditor.ScrollIntoView(lbEditor.SelectedItem);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Erstellen einer neuen Playlist ist ein Fehler aufgetreten.", ex);
            }
        }

        private void tiMusik_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbMusik.Items.Count == 0)
                    AktualisiereMusikPlaylist();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Wechseln auf das TabItem ist ein Fehler aufgetreten.", ex);
            }
        }

        private void lbMusiktitellist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((lbMusiktitellist.SelectedIndex >= 0) &&
                   (((ListBoxItem)lbMusiktitellist.SelectedItem).Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString()))         // Red))
                {
                    if (lbMusik.SelectedIndex == -1)
                    {
                        lbMusik.SelectionChanged -= lbMusik_SelectionChanged;
                        lbMusik.SelectedIndex = Convert.ToInt16(lbMusik.Tag);
                        lbMusik.SelectionChanged += lbMusik_SelectionChanged;
                    }
                    if (e != null && e.RemovedItems.Count == 0)
                        RenewMusikNochZuSpielen();
                    chkbxPlayRange.IsChecked = false;
                    rsldTeilSong.Visibility = Visibility.Hidden;
                    rsldTeilSong.LowerValue = 0;
                    rsldTeilSong.UpperValue = 100000;

                    ListBoxItem lbItem = (ListBoxItem)lbMusiktitellist.SelectedItem;

                    Audio_Titel titel = null;
                    Audio_Playlist_Titel aPlayListtitel = _BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)lbItem.Tag);
                    if (aPlayListtitel != null)
                        titel = _BGPlayer.AktTitel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)lbItem.Tag);

                    if (titel == null)
                    {
                        lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                        lbItem.ToolTip = "Keine Titel-Informationen gefunden";

                        lbMusik_SelectionChanged(lbMusiktitellist, e);
                        lbMusiktitellist.Tag = -1;
                        btnBGPrev.IsEnabled = false;
                    }
                    else
                    {
                        if (!File.Exists(titel.Pfad + "\\" + titel.Datei))
                        {
                            titel = setTitelStdPfad(titel);
                            if (File.Exists(titel.Pfad + "\\" + titel.Datei))
                                Global.ContextAudio.Update<Audio_Titel>(titel);
                        }

                        if (Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei) ||
                            !Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)))
                        {
                            lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                            lbItem.ToolTip = "Datei nicht gefunden. -> " + titel.Pfad + "\\" + titel.Datei;
                            lbMusiktitellist.Tag = -1;
                            btnBGPrev.IsEnabled = false;
                            SpieleNeuenMusikTitel(Guid.Empty);
                        }
                        else
                        {
                            if (Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)) &&
                                File.Exists(titel.Pfad + "\\" + titel.Datei))
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
                                    if (lbMusiktitellist.SelectedIndex != -1)
                                    {
                                        if (!_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted && lbMusiktitellist.SelectedIndex != -1)
                                        {
                                            _BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
                                            BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], false, false);
                                        }
                                        _BGPlayer.aktiv = (_BGPlayer.aktiv == 0) ? 1 : 0;
                                    }
                                }
                                _BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;

                                _BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals((Guid)lbItem.Tag));
                                _BGPlayer.BG[_BGPlayer.aktiv].mPlayer =
                                    PlayFile(false, null, -1, _BGPlayer.BG[_BGPlayer.aktiv].mPlayer, titel.Pfad + "\\" + titel.Datei, slBGVolume.Value, true);

                                btnBGPrev.IsEnabled = true;
                                btnBGStoppen.IsEnabled = true;

                                if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                                {
                                    btnBGAbspielen.Tag = true;
                                    imgbtnBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
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
                                    btnBGAbspielen.IsEnabled = true;
                                    starsUpdate();
                                    grdSongInfo.Visibility = Visibility.Visible;

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
                else
                {
                    btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Nach Auswählen ist ein unvorhergesehner Fehler aufgetreten", ex);
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



        private void UpdatePlaylistLänge(Audio_Playlist aPlaylist)
        {
            double gesamt = 0;
            foreach (Audio_Playlist_Titel aPlyTitel in aPlaylist.Audio_Playlist_Titel)
                gesamt += aPlyTitel.Audio_Titel.Länge != null ? aPlyTitel.Audio_Titel.Länge.Value : 0;

            if (aPlaylist.Länge != gesamt)
            {
                aPlaylist.Länge = gesamt;
                Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);
                if (aPlaylist.Hintergrundmusik)
                    AktualisiereMusikPlaylist();
            }
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
                        SpieleNeuenMusikTitel(Guid.Empty);
                    else
                        MusikProgBarTimer.Tag = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds;
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Zyklischen Check der Hintergrundmusik ist ein Fehler aufgetreten.", ex);
            }
        }

        private void chkVolMove0_0_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    if (chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkVolMove == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;
                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkVolMove == (Control)sender));

                grpobj.anzVolChange = Convert.ToUInt16(
                    grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkVolMove.IsChecked == true).Count);

                if (grpobj.anzPauseChange == grpobj._listZeile.Count)
                    grpobj.chkbxTopVolChange.IsChecked = true;
                else
                    grpobj.chkbxTopVolChange.IsChecked = false;

                grpobj._listZeile[zeile].audiotitel.VolumeChange = ((CheckBox)sender).IsChecked.Value;
                CheckAlleAngehakt(grpobj);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Auswahlfehler" + Environment.NewLine + "Beim Anklicken der Checkbox 'chkVolMove' ist ein Fehler aufgetreten", ex);
            }
        }

        private void sldKlangPause0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    if (chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.sldKlangPause == (Slider)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;
                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.sldKlangPause == (Control)sender));

                grpobj._listZeile[zeile].audiotitel.Pause = Convert.ToInt32(Math.Round(((Slider)sender).Value));

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Verlassen des Sliders 'sldKlangPause' ist ein Fehler aufgetreten", ex);
            }
        }

        private void chkKlangPauseMove0_0_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    if (chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;
                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (Control)sender));

                grpobj.anzPauseChange = Convert.ToUInt16(
                    grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkKlangPauseMove.IsChecked == true).Count);

                grpobj._listZeile[zeile].audiotitel.PauseChange = ((CheckBox)sender).IsChecked.Value;

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);

                CheckAlleAngehakt(grpobj);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken der CheckBox 'chkKlangPauseMove' ist ein Fehler aufgetreten", ex);
            }
        }

        private void rsldTeilSong_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _BGPlayer.AktPlaylistTitel.TeilStart = rsldTeilSong.LowerValue;
                _BGPlayer.AktPlaylistTitel.TeilEnde = rsldTeilSong.UpperValue;
            }
            catch (Exception)
            {
            }
        }

        private void imgTrash0_0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (isDeleting)
                    return;
                isDeleting = true;

                KlangZeile kZeile = (KlangZeile)((Image)sender).Tag;
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);

                if (kZeile.audiotitel.Audio_Playlist == null || kZeile.audiotitel.Audio_Titel == null)     // sehr schnell geklickt - Daten schon gelöscht
                    return;

                //Stopp abspielen
                if (kZeile.audioZeile.chkTitel.IsChecked.Value && kZeile.istLaufend)
                {
                    kZeile.audioZeile.chkTitel.IsChecked = false;
                    chkTitel0_0_Click(kZeile.audioZeile.chkTitel, new RoutedEventArgs());
                }

                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.imgTrash == (Image)sender));

                grpobj.lbEditorListe.Items.Remove(kZeile.audioZeile);
                grpobj._listZeile.Remove(kZeile);

                Global.ContextAudio.RemoveTitelFromPlaylist(kZeile.audiotitel.Audio_Playlist, kZeile.audiotitel.Audio_Titel);

                sortPlaylist(grpobj.aPlaylist, zeile);
                grpobj.Gespielt.Clear();

                if (kZeile.audiotitel == null)
                    ZeigeKlangSongsParallel(grpobj, false);
                else
                    grpobj.NochZuSpielen.RemoveAll(t => t.Equals(kZeile.audiotitel.Audio_TitelGUID));

                //grpobj.lbEditorListe.Items.Remove(kZeile.audioZeile);//zeile);// .wpnl.Children.RemoveAt(zeile);
                // grpobj._listZeile.Remove(kZeile);//zeile);

                if (grpobj._listZeile.Count > 0)
                {
                    if (Convert.ToInt16(grpobj.tbTopKlangSongsParallel.Text) > grpobj._listZeile.Count)
                    {
                        grpobj.tbTopKlangSongsParallel.Tag = grpobj._listZeile.Count;
                        grpobj.tbTopKlangSongsParallel.Text = grpobj._listZeile.Count.ToString();
                    }
                }
                else
                {
                    ZeigeKlangSongsParallel(grpobj, false);
                }

                CheckBtnGleicherPfad(grpobj);
                GC.GetTotalMemory(true);
            }
            finally
            {
                isDeleting = false;
            }
            //	catch (Exception ex)
            //	{
            //		ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Löschvorgang der Zeile ist in der Prozedure 'imgTrash' ist ein Fehler aufgetreten", ex);
            //       isDeleting = false;
            //	}
        }

        private void rbEditorKlang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (grdEditorMain != null)
                {
                    int klangzeile = lbEditor.SelectedIndex;

                    string klangname = klangzeile != -1 ? ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content.ToString() : "";

                    if (((TabItem)tcAudioPlayer.SelectedItem) == tiMusik)
                        AktualisiereMusikPlaylist();
                    else
                        if (((TabItem)tcAudioPlayer.SelectedItem) == tiEditor)
                            AktualisiereEditorPlaylist();

                    tbEditorPlaylistFilter.Text = "";
                    if (klangname != "") SelektiereKlangZeile(klangname);
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswählen des Editor-Klang-Buttons ist ein Fehler aufgetreten", ex);
            }
        }

        private void exKlangPList_Expanded(object sender, RoutedEventArgs e)
        {
            grdEditorList.RowDefinitions[1].Height = new GridLength(330, GridUnitType.Star);
        }

        private void exKlangPList_Collapsed(object sender, RoutedEventArgs e)
        {
            grdEditorList.RowDefinitions[1].Height = new GridLength(330, GridUnitType.Auto);
        }

        private void rbEditorEditTheme_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                AktualisiereEditorThemes();
                AktualisiereEditorPlaylist();
                lbEditor.ToolTip = "Per Drag&&Drop die Playlisten in das Theme einbinden";
                if (lbEditor.Items.Count > 0 && !(lbEditor.Items[0] as ListboxItemIcon)._animateOnMouseEvent)
                {
                    foreach (ListboxItemIcon lbi in lbEditor.Items)
                    {
                        lbi.PreviewMouseLeftButtonDown += lbiEditorPlaylist_PreviewMouseLeftButtonDown;
                        lbi.PreviewMouseMove += lbiEditorPlaylist_MouseMove;
                        lbi._animateOnMouseEvent = false;
                    }
                }
                grdEditorList.RowDefinitions[0].Height = new GridLength(200, GridUnitType.Star);
                exKlangPList.IsExpanded = true;

                brdEditorListe.Visibility = Visibility.Collapsed;
                if (rbEditorEditTheme.Tag != null && (Boolean)rbEditorEditTheme.Tag)
                    grdEditorListe.Visibility = Visibility.Collapsed;

                rbEditorEditTheme.Tag = false;

                if (AktKlangTheme != null)
                {
                    foreach (ListboxItemIcon lbi in lbEditorTheme.Items)
                    {
                        if (AktKlangTheme.Audio_ThemeGUID == ((Guid)lbi.Tag))
                            lbi.IsSelected = true;
                    }
                }
                else
                    tboxEditorName.Text = GetNeuenNamen("Neues Theme", 1);

                lblEditorListName.Content = "Theme-Name";
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswählen des Editor-Theme-Butttons ist ein Fehler aufgetreten", ex);
            }
        }

        private void rbEditorEditPlaylist_Checked(object sender, RoutedEventArgs e)
        {
            AktualisiereEditorPlaylist();
            lbEditor.ToolTip = null;
            if (lbEditor.Items.Count > 0 && (lbEditor.Items[0] as ListboxItemIcon)._animateOnMouseEvent)
            {
                foreach (ListboxItemIcon lbi in lbEditor.Items)
                {
                    lbi.PreviewMouseLeftButtonDown -= lbiEditorPlaylist_PreviewMouseLeftButtonDown;
                    lbi.PreviewMouseMove -= lbiEditorPlaylist_MouseMove;
                    lbi._animateOnMouseEvent = true;
                }
            }
            grdEditorList.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Auto);
            exKlangPList.IsExpanded = true;
            grdEditorListe.Visibility = Visibility.Visible;
            brdEditorListe.Visibility = Visibility.Visible;
            rbEditorEditTheme.Tag = true;


            if (((RadioButton)sender).Name == "rbEditorEditPList")
                lbEditorTheme.SelectedIndex = -1;

            wpnlEditorTopThemesThemes.Children.RemoveRange(1, wpnlEditorTopThemesThemes.Children.Count);

            lblEditorListName.Content = "Playlist-Name";
            tboxEditorName.Text = lbEditor.SelectedIndex == -1 ? GetNeuenNamen("NeuePlayliste", 0) : ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content.ToString();

            //tiPlus_MouseUp(false, null); 
        }


        /// <summary>
        /// Sucht den nächsten verfügbaren Listen-Namen
        /// 0 = PlaylistListe
        /// 1 = ThemeListe
        /// </summary>
        private string GetNeuenNamen(string titel, int liste)
        {
            string NeuerName = titel;
            int ver = 0;
            if (liste == 0)
            {
                Audio_Playlist playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuerName));

                while (playlistlist != null)
                {
                    NeuerName = titel + "-" + ver;
                    ver++;
                    playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuerName));
                }
            }
            else
            {
                if (liste == 1)
                {
                    Audio_Theme themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(NeuerName));

                    while (themelist != null)
                    {
                        NeuerName = titel + "-" + ver;
                        ver++;
                        themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(NeuerName));
                    }
                }
            }
            return NeuerName;
        }

        private void rbTopIstKlangPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                grpobj.spnlTopHotkey.Visibility = Visibility.Visible;
                if (AktKlangPlaylist != null)
                {
                    if (_BGPlayer.AktPlaylist == AktKlangPlaylist && lbMusik.SelectedIndex != -1)
                    {
                        btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        lbMusik.SelectedIndex = -1;
                        lbMusik.Tag = -1;
                        lbMusiktitellist.Items.Clear();
                    }

                    grpobj.istMusik = false;
                    AktKlangPlaylist.Hintergrundmusik = false;
                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);

                    ZeigeKlangSongsParallel(grpobj, true);
                    //ZeigeKlangTop(grpobj, true);
                    ZeigeZeileKlangSpalten(grpobj, true);
                    //btnEditRepeat.Visibility = Visibility.Collapsed;
                    //btnEditShuffle.Visibility = Visibility.Collapsed;

                    for (UInt16 i = 0; i < grpobj._listZeile.Count; i++)
                    {
                        if (grpobj._listZeile[i].audioZeile.chkTitel.IsChecked == true)
                            grpobj._listZeile[i].istStandby = true;
                    }

                    //AktualisiereEditorPlaylist();

                    ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
                    ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.ToolTip = "Geräusche-Playlist";
                }
                //else
                //	ZeigeKlangTop(grpobj, true);
            }
            catch (Exception)
            {
            }
        }

        private void rbTopIstMusikPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                    AlleKlangSongsAus(grpobj, false, false, false);
                    //btnEditRepeat.Visibility = Visibility.Visible;
                    //btnEditShuffle.Visibility = Visibility.Visible;

                    if (grpobj.wirdAbgespielt)
                    {
                        grpobj.wirdAbgespielt = false;
                        ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                        ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(1, 2, 0, 0);
                    }

                    ZeigeKlangSongsParallel(grpobj, false);
                    //ZeigeKlangTop(grpobj, false);
                    ZeigeZeileKlangSpalten(grpobj, false);

                    AktKlangPlaylist.MaxSongsParallel = 1;
                    grpobj.istMusik = true;

                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    //AktualisiereEditorPlaylist();

                    ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png"));
                    ((ListboxItemIcon)lbEditor.SelectedItem).imgIcon.ToolTip = "Musik-Playlist";

                }
                //else
                //ZeigeKlangTop(grpobj, false);
            }
            catch (Exception)
            {
            }
        }

        private void tboxEditorName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    tboxEditorName.Text = tboxEditorName.Text.Replace("--", "-");
                    while (tboxEditorName.Text.EndsWith("-") || tboxEditorName.Text.EndsWith(" "))
                        tboxEditorName.Text = tboxEditorName.Text.Substring(0, tboxEditorName.Text.Length - 1);

                    if (rbEditorEditTheme.IsChecked.Value)
                    {
                        if (AktKlangTheme == null)
                        {
                            List<Audio_Theme> klTheme = Global.ContextAudio.ThemeListe.FindAll(t => t.Name.Equals(tboxEditorName.Text)).ToList();
                            if (klTheme.Count == 1)
                            {
                                AktKlangTheme = klTheme[0];
                                ((TabItem)lbEditorTheme.SelectedItem).Content = tboxEditorName.Text;
                                tboxEditorName.Text = AktKlangTheme.Name;
                            }
                            else
                                NeueKlangThemeInDB(tboxEditorName.Text);
                        }
                        AktKlangTheme.Name = tboxEditorName.Text;
                        ((ListboxItemIcon)lbEditorTheme.SelectedItem).lbText.Content = AktKlangTheme.Name;
                        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                        ((TextBox)(sender)).Background = null;
                    }
                    else
                    {
                        if (AktKlangPlaylist == null || lbEditor.SelectedItem == null)
                        {
                            List<Audio_Playlist> klPlaylist = Global.ContextAudio.PlaylistListe.FindAll(t => t.Name.Equals(tboxEditorName.Text)).ToList();
                            if (klPlaylist.Count == 1)
                            {
                                AktKlangPlaylist = klPlaylist[0];
                                tboxEditorName.Text = AktKlangPlaylist.Name;
                            }
                            else
                            {
                                NeueKlangPlaylistInDB(tboxEditorName.Text);
                                for (int i = 0; i < lbEditor.Items.Count; i++)
                                    if ((Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
                                    {
                                        lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                                        lbEditor.SelectedIndex = i;
                                        lbEditor.SelectionChanged += lbEditor_SelectionChanged;
                                    }
                            }
                        }
                        AktKlangPlaylist.Name = tboxEditorName.Text;

                        ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content = AktKlangPlaylist.Name;
                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                        ((TextBox)(sender)).Background = null;
                    }
                }
                else
                    ((TextBox)(sender)).Background = Brushes.LightSalmon;
            }
            catch (Exception) { }
        }

        private void btnKlangNeuTheme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NeueKlangThemeInDB("");
                tboxEditorName.Focus();
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
                string s = "";

                if (_GrpObjecte.Count == 0)
                    tiPlus_MouseUp(false, null);  //1. Auswahl
                else
                {
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (grpobj == null)
                        return;
                    List<Audio_Playlist> playlistliste = null;

                    s = tboxEditorName.Text;

                    if (playlistliste != null && playlistliste.Count != 0 && grpobj._listZeile.Count > 0)
                    {
                        List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]);
                        AktKlangPlaylist = playlistliste[0];

                        if (AktKlangPlaylist.Hintergrundmusik)
                            grpobj.rbTopIstMusikPlaylist.IsChecked = true;
                        else
                            grpobj.rbTopIstKlangPlaylist.IsChecked = true;

                        rbEditorFading.IsChecked = AktKlangPlaylist.Fading;

                        tboxEditorName.Text = AktKlangTheme.Name;
                        grpobj.tbTopKlangKategorie.Text = AktKlangPlaylist.Kategorie;
                        grpobj.tbTopKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;

                        if (titelliste.Count > 0)
                        {
                            grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallelX_TextChanged;
                            grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;

                            grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
                            grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallelX_TextChanged;

                            ZeigeKlangSongsParallel(grpobj, !playlistliste[0].Hintergrundmusik);
                            //ZeigeKlangTop(grpobj, !playlistliste[0].Hintergrundmusik);
                            ZeigeZeileKlangSpalten(grpobj, !playlistliste[0].Hintergrundmusik);
                        }
                    }
                    else
                    {
                        lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                        lbEditor.SelectedIndex = -1;
                        lbEditor.SelectionChanged += lbEditor_SelectionChanged;


                        //grpobj.rbTopIstKlangPlaylist.IsChecked = rbEditorKlang.IsChecked;
                        //grpobj.rbTopIstMusikPlaylist.IsChecked = rbEditorMusik.IsChecked;
                        //ZeigeKlangSongsParallel(grpobj, false);
                        tboxEditorName.Text = s;

                        grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallelX_TextChanged;
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
                        grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallelX_TextChanged;
                    }

                    SelektiereKlangZeile(s);
                }
                if (tcAudioPlayer.SelectedItem == tiEditor && lbEditorTheme.Tag == null)
                    CheckBtnGleicherPfad(_GrpObjecte.FirstOrDefault(t => t.visuell));
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
                    //grpobj.tbTopKlangKategorie.Visibility = Visibility.Visible;
                    grpobj.gboxTopTyp.Visibility = Visibility.Hidden;
                    grpobj.btnTopKlangOpen.Visibility = Visibility.Hidden;
                }
                lbEditor.SelectedIndex = -1;
            }
        }

        private void tiPlus_MouseUp(bool virtuell, MouseButtonEventArgs e)
        {
            try
            {
                tiErstellt++;
                Int16 objGruppe = Convert.ToInt16(tiErstellt);
                //AktKlangPlaylist = null;               

                GruppenObjekt grpobj = new GruppenObjekt();

                grpobj.objGruppe = tiErstellt;

                if (!virtuell)
                {
                    tboxEditorName.Text = GetNeuenNamen("NeuePlayliste", 0);

                    tbtnKlangPause1.Tag = tiErstellt;
                    chkbxTopAktiv1.Tag = objGruppe;
                    chkbxTopVolChange1.Tag = objGruppe;
                    chkbxTopPauseChange1.Tag = objGruppe;

                    grpobj.playlistName = tboxEditorName.Text;
                    grpobj.aPlaylist = new Audio_Playlist();
                    grpobj.aPlaylist.Name = tboxEditorName.Text;

                    //grpobj.sviewer = sviewer1;
                    grpobj.grdEditor = grdEditor1;
                    grpobj.grdEditorTop = grdEditorTop1;
                    //grpobj.wpnl = wpnl1;
                    grpobj.lbEditorListe = lbEditorListe;
                    grpobj.tbtnKlangPause = tbtnKlangPause1;

                    grpobj.tbTopFilter = tbEditorTopFilter1;
                    grpobj.btnTopFilter = btnKlangTopFilter1;
                    grpobj.brdTopKlangKategorie = brdTopKlangKategorie1;
                    grpobj.tbTopKlangKategorie = tboxTopKlangKategorie1;

                    grpobj.gboxTopSongsParallel = gboxTopSongsParallel1;
                    grpobj.tbTopKlangSongsParallel = tboxTopklangsongparallel1;
                    grpobj.btnTopSongParPlus = btnTopSongParPlus1;
                    grpobj.btnTopSongParMinus = btnTopSongParMinus1;

                    grpobj.gboxTopMusikSort = gboxTopMusikSort1;
                    grpobj.btnTopMusikAbisZ = btnTopMusikAbisZ1;
                    grpobj.btnTopMusik1bis9 = btnTopMusik1bis91;

                    grpobj.gboxTopTyp = gboxTopTyp1;
                    grpobj.rbTopIstKlangPlaylist = rbIstTopKlangPlaylist1;
                    grpobj.rbTopIstMusikPlaylist = rbIstTopMusikPlaylist1;

                    grpobj.chkbxTopAktiv = chkbxTopAktiv1;
                    grpobj.spnlTopGeräuschIcon = spnlTopGeräuschIcon1;
                    grpobj.btnTopVolMin = btnTopVolMin1;
                    grpobj.btnTopVolDown = btnTopVolDown1;
                    grpobj.btnTopVolUp = btnTopVolUp1;
                    grpobj.btnTopVolMax = btnTopVolMax1;
                    grpobj.chkbxTopVolChange = chkbxTopVolChange1;
                    grpobj.btnTopPauseMin = btnTopPauseMin1;
                    grpobj.btnTopPauseDown = btnTopPauseDown1;
                    grpobj.btnTopPauseUp = btnTopPauseUp1;
                    grpobj.btnTopPauseMax = btnTopPauseMax1;
                    grpobj.chkbxTopPauseChange = chkbxTopPauseChange1;
                    grpobj.btnTopVolMinMinus = btnTopVolMinMinus1;
                    grpobj.btnTopVolMinPlus = btnTopVolMinPlus1;
                    grpobj.btnTopVolMaxMinus = btnTopVolMaxMinus1;
                    grpobj.btnTopVolMaxPlus = btnTopVolMaxPlus1;
                    grpobj.brdTrennstrich = brdTrennstrich1;
                    grpobj.btnTopPauseMinMinus = btnTopPauseMinMinus1;
                    grpobj.btnTopPauseMinPlus = btnTopPauseMinPlus1;
                    grpobj.btnTopPauseMaxMinus = btnTopPauseMaxMinus1;
                    grpobj.btnTopPauseMaxPlus = btnTopPauseMaxPlus1;

                    grpobj.btnTopHotkeySet = btnTopHotkeySet1;
                    grpobj.spnlTopHotkey = spnlTopHotkey1;
                    grpobj.cmboxTopHotkey = cmboxTopHotkey1;
                    grpobj.btnHotkeyEntfernen = btnHotkeyEntfernen1;
                    grpobj.btnTopKlangOpen = btnTopKlangOpen1;
                    grpobj.btnKlangUpdateFiles = btnKlangUpdateFiles1;
                }
                else  //if (virtuell)
                {
                    ToggleButton tbtn = new ToggleButton();
                    tbtn.Name = "tbtnKlangPause" + objGruppe;
                    tbtn.Tag = tiErstellt;
                    tbtn.Checked += tbtnKlangPauseX_Checked;
                    tbtn.Unchecked += tbtnKlangPauseX_Unchecked;
                    grpobj.tbtnKlangPause = tbtn;
                }
                _GrpObjecte.Add(grpobj);
                //**********************************************************************************************************

                if (!virtuell)
                {
                    // AktKlangPlaylist = grpobj.aPlaylist;
                    lbEditor.SelectionChanged -= lbEditor_SelectionChanged;
                    lbEditor.SelectedIndex = -1;
                    lbEditor.SelectionChanged += lbEditor_SelectionChanged;

                    if (tcAudioPlayer.SelectedItem == tiEditor)
                        tboxEditorName.Text = lbEditor.SelectedIndex == -1 ? tboxEditorName.Text : ((ListboxItemIcon)lbEditor.SelectedItem).lbText.Content.ToString();
                    grpobj.tbTopKlangKategorie.Text = "";
                    grpobj.tbTopKlangKategorie.Tag = _GrpObjecte[_GrpObjecte.Count - 1].aPlaylist.Audio_PlaylistGUID;
                    ZeigeKlangGerneral(grpobj, true);
                    ZeigeKlangSongsParallel(grpobj, false);

                    grpobj.tbTopKlangSongsParallel.Tag = null;
                    grpobj.tbTopKlangSongsParallel.Text = "1";

                    grpobj.btnKlangUpdateFiles.Visibility = Visibility.Hidden;
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
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxVolMin == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) > grpobj._listZeile[zeile].volMax_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].volMax_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.VolumeMin = Convert.ToInt16(((TextBox)(sender)).Text);
                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxVolMax.Text) < grpobj._listZeile[zeile].audiotitel.VolumeMin)
                        grpobj._listZeile[zeile].audioZeile.tboxVolMax.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.VolumeMin);

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
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxVolMax == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) < grpobj._listZeile[zeile].volMin_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].volMin_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.VolumeMax = Convert.ToInt16(((TextBox)(sender)).Text);

                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxVolMin.Text) > grpobj._listZeile[zeile].audiotitel.VolumeMax)
                        grpobj._listZeile[zeile].audioZeile.tboxVolMin.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.VolumeMax);

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
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxPauseMin == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) > grpobj._listZeile[zeile].pauseMax_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].pauseMax_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.PauseMin = Convert.ToInt16(((TextBox)(sender)).Text);

                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text) > grpobj._listZeile[zeile].audiotitel.PauseMax)
                        grpobj._listZeile[zeile].audioZeile.tboxPauseMin.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.PauseMax);

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
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (grpobj == null)
                        return;
                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.tboxPauseMax == (Control)sender));

                    if (Convert.ToInt32(((TextBox)(sender)).Text) < grpobj._listZeile[zeile].pauseMin_wert)
                        ((TextBox)(sender)).Text = grpobj._listZeile[zeile].pauseMin_wert.ToString();
                    grpobj._listZeile[zeile].audiotitel.PauseMax = Convert.ToInt16(((TextBox)(sender)).Text);

                    if (Convert.ToInt16(grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text) < grpobj._listZeile[zeile].audiotitel.PauseMin)
                        grpobj._listZeile[zeile].audioZeile.tboxPauseMax.Text = Convert.ToString(grpobj._listZeile[zeile].audiotitel.PauseMin);

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
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }


        private void _btnVolMaxMinus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnVolMinPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnVolMaxPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMinMinus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMaxMinus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMinPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void _btnPauseMaxPlus0_X_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                if (grpobj == null)
                    return;
                KlangZeile klZeile = grpobj._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
                    (((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

                int sollWert = klZeile.pauseMax_wert + PauseSprung;
                int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

                klZeile.pauseMax_wert = sollWert < max ? sollWert : max;
                klZeile.audioZeile.tboxPauseMax.Text = Convert.ToString(klZeile.pauseMax_wert);
                klZeile.audiotitel.PauseMax = klZeile.pauseMax_wert;

                //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                if (e.Source != null) SetzeChangeBit(grpobj.aPlaylist);
            }
            catch (Exception) { }
        }

        private void tbtnKlangPauseX_Checked(object sender, RoutedEventArgs e)
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

                        if (grpobj.istMusik)
                        {
                            grpobj._listZeile[i].FadingOutStarted = false;
                            FadingIn(grpobj._listZeile[i], grpobj._listZeile[i]._mplayer, grpobj._listZeile[i].Aktuell_Volume / 100);
                            // Sichtbares Abspielen im Editor -> Scrollen
                            if (grpobj.visuell)
                                grpobj.lbEditorListe.ScrollIntoView(grpobj._listZeile[i].audioZeile);
                            //grpobj.sviewer.ScrollToVerticalOffset(i * grpobj._listZeile[i].audioZeile.ActualHeight);
                        }
                    }
                    else
                        grpobj._listZeile[i].istStandby =
                            (!grpobj._listZeile[i].istPause && !grpobj._listZeile[i].istLaufend && grpobj._listZeile[i].audiotitel.Aktiv) ? true : false;
                }
                CheckPlayStandbySongs(grpobj);

                if (!grpobj.istMusik && grpobj.aPlaylist.Fading)
                    FadingInGeräusch(grpobj);

                if (grpobj.visuell)
                {
                    ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/stop.png"));
                    ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(3, 4, 2, 2);
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
                GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.tbtnKlangPause == ((ToggleButton)sender));
                if (grpobj == null)
                    return;
                if (tiEditor.IsSelected && grpobj.sollBtnGedrueckt > 0)
                    grpobj.sollBtnGedrueckt--;
                grpobj.wirdAbgespielt = false;

                if (!grpobj.istMusik && grpobj.aPlaylist.Fading)// && !_timerFadingOutGeräusche.IsEnabled)
                    FadingOutGeräusch(true, true, grpobj);

                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    if (!grpobj.istMusik)
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
                            FadingOut(grpobj._listZeile[i], true, true);

                            grpobj._listZeile[i].istLaufend = false;
                            grpobj._listZeile[i].audioZeile.pbarTitel.Value = 0;
                            grpobj._listZeile[i].istStandby = true;
                        }
                    }
                }
                CheckPlayStandbySongs(grpobj);
                if (grpobj.visuell)
                {
                    ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
                    ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(1, 2, 0, 0);
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

        private void tboxklangsongparallelX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (AktKlangPlaylist != null)
                {
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
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
                                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Die Datenbank konnte nicht aktualisiert werden", ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewHelper.ShowError("Eingabefehler" + Environment.NewLine + "Ungültige Eingabe. Bitte geben Sie nur Ganzzahlwert ein.", ex);
                        grpobj.tbTopKlangSongsParallel.Tag = AktKlangPlaylist.MaxSongsParallel;
                        grpobj.tbTopKlangSongsParallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnSongParPlusX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int dif = Convert.ToInt32(((Button)sender).Tag);
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
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
                    klZeile.Aktuell_Volume = e.NewValue;
                    klZeile.audioZeile.sldKlangVol.ToolTip = Convert.ToInt16(Math.Round(e.NewValue)) + " %";

                    klZeile.audiotitel.Volume = Convert.ToInt16(klZeile.Aktuell_Volume);

                    if (_GrpObjecte[seite - 1].visuell && klZeile._mplayer != null)
                        klZeile._mplayer.Volume = e.NewValue / 100;

                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen                        
                    if (e.Source != null) SetzeChangeBit(_GrpObjecte[seite - 1].aPlaylist);
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
                    GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
                    if (grpobj == null)
                        return;

                    int zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.sldPlaySpeed == (Slider)sender));

                    if (zeile >= 0)
                    {
                        double speed = grpobj._listZeile[zeile].audioZeile.sldPlaySpeed.Value;
                        if (grpobj._listZeile[zeile]._mplayer != null)
                            grpobj._listZeile[zeile]._mplayer.SpeedRatio = speed;

                        grpobj._listZeile[zeile].audiotitel.Speed = speed;

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
                ViewHelper.ShowError("Datenbankfehler" + Environment.NewLine + "Ändern der Geschwindigkeit des Titel konnte nicht durchgeführt werden.", new Exception());
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
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
                if (grpobj == null)
                    return;
                double d = Convert.ToDouble(((sender) as Button).Tag);

                grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.lbiEditorRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(t => t.audioZeile.sldKlangVol.Value += d);
            }
            catch (Exception) { }
        }

        private void btnAllPauseUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
                if (grpobj == null)
                    return;
                double d = Convert.ToDouble(((sender) as Button).Tag);

                grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.lbiEditorRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
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

        private void tboxEditorName_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbEditorEditTheme.IsChecked.Value)
                {
                    if (((TextBox)(sender)).Background != null && AktKlangTheme != null)
                    {
                        ((TextBox)(sender)).Text = AktKlangTheme.Name;
                        ((TextBox)(sender)).Background = null;
                    }
                }
                else
                {
                    if (((TextBox)(sender)).Background != null && AktKlangPlaylist != null)
                    {
                        ((TextBox)(sender)).Text = AktKlangPlaylist.Name;
                        ((TextBox)(sender)).Background = null;
                    }
                }
                tbLostFocus(null, null);
            }
            catch (Exception) { }
        }

        private void tiEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                _debugTreeview.Stop();
                if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)          //nur wenn TabItem gewechselt wurde
                {
                    tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;  // = 0

                    rbEditorEditTheme.IsEnabled = true;
                    if (lbEditor.Items.Count == 0)                  //true
                        AktualisiereEditorPlaylist();

                    if (_GrpObjecte.Count > 0)
                        foreach (KlangZeile kZeile in _GrpObjecte.FirstOrDefault(t => t.visuell)._listZeile)
                            UpdateKlangZeileRatingVisuell(kZeile);
                    if (lbEditor.SelectedIndex != -1)
                        lbEditor.ScrollIntoView(lbEditor.SelectedItem);
                }
            }
            catch (Exception) { }
        }

        private void tiMusik_GotFocus(object sender, RoutedEventArgs e)
        {
            _debugTreeview.Stop();
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
                _debugTreeview.Stop();
                if (Convert.ToInt16(tcAudioPlayer.Tag) != tcAudioPlayer.SelectedIndex)      //nur wenn TabItem gewechselt wurde
                {
                    tcAudioPlayer.Tag = tcAudioPlayer.SelectedIndex;
                    AktualisierePListPlaylist();
                    if (lbPListMusik.SelectedIndex == -1 && lbMusik.SelectedIndex != -1)
                    {
                        lbPListMusik.SelectionChanged -= lbPListMusik_SelectionChanged;
                        lbPListMusik.SelectedIndex = lbMusik.SelectedIndex;
                        lbPListMusik.SelectionChanged += lbPListMusik_SelectionChanged;
                    }
                    AktualisierePlaylistThemes();
                }
            }
            catch (Exception ex) { }
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ((((Button)(sender)).Parent as Grid).Parent as Window).Close();
            }
            catch (Exception) { }
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell)._listZeile.
                    FindAll(t => t.audioZeile != null).
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

                _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked.Value != soll).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.audioZeile.chkTitel.IsChecked = soll;
                    klZeile.audiotitel.Aktiv = soll;
                    klZeile.audioZeile.chkTitel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                });

                if (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).Count == _GrpObjecte[posObjGruppe]._listZeile.Count &&
                    ((CheckBox)(e.Source)).IsChecked == true)
                {
                    //Zufallsaktivierung der Zeilen
                    List<KlangZeile> klZeileAktiv;
                    klZeileAktiv = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.playable == true).FindAll(t => t.audiotitel.Aktiv);

                    while (klZeileAktiv.Count > 0)
                    {
                        int zeileIndex = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(klZeileAktiv[(new Random()).Next(0, klZeileAktiv.Count)]);
                        chkTitel0_0_Click(_GrpObjecte[posObjGruppe]._listZeile[zeileIndex].audioZeile.chkTitel, null);

                        klZeileAktiv = klZeileAktiv.FindAll(t => t.audiotitel.Aktiv != true);
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

                _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked == true).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.audioZeile.chkVolMove.IsChecked = changeto;
                    klZeile.audiotitel.VolumeChange = changeto;
                });

                _GrpObjecte[posObjGruppe].anzVolChange = Convert.ToUInt16(
                    _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkVolMove.IsChecked == true).Count);
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

                _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkTitel.IsChecked == true).
                    FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.audioZeile.chkKlangPauseMove.IsChecked = changeto;
                    klZeile.audiotitel.PauseChange = changeto;
                });

                _GrpObjecte[posObjGruppe].anzPauseChange = Convert.ToUInt16(
                    _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.chkKlangPauseMove.IsChecked == true).Count);
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
            }
            catch (Exception) { }
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tcAudioPlayer.SelectedItem == tiMusik)
                    btnShuffleImg.Source = ((ToggleButton)sender).IsChecked == true ?
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/shuffle.png")) :
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/no_shuffle.png"));
                else
                    btnEditShuffleImg.Source = ((ToggleButton)sender).IsChecked == true ?
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/shuffle.png")) :
                        new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/no_shuffle.png"));

                if (tcAudioPlayer.SelectedItem == tiMusik)
                {
                    _BGPlayer.AktPlaylist.Shuffle = ((ToggleButton)sender).IsChecked.Value;
                    Global.ContextAudio.Update<Audio_Playlist>(_BGPlayer.AktPlaylist);
                }
                else
                {
                    if (AktKlangPlaylist.Shuffle != ((ToggleButton)sender).IsChecked.Value)
                    {
                        AktKlangPlaylist.Shuffle = ((ToggleButton)sender).IsChecked.Value;
                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    }
                }
            }
            catch (Exception) { }
        }

        public void BGFadingOut(Musik BG, bool playerStoppen, bool sofort)
        {
            try
            {
                if (_timerBGFadingOut.IsEnabled && ((Fading)_timerBGFadingOut.Tag).BG != BG)              //Anderer Fading-Out am laufen -> Abbrechen
                {
                    _timerBGFadingOut.Stop();
                    ((Fading)_timerBGFadingOut.Tag).mp.Pause();
                    ((Fading)_timerBGFadingOut.Tag).mp.Position = TimeSpan.FromMilliseconds(0);
                    if (((Fading)_timerBGFadingOut.Tag).BG != null)
                        ((Fading)_timerBGFadingOut.Tag).BG.isPaused = true;
                }

                if (_timerFadingIn.IsEnabled && ((Fading)_timerFadingIn.Tag).mp == BG.mPlayer)          // Titel ist Fading-In --> Fading-In abbrechen
                {
                    _timerFadingIn.Stop();
                    FadingIn_Started = null;
                }
                FadingOut_Started = BG.mPlayer;

                _timerBGFadingOut.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

                Fading fadInfo = new Fading();
                fadInfo.BG = BG;
                fadInfo.mp = BG.mPlayer;
                fadInfo.Start = DateTime.MinValue;
                fadInfo.startVol = BG.mPlayer.Volume;
                fadInfo.fadingOutSofort = sofort;
                fadInfo.mPlayerStoppen = playerStoppen;

                _timerBGFadingOut.Tag = fadInfo;
                //_volProzentModFadingOut = 1;        // Volume am Start modifiziert mit 100%

                //_fadingOutGeräusche = _GrpObjecte.FindAll(t => !t.istMusik).FindAll(t => t.wirdAbgespielt).ToList();
                _timerBGFadingOut.Start();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("BGFading Out Exeption" + Environment.NewLine + "Fehler beim Fading-Out ist ein Fehler aufgetreten.", ex);
            }
        }

        public void _timerBGFadingOut_Tick(object sender, EventArgs e)
        {
            Fading fadInfo = (Fading)_timerBGFadingOut.Tag;

            if (fadInfo.Start == DateTime.MinValue)
                fadInfo.Start = DateTime.Now;

            double _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;

            if (!fadInfo.fadingOutSofort && _vergangeneZeit > 5000)  //maximale Wartezeit zum FadingOut
                fadInfo.fadingOutSofort = true;

            if (FadingIn_Started != null && FadingIn_Started.Source != null || fadInfo.fadingOutSofort)
            {
                fadInfo.fadingOutSofort = true;

                //solange Volume runterregeln bis der Titel extern das Fadingstoppt

                if (fadInfo.mp != null)
                {
                    double aktVol = (fadingTime != 0) ? fadInfo.startVol - fadInfo.startVol * (_vergangeneZeit / (fadingTime * fadingIntervall)) : 0;
                    //_volProzentModFadingOut = (_volProzentModFadingOut != 0) ? 1 - 1 * (_vergangeneZeit / (fadingTime * fadingIntervall)) : 0;
                    aktVol = aktVol < 0 ? 0 : aktVol;

                    if (fadInfo.mp.Volume != aktVol)
                        fadInfo.mp.Volume = aktVol;

                    if (fadInfo.mp.Volume == 0)
                    {
                        if (fadInfo.mPlayerStoppen && fadInfo.BG.FadingOutStarted)   //bei Volume 0 Fadingauf false und MediaPlayer freigeben
                        {
                            //setNewLbFading(fadInfo.BG.mPlayer, "Backgroundmusik gestoppt");
                            fadInfo.mp.Stop();
                            fadInfo.mp.Close();
                        }
                        if (!fadInfo.mPlayerStoppen && fadInfo.BG.FadingOutStarted)
                        {
                            //setNewLbFading(fadInfo.mp, "Backgroundmusik gepaused");
                            fadInfo.mp.Pause();
                            fadInfo.mp.Position = TimeSpan.FromMilliseconds(0);
                            fadInfo.BG.isPaused = true;
                        }

                        //setNewLbFading(fadInfo.mp, "Fading Out beendet");
                        fadInfo.BG.FadingOutStarted = false;
                        FadingOut_Started = null;
                        _timerBGFadingOut.Stop();
                    }
                }
            }
        }

        public void FadingIn(Musik BG, MediaPlayer mplayer, double zielVol)
        {
            if (_timerFadingIn.IsEnabled && BG != null && ((Fading)_timerFadingIn.Tag).mp != BG.mPlayer)              //Anderer Fading-In am laufen -> Abbrechen
            {
                _timerFadingIn.Stop();
                ((Fading)_timerFadingIn.Tag).mp.Pause();
                ((Fading)_timerFadingIn.Tag).mp.Position = TimeSpan.FromMilliseconds(0);
                if (((Fading)_timerFadingIn.Tag).BG != null)
                    ((Fading)_timerFadingIn.Tag).BG.isPaused = true;
            }
            FadingIn_Started = mplayer;

            _timerFadingIn.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

            mplayer.Volume = 0;
            Fading fadInfo = new Fading();
            fadInfo.BG = BG;
            fadInfo.mp = mplayer;
            fadInfo.Start = DateTime.MinValue;
            fadInfo.zielVol = zielVol;

            if (_timerBGFadingOut.IsEnabled && ((Fading)_timerBGFadingOut.Tag).mp == mplayer)                       //Titel gerade Fading-Out -> Stoppen
            {
                fadInfo.startVol = ((Fading)_timerBGFadingOut.Tag).mp.Volume;
                ((Fading)_timerBGFadingOut.Tag).BG.FadingOutStarted = false;
                FadingOut_Started = null;
                _timerBGFadingOut.Stop();
            }
            else
                mplayer.Play();

            _timerFadingIn.Tag = fadInfo;
            _timerFadingIn.Start();
        }

        public void FadingIn(KlangZeile klZeile, MediaPlayer mplayer, double zielVol)
        {
            if (_timerFadingIn.IsEnabled && klZeile != null && ((Fading)_timerFadingIn.Tag).mp != mplayer)              //Anderer Fading-In am laufen -> Abbrechen
            {
                _timerFadingIn.Stop();
                ((Fading)_timerFadingIn.Tag).mp.Pause();
                ((Fading)_timerFadingIn.Tag).mp.Position = TimeSpan.FromMilliseconds(0);
                if (((Fading)_timerFadingIn.Tag).klZeile != null)
                    ((Fading)_timerFadingIn.Tag).klZeile.istPause = true;
            }
            FadingIn_Started = mplayer;

            _timerFadingIn.Interval = TimeSpan.FromMilliseconds(fadingIntervall);
            mplayer.Volume = 0;

            Fading fadInfo = new Fading();
            fadInfo.klZeile = klZeile;
            fadInfo.mp = mplayer;
            fadInfo.Start = DateTime.MinValue;
            fadInfo.zielVol = zielVol;

            if (_timerBGFadingOut.IsEnabled && ((Fading)_timerBGFadingOut.Tag).mp == mplayer)                       //Titel gerade Fading-Out -> Stoppen
            {
                fadInfo.startVol = ((Fading)_timerBGFadingOut.Tag).mp.Volume;
                ((Fading)_timerBGFadingOut.Tag).BG.FadingOutStarted = false;
                FadingOut_Started = null;
                _timerBGFadingOut.Stop();
            }
            else
                mplayer.Play();

            _timerFadingIn.Tag = fadInfo;
            _timerFadingIn.Start();
        }

        public void FadingOut(KlangZeile klZeile, bool playerStoppen, bool sofort)
        {
            try
            {
                if (_timerFadingOut.IsEnabled && ((Fading)_timerFadingOut.Tag).mp != klZeile._mplayer)              //Anderer Fading-Out am laufen -> Abbrechen
                {
                    _timerFadingOut.Stop();
                    ((Fading)_timerFadingOut.Tag).mp.Pause();
                    ((Fading)_timerFadingOut.Tag).mp.Position = TimeSpan.FromMilliseconds(0);
                    if (((Fading)_timerFadingOut.Tag).klZeile != null)
                        ((Fading)_timerFadingOut.Tag).klZeile.istPause = true;
                }

                if (_timerFadingIn.IsEnabled && ((Fading)_timerFadingIn.Tag).mp == klZeile._mplayer)          // Titel ist Fading-In --> Fading-In abbrechen
                {
                    _timerFadingIn.Stop();
                    FadingIn_Started = null;
                }
                FadingOut_Started = klZeile._mplayer;

                _timerFadingOut.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

                Fading fadInfo = new Fading();
                fadInfo.klZeile = klZeile;
                fadInfo.mp = klZeile._mplayer;
                fadInfo.Start = DateTime.MinValue;
                fadInfo.startVol = klZeile._mplayer.Volume;
                fadInfo.fadingOutSofort = sofort;
                fadInfo.mPlayerStoppen = playerStoppen;

                _timerFadingOut.Tag = fadInfo;
                _timerFadingOut.Start();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fading Out Exeption" + Environment.NewLine + "Fehler beim Fading-Out ist ein Fehler aufgetreten.", ex);
            }
        }

        private class group
        {
            public GruppenObjekt grpobj = null;
            public double zielProzent;
            public double startProzent;
            public DateTime StartZeit;
            public double _vergangeneZeit;
        }

        private class FadingInGeräusche
        {
            public List<group> gruppen = new List<group>();
        }

        private class FadingOutGeräusche
        {
            public List<group> gruppen = new List<group>();
            public bool fadingOutSofort = false;
        }

        public void FadingInGeräusch(GruppenObjekt grpobj)
        {
            group newgroup = new group();
            newgroup.grpobj = grpobj;
            newgroup.zielProzent = grpobj.force_Volume == null ? slPlaylistVolume.Value : (double)grpobj.force_Volume * 100;
            newgroup.startProzent = grpobj.Vol_PlaylistMod != slPlaylistVolume.Value ? grpobj.Vol_PlaylistMod : 0;
            newgroup.StartZeit = DateTime.MinValue;

            if (!_timerFadingInGeräusche.IsEnabled)
            {
                FadingInGeräusche _fadingInGeräusche = new FadingInGeräusche();
                _fadingInGeräusche.gruppen.Clear();
                _fadingInGeräusche.gruppen.Add(newgroup);

                _timerFadingInGeräusche.Tag = _fadingInGeräusche;
                _timerFadingInGeräusche.Start();
            }
            else
            {
                ((FadingInGeräusche)_timerFadingInGeräusche.Tag).gruppen.Add(newgroup);
            }
        }

        public void _timerFadingInGeräusche_Tick(object sender, EventArgs e)
        {
            FadingInGeräusche fadInfo = (FadingInGeräusche)_timerFadingInGeräusche.Tag;

            List<group> grpToDelete = new List<group>();
            
            foreach (group gruppe in fadInfo.gruppen)
            {
                if (gruppe.StartZeit == DateTime.MinValue)
                    gruppe.StartZeit = DateTime.Now;

                gruppe._vergangeneZeit = DateTime.Now.Subtract(gruppe.StartZeit).TotalMilliseconds;

                if (!gruppe.grpobj.wirdAbgespielt)
                {
                    grpToDelete.Add(gruppe);
                    continue;
                }
                if (gruppe.zielProzent != slPlaylistVolume.Value)
                    gruppe.zielProzent = slPlaylistVolume.Value;
                gruppe.grpobj.Vol_PlaylistMod = (gruppe.grpobj.Vol_PlaylistMod != gruppe.zielProzent) ? (gruppe.zielProzent) * (gruppe._vergangeneZeit / (fadingTime * fadingIntervall)) + gruppe.startProzent : gruppe.zielProzent;
                gruppe.grpobj.Vol_PlaylistMod = gruppe.grpobj.Vol_PlaylistMod > gruppe.zielProzent ? gruppe.zielProzent : gruppe.grpobj.Vol_PlaylistMod;// _volProzentModFadingIn;
                double l = 0;
                foreach (KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t._mplayer != null))
                {
                    kZeile._mplayer.Volume = gruppe.grpobj.force_Volume == null ?
                        ((double)kZeile.Aktuell_Volume / 100) * (gruppe.grpobj.Vol_PlaylistMod / 100) :
                        gruppe.grpobj.Vol_PlaylistMod / 100;
                    l = kZeile._mplayer.Volume;
                }

                if (!gruppe.grpobj.wirdAbgespielt || gruppe.grpobj.Vol_PlaylistMod == gruppe.zielProzent)
                    grpToDelete.Add(gruppe);
            }
            //Check noch angeklickt zum FadingIn
            for (int c = 0; c < grpToDelete.Count; c++)
                fadInfo.gruppen.Remove(grpToDelete[c]);

            if (fadInfo.gruppen.Count == 0)
                _timerFadingInGeräusche.Stop();
        }

        public void FadingOutGeräusch(bool playerStoppen, bool sofort, GruppenObjekt grpobj)
        {
            group newgroup = new group();
            newgroup.grpobj = grpobj;
            newgroup.startProzent = grpobj.Vol_PlaylistMod;
            newgroup.zielProzent = 0;
            newgroup.StartZeit = DateTime.MinValue;

            if (!_timerFadingOutGeräusche.IsEnabled)
            {
                FadingOutGeräusche _fadingOutGeräusche = new FadingOutGeräusche();
                _fadingOutGeräusche.gruppen.Clear();
                _fadingOutGeräusche.gruppen.Add(newgroup);
                _fadingOutGeräusche.fadingOutSofort = sofort;

                _timerFadingOutGeräusche.Interval = TimeSpan.FromMilliseconds(fadingIntervall);
                _timerFadingOutGeräusche.Tag = _fadingOutGeräusche;
                _timerFadingOutGeräusche.Start();
            }
            else
            {
                ((FadingOutGeräusche)_timerFadingOutGeräusche.Tag).gruppen.Add(newgroup);
            }
        }

        public void _timerFadingOutGeräusche_Tick(object sender, EventArgs e)
        {
            FadingOutGeräusche fadInfo = (FadingOutGeräusche)_timerFadingOutGeräusche.Tag;

            if (1 == 1)
            {
                List<group> grpToDelete = new List<group>();

                
                foreach (group gruppe in fadInfo.gruppen)
                {
                    if (gruppe.StartZeit == DateTime.MinValue)
                        gruppe.StartZeit = DateTime.Now;

                    gruppe._vergangeneZeit = DateTime.Now.Subtract(gruppe.StartZeit).TotalMilliseconds;

                    if (!fadInfo.fadingOutSofort && gruppe._vergangeneZeit > 5000)  //maximale Wartezeit zum FadingOut
                        fadInfo.fadingOutSofort = true;

                    if (gruppe.grpobj.wirdAbgespielt)
                    {
                        grpToDelete.Add(gruppe);
                        continue;
                    }
                    if (FadingIn_Started != null && FadingIn_Started.Source != null || fadInfo.fadingOutSofort)
                    {
                        fadInfo.fadingOutSofort = true;

                        gruppe.grpobj.Vol_PlaylistMod = (gruppe.grpobj.Vol_PlaylistMod != 0) ? gruppe.startProzent - gruppe.startProzent * (gruppe._vergangeneZeit / (fadingTime * fadingIntervall)) : 0;
                        gruppe.grpobj.Vol_PlaylistMod = gruppe.grpobj.Vol_PlaylistMod < 0 ? 0 : gruppe.grpobj.Vol_PlaylistMod;

                        double l = 0;
                        foreach (KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t._mplayer != null))
                        {
                            kZeile._mplayer.Volume = gruppe.grpobj.force_Volume == null ?
                                ((double)kZeile.Aktuell_Volume / 100) * (gruppe.grpobj.Vol_PlaylistMod / 100) :
                                gruppe.grpobj.Vol_PlaylistMod / 100;
                            l = kZeile._mplayer.Volume;
                        }
                        
                        if (gruppe.grpobj.Vol_PlaylistMod == 0)
                        {
                            foreach (KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t._mplayer != null))
                            {
                                kZeile.istPause = true;
                                kZeile._mplayer.Pause();
                                kZeile.istStandby = true; //f
                                kZeile.istLaufend = false;
                            }
                            grpToDelete.Add(gruppe);
                        }
                    }
                    if (gruppe.grpobj.wirdAbgespielt)
                        grpToDelete.Add(gruppe);
                }

                for (int c = 0; c < grpToDelete.Count; c++)
                    fadInfo.gruppen.Remove(grpToDelete[c]);

                if (fadInfo.gruppen.Count == 0)
                    _timerFadingOutGeräusche.Stop();
            }
        }

        public void _timerFadingOut_Tick(object sender, EventArgs e)
        {
            Fading fadInfo = (Fading)_timerFadingOut.Tag;

            if (fadInfo.Start == DateTime.MinValue)
                fadInfo.Start = DateTime.Now;

            double _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;

            if (!fadInfo.fadingOutSofort && _vergangeneZeit > 5000)  //maximale Wartezeit zum FadingOut
                fadInfo.fadingOutSofort = true;

            if (FadingIn_Started != null && FadingIn_Started.Source != null || fadInfo.fadingOutSofort)
            {
                fadInfo.fadingOutSofort = true;
                //solange Volume runterregeln bis der Titel extern das Fadingstoppt
                if (fadInfo.mp != null)
                {
                    double aktVol = (fadingTime != 0) ? fadInfo.startVol - fadInfo.startVol * (_vergangeneZeit / (fadingTime * fadingIntervall)) : 0;
                    aktVol = aktVol < 0 ? 0 : aktVol;

                    if (fadInfo.mp.Volume != aktVol)
                        fadInfo.mp.Volume = aktVol;

                    if (fadInfo.mp.Volume == 0)
                    {
                        if (fadInfo.mPlayerStoppen && fadInfo.klZeile.FadingOutStarted)   //bei Volume 0 Fadingauf false und MediaPlayer freigeben
                        {
                            fadInfo.mp.Stop();
                            fadInfo.mp.Close();
                        }
                        if (!fadInfo.mPlayerStoppen && fadInfo.klZeile.FadingOutStarted)
                        {
                            fadInfo.mp.Pause();
                            fadInfo.mp.Position = TimeSpan.FromMilliseconds(0);
                            fadInfo.klZeile.istPause = true;
                        }

                        //     _volProzentModFadingOut = 1;
                        fadInfo.klZeile.FadingOutStarted = false;
                        FadingOut_Started = null;
                        _timerFadingOut.Stop();
                    }
                }
            }
        }

        private class Fading
        {
            public MediaPlayer mp;
            public DateTime Start;
            public double zielVol;
            public double startVol = 0;
            public bool fadingOutSofort;
            public bool mPlayerStoppen;
            public Musik BG;
            public KlangZeile klZeile;
        }

        public void _timerFadingIn_Tick(object sender, EventArgs e)
        {
            Fading fadInfo = (Fading)_timerFadingIn.Tag;
            if (fadInfo.Start == DateTime.MinValue)
                fadInfo.Start = DateTime.Now;

            double _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;

            double aktVol = (fadingTime != 0) ? fadInfo.zielVol * ((_vergangeneZeit / (fadingTime * fadingIntervall)) + fadInfo.startVol) : fadInfo.zielVol;

            if (slBGVolume.Value / 100 != fadInfo.zielVol)        // wenn während des Fading die Lautstärke verändert wird
                fadInfo.zielVol = slBGVolume.Value / 100;

            if (FadingIn_Started != fadInfo.mp)
                stopFadingIn = true;

            if (fadInfo.mp != null)
                fadInfo.mp.Volume = aktVol;

            if (stopFadingIn || fadInfo.mp.Volume >= fadInfo.zielVol)
            {
                if (fadInfo.mp.Volume >= fadInfo.zielVol && FadingIn_Started == fadInfo.mp)
                {
                    FadingIn_Started = null;
                    _timerFadingIn.Stop();
                }
                stopFadingIn = false;
            }
        }


        public void btnEditorGewichtung_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
                if (grpobj == null)
                    return;
                KlangZeile kZeile = grpobj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.btnGewichtung == (Button)sender);

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
                ViewHelper.ShowError("Speicherfehler" + Environment.NewLine + "Die Gewichtung des Titels konnte nicht vorgenommen werden.", ex);
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
        /*
        public void tiUebersicht_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                AktKlangPlaylist = null;
                AktKlangTheme = null;
            }
            catch (Exception) { }
        }

        public void tiUebersicht_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbEditorEditTheme.IsChecked.Value)
                    rbEditorEditPlaylist_Checked(null, null);
            }
            catch (Exception) { }
        }*/

        public void checkPlayableFilesInGrpobj(Audio_Playlist aPlaylist)
        {
            BackgroundWorker bkwChkPlayable = new BackgroundWorker();
            bkwChkPlayable.WorkerReportsProgress = true;
            bkwChkPlayable.WorkerSupportsCancellation = true;
            bkwChkPlayable.DoWork += new DoWorkEventHandler(bkwChkPlayable_DoWork);
            bkwChkPlayable.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwChkPlayable_RunWorkerCompleted);
            bkwChkPlayable.RunWorkerAsync(aPlaylist);
        }

        // ***Problem***  hier könnte das Problem liegen, dass die Titel Deaktiviert werden, obwohl sie da sind
        private void bkwChkPlayable_DoWork(object sender, DoWorkEventArgs args)
        {
            string old_Pfad;
            string old_Datei;
            MediaPlayer mp = new MediaPlayer();

            try
            {
                Audio_Playlist plylst = (Audio_Playlist)args.Argument;
                for (int i = 0; i < plylst.Audio_Playlist_Titel.Count; i++)
                {
                    Audio_Titel aTitel = plylst.Audio_Playlist_Titel.ElementAt(i).Audio_Titel;

                    _GrpObjecte.FindAll(t => t.aPlaylist == plylst).ForEach(delegate(GruppenObjekt grpObj)
                    {
                        KlangZeile klZeile = grpObj._listZeile.First(t => t.audiotitel.Audio_TitelGUID == plylst.Audio_Playlist_Titel.ElementAt(i).Audio_TitelGUID);


                        //***Problem : Vielleicht verursacht das Speichern der Daten eine Execption ?!?
                        old_Pfad = klZeile.audiotitel.Audio_Titel.Pfad;
                        old_Datei = klZeile.audiotitel.Audio_Titel.Datei;
                        Audio_Titel neuaTitel = setTitelStdPfad(klZeile.audiotitel.Audio_Titel);
                        if (neuaTitel.Pfad != old_Pfad ||
                            neuaTitel.Datei != old_Datei)
                        {
                            klZeile.audiotitel.Audio_Titel = neuaTitel;
                            //Global.ContextAudio.Update<Audio_Titel>(klZeile.audiotitel.Audio_Titel);
                        }

                        //***Problem : IsPathRooted wurde vorerst für Testzwecke entfernt (Problem DropBox Ordner erzeugt x von y Dateien nicht abspielbar ***//
                        klZeile.playable = (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                                            File.Exists(aTitel.Pfad + "\\" + aTitel.Datei));
                    });
                }
                args.Result = (Audio_Playlist)args.Argument;
            }
            catch (Exception)
            { }
        }

        private void bkwChkPlayable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            try
            {
                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                {
                    if ((Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag == (Guid)(((Audio_Playlist)args.Result).Audio_PlaylistGUID))
                    {
                        _GrpObjecte.FindAll(t => t.aPlaylist == ((Audio_Playlist)args.Result)).ForEach(delegate(GruppenObjekt grpobj)
                        {
                            if (grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.audiotitel.Aktiv).Count != grpobj._listZeile.FindAll(t => t.audiotitel.Aktiv).Count)
                            {
                                ((MusikZeile)lbPListGeräusche.Items[i]).spnlMusikZeile.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 80));   // Yellow
                                ((MusikZeile)lbPListGeräusche.Items[i]).spnlMusikZeile.ToolTip =
                                    grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.audiotitel.Aktiv).Count + " von " + grpobj._listZeile.FindAll(t => t.audiotitel.Aktiv).Count + " Titel abspielbar";
                            }
                            else
                            {
                                ((MusikZeile)lbPListGeräusche.Items[i]).spnlMusikZeile.Background = null;
                                ((MusikZeile)lbPListGeräusche.Items[i]).spnlMusikZeile.ToolTip = null;
                            }
                        });
                    }
                }
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                (sender as BackgroundWorker).Dispose();
            }
        }

        public void GetTotalLength(Audio_Playlist aPlaylist)
        {
            if (!AudioSpieldauerBerechnen)
                return;
            if (aPlaylist.Länge == 0)
            {
                double gesLänge = 0;
                for (int i = 0; i < aPlaylist.Audio_Playlist_Titel.Count; i++)
                    if (aPlaylist.Audio_Playlist_Titel.ElementAt<Audio_Playlist_Titel>(i).Audio_Titel.Länge.HasValue)
                        gesLänge += aPlaylist.Audio_Playlist_Titel.ElementAt(i).Audio_Titel.Länge.Value;
            }

            imgPlaylistLängeCheck.Visibility = Visibility.Visible;

            Global.SetIsBusy(true, string.Format("Länge der Titel wird überarbeitet..."));

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker.RunWorkerAsync(aPlaylist);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs args)
        {
            MediaPlayer mp = new MediaPlayer();
            TimeSpan totalLength = TimeSpan.FromMilliseconds(0);

            try
            {
                Audio_Playlist plylst = (Audio_Playlist)args.Argument;
                for (int i = 0; i < plylst.Audio_Playlist_Titel.Count; i++)
                {
                    Audio_Titel aTitel = plylst.Audio_Playlist_Titel.ElementAt(i).Audio_Titel;
                    if (aTitel.Länge == null || aTitel.Länge == 0)
                    {
                        //aTitel = setTitelStdPfad(aTitel);

                        if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                            Directory.Exists(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                            File.Exists(aTitel.Pfad + "\\" + aTitel.Datei))
                        {
                            if (plylst != (Audio_Playlist)args.Argument) break;
                            mp.Volume = 0;
                            mp.Open(new Uri(aTitel.Pfad + "\\" + aTitel.Datei));
                            mp.Play();
                            if (SpinWait.SpinUntil(() => { return mp.NaturalDuration.HasTimeSpan; }, 4000))
                            {
                                if (plylst != (Audio_Playlist)args.Argument)
                                {
                                    mp.Stop();  //*** neu
                                    mp.Close();
                                    break;
                                }
                                mp.Pause();
                                totalLength += mp.NaturalDuration.TimeSpan;

                                if (plylst != (Audio_Playlist)args.Argument) break;
                                if (plylst.Audio_Playlist_Titel.Count >= i + 1)
                                {                                    
                                    if (aTitel.Länge != mp.NaturalDuration.TimeSpan.TotalMilliseconds)
                                    {
                                        aTitel.Länge = mp.NaturalDuration.TimeSpan.TotalMilliseconds;
                                        Global.ContextAudio.Update<Audio_Titel>(aTitel);
                                    }
                                }
                            }
                            mp.Stop();
                            mp.Close();
                            if (plylst != (Audio_Playlist)args.Argument) break;
                        }
                        else
                        {
                            _GrpObjecte.FindAll(t => t.aPlaylist == plylst).ForEach(delegate(GruppenObjekt grpObj)
                            {
                                KlangZeile klZeile = grpObj._listZeile.FirstOrDefault(t => t.audiotitel.Audio_TitelGUID == plylst.Audio_Playlist_Titel.ElementAt(i).Audio_TitelGUID);
                                if (klZeile != null) klZeile.playable = false;
                            });
                        }
                    }
                    else
                        totalLength += TimeSpan.FromMilliseconds(aTitel.Länge.Value);
                }
                if (plylst != (Audio_Playlist)args.Argument)
                    totalLength = TimeSpan.FromMilliseconds(0);
                ((Audio_Playlist)args.Argument).Länge = totalLength.TotalMilliseconds;

                args.Result = (Audio_Playlist)args.Argument;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Berechnungsfehler" + Environment.NewLine + "Beim Ermitteln der Gesamtlänge ist ein Fehler aufgetreten.", ex);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            try
            {
                imgPlaylistLängeCheck.Visibility = Visibility.Hidden;
                if (args.Error == null && ((Audio_Playlist)args.Result).Länge != 0)
                {
                    Global.ContextAudio.Update<Audio_Playlist>(((Audio_Playlist)args.Result));
                    if (_BGPlayer.AktPlaylist == ((Audio_Playlist)args.Result))
                    {
                        _BGPlayer.totalLength = ((Audio_Playlist)args.Result).Länge;
                        ((MusikZeile)lbMusik.SelectedItem).tblkLänge.Text = TimeSpan.FromMilliseconds(((Audio_Playlist)args.Result).Länge).ToString(@"hh\:mm\:ss");
                        ((MusikZeile)lbPListMusik.SelectedItem).tblkLänge.Text = ((MusikZeile)lbMusik.SelectedItem).tblkLänge.Text;

                        for (int i = 0; i < lbPListMusik.Items.Count; i++)
                        {
                            if ((Guid)((MusikZeile)lbPListMusik.Items[i]).Tag == (Guid)(((Audio_Playlist)args.Result).Audio_PlaylistGUID))
                            {
                                ((MusikZeile)lbPListMusik.Items[i]).tblkLänge.Text = TimeSpan.FromMilliseconds(((Audio_Playlist)args.Result).Länge).ToString(@"hh\:mm\:ss");
                                break;
                            }
                        }
                        GruppenObjekt grpObj = _GrpObjecte.FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == (Guid)(((Audio_Playlist)args.Result).Audio_PlaylistGUID));
                        if (grpObj != null)
                            grpObj.totalTimePlylist = ((Audio_Playlist)args.Result).Länge;

                    }
                    else
                    {
                        for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                        {
                            if ((Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag == (Guid)(((Audio_Playlist)args.Result).Audio_PlaylistGUID))
                            {
                                ((MusikZeile)lbPListGeräusche.Items[i]).tblkLänge.Text = TimeSpan.FromMilliseconds(((Audio_Playlist)args.Result).Länge).ToString(@"hh\:mm\:ss");
                                break;
                            }
                        }
                        GruppenObjekt grpObj = _GrpObjecte.FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == (Guid)(((Audio_Playlist)args.Result).Audio_PlaylistGUID));
                        if (grpObj != null)
                            grpObj.totalTimePlylist = ((Audio_Playlist)args.Result).Länge;

                        foreach (KlangZeile kZeile in grpObj._listZeile)
                        {
                            if (kZeile.audiotitel.Audio_Titel.Länge != null &&
                                kZeile.audioZeile.lblDauer.Content.ToString() != TimeSpan.FromMilliseconds(kZeile.audiotitel.Audio_Titel.Länge.Value).ToString(@"mm\:ss"))
                                kZeile.audioZeile.lblDauer.Content = TimeSpan.FromMilliseconds(kZeile.audiotitel.Audio_Titel.Länge.Value).ToString(@"mm\:ss");
                        }
                    }
                }
                Global.SetIsBusy(false);
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                Global.SetIsBusy(false);
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
                if (((ListboxItemIcon)lbEditor.SelectedItem).IsVisible)
                    lbEditor.ScrollIntoView(lbEditor.SelectedItem);
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

        private void tbEditorTopFilterX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
                if (grpobj == null)
                    return;
                grpobj._listZeile.FindAll(t => t.audioZeile != null).FindAll(t => t.audioZeile.lbiEditorRow != null).ForEach(delegate(KlangZeile klZeile)
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
                for (int i = 0; i < lbMusik.Items.Count; i++)
                {
                    ((MusikZeile)lbMusik.Items[i]).Visibility =
                        ((MusikZeile)lbMusik.Items[i]).tblkTitel.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ||
                        ((MusikZeile)lbMusik.Items[i]).tboxKategorie.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ?
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

        private void btneditorFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnBGFilter_Click(sender, e);
                lbEditor.ScrollIntoView(lbEditor.SelectedItem);
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

                dialog.SelectedPath = MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Eingabefehler" + Environment.NewLine + "Das Auswählen des Standard-Verzeichnisses hat eine Exeption ausgelöst.", ex);
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

        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e != null && e.Source != null && e.Source is TabItem
                    && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
                {
                    TabItem item = (TabItem)(e.Source);
                    DragDrop.DoDragDrop(item, item, DragDropEffects.All);
                    lbEditor.Tag = null;
                }
            }
            catch (Exception) { }
        }

        private void tbtnMusikZeileBtnCheck_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
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
                    grpobj = _GrpObjecte.FindAll(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == ((Guid)(mZeile.Tag)));

                if (grpobj == null)
                {
                    tiPlus_MouseUp(true, null);   // Neue Geräusch Playlist im Theme ausgewählt
                    ((ToggleButton)sender).Tag = _GrpObjecte[_GrpObjecte.Count - 1].objGruppe;
                    string sTitel = ((TextBlock)((StackPanel)((ToggleButton)e.Source).Parent).FindName("tblkTitel")).Text;

                    int plylstItemPos = 0;
                    while (((ListboxItemIcon)lbEditor.Items[plylstItemPos]).lbText.Content.ToString() != sTitel)
                        plylstItemPos++;

                    grpobj = _GrpObjecte.FirstOrDefault(t => t.objGruppe == tiErstellt);

                    //Get Playlist
                    grpobj.aPlaylist = Global.ContextAudio.PlaylistListe.
                            FirstOrDefault(t => t.Audio_PlaylistGUID.Equals(((ListboxItemIcon)lbEditor.Items[plylstItemPos]).Tag));

                    if (grpobj.aPlaylist != null)
                    {
                        //AktKlangPlaylist = grpobj.aPlaylist;
                        grpobj.visuell = false;
                        grpobj.istMusik = grpobj.aPlaylist.Hintergrundmusik;
                        grpobj.playlistName = grpobj.aPlaylist.Name;

                        foreach (Audio_Playlist_Titel aPlaylistTitel in grpobj.aPlaylist.Audio_Playlist_Titel)
                        {
                            /*if (!File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei == null ? "" : aPlaylistTitel.Audio_Titel.Datei))
                            {
                                aPlaylistTitel.Audio_Titel = setTitelStdPfad(aPlaylistTitel.Audio_Titel);
                                if (File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei))
                                    Global.ContextAudio.Update<Audio_Titel>(aPlaylistTitel.Audio_Titel);
                            }*/

                            KlangNewRow(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei, grpobj, 0, aPlaylistTitel);

                            if (aPlaylistTitel.Aktiv &&
                                !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
                            {
                                for (int i = 0; i <= aPlaylistTitel.Rating; i++)
                                    grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                            }
                        }
                    }
                }

                if (mZeile.chkbxForceVol.IsChecked.Value)
                    grpobj.force_Volume = mZeile.sldForceVolume.Value / 100;
                else
                    grpobj.force_Volume = null;

                //grpobj.Vol_PlaylistMod = Convert.ToUInt16(slPlaylistVolume.Value);

                if ((!btnPListPListAbspielen.IsEnabled) ||
                    (Convert.ToBoolean(btnPListPListAbspielen.Tag) &&
                     !grpobj.wirdAbgespielt &&
                     _GrpObjecte.FindAll(t => t.wirdAbgespielt).FindAll(t => !t.visuell).FindAll(t => t.tiEditor == null).Count != 0))    //Abspielen
                {
                    grpobj.wirdAbgespielt = true;
                    grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                    if (grpobj.visuell)
                    {
                        ((Image)grpobj.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/stop.png"));
                        ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(3, 4, 2, 2);
                    }
                    btnPListPListAbspielen.Tag = true;

                    ////////////////////////
                    //if (grpobj.aPlaylist.Fading)
                    //    FadingInGeräusch(grpobj);

                    ///////////////////////

                    foreach (MusikZeile _mZeile in lbPListGeräusche.Items)
                    {
                        if (_mZeile.tbtnCheck.IsChecked.Value && _mZeile.tbtnCheck != ((ToggleButton)sender))
                        {
                            GruppenObjekt grpObjAlleAnderen = _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(_mZeile.tbtnCheck.Tag));
                            if (grpObjAlleAnderen != null && grpObjAlleAnderen.wirdAbgespielt != grpobj.wirdAbgespielt)
                            {
                                grpObjAlleAnderen.wirdAbgespielt = !grpobj.wirdAbgespielt;
                                grpObjAlleAnderen.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                                if (grpObjAlleAnderen.tbtnKlangPause.Content != null)
                                {
                                    ((Image)grpObjAlleAnderen.tbtnKlangPause.Content).Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
                                    ((Image)grpobj.tbtnKlangPause.Content).Margin = new Thickness(3, 4, 2, 2);
                                }
                            }
                        }
                    }
                }
                checkPListPlaybtnGeräusche();
                checkPlayableFilesInGrpobj(grpobj.aPlaylist);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anwählen der Geräusche-Playlist ist ein Fehler aufgetreten", ex);
            }
        }

        //***
        private void tbtnMusikZeileBtnCheck_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                MusikZeile mZeile = null;
                foreach (MusikZeile zeile in lbPListGeräusche.Items)
                {
                    if (zeile.tbtnCheck == (ToggleButton)sender)
                    {
                        mZeile = zeile;
                        break;
                    }
                }
                btnPListPListAbspielen.IsEnabled = false;
                GruppenObjekt grpobj = ((ToggleButton)sender).Tag == null ? null : _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.objGruppe == Convert.ToInt32(((ToggleButton)sender).Tag));
                if (grpobj == null)
                    grpobj = _GrpObjecte.FindAll(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == ((Guid)(mZeile.Tag)));

                ((StackPanel)((Grid)((StackPanel)((ToggleButton)e.Source).Parent).Parent).Parent).Background = null;
                ((StackPanel)((Grid)((StackPanel)((ToggleButton)e.Source).Parent).Parent).Parent).ToolTip = null;

                if (grpobj != null && grpobj.wirdAbgespielt)
                {
                    grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));

                    if (_GrpObjecte.FindAll(t => t.wirdAbgespielt).Count == 0)
                        btnPListPListAbspielen.Tag = false;

                    /*  grpobj.changed = true;
                      if (grpobj.changed)
                      {
                          _GrpObjecte.Remove(grpobj);
                          ((ToggleButton)sender).Tag = null;
                      }*/
                }
                btnPListPListAbspielen.IsEnabled = true;
                checkPListPlaybtnGeräusche();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Abwählen der Geräusche-Playlist ist ein Fehler aufgetreten", ex);
            }
        }

        private void checkPListPlaybtnGeräusche()
        {
            bool found = false;
            foreach (MusikZeile mZeile in lbPListGeräusche.Items)
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
            bool neu = true;
            List<Audio_Playlist> aPlyList = Global.ContextAudio.PlaylistListe;

            Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik).OrderBy(t => t.Name).ToList().ForEach(delegate(Audio_Playlist playlistliste)
            {
                neu = (posMusik + 1 > lbPListMusik.Items.Count) ? true : false;

                MusikZeile mZeile = neu ? new MusikZeile() : ((MusikZeile)(lbPListMusik.Items[posMusik]));
                if (neu)
                {
                    mZeile.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                    mZeile.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    mZeile.Cursor = Cursors.Hand;
                    mZeile.tboxKategorie.GotFocus += tbGotFocus;
                    mZeile.tboxKategorie.LostFocus += tboxTopKategorieX_LostFocus;
                    mZeile.pbarSong.MouseLeftButtonDown += pbarBGSong_MouseLeftButtonDown;
                    mZeile.tbtnCheck.Visibility = Visibility.Collapsed;
                    lbPListMusik.Items.Add(mZeile);
                }
                ((Grid)mZeile.grdForceVol.Parent).RowDefinitions[1].Height = tbThemePListNormSize.IsChecked.Value ? new GridLength(0) : GridLength.Auto;
                mZeile.ToolTip = (tbThemePListNormSize.IsChecked.Value && mZeile.tboxKategorie.Text != "") ? mZeile.tboxKategorie.Text : null;
                mZeile.Tag = playlistliste.Audio_PlaylistGUID;
                mZeile.tblkTitel.Text = playlistliste.Name;
                mZeile.tblkLänge.Text = (playlistliste.Länge != 0) ? TimeSpan.FromMilliseconds(playlistliste.Länge).ToString(@"hh\:mm\:ss") : "";
                mZeile.tboxKategorie.Tag = mZeile.Tag;
                mZeile.tboxKategorie.Text = playlistliste.Kategorie;
                posMusik++;
            });

            Global.ContextAudio.PlaylistListe.FindAll(t => !t.Hintergrundmusik).OrderBy(t => t.Name).ToList().ForEach(delegate(Audio_Playlist playlistliste)
            {
                neu = (posGeräusche + 1 > lbPListGeräusche.Items.Count) ? true : false;

                MusikZeile mZeile = neu ? new MusikZeile() : ((MusikZeile)(lbPListGeräusche.Items[posGeräusche]));

                if (neu)
                {
                    mZeile.grdForceVol.ColumnDefinitions[0].Width = new GridLength(90);
                    mZeile.grdForceVol.ColumnDefinitions[1].Width = new GridLength(70);
                    mZeile.tbtnCheck.Visibility = Visibility.Visible;
                    mZeile.tbtnCheck.Tag = null;
                    mZeile.tbtnCheck.Checked += tbtnMusikZeileBtnCheck_Checked;
                    mZeile.tbtnCheck.Unchecked += tbtnMusikZeileBtnCheck_UnChecked;
                    mZeile.tboxKategorie.GotFocus += tbGotFocus;
                    mZeile.tboxKategorie.LostFocus += tboxTopKategorieX_LostFocus;
                    mZeile.chkbxForceVol.Click += chkbxForceVol_Click;
                    mZeile.sldForceVolume.ValueChanged += sldForceVolume_ValueChanged;
                    lbPListGeräusche.Items.Add(mZeile);
                }
                
                Grid.SetRow(mZeile.grdForceVol, tbThemePListNormSize.IsChecked.Value ? 0 : 1);
                Grid.SetColumn(mZeile.grdForceVol, tbThemePListNormSize.IsChecked.Value ? 2 : 0);
                mZeile.tblkLänge.Visibility = tbThemePListNormSize.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
                mZeile.grdForceVol.ColumnDefinitions[0].Width = tbThemePListNormSize.IsChecked.Value ? new GridLength(20) : new GridLength(90);
                mZeile.chkbxForceVol.Content = tbThemePListNormSize.IsChecked.Value ? null : "Lautstärke";
                mZeile.chkbxForceVol.Margin = tbThemePListNormSize.IsChecked.Value ? new Thickness(0, 1, 0, 0) : new Thickness(15, 1, 0, 0);
                mZeile.brdKategorie.Visibility = tbThemePListNormSize.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
                ((Grid)mZeile.grdForceVol.Parent).ColumnDefinitions[1].Width = tbThemePListNormSize.IsChecked.Value ? new GridLength(100) : new GridLength(70);
                mZeile.ToolTip = (tbThemePListNormSize.IsChecked.Value && mZeile.tboxKategorie.Text != "") ? mZeile.tboxKategorie.Text : null;

                mZeile.Tag = playlistliste.Audio_PlaylistGUID;
                mZeile.chkbxForceVol.Tag = playlistliste;
                mZeile.sldForceVolume.Tag = playlistliste;
                mZeile.tblkLänge.Text = (playlistliste.Länge != 0) ? TimeSpan.FromMilliseconds(playlistliste.Länge).ToString(@"hh\:mm\:ss") : "";
                mZeile.tblkTitel.Text = playlistliste.Name;
                mZeile.tboxKategorie.Tag = mZeile.Tag;
                mZeile.tboxKategorie.Text = playlistliste.Kategorie;
                mZeile.chkbxForceVol.IsChecked = (playlistliste.DoForce) ? true : false;
                mZeile.sldForceVolume.Value = (!playlistliste.DoForce && playlistliste.ForceVolume == 0) ? 50 : playlistliste.ForceVolume;
                posGeräusche++;
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

            aPlaylist.DoForce = (mZeile.chkbxForceVol.IsChecked == true) ? true : false;
            Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);

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


            aPlaylist.ForceVolume = Convert.ToInt32(mZeile.sldForceVolume.Value);
            Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);

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
                string[] split = tbPListGeräuscheName.Text.ToLower().Split(new Char[] { ' ', ',' });

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

        private void tbThemesFilterAll_TextChanged(object sender, TextChangedEventArgs e)
        {
            AktualisierePlaylistThemes();
            tbPListFilterAll_TextChanged(sender, e);
        }

        private void tbPListFilterAll_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                btnPListAktFilter.Visibility = Visibility.Hidden;
                for (int i = 0; i < lbPListMusik.Items.Count; i++)
                {
                    if ((chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListMusik.Items[i])).tboxKategorie.Text) ||
                        chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListMusik.Items[i])).tblkTitel.Text)) &&
                        (chkPListFilter(tbPListMusikName.Text, ((MusikZeile)(lbPListMusik.Items[i])).tboxKategorie.Text) ||
                        chkPListFilter(tbPListMusikName.Text, ((MusikZeile)(lbPListMusik.Items[i])).tblkTitel.Text)))
                        ((MusikZeile)(lbPListMusik.Items[i])).Visibility = Visibility.Visible;
                    else
                        ((MusikZeile)(lbPListMusik.Items[i])).Visibility = Visibility.Collapsed;
                }

                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                {
                    if ((chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tboxKategorie.Text) ||
                        chkPListFilter(tbThemesFilterAll.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tblkTitel.Text)) &&
                        (chkPListFilter(tbPListGeräuscheName.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tboxKategorie.Text) ||
                        chkPListFilter(tbPListGeräuscheName.Text, ((MusikZeile)(lbPListGeräusche.Items[i])).tblkTitel.Text)))
                        ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Visible;
                    else
                        ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception) { }
        }

        private void btnThemeGeräuscheFilterAktiv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbPListGeräuscheName.Text = "";
                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                {
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
                //lbTest.Visibility = lbTest.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                //lbTest.Items.Clear();

                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                    ((MusikZeile)(lbPListGeräusche.Items[i])).Visibility = Visibility.Visible;

                tbPListFilterAll_TextChanged(tbPListGeräuscheName, null);
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
                lbMusik.SelectedIndex = lbPListMusik.SelectedIndex;
            }
            catch (Exception) { }
        }

        /// <summary>
        ///  Wenn Filter im Text Rückgabe True
        /// <summary>
        /// <param name="pfad">Xml-Datei</param>
        private bool chkPListFilter(string filter, string text)
        {
            bool result = true;

            foreach (string s in filter.ToLower().Split(new Char[] { ' ', ',' }))
                if (s != "" && (!text.ToLower().Contains(s) || text == ""))
                    result = false;
            return result;
        }

        private void slPlaylistVolume_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsInitialized)
                {
                    slPlaylistVolume.ToolTip = Math.Round(slPlaylistVolume.Value) + " %";
                    if (Convert.ToDouble(btnPListPListSpeaker.Tag) != -1)
                        btnPListPListSpeaker.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                    {
                        GruppenObjekt grpObj = (mZeile.tbtnCheck.Tag == null) ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
                        if (grpObj != null)
                            grpObj.Vol_PlaylistMod = Convert.ToInt32(slPlaylistVolume.Value);
                    }

                    if (Einstellungen.GeneralGeräuscheVolume != (int)Math.Round(((Slider)sender).Value))
                        Einstellungen.SetEinstellung<int>("GeneralGeräuscheVolume", (int)Math.Round(((Slider)sender).Value));
                }
            }
            catch (Exception) { }
        }

        private void slBGVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (IsInitialized && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null &&
                    (FadingIn_Started == null || FadingIn_Started.Source == null) &&
                    !_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted &&
                    _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Volume != e.NewValue / 100)
                    _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Volume = e.NewValue / 100;
                if (Convert.ToDouble(btnBGSpeaker.Tag) != -1)
                    btnBGSpeaker.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                slBGVolume.ToolTip = Math.Round(slBGVolume.Value) + " %";

                if (IsInitialized && Einstellungen.GeneralMusikVolume != (int)Math.Round(((Slider)sender).Value))
                    Einstellungen.SetEinstellung<int>("GeneralMusikVolume", (int)Math.Round(((Slider)sender).Value));
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ändern der Hintergrund Lautstärke ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnKlangUpdateFiles_Click(object sender, RoutedEventArgs e)
        {
            string titelRef = "";
            try
            {
                Global.SetIsBusy(true, string.Format("Neue Dateien werden integriert..."));
                List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);

                titelRef = ((Button)sender).Tag.ToString();
                ((Button)sender).Visibility = Visibility.Visible;

                List<string> allFiles = new List<string>();
                foreach (string datei in Directory.GetFiles(titelRef, "*.mp3", SearchOption.AllDirectories))
                    if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                        allFiles.Add(datei);

                foreach (string datei in Directory.GetFiles(titelRef, "*.wav", SearchOption.AllDirectories))
                    if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                        allFiles.Add(datei);

                foreach (string datei in Directory.GetFiles(titelRef, "*.ogg", SearchOption.AllDirectories))
                    if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                        allFiles.Add(datei);

                foreach (string datei in Directory.GetFiles(titelRef, "*.wma", SearchOption.AllDirectories))
                    if (titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                        allFiles.Add(datei);

                if (allFiles.Count > 0)
                {
                    if (ViewHelper.ConfirmYesNoCancel("Hinzufügen von Musiktitel aus dem Verzeichnis", "Es wurden insgesamt " + allFiles.Count +
                        " Dateien gefunden, die noch nicht in der Playliste eingetragen sind." + Environment.NewLine +
                        "Sollen diese integriert werden?") == 2)
                    {
                        Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine + titelRef + "werden eingebunden"));
                        grdEditorListe.Visibility = Visibility.Hidden;
                        foreach (string newFile in allFiles)
                        {
                            Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine +
                                titelRef + "werden eingebunden" +
                                Environment.NewLine + System.IO.Path.GetFileName(newFile)));
                            KlangDateiHinzu(newFile, null, AktKlangPlaylist, 0);
                        }
                        grdEditorListe.Visibility = Visibility.Visible;
                    }
                }
                ((Button)sender).Visibility = Visibility.Hidden;
                _chkAnzDateienInDir(_GrpObjecte.FirstOrDefault(t => t.aPlaylist == AktKlangPlaylist));
                Global.SetIsBusy(false);
            }
            catch (Exception ex)
            {
                grdEditorListe.Visibility = Visibility.Visible;
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Ungültiger Pfad" + Environment.NewLine + "Bitte überprüfen Sie das Verzeichnis:" + Environment.NewLine + titelRef, ex);
            }
        }

        private void btnPListPListAbspielen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                {
                    grpobj = (mZeile.tbtnCheck.Tag == null) ? null : _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(mZeile.tbtnCheck.Tag));
                    if (grpobj != null && mZeile.tbtnCheck.IsChecked.Value)
                    {
                        if (Convert.ToBoolean(btnPListPListAbspielen.Tag))
                            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
                        else
                        {
                            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                            if (grpobj.aPlaylist.Fading)
                                FadingInGeräusch(grpobj);
                        }
                    }
                    else
                    {
                        int i = 1;
                        i++;
                    }
                }
                btnPListPListAbspielen.Tag = !Convert.ToBoolean(btnPListPListAbspielen.Tag);
                btnimgPListPListAbspielen.Source = !Convert.ToBoolean(btnPListPListAbspielen.Tag) ?
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")) :
                    new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));

                if (grpobj != null)
                {
                    grpobj.wirdAbgespielt = Convert.ToBoolean(btnPListPListAbspielen.Tag);
                    grpobj.totalTimePlylist = -1;
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Fehler bei der Funktion btnPListPListAbspielen_Click", ex);
            }
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

        private void _datenloeschen(int mrRes, bool allesloeschen, string saveTMPdatei)
        {
            hotkeys.Clear();
            spnlHotkeys.Children.Clear();

            if (mrRes == 1)
            {
                Global.SetIsBusy(true, string.Format("Bestehende Daten werden gesichert..." + Environment.NewLine + saveTMPdatei));
                this.UpdateLayout();
                if (Global.ContextAudio.PlaylistListe.Count > 0)
                    Global.ContextAudio.PlaylistListe[0].Export(saveTMPdatei, Global.ContextAudio.PlaylistListe[0].Audio_PlaylistGUID);
            }
            if (mrRes == 1 || allesloeschen)
            {
                Global.SetIsBusy(true, string.Format("Laufende Songs werden beendet..."));
                foreach (GruppenObjekt grpobj in _GrpObjecte.FindAll(t => t.visuell))
                    AlleKlangSongsAus(grpobj, false, false, true);

                foreach (MusikZeile aZeile in lbPListGeräusche.Items) aZeile.tbtnCheck.IsChecked = false;

                if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
                {
                    if (btnBGStoppen.IsEnabled)
                        btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                    _BGPlayer.NochZuSpielen.Clear();
                    _BGPlayer.Gespielt.Clear();
                    lbMusik.Tag = -1;
                    _BGPlayer.AktPlaylist = null;
                    lbMusik.Items.Clear();
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
            FadingIn_Started = null;
            stopFadingIn = false;

            hotkeys = new List<hotkey>();
            _BGPlayer = new MusikView();
            _GrpObjecte = new List<GruppenObjekt>();
            AktKlangPlaylist = null;
            AktKlangTheme = null;
            BGSongTimer.Close();
            foreach (DispatcherTimer dispTmr in lstKlangPlayEndetimer)
                if (dispTmr != null) dispTmr.Stop();
            lstKlangPlayEndetimer.Clear();

            KlangProgBarTimer.Stop();
            MusikProgBarTimer.Stop();
            lbMusik.Items.Clear();

            _BGPlayer.BG.Clear();
            _BGPlayer.BG.Add(new Musik());
            _BGPlayer.BG.Add(new Musik());

            setStdPfad();
            fadingTime = MeisterGeister.Logic.Einstellung.Einstellungen.Fading;

            DataContext = _zeile;
        }

        private void btnAudioDatenImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int mrRes = ViewHelper.ConfirmYesNoCancel("Löschen bestehender Daten", "Soll die aktuelle Datenbank erweitert werden?" + Environment.NewLine + Environment.NewLine + "Wählen sie 'Ja' damit die Datenbank erweitert wird." +
                    Environment.NewLine + "Wählen Sie 'Nein' um die bestehende Datenbank zu ersetzten. Achtung! Alle Daten gehen verloren.");
                if (mrRes == 2 || mrRes == 1)
                {
                    Global.SetIsBusy(true);


                    int mrImpVar = (ViewHelper.ConfirmYesNoCancel("Komplette Sicherung", "Aus der Historie heraus, gibt es zwei Varianten einer Komplettsicherung." + Environment.NewLine +
                            Environment.NewLine + "Die Sicherung der Musikdaten werden in neuerer Version auf verschiedene Dateien aufgeteilt." + Environment.NewLine +
                            "Wenn Sie eine einzige XML-Datei als Komplettsicherung gespeichert haben, deutet das auf die vorherige Methode hin." + Environment.NewLine +
                            Environment.NewLine + "Wollen Sie das Importieren des neuen Prozesses durchführen?"));
                    if (mrImpVar == 0)
                    {
                        Global.SetIsBusy(false);
                        return;
                    }

                    //Importieren aller Playlisten und danach aller Themelisten
                    if (mrImpVar == 2)
                    {
                        System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                        folderDlg.SelectedPath = Environment.CurrentDirectory;
                        folderDlg.Description = "Wählen Sie ein Verzeichnis das alle Dateien der Sicherung enthält";
                        List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();

                        if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (mrRes == 1)
                            {
                                btnAudioDatenDelete.Tag = true;
                                btnAudioDatenDelete.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                btnAudioDatenDelete.Tag = null;
                            }
                            Global.SetIsBusy(true, string.Format("Alle Playlisten werden importiert ..."));
                            string pfad = folderDlg.SelectedPath;

                            DirectoryInfo d = new DirectoryInfo(pfad);
                            List<string> listXML = new List<string>();
                            foreach (FileInfo f in d.GetFiles("*.xml"))
                                listXML.Add(f.DirectoryName + "\\" + f.Name);

                            btnAudioDatenImport.Tag = listXML;
                            btnPlaylistImport.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            btnKlangThemeImport.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            btnAudioDatenImport.Tag = null;
                        }
                    }
                    else
                    {
                        if (ViewHelper.ConfirmYesNoCancel("Unsicherer Verlauf", "ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
                           "Leider konnte dieser Prozess noch NICHT ZUVERLÄSSIG programmiert werden." + Environment.NewLine +
                           Environment.NewLine + "Es muss damit gerechtnet werden, das die exportierte Datei" + Environment.NewLine + "NICHT MEHR IMPORTIERT werden kann!" +
                           Environment.NewLine + Environment.NewLine + "Soll der Vorgang trotzdem fortgesetzt werden?") != 2)
                            return;

                        string pfad = ViewHelper.ChooseFile("Audio-Daten importieren", "", false, "xml");
                        if (pfad != null)
                        {
                            try
                            {
                                string tmpFile = Directory.GetCurrentDirectory() + @"\AudioDB_temp.xml";
                                _datenloeschen(mrRes, false, tmpFile);

                                Global.SetIsBusy(true, string.Format("Neue Daten werden importiert ..."));

                                if (Audio_Playlist.Import(pfad, "") != null)
                                {
                                    Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                                    Global.ContextAudio.Save();
                                }

                                Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                                AktualisiereMusikPlaylist();
                                AktualisiereEditorPlaylist();
                                AktualisiereHotKeys();
                                Global.SetIsBusy(true, string.Format("Temporäre Daten werden gelöscht ..."));
                                if (mrRes == 1)
                                    File.Delete(tmpFile);
                                Global.SetIsBusy(false);
                                rbEditorEditPList.IsChecked = false;
                                tcAudioPlayer.Tag = -1;

                                tiEditor_GotFocus(sender, null);
                                rbEditorKlang_Click(rbEditorKlang, null);
                                rbEditorEditPList.IsChecked = true;
                                (wpnlPListThemes.Tag as List<Guid>).Clear();
                            }
                            catch (Exception ex)
                            {
                                Global.SetIsBusy(false);
                                ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
                            }
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
                int mrRes = btnAudioDatenDelete.Tag != null ? 2 :
                    ViewHelper.ConfirmYesNoCancel("Löschen ALLER bestehender Audio-Daten", "Soll die komplette Audio-Datenbank gelöscht werden?" +
                        Environment.NewLine + Environment.NewLine + "Achtung! Alle Daten gehen unwiderruflich verloren.");
                if (mrRes == 2)
                {
                    _datenloeschen(mrRes, true, "");

                    Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                    Global.ContextAudio.UpdateList<Audio_Titel>();
                    Global.ContextAudio.UpdateList<Audio_Playlist>();
                    Global.ContextAudio.UpdateList<Audio_Playlist_Titel>();
                    Global.ContextAudio.UpdateList<Audio_Theme>();
                    Global.ContextAudio.UpdateList<Audio_Theme_Playlist>();

                    Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                    AktualisiereMusikPlaylist();
                    AktualisiereEditorPlaylist();
                    AktualisiereHotKeys();
                    Global.SetIsBusy(false);

                    rbEditorEditPList.IsChecked = false;
                    tcAudioPlayer.Tag = -1;

                    tiEditor_GotFocus(sender, null);
                    rbEditorKlang_Click(rbEditorKlang, null);
                    rbEditorEditPList.IsChecked = true;
                    (wpnlPListThemes.Tag as List<Guid>).Clear();
                    lbEditorListe.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
            }
        }


        private void btnPlaylistImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> dateien;
                if (btnAudioDatenImport.Tag != null)
                    dateien = btnAudioDatenImport.Tag as List<string>;
                else
                    dateien = ViewHelper.ChooseFiles("Playlist(en) importieren", "", true, new string[3]{"xml","wpl","m3u8"});
                if (dateien != null)
                {
                    try
                    {
                        bool _nicht_first = false;
                        int was = lbEditor.SelectedIndex;
                        
                        foreach (string pfad in dateien)
                        {
                            Global.SetIsBusy(true, string.Format("Neue Playlist  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + "'  wird importiert ..."));
                            (App.Current.MainWindow as View.MainView).UpdateLayout();

                            if (pfad.EndsWith(".xml"))
                            {
                                AktKlangPlaylist = Audio_Playlist.Import(pfad, "Audio_Playlist", _nicht_first);
                            }
                            else
                            {
                                FileInfo fi = new FileInfo(pfad);
                                AktKlangPlaylist = NeueKlangPlaylistInDB(fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length));

                                _DateienAufnehmen(new List<string>() { pfad }, null, AktKlangPlaylist, 0, false);
                                Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                            }
                            Global.ContextAudio.Save();
                            _nicht_first = true;
                        }
                        Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                        tbEditorPlaylistFilter.Text = "";
                        AktualisiereMusikPlaylist();
                        AktualisiereEditorPlaylist();
                        AktualisiereHotKeys();
                        
                        if (AktKlangPlaylist != null)
                        {
                            for (int i = 0; i < lbEditor.Items.Count; i++)
                                if (AktKlangPlaylist != null && (Guid)((ListboxItemIcon)lbEditor.Items[i]).Tag == AktKlangPlaylist.Audio_PlaylistGUID)
                                    lbEditor.SelectedIndex = i;
                        }
                        lbEditor.ScrollIntoView(lbEditor.SelectedItem);
                        Global.SetIsBusy(false);
                    }
                    catch (Exception ex)
                    {
                        Global.SetIsBusy(false);
                        ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
                        AktualisiereEditorPlaylist();
                        AktualisiereHotKeys();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
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
                    if (ViewHelper.ConfirmYesNoCancel("Löschen des Themes", "Wollen Sie wirklich das ausgewählte Theme  '" + aTheme.Name + "'  löschen.") == 2)
                    {
                        Global.SetIsBusy(true, string.Format("Theme '" + aTheme.Name + "' wird gelöscht..."));

                        if (AktKlangTheme != null && (wpnlPListThemes.Tag as List<Guid>).Contains(AktKlangTheme.Audio_ThemeGUID))
                        {
                            for (int i = 0; i < wpnlPListThemes.Children.Count; i++)
                                if ((Guid)((grdThemeButton)wpnlPListThemes.Children[i]).Tag == AktKlangTheme.Audio_ThemeGUID)
                                    ((grdThemeButton)wpnlPListThemes.Children[i]).tbtnTheme.IsChecked = false;
                        }

                        if (!Global.ContextAudio.Delete<Audio_Theme>(aTheme))
                            Global.ContextAudio.ThemeListe.Remove(aTheme);
                        lbEditorTheme.SelectedIndex = -1;
                        //expEditorTopThemeTheme.Visibility = Visibility.Collapsed;
                        AktualisiereEditorThemes();

                        AktKlangTheme = null;

                        wpnlEditorThemeGeräusch.Children.Clear();
                        wpnlEditorThemeMusik.Children.Clear();
                        wpnlEditorTopThemesThemes.Children.Clear();
                        tboxEditorName.Text = GetNeuenNamen("Neues Theme", 1);

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

        private void lbItembtnLöschenPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
                Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == g);
                if (aPlaylist != null)
                {
                    if (ViewHelper.ConfirmYesNoCancel("Löschen der Playlist", "Wollen Sie wirklich die ausgewählte Playlist  '" + aPlaylist.Name + "'  löschen.") == 2)
                    {
                        Global.SetIsBusy(true, string.Format("Playlist '" + aPlaylist.Name + "' wird gelöscht..."));
                        if (AktKlangPlaylist != null && AktKlangPlaylist.Name == aPlaylist.Name)
                        {
                            for (UInt16 i = 0; i <= lbEditor.Items.Count - 1; i++)
                            {
                                if (((ListboxItemIcon)lbEditor.Items[i]).lbText.Content.ToString() == aPlaylist.Name)
                                {
                                    GruppenObjekt grpobj = _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.visuell);
                                    if (grpobj == null || grpobj.objGruppe == -1)
                                        return;

                                    foreach (MusikZeile mZeile in lbPListGeräusche.Items)
                                    {
                                        if (grpobj != null && mZeile.tbtnCheck.IsChecked.Value)
                                            grpobj.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
                                    }
                                    if (grpobj != null)
                                    {
                                        PlaylisteLeeren(grpobj);

                                        grpobj.tbTopKlangKategorie.Text = "";
                                        grpobj.tbTopKlangKategorie.Tag = null;
                                        tboxEditorName.Text = GetNeuenNamen("NeuePlayliste", 0);

                                        grpobj.tbTopKlangSongsParallel.TextChanged -= tboxklangsongparallelX_TextChanged;
                                        grpobj.tbTopKlangSongsParallel.Tag = null;
                                        grpobj.tbTopKlangSongsParallel.Text = "1";
                                        grpobj.tbTopKlangSongsParallel.TextChanged += tboxklangsongparallelX_TextChanged;
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

                        AktualisiereEditorPlaylist();
                        tbEditorPlaylistFilter_TextChanged(null, null);
                        int oldIndex = lbEditor.SelectedIndex;
                        lbEditor.SelectedIndex = -1;
                        lbEditor.SelectedIndex = oldIndex != 0? oldIndex-1: oldIndex;
                        Global.SetIsBusy(false);
                        ViewHelper.Popup("Die Playlist wurde erfolgreich gelöscht.");
                    }
                }
                else
                    ViewHelper.ShowError("Die ausgewählte Playlist konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", new Exception());
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Löschen der Playlist ist ein Fehler aufgetreten.", ex);
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
                    string datei = ViewHelper.ChooseFile("Playliste exportieren", "Playlist_" + aPlaylist.Name.Replace("/", "_") + ".xml", true, "xml");
                    if (datei != null)
                    {
                        Global.SetIsBusy(true, string.Format("Die Playlist wird exportiert ..."));

                        datei = datei.Replace("--", "-");
                        while (datei.EndsWith("-.xml") || datei.EndsWith(" .xml"))
                            datei = datei.Substring(0, datei.Length - 5) + ".xml";       

                        File.Delete(datei);
                        aPlaylist.Export(datei, g);

                        Global.SetIsBusy(false);
                        ViewHelper.Popup("Die Playlist-Daten wurden erfolgreich gesichert.");
                    }
                }
                else
                    ViewHelper.ShowError("Die ausgewählte Playlist konnte in der Datenbank nicht gefunden werden. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", new Exception());

            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren der Playlist ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnAudioDatenExport_Click(object sender, RoutedEventArgs e)
        {
            Global.ContextAudio.Save();
            try
            {
                if (ViewHelper.ConfirmYesNoCancel("Komplette Sicherung", "Eine komplette Sicherung der Audiodaten wird durchgeführt, " +
                        Environment.NewLine + "bestehend aus der Sicherung aller Playlisten und aller vorhandene Themelisten." + Environment.NewLine +
                       Environment.NewLine + "Wollen Sie den Prozess durchführen?") != 2)
                    return;

                btnAudioDatenExport.Tag = true;
                btnPlaylistExportALL.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                btnAudioDatenExport.Tag = null;
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei dem Speichern der kompletten Audiodatenbank ist ein Fehler aufgetreten.", ex);
            }
        }

        private void AlleThemesExportieren(string dlgFolder)
        {
            Global.SetIsBusy(true, string.Format("Alle Themes werden exportiert ..."));

            foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe.
                                Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")))
            {
                Global.SetIsBusy(true, string.Format("Theme '" + aTheme.Name + "' wird exportiert"));
                string pfaddatei = dlgFolder + "\\Theme_" + aTheme.Name.Replace("/", "_") + ".xml";

                pfaddatei = pfaddatei.Replace("--", "-");
                while (pfaddatei.EndsWith("-.xml") || pfaddatei.EndsWith(" .xml"))
                    pfaddatei = pfaddatei.Substring(0, pfaddatei.Length - 5) + ".xml";

                ExportTheme(aTheme, pfaddatei);
            }

            Global.SetIsBusy(true, string.Format("Theme Export beendet ..."));
        }

        private void btnThemeExportAll_Click(object sender, RoutedEventArgs e)
        {
            Global.ContextAudio.Save();
            try
            {
                System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                folderDlg.SelectedPath = Environment.CurrentDirectory;
                folderDlg.Description = "Wählen Sie ein Verzeichnis aus in das alle Dateien gespeichert werden sollen." + Environment.NewLine;
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    AlleThemesExportieren(folderDlg.SelectedPath);

                    ViewHelper.Popup("Der Export wurde erfolgreich beendet" + Environment.NewLine + "Alle Themelisten wurden folgednes Verzeichnis exportiert" +
                        Environment.NewLine + Environment.NewLine + folderDlg.SelectedPath + Environment.NewLine +
                        Environment.NewLine + "!!! Bitte beachten Sie, dass Die PLAYLISTEN SEPARAT gesichert werden müssen !!!");
                    Global.SetIsBusy(false);
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei dem Speichern der kompletten Audio-Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnPlaylistExportAll_Click(object sender, RoutedEventArgs e)
        {
            Global.ContextAudio.Save();
            try
            {
                System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                folderDlg.SelectedPath = Environment.CurrentDirectory;
                folderDlg.Description = "Wählen Sie ein Verzeichnis aus in das alle Dateien gespeichert werden sollen";
                List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Global.SetIsBusy(true, string.Format("Export aller Playlisten wird vorbereitet..."));
                    string pfad = folderDlg.SelectedPath;
                    Audio_Playlist.Export(Global.ContextAudio.PlaylistListe, pfad);

                    if (btnAudioDatenExport.Tag != null && Convert.ToBoolean(btnAudioDatenExport.Tag))
                    {
                        AlleThemesExportieren(pfad);
                        ViewHelper.Popup("Der Export aller Audio-Daten wurde erfolgreich beendet." + Environment.NewLine + Environment.NewLine +
                            "Alle Audio-Playlisten und Themes wurden in folgendes Verzeichnis exportiert." + Environment.NewLine +
                            Environment.NewLine + Environment.NewLine + pfad + Environment.NewLine);
                    }
                    else
                        ViewHelper.Popup("Der Export wurde erfolgreich beendet." + Environment.NewLine + Environment.NewLine +
                            "Alle Playlisten wurden in folgendes Verzeichnis exportiert" + Environment.NewLine +
                            Environment.NewLine + Environment.NewLine + pfad + Environment.NewLine);

                    Global.SetIsBusy(false);
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei dem Speichern der kompletten Audio-Playlisten ist ein Fehler aufgetreten.", ex);
            }
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
                grdEditorThemeWPnlUTheme.Children.Add(cmbx);
                Grid.SetColumn(cmbx, 0);
                cmbx.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Erstellen des DropDown-Feldes für die Themeliste ist ein Fehler aufgetreten.", ex);
            }
        }

        private void cmbxThemeTheme_DropDownClosed(object sender, EventArgs e)
        {
            grdEditorThemeWPnlUTheme.Children.Remove((ComboBox)sender);
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
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei der Auswahl des untergeordneten Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        private void bxThemeBtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid ThemeORPlaylistGuid = (Guid)((Button)sender).Tag;

                Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == ThemeORPlaylistGuid);
                if (aPlaylist != null)
                {
                    AktKlangTheme.Audio_Playlist.Remove(aPlaylist);
                }
                else
                {
                    Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == ThemeORPlaylistGuid);
                    if (aTheme != null)
                    {
                        AktKlangTheme.Audio_Theme1.Remove(Global.ContextAudio.ThemeListe.First(t => t.Audio_ThemeGUID == ThemeORPlaylistGuid));
                        wpnlEditorTopThemesThemes.Children.Remove((boxThemeTheme)((GroupBox)((Grid)((Button)sender).Parent).Parent).Parent);
                    }
                }
                Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Lösen des untergeordenten Themes aus dem Theme ist ein Fehler aufgetreten.", ex);
            }
        }

        private void bxEditorThemeBtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid ThemeGuid = (Guid)((Button)sender).Tag;

                Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == (ThemeGuid));
                if (aPlaylist != null)
                {
                    if (aPlaylist.Hintergrundmusik)
                    {
                        wpnlEditorThemeMusik.Children.Remove((boxThemeTheme)((GroupBox)((Grid)((Button)sender).Parent).Parent).Parent);
                        if (rbEditorMusik.IsChecked.Value)
                        {
                            foreach (ListboxItemIcon lbi in lbEditor.Items)
                                if (ThemeGuid == (Guid)lbi.Tag)
                                {
                                    lbi.Visibility = Visibility.Visible;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        wpnlEditorThemeGeräusch.Children.Remove((boxThemeTheme)((GroupBox)((Grid)((Button)sender).Parent).Parent).Parent);
                        if (rbEditorKlang.IsChecked.Value)
                            foreach (ListboxItemIcon lbi in lbEditor.Items)
                            {
                                if (ThemeGuid == (Guid)lbi.Tag)
                                {
                                    lbi.Visibility = Visibility.Visible;
                                    break;
                                }
                            }
                    }
                    AktKlangTheme.Audio_Playlist.Remove(aPlaylist);

                }
                Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Fehler beim Entfernen des Playlist/Theme-Buttons im Editor", ex);
            }
        }

        private void ThemeItemIconAblegen(ListboxItemIcon lbi)
        {
            try
            {
                boxThemeTheme bxTheme = new boxThemeTheme();
                bxTheme.imgIcon.Source = lbi.imgIcon.Source;
                bxTheme.txblkName.Text = lbi.lbText.Content.ToString();
                bxTheme.txblkName.IsEnabled = false;
                bxTheme.Margin = new Thickness(5);
                bxTheme.Tag = lbi;
                bxTheme.btnClose.Tag = lbi.Tag;
                bxTheme.btnClose.Click += bxEditorThemeBtnClose_Click;

                Audio_Playlist aPlaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == ((Guid)lbi.Tag));
                if (aPlaylist != null)
                {
                    if (AktKlangTheme == null)
                        NeueKlangThemeInDB(tboxEditorName.Text);
                    if (aPlaylist.Hintergrundmusik)
                    {
                        if (wpnlEditorThemeMusik.Children.Count == 0)
                        {
                            wpnlEditorThemeMusik.Children.Add(bxTheme);
                            ((ListboxItemIcon)lbEditor.Items[lbEditor.Items.IndexOf(lbi)]).Visibility = Visibility.Collapsed;
                            AktKlangTheme.Audio_Playlist.Add(aPlaylist);
                            Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                        }
                        else
                        {
                            ViewHelper.ShowError("Es ist bereits eine Musik-Playliste in dem Theme eingetragen." + Environment.NewLine +
                                "Entfernen Sie zunächst die aktuelle Musik-Playliste des Themes um eine neue zu definieren.", new Exception());
                            return;
                        }
                    }
                    else
                    {
                        wpnlEditorThemeGeräusch.Children.Add(bxTheme);
                        ((ListboxItemIcon)lbEditor.Items[lbEditor.Items.IndexOf(lbi)]).Visibility = Visibility.Collapsed;
                        AktKlangTheme.Audio_Playlist.Add(aPlaylist);
                        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                    }
                }
                else
                {
                    Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == ((Guid)lbi.Tag));
                    if (aTheme != null)
                    {
                        wpnlEditorTopThemesThemes.Children.Add(bxTheme);
                        AktKlangTheme.Audio_Theme1.Add(aTheme);
                        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Einfügen deiner Playliste/Themes in ein Theme ist ein Fehler aufgetreten", ex);
            }
        }

        private void MoveItem(ListBox lb, AudioZeile audioZeile, Audio_Playlist_Titel aPlaylistTitel, int dif)
        {
            Audio_Playlist_Titel aPlaylistTitel_alt = AktKlangPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Reihenfolge == aPlaylistTitel.Reihenfolge + dif);
            aPlaylistTitel_alt.Reihenfolge = aPlaylistTitel_alt.Reihenfolge - dif;
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel_alt);

            aPlaylistTitel.Reihenfolge = aPlaylistTitel.Reihenfolge + dif;
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel);

            object selected = audioZeile;
            lb.Items.Remove(selected);
            lb.Items.Insert(aPlaylistTitel.Reihenfolge, selected);
            lb.ScrollIntoView(selected);
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                KlangZeile klZeile = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    klZeile = chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.btnMoveUp == (Button)sender);
                    if (klZeile != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null || klZeile == null)
                    return;

                Audio_Playlist_Titel aPlaylistTitel = AktKlangPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == klZeile.audiotitel.Audio_TitelGUID);
                if (aPlaylistTitel.Reihenfolge > 0)
                    MoveItem(grpobj.lbEditorListe, klZeile.audioZeile, aPlaylistTitel, -1);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken des Buttons 'btnMoveUp' ist ein Fehler aufgetreten", ex);
            }
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GruppenObjekt grpobj = null;
                KlangZeile klZeile = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte)
                {
                    klZeile = chkgrpObj._listZeile.FindAll(t => t.audioZeile != null).FirstOrDefault(t => t.audioZeile.btnMoveDown == (Button)sender);
                    if (klZeile != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null || klZeile == null)
                    return;

                Audio_Playlist_Titel aPlaylistTitel = AktKlangPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == klZeile.audiotitel.Audio_TitelGUID);
                if (aPlaylistTitel.Reihenfolge < grpobj._listZeile.Count - 1)
                    MoveItem(grpobj.lbEditorListe, klZeile.audioZeile, aPlaylistTitel, +1);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Anklicken des Buttons 'btnMoveDown' ist ein Fehler aufgetreten", ex);
            }
        }

        private void audioZeile_MouseEnter(object sender, MouseEventArgs e)
        {
            ((AudioZeile)sender).btnMoveUp.Visibility = Visibility.Visible;
            ((AudioZeile)sender).btnMoveDown.Visibility = Visibility.Visible;
            ((AudioZeile)sender).imgTrash.Visibility = Visibility.Visible;
        }

        private void audioZeile_MouseLeave(object sender, MouseEventArgs e)
        {
            ((AudioZeile)sender).btnMoveUp.Visibility = Visibility.Hidden;
            ((AudioZeile)sender).btnMoveDown.Visibility = Visibility.Hidden;
            ((AudioZeile)sender).imgTrash.Visibility = Visibility.Hidden;
        }

        private void btnBGRepeat_Click(object sender, RoutedEventArgs e)
        {
            if (_BGPlayer.AktPlaylist == null) return;
            _BGPlayer.AktPlaylist.Repeat = btnBGRepeat.IsChecked.Value;
            Global.ContextAudio.Update<Audio_Playlist>(_BGPlayer.AktPlaylist);
        }

        private void btnEditRepeat_Click(object sender, RoutedEventArgs e)
        {
            AktKlangPlaylist.Repeat = btnEditRepeat.IsChecked.Value;
            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
        }

        private void exPListPlaylists_Collapsed(object sender, RoutedEventArgs e)
        {
            if (IsInitialized)
            {
                if (!exPListPlaylists.IsExpanded && !exPListMusik.IsExpanded)
                    grdtiPList.RowDefinitions[3].Height = new GridLength(0, GridUnitType.Auto);
                else
                    grdtiPList.RowDefinitions[3].Height = new GridLength(40, GridUnitType.Star);
            }
        }

        private void tcAudioPlayer_SizeChanged(object sender, RoutedEventArgs e)
        {
            if (IsInitialized)
                exErwPlayerTheme.MaxHeight = tcAudioPlayer.ActualHeight * 2 / 3;
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

        private void lbItembtnExportTheme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
                Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == g);

                if (aTheme != null)
                {
                    string pfaddatei = ViewHelper.ChooseFile("Theme exportieren", "Theme_" + aTheme.Name.Replace("/", "_") + ".xml", true, "xml");

                    pfaddatei = pfaddatei.Replace("--", "-");
                    while (pfaddatei.EndsWith("-.xml") || pfaddatei.EndsWith(" .xml"))
                        pfaddatei = pfaddatei.Substring(0, pfaddatei.Length - 5) + ".xml";

                    ExportTheme(aTheme, pfaddatei);
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

        private void btnKlangThemeImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> dateien;
                if (btnAudioDatenImport.Tag != null)
                    dateien = btnAudioDatenImport.Tag as List<string>;
                else
                    dateien = ViewHelper.ChooseFiles("Theme importieren", "", false, "xml");
                if (dateien != null)
                {
                    try
                    {
                        bool _nicht_first = false;
                        foreach (string pfad in dateien)
                        {
                            Global.SetIsBusy(true, string.Format("Neues Theme  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + "'  wird importiert ..."));

                            XmlTextReader textReader = new XmlTextReader(pfad);
                            textReader.Read();
                            while (textReader.NodeType == XmlNodeType.XmlDeclaration ||
                                (textReader.NodeType == XmlNodeType.Element && textReader.Name == "Audio_Theme"))
                                textReader.Read();
                            if (textReader.Name == "Audio_Playlist")
                                continue;
                            if (textReader.NodeType != XmlNodeType.Comment)
                            {
                                textReader.Read();
                                XmlDocument doc = new XmlDocument();
                                XmlNode node = doc.ReadNode(textReader);
                                if (node.Attributes.Count > 0 && node.Attributes["Audio_ThemeGUID"] != null &&
                                    node.Attributes["Audio_ThemeGUID"].Value == "00000000-0000-0000-0000-00000000a11e")
                                    break;

                                if (ViewHelper.ConfirmYesNoCancel("Unsicherer Verlauf", "Theme-Import:  " + System.IO.Path.GetFileNameWithoutExtension(pfad) + Environment.NewLine + Environment.NewLine +
                                    "ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
                                   "Leider konnte dieser Prozess noch NICHT ZUVERLÄSSIG programmiert werden." + Environment.NewLine +
                                   Environment.NewLine + "Es muss damit gerechtnet werden, das die exportierte Datei" + Environment.NewLine + "NICHT MEHR IMPORTIERT werden kann!" +
                                   Environment.NewLine + Environment.NewLine + "Soll der Vorgang trotzdem fortgesetzt werden?") != 2)
                                {
                                    Global.SetIsBusy(false);
                                    return;
                                }

                                Audio_Playlist.Import(pfad, "Audio_Theme", _nicht_first);
                                _nicht_first = true;
                                Global.ContextAudio.Save();
                            }
                            else
                            {
                                string thName = "";
                                List<string> aPlayListsName = new List<string>();
                                List<string> aPlayListsGuid = new List<string>();
                                List<string> aThemesName = new List<string>();
                                List<string> aThemesGuid = new List<string>();
                                List<string> lstNotInclude = new List<string>();
                                bool nurGeräusch = false;
                                while (textReader.Read())
                                {
                                    XmlDocument doc = new XmlDocument();

                                    if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "Themename")
                                    {
                                        thName = textReader.NamespaceURI;

                                        for (int i = 0; i < textReader.AttributeCount; i++)
                                        {
                                            if (textReader.Name == "IstNurGeräuschTheme")
                                            {
                                                nurGeräusch = Convert.ToBoolean(textReader.Value);
                                                break;
                                            }
                                            textReader.MoveToNextAttribute();
                                        }

                                        textReader.Read();
                                        while (textReader.NodeType == XmlNodeType.Element &&
                                            textReader.Name.StartsWith("Playlist") || textReader.Name.StartsWith("Theme"))
                                        {
                                            XmlNode node = doc.ReadNode(textReader);
                                            if (node.Attributes.Count > 0 && node.Attributes["Name"] != null &&
                                                (node.Attributes["Audio_PlaylistGUID"] != null || node.Attributes["Audio_ThemeGUID"] != null))
                                            {
                                                if (node.Name.StartsWith("Playlist"))
                                                {
                                                    // Playlisten einlesen
                                                    aPlayListsName.Add(node.Attributes["Name"].Value);
                                                    aPlayListsGuid.Add(node.Attributes["Audio_PlaylistGUID"].Value);
                                                }
                                                else
                                                    if (node.Name.StartsWith("Theme"))
                                                    {
                                                        // UnterThemes einlesen
                                                        if (node.Attributes["Audio_ThemeGUID"].Value.ToUpper() != "00000000-0000-0000-0000-00000000A11E")
                                                        {
                                                            aThemesName.Add(node.Attributes["Name"].Value);
                                                            aThemesGuid.Add(node.Attributes["Audio_ThemeGUID"].Value);
                                                        }
                                                    }
                                            }
                                            if (textReader.NodeType == XmlNodeType.EndElement)
                                                break;
                                        }

                                        // Theme erstellen
                                        if (thName != "")
                                        {
                                            if (Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name == thName) != null)
                                            {
                                                int resp = ViewHelper.ConfirmYesNoCancel("Doppelter Theme-Name", "Ein Theme mit dem Namen '" + thName + "' ist schon vorhanden." + Environment.NewLine +
                                                    Environment.NewLine + "Soll das vorhandene Theme überschrieben werden");
                                                if (resp == 2)
                                                {
                                                    AktKlangTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name == thName);
                                                    AktKlangTheme.Audio_Playlist.Clear();
                                                    AktKlangTheme.Audio_Theme1.Clear();
                                                    AktKlangTheme.Audio_Theme2.Clear();
                                                }
                                                else
                                                    if (resp == 0)
                                                    {
                                                        Global.SetIsBusy(false);
                                                        return;
                                                    }
                                            }
                                            else
                                                NeueKlangThemeInDB(thName);

                                            AktKlangTheme.NurGeräusche = nurGeräusch;
                                            foreach (string aPlyLstGuid in aPlayListsGuid)
                                            {
                                                Audio_Playlist aPlayList = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID.ToString() == aPlyLstGuid);
                                                if (aPlayList == null)
                                                    aPlayList = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Name == aPlayListsName[aPlayListsGuid.IndexOf(aPlyLstGuid)]);

                                                if (aPlayList != null)
                                                    AktKlangTheme.Audio_Playlist.Add(aPlayList);
                                                else
                                                    lstNotInclude.Add(aPlayListsName[aPlayListsGuid.IndexOf(aPlyLstGuid)]);
                                            }

                                            foreach (string aThemeGuid in aThemesGuid)
                                            {
                                                Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID.ToString() == aThemeGuid);
                                                if (aTheme == null)
                                                    aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name != aThemesName[aThemesGuid.IndexOf(aThemeGuid)]);

                                                if (aTheme != null)
                                                    AktKlangTheme.Audio_Theme1.Add(aTheme);
                                                else
                                                    lstNotInclude.Add(aThemesName[aThemesGuid.IndexOf(aThemeGuid)]);
                                            }
                                        }
                                    }
                                }
                                Global.ContextAudio.Save();
                                if (lstNotInclude.Count > 0)
                                {
                                    string text = "";
                                    foreach (string s in lstNotInclude)
                                        text += s + Environment.NewLine;

                                    ViewHelper.Popup("ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
                                        Environment.NewLine + "Import nur teilweise durchgeführt" + Environment.NewLine + Environment.NewLine +
                                        "Von dem Theme -" + thName + "- konnten folgende Playlisten/Unterthemes leider nicht gefunden werden." +
                                        Environment.NewLine + Environment.NewLine + text + Environment.NewLine +
                                        "Bitte stellen Sie sicher, dass die Playlisten/Unterthemes integriert sind, bevor die Themes importiert werden.");
                                }
                            }
                        }
                        Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                        Global.ContextAudio.Save();

                        Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                        AktualisiereMusikPlaylist();
                        AktualisiereEditorPlaylist();
                        AktualisiereHotKeys();

                        Global.SetIsBusy(false);
                        lbEditor.SelectedIndex = -1;

                        rbEditorEditPList.IsChecked = false;
                        tcAudioPlayer.Tag = -1;

                        tiEditor_GotFocus(sender, null);
                        rbEditorKlang_Click(rbEditorKlang, null);
                        rbEditorEditPList.IsChecked = true;
                        (wpnlPListThemes.Tag as List<Guid>).Clear();

                    }
                    catch (Exception ex)
                    {
                        Global.SetIsBusy(false);
                        ViewHelper.ShowError("Beim Import des Themes ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Importieren der Theme-Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
            }
        }

        private void sortPlaylist(Audio_Playlist aPlaylist, int abPos)
        {
            int reihe = abPos;
            foreach (Audio_Playlist_Titel playlisttitel in aPlaylist.Audio_Playlist_Titel.Where(t => t.Reihenfolge >= abPos).OrderBy(t => t.Reihenfolge))
            {
                if (playlisttitel.Reihenfolge != reihe)
                {
                    playlisttitel.Reihenfolge = reihe;
                    Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel);
                }
                reihe++;
            }
        }


        private void lbEditor_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meineAudioZeile"))
                AudioZeileItemAblegen(e.Data.GetData("meineAudioZeile") as AudioZeile, AktKlangPlaylist, e);
            else
            {
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    {
                        List<string> gedroppteDateien = new List<string>((string[])e.Data.GetData(DataFormats.FileDrop, false));
                        _DateienAufnehmen(gedroppteDateien, null, AktKlangPlaylist, 0, false);
                    }
                    Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    Mouse.OverrideCursor = null;
                }
                catch (Exception ex)
                {
                    Mouse.OverrideCursor = null;
                    ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Ablegen der Dateien in der Playlist ist ein Fehler aufgetreten.", ex);
                }
            }
        }

        private void audiozeile_Drop(object sender, DragEventArgs e)
        {
            audioZeileMouseOverDropped = lbEditorListe.Items.IndexOf(((Grid)((ListBoxItem)(sender as Grid).Parent).Parent).Parent as AudioZeile);
        }

        private void brdEditorTheme_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meinListBoxItemIcon"))
            {
                ThemeItemIconAblegen(e.Data.GetData("meinListBoxItemIcon") as ListboxItemIcon);
                _dndZeilenCursor = null;
            }
        }

        private void lbitemEditor_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meineAudioZeile"))
            {
                Audio_Playlist aplaylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)((ListboxItemIcon)sender).Tag);
                AudioZeileItemAblegen(e.Data.GetData("meineAudioZeile") as AudioZeile, aplaylist, e);
            }
            else
                if (e.Data.GetDataPresent("meinListBoxItemIcon"))
                    _dndZeilenCursor = null;
        }

        private void AudioZeileItemAblegen(AudioZeile aZeile, Audio_Playlist aPlaylist, DragEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                List<string> gedroppteDateien = new List<string>();
                gedroppteDateien.Add((string)aZeile.chkTitel.Tag);

                _DateienAufnehmen(gedroppteDateien, aZeile, aPlaylist, audioZeileMouseOverDropped - 1, true);
                if (aPlaylist == AktKlangPlaylist && Keyboard.Modifiers != ModifierKeys.Control) // Verschieben in akt. Playliste
                {
                    Audio_Playlist_Titel aplytitel1 = Global.ContextAudio.PlaylistTitelListe.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)aZeile.Tag);
                    int oldReihenfolge = aplytitel1.Reihenfolge;
                    Audio_Playlist_Titel aplytitel2 = Global.ContextAudio.PlaylistTitelListe.FirstOrDefault(t => t.Reihenfolge == audioZeileMouseOverDropped);
                    aplytitel1.Reihenfolge = aplytitel2.Reihenfolge != aplytitel1.Reihenfolge ? aplytitel2.Reihenfolge - 1 : aplytitel2.Reihenfolge;

                    sortPlaylist(aPlaylist, oldReihenfolge < aplytitel1.Reihenfolge ? oldReihenfolge : aplytitel1.Reihenfolge);

                    lbEditorListe.SelectedIndex = lbEditorListe.Items.Count - 1;
                    lbEditorListe.Items.MoveCurrentToPosition(audioZeileMouseOverDropped);
                }

                if (aPlaylist != AktKlangPlaylist && Keyboard.Modifiers != ModifierKeys.Control)             //Verschieben = Löschen in akt. Playliste
                {
                    Audio_Titel aTitel = Global.ContextAudio.TitelListe.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)aZeile.Tag);
                    Global.ContextAudio.RemoveTitelFromPlaylist(AktKlangPlaylist, aTitel);
                }
                audioZeileMouseOverDropped = 0;
                _dndZeilenCursor = null;
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
            if (e.LeftButton == MouseButtonState.Pressed && !(lbEditor.Tag is ListboxItemIcon))
                lbEditor.Tag = e.GetPosition(null);
        }

        private void audiozeile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                pointerZeileDragDrop = e.GetPosition(null);
        }

        private Cursor _dndZeilenCursor = null;
        private Cursor _dndZeilenCursorPlus = null;

        private void audiozeile_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            try
            {
                if (rbEditorEditTheme.IsChecked.Value) return;

                if (e.Effects == DragDropEffects.Copy || e.Effects == DragDropEffects.Move)
                {
                    if (_dndZeilenCursor == null && (pointerZeileDragDrop != null || lbEditor.Tag != null))
                    {
                        _dndZeilenCursor = AudioPlayerView.CreateCursor(e.Source as UIElement, false);
                        _dndZeilenCursorPlus = AudioPlayerView.CreateCursor(e.Source as UIElement, true);
                    }

                    if (_dndZeilenCursor != null)
                    {
                        e.UseDefaultCursors = false;
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                            Mouse.SetCursor(_dndZeilenCursorPlus);
                        else
                            Mouse.SetCursor(_dndZeilenCursor);
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

        private void lbiEditorPlaylist_MouseMove(object sender, MouseEventArgs e)
        {
            if (lbEditor.Tag == null || lbEditor.Tag is ListboxItemIcon)
                return;

            Point mousePos = e.GetPosition(null);
            Vector diff = ((Point)lbEditor.Tag) - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed)// &&
            //(Mouse.GetPosition((AudioZeile)sender).X > 35 + 10 + ((AudioZeile)sender)._audioZeile.pbarTitel.ActualWidth))
            {
                // Initialisiere drag & drop Operation
                DataObject dragData = new DataObject("meinListBoxItemIcon", sender as ListboxItemIcon);
                lbEditor.Tag = sender as ListboxItemIcon;
                DragDrop.DoDragDrop(sender as ListboxItemIcon, dragData, DragDropEffects.All);
                lbEditor.Tag = null;
            }
        }

        private void audiozeileLbi_MouseMove(object sender, MouseEventArgs e)
        {
            AudioZeile aZeile = ((sender as ListBoxItem).Parent as Grid).Parent as AudioZeile;
            audioZeileMouseOverDropped = lbEditorListe.Items.IndexOf(aZeile);
            if (pointerZeileDragDrop == null)
                return;

            Point mousePos = e.GetPosition(null);
            Vector diff = ((Point)pointerZeileDragDrop) - mousePos;

            Point mp = Mouse.GetPosition(aZeile);
            audioZeileMouseOverDropped = lbEditorListe.Items.IndexOf(aZeile);
            //Abfrage bei gedrückter Maustaste, wenn im Vorderen Bereich und nicht auf der ProgressBar (um Teilabspielen zu editieren)
            if (e.LeftButton == MouseButtonState.Pressed &&
                (mp.X < 35 + 10 + aZeile.pbarTitel.ActualWidth) && mp.X > 0 &&
                ((mp.Y > 0 && mp.Y < aZeile.lbiEditorRow.ActualHeight / 2 - aZeile.pbarTitel.ActualHeight / 2) ||
                 (mp.Y > aZeile.lbiEditorRow.ActualHeight / 2 + aZeile.pbarTitel.ActualHeight / 2)))
            {
                // Initialisiere drag & drop Operation
                DataObject dragData = new DataObject("meineAudioZeile", aZeile);
                DragDrop.DoDragDrop(aZeile, dragData, DragDropEffects.Copy);
                pointerZeileDragDrop = null;
            }
        }

        private void rbEditorFading_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AktKlangPlaylist.Fading = rbEditorFading.IsChecked.Value;
                Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Beim Ändern der Fading-Einstellung der Playliste ist ein Fehler aufgetreten.", ex);
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            _zeile.XPos = 50;
        }
        
       /* double _XPos = 0;
        public double XPos
        {
            get { return _XPos; }
            set
            {
                _XPos = value;
                //Notify the binding that the value has changed.
                this.OnPropertyChanged("XPos");
            }
        }*/


        // We need this so that DataBindings are refreshed
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string strPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
        }

        #endregion
    
    
        private void tiDebug_GotFocus(object sender, RoutedEventArgs e)
        {
            _debugTreeview.Start();
        }

        private void _debugTreeview_Tick(object sender, EventArgs e)
        {
            tvDebug.Items.Clear();
            foreach(GruppenObjekt grpobj in _GrpObjecte)
            {
                if (grpobj.wirdAbgespielt ||
                    grpobj._listZeile.FirstOrDefault(t => t.istLaufend) != null ||
                    grpobj._listZeile.FirstOrDefault(t => t.FadingOutStarted) != null)
                {
                    TreeViewItem tvItem = null;
                    foreach (TreeViewItem tvi in tvDebug.Items)
                        if ((Guid)tvi.Tag == grpobj.aPlaylist.Audio_PlaylistGUID)
                            tvItem = tvi;
                    if (tvItem == null)
                    {
                        tvItem = new TreeViewItem();
                        tvDebug.Items.Add(tvItem);
                    }
                    tvItem.Tag = grpobj.aPlaylist.Audio_PlaylistGUID;
                    tvItem.Header = grpobj.aPlaylist.Name;

                    updateTVTitel(tvItem);
                }
            }
        }

        private void updateTVTitel(TreeViewItem tvItem)
        {
            GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == (Guid)tvItem.Tag);
            if (grpobj == null) return;

            tvItem.Items.Clear();

            foreach (KlangZeile kZeile in grpobj._listZeile.FindAll(t => t._mplayer != null).FindAll(tt => tt.istLaufend))
            {
                TreeViewItem tvi = new TreeViewItem();
                tvi.Header = Math.Round(kZeile._mplayer.Volume,4) * 100 + "% Total-Volumen  von " + kZeile.audiotitel.Volume + "% bei 100% Regelerstellung    " + kZeile.audiotitel.Audio_Titel.Name;
                tvItem.Items.Add(tvi);
            }
            tvItem.ExpandSubtree();            
        }

    }



}