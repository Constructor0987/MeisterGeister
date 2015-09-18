using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MeisterGeister.Model;
using System.Windows.Data;
//Eigene usings
using MeisterGeister.ViewModel.Basar.Logic;
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using MeisterGeister.Logic.Umrechner;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model.Extensions;
using System.Windows.Threading;
using System.Windows.Media;
using MeisterGeister.View.AudioPlayer;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MeisterGeister.View.General;
using System.Windows;
using System.Windows.Input;
using System.IO;
using MeisterGeister.Logic.Einstellung;
using System.Globalization;
using System.Xml;
using System.Threading;
using NAudio.Wave;

namespace MeisterGeister.ViewModel.AudioPlayer
{
    public static class FocusExtension
    {
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool?), typeof(FocusExtension), new FrameworkPropertyMetadata(IsFocusedChanged));

        public static bool? GetIsFocused(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return (bool?)element.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject element, bool? value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            element.SetValue(IsFocusedProperty, value);
        }

        private static void IsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fe = (FrameworkElement)d;
            if (e.OldValue == null)
            {
                fe.GotFocus += FrameworkElement_GotFocus;
                fe.LostFocus += FrameworkElement_LostFocus;
            }
        }

        private static void fe_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var fe = (FrameworkElement)sender;
            if (fe.IsVisible && (bool)((FrameworkElement)sender).GetValue(IsFocusedProperty))
            {
                fe.IsVisibleChanged -= fe_IsVisibleChanged;
                fe.Focus();
            }
        }

        private static void FrameworkElement_GotFocus(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).SetValue(IsFocusedProperty, true);
        }

        private static void FrameworkElement_LostFocus(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).SetValue(IsFocusedProperty, false);
        }
    }
    
    #region //---- Converters ----

    public class SliderRangeMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rootActualWidth = values[0] is double? (double)values[0]: 0;
            var LowerSliderMaximum = values[1] is double? (double)values[1]: 0;
            var LowerSliderValue = values[2] is double? (double)values[2]: 0;
            var UpperSliderValue = values[3] is double ? (double)values[3] : 0;

            return new Thickness(
                (rootActualWidth - 17) / (LowerSliderMaximum / LowerSliderValue) + 8,  
                0,
                rootActualWidth - 17 - (rootActualWidth - 17) / (LowerSliderMaximum / UpperSliderValue) + 8, 
                0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvertedBoolenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
    
    public class MillisecondToMinuteSecondsTextConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new MillisecondToMinuteSecondsTextConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = (int)value >= 60000 ? (int)value - ((int)value / 60000) * 60000 : 0; 
            string s = ((int)value < 1000 ? value + "ms" : (int)value < 60000 ?
                    Math.Round((double)( System.Convert.ToDecimal((int)value) / 1000), 2).ToString() + "sek" :
                    (int)value / 60000 + "min" +
                        (i < 1000 ? i + "ms" : i < 60000 ?
                        Math.Round((double)( System.Convert.ToDecimal(i) / 1000), 2).ToString() + "sek":""));
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleToTimespanHHMMSSConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new DoubleToTimespanHHMMSSConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = (value is double && (double)value != 0)? TimeSpan.FromMilliseconds((double)value): TimeSpan.Zero;
            return (ts.TotalHours >= 1) ? ts.ToString(@"hh\:mm\:ss") : ts.ToString(@"mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiBooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if (((value is bool) && (bool)value == false) || value == null)
                {
                    return false;
                }
            }
            return true;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }

    public class MultiBooleanAndConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if (((value is bool) && (bool)value == false) || value == null)
                {
                    return false;
                }
            }
            return true;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }

    public class MultiBooleanORConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if ((value is bool) && (bool)value)
                {
                    return true;
                }
            }
            return false;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }

    public class MultiBooleanToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                                Type targetType,
                                object parameter,
                                System.Globalization.CultureInfo culture)
        {
            bool visible = true;
            foreach (object value in values)
                if (value is bool)
                    visible = visible && (bool)value;

            if (visible)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class IsVisibleToBooleanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsVisibleToBooleanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility)value == Visibility.Visible) ? true: false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class MultiBooleanODERToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                                Type targetType,
                                object parameter,
                                System.Globalization.CultureInfo culture)
        {
            bool visible = false;
            foreach (object value in values)
                if (value is bool)
                {
                    visible = (bool)value;
                    if (visible) break;
                }

            if (visible)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsEqualOrGreaterThanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrGreaterThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            int compareToValue = System.Convert.ToInt32(parameter);

            return intValue >= compareToValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class BooleanInverseToVisibilityConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BooleanInverseToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(bool)value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanAndNotReihenfolgeConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BooleanAndNotReihenfolgeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanInverseToVisibilityHiddenConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BooleanInverseToVisibilityHiddenConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(bool)value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringNullOrEmptyToVisibilityConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string)
                ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class IsNullOrEmptyToBoolConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null? false : true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    
    public class IsEqualOrGreaterThanVisibleConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrGreaterThanVisibleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            int compareToValue = System.Convert.ToInt32(parameter);

            return intValue >= compareToValue? Visibility.Visible: Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    public class AudioPlayerViewModel : Base.ViewModelBase
    {
        #region //---- View Komponeten ----

        public DoubleCollection SliderTicks
        {
            get
            {
                return new DoubleCollection 
                    {0, 100, 200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000, 3000, 4000, 5000, 7500, 8000, 9000, 10000, 15000, 
                     20000, 25000, 30000, 40000, 50000, 60000, 90000, 120000, 180000, 240000, 300000, 450000, 600000, 900000};
            }
        }
        
        public Cursor _dndZeilenCursor = null;
        public Cursor _dndZeilenCursorPlus = null;

        public object DnDZielObject = null;

        private bool _erwPlayerGeräuscheAktiv = false;
        public bool ErwPlayerGeräuscheAktiv
        {
            get { return _erwPlayerGeräuscheAktiv; }
            set
            {
                int wirdAbgespielt = ErwPlayerGeräuscheListItemListe.FindAll(t => t.VM.grpobj.wirdAbgespielt).Count;
                int angehakt = ErwPlayerGeräuscheListItemListe.FindAll(t => t.tbtnCheck.IsChecked.Value).Count;
                
                _erwPlayerGeräuscheAktiv = angehakt > 0;

                if (angehakt >= 1 && wirdAbgespielt == 1 && !ErwPlayerGeräuscheLaufen)
                    ErwPlayerGeräuscheLaufen = true;

                OnChanged();
            }
        }


        private bool _stdPadAufC = false;
        public bool StdPadAufC
        {
            get { return _stdPadAufC; }
            set
            {
                _stdPadAufC = value;
                OnChanged();
            }
        }

        private bool _editorListeVisible = false;
        public bool EditorListeVisible
        {
            get { return _editorListeVisible; }
            set
            {
                _editorListeVisible = value;
                OnChanged();
            }
        }
                
        private bool _erwPlayerGeräuscheLaufen = true;
        public bool ErwPlayerGeräuscheLaufen
        {
            get { return _erwPlayerGeräuscheLaufen; }
            set
            {
                _erwPlayerGeräuscheLaufen = value;
                OnChanged();
            }
        }

        private bool _berechneSpieldauer = false;
        public bool BerechneSpieldauer
        {
            get { return _berechneSpieldauer; }
            set
            {
                _berechneSpieldauer = value;
                OnChanged();
            }
        }

        private bool _themeGeräuscheFilterAktiv = false;
        public bool ThemeGeräuscheFilterAktiv
        {
            get { return _themeGeräuscheFilterAktiv; }
            set
            {
                _themeGeräuscheFilterAktiv = value;
                OnChanged();
            }
        }


        private string _info_BGTitel;
        public string Info_BGTitel
        {
            get { return _info_BGTitel; }
            set {
                _info_BGTitel = value;
                OnChanged();
            }
        }

        private string _info_BGArtist;
        public string Info_BGArtist
        {
            get { return _info_BGArtist; }
            set
            {
                _info_BGArtist = value;
                OnChanged();
            }
        }
        
        private string _info_BGAlbum;
        public string Info_BGAlbum
        {
            get { return _info_BGAlbum; }
            set
            {
                _info_BGAlbum = value;
                OnChanged();
            }
        }

        private string _info_BGJahr;
        public string Info_BGJahr
        {
            get { return _info_BGJahr; }
            set
            {
                _info_BGJahr = value;
                OnChanged();
            }
        }
        
        private string _info_BGGenre;
        public string Info_BGGenre
        {
            get { return _info_BGGenre; }
            set
            {
                _info_BGGenre = value;
                OnChanged();
            }
        }
        
        private double _bgPosition;        
        public double BGPosition
        {
            get { return _bgPosition; }
            set
            {
                _bgPosition = value; 
                OnChanged(); 
                OnChanged("sBGPosition");                
            }
        }

        public bool AllTitelAktiv
        {
            get
            {
                return (AktKlangPlaylist != null) ?
                  (AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.Aktiv) == AktKlangPlaylist.Audio_Playlist_Titel.Count) : false; 
            }
            set { OnChanged(); }
        }

        private bool _isAuswahlHotkey = false;
        public bool IsAuswahlHotkey
        {
            get { return _isAuswahlHotkey; }
            set
            {
                _isAuswahlHotkey = value;
                OnChanged();
            }
        }

        public string sBGPosition
        {
            get
            {
                return BGPlayer.BG[BGPlayeraktiv].mPlayer == null ? "--:--" :
                    BGPlayer.BG[BGPlayeraktiv].mPlayer.Position.ToString(@"mm\:ss"); 
            }
        }

        private double _musikTeilStart;
        public double MusikTeilStart
        {
            get { return _musikTeilStart; }
            set
            {
                _musikTeilStart = value;
                BGPlayer.AktPlaylistTitel.TeilStart = value;
                OnChanged();
            }
        }

        private double _musikTeilEnde;
        public double MusikTeilEnde
        {
            get { return _musikTeilEnde; }
            set
            {
                _musikTeilEnde = value;
                BGPlayer.AktPlaylistTitel.TeilEnde = value;
                OnChanged();
            }
        }

        private double _musikTeilMax = 10000000;
        public double MusikTeilMax
        {
            get { return _musikTeilMax; }
            set
            {
                _musikTeilMax = value;
                OnChanged();
            }
        }
        
        #endregion

        #region //---- Klassen ----

        public class Musik: Base.ViewModelBase
        {
            public bool FadingOutStarted = false;

            private bool _isPaused = false;
            public bool isPaused
            {
                get { return _isPaused;}
                set
                {
                    _isPaused = value;
                    OnChanged();
                }
            }

            public Audio_Playlist aPlaylist = null;
            public MediaPlayer mPlayer = null;            
        }
        
        public class group
        {
            public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
            public MusikZeile mZeile = null;
            public double zielProzent;
            public double startProzent;
            public DateTime StartZeit;
            public double _vergangeneZeit;
        }

        public class FadingInGeräusche
        {
            public List<group> gruppenIn = new List<group>();
        }

        public class FadingOutGeräusche
        {
            public List<group> gruppenOut = new List<group>();
            public bool fadingOutSofort = false;
        }

        public class Fading
        {
            public MediaPlayer mp;
            public DateTime Start;
            public double zielVol;
            public double startVol = 0;
            public bool fadingOutSofort;
            public bool mPlayerStoppen;
            public Musik BG;
            public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile;
            public GruppenObjekt grpobj = null;
        }

        [DependentProperty("AktKlangPlaylist")]
        public string imgListboxPlaylist
        {
            get
            {
                if (AktKlangPlaylist != null)
                    return (AktKlangPlaylist.Hintergrundmusik ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png" :
                                                         "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png");
                else
                    return "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/copy.png";
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
            //public string playlistName = "";
            //public bool istMusik = false;
            public List<KlangZeile> _listZeile = new List<KlangZeile>();
            public bool wirdAbgespielt = false;
            public List<Guid> NochZuSpielen = new List<Guid>();

            public List<UInt16> Gespielt = new List<UInt16>();

            public bool DoForceVolume = false;
            public Nullable<double> force_Volume = null;

            public bool visuell = false;
            public DispatcherTimer wartezeitTimer = new DispatcherTimer();

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
            public IntBox intbxSongsParallel = null;

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

        public class KlangZeile
        {
            public UInt16 ID_Zeile;
            public MediaPlayer _mplayer = null;
            public Audio_Playlist_Titel aPlaylistTitel = new Audio_Playlist_Titel();
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

            public AudioZeileVM audioZeileVM = null;
            public KlangZeile(UInt16 id)
            {
                ID_Zeile = id;
            }
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

        public class MusikView
        {
            private bool _isMuted;
            public bool isMuted
            {
                get { return _isMuted; }
                set { _isMuted = value; }
            }
            private bool _isPaused;
            public bool isPaused
            {
                get { return _isPaused; }
                set { _isPaused = value; }
            }
            public List<Musik> BG = new List<Musik>();
            public string[] s = new string[2];
            public double totalLength;
            public List<Guid> NochZuSpielen = new List<Guid>();
            public List<Guid> Gespielt = new List<Guid>();
            public List<Guid> MusikNOK = new List<Guid>();
            public Audio_Playlist AktPlaylist;
            public Audio_Playlist_Titel AktPlaylistTitel;
            
            public List<Audio_Titel> AktTitel = new List<Audio_Titel>();
        }

        #endregion

        #region //---- FELDER & EIGENSCHAFTEN ----
        public BackgroundWorker workerGetLength = new BackgroundWorker();
        public bool AudioInAnderemPfadSuchen = Einstellungen.AudioInAnderemPfadSuchen;
        public bool AudioSpieldauerBerechnen = Einstellungen.AudioSpieldauerBerechnen;
        private bool firstshot = true;
        public int BGPlayeraktiv = 0;
                        
        public static RoutedCommand ThemeCommandCheck = new RoutedCommand();
        System.Timers.Timer BGSongTimer = new System.Timers.Timer();
        public List<DispatcherTimer> lstKlangPlayEndetimer = new List<DispatcherTimer>();
        DispatcherTimer KlangPlayEndetimer;

        public DispatcherTimer _timerBGFadingOut = new DispatcherTimer();
        public DispatcherTimer _timerFadingIn = new DispatcherTimer();
        public DispatcherTimer _timerFadingOut = new DispatcherTimer();
        public DispatcherTimer _timerFadingOutGeräusche = new DispatcherTimer();
        public DispatcherTimer _timerFadingInGeräusche = new DispatcherTimer();
        public DispatcherTimer KlangProgBarTimer = new DispatcherTimer();
        public DispatcherTimer MusikProgBarTimer = new DispatcherTimer();
        
        public MediaPlayer FadingIn_Started = new MediaPlayer();
        public MediaPlayer FadingOut_Started = new MediaPlayer();

        private List<GruppenObjekt> _grpObjecte = new List<GruppenObjekt>();
        public List<GruppenObjekt> _GrpObjecte
        {
            get { return _grpObjecte; }
            set
            {
                _grpObjecte = value;
                OnChanged();
            }
        }

        public double fadingIntervall = 10;
        public double fadingTime = 600;    // * fadingIntervall = Übergang in ms
        public List<string> stdPfad = new List<string>();

        public string[] validExt = new String[6] { "mp3", "wav", "wma", "ogg", "m3u8", "wpl" };

        private double Zeitüberlauf = 1000;   // in ms
        public UInt16 tiErstellt = 0;
        public UInt16 rowErstellt = 0;

        public bool stopFadingIn = false;
        public bool isDeleting = false;

        public int SliderTeile = 25;
        //private Int16 PauseSprung = 200;
        //private Int16 VolSprung = 5;

        public Nullable<Point> pointerZeileDragDrop = null;
        public Nullable<Point> pointerPlaylistDragDrop = null;
        public lbEditorItem lbiEditorPlaylistStartDnD = null;
        
        private Audio_Playlist _dropZielPlaylist = null;
        public Audio_Playlist DropZielPlaylist
        {
            get { return _dropZielPlaylist; }
            set { _dropZielPlaylist = value; OnChanged(); }
        }


        private int _audioZeileMouseOverDropped = 0;
        public int audioZeileMouseOverDropped
        { get { return _audioZeileMouseOverDropped; }
            set { _audioZeileMouseOverDropped = value; OnChanged(); }
        }

        public int _lbiPlaylistMouseOverDropped = 0;
        public int lbiPlaylistMouseOverDropped
        {
            get { return _lbiPlaylistMouseOverDropped; }
            set { _lbiPlaylistMouseOverDropped = value; OnChanged(); }
        }
        
        public List<btnHotkey> hotkeyListe = new List<btnHotkey>();

        private List<KlangZeile> _klangzeilen;

        private bool _playlistListeNichtUpdaten = false;
        public bool PlaylistListeNichtUpdaten
        {
            get { return _playlistListeNichtUpdaten; }
            set
            {
                _playlistListeNichtUpdaten = value;
                OnChanged();
            }
        }

        private bool _editReihenfolgeVis;
        public bool editReihenfolgeVis
        {
            get { return _editReihenfolgeVis; }
            set
            {
                _editReihenfolgeVis = PlaylistAZ;
                    
                OnChanged();
            }
        }
                        
        private bool _rbEditorEditPlaylist = true;
        public bool rbEditorEditPlaylist
        {
            get { return _rbEditorEditPlaylist; }
            set
            {
                _rbEditorEditPlaylist = value;

                EditorListeVisible = rbEditorEditPlaylist ? true : false;
                FilterEditorPlaylistListe();
                FilterThemeEditorPlaylistListe();
                OnChanged();
            }
        }


        private bool _showHotkeyPanel = false;
        [DependentProperty("hotkeyListUsed"), DependentProperty("hotkeyListUsed")]
        public bool ShowHotkeyPanel
        {
            get { return _showHotkeyPanel; }
            set
            {
                _showHotkeyPanel = !value && hotkeyListUsed.Count > 0;
                OnChanged();
            }
        }
        
        private chkeckAnzDateien _chkAnzDateien = new chkeckAnzDateien();
        public chkeckAnzDateien ChkAnzDateien
        {
            get { return _chkAnzDateien; }
            set
            {
                _chkAnzDateien = value;
                OnChanged();
            }
        }

        private string _chkAnzDateienResult = null;
        public string ChkAnzDateienResult
        {
            get { return _chkAnzDateienResult; }
            set
            {
                _chkAnzDateienResult = value;
                OnChanged();
            }
        }

        private bool _chkAnzDateienVerfügbar = false;
        public bool ChkAnzDateienVerfügbar
        {
            get { return _chkAnzDateienVerfügbar; }
            set
            {
                _chkAnzDateienVerfügbar = value;
                OnChanged();
            }
        }

        private bool _bgmPlayerIsPaused;
        public bool BGmPlayerIsPaused
        {
            get { return _bgmPlayerIsPaused; }
            set
            {
                _bgmPlayerIsPaused = value;
                BGPlayer.isPaused = value;
                BGPlayer.BG[BGPlayeraktiv].isPaused = value;
                    
                OnChanged();
            }
        }

        private int _bgPlayerGespieltCount;
        public int BGPlayerGespieltCount
        {
            get { return _bgPlayerGespieltCount; }
            set
            {
                _bgPlayerGespieltCount = value;
                OnChanged();
            }
        }
        
        private List<btnHotkey> _hotkeyListUsed = new List<btnHotkey>();
        public List<btnHotkey> hotkeyListUsed
        {
            get { return _hotkeyListUsed; }
            set
            {
                _hotkeyListUsed = value;
                ShowHotkeyPanel = ShowHotkeyPanel;
                
                OnChanged();
            }

        }
        private bool _setHintergrundmusik;
        public bool SetHintergrundmusik
        {
            get { return _setHintergrundmusik; }
            set
            {
                _setHintergrundmusik = value;
                if (AktKlangPlaylist != null)
                    AktKlangPlaylist.Hintergrundmusik = value;
                OnChanged("AktKlangPlaylist");
            }
        }

        private bool _setGeräusch;
        public bool SetGeräusch
        {
            get { return _setGeräusch; }
            set
            {
                _setGeräusch = value; 
                if (AktKlangPlaylist != null)
                    AktKlangPlaylist.Hintergrundmusik = !value;
                OnChanged("AktKlangPlaylist");
            }
        }

        private bool _editorGroßeAnsicht;
        public bool EditorGroßeAnsicht
        {
            get { return _editorGroßeAnsicht; }
            set
            {
                _editorGroßeAnsicht = value;
                FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM) { aZeileVM.EditorGroßeAnsicht = value; });
                OnChanged();
            }
        }

        private bool _pListGroßeAnsicht = true;
        public bool PListGroßeAnsicht
        {
            get { return _pListGroßeAnsicht; }
            set
            {
                _pListGroßeAnsicht = value;
                ErwPlayerMusikListItemListe.ForEach(delegate(MusikZeile mZeile) { mZeile.VM.GroßeAnsicht = PListGroßeAnsicht; });
                ErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile mZeile) { mZeile.VM.GroßeAnsicht = PListGroßeAnsicht; });
                OnChanged();
            }
        }

        private bool _themeGroßeAnsicht = true;
        public bool ThemeGroßeAnsicht
        {
            get { return _themeGroßeAnsicht; }
            set
            {
                _themeGroßeAnsicht = value;
                ErwPlayerThemeListe.ForEach(delegate(grdThemeButton grdThBtn) { grdThBtn.VM.GroßeAnsicht = ThemeGroßeAnsicht; });
                OnChanged();
            }
        }

        private int _hotkeyVolume = Einstellungen.GeneralHotkeyVolume;
        public int HotkeyVolume
        {
            get { return _hotkeyVolume; }
            set
            {
                
                _hotkeyVolume = value;
                hotkeyListe.ForEach(t => t.VM.volume = HotkeyVolume);
                hotkeyListUsed.ForEach(t => t.VM.volume = HotkeyVolume);
                //hotkeyListe.FindAll(t => t.VM.TitelPlayList.ForEach(delegate() t2 => t2.mp){}
                //    delegate(t.VM.TitelPlay titelPlay)
                //{ titelPlay.mp != null).ForEach(ti => ti.VM.volume = HotkeyVolume);
                //hotkeyListUsed.FindAll(t => t.VM.mp != null).ForEach(ti => ti.VM.volume = HotkeyVolume);

                Einstellungen.SetEinstellung<int>("GeneralHotkeyVolume", _hotkeyVolume); //(int)Math.Round(
                OnChanged();
            }
        }
        
        private Audio_Playlist _aktKlangPlaylist;
        public Audio_Playlist AktKlangPlaylist
        {
            get { return _aktKlangPlaylist; }
            set
            {
                _aktKlangPlaylist = value;
                if (value != null)
                {
                    LadeAudioZeilen();
                    LadeFilteredAudioZeilen();
                }
                OnChanged();
            }
        }
        
        private Audio_Theme _aktKlangTheme;
        public Audio_Theme AktKlangTheme
        {
            get { return _aktKlangTheme; }
            set
            {
                _aktKlangTheme = value;
                LadeAudioZeilen();
                OnChanged();
            }
        }

        private string _aktEditorName;
        public string AktEditorName
        {
            get { return _aktEditorName; }
            set
            {
                if (AktKlangPlaylist != null)
                {
                    AktKlangPlaylist.Name = value;
                    OnChanged("AktKlangPlaylist");
                }
                if (AktKlangTheme != null)
                {
                    AktKlangTheme.Name = value;
                    OnChanged("AktKlangTheme");
                }
                _aktEditorName = value;
                OnChanged();
            }
        }
        
        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikItem"), DependentProperty("BGPlayerAktPlaylistTitelTeilAbspielen")]
        public double BGPlayerAktPlaylistTitelLänge
        {
            get
            {
                if (_bgPlayerAktPlaylistTitel != null &&
                    _bgPlayerAktPlaylistTitel.Audio_Titel != null &&
                    (_bgPlayerAktPlaylistTitel.Audio_Titel.Länge == 0 || _bgPlayerAktPlaylistTitel.Audio_Titel.Länge == null) &&
                    _bgPlayerAktPlaylistTitel.Länge != 0)
                    _bgPlayerAktPlaylistTitel.Audio_Titel.Länge = _bgPlayerAktPlaylistTitel.Länge;
                return ((
                    _bgPlayerAktPlaylistTitel != null &&  
                    _bgPlayerAktPlaylistTitel.Audio_Titel.Länge != null &&
                    _bgPlayerAktPlaylistTitel.Audio_Titel.Länge != 0) ? _bgPlayerAktPlaylistTitel.Audio_Titel.Länge.Value :
                    BGPlayer != null && 
                    BGPlayer.AktPlaylistTitel != null && 
                    BGPlayer.AktPlaylistTitel.Audio_Titel.Länge != null? 
                    BGPlayer.AktPlaylistTitel.Audio_Titel.Länge.Value: 10000000);
            }  
            set { OnChanged(); }
        }

        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikItem"), DependentProperty("BGPlayerAktPlaylistTitelTeilAbspielen")]
        public double BGPlayerAktPlaylistTitelTeilStart
        {
            get { return (_bgPlayerAktPlaylistTitel != null && _bgPlayerAktPlaylistTitel.TeilStart != null ? 
                _bgPlayerAktPlaylistTitel.TeilStart.Value : 0); }
            set 
            { 
                BGPlayer.AktPlaylistTitel.TeilStart = value;
                OnChanged();
            }
        }

        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikItem")]
        public bool BGPlayerAktPlaylistTitelTeilAbspielen
        {
            get { return _bgPlayerAktPlaylistTitel != null ?
                    _bgPlayerAktPlaylistTitel.TeilAbspielen : false; }
            set
            {
                BGPlayer.AktPlaylistTitel.TeilAbspielen = value;
                if (value)
                    BGPlayer.AktPlaylistTitel.TeilEnde = BGPlayer.AktPlaylistTitel.Audio_Titel.Länge;
                OnChanged();
            }
        }

        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikItem"), DependentProperty("BGPlayerAktPlaylistTitelTeilAbspielen")]
        public double BGPlayerAktPlaylistTitelTeilEnde
        {
            get { return (_bgPlayerAktPlaylistTitel != null && _bgPlayerAktPlaylistTitel.TeilEnde != null ? 
                _bgPlayerAktPlaylistTitel.TeilEnde.Value : BGPlayerAktPlaylistTitelLänge); }
            set
            {
                BGPlayer.AktPlaylistTitel.TeilEnde = value;
                OnChanged();
            }
        }


        private Audio_Playlist_Titel _bgPlayerAktPlaylistTitel;
        public Audio_Playlist_Titel BGPlayerAktPlaylistTitel
        {
            get { return _bgPlayerAktPlaylistTitel; }
            set
            {
                _bgPlayerAktPlaylistTitel = value;
                BGPlayer.AktPlaylistTitel = value;
                OnChanged();
            }
        }

        private Audio_Playlist _bgPlayerAktPlaylist;
        public Audio_Playlist BGPlayerAktPlaylist
        {
            get { return _bgPlayerAktPlaylist; }
            set
            {
                _bgPlayerAktPlaylist = value;
                OnChanged();
            }
        }

        private MusikView _bgPlayer;
        public MusikView BGPlayer
        {
            get
            { return _bgPlayer; }
            set
            {
                _bgPlayer = value;
                LoadMusikTitelListe();
                BGPlayerAktPlaylist = BGPlayer.AktPlaylist;
                OnChanged();
                OnChanged("BGPlayer");
            }
        }

        private bool _hotkeyVisible;
        public bool HotkeyVisible
        {
            get { return _hotkeyVisible; }
            set
            {
                _hotkeyVisible = value;
                OnChanged();
            }
        }
        
        private bool _lbEditorMitGeräusche;
        public bool LbEditorMitGeräusche
        {
            get { return _lbEditorMitGeräusche; }
            set
            {
                _lbEditorMitGeräusche = value;
                OnChanged();
                FilterEditorPlaylistListe();
            }
        }

        private bool _lbEditorMitMusik;
        public bool LbEditorMitMusik
        {
            get { return _lbEditorMitMusik; }
            set
            {
                _lbEditorMitMusik = value;
                OnChanged();
                FilterEditorPlaylistListe();
            }
        }

        private lbEditorItemVM _selectedEditorThemeItem;
        public lbEditorItemVM SelectedEditorThemeItem
        {
            get { return _selectedEditorThemeItem; }
            set
            {
                _selectedEditorThemeItem = value;
                AktKlangTheme = value == null ? null : value.ATheme;
                OnChanged();
                if (AktKlangTheme != null) 
                    LadeBoxThemeList();
                EditorThemeÜbrigThemeListe = FilterThemeÜbrigListBoxItemListe();
                FilterEditorPlaylistListe();
            }
        }

        private lbEditorItemVM _lbEditorThemeListboxIconSelected;
        public lbEditorItemVM lbEditorThemeListboxIconSelected
        {
            get { return _lbEditorThemeListboxIconSelected; }
            set
            {
                _lbEditorThemeListboxIconSelected = value;
                if (value != null && !AktKlangTheme.Audio_Theme1.Contains(value.ATheme))
                {
                    AktKlangTheme.Audio_Theme1.Add(value.ATheme);
                    Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                    SelectedEditorThemeItem = SelectedEditorThemeItem;
                    _lbEditorThemeListboxIconSelected = null;
                }
                OnChanged();                
            }
        }

        private lbEditorItemVM _selectedEditorItem;
        public lbEditorItemVM SelectedEditorItem
        {
            get { return _selectedEditorItem; }
            set
            {
                //In der Theme-Editor-Ansicht nichts unternehmen, sodass das D&D im View sauber läuft
                if (!rbEditorEditPlaylist || PlaylistListeNichtUpdaten) 
                    return;

                //MouseOnSubObject = Pfeile f. Reihenfolge, Export, Löschen (so wird die Playlist nicht vorher geladen)
                if (value != null && value.MouseOnSubObject) 
                    return;

                AktKlangPlaylist = null;
                _selectedEditorItem = value;

                if (value == null)
                    EditorListeVisible = false;
                else
                {
                    AktKlangPlaylist = value.APlaylist;
                    
                    //Check Reihenfolge okay
                    bool neuaufbau = false;
                    foreach (Audio_Playlist_Titel aPlayTitel in AktKlangPlaylist.Audio_Playlist_Titel) 
                    { 
                        //Doppelte Reihenfolgenummern
                        if (AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.Reihenfolge == aPlayTitel.Reihenfolge) > 1)
                        {
                            sortPlaylist(AktKlangPlaylist, -1);
                            neuaufbau = true;
                            break;
                        }
                    }
                    if (!neuaufbau)
                    {
                        // Nummern richtig vergeben (0 ....)
                        List<Audio_Playlist_Titel> lstPlaylist_Titel = AktKlangPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Reihenfolge).ToList();
                        for (int cnt = 0; cnt < lstPlaylist_Titel.Count - 1; cnt++)
                        {
                            if (lstPlaylist_Titel[cnt].Reihenfolge != cnt)
                            {
                                sortPlaylist(AktKlangPlaylist, -1);
                                break;
                            }
                        }
                    }
                    
                    EditorListeVisible = rbEditorEditPlaylist ? true : false;

                    //Altbestand löschen
                    GruppenObjekt alt_grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (alt_grpobj != null)
                    {
                        AlleKlangSongsAus(alt_grpobj, false, false, false, true);
                        alt_grpobj.wirdAbgespielt = false;
                        _GrpObjecte.Remove(alt_grpobj);
                    }

                    VisualGrpObj();
                    GruppenObjekt grpobj = _GrpObjecte.First(t => t.visuell);
                    grpobj.aPlaylist = value.APlaylist;
                    foreach (Audio_Playlist_Titel aPlaylistTitel in grpobj.aPlaylist.Audio_Playlist_Titel)
                    {
                        if (AudioInAnderemPfadSuchen &&
                            !File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei == null ? "" : aPlaylistTitel.Audio_Titel.Datei))
                        {
                            aPlaylistTitel.Audio_Titel = setTitelStdPfad(aPlaylistTitel.Audio_Titel);
                            if (File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei))
                                Global.ContextAudio.Update<Audio_Titel>(aPlaylistTitel.Audio_Titel);
                        }
                        KlangNewRow(grpobj, aPlaylistTitel);

                        if (aPlaylistTitel.Aktiv &&
                            !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= aPlaylistTitel.Rating; i++)
                                grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                        }
                    }
                    OnChanged("AllTitelAktiv");                    
                    OnChanged("IsVolumeChangeChecked");
                    OnChanged("IsPausenZeitChangeChecked");
                    OnChanged("AktKlangPlaylistWarteZeit");
                    OnChanged("AktKlangPlaylistWarteZeitMin");
                    OnChanged("AktKlangPlaylistWarteZeitMax");
                }
                OnChanged();

                //Muss nach dem OnChange durchgeführt werden, damit der BackgroundWorker nicht das Speichern der DB beeinflusst 
                if (value != null && !firstshot && MeisterGeister.Logic.Einstellung.Einstellungen.AudioSpieldauerBerechnen)                    
                    GetTotalLength(AktKlangPlaylist, true);
            }
        }  

        private MusikZeile _lbiMusikSelect;
        public MusikZeile lbiMusikSelect
        {
            get { return _lbiMusikSelect; }
            set
            {
                if (value != null && _lbiMusikSelect != null &&
                    _lbiMusikSelect.VM.aPlaylist.Audio_PlaylistGUID == value.VM.aPlaylist.Audio_PlaylistGUID)
                    return;
                SelectedMusikItem = value;
                OnChanged("SelectedMusikItem");          
                OnChanged("FilteredMusikListItemListe");
                OnChanged("FilteredErwPlayerMusikListItemListe");
                _lbiMusikSelect = value;
                OnChanged();
            }
        }

        private MusikZeile _selectedMusikItem;
        public MusikZeile SelectedMusikItem
        {
            get { return _selectedMusikItem; }
            set
            {
                _selectedMusikItem = value;

                SuchTextMusikTitel = "";
                BGPlayer.AktPlaylist = (value == null || value.VM.aPlaylist == null) ? null : value.VM.aPlaylist;
                BGPlayerAktPlaylistTitel = null;
                BGPlayerAktPlaylist = BGPlayer.AktPlaylist;

                BGPlayer.MusikNOK.Clear();
                BGPlayer.NochZuSpielen.Clear();

                BGPlayer.Gespielt.Clear();
                BGPlayerGespieltCount = 0;
                RenewMusikNochZuSpielen();
                LoadMusikTitelListe();
                                
                if (FilteredHintergrundMusikListe.Count > 0)
                    SelectedMusikTitelItem = GetNextMusikTitel();
                BGPlayer.BG[BGPlayeraktiv].aPlaylist = BGPlayer.AktPlaylist;
                                
                OnChanged();
                if (!firstshot)
                    GetTotalLength(BGPlayerAktPlaylist, false);
            }
        }
        
        private Brush _background;
        public Brush Background
        {
            get
            { return _background; }
            set
            {
                _background = value;
                OnChanged();
            }
        }
        
        private bool _musikStern1;
        public bool MusikStern1
        {
            get { return _musikStern1;}
            set
            {
                _musikStern1 = value;
                OnChanged();                
            }
        }

        private bool _musikStern2;
        public bool MusikStern2
        {
            get { return _musikStern2; }
            set
            {
                _musikStern2 = value;
                OnChanged();
            }
        }

        private bool _musikStern3;
        public bool MusikStern3
        {
            get { return _musikStern3; }
            set
            {
                _musikStern3 = value;
                OnChanged();
            }
        }

        private bool _musikStern4;
        public bool MusikStern4
        {
            get { return _musikStern4; }
            set
            {
                _musikStern4 = value;
                OnChanged();
            }
        }

        private bool _musikStern5;
        public bool MusikStern5
        {
            get { return _musikStern5; }
            set
            {
                _musikStern5 = value;
                OnChanged();
            }
        }
        
        private ListBoxItem _selectedMusikTitelItem;
        public ListBoxItem SelectedMusikTitelItem
        {
            get { return _selectedMusikTitelItem; }
            set
            {
                _selectedMusikTitelItem = value;
                if (value != null)
                {
                    BGmPlayerIsPaused = false;
                    BGPlayer.BG[BGPlayeraktiv].aPlaylist = BGPlayer.AktPlaylist;
                    BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID);

                    BGPosition = 0;
                    if (BGPlayerAktPlaylistTitel == null)
                        //Falls nicht gefunden, neuen Titel abspielen
                        SpieleNeuenMusikTitel(Guid.Empty);
                    else
                        SpieleNeuenMusikTitel((Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID);
                    
                    //BGPlayer.BG[BGPlayeraktiv].aPlaylist = BGPlayer.AktPlaylist;
                    if (BGPlayer.MusikNOK.Contains((Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID))
                    {
                        value.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                        Audio_Playlist_Titel aPlayTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID);
                        value.ToolTip = "Datei nicht gefunden. -> " + aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei;

                        SelectedMusikTitelItem = GetNextMusikTitel();
                        OnChanged();
                    }
                }
                else
                    BGPlayerAktPlaylistTitel = null;
                OnChanged("BGPlayerAktPlaylistTitelTeilAbspielen");
                OnChanged("BGPlayerAktPlaylistTitelLänge");
                OnChanged("BGPlayerAktPlaylistTitelTeilEnde");
                OnChanged("BGPlayerAktPlaylistTitelTeilStart");

                OnChanged();                
            }
        }

        private ListBoxItem GetNextMusikTitel()
        {
            if (BGPlayer == null || BGPlayer.AktPlaylist == null)
                return null;
            Guid old_GUID = BGPlayer.AktPlaylistTitel != null ? BGPlayer.AktPlaylistTitel.Audio_TitelGUID : Guid.Empty;
            if (BGPlayer.AktPlaylist.Repeat == null &&
                BGPlayer.AktPlaylistTitel != null)
            {
                BGPlayerAktPlaylistTitel = null;
                return FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == old_GUID);
            }

            BGPlayerAktPlaylistTitel = null;
            if (BGPlayer.NochZuSpielen.Count == 0 && BGPlayer.AktPlaylist != null && BGPlayer.AktPlaylist.Repeat != null &&
                BGPlayer.AktPlaylist.Repeat.Value || BGPlayer.BG[BGPlayeraktiv].isPaused)
                RenewMusikNochZuSpielen();
            if (BGPlayer.NochZuSpielen.Count > 0)
            {
                Guid next;
                if (BGPlayer.AktPlaylist.Shuffle)
                    next = BGPlayer.NochZuSpielen[(new Random()).Next(0, BGPlayer.NochZuSpielen.Count - 1)];
                else
                {
                    // Liste neu auffrischen
                    RenewMusikNochZuSpielen();
                    // wenn der momentane Titel der letzte war, von vorne beginnen
                    if (BGPlayer.NochZuSpielen.Count == 1)
                        next = BGPlayer.NochZuSpielen[0];
                    else
                    {                        
                        //ansonsten Liste löschen bis zum Nächsten 
                        if (old_GUID != Guid.Empty)
                        {
                            while (BGPlayer.NochZuSpielen.Count != 0 && 
                                BGPlayer.NochZuSpielen[0] != old_GUID)
                                BGPlayer.NochZuSpielen.RemoveAt(0);

                            if (BGPlayer.NochZuSpielen.Count == 0)
                                return null;
                            Guid zuLöschen = BGPlayer.NochZuSpielen[0];
                            while (BGPlayer.NochZuSpielen[0] == zuLöschen)
                                BGPlayer.NochZuSpielen.RemoveAt(0);
                        }
                        next = BGPlayer.NochZuSpielen[0];
                    }
                }
                return FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == next);
            }
            else
                BGStoppen(); 

            return null;
        }
        
        public void RenewMusikNochZuSpielen()
        {
            if (BGPlayer.AktPlaylist == null)
                return;
            if (MusikNacheinander && (BGPlayer.Gespielt.Count != 0 || BGPlayer.AktPlaylist.Audio_Playlist_Titel.Count == 0))
            {
                int aktPos = FilteredMusikListItemListe.FindIndex(t => t.VM.aPlaylist == BGPlayer.AktPlaylist);
                if (aktPos == FilteredMusikListItemListe.Count-1)
                    SelectedMusikItem = FilteredMusikListItemListe.First();
                else
                    SelectedMusikItem = FilteredMusikListItemListe.ElementAt(aktPos+1);                
                return;
            }
            foreach (Audio_Playlist_Titel aPlaylistTitel in BGPlayer.AktPlaylist.Audio_Playlist_Titel)
            {
                if (aPlaylistTitel.Aktiv && !BGPlayer.MusikNOK.Contains(aPlaylistTitel.Audio_TitelGUID))
                {
                    for (int bew = 0; bew <= aPlaylistTitel.Rating; bew++)
                        BGPlayer.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                }
            }            
        }

        private int _onWarteZeitMinChange;
        public int OnWarteZeitMinChange
        {
            get { return _onWarteZeitMinChange; }
            set
            {
                _onWarteZeitMinChange = value;
                OnChanged();
            }
        }

        #endregion

        #region // Listen
        
        private List<boxThemeTheme> _boxThemeThemeHintergrundList;
        public List<boxThemeTheme> boxThemeThemeHintergrundList
        {
            get { return _boxThemeThemeHintergrundList; }
            set
            {
                _boxThemeThemeHintergrundList = value;
                OnChanged();
            }
        }

        private List<boxThemeTheme> _boxThemeThemeGeräuscheList;
        public List<boxThemeTheme> boxThemeThemeGeräuscheList
        {
            get { return _boxThemeThemeGeräuscheList; }
            set
            {
                _boxThemeThemeGeräuscheList = value;
                OnChanged();
            }
        }

        private List<boxThemeTheme> _boxThemeThemeList;
        public List<boxThemeTheme> boxThemeThemeList
        {
            get { return _boxThemeThemeList; }
            set
            {
                _boxThemeThemeList = value;
                OnChanged();
            }
        }

        public List<KlangZeile> KlangZeilen
        {
            get { return _klangzeilen; }
            set
            {
                _klangzeilen = value;
                OnChanged();
            }
        }

        private List<ListBoxItem> _hintergrundMusikListe;
        public List<ListBoxItem> HintergrundMusikListe
        {
            get { return _hintergrundMusikListe; }
            set
            {
                _hintergrundMusikListe = value;
                OnChanged();
            }
        }

        private List<ListBoxItem> _filterehintergrundMusikListe;
        public List<ListBoxItem> FilteredHintergrundMusikListe
        {
            get { return _filterehintergrundMusikListe; }
            set
            {
                _filterehintergrundMusikListe = value;
                OnChanged();
            }
        }

        private List<MusikZeile> _musikListItemListe;
        public List<MusikZeile> MusikListItemListe
        {
            get { return _musikListItemListe; }
            set
            {
                _musikListItemListe = value;

                FilterMusikPlaylistListe();

                ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
                FilterErwPlayerMusikPlaylistListe();
                OnChanged();
            }
        }

        private List<MusikZeile> _filteredMusikListItemListe;
        public List<MusikZeile> FilteredMusikListItemListe
        {
            get { return _filteredMusikListItemListe; }
            set
            {
                _filteredMusikListItemListe = value;
                OnChanged();
            }
        }

        private List<MusikZeile> _filteredErwPlayerGeräuscheListItemListe;
        public List<MusikZeile> FilteredErwPlayerGeräuscheListItemListe
        {
            get { return _filteredErwPlayerGeräuscheListItemListe; }
            set
            {
                _filteredErwPlayerGeräuscheListItemListe = value;
                OnChanged();
            }
        }

        private List<MusikZeile> _erwPlayerGeräuscheListItemListe;
        public List<MusikZeile> ErwPlayerGeräuscheListItemListe
        {
            get { return _erwPlayerGeräuscheListItemListe; }
            set
            {
                _erwPlayerGeräuscheListItemListe = value;

                FilterErwPlayerGeräuschePlaylistListe();
                OnChanged();
            }
        }

        private List<MusikZeile> _filteredErwPlayerMusikListItemListe;
        public List<MusikZeile> FilteredErwPlayerMusikListItemListe
        {
            get { return _filteredErwPlayerMusikListItemListe; }
            set
            {
                _filteredErwPlayerMusikListItemListe = value;
                OnChanged();
            }
        }

        private List<MusikZeile> _erwPlayerMusikListItemListe;
        public List<MusikZeile> ErwPlayerMusikListItemListe
        {
            get { return _erwPlayerMusikListItemListe; }
            set
            {
                value.ForEach(delegate(MusikZeile mZeile) { mZeile.VM.GroßeAnsicht = PListGroßeAnsicht; });
                _erwPlayerMusikListItemListe = value;
                OnChanged("FilteredErwPlayerMusikListItemListe");
                OnChanged();
            }
        }

        private List<grdThemeButton> _filteredErwPlayerThemeListe;
        public List<grdThemeButton> FilteredErwPlayerThemeListe
        {
            get { return _filteredErwPlayerThemeListe; }
            set
            {
                _filteredErwPlayerThemeListe = value;
                OnChanged();
            }
        }

        private List<grdThemeButton> _erwPlayerThemeListe;
        public List<grdThemeButton> ErwPlayerThemeListe
        {
            get { return _erwPlayerThemeListe; }
            set
            {
                _erwPlayerThemeListe = value;
                OnChanged();
            }
        }

        private List<lbEditorItemVM> _editorThemeÜbrigThemeListe;
        public List<lbEditorItemVM> EditorThemeÜbrigThemeListe
        {
            get { return _editorThemeÜbrigThemeListe; }
            set
            {
                _editorThemeÜbrigThemeListe = value;
                OnChanged();
            }
        }

        private List<lbEditorItemVM> _editorThemeListBoxItemListe;
        public List<lbEditorItemVM> EditorThemeListBoxItemListe
        {
            get { return _editorThemeListBoxItemListe; }
            set
            {
                _editorThemeListBoxItemListe = value;
                OnChanged();
            }
        }

        private List<lbEditorItemVM> _filteredEditorThemeListBoxItemListe;
        public List<lbEditorItemVM> FilteredEditorThemeListBoxItemListe
        {
            get { return _filteredEditorThemeListBoxItemListe; }
            set
            {
                _filteredEditorThemeListBoxItemListe = value;
                OnChanged();
            }
        }

        private List<lbEditorItemVM> _editorListBoxItemListe;
        public List<lbEditorItemVM> EditorListBoxItemListe
        {
            get { return _editorListBoxItemListe; }
            set
            {
                _editorListBoxItemListe = value;

                FilterEditorPlaylistListe();
                OnChanged();
            }
        }

        private List<lbEditorItemVM> _filteredEditorListBoxItemListe;
        public List<lbEditorItemVM> FilteredEditorListBoxItemListe
        {
            get { return _filteredEditorListBoxItemListe; }
            set
            {
                _filteredEditorListBoxItemListe = value;

                FilterMusikPlaylistListe();
                FilterErwPlayerMusikPlaylistListe();
                FilterErwPlayerGeräuschePlaylistListe();

                OnChanged();
            }
        }

        private List<AudioZeileVM> _filteredLbEditorAudioZeilenListe;
        public List<AudioZeileVM> FilteredLbEditorAudioZeilenListe
        {
            get { return _filteredLbEditorAudioZeilenListe; }
            set
            {
                _filteredLbEditorAudioZeilenListe = value;
                OnChanged();
            }
        }

        private List<AudioZeileVM> _lbEditorIAudioZeilenListe = new List<AudioZeileVM>();
        public List<AudioZeileVM> LbEditorAudioZeilenListe
        {
            get { return _lbEditorIAudioZeilenListe; }
            set
            {
                _lbEditorIAudioZeilenListe = value;
                OnChanged();
            }
        }

        private AudioZeileVM _lbEditorAudioZeilenSelected = new AudioZeileVM();
        public AudioZeileVM LbEditorAudioZeilenSelected
        {
            get { return _lbEditorAudioZeilenSelected; }
            set
            {
                _lbEditorAudioZeilenSelected = value;
                OnChanged();
            }
        }

        //private List<Audio_Playlist> _playlistListe;
        //public List<Audio_Playlist> PlaylistListe
        //{
        //    get { return _playlistListe; }
        //    set
        //    {
        //        _playlistListe = value;
        //        OnChanged();
        //    }
        //}

        private List<string> _hotkeysAvailable = new List<string>();
        public List<string> HotkeysAvailable
        {
            get { return _hotkeysAvailable; }
            set
            {
                Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null).ForEach(ti => _hotkeysAvailable.Add(ti.Key)); 
                OnChanged();
            }
        }
        
        private ObservableCollection<Button> _hotkeyButtons = null;
        public ObservableCollection<Button> HotkeyButtons 
        {
            get { return _hotkeyButtons; }
 
            set
            {
                hotkeyListUsed.ForEach(delegate(btnHotkey hkey)            
                {
                    Audio_Playlist aplylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.VM.aPlaylistGuid);                    
                    hkey.VM.aPlaylist = aplylist;
                    hkey.VM.TitelPlayList.ForEach(t => t.mp.Stop());
                });
                OnChanged();
            } 
        }
        
        #region //---- Volume-Modifikation ----


        private double _fadingGeräuscheVolProzent = 100;
        public double FadingGeräuscheVolProzent
        {
            get { return _fadingGeräuscheVolProzent; }
            set
            {
                _fadingGeräuscheVolProzent = value;
                ErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile mZeile)
                {
                    if (mZeile.VM.grpobj != null)
                        mZeile.VM.grpobj.Vol_PlaylistMod = Convert.ToInt32(value);
                });

                if (Einstellungen.GeneralGeräuscheVolume != (int)Math.Round(value))
                    Einstellungen.SetEinstellung<int>("GeneralGeräuscheVolume", (int)Math.Round(value));
                OnChanged();
            }
        }

        private Base.CommandBase _allVol0 = null;
        public Base.CommandBase OnAllVol0
        {
            get
            {
                if (_allVol0 == null)
                    _allVol0 = new Base.CommandBase(AllVol0, null);
                return _allVol0;
            }
        }
        void AllVol0(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM){ aZeileVM.aPlayTitel.Volume = 0;});            
        }

        private Base.CommandBase _allVol100 = null;
        public Base.CommandBase OnAllVol100
        {
            get
            {
                if (_allVol100 == null)
                    _allVol100 = new Base.CommandBase(AllVol100, null);
                return _allVol100;
            }
        }
        void AllVol100(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM) { aZeileVM.aPlayTitel.Volume = 100; });
        }

        private Base.CommandBase _allVolDown = null;
        public Base.CommandBase OnAllVolDown
        {
            get
            {
                if (_allVolDown == null)
                    _allVolDown = new Base.CommandBase(AllVolDown, null);
                return _allVolDown;
            }
        }
        void AllVolDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.Volume = aZeileVM.aPlayTitel.Volume - 3 >= 0 ? aZeileVM.aPlayTitel.Volume - 3 : 0; });
        }

        private Base.CommandBase _allVolUp = null;
        public Base.CommandBase OnAllVolUp
        {
            get
            {
                if (_allVolUp == null)
                    _allVolUp = new Base.CommandBase(AllVolUp, null);
                return _allVolUp;
            }
        }
        void AllVolUp(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.Volume = aZeileVM.aPlayTitel.Volume + 3 <= 100 ? aZeileVM.aPlayTitel.Volume + 3 : 100; });
        }

        private Base.CommandBase _allVolMinDown = null;
        public Base.CommandBase OnAllVolMinDown
        {
            get
            {
                if (_allVolMinDown == null)
                    _allVolMinDown = new Base.CommandBase(AllVolMinDown, null);
                return _allVolMinDown;
            }
        }
        void AllVolMinDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.VolumeMin = aZeileVM.aPlayTitel.VolumeMin - 3 >= 0 ? aZeileVM.aPlayTitel.VolumeMin - 3 : 0; });
        }

        private Base.CommandBase _allVolMaxDown = null;
        public Base.CommandBase OnAllVolMaxDown
        {
            get
            {
                if (_allVolMaxDown == null)
                    _allVolMaxDown = new Base.CommandBase(AllVolMaxDown, null);
                return _allVolMaxDown;
            }
        }
        void AllVolMaxDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.VolumeMax = aZeileVM.aPlayTitel.VolumeMax - 3 >= 0 ? aZeileVM.aPlayTitel.VolumeMax - 3 : 0; });
        }

        private Base.CommandBase _allVolMinUp = null;
        public Base.CommandBase OnAllVolMinUp
        {
            get
            {
                if (_allVolMinUp == null)
                    _allVolMinUp = new Base.CommandBase(AllVolMinUp, null);
                return _allVolMinUp;
            }
        }
        void AllVolMinUp(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.VolumeMin = aZeileVM.aPlayTitel.VolumeMin + 3 <= 100 ? aZeileVM.aPlayTitel.VolumeMin + 3 : 100; });
        }

        private Base.CommandBase _allVolMaxUp = null;
        public Base.CommandBase OnAllVolMaxUp
        {
            get
            {
                if (_allVolMaxUp == null)
                    _allVolMaxUp = new Base.CommandBase(AllVolMaxUp, null);
                return _allVolMaxUp;
            }
        }
        void AllVolMaxUp(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.VolumeMax = aZeileVM.aPlayTitel.VolumeMax + 3 <= 100 ? aZeileVM.aPlayTitel.VolumeMax + 3 : 100; });
        }

        [DependentProperty("SelectedEditorItem")]
        public bool IsVolumeChangeChecked
        {
            get
            {
                bool _isVolumeChangeChecked = true;
                if (AktKlangPlaylist != null && !AktKlangPlaylist.Hintergrundmusik)
                {
                    foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
                    {
                        if (!aZeileVM.aPlayTitelVolumeChange)
                        {
                            _isVolumeChangeChecked = false;
                            break;
                        }
                    }                    
                }
                return _isVolumeChangeChecked;
            }
            set { OnChanged(); }
        }

        private Base.CommandBase _onAllVolumeChange = null;
        public Base.CommandBase OnAllVolumeChange
        {
            get
            {
                if (_onAllVolumeChange == null)
                    _onAllVolumeChange = new Base.CommandBase(AllVolumeChange, null);
                return _onAllVolumeChange;
            }
        }
        void AllVolumeChange(object obj)
        {
            bool ziel = !IsVolumeChangeChecked;
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.VolumeChange = ziel; aZeileVM.aPlayTitelVolumeChange = ziel; });
            OnChanged("IsVolumeChangeChecked");
        }

        #endregion

        #region //---- Pausenzeit-Modifikation ----

        private Base.CommandBase _onPausenZeitMaxPlus = null;
        public Base.CommandBase OnPausenZeitMaxPlus
        {
            get
            {
                if (_onPausenZeitMaxPlus == null)
                    _onPausenZeitMaxPlus = new Base.CommandBase(PausenZeitMaxPlus, null);
                return _onPausenZeitMaxPlus;
            }
        }
        void PausenZeitMaxPlus(object obj)
        {
            int max = 900000;
            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {
                int sollWert = (aZeileVM.aPlayTitelPauseMax >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMax >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMax = sollWert > max ? max : aZeileVM.aPlayTitelPauseMax + sollWert;
            }
            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onPausenZeitMaxMinus = null;
        public Base.CommandBase OnPausenZeitMaxMinus
        {
            get
            {
                if (_onPausenZeitMaxMinus == null)
                    _onPausenZeitMaxMinus = new Base.CommandBase(PausenZeitMaxMinus, null);
                return _onPausenZeitMaxMinus;
            }
        }
        void PausenZeitMaxMinus(object obj)
        {
            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {
                int sollWert = (aZeileVM.aPlayTitelPauseMax >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMax >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMax = aZeileVM.aPlayTitelPauseMax - sollWert < 0 ? 0 : aZeileVM.aPlayTitelPauseMax - sollWert;

                if (aZeileVM.aPlayTitelPauseMin > aZeileVM.aPlayTitelPauseMax)
                    aZeileVM.aPlayTitelPauseMin = aZeileVM.aPlayTitelPauseMax;
            }
            OnChanged("AktKlangPlaylist");
        }



        private Base.CommandBase _onPausenZeitMinPlus = null;
        public Base.CommandBase OnPausenZeitMinPlus
        {
            get
            {
                if (_onPausenZeitMinPlus == null)
                    _onPausenZeitMinPlus = new Base.CommandBase(PausenZeitMinPlus, null);
                return _onPausenZeitMinPlus;
            }
        }
        void PausenZeitMinPlus(object obj)
        {
            int max = 900000;
            
            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {  
                int sollWert = (aZeileVM.aPlayTitelPauseMin >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMin >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMin = sollWert > max ? max : aZeileVM.aPlayTitelPauseMin + sollWert;
                
                if (aZeileVM.aPlayTitelPauseMax < aZeileVM.aPlayTitelPauseMin)
                    aZeileVM.aPlayTitelPauseMax = aZeileVM.aPlayTitelPauseMin;
            }
            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onPausenZeitMinMinus = null;
        public Base.CommandBase OnPausenZeitMinMinus
        {
            get
            {
                if (_onPausenZeitMinMinus == null)
                    _onPausenZeitMinMinus = new Base.CommandBase(PausenZeitMinMinus, null);
                return _onPausenZeitMinMinus;
            }
        }
        void PausenZeitMinMinus(object obj)
        {
            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {                
                int sollWert = (aZeileVM.aPlayTitelPauseMin >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMin >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMin = aZeileVM.aPlayTitelPauseMin - sollWert < 0 ? 0 : aZeileVM.aPlayTitelPauseMin - sollWert;                
            }
            OnChanged("AktKlangPlaylist");
        }

        public bool IsPausenZeitChangeChecked
        {
            get
            {
                bool _isPausenZeitChangeChecked = true;
                if (AktKlangPlaylist != null && !AktKlangPlaylist.Hintergrundmusik)
                {
                    foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
                    {
                        if (!aZeileVM.aPlayTitelPauseChange)
                        {
                            _isPausenZeitChangeChecked = false;
                            break;
                        }
                    }
                }
                return _isPausenZeitChangeChecked;
            }
            set { OnChanged(); }
        }

        private Base.CommandBase _onAllPausenZeitChange = null;
        public Base.CommandBase OnAllPausenZeitChange
        {
            get
            {
                if (_onAllPausenZeitChange == null)
                    _onAllPausenZeitChange = new Base.CommandBase(AllPausenZeitChange, null);
                return _onAllPausenZeitChange;
            }
        }
        void AllPausenZeitChange(object obj)
        {
            bool ziel = !IsPausenZeitChangeChecked;
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM) 
            { aZeileVM.aPlayTitel.PauseChange = ziel; aZeileVM.aPlayTitelPauseChange = ziel; }); 
            OnChanged("IsPausenZeitChangeChecked");            
        }


        private Base.CommandBase _allPause0 = null;
        public Base.CommandBase OnAllPause0
        {
            get
            {
                if (_allPause0 == null)
                    _allPause0 = new Base.CommandBase(AllPause0, null);
                return _allPause0;
            }
        }
        void AllPause0(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelPause = 0; });
            OnChanged("AktKlangPlaylist");
        }

        private Base.CommandBase _allPause100 = null;
        public Base.CommandBase OnAllPause100
        {
            get
            {
                if (_allPause100 == null)
                    _allPause100 = new Base.CommandBase(AllPause100, null);
                return _allPause100;
            }
        }
        void AllPause100(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelPause = 9000000; });
            OnChanged("AktKlangPlaylist");
        }

        private Base.CommandBase _allPauseDown = null;
        public Base.CommandBase OnAllPauseDown
        {
            get
            {
                if (_allPauseDown == null)
                    _allPauseDown = new Base.CommandBase(AllPauseDown, null);
                return _allPauseDown;
            }
        }
        void AllPauseDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            {
                if (aZeileVM.aPlayTitelPause > SliderTicks.Min())
                {
                    if (!SliderTicks.Contains(aZeileVM.aPlayTitelPause))
                    {
                        int i = 0;
                        while (SliderTicks[i] != SliderTicks.Max() && SliderTicks[i] < aZeileVM.aPlayTitelPause &&
                            i < SliderTicks.Count - 1) i++;
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[i];
                    }
                    else
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[SliderTicks.IndexOf(aZeileVM.aPlayTitelPause) - 1];

                }
            });            
            OnChanged("AktKlangPlaylist");
        }

        private Base.CommandBase _allPauseUp = null;
        public Base.CommandBase OnAllPauseUp
        {
            get
            {
                if (_allPauseUp == null)
                    _allPauseUp = new Base.CommandBase(AllPauseUp, null);
                return _allPauseUp;
            }
        }
        void AllPauseUp(object obj)
        {
            
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            {
                if (aZeileVM.aPlayTitelPause < SliderTicks.Max())
                {
                    if (!SliderTicks.Contains(aZeileVM.aPlayTitelPause))
                    {
                        int i = SliderTicks.Count - 1;
                        while (SliderTicks[i] != SliderTicks.Min() && SliderTicks[i] > aZeileVM.aPlayTitelPause &&
                            i < SliderTicks.Count - 1) i++;
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[i];
                    }
                    else                        
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[SliderTicks.IndexOf(aZeileVM.aPlayTitelPause) + 1];
                }
            });                
            OnChanged("AktKlangPlaylist");
        }

        
        /// <summary>
        /// Sucht den nächsten verfügbaren Listen-Namen
        /// 0 = PlaylistListe
        /// 1 = ThemeListe
        /// </summary>
        public string GetNeuenNamen(string titel, int liste)
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

                    while (themelist != null &&
                        (themelist.Audio_Playlist.Count != 0 ||
                        themelist.Audio_Theme1.Count != 0 ||
                        themelist.Audio_Theme2.Count != 0))
                    {
                        NeuerName = titel + "-" + ver;
                        ver++;
                        themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(NeuerName));
                    }
                }
            }
            return NeuerName;
        }




        #endregion

        #region //---- Wartezeit-Modifikation ----


        private Base.CommandBase _onWarteZeitMaxPlus = null;
        public Base.CommandBase OnWarteZeitMaxPlus
        {
            get
            {
                if (_onWarteZeitMaxPlus == null)
                    _onWarteZeitMaxPlus = new Base.CommandBase(WarteZeitMaxPlus, null);
                return _onWarteZeitMaxPlus;
            }
        }
        void WarteZeitMaxPlus(object obj)
        {
            int max = 900000;

            int sollWert = (AktKlangPlaylist.WarteZeitMax >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMax >= 2000) ? 1000 : 200;

            AktKlangPlaylist.WarteZeitMax = sollWert > max ? max : AktKlangPlaylist.WarteZeitMax + sollWert;

            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onWarteZeitMaxMinus = null;
        public Base.CommandBase OnWarteZeitMaxMinus
        {
            get
            {
                if (_onWarteZeitMaxMinus == null)
                    _onWarteZeitMaxMinus = new Base.CommandBase(WarteZeitMaxMinus, null);
                return _onWarteZeitMaxMinus;
            }
        }
        void WarteZeitMaxMinus(object obj)
        {
            int sollWert = (AktKlangPlaylist.WarteZeitMax >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMax >= 2000) ? 1000 : 200;

            AktKlangPlaylist.WarteZeitMax = AktKlangPlaylist.WarteZeitMax - sollWert < 0 ? 0 : AktKlangPlaylist.WarteZeitMax - sollWert;

            if (AktKlangPlaylist.WarteZeitMin > AktKlangPlaylist.WarteZeitMax)
                AktKlangPlaylist.WarteZeitMin = AktKlangPlaylist.WarteZeitMax;

            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onWarteZeitMinPlus = null;
        public Base.CommandBase OnWarteZeitMinPlus
        {
            get
            {
                if (_onWarteZeitMinPlus == null)
                    _onWarteZeitMinPlus = new Base.CommandBase(WarteZeitMinPlus, null);
                return _onWarteZeitMinPlus;
            }
        }
        void WarteZeitMinPlus(object obj)
        {
            int max = 900000;

            int sollWert = (AktKlangPlaylist.WarteZeitMin >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMin >= 2000) ? 1000 : 200;
            AktKlangPlaylist.WarteZeitMin = sollWert > max ? max : AktKlangPlaylist.WarteZeitMin + sollWert;

            if (AktKlangPlaylist.WarteZeitMax < AktKlangPlaylist.WarteZeitMin)
                AktKlangPlaylist.WarteZeitMax = AktKlangPlaylist.WarteZeitMin;

            OnChanged("AktKlangPlaylist");
        }
        private Base.CommandBase _onWarteZeitMinMinus = null;
        public Base.CommandBase OnWarteZeitMinMinus
        {
            get
            {
                if (_onWarteZeitMinMinus == null)
                    _onWarteZeitMinMinus = new Base.CommandBase(WarteZeitMinMinus, null);
                return _onWarteZeitMinMinus;
            }
        }

        void WarteZeitMinMinus(object obj)
        {
            int sollWert = (AktKlangPlaylist.WarteZeitMin >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMin >= 2000) ? 1000 : 200;

            AktKlangPlaylist.WarteZeitMin = AktKlangPlaylist.WarteZeitMin - sollWert < 0 ? 0 : AktKlangPlaylist.WarteZeitMin - sollWert;

            OnChanged("AktKlangPlaylist");
        }

        #endregion


        #endregion


        #region // ---- Commands ----


       


        public int PlaylistWarteZeitMinIncValue
        {
            get
            {
                return (AktKlangPlaylist.WarteZeitMin >= 10000) ? 5000 :
                         (AktKlangPlaylist.WarteZeitMin >= 2000) ? 1000 : 200;
            }
        }

        public int PlaylistWarteZeitMaxIncValue
        {
            get
            {
                return (AktKlangPlaylist.WarteZeitMax >= 10000) ? 5000 :
                         (AktKlangPlaylist.WarteZeitMax >= 2000) ? 1000 : 200;
            }
        }
        public string AktKlangPlaylistWarteZeitTooltip
        {
            get
            {
                return (AktKlangPlaylistWarteZeit < 1000 ? AktKlangPlaylistWarteZeit + " ms" : AktKlangPlaylistWarteZeit < 60000 ?
                    Math.Round((double)AktKlangPlaylistWarteZeit / 1000, 2).ToString() + " sek." :
                    AktKlangPlaylistWarteZeit / 60000 + " min.");
            }
        }

        public long AktKlangPlaylistWarteZeit
        {
            get { return AktKlangPlaylist.WarteZeit; }
            set
            {
                AktKlangPlaylist.WarteZeit = value;
                OnChanged();
                OnChanged("AktKlangPlaylistWarteZeitToolTip");
            }
        }
        
        public long AktKlangPlaylistWarteZeitMin
        {
            get { return AktKlangPlaylist.WarteZeitMin; }
            set
            {
                AktKlangPlaylist.WarteZeitMin = value;
                if (AktKlangPlaylistWarteZeitMax < value)
                    AktKlangPlaylistWarteZeitMax = value;
                OnChanged();
                OnChanged("PlaylistWarteZeitMinIncValue");
            }
        }

        public long AktKlangPlaylistWarteZeitMax 
        {
            get { return AktKlangPlaylist.WarteZeitMax; }
            set
            {
                AktKlangPlaylist.WarteZeitMax = value;
                if (AktKlangPlaylistWarteZeitMin > value)
                    AktKlangPlaylistWarteZeitMin = value;
                OnChanged();
                OnChanged("PlaylistWarteZeitMaxIncValue");
            }
        }
        
        
        public void OnAudioTabClose(object sender, RoutedEventArgs e)
        {
            KlangProgBarTimer.Stop();
            MusikProgBarTimer.Stop();
            for (int posObjGruppe = 0; posObjGruppe < _GrpObjecte.Count; posObjGruppe++)
            {
                List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> KlangZeilenLaufend = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);

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
            if (BGPlayer != null && BGPlayer.BG[0].mPlayer != null)
            {
                BGPlayer.BG[0].mPlayer.Stop();
                BGPlayer.BG[0].mPlayer.Close();
            }
            if (BGPlayer != null && BGPlayer.BG[1].mPlayer != null)
            {
                BGPlayer.BG[1].mPlayer.Stop();
                BGPlayer.BG[1].mPlayer.Close();
            }

            foreach (DispatcherTimer dispTmr in lstKlangPlayEndetimer)
                if (dispTmr != null) dispTmr.Stop();

            AlleKlangSongsAus(null, true, true, false, true);

            _GrpObjecte.Clear();
            lstKlangPlayEndetimer.Clear();
        }

        private Base.CommandBase _onRbtnPlaylistAlsHintergrundmusik = null;
        public Base.CommandBase OnRbtnPlaylistAlsHintergrundmusik
        {
            get
            {
                if (_onRbtnPlaylistAlsHintergrundmusik == null)
                    _onRbtnPlaylistAlsHintergrundmusik = new Base.CommandBase(RbtnPlaylistAlsHintergrundmusik, null);
                return _onRbtnPlaylistAlsHintergrundmusik;
            }
        }
        void RbtnPlaylistAlsHintergrundmusik(object obj)
        {
            AktKlangPlaylist = _GrpObjecte.FirstOrDefault(t => t.visuell).aPlaylist;

            if (AktKlangPlaylist != null)
            {
                if (AktKlangPlaylist.Hintergrundmusik)
                {
                    MusikZeile mZeile = ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => t.VM.aPlaylist.Audio_PlaylistGUID == AktKlangPlaylist.Audio_PlaylistGUID);
                    if (mZeile != null && mZeile.tbtnCheck.IsChecked.Value)
                        mZeile.tbtnCheck.IsChecked = false;
                }
                else
                {
                    if (AktKlangPlaylist == BGPlayerAktPlaylist)
                        btnBGStoppen(null);
                }
            }

            SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);


            MusikListItemListe = mZeileEditorMusikNeuErstellen();
            ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
            ErwPlayerGeräuscheListItemListe = mZeileErwPlayerGeräuscheNeuErstellen();
            
            FilterMusikPlaylistListe();
            //FilterThemeEditorPlaylistListe();
            FilterErwPlayerMusikPlaylistListe();
            FilterErwPlayerGeräuschePlaylistListe();
            //FilterErwPlayerThemeListe();
        }
        

        private Base.CommandBase _onBtnDeleteAll = null;
        public Base.CommandBase OnBtnDeleteAll
        {
            get
            {
                if (_onBtnDeleteAll == null)
                    _onBtnDeleteAll = new Base.CommandBase(BtnDeleteAll, null);
                return _onBtnDeleteAll;
            }
        }
        void BtnDeleteAll(object obj)
        {
            try
            {
                if (ViewHelper.ConfirmYesNoCancel("Löschen ALLER bestehender Audio-Daten", "Soll die komplette Audio-Datenbank gelöscht werden?" +
                        Environment.NewLine + Environment.NewLine + "Achtung! Alle Daten gehen unwiderruflich verloren.") == 2)
                    DeleteAll(2);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
            }
        }
                
        private Base.CommandBase _onBtnImportAll = null;
        public Base.CommandBase OnBtnImportAll
        {
            get
            {
                if (_onBtnImportAll == null)
                    _onBtnImportAll = new Base.CommandBase(BtnImportAll, null);
                return _onBtnImportAll;
            }
        }
        void BtnImportAll(object obj)
        {            
            try
            {
                int mrRes = (Global.ContextAudio.PlaylistListe.Count == 0 && Global.ContextAudio.ThemeListe.Count == 0)? 2:
                    ViewHelper.ConfirmYesNoCancel("Löschen bestehender Daten", "Soll die aktuelle Datenbank erweitert werden?" + 
                    Environment.NewLine + Environment.NewLine + "Wählen sie 'Ja' damit die Datenbank erweitert wird." +
                    Environment.NewLine + "Wählen Sie 'Nein' um die bestehende Datenbank zu ersetzten. Achtung! Alle Daten gehen verloren.");                
                if (mrRes == 2 || mrRes == 1)
                {
                    Global.SetIsBusy(true);
                    
                    //Importieren aller Playlisten und danach aller Themelisten
                    
                    System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                    folderDlg.SelectedPath = Environment.CurrentDirectory;
                    folderDlg.Description = "Wählen Sie ein Verzeichnis das alle Dateien der Sicherung enthält";
                    List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();

                    if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (mrRes == 1)
                            DeleteAll(2);

                        Global.SetIsBusy(true, string.Format("Alle Playlisten werden importiert ..."));
                        string pfad = folderDlg.SelectedPath;

                        DirectoryInfo d = new DirectoryInfo(pfad);
                        List<string> listXML = new List<string>();
                        foreach (FileInfo f in d.GetFiles("*.xml"))
                            listXML.Add(f.DirectoryName + "\\" + f.Name);

                        PlaylistenImportieren(listXML);
                        ThemeImportieren(listXML);

                        UpdateAlleListen();

                        SelectedEditorThemeItem = EditorThemeListBoxItemListe.First(t => t.ATheme == AktKlangTheme);
                    }
                    Global.SetIsBusy(false);
                }
            }
            catch (Exception) { }
        }


        private Base.CommandBase _onBtnThemeImport = null;
        public Base.CommandBase OnBtnThemeImport
        {
            get
            {
                if (_onBtnThemeImport == null)
                    _onBtnThemeImport = new Base.CommandBase(BtnThemeImport, null);
                return _onBtnThemeImport;
            }
        }
        void BtnThemeImport(object obj)
        {
            try
            {
                List<string> dateien = ViewHelper.ChooseFiles("Theme importieren", "", false, "xml");
                if (dateien != null)
                {
                    try
                    {
                        ThemeImportieren(dateien);

                        Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                        Global.ContextAudio.Save();
                        Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                        AktualisiereHotKeys();
                        UpdateAlleListen();

                        SelectedEditorThemeItem = EditorThemeListBoxItemListe.FirstOrDefault(t => t.ATheme == AktKlangTheme);
                        Global.SetIsBusy(false);
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

        //private Base.CommandBase _onBtnThemeExport = null;
        //public Base.CommandBase OnBtnThemeExport
        //{
        //    get
        //    {
        //        if (_onBtnThemeExport == null)
        //            _onBtnThemeExport = new Base.CommandBase(BtnThemeExport, null);
        //        return _onBtnThemeExport;
        //    }
        //}
        //void BtnThemeExport(object obj)
        //{
        //    try
        //    {
        //        Guid g = (Guid)((ListboxItemIcon)((StackPanel)((Button)sender).Parent).Parent).Tag;
        //        Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == g);

        //        if (aTheme != null)
        //        {
        //            string pfaddatei = ViewHelper.ChooseFile("Theme exportieren", "Theme_" + aTheme.Name.Replace("/", "_") + ".xml", true, "xml");

        //            pfaddatei = validateString(pfaddatei);

        //            ExportTheme(aTheme, pfaddatei);
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


        private Base.CommandBase _onThemeExportAll = null;
        public Base.CommandBase OnThemeExportAll
        {
            get
            {
                if (_onThemeExportAll == null)
                    _onThemeExportAll = new Base.CommandBase(ThemeExportAll, null);
                return _onThemeExportAll;
            }
        }
        void ThemeExportAll(object obj)
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

        private Base.CommandBase _onPlaylistenExportAll = null;
        public Base.CommandBase OnPlaylistenExportAll
        {
            get
            {
                if (_onPlaylistenExportAll == null)
                    _onPlaylistenExportAll = new Base.CommandBase(PlaylistenExportAll, null);
                return _onPlaylistenExportAll;
            }
        }
        void PlaylistenExportAll(object obj)
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
                                        
                    AlleThemesExportieren(pfad);
                    ViewHelper.Popup("Der Export aller Audio-Daten wurde erfolgreich beendet." + Environment.NewLine + Environment.NewLine +
                        "Alle Audio-Playlisten und Themes wurden in folgendes Verzeichnis exportiert." + Environment.NewLine +
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

        private Base.CommandBase _onExportAllPlaylisten = null;
        public Base.CommandBase OnExportAllPlaylisten
        {
            get
            {
                if (_onExportAllPlaylisten == null)
                    _onExportAllPlaylisten = new Base.CommandBase(ExportAllPlaylisten, null);
                return _onExportAllPlaylisten;
            }
        }
        void ExportAllPlaylisten(object obj)
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
        
        private Base.CommandBase _onbtnKlangUpdateFiles = null;
        public Base.CommandBase OnbtnKlangUpdateFiles
        {
            get
            {
                if (_onbtnKlangUpdateFiles == null)
                    _onbtnKlangUpdateFiles = new Base.CommandBase(btnKlangUpdateFiles, null);
                return _onbtnKlangUpdateFiles;
            }
        }
        void btnKlangUpdateFiles(object obj)
        {            
            string titelRef = "";
            try
            {
                Global.SetIsBusy(true, string.Format("Neue Dateien werden integriert..."));
                List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);                
                List<string> allFiles = new List<string>();

                allFiles.AddRange(_chkAnzDateien.allFilesMP3);
                allFiles.AddRange(_chkAnzDateien.allFilesOGG);
                allFiles.AddRange(_chkAnzDateien.allFilesWAV);
                allFiles.AddRange(_chkAnzDateien.allFilesWMA);
                
                if (allFiles.Count > 0)
                {
                    if (ViewHelper.ConfirmYesNoCancel("Hinzufügen von Musiktitel aus dem Verzeichnis", "Es wurden insgesamt " + allFiles.Count +
                        " Dateien gefunden, die noch nicht in der Playliste eingetragen sind." + Environment.NewLine +
                        "Sollen diese integriert werden?") == 2)
                    {
                        Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine + titelRef + "werden eingebunden"));
                        
                        foreach (string newFile in allFiles)
                        {
                            Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine +
                                titelRef + "werden eingebunden" +
                                Environment.NewLine + System.IO.Path.GetFileName(newFile)));
                            KlangDateiHinzu(newFile, null, null, AktKlangPlaylist, 0);
                        }
                        SelectedEditorItem = SelectedEditorItem;
                    }
                }
                OnChanged("ChkAnzDateienResult");
                _chkAnzDateienInDir(AktKlangPlaylist);  
                Global.SetIsBusy(false);
            }
            catch (Exception ex)
            {
                ChkAnzDateienResult = null;
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Ungültiger Pfad" + Environment.NewLine + "Bitte überprüfen Sie das Verzeichnis:" + Environment.NewLine + titelRef, ex);
            }
        }
        

        private Base.CommandBase _onbtnBGStoppen = null;
        public Base.CommandBase OnbtnBGStoppen 
        {
            get
            {
                if (_onbtnBGStoppen == null)
                    _onbtnBGStoppen = new Base.CommandBase(btnBGStoppen, null);
                return _onbtnBGStoppen;
            }
        }
        void btnBGStoppen(object obj)
        {
            //BGStoppen();
                        
            //BGPlayer.BG[BGPlayeraktiv].isPaused = false;
            //if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
            //{
            //    if (!BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
            //    {
            //        BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
            //        BGFadingOut(BGPlayer.BG[BGPlayeraktiv], true, true);
            //        BGPlayeraktiv = (BGPlayeraktiv == 0) ? 1 : 0;
            //    }
            //}
            if (!BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
            {
                BGmPlayerIsPaused = false;
                //BGPlayer.BG[BGPlayeraktiv].isPaused = false;
                if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null &&
                    !BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
                {
                    BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
                    BGFadingOut(BGPlayer.BG[BGPlayeraktiv], true, true);
                }
            }
            MusikProgBarTimer.Stop();
            BGmPlayerIsPaused = true;
            BGPlayer.BG[BGPlayeraktiv].aPlaylist = null;
            //BGPlayeraktiv = (BGPlayeraktiv == 0) ? 1 : 0;
            BGPlayer.Gespielt.Clear();
            BGPlayerGespieltCount = 0;
            RenewMusikNochZuSpielen();

            //BGPlayer.Gespielt.RemoveRange(0, BGPlayer.Gespielt.Count);
            //BGPlayerGespieltCount = BGPlayer.Gespielt.Count;
            BGPlayer.NochZuSpielen.RemoveRange(0, BGPlayer.NochZuSpielen.Count);
                        
            //
            SelectedMusikTitelItem = null;     
        }
  
      
        private Base.CommandBase _onbtnBGApspielen = null;
        public Base.CommandBase OnbtnBGApspielen
        {
            get
            {
                if (_onbtnBGApspielen == null)
                    _onbtnBGApspielen = new Base.CommandBase(btnBGApspielen, null);
                return _onbtnBGApspielen;
            }
        }
        void btnBGApspielen(object obj)
        {
            if (BGPlayer.BG[BGPlayeraktiv].aPlaylist == null || BGmPlayerIsPaused)
            {
                BGmPlayerIsPaused = false;
                if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null && BGPlayer.BG[BGPlayeraktiv].mPlayer.Source != null)
                {
                    if ((!BGPlayer.BG[BGPlayeraktiv].isPaused || SelectedMusikTitelItem == null) &&
                        BGPlayer.BG[BGPlayeraktiv].aPlaylist != BGPlayer.AktPlaylist)
                        SelectedMusikTitelItem = GetNextMusikTitel();

                    BGPlayer.BG[BGPlayeraktiv].aPlaylist = BGPlayer.AktPlaylist;
                    BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = false;

                    BGmPlayerIsPaused = false;
                    //BGPlayer.BG[BGPlayeraktiv].isPaused = false;                    
                    FadingIn(BGPlayer.BG[BGPlayeraktiv], 
                        BGPlayer.BG[BGPlayeraktiv].mPlayer, 
                        BGPlayer.AktPlaylistTitel != null? (double)BGPlayer.AktPlaylistTitel.Volume / 100: 1);
                }
                else
                    SpieleNeuenMusikTitel(Guid.Empty);
            }
            else
            {
                BGmPlayerIsPaused = true;
                BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
                BGFadingOut(BGPlayer.BG[BGPlayeraktiv], false, true);
                //BGPlayer.BG[BGPlayeraktiv].aPlaylist = null; 
                //BGPlayeraktiv = (BGPlayeraktiv == 0) ? 1 : 0;
            }
        }


        private Base.CommandBase _onbtnBGPrev = null;
        public Base.CommandBase OnbtnBGPrev
        {
            get
            {
                if (_onbtnBGPrev == null)
                    _onbtnBGPrev = new Base.CommandBase(btnBGPrevClick, null);
                return _onbtnBGPrev;
            }
        }
        void btnBGPrevClick(object obj)
        {
            BGmPlayerIsPaused = false;
            if (BGPlayer.Gespielt.Count == 0)
                SpieleNeuenMusikTitel(SelectedMusikTitelItem != null ? (Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID : Guid.Empty);// (ListBoxItem)lbMusiktitellist.Items[lbMusiktitellist.SelectedIndex]).Tag);
            else
            {
                BGPlayer.Gespielt.RemoveAll(t => t.Equals((Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID));
                Guid vorher = BGPlayer.Gespielt.Count >= 1?
                    BGPlayer.Gespielt.ElementAt(BGPlayer.Gespielt.Count - 1):
                    (Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID;
                BGPlayerGespieltCount = BGPlayer.Gespielt.Count;

                SelectedMusikTitelItem = FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == vorher);                
            }           
            OnChanged();
            OnChanged("BGPlayer");
            OnChanged("SelectedMusikTitelItem");
        }


        private Base.CommandBase _onbtnBGNext = null;
        public Base.CommandBase OnbtnBGNext
        {
            get
            {
                if (_onbtnBGNext == null)
                    _onbtnBGNext = new Base.CommandBase(btnBGNextClick, null);
                return _onbtnBGNext;
            }
        }
        void btnBGNextClick(object obj)
        {
            BGmPlayerIsPaused = false;
            Info_BGTitel = null;
            SelectedMusikTitelItem = GetNextMusikTitel();
            if (SelectedMusikTitelItem == null && (BGPlayer.AktPlaylist.Repeat == null || BGPlayer.AktPlaylist.Repeat.Value)) 
                RenewMusikNochZuSpielen();
            OnChanged("BGPlayer");
            OnChanged("SelectedMusikTitelItem");
        }


        private Base.CommandBase _onMusikStern1 = null;
        public Base.CommandBase OnMusikStern1
        {
            get
            {
                if (_onMusikStern1 == null)
                    _onMusikStern1 = new Base.CommandBase(MusikStern1Click, null);
                return _onMusikStern1;
            }
        }
        void MusikStern1Click(object obj)
        {
            MusikStern1 = MusikStern1 && !MusikStern2 ? false : true;
            MusikStern2 = false;
            MusikStern3 = false;
            MusikStern4 = false;
            MusikStern5 = false;
            RatingUpdate(1,BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern2 = null;
        public Base.CommandBase OnMusikStern2
        {
            get
            {
                if (_onMusikStern2 == null)
                    _onMusikStern2 = new Base.CommandBase(MusikStern2Click, null);
                return _onMusikStern2;
            }
        }        
        void MusikStern2Click(object obj)
        {
            MusikStern2 = MusikStern2 && !MusikStern3 ? false : true;
            MusikStern1 = MusikStern2;
            MusikStern3 = false;
            MusikStern4 = false;
            MusikStern5 = false;
            RatingUpdate(2, BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern3 = null;
        public Base.CommandBase OnMusikStern3
        {
            get
            {
                if (_onMusikStern3 == null)
                    _onMusikStern3 = new Base.CommandBase(MusikStern3Click, null);
                return _onMusikStern3;
            }
        }
        void MusikStern3Click(object obj)
        {
            MusikStern3 = MusikStern3 && !MusikStern4 ? false : true;
            MusikStern1 = MusikStern3;
            MusikStern2 = MusikStern3;
            MusikStern4 = false;
            MusikStern5 = false;
            RatingUpdate(3, BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern4 = null;
        public Base.CommandBase OnMusikStern4
        {
            get
            {
                if (_onMusikStern4 == null)
                    _onMusikStern4 = new Base.CommandBase(MusikStern4Click, null);
                return _onMusikStern4;
            }
        }
        void MusikStern4Click(object obj)
        {
            MusikStern4 = MusikStern4 && !MusikStern5 ? false : true;
            MusikStern1 = MusikStern4;
            MusikStern2 = MusikStern4;
            MusikStern3 = MusikStern4;
            MusikStern5 = false;
            RatingUpdate(4, BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern5 = null;
        public Base.CommandBase OnMusikStern5
        {
            get
            {
                if (_onMusikStern5 == null)
                    _onMusikStern5 = new Base.CommandBase(MusikStern5Click, null);
                return _onMusikStern5;
            }
        }
        void MusikStern5Click(object obj)
        {
            MusikStern5 = MusikStern5 ? false : true;
            MusikStern1 = MusikStern5;
            MusikStern2 = MusikStern5;
            MusikStern3 = MusikStern5;
            MusikStern4 = MusikStern5;
            RatingUpdate(5, BGPlayer.AktPlaylistTitel);
        }


        private Base.CommandBase _onAllHotkeysStop = null;
        public Base.CommandBase OnAllHotkeysStop
        {
            get
            {
                if (_onAllHotkeysStop == null)
                    _onAllHotkeysStop = new Base.CommandBase(AllHotkeysStop, null);
                return _onAllHotkeysStop;
            }
        }
        void AllHotkeysStop(object obj)
        {
            //.FindAll(t => t.VM.mp != null).FindAll(t => t.VM.mp.HasAudio)
            hotkeyListUsed.ForEach(delegate(btnHotkey hkey)
            {
                hkey.VM.TitelPlayList.FindAll(t => t.mp != null && t.mp.HasAudio).ForEach(delegate(btnHotkeyVM.TitelPlay titelPlay)
                { titelPlay.mp.Stop(); });
            });
                //hkey.VM.mp.Stop(); });
        }


        private Base.CommandBase _onAlleHotkeysEntfenrnen = null;
        public Base.CommandBase OnAlleHotkeysEntfenrnen
        {
            get
            {
                if (_onAlleHotkeysEntfenrnen == null)
                    _onAlleHotkeysEntfenrnen = new Base.CommandBase(AlleHotkeysEntfenrnen, null);
                return _onAlleHotkeysEntfenrnen;
            }
        }
        void AlleHotkeysEntfenrnen(object obj)
        {
            if (ViewHelper.ConfirmYesNoCancel("Löschen aller hotkeyListe", "Klicken Sie 'Ja' um alle hotkeyListe aus sämtlichen Plalyisten zu entfernen") == 2)
            {
                Global.ContextAudio.PlaylistListe.ForEach(delegate(Audio_Playlist aPlaylist) { aPlaylist.Key = null; });
                UpdateHotkeyUsed();
            }
        }
       
  
        private Base.CommandBase _onHotkeyEntfernen = null;
        public Base.CommandBase OnHotkeyEntfernen
        {
            get
            {
                if (_onHotkeyEntfernen == null)
                    _onHotkeyEntfernen = new Base.CommandBase(HotkeyEntfernen, null);
                return _onHotkeyEntfernen;
            }
        }
        void HotkeyEntfernen(object obj)
        {
            if (AktKlangPlaylist != null) 
                AktKlangPlaylist.Key = null;
            UpdateHotkeyUsed();
        }


        private Base.CommandBase _onHotkeyBtnDefine = null;
        public Base.CommandBase OnHotkeyBtnDefine
        {
            get
            {
                if (_onHotkeyBtnDefine == null)
                    _onHotkeyBtnDefine = new Base.CommandBase(HotkeyBtnDefine, null);
                return _onHotkeyBtnDefine;
            }
        }
        void HotkeyBtnDefine(object obj)
        {
            try
            {
                hotkeyListe.ForEach(delegate(btnHotkey hkey)
                {
                    bool found = false;
                    hotkeyListUsed.ForEach(delegate(btnHotkey hkeyUsed) {
                        if (hkey.VM.taste == hkeyUsed.VM.taste)
                            found = true;});
                    if (!found)
                        HotkeysAvailable.Add(Convert.ToChar(hkey.VM.taste).ToString());
                });
                IsAuswahlHotkey = true;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Setzen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
            }
        }
       
 
        private Base.CommandBase _onHotkeyCmbxDefine = null;
        public Base.CommandBase OnHotkeyCmbxDefine
        {
            get
            {
                if (_onHotkeyCmbxDefine == null)
                    _onHotkeyCmbxDefine = new Base.CommandBase(HotkeyCmbxDefine, null);
                return _onHotkeyCmbxDefine;
            }
        }
        void HotkeyCmbxDefine(object obj)
        {
            try
            {
                if (AktKlangPlaylist == null)
                    return;
                if (((ComboBox)obj).SelectedIndex != -1)
                {
                    GruppenObjekt grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    grpobj.cmboxTopHotkey.Visibility = Visibility.Collapsed;
                    grpobj.btnTopHotkeySet.Visibility = Visibility.Visible;

                    if (hotkeyListe.FirstOrDefault(t => t.VM.aPlaylistGuid == AktKlangPlaylist.Audio_PlaylistGUID) != null)
                        grpobj.btnHotkeyEntfernen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                    btnHotkey hkey = hotkeyListe.FirstOrDefault(t => t.VM.taste == Convert.ToInt32(((Border)((ComboBox)obj).SelectedItem).Tag));
                    if (hkey != null)
                    {
                        hkey.VM.aPlaylistGuid = AktKlangPlaylist.Audio_PlaylistGUID;
                        Audio_Playlist aplylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.VM.aPlaylistGuid);
                        grpobj.btnTopHotkeySet.Content = hkey.VM.taste.ToString();
                    }
                    grpobj.aPlaylist.Key = Convert.ToChar(hkey.VM.taste).ToString();
                    Global.ContextAudio.Update<Audio_Playlist>(grpobj.aPlaylist);
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Wechseln der Hotkey-Taste ist ein Fehler aufgetreten.", ex);
            }
        }


        private Base.CommandBase _geräuscheSpeakerMuting = null;
        public Base.CommandBase OnGeräuscheSpeakerMuting
        {
            get
            {
                if (_geräuscheSpeakerMuting == null)
                    _geräuscheSpeakerMuting = new Base.CommandBase(GeräuscheSpeakerMuting, null);
                return _geräuscheSpeakerMuting;
            }
        }
        void GeräuscheSpeakerMuting(object obj)
        {
            GeräuscheIsMuted =! GeräuscheIsMuted;            
            _GrpObjecte.ForEach(delegate(GruppenObjekt grpObj)
            {
                grpObj._listZeile.FindAll(t => t._mplayer != null).ForEach(delegate(KlangZeile kZeile)
                {
                    kZeile._mplayer.IsMuted = GeräuscheIsMuted;
                });
            });
        }


        private Base.CommandBase _bgSpeakerMuting = null;
        public Base.CommandBase OnBGSpeakerMuting
        {
            get
            {
                if (_bgSpeakerMuting == null)
                    _bgSpeakerMuting = new Base.CommandBase(BGSpeakerMuting, null);
                return _bgSpeakerMuting;
            }
        }
        void BGSpeakerMuting(object obj)
        {
            BGPlayer.isMuted =! BGPlayer.isMuted;
            if (BGPlayer.BG[0].mPlayer != null)
                BGPlayer.BG[0].mPlayer.IsMuted = BGPlayer.isMuted;
            if (BGPlayer.BG[1].mPlayer != null)
                BGPlayer.BG[1].mPlayer.IsMuted = BGPlayer.BG[0].mPlayer.IsMuted;
            BGPlayerIsMuted = BGPlayer.isMuted;
            OnChanged("BGPlayer");
        }

        private Base.CommandBase _onSuchTextEditorAudioZeilenLöschen;
        public Base.CommandBase OnSuchTextEditorAudioZeilenLöschen
        {
            get
            {
                if (_onSuchTextEditorAudioZeilenLöschen == null)
                    _onSuchTextEditorAudioZeilenLöschen = new Base.CommandBase(OnSuchTextEditorAudioZeilenLöschenClick, null);
                return _onSuchTextEditorAudioZeilenLöschen;
            }
        }
        void OnSuchTextEditorAudioZeilenLöschenClick(object obj)
        {
            SuchTextEditorAudioZeilen = "";
        }

        private Base.CommandBase _onSuchTextEditorThemeLöschen;
        public Base.CommandBase OnSuchTextEditorThemeLöschen
        {
            get
            {
                if (_onSuchTextEditorThemeLöschen == null)
                    _onSuchTextEditorThemeLöschen = new Base.CommandBase(OnSuchTextEditorThemeLöschenClick, null);
                return _onSuchTextEditorThemeLöschen;
            }
        }
        void OnSuchTextEditorThemeLöschenClick(object obj)
        {
            SuchTextEditorTheme = "";
        }

        private Base.CommandBase _onSuchTextEditorPlaylistLöschen;
        public Base.CommandBase OnSuchTextEditorPlaylistLöschen
        {
            get
            {
                if (_onSuchTextEditorPlaylistLöschen == null)
                    _onSuchTextEditorPlaylistLöschen = new Base.CommandBase(OnSuchTextEditorPlaylistLöschenClick, null);
                return _onSuchTextEditorPlaylistLöschen;
            }
        }
        void OnSuchTextEditorPlaylistLöschenClick(object obj)
        {
            SuchTextEditor = "";
        }

        private Base.CommandBase _onSuchTextErwPlayerThemesLöschen;
        public Base.CommandBase OnSuchTextErwPlayerThemesLöschen
        {
            get
            {
                if (_onSuchTextErwPlayerThemesLöschen == null)
                    _onSuchTextErwPlayerThemesLöschen = new Base.CommandBase(OnSuchTextErwPlayerThemesLöschenClick, null);
                return _onSuchTextErwPlayerThemesLöschen;
            }
        }
        void OnSuchTextErwPlayerThemesLöschenClick(object obj)
        {
            SuchTextErwPlayerTheme = "";
        }

        private Base.CommandBase _onSuchTextMusikLöschen;
        public Base.CommandBase OnSuchTextMusikLöschen
        {
            get
            {
                if (_onSuchTextMusikLöschen == null)
                    _onSuchTextMusikLöschen = new Base.CommandBase(OnSuchTextMusikLöschenClick, null);
                return _onSuchTextMusikLöschen;
            }
        }
        void OnSuchTextMusikLöschenClick(object obj)
        {
            SuchTextMusik = "";
        }

        private Base.CommandBase _onSuchTextMusikTitelLöschen;
        public Base.CommandBase OnSuchTextMusikTitelLöschen
        {
            get
            {
                if (_onSuchTextMusikTitelLöschen == null)
                    _onSuchTextMusikTitelLöschen = new Base.CommandBase(OnSuchTextMusikTitelLöschenClick, null);
                return _onSuchTextMusikTitelLöschen;
            }
        }
        void OnSuchTextMusikTitelLöschenClick(object obj)
        {
            SuchTextMusikTitel = "";
        }
                
        private Base.CommandBase _onSuchTextErwPlayerMusikLöschen;
        public Base.CommandBase OnSuchTextErwPlayerMusikLöschen
        {
            get
            {
                if (_onSuchTextErwPlayerMusikLöschen == null)
                    _onSuchTextErwPlayerMusikLöschen = new Base.CommandBase(OnSuchTextErwPlayerMusikLöschenClick, null);
                return _onSuchTextErwPlayerMusikLöschen;
            }
        }
        void OnSuchTextErwPlayerMusikLöschenClick(object obj)
        {
            SuchTextMusik = "";
        }


        private Base.CommandBase _onSuchTextErwPlayerGeräuscheLöschen;
        public Base.CommandBase OnSuchTextErwPlayerGeräuscheLöschen
        {
            get
            {
                if (_onSuchTextErwPlayerGeräuscheLöschen == null)
                    _onSuchTextErwPlayerGeräuscheLöschen = new Base.CommandBase(OnSuchTextErwPlayerGeräuscheLöschenClick, null);
                return _onSuchTextErwPlayerGeräuscheLöschen;
            }
        }
        void OnSuchTextErwPlayerGeräuscheLöschenClick(object obj)
        {
            SuchTextErwPlayerGeräusche = "";
        }


        private Base.CommandBase _onThemeGeräuscheFilterNichtAktiv;
        public Base.CommandBase OnThemeGeräuscheFilterNichtAktiv
        {
            get
            {
                if (_onThemeGeräuscheFilterNichtAktiv == null)
                    _onThemeGeräuscheFilterNichtAktiv = new Base.CommandBase(OnThemeGeräuscheFilterNichtAktivClick, null);
                return _onThemeGeräuscheFilterNichtAktiv;
            }
        }
        void OnThemeGeräuscheFilterNichtAktivClick(object obj)
        {
            string s = SuchTextErwPlayerGeräusche;
            SuchTextErwPlayerGeräusche = "";
            SuchTextErwPlayerGeräusche = s;
            ThemeGeräuscheFilterAktiv = false;
        }


        private Base.CommandBase _onThemeGeräuscheFilterAktiv;
        public Base.CommandBase OnThemeGeräuscheFilterAktiv
        {
            get
            {
                if (_onThemeGeräuscheFilterAktiv == null)
                    _onThemeGeräuscheFilterAktiv = new Base.CommandBase(OnThemeGeräuscheFilterAktivClick, null);
                return _onThemeGeräuscheFilterAktiv;
            }
        }
        void OnThemeGeräuscheFilterAktivClick(object obj)
        {
            SuchTextErwPlayerGeräusche = "";

            FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.
                FindAll(s => s.tbtnCheck.IsChecked.Value).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            ThemeGeräuscheFilterAktiv = true;
        }


        private Base.CommandBase _onThemeGeräuscheAus;
        public Base.CommandBase OnThemeGeräuscheAus
        {
            get
            {
                if (_onThemeGeräuscheAus == null)
                    _onThemeGeräuscheAus = new Base.CommandBase(OnThemeGeräuscheAusClick, null);
                return _onThemeGeräuscheAus;
            }
        }
        void OnThemeGeräuscheAusClick(object obj)
        {
            ErwPlayerGeräuscheListItemListe.FindAll(s => s.tbtnCheck.IsChecked.Value).ForEach(delegate(MusikZeile mZeile) { 
                mZeile.tbtnCheck.IsChecked = false;
                mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                });
        }

        private Base.CommandBase _onBtnCloseTheme;
        public Base.CommandBase OnBtnCloseTheme
        {
            get
            {
                if (_onBtnCloseTheme == null)
                    _onBtnCloseTheme = new Base.CommandBase(BtnCloseTheme, null);
                return _onBtnCloseTheme;
            }
        }
        void BtnCloseTheme(object obj)
        {
            AktKlangTheme.Audio_Playlist.Remove(((obj as Button).Tag) as Audio_Playlist);
            Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        }


        private Base.CommandBase _onBtnErwPlayerPListAbspielen;
        public Base.CommandBase OnBtnErwPlayerPListAbspielen
        {
            get
            {
                if (_onBtnErwPlayerPListAbspielen == null)
                    _onBtnErwPlayerPListAbspielen = new Base.CommandBase(BtnErwPlayerPListAbspielen, null);
                return _onBtnErwPlayerPListAbspielen;
            }
        }            
        private void BtnErwPlayerPListAbspielen(object obj)
        {
            try
            {
                ErwPlayerGeräuscheLaufen = !ErwPlayerGeräuscheLaufen;
                foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                {
                    if (mZeile.VM.grpobj != null && mZeile.VM.grpobj.aPlaylist != null && mZeile.tbtnCheck.IsChecked.Value)
                    {
                        if (ErwPlayerGeräuscheLaufen)
                            mZeile.VM.tbtnCheckChecked(mZeile.tbtnCheck);
                        else
                        {
                            mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                            if (mZeile.VM.grpobj.aPlaylist.Fading)
                                FadingInGeräusch(mZeile.VM.grpobj);
                        }
                    }
                    if (mZeile.VM.grpobj != null)
                    {
                        mZeile.VM.grpobj.wirdAbgespielt = ErwPlayerGeräuscheLaufen;
                        mZeile.VM.grpobj.totalTimePlylist = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Fehler bei der Funktion btnPListPListAbspielen_Click", ex);
            }
        }


        private string _neuerPlaylistName = "NeuePlayliste";
        public string NeuerPlaylistName
        {
            get { return _neuerPlaylistName; }
            set
            {
                _neuerPlaylistName = GetNeuenNamen("NeuePlayliste", 0);
                OnChanged();
            }
        }

        private bool _playlistAZ = false;
        public bool PlaylistAZ
        {
            get { return _playlistAZ; }
            set
            {
                _playlistAZ = value;
                FilterEditorPlaylistListe();
                OnChanged();
            }
        }

        private bool _titellistAZ = false;
        public bool TitellistAZ
        {
            get { return _titellistAZ; }
            set
            {
                _titellistAZ = value;
                LadeFilteredAudioZeilen();
                OnChanged();
            }
        }
        private bool _musikNacheinander = false;
        public bool MusikNacheinander
        {
            get { return _musikNacheinander; }
            set
            {
                _musikNacheinander = value;
                OnChanged();
            }
        }

        private bool _musikAZ = false;
        public bool MusikAZ
        {
            get { return _musikAZ; }
            set
            {
                _musikAZ = value;
                FilterMusikPlaylistListe();
                FilterErwPlayerMusikPlaylistListe();
                OnChanged();
            }
        }

        private bool _geräuscheAZ = false;
        public bool GeräuscheAZ
        {
            get { return _geräuscheAZ; }
            set
            {
                _geräuscheAZ = value;

                FilterErwPlayerGeräuschePlaylistListe();       
                OnChanged();
            }
        }
        private bool _musikTitelAZ = false;
        public bool MusikTitelAZ
        {
            get { return _musikTitelAZ; }
            set
            {
                _musikTitelAZ = value;
                FilterMusikTitelListe();
                OnChanged();
            }
        }

        private Base.CommandBase _onNeuePlaylist;
        public Base.CommandBase OnNeuePlaylist
        {
            get
            {
                if (_onNeuePlaylist == null)
                    _onNeuePlaylist = new Base.CommandBase(NeuePlaylist, null);
                return _onNeuePlaylist;
            }
        }
        void NeuePlaylist(object obj)
        {
            try
            {
                SuchTextEditor = "";
                OnChanged("SuchTextEditor");
                string NeuePlaylist = GetNeuenNamen("NeuePlayliste", 0);
               
                Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                playlist.Name = NeuePlaylist.ToString();
                playlist.Hintergrundmusik = false;

                playlist.WarteZeitAktiv = true;
                playlist.WarteZeit = 500;
                playlist.WarteZeitChange = true;
                playlist.WarteZeitMin = 500;
                playlist.WarteZeitMax = 2500;

                //zur datenbank hinzufügen
                if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                {
                    playlist.MaxSongsParallel = 1;                    
                    EditorListBoxItemListe = lbiPlaylistListNeuErstellen();
                    FilterEditorPlaylistListe();
                    AktKlangPlaylist = playlist;
                }
                OnChanged("SelectedEditorItem");
                OnChanged();
                SelectedEditorItem = FilteredEditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Erstellen einer neuen Playlist ist ein Fehler aufgetreten.", ex);
            }
        }


        private Base.CommandBase _onNeuesTheme = null;
        public Base.CommandBase OnNeuesTheme
        {
            get
            {
                if (_onNeuesTheme == null)
                    _onNeuesTheme = new Base.CommandBase(NeuesTheme, null);
                return _onNeuesTheme;
            }
        }
        void NeuesTheme(object obj)
        {
            NeuesKlangThemeInDB("");
            UpdateAlleListen();
            OnChanged();
            SelectedEditorThemeItem = FilteredEditorThemeListBoxItemListe.FirstOrDefault(t => t.ATheme == AktKlangTheme);
        }


        #endregion


        #region //---- KONSTRUKTOR ----

        public AudioPlayerViewModel()
        {
            setStdPfad();
            StdPadAufC = stdPfad.Contains("c:\\") || stdPfad.Contains("C:\\");
            Init();
        }

        #endregion


        #region //---- INSTANZMETHODEN ----
        
        public void Init()
        {
            workerGetLength.WorkerReportsProgress = true;
            workerGetLength.WorkerSupportsCancellation = true;
            workerGetLength.DoWork += new DoWorkEventHandler(workerGetLength_DoWork);
            workerGetLength.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerGetLength_RunWorkerCompleted);
            
            UpdateHotkeyUsed();
            UpdateAlleListen();
            Refresh();

            LbEditorMitGeräusche = true;
            LbEditorMitMusik = true;
            
            _timerBGFadingOut.Tick += new EventHandler(_timerBGFadingOut_Tick);
            _timerFadingIn.Tick += new EventHandler(_timerFadingIn_Tick);
            _timerFadingOut.Tick += new EventHandler(_timerFadingOut_Tick);
            _timerFadingOutGeräusche.Tick += new EventHandler(_timerFadingOutGeräusche_Tick);
            _timerFadingInGeräusche.Tick += new EventHandler(_timerFadingInGeräusche_Tick);

            KlangProgBarTimer.Tick += new EventHandler(KlangProgBarTimer_Tick);
            KlangProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            KlangProgBarTimer.Tag = 0;

            MusikProgBarTimer.Tick += new EventHandler(MusikProgBarTimer_Tick);
            MusikProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            VisualGrpObj();
            if (AktKlangPlaylist == null)
            {
                if (EditorListBoxItemListe.Count == 0)
                {
                    NeueKlangPlaylistInDB(NeuerPlaylistName);
                    EditorListBoxItemListe = lbiPlaylistListNeuErstellen();
                    FilterEditorPlaylistListe();
                }

                _GrpObjecte.FirstOrDefault(t => t.visuell).aPlaylist = FilteredEditorListBoxItemListe[0].APlaylist;
                
                AktKlangPlaylist = _GrpObjecte.FirstOrDefault(t => t.visuell).aPlaylist;
            }
            SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
            //if (AktKlangTheme == null)
            //{
            //    NeuesKlangThemeInDB("");
            //    //UpdateAlleListen();
            //    FilterThemeEditorPlaylistListe();
            //    FilterErwPlayerThemeListe();

            //    SelectedEditorThemeItem = FilteredEditorThemeListBoxItemListe.FirstOrDefault(t => t.ATheme == AktKlangTheme);
            //}
            firstshot = false;        
        }

        public void UpdateAlleListen()
        {
            EditorThemeListBoxItemListe = lbiThemeListNeuErstellen();
            EditorListBoxItemListe = lbiPlaylistListNeuErstellen();

            MusikListItemListe = mZeileEditorMusikNeuErstellen();
            ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
            ErwPlayerGeräuscheListItemListe = mZeileErwPlayerGeräuscheNeuErstellen();
            ErwPlayerThemeListe = ThemeErwPlayerListeNeuErstellen();

            Refresh();

            FilterEditorPlaylistListe();
            FilterMusikPlaylistListe();
            FilterThemeEditorPlaylistListe();
            FilterErwPlayerMusikPlaylistListe();
            FilterErwPlayerGeräuschePlaylistListe();
            FilterErwPlayerThemeListe();
        }

        private void VisualGrpObj()
        {
            GruppenObjekt grpobj = new GruppenObjekt();
            tiErstellt++;

            grpobj.objGruppe = Convert.ToInt16(tiErstellt);

            //grpobj.wartezeitTimer.Tick += new EventHandler(wartezeitTimer_Tick);
            grpobj.visuell = true;            
            _GrpObjecte.Add(grpobj);
        }


        public List<MusikZeile> mZeileErwPlayerGeräuscheNeuErstellen()
        {
            bool vorhanden = false;
            List<MusikZeile> mZeileList = new List<MusikZeile>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe.FindAll(t => !t.Hintergrundmusik))
            {
                //Gruppenobjekte ertsellen
                GruppenObjekt grpobj = _GrpObjecte.Where(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == aplylist.Audio_PlaylistGUID);
                vorhanden = grpobj != null;

                if (!vorhanden)
                {
                    grpobj = new GruppenObjekt();
                }
                    grpobj.aPlaylist = aplylist;

                // Geräusche-Playlisten
                //Erweiterter-Player GeräuscheMusikZeilen - Items erstellen
                MusikZeile mZeileErw = ErwPlayerGeräuscheListItemListe != null ? ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => t.VM.aPlaylist == aplylist) : null;
                if (mZeileErw == null)
                {
                    mZeileErw = new MusikZeile();
                    mZeileErw.VM.grpobj = grpobj;
                    mZeileErw.VM.grpobj.wartezeitTimer.Tick += new EventHandler(mZeileErw.VM.wartezeitTimer_Tick);
                }
                    mZeileErw.VM.aPlayerVM = this;
                    if (mZeileErw.VM.aPlaylist != aplylist) mZeileErw.VM.aPlaylist = aplylist;
                

                Grid.SetRow(mZeileErw.grdForceVol, !PListGroßeAnsicht ? 0 : 1);
                Grid.SetColumn(mZeileErw.grdForceVol, !PListGroßeAnsicht ? 2 : 0);
                mZeileErw.Tag = aplylist.Audio_PlaylistGUID;
          //      mZeileErw.chkbxForceVol.Tag = aplylist;
                mZeileErw.sldForceVolume.Tag = aplylist;
                mZeileErw.tboxKategorie.Tag = aplylist.Audio_PlaylistGUID;
                
                mZeileList.Add(mZeileErw);
            }
            return mZeileList;
        }
                
        public List<MusikZeile> mZeileEditorMusikNeuErstellen()
        {           
            List<MusikZeile> mZeileList = new List<MusikZeile>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik))
            {
                //Hintergrund MusikZeilen - Items erstellen
                MusikZeile mZeile = new MusikZeile();

                mZeile.tbtnCheck.Visibility = Visibility.Collapsed;
                mZeile.tboxKategorie.Tag = mZeile.Tag;
                mZeile.Cursor = Cursors.Hand;
                mZeile.Tag = aplylist.Audio_PlaylistGUID;
                mZeile.VM.aPlaylist = aplylist;
                mZeile.VM.GroßeAnsicht = PListGroßeAnsicht;
                mZeile.VM.StdPfad = stdPfad;
                mZeile.VM.Iterations = aplylist.Audio_Playlist_Titel.Count;
                
                mZeileList.Add(mZeile);
            }
            return mZeileList;
        }

        public List<MusikZeile> mZeileErwPlayerMusikNeuErstellen()
        {
            List<MusikZeile> mZeileList = new List<MusikZeile>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik))
            {                            
                //Erweiterter-Player HintergrundMusikZeilen - Items erstellen
                MusikZeile mZeileErw = new MusikZeile();
                mZeileErw.VM.GroßeAnsicht = PListGroßeAnsicht;
                mZeileErw.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                mZeileErw.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                mZeileErw.Cursor = Cursors.Hand; 
                mZeileErw.Tag = aplylist.Audio_PlaylistGUID;
                mZeileErw.VM.aPlaylist = aplylist;
                mZeileErw.tboxKategorie.Tag = aplylist.Audio_PlaylistGUID;

                mZeileList.Add(mZeileErw);
            }
            return mZeileList;
        }

        public List<lbEditorItemVM> lbiPlaylistListNeuErstellen()
        {
            List<lbEditorItemVM> lbiPlaylistList = new List<lbEditorItemVM>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe)
            {
                //Playlist - Items erstellen
                lbEditorItemVM lbi = new lbEditorItemVM();
                lbi.APlaylist = aplylist;
                lbi.IstMusik = aplylist.Hintergrundmusik;
                lbi.PPlaylistName = lbi.Name;
                lbi.PlayerVM = this;
                lbi.Item = lbi.Item;
                
                lbiPlaylistList.Add(lbi);
            }
            return lbiPlaylistList;
        }

        public List<lbEditorItemVM> lbiThemeListNeuErstellen()
        {
            List<lbEditorItemVM> lbiThemeList = new List<lbEditorItemVM>();
            foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe.FindAll(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                //Theme - Items erstellen        
                lbEditorItemVM lbi = new lbEditorItemVM();
                lbi.ATheme = aTheme;
                lbi.PPlaylistName = aTheme.Name;
                lbi.PlayerVM = this;
                lbi.Item = lbi.Item;
                lbiThemeList.Add(lbi);
            }
            return lbiThemeList;
        }

        private List<grdThemeButton> ThemeErwPlayerListeNeuErstellen()
        {
            List<grdThemeButton> grdThemeBtnList = new List<grdThemeButton>();

            foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe.FindAll(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                grdThemeButton grdThButton = new grdThemeButton();
                grdThButton.Tag = aTheme.Audio_ThemeGUID;
                grdThButton.VM.Theme = aTheme;
                grdThButton.VM.GroßeAnsicht = ThemeGroßeAnsicht;
                grdThButton.tbtnTheme.Tag = aTheme.Audio_ThemeGUID;
                grdThButton.chkbxPlus.Tag = aTheme;
                grdThButton.chkbxPlus.IsChecked = aTheme.NurGeräusche;
                grdThButton.tbtnTheme.CommandBindings.Add(new CommandBinding(ThemeCommandCheck, ThemeButton_Checked));
                grdThButton.tbtnTheme.Command = ThemeCommandCheck;
                grdThemeBtnList.Add(grdThButton);
            }
            return grdThemeBtnList;
        }

        
        private void RatingUpdate(int rating, Audio_Playlist_Titel aPlaylistTitel)
        {
            aPlaylistTitel.Rating = rating;
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel);
        }


        public void ExportTheme(Audio_Theme aTheme, string pfaddatei)
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


        private void ThemeButton_Checked(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {                
                Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == ((Guid)((ToggleButton)sender).Tag));

                if (((ToggleButton)sender).IsChecked.Value)
                {
                    if (!aTheme.NurGeräusche)
                    {
                        foreach (grdThemeButton grdTbtn in ErwPlayerThemeListe)
                        {
                            if (grdTbtn.tbtnTheme.IsChecked.Value &&
                                grdTbtn.tbtnTheme != ((ToggleButton)sender) &&
                                !grdTbtn.chkbxPlus.IsChecked.Value)
                            {
                                grdTbtn.tbtnTheme.IsChecked = false;
                                ThemeButton_Checked(grdTbtn.tbtnTheme, e);
                            }
                        }

                        bool foundHintergrund = false;
                        foreach (Audio_Playlist aPlaylist in aTheme.Audio_Playlist)
                        {
                            if (aPlaylist.Hintergrundmusik)
                            {
                                foundHintergrund = true;
                                foreach (MusikZeile mZeile in ErwPlayerMusikListItemListe)
                                {
                                    if ((Guid)mZeile.Tag == aPlaylist.Audio_PlaylistGUID)
                                    {
                                        SelectedMusikItem = mZeile;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!foundHintergrund)
                            btnBGStoppen(null);
                    }

                    foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                    {
                        if (aTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                        {
                            mZeile.tbtnCheck.IsChecked = true;
                            mZeile.VM.tbtnCheckChecked(mZeile.tbtnCheck);
                        }
                    }
                    //Auswählen der Geräusche-Playlisst der untergeorgenten Themes
                    CheckUnterThemes(aTheme);
                    FilterGeräuscheAktiv();

                    OnThemeGeräuscheFilterAktivClick(null);
                }

                //  ----------- UNCHECKED ----------------
                else
                {
                    if (!aTheme.NurGeräusche)
                        btnBGStoppen(null);
                        
                    foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                    {
                        if (aTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                        {
                            mZeile.tbtnCheck.IsChecked = false;
                            mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswählen des Themes ist ein Fehler aufgetreten.", ex);
            }
        }
                

        private void CheckUnterThemes(Audio_Theme aTheme)
        {
            foreach (Audio_Theme aUnterTheme in aTheme.Audio_Theme1.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")))
            {
                foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
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
            SuchTextErwPlayerGeräusche = "";
            OnChanged("OnThemeGeräuscheFilterNichtAktiv");
            bool found = false;
            foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
            {
                if (mZeile.tbtnCheck.IsChecked.Value)
                {
                    found = true;
                    break;
                }
            }
            if (found)
                OnChanged("OnThemeGeräuscheFilterAktiv");
        }

        public void ClickTbtnCheckErwPlayer(object sender, ExecutedRoutedEventArgs e)
        {
            MusikZeile mZeile = null;
            foreach (MusikZeile zeile in ErwPlayerGeräuscheListItemListe)
            {
                if (zeile.tbtnCheck == (ToggleButton)sender)
                {
                    mZeile = zeile;
                    break;
                }
            }

            GruppenObjekt grpobj = ((ToggleButton)sender).Tag == null ? null : 
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.objGruppe == Convert.ToInt32(((ToggleButton)sender).Tag));
            if (grpobj == null)
                grpobj = _GrpObjecte.FindAll(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == ((Guid)(mZeile.Tag)));

            if (grpobj == null)
            {                
                ((ToggleButton)sender).Tag = _GrpObjecte[_GrpObjecte.Count - 1].objGruppe;
                string sTitel = ((TextBlock)((StackPanel)((ToggleButton)e.Source).Parent).FindName("tblkTitel")).Text;

                int plylstItemPos = 0;
                while (((MusikZeile)ErwPlayerGeräuscheListItemListe[plylstItemPos]).tblkTitel.Text != sTitel)
                    plylstItemPos++;

                grpobj = _GrpObjecte.FirstOrDefault(t => t.objGruppe == tiErstellt);

                //Get Playlist
                grpobj.aPlaylist = Global.ContextAudio.PlaylistListe.
                        FirstOrDefault(t => t.Audio_PlaylistGUID.Equals(((MusikZeile)ErwPlayerGeräuscheListItemListe[plylstItemPos]).Tag));

                if (grpobj.aPlaylist != null)
                {
                    grpobj.visuell = false;

                    foreach (Audio_Playlist_Titel aPlaylistTitel in grpobj.aPlaylist.Audio_Playlist_Titel)
                    {
                        KlangNewRow(grpobj, aPlaylistTitel);

                        if (aPlaylistTitel.Aktiv &&
                            !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= aPlaylistTitel.Rating; i++)
                                grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                        }
                    }
                }
            }

           // grpobj.DoForceVolume = (mZeile.chkbxForceVol.IsChecked.Value);
            
            if ((FilteredErwPlayerGeräuscheListItemListe.Count == 0) || 
                (1==1) && 
                 !grpobj.wirdAbgespielt &&
                 _GrpObjecte.FindAll(t => t.wirdAbgespielt).FindAll(t => !t.visuell).FindAll(t => t.tiEditor == null).Count != 0)    //Abspielen
            {
                grpobj.wirdAbgespielt = true;
                if (!grpobj.aPlaylist.Fading)
                    mZeile.VM.FadingPercentage = 100;

                //WARTEZEIT DER PLAYLISTE EINBAUEN
                grpobj.wartezeitTimer.Tag = grpobj;

                if (grpobj.aPlaylist.WarteZeitAktiv)
                {
                    if (grpobj.wartezeitTimer.IsEnabled)
                        grpobj.wartezeitTimer.Stop();

                    grpobj.wartezeitTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)grpobj.aPlaylist.WarteZeit);
                    grpobj.wartezeitTimer.Start();
                }

                FilteredErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile _mZeile)
                {
                    if (_mZeile.tbtnCheck.IsChecked.Value && _mZeile.tbtnCheck != ((ToggleButton)sender))
                    {
                        GruppenObjekt grpObjAlleAnderen = _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(_mZeile.tbtnCheck.Tag));
                        if (grpObjAlleAnderen != null && grpObjAlleAnderen.wirdAbgespielt != grpobj.wirdAbgespielt)
                        {
                            grpObjAlleAnderen.wirdAbgespielt = !grpobj.wirdAbgespielt;
                            grpObjAlleAnderen.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                        }
                    }
                });
            }
        }

        public void LoadMusikTitelListe()
        {            
            List<KlangZeile> kZeileList = new List<KlangZeile>();
            List<ListBoxItem> itemList = new List<ListBoxItem>();
            if (BGPlayer.AktPlaylist != null)
            {
                List<Audio_Playlist_Titel> aPlaylisttitelSort = MusikTitelAZ ? 
                    BGPlayer.AktPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Audio_Titel.Name).ToList() :
                    BGPlayer.AktPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Reihenfolge).ToList();

                foreach (Audio_Playlist_Titel playlisttitel in aPlaylisttitelSort)
                {
                    ListBoxItem lbitem = new ListBoxItem();
                    lbitem.Tag = playlisttitel;
                    lbitem.Content = playlisttitel.Audio_Titel.Name;
                    lbitem.ToolTip = playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;
                    lbitem.Background = Background;
                    
                    if (!BGPlayer.AktPlaylist.Audio_Playlist_Titel.First(t => t.Audio_TitelGUID == playlisttitel.Audio_Titel.Audio_TitelGUID).Aktiv)
                    {
                        lbitem.FontStyle = FontStyles.Italic;
                        lbitem.Foreground = Brushes.DarkSlateGray;
                        lbitem.ToolTip = "Audio-Titel inaktiv." + Environment.NewLine + "Im Playlist-Editor anhaken zum Aktivieren" +
                                            Environment.NewLine + "Anklicken um den Titel abzuspielen";
                    }
                    if (BGPlayer.MusikNOK.Contains(playlisttitel.Audio_TitelGUID))
                    {
                        lbitem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                        lbitem.ToolTip = "Datei nicht gefunden. -> " + playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;
                    }
                    itemList.Add(lbitem);

                    KlangZeile kZeile = new KlangZeile(rowErstellt);
                    kZeile.aPlaylistTitel = playlisttitel;
                    rowErstellt++;

                    kZeileList.Add(kZeile);
                }
            }
            HintergrundMusikListe = itemList;
            FilterMusikTitelListe();
            KlangZeilen = kZeileList;
        }

        private lbEditorItem NewlbEditorItem(lbEditorItem item)
        {
            lbEditorItem editorItem = new lbEditorItem();
            return editorItem;
        }

        public void Refresh()
        {
            OnChanged("AktKlangPlaylist");
            OnChanged("AktKlangTheme");
        }

                
        /// <summary>
        /// Läd die AudioZeilen-Liste auf Basis der ausgewählten AktKlangPlaylist.
        /// </summary>
        public void LadeAudioZeilen()
        {
            List<AudioZeileVM> itemList = new List<AudioZeileVM>();

            if (AktKlangPlaylist != null && 
                (AktKlangPlaylist.Audio_Playlist_Titel.Count == 0 ||
                 LbEditorAudioZeilenListe.Count == 0 ||
                 LbEditorAudioZeilenListe.Count(t => t.AktKlangPlaylist == AktKlangPlaylist) != AktKlangPlaylist.Audio_Playlist_Titel.Count))
            {
                Global.SetIsBusy(true, string.Format("Öffne Playliste & Überprüfe Dateien..."));
            
                foreach (Audio_Playlist_Titel aPlayTitel in AktKlangPlaylist.Audio_Playlist_Titel)
                {
                    AudioZeileVM aZeile = new AudioZeileVM();
                    aZeile.PlayerVM = this;
                    aZeile.AktKlangPlaylist = AktKlangPlaylist;
                    aZeile.aPlayTitel = aPlayTitel;
                    aZeile.Checked = aPlayTitel.Aktiv;
                    
                    if (AudioInAnderemPfadSuchen &&
                        !File.Exists(aZeile.aPlayTitel.Audio_Titel.Pfad + "\\" + aZeile.aPlayTitel.Audio_Titel.Datei))
                    {
                        Audio_Titel titel = setTitelStdPfad(aZeile.aPlayTitel.Audio_Titel);
                        if (File.Exists(titel.Pfad + "\\" + titel.Datei))
                            Global.ContextAudio.Update<Audio_Titel>(titel);
                    }
                    if (!File.Exists(aZeile.aPlayTitel.Audio_Titel.Pfad + "\\" + aZeile.aPlayTitel.Audio_Titel.Datei))
                        aZeile.FileNotExist = true;
                    
                    itemList.Add(aZeile);
                }            
                LbEditorAudioZeilenListe = itemList;
                CheckBtnGleicherPfad(AktKlangPlaylist);
                Global.SetIsBusy(false);
            }
                
        }

        /// <summary>
        /// Läd die ThemeZeilen-Liste auf Basis der ausgewählten AktKlangTheme.
        /// </summary>
        private void LadeAudioThemeZeilen()
        {
            List<ListboxItemIcon> itemList = new List<ListboxItemIcon>();

            foreach (Audio_Playlist aPlaylist in AktKlangTheme.Audio_Playlist)
            {
                ListboxItemIcon aZeile = new ListboxItemIcon();
                aZeile.Tag = aPlaylist;
            }
        }

        /// <summary>
        /// Läd alle Lieder und Geräusche - Boxen des ausgewählten Themes
        /// </summary>
        public static RoutedCommand cmdThemeThemeBtnClose = new RoutedCommand();
        private void LadeBoxThemeList()
        {
            List<boxThemeTheme> lstBoxThemeHintergrund = new List<boxThemeTheme>();
            List<boxThemeTheme> lstBoxThemeGeräusche= new List<boxThemeTheme>();
            List<boxThemeTheme> lstBoxThemeTheme = new List<boxThemeTheme>();

            foreach (Audio_Playlist aPlayList in AktKlangTheme.Audio_Playlist)
            {
                boxThemeTheme boxTheme = new boxThemeTheme();
                boxTheme.aPlaylist = aPlayList;
                boxTheme.aTheme = AktKlangTheme;
                boxTheme.btnRemove.Tag = aPlayList;
                boxTheme.btnRemove.Click += btnRemove_Click;
                
                if (aPlayList.Hintergrundmusik)
                    lstBoxThemeHintergrund.Add(boxTheme);                    
                else
                    lstBoxThemeGeräusche.Add(boxTheme);
            }
            boxThemeThemeHintergrundList = lstBoxThemeHintergrund;
            boxThemeThemeGeräuscheList = lstBoxThemeGeräusche;

            // Erstelle Untergeordnete Themes
            foreach (Audio_Theme aUnterTheme in AktKlangTheme.Audio_Theme1.
                Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                boxThemeTheme bxTheme = new boxThemeTheme();
                bxTheme.txblkName.Text = aUnterTheme.Name;
                bxTheme.Tag = aUnterTheme.Audio_ThemeGUID;
                bxTheme.btnRemove.Tag = aUnterTheme;
                bxTheme.btnRemove.Click += btnRemoveTheme_Click;
                lstBoxThemeTheme.Add(bxTheme);
            }
            boxThemeThemeList = lstBoxThemeTheme;             
        }

        void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            AktKlangTheme.Audio_Playlist.Remove(((Button)sender).Tag as Audio_Playlist);
            SelectedEditorThemeItem = SelectedEditorThemeItem;
        }

        void btnRemoveTheme_Click(object sender, RoutedEventArgs e)
        {
            AktKlangTheme.Audio_Theme1.Remove(((Button)sender).Tag as Audio_Theme);
            SelectedEditorThemeItem = SelectedEditorThemeItem;
        }

        public void ThemeItemIconAblegen(Audio_Playlist aPlaylist)
        {
            if (AktKlangTheme == null)
            {
                NeuesKlangThemeInDB("");
                UpdateAlleListen();
                SelectedEditorThemeItem = FilteredEditorThemeListBoxItemListe.FirstOrDefault(t => t.ATheme == AktKlangTheme);
            }
            if (aPlaylist.Hintergrundmusik && AktKlangTheme.Audio_Playlist.Count(t => t.Hintergrundmusik) == 1)
            {
                ViewHelper.Popup("Es ist bereits eine Musik-Playliste in dem Theme eingetragen." + Environment.NewLine + Environment.NewLine +
                       "Entfernen Sie zunächst die aktuelle Musik-Playliste des Themes um eine neue zu definieren.");
            } 
            else
                AktKlangTheme.Audio_Playlist.Add(aPlaylist);
            SelectedEditorThemeItem = SelectedEditorThemeItem;
        }
        

        #region Textfilter

        /// <summary>
        /// Läd die AudioZeilen-Liste auf Basis der ausgewählten AktKlangPlaylist.
        /// </summary>
        public void LadeFilteredAudioZeilen()
        {
            string suchTextEditorAudioZeilen = _suchTextEditorAudioZeilen.ToLower().Trim();
            string[] suchWorte = suchTextEditorAudioZeilen.Split(' ');

            if (!TitellistAZ)
            {
                if (suchTextEditorAudioZeilen == string.Empty) // kein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.OrderBy(n => n.aPlayTitel.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.aPlayTitel.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.aPlayTitel.Reihenfolge).ToList();
            }
            else
            {
                //Sortierung nach Reihenfolge
                if (suchTextEditorAudioZeilen == string.Empty) // kein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.OrderBy(n => n.aPlayTitel.Audio_Titel.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.aPlayTitel.Audio_Titel.Name).ToList();
                else // mehrere Suchwörter
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.aPlayTitel.Audio_Titel.Name).ToList();
            }
        }


        /// <summary>
        /// Erstellt die Liste der noch nicht in dem AktTheme benutzten ThemesListBoxItem.
        /// </summary>
        private List<lbEditorItemVM> FilterThemeÜbrigListBoxItemListe()
        {
            List<lbEditorItemVM> lbiThemes = new List<lbEditorItemVM>();            
            List<Guid> schonAktiveThemes = new List<Guid>();
            
            //Guid-Liste der schon verwendeten Themes erstellen
            if (AktKlangTheme != null)
                schonAktiveThemes.Add(AktKlangTheme.Audio_ThemeGUID);

            if (SelectedEditorThemeItem != null)
            {
                foreach (Audio_Theme aTheme in SelectedEditorThemeItem.ATheme.Audio_Theme2)
                {
                    schonAktiveThemes.Add(aTheme.Audio_ThemeGUID);
                    schonAktiveThemes = CheckUnterTheme(aTheme.Audio_ThemeGUID, schonAktiveThemes);
                }

                //Themes, die das Aktuelle Theme enthalten auch auf die Guid-Liste
                foreach (Audio_Theme aTheme in SelectedEditorThemeItem.ATheme.Audio_Theme1)
                {
                    schonAktiveThemes.Add(aTheme.Audio_ThemeGUID);
                    schonAktiveThemes = CheckUnterThemeInLbi(aTheme.Audio_ThemeGUID, schonAktiveThemes);
                }
            }

            foreach (lbEditorItemVM lbi in EditorThemeListBoxItemListe)
            {
                if (!schonAktiveThemes.Contains(lbi.ATheme.Audio_ThemeGUID))
                    lbiThemes.Add(lbi);
            }

            return lbiThemes;
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

        /// <summary>
        /// Filtert die EditorListBoxItem-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterThemeEditorPlaylistListe()
        {
            string suchTextEditorTheme = _suchTextEditorTheme.ToLower().Trim();
            string[] suchWorte = suchTextEditorTheme.Split(' ');

            if (suchTextEditorTheme == string.Empty) // kein Suchwort
                FilteredEditorThemeListBoxItemListe = EditorThemeListBoxItemListe.OrderBy(n => n.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredEditorThemeListBoxItemListe = EditorThemeListBoxItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredEditorThemeListBoxItemListe = EditorThemeListBoxItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }
                        

        /// <summary>
        /// Filtert die EditorListBoxItem-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterEditorPlaylistListe()
        {
            FilteredEditorListBoxItemListe = EditorListBoxItemListe.AsParallel()
                .Where(t => (t.APlaylist.Hintergrundmusik == LbEditorMitMusik && LbEditorMitMusik == true) ||
                        (!t.APlaylist.Hintergrundmusik == LbEditorMitGeräusche && LbEditorMitGeräusche == true)).ToList();

            string suchTextEditor = _suchTextEditor.ToLower().Trim();
            string[] suchWorte = suchTextEditor.Split(' ');

            if (PlaylistAZ)
            {
                //Sortierung nach Alphabet
                if (suchTextEditor == string.Empty) // kein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.OrderBy(n => n.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
                else // mehrere Suchwörter
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
            }
            else
            {
                //Sortierung nach Reihenfolge
                if (suchTextEditor == string.Empty) // kein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.OrderBy(n => n.APlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.APlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.APlaylist.Reihenfolge).ToList();
            }

            if (!rbEditorEditPlaylist && AktKlangTheme != null)
            {
                foreach (Audio_Playlist aPlaylist in AktKlangTheme.Audio_Playlist)
                {
                    lbEditorItemVM lbi = FilteredEditorListBoxItemListe.FirstOrDefault(t => t.APlaylist.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID);
                    if (lbi != null)
                        FilteredEditorListBoxItemListe.Remove(lbi);
                }
            }
        }
        
        /// <summary>
        /// Filtert die MusikListBoxItem-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterMusikPlaylistListe()
        {
            if (MusikListItemListe == null)
                return;
            string[] suchWorte = _suchTextMusik.Split(' ');
            if (MusikAZ)
            {
                if (_suchTextMusik == string.Empty) // kein Suchwort
                    FilteredMusikListItemListe = MusikListItemListe.OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredMusikListItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else // mehrere Suchwörter
                    FilteredMusikListItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            }
            else
            {
                if (_suchTextMusik == string.Empty) // kein Suchwort
                    FilteredMusikListItemListe = MusikListItemListe.OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredMusikListItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredMusikListItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
            }
        }

        /// <summary>
        /// Filtert die MusikTitel-Liste auf Basis des SuchTextes.
        /// </summary>
        private void FilterMusikTitelListe()
        {
            string[] suchWorte = _suchTextMusikTitel.Split(' ');
            
            HintergrundMusikListe.ForEach(delegate(ListBoxItem lbi) { lbi.Visibility = Visibility.Visible; });
            FilteredHintergrundMusikListe = MusikTitelAZ ?
                HintergrundMusikListe.OrderBy(n => ((Audio_Playlist_Titel)n.Tag).Audio_Titel.Name).ToList():
                HintergrundMusikListe.OrderBy(t => ((Audio_Playlist_Titel)t.Tag).Reihenfolge).ToList(); 
            
            if (suchWorte.Length == 1) // nur ein Suchwort                
            {
                FilteredHintergrundMusikListe.FindAll(t => !t.Content.ToString().ToLower().Contains(suchWorte[0])).ForEach(delegate(ListBoxItem lbi)
                { lbi.Visibility = Visibility.Collapsed;});
            }
            else
            {
                FilteredHintergrundMusikListe.FindAll(t => !TextContains(t.Content.ToString().ToLower(), suchWorte)).ForEach(delegate(ListBoxItem lbi)
                { lbi.Visibility = Visibility.Collapsed;});
            }
        }

        private bool TextContains(string suchwort, string[] suchWorte)
        {
            foreach (string wort in suchWorte)
            {
                if (suchwort != wort)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Filtert die Geräusche im Erw.Player ListBoxItem-Liste auf Basis des SuchTextes und der Theme-SuchTextes
        /// </summary>
        public void FilterErwPlayerGeräuschePlaylistListe()
        {
            if (ErwPlayerGeräuscheListItemListe == null)
                return;
            string st = (_suchTextErwPlayerGeräusche + _suchTextErwPlayerTheme != "") ? _suchTextErwPlayerGeräusche + ' ' + _suchTextErwPlayerTheme : string.Empty;
            string[] suchWorte = st.Split(' ');
            if (GeräuscheAZ)
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerGeräuscheListItemListe =
                        ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            }
            else
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerGeräuscheListItemListe =
                        ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
            }
        }

        /// <summary>
        /// Filtert die Musik im Erw.Player ListBoxItem-Liste auf Basis des SuchTextes und der Theme-SuchTextes.
        /// </summary>
        public void FilterErwPlayerMusikPlaylistListe()
        {
            if (ErwPlayerMusikListItemListe == null)
                return;
            string st = (_suchTextMusik + _suchTextErwPlayerTheme != "")? _suchTextMusik + ' ' + _suchTextErwPlayerTheme : string.Empty;
            string[] suchWorte = st.Split(' ');

            if (MusikAZ)
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            }
            else
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
            }
        }

        /// <summary>
        /// Filtert die Themes im Erw.Player grdThemeButton-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterErwPlayerThemeListe()
        {
            string[] suchWorte = _suchTextErwPlayerTheme.Split(' ');

            if (_suchTextErwPlayerTheme == string.Empty) // kein Suchwort
                FilteredErwPlayerThemeListe = ErwPlayerThemeListe.OrderBy(n => n.VM.Theme.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort                
                FilteredErwPlayerThemeListe =
                    ErwPlayerThemeListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.Theme.Name).ToList();
            else // mehrere Suchwörter
                FilteredErwPlayerThemeListe = ErwPlayerThemeListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.Theme.Name).ToList();
        }

        #endregion

        private string _suchTextEditorAudioZeilen = string.Empty;
        public string SuchTextEditorAudioZeilen
        {
            get { return _suchTextEditorAudioZeilen; }
            set
            {
                _suchTextEditorAudioZeilen = value;
                OnChanged();
                LadeFilteredAudioZeilen();
            }
        }

        private string _suchTextEditorTheme = string.Empty;
        public string SuchTextEditorTheme
        {
            get { return _suchTextEditorTheme; }
            set
            {
                _suchTextEditorTheme = value;
                OnChanged();
                FilterThemeEditorPlaylistListe();
            }
        }

        private string _suchTextEditor = string.Empty;
        public string SuchTextEditor
        {
            get { return _suchTextEditor; }
            set
            {
                _suchTextEditor = value;
                OnChanged();
                FilterEditorPlaylistListe();
            }
        }
                
        private string _suchTextErwPlayerGeräusche = string.Empty;
        public string SuchTextErwPlayerGeräusche
        {
            get { return _suchTextErwPlayerGeräusche; }
            set
            {
                _suchTextErwPlayerGeräusche = value;
                OnChanged();
                FilterErwPlayerGeräuschePlaylistListe();
            }
        }

        //private string _suchTextErwPlayerMusik = string.Empty;
        //public string SuchTextErwPlayerMusik
        //{
        //    get { return _suchTextErwPlayerMusik; }
        //    set
        //    {
        //        _suchTextErwPlayerMusik = value;
        //        OnChanged();
        //        FilterErwPlayerMusikPlaylistListe();
        //    }
        //}

        private string _suchTextErwPlayerTheme = string.Empty;
        public string SuchTextErwPlayerTheme
        {
            get { return _suchTextErwPlayerTheme; }
            set
            {
                _suchTextErwPlayerTheme = value;
                OnChanged();
                FilterErwPlayerThemeListe();
                FilterErwPlayerMusikPlaylistListe();
                FilterErwPlayerGeräuschePlaylistListe();
            }
        }

        private string _suchTextMusik = string.Empty;
        public string SuchTextMusik
        {
            get { return _suchTextMusik; }
            set
            {
                _suchTextMusik = value;
                OnChanged();
                FilterMusikPlaylistListe();
                FilterErwPlayerMusikPlaylistListe();
            }
        }
        private string _suchTextMusikTitel = string.Empty;
        public string SuchTextMusikTitel
        {
            get { return _suchTextMusikTitel; }
            set
            {
                _suchTextMusikTitel = value;
                OnChanged();
                FilterMusikTitelListe();
            }
        }

        #endregion


        #region //---- EVENTS ----


        #endregion

        #region //---- Konvertierungen ----

        private static String ConvertByteToString(byte[] bytes, int pos1, int pos2)
        {
            if ((pos1 > pos2) || (pos2 > bytes.Length - 1))
                throw new ArgumentException("Aruments of range");

            int length = pos2 - pos1 + 1;
            Char[] chars = new Char[length];

            for (int i = 0; i < length; i++)
                chars[i] = Convert.ToChar(bytes[i + pos1]);
            String s = new String(chars);
            s = s.Replace("\0", "");

            return s;
        }

        #endregion

        public Audio_Theme NeuesKlangThemeInDB(string titel)
        {
            string themeName = GetNeuenNamen(titel == "" ? "Neues Theme" : titel, 1);
            Audio_Theme themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(themeName));

            if (themelist == null)
            {
                //Theme - Item erstellen      
                Audio_Theme aTheme = Global.ContextAudio.New<Audio_Theme>();
                aTheme.Name = themeName;
                aTheme.Hintergrund_VolMod = 50;
                aTheme.Klang_VolMod = 50;

                if (Global.ContextAudio.Insert<Audio_Theme>(aTheme))               //erfolgreich hinzugefügt
                {
                    Global.ContextAudio.Update<Audio_Theme>(aTheme);
                    AktKlangTheme = aTheme;
                }
               // UpdateAlleListen();
                return aTheme;
            }
            else
            {
                AktKlangTheme = themelist;
              //  UpdateAlleListen();
                return themelist;
            }
        }

        private ObservableCollection<TitelInfo> _titelListe = new ObservableCollection<TitelInfo>();

        /// <summary>
        /// Titel der aktuellen Playlist.
        /// </summary>
        public ObservableCollection<TitelInfo> TitelListe
        {
            get { return _titelListe; }
            set
            {
                _titelListe = value;
                OnChanged("TitelListe");
            }
        }

        private Audio_Playlist _currentPlaylist;
        public Audio_Playlist CurrentPlaylist
        {
            get { return _currentPlaylist; }
            set
            {
                _currentPlaylist = value;
                TitelListe.Clear();
                if (value != null)
                    value.Audio_Playlist_Titel.Select(pt => new TitelInfo(pt)).ToList().ForEach(ti => TitelListe.Add(ti));
                OnChanged("CurrentPlaylist");
                OnChanged("TitelListe");
            }
        }


        private bool _geräuscheIsMuted = false;
        public bool GeräuscheIsMuted
        {
            get { return _geräuscheIsMuted; }
            set
            {
                _geräuscheIsMuted = value;
                OnChanged();
            }
        }

        private bool _bgPlayerIsMuted = false;
        public bool BGPlayerIsMuted
        {
            get { return _bgPlayerIsMuted; }
            set
            {
                _bgPlayerIsMuted = value;
                OnChanged();
            }
        }

        private int _bgPlayerVolume = Einstellungen.GeneralMusikVolume;
        public int BGPlayerVolume
        {
            get { return _bgPlayerVolume; }
            set
            { 
                _bgPlayerVolume = value;
                Einstellungen.GeneralMusikVolume = value;
                if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)  
                    BGPlayer.BG[BGPlayeraktiv].mPlayer.Volume = ((double)value) / 100;
                OnChanged();
            }
        }


        public class TitelInfo : Base.ViewModelBase
        {
            private Audio_Playlist_Titel _playlistTitel;

            public Audio_Playlist_Titel PlaylistTitel
            {
                get { return _playlistTitel; }
            }

            public TitelInfo(Audio_Playlist_Titel aPlaylistTitel)
            {
                _playlistTitel = aPlaylistTitel;
            }

            public event EventHandler OnRemoveTitel;
            void RemoveTitel(object sender)
            {
                Global.ContextAudio.RemoveTitelFromPlaylist(PlaylistTitel);
                if (OnRemoveTitel != null)
                {
                    OnRemoveTitel(this, new EventArgs());
                }
            }

        }

        /// <summary>
        /// Neuer Titel vom FileChooser aus. Wird zur aktuellen Playlist hinzugefügt.
        /// </summary>
        /// <param name="sender"></param>
        private void AddTitel(object sender)
        {
            string pfad = "";
            Audio_Titel aTitel = CreateTitel(pfad);
            Audio_Playlist_Titel aPlaylistTitel = AddTitelToPlaylist(CurrentPlaylist, aTitel);
            TitelListe.Add(new TitelInfo(aPlaylistTitel));
        }

        private Audio_Titel CreateTitel(string pfad)
        {
            Audio_Titel tmp = Global.ContextAudio.New<Audio_Titel>();
            tmp.Name = "Neuer Titel";
            tmp.Pfad = pfad;
            return tmp;
        }

        private Audio_Playlist_Titel AddTitelToPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel)
        {
            Audio_Playlist_Titel tmp;
            Global.ContextAudio.AddTitelToPlaylist(aPlaylist, aTitel, out tmp);
            return tmp;
        }



        public Audio_Titel setTitelStdPfad(Audio_Titel aTitel)
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

        public void SpieleNeuenMusikTitel(Guid Index, bool addGespielt = true)
        {
            if (BGPlayer.NochZuSpielen.Count == 0 &&
                (BGPlayer.AktPlaylist.Repeat == null || (BGPlayer.AktPlaylist.Repeat != null && BGPlayer.AktPlaylist.Repeat.Value)) && 
                BGPlayer.MusikNOK.Count != FilteredHintergrundMusikListe.Count)
            {
                BGPlayer.Gespielt.Clear();
                BGPlayerGespieltCount = 0;
                RenewMusikNochZuSpielen();
                if (BGPlayer.NochZuSpielen.Count == 0)
                    return;
            }
            else
                if (BGPlayer.NochZuSpielen.Count == 0)
                {
                    BGmPlayerIsPaused = true;
                    return;
                }

            if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null && BGPlayer.BG[BGPlayeraktiv].mPlayer.Position.TotalMilliseconds > 0)
            {
                if (SelectedMusikTitelItem != null && addGespielt)
                {
                    BGPlayer.Gespielt.Add((Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID);

                    if (BGPlayer.Gespielt.Count > 50)
                        BGPlayer.Gespielt.RemoveAt(0);
                }
                MusikProgBarTimer.Stop();
            }
            else
                if (BGPlayer.BG[BGPlayeraktiv].mPlayer == null)
                    BGPlayer.Gespielt.Add(Index);
            BGPlayerGespieltCount = BGPlayer.Gespielt.Count;

            if (BGPlayer.NochZuSpielen.Count != 0)  // kein abspielbarer Titel gefunden
            {
                if (FilteredHintergrundMusikListe.Count == 0)
                {
                    SelectedMusikTitelItem = null;
                    BGPlayerAktPlaylistTitel = null;
                    SelectedMusikTitelItem = FilteredHintergrundMusikListe[0];
                }
                else
                {
                    if (Index != Guid.Empty)
                    {
                        BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(Index));
                        if (!addGespielt)
                            BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == Index);                        
                    }
                    else
                    {
                        // Shuffle-Modus
                        if (BGPlayer.AktPlaylist.Shuffle)
                        {
                            Guid u = BGPlayer.NochZuSpielen[(new Random()).Next(0, BGPlayer.NochZuSpielen.Count)];
                            BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(u));
                            BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == u);
                        }
                        else
                        {
                            int i = FilteredHintergrundMusikListe.IndexOf(FilteredHintergrundMusikListe.FirstOrDefault(t => ((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == BGPlayer.AktPlaylistTitel.Audio_TitelGUID));
                            BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)((Audio_Playlist_Titel)FilteredHintergrundMusikListe[i + 1].Tag).Audio_TitelGUID);
                            BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(BGPlayer.AktPlaylistTitel.Audio_TitelGUID));                            
                        }
                        SelectedMusikTitelItem = FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == BGPlayer.AktPlaylistTitel.Audio_TitelGUID);
                    }

                    if (!BGPlayer.MusikNOK.Contains(BGPlayer.AktPlaylistTitel.Audio_TitelGUID))
                    {
                        Audio_Titel titel = BGPlayer.AktPlaylistTitel.Audio_Titel;
                        if (AudioInAnderemPfadSuchen &&
                            !Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei))
                        {
                            titel = setTitelStdPfad(titel);
                            if (File.Exists(titel.Pfad + "\\" + titel.Datei))
                                Global.ContextAudio.Update<Audio_Titel>(titel);
                        }

                        if (Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei) ||
                            !Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)))
                        {
                            if (!BGPlayer.MusikNOK.Contains(BGPlayer.AktPlaylistTitel.Audio_TitelGUID))
                                BGPlayer.MusikNOK.Add(BGPlayer.AktPlaylistTitel.Audio_TitelGUID);
                        }
                        else
                        {
                            if (Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)) &&
                                File.Exists(titel.Pfad + "\\" + titel.Datei))
                            {
                                if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
                                {
                                    if (FilteredHintergrundMusikListe != null) 
                                    {
                                        {
                                            BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
                                            BGFadingOut(BGPlayer.BG[BGPlayeraktiv], false, false);
                                            BGPlayeraktiv = (BGPlayeraktiv == 0) ? 1 : 0;
                                        }
                                    }
                                }
                                BGmPlayerIsPaused = false;

                                BGPlayer.BG[BGPlayeraktiv].mPlayer =
                                    PlayFile(false, null, null, BGPlayer.BG[BGPlayeraktiv].mPlayer, 
                                        titel.Pfad + "\\" + titel.Datei, Einstellungen.GeneralMusikVolume, true,
                                        BGPlayer.AktPlaylistTitel.TeilAbspielen? BGPlayer.AktPlaylistTitel.TeilStart: null);
                                
                                if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
                                {
                                    Info_BGTitel = null;
                                    MusikProgBarTimer.Tag = -1;
                                    MusikProgBarTimer.Start();
                                }
                            }                            
                        }
                    }          
                }
            }
            else
                SelectedMusikTitelItem = null;
            OnChanged("BGPlayerAktPlaylistTitelLänge");
        }


        #region //---- Fading ----


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
                if (!fadInfo.fadingOutSofort)
                {
                    fadInfo.Start = DateTime.Now;
                    _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;
                }
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
                            //fadInfo.mp.Position = TimeSpan.FromMilliseconds(0);
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

        public void FadingIn(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile, MediaPlayer mplayer, double zielVol)
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
        
        public void _timerFadingIn_Tick(object sender, EventArgs e)
        {
            Fading fadInfo = (Fading)_timerFadingIn.Tag;
            if (fadInfo.Start == DateTime.MinValue)
                fadInfo.Start = DateTime.Now;

            double _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;

            double aktVol = (fadingTime != 0) ? fadInfo.zielVol * ((_vergangeneZeit / (fadingTime * fadingIntervall)) + fadInfo.startVol) : fadInfo.zielVol;

            //if (slBGVolume.Value / 100 != fadInfo.zielVol)        // wenn während des Fading die Lautstärke verändert wird
            //    fadInfo.zielVol = slBGVolume.Value / 100;

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
        
        public void FadingOut(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile, GruppenObjekt grpobj, bool playerStoppen, bool sofort)
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
        
        public void FadingInGeräusch(GruppenObjekt grpobj)
        {
            group newgroup = new group();
            newgroup.grpobj = grpobj;    //force_Volume == null 
            newgroup.zielProzent = !grpobj.DoForceVolume ? FadingGeräuscheVolProzent : grpobj.force_Volume == null? 0: (double)grpobj.force_Volume * 100;
            newgroup.startProzent = grpobj.Vol_PlaylistMod != FadingGeräuscheVolProzent ? grpobj.Vol_PlaylistMod : 0;
            newgroup.StartZeit = DateTime.MinValue;
            newgroup.mZeile = ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => (Guid)t.Tag == grpobj.aPlaylist.Audio_PlaylistGUID);
           
            if (!_timerFadingInGeräusche.IsEnabled)
            {
                FadingInGeräusche _fadingInGeräusche = new FadingInGeräusche();
                _fadingInGeräusche.gruppenIn.Clear();
                _fadingInGeräusche.gruppenIn.Add(newgroup);

                _timerFadingInGeräusche.Tag = _fadingInGeräusche;
                _timerFadingInGeräusche.Start();
            }
            else
            {
                ((FadingInGeräusche)_timerFadingInGeräusche.Tag).gruppenIn.Add(newgroup);
            }
        }

        public void _timerFadingInGeräusche_Tick(object sender, EventArgs e)
        {
            FadingInGeräusche fadInfo = (FadingInGeräusche)_timerFadingInGeräusche.Tag;

            List<group> grpToDelete = new List<group>();

            foreach (group gruppe in fadInfo.gruppenIn)
            {
                if (gruppe.StartZeit == DateTime.MinValue)
                    gruppe.StartZeit = DateTime.Now;

                gruppe._vergangeneZeit = DateTime.Now.Subtract(gruppe.StartZeit).TotalMilliseconds;

                if (!gruppe.grpobj.wirdAbgespielt)
                {
                    grpToDelete.Add(gruppe);
                    continue;
                }
                //if (gruppe.grpobj.force_Volume == null && gruppe.zielProzent != slPlaylistVolume.Value)
                //    gruppe.zielProzent = slPlaylistVolume.Value;
                gruppe.grpobj.Vol_PlaylistMod = (gruppe.grpobj.Vol_PlaylistMod != gruppe.zielProzent) ? (gruppe.zielProzent) * (gruppe._vergangeneZeit / (fadingTime * fadingIntervall)) + gruppe.startProzent : gruppe.zielProzent;
                gruppe.grpobj.Vol_PlaylistMod = gruppe.grpobj.Vol_PlaylistMod > gruppe.zielProzent ? gruppe.zielProzent : gruppe.grpobj.Vol_PlaylistMod;// _volProzentModFadingIn;
                double l = 0;
                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t._mplayer != null))
                {
                    kZeile._mplayer.Volume = !gruppe.grpobj.DoForceVolume?// .force_Volume == null ?
                        ((double)kZeile.Aktuell_Volume / 100) * (gruppe.grpobj.Vol_PlaylistMod / 100) :
                        gruppe.grpobj.Vol_PlaylistMod / 100;
                    l = kZeile._mplayer.Volume;
                }
                
                if (gruppe.mZeile != null)
                    gruppe.mZeile.VM.FadingPercentage = Math.Round(gruppe.grpobj.Vol_PlaylistMod, 2);
                     

                if (!gruppe.grpobj.wirdAbgespielt || gruppe.grpobj.Vol_PlaylistMod == gruppe.zielProzent)
                {
                    grpToDelete.Add(gruppe);
                    gruppe.mZeile.VM.FadingPercentage = 0;
                }
            }
            //Check noch angeklickt zum FadingIn
            for (int c = 0; c < grpToDelete.Count; c++)
                fadInfo.gruppenIn.Remove(grpToDelete[c]);

            if (fadInfo.gruppenIn.Count == 0)
                _timerFadingInGeräusche.Stop();
        }

        public void FadingOutGeräusch(bool playerStoppen, bool sofort, MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj)
        {
            group newgroup = new group();
            newgroup.grpobj = grpobj;
            newgroup.mZeile = ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => (Guid)t.Tag == grpobj.aPlaylist.Audio_PlaylistGUID);
            // .force_Volume != null      
            newgroup.startProzent = grpobj.DoForceVolume && !grpobj.aPlaylist.Fading? grpobj.force_Volume.Value * 100 : grpobj.Vol_PlaylistMod;
            newgroup.zielProzent = 0;
            newgroup.StartZeit = DateTime.MinValue;

            if (!_timerFadingOutGeräusche.IsEnabled)
            {
                FadingOutGeräusche _fadingOutGeräusche = new FadingOutGeräusche();
                _fadingOutGeräusche.gruppenOut.Clear();
                _fadingOutGeräusche.gruppenOut.Add(newgroup);
                _fadingOutGeräusche.fadingOutSofort = sofort;

                _timerFadingOutGeräusche.Interval = TimeSpan.FromMilliseconds(fadingIntervall);
                _timerFadingOutGeräusche.Tag = _fadingOutGeräusche;
                _timerFadingOutGeräusche.Start();
            }
            else
            {
                ((FadingOutGeräusche)_timerFadingOutGeräusche.Tag).gruppenOut.Add(newgroup);
            }
        }

        public void _timerFadingOutGeräusche_Tick(object sender, EventArgs e)
        {
            FadingOutGeräusche fadInfo = (FadingOutGeräusche)_timerFadingOutGeräusche.Tag;

            if (1 == 1)
            {
                List<group> grpToDelete = new List<group>();


                foreach (group gruppe in fadInfo.gruppenOut)
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
                        foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t._mplayer != null))
                        {
                            kZeile._mplayer.Volume = !gruppe.grpobj.DoForceVolume? //.force_Volume == null ?
                                ((double)kZeile.Aktuell_Volume / 100) * (gruppe.grpobj.Vol_PlaylistMod / 100) :
                                gruppe.grpobj.Vol_PlaylistMod / 100;
                            l = kZeile._mplayer.Volume;
                        }
                        if (gruppe.mZeile != null)
                            gruppe.mZeile.VM.FadingPercentage = Math.Round(gruppe.grpobj.Vol_PlaylistMod, 2);
                            //gruppe.mZeile.recProzent.Width = Math.Round(gruppe.grpobj.Vol_PlaylistMod, 2) * .65;// (gruppe.mZeile.grdForceVol.ActualWidth / 100 - .05);

                        if (gruppe.grpobj.Vol_PlaylistMod == 0)
                        {
                            foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t._mplayer != null))
                            {
                                kZeile.istPause = true;
                                kZeile._mplayer.Pause();
                                kZeile.istStandby = true;
                                kZeile.istLaufend = false;
                            }
                            //if (gruppe.mZeile != null)
                            //    gruppe.mZeile.recProzent.Visibility = Visibility.Collapsed;
                            grpToDelete.Add(gruppe);
                        }
                    }
                    if (gruppe.grpobj.wirdAbgespielt)
                        grpToDelete.Add(gruppe);
                }

                for (int c = 0; c < grpToDelete.Count; c++)
                {
                    fadInfo.gruppenOut.Remove(grpToDelete[c]);

                    if (!grpToDelete[c].grpobj.wirdAbgespielt)
                        _GrpObjecte.Remove(grpToDelete[c].grpobj);
                }

                if (fadInfo.gruppenOut.Count == 0)
                {
                    _timerFadingOutGeräusche.Stop();
                }
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
                        if (fadInfo.mPlayerStoppen && fadInfo.klZeile.FadingOutStarted)
                            _GrpObjecte.Remove(fadInfo.grpobj);
                    }
                }
            }
        }
        
        #endregion

        private void GetMusikGeneralInfo()
        {
            FileInfo file = new FileInfo(BGPlayer.BG[BGPlayeraktiv].mPlayer.Source.LocalPath);
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
                Info_BGTitel = System.IO.Path.GetFileNameWithoutExtension(BGPlayer.BG[BGPlayeraktiv].mPlayer.Source.LocalPath);
                Info_BGArtist = "---";
                Info_BGAlbum = "---";
                Info_BGJahr = "---";
                Info_BGGenre = "---";
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
                Info_BGTitel = titel != "" ? titel : System.IO.Path.GetFileNameWithoutExtension(BGPlayer.BG[BGPlayeraktiv].mPlayer.Source.LocalPath);
                Info_BGArtist = ConvertByteToString(bytes, 33, 62);
                Info_BGAlbum = ConvertByteToString(bytes, 63, 92);
                Info_BGJahr = ConvertByteToString(bytes, 93, 96);
                int z = Convert.ToInt32(bytes[127]);
                if (z <= _genres.Length - 1)
                    Info_BGGenre = _genres[z];
            }
            MusikStern1 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 1;
            MusikStern2 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 2;
            MusikStern3 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 3;
            MusikStern4 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 4;
            MusikStern5 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 5;
            BGPlayerAktPlaylistTitelTeilAbspielen = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.TeilAbspielen;
            MusikTeilMax = BGPlayer.AktPlaylistTitel.Audio_Titel.Länge == 0 ? 10000000 :
                BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.HasTimeSpan ?
                BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds : 10000000;
            MusikTeilStart = BGPlayer.AktPlaylistTitel.TeilStart == null ? 0 : BGPlayer.AktPlaylistTitel.TeilStart.Value;
            MusikTeilEnde = BGPlayer.AktPlaylistTitel.TeilEnde == null && BGPlayer.AktPlaylistTitel.Audio_Titel.Länge != null ?
                BGPlayer.AktPlaylistTitel.Audio_Titel.Länge.Value :
                    BGPlayer.AktPlaylistTitel.TeilEnde != null ?
                        BGPlayer.AktPlaylistTitel.TeilEnde.Value : 10000000;
        }

        public void MusikProgBarTimer_Tick(object sender, EventArgs e)
        {
            if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null && BGPlayer.BG[BGPlayeraktiv].mPlayer.Source != null)
            {
                if (BGPlayer.AktPlaylistTitel != null && Info_BGTitel == null)
                    GetMusikGeneralInfo();

                BGPosition = BGPlayer.BG[BGPlayeraktiv].mPlayer.Position.TotalMilliseconds;

                if (BGPlayer.AktPlaylistTitel != null &&
                    BGPlayer.AktPlaylistTitel.TeilAbspielen &&
                    BGPosition < BGPlayer.AktPlaylistTitel.TeilStart.Value)
                    BGPlayer.BG[BGPlayeraktiv].mPlayer.Position = TimeSpan.FromMilliseconds(BGPlayer.AktPlaylistTitel.TeilStart.Value);

                if (BGPlayer.AktPlaylistTitel != null &&
                    BGPlayer.AktPlaylistTitel.Audio_Titel != null &&
                    (BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.HasTimeSpan &&BGPlayer.AktPlaylistTitel != null &&
                    BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds != BGPlayer.AktPlaylistTitel.Audio_Titel.Länge))
                {
                    BGPlayer.AktPlaylistTitel.Audio_Titel.Länge = BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                    OnChanged("BGPlayerAktPlaylistTitelLänge");
                }

                //Bei Musikplaylists die Endposition vor Fading überprüfen
                if ((BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.HasTimeSpan &&
                        BGPlayer.BG[BGPlayeraktiv].mPlayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= BGPlayer.BG[BGPlayeraktiv].mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds) ||
                        (BGPlayer.AktPlaylistTitel != null && BGPlayer.AktPlaylistTitel.TeilAbspielen && BGPlayer.BG[BGPlayeraktiv].mPlayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= BGPlayer.AktPlaylistTitel.TeilEnde))
                {                        
                    Info_BGTitel = null;
                    SelectedMusikTitelItem = GetNextMusikTitel();                       
                }         
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
                                KlangZeilenLaufend[durchlauf].audioZeileVM != null)
                            {
                            if (KlangZeilenLaufend[durchlauf]._mplayer != null &&
                                KlangZeilenLaufend[durchlauf]._mplayer.HasAudio == false &&
                                KlangZeilenLaufend[durchlauf]._mplayer.BufferingProgress == 1)
                                KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.Now;
                            else
                                KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.MinValue;

                                //keine Informationen nach 1 Sekunde vom MediaPlayer über Track -> Gelb -> nächstes Lied
                                if (((TimeSpan)(DateTime.Now - KlangZeilenLaufend[durchlauf].dtLiedLastCheck)).TotalMilliseconds > ((double)Zeitüberlauf+5000))
                                {
                                    if (!KlangZeilenLaufend[durchlauf]._mplayer.HasAudio)
                                    {
                                        KlangZeilenLaufend[durchlauf].audioZeileVM.FilePlayable = false;
                                        KlangZeilenLaufend[durchlauf]._mplayer.Stop();
                                        KlangZeilenLaufend[durchlauf]._mplayer.Close();
                                        KlangZeilenLaufend[durchlauf].playable = false;
                                        KlangZeilenLaufend[durchlauf].istStandby = true;
                                        KlangZeilenLaufend[durchlauf].istLaufend = false;
                                        KlangZeilenLaufend[durchlauf].istPause = false;
                                        CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);
                                    }
                                }
                            }
                            if (KlangZeilenLaufend[durchlauf].aPlaylistTitel.Aktiv && (KlangProgBarTimer.Tag.ToString() == "0") &&
                                KlangZeilenLaufend[durchlauf]._mplayer != null)
                            {
                                if ((!_timerFadingInGeräusche.IsEnabled && !_timerFadingOutGeräusche.IsEnabled) ||
                                    !_GrpObjecte[posObjGruppe].aPlaylist.Fading)
                                {
                                    // Volume anpassen
                                    if ((!_GrpObjecte[posObjGruppe].DoForceVolume) &&// .force_Volume == null) &&
                                        KlangZeilenLaufend[durchlauf].aPlaylistTitel.VolumeChange && !KlangZeilenLaufend[durchlauf].FadingOutStarted)
                                    {
                                        if ((((TimeSpan)(DateTime.Now - _GrpObjecte[posObjGruppe].LastVolUpdate)).Seconds > KlangZeilenLaufend[durchlauf].UpdateZyklusVol) &&
                                            Math.Abs(KlangZeilenLaufend[durchlauf]._mplayer.Volume * 100 - KlangZeilenLaufend[durchlauf].volZiel) <= KlangZeilenLaufend[durchlauf].Vol_jump)
                                        {
                                            KlangZeilenLaufend[durchlauf].volZiel =
                                                (new Random()).Next(0, KlangZeilenLaufend[durchlauf].volMax_wert - KlangZeilenLaufend[durchlauf].volMin_wert) +
                                                KlangZeilenLaufend[durchlauf].volMin_wert;
                                        }
                                        
                                        int speed = (KlangZeilenLaufend[durchlauf].Aktuell_Volume < (double)KlangZeilenLaufend[durchlauf].volMin_wert ||
                                            KlangZeilenLaufend[durchlauf].Aktuell_Volume > (double)KlangZeilenLaufend[durchlauf].volMax_wert) ||
                                            (Math.Abs((double)KlangZeilenLaufend[durchlauf].volZiel - KlangZeilenLaufend[durchlauf].Aktuell_Volume) > 6) ?
                                            KlangZeilenLaufend[durchlauf].Vol_jump : 1;

                                        KlangZeilenLaufend[durchlauf].Aktuell_Volume = (KlangZeilenLaufend[durchlauf].volZiel < KlangZeilenLaufend[durchlauf].Aktuell_Volume) ?
                                            KlangZeilenLaufend[durchlauf].Aktuell_Volume -= speed :
                                            KlangZeilenLaufend[durchlauf].Aktuell_Volume += speed;
                                        
                                    }
                                    double sollWert = (KlangZeilenLaufend[durchlauf].Aktuell_Volume / 100) *
                                        (!_GrpObjecte[posObjGruppe].aPlaylist.Fading ? 1 * (FadingGeräuscheVolProzent / 100): 
                                        (!_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik ? _GrpObjecte[posObjGruppe].Vol_PlaylistMod / 100 : 
                                        1));

                                    //_player.Volume =
                                
                                    //    (grpobj.aPlaylist.DoForce) ?
                                    //    (double)grpobj.aPlaylist.ForceVolume / 100 :
                                    //    (vol / 100) * (FadingGeräuscheVolProzent / 100));

                                    //Forcing des VOLUME
                                    if ((FadingIn_Started == null || FadingIn_Started.Source == null) &&
                                        !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                        _GrpObjecte[posObjGruppe].DoForceVolume)// .force_Volume != null)
                                        KlangZeilenLaufend[durchlauf]._mplayer.Volume = _GrpObjecte[posObjGruppe].force_Volume.Value;
                                    else
                                        if ((FadingIn_Started == null || FadingIn_Started.Source == null) &&
                                            !KlangZeilenLaufend[durchlauf].FadingOutStarted && sollWert != KlangZeilenLaufend[durchlauf]._mplayer.Volume)
                                            KlangZeilenLaufend[durchlauf]._mplayer.Volume = sollWert;
                                }

                                //einmaliges ermitteln des Endzeitpunkts
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeileVM != null &&                                
                                    KlangZeilenLaufend[durchlauf].audioZeileVM.TitelMaximum == 10000000 && 
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan &&
                                    KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel != null)
                                {
                                    if (KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel.Länge != KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
                                    {
                                        KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel.Länge = KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                        Global.ContextAudio.Update<Audio_Titel>(KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel);
                                        UpdatePlaylistLänge(_GrpObjecte[posObjGruppe].aPlaylist);
                                    }
                                    KlangZeilenLaufend[durchlauf].audioZeileVM.TitelMaximum = (double)KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel.Länge;
                                }

                                //aktualisiere ProgressBar - bei Wartezeit auf Maximum
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeileVM != null &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)
                                    KlangZeilenLaufend[durchlauf].audioZeileVM.Progress = // .pbarTitel.Value =
                                        (!KlangZeilenLaufend[durchlauf].istWartezeit) ? 
                                            KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds:
                                            KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds;

                                // Endposition von Geräuschen überprüfen bei vorzeitigem Ende
                                if (!_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik && KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)
                                    if (KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen &&
                                        KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds >= KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilEnde)
                                    {
                                        _GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));
                                        if (_GrpObjecte[posObjGruppe].Gespielt.Count > 50)
                                            _GrpObjecte[posObjGruppe].Gespielt.RemoveAt(0);

                                        bool nurEinLiedAktiv = (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count == 1) ? true : false;
                                        if (nurEinLiedAktiv)
                                        {
                                            KlangZeilenLaufend[durchlauf]._mplayer.Position =
                                                (KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen) ?
                                                    TimeSpan.FromMilliseconds(Math.Round(KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilStart.Value, 0, MidpointRounding.ToEven)) :
                                                    TimeSpan.FromMilliseconds(0);
                                        }
                                        else
                                        {
                                            KlangZeilenLaufend[durchlauf]._mplayer.Stop();
                                            KlangZeilenLaufend[durchlauf]._mplayer.Close();
                                            KlangZeilenLaufend[durchlauf].istLaufend = false;
                                            KlangZeilenLaufend[durchlauf].istPause = false;
                                            KlangZeilenLaufend[durchlauf].istStandby = false;

                                            CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);

                                            KlangZeilenLaufend[durchlauf].istStandby = true;

                                            if (KlangZeilenLaufend[durchlauf].audioZeileVM != null)
                                                KlangZeilenLaufend[durchlauf].audioZeileVM.Progress = 0;
                                        }
                                    }
                                //Musik Endposition (incl vor Fading) überprüfen
                                if (_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik && !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                    KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)

                                    //Bei TeilAbspielen und Ende erreicht
                                    if ((KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen &&
                                         KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilEnde)
                                        ||
                                        (!KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen &&
                                        KlangZeilenLaufend[durchlauf]._mplayer.Position.TotalMilliseconds + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.TimeSpan.TotalMilliseconds)
                                        )
                                    {
                                        _GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));
                                        if (_GrpObjecte[posObjGruppe].Gespielt.Count > 50)
                                            _GrpObjecte[posObjGruppe].Gespielt.RemoveAt(0);

                                        if (!KlangZeilenLaufend[durchlauf].FadingOutStarted)
                                        {
                                            KlangZeilenLaufend[durchlauf].FadingOutStarted = true;
                                            FadingOut(KlangZeilenLaufend[durchlauf], _GrpObjecte[posObjGruppe], true, false);
                                        }

                                        KlangZeilenLaufend[durchlauf].istLaufend = false;
                                        KlangZeilenLaufend[durchlauf].istPause = false;

                                        bool nurEinLiedAktiv = (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count == 1) ? true : false;
                                        KlangZeilenLaufend[durchlauf].istStandby = nurEinLiedAktiv;

                                        CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);

                                        if (!nurEinLiedAktiv)
                                            KlangZeilenLaufend[durchlauf].istStandby = true;

                                        if (KlangZeilenLaufend[durchlauf].audioZeileVM != null)
                                            KlangZeilenLaufend[durchlauf].audioZeileVM.Progress = 0;
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



        public MediaPlayer PlayFile(bool notMusikPlayer, MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile, 
            GruppenObjekt grpobj, MediaPlayer _player, String url, double vol, bool fading, Nullable<double> posStart)
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
                    if (_player.Source == null || _player.Source.LocalPath.ToString() != url)
                    {
                        if (grpobj == null)
                            _player.SpeedRatio = klZeile != null ? klZeile.playspeed : 1;
                        _player.Open(new Uri(url));
                    };
                    
                    if (posStart != null)
                    {
                        _player.Position = TimeSpan.FromMilliseconds(0);
                        // Bis zu 1000ms warten um die Musikdatei auszulesen und die Laufzeit zu ermitteln
                        if (SpinWait.SpinUntil(() => { return _player.NaturalDuration.HasTimeSpan; }, 1000))
                            _player.Position = TimeSpan.FromMilliseconds(posStart.Value);
                    }
                    else
                        _player.Position = TimeSpan.FromMilliseconds(0);

                    if (fading)   // Musik-Playlist
                    {
                        _player.IsMuted = BGPlayerIsMuted;
                        _player.Volume = 0;
                        if (!notMusikPlayer)
                        {
                            BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = false;
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
                        _player.IsMuted = GeräuscheIsMuted;
                        
                        if (_timerFadingOut.IsEnabled || !grpobj.aPlaylist.Fading)
                            _player.Volume =
                                ((!notMusikPlayer || grpobj.visuell) ?
                                    vol / 100 :
                                        (grpobj.aPlaylist.DoForce) ?
                                        (double)grpobj.aPlaylist.ForceVolume /100 :                      
                                        (vol / 100) * (FadingGeräuscheVolProzent / 100));
                        else
                            if (grpobj.aPlaylist.Fading)
                                _player.Volume = 0;                                      //Fading In Geräusche Start

                        _player.Play();
                    }
                }
                catch (Exception ex2)
                {                    
                    SelectedMusikTitelItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));   // Brushes.Yellow
                    SelectedMusikTitelItem.ToolTip = "Datei konnte nicht geöffnet werden (Datei abspielbar / Codec installiert?)" + ex2;
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
                        if (((FadingOutGeräusche)_timerFadingOutGeräusche.Tag).gruppenOut.FirstOrDefault(t => t.grpobj == _GrpObjecte[posObjGruppe]) != null)
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

                    if (!_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik &&           // Direkt wieder anstarten wenn der Titel die einigste Möglichkeit ist
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.Pause == 0 &&
                        _GrpObjecte[posObjGruppe].aPlaylist.MaxSongsParallel == _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend).Count &&
                        _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istStandby).Count == 0)
                    {
                        _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Position = (!_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.TeilAbspielen) ?
                            TimeSpan.FromMilliseconds(0) : TimeSpan.FromMilliseconds(Math.Round(_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.TeilStart.Value, 0, MidpointRounding.ToEven));

                        if (_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.PauseChange)
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.Pause =
                                (new Random()).Next(_GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert,
                                    _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert);
                    }
                    else
                    {
                        _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Stop();
                        KlangPlayEndetimer = new DispatcherTimer();

                        if (_GrpObjecte[posObjGruppe].visuell)
                            //Im Editor keine Wartezeit abwarten
                            KlangPlayEndetimer.Interval = TimeSpan.FromMilliseconds(0);
                        else
                            //Neue Wartezeit fest oder per RANDOM bestimmen                        
                            KlangPlayEndetimer.Interval = (_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.PauseChange) ?
                                TimeSpan.FromMilliseconds(
                                    (new Random()).Next(_GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert,
                                    _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert)) :
                                TimeSpan.FromMilliseconds(_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.Pause);

                        _GrpObjecte[posObjGruppe]._listZeile[zeile].istWartezeit = true;
                        KlangPlayEndetimer.Tag = _GrpObjecte[posObjGruppe]._listZeile[zeile].ID_Zeile;
                        KlangPlayEndetimer.Tick += new EventHandler(KlangPlayEndetimer_Tick);
                        KlangPlayEndetimer.Start();
                        lstKlangPlayEndetimer.Add(KlangPlayEndetimer);

                        if (!_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.TeilAbspielen)
                            _GrpObjecte[posObjGruppe]._listZeile[zeile]._mplayer.Close();
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
                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt chkgrpobj in _GrpObjecte)
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

                    grpobj.NochZuSpielen.RemoveAll(t => t.Equals(grpobj._listZeile[zeile].aPlaylistTitel.Audio_TitelGUID));
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
                    if (grpobj._listZeile[zeile].audioZeileVM != null)
                        grpobj._listZeile[zeile].audioZeileVM.FilePlayable = false;

                    foreach (MusikZeile mZeile in ErwPlayerMusikListItemListe)
                    {
                        if ((Guid)mZeile.Tag == grpobj.aPlaylist.Audio_PlaylistGUID)
                        {
                            mZeile.spnlMusikZeile.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 80));   // Yellow
                            mZeile.spnlMusikZeile.ToolTip = grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.aPlaylistTitel.Aktiv).Count + " von " + grpobj._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count + " Titel abspielbar";
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

        void Player_MusikMediaFailed(object sender, ExceptionEventArgs e)
        {
            try
            {

                (sender as MediaPlayer).Stop();
                (sender as MediaPlayer).Close();
                (sender as MediaPlayer).MediaFailed -= Player_KlangMediaFailed;
                MusikProgBarTimer.Stop();
                if (SelectedMusikTitelItem != null)
                {
                    SelectedMusikTitelItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));      // Yellow
                    SelectedMusikTitelItem.ToolTip = "Datei kann nicht abgespielt werden. Ungültiger Name? Vermeiden Sie Sonderzeichen( #&'! ) im Zusammenhang mit Netzlaufwerken.";
                }
                SpieleNeuenMusikTitel(Guid.Empty);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswerten des Musikfehlers ist ein Fehler aufgetreten.", ex);
            }
        }


        public void _datenloeschen(int mrRes, bool allesloeschen, string saveTMPdatei)
        {
            hotkeyListe.Clear();
            if (mrRes == 1)
            {
                Global.SetIsBusy(true, string.Format("Bestehende Daten werden gesichert..." + Environment.NewLine + saveTMPdatei));
                //this.UpdateLayout();
                if (Global.ContextAudio.PlaylistListe.Count > 0)
                    Global.ContextAudio.PlaylistListe[0].Export(saveTMPdatei, Global.ContextAudio.PlaylistListe[0].Audio_PlaylistGUID);
            }
            if (mrRes == 1 || allesloeschen)
            {
                Global.SetIsBusy(true, string.Format("Laufende Songs werden beendet..."));
                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj in _GrpObjecte.FindAll(t => t.visuell))
                    AlleKlangSongsAus(grpobj, true, false, false, true);

                foreach (MusikZeile aZeile in ErwPlayerMusikListItemListe) aZeile.tbtnCheck.IsChecked = false;

                if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null)
                {
                    BGPlayer.NochZuSpielen.Clear();
                    BGPlayer.Gespielt.Clear();
                    BGPlayerGespieltCount = BGPlayer.Gespielt.Count;
                    BGPlayer.AktPlaylist = null;
                    BGPlayer.AktTitel.Clear();
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

            hotkeyListe = new List<btnHotkey>();
            BGPlayer = new MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.MusikView();
            _GrpObjecte = new List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt>();
            AktKlangPlaylist = null;
            AktKlangTheme = null;
            BGSongTimer.Close();
            foreach (DispatcherTimer dispTmr in lstKlangPlayEndetimer)
                if (dispTmr != null) dispTmr.Stop();
            lstKlangPlayEndetimer.Clear();

            KlangProgBarTimer.Stop();
            MusikProgBarTimer.Stop();
            BGPlayer.BG.Clear();
            BGPlayer.BG.Add(new Musik());
            BGPlayer.BG.Add(new Musik());

            setStdPfad();
            fadingTime = MeisterGeister.Logic.Einstellung.Einstellungen.Fading;
        }

        public void abspielProzess(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj, bool checkStatus, bool sollStandby, MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile, RoutedEventArgs e)
        {
            if (klZeile.aPlaylistTitel.Audio_Titel == null)
                return;
            Audio_Titel aTitel = AudioInAnderemPfadSuchen ? setTitelStdPfad(klZeile.aPlaylistTitel.Audio_Titel) : klZeile.aPlaylistTitel.Audio_Titel;
            if (aTitel.Pfad != klZeile.aPlaylistTitel.Audio_Titel.Pfad ||
                aTitel.Datei != klZeile.aPlaylistTitel.Audio_Titel.Datei)
            {
                klZeile.aPlaylistTitel.Audio_Titel = aTitel;
                Global.ContextAudio.Update<Audio_Titel>(klZeile.aPlaylistTitel.Audio_Titel);
            }
            try
            {
                if (e == null || e.Source == null)
                {
                    if (!Directory.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad) ||
                        !File.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + klZeile.aPlaylistTitel.Audio_Titel.Datei))
                    {
                        klZeile.playable = false;
                        klZeile.istLaufend = false;
                        if (klZeile.audioZeileVM != null) klZeile.audioZeileVM.FileNotExist = true;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.aPlaylistTitel.Audio_TitelGUID));
                    }
                    else
                    {
                        klZeile.playable = true;
                        if (klZeile.audioZeileVM != null) klZeile.audioZeileVM.FileNotExist = false;
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
                                if (grpObj.aPlaylist.Hintergrundmusik)
                                {
                                    klZeile.FadingOutStarted = false;
                                    FadingOut_Started = null;
                                }

                                klZeile._mplayer =
                                    PlayFile(true, klZeile,                                         
                                        grpObj,
                                        klZeile._mplayer,
                                        klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + klZeile.aPlaylistTitel.Audio_Titel.Datei,
                                        klZeile.Aktuell_Volume, grpObj.aPlaylist.Hintergrundmusik,
                                        klZeile.aPlaylistTitel.TeilAbspielen? klZeile.aPlaylistTitel.TeilStart: null);

                                if (klZeile._mplayer != null)
                                    klZeile.mediaHashCode = klZeile._mplayer.GetHashCode();
                                
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
                            if (klZeile._mplayer != null && klZeile._mplayer.Source != null)
                            {
                                if (grpObj.aPlaylist.Hintergrundmusik)
                                {
                                    if (!klZeile.FadingOutStarted)
                                    {
                                        klZeile.FadingOutStarted = true;
                                        FadingOut(klZeile, grpObj, true, true);
                                    }
                                }
                                else
                                {
                                    klZeile._mplayer.Stop();
                                    if (!klZeile.aPlaylistTitel.TeilAbspielen)
                                        klZeile._mplayer.Close();
                                }
                                klZeile.istStandby = false;
                                klZeile.istLaufend = false;
                                klZeile.istPause = false;
                            }
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
                    klZeile.aPlaylistTitel.Aktiv = checkStatus;
                    klZeile.istStandby = checkStatus;
                    if (checkStatus)
                    {
                        klZeile.istLaufend = false;
                        if (!grpObj.NochZuSpielen.Contains(klZeile.aPlaylistTitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= klZeile.aPlaylistTitel.Rating; i++)
                                grpObj.NochZuSpielen.Add(klZeile.aPlaylistTitel.Audio_TitelGUID);
                        }
                    }
                    else
                    {
                        if (klZeile._mplayer != null)
                        {
                            klZeile._mplayer.Stop();
                            if (!klZeile.aPlaylistTitel.TeilAbspielen)
                                klZeile._mplayer.Close();
                        }
                        klZeile.istLaufend = false;
                        klZeile.istPause = false;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.aPlaylistTitel.Audio_TitelGUID));
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Der AbspielProzess konnte nicht ordnungsgemäß durchgeführt werden.", ex);
            }
        }

        public void chkTitel(AudioZeileVM audioZeileVM)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte.FindAll(t => t.visuell))
                {
                    if (chkgrpObj._listZeile.FirstOrDefault(t => t.audioZeileVM == audioZeileVM) != null) // .FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeileVM.chkTitel == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;

                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeileVM == audioZeileVM)); //.chkTitel == (Control)sender));
                
                if (!audioZeileVM.Checked && grpobj._listZeile[zeile].istLaufend)
                {
                    grpobj._listZeile[zeile]._mplayer.Pause();
                    grpobj._listZeile[zeile]._mplayer.Position = TimeSpan.FromMilliseconds(0);
                    grpobj._listZeile[zeile].istLaufend = false;
                }

                if ((grpobj.visuell && grpobj.wirdAbgespielt || !grpobj.visuell) ||
                    (grpobj.visuell && !grpobj.wirdAbgespielt && grpobj.tbtnKlangPause != null &&  grpobj.tbtnKlangPause.IsChecked.Value))
                    abspielProzess(grpobj, audioZeileVM.Checked, grpobj.wirdAbgespielt, grpobj._listZeile[zeile], null); //e

                if (grpobj.aPlaylist != null)
                {
                    Audio_Playlist_Titel playlisttitel =
                        grpobj.aPlaylist.Audio_Playlist_Titel.Where(t => t.Audio_TitelGUID ==
                            grpobj._listZeile[zeile].aPlaylistTitel.Audio_TitelGUID).FirstOrDefault(
                            t => t.Aktiv != audioZeileVM.Checked);

                    if (playlisttitel != null)
                        playlisttitel.Aktiv = audioZeileVM.Checked;

                    //wenn von Hand geändert, change-Bit auf alle versteckten Playlists mit dem selben GUID setzen   
                    SetzeChangeBit(grpobj.aPlaylist);
                }
                if (grpobj.visuell && grpobj.wirdAbgespielt) CheckPlayStandbySongs(grpobj);
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

        
        public void CheckAlleAngehakt(GruppenObjekt grpobj)
        {
            grpobj.chkbxTopAktiv.IsChecked = (grpobj._listZeile.Count != 0 &&
                grpobj._listZeile.Count == grpobj._listZeile.FindAll(t => t.audioZeileVM != null).Count(t => t.audioZeileVM.Checked == true)) ?
                true : false;

            grpobj.chkbxTopVolChange.IsChecked = (grpobj._listZeile.Count != 0 &&               // .chkTitel.IsChecked 
                 grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FindAll(t => t.audioZeileVM.Checked == true).Count ==
                 grpobj.anzVolChange) ? true : false;

            grpobj.chkbxTopPauseChange.IsChecked = (grpobj._listZeile.Count != 0 &&
                grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FindAll(t => t.audioZeileVM.Checked == true).Count ==
                grpobj.anzPauseChange) ? true : false;
        }

        public void CheckPlayStandbySongs(GruppenObjekt grpobj)
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
                                    if (klZeilenStandbyNichtPause[x].audioZeileVM == null &&
                                        klZeilenStandbyNichtPause[x].istStandby &&
                                        klZeilenStandbyNichtPause[x].aPlaylistTitel.Aktiv)
                                    {
                                        for (int t = 0; t <= klZeilenStandbyNichtPause[x].aPlaylistTitel.Rating; t++)
                                            grpobj.NochZuSpielen.Add(klZeilenStandbyNichtPause[x].aPlaylistTitel.Audio_TitelGUID);
                                    }
                                }
                            }

                            if (grpobj.aPlaylist.Hintergrundmusik)
                            {
                                if (grpobj.NochZuSpielen.Count > 0)
                                {
                                    int neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                    Guid zuspielendeGuid = grpobj.NochZuSpielen[neuPos];
                                    int posZeile = grpobj._listZeile.FindIndex(t => t.aPlaylistTitel.Audio_TitelGUID == zuspielendeGuid);
                                    grpobj._listZeile[posZeile].istStandby = false;

                                    // Titel anstarten
                                    if (!grpobj.visuell)
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].aPlaylistTitel.Aktiv, grpobj.wirdAbgespielt, grpobj._listZeile[posZeile], null);
                                    else
                                    {
                                        if (grpobj._listZeile[posZeile].audioZeileVM != null)
                                            chkTitel(grpobj._listZeile[posZeile].audioZeileVM);
                                        else
                                            grpobj._listZeile[posZeile].istStandby = true;
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
                                        int loops = 0;
                                        while (!grpobj._listZeile.First(t => t.aPlaylistTitel.Audio_TitelGUID == grpobj.NochZuSpielen[neuPos]).istStandby &&
                                               (grpobj._listZeile.FindAll(t => t.istStandby).Count != 0) && 
                                               loops < 5 * grpobj.NochZuSpielen.Count)                         // sicher gehen, dass kein unendlich-Loop entsteht
                                        {
                                            neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                            loops++;
                                        }
                                    }

                                    Guid zuspielendeGuid = grpobj.NochZuSpielen[neuPos];
                                    int posZeile = grpobj._listZeile.FindIndex(t => t.aPlaylistTitel.Audio_TitelGUID == zuspielendeGuid);

                                    grpobj._listZeile[posZeile].istStandby = false;
                                    if (!grpobj.visuell)
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].aPlaylistTitel.Aktiv, grpobj.wirdAbgespielt, grpobj._listZeile[posZeile], null);
                                    else
                                    {
                                        // Titel anstarten
                                        if (grpobj._listZeile[posZeile].audioZeileVM != null)
                                            chkTitel(grpobj._listZeile[posZeile].audioZeileVM);
                                        else
                                            grpobj._listZeile[posZeile].istStandby = true;
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

                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt _chkGrpObj in _GrpObjecte)
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
                        grpobj._listZeile[zeile].audioZeileVM.Progress = 0; // .pbarTitel.Value = 0;
                        if (grpobj._listZeile[zeile].aPlaylistTitel.PauseChange)
                        {
                            if (Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMin) > Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMax))
                                grpobj._listZeile[zeile].aPlaylistTitel.PauseMin = grpobj._listZeile[zeile].aPlaylistTitel.PauseMax;
                            neu = (new Random()).Next(Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMin),
                                                      Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMax));
                            grpobj._listZeile[zeile].aPlaylistTitel.Pause = neu;
                        }
                    }

                    // Song aus der Liste der laufenden Songs herausnehmen
                    grpobj._listZeile[zeile].istLaufend = false;

                    // Song in die Liste der Standby-Songs aufnehmen wenn nur ein Song in Liste
                    if (grpobj._listZeile.FindAll(t => t.istStandby).Count == 0)
                    {
                        grpobj._listZeile[zeile].istStandby = true;
                        CheckPlayStandbySongs(grpobj);
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
        
        public void AlleKlangSongsAus(GruppenObjekt grpobj, bool erwPlayerAus , bool checkboxAus, bool ZeileLoeschen, bool sofortAus)
        {
            if (erwPlayerAus)
                ErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile mZeile)
                {
                    if (mZeile.tbtnCheck.IsChecked.Value)
                    {
                        mZeile.tbtnCheck.IsChecked = false;
                        mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                    }
                });

            if (grpobj == null || !grpobj.wirdAbgespielt)
                return;

            List<KlangZeile> laufendeKZeilen = grpobj._listZeile.FindAll(t => t.istLaufend == true).ToList();
            
            laufendeKZeilen.ForEach(delegate(KlangZeile kZeile)
            {
                if (kZeile._mplayer != null)
                {

                    if (!grpobj.aPlaylist.Hintergrundmusik || sofortAus)
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
                            FadingOut(kZeile, grpobj, true, true);
                        }
                    }
                }
                kZeile.istLaufend = false;
                kZeile.istPause = false;

                kZeile.audioZeileVM.TitelMaximum = 100;
                kZeile.audioZeileVM.TitelMinimum = 0;

                if (ZeileLoeschen)
                {
                    grpobj.lbEditorListe.Items.Clear();
                    grpobj._listZeile.Clear();
                }
            });
            grpobj.wirdAbgespielt = false;

            GC.GetTotalMemory(true);                //GC update (Memory wird aktualisiert)
        }

        public void BGStoppen()
        {
            try
            {
                if (!BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
                {
                    BGmPlayerIsPaused = false;
                    //BGPlayer.BG[BGPlayeraktiv].isPaused = false;
                    if (BGPlayer.BG[BGPlayeraktiv].mPlayer != null &&
                        !BGPlayer.BG[BGPlayeraktiv].FadingOutStarted)
                    {
                        BGPlayer.BG[BGPlayeraktiv].FadingOutStarted = true;
                        BGFadingOut(BGPlayer.BG[BGPlayeraktiv], true, true);
                    }
                }
                MusikProgBarTimer.Stop();
                BGmPlayerIsPaused = true;
                BGPlayer.BG[BGPlayeraktiv].aPlaylist = null;
                BGPlayeraktiv = (BGPlayeraktiv == 0) ? 1 : 0;
                RenewMusikNochZuSpielen();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Stop-Buttons ist ein Fehler aufgetreten.", ex);
            }
        }

        public void PlaylisteLeeren(GruppenObjekt grpobj)
        {
            if (grpobj == null)
                return;

            if (AktKlangPlaylist != null)
            {
                if (BGPlayer != null && BGPlayer.AktPlaylist == AktKlangPlaylist && grpobj.wirdAbgespielt)
                    BGStoppen();

                AlleKlangSongsAus(grpobj, true, true, true, true);
                grpobj.wirdAbgespielt = false;
            }
            if (grpobj._listZeile.Count > 0)
            {
                grpobj.lbEditorListe.Items.Clear();
                grpobj._listZeile.RemoveRange(0, grpobj._listZeile.Count);
            }

            grpobj.anzVolChange = 0;
            grpobj.anzPauseChange = 0;
            grpobj.NochZuSpielen.Clear();
            grpobj.Gespielt.Clear();
        }

        #region *** hotkeyListe ***

        private void addHotkey(int i)
        {
            btnHotkey hkey = new btnHotkey();
            hkey.VM.AudioVM = this;
            hkey.VM.taste = i;
            hkey.VM.aPlaylistGuid = Guid.Empty;

            hotkeyListe.Add(hkey);
        }

        public void AktualisiereHotKeys()
        {
            hotkeyListe.RemoveRange(0, hotkeyListe.Count);
            for (int i = 48; i <= 57; i++)
                addHotkey(i);

            for (int i = 65; i < 91; i++)
                addHotkey(i);

            //UpdateHotkeys();
        }

        

        #endregion


        public void sortPlaylist(Audio_Playlist aPlaylist, int abPos)
        {
            int reihe = abPos < 0 ? 0 : abPos;
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
        public void sortPlaylist(List<lbEditorItemVM> listLbi, int abPos)
        {
            int reihe = abPos < 0 ? 0 : abPos;
            foreach (lbEditorItemVM lbi in listLbi.Where(t => t.APlaylist.Reihenfolge >= abPos).OrderBy(t => t.APlaylist.Reihenfolge))
            {
                if (lbi.APlaylist.Reihenfolge != reihe)
                {
                    lbi.APlaylist.Reihenfolge = reihe;
                    Global.ContextAudio.Update<Audio_Playlist>(lbi.APlaylist);
                }
                reihe++;
            }
        }

        public bool KlangDateiHinzu(string pfad_datei, AudioZeile aZeile, AudioZeileVM aZeileVM, Audio_Playlist aPlaylist, int position)
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
                        temp_pfad_datei[1] = pfad_max.Substring(pfad.EndsWith(@"\") ? pfad.Length : pfad.Length + 1) + "\\" + temp_pfad_datei[1];
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

            if ((aZeile != null && aZeile.chkTitel.ContextMenu != null && aZeile.chkTitel.ContextMenu.IsVisible) || aZeileVM != null)
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

                Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(aPlaylist, titel).Last();
                if (playlisttitel != null)
                {
                    //AudioZeile per Drag&Drop hinzugefügt
                    if (aZeile != null || aZeileVM != null)
                    {
                        if (aZeile != null)
                            aZeile.rsldTeilSong.PlayTitel = playlisttitel;

                        Audio_Playlist_Titel aplaytitelDnD =
                            aZeile != null ? (Audio_Playlist_Titel)(aZeile.Tag) :
                            aZeileVM.aPlayTitel;
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
                        playlisttitel.Reihenfolge = playlisttitel.Audio_Playlist.Audio_Playlist_Titel.Count - 1;
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
                    sortPlaylist(playlisttitel.Audio_Playlist, -1);
                    Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel);
                }
            }
            return titelNeuHinzugefügt;
        }


        public void UpdatePlaylistLänge(Audio_Playlist aPlaylist)
        {
            double gesamt = 0;
            foreach (Audio_Playlist_Titel aPlyTitel in aPlaylist.Audio_Playlist_Titel)
                gesamt += aPlyTitel.Audio_Titel.Länge != null ? aPlyTitel.Audio_Titel.Länge.Value : 0;

            if (aPlaylist.Länge != gesamt)
            {
                aPlaylist.Länge = gesamt;
                Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);                
            }
        }

        public void KlangNewRow(GruppenObjekt grpobj, Audio_Playlist_Titel playlisttitel)
        {
            string songdatei = playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;
            if (grpobj == null)
                return;
            int objGruppe = grpobj.objGruppe;


            KlangZeile klZeile = new KlangZeile(rowErstellt);
            
            klZeile.aPlaylistTitel = playlisttitel;
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
                klZeile.audioZeileVM = LbEditorAudioZeilenListe.First(t => t.aPlayTitel == playlisttitel);
                klZeile.audioZeileVM.grpobj = grpobj;
                if (AudioInAnderemPfadSuchen &&
                    !File.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + (klZeile.aPlaylistTitel.Audio_Titel.Datei == null ? "" : klZeile.aPlaylistTitel.Audio_Titel.Datei)))
                {
                    klZeile.aPlaylistTitel.Audio_Titel = setTitelStdPfad(klZeile.aPlaylistTitel.Audio_Titel);
                    if (File.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + klZeile.aPlaylistTitel.Audio_Titel.Datei))
                        Global.ContextAudio.Update<Audio_Titel>(klZeile.aPlaylistTitel.Audio_Titel);
                }
            }

            klZeile.pauseMin_wert = Convert.ToInt32(playlisttitel.PauseMin);
            klZeile.pauseMax_wert = Convert.ToInt32(playlisttitel.PauseMax);
            klZeile.volMin_wert = Convert.ToInt32(playlisttitel.VolumeMin);
            klZeile.volMax_wert = (Convert.ToInt32(playlisttitel.VolumeMax) >= klZeile.volMin_wert) ?
                Convert.ToInt16(playlisttitel.VolumeMax) : klZeile.volMin_wert;
            klZeile.Aktuell_Volume = playlisttitel.Volume;
            klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

            klZeile.playspeed = playlisttitel.Speed;

            if (playlisttitel.Aktiv && !grpobj.aPlaylist.Hintergrundmusik)
                klZeile.istStandby = true;
            else
                klZeile.istStandby = false;

            if (!grpobj.wirdAbgespielt)
                klZeile.istPause = true;

            klZeile.playable = playlisttitel.Aktiv;

            grpobj._listZeile.Add(klZeile);
            if (playlisttitel.VolumeChange) grpobj.anzVolChange++;
            if (playlisttitel.PauseChange) grpobj.anzPauseChange++;
            rowErstellt++;
        }
        
        public void setStdPfad()
        {
            char[] charsToTrim = { '\\' };
            if (stdPfad.Count > 0) stdPfad.RemoveRange(0, stdPfad.Count);
            stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
        }

        public void UpdateHotkeyUsed()
        {
            if (Global.ContextAudio.PlaylistListe == null) return;
            List<btnHotkey> lstHotKeyUsed = new List<btnHotkey>();

            foreach(Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null).OrderBy(tt => tt.Key))
            {
                btnHotkey hkey = new btnHotkey();
                hkey.VM.AudioVM = this;
                hkey.VM.aPlaylistGuid = aPlaylist.Audio_PlaylistGUID;
                hkey.VM.taste = (char)aPlaylist.Key[0];
                hkey.VM.aPlaylist = aPlaylist;
                hkey.VM.volume = HotkeyVolume;
                lstHotKeyUsed.Add(hkey);
            };

            hotkeyListUsed = lstHotKeyUsed;
            IsAuswahlHotkey = false;
        }


        public void CheckBtnGleicherPfad(Audio_Playlist aPlaylist)
        {
            List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(aPlaylist);
            if (titelliste.Count > 0 && 
                System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(titelliste[0].Pfad + "\\" + titelliste[0].Datei)))
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
                _chkAnzDateienInDir(aPlaylist);
            }
        }

        public void _chkAnzDateienInDir(Audio_Playlist aPlaylist)
        {
            if (!Global.IsInitialized)
                return;
            ChkAnzDateienVerfügbar = false;
            if (_chkAnzDateien._bkworker.IsBusy)
            {
                _chkAnzDateien._bkworker.CancelAsync();
                _chkAnzDateien._bkworker.Dispose();
            }
            _chkAnzDateien.aPlaylist = aPlaylist;
            _chkAnzDateien._bkworker = new BackgroundWorker();
            _chkAnzDateien._bkworker.WorkerReportsProgress = true;
            _chkAnzDateien._bkworker.WorkerSupportsCancellation = true;
            _chkAnzDateien._bkworker.DoWork += new DoWorkEventHandler(_bkworkerCHKAnzDateien_DoWork);
            _chkAnzDateien._bkworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkworkerCHKAnzDateien_RunWorkerCompleted);
            _chkAnzDateien._bkworker.RunWorkerAsync();
        }

        public void _bkworkerCHKAnzDateien_DoWork(object sender, DoWorkEventArgs e)
        {
            _chkAnzDateien.titelliste = Global.ContextAudio.LoadTitelByPlaylist(_chkAnzDateien.aPlaylist);

            if (_chkAnzDateien.titelliste != null && _chkAnzDateien.titelliste.Count > 0)
            {
                _chkAnzDateien.allFilesMP3.Clear();
                _chkAnzDateien.allFilesWAV.Clear();
                _chkAnzDateien.allFilesOGG.Clear();
                _chkAnzDateien.allFilesWMA.Clear();

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

                        if (_chkAnzDateien.aPlaylist != _aktKlangPlaylist) return;
                    }
                }
            }
        }

        public void _bkworkerCHKAnzDateien_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ChkAnzDateienVerfügbar = true;
                if (e.Error == null)
                {
                    int all = _chkAnzDateien.allFilesMP3.Count + _chkAnzDateien.allFilesOGG.Count +
                              _chkAnzDateien.allFilesWAV.Count + _chkAnzDateien.allFilesWMA.Count;
                    if (_chkAnzDateien.aPlaylist == AktKlangPlaylist &&
                        System.IO.Path.IsPathRooted(_chkAnzDateien.titelRef) &&
                        Directory.Exists(_chkAnzDateien.titelRef) &&
                        all > 0)
                    {
                        ChkAnzDateienResult =
                            _chkAnzDateien.titelRef + Environment.NewLine + "Update der Titel im o.g. Verzeichnis" + Environment.NewLine +
                            _chkAnzDateien.titelliste.Count + " Dateien sind in der Playlist vorhanden." + Environment.NewLine +
                            all + " neue Sound-Dateien wurden incl. den Unterverzeichnisse gefunden" + Environment.NewLine + Environment.NewLine +
                            _chkAnzDateien.allFilesMP3.Count + " neue MP3-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesOGG.Count + " neue OGG-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesWAV.Count + " neue WAV-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesWMA.Count + " neue WMA-Dateien gefunden." + Environment.NewLine + Environment.NewLine +
                            "Klicken um alle Dateien neu gefundenen Dateien zu integrieren.";
                    }
                    else
                        ChkAnzDateienResult = null;
                }
                else
                    ChkAnzDateienResult = null;
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                ChkAnzDateienVerfügbar = true;
                (sender as BackgroundWorker).Dispose();
                ChkAnzDateienResult = null;
            }
        }

        private Base.CommandBase _oncbAllTitelChecked = null;
        public Base.CommandBase OncbAllTitelChecked
        {
            get
            {
                if (_oncbAllTitelChecked == null)
                    _oncbAllTitelChecked = new Base.CommandBase(cbAllTitelChecked, null);
                return _oncbAllTitelChecked;
            }
        }
        void cbAllTitelChecked(object obj)
        {
            bool ziel = !AllTitelAktiv;
            foreach (Audio_Playlist_Titel aPlaylistTitel in AktKlangPlaylist.Audio_Playlist_Titel)
                aPlaylistTitel.Aktiv = ziel;
            OnChanged("AllTitelAktiv");
        }


        private Base.CommandBase _onPlaylistImportieren = null;
        public Base.CommandBase OnPlaylistImportieren
        {
            get
            {
                if (_onPlaylistImportieren == null)
                    _onPlaylistImportieren = new Base.CommandBase(PlaylistImportieren, null);
                return _onPlaylistImportieren;
            }
        }
        void PlaylistImportieren(object obj)
        {
            try
            {
                List<string> dateien;
                dateien = ViewHelper.ChooseFiles("Playlist(en) importieren", "", true, new string[3] { "xml", "wpl", "m3u8" });
                if (dateien != null)
                {
                    PlaylistenImportieren(dateien);
                    BGStoppen();

                    EditorListBoxItemListe = lbiPlaylistListNeuErstellen();
                    MusikListItemListe = mZeileEditorMusikNeuErstellen();
                    ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
                    ErwPlayerGeräuscheListItemListe = mZeileErwPlayerGeräuscheNeuErstellen();                    
                    FilterEditorPlaylistListe();
                    FilterMusikPlaylistListe();
                    FilterErwPlayerMusikPlaylistListe();
                    FilterErwPlayerGeräuschePlaylistListe();
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
            }
        }


        public void _DateienAufnehmen(List<string> files, AudioZeile aZeile, AudioZeileVM aZeileVM, Audio_Playlist aPlaylist, int position, bool jetztUpdaten)
        {
            string[] extension = new String[4] { ".mp3", ".wav", ".wma", ".ogg" };
            bool hinzugefuegt = false;

            if (aPlaylist == null)
                aPlaylist = NeueKlangPlaylistInDB(AktKlangPlaylist.Name);

            foreach (string dateihinzu in files)
            {
                if (Array.IndexOf(extension, dateihinzu.ToLower().Substring(dateihinzu.Length - 4)) != -1)
                {
                    KlangDateiHinzu(dateihinzu, aZeile, aZeileVM, aPlaylist, position);
                    hinzugefuegt = true;
                }
                else
                {
                    // Winamp-Datei
                    if (dateihinzu.ToLower().EndsWith(".m3u8"))
                    {
                        _DateienAufnehmen(getWinampFilesFromPlaylist(dateihinzu), null, null, aPlaylist, 0, true);
                        hinzugefuegt = true;
                    }
                    else
                    {
                        if (dateihinzu.ToLower().EndsWith(".wpl"))
                        {
                            _DateienAufnehmen(getMPlayerFilesFromPlaylist(dateihinzu), null, null, aPlaylist, 0, true);
                            hinzugefuegt = true;
                        }
                    }
                }
            }
            if (hinzugefuegt)
            {
                if (aPlaylist == AktKlangPlaylist)
                {
                    if (SelectedEditorItem == null || SelectedEditorItem.APlaylist != aPlaylist)
                        SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == aPlaylist);
                    else
                        AktKlangPlaylist = AktKlangPlaylist;
                }
                else
                    if (jetztUpdaten)
                        Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);
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
                        else
                            if (!line.StartsWith("#"))
                            {
                                string s = System.IO.Path.GetDirectoryName(filename);
                                lstFiles.Add(s.EndsWith("\\") ? s + line : s + "\\" + line);
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
        
        private Audio_Playlist NeueKlangPlaylistInDB(string playlistname)
        {
            Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
            playlist.MaxSongsParallel = 1;
            playlist.Name = playlistname;
            playlist.Hintergrundmusik = false;

            //zur datenbank hinzufügen
            if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                AktKlangPlaylist = playlist;

            return playlist;     
        }


        private Base.CommandBase _onTopKlangOpen = null;
        public Base.CommandBase OnTopKlangOpen
        {
            get
            {
                if (_onTopKlangOpen == null)
                    _onTopKlangOpen = new Base.CommandBase(TopKlangOpen, null);
                return _onTopKlangOpen;
            }
        }
        void TopKlangOpen(object obj)
        {
            try
            {
                string bezugsDir = stdPfad[0];
                if (AktKlangPlaylist != null)
                {
                    List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
                    if (titelliste.Count > 0)
                    {
                        if (AudioInAnderemPfadSuchen) 
                            titelliste[0] = setTitelStdPfad(titelliste[0]);

                        bezugsDir = (titelliste[0].Pfad + @"\" + titelliste[0].Datei).LastIndexOf(@"\") != -1 ?
                            (titelliste[0].Pfad + @"\" + titelliste[0].Datei).Substring(0, (titelliste[0].Pfad + @"\" + titelliste[0].Datei).LastIndexOf(@"\")) :
                            titelliste[0].Pfad + @"\" + titelliste[0].Datei;
                                                
                        titelliste.ForEach(delegate(Audio_Titel aTitel)
                        {
                            string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ?
                                System.IO.Path.GetDirectoryName(stdPfad[0] + @"\" + aTitel.Pfad + @"\" + aTitel.Datei) :
                                System.IO.Path.GetDirectoryName(aTitel.Pfad + @"\" + aTitel.Datei);

                            while (!vergleich.StartsWith(bezugsDir) &&
                                !bezugsDir.StartsWith(vergleich))
                            {
                                if (bezugsDir.Contains(@"\"))
                                    bezugsDir = bezugsDir.Substring(0, bezugsDir.LastIndexOf(@"\"));
                                else break;
                            }
                        });
                    }
                }
                string s = Environment.CurrentDirectory;
                if (Directory.Exists(bezugsDir))
                    Environment.CurrentDirectory = bezugsDir != "" ? bezugsDir : s;
                List<string> files = ViewHelper.ChooseFiles("Musiktitel auswählen", "", true, validExt);
                Environment.CurrentDirectory = s;

                // Öffnen bestätigt
                if (files != null && files.Count > 0)
                {
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        _DateienAufnehmen(files, null, null, AktKlangPlaylist, 0, false);
                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                        SelectedEditorItem = SelectedEditorItem;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Beim Einfügen der Datei(en) ist ein Fehler aufgetreten.", ex);
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

                pfaddatei = validateString(pfaddatei);

                ExportTheme(aTheme, pfaddatei);
            }

            Global.SetIsBusy(true, string.Format("Theme Export beendet ..."));
        }

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

        private void DeleteAll(int mrRes)
        {
            _datenloeschen(mrRes, true, "");

            Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
            Global.ContextAudio.UpdateList<Audio_Titel>();
            Global.ContextAudio.UpdateList<Audio_Playlist>();
            Global.ContextAudio.UpdateList<Audio_Playlist_Titel>();
            Global.ContextAudio.UpdateList<Audio_Theme>();
            Global.ContextAudio.UpdateList<Audio_Theme_Playlist>();

            Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
            AktualisiereHotKeys();
            Global.SetIsBusy(false);

            Init();
        }

        private void ThemeImportieren(List<string> dateien)
        {
            bool _nicht_first = false;
            bool allePlaylistenÜberspringen = false;
            bool allePlaylistenImportieren = false;
            bool einePlaylisteImportieren = false;
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
                            while (textReader.NodeType != XmlNodeType.EndElement &&
                                   (textReader.NodeType == XmlNodeType.Element &&
                                    textReader.Name.StartsWith("Playlist") || 
                                    textReader.Name.StartsWith("Theme")))
                            {
                                XmlNode node = doc.ReadNode(textReader);
                                if (node.Attributes.Count > 0 && node.Attributes["Name"] != null &&
                                    (node.Attributes["Audio_PlaylistGUID"] != null || node.Attributes["Audio_ThemeGUID"] != null))
                                {
                                    if (node.Name.StartsWith("Playlist"))
                                    {
                                        // Playlisten einlesen
                                        aPlayListsName.Add(node.Attributes["Name"].Value);

                                        // Suche nach der Playlistendatei in gleichen Ordner und hinzufügen
                                        List<string> playlist_file = new List<string>();

                                        string dir = System.IO.Path.GetDirectoryName(pfad);
                                        DirectoryInfo d = new DirectoryInfo(dir);

                                        foreach (FileInfo f in d.GetFiles("*.xml"))
                                        {
                                            XmlTextReader textReaderPlayList = new XmlTextReader(f.FullName);
                                            textReaderPlayList.Read();
                                            while (textReaderPlayList.NodeType == XmlNodeType.XmlDeclaration ||
                                                (textReaderPlayList.NodeType == XmlNodeType.Element && textReader.Name == "Audio_Theme"))
                                                textReaderPlayList.Read();
                                            if (textReaderPlayList.Name == "Audio_Playlist")
                                            {
                                                while ( textReaderPlayList.Name != "Audio_PlaylistGUID")                                                
                                                    textReaderPlayList.Read();
                                                if (textReaderPlayList.NodeType == XmlNodeType.EndElement)
                                                    break;
                                                textReaderPlayList.Read();
                                                if (textReaderPlayList.NodeType == XmlNodeType.EndElement)
                                                    break;
                                                if (node.Attributes["Audio_PlaylistGUID"].Value == textReaderPlayList.Value)
                                                {
                                                    playlist_file.Add(f.FullName);
                                                    textReaderPlayList.Close();
                                                    break;
                                                }
                                                else
                                                {
                                                    textReaderPlayList.Close();
                                                    continue;
                                                }
                                            }                                            
                                        }

                                        aPlayListsGuid.Add(node.Attributes["Audio_PlaylistGUID"].Value);
                                        
                                        if (allePlaylistenÜberspringen)
                                            continue;
                                        //Abfrage Vorgehensweise beim Laden der Playlisten
                                        if (playlist_file.Count > 0 && !allePlaylistenImportieren)
                                        {
                                            int fragePlaylistüberschrieben = ViewHelper.ConfirmYesNoCancel("Enthaltene Playliste gefunden", 
                                                "Das Theme enthält Informationen zu einer Playliste die auch in dem Verzeichnis gefunden wurde." + 
                                                Environment.NewLine + Environment.NewLine +
                                                "Soll die Playliste des Themes ebenfalls geladen werden?" + 
                                                Environment.NewLine + Environment.NewLine + 
                                                "ACHTUNG!  Evtl. werden existierende Playlisten überschrieben.");
                                            
                                            if (fragePlaylistüberschrieben == 2)
                                            {
                                                int frageFürAlle = ViewHelper.ConfirmYesNoCancel("Alle Playlisten laden/überschreiben", 
                                                    "Soll dieser Vorgang für alle enthaltenen Playlisten gelten" + Environment.NewLine);
                                                if (frageFürAlle == 2) allePlaylistenImportieren = true;
                                                else
                                                    if (frageFürAlle == 1) einePlaylisteImportieren = true;
                                                    else
                                                        if (frageFürAlle == 0) break;
                                            } else
                                                if (fragePlaylistüberschrieben == 1)
                                                {
                                                    int frageFürAlle = ViewHelper.ConfirmYesNoCancel("Alle Playlisten überspringen",
                                                        "Soll dieser Vorgang für alle enthaltenen Playlisten gelten" + Environment.NewLine);
                                                    if (frageFürAlle == 2) allePlaylistenÜberspringen = true;
                                                    else
                                                        if (frageFürAlle == 1) allePlaylistenÜberspringen = false;
                                                        else
                                                            if (frageFürAlle == 0) break;
                                                }
                                        }

                                        if (!allePlaylistenÜberspringen && playlist_file.Count > 0 && 
                                            (allePlaylistenImportieren || einePlaylisteImportieren))                                             
                                            PlaylistenImportieren(playlist_file);

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
                                    AktKlangTheme = NeuesKlangThemeInDB(thName);

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
            UpdateAlleListen();
        }

        private void PlaylistenImportieren(List<string> dateien)
        {
            try
            {
                bool _nicht_first = false;

                foreach (string pfad in dateien)
                {
                    Global.SetIsBusy(true, string.Format("Neue Playlist  '" + System.IO.Path.GetFileNameWithoutExtension(pfad) + "'  wird importiert ..."));
                    (App.Current.MainWindow as View.MainView).UpdateLayout();

                    if (pfad.EndsWith(".xml"))
                    {
                        if (AktKlangPlaylist == null) AktKlangPlaylist = new Audio_Playlist();
                        if (Audio_Playlist.Import(pfad, "Audio_Playlist", _nicht_first) != null)
                            AktKlangPlaylist = Global.ContextAudio.Liste<Audio_Playlist>()[0];
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(pfad);
                        AktKlangPlaylist = NeueKlangPlaylistInDB(fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length));

                        _DateienAufnehmen(new List<string>() { pfad }, null, null, AktKlangPlaylist, 0, false);
                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    }
                    Global.ContextAudio.Save();
                    _nicht_first = true;
                }
                Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));

                AktualisiereHotKeys();
                if (AktKlangPlaylist != null)
                    SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
                
                Global.SetIsBusy(false);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
                AktualisiereHotKeys();
            }
        }

        
        public void GetTotalLength(Audio_Playlist aPlaylist, bool busyWindow)
        {
            BerechneSpieldauer = false;
            if (!AudioSpieldauerBerechnen || aPlaylist == null)
                return;
            if (aPlaylist.Länge == 0)
            {
                double gesLänge = 0;
                for (int i = 0; i < aPlaylist.Audio_Playlist_Titel.Count; i++)
                    if (aPlaylist.Audio_Playlist_Titel.ElementAt<Audio_Playlist_Titel>(i).Audio_Titel.Länge.HasValue)
                        gesLänge += aPlaylist.Audio_Playlist_Titel.ElementAt(i).Audio_Titel.Länge.Value;
            }
            BerechneSpieldauer = true;

            if (busyWindow)
                Global.SetIsBusy(true, string.Format("Länge der Titel wird überarbeitet..."));

            //if (!workerGetLength.IsBusy)
            //    workerGetLength.RunWorkerAsync(aPlaylist);

            BackgroundWorker workerGetLength = new BackgroundWorker();
            workerGetLength.WorkerReportsProgress = true;
            workerGetLength.WorkerSupportsCancellation = true;
            workerGetLength.DoWork += new DoWorkEventHandler(workerGetLength_DoWork);
            workerGetLength.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerGetLength_RunWorkerCompleted);

            workerGetLength.RunWorkerAsync(aPlaylist);
        }

        private void workerGetLength_DoWork(object sender, DoWorkEventArgs args)
        {
            MediaPlayer mp = new MediaPlayer();
            TimeSpan totalLength = TimeSpan.FromMilliseconds(0);

            try
            {
                Audio_Playlist plylst = (Audio_Playlist)args.Argument;

                totalLength = TimeSpan.FromMilliseconds(0);
                plylst = (Audio_Playlist)args.Argument;
                for (int i = 0; i < plylst.Audio_Playlist_Titel.Count; i++)
                {
                    Audio_Titel aTitel = plylst.Audio_Playlist_Titel.ElementAt(i).Audio_Titel;                    
                    if (aTitel.Länge == null || aTitel.Länge == 0)
                    {
                        if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                            Directory.Exists(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                            File.Exists(aTitel.Pfad + "\\" + aTitel.Datei))
                        {        
                            if (plylst != (Audio_Playlist)args.Argument) break;
                            TimeSpan duration = TimeSpan.FromMilliseconds(0);
                            
                            if (aTitel.Datei.ToLower().EndsWith("ogg"))
                            {
                                NVorbis.VorbisReader reader = new NVorbis.VorbisReader(aTitel.Pfad + "\\" + aTitel.Datei);
                                duration = reader.TotalTime;                                
                            }
                            else
                            if (
                                aTitel.Datei.ToLower().EndsWith("wav") ||                             
                                aTitel.Datei.ToLower().EndsWith("wma") ||
                                aTitel.Datei.ToLower().EndsWith("mp3"))
                            {
                                AudioFileReader reader = new AudioFileReader(aTitel.Pfad + "\\" + aTitel.Datei);
                                duration = reader.TotalTime;
                                reader.Close();
                            }    
                            
                            totalLength += duration;
                            
                            if (plylst.Audio_Playlist_Titel.Count >= i + 1)
                            {
                                if (aTitel.Länge != duration.TotalMilliseconds)
                                    aTitel.Länge = duration.TotalMilliseconds;
                            }
                            if (plylst != (Audio_Playlist)args.Argument) break;
                        }
                        else
                        {
                            _GrpObjecte.FindAll(t => t.aPlaylist == plylst).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj)
                            {
                                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpObj._listZeile.FirstOrDefault(t => t.aPlaylistTitel.Audio_TitelGUID == plylst.Audio_Playlist_Titel.ElementAt(i).Audio_TitelGUID);
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

        private void workerGetLength_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            try
            {
                BerechneSpieldauer = false;

                if (args.Error == null &&
                    ((Audio_Playlist)args.Result).Länge != 0)
                    Global.ContextAudio.Update<Audio_Playlist>(((Audio_Playlist)args.Result));

                Global.SetIsBusy(false);
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                Global.SetIsBusy(false);
                (sender as BackgroundWorker).Dispose();
            }
        }


        public void MoveLbEditorItem(Audio_Playlist aPlaylist, int dif)
        {
            lbEditorItemVM lbiAnfangVM = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == aPlaylist);
            int posEnde = FilteredEditorListBoxItemListe.IndexOf(lbiAnfangVM) + dif;
            lbEditorItemVM lbiEndeVM = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == FilteredEditorListBoxItemListe.ElementAt(posEnde).APlaylist);

            int anf = lbiAnfangVM.APlaylist.Reihenfolge;
            int end = lbiEndeVM.APlaylist.Reihenfolge;

            lbEditorItemVM lbi;
            if (lbiEndeVM != null)
            {
                if (anf < end)
                {
                    for (int x = anf + 1; x <= end; x++)
                    {
                        lbi = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist.Reihenfolge == x);
                        if (lbi != null)
                        {
                            lbi.APlaylist.Reihenfolge--;

                            Global.ContextAudio.Update<Audio_Playlist>(lbi.APlaylist);
                        }
                    }
                    lbiAnfangVM.APlaylist.Reihenfolge = end;
                }
                else
                {
                    for (int x = anf - 1; x >= end; x--)
                    {
                        lbi = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist.Reihenfolge == x);
                        lbi.APlaylist.Reihenfolge++;
                        Global.ContextAudio.Update<Audio_Playlist>(lbi.APlaylist);
                    }
                    lbiAnfangVM.APlaylist.Reihenfolge = end;
                }
                Global.ContextAudio.Update<Audio_Playlist>(lbiAnfangVM.APlaylist);
            }
            OnChanged("EditorListBoxItemListe");
        }

        public void MoveAudioZeileItem(Audio_Playlist_Titel aPlaylistTitel, int dif)
        {
            AudioZeileVM aZeileAnfangVM = FilteredLbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel == aPlaylistTitel);
            int posEnde = FilteredLbEditorAudioZeilenListe.IndexOf(aZeileAnfangVM) + dif;
            AudioZeileVM aZeileEndeVM = LbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel == FilteredLbEditorAudioZeilenListe.ElementAt(posEnde).aPlayTitel);

            int anf = aZeileAnfangVM.aPlayTitel.Reihenfolge;
            int end = aZeileEndeVM.aPlayTitel.Reihenfolge;

            AudioZeileVM lbi;
            if (aZeileEndeVM != null)
            {
                if (anf < end)
                {
                    for (int x = anf + 1; x <= end; x++)
                    {
                        lbi = LbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel.Reihenfolge == x);
                        lbi.aPlayTitel.Reihenfolge--;

                        Global.ContextAudio.Update<Audio_Playlist_Titel>(lbi.aPlayTitel);
                    }
                    aZeileAnfangVM.aPlayTitel.Reihenfolge = end;
                }
                else
                {
                    for (int x = anf - 1; x >= end; x--)
                    {
                        lbi = LbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel.Reihenfolge == x);
                        if (lbi != null)
                        {
                            lbi.aPlayTitel.Reihenfolge++;
                            Global.ContextAudio.Update<Audio_Playlist_Titel>(lbi.aPlayTitel);
                        }
                    }
                    aZeileAnfangVM.aPlayTitel.Reihenfolge = end;
                }
                Global.ContextAudio.Update<Audio_Playlist_Titel>(aZeileAnfangVM.aPlayTitel);
            }
            OnChanged("AktKlangPlaylist");
        }

    }
}
