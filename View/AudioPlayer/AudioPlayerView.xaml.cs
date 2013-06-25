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

public class ThemeGruppe
{
	public bool ThemeJustRead = false;
	public string ThemeName = null;
	public HitchPanel MusikPanel = new HitchPanel();
	public List<HitchPanel> KlangPanel = new List<HitchPanel>();
	public List<ThemeGruppe> UnterTheme = new List<ThemeGruppe>();
	public List<GruppenObjekt> Geraeusche = new List<GruppenObjekt>();
	public GruppenObjekt Musik = new GruppenObjekt();

	public Audio_Playlist HGThemePlaylist = new Audio_Playlist();
	public AudioTheme pnlAudioTheme = new AudioTheme();
	public Audio_Theme dbAudioTheme = new Audio_Theme();
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
	//public bool pausiert = true;
	public double totalTimePlylist = 0;
	public int Vol_ThemeMod = 100;        // Multiplikator(/100) auf den Aktuell_Volume (werte von 0 bis 200)
	public int Vol_PlaylistMod = 100;
	public DateTime LastVolUpdate = DateTime.Now;
	public int seite;
	public uint sollBtnGedrueckt = 0;
	public Guid Audio_Playlist_GUID;
	public int objGruppe;
	public UInt16 anzTitelAkt = 0;
	public UInt16 anzVolChange = 0;
	public UInt16 anzPauseChange = 0;
	public string playlistName = "";
	public UInt16 maxsongparallel = 1;
	public bool istMusik = true;
	public List<KlangZeile> _listZeile = new List<KlangZeile>();
	public bool wirdAbgespielt = false;
	public List<Guid> NochZuSpielen = new List<Guid>();
	public List<UInt16> Gespielt = new List<UInt16>();
	
	public TabItem tiKlang = null;
	public TabItemControl ticKlang = null;
	public ScrollViewer sviewer;
	public Grid grdKlang = null;
	public Grid grdKlangTop = null;
	public Button btnKlangPause = null;
	public WrapPanel wpnl = null;
	public Image btnImgKlangPause = null;

	public TextBox tbTopFilter = null;
	public Image imgTopFilter = null;

	public CheckBox chkbxTopAktiv = null;
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
}


public class Musik
{
	public bool FadingOutStarted = false;
	public bool isPaused = false;
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
}



namespace MeisterGeister.View.AudioPlayer {
	/// <summary>
	/// Interaktionslogik für AudioPlayerView.xaml
	/// </summary>
	/// 

	public partial class AudioPlayerView : UserControl
	{
		private double Zeitüberlauf = 1;
		public string stdPfad = @"C:\";
		private UInt16 tiErstellt = 0;
		private UInt16 rowErstellt = 0;
		private int fadingIntervall = 10;
		public double fadingTime = 600;    // * fadingIntervall = Übergang in ms
		private bool FadingIn_Started = false;
		private bool stopFadingIn = false;

		private int SliderTeile = 25;
		private Int16 PauseSprung = 200;
		private Int16 VolSprung = 5;
		//private string orgStackString;

		private List<Audio_Playlist_Titel> plyTitelToSave = new List<Audio_Playlist_Titel>();

		private MusikView _BGPlayer = new MusikView();
				
		private List<GruppenObjekt> _GrpObjecte = new List<GruppenObjekt>();
		private List<ThemeGruppe> _ThemeGruppe = new List<ThemeGruppe>();

		private int tcKlang_vorher, tcKlang_vorherTag;

		private Audio_Playlist AktKlangPlaylist;
		private Audio_Playlist aPlaylistLengthCheck;
		private ThemeGruppe AktThemeGruppe = null;
		private int aktiveThemeGruppe = -1;

		System.Timers.Timer BGSongTimer = new System.Timers.Timer();
		DispatcherTimer KlangPlayEndetimer;
		DispatcherTimer plyTitelToSaveTimer = new DispatcherTimer();

		DispatcherTimer KlangProgBarTimer = new DispatcherTimer();
		DispatcherTimer MusikProgBarTimer = new DispatcherTimer();

		List<HitchPanel> ThemeLstBGPanel = new List<HitchPanel>();
		List<HitchPanel> ThemeLstKlangPanel = new List<HitchPanel>();
		List<ThemeGruppe> ThemeLstThemePanel = new List<ThemeGruppe>();

		ZeileVM _zeile = new ZeileVM();

		delegate void UpdateUI();

		public AudioPlayerView()
		{
			InitializeComponent();

			_BGPlayer.BG.Add(new Musik());
			_BGPlayer.BG.Add(new Musik());

			_rbtnGleichSpielen.IsChecked = Logic.Settings.Einstellungen.AudioDirektAbspielen;
			stdPfad = MeisterGeister.Logic.Settings.Einstellungen.GetOrCreateEinstellung("AudioVerzeichnis", @"C:\");
			_tbStdPfad.Text = stdPfad;
			fadingTime = MeisterGeister.Logic.Settings.Einstellungen.GetOrCreateEinstellung("Fading", 600);

			DataContext = _zeile;

			plyTitelToSaveTimer.Tick += new EventHandler(plyTitelToSaveTimer_Tick);
			plyTitelToSaveTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

			KlangProgBarTimer.Tick += new EventHandler(KlangProgBarTimer_Tick);
			KlangProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
			KlangProgBarTimer.Tag = 0;

			MusikProgBarTimer.Tick += new EventHandler(MusikProgBarTimer_Tick);
			MusikProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
		}
		
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			_tbStdPfad.Text = MeisterGeister.Logic.Settings.Einstellungen.GetOrCreateEinstellung("AudioVerzeichnis", @"C:\");
			_rbtnGleichSpielen.IsChecked = MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen;
			_sldFading.Value = MeisterGeister.Logic.Settings.Einstellungen.Fading;
			_sldFading.ToolTip = Math.Round(_sldFading.Value / 100, 1) + " Sekunden In-/Out-Fading";
			tiKlang_Loaded(sender, null);            
		}

		private void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			if ((this.Parent as TabItemControl).Parent == null)
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
			   //     FadingIn_Started = false;
				}

				for (int i = 0; i < _GrpObjecte.Count; i++)
					AlleKlangSongsAus(GetPosObjGruppe(_GrpObjecte[i].objGruppe), true, false);

				KlangProgBarTimer.Stop();
				plyTitelToSaveTimer.Stop();
				MusikProgBarTimer.Stop();
			}
		}

		private void plyTitelToSaveTimer_Tick(object sender, EventArgs e)
		{
			plyTitelToSave.ForEach(delegate(Audio_Playlist_Titel plyTitel)
			{
				Global.ContextAudio.Update<Audio_Playlist_Titel>(plyTitel);
				plyTitelToSave.Remove(plyTitel);
			});
			if (plyTitelToSave.Count == 0)
				plyTitelToSaveTimer.Stop();
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

		public MediaPlayer PlayFile(int seite, int zeile, int posObjGruppe, MediaPlayer _player, String url, double vol, bool fading)
		{
			try
			{
				if (_player == null)
					_player = new MediaPlayer();
				try
				{
					if (seite >= 0)
					{
						_player.MediaEnded += new EventHandler(Player_Ended);
					    _player.MediaFailed += new EventHandler<ExceptionEventArgs>(Player_KlangMediaFailed);
					}
					else
					{
						_player.MediaEnded += new EventHandler(MusikPlayer_Ended);
					    _player.MediaFailed += new EventHandler<ExceptionEventArgs>(Player_MusikMediaFailed);
						_player.IsMuted = btnBGSpeaker.IsPressed;
					}
					_player.Open(new Uri(url));
                    if (posObjGruppe != -1)
					    _player.SpeedRatio = _GrpObjecte[posObjGruppe]._listZeile[zeile].playspeed;

					if (fading)
                    {
					    _player.Volume = 0;
						FadingIn(_player, (seite == -1) ? vol / 100 : (vol * (_GrpObjecte[posObjGruppe].Vol_ThemeMod / 100)) / 100);
                    }
					else
					{
						_player.Volume = (seite == -1) ? vol / 100 : (vol * (_GrpObjecte[posObjGruppe].Vol_ThemeMod / 100)) / 100;
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

		void CheckPlayStandbySongs(int posObjGruppe)
		{
			if (posObjGruppe == -1)
				return;

			int laufende = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend).FindAll(tt => tt.audioZeile.chkTitel.IsChecked.Value).Count;

			List<KlangZeile> klZeilenStandbyNichtPause = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istStandby).FindAll(t => t.istPause == false);
			int standbyNichtPausePlayable = klZeilenStandbyNichtPause.Count;

			if ((laufende == 0 && standbyNichtPausePlayable != 0) ||
			   (laufende != 0 && standbyNichtPausePlayable != 0 && _GrpObjecte[posObjGruppe].maxsongparallel > laufende))
			{

				int neueSongs;
				if (laufende == 0)
					neueSongs = _GrpObjecte[posObjGruppe].maxsongparallel;
				else
					neueSongs = _GrpObjecte[posObjGruppe].maxsongparallel - laufende;

				if (neueSongs == 0 && _GrpObjecte[posObjGruppe].maxsongparallel == 0)
					neueSongs = 1;

				for (int i = 0; i < neueSongs; i++)
				{
					if (standbyNichtPausePlayable >= 1)
					{
						if (_GrpObjecte[posObjGruppe].istMusik)
						{
							if (_GrpObjecte[posObjGruppe].NochZuSpielen.Count == 0)
							{
								for (int x = 0; x < standbyNichtPausePlayable; x++)
								{
									if (((klZeilenStandbyNichtPause[x].audioZeile.lbiKlangRow.Background != null) &&
										klZeilenStandbyNichtPause[x].audioZeile.lbiKlangRow.Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 255, 0)).ToString() &&       // Yellow
										klZeilenStandbyNichtPause[x].audioZeile.lbiKlangRow.Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString()) ||         // Red))     
										(klZeilenStandbyNichtPause[x].audioZeile.lbiKlangRow.Background == null) &&
										klZeilenStandbyNichtPause[x].audiotitel.Aktiv)
									{
										for (int t = 0; t <= klZeilenStandbyNichtPause[x].audiotitel.Rating; t++)
											_GrpObjecte[posObjGruppe].NochZuSpielen.Add(klZeilenStandbyNichtPause[x].audiotitel.Audio_TitelGUID); // .ID_Zeile);
									}
								}
							}
							if (_GrpObjecte[posObjGruppe].NochZuSpielen.Count > 0)
							{
								int neuPos = (new Random()).Next(0, _GrpObjecte[posObjGruppe].NochZuSpielen.Count - 1);
								Guid zuspielendeGuid = _GrpObjecte[posObjGruppe].NochZuSpielen[neuPos];

								int posZeile = _GrpObjecte[posObjGruppe]._listZeile.FindIndex(t => t.audiotitel.Audio_TitelGUID == zuspielendeGuid);
								_GrpObjecte[posObjGruppe]._listZeile[posZeile].istStandby = false;

								// Titel anstarten
								if (_GrpObjecte[posObjGruppe]._listZeile[posZeile].audioZeile.chkTitel != null)
									chkTitel0_0_Click(_GrpObjecte[posObjGruppe]._listZeile[posZeile].audioZeile.chkTitel, new RoutedEventArgs());
								else
									_GrpObjecte[posObjGruppe]._listZeile[posZeile].istStandby = true;

								standbyNichtPausePlayable--;
								if (neuPos < _GrpObjecte[posObjGruppe].NochZuSpielen.Count)
								{
									klZeilenStandbyNichtPause.Remove(_GrpObjecte[posObjGruppe]._listZeile[posZeile]);
									_GrpObjecte[posObjGruppe].NochZuSpielen.RemoveAll(t => t.Equals(zuspielendeGuid));
								}
							}
						}
						else
						{
							if (standbyNichtPausePlayable > 0)
							{
								int neuPos = (new Random()).Next(0, standbyNichtPausePlayable - 1);
								int zuspielendersong = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(klZeilenStandbyNichtPause[neuPos]);
								_GrpObjecte[posObjGruppe]._listZeile[zuspielendersong].istStandby = false;

								// Titel anstarten
								if (_GrpObjecte[posObjGruppe]._listZeile[zuspielendersong].audioZeile.chkTitel != null)
									chkTitel0_0_Click(_GrpObjecte[posObjGruppe]._listZeile[zuspielendersong].audioZeile.chkTitel, new RoutedEventArgs());
								else
									_GrpObjecte[posObjGruppe]._listZeile[zuspielendersong].istStandby = true;

								standbyNichtPausePlayable--;
								klZeilenStandbyNichtPause.RemoveAt(neuPos);
							}
						}
					}
				}
			}
		}

		void KlangPlayEndetimer_Tick(object sender, EventArgs e)
		{
			try
			{
				(sender as DispatcherTimer).Stop();
				UInt16 sollID_Zeile = Convert.ToUInt16((sender as DispatcherTimer).Tag);
				//string s = (sender as DispatcherTimer).Tag.ToString();
				//char[] Separator = new char[] { '_' };
				//string[] werte = s.Split(Separator, StringSplitOptions.None);

				//UInt16 zeile = Convert.ToUInt16(werte[1]);

				int posObjGruppe = -1;
				foreach (GruppenObjekt grpObj in _GrpObjecte)
				{
					if (grpObj._listZeile.FirstOrDefault(t => t.ID_Zeile == sollID_Zeile) != null)
					{
						posObjGruppe = GetPosObjGruppe(GetObjGruppe(grpObj.seite));
						break;
					}
				}
				if (posObjGruppe != -1)
				{
				int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
					_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.ID_Zeile == sollID_Zeile));
			


			//	int posObjGruppe = GetPosObjGruppe(Convert.ToInt16(werte[0]));
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Value = 0;
					if (_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.chkKlangPauseMove.IsChecked == true)
					{
						int neu = (new Random()).Next(Convert.ToUInt16(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxPauseMin.Text),
												Convert.ToUInt16(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxPauseMax.Text));

						double wertPlus = _GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldKlangPause.Ticks.First(t => t >= neu);
						int IndexPlus = _GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldKlangPause.Ticks.IndexOf(wertPlus);
						
						_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldKlangPause.Value = 
							(neu - _GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldKlangPause.Ticks[IndexPlus-1] < wertPlus - neu)?
							_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldKlangPause.Ticks[IndexPlus-1]: wertPlus;
					}

					// Song aus der Liste der laufenden Songs herausnehmen
					_GrpObjecte[posObjGruppe]._listZeile[zeile].istLaufend = false;

					// Song in die Liste der Standby-Songs aufnehmen wenn nur ein Song in Liste
					if (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istStandby).Count == 0)
					{
						_GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = true;
						CheckPlayStandbySongs(posObjGruppe);
					}
					else
					{
						// Song in die Liste der Standby-Songs aufnehmen wenn nur mehere Songs verfügbar
						// somit wird nicht 2x der gleiche Song gespielt
						CheckPlayStandbySongs(posObjGruppe);
						_GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = true;
					}
				}
			}
			catch (Exception ex)
			{
				var errWin = new MsgWindow("Playlist Fehler", "Fehler beim Überprüfen der Endewartezeit", ex);
				errWin.ShowDialog();
				errWin.Close();
			}			
		}

		void MusikPlayer_Ended(object sender, EventArgs e)
		{
			(sender as MediaPlayer).Stop();
			(sender as MediaPlayer).Close();
			lbMusiktitellist.Tag = lbMusiktitellist.SelectedIndex;			
		}

		void Player_MusikMediaFailed(object sender, ExceptionEventArgs e)
		{
			(sender as MediaPlayer).Stop();
			(sender as MediaPlayer).Close();
			MusikProgBarTimer.Stop();
			ListBoxItem lbItem = (ListBoxItem)lbMusiktitellist.SelectedItem;
			lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));      // Yellow
			lbItem.ToolTip = "Datei kann nicht abgespielt werden. Falscher oder nicht kompatibler Typ (" + (sender as MediaPlayer).Source.LocalPath + ")";
			SpieleNeuenMusikTitel(Guid.Empty);
		}

		void Player_Ended(object sender, EventArgs e)
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
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldKlangPause.Value == 0 &&
					_GrpObjecte[posObjGruppe].maxsongparallel == _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend).Count &&
					_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istStandby).Count == 0) 
					_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Position = TimeSpan.FromMilliseconds(0) ;
				else
				{
					KlangPlayEndetimer = new DispatcherTimer();
					KlangPlayEndetimer.Interval = TimeSpan.FromMilliseconds(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldKlangPause.Value);
					KlangPlayEndetimer.Tick += new EventHandler(KlangPlayEndetimer_Tick);
					KlangPlayEndetimer.Tag = _GrpObjecte[posObjGruppe]._listZeile[zeile].ID_Zeile;// objGruppe + "_" + zeile;
					KlangPlayEndetimer.Start();
				}
			}
			App.CloseSplashScreen();
		}


		void Player_KlangMediaFailed(object sender, ExceptionEventArgs e)
		{
			char[] Separator = new char[] { '_' };

			int mediahash = (sender as MediaPlayer).GetHashCode();
			int posObjGruppe = 0;

			while (posObjGruppe < _GrpObjecte.Count &&
				   !_GrpObjecte[posObjGruppe]._listZeile.Exists(t => t.mediaHashCode.Equals(mediahash)))
				posObjGruppe++;
			if (posObjGruppe < _GrpObjecte.Count)
			{
				int zeile = _GrpObjecte[posObjGruppe]._listZeile.FindIndex(t => t.mediaHashCode.Equals(mediahash));

				int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
				if (objGruppe == -1)
					return;

				_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.lbiKlangRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));
				_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.lbiKlangRow.ToolTip = "Datei kann nicht abgespielt werden (Falscher oder nicht kompatibler Typ).";
				if (_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer != null)
				{
					_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Stop();
					_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Close();
					_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer = null;
				}

				_GrpObjecte[posObjGruppe]._listZeile[zeile].istPause = false;
				_GrpObjecte[posObjGruppe]._listZeile[zeile].istLaufend = false;
				_GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = true;
				_GrpObjecte[posObjGruppe]._listZeile[zeile].playable = true;
				CheckPlayStandbySongs(posObjGruppe);
			}
		}
		
		private void slBGVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (IsInitialized && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
				_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Volume = e.NewValue / 100;
		}

		private void lbBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{            
			if (lbPListMusik.Items.Count != lbBackground.Items.Count)
				AktualisierePListPlaylist();
			if (lbPListMusik.SelectedIndex != lbBackground.SelectedIndex)
				lbPListMusik.SelectedIndex = lbBackground.SelectedIndex;

			ListBox selListBox = ((ListBox)(e.Source));
			if (selListBox.SelectedItems.Count != 0 && Convert.ToInt16(lbBackground.Tag) != selListBox.SelectedIndex)
			{
				try
				{
					if (MusikProgBarTimer != null)
					{
						MusikProgBarTimer.Stop();

						btnBGAbspielen.Tag = 1;
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
						List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(playlistliste).ToList();

						for (int i = 0; i < titel.Count; i++)
						{
							ListBoxItem lbitem = new ListBoxItem();
							lbitem.Name = "titel" + i;
							lbitem.Tag = titel[i].Audio_TitelGUID;
							lbitem.Content = titel[i].Name;

							if (!playlistliste.Audio_Playlist_Titel.First(t => t.Audio_TitelGUID == titel[i].Audio_TitelGUID).Aktiv)
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
							btnBGAbspielen.Tag = 0;
							btnBGNext.IsEnabled = true;
							imgbtnBGNext.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_next.png"));
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
									btnBGAbspielen.Tag = 1;
									btnBGAbspielen_Click(btnBGAbspielen, new RoutedEventArgs());
								}
							}

                            if (_BGPlayer.totalLength != -1)
                            {
                                aPlaylistLengthCheck = _BGPlayer.AktPlaylist;
                                _BGPlayer.totalLength = -1;
                                GetTotalLength();
                            }
						}
						else
						{
							grdSongInfo.Visibility = Visibility.Hidden;

							btnBGNext.IsEnabled = false;
							imgbtnBGNext.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_next_grau.png"));
							imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
							btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;

							btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

							btnBGAbspielen.IsEnabled = false;
							btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png"));
							btnPListMusikAbspielen.IsEnabled = btnBGAbspielen.IsEnabled;
							btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
						}
					}
					else
					{
						lbBackground.Tag = -1;
						grdSongInfo.Visibility = Visibility.Hidden;

						btnBGNext.IsEnabled = false;
						imgbtnBGNext.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_next_grau.png"));
						imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
						btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;

						btnBGAbspielen.IsEnabled = false;
						btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png"));
						btnPListMusikAbspielen.IsEnabled = btnBGAbspielen.IsEnabled;
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
					imgbtnBGNext.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_next_grau.png"));
					imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
					btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;

					btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

					btnBGAbspielen.IsEnabled = false;
					btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png"));
					btnPListMusikAbspielen.IsEnabled = btnBGAbspielen.IsEnabled;
					btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
				}
			}
		}

		private void MusikSongInfo(Visibility sichtbar)
		{
			for (int i = 0; i <= grdSongInfo.Children.Count - 1; i++)
				grdSongInfo.Children[i].Visibility = sichtbar;
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
					if (Index  != Guid.Empty)
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
			if (Convert.ToInt32(btnBGSpeaker.Tag) != -1)
			{
				slBGVolume.Value = Convert.ToInt32(btnBGSpeaker.Tag);
				btnBGSpeaker.Tag = -1;
				btnImgBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
			}
			else
			{
				double volWert = slBGVolume.Value;
				slBGVolume.Value = 0;
				btnBGSpeaker.Tag = volWert;
				btnImgBGSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker-mute.png"));
			}
			btnImgPListMusikSpeaker.Source = btnImgBGSpeaker.Source;
			slPlaylistMusikVolume.Value = slBGVolume.Value;                
		}

		private void btnBGAbspielen_Click(object sender, RoutedEventArgs e)
		{
			if (Convert.ToInt32(btnBGAbspielen.Tag) == 0 && !_BGPlayer.BG[_BGPlayer.aktiv].isPaused || 
				_BGPlayer.BG[_BGPlayer.aktiv].isPaused)
			{
				btnBGAbspielen.Tag = 0;
				if (aktiveThemeGruppe != -1)
				{
					AktThemeGruppe.pnlAudioTheme.imgPlay.Tag = 0;
					btnAudioTheme_Unchecked(AktThemeGruppe.pnlAudioTheme.btnAudioTheme, new RoutedEventArgs(Button.ClickEvent));
				}
				if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
				{
					grdSongInfo.Visibility = Visibility.Visible;
					_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.IsMuted = btnBGSpeaker.IsPressed;
					if (!_BGPlayer.BG[_BGPlayer.aktiv].isPaused)
					{
						lbMusiktitellist.SelectionChanged -= new SelectionChangedEventHandler(lbMusiktitellist_SelectionChanged);
						lbMusiktitellist.SelectedIndex = Convert.ToInt16(lbMusiktitellist.Tag);
						lbMusiktitellist.SelectionChanged += new SelectionChangedEventHandler(lbMusiktitellist_SelectionChanged);
					}
					_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = false;
					FadingIn_Started = false;
					FadingIn(_BGPlayer.BG[_BGPlayer.aktiv].mPlayer, Convert.ToDouble(_BGPlayer.AktPlaylistTitel.Volume) / 100);
					btnBGAbspielen.Tag = true;
					btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
				}
				else
					SpieleNeuenMusikTitel(Guid.Empty);

				_BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;
				btnBGStoppen.IsEnabled = true;
				btnImgBGStoppen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_stop.png"));
				btnBGPrev.IsEnabled = true;
				imgbtnBGPrev.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_prev.png"));
				imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
				btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
				btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;
				btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
			}
			else
			{
				if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null && !_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
				{
					_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
					BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], false, true);
				}
				btnBGAbspielen.Tag = true;
				btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
			}
			btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
			(lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
		}

		private void btnBGStoppen_Click(object sender, RoutedEventArgs e)
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
			imgbtnBGPrev.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_prev_grau.png"));
			imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
			btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;

			MusikProgBarTimer.Stop();
			btnBGAbspielen.Tag = 0;
			grdSongInfo.Visibility = Visibility.Hidden;
			lbMusiktitellist.SelectedIndex = -1;
			btnImgBGStoppen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_stop-grau.png"));
			btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
			if (lbPListMusik.SelectedItem != null)
				((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Visibility = Visibility.Collapsed;
			btnBGStoppen.IsEnabled = false;

			btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;
			btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
			btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
		}

		private void lbBackground_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			lbBackground_SelectionChanged(sender, null);
		}

		private void btnBGNext_Click(object sender, RoutedEventArgs e)
		{
			_BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;
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
			(lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
		}

		private void btnBGPrev_Click(object sender, RoutedEventArgs e)
		{
			_BGPlayer.BG[_BGPlayer.aktiv].isPaused = false;
			if (_BGPlayer.Gespielt.Count == 0)
				SpieleNeuenMusikTitel((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
			else
			{
				Guid vorher = _BGPlayer.Gespielt.ElementAt(_BGPlayer.Gespielt.Count - 1);
				
				SpieleNeuenMusikTitel(vorher);
				_BGPlayer.Gespielt.RemoveAt(_BGPlayer.Gespielt.Count - 1);
				lbMusiktitellist.Tag = -1;
			}
			btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
			(lbPListMusik.SelectedItem as MusikZeile).pbarSong.Visibility = Visibility.Visible;
		}
		

		private void lbKlang_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lbKlang.SelectedIndex != -1)
			{
				btnPlaylistLoeschen.IsEnabled = true;
				try
				{                    
					Mouse.OverrideCursor = Cursors.Wait;
					if (tcKlang.SelectedItem == null ||
						(tcKlang.SelectedItem.GetType() == typeof(TabItemControl) && ((TabItemControl)tcKlang.SelectedItem).Visibility != Visibility.Visible) ||
						(tcKlang.SelectedItem.GetType() == typeof(TabItem)))                    
					{
						int was = lbKlang.SelectedIndex;
						tiPlus_MouseUp(tiPlus, null);
						lbKlang.SelectionChanged -= new SelectionChangedEventHandler(lbKlang_SelectionChanged);
						lbKlang.SelectedIndex = was;
						lbKlang.SelectionChanged += new SelectionChangedEventHandler(lbKlang_SelectionChanged);
					}                    
					Int16 posObjGruppe = GetPosObjGruppe(Convert.ToInt16(((TabItemControl)tcKlang.SelectedItem).Name.Substring(7)));

					List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.
						Where(t => t.Audio_PlaylistGUID.Equals(((ListBoxItem)lbKlang.Items[lbKlang.SelectedIndex]).Tag)).ToList();
					
					for (int i = 0; i <= grdKlangPlaylistInfo.Children.Count - 1; i++)
						if (((grdKlangPlaylistInfo.Children[i] is UIElement) && (grdKlangPlaylistInfo.Children[i] as UIElement) is Border) ||
							(grdKlangPlaylistInfo.Children[i] as Control).Name != "btnKlangSave") grdKlangPlaylistInfo.Children[i].Visibility = Visibility.Visible;

					_GrpObjecte[posObjGruppe].grdKlang.Visibility = Visibility.Hidden;
					
					if (playlistliste.Count == 1)
					{
						((TabItemControl)tcKlang.SelectedItem).Tag = ((ListBoxItem)lbKlang.Items[lbKlang.SelectedIndex]).Tag;
						List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]);
						PlaylisteLeeren(posObjGruppe);
						AktKlangPlaylist = playlistliste[0];

						if (AktKlangPlaylist.Hintergrundmusik)
							rbIstMusikPlaylist.IsChecked = true;
						else
						{
							rbIstKlangPlaylist.IsChecked = true;
							brdKlangKategorie.Visibility = Visibility.Hidden;
						}

						tboxPlaylistName.Text = AktKlangPlaylist.Name;                        
						((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text = AktKlangPlaylist.Name;

						tboxKlangKategorie.Text = AktKlangPlaylist.Kategorie;
						tboxKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;

						tboxklangsongparallel.TextChanged -= new TextChangedEventHandler(tboxklangsongparallel_TextChanged);
						tboxklangsongparallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;
						tboxklangsongparallel.Text = "1";
						_GrpObjecte[posObjGruppe].istMusik = AktKlangPlaylist.Hintergrundmusik;

						_GrpObjecte[posObjGruppe].maxsongparallel = 1;

						tboxklangsongparallel.TextChanged += new TextChangedEventHandler(tboxklangsongparallel_TextChanged);

						if (titelliste.Count > 0)
						{
							_GrpObjecte[posObjGruppe].playlistName = AktKlangPlaylist.Name;
							for (UInt16 x = 0; x <= AktKlangPlaylist.Audio_Playlist_Titel.Count - 1; x++)
							{
								Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titelliste[x])[0];
								//*************** TEMP *********************
								if (playlisttitel.Audio_Titel.Pfad.StartsWith(stdPfad))
								{
									playlisttitel.Audio_Titel.Pfad = playlisttitel.Audio_Titel.Pfad.Substring(stdPfad.Length+1);
									Global.ContextAudio.Update<Audio_Titel>(playlisttitel.Audio_Titel);
								}
								//******************************************
																
								KlangNewRow(playlisttitel.Audio_Titel.Pfad, posObjGruppe, x, playlisttitel);

								if (AktKlangPlaylist.Hintergrundmusik)
								{
									if (playlisttitel.Aktiv &&
										!_GrpObjecte[posObjGruppe].NochZuSpielen.Contains(_GrpObjecte[posObjGruppe]._listZeile[x].audiotitel.Audio_TitelGUID))// .ID_Zeile))
									{
										for (int i = 0; i < _GrpObjecte[posObjGruppe]._listZeile[x].audiotitel.Rating; i++)
											_GrpObjecte[posObjGruppe].NochZuSpielen.Add(_GrpObjecte[posObjGruppe]._listZeile[x].audiotitel.Audio_TitelGUID);//.ID_Zeile);                                   
									}
								}
							}
							if (AktKlangPlaylist.Hintergrundmusik)
								ZeigeZeileKlangSpalten(posObjGruppe, false);

							_GrpObjecte[posObjGruppe].Audio_Playlist_GUID = AktKlangPlaylist.Audio_PlaylistGUID;
							_GrpObjecte[posObjGruppe].grdKlang.Visibility = Visibility.Visible;

							tboxklangsongparallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
							_GrpObjecte[posObjGruppe].maxsongparallel = Convert.ToUInt16(AktKlangPlaylist.MaxSongsParallel);

							CheckAlleAngehakt(posObjGruppe);
						}

						_GrpObjecte[posObjGruppe].grdKlang.Visibility = Visibility.Visible;
						
						if (!Convert.ToBoolean(_GrpObjecte[posObjGruppe].btnKlangPause.Tag))// .pausiert)
						{
							AlleKlangSongsAus(posObjGruppe, false, false);

							if (_GrpObjecte[posObjGruppe].istMusik)
								_GrpObjecte[posObjGruppe].btnKlangPause.Tag = true;
							_GrpObjecte[posObjGruppe].btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
						}

						if (AktKlangPlaylist.Hintergrundmusik)
						{
							ZeigeKlangSongsParallel(posObjGruppe, false);
							ZeigeKlangTop(posObjGruppe, false);
						}
						else
						{
							ZeigeKlangSongsParallel(posObjGruppe, true);
							ZeigeKlangTop(posObjGruppe, true);
						}
						
						_GrpObjecte[posObjGruppe].grdKlangTop.Visibility = (titelliste.Count != 0)? Visibility.Visible: Visibility.Hidden;

						if (!AktKlangPlaylist.Hintergrundmusik && _rbtnGleichSpielen.IsChecked == true && 
						   ((TabItem)tcAudioPlayer.SelectedItem).Name == "tiKlang")
						{
							_GrpObjecte[posObjGruppe].btnKlangPause.Tag = true;
							_GrpObjecte[posObjGruppe].btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
							_GrpObjecte[posObjGruppe].btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
						}
						tbKlangTopFilterX.Text = "";

						if (((TabItem)tcAudioPlayer.SelectedItem) == tiKlang)
							CheckBtnGleicherPfad();
					}
					else
					{
						AktKlangPlaylist = null;
						var errWin = new MsgWindow("Datenfehler", "Die Playlist-Liste konnte nicht eindeutig in der Datenbank detektiert werden.", null);
						errWin.ShowDialog();
						errWin.Close();
												
						for (int i = 0; i <= grdKlangPlaylistInfo.Children.Count - 1; i++)
							if ((grdKlangPlaylistInfo.Children[i] as Control).Name != "btnKlangSave") grdKlangPlaylistInfo.Children[i].Visibility = Visibility.Hidden;
						_GrpObjecte[posObjGruppe].grdKlangTop.Visibility = Visibility.Hidden;
					}
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}
			}
			else
				btnPlaylistLoeschen.IsEnabled = false;
		}

		private void CheckBtnGleicherPfad()
		{
			btnKlangUpdateFiles.Tag = null;
			btnKlangUpdateFiles.Visibility = Visibility.Hidden;

			List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
			if (titelliste.Count > 0)
			{
				string titelRef = titelliste[0].Pfad.LastIndexOf("\\") != -1? titelliste[0].Pfad.Substring(0, titelliste[0].Pfad.LastIndexOf("\\")) : titelliste[0].Pfad;
				titelRef = (titelRef.Substring(1, 2) != @":\") ? stdPfad + "\\" + titelRef : titelRef;

				titelliste.ForEach(delegate(Audio_Titel aTitel)
				{
					string vergleich = (aTitel.Pfad.Substring(1, 2) != @":\") ? stdPfad + "\\" + aTitel.Pfad : aTitel.Pfad;

					while (!vergleich.StartsWith(titelRef))// titelRef != vergleich.Substring(0, vergleich.Length < titelRef.Length? vergleich.Length: titelRef.Length))
					{
						if (titelRef.Contains("\\"))
							titelRef = titelRef.Substring(0, titelRef.LastIndexOf("\\"));
						else break;
					}
				});

				if (!CheckAlleTitelGleicherPfad(titelRef, titelliste))
				{
					btnKlangUpdateFiles.Tag = titelRef;
					btnKlangUpdateFiles.Visibility = Visibility.Visible;
				}
			}
		}

		private bool CheckAlleTitelGleicherPfad(string pfad, List<Audio_Titel> lstTitel)
		{
			bool pfadeGleich = true;
			lstTitel.ForEach(delegate(Audio_Titel aTitel)
			{
				if (!aTitel.Pfad.StartsWith(pfad)) pfadeGleich = false;
			});
			return pfadeGleich;
		}

		private void CheckAlleAngehakt(int posObjGruppe)
		{
			_GrpObjecte[posObjGruppe].chkbxTopAktiv.IsChecked = (_GrpObjecte[posObjGruppe]._listZeile.Count != 0 &&
				_GrpObjecte[posObjGruppe]._listZeile.Count == _GrpObjecte[posObjGruppe]._listZeile.Count(t => t.audioZeile.chkTitel.IsChecked == true))?
				true : false;

			_GrpObjecte[posObjGruppe].chkbxTopVolChange.IsChecked = (_GrpObjecte[posObjGruppe]._listZeile.Count != 0 &&
				 _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked == true).Count == 
				 _GrpObjecte[posObjGruppe].anzVolChange)? true: false;

			_GrpObjecte[posObjGruppe].chkbxTopPauseChange.IsChecked = (_GrpObjecte[posObjGruppe]._listZeile.Count != 0 &&
				_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked == true).Count ==
				_GrpObjecte[posObjGruppe].anzPauseChange)? true: false;
		}

		private void AlleKlangSongsAus(Int16 posObjGruppe, bool checkboxAus, bool ZeileLoeschen)
		{
			if (posObjGruppe == -1)
				return;

			_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).ForEach(delegate(KlangZeile kZeile)
			{
				if (checkboxAus)
				{
					kZeile.audioZeile.chkTitel.Click -= new RoutedEventHandler(chkTitel0_0_Click);
					kZeile.audioZeile.chkTitel.IsChecked = false;
				}
				if (kZeile._mplayer != null)
				{
					kZeile._mplayer.MediaEnded -= new EventHandler(Player_Ended);

					if (!_GrpObjecte[posObjGruppe].istMusik)
					{
						kZeile._mplayer.Stop();
						kZeile._mplayer.Close();
						kZeile._mplayer = null;
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
					//_GrpObjecte[posObjGruppe]._listZeile[i].audioZeile.grdKlangRow.Children.Remove(_GrpObjecte[posObjGruppe]._listZeile[i].audioZeile.imgTrash);
					//_GrpObjecte[posObjGruppe].grdKlang.Children.Remove(_GrpObjecte[posObjGruppe]._listZeile[i].lbiKlangRow);
					_GrpObjecte[posObjGruppe].wpnl.Children.Remove(kZeile.audioZeile.lbiKlangRow);
				}
			});
		}

		private Int16 GetPosObjGruppe(int objGruppe)
		{
			Int16 posObjGruppe = Convert.ToInt16(_GrpObjecte.FindIndex(t => t.objGruppe == objGruppe));
			return posObjGruppe;
		}

		private void PlaylisteLeeren(Int16 posObjGruppe)
		{
			if (posObjGruppe == -1)
				return;
			
			if (AktKlangPlaylist != null)
			{
				if (_BGPlayer.AktPlaylist == AktKlangPlaylist)
				{
					btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
					lbBackground.SelectedIndex = -1;
				}
				AlleKlangSongsAus(posObjGruppe, true, true);
				ZeigeKlangSongsParallel(posObjGruppe, false);

				if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen)
					_GrpObjecte[posObjGruppe].btnKlangPause.Tag = false;
				else
					_GrpObjecte[posObjGruppe].btnKlangPause.Tag = true;
								
			}
			if (_GrpObjecte[posObjGruppe]._listZeile.Count > 0)
			{
				_GrpObjecte[posObjGruppe].wpnl.Children.RemoveRange(0, _GrpObjecte[posObjGruppe]._listZeile.Count);
				_GrpObjecte[posObjGruppe]._listZeile.RemoveRange(0, _GrpObjecte[posObjGruppe]._listZeile.Count);
			}
			_GrpObjecte[posObjGruppe].anzTitelAkt = 0;
			_GrpObjecte[posObjGruppe].istMusik = true;
			_GrpObjecte[posObjGruppe].maxsongparallel = 1;
			_GrpObjecte[posObjGruppe].anzTitelAkt = 0;
			_GrpObjecte[posObjGruppe].anzVolChange = 0;
			_GrpObjecte[posObjGruppe].anzPauseChange = 0;
			_GrpObjecte[posObjGruppe].playlistName = tboxPlaylistName.Text;           
			_GrpObjecte[posObjGruppe].NochZuSpielen.Clear();
			_GrpObjecte[posObjGruppe].Gespielt.Clear();
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

		private void ZeigeKlangTop(Int16 posObjGruppe, bool sichtbar)
		{
			if (sichtbar)
			{
				for (int i = _GrpObjecte[posObjGruppe].grdKlangTop.ColumnDefinitions.Count - 1; i >= 4; i--)                  
					_GrpObjecte[posObjGruppe].grdKlangTop.ColumnDefinitions[i].Width = grdKlangTopX.ColumnDefinitions[i].Width;
				_GrpObjecte[posObjGruppe].brdTrennstrich.Visibility = Visibility.Visible;
			}
			else
			{
				for (int i = _GrpObjecte[posObjGruppe].grdKlangTop.ColumnDefinitions.Count - 1; i >= 4; i--)
					_GrpObjecte[posObjGruppe].grdKlangTop.ColumnDefinitions[i].Width = new GridLength(0);
				_GrpObjecte[posObjGruppe].brdTrennstrich.Visibility = Visibility.Collapsed;
			}
		}

		private void ZeigeKlangSongsParallel(Int16 posObjGruppe, bool sichtbar)
		{
			if (sichtbar && posObjGruppe != -1)
			{
				lblKlangSongsParallel.Visibility = Visibility.Visible;
				tboxklangsongparallel.Visibility = Visibility.Visible;
				btnSongParMinus.Visibility = Visibility.Visible;
				btnSongParPlus.Visibility = Visibility.Visible;

				tboxklangsongparallel.Text = Convert.ToString(_GrpObjecte[posObjGruppe].maxsongparallel);
				_GrpObjecte[posObjGruppe].btnKlangPause.Visibility = Visibility.Visible;
				if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen && ((TabItem)tcKlang.SelectedItem).Name == "tiKlang")
				{
					_GrpObjecte[posObjGruppe].sollBtnGedrueckt++;
					btnKlangPauseX_Click(_GrpObjecte[posObjGruppe].btnKlangPause, new RoutedEventArgs());
				}
				brdKlangKategorie.Visibility = Visibility.Hidden;

			}
			else
			{
				lblKlangSongsParallel.Visibility = Visibility.Hidden;
				tboxklangsongparallel.Visibility = Visibility.Hidden;
				btnSongParMinus.Visibility = Visibility.Hidden;
				btnSongParPlus.Visibility = Visibility.Hidden;
				if (posObjGruppe == -1)
					lbKlang.SelectedIndex = -1;

				brdKlangKategorie.Visibility = Visibility.Visible;
			}
		}

		private void ZeigeZeileKlangSpalten(Int16 posObjGruppe, bool sichtbar)
		{
			_GrpObjecte[posObjGruppe]._listZeile.ForEach(delegate(KlangZeile kZeile) {
				if (kZeile.audioZeile.lbiKlangRow != null)
				{
					if (!sichtbar)
					{
						if (kZeile.audioZeile.grdKlangRow.ColumnDefinitions[3].Width != new GridLength(0))
						{
							for (int i = kZeile.audioZeile.grdKlangRow.ColumnDefinitions.Count - 1; i >= 3; i--)
								kZeile.audioZeile.grdKlangRow.ColumnDefinitions[i].Width = new GridLength(0);
							kZeile.audioZeile.brdTrennstrich.Visibility = Visibility.Collapsed;
						}
					}
					else
					{
						if (kZeile.audioZeile.grdKlangRow.ColumnDefinitions[3].Width != new GridLength(_GrpObjecte[posObjGruppe].grdKlangTop.ColumnDefinitions[1].Width.Value))
						{
							for (int i = kZeile.audioZeile.grdKlangRow.ColumnDefinitions.Count - 1; i >= 3; i--)
								kZeile.audioZeile.grdKlangRow.ColumnDefinitions[i].Width = new GridLength(_GrpObjecte[posObjGruppe].grdKlangTop.ColumnDefinitions[i + 1].Width.Value);

							kZeile.audioZeile.grdKlangRow.ColumnDefinitions[2].MinWidth = _GrpObjecte[posObjGruppe].grdKlangTop.ColumnDefinitions[2].MinWidth;
							kZeile.audioZeile.brdTrennstrich.Visibility = Visibility.Visible;
						}
					}
				}
			});
			
		}

		private void KlangNewRow(string songdatei, int posObjGruppe, UInt16 row, Audio_Playlist_Titel playlisttitel)
		{
			bool neuerstellen = true;
			int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
			if (posObjGruppe == -1)
				return;

			if (((ListBoxItem)this.FindName("lbiKlangRow" + objGruppe + "_" + row)) != null)
				neuerstellen = false;
													 
			KlangZeile klZeile = new KlangZeile(rowErstellt);

			klZeile.audiotitel = playlisttitel;
			klZeile._mplayer = new MediaPlayer();
			klZeile.mediaHashCode = klZeile._mplayer.GetHashCode();


			klZeile.audioZeile = new AudioZeile();
			klZeile.audioZeile.Name = "audioZeile" + objGruppe + "_" + row;
			klZeile.audioZeile.Tag = klZeile.ID_Zeile;
						
			klZeile.audioZeile.lbiKlangRow.Tag = rowErstellt;

			//*************************************************************************************************
			//Papierkorb
			klZeile.audioZeile.imgTrash.Tag = playlisttitel.Audio_Titel.Audio_TitelGUID;           
			klZeile.audioZeile.chkTitel.Content = System.IO.Path.GetFileNameWithoutExtension(songdatei);
			
			if (playlisttitel.Aktiv)
				klZeile.audioZeile.chkTitel.IsChecked = playlisttitel.Aktiv;

			klZeile.audioZeile.chkTitel.Tag = (System.IO.Path.GetDirectoryName(songdatei).StartsWith(stdPfad)) ? 
				songdatei.Substring(stdPfad.Length) : songdatei;
			klZeile.audioZeile.chkTitel.ToolTip = songdatei;
						
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

			klZeile.audioZeile.grdKlangRow.Name += objGruppe + "_" + row;
			klZeile.audioZeile.lbiKlangRow.Name += objGruppe + "_" + row;            
			klZeile.audioZeile.imgTrash.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile.chkTitel.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile.sldKlangVol.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile.chkVolMove.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile._btnVolMinMinus.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile._btnVolMinPlus.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile.tboxVolMin.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile._btnVolMaxMinus.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile._btnVolMaxPlus.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile.tboxVolMax.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile.sldKlangPause.Name += objGruppe + "_" + row;
			klZeile.audioZeile.chkKlangPauseMove.Name +=  objGruppe + "_" + row;
			klZeile.audioZeile._btnPauseMinMinus.Name += objGruppe + "_" + row;
			klZeile.audioZeile._btnPauseMinPlus.Name += objGruppe + "_" + row;
			klZeile.audioZeile.tboxPauseMin.Name += objGruppe + "_" + row;
			klZeile.audioZeile._btnPauseMaxMinus.Name += objGruppe + "_" + row;
			klZeile.audioZeile._btnPauseMaxPlus.Name += objGruppe + "_" + row;
			klZeile.audioZeile.tboxPauseMin.Name += objGruppe + "_" + row;
			klZeile.audioZeile.tboxPauseMax.Name += objGruppe + "_" + row;
			klZeile.audioZeile.sldPlaySpeed.Name += objGruppe + "_" + row;
			
			if (neuerstellen)
			{
				klZeile.audioZeile.imgTrash.MouseUp += new MouseButtonEventHandler(imgTrash0_0_MouseUp);
				klZeile.audioZeile.chkTitel.Click += new RoutedEventHandler(chkTitel0_0_Click);
				klZeile.audioZeile.sldKlangVol.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldKlangVol0_X_ValueChanged);
				//klZeile.audioZeile.sldKlangVol.MouseWheel += new MouseWheelEventHandler(slVolume_MouseWheel);
				klZeile.audioZeile.chkVolMove.Click += new RoutedEventHandler(chkVolMove0_0_Click);
				klZeile.audioZeile._btnVolMinMinus.Click += new RoutedEventHandler(_btnVolMinMinus0_X_Click);
				klZeile.audioZeile._btnVolMinPlus.Click += new RoutedEventHandler(_btnVolMinPlus0_X_Click);
				klZeile.audioZeile.tboxVolMin.PreviewTextInput += new TextCompositionEventHandler(tboxVolMin0_X_PreviewTextInput);
				klZeile.audioZeile.tboxVolMin.LostFocus += new RoutedEventHandler(tboxVolMin0_X_LostFocus);
				klZeile.audioZeile._btnVolMaxMinus.Click += new RoutedEventHandler(_btnVolMaxMinus0_X_Click);
				klZeile.audioZeile._btnVolMaxPlus.Click += new RoutedEventHandler(_btnVolMaxPlus0_X_Click);
				klZeile.audioZeile.tboxVolMax.PreviewTextInput += new TextCompositionEventHandler(tboxVolMin0_X_PreviewTextInput);
				klZeile.audioZeile.tboxVolMax.LostFocus += new RoutedEventHandler(tboxVolMax0_X_LostFocus);
				klZeile.audioZeile.sldKlangPause.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldKlangPause0_X_ValueChanged);
				//klZeile.audioZeile.sldKlangPause.MouseWheel += new MouseWheelEventHandler(sldPause_MouseWheel);
				klZeile.audioZeile.chkKlangPauseMove.Click += new RoutedEventHandler(chkKlangPauseMove0_0_Click);
				klZeile.audioZeile._btnPauseMinMinus.Click += new RoutedEventHandler(_btnPauseMinMinus0_X_Click);
				klZeile.audioZeile._btnPauseMinPlus.Click += new RoutedEventHandler(_btnPauseMinPlus0_X_Click);
				klZeile.audioZeile.tboxPauseMin.PreviewTextInput += new TextCompositionEventHandler(tboxVolMin0_X_PreviewTextInput);
				klZeile.audioZeile._btnPauseMaxMinus.Click += new RoutedEventHandler(_btnPauseMaxMinus0_X_Click);
				klZeile.audioZeile._btnPauseMaxPlus.Click += new RoutedEventHandler(_btnPauseMaxPlus0_X_Click);
				klZeile.audioZeile.tboxPauseMin.LostFocus += new RoutedEventHandler(tboxPauseMin0_X_LostFocus);
				klZeile.audioZeile.tboxPauseMax.PreviewTextInput += new TextCompositionEventHandler(tboxVolMin0_X_PreviewTextInput);
				klZeile.audioZeile.tboxPauseMax.LostFocus += new RoutedEventHandler(tboxPauseMax0_X_LostFocus);
				klZeile.audioZeile.sldPlaySpeed.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldPlaySpeed0_X_ValueChanged);
			}

			_GrpObjecte[posObjGruppe].wpnl.Children.Add(klZeile.audioZeile);
			//*************************************************************************************************
            
			klZeile.pauseMin_wert = Convert.ToInt16(klZeile.audioZeile.tboxPauseMin.Text);
			klZeile.pauseMax_wert = Convert.ToInt16(klZeile.audioZeile.tboxPauseMax.Text);
			klZeile.volMin_wert = Convert.ToInt16(klZeile.audioZeile.tboxVolMin.Text);
			klZeile.volMax_wert = (Convert.ToInt16(klZeile.audioZeile.tboxVolMax.Text) >= klZeile.volMin_wert) ?
				Convert.ToInt16(klZeile.audioZeile.tboxVolMax.Text) : klZeile.volMin_wert;
			klZeile.Aktuell_Volume = playlisttitel.Volume;
			klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
				(klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

			klZeile.playspeed = klZeile.audioZeile.sldPlaySpeed.Value;

			if (playlisttitel.Aktiv && !_GrpObjecte[posObjGruppe].istMusik)
				klZeile.istStandby = true;
			else
				klZeile.istStandby = false;

			if (Convert.ToBoolean(_GrpObjecte[posObjGruppe].btnKlangPause.Tag))
				klZeile.istPause = true;

			klZeile.playable = klZeile.audioZeile.chkTitel.IsChecked.Value;

			_GrpObjecte[posObjGruppe]._listZeile.Add(klZeile);
			if (klZeile.audioZeile.chkTitel.IsChecked == true) _GrpObjecte[posObjGruppe].anzTitelAkt++;
			if (klZeile.audioZeile.chkVolMove.IsChecked == true) _GrpObjecte[posObjGruppe].anzVolChange++;
			if (klZeile.audioZeile.chkKlangPauseMove.IsChecked == true) _GrpObjecte[posObjGruppe].anzPauseChange++;
			rowErstellt++;
		}

		private void chkTitel0_0_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = -1;
			foreach (GruppenObjekt grpObj in _GrpObjecte)
			{
				if (grpObj._listZeile.FirstOrDefault(t => t.audioZeile.chkTitel == (CheckBox)sender) != null)
				{
					posObjGruppe = GetPosObjGruppe(GetObjGruppe(grpObj.seite));
					break;
				}
			}
			if (posObjGruppe == -1)
				return;

			_GrpObjecte[posObjGruppe].wirdAbgespielt = false;
			int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
				_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.chkTitel == (Control)sender));
			
			string file = (sender as CheckBox).Tag.ToString();
			if (file.Substring(1, 2) != @":\")
			{
				if (stdPfad.EndsWith(@"\"))
					file = stdPfad + file;
				else
					file = stdPfad + "\\" + file;
			}

			if (e.Source != null)
			{
				if ((sender as CheckBox).IsChecked == true)
					_GrpObjecte[posObjGruppe].anzTitelAkt++;
				else
					_GrpObjecte[posObjGruppe].anzTitelAkt--;
			}

			if (!_GrpObjecte[posObjGruppe]._listZeile[zeile].istPause)
			{
				if (!File.Exists(file))
				{
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.lbiKlangRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));       // Red))
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.lbiKlangRow.ToolTip = "Datei nicht gefunden";
					_GrpObjecte[posObjGruppe]._listZeile[zeile].playable = false;
					_GrpObjecte[posObjGruppe]._listZeile[zeile].istLaufend = false;
					_GrpObjecte[posObjGruppe].NochZuSpielen.RemoveAll(t => t.Equals(_GrpObjecte[posObjGruppe]._listZeile[zeile].ID_Zeile));  //zeile
					CheckPlayStandbySongs(posObjGruppe);
				}
				else
				{
					_GrpObjecte[posObjGruppe]._listZeile[zeile].playable = true;
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.lbiKlangRow.Background = null;
					if ((sender as CheckBox).IsChecked.Value == true)
					{
						if (_GrpObjecte[posObjGruppe].maxsongparallel > _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend == true).Count)
						{
							if (_GrpObjecte[posObjGruppe].istMusik)
								_GrpObjecte[posObjGruppe]._listZeile[zeile].FadingOutStarted = false;
							_GrpObjecte[posObjGruppe].wirdAbgespielt = true;
							_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer =
								PlayFile(_GrpObjecte[posObjGruppe].seite, zeile, posObjGruppe, _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer, file,
									_GrpObjecte[posObjGruppe]._listZeile[zeile].Aktuell_Volume, _GrpObjecte[posObjGruppe].istMusik);
							_GrpObjecte[posObjGruppe]._listZeile[zeile].mediaHashCode = _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.GetHashCode();

							if (_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.NaturalDuration.HasTimeSpan)
								_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Maximum =
									_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;
							else
								_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Maximum = 100000;

							if (aktiveThemeGruppe >= 0 && _ThemeGruppe[aktiveThemeGruppe].Musik != null &&
								_GrpObjecte[posObjGruppe].playlistName == _ThemeGruppe[aktiveThemeGruppe].Musik.playlistName)
								_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.pbarActBGTitel.Maximum = _GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Maximum;

							if (e.Source != null) _GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = false;
							_GrpObjecte[posObjGruppe]._listZeile[zeile].istLaufend = true;
							_GrpObjecte[posObjGruppe]._listZeile[zeile].istPause = false;
						}
						else
							_GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = true;
					}
					else
					{
						if (_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer != null)
						{
							if (_GrpObjecte[posObjGruppe].istMusik)
							{
								if (!_GrpObjecte[posObjGruppe]._listZeile[zeile].FadingOutStarted)
								{
									_GrpObjecte[posObjGruppe]._listZeile[zeile].FadingOutStarted = true;
									FadingOut(_GrpObjecte[posObjGruppe]._listZeile[zeile], true, true);
								}
							}
							else
							{
								_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Stop();
								_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Close();
								_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer = null;
							}
							_GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = false;
							_GrpObjecte[posObjGruppe]._listZeile[zeile].istLaufend = false;
							_GrpObjecte[posObjGruppe]._listZeile[zeile].istPause = false;

						}
						_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Maximum = 100;
						_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.pbarTitel.Value = 0;

						if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen)
							CheckPlayStandbySongs(posObjGruppe);
					}
					if (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend).Count > 0)
					{
						KlangProgBarTimer.IsEnabled = true;
						KlangProgBarTimer.Start();
					}
					else
					{
						for (int i = 0; i < _GrpObjecte.Count; i++)
						{
							if (_GrpObjecte[i]._listZeile.FindAll(t => t.istLaufend).Count > 0)
							{
								KlangProgBarTimer.IsEnabled = true;
								KlangProgBarTimer.Start();
								break;
							}
							else
							{
								KlangProgBarTimer.IsEnabled = false;
								KlangProgBarTimer.Stop();
							}
						}
					}
				}
			}
			else
			{
				if ((sender as CheckBox).IsChecked.Value == true)
				{
					if (!_GrpObjecte[posObjGruppe].NochZuSpielen.Contains(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Audio_TitelGUID))//.ID_Zeile))
					{
						for (int i = 0; i < _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Rating; i++)
							_GrpObjecte[posObjGruppe].NochZuSpielen.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Audio_TitelGUID);//.ID_Zeile);
					}
				}
				else
					_GrpObjecte[posObjGruppe].NochZuSpielen.RemoveAll(t => t.Equals(_GrpObjecte[posObjGruppe]._listZeile[zeile].ID_Zeile));  //zeile
			}

            string cap = _GrpObjecte[posObjGruppe].playlistName;

			Audio_Playlist playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(cap)).First();
			if (playlistliste != null)
			{
				Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(playlistliste,
					Global.ContextAudio.LoadTitelByPlaylist(playlistliste)[zeile]).FirstOrDefault();

				if (playlisttitel != null)
				{
					playlisttitel.Aktiv = (sender as CheckBox).IsChecked.Value;

					plyTitelToSave.Add(playlisttitel);
					if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
				}
				
			}

			CheckAlleAngehakt(posObjGruppe);
		}

		private void NeueKlangPlaylistInDB()
		{
			string NeuePlaylist = ((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text;
			
			Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
			playlist.MaxSongsParallel = 1;
			playlist.Name = NeuePlaylist.ToString();
			if (rbIstKlangPlaylist.IsChecked == true)
				playlist.Hintergrundmusik = false;
			else
				playlist.Hintergrundmusik = true;

			//zur datenbank hinzufügen
			if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
				AktKlangPlaylist = playlist;
			if (playlist.Hintergrundmusik)
				rbIstMusikPlaylist.IsChecked = true;
			else
				rbIstKlangPlaylist.IsChecked = true;

			AktualisiereKlangPlaylist();
		}

		private Guid KlangDateiHinzu(string datei)
		{            
			//erstelle ein leeres Titel-Objekt
			Audio_Titel titel = Global.ContextAudio.New<Audio_Titel>();
			//eigenschaften setzen
			titel.Name = System.IO.Path.GetFileNameWithoutExtension(datei);
			if (datei.StartsWith(stdPfad))
				titel.Pfad = datei.Substring(stdPfad.Length + 1);
			else
				titel.Pfad = datei;
			
			//zur datenbank hinzufügen
			if (Global.ContextAudio.Insert<Audio_Titel>(titel))
			{
				Global.ContextAudio.AddTitelToPlaylist(AktKlangPlaylist, titel);
				Int16 posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));

				tboxklangsongparallel.Tag = _GrpObjecte[posObjGruppe]._listZeile.Count + 1;

				Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel).First();
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

					KlangNewRow(datei, posObjGruppe, Convert.ToUInt16(_GrpObjecte[posObjGruppe]._listZeile.Count), playlisttitel);
					if (playlisttitel.Aktiv)
					{
						for (int i = 0; i < playlisttitel.Rating; i++)
							_GrpObjecte[posObjGruppe].NochZuSpielen.Add(_GrpObjecte[posObjGruppe]._listZeile[_GrpObjecte[posObjGruppe]._listZeile.Count].audiotitel.Audio_TitelGUID);//.ID_Zeile);
					}

					if (AktKlangPlaylist.Hintergrundmusik)
						ZeigeZeileKlangSpalten(posObjGruppe, false);
				}
			}
			return titel.Audio_TitelGUID;
		}

		private void grdKlangX_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effects = DragDropEffects.Copy;
			else
				e.Effects = DragDropEffects.None;
		}

		private void grdKlangX_Drop(object sender, DragEventArgs e)
		{
			bool hinzugefuegt = false;
			bool neu = false;
			try
			{
				Mouse.OverrideCursor = Cursors.Wait;                
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					if (AktKlangPlaylist == null)
					{
						neu = true;
						NeueKlangPlaylistInDB();
					}
					
					string[] gedroppteDateien = (string[])e.Data.GetData(DataFormats.FileDrop, false);
					string[] extension = new String[4] { ".mp3", ".wav", ".ogg", ".wma" };
					
					foreach (string droppedFilePath in gedroppteDateien)
					{
						if (Array.IndexOf(extension, droppedFilePath.Substring(droppedFilePath.Length - 4)) != -1)
						{
							if (neu || _GrpObjecte[Convert.ToInt16(tcKlang.Tag)].Audio_Playlist_GUID.Equals(Guid.Empty))
							{
								KlangDateiHinzu(droppedFilePath);
								hinzugefuegt = true;
							}
							Guid g = Guid.Empty; 
							Audio_Playlist altAktKlangPlaylist = AktKlangPlaylist;
							
							// check gleiche Playlists im Hintergrund und aktualisieren
							_GrpObjecte.FindAll(t => t.Audio_Playlist_GUID == AktKlangPlaylist.Audio_PlaylistGUID).ForEach(delegate(GruppenObjekt grpObj){
								int altIndex = tcKlang.SelectedIndex;
								AktKlangPlaylist = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(grpObj.Audio_Playlist_GUID)).FirstOrDefault();
								tcKlang.SelectedIndex = grpObj.seite;
								g = KlangDateiHinzu(droppedFilePath);
								AktKlangPlaylist = altAktKlangPlaylist;
								tcKlang.SelectedIndex = altIndex;
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
					Int16 posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
					_GrpObjecte[posObjGruppe].grdKlangTop.Visibility = Visibility.Visible;


					if (rbIstKlangPlaylist.IsChecked == true)
						AktKlangPlaylist.Hintergrundmusik = false;
					else
						AktKlangPlaylist.Hintergrundmusik = true;

					if (AktKlangPlaylist.Hintergrundmusik)
					{
						ZeigeKlangSongsParallel(posObjGruppe, false);
						ZeigeKlangTop(posObjGruppe, false);
					}
					else
					{
						ZeigeKlangSongsParallel(posObjGruppe, true);
						ZeigeKlangTop(posObjGruppe, true);
					}
					CheckAlleAngehakt(posObjGruppe);
					_GrpObjecte[posObjGruppe].grdKlang.Visibility = Visibility.Visible;
					_GrpObjecte[posObjGruppe].Audio_Playlist_GUID = AktKlangPlaylist.Audio_PlaylistGUID;
					Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
				}
			}
			finally
			{
				Mouse.OverrideCursor = null;
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
				List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(AktPlaylist.Audio_PlaylistGUID)).ToList();
				if (playlistliste.Count == 0)
				{
					Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
					playlist.Name = NeuerPlaylistName;
					playlist.Hintergrundmusik = false;
					playlist.MaxSongsParallel = Convert.ToInt32(tboxklangsongparallel.Text);

					//zur datenbank hinzufügen
					if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
					{
						List<Audio_Titel> titelMitNeuImNamen = Global.ContextAudio.TitelListe.Where(t => t.Name.StartsWith("Neu")).ToList();
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
	
		private void tboxKategorie_LostFocus(object sender, RoutedEventArgs e)
		{
			Audio_Playlist aPlyLst = Global.ContextAudio.PlaylistListe.Find(t => t.Audio_PlaylistGUID == ((Guid)((TextBox)e.Source).Tag));
			if (aPlyLst != null)
			{
				aPlyLst.Kategorie = ((TextBox)e.Source).Text;
				Global.ContextAudio.Update<Audio_Playlist>(aPlyLst);
			}
		}

		private void AktualisiereMusikPlaylist()
		{
			UInt16 pos = 0;
			List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.ToList();
			for (int i = 0; i < playlistliste.Count; i++)
			{
				if (playlistliste[i].Hintergrundmusik)
				{
					List<Audio_Titel> s = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[i]).ToList();
					if (s.Count == 1 && lbBackground.SelectedIndex != -1 &&
						s != lbBackground.Items[lbBackground.SelectedIndex])
						lbBackground.SelectedIndex = -1;

					if (pos + 1 > lbBackground.Items.Count)
					{
						MusikZeile mZeile = new MusikZeile();
						mZeile.Name = "titel" + i;
						mZeile.Cursor = Cursors.Hand;
						mZeile.Tag = playlistliste[i].Audio_PlaylistGUID;
						mZeile.tblkTitel.Text = playlistliste[i].Name;
						mZeile.tblkLänge.Text = (playlistliste[i].Länge != 0)? TimeSpan.FromMilliseconds(playlistliste[i].Länge).ToString(@"hh\:mm\:ss"): "";
						mZeile.tboxKategorie.Tag = mZeile.Tag;
						mZeile.tboxKategorie.Text = playlistliste[i].Kategorie;
						mZeile.tboxKategorie.LostFocus += new RoutedEventHandler(tboxKategorie_LostFocus);

						lbBackground.Items.Add(mZeile);
					}
					else
					{
						((MusikZeile)lbBackground.Items[pos]).Name = "titel" + i;
						((MusikZeile)(lbBackground.Items[pos])).Tag = playlistliste[i].Audio_PlaylistGUID;
						if (((MusikZeile)(lbBackground.Items[pos])).tblkTitel.Text != playlistliste[i].Name)
							((MusikZeile)(lbBackground.Items[pos])).tblkTitel.Text = playlistliste[i].Name;
						if (((MusikZeile)(lbBackground.Items[pos])).tboxKategorie.Text != playlistliste[i].Kategorie)
							((MusikZeile)(lbBackground.Items[pos])).tboxKategorie.Text = playlistliste[i].Kategorie;
					}
					pos++;
				}
			}
			if (lbBackground.Items.Count != 0)
			{
                while (lbBackground.Items.Count > pos)
                    lbBackground.Items.RemoveAt(lbBackground.Items.Count-1);
			}
		}
		
		private void AktualisiereKlangPlaylist()
		{
			lbKlang.Items.Clear();
			List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.ToList();
			for (int i = 0; i < playlistliste.Count; i++)
			{
				if ((playlistliste[i].Hintergrundmusik) && (rbKlangMusik.IsChecked == true || rbKlangAlle.IsChecked == true))
				{
					ListBoxItem lbitem = new ListBoxItem();
					lbitem.Name = "titel" + i;
					lbitem.Tag = playlistliste[i].Audio_PlaylistGUID;
					lbitem.Content = playlistliste[i].Name;
					lbKlang.Items.Add(lbitem);
				}
				if ((playlistliste[i].Hintergrundmusik == false) && (rbKlangAlle.IsChecked == true || rbKlangKlang.IsChecked == true))
				{
					ListBoxItem lbitem = new ListBoxItem();
					lbitem.Name = "titel" + i;
					lbitem.Tag = playlistliste[i].Audio_PlaylistGUID;
					lbitem.Content = playlistliste[i].Name;
					lbKlang.Items.Add(lbitem);
				}
			}
		}

		private void bntNeuePlaylist_Click(object sender, RoutedEventArgs e)
		{
			string NeuePlaylist = "NeuePlayliste";
			int ver = 0;
			string[] str_tiHeader = new string[tcKlang.Items.Count - 2];

			for (int i = 0; i < tcKlang.Items.Count - 2; i++)
				str_tiHeader[i] = (i == 0) ? ((TabItem)tcKlang.Items[i]).Header.ToString() : ((TabItemControl)tcKlang.Items[i])._textBlockTitel.Text;

			Audio_Playlist playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuePlaylist));
			while (playlistlist != null) 
			{
				NeuePlaylist = "NeuePlayliste-" + ver;
				ver++;
				playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuePlaylist));
			}

			if (playlistlist == null)
			{
				Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
				playlist.Name = NeuePlaylist.ToString();
				if (rbKlangKlang.IsChecked.Value)
					playlist.Hintergrundmusik = false;
				else
					playlist.Hintergrundmusik = true;

				//zur datenbank hinzufügen
				if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
				{
					AktKlangPlaylist = playlist;
					tboxklangsongparallel.Text = "1";
					tboxklangsongparallel.Tag = null;
					playlist.MaxSongsParallel = 1;
					AktualisiereKlangPlaylist();
					for (int i = 0; i <= lbKlang.Items.Count - 1; i++)
						if ((lbKlang.Items[i] as ListBoxItem).Content.ToString() == playlist.Name)
							lbKlang.SelectedIndex = i;

					tboxPlaylistName.Background = Brushes.LightSalmon;
					tboxPlaylistName.Focus();
				}
			}
			else
			{
				var errWin = new MsgWindow("Datenbankfehler", "Playlist schon vorhanden. Bitte wiederholen Sie den Vorgang und wählen einen anderen Titel");
				errWin.ShowDialog();
				errWin.Close();
			}
		}

		private void tiMusik_Loaded(object sender, RoutedEventArgs e)
		{
			if (lbBackground.Items.Count == 0)
				AktualisiereMusikPlaylist();
		}

		private void tiKlang_Loaded(object sender, RoutedEventArgs e)
		{
			if (lbKlang.Items.Count == 0)
				AktualisiereKlangPlaylist();
			if (tcKlang.SelectedItem != null)
			{
				if (tcKlang.SelectedItem.GetType() == typeof(TabItem))
					tiPlus_MouseUp(null, null);
				else
					if (tcKlang.SelectedItem.GetType() == typeof(TabItemControl) && 
						((TabItemControl)tcKlang.SelectedItem).Visibility != Visibility.Visible)
					{
						tcKlang.SelectedIndex = -1;
						ZeigeKlangGerneral(-1, false);
						tiPlus_MouseUp(null, null);
					}
				if (!rbIstMusikPlaylist.IsChecked.Value && !rbIstKlangPlaylist.IsChecked.Value)
					rbIstMusikPlaylist.IsChecked = true;
			}
			btnKlangUpdateFiles.Visibility = Visibility.Collapsed;
		}

		private void lbMusiktitellist_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((lbMusiktitellist.SelectedIndex >= 0) &&
			   (((ListBoxItem)lbMusiktitellist.SelectedItem).Background.ToString() != new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)).ToString()))         // Red))
			{
				if (lbBackground.SelectedIndex == -1)
				{
					lbBackground.SelectionChanged -= new SelectionChangedEventHandler(lbBackground_SelectionChanged);
					lbBackground.SelectedIndex = Convert.ToInt16(lbBackground.Tag);
					lbBackground.SelectionChanged += new SelectionChangedEventHandler(lbBackground_SelectionChanged);
				}
				chkbxPlayRange.IsChecked = false;
				rsldTeilSong.Visibility = Visibility.Hidden;
				rsldTeilSong.LowerValue = 0;
				rsldTeilSong.UpperValue = 100000;

				ListBoxItem lbItem = (ListBoxItem)lbMusiktitellist.SelectedItem;
				string st = lbItem.Tag.ToString();

				Audio_Titel titel = Global.ContextAudio.LoadTitelByPlaylist(_BGPlayer.AktPlaylist).FirstOrDefault(t => t.Audio_TitelGUID == (Guid)lbItem.Tag);

				if (titel == null)
				{
					//if (!File.Exists(titel[lbMusiktitellist.SelectedIndex].Pfad))
					lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
					lbItem.ToolTip = "Datei nicht gefunden";
					
					lbBackground_SelectionChanged(lbMusiktitellist, e);
					lbMusiktitellist.Tag = -1;
					btnBGPrev.IsEnabled = false;
					imgbtnBGPrev.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_prev_grau.png"));
					imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
					btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
				}
				else
				{
					string file = titel.Pfad;
					if (file.Substring(1, 2) != @":\")
					{
						if (stdPfad.EndsWith(@"\"))
							file = stdPfad + file;
						else
							file = stdPfad + "\\" + file;
					}

					if (!File.Exists(file))
					{
						lbItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
						lbItem.ToolTip = "Datei nicht gefunden";
						lbMusiktitellist.Tag = -1;
						btnBGPrev.IsEnabled = false;
						imgbtnBGPrev.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_prev_grau.png"));
						imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
						btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
						SpieleNeuenMusikTitel(Guid.Empty);
					}
					else
					{
						if (File.Exists(file))
						{
							grdSongInfo.Visibility = Visibility.Visible;
							if (aktiveThemeGruppe != -1)
							{
								AktThemeGruppe.pnlAudioTheme.imgPlay.Tag = 0;
								btnAudioTheme_Unchecked(AktThemeGruppe.pnlAudioTheme.btnAudioTheme, new RoutedEventArgs(Button.ClickEvent));
							}
							if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
							{
								if (!_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
								{
									_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
									BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], true, false);
								}
								_BGPlayer.aktiv = (_BGPlayer.aktiv == 0) ? 1 : 0;
								if (_BGPlayer.BG[0].FadingOutStarted && _BGPlayer.BG[1].FadingOutStarted)
									_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = false;
								MusikProgBarTimer.Stop();
							}
							lblBgTimeMax.Content = "--:--";
							lblBgTitel.Content = "";
							lblBgAlbum.Content = "";
							lblBgArtist.Content = "";
							lblBgJahr.Content = "";
							lblBgGenre.Content = "";

							_BGPlayer.AktPlaylistTitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(_BGPlayer.AktPlaylist, titel).First();
							chkbxPlayRange.IsChecked = _BGPlayer.AktPlaylistTitel.TeilAbspielen;

							_BGPlayer.BG[_BGPlayer.aktiv].mPlayer = PlayFile(-1, 0, -1, _BGPlayer.BG[_BGPlayer.aktiv].mPlayer, file, slBGVolume.Value, true);
							btnBGPrev.IsEnabled = true;
							imgbtnBGPrev.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_prev.png"));
							imgbtnPListMusikPrev.Source = imgbtnBGPrev.Source;
							btnBGStoppen.IsEnabled = true;
							btnImgBGStoppen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_stop.png"));

							btnPListMusikPrev.IsEnabled = btnBGPrev.IsEnabled;
							btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
							btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;
							
							if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
							{
								btnBGAbspielen.Tag = 1;
								btnImgBGAbspielen.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
								pbarBGSong.Value = 0;
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
									((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Maximum = pbarBGSong.Maximum;
									lblBgTimeMax.Content = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
								}
								btnBGNext.IsEnabled = true;
								imgbtnBGNext.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_next.png"));
								imgbtnPListMusikNext.Source = imgbtnBGNext.Source;
								btnBGAbspielen.IsEnabled = true;
								btnPListMusikNext.IsEnabled = btnBGNext.IsEnabled;
								btnPListMusikAbspielen.IsEnabled = btnBGAbspielen.IsEnabled;
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
			bool found = false;
			KlangProgBarTimer.Tag = (KlangProgBarTimer.Tag.ToString() == "0") ? "1" : "0";

			for (int posObjGruppe = 0; posObjGruppe < _GrpObjecte.Count; posObjGruppe++)
			{
				List<KlangZeile> KlangZeilenLaufend = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);
				
				if (KlangZeilenLaufend.Count != 0)
				{
					found = true;
					for (int durchlauf = 0; durchlauf < KlangZeilenLaufend.Count; durchlauf++)
					{
						if (KlangZeilenLaufend[durchlauf].istPause)
							continue;

						int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
						if (objGruppe == -1)
							break;

						if (KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel != null &&
							KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Tag != null)
						{
							//keine Informationen nach 1 Sekunde vom MediaPlayer über Track -> Gelb -> nächstes Lied
							if (((TimeSpan)(DateTime.Now - KlangZeilenLaufend[durchlauf].dtLiedLastCheck)).TotalSeconds > Zeitüberlauf)
							{
								if (!KlangZeilenLaufend[durchlauf]._mplayer.HasAudio)
								{
									KlangZeilenLaufend[durchlauf].audioZeile.lbiKlangRow.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));       // Brushes.Yellow;
									KlangZeilenLaufend[durchlauf].audioZeile.lbiKlangRow.ToolTip = "Datei kann nicht abgespielt werden (Zeitüberlauf)";
									KlangZeilenLaufend[durchlauf]._mplayer.Stop();
									KlangZeilenLaufend[durchlauf]._mplayer.Close();
									KlangZeilenLaufend[durchlauf]._mplayer = null;
									KlangZeilenLaufend[durchlauf].playable = false;
									KlangZeilenLaufend[durchlauf].istStandby = true;
									KlangZeilenLaufend[durchlauf].istLaufend = false;
									KlangZeilenLaufend[durchlauf].istPause = false;
									CheckPlayStandbySongs(posObjGruppe);
								}
								else
									KlangZeilenLaufend[durchlauf].audioZeile.lbiKlangRow.Background = null;
							}
						}
						if (KlangZeilenLaufend[durchlauf]._mplayer != null &&
							KlangZeilenLaufend[durchlauf]._mplayer.HasAudio == false &&
							KlangZeilenLaufend[durchlauf]._mplayer.BufferingProgress == 1)
							KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.Now;
						else
							KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.MinValue;

						if (KlangZeilenLaufend[durchlauf].audioZeile.chkTitel.IsChecked == true && (KlangProgBarTimer.Tag.ToString() == "0") &&
							KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel != null &&
							KlangZeilenLaufend[durchlauf]._mplayer != null)
						{
							// Volume anpassen
							if (KlangZeilenLaufend[durchlauf].audioZeile.chkVolMove.IsChecked == true && !KlangZeilenLaufend[durchlauf].FadingOutStarted)
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

								if (((TabItemControl)tcKlang.Items[_GrpObjecte[posObjGruppe].seite]).Visibility == Visibility.Visible)
									KlangZeilenLaufend[durchlauf].audioZeile.sldKlangVol.Value = KlangZeilenLaufend[durchlauf].Aktuell_Volume;
							}
							double sollWert = (KlangZeilenLaufend[durchlauf].Aktuell_Volume / 100) * (_GrpObjecte[posObjGruppe].Vol_ThemeMod) / 100 * _GrpObjecte[posObjGruppe].Vol_PlaylistMod / 100;
							if (!FadingIn_Started && sollWert != KlangZeilenLaufend[durchlauf]._mplayer.Volume)
								KlangZeilenLaufend[durchlauf]._mplayer.Volume = sollWert;

							//einmaliges ermitteln des Endzeitpunkts
							if (KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum == 100000 && KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan &&
								KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel != null)
							{
								if (KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge != KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
									Global.ContextAudio.Update<Audio_Titel>(KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel);

								KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge = KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;
								KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Maximum = (double)KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Länge;

								//aktualisiere Endzeitpunkt Minutenposition
								KlangZeilenLaufend[durchlauf].audioZeile.lblDauer.Content = KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.ToString().Substring(3, 5);
							}

							//einmaliges ermitteln des Endzeitpunkts im Theme
							if (aktiveThemeGruppe >= 0 && _ThemeGruppe[aktiveThemeGruppe].Musik != null &&
								_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.pbarActBGTitel.Maximum == 100000 &&
								KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan &&
								_GrpObjecte[posObjGruppe].playlistName == _ThemeGruppe[aktiveThemeGruppe].Musik.playlistName)
							{
								_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.pbarActBGTitel.Maximum = (KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen) ?
									KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde.Value - KlangZeilenLaufend[durchlauf].audiotitel.TeilStart.Value :
									KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;
							}

							//aktualisiere ProgressBar
							if (((TabItemControl)tcKlang.Items[_GrpObjecte[posObjGruppe].seite]).Visibility == Visibility.Visible && //_GrpObjecte[posObjGruppe].seite == 0 ||
								KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)
								KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value = KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds;

							//aktualisiere ProgressBar im Theme
							if (aktiveThemeGruppe >= 0)
							{
								if (_ThemeGruppe[aktiveThemeGruppe].Musik != null && 
									_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.btnAudioTheme.IsChecked.Value &&
									!KlangZeilenLaufend[durchlauf].FadingOutStarted &&
									_GrpObjecte[posObjGruppe].playlistName == _ThemeGruppe[aktiveThemeGruppe].Musik.playlistName)
								{
									_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.pbarActBGTitel.Value = (KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen) ?
										KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds - KlangZeilenLaufend[durchlauf].audiotitel.TeilStart.Value :
										KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds;
									_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.lblActBGTitel.Content = (KlangZeilenLaufend[durchlauf] != null) ? KlangZeilenLaufend[durchlauf].audiotitel.Audio_Titel.Name : "kein aktiver Musiktitel";
								}
							}

							//Startposition überprüfen
							if (KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan && KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
								KlangZeilenLaufend[durchlauf].audiotitel.TeilStart > KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds)
								KlangZeilenLaufend[durchlauf]._mplayer.Position = TimeSpan.FromMilliseconds(KlangZeilenLaufend[durchlauf].audiotitel.TeilStart.Value);

							//Endposisiton überprüfen
							if (KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan && KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
								KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * 10).TotalMilliseconds >= KlangZeilenLaufend[durchlauf].audiotitel.TeilEnde)
							{
								if (_GrpObjecte[posObjGruppe].istMusik)
								{
									_GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));

									if (!KlangZeilenLaufend[durchlauf].FadingOutStarted)
									{
										KlangZeilenLaufend[durchlauf].FadingOutStarted = true;
										FadingOut(KlangZeilenLaufend[durchlauf], true, false);
									}

									if (aktiveThemeGruppe != -1 && _ThemeGruppe.Count > 0)
										_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.pbarActBGTitel.Value = 0;
									
								}
								KlangZeilenLaufend[durchlauf].istLaufend = false;
								KlangZeilenLaufend[durchlauf].istStandby = true;
								KlangZeilenLaufend[durchlauf].istPause = false;
								CheckPlayStandbySongs(posObjGruppe);
							}

							//Bei Musikplaylists die Endposition vor Fading überprüfen
							if (_GrpObjecte[posObjGruppe].istMusik && !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
								KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan && !KlangZeilenLaufend[durchlauf].audiotitel.TeilAbspielen &&
								KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * 10).TotalMilliseconds >= KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
							{
								_GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));

								if (!KlangZeilenLaufend[durchlauf].FadingOutStarted)
								{
									KlangZeilenLaufend[durchlauf].FadingOutStarted = true;
									FadingOut(KlangZeilenLaufend[durchlauf], true, false);
								}

								if (_ThemeGruppe.Count > 0 && aktiveThemeGruppe != -1)
									_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.pbarActBGTitel.Value = 0;
								KlangZeilenLaufend[durchlauf].istLaufend = false;
								KlangZeilenLaufend[durchlauf].istStandby = true;
								KlangZeilenLaufend[durchlauf].istPause = false;
								CheckPlayStandbySongs(posObjGruppe);
								KlangZeilenLaufend[durchlauf].audioZeile.pbarTitel.Value = 0;
							}
						}
					}
				}
			}
			if (!found)
			{
				KlangProgBarTimer.IsEnabled = false;
				KlangProgBarTimer.Stop();
			}
		}


		private void MusikProgBarTimer_Tick(object sender, EventArgs e)
		{
			if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
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
					}
				}
				
				pbarBGSong.Value = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds;
				((MusikZeile)lbPListMusik.SelectedItem).pbarSong.Value = pbarBGSong.Value;
				lblBgTimeActual.Content = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.ToString(@"mm\:ss"); 

				if (chkbxPlayRange.IsChecked.Value && pbarBGSong.Value < rsldTeilSong.LowerValue)
					_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position = TimeSpan.FromMilliseconds(rsldTeilSong.LowerValue);

				//Bei Musikplaylists die Endposition vor Fading überprüfen
				if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.HasTimeSpan && 
					_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * 10).TotalMilliseconds >=
						_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds)
				{
					_BGPlayer.Gespielt.Add((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);

					if (!_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
					{
						_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
						BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], true, false);
					}
					MusikProgBarTimer.Stop();
					if (btnBGRepeat.IsChecked.Value)
						SpieleNeuenMusikTitel((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
					else
						SpieleNeuenMusikTitel(Guid.Empty);
				}

				if ((_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.HasTimeSpan &&
					 _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds == _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds) ||
					 (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * 10).TotalMilliseconds >= rsldTeilSong.UpperValue &&
					 chkbxPlayRange.IsChecked.Value))
				{
					if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
					{
						if (!_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted)
						{
							_BGPlayer.BG[_BGPlayer.aktiv].FadingOutStarted = true;
							BGFadingOut(_BGPlayer.BG[_BGPlayer.aktiv], true, false);
						}
						_BGPlayer.aktiv = (_BGPlayer.aktiv == 0) ? 1 : 0;
					}
					MusikProgBarTimer.Stop();
					if (btnBGRepeat.IsChecked.Value)
						SpieleNeuenMusikTitel((Guid)((ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
					else
						SpieleNeuenMusikTitel(Guid.Empty);
				}
				else
					MusikProgBarTimer.Tag = _BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position.TotalMilliseconds;
			}
		}

		private void chkVolMove0_0_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = -1;
			
			foreach (GruppenObjekt grpObj in _GrpObjecte)
			{
				if (grpObj._listZeile.FirstOrDefault(t => t.audioZeile.chkVolMove == (CheckBox)sender) != null)
				{
					posObjGruppe = GetPosObjGruppe(GetObjGruppe(grpObj.seite));
					break;
				}
			}
			if (posObjGruppe == -1)
				return;
			int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
				_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.chkVolMove == (Control)sender));

			_GrpObjecte[posObjGruppe].anzVolChange = Convert.ToUInt16(
				_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkVolMove.IsChecked == true).Count);

			if (_GrpObjecte[posObjGruppe].anzPauseChange == _GrpObjecte[posObjGruppe]._listZeile.Count)
				_GrpObjecte[posObjGruppe].chkbxTopVolChange.IsChecked = true;
			else
				_GrpObjecte[posObjGruppe].chkbxTopVolChange.IsChecked = false;

			_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.VolumeChange = ((CheckBox)sender).IsChecked.Value;
			AlleKlangzeilenSpeichern(posObjGruppe);

			CheckAlleAngehakt(posObjGruppe);
		
		}
		
		private void sldKlangPause0_0_LostFocus(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = -1;

			foreach (GruppenObjekt grpObj in _GrpObjecte)
			{
				if (grpObj._listZeile.FirstOrDefault(t => t.audioZeile.sldKlangPause == (Slider)sender) != null)
				{
					posObjGruppe = GetPosObjGruppe(GetObjGruppe(grpObj.seite));
					break;
				}
			}
			if (posObjGruppe == -1)
				return;
			int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
				_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.sldKlangPause == (Control)sender));

			_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Pause = Convert.ToInt32(Math.Round(((Slider)sender).Value));
			plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel);
			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
		}

		private void chkKlangPauseMove0_0_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = -1;

			foreach (GruppenObjekt grpObj in _GrpObjecte)
			{
				if (grpObj._listZeile.FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (CheckBox)sender) != null)
				{
					posObjGruppe = GetPosObjGruppe(GetObjGruppe(grpObj.seite));
					break;
				}
			}
			if (posObjGruppe == -1)
				return;
			int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
				_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.chkKlangPauseMove == (Control)sender));
			
			_GrpObjecte[posObjGruppe].anzPauseChange = Convert.ToUInt16(
				_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkKlangPauseMove.IsChecked == true).Count);

			_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseChange = ((CheckBox)sender).IsChecked.Value;
			plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel);
			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

			CheckAlleAngehakt(posObjGruppe);
		}

		private void rsldTeilSong_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			_BGPlayer.AktPlaylistTitel.TeilStart = rsldTeilSong.LowerValue;
			_BGPlayer.AktPlaylistTitel.TeilEnde = rsldTeilSong.UpperValue;
			plyTitelToSave.Add(_BGPlayer.AktPlaylistTitel);
			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
		}

		private void imgTrash0_0_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Int16 objGruppe = GetObjGruppe(tcKlang.SelectedIndex);
			Int16 posObjGruppe = GetPosObjGruppe(objGruppe);
						
			if (posObjGruppe == -1)
				return;
						
			int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
				_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.imgTrash == (Image)sender));

			if (zeile != -1)
			{
				if (_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.chkTitel.IsChecked.Value == true)
				{
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.chkTitel.IsChecked = false;
					chkTitel0_0_Click(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.chkTitel, new RoutedEventArgs());
				}

				//_GrpObjecte[posObjGruppe].NochZuSpielen.FindAll(t => t > zeile).ForEach(t => t--);
				_GrpObjecte[posObjGruppe].Gespielt.FindAll(t => t > zeile).ForEach(t => t--);

				List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe;
				int anz = 0;
				int gefunden = 0;
				while (anz < playlistliste.Count && gefunden <= 1)
				{
					gefunden += Convert.ToUInt16((Global.ContextAudio.LoadPlaylist_TitelByPlaylist(playlistliste[anz],
						_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Audio_Titel)).Count);
					anz++;
				}
				Global.ContextAudio.RemoveTitelFromPlaylist(AktKlangPlaylist, _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Audio_Titel);
				if (gefunden <= 1)
					Global.ContextAudio.RemoveTitel(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Audio_Titel);

				if (_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Audio_Titel == null)
					ZeigeKlangSongsParallel(posObjGruppe, false);

				_GrpObjecte[posObjGruppe].wpnl.Children.RemoveAt(zeile);
				_GrpObjecte[posObjGruppe]._listZeile.RemoveAt(zeile);
				CheckBtnGleicherPfad();
			}
		}

		private void rbKlangKlang_Click(object sender, RoutedEventArgs e)
		{
			if (((TabItemControl)tcKlang.SelectedItem) != null)
			{
				int klangzeile = lbKlang.SelectedIndex;

				string klangname = klangzeile != -1 ? ((ListBoxItem)lbKlang.SelectedItem).Content.ToString() :
					(((TabItemControl)tcKlang.SelectedItem) != null) ? ((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text : "";

				if (((TabItem)tcAudioPlayer.SelectedItem).Header.ToString() == "Musik")
					AktualisiereMusikPlaylist();
				else
					if (((TabItem)tcAudioPlayer.SelectedItem).Header.ToString() == "Playlist-Editor")
						AktualisiereKlangPlaylist();

				tbKlangPlaylistFilter.Text = "";
				if (klangname != "") SelektiereKlangZeile(klangname);
			}
		}

		private void rbKlangAlle_Click(object sender, RoutedEventArgs e)
		{
			if (((TabItemControl)tcKlang.SelectedItem) != null)
			{
				int klangzeile = lbKlang.SelectedIndex;

				string klangname = klangzeile != -1 ? ((ListBoxItem)lbKlang.SelectedItem).Content.ToString() : ((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text;

				if (((TabItem)tcAudioPlayer.SelectedItem).Header.ToString() == "Musik")
					AktualisiereMusikPlaylist();
				else
					if (((TabItem)tcAudioPlayer.SelectedItem).Header.ToString() == "Playlist-Editor")
						AktualisiereKlangPlaylist();

				tbKlangPlaylistFilter.Text = "";
				SelektiereKlangZeile(klangname);
			}
		}

		private void rbIstKlangPlaylist_Click(object sender, RoutedEventArgs e)
		{
			if (AktKlangPlaylist != null)
			{
				if (_BGPlayer.AktPlaylist == AktKlangPlaylist && lbBackground.SelectedIndex != -1)
				{
					btnBGStoppen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
					lbBackground.SelectedIndex = -1; 
					lbBackground.Tag = -1;
					lbMusiktitellist.Items.Clear();
				}


				Int16 posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));

				_GrpObjecte[posObjGruppe].istMusik = false;
				AktKlangPlaylist.Hintergrundmusik = false;
				Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);

				ZeigeKlangSongsParallel(posObjGruppe, true);
				ZeigeKlangTop(posObjGruppe, true);
				ZeigeZeileKlangSpalten(posObjGruppe, true);

				for (UInt16 i = 0; i < _GrpObjecte[posObjGruppe]._listZeile.Count; i++)
				{
					if (_GrpObjecte[posObjGruppe]._listZeile[i].audioZeile.chkTitel.IsChecked == true)
						_GrpObjecte[posObjGruppe]._listZeile[i].istStandby = true;
				}

				AktualisiereKlangPlaylist();
				if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen && ((TabItem)tcKlang.SelectedItem).Name == "tiKlang")
					CheckPlayStandbySongs(posObjGruppe);
			}
			brdKlangKategorie.Visibility = Visibility.Hidden;
		}

		private void rbIstMusikPlaylist_Click(object sender, RoutedEventArgs e)
		{
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

				Int16 posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));

				AlleKlangSongsAus(posObjGruppe, false, false);
				if (!Convert.ToBoolean(_GrpObjecte[posObjGruppe].btnKlangPause.Tag))
				{
					_GrpObjecte[posObjGruppe].btnKlangPause.Tag = true;
					_GrpObjecte[posObjGruppe].btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
				}

				ZeigeKlangSongsParallel(posObjGruppe, false);
				ZeigeKlangTop(posObjGruppe, false);
				ZeigeZeileKlangSpalten(posObjGruppe, false);

				AktKlangPlaylist.MaxSongsParallel = 1;
				_GrpObjecte[posObjGruppe].maxsongparallel = 1;
				_GrpObjecte[posObjGruppe].istMusik = true;

				Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);

				AktualisiereKlangPlaylist();
			}
			brdKlangKategorie.Visibility = Visibility.Visible;
		}

		private void tboxPlaylistName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (AktKlangPlaylist == null)
				{
					List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(tboxPlaylistName.Text)).ToList();
					if (playlistliste.Count == 1)
					{
						List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]);
						AktKlangPlaylist = playlistliste[0];
						if (rbIstMusikPlaylist.IsChecked == true)
							AktKlangPlaylist.Hintergrundmusik = true;
						else
							AktKlangPlaylist.Hintergrundmusik = false;
						tboxPlaylistName.Text = AktKlangPlaylist.Name;
						((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text = AktKlangPlaylist.Name;
					}
					else
					{
						NeueKlangPlaylistInDB();
					}
				}
				for (int i = 0; i <= lbKlang.Items.Count - 1; i++)
				{
					if (((ListBoxItem)lbKlang.Items[i]).Content.ToString() == AktKlangPlaylist.Name)
						((ListBoxItem)lbKlang.Items[i]).Content = tboxPlaylistName.Text;
				}
				AktKlangPlaylist.Name = tboxPlaylistName.Text;
				Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);

				((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text = AktKlangPlaylist.Name;
				
				
				//_GrpObjecte[GetPosOb jGruppe(GetObjGruppe(tcKlang.SelectedIndex))].playlistName = AktKlangPlaylist.Name;

				((TextBox)(sender)).Background = null;
			}
			else
			{
				((TextBox)(sender)).Background = Brushes.LightSalmon;
			}
		}


		private void pbarBGSong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (_BGPlayer.AktPlaylist != null && _BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
			{
				Point pts = e.GetPosition((sender as ProgressBar));
				double total = (sender as ProgressBar).Maximum;
				double res = ((pts.X * 100) / ((double)(sender as ProgressBar).ActualWidth)) / 100;
				_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Position = TimeSpan.FromMilliseconds(total * res);
			}
		}

		private void tcKlang_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			tcKlang.Tag = tcKlang.SelectedIndex;
			if (tcKlang.SelectedIndex == tcKlang.Items.Count - 1)
			{
				if (tcKlang.Items.Count == 1)
					tcKlang.SelectedIndex = -1;
				else
					tcKlang.SelectedIndex = tcKlang.Items.Count - 2;
			}
			string s = "";
			Int16 posObjGruppe;
			
			if (tcKlang.SelectedIndex == -1)
			{
				ZeigeKlangSongsParallel(-1, false);
				if (tiKlang.IsSelected && lbKlang.SelectedIndex == -1) 
					tcKlang.SelectedItem = null;
			}
			else
			{
				if (((TabItem)tcKlang.Items[0]).Header.ToString() == "")
				{
					List<Audio_Playlist> plylstliste;
					int ver = 0;
					s = "NeuePlayliste";
					while (Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(s)).ToList().Count != 0)
					{
						s = "NeuePlayliste-" + ver;
						ver++;
					}
					((TabItem)tcKlang.SelectedItem).Header = s;
					plylstliste = Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(s)).ToList();
					tboxPlaylistName.Text = s;
					tboxKlangKategorie.Text = "";
					tboxKlangKategorie.Tag = null;
					btnKlangOpen.Focus();

					GruppenObjekt grpobj = new GruppenObjekt();
					grpobj.seite = tcKlang.SelectedIndex;
					grpobj.tiKlang = ((TabItem)tcKlang.SelectedItem);
					grpobj.objGruppe = 0;
					grpobj.playlistName = s;

					grpobj.sviewer = sviewerX;
					grpobj.grdKlang = grdKlangX;
					grpobj.grdKlangTop = grdKlangTopX;
					grpobj.btnKlangPause = btnKlangPauseX;
					grpobj.btnImgKlangPause = btnImgKlangPauseX;

					grpobj.chkbxTopAktiv = chkbxTopAktivX;
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
					tboxKlangKategorie.Tag = _GrpObjecte[posObjGruppe].Audio_Playlist_GUID;
				}
				else
				{
					Int16 objGruppe = GetObjGruppe(tcKlang.SelectedIndex);
					if (objGruppe == -1)
						return;
					posObjGruppe = GetPosObjGruppe(objGruppe);
					if (tcKlang.SelectedIndex >= 0)
					{

						List<Audio_Playlist> playlistliste = null;

						if (tcKlang.SelectedItem.GetType() == typeof(TabItemControl))
						{
							s = ((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text.ToString();
							playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(((TabItemControl)tcKlang.SelectedItem).Tag)).ToList();
						}
						else
							s = ((TabItem)tcKlang.SelectedItem).Header.ToString();

						if (playlistliste != null && playlistliste.Count != 0 && _GrpObjecte[posObjGruppe]._listZeile.Count > 0)
						{
							List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]);
							AktKlangPlaylist = playlistliste[0];

							if (AktKlangPlaylist.Hintergrundmusik)
								rbIstMusikPlaylist.IsChecked = true;
							else
								rbIstKlangPlaylist.IsChecked = true;
							tboxPlaylistName.Text = AktKlangPlaylist.Name;
							tboxKlangKategorie.Text = AktKlangPlaylist.Kategorie;
							tboxKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;

							if (titelliste.Count > 0)
							{
								tboxklangsongparallel.TextChanged -= new TextChangedEventHandler(tboxklangsongparallel_TextChanged);
								tboxklangsongparallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;

								tboxklangsongparallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
								tboxklangsongparallel.TextChanged += new TextChangedEventHandler(tboxklangsongparallel_TextChanged);

								ZeigeKlangSongsParallel(posObjGruppe, !playlistliste[0].Hintergrundmusik);
								ZeigeKlangTop(posObjGruppe, !playlistliste[0].Hintergrundmusik);
								ZeigeZeileKlangSpalten(posObjGruppe, !playlistliste[0].Hintergrundmusik);
							}
							_GrpObjecte[posObjGruppe].grdKlangTop.Visibility = Visibility.Visible;
						}
						else
						{
							lbKlang.SelectionChanged -= new SelectionChangedEventHandler(lbKlang_SelectionChanged);
							lbKlang.SelectedIndex = -1;
							lbKlang.SelectionChanged += new SelectionChangedEventHandler(lbKlang_SelectionChanged);

							rbIstKlangPlaylist.IsChecked = rbKlangKlang.IsChecked;
							rbIstMusikPlaylist.IsChecked = rbKlangMusik.IsChecked;
							tboxPlaylistName.Text = s;
							ZeigeKlangSongsParallel(posObjGruppe, false);

							tboxklangsongparallel.TextChanged -= new TextChangedEventHandler(tboxklangsongparallel_TextChanged);
							if (playlistliste != null && _GrpObjecte[posObjGruppe]._listZeile.Count > 0)
							{
								tboxklangsongparallel.Tag = AktKlangPlaylist.Audio_Playlist_Titel.Count;
								tboxklangsongparallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
								tboxKlangKategorie.Text = AktKlangPlaylist.Kategorie;
								tboxKlangKategorie.Tag = AktKlangPlaylist.Audio_PlaylistGUID;
							}
							else
							{
								tboxklangsongparallel.Tag = null;
								tboxklangsongparallel.Text = "1";
								tboxKlangKategorie.Text = "";
								tboxKlangKategorie.Tag = null;
							}
							tboxklangsongparallel.TextChanged += new TextChangedEventHandler(tboxklangsongparallel_TextChanged);							
						}
					}
					SelektiereKlangZeile(s);                    
				}                
			}
		}

		private void SelektiereKlangZeile(string klangGUID)
		{
			int i = 0;
			while (i <= lbKlang.Items.Count - 1)
			{
				if (((ListBoxItem)lbKlang.Items[i]).Content.ToString() == klangGUID)
				{
					lbKlang.SelectionChanged -= new SelectionChangedEventHandler(lbKlang_SelectionChanged);
					lbKlang.SelectedIndex = i;
					lbKlang.SelectionChanged += new SelectionChangedEventHandler(lbKlang_SelectionChanged);
					break;
				}
				i++;
			}
		}

		private void tiKlangPlaylistClose_Click(object sender, RoutedEventArgs e)
		{
			Int16 posObjGruppe = GetPosObjGruppe(GetObjGruppe(Convert.ToInt16(tcKlang.Tag)));
			if (posObjGruppe == -1)
				return;

			int seite = _GrpObjecte[posObjGruppe].seite;

			AlleKlangSongsAus(posObjGruppe, true, false);
		   
			PlaylisteLeeren(posObjGruppe);
			_GrpObjecte.RemoveAt(seite);

			for (int i = seite; i < _GrpObjecte.Count; i++)
				_GrpObjecte[i].seite--;

			if (tcKlang.SelectedIndex == tcKlang.Items.Count - 2)
			{
				int switchToItem = tcKlang.SelectedIndex - 1;
				while (switchToItem > 0 && ((TabItemControl)tcKlang.Items[switchToItem]).Visibility == Visibility.Collapsed)
					switchToItem--;
				tcKlang.SelectedIndex = switchToItem;
			}
			if (tcKlang.SelectedIndex == -1 || ((TabItemControl)tcKlang.SelectedItem).Visibility == Visibility.Collapsed)
			{
				ZeigeKlangGerneral(-1, false);
				tiPlus_MouseUp(null, null);

				if (!rbIstMusikPlaylist.IsChecked.Value && !rbIstKlangPlaylist.IsChecked.Value)
					rbIstMusikPlaylist.IsChecked = true;
			}
			else
			{
				seite = tcKlang.SelectedIndex;
				posObjGruppe = GetPosObjGruppe(GetObjGruppe(seite));
				if (posObjGruppe >= 0)
					ZeigeKlangGerneral(posObjGruppe, true);
			}
			tcKlang.Tag = tcKlang.SelectedIndex;

			if (AktKlangPlaylist != null)
			{
				if (AktKlangPlaylist.Hintergrundmusik)
					ZeigeKlangSongsParallel(posObjGruppe, false);
				else
					ZeigeKlangSongsParallel(posObjGruppe, true);
			}
			else
				ZeigeKlangSongsParallel(-1, false);
		}

		private void ZeigeKlangGerneral(Int16 posObjGruppe, bool sichtbar)
		{
			if (sichtbar)
			{
				ZeigeKlangSongsParallel(posObjGruppe, true);
				tboxKlangKategorie.Visibility = Visibility.Hidden;
				tboxPlaylistName.Visibility = Visibility.Visible;
				rbIstKlangPlaylist.Visibility = Visibility.Visible;
				rbIstMusikPlaylist.Visibility = Visibility.Visible;
				btnKlangOpen.Visibility = Visibility.Visible;
				if (_GrpObjecte[posObjGruppe].istMusik) 
					brdKlangKategorie.Visibility = Visibility.Visible;
			}
			else
			{
				ZeigeKlangSongsParallel(posObjGruppe, false);
				tboxKlangKategorie.Visibility = Visibility.Visible;
				tboxPlaylistName.Visibility = Visibility.Hidden;
				rbIstKlangPlaylist.Visibility = Visibility.Hidden;
				rbIstMusikPlaylist.Visibility = Visibility.Hidden;
				btnKlangOpen.Visibility = Visibility.Hidden;
				brdKlangKategorie.Visibility = Visibility.Hidden;
				lbKlang.SelectedIndex = -1;                
			}
		}

		private void tiPlus_MouseUp(object sender, MouseButtonEventArgs e)
		{
			tiErstellt++;
			Int16 objGruppe = Convert.ToInt16(tiErstellt);

			AktKlangPlaylist = null;
			Grid GridTmp = (Grid)DeepCopy(GridX, "X", objGruppe.ToString());

			TabItemControl tabItem = new TabItemControl();
			tabItem.Visibility = Visibility.Visible;
			tabItem._image.Source = null;
			tabItem._image.Width = 0;
			tabItem.Height = 19;
			tabItem.Name = "tiKlang" + objGruppe;

			string NeuePlaylist = "NeuePlayliste";
			int ver = 0;
			string[] str_tiHeader = new string[tcKlang.Items.Count - 2];

			for (int i = 0; i < tcKlang.Items.Count - 2; i++)
				str_tiHeader[i] = ((TabItemControl)tcKlang.Items[i])._textBlockTitel.Text;

			while (Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(NeuePlaylist)).ToList().Count != 0 ||
				str_tiHeader.Contains(NeuePlaylist))
			{
				NeuePlaylist = "NeuePlayliste-" + ver;
				ver++;
			}

			tabItem._textBlockTitel.Text = NeuePlaylist;
			tabItem._buttonClose.Click += new RoutedEventHandler(tiKlangPlaylistClose_Click);

			tcKlang.Items.Insert(tcKlang.Items.Count - 2, tabItem);
			tabItem.Content = GridTmp;

			ScrollViewer scrViewer = (ScrollViewer)GridTmp.FindName("sviewer" + objGruppe);
			scrViewer.ToolTip = sviewerX.ToolTip;
			scrViewer.Drop += new DragEventHandler(grdKlangX_Drop);
			scrViewer.DragEnter += new DragEventHandler(grdKlangX_DragEnter);
			
			Grid grdKlang = (Grid)scrViewer.FindName("grdKlang" + objGruppe);
			WrapPanel wpnl = (WrapPanel)grdKlang.FindName("wpnl" + objGruppe);

			Grid grdKlangTop = (Grid)grdKlang.FindName("grdKlangTop" + objGruppe);
			Button tbtn = (Button)grdKlangTop.FindName("btnKlangPause" + objGruppe);
			tbtn.Tag = true; 
			tbtn.Click += new RoutedEventHandler(btnKlangPauseX_Click);

			Image tbtnImg = (Image)tbtn.FindName("btnImgKlangPause" + objGruppe);

			TextBox tbTopFilter = (TextBox)grdKlangTop.FindName("tbKlangTopFilter" + objGruppe);
			tbTopFilter.TextChanged += new TextChangedEventHandler(tbKlangTopFilter_TextChanged);

			Image imgTopFilter = (Image)grdKlangTop.FindName("imgKlangTopFilter" + objGruppe);
			imgTopFilter.MouseUp += new MouseButtonEventHandler(imgBGFilter_MouseUp);

			CheckBox chbxTopAkt = (CheckBox)grdKlangTop.FindName("chkbxTopAktiv" + objGruppe);
			chbxTopAkt.Tag = objGruppe;
			chbxTopAkt.Click += new RoutedEventHandler(chkbxTopAktivX_Click);

			Button btnTopVolMin = (Button)grdKlangTop.FindName("btnTopVolMin" + objGruppe);
			btnTopVolMin.Click += new RoutedEventHandler(btnAllVolUp_Click);
			Button btnTopVolDown = (Button)grdKlangTop.FindName("btnTopVolDown" + objGruppe);
			btnTopVolDown.Click += new RoutedEventHandler(btnAllVolUp_Click);
			Button btnTopVolUp = (Button)grdKlangTop.FindName("btnTopVolUp" + objGruppe);
			btnTopVolUp.Click += new RoutedEventHandler(btnAllVolUp_Click);
			Button btnTopVolMax = (Button)grdKlangTop.FindName("btnTopVolMax" + objGruppe);
			btnTopVolMax.Click += new RoutedEventHandler(btnAllVolUp_Click);

			CheckBox chbxTopVolCh = (CheckBox)grdKlangTop.FindName("chkbxTopVolChange" + objGruppe);
			chbxTopVolCh.Tag = objGruppe;
			chbxTopVolCh.Click += new RoutedEventHandler(chkbxTopVolChangeX_Click);

			Button btnTopPauseMin = (Button)grdKlangTop.FindName("btnTopPauseMin" + objGruppe);
			btnTopPauseMin.Click += new RoutedEventHandler(btnAllPauseUp_Click);
			Button btnTopPauseDown = (Button)grdKlangTop.FindName("btnTopPauseDown" + objGruppe);
			btnTopPauseDown.Click += new RoutedEventHandler(btnAllPauseUp_Click);
			Button btnTopPauseUp = (Button)grdKlangTop.FindName("btnTopPauseUp" + objGruppe);
			btnTopPauseUp.Click += new RoutedEventHandler(btnAllPauseUp_Click);
			Button btnTopPauseMax = (Button)grdKlangTop.FindName("btnTopPauseMax" + objGruppe);
			btnTopPauseMax.Click += new RoutedEventHandler(btnAllPauseUp_Click);

			CheckBox chbxTopPauseCh = (CheckBox)grdKlangTop.FindName("chkbxTopPauseChange" + objGruppe);
			chbxTopPauseCh.Tag = objGruppe;
			chbxTopPauseCh.Click += new RoutedEventHandler(chkbxTopPauseChangeX_Click);

			Button btnTopVolMinMinus = (Button)grdKlangTop.FindName("btnTopVolMinMinus" + objGruppe);
			btnTopVolMinMinus.Click += new RoutedEventHandler(btnTopVolMinMinusX_Click);
			Button btnTopVolMinPlus = (Button)grdKlangTop.FindName("btnTopVolMinPlus" + objGruppe);
			btnTopVolMinPlus.Click += new RoutedEventHandler(btnTopVolMinPlusX_Click);
			Button btnTopVolMaxMinus = (Button)grdKlangTop.FindName("btnTopVolMaxMinus" + objGruppe);
			btnTopVolMaxMinus.Click += new RoutedEventHandler(btnTopVolMaxMinusX_Click);
			Button btnTopVolMaxPlus = (Button)grdKlangTop.FindName("btnTopVolMaxPlus" + objGruppe);
			btnTopVolMaxPlus.Click += new RoutedEventHandler(btnTopVolMaxPlusX_Click);

			Border brdTrennstrich = (Border)grdKlangTop.FindName("brdTrennstrich" + objGruppe);

			Button btnTopPauseMinMinus = (Button)grdKlangTop.FindName("btnTopPauseMinMinus" + objGruppe);
			btnTopPauseMinMinus.Click += new RoutedEventHandler(btnTopPauseMinMinusX_Click);
			Button btnTopPauseMinPlus = (Button)grdKlangTop.FindName("btnTopPauseMinPlus" + objGruppe);
			btnTopPauseMinPlus.Click += new RoutedEventHandler(btnTopPauseMinPlusX_Click);
			Button btnTopPauseMaxMinus = (Button)grdKlangTop.FindName("btnTopPauseMaxMinus" + objGruppe);
			btnTopPauseMaxMinus.Click += new RoutedEventHandler(btnTopPauseMaxMinusX_Click);
			Button btnTopPauseMaxPlus = (Button)grdKlangTop.FindName("btnTopPauseMaxPlus" + objGruppe);
			btnTopPauseMaxPlus.Click += new RoutedEventHandler(btnTopPauseMaxPlusX_Click);

			tcKlang.SelectedIndex = tcKlang.Items.Count - 2-1;

			//**********************************************************************************************************
			GruppenObjekt grpobj = new GruppenObjekt();
			grpobj.seite = tcKlang.SelectedIndex;
			grpobj.ticKlang = tabItem;
			grpobj.objGruppe = tiErstellt;
			grpobj.playlistName = NeuePlaylist;

			grpobj.sviewer = scrViewer;
			grpobj.grdKlang = grdKlang;
			grpobj.grdKlangTop = grdKlangTop;
			grpobj.wpnl = wpnl;
			grpobj.btnKlangPause = tbtn;
			grpobj.btnImgKlangPause = tbtnImg;

			grpobj.tbTopFilter = tbTopFilter;
			grpobj.imgTopFilter = imgTopFilter;

			grpobj.chkbxTopAktiv = chbxTopAkt;
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
			
			_GrpObjecte.Add(grpobj);
			//**********************************************************************************************************

			lbKlang.SelectionChanged -= new SelectionChangedEventHandler(lbKlang_SelectionChanged);
			lbKlang.SelectedIndex = -1;
			lbKlang.SelectionChanged += new SelectionChangedEventHandler(lbKlang_SelectionChanged);

			tboxPlaylistName.Text = NeuePlaylist;
			tboxKlangKategorie.Text = "";
			tboxKlangKategorie.Tag = _GrpObjecte[_GrpObjecte.Count - 1].Audio_Playlist_GUID;
			ZeigeKlangGerneral(GetPosObjGruppe(objGruppe), true);
			ZeigeKlangSongsParallel(GetPosObjGruppe(objGruppe), false);
			
			tboxklangsongparallel.Text = "1";
			rbIstMusikPlaylist.IsChecked = true;
		}

		private void tboxVolMin0_X_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			foreach (var item in e.Text)
				e.Handled = !char.IsDigit(item);
		}

		private void tboxVolMin0_X_LostFocus(object sender, RoutedEventArgs e)
		{
			if (AktKlangPlaylist != null)
			{
				int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));                
				if (posObjGruppe == -1)
					return;
				int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
					_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.tboxVolMin == (Control)sender));
				
				if (Convert.ToInt32(((TextBox)(sender)).Text) > _GrpObjecte[posObjGruppe]._listZeile[zeile].volMax_wert)
					((TextBox)(sender)).Text = _GrpObjecte[posObjGruppe]._listZeile[zeile].volMax_wert.ToString();
				_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.VolumeMin = Convert.ToInt16(((TextBox)(sender)).Text);
				if (Convert.ToInt16(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxVolMax.Text) < _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.VolumeMin)
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxVolMax.Text = Convert.ToString(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.VolumeMin);

				plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel);
				if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
				
			}
		}

		private void tboxVolMax0_X_LostFocus(object sender, RoutedEventArgs e)
		{
			if (AktKlangPlaylist != null)
			{
				Int16 objGruppe = GetObjGruppe(tcKlang.SelectedIndex);
				int posObjGruppe = GetPosObjGruppe(objGruppe);
				
				if (posObjGruppe == -1)
					return;
				int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
					_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.tboxVolMax == (Control)sender));
				
				if (Convert.ToInt32(((TextBox)(sender)).Text) < _GrpObjecte[posObjGruppe]._listZeile[zeile].volMin_wert)
						((TextBox)(sender)).Text = _GrpObjecte[posObjGruppe]._listZeile[zeile].volMin_wert.ToString();
				_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.VolumeMax = Convert.ToInt16(((TextBox)(sender)).Text);

				if (Convert.ToInt16(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxVolMin.Text) > _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.VolumeMax)
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxVolMin.Text = Convert.ToString(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.VolumeMax);

				plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel);
				if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
			}
		}

		private void tboxPauseMin0_X_LostFocus(object sender, RoutedEventArgs e)
		{
			if (AktKlangPlaylist != null)
			{
				Int16 objGruppe = GetObjGruppe(tcKlang.SelectedIndex);
				int posObjGruppe = GetPosObjGruppe(objGruppe);

				if (posObjGruppe == -1)
					return;
				int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
					_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.tboxPauseMin == (Control)sender));

				if (Convert.ToInt32(((TextBox)(sender)).Text) > _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert)
						((TextBox)(sender)).Text = _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert.ToString();
				_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseMin = Convert.ToInt16(((TextBox)(sender)).Text);

				if (Convert.ToInt16(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxPauseMin.Text) > _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseMax)
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxPauseMin.Text = Convert.ToString(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseMax);

				plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel);
				if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
			}
		}

		private void tboxPauseMax0_X_LostFocus(object sender, RoutedEventArgs e)
		{
			if (AktKlangPlaylist != null)
			{
				Int16 objGruppe = GetObjGruppe(tcKlang.SelectedIndex);
				int posObjGruppe = GetPosObjGruppe(objGruppe);

				if (posObjGruppe == -1)
					return;
				int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
					_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.tboxPauseMax == (Control)sender));

				if (Convert.ToInt32(((TextBox)(sender)).Text) < _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert)
						((TextBox)(sender)).Text = _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert.ToString();
				_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseMax = Convert.ToInt16(((TextBox)(sender)).Text);

				if (Convert.ToInt16(_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxPauseMax.Text) < _GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseMin)
					_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.tboxPauseMax.Text = Convert.ToString(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.PauseMin);

				plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel);
				if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();				
			}
		}

		private void _btnVolMinMinus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
				(((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

			int sollWert = klZeile.volMin_wert - VolSprung;

			if (sollWert <= klZeile.audioZeile.sldKlangVol.Maximum)
				klZeile.volMin_wert = sollWert < 0 ? 0 : sollWert;
			else
				klZeile.volMin_wert = Convert.ToInt16(klZeile.audioZeile.sldKlangVol.Minimum);
			klZeile.audioZeile.tboxVolMin.Text = Convert.ToString(klZeile.volMin_wert);
			klZeile.audiotitel.VolumeMin = klZeile.volMin_wert;
			klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3)? 1: 
				(klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

			plyTitelToSave.Add(klZeile.audiotitel);
			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
		}


		private void _btnVolMaxMinus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
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

		}

		private void _btnVolMinPlus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
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
		}

		private void _btnVolMaxPlus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
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
		}

		private void _btnPauseMinMinus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
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
		}

		private void _btnPauseMaxMinus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
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
		}

		private void _btnPauseMinPlus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
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
		}

		private void _btnPauseMaxPlus0_X_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			KlangZeile klZeile = _GrpObjecte[posObjGruppe]._listZeile.Find(t => t.ID_Zeile == Convert.ToUInt16(
				(((ListBoxItem)((Grid)((Button)sender).Parent).Parent)).Tag));

			int sollWert = klZeile.pauseMax_wert + PauseSprung;
			int max = Convert.ToInt32(klZeile.audioZeile.sldKlangPause.Maximum);

			klZeile.pauseMax_wert = sollWert < max ? sollWert : max;
			klZeile.audioZeile.tboxPauseMax.Text = Convert.ToString(klZeile.pauseMax_wert);
			klZeile.audiotitel.PauseMax = klZeile.pauseMax_wert;

			plyTitelToSave.Add(klZeile.audiotitel);
			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
		}

		private void btnKlangPauseX_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(Convert.ToInt16(tcKlang.Tag)));
			_GrpObjecte[posObjGruppe].wirdAbgespielt = !_GrpObjecte[posObjGruppe].wirdAbgespielt;

			if (tiKlang.IsSelected)
			{
				if (!Convert.ToBoolean(_GrpObjecte[posObjGruppe].btnKlangPause.Tag))
					_GrpObjecte[posObjGruppe].sollBtnGedrueckt--;
				else
					_GrpObjecte[posObjGruppe].sollBtnGedrueckt++;
			}
			_GrpObjecte[posObjGruppe].btnKlangPause.Tag = !Convert.ToBoolean(_GrpObjecte[posObjGruppe].btnKlangPause.Tag);

			for (int i = 0; i < _GrpObjecte[posObjGruppe]._listZeile.Count; i++)
			{
				_GrpObjecte[posObjGruppe]._listZeile[i].istPause = Convert.ToBoolean(_GrpObjecte[posObjGruppe].btnKlangPause.Tag);
				if (_GrpObjecte[posObjGruppe]._listZeile[i].istPause && _GrpObjecte[posObjGruppe]._listZeile[i].istLaufend)
				{
					if (!_GrpObjecte[posObjGruppe].istMusik)
					{
						_GrpObjecte[posObjGruppe]._listZeile[i]._mplayer.Pause();
						_GrpObjecte[posObjGruppe]._listZeile[i].istStandby = false;
						_GrpObjecte[posObjGruppe]._listZeile[i].istLaufend = false;
					}
					else
					{
						if (!_GrpObjecte[posObjGruppe]._listZeile[i].FadingOutStarted)
						{
							_GrpObjecte[posObjGruppe]._listZeile[i].FadingOutStarted = true;
							FadingOut(_GrpObjecte[posObjGruppe]._listZeile[i], false, true);
						}
					}				
				}
				else
				if (!_GrpObjecte[posObjGruppe]._listZeile[i].istPause && _GrpObjecte[posObjGruppe]._listZeile[i].istLaufend &&
					_GrpObjecte[posObjGruppe]._listZeile[i].audioZeile.chkTitel.IsChecked.Value)
				{
					_GrpObjecte[posObjGruppe]._listZeile[i].FadingOutStarted = false;
					FadingIn_Started = false;
					if (_GrpObjecte[posObjGruppe].ticKlang.Visibility == Visibility.Collapsed)
						FadingIn(_GrpObjecte[posObjGruppe]._listZeile[i]._mplayer, 
								(_GrpObjecte[posObjGruppe]._listZeile[i].Aktuell_Volume / 100) * (_GrpObjecte[posObjGruppe].Vol_ThemeMod) / 100);
					else
						FadingIn(_GrpObjecte[posObjGruppe]._listZeile[i]._mplayer, _GrpObjecte[posObjGruppe]._listZeile[i].Aktuell_Volume);
				}
				else
				if (!_GrpObjecte[posObjGruppe]._listZeile[i].istPause && !_GrpObjecte[posObjGruppe]._listZeile[i].istLaufend &&
					_GrpObjecte[posObjGruppe]._listZeile[i].audioZeile.chkTitel.IsChecked.Value)
					_GrpObjecte[posObjGruppe]._listZeile[i].istStandby = true;
				else
				_GrpObjecte[posObjGruppe]._listZeile[i].istStandby = false;
			}
			CheckPlayStandbySongs(posObjGruppe);
			if (!_GrpObjecte[posObjGruppe].wirdAbgespielt)
				_GrpObjecte[posObjGruppe].btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
			else
				_GrpObjecte[posObjGruppe].btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));

            if (_GrpObjecte[posObjGruppe].totalTimePlylist != -1)
            {
                aPlaylistLengthCheck = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(_GrpObjecte[posObjGruppe].Audio_Playlist_GUID)).FirstOrDefault();
                _GrpObjecte[posObjGruppe].totalTimePlylist = -1;
                GetTotalLength();
            }
		}

		private void tboxklangsongparallel_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (AktKlangPlaylist != null && tcKlang.SelectedIndex >= 0)
			{
				try
				{
					if (Convert.ToInt32(tboxklangsongparallel.Text) >= 0 && Convert.ToInt32(tboxklangsongparallel.Text) != AktKlangPlaylist.MaxSongsParallel)
					{
						if (Convert.ToInt32(tboxklangsongparallel.Text) > AktKlangPlaylist.Audio_Playlist_Titel.Count)
							tboxklangsongparallel.Text = AktKlangPlaylist.Audio_Playlist_Titel.Count.ToString();
						AktKlangPlaylist.MaxSongsParallel = Convert.ToInt32(tboxklangsongparallel.Text);
						int objGruppe = GetObjGruppe(tcKlang.SelectedIndex);
						int posObjGruppe = GetPosObjGruppe(objGruppe);
						_GrpObjecte[posObjGruppe].maxsongparallel = Convert.ToUInt16(AktKlangPlaylist.MaxSongsParallel);

						if (MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen || !Convert.ToBoolean(_GrpObjecte[posObjGruppe].btnKlangPause.Tag))
							CheckPlayStandbySongs(posObjGruppe);

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
					tboxklangsongparallel.Text = AktKlangPlaylist.MaxSongsParallel.ToString();
				}
			}
		}

		private Int16 GetObjGruppe(int seite)
		{
			List<GruppenObjekt> listeGrpObjecte = _GrpObjecte.Where(t => t.seite.Equals(seite)).ToList();
			if (listeGrpObjecte.Count == 1)
				return Convert.ToInt16(listeGrpObjecte[0].objGruppe);
			else
				return -1;
		}

		private void btnSongParPlus_Click(object sender, RoutedEventArgs e)
		{
			int dif = Convert.ToInt32(((Button)sender).Tag);
			int momentan = Convert.ToInt32(tboxklangsongparallel.Text);
			int max = Convert.ToInt32(tboxklangsongparallel.Tag);
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));

			if ((dif > 0 && dif + momentan <= max) ||
			   ((dif < 0 && dif + momentan >= 0)))
			{
				tboxklangsongparallel.Text = (Convert.ToInt32(tboxklangsongparallel.Text) + dif).ToString();
				_GrpObjecte[posObjGruppe].maxsongparallel = Convert.ToUInt16(tboxklangsongparallel.Text);
			}
		}

		private void btnPlaylistLoeschen_Click(object sender, RoutedEventArgs e)
		{
			Audio_Playlist playlistliste = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Name.Equals((lbKlang.SelectedItem as ListBoxItem).Content));
			if (playlistliste != null)
			{
				Global.SetIsBusy(true, string.Format("Playlist '" + playlistliste.Name + "' wird gelöscht..."));
				if (AktKlangPlaylist != null && AktKlangPlaylist.Name == playlistliste.Name)
				{
					for (UInt16 i = 0; i <= lbKlang.Items.Count - 1; i++)
					{
						if (((ListBoxItem)lbKlang.Items[i]).Content.ToString() == playlistliste.Name)
						{
							Int16 objGruppe = Convert.ToInt16(GetObjGruppe(tcKlang.SelectedIndex));
							if (objGruppe == -1)
								return;

							Int16 posObjGruppe = GetPosObjGruppe(objGruppe);
							if (posObjGruppe == -1)
								return;
							PlaylisteLeeren(posObjGruppe);

							tboxPlaylistName.Text = "NeuePlayliste";
							tboxKlangKategorie.Text = "";
							tboxKlangKategorie.Tag = null;
							
							((TabItemControl)tcKlang.SelectedItem)._textBlockTitel.Text = tboxPlaylistName.Text;

							tboxklangsongparallel.TextChanged -= new TextChangedEventHandler(tboxklangsongparallel_TextChanged);
							tboxklangsongparallel.Tag = null;
							tboxklangsongparallel.Text = "1";
							_GrpObjecte[posObjGruppe].maxsongparallel = 1;
							tboxklangsongparallel.TextChanged += new TextChangedEventHandler(tboxklangsongparallel_TextChanged);

							ZeigeKlangSongsParallel(posObjGruppe, false);

							_GrpObjecte[posObjGruppe].grdKlangTop.Visibility = Visibility.Hidden;
						}
					}
					AktKlangPlaylist = null;
				}

				if (_BGPlayer.AktPlaylist != null && _BGPlayer.AktPlaylist.Name == playlistliste.Name)
				{
					MusikProgBarTimer.Stop();
					if (_BGPlayer.BG[_BGPlayer.aktiv].mPlayer != null)
					{
						_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Stop();
						_BGPlayer.BG[_BGPlayer.aktiv].mPlayer.Close();
						_BGPlayer.BG[_BGPlayer.aktiv].mPlayer = null;
						btnBGAbspielen.IsEnabled = false;
						_BGPlayer.AktPlaylist = null;
						lbMusiktitellist.Items.Clear();
					}
					grdSongInfo.Visibility = Visibility.Hidden;       
				}

			//	try
			//	{

					List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(playlistliste);
					titel.ForEach(delegate(Audio_Titel aTitel)
					{
						Global.ContextAudio.RemoveTitelFromPlaylist(playlistliste, aTitel);
					});
					Global.ContextAudio.Delete<Audio_Playlist>(playlistliste);

					/*if (Global.ContextAudio.Delete<Audio_Playlist>(playlistliste))
					{
						MyTimer.stop_timer();
						List<Audio_Playlist> plistliste = Global.ContextAudio.PlaylistListe;
						for (int i = 0; i < titel.Count; i++)
						{
							
							int anz = 0;
							bool gefunden = false;
							while (anz < plistliste.Count && !gefunden)
							{

								if ((Global.ContextAudio.LoadPlaylist_TitelByPlaylist(plistliste[anz], titel[i])).Count > 0)
									gefunden = true;
								else
									anz++;
							}
							if (!gefunden)
								Global.ContextAudio.RemoveTitel(titel[i]);
							MyTimer.stop_timer();
						}
						MyTimer.start_timer();
						AktualisiereKlangPlaylist();
						MyTimer.stop_timer();
					}
				}
				catch (Exception ex)
				{
					Global.SetIsBusy(false);
					var errWin = new MsgWindow("Playlist Fehler", "Die Playliste konnte nicht erfolgreich gelöscht werden", ex);
					errWin.ShowDialog();
					errWin.Close();
				}*/
				AktualisiereKlangPlaylist();

				Global.SetIsBusy(false);
			}
		}

		private void sldKlangVol0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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

				plyTitelToSave.Add(klZeile.audiotitel);
				if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
			}
		}

		private void sldPlaySpeed0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (_GrpObjecte.Count > 0 && _GrpObjecte[0] != null)
			{
				int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));

				int zeile = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(
					_GrpObjecte[posObjGruppe]._listZeile.FirstOrDefault(t => t.audioZeile.sldPlaySpeed == (Slider)sender));

				double speed = _GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldPlaySpeed.Value;
				if (_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer != null)
					_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.SpeedRatio = speed;

				_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel.Speed = speed;//_GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.SpeedRatio;

				plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[zeile].audiotitel);
				if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

				string geschw = "Abspielgeschwindigkeit: ";

				geschw += speed == .1 ? "sehr langsam" :
						  speed == .5 ? "langsam" :
						  speed == .75 ? "gedrosselt" :
						  speed == 1 ? "normal" :
						  speed == 2 ? "erhöht" :
						  speed == 3 ? "schnell" :
						  speed == 4 ? "sehr schnell" :
						  speed == 5 ? "ultra schnell" : "nicht definiert";
				_GrpObjecte[posObjGruppe]._listZeile[zeile].audioZeile.sldPlaySpeed.ToolTip = geschw;
				_GrpObjecte[posObjGruppe]._listZeile[zeile].playspeed = speed;
			}
		}


		private void sldKlangPause0_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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

				plyTitelToSave.Add(klZeile.audiotitel);
				if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
			}
		}

		private void btnAllVolUp_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			double d = Convert.ToDouble(((sender) as Button).Tag);

			_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.lbiKlangRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(t => t.audioZeile.sldKlangVol.Value += d);          
		}

		private void btnAllPauseUp_Click(object sender, RoutedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));            
			double d = Convert.ToDouble(((sender) as Button).Tag);

			_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.lbiKlangRow != null).FindAll(t2 => t2.audioZeile.chkTitel.IsChecked == true).
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

		private void tboxPlaylistName_LostFocus(object sender, RoutedEventArgs e)
		{
			if (((TextBox)(sender)).Background != null && AktKlangPlaylist != null)
			{
				((TextBox)(sender)).Text = AktKlangPlaylist.Name;
				((TextBox)(sender)).Background = null;
			}
		}

		private void tiMusik_GotFocus(object sender, RoutedEventArgs e)
		{
			AktualisiereMusikPlaylist();            
		}

		private void tiPList_GotFocus(object sender, RoutedEventArgs e)
		{
			if (e.Source.GetType() != typeof(MusikZeile))
			{
				slPlaylistMusikVolume.Value = slBGVolume.Value;
				AktualisierePListPlaylist();
				if (lbPListMusik.SelectedIndex == -1 && lbBackground.SelectedIndex != -1)
				{
					lbPListMusik.SelectionChanged -= new SelectionChangedEventHandler(lbPListMusik_SelectionChanged);
					lbPListMusik.SelectedIndex = lbBackground.SelectedIndex;
					lbPListMusik.SelectionChanged += new SelectionChangedEventHandler(lbPListMusik_SelectionChanged);
				}
				slPlaylistVolume_ValueChanged(slPlaylistVolume, e);
			}
		}

		private void btnKlangOpen_Click(object sender, RoutedEventArgs e)
		{
			// Konfiguren des Öffnen Ddialogs
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.CheckFileExists = true;
			dlg.Multiselect = true;
			dlg.DefaultExt = ".mp3;.wav;.wma;.ogg"; // Extensionen
			dlg.Filter = "Alle Musikdateien |*.mp3;*.wav;*.wma;*.ogg|MP3-Dateien|*.mp3|Wave-Dateien|*.wav|Windows Media Player-Dateien|*.wma|OGG-Dateien|*.ogg"; // Filter Dateien pro extension
			dlg.InitialDirectory = Directory.Exists(stdPfad)? stdPfad: @"C:\";

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
							Int16 objGruppe = Convert.ToInt16(GetObjGruppe(tcKlang.SelectedIndex));
							if (objGruppe == -1)
								return;
							Int16 posObjGruppe = GetPosObjGruppe(objGruppe);
							_GrpObjecte[posObjGruppe].grdKlangTop.Visibility = Visibility.Visible;


							if (rbIstKlangPlaylist.IsChecked == true)
								AktKlangPlaylist.Hintergrundmusik = false;
							else
								AktKlangPlaylist.Hintergrundmusik = true;

							if (AktKlangPlaylist.Hintergrundmusik)
							{
								ZeigeKlangSongsParallel(posObjGruppe, false);
								ZeigeKlangTop(posObjGruppe, false);
							}
							else
							{
								ZeigeKlangSongsParallel(posObjGruppe, true);
								ZeigeKlangTop(posObjGruppe, true);
							}
							CheckAlleAngehakt(GetPosObjGruppe(objGruppe));
							_GrpObjecte[posObjGruppe].grdKlang.Visibility = Visibility.Visible;
						}
					}
				}
				finally
				{
					Mouse.OverrideCursor = null;
					Global.ContextAudio.Save();
					int i = tcKlang.SelectedIndex;
					tcKlang.SelectedIndex = -1;
					tcKlang.SelectedIndex = i;
					CheckBtnGleicherPfad();
				}
			}
		}

		private void image1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			CustomMessage("Codec Add-On Hinweis", "OGG-Dateien integrieren",
				"OGG-Dateien können nach dem Installieren eines entsprechenden AddOns bzw. " + Environment.NewLine +
				"Codec-Packs ebenfalls wiedergegeben werden." + Environment.NewLine + Environment.NewLine +
				"Ein entsprechendes Codec bietet das 'Media Player Codec Pack' und " + Environment.NewLine +
				"kann unter folgender Adresse heruntergeladen werden:" + Environment.NewLine + Environment.NewLine,
				"http://www.mediaplayercodecpack.com/");            
		}


		private void btnClick(object sender, RoutedEventArgs e)
		{
			((((Button)(sender)).Parent as Grid).Parent as Window).Close();
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
				hyperl.Click += new RoutedEventHandler(hyperlink_Click);
				tx.Inlines.Add(hyperl);
			}

			scr.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			scr.Content = tx;

			btn.Content = "OK";
			btn.MinWidth = 50;
			btn.Margin = new Thickness(10);
			btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
			btn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
			btn.Click += new RoutedEventHandler(btnClick);

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
			System.Diagnostics.Process.Start(((Hyperlink)sender).NavigateUri.AbsoluteUri);
		}

		private void btnTopVolMinMinusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnVolMinMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void btnTopVolMinPlusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnVolMinPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void btnTopVolMaxMinusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnVolMaxMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void btnTopVolMaxPlusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnVolMaxPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void btnTopPauseMinMinusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnPauseMinMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void btnTopPauseMinPlusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnPauseMinPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void btnTopPauseMaxMinusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnPauseMaxMinus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void btnTopPauseMaxPlusX_Click(object sender, RoutedEventArgs e)
		{
			_GrpObjecte[GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex))]._listZeile.
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).
				FindAll(t => t.audioZeile.chkTitel.IsChecked.Value).
				ForEach(t => _btnPauseMaxPlus0_X_Click(t.audioZeile._btnVolMinMinus, e));
		}

		private void chkbxTopAktivX_Click(object sender, RoutedEventArgs e)
		{
			bool soll = ((CheckBox)(e.Source)).IsChecked.Value;
			UInt16 objGruppe = Convert.ToUInt16(((CheckBox)sender).Tag);
			int posObjGruppe = GetPosObjGruppe(objGruppe);

			_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.chkTitel.IsChecked.Value != soll).
				FindAll(t => t.audioZeile.Visibility == Visibility.Visible).ForEach(delegate(KlangZeile klZeile)
			{
				klZeile.audioZeile.chkTitel.IsChecked = soll;
				klZeile.audiotitel.Aktiv = soll;               
				klZeile.audioZeile.chkTitel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));// .IsChecked = ((CheckBox)(e.Source)).IsChecked;

				plyTitelToSave.Add(klZeile.audiotitel);
			});

			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();

			if (_GrpObjecte[posObjGruppe].anzTitelAkt == _GrpObjecte[posObjGruppe]._listZeile.Count &&
				((CheckBox)(e.Source)).IsChecked == true)
			{
				//Zufallsaktivierung der Zeilen
				List<KlangZeile> klZeileAktiv;
				klZeileAktiv = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.playable == true);

				while (klZeileAktiv.Count > 0)
				{
					int zeileIndex = _GrpObjecte[posObjGruppe]._listZeile.IndexOf(klZeileAktiv[(new Random()).Next(0, klZeileAktiv.Count)]);
					chkTitel0_0_Click(_GrpObjecte[posObjGruppe]._listZeile[zeileIndex].audioZeile.chkTitel, e);

					klZeileAktiv = klZeileAktiv.FindAll(t => t.istLaufend != true);
					klZeileAktiv = klZeileAktiv.FindAll(t => t.istStandby != true);
				}
			}
			((CheckBox)(e.Source)).IsChecked = soll;
		}

		private void chkbxTopVolChangeX_Click(object sender, RoutedEventArgs e)
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
			CheckAlleAngehakt(posObjGruppe);
		}


		private void chkbxTopPauseChangeX_Click(object sender, RoutedEventArgs e)
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
			CheckAlleAngehakt(posObjGruppe);
		}


		private void chkbxPlayRange_Click(object sender, RoutedEventArgs e)
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

		private void btnShuffle_Click(object sender, RoutedEventArgs e)
		{			
			btnShuffleImg.Source = ((ToggleButton)sender).IsChecked == true ?
				new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/shuffle.png")):
				new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/no_shuffle.png"));
		}

		private void btnThemePlayNext_Click(object sender, RoutedEventArgs e)
		{
			if (AktThemeGruppe != null)
			{
				int momLaufend = AktThemeGruppe.Musik._listZeile.FindIndex(t => t.istLaufend);
				if (momLaufend >= 0)
				{
					_ThemeGruppe[aktiveThemeGruppe].pnlAudioTheme.pbarActBGTitel.Value = 0;

					if (!AktThemeGruppe.Musik._listZeile[momLaufend].FadingOutStarted)
					{
						AktThemeGruppe.Musik._listZeile[momLaufend].FadingOutStarted = true;
						FadingOut(AktThemeGruppe.Musik._listZeile[momLaufend], true, false);
					}

					AktThemeGruppe.Musik._listZeile[momLaufend].istLaufend = false;
					AktThemeGruppe.Musik._listZeile[momLaufend].istStandby = false;
					ThemeEinSchalten(AktThemeGruppe, false, !Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag));   //kein Editmodus
					AktThemeGruppe.Musik._listZeile[momLaufend].istStandby = true;
				}
			}
		}

		private void ThemeEinSchalten(ThemeGruppe thGruppe, bool CheckPlay, bool EditModus)
		{
			bool isHauptTheme = _ThemeGruppe.Contains(thGruppe);

			thGruppe.pnlAudioTheme.btnAudioTheme.Checked -= isHauptTheme ?
				new RoutedEventHandler(btnAudioTheme_Checked):
				new RoutedEventHandler(btnThemeThemeAngehakt_Checked);

			thGruppe.pnlAudioTheme.btnAudioTheme.IsChecked = true;

			thGruppe.pnlAudioTheme.btnAudioTheme.Checked += isHauptTheme ?
				new RoutedEventHandler(btnAudioTheme_Checked):
				new RoutedEventHandler(btnThemeThemeAngehakt_Checked);

			if (isHauptTheme) tboxThemeBezeichnung.Text = thGruppe.dbAudioTheme.Name;

			if (!EditModus)
			{
				if (thGruppe.Musik != null && thGruppe.Musik.btnKlangPause != null &&
					thGruppe.Musik._listZeile.Count > 0)
				{
					thGruppe.Musik.sollBtnGedrueckt = 1;
					thGruppe.Musik.btnKlangPause.Tag = true;
					tcKlang.Tag = thGruppe.Musik.seite;
					btnKlangPauseX_Click(thGruppe.Musik.btnKlangPause, new RoutedEventArgs());

					KlangZeile klZeileLaufende = thGruppe.Musik._listZeile.Find(t => t.istLaufend);
					if (isHauptTheme) AktThemeGruppe.pnlAudioTheme.lblActBGTitel.Content = (klZeileLaufende != null) ? klZeileLaufende.audiotitel.Audio_Titel.Name : "kein aktiver Musiktitel";
				}

				for (int i = 0; i < thGruppe.Geraeusche.Count; i++)
				{
					thGruppe.Geraeusche[i].sollBtnGedrueckt++;
					thGruppe.Geraeusche[i].btnKlangPause.Tag = true;
					tcKlang.Tag = thGruppe.Geraeusche[i].seite;
					btnKlangPauseX_Click(thGruppe.Geraeusche[i].btnKlangPause, new RoutedEventArgs());
				}

				for (int i = 0; i < thGruppe.UnterTheme.Count; i++)
				{
					if (!thGruppe.UnterTheme[i].pnlAudioTheme.btnAudioTheme.IsChecked.Value &&
						thGruppe.UnterTheme[i].pnlAudioTheme.pbarActBGTitel.Visibility != Visibility.Visible)
						ThemeEinSchalten(thGruppe.UnterTheme[i], true, EditModus); 
				}
			}
			if (isHauptTheme && CheckPlay)
				CheckThemePlayListAuswahl(thGruppe);
		}

		private void ThemeAusSchalten(ThemeGruppe thGruppe, bool CheckPlay, bool EditModus)
		{
			bool isHauptTheme = _ThemeGruppe.Contains(thGruppe);			
			if (isHauptTheme) tboxThemeBezeichnung.Text = "";
 
			thGruppe.pnlAudioTheme.btnAudioTheme.Unchecked -= isHauptTheme ?
				new RoutedEventHandler(btnAudioTheme_Unchecked) :
				new RoutedEventHandler(btnThemeThemeAngehakt_UnChecked);
			
			thGruppe.pnlAudioTheme.btnAudioTheme.IsChecked = false;
			
			thGruppe.pnlAudioTheme.btnAudioTheme.Unchecked += isHauptTheme ? 
				new RoutedEventHandler(btnAudioTheme_Unchecked) : 
				new RoutedEventHandler(btnThemeThemeAngehakt_UnChecked);

			if (isHauptTheme) thGruppe.pnlAudioTheme.imgPlay.Visibility = Visibility.Hidden;

			if (!EditModus)
			{
				if (thGruppe.HGThemePlaylist != null && thGruppe.Musik.btnKlangPause != null &&
					!Convert.ToBoolean(thGruppe.Musik.btnKlangPause.Tag))
				{
					thGruppe.Musik.sollBtnGedrueckt = 0;
					if (!Convert.ToBoolean(thGruppe.Musik.btnKlangPause.Tag) && thGruppe.Musik.sollBtnGedrueckt >= 0)
					{
						thGruppe.Musik.btnKlangPause.Tag = false;
						tcKlang.Tag = thGruppe.Musik.seite;
						btnKlangPauseX_Click(thGruppe.Musik.btnKlangPause, new RoutedEventArgs());
					}
					KlangZeile klZeileLaufend = thGruppe.Musik._listZeile.Find(t => t.istLaufend);
					if (isHauptTheme) AktThemeGruppe.pnlAudioTheme.lblActBGTitel.Content = (klZeileLaufend != null) ? klZeileLaufend.audiotitel.Audio_Titel.Name : "kein aktiver Musiktitel";
				}

				for (int i = 0; i < thGruppe.Geraeusche.Count; i++)  // thGruppe.Geraeusche.Count
				{
					thGruppe.Geraeusche[i].sollBtnGedrueckt--;
					if (!Convert.ToBoolean(thGruppe.Geraeusche[i].btnKlangPause.Tag) && thGruppe.Geraeusche[i].sollBtnGedrueckt >= 0)
					{
						thGruppe.Geraeusche[i].btnKlangPause.Tag = false;
						tcKlang.Tag = thGruppe.Geraeusche[i].seite;
						btnKlangPauseX_Click(thGruppe.Geraeusche[i].btnKlangPause, new RoutedEventArgs());
					}
				}

				thGruppe.UnterTheme.FindAll(t => t.pnlAudioTheme.pbarActBGTitel.Visibility != Visibility.Visible).ForEach(t => ThemeAusSchalten(t, true, EditModus));
			   /* for (int i = 0; i < thGruppe.UnterTheme.Count; i++)
				{
					if (thGruppe.UnterTheme[i].pnlAudioTheme.pbarActBGTitel.Visibility != Visibility.Visible)
						ThemeAusSchalten(thGruppe.UnterTheme[i], true, EditModus); 
				}*/
			}
			
			if (isHauptTheme && CheckPlay)
				CheckThemePlayListAuswahl(thGruppe);
		}
		
		private void btnAudioTheme_Unchecked(object sender, RoutedEventArgs e)
		{
			int i = Convert.ToInt16(((ToggleButton)sender).Tag);

			if (AktThemeGruppe == null || aktiveThemeGruppe == -1)
			{
				AktThemeGruppe = _ThemeGruppe[i];
				aktiveThemeGruppe = i;
			}
			AktThemeGruppe.ThemeJustRead = true;
			ThemeAusSchalten(AktThemeGruppe, true, !Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag));  
			ThemeGruppe thGrp = ThemeLstThemePanel.First(t => (Guid)t.pnlAudioTheme.btnAudioTheme.Tag == AktThemeGruppe.dbAudioTheme.Audio_ThemeGUID);
			if (thGrp != null)
				thGrp.pnlAudioTheme.Visibility = Visibility.Visible;

			AktThemeGruppe.ThemeJustRead = false;
			AktThemeGruppe.pnlAudioTheme.btnPlayNext.IsEnabled = false;
			AktThemeGruppe = null;
			aktiveThemeGruppe = -1;
			tboxThemeBezeichnung.Visibility = Visibility.Hidden;
			btnThemeLöschen.Visibility = Visibility.Hidden;
			ThemeLstBGPanel.ForEach(t => t.IsEnabled = false);
			ThemeLstKlangPanel.ForEach(t => t.IsEnabled = false);
			ThemeLstThemePanel.ForEach(t => t.pnlAudioTheme.IsEnabled = false);
		}

		private void btnAudioTheme_Checked(object sender, RoutedEventArgs e)
		{
			int i = Convert.ToInt16(((ToggleButton)sender).Tag);

			if (AktThemeGruppe != null &&
				AktThemeGruppe.pnlAudioTheme.btnAudioTheme != ((ToggleButton)sender))
			{
				AktThemeGruppe.pnlAudioTheme.imgPlay.Tag = 0;                
				btnAudioTheme_Unchecked(AktThemeGruppe.pnlAudioTheme.btnAudioTheme, new RoutedEventArgs());
			}

			AktThemeGruppe = new ThemeGruppe();
			AktThemeGruppe = _ThemeGruppe[i];
			AktThemeGruppe.ThemeJustRead = true;
			aktiveThemeGruppe = i;
			AktThemeGruppe.ThemeName = AktThemeGruppe.dbAudioTheme.Name;

			ThemeLstKlangPanel.ForEach(t => t.IsEnabled = true);
			ThemeLstBGPanel.ForEach(t => t.IsEnabled = true);
			ThemeLstThemePanel.ForEach(t => t.pnlAudioTheme.IsEnabled = true);

			ThemeEinSchalten(AktThemeGruppe, true, !Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag));   //kein Editmodus
			ThemeGruppe thGrp = ThemeLstThemePanel.FirstOrDefault(t => (Guid)t.pnlAudioTheme.btnAudioTheme.Tag == AktThemeGruppe.dbAudioTheme.Audio_ThemeGUID);
			if (thGrp != null) 
				thGrp.pnlAudioTheme.Visibility = Visibility.Collapsed;

			AktThemeGruppe.pnlAudioTheme.imgPlay.Tag = 0;

			AktThemeGruppe.ThemeJustRead = false;
			tboxThemeBezeichnung.Visibility = Visibility.Visible;

			if (!Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag)) //btnThemeEdit.IsChecked.Value)
			{
				AktThemeGruppe.pnlAudioTheme.btnPlayNext.IsEnabled = false;
				btnThemeLöschen.Visibility = Visibility.Visible;
			}
			else
			{
				if (Convert.ToInt16(btnBGAbspielen.Tag) == 1 && !_BGPlayer.BG[_BGPlayer.aktiv].isPaused)
					btnBGAbspielen_Click(btnBGAbspielen, new RoutedEventArgs());
				AktThemeGruppe.pnlAudioTheme.btnPlayNext.IsEnabled = true;
				ThemeLstKlangPanel.ForEach(t => t.IsEnabled = true);
				ThemeLstBGPanel.ForEach(t => t.IsEnabled = true);
				ThemeLstThemePanel.ForEach(t => t.pnlAudioTheme.IsEnabled = true);
			}
		}

		private void CheckThemePlayListAuswahl(ThemeGruppe grpTheme)
		{
			ThemeLstBGPanel.FindAll(t => t.btnAngehakt.IsChecked.Value).ForEach(t => t.btnAngehakt.IsChecked = false);
			ThemeLstKlangPanel.FindAll(t => t.btnHitchPanel.IsChecked.Value).ForEach(t => t.btnHitchPanel.IsChecked = false);
			
			ThemeLstThemePanel.FindAll(t => t.pnlAudioTheme.btnAudioTheme.IsChecked.Value).ForEach(delegate (ThemeGruppe thGrp)
			{
				thGrp.pnlAudioTheme.btnAudioTheme.Unchecked -= new RoutedEventHandler(btnThemeThemeAngehakt_UnChecked);
				thGrp.pnlAudioTheme.btnAudioTheme.IsChecked = false;
				thGrp.pnlAudioTheme.btnAudioTheme.Unchecked -= new RoutedEventHandler(btnThemeThemeAngehakt_UnChecked);             
			});

			if (grpTheme != null)
			{
				if (grpTheme.HGThemePlaylist != null)
					ThemeLstBGPanel.FindAll(t => t.lblThemeName.Content.Equals(grpTheme.HGThemePlaylist.Name)).ForEach(t => t.btnHitchPanel.IsChecked = true);

				for (int i = 0; i < grpTheme.Geraeusche.Count; i++)
					ThemeLstKlangPanel.FindAll(t => t.lblThemeName.Content.Equals(grpTheme.Geraeusche[i].playlistName)).ForEach(t => t.btnHitchPanel.IsChecked = true);
				
				foreach (ThemeGruppe thGrp in grpTheme.UnterTheme)
					ThemeLstThemePanel.FindAll(t => t.pnlAudioTheme.lblThemeName.Content.Equals(thGrp.pnlAudioTheme.lblThemeName.Content)).ForEach(t => t.pnlAudioTheme.btnAudioTheme.IsChecked = true);
			}
		}

		private void pbarThemeActBGTitel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			int thGruppe = Convert.ToInt16(((ProgressBar)sender).Tag);
			//aktiveThemeGruppe
			if (AktThemeGruppe == _ThemeGruppe[thGruppe] && 
				thGruppe >= 0 && _ThemeGruppe[thGruppe].Musik != null)
			{
				_ThemeGruppe[thGruppe].pnlAudioTheme.btnAudioTheme.Checked -= new RoutedEventHandler(btnAudioTheme_Checked);
				_ThemeGruppe[thGruppe].pnlAudioTheme.btnAudioTheme.Unchecked -= new RoutedEventHandler(btnAudioTheme_Unchecked);

				int momAktSong = _ThemeGruppe[thGruppe].Musik._listZeile.FindIndex(t => t.istLaufend == true);
				if (momAktSong >= 0)
				{
					Point pts = e.GetPosition(sender as ProgressBar);
					double res = pts.X / ((double)((sender as ProgressBar).ActualWidth));

					_ThemeGruppe[thGruppe].Musik._listZeile[momAktSong]._mplayer.Position =
						(_ThemeGruppe[thGruppe].Musik._listZeile[momAktSong].audiotitel.TeilAbspielen) ?
						TimeSpan.FromMilliseconds(_ThemeGruppe[thGruppe].Musik._listZeile[momAktSong].audiotitel.TeilStart.Value + (res * (sender as ProgressBar).Maximum)) :
						TimeSpan.FromMilliseconds(res * (sender as ProgressBar).Maximum);
				}
				_ThemeGruppe[thGruppe].pnlAudioTheme.btnAudioTheme.Checked += new RoutedEventHandler(btnAudioTheme_Checked);
				_ThemeGruppe[thGruppe].pnlAudioTheme.btnAudioTheme.Unchecked += new RoutedEventHandler(btnAudioTheme_Unchecked);
			}
		}

		private void pbarThemeActBGTitel_MouseMove(object sender, MouseEventArgs e)
		{
			if (AktThemeGruppe != null)
			{
				int thGruppe = Convert.ToInt16(((ProgressBar)sender).Tag);

				if (thGruppe >= 0 && _ThemeGruppe[thGruppe].Musik != null && AktThemeGruppe.pnlAudioTheme.pbarActBGTitel == (sender as ProgressBar))
				{
					int momAktSong = _ThemeGruppe[thGruppe].Musik._listZeile.FindIndex(t => t.istLaufend == true);
					if (momAktSong >= 0)
					{
						TimeSpan ts = TimeSpan.FromMilliseconds(
							e.GetPosition(sender as ProgressBar).X / ((double)((sender as ProgressBar).ActualWidth)) * (sender as ProgressBar).Maximum);
						(sender as ProgressBar).ToolTip = string.Format("{0:00}:{1:00}", (int)ts.Minutes, ts.Seconds);
					}
				}
			}
		}


		private void AktualisiereThemeEditor()
		{
			AktualisiereThemeBGPlaylist();
			AktualisiereThemeKlangPlaylist();
		}

		private GruppenObjekt LadeThemeGruppe(Audio_Playlist aPlaylist)
		{
			//MusikTheme laden
			if (aPlaylist != null)
			{
				tcKlang.SelectedIndex = tcKlang.Items.Count - 2;
				tiPlus_MouseUp(null, null);
				int hgItemPos = 0;
				while (((ListBoxItem)lbKlang.Items[hgItemPos]).Content.ToString() != aPlaylist.Name)
					hgItemPos++;
				lbKlang.SelectedIndex = hgItemPos;

				GruppenObjekt grpObjThemeHG = _GrpObjecte.Find(t => t.seite.Equals(tcKlang.SelectedIndex));
				((TabItemControl)tcKlang.SelectedItem).Visibility = Visibility.Collapsed;
				grpObjThemeHG.grdKlang.Visibility = Visibility.Collapsed;
				grpObjThemeHG.ticKlang._textBlockTitel.Text = grpObjThemeHG.playlistName + "Collapsed";

				return grpObjThemeHG;
			}
			else
				return null;
		}
		private void AktualisiereThemeGruppe(int start, bool UdpateEditor)
		{
			Global.SetIsBusy(true, string.Format("Lade Audio-Themes Informationen..."));
			List<Audio_Theme> aThemes = Global.ContextAudio.ThemeListe;

			for (int i = start; i < aThemes.Count; i++)
			{
				Global.SetIsBusy(true, string.Format("Lade Audio-Theme: " + (i + 1) + " von " + aThemes.Count));
				(App.Current.MainWindow as View.MainView).UpdateLayout();
			
				ThemeGruppe _ThemeGrp = new ThemeGruppe();
				_ThemeGrp.dbAudioTheme = aThemes[i];
				_ThemeGrp.ThemeName = aThemes[i].Name;

				rbKlangAlle.IsChecked = true;
				AktualisiereKlangPlaylist();

				_ThemeGrp.pnlAudioTheme = new AudioTheme();
				_ThemeGrp.pnlAudioTheme.Tag = btnThemeEdit.IsChecked.Value ? 0 : 1;
				_ThemeGrp.pnlAudioTheme.Name = "pnlTheme" + i;
				_ThemeGrp.pnlAudioTheme.btnAudioTheme.Tag = i;
				_ThemeGrp.pnlAudioTheme.btnAudioTheme.Checked += new RoutedEventHandler(btnAudioTheme_Checked);
				_ThemeGrp.pnlAudioTheme.btnAudioTheme.Unchecked += new RoutedEventHandler(btnAudioTheme_Unchecked);
				_ThemeGrp.pnlAudioTheme.btnPlayNext.Tag = i;
				_ThemeGrp.pnlAudioTheme.btnPlayNext.Click += new RoutedEventHandler(btnThemePlayNext_Click);

				_ThemeGrp.pnlAudioTheme.sldVolMusik.Value = _ThemeGrp.dbAudioTheme.Hintergrund_VolMod;
				_ThemeGrp.pnlAudioTheme.sldVolMusik.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldThemeVolHG_ValueChanged);
				_ThemeGrp.pnlAudioTheme.sldVolMusik.Tag = i;
				_ThemeGrp.pnlAudioTheme.sldVolGeraeusche.Value = _ThemeGrp.dbAudioTheme.Klang_VolMod;
				_ThemeGrp.pnlAudioTheme.sldVolGeraeusche.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldThemeVolGeraeusche_ValueChanged);
				_ThemeGrp.pnlAudioTheme.sldVolGeraeusche.Tag = i;

				_ThemeGrp.pnlAudioTheme.pbarActBGTitel.Tag = i;
				_ThemeGrp.pnlAudioTheme.pbarActBGTitel.MouseLeftButtonDown += new MouseButtonEventHandler(pbarThemeActBGTitel_MouseLeftButtonDown);
				_ThemeGrp.pnlAudioTheme.pbarActBGTitel.MouseMove += new MouseEventHandler(pbarThemeActBGTitel_MouseMove);

				_ThemeGrp.HGThemePlaylist = aThemes[i].Audio_Playlist.FirstOrDefault(t => t.Hintergrundmusik == true) != null ?
											aThemes[i].Audio_Playlist.FirstOrDefault(t => t.Hintergrundmusik == true) : null;

				_ThemeGrp.Musik = LadeThemeGruppe(_ThemeGrp.HGThemePlaylist);
				_ThemeGrp.pnlAudioTheme.lblThemeName.Content = _ThemeGrp.dbAudioTheme.Name;
						 

				//Klang-Themes laden
				for (int x = 0; x < aThemes[i].Audio_Playlist.Count; x++)
				{
					if (!aThemes[i].Audio_Playlist.ElementAt(x).Hintergrundmusik)
					{
						tcKlang.SelectedIndex = tcKlang.Items.Count - 2;
						tiPlus_MouseUp(null, null);

						int plylstItemPos = 0;
						while (((ListBoxItem)lbKlang.Items[plylstItemPos]).Content.ToString() != aThemes[i].Audio_Playlist.ElementAt(x).Name)
							plylstItemPos++;
						lbKlang.SelectedIndex = plylstItemPos;

						GruppenObjekt grpObjTheme = _GrpObjecte.Find(t => t.seite.Equals(tcKlang.SelectedIndex));
						((TabItemControl)tcKlang.SelectedItem).Visibility = Visibility.Collapsed;
						grpObjTheme.ticKlang._textBlockTitel.Text = grpObjTheme.playlistName + "Collapsed";
						_ThemeGrp.Geraeusche.Add(grpObjTheme);
					}
				}
				//Theme hinzufügen
				_ThemeGruppe.Add(_ThemeGrp);
				_ThemeGrp.pnlAudioTheme.sldVolMusik.Value = _ThemeGrp.dbAudioTheme.Hintergrund_VolMod;
				_ThemeGrp.pnlAudioTheme.sldVolGeraeusche.Value = _ThemeGrp.dbAudioTheme.Klang_VolMod;
			}
			
			if (UdpateEditor)
				AktualisiereThemeEditor();

			// Update UnterThemes
			_ThemeGruppe.ForEach(delegate (ThemeGruppe thGrp)
			{
				foreach (Audio_Theme aTheme in thGrp.dbAudioTheme.Audio_Theme1)
				{
					thGrp.UnterTheme.Add(ThemeLstThemePanel.First(t => t.dbAudioTheme.Audio_ThemeGUID.Equals(aTheme.Audio_ThemeGUID)));
				}
				UpdateAudioThemeToolTip(thGrp);
			});
			Global.SetIsBusy(false);
		}
		
		private void UpdateAudioThemeToolTip(ThemeGruppe _ThemeGrp)
		{
			string s = (_ThemeGrp.HGThemePlaylist != null && _ThemeGrp.HGThemePlaylist.Name != null) ? 
				s = "Musik:    " + _ThemeGrp.HGThemePlaylist.Name : "";
			
			for (int i = 0; i < _ThemeGrp.Geraeusche.Count; i++)
			{
				if (s != "" && _ThemeGrp.Geraeusche.Count > 0) s += Environment.NewLine;
				s += "Klang " + (i + 1) + ":  " + _ThemeGrp.Geraeusche[i].playlistName;
			}

			for (int i = 0; i < _ThemeGrp.UnterTheme.Count; i++)
			{
				if (s != "" && _ThemeGrp.UnterTheme.Count > 0) s += Environment.NewLine;
				s += "Sub-Theme " + (i + 1) + ":  " + _ThemeGrp.UnterTheme[i].pnlAudioTheme.lblThemeName.Content;
			}
			_ThemeGruppe.FindAll(t => t.dbAudioTheme.Audio_ThemeGUID.Equals(_ThemeGrp.dbAudioTheme.Audio_ThemeGUID)).ForEach(t => t.pnlAudioTheme.ToolTip = s);
			ThemeLstThemePanel.FindAll(t => t.dbAudioTheme.Audio_ThemeGUID.Equals(_ThemeGrp.dbAudioTheme.Audio_ThemeGUID)).ForEach(t => t.pnlAudioTheme.ToolTip = s);
		}

		private void btnThemeUpdate_Click(object sender, RoutedEventArgs e)
		{
			MyTimer.start_timer();
			Global.SetIsBusy(true, string.Format("Theme-Speicher bereinigen..."));
			
			if (AktThemeGruppe != null)
			{
				AktThemeGruppe.pnlAudioTheme.imgPlay.Tag = 0;
				btnAudioTheme_Unchecked(AktThemeGruppe.pnlAudioTheme.btnAudioTheme, new RoutedEventArgs());
			}

			for (int i = 0; i < _ThemeGruppe.Count; i++)
			{
				if (!Convert.ToBoolean(_ThemeGruppe[i].pnlAudioTheme.Tag) && _ThemeGruppe[i].pnlAudioTheme.btnAudioTheme.IsChecked.Value)
				{
					_ThemeGruppe[i].pnlAudioTheme.imgPlay.Tag = 0;
					btnAudioTheme_Unchecked(_ThemeGruppe[i].pnlAudioTheme.btnAudioTheme, new RoutedEventArgs());
				}
				_ThemeGruppe[i].pnlAudioTheme.Tag = btnThemeEdit.IsChecked.Value;
			}

			//Theme-Listen-Var löschen
			ThemeLstBGPanel.RemoveRange(0, ThemeLstBGPanel.Count);
			ThemeLstKlangPanel.RemoveRange(0, ThemeLstKlangPanel.Count);
			ThemeLstThemePanel.RemoveRange(0, ThemeLstThemePanel.Count);

			//Themelist-Panels löschen
			WrapPnlGeraeusche.Children.RemoveRange(0, WrapPnlGeraeusche.Children.Count);
			WrapPnlMusik.Children.RemoveRange(0, WrapPnlMusik.Children.Count);
			WrapPnlThemes.Children.RemoveRange(0, WrapPnlThemes.Children.Count);

			//Theme-Panels löschen
			for (int i = 0; i < _ThemeGruppe.Count; i++)
				WrapPnlUebersicht.Children.Remove(_ThemeGruppe[i].pnlAudioTheme);
			_ThemeGruppe.Clear();

			MyTimer.stop_timer();       
			AktualisiereThemeGruppe(0, true);
			ShowThemeGruppen(0);
			
			btnThemeEdit.Visibility = Visibility.Visible;

			ZeigeKlangGerneral(-1, false);
			MyTimer.stop_timer();

			WrapPnlUebersicht.IsEnabled = true;

			Global.SetIsBusy(false);
		}

		private static int CompareDinosByLength(string x, string y)
		{
			if (x == null)
			{
				if (y == null)
					return 0;
				else
					return -1;
			}
			else
			{
				if (y == null)
					return 1;
				else
				{
					int retval = x.Length.CompareTo(y.Length);

					if (retval != 0)
						return retval;
					else
						return x.CompareTo(y);
				}
			}
		}

		private void ShowThemeGruppen(int start)
		{
			lblNoThemes.Content = "Alle Audio-Themes geladen." + Environment.NewLine + Environment.NewLine + "Bitte warten...";
			lblNoThemes.Refresh();
			tcKlang.SelectedIndex = 0;
			lblNoThemes.Content = _ThemeGruppe.Count == 0 ? "Keine Audio-Themes gefunden" : "";

			for (int i = start; i < _ThemeGruppe.Count; i++)  //start
			{
				if (_ThemeGruppe[i].pnlAudioTheme.Parent == null)
					WrapPnlUebersicht.Children.Insert(getSortedThemeUebersichtPos(_ThemeGruppe, _ThemeGruppe[i].pnlAudioTheme), _ThemeGruppe[i].pnlAudioTheme);
				else
				{
					int i_neu = Convert.ToInt16(_ThemeGruppe[i].pnlAudioTheme.btnAudioTheme.Tag) - 1;
					_ThemeGruppe[i].pnlAudioTheme.btnAudioTheme.Tag = i_neu;
					_ThemeGruppe[i].pnlAudioTheme.btnPlayNext.Tag = i_neu;
					_ThemeGruppe[i].pnlAudioTheme.sldVolMusik.Tag = i_neu;
					_ThemeGruppe[i].pnlAudioTheme.sldVolGeraeusche.Tag = i_neu;
				}
			}
			lblNoThemes.Visibility = _ThemeGruppe.Count == 0 ? Visibility.Visible : Visibility.Hidden;
			btnThemeNeu.Visibility = Visibility.Visible;
		}

		private void btnBGthemeAngehakt_Checked(object sender, RoutedEventArgs e)
		{
			if (!AktThemeGruppe.ThemeJustRead)
			{
				if (AktThemeGruppe.Musik != null && AktThemeGruppe.Musik.ticKlang != null)
				{        
					HitchPanel hBGPanel;
					hBGPanel = ThemeLstBGPanel.Find(t => t.Tag.ToString() == Convert.ToString(AktThemeGruppe.Musik.Audio_Playlist_GUID));
					if (hBGPanel == null && AktThemeGruppe.Musik._listZeile.Count == 0)
						hBGPanel = ThemeLstBGPanel.Find(t => t.lblThemeName.Content.Equals(AktThemeGruppe.Musik.ticKlang._textBlockTitel.Text));

					if (hBGPanel != null && hBGPanel.btnAngehakt.IsChecked.Value)               
						hBGPanel.btnHitchPanel.IsChecked = false; 
				}

				int index = _ThemeGruppe.FindIndex(t => t.dbAudioTheme.Audio_ThemeGUID == AktThemeGruppe.dbAudioTheme.Audio_ThemeGUID);
				if (index < 0)
				{
					Audio_Theme theme = new Audio_Theme();
					aktiveThemeGruppe = index;
					theme.Name = AktThemeGruppe.ThemeName;
					AktThemeGruppe.dbAudioTheme = theme;
				}
				aktiveThemeGruppe = index;

				AktThemeGruppe.dbAudioTheme.Name = AktThemeGruppe.ThemeName; ///****

				Audio_Playlist aThemePlaylist = new Audio_Playlist();
				aThemePlaylist = Global.ContextAudio.PlaylistListe.Find(t => t.Audio_PlaylistGUID.Equals((sender as ToggleButton).Tag));

				if (!AktThemeGruppe.dbAudioTheme.Audio_Playlist.Contains(aThemePlaylist))
				{
					AktThemeGruppe.dbAudioTheme.Audio_Playlist.Add(aThemePlaylist);
					AktThemeGruppe.HGThemePlaylist = aThemePlaylist;  ///*****

					GruppenObjekt grpObjThemeHG = _GrpObjecte.Find(t => t.Audio_Playlist_GUID.Equals(AktThemeGruppe.HGThemePlaylist.Audio_PlaylistGUID));

					if (grpObjThemeHG == null)  //Noch nicht geladen
					{
						tcKlang.SelectedIndex = tcKlang.Items.Count - 2;
						tiPlus_MouseUp(null, null);
						int plylstItemPos = 0;
						if (lbKlang.Items.Count == 0)                            
							AktualisiereKlangPlaylist();
						while (((ListBoxItem)lbKlang.Items[plylstItemPos]).Content.ToString() != aThemePlaylist.Name)
							plylstItemPos++;
						lbKlang.SelectedIndex = plylstItemPos;

						GruppenObjekt grpObjTheme = _GrpObjecte.Find(t => t.seite.Equals(tcKlang.SelectedIndex));
						((TabItemControl)tcKlang.SelectedItem).Visibility = Visibility.Collapsed;
						grpObjTheme.ticKlang._textBlockTitel.Text = grpObjTheme.playlistName + "Collapsed";
						
						AktThemeGruppe.Musik = grpObjTheme;

						if (Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag)) //Kein Editiermodus
						{
							if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
								AktThemeGruppe.Musik.sollBtnGedrueckt = 1;
							if (Convert.ToBoolean(grpObjTheme.btnKlangPause.Tag))                                              // noch nicht laufend
							{
								tcKlang.Tag = grpObjTheme.seite;
								grpObjTheme.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
							}
						}
					}
					else
					{
						AktThemeGruppe.Musik = grpObjThemeHG;

						if (Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag))
						{
							if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
								AktThemeGruppe.Musik.sollBtnGedrueckt = 1;
							if (Convert.ToBoolean(grpObjThemeHG.btnKlangPause.Tag))                                              // noch nicht laufend
							{
								tcKlang.Tag = grpObjThemeHG.seite;
								grpObjThemeHG.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
							}
						}
					}
					Global.ContextAudio.Update<Audio_Theme>(AktThemeGruppe.dbAudioTheme);
				}
				UpdateAudioThemeToolTip(AktThemeGruppe);
			}
		}
		
		private void btnKlangThemeAngehakt_Checked(object sender, RoutedEventArgs e)
		{
			if (!AktThemeGruppe.ThemeJustRead)
			{
				int index = _ThemeGruppe.FindIndex(t => t.dbAudioTheme.Audio_ThemeGUID == AktThemeGruppe.dbAudioTheme.Audio_ThemeGUID);
				if (index < 0)
				{
					Audio_Theme theme = new Audio_Theme();
					aktiveThemeGruppe = index;
					theme.Name = AktThemeGruppe.ThemeName;
					AktThemeGruppe.dbAudioTheme = theme;
				}
				aktiveThemeGruppe = index;

				Audio_Playlist aThemePlaylist = new Audio_Playlist();
				aThemePlaylist = Global.ContextAudio.PlaylistListe.Find(t => t.Audio_PlaylistGUID.Equals((sender as ToggleButton).Tag));

				if (!AktThemeGruppe.dbAudioTheme.Audio_Playlist.Contains(aThemePlaylist))
				{
					AktThemeGruppe.dbAudioTheme.Audio_Playlist.Add(aThemePlaylist);

					GruppenObjekt grpKlang = _GrpObjecte.Find(t => t.Audio_Playlist_GUID == aThemePlaylist.Audio_PlaylistGUID);
					if (grpKlang == null)   //Noch nicht geladen
					{
						tcKlang.SelectedIndex = tcKlang.Items.Count - 2;
						tiPlus_MouseUp(null, null);

						int plylstItemPos = 0;
						while (((ListBoxItem)lbKlang.Items[plylstItemPos]).Content.ToString() != aThemePlaylist.Name)
							plylstItemPos++;
						lbKlang.SelectedIndex = plylstItemPos;

						GruppenObjekt grpObjTheme = _GrpObjecte.Find(t => t.seite.Equals(tcKlang.SelectedIndex));
						((TabItemControl)tcKlang.SelectedItem).Visibility = Visibility.Collapsed;
						grpObjTheme.ticKlang._textBlockTitel.Text = grpObjTheme.playlistName + "Collapsed";

						AktThemeGruppe.Geraeusche.Add(grpObjTheme);
						if (Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag))
						{
							if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
								AktThemeGruppe.Geraeusche[AktThemeGruppe.Geraeusche.Count - 1].sollBtnGedrueckt++;
							if (Convert.ToBoolean(grpObjTheme.btnKlangPause.Tag))                                              // noch nicht laufend
							{
								tcKlang.Tag = grpObjTheme.seite;
								grpObjTheme.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
							}
						}
					}
					else
					{
						AktThemeGruppe.Geraeusche.Add(grpKlang);
						if (Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag))
						{                            
							if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
								AktThemeGruppe.Geraeusche[AktThemeGruppe.Geraeusche.Count-1].sollBtnGedrueckt++;
							if (Convert.ToBoolean(grpKlang.btnKlangPause.Tag))                                              // noch nicht laufend
							{
								tcKlang.Tag = grpKlang.seite;
								grpKlang.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
							}
						}
					}
					Global.ContextAudio.Update<Audio_Theme>(AktThemeGruppe.dbAudioTheme);
				}
				UpdateAudioThemeToolTip(AktThemeGruppe);
			}
		}

		private void btnBGthemeAngehakt_UnChecked(object sender, RoutedEventArgs e)
		{
			if (!AktThemeGruppe.ThemeJustRead && AktThemeGruppe.Musik.ticKlang != null)
			{
				if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
					AktThemeGruppe.Musik.sollBtnGedrueckt = 0;
				if (!Convert.ToBoolean(AktThemeGruppe.Musik.btnKlangPause.Tag) && AktThemeGruppe.Musik.sollBtnGedrueckt >= 0)
				{
					tcKlang.Tag = AktThemeGruppe.Musik.seite;
					if (!btnThemeEdit.IsChecked.Value && !Convert.ToBoolean(AktThemeGruppe.Musik.btnKlangPause.Tag))    //Kein Editiermodus                
						AktThemeGruppe.Musik.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
				}
				AktThemeGruppe.Musik = null;
				AktThemeGruppe.dbAudioTheme.Audio_Playlist.Remove(AktThemeGruppe.HGThemePlaylist);
				AktThemeGruppe.HGThemePlaylist = null;
				Global.ContextAudio.Update<Audio_Theme>(AktThemeGruppe.dbAudioTheme);
				UpdateAudioThemeToolTip(AktThemeGruppe);
				AktThemeGruppe.pnlAudioTheme.lblActBGTitel.Content = "kein aktiver Musiktitel";
			}
		}

		private void btnKlangThemeAngehakt_UnChecked(object sender, RoutedEventArgs e)
		{
			if (!AktThemeGruppe.ThemeJustRead)
			{
				Audio_Playlist aThemePlaylist = AktThemeGruppe.dbAudioTheme.Audio_Playlist.Where(t => t.Audio_PlaylistGUID.Equals((sender as ToggleButton).Tag)).First();

				int klangPos = AktThemeGruppe.Geraeusche.FindIndex(t => t.Audio_Playlist_GUID == aThemePlaylist.Audio_PlaylistGUID);

				if (Convert.ToBoolean(AktThemeGruppe.pnlAudioTheme.Tag))
				{
					if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
						AktThemeGruppe.Geraeusche[klangPos].sollBtnGedrueckt--;
					if (!Convert.ToBoolean(AktThemeGruppe.Geraeusche[klangPos].btnKlangPause.Tag) && AktThemeGruppe.Geraeusche[klangPos].sollBtnGedrueckt >= 0)
					{
						tcKlang.Tag = AktThemeGruppe.Geraeusche[klangPos].seite;
						AktThemeGruppe.Geraeusche[klangPos].btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
					}
				}
				AktThemeGruppe.Geraeusche.RemoveAt(klangPos);
				AktThemeGruppe.dbAudioTheme.Audio_Playlist.Remove(aThemePlaylist);
				Global.ContextAudio.Update<Audio_Theme>(AktThemeGruppe.dbAudioTheme);
				UpdateAudioThemeToolTip(AktThemeGruppe);
			}
		}

		private void AlleKlangzeilenSpeichern(int posObjGruppe)
		{   
			for (int i = 0; i < _GrpObjecte[posObjGruppe]._listZeile.Count; i++)
				plyTitelToSave.Add(_GrpObjecte[posObjGruppe]._listZeile[i].audiotitel);

			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
		}

		private void btnThemeNeu_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				btnThemeEdit.IsChecked = true; 
				btnThemeEdit_Click(btnThemeEdit, new RoutedEventArgs());
				
				int i = _ThemeGruppe.Count;
				ThemeGruppe _ThemeGrp = new ThemeGruppe();
				string s = tboxThemeBezeichnung.Text != "" ? tboxThemeBezeichnung.Text : "Neues_Theme";
				int sub = 1;
				while (_ThemeGruppe.Exists(t => t.dbAudioTheme.Name == s))
				{
					s = "Neues_Theme_" + sub;
					sub++;
				}
				
				_ThemeGrp.ThemeName = s;
				_ThemeGrp.pnlAudioTheme = new AudioTheme();
				_ThemeGrp.pnlAudioTheme.Tag = btnThemeEdit.IsChecked.Value ? 0 : 1; 
				_ThemeGrp.pnlAudioTheme.Name = "pnlTheme" + i;
				_ThemeGrp.pnlAudioTheme.btnAudioTheme.Tag = i;
				_ThemeGrp.pnlAudioTheme.btnAudioTheme.Checked += new RoutedEventHandler(btnAudioTheme_Checked);
				_ThemeGrp.pnlAudioTheme.btnAudioTheme.Unchecked += new RoutedEventHandler(btnAudioTheme_Unchecked);
				_ThemeGrp.pnlAudioTheme.btnPlayNext.Tag = i;
				_ThemeGrp.pnlAudioTheme.imgPlay.Tag = 1; 
				_ThemeGrp.pnlAudioTheme.imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/feder.png"));
				_ThemeGrp.pnlAudioTheme.imgPlay.Visibility = Visibility.Visible;
				_ThemeGrp.pnlAudioTheme.btnPlayNext.Click += new RoutedEventHandler(btnThemePlayNext_Click);
				_ThemeGrp.dbAudioTheme.Hintergrund_VolMod = Convert.ToInt16(_ThemeGrp.pnlAudioTheme.sldVolMusik.Value);
				_ThemeGrp.dbAudioTheme.Klang_VolMod = Convert.ToInt16(_ThemeGrp.pnlAudioTheme.sldVolGeraeusche.Value);
				_ThemeGrp.pnlAudioTheme.sldVolMusik.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldThemeVolHG_ValueChanged);
				_ThemeGrp.pnlAudioTheme.sldVolMusik.Tag = i;
				_ThemeGrp.pnlAudioTheme.sldVolGeraeusche.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldThemeVolGeraeusche_ValueChanged);
				_ThemeGrp.pnlAudioTheme.sldVolGeraeusche.Tag = i;

				_ThemeGrp.pnlAudioTheme.pbarActBGTitel.Tag = i;
				_ThemeGrp.pnlAudioTheme.pbarActBGTitel.MouseLeftButtonDown += new MouseButtonEventHandler(pbarThemeActBGTitel_MouseLeftButtonDown);
				_ThemeGrp.pnlAudioTheme.pbarActBGTitel.MouseMove += new MouseEventHandler(pbarThemeActBGTitel_MouseMove);
				_ThemeGrp.pnlAudioTheme.lblThemeName.Content = s;

				_ThemeGrp.dbAudioTheme.Name = s;
				_ThemeGruppe.Add(_ThemeGrp);
				aktiveThemeGruppe = _ThemeGruppe.Count - 1;
				AktThemeGruppe = _ThemeGruppe[aktiveThemeGruppe];
				Global.ContextAudio.Update<Audio_Theme>(AktThemeGruppe.dbAudioTheme);
				Global.ContextAudio.Save();
				AktualisiereThemeGruppe(_ThemeGruppe.Count - 1, false);
				ShowThemeGruppen(_ThemeGruppe.Count - 1);

				_ThemeGruppe[_ThemeGruppe.Count - 1].pnlAudioTheme.btnAudioTheme.Checked -= new RoutedEventHandler(btnAudioTheme_Checked);
				_ThemeGruppe[_ThemeGruppe.Count - 1].pnlAudioTheme.btnAudioTheme.IsChecked = true;
				_ThemeGruppe[_ThemeGruppe.Count - 1].pnlAudioTheme.btnAudioTheme.Checked += new RoutedEventHandler(btnAudioTheme_Checked);
				tboxThemeBezeichnung.Text = s;
				tboxThemeBezeichnung.Visibility = Visibility.Visible;

				ThemeLstKlangPanel.ForEach(t => t.IsEnabled = true);
				ThemeLstBGPanel.ForEach(t => t.IsEnabled = true);
				ThemeLstThemePanel.ForEach(t => t.pnlAudioTheme.IsEnabled = true);

				if (btnThemeEdit.IsChecked.Value)
					btnThemeLöschen.Visibility = Visibility.Visible;
				tboxThemeBezeichnung.Focus();
			}
			catch
			{
				var errWin = new MsgWindow("Datenbankfehler", "AudioTheme schon vorhanden oder Fehler beim Schreiben. Bitte wiederholen Sie den Vorgang und wählen einen anderen Titel");
				errWin.ShowDialog();
				errWin.Close();
			}
		}

		private void sldThemeVolHG_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			int modifikator = Convert.ToInt32(e.NewValue) * 2;
			int themePos = Convert.ToInt32(((Slider)sender).Tag);
			if (_ThemeGruppe[themePos].Musik != null)
			{
				_ThemeGruppe[themePos].Musik.Vol_ThemeMod = modifikator;
				_ThemeGruppe[themePos].Musik.Vol_PlaylistMod = 100;
			}

			if (_ThemeGruppe[themePos].dbAudioTheme.Hintergrund_VolMod != modifikator / 2)
			{
				_ThemeGruppe[themePos].dbAudioTheme.Hintergrund_VolMod = modifikator / 2;
				Global.ContextAudio.Update<Audio_Theme>(_ThemeGruppe[themePos].dbAudioTheme);
				ThemeLstThemePanel.FindAll(t => t.ThemeName == _ThemeGruppe[themePos].ThemeName).ForEach(t => t.pnlAudioTheme.sldVolMusik.Value = ((Slider)sender).Value);
				_ThemeGruppe.FindAll(t => t.ThemeName == _ThemeGruppe[themePos].ThemeName).ForEach(t => t.pnlAudioTheme.sldVolMusik.Value = ((Slider)sender).Value);
			}
		}
		
		private void sldThemeVolGeraeusche_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			int modifikator = Convert.ToInt32(e.NewValue) * 2;
			int themePos = Convert.ToInt32(((Slider)sender).Tag);

			for (int i = 0; i < _ThemeGruppe[themePos].Geraeusche.Count; i++)
			{
				_ThemeGruppe[themePos].Geraeusche[i].Vol_ThemeMod = modifikator;
				_ThemeGruppe[themePos].Geraeusche[i].Vol_PlaylistMod = 100;
			}

			if (_ThemeGruppe[themePos].dbAudioTheme.Klang_VolMod != modifikator / 2)
			{
				_ThemeGruppe[themePos].dbAudioTheme.Klang_VolMod = modifikator / 2;
				Global.ContextAudio.Update<Audio_Theme>(_ThemeGruppe[themePos].dbAudioTheme);
				ThemeLstThemePanel.FindAll(t => t.ThemeName == _ThemeGruppe[themePos].ThemeName).ForEach(t => t.pnlAudioTheme.sldVolGeraeusche.Value = ((Slider)sender).Value);
				_ThemeGruppe.FindAll(t => t.ThemeName == _ThemeGruppe[themePos].ThemeName).ForEach(t => t.pnlAudioTheme.sldVolGeraeusche.Value = ((Slider)sender).Value);
			}
		}

		private void tboxThemeBezeichnung_KeyUp(object sender, KeyEventArgs e)
		{
			if (tboxThemeBezeichnung.Text != "")
			{
				AktThemeGruppe.pnlAudioTheme.lblThemeName.Content = tboxThemeBezeichnung.Text;
				AktThemeGruppe.ThemeName = tboxThemeBezeichnung.Text;
				AktThemeGruppe.dbAudioTheme.Name = AktThemeGruppe.ThemeName;
				_ThemeGruppe[aktiveThemeGruppe].dbAudioTheme.Name = AktThemeGruppe.ThemeName;
				Global.ContextAudio.Update<Audio_Theme>(AktThemeGruppe.dbAudioTheme);
			}
		}

		private void btnThemeLöschen_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AktThemeGruppe.ThemeJustRead = true;
				List<HitchPanel> hpBG = ThemeLstBGPanel.FindAll(t => t.btnAngehakt.IsChecked.Value).ToList();
				for (int i = 0; i < hpBG.Count; i++)
					hpBG[i].btnAngehakt.IsChecked = false;

				List<HitchPanel> hpKlang = ThemeLstKlangPanel.FindAll(t => t.btnAngehakt.IsChecked.Value).ToList();
				for (int i = 0; i < hpKlang.Count; i++)
						hpKlang[i].btnAngehakt.IsChecked = false;
				AktThemeGruppe.ThemeJustRead = false;

				int th = aktiveThemeGruppe;
				aktiveThemeGruppe = -1;
				AktThemeGruppe = null;
				WrapPnlUebersicht.Children.Remove(_ThemeGruppe[th].pnlAudioTheme);
				
				Global.ContextAudio.Delete<Audio_Theme>(_ThemeGruppe[th].dbAudioTheme);
				_ThemeGruppe.Remove(_ThemeGruppe[th]);
				tboxThemeBezeichnung.Visibility = Visibility.Hidden;
				tboxThemeBezeichnung.Text = "";
				btnThemeLöschen.Visibility = Visibility.Hidden;
				ShowThemeGruppen(th);

				ThemeLstKlangPanel.ForEach(t => t.IsEnabled = false);
				ThemeLstBGPanel.ForEach(t => t.IsEnabled = false);
				ThemeLstThemePanel.ForEach(t => t.pnlAudioTheme.IsEnabled = false);
			}
			catch
			{
				var errWin = new MsgWindow("Datenbankfehler", "AudioTheme konnte nicht gelöscht werden. Bitte wiederholen Sie den Vorgang");
				errWin.ShowDialog();
				errWin.Close();
			}
		}

		private void btnThemeEdit_Click(object sender, RoutedEventArgs e)
		{   
			for (int i = 0; i < _ThemeGruppe.Count; i++)
			{
				if (_ThemeGruppe[i].pnlAudioTheme.btnAudioTheme.IsChecked.Value)
				{
					_ThemeGruppe[i].ThemeJustRead = true;
					ThemeAusSchalten(_ThemeGruppe[i], true, !Convert.ToBoolean(_ThemeGruppe[i].pnlAudioTheme.Tag));
					_ThemeGruppe[i].pnlAudioTheme.imgPlay.Tag = 0;
					_ThemeGruppe[i].ThemeJustRead = false;
				}
				_ThemeGruppe[i].pnlAudioTheme.Tag = !btnThemeEdit.IsChecked.Value;
			}
			AktThemeGruppe = null;
			btnThemeLöschen.Visibility = Visibility.Hidden;

			ThemeLstKlangPanel.ForEach(t => t.IsEnabled = false);
			ThemeLstBGPanel.ForEach(t => t.IsEnabled = false);
			ThemeLstThemePanel.ForEach(t => t.pnlAudioTheme.IsEnabled = false);
			
			tboxThemeBezeichnung.Focus();
		}

		public void exThemeEditor_Expanded(object sender, RoutedEventArgs e)
		{
			grdThemes.RowDefinitions[1].Height = new GridLength((scrTheme.ExtentHeight +10 > 220) ? 230 : scrTheme.ExtentHeight, GridUnitType.Pixel);
			grdThemes.RowDefinitions[2].Height = new GridLength(22, GridUnitType.Star);
		}

		public void exThemeEditor_Collapsed(object sender, RoutedEventArgs e)
		{
			grdThemes.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
			grdThemes.RowDefinitions[2].Height = new GridLength(grdThemes.RowDefinitions[2].MinHeight);
		}

		public void _rbtnGleichSpielen_Click(object sender, RoutedEventArgs e)
		{
			if (IsInitialized)
				MeisterGeister.Logic.Settings.Einstellungen.AudioDirektAbspielen= (bool)_rbtnGleichSpielen.IsChecked;
		}

		public void BGFadingOut(Musik BG, bool playerStoppen, bool sofort)
		{
			DispatcherTimer _timer = new DispatcherTimer();

			double stVol = BG.mPlayer.Volume;
			//bool jetztRunterregeln =  false;
			_timer.Interval = TimeSpan.FromMilliseconds(fadingIntervall);            
			
			DateTime fadOutStart = DateTime.Now;
						
			_timer.Tick += new EventHandler(delegate(object s, EventArgs a)
			{
				if (!sofort && fadOutStart.Subtract(DateTime.Now).TotalMilliseconds > 5000)  //maximale Wartezeit zum FadingOut
					sofort = true;

				if (FadingIn_Started || sofort)
				{
					sofort = true;
					if (BG.FadingOutStarted)                                //solange Volume runterregeln bis der Titel extern das Fadingstoppt
					{
						if (BG.mPlayer != null)
						{
							stVol = (fadingTime != 0) ? stVol - (fadingIntervall / fadingTime) / 2 : 0;
							if (BG.mPlayer.Volume != stVol)                                
								BG.mPlayer.Volume = stVol;

							if (BG.mPlayer.Volume == 0)
							{
								if (playerStoppen && BG.FadingOutStarted)   //bei Volume 0 Fadingauf false und MediaPlayer freigeben
								{
									BG.mPlayer.Stop();
									BG.mPlayer.Close();
									BG.mPlayer = null;
								}
								if (!playerStoppen && BG.FadingOutStarted)
								{
									BG.mPlayer.Pause();
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
			
			_timer.Interval = TimeSpan.FromMilliseconds(fadingIntervall);
			
			DateTime fadOutStart = DateTime.Now;
	  //      bool sofortRunterregeln = false;
			
			_timer.Tick += new EventHandler(delegate(object s, EventArgs a)
			{
				double d = DateTime.Now.Subtract(fadOutStart).TotalMilliseconds;
				if (!sofort && d > 5000)                                 //maximale Wartezeit zum FadingOut
					sofort = true;

				if (FadingIn_Started || sofort)
				{
					sofort = true;
					if (klZeile.FadingOutStarted)                                   //solange Volume runterregeln bis der Titel extern das Fadingstoppt
					{
						if (klZeile._mplayer != null)
						{
							stVol = (fadingTime != 0) ? stVol - (fadingIntervall / fadingTime) / 2 : 0;
							if (klZeile._mplayer.Volume != stVol)
								klZeile._mplayer.Volume = stVol;

							if (klZeile._mplayer.Volume == 0)
							{
								if (playerStoppen && klZeile.FadingOutStarted)     //bei Volume 0 Fadingauf false und MediaPlayer freigeben
								{
									klZeile._mplayer.Stop();
									klZeile._mplayer.Close();
									klZeile._mplayer = null;
								}
								if (!playerStoppen && klZeile.FadingOutStarted)
								{
									klZeile._mplayer.Pause();
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

			_timer.Tick += new EventHandler(delegate(object s, EventArgs a)
			{
				FadingIn_Started = true;
				aktVol = (fadingTime != 0) ? aktVol + zielVol * fadingIntervall / fadingTime : zielVol;
				if (mplayer != null) mplayer.Volume = aktVol;

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

		public void imgBGStern_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (Convert.ToInt16(((Image)sender).Tag) == 1 && _BGPlayer.AktPlaylistTitel.Rating == 1)
				_BGPlayer.AktPlaylistTitel.Rating = 0;
			else
				_BGPlayer.AktPlaylistTitel.Rating = Convert.ToInt16(((Image)sender).Tag);

			plyTitelToSave.Add(_BGPlayer.AktPlaylistTitel);
			if (!plyTitelToSaveTimer.IsEnabled) plyTitelToSaveTimer.Start();
			starsUpdate();
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
			tcKlang_vorher = tcKlang.SelectedIndex;
			tcKlang_vorherTag = Convert.ToInt16(tcKlang.Tag);
		}

		public void tiUebersicht_LostFocus(object sender, RoutedEventArgs e)
		{
			tcKlang.SelectedIndex = tcKlang_vorher;
			tcKlang.Tag = tcKlang_vorherTag;
		}

		public void _sldFading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			fadingTime = _sldFading.Value;
			_sldFading.ToolTip = Math.Round(e.NewValue / 100, 1) + " Sekunden In-/Out-Fading";
			if (IsInitialized)
				MeisterGeister.Logic.Settings.Einstellungen.Fading = (int)e.NewValue;
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

			Audio_Playlist plylst = aPlaylistLengthCheck;
			for (int i = 0; i < plylst.Audio_Playlist_Titel.Count; i++)
			{
				file = plylst.Audio_Playlist_Titel.ElementAt(i).Audio_Titel.Pfad;
				if (file.Substring(1, 2) != @":\")
						file = (stdPfad.EndsWith(@"\"))? (stdPfad + file): (stdPfad + "\\" + file);

				if (File.Exists(file))
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
						aPlaylistLengthCheck.Audio_Playlist_Titel.ElementAt(i).Länge = mp.NaturalDuration.TimeSpan.TotalMilliseconds;
					}
					mp.Stop();
					mp.Close();
				}
			}
			aPlaylistLengthCheck.Länge = totalLength.TotalMilliseconds;
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error == null && aPlaylistLengthCheck.Länge != 0) 
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
                        _GrpObjecte.FirstOrDefault(t => t.Audio_Playlist_GUID == (Guid)(aPlaylistLengthCheck.Audio_PlaylistGUID)).totalTimePlylist = aPlaylistLengthCheck.Länge;
                        ((MusikZeile)lbPListMusik.Items[i]).tblkLänge.Text = TimeSpan.FromMilliseconds(aPlaylistLengthCheck.Länge).ToString(@"hh\:mm\:ss");
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
                {
                    if ((Guid)((MusikZeile)lbPListGeräusche.Items[i]).Tag == (Guid)(aPlaylistLengthCheck.Audio_PlaylistGUID))
                    {
                        _GrpObjecte.FirstOrDefault(t => t.Audio_Playlist_GUID == (Guid)(aPlaylistLengthCheck.Audio_PlaylistGUID)).totalTimePlylist = aPlaylistLengthCheck.Länge;
                        ((MusikZeile)lbPListGeräusche.Items[i]).tblkLänge.Text = TimeSpan.FromMilliseconds(aPlaylistLengthCheck.Länge).ToString(@"hh\:mm\:ss");
                        break;
                    }
                }
            }
		}

        private void tbKlangPlaylistFilter_TextChanged(object sender, TextChangedEventArgs e)
		{            
			for (int i = 0; i < lbKlang.Items.Count; i++)
				((ListBoxItem)lbKlang.Items[i]).Visibility = ((ListBoxItem)lbKlang.Items[i]).Content.ToString().ToLower().Contains(tbKlangPlaylistFilter.Text.ToLower())? 
					Visibility.Visible: Visibility.Collapsed;
		}
		
		private void tbKlangTopFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			int posObjGruppe = GetPosObjGruppe(GetObjGruppe(tcKlang.SelectedIndex));
			_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.audioZeile.lbiKlangRow != null).ForEach(delegate(KlangZeile klZeile)
			{
				klZeile.audioZeile.Visibility = (klZeile.audioZeile.chkTitel.Content.ToString().ToLower().Contains(((TextBox)(e.Source)).Text.ToLower()))? Visibility.Visible : Visibility.Collapsed;
			});
			  
		}

		private void tbKBGFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			for (int i = 0; i < lbBackground.Items.Count; i++)
			{
				((MusikZeile)lbBackground.Items[i]).Visibility = 
					((MusikZeile)lbBackground.Items[i]).tblkTitel.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ||
					((MusikZeile)lbBackground.Items[i]).tboxKategorie.Text.ToLower().Contains(tbBGFilter.Text.ToLower()) ?
					  Visibility.Visible : Visibility.Collapsed;
			}
		}

		private void tbThemeKlangPlaylistFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			ThemeLstKlangPanel.FindAll(t => t.lblThemeName.Content.ToString().ToLower().Contains(tbThemeKlangPlaylistFilter.Text.ToLower()) == false).ForEach(t => t.Visibility = Visibility.Collapsed);
			ThemeLstKlangPanel.FindAll(t => t.lblThemeName.Content.ToString().ToLower().Contains(tbThemeKlangPlaylistFilter.Text.ToLower())).ForEach(t => t.Visibility = Visibility.Visible);

			ThemeLstThemePanel.FindAll(t => t.pnlAudioTheme.lblThemeName.Content.ToString().ToLower().Contains(tbThemeKlangPlaylistFilter.Text.ToLower()) == false).ForEach(t => t.pnlAudioTheme.Visibility = Visibility.Collapsed);
			ThemeLstThemePanel.FindAll(t => t.pnlAudioTheme.lblThemeName.Content.ToString().ToLower().Contains(tbThemeKlangPlaylistFilter.Text.ToLower())).ForEach(t => t.pnlAudioTheme.Visibility = Visibility.Visible);			
		}

		private void tbThemeBGPlaylistFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			ThemeLstBGPanel.FindAll(t => t.lblThemeName.Content.ToString().ToLower().Contains(tbThemeBGPlaylistFilter.Text.ToLower()) == false).ForEach(t => t.Visibility = Visibility.Collapsed);
			ThemeLstBGPanel.FindAll(t => t.lblThemeName.Content.ToString().ToLower().Contains(tbThemeBGPlaylistFilter.Text.ToLower())).ForEach(t => t.Visibility = Visibility.Visible);			
		}

		private void imgBGFilter_MouseUp(object sender, MouseButtonEventArgs e)
		{
			((TextBox)((Grid)((Image)(e.Source)).Parent).Children.OfType<TextBox>().First()).Text = "";
		}
		
		public void _btnStdPfad_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (AktThemeGruppe != null)
				{
					AktThemeGruppe.pnlAudioTheme.imgPlay.Tag = 0;
					btnAudioTheme_Unchecked(AktThemeGruppe.pnlAudioTheme.btnAudioTheme, new RoutedEventArgs(Button.ClickEvent));
				}
				_GrpObjecte.FindAll(t => t._listZeile.Exists(t2 => t2.istLaufend)).ForEach(delegate(GruppenObjekt grpobj)
				{
					grpobj.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));				
				});

				var dialog = new System.Windows.Forms.FolderBrowserDialog();
				dialog.SelectedPath = MeisterGeister.Logic.Settings.Einstellungen.GetOrCreateEinstellung("AudioVerzeichnis", @"C:\");
				System.Windows.Forms.DialogResult result = dialog.ShowDialog();
				if (result == System.Windows.Forms.DialogResult.OK)
					_tbStdPfad.Text = _btnStdPfad.Tag.ToString();
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
			if (IsInitialized)
			{
				_btnStdPfad.Tag = _tbStdPfad.Text;
				MeisterGeister.Logic.Settings.Einstellungen.SetEinstellung("AudioVerzeichnis", _tbStdPfad.Text);
				stdPfad = _tbStdPfad.Text;
			}
		}

		public void slVolume_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			((Slider)sender).Value += (e.Delta > 1) ? 3 : -3;
		}

		public void sldPause_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			((Slider)sender).Value = (e.Delta > 1)?
				((((Slider)sender).Value != ((Slider)sender).Maximum)? 
					((Slider)sender).Ticks.First(t => t > ((Slider)sender).Value) :
					((Slider)sender).Value = ((Slider)sender).Maximum) :
				(e.Delta < 1 && ((Slider)sender).Value != ((Slider)sender).Minimum)?
					((Slider)sender).Ticks[((Slider)sender).Ticks.IndexOf(((Slider)sender).Value) - 1] : 0;
		}
		
		public void imgThemeKlangPlaylistRefresh_MouseDown(object sender, RoutedEventArgs e)
		{
			AktualisiereThemeKlangPlaylist();
		}

		public void imgThemeBGPlaylistRefresh_MouseDown(object sender, RoutedEventArgs e)
		{
			AktualisiereThemeBGPlaylist();
		}

		public int getSortedThemeListPos(List<HitchPanel> lstHPnl, HitchPanel hpnl)
		{
			IOrderedEnumerable<HitchPanel> ohp = lstHPnl.OrderBy(t => t.lblThemeName.Content);
			int i = 0;

			while (!ohp.ElementAt(i).Equals(hpnl) && i < lstHPnl.Count)
				i++;
			return i;
		}

		public int getSortedThemeUebersichtPos(List<ThemeGruppe> lstaTheme, AudioTheme aTheme)
		{
			List<string> lstS = new List<string>();

			for (int i = 0; i < WrapPnlUebersicht.Children.Count; i++)
				lstS.Add(((AudioTheme)WrapPnlUebersicht.Children[i]).lblThemeName.Content.ToString());

			lstS.Add(aTheme.lblThemeName.Content.ToString());
			lstS.Sort();
			return lstS.IndexOf(aTheme.lblThemeName.Content.ToString());
		}
		

		public void AktualisiereThemeKlangPlaylist()
		{
			List<Audio_Playlist> klangPlaylist = Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik == false).ToList();
			
			for (int i = 0; i < klangPlaylist.Count; i++)
			{                   
				// Noch nicht in der Liste
				if (ThemeLstKlangPanel.FindIndex(t => t.Tag.ToString() == klangPlaylist[i].Audio_PlaylistGUID.ToString()) == -1)
				{
					HitchPanel _ThemePanel = new HitchPanel();
					_ThemePanel.lblThemeName.Content = klangPlaylist[i].Name;
					_ThemePanel.Name = "ThemeKlangPanel" + i;
					_ThemePanel.Tag = klangPlaylist[i].Audio_PlaylistGUID;
					_ThemePanel.btnAngehakt.Tag = klangPlaylist[i].Audio_PlaylistGUID;
					_ThemePanel.btnAngehakt.Checked += new RoutedEventHandler(btnKlangThemeAngehakt_Checked);
					_ThemePanel.btnAngehakt.Unchecked += new RoutedEventHandler(btnKlangThemeAngehakt_UnChecked);
					_ThemePanel.IsEnabled = false;

					ThemeLstKlangPanel.Add(_ThemePanel);
					WrapPnlGeraeusche.Children.Insert(getSortedThemeListPos(ThemeLstKlangPanel, _ThemePanel), _ThemePanel);                    
				}
			}
			// Lösche alle nicht vorhandenen Panels
			for (int i = 0; i < ThemeLstKlangPanel.Count; i++)
			{
				if (klangPlaylist.FindIndex(t => t.Audio_PlaylistGUID.ToString() == ThemeLstKlangPanel[i].Tag.ToString()) == -1)
				{                    
					WrapPnlGeraeusche.Children.Remove(ThemeLstKlangPanel[i]);      //Themelist-Panels löschen                    
					ThemeLstKlangPanel.RemoveAt(i);                             //Theme-Listen-Var löschen
				}
			}
			
			// Alle ThemeMusik hinzufügen
			for (int i = 0; i < _ThemeGruppe.Count; i++)
			{
				if (ThemeLstThemePanel.FindIndex(t => t.pnlAudioTheme.Tag.ToString() == _ThemeGruppe[i].dbAudioTheme.Audio_ThemeGUID.ToString()) == -1)
				{
					ThemeGruppe aThemeGrp = new ThemeGruppe();
					aThemeGrp.Musik = _ThemeGruppe[i].Musik;
					aThemeGrp.dbAudioTheme = _ThemeGruppe[i].dbAudioTheme;
					aThemeGrp.ThemeName = _ThemeGruppe[i].ThemeName;
					aThemeGrp.Geraeusche = _ThemeGruppe[i].Geraeusche;

					AudioTheme aTheme = new AudioTheme();
					aTheme.lblThemeName.Content = _ThemeGruppe[i].pnlAudioTheme.lblThemeName.Content;
					aTheme.Name = "pnlKlangListTheme" + i;
					aTheme.Tag = _ThemeGruppe[i].dbAudioTheme.Audio_ThemeGUID;
					aTheme.btnAudioTheme.Tag = _ThemeGruppe[i].dbAudioTheme.Audio_ThemeGUID;
					aTheme.btnAudioTheme.Checked += new RoutedEventHandler(btnThemeThemeAngehakt_Checked);
					aTheme.btnAudioTheme.Unchecked += new RoutedEventHandler(btnThemeThemeAngehakt_UnChecked);
					aTheme.ToolTip = _ThemeGruppe[i].pnlAudioTheme.ToolTip;
					aTheme.imgPlay.Tag = -1;
					aTheme.imgPlay.Visibility = Visibility.Collapsed;
					aTheme.btnPlayNext.Visibility = Visibility.Collapsed;

					aTheme.sldVolMusik.Value = _ThemeGruppe[i].pnlAudioTheme.sldVolMusik.Value;
					aTheme.sldVolMusik.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldThemeVolHG_ValueChanged);
					aTheme.sldVolMusik.Tag = i;
					aTheme.sldVolGeraeusche.Value = _ThemeGruppe[i].pnlAudioTheme.sldVolGeraeusche.Value;
					aTheme.sldVolGeraeusche.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldThemeVolGeraeusche_ValueChanged);
					aTheme.sldVolGeraeusche.Tag = i;
					aTheme.pbarActBGTitel.Visibility = Visibility.Collapsed;
					aTheme.lblActBGTitel.Visibility = Visibility.Collapsed;
					aTheme.IsEnabled = false;
					aTheme.grdTop.Width = 176; //Endbreite = Wert + 16

					aThemeGrp.pnlAudioTheme = aTheme;
					ThemeLstThemePanel.Add(aThemeGrp);
					WrapPnlThemes.Children.Add(aTheme);
				}
			}
		
			// Lösche alle nicht vorhandenen Musik-Panels			
			for (int i = 0; i < ThemeLstThemePanel.Count; i++)
			{
				if (ThemeLstThemePanel.FindIndex(t => t.pnlAudioTheme.Tag.ToString() == ThemeLstThemePanel[i].pnlAudioTheme.Tag.ToString()) == -1)
				{
					WrapPnlThemes.Children.Remove(ThemeLstThemePanel[i].pnlAudioTheme);      //Musik-Themelist-Panels löschen                    
					ThemeLstThemePanel.RemoveAt(i);                                          //Musik-Theme-Listen-Var löschen
				}
			}
		}

	   
		public void AktualisiereThemeBGPlaylist()
		{
			List<Audio_Playlist> Musikplaylist = Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik == true).ToList();

			for (int i = 0; i < Musikplaylist.Count; i++)
			{   
				if (ThemeLstBGPanel.FindIndex(t => t.Tag.ToString() == Musikplaylist[i].Audio_PlaylistGUID.ToString()) == -1)
				{
					HitchPanel _ThemePanel = new HitchPanel();
					_ThemePanel.lblThemeName.Content = Musikplaylist[i].Name;
					_ThemePanel.Name = "ThemeBGPanel" + i;
					_ThemePanel.Tag = Musikplaylist[i].Audio_PlaylistGUID;
					_ThemePanel.btnAngehakt.Tag = Musikplaylist[i].Audio_PlaylistGUID;
					_ThemePanel.btnAngehakt.Checked += new RoutedEventHandler(btnBGthemeAngehakt_Checked);
					_ThemePanel.btnAngehakt.Unchecked += new RoutedEventHandler(btnBGthemeAngehakt_UnChecked);
					_ThemePanel.IsEnabled = false;

					ThemeLstBGPanel.Add(_ThemePanel);
					WrapPnlMusik.Children.Insert(getSortedThemeListPos(ThemeLstBGPanel, _ThemePanel), _ThemePanel);
				}
			}

			for (int i = 0; i < ThemeLstBGPanel.Count; i++)
			{
				if (Musikplaylist.FindIndex(t => t.Audio_PlaylistGUID.ToString() == ThemeLstBGPanel[i].Tag.ToString()) == -1)
				{
					WrapPnlMusik.Children.Remove(ThemeLstBGPanel[i]);     //Themelist-Panels löschen                    
					ThemeLstBGPanel.RemoveAt(i);                            //Theme-Listen-Var löschen
				}
			}
		}
		
		private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (e != null && e.Source != null && e.Source is TabItemControl
				&& Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
			{
				TabItemControl item = (TabItemControl)(e.Source);                
				DragDrop.DoDragDrop(item, item, DragDropEffects.All);
			}            
		}
		
		private void TabItem_Drop(object sender, DragEventArgs e)
		{
			if (e != null && e.Source != null
				&& (e.Source is TabItemControl || e.Source is ListBoxItem || e.Source is Image || e.Source is TextBlock))
			{
				TabItem target = null;
				if (e.Source is TabItemControl)
					target = (TabItemControl)(e.Source);
				else if (e.Source is ListBoxItem)
					target = (TabItemControl)((ListBoxItem)e.Source).Parent;
				else if (e.Source is Image)
					target = (TabItem)((ListBoxItem)((Image)e.Source).Parent).Parent;
				else if (e.Source is TextBlock)
					target = (TabItemControl)((ListBoxItem)((TextBlock)e.Source).Parent).Parent;
				if (e.Data != null)
				{
					TabItemControl source = (TabItemControl)(e.Data.GetData(typeof(TabItemControl)));
					if (!source.Equals(target))
					{
						if (target != null && target.Parent != null && target.Parent is TabControl)
						{
							TabControl tab = (TabControl)(target.Parent);
							int sourceIndex = tab.Items.IndexOf(source);
							int targetIndex = (tab.Items.IndexOf(target) < tab.Items.Count - 2) ? tab.Items.IndexOf(target) : tab.Items.IndexOf(target) - 1;

							int quellPosObjGruppe = GetPosObjGruppe(GetObjGruppe(sourceIndex));
							int quellSeite = _GrpObjecte[GetPosObjGruppe(GetObjGruppe(sourceIndex))].seite;
							int zielSeite = _GrpObjecte[GetPosObjGruppe(GetObjGruppe(targetIndex))].seite;

							_GrpObjecte[quellPosObjGruppe].seite = -2; //Temporär ausserhalb des Bereichs

							for (int i = quellSeite-1; i >= zielSeite; i--)
								_GrpObjecte[GetPosObjGruppe(GetObjGruppe(i))].seite++;

							_GrpObjecte[quellPosObjGruppe].seite = zielSeite;
							
							tab.SelectionChanged -= new SelectionChangedEventHandler(tcKlang_SelectionChanged);
							tab.Items.Remove(source);
							tab.Items.Insert(targetIndex, source);
							tab.SelectedItem = source;
							tab.Tag = tab.SelectedIndex;
							tab.SelectionChanged += new SelectionChangedEventHandler(tcKlang_SelectionChanged);
						}
					}
				}
			}
		}

		public void btnThemeThemeAngehakt_Checked(object sender, RoutedEventArgs e)
		{
			ThemeGruppe tGrp = ThemeLstThemePanel.First(t => t.dbAudioTheme.Audio_ThemeGUID == (Guid)((ToggleButton)e.Source).Tag);

			if (!AktThemeGruppe.ThemeJustRead)
			{
				if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
				{
					if (tGrp.Musik != null && tGrp.Musik.btnKlangPause != null)
					{
						tGrp.Musik.sollBtnGedrueckt = 1;
						if (Convert.ToBoolean(tGrp.Musik.btnKlangPause.Tag))                                              // noch nicht laufend
						{
							tcKlang.Tag = tGrp.Musik.seite;
							btnKlangPauseX_Click(tGrp.Musik.btnKlangPause, new RoutedEventArgs());
						}
					}

					for (int i = 0; i < tGrp.Geraeusche.Count; i++)
					{
						tGrp.Geraeusche[i].sollBtnGedrueckt++;
						if (Convert.ToBoolean(tGrp.Geraeusche[i].btnKlangPause.Tag))                                     // noch nicht laufend
						{
							tcKlang.Tag = tGrp.Geraeusche[i].seite;
							btnKlangPauseX_Click(tGrp.Geraeusche[i].btnKlangPause, new RoutedEventArgs());
						}
					}
				}
				ThemeGruppe aUnterTheme = ThemeLstThemePanel.FirstOrDefault(t => (Guid)t.pnlAudioTheme.btnAudioTheme.Tag == tGrp.dbAudioTheme.Audio_ThemeGUID);
				if (aUnterTheme != null &&
					_ThemeGruppe[aktiveThemeGruppe].UnterTheme.Find(t => t.dbAudioTheme.Audio_ThemeGUID.Equals(aUnterTheme.dbAudioTheme.Audio_ThemeGUID)) == null)
				{
					_ThemeGruppe[aktiveThemeGruppe].UnterTheme.Add(aUnterTheme);
					_ThemeGruppe[aktiveThemeGruppe].dbAudioTheme.Audio_Theme1.Add(aUnterTheme.dbAudioTheme);
					Global.ContextAudio.Update<Audio_Theme>(_ThemeGruppe[aktiveThemeGruppe].dbAudioTheme);

					UpdateAudioThemeToolTip(_ThemeGruppe[aktiveThemeGruppe]);
				}
			}
		}

		public void btnThemeThemeAngehakt_UnChecked(object sender, RoutedEventArgs e)
		{
			ThemeGruppe tGrp = ThemeLstThemePanel.First(t => t.dbAudioTheme.Audio_ThemeGUID == (Guid)((ToggleButton)e.Source).Tag);

			if (!AktThemeGruppe.ThemeJustRead)
			{
				if (!btnThemeEdit.IsChecked.Value)      //Kein Editiermodus
				{
					if (tGrp.HGThemePlaylist != null && tGrp.Musik.btnKlangPause != null &&
						!Convert.ToBoolean(tGrp.Musik.btnKlangPause.Tag))
					{
						tGrp.Musik.sollBtnGedrueckt = 0;
						if (!Convert.ToBoolean(tGrp.Musik.btnKlangPause.Tag) && tGrp.Musik.sollBtnGedrueckt >= 0)
						{
							tcKlang.Tag = tGrp.Musik.seite;
							btnKlangPauseX_Click(tGrp.Musik.btnKlangPause, new RoutedEventArgs());
						}
					}

					for (int i = 0; i < tGrp.Geraeusche.Count; i++)
					{
						tGrp.Geraeusche[i].sollBtnGedrueckt--;
						if (!Convert.ToBoolean(tGrp.Geraeusche[i].btnKlangPause.Tag) && tGrp.Geraeusche[i].sollBtnGedrueckt >= 0)
						{
							tcKlang.Tag = tGrp.Geraeusche[i].seite;
							btnKlangPauseX_Click(tGrp.Geraeusche[i].btnKlangPause, new RoutedEventArgs());
						}
					}
					ThemeAusSchalten(tGrp, false, false);
				}
				ThemeGruppe aUnterTheme = ThemeLstThemePanel.First(t => (Guid)t.pnlAudioTheme.btnAudioTheme.Tag == tGrp.dbAudioTheme.Audio_ThemeGUID);
				if (aUnterTheme != null &&
					_ThemeGruppe[aktiveThemeGruppe].UnterTheme.Find(t => t.dbAudioTheme.Audio_ThemeGUID.Equals(aUnterTheme.dbAudioTheme.Audio_ThemeGUID)) != null)
				{
					_ThemeGruppe[aktiveThemeGruppe].UnterTheme.Remove(aUnterTheme);

					_ThemeGruppe[aktiveThemeGruppe].dbAudioTheme.Audio_Theme1.Remove(aUnterTheme.dbAudioTheme);
					Global.ContextAudio.Update<Audio_Theme>(_ThemeGruppe[aktiveThemeGruppe].dbAudioTheme);

					UpdateAudioThemeToolTip(_ThemeGruppe[aktiveThemeGruppe]);
				}
			}
		}
		
		private void musikZeileBtnCheck_Checked(object sender, RoutedEventArgs e)
		{
			GruppenObjekt grpObj = _GrpObjecte.FindAll(t => t.ticKlang.Visibility == Visibility.Collapsed).
						FirstOrDefault(t => t.Audio_Playlist_GUID == (Guid)((ToggleButton)e.Source).Tag);
			AktualisiereKlangPlaylist();
			int war = tcKlang.SelectedIndex; 
			int warTag = Convert.ToInt32(tcKlang.Tag);
			if (grpObj == null)
			{
				tcKlang.SelectedIndex = tcKlang.Items.Count - 2;
				tiPlus_MouseUp(null, null);
							
				string sTitel = ((TextBlock)((StackPanel)((ToggleButton)e.Source).Parent).FindName("tblkTitel")).Text;

				int plylstItemPos = 0;    
				while (((ListBoxItem)lbKlang.Items[plylstItemPos]).Content.ToString() != sTitel)
					plylstItemPos++;
				lbKlang.SelectedIndex = plylstItemPos;

				grpObj = _GrpObjecte.FirstOrDefault(t => t.objGruppe == tiErstellt);
				((TabItemControl)tcKlang.SelectedItem).Visibility = Visibility.Collapsed;
			 
				//grpObj.grdKlang.Visibility = Visibility.Collapsed;
				grpObj.ticKlang._textBlockTitel.Text = grpObj.playlistName + "Collapsed";
			}
			grpObj.Vol_PlaylistMod = Convert.ToUInt16(slPlaylistVolume.Value);
			grpObj.Vol_ThemeMod = 100;
			if (Convert.ToBoolean(grpObj.btnKlangPause.Tag) && !Convert.ToBoolean(grpObj.wirdAbgespielt))// btnPListPListAbspielen.Tag))    //Abspielen
			{
				int old_tcKlangTag = Convert.ToInt16(tcKlang.Tag);
				tcKlang.Tag = grpObj.seite;
				grpObj.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
				grpObj.btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
				tcKlang.Tag = old_tcKlangTag;
			}
			checkPListPlaybtnGeräusche();
			tcKlang.SelectedIndex = war;
			tcKlang.Tag = warTag;
		}

		private void musikZeileBtnCheck_UnChecked(object sender, RoutedEventArgs e)
		{
			GruppenObjekt grpObj = _GrpObjecte.FindAll(t => t.ticKlang.Visibility == Visibility.Collapsed).
											   First(t => t.Audio_Playlist_GUID == (Guid)((ToggleButton)e.Source).Tag);
			int war = tcKlang.SelectedIndex;
			int warTag = Convert.ToInt32(tcKlang.Tag);
			if (!Convert.ToBoolean(grpObj.btnKlangPause.Tag))
			{
				tcKlang.Tag = grpObj.seite;
				grpObj.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
				grpObj.btnImgKlangPause.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png"));
			}
			checkPListPlaybtnGeräusche();
			tcKlang.SelectedIndex = war;
			tcKlang.Tag = warTag;
		}

		private void checkPListPlaybtnGeräusche()
		{
			bool found = false;
			for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
			{
				if ((lbPListGeräusche.Items[i] as MusikZeile).tbtnCheck.IsChecked.Value)
				{
					found = true;
					break;
				}
			}
			btnPListPListAbspielen.IsEnabled = found;
			if (found)
				btnimgPListPListAbspielen.Source = Convert.ToBoolean(btnPListPListAbspielen.Tag) ?
					new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")) :
					new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
			else
				btnimgPListPListAbspielen.Source = Convert.ToBoolean(btnPListPListAbspielen.Tag) ?
					new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play_grau.png")) :
					new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause_grau.png"));
		}
		
		private void AktualisierePListPlaylist()
		{
			UInt16 posMusik = 0;
			UInt16 posGeräusche = 0;
			Global.ContextAudio.PlaylistListe.ToList().ForEach(delegate(Audio_Playlist playlistliste)
			{
					MusikZeile mZeile = new MusikZeile();
					mZeile.Name = "PListOnetitel" + (posMusik + posGeräusche);
					mZeile.Tag = playlistliste.Audio_PlaylistGUID;
					mZeile.Cursor = Cursors.Hand;

					mZeile.tbtnCheck.Tag = playlistliste.Audio_PlaylistGUID;
					mZeile.tbtnCheck.Checked += new RoutedEventHandler(musikZeileBtnCheck_Checked);
					mZeile.tbtnCheck.Unchecked += new RoutedEventHandler(musikZeileBtnCheck_UnChecked);
					mZeile.tblkTitel.Text = playlistliste.Name;
					mZeile.tblkLänge.Text = (playlistliste.Länge != 0) ? TimeSpan.FromMilliseconds(playlistliste.Länge).ToString(@"hh\:mm\:ss") : "";
					mZeile.tboxKategorie.Tag = mZeile.Tag;
					mZeile.tboxKategorie.Text = playlistliste.Kategorie;
					mZeile.tboxKategorie.LostFocus += new RoutedEventHandler(tboxKategorie_LostFocus);

					if (playlistliste.Hintergrundmusik)
					{
						mZeile.pbarSong.MouseLeftButtonDown += new MouseButtonEventHandler(pbarBGSong_MouseLeftButtonDown);
						mZeile.tbtnCheck.Visibility = Visibility.Collapsed;                        
						if (posMusik + 1 > lbPListMusik.Items.Count)
							lbPListMusik.Items.Add(mZeile);
						else
						{
							((MusikZeile)lbPListMusik.Items[posMusik]).Name = mZeile.Name;
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

		private void tbPListGeräusche_TextChanged(object sender, TextChangedEventArgs e)
		{
			string [] split = tbPListGeräusche.Text.ToLower().Split(new Char [] { ' ', ','});

			for (int i = 0; i < lbPListGeräusche.Items.Count; i++)
			{
				((MusikZeile)lbPListGeräusche.Items[i]).Visibility = Visibility.Visible;

				foreach (string s in split) {
					if (s != "")
						if (!((MusikZeile)lbPListGeräusche.Items[i]).tblkTitel.Text.ToLower().Contains(s) ||
							((MusikZeile)lbPListMusik.Items[i]).tboxKategorie.Text == "")
						((MusikZeile)lbPListGeräusche.Items[i]).Visibility = Visibility.Collapsed;
				}                
			}
		}


		private void tbPListKategorie_TextChanged(object sender, TextChangedEventArgs e)
		{
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

		private void lbPListMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.RemovedItems.Count == 1)
				(e.RemovedItems[0] as MusikZeile).pbarSong.Visibility = Visibility.Collapsed;
			lbBackground.SelectedIndex = lbPListMusik.SelectedIndex;
			btnPListMusikStoppen.IsEnabled = btnBGStoppen.IsEnabled;
			btnImgPListMusikStoppen.Source = btnImgBGStoppen.Source;
			btnImgPListMusikAbspielen.Source = btnImgBGAbspielen.Source;
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
			if (this.IsInitialized)
			{
				btnPListPListSpeaker.Tag = -1;
				foreach (MusikZeile mZeile in lbPListGeräusche.Items)
				{
					GruppenObjekt grpObj = _GrpObjecte.FindAll(t => t.ticKlang.Visibility == Visibility.Collapsed).
						FirstOrDefault(t => t.Audio_Playlist_GUID == ((Guid)mZeile.tbtnCheck.Tag));
					if (grpObj != null)// && !Convert.ToBoolean(grpObj.btnKlangPause.Tag))
					{
						grpObj.Vol_PlaylistMod = Convert.ToInt32(((Slider)sender).Value);
						grpObj.Vol_ThemeMod = 100;
					}
				}
				btnimgPListPListSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
			}
		}

		private void slPlaylistMusikVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (IsInitialized)
				slBGVolume.Value = ((Slider)sender).Value;
		}


		private void btnKlangUpdateFiles_Click(object sender, RoutedEventArgs e)
		{            
			Global.SetIsBusy(true, string.Format("Neue Dateien werden integriert..."));
			List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
			string titelRef = btnKlangUpdateFiles.Tag.ToString();
			try
			{
				if (!CheckAlleTitelGleicherPfad(titelRef, titelliste))
				{
					btnKlangUpdateFiles.Visibility = Visibility.Visible;

					string[] allFilesMP3 = Directory.GetFiles(titelRef, "*.mp3", SearchOption.AllDirectories);
					string[] allFilesWAV = Directory.GetFiles(titelRef, "*.wav", SearchOption.AllDirectories);
					string[] allFilesOGG = Directory.GetFiles(titelRef, "*.ogg", SearchOption.AllDirectories);
					string[] allFilesWMA = Directory.GetFiles(titelRef, "*.wma", SearchOption.AllDirectories);

					string[] allFiles = new string[allFilesMP3.Length + allFilesOGG.Length + allFilesWAV.Length + allFilesWMA.Length];

					allFilesMP3.CopyTo(allFiles, 0);
					allFilesWAV.CopyTo(allFiles, allFilesMP3.Length);
					allFilesOGG.CopyTo(allFiles, allFilesMP3.Length + allFilesWAV.Length);
					allFilesWMA.CopyTo(allFiles, allFilesMP3.Length + allFilesWAV.Length + allFilesOGG.Length);
					
					List<string> newfiles = new List<string>();
					int durchlauf = 0;
					string prefix = (titelliste[0].Pfad.Substring(1, 3) != @":\\") ?
						stdPfad + "\\" : "";

					while (durchlauf < allFiles.Length)
					{
						if (titelliste.FirstOrDefault(t => prefix + t.Pfad == allFiles[durchlauf]) == null) newfiles.Add(allFiles[durchlauf]);
						durchlauf++;
					}
					
					Global.SetIsBusy(true, string.Format(newfiles.Count + " Titel im Verzeichnis: " + Environment.NewLine + titelRef + "werden eingebunden"));
					
					foreach (string newFile in newfiles)
					{
						Global.SetIsBusy(true, string.Format(newfiles.Count + " Titel im Verzeichnis: " + Environment.NewLine + titelRef + "werden eingebunden"+
							Environment.NewLine + newFile));
						Guid g = KlangDateiHinzu(newFile);
						if (AktKlangPlaylist == _BGPlayer.AktPlaylist)
						{

						}
					}            
					btnKlangUpdateFiles.Visibility = Visibility.Hidden;       
					Global.SetIsBusy(false);       
				}
			}
			catch (Exception ex)
			{          
				var errWin = new MsgWindow("Ungültiger Pfad", "Bitte überprüfen Sie das Verzeichnis:" + Environment.NewLine + titelRef, ex);
				errWin.ShowDialog();
				errWin.Close();
			}
		}
		
		private void btnPListPListAbspielen_Click(object sender, RoutedEventArgs e)
		{
			GruppenObjekt grpObj = null;
			foreach (MusikZeile mZeile in lbPListGeräusche.Items)
			{
				grpObj = _GrpObjecte.FindAll(t => t.ticKlang.Visibility == Visibility.Collapsed).
					FirstOrDefault(t => t.Audio_Playlist_GUID == ((Guid)mZeile.tbtnCheck.Tag));
				if (grpObj != null && mZeile.tbtnCheck.IsChecked.Value)
				{
					tcKlang.Tag = grpObj.seite;
					grpObj.btnKlangPause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
				}
			}
			btnPListPListAbspielen.Tag = !Convert.ToBoolean(btnPListPListAbspielen.Tag);
			btnimgPListPListAbspielen.Source = Convert.ToBoolean(btnPListPListAbspielen.Tag)?
				new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/play.png")):
				new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/pause.png"));
			grpObj.wirdAbgespielt = Convert.ToBoolean(btnPListPListAbspielen.Tag);

            if (grpObj.totalTimePlylist != -1)
            {
                aPlaylistLengthCheck = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(grpObj.Audio_Playlist_GUID)).FirstOrDefault();
                grpObj.totalTimePlylist = -1;
                GetTotalLength();
            }
		}

		private void btnPListPListSpeaker_Click(object sender, RoutedEventArgs e)
		{
			if (Convert.ToInt32((sender as Button).Tag) != -1)
			{
				slPlaylistVolume.Value = Convert.ToInt32((sender as Button).Tag);
				(sender as Button).Tag = -1;
				btnimgPListPListSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png"));
			}
			else
			{
				double volWert = slPlaylistVolume.Value;
				slPlaylistVolume.Value = 0;
				(sender as Button).Tag = volWert;
				btnimgPListPListSpeaker.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker-mute.png"));
			}
		}

	}
}