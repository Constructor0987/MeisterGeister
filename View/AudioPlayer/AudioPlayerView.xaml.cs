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
using System.Windows.Threading;
// Eigene Usings
using MeisterGeister.Logic.Settings;
using MeisterGeister.Logic.General;
using MeisterGeister.View.General;
using MeisterGeister.View.Windows;
using Global = MeisterGeister.Global;
using MeisterGeister.Model;
using System.Xml;
using System.IO;
using System.Windows.Markup;
using System.ComponentModel;
//using Id3Lib;
//using Mp3Lib;

/*
Song title  30 Zeichen
Artist	    30 Zeichen
Album	    30 Zeichen
Year	    4 Zeichen
Comment	    30 Zeichen
Genre	    1 Byte

 * */

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für AudioPlayerView.xaml
    /// </summary>
    /// 

    delegate void updateBGSongCallback(MediaPlayer _player, string tekst);

    public partial class AudioPlayerView : UserControl
    {
        DispatcherTimer KlangProgBarTimer = new DispatcherTimer();
        DispatcherTimer HintergrundProgBarTimer = new DispatcherTimer();
        public String[] AnzKlangParallel = new String[0];
     //   string savedSpnlKlang =
     //       "<StackPanel Name=\"spnlKlangRow\" Grid.Row=\"2\" Grid.ColumnSpan=\"14\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:mgvg=\"clr-namespace:MeisterGeister.View.General;assembly=DSA MeisterGeister\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"><Grid><Grid.ColumnDefinitions><ColumnDefinition Width=\"1\" /><ColumnDefinition Width=\"35\" /><ColumnDefinition Width=\"100*\" /><ColumnDefinition Width=\"42\" /><ColumnDefinition Width=\"28\" /><ColumnDefinition Width=\"92\" /><ColumnDefinition Width=\"21\" /><ColumnDefinition Width=\"75\" /><ColumnDefinition Width=\"72\" /><ColumnDefinition Width=\"14\" /><ColumnDefinition Width=\"92\" /><ColumnDefinition Width=\"21\" /><ColumnDefinition Width=\"75\" /><ColumnDefinition Width=\"72\" /></Grid.ColumnDefinitions><Grid.RowDefinitions><RowDefinition Height=\"23\" /></Grid.RowDefinitions><Image Source=\"pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/entf_01.png\" Stretch=\"Fill\" Name=\"imgTrash0_X\" Tag=\"0\" Width=\"16\" Height=\"16\" Margin=\"0,0,0,4\" HorizontalAlignment=\"Left\" VerticalAlignment=\"Bottom\" Grid.Column=\"1\" Grid.Row=\"2\" /><ProgressBar Name=\"pbarTitel0_X\" Height=\"18\" Margin=\"15,0,0,3\" VerticalAlignment=\"Bottom\" Grid.Column=\"2\" Grid.Row=\"2\" Grid.ColumnSpan=\"1\" /><CheckBox IsChecked=\"False\" Name=\"chkTitel0_X\" Height=\"20\" Margin=\"0,2,0,0\" HorizontalAlignment=\"Left\" Grid.Column=\"2\" Grid.Row=\"2\" Grid.ColumnSpan=\"1\">chkTitel0</CheckBox><Label HorizontalContentAlignment=\"Center\" Padding=\"0,0,0,0\" Name=\"lblDauer0_X\" Height=\"14\" Margin=\"5,3,0,6\" Grid.Column=\"3\" Grid.Row=\"2\">-:--</Label><Button Name=\"btnFile0_X\" Width=\"17\" Height=\"23\" Margin=\"6,0,0,0\" HorizontalAlignment=\"Left\" Grid.Column=\"4\" Grid.Row=\"2\">...</Button><Slider Maximum=\"100\" Value=\"50\" Name=\"sldVol0_X\" Height=\"23\" Margin=\"6,0,4,0\" ToolTip=\"50 %\" Grid.Column=\"5\" Grid.Row=\"2\" /><CheckBox Name=\"chkVolMove0_X\" Width=\"14\" Height=\"14\" Margin=\"0,6,0,4\" Grid.Column=\"6\" Grid.Row=\"2\" /><mgvg:IntBox MinValue=\"0\" MaxValue=\"100\" ShowButtons=\"True\" Value=\"0\" Name=\"iboxVolMin0_X\" MinWidth=\"65\" Height=\"23\" HorizontalAlignment=\"Left\" Grid.Column=\"7\" Grid.Row=\"2\"><mgvg:IntBox.Resources><BooleanToVisibilityConverter x:Key=\"BooleanToVisibilityConverter1\" /><Style TargetType=\"TextBox\" x:Key=\"TextBoxIntStyle\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></mgvg:IntBox.Resources><Grid><Grid.ColumnDefinitions><ColumnDefinition Width=\"*\" /><ColumnDefinition Width=\"Auto\" /></Grid.ColumnDefinitions><TextBox TextAlignment=\"Center\" BorderBrush=\"#00FFFFFF\" HorizontalContentAlignment=\"Center\" VerticalContentAlignment=\"Center\" Name=\"_textBoxInt\" Grid.Column=\"0\"><TextBox.Style><Style TargetType=\"TextBox\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></TextBox.Style>0</TextBox><StackPanel Orientation=\"Horizontal\" Name=\"_stackPanelButtons\" Visibility=\"Visible\" Grid.Column=\"1\"><Button Name=\"_buttonMinus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/entf_02.png\" /></Button><Button Name=\"_buttonPlus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/add.png\" /></Button></StackPanel></Grid></mgvg:IntBox><mgvg:IntBox MinValue=\"0\" MaxValue=\"100\" ShowButtons=\"True\" Value=\"0\" Name=\"iboxVolMax0_X\" MinWidth=\"65\" Height=\"23\" HorizontalAlignment=\"Left\" Grid.Column=\"8\" Grid.Row=\"2\"><mgvg:IntBox.Resources><BooleanToVisibilityConverter x:Key=\"BooleanToVisibilityConverter1\" /><Style TargetType=\"TextBox\" x:Key=\"TextBoxIntStyle\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></mgvg:IntBox.Resources><Grid><Grid.ColumnDefinitions><ColumnDefinition Width=\"*\" /><ColumnDefinition Width=\"Auto\" /></Grid.ColumnDefinitions><TextBox TextAlignment=\"Center\" BorderBrush=\"#00FFFFFF\" HorizontalContentAlignment=\"Center\" VerticalContentAlignment=\"Center\" Name=\"_textBoxInt\" Grid.Column=\"0\"><TextBox.Style><Style TargetType=\"TextBox\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></TextBox.Style>0</TextBox><StackPanel Orientation=\"Horizontal\" Name=\"_stackPanelButtons\" Visibility=\"Visible\" Grid.Column=\"1\"><Button Name=\"_buttonMinus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/entf_02.png\" /></Button><Button Name=\"_buttonPlus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/add.png\" /></Button></StackPanel></Grid></mgvg:IntBox><Slider Interval=\"50\" Maximum=\"30000\" Value=\"1000\" LargeChange=\"500\" SmallChange=\"100\" Name=\"sldKlangPause0_X\" Height=\"23\" Margin=\"6,0,4,0\" ToolTip=\"1000 ms\" Grid.Column=\"10\" Grid.Row=\"2\" /><CheckBox Name=\"chkKlangPauseMove0_X\" Width=\"14\" Height=\"14\" Margin=\"0,6,0,4\" Grid.Column=\"11\" Grid.Row=\"2\" /><mgvg:IntBox MinValue=\"0\" MaxValue=\"30000\" ShowButtons=\"True\" Value=\"0\" Name=\"iboxKlangPauseMin0_X\" MinWidth=\"65\" Height=\"23\" HorizontalAlignment=\"Left\" Grid.Column=\"12\" Grid.Row=\"2\"><mgvg:IntBox.Resources><BooleanToVisibilityConverter x:Key=\"BooleanToVisibilityConverter1\" /><Style TargetType=\"TextBox\" x:Key=\"TextBoxIntStyle\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></mgvg:IntBox.Resources><Grid><Grid.ColumnDefinitions><ColumnDefinition Width=\"*\" /><ColumnDefinition Width=\"Auto\" /></Grid.ColumnDefinitions><TextBox TextAlignment=\"Center\" BorderBrush=\"#00FFFFFF\" HorizontalContentAlignment=\"Center\" VerticalContentAlignment=\"Center\" Name=\"_textBoxInt\" Grid.Column=\"0\"><TextBox.Style><Style TargetType=\"TextBox\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></TextBox.Style>0</TextBox><StackPanel Orientation=\"Horizontal\" Name=\"_stackPanelButtons\" Visibility=\"Visible\" Grid.Column=\"1\"><Button Name=\"_buttonMinus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/entf_02.png\" /></Button><Button Name=\"_buttonPlus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/add.png\" /></Button></StackPanel></Grid></mgvg:IntBox><mgvg:IntBox MinValue=\"0\" MaxValue=\"30000\" ShowButtons=\"True\" Value=\"0\" Name=\"iboxKlangPauseMax0_X\" MinWidth=\"65\" Height=\"23\" HorizontalAlignment=\"Left\" Grid.Column=\"13\" Grid.Row=\"2\"><mgvg:IntBox.Resources><BooleanToVisibilityConverter x:Key=\"BooleanToVisibilityConverter1\" /><Style TargetType=\"TextBox\" x:Key=\"TextBoxIntStyle\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></mgvg:IntBox.Resources><Grid><Grid.ColumnDefinitions><ColumnDefinition Width=\"*\" /><ColumnDefinition Width=\"Auto\" /></Grid.ColumnDefinitions><TextBox TextAlignment=\"Center\" BorderBrush=\"#00FFFFFF\" HorizontalContentAlignment=\"Center\" VerticalContentAlignment=\"Center\" Name=\"_textBoxInt\" Grid.Column=\"0\"><TextBox.Style><Style TargetType=\"TextBox\"><Style.Triggers><DataTrigger Binding=\"{Binding Path=NoBackground, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#00FFFFFF</SolidColorBrush></Setter.Value></Setter></DataTrigger><DataTrigger Binding=\"{Binding Path=WeissAufSchwarz, ElementName=_intBox}\" Value=\"true\"><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><SolidColorBrush>#FFC0C0C0</SolidColorBrush></Setter.Value></Setter></DataTrigger></Style.Triggers><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property=\"TextElement.Foreground\"><Setter.Value><SolidColorBrush>#FF000000</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Panel.Background\"><Setter.Value><SolidColorBrush>#FFFFFFFF</SolidColorBrush></Setter.Value></Setter><Setter Property=\"Border.BorderBrush\"><Setter.Value><LinearGradientBrush EndPoint=\"0,20\" MappingMode=\"Absolute\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFABADB3\" Offset=\"0.05\" /><GradientStop Color=\"#FFE2E3EA\" Offset=\"0.07\" /><GradientStop Color=\"#FFE3E9EF\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush></Setter.Value></Setter></Style></TextBox.Style>0</TextBox><StackPanel Orientation=\"Horizontal\" Name=\"_stackPanelButtons\" Visibility=\"Visible\" Grid.Column=\"1\"><Button Name=\"_buttonMinus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/entf_02.png\" /></Button><Button Name=\"_buttonPlus\" Width=\"18\"><Image Source=\"pack://application:,,,/Images/Icons/General/add.png\" /></Button></StackPanel></Grid></mgvg:IntBox></Grid></StackPanel>";
        
        public int[][] laufendeSongs = new int[8][];
        public int[][] standbySongs = new int[8][];
        public int[] zeilenAufSeite = new int[8] {0,0,0,0,0,0,0,0};
        public Audio_Playlist AktHintergrundPlaylist, AktKlangPlaylist;

        public String[] BGSong = new String[3] { "MusikSong", "Interpret", "Verzeichnis" };
        public string orgStackString;

        public MediaPlayer[][] _player = new MediaPlayer[8][];
        MediaPlayer HintergrundPlayer;

        System.Timers.Timer BGSongTimer = new System.Timers.Timer();
        DispatcherTimer KlangPlayEndetimer;


        public AudioPlayerView()
        {
            InitializeComponent();

            // Speichert die Klangzeile mit Komponenten in einen String
       //     savedSpnlKlang = XamlWriter.Save(spnlKlangRow);

            KlangProgBarTimer.Tick += new EventHandler(KlangProgBarTimer_Tick);
            KlangProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            HintergrundProgBarTimer.Tick += new EventHandler(HintergrundProgBarTimer_Tick);
            HintergrundProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            _player[0] = new MediaPlayer[1];
            
        }


        public string SongInfo(String musikfile)
        {
            String Filename = "";

            if (musikfile != null)
                Filename = musikfile;
            // BGSong[0] = musikfile;
            Filename = Filename + Convert.ToString(DateTime.Now);
            return Filename;
        }

  /*      private void UpdateBGSong(MediaPlayer _player, string tekst)
        {
            if (label1.Dispatcher.CheckAccess() == false)
            {
                updateBGSongCallback uCallBack = new updateBGSongCallback(UpdateBGSong);
                this.Dispatcher.Invoke(uCallBack, (tekst));
            }
            else
            {
       

                if ((_player.NaturalDuration.HasTimeSpan) && lblBgTimeMax.Content == "")
                {
                    string maxSec = "00";
                    if (_player.NaturalDuration.TimeSpan.Seconds <= 9)
                        maxSec = "0" + _player.NaturalDuration.TimeSpan.Seconds;
                    else
                        maxSec = Convert.ToString(_player.NaturalDuration.TimeSpan.Seconds);
                    lblBgTimeMax.Content = _player.NaturalDuration.TimeSpan.Minutes + ":" + maxSec;

                    pbarBGSong.Maximum = _player.NaturalDuration.TimeSpan.TotalMilliseconds;
                }

                pbarBGSong.Value = _player.Position.TotalMilliseconds;
                string acSec = "00";
                if (_player.Position.Seconds <= 9)
                    acSec = "0" + _player.Position.Seconds;
                else
                    acSec = Convert.ToString(_player.Position.Seconds);

                lblBgTimeActual.Content = _player.Position.Minutes + ":" + acSec;

                //(this.FindName(tekst) as Label).Content = "Durchgang "; 
            }
        }*/

        public MediaPlayer PlayFile(int seite, int zeile, MediaPlayer _player, String url, double vol)
        {
            try
            {
                if (_player == null)
                    _player = new MediaPlayer();
                try
                {
                    _player.Open(new Uri(url));
                    if (seite >= 0)
                    {
                        Array.Resize(ref AnzKlangParallel, AnzKlangParallel.Length + 1);
                        AnzKlangParallel[AnzKlangParallel.Length - 1] = seite + "|" + zeile + "|" + _player.GetHashCode();
                
                        _player.MediaEnded += new EventHandler(Player_Ended);
                    }
                    else
                        _player.MediaEnded += new EventHandler(HintergrundPlayer_Ended);
                }
                catch (Exception ex2)
                {
                    ListBoxItem lbItem = lbhintergrundtitellist.SelectedItem as ListBoxItem;
                    lbItem.Background = Brushes.Yellow;
                    lbItem.ToolTip = "Datei konnte nicht geöffnet werden (Datei vorhanden?)" + ex2;
                    SpieleNeuenHintergrundTitel();
                    return null;
                } 
                finally
                {
                    _player.Volume = vol;
                    _player.Play();
                }
                
                if (seite >= 0)
                    _player.MediaFailed += new EventHandler<ExceptionEventArgs>(Player_KlangMediaFailed);
                else
                    _player.MediaFailed += new EventHandler<ExceptionEventArgs>(Player_HintergrundMediaFailed);

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

        public void VolChanged(MediaPlayer _player, double vol)
        {
            if (_player != null)
                _player.Volume = vol / 100;
        }
        
        void CheckPlayStandbySongs(int seite)
        {
            Grid grdKlang = this.FindName("grdKlang" + seite) as Grid;

            if ((laufendeSongs[seite] == null && standbySongs[seite] != null) ||
               (laufendeSongs[seite] != null && standbySongs[seite] != null &&
                (this.FindName("klangsongparallel" + seite) as IntBox).Value > laufendeSongs[seite].Length))
            {
                uint anzStandbySongs;
                int neueSongs;
                if (laufendeSongs[seite] == null)
                    neueSongs = Convert.ToInt32((this.FindName("klangsongparallel" + seite) as IntBox).Value);
                else
                    neueSongs = Convert.ToInt32((this.FindName("klangsongparallel" + seite) as IntBox).Value) - laufendeSongs[seite].Length;
                
                for (int i = 0; i < neueSongs; i++)
                {
                    if (standbySongs[seite] == null)
                        anzStandbySongs = 0;
                    else
                    {
                        anzStandbySongs = Convert.ToUInt32(standbySongs[seite].Length);
                        Würfel w = new Würfel(anzStandbySongs);
                        w.Würfeln(1);
                        int zuspielendersong = standbySongs[seite][w.Ergebnis - 1];
                        RemoveSongStandby(seite, w.Ergebnis);

                        // Titel anstarten
                        StackPanel spnlZeile = grdKlang.FindName("spnlKlangRow" + seite + "_" + zuspielendersong) as StackPanel;
                        if ((spnlZeile.FindName("chkTitel" + seite + "_" + zuspielendersong) as CheckBox) != null)
                            chkTitel0_0_Click((spnlZeile.FindName("chkTitel" + seite + "_" + zuspielendersong)), new RoutedEventArgs());
                    }
                }
            }
        }

        void KlangPlayEndetimer_Tick(object sender, EventArgs e)
        {
            (sender as DispatcherTimer).Stop();

            string s = Convert.ToString((sender as DispatcherTimer).Tag);
            char[] Separator = new char[] { '_' };
            string[] werte = s.Split(Separator, StringSplitOptions.None);

            int seite = Convert.ToInt32(werte[0]);
            int zeile = Convert.ToInt32(werte[1]);
            Grid grdKlang = this.FindName("grdKlang" + seite) as Grid;

            // Player löschen, auf Urspring zurück
            _player[seite][zeile] = null;
            if ((grdKlang.FindName("spnlKlangRow" + seite + "_" + zeile) as StackPanel).FindName("pbarTitel" + s) as ProgressBar != null)
                ((grdKlang.FindName("spnlKlangRow" + seite + "_" + zeile) as StackPanel).FindName("pbarTitel" + s) as ProgressBar).Value = 0;

            // Song aus der Liste der laufenden Songs herausnehmen
            RemoveSongLaufend(seite, zeile);

            // Song in die Liste der Standby-Songs aufnehmen
            AddSongStandby(seite, zeile);

            CheckPlayStandbySongs(seite);
        }

        void AddSongStandby(int seite, int zeile)
        {
            if (standbySongs[seite] == null)
                Array.Resize(ref standbySongs[seite], 1);
            else
                Array.Resize(ref standbySongs[seite], standbySongs[seite].Length + 1);
            standbySongs[seite][standbySongs[seite].Length - 1] = zeile;
        }

        void RemoveSongStandby(int seite, int zeile)
        {
            for (int x = zeile - 1; x < standbySongs[seite].Length - 1; x++)
                standbySongs[seite][x] = standbySongs[seite][x + 1];
            Array.Resize(ref standbySongs[seite], standbySongs[seite].Length - 1);
            if (standbySongs[seite].Length == 0)
                standbySongs[seite] = null;
        }

        void RemoveSongLaufend(int seite, int zeile)
        {
            int r = Array.IndexOf(laufendeSongs[seite], zeile);

            for (int x = r; x < laufendeSongs[seite].Length - 1; x++)
                laufendeSongs[seite][x] = laufendeSongs[seite][x + 1];
            Array.Resize(ref laufendeSongs[seite], laufendeSongs[seite].Length - 1);
        }

        void RemoveSongParallel(int seite, int zeile)
        {
            char[] Separator = new char[] { '|' };
            for (int i = 0; i <= AnzKlangParallel.Length - 1; i++)
            {
                string[] s = AnzKlangParallel[i].Split(Separator, StringSplitOptions.None);
                if (s[0] == Convert.ToString(seite) && s[1] == Convert.ToString(zeile))
                {
                    for (int x = i; x < AnzKlangParallel.Length - 1; x++)
                        AnzKlangParallel[x] = AnzKlangParallel[x + 1];
                    Array.Resize(ref AnzKlangParallel, AnzKlangParallel.Length - 1);
                    break;
                }
            }
        }
        
        void HintergrundPlayer_Ended(object sender, EventArgs e)
        {
            (sender as MediaPlayer).Stop();
            HintergrundProgBarTimer.Stop();
            SpieleNeuenHintergrundTitel();
        }

        void Player_HintergrundMediaFailed(object sender, ExceptionEventArgs e)
        {
            (sender as MediaPlayer).Stop();
            HintergrundProgBarTimer.Stop();
            ListBoxItem lbItem = lbhintergrundtitellist.SelectedItem as ListBoxItem;
            lbItem.Background = Brushes.Yellow;
            lbItem.ToolTip = "Datei kann nicht abgespielt werden. Falscher oder nicht kompatibler Typ (" + (sender as MediaPlayer).Source.LocalPath + ")";
            SpieleNeuenHintergrundTitel();
        }

        void Player_Ended(object sender, EventArgs e)
        {
            Grid grdKlang;
            // MediaPlayerbezug finden
            for (int x = 0; x < AnzKlangParallel.Length; x++)
            {
                char[] Separator = new char[] { '|' };
                string[] s = AnzKlangParallel[x].Split(Separator, StringSplitOptions.None);
                if (s[2] == Convert.ToString((sender as MediaPlayer).GetHashCode()))
                {
                    grdKlang = this.FindName("grdKlang" + s[0]) as Grid;

                    KlangPlayEndetimer = new DispatcherTimer();
                    KlangPlayEndetimer.Interval = TimeSpan.FromMilliseconds(
                        ((grdKlang.FindName("spnlKlangRow" + s[0] + "_" + s[1]) as StackPanel).FindName("sldKlangPause" + s[0] + "_" + s[1]) as Slider).Value);
                    KlangPlayEndetimer.Tick += new EventHandler(KlangPlayEndetimer_Tick);
                    KlangPlayEndetimer.Tag = s[0] + "_" + s[1];
                    KlangPlayEndetimer.Start();
                    break;
                }
            }

            if (Convert.ToInt32(btnBGAbspielen.Tag) == 1)
                lbBackground_SelectionChanged(lbBackground, null);
            App.CloseSplashScreen();
        }


        void Player_KlangMediaFailed(object sender, ExceptionEventArgs e)
        {
            char[] Separator = new char[] { '|' };
            int mediahash = (sender as MediaPlayer).GetHashCode();

            string[] s;
            for (int i = 0; i <= AnzKlangParallel.Length -1; i++)
            {  
                s = AnzKlangParallel[i].Split(Separator, StringSplitOptions.None);
                if (mediahash == Convert.ToInt32(s[2]))
                {
                    int x = Convert.ToInt32(s[0]);
                    int y = Convert.ToInt32(s[1]);

                    Grid grdKlang = this.FindName("grdKlang" + s[0]) as Grid;
                    StackPanel spnlZeile = grdKlang.FindName("spnlKlangRow" + x + "_" + y) as StackPanel;
                    spnlZeile.Background = Brushes.Yellow;
                    spnlZeile.ToolTip = "Datei kann nicht abgespielt werden (Falscher oder nicht kompatibler Typ).";
                    _player[x][y].Stop();
                    _player[x][y] = null;
                    RemoveSongLaufend(x, y);
                    RemoveSongParallel(x, y);
                    CheckPlayStandbySongs(x);
                }
            }
        }

        private void KlangProgBarTimer_Tick(object sender, EventArgs e)
        {
            Grid grdKlang;
            for (int i = 0; i + 1 <= AnzKlangParallel.Length; i++)
            {                
                char[] Separator = new char[] { '|' };
                string[] s = AnzKlangParallel[i].Split(Separator, StringSplitOptions.None);
                int x = Convert.ToInt32(s[0]);
                int y = Convert.ToInt32(s[1]);
                grdKlang = this.FindName("grdKlang" + s[0]) as Grid;

                StackPanel spnlZeile = grdKlang.FindName("spnlKlangRow" + x + "_" + y) as StackPanel;
                if (spnlZeile.Tag != null)
                {                    
                    DateTime dtJetzt = DateTime.Now;
                    DateTime dtVorher = Convert.ToDateTime(spnlZeile.Tag);

                    TimeSpan diffResult = dtJetzt - dtVorher;
                    if (diffResult.TotalSeconds > 1)
                    {
                        if (!_player[x][y].HasAudio)
                        {
                            spnlZeile.Background = Brushes.Yellow;
                            spnlZeile.ToolTip = "Datei kann nicht abgespielt werden";
                            _player[x][y].Stop();
                            _player[x][y] = null;
                            RemoveSongLaufend(x, y);
                            RemoveSongParallel(x, y);
                            CheckPlayStandbySongs(x);
                        }
                        else
                            spnlZeile.Background = null;
                    }
                }
                if (_player[x][y] != null && _player[x][y].HasAudio == false && _player[x][y].BufferingProgress == 1)
                    spnlZeile.Tag = DateTime.Now;
                else
                    spnlZeile.Tag = null;

                CheckBox ch = spnlZeile.FindName("chkTitel" + x + "_" + y) as CheckBox;
                ProgressBar prog = spnlZeile.FindName("pbarTitel" + x + "_" + y) as ProgressBar;

                if (ch != null && prog != null && ch.IsChecked == true && _player[x][y] != null)
                {
                    //if prog.Value >= prog.Maximum
                    if (prog.Maximum == 10000 && _player[x][y].NaturalDuration.HasTimeSpan)
                        prog.Maximum = _player[x][y].NaturalDuration.TimeSpan.TotalMilliseconds;
                    Label lbl = spnlZeile.FindName("lblDauer" + x + "_" + y) as Label;
                    if (lbl.Content.ToString() == "-:--" && _player[x][y].NaturalDuration.HasTimeSpan)
                        lbl.Content = _player[x][y].NaturalDuration.TimeSpan.ToString().Substring(3, 5);

                    prog.Value = _player[x][y].Position.TotalMilliseconds;
                }
            }
        }

        private void slBGVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VolChanged(HintergrundPlayer, (sender as Slider).Value);
        }


      /*  public void EditTag(string filename)
        {            
            Mp3File mp3File = null;

            try
            {
                // create mp3 file wrapper; open it and read the tags
                mp3File = new Mp3File(filename);
            }
            catch (Exception e)
            {
                ExceptionMessageBox.Show(_form, e, "Error Reading Tag");
                return;
            }

            // create dialog and give it the ID3v2 block for editing
            // this is a bit sneaky; it uses the edit dialog straight out of TagScanner.exe as if it was a dll.
            TagEditor.ID3AdapterEdit id3Edit = new TagEditor.ID3AdapterEdit(mp3File);

            if (id3Edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    using (new CursorKeeper(Cursors.WaitCursor))
                    {
                        mp3File.Update();
                    }
                }
                catch (Exception e)
                {
                    ExceptionMessageBox.Show(_form, e, "Error Writing Tag");
                }
            }
        }*/

        private void lbBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbBackground.SelectedItems.Count != 0)
            {
                try
                {
                    if (HintergrundProgBarTimer != null)
                    {
                        HintergrundProgBarTimer.Stop();
                        if (HintergrundPlayer != null)
                        {
                            HintergrundPlayer.Stop();
                            HintergrundPlayer = null;
                        }
                    }

                    List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals((lbBackground.SelectedItem as ListBoxItem).Tag)).ToList();
                    if (playlistliste != null)
                    {
                        AktHintergrundPlaylist = playlistliste[0];
                        List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]).ToList();
                        lbhintergrundtitellist.Items.Clear();
                        for (int i = 0; i < titel.Count; i++)
                        {
                            ListBoxItem lbitem = new ListBoxItem();
                            lbitem.Name = "titel" + i;
                            lbitem.Tag = titel[i].Audio_TitelGUID;
                            lbitem.Content = titel[i].Name;
                            lbhintergrundtitellist.Items.Add(lbitem);
                        }
                        SpieleNeuenHintergrundTitel();
                    }
                }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Playlist Fehler", "Die Playliste konnte nicht geöffnet werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }

                /*
                grdSongInfo.Visibility = Visibility.Visible;
                btnBGAbspielen.Tag = 1;
                btnBGAbspielen.Content = "Stoppen";

                pbarBGSong.Value = 0;
                btnBGAbspielen.IsEnabled = true;

                ListBoxItem lbi = (lbBackground.SelectedItem as ListBoxItem);

                String playlist = lbi.Content.ToString();
                int index = 0;

                while (PlaylistArray[index][0] != playlist)
                {
                    index++;
                    if (index > PlaylistArray[index].Length)
                        break;
                }

                if (index <= PlaylistArray[index].Length)
                {
                    String s = PlayBGList(index, PlaylistArray[index], slBGVolume.Value);
                }
                else
                {
                    string msg = "Die Playliste wurde nicht gefunden";
                    MsgWindow errWin = new MsgWindow("Datenbank-Fehler", msg);
                    errWin.ShowDialog();
                }*/
            }
            else
                btnBGAbspielen.IsEnabled = false;
        }

        private void SpieleNeuenHintergrundTitel()
        {
            if (lbhintergrundtitellist.Items.Count == 1)
                lbhintergrundtitellist.SelectedIndex = 1;
            else
            {
                int[] titelmoeglich = new int[0];
                for (int i = 0; i <= lbhintergrundtitellist.Items.Count -1; i++)
                {
                    ListBoxItem lbitem = lbhintergrundtitellist.Items[i] as ListBoxItem;
                    if (lbitem.Background != Brushes.Yellow && lbitem.Background != Brushes.Red) //lbhintergrundtitellist.Background)
                    {
                        Array.Resize(ref titelmoeglich, titelmoeglich.Length +1);
                        titelmoeglich[titelmoeglich.Length-1] = i;                
                    }
                }
                if (titelmoeglich.Length > 0)
                {
                    Würfel w = new Würfel(Convert.ToUInt32(titelmoeglich.Length));
                    w.Würfeln(1);

                    while (titelmoeglich[w.Ergebnis - 1] == lbhintergrundtitellist.SelectedIndex)
                        w.Würfeln(1);
                    lbhintergrundtitellist.SelectedIndex = titelmoeglich[w.Ergebnis - 1];
                }
            }
        }   

        private void btnBGAbspielen_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(btnBGAbspielen.Tag) == 0)
            {
                SpieleNeuenHintergrundTitel();
            }
            else
            {
                if (HintergrundPlayer != null)
                {
                    HintergrundPlayer.Stop();
                    HintergrundPlayer = null;
                }    
                HintergrundProgBarTimer.Stop();  
                btnBGAbspielen.Tag = 0;      
                btnBGAbspielen.Content = "Abspielen";
                grdSongInfo.Visibility = Visibility.Hidden;
                btnBGNext.IsEnabled = false;
                lbhintergrundtitellist.SelectedIndex = -1;
            }
        }

        private void lbBackground_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lbBackground_SelectionChanged(sender, null);
        }

        private void btnBGNext_Click(object sender, RoutedEventArgs e)
        {
            SpieleNeuenHintergrundTitel();
        }

        private void lbKlang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbKlang.SelectedIndex != -1)
            {
                int seite = tcKlang.SelectedIndex;
                Grid grdKlangPlaylistInfo = this.FindName("grdKlangPlaylistInfo" + seite) as Grid;

                for (int i = 0; i <= grdKlangPlaylistInfo.Children.Count - 1; i++)
                    grdKlangPlaylistInfo.Children[i].Visibility = Visibility.Visible;

                string s = (lbKlang.Items[lbKlang.SelectedIndex] as ListBoxItem).Content.ToString();
                List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(s)).ToList();
                if (playlistliste.Count == 1)
                {
                    List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(playlistliste[0]);
                    PlaylisteLeeren(seite);
                    AktKlangPlaylist = playlistliste[0];
                    if (AktKlangPlaylist.Hintergrundmusik)
                        rbIstMusikPlaylist.IsChecked = true;
                    else
                        rbIstKlangPlaylist.IsChecked = true;
                    tboxPlaylistName.Text = AktKlangPlaylist.Name;
                    (tcKlang.SelectedItem as TabItem).Header = AktKlangPlaylist.Name;

                    if (titelliste.Count > 0)
                    {
                        IntBox klangsongparallel = this.FindName("klangsongparallel" + seite) as IntBox;
                        klangsongparallel.NumValueChanged -= new NumValueChangedEventHandler(klangsongparallel0_NumValueChanged);

                        klangsongparallel.MaxValue = AktKlangPlaylist.Audio_Playlist_Titel.Count;// playlisttitel.Count;
                        klangsongparallel.MinValue = 0;
                        klangsongparallel.Value = 0;
                        klangsongparallel.NumValueChanged += new NumValueChangedEventHandler(klangsongparallel0_NumValueChanged);

                        for (int x = 0; x <= AktKlangPlaylist.Audio_Playlist_Titel.Count - 1; x++)
                        {
                            Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titelliste[x])[0];
                            KlangNewRow(playlisttitel.Audio_Titel.Pfad, seite, x, playlisttitel);
                        }

                        klangsongparallel.Value = AktKlangPlaylist.MaxSongsParallel;
                        klangsongparallel.MinValue = 1;
                        zeilenAufSeite[seite] = AktKlangPlaylist.Audio_Playlist_Titel.Count;
                    }
                }
                else
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Playlist-Liste konnte nicht eindeutig in der Datenbank detektiert werden.", null);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void PlaylisteLeeren(int seite)
        {
         /*   if ((grdKlang0.FindName("chkTitel" + seite + "_0") as CheckBox).IsChecked.Value == true)
            {
                (grdKlang0.FindName("chkTitel" + seite + "_0") as CheckBox).Click -= new RoutedEventHandler(chkTitel0_0_Click);
                (grdKlang0.FindName("chkTitel" + seite + "_0") as CheckBox).IsChecked = false;
                if (_player[seite][0] != null)
                {
                    _player[seite][0].Stop();
                    _player[seite][0] = null;
                }
                if (laufendeSongs != null && laufendeSongs[seite].Contains(0))
                    RemoveSongLaufend(seite, 0);
                (this.FindName("pbarTitel" + seite + "_0") as ProgressBar).Maximum = 100;
                (this.FindName("pbarTitel" + seite + "_0") as ProgressBar).Value = 0;
                RemoveSongParallel(seite, 0);
            }
            (this.FindName("imgTrash" + seite + "_0") as Image).Visibility = Visibility.Hidden;
            (this.FindName("pbarTitel" + seite + "_0") as ProgressBar).Visibility = Visibility.Hidden;
            (this.FindName("chkTitel" + seite + "_0") as CheckBox).Visibility = Visibility.Hidden;
            (this.FindName("lblDauer" + seite + "_0") as Label).Visibility = Visibility.Hidden;
            (this.FindName("btnFile" + seite + "_0") as Button).Visibility = Visibility.Hidden;
            (this.FindName("sldVol" + seite + "_0") as Slider).Visibility = Visibility.Hidden;
            (this.FindName("chkVolMove" + seite + "_0") as CheckBox).Visibility = Visibility.Hidden;
            (this.FindName("iboxVolMin" + seite + "_0") as IntBox).Visibility = Visibility.Hidden;
            (this.FindName("iboxVolMax" + seite + "_0") as IntBox).Visibility = Visibility.Hidden;
            (this.FindName("sldKlangPause" + seite + "_0") as Slider).Visibility = Visibility.Hidden;
            (this.FindName("chkKlangPauseMove" + seite + "_0") as CheckBox).Visibility = Visibility.Hidden;
            (this.FindName("iboxKlangPauseMin" + seite + "_0") as IntBox).Visibility = Visibility.Hidden;
            (this.FindName("iboxKlangPauseMax" + seite + "_0") as IntBox).Visibility = Visibility.Hidden;
            */
            if (AktKlangPlaylist != null)
            {
                Grid grdKlang = this.FindName("grdKlang" + seite) as Grid;
                Array.Resize(ref standbySongs[seite], 0);
                for (int i = 0; i < zeilenAufSeite[seite]; i++)
                {
                    StackPanel StackZeile = grdKlang.FindName("spnlKlangRow" + seite + "_" + i) as StackPanel;
                    if ((StackZeile.FindName("chkTitel" + seite + "_" + i) as CheckBox).IsChecked.Value == true)
                    {
                        (StackZeile.FindName("chkTitel" + seite + "_" + i) as CheckBox).Click -= new RoutedEventHandler(chkTitel0_0_Click);
                        (StackZeile.FindName("chkTitel" + seite + "_" + i) as CheckBox).IsChecked = false;
                        if (_player[seite][i] != null)
                        {
                            _player[seite][i].Stop();
                            _player[seite][i] = null;
                        }
                        if (laufendeSongs[seite] != null && laufendeSongs[seite].Contains(i))
                                RemoveSongLaufend(seite, i);

                        (StackZeile.FindName("pbarTitel" + seite + "_" + i) as ProgressBar).Maximum = 100;
                        (StackZeile.FindName("pbarTitel" + seite + "_" + i) as ProgressBar).Value = 0;
                        RemoveSongParallel(seite, i);
                    }
                    grdKlang.Children.Remove(StackZeile);
                    grdKlang.UnregisterName(StackZeile.Name);
                    /*
                    grdKlang0.Children.Remove(grdKlang0.FindName("imgTrash" + seite + "_" + i) as Image);
                    grdKlang0.UnregisterName("imgTrash" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("pbarTitel" + seite + "_" + i) as ProgressBar);
                    grdKlang0.UnregisterName("pbarTitel" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("chkTitel" + seite + "_" + i) as CheckBox);
                    grdKlang0.UnregisterName("chkTitel" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("lblDauer" + seite + "_" + i) as Label);
                    grdKlang0.UnregisterName("lblDauer" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("btnFile" + seite + "_" + i) as Button);
                    grdKlang0.UnregisterName("btnFile" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("sldVol" + seite + "_" + i) as Slider);
                    grdKlang0.UnregisterName("sldVol" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("chkVolMove" + seite + "_" + i) as CheckBox);
                    grdKlang0.UnregisterName("chkVolMove" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("iboxVolMin" + seite + "_" + i) as IntBox);
                    grdKlang0.UnregisterName("iboxVolMin" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("iboxVolMax" + seite + "_" + i) as IntBox);
                    grdKlang0.UnregisterName("iboxVolMax" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("sldKlangPause" + seite + "_" + i) as Slider);
                    grdKlang0.UnregisterName("sldKlangPause" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("chkKlangPauseMove" + seite + "_" + i) as CheckBox);
                    grdKlang0.UnregisterName("chkKlangPauseMove" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("iboxKlangPauseMin" + seite + "_" + i) as IntBox);
                    grdKlang0.UnregisterName("iboxKlangPauseMin" + seite + "_" + i);
                    grdKlang0.Children.Remove(grdKlang0.FindName("iboxKlangPauseMax" + seite + "_" + i) as IntBox);
                    grdKlang0.UnregisterName("iboxKlangPauseMax" + seite + "_" + i); */
                }
                grdKlang.RowDefinitions.RemoveRange(2, grdKlang.RowDefinitions.Count -3);
                zeilenAufSeite[seite] = 0;
            }

        }

        public UIElement DeepCopy(UIElement element, string oldValue, string newValue)
        {
            if (orgStackString == null)
                orgStackString = XamlWriter.Save(spnlKlangRow0_X);

            string shapestring = orgStackString.Replace(oldValue, newValue);
            StringReader stringReader = new StringReader(shapestring);
            XmlReader xmlTextReader = new XmlTextReader(stringReader); 
            UIElement DeepCopyobject = (UIElement)XamlReader.Load(xmlTextReader);
            return DeepCopyobject;
        }
       
        private void KlangNewRow(string songdatei, int seite, int row, Audio_Playlist_Titel playlisttitel)
        {
            StackPanel newStack = DeepCopy(spnlKlangRow0_X, "0_X", "0_" + row) as StackPanel;
           
            newStack.Visibility = Visibility.Visible;
            newStack.Name = "spnlKlangRow" + seite + "_" + row;
            Grid grdKlang = this.FindName("grdKlang" + seite) as Grid;
                                  
            grdKlang.Children.Add(newStack); 
            Grid.SetRow(newStack, grdKlang.RowDefinitions.Count - 1);

            grdKlang.RegisterName(newStack.Name, newStack);
            //Papierkorb
            Image imgTrash = newStack.FindName("imgTrash" + seite + "_" + row) as Image;
            imgTrash.Tag = playlisttitel.Audio_Titel.Audio_TitelGUID;
            imgTrash.MouseUp += new MouseButtonEventHandler(imgTrash0_0_MouseUp);

            //Titel
            CheckBox chkTitel = newStack.FindName("chkTitel" + seite + "_" + row) as CheckBox;
            chkTitel.Click += new RoutedEventHandler(chkTitel0_0_Click);
            chkTitel.Content = System.IO.Path.GetFileNameWithoutExtension(songdatei);
            if (playlisttitel.Aktiv)
            {
                chkTitel.IsChecked = playlisttitel.Aktiv;
                AddSongStandby(seite, row);
            }
            chkTitel.ToolTip = songdatei;
            chkTitel.Tag = songdatei;

            // Schieberegler Lautstärke
            Slider sldVol = newStack.FindName("sldVol" + seite + "_" + row) as Slider;
            sldVol.Value = playlisttitel.Volume;
            sldVol.Tag = row;
            sldVol.LostFocus += new RoutedEventHandler(sldVol0_0_LostFocus);
            sldVol.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldVol0_0_ValueChanged);

            //        Kontrollkasten veränderbare Lautstärke
            CheckBox chkVolMove = newStack.FindName("chkVolMove" + seite + "_" + row) as CheckBox;
            chkVolMove.Tag = row;
            chkVolMove.Click += new RoutedEventHandler(chkVolMove0_0_Click);

         //   (newStack.FindName("iboxVolMin0_" + row) as IntBox).LostFocus += new RoutedEventHandler(iboxVolMin0_0_LostFocus);
         //   (newStack.FindName("iboxVolMax0_" + row) as IntBox).LostFocus += new RoutedEventHandler(iboxVolMax0_0_LostFocus);
            
            // Schieberegler Zwischenpause
            Slider sldKlangPause = newStack.FindName("sldKlangPause" + seite + "_" + row) as Slider;
            sldKlangPause.Value = playlisttitel.Pause;
            sldKlangPause.Tag = row;
            sldKlangPause.LostFocus += new RoutedEventHandler(sldKlangPause0_0_LostFocus);
            sldKlangPause.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sldKlangPause0_0_ValueChanged);

            //        Kontrollkasten veränderbare Zwischenpause
            CheckBox chkKlangPauseMove = newStack.FindName("chkKlangPauseMove" + seite + "_" + row) as CheckBox;
            chkKlangPauseMove.Tag = row;
            chkKlangPauseMove.Click += new RoutedEventHandler(chkKlangPauseMove0_0_Click);

        //    (newStack.FindName("iboxKlangPauseMin0_" + row) as IntBox).LostFocus += new RoutedEventHandler(iboxKlangPauseMin0_0_LostFocus);
        //    (newStack.FindName("iboxKlangPauseMax0_" + row) as IntBox).LostFocus += new RoutedEventHandler(iboxKlangPauseMax0_0_LostFocus);
/*

            //        Einstellung Minimaler Lautstärkewert
            iboxVolMin.Name = "iboxVolMin" + tcKlang.SelectedIndex + "_" + Convert.ToString(row);
            iboxVolMin.Value = playlisttitel.VolumeMin;
            iboxVolMin.Tag = row;
            iboxVolMin.LostFocus += new RoutedEventHandler(iboxVolMin0_0_LostFocus);

            //        Einstellung Maximaler Lautstärkewert
            iboxVolMax.Name = "iboxVolMax" + tcKlang.SelectedIndex + "_" + Convert.ToString(row);
            iboxVolMax.Value = playlisttitel.VolumeMax;
            iboxVolMax.Tag = row;
            iboxVolMax.LostFocus += new RoutedEventHandler(iboxVolMax0_0_LostFocus);
            
                        
            //        Einstellung Minimaler Zwischenpausenwert
            iboxKlangPauseMin.Name = "iboxKlangPauseMin" + tcKlang.SelectedIndex + "_" + Convert.ToString(row);
            iboxKlangPauseMin.Value = Convert.ToInt32(playlisttitel.PauseMin);
            iboxKlangPauseMin.Tag = row;
            iboxKlangPauseMin.LostFocus += new RoutedEventHandler(iboxKlangPauseMin0_0_LostFocus);
            
            //        Einstellung Maximaler Zwischenpausenwert
            iboxKlangPauseMax.Name = "iboxKlangPauseMax" + tcKlang.SelectedIndex + "_" + Convert.ToString(row);
            iboxKlangPauseMax.Value = Convert.ToInt32(playlisttitel.PauseMax);
            iboxKlangPauseMax.Value = Convert.ToInt32(playlisttitel.PauseMax);
            iboxKlangPauseMax.Tag = row;
            iboxKlangPauseMax.LostFocus += new RoutedEventHandler(iboxKlangPauseMax0_0_LostFocus);
        */
            RowDefinition rowDef1 = new RowDefinition();
            rowDef1.Height = grdKlang.RowDefinitions[2].Height;
            grdKlang.RowDefinitions.Insert(grdKlang.RowDefinitions.Count, rowDef1);
            

            if (_player[tcKlang.SelectedIndex].Length < row + 1)
                Array.Resize(ref _player[tcKlang.SelectedIndex], _player[tcKlang.SelectedIndex].Length + 1);
                //_player[tcKlang.SelectedIndex] = new MediaPlayer[row + 1];

//            if (chkTitel.IsChecked.Value == true)
//                chkTitel0_0_Click((this.FindName("chkTitel" + tcKlang.SelectedIndex + "_" + row)), new RoutedEventArgs());
        }

        private void chkTitel0_0_Click(object sender, RoutedEventArgs e)
        {
            int seite = tcKlang.SelectedIndex;
            int zeile = Convert.ToInt32((sender as CheckBox).Name.Substring(10));
            string file = Convert.ToString((sender as CheckBox).Tag);
            StackPanel spnlZeile = ((sender as CheckBox).Parent as Grid).Parent as StackPanel;

            if (File.Exists(file) != true)
            {
                spnlZeile.Background = Brushes.Red;
                spnlZeile.ToolTip = "Datei nicht gefunden";
            }
            else
            {
                spnlZeile.Background = null;
                if ((sender as CheckBox).IsChecked.Value == true)
                {
                    if ((laufendeSongs[seite] == null) ||
                       ((this.FindName("klangsongparallel" + seite) as IntBox).Value > laufendeSongs[seite].Length))
                    {
                        _player[seite][zeile] = PlayFile(seite, zeile, _player[seite][zeile], file, (spnlZeile.FindName("sldVol" + seite + "_" + zeile) as Slider).Value / 100);
                                              
                        if (_player[seite][zeile].NaturalDuration.HasTimeSpan)
                            (spnlZeile.FindName("pbarTitel" + seite + "_" + zeile) as ProgressBar).Maximum = _player[seite][zeile].NaturalDuration.TimeSpan.TotalMilliseconds;
                        else
                            (spnlZeile.FindName("pbarTitel" + seite + "_" + zeile) as ProgressBar).Maximum = 10000;

                        if (laufendeSongs[seite] == null)
                            Array.Resize(ref laufendeSongs[seite], 1);
                        else
                            Array.Resize(ref laufendeSongs[seite], laufendeSongs[seite].Length + 1);
                        laufendeSongs[seite][laufendeSongs[seite].Length - 1] = zeile;
               
                    }
                    else
                        AddSongStandby(seite, zeile);
                }
                else
                {
                    if (_player[seite][zeile] != null)
                    {
                        _player[seite][zeile].Stop();
                        _player[seite][zeile] = null;

                        RemoveSongLaufend(seite, zeile);
                    }
                    (spnlZeile.FindName("pbarTitel" + seite + "_" + zeile) as ProgressBar).Maximum = 100;
                    (spnlZeile.FindName("pbarTitel" + seite + "_" + zeile) as ProgressBar).Value = 0;

                    RemoveSongParallel(seite, zeile);
                    CheckPlayStandbySongs(seite);
                }
                if (AnzKlangParallel.Length > 0)
                {
                    KlangProgBarTimer.IsEnabled = true;
                    KlangProgBarTimer.Start();
                }
                else
                {
                    KlangProgBarTimer.IsEnabled = false;
                    KlangProgBarTimer.Stop();
                }

                List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
                List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

                if (playlisttitel.Count != 0)
                {
                    playlisttitel[0].Aktiv = (sender as CheckBox).IsChecked.Value;
                    try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                    catch (Exception ex)
                    {
                        var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                        errWin.ShowDialog();
                        errWin.Close();
                    }
                }
            }
        }

        private void sldVol0_0_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player != null)
            {
                string s = (sender as Slider).Name.Substring(6);

                char[] Separator = new char[] { '_' };
                string[] werte = s.Split(Separator, StringSplitOptions.None);

                int seite = Convert.ToInt32(werte[0]);
                int zeile = Convert.ToInt32(werte[1]);

                if (_player[seite].Length - 1 >= zeile && _player[seite][zeile] != null)
                    _player[seite][zeile].Volume = Math.Round(e.NewValue)/100;
                (sender as Slider).ToolTip = Math.Round(e.NewValue) + " %";
            }            
        }

        private void klangsongparallel0_NumValueChanged(IntBox sender)
        {
            if (tcKlang.SelectedIndex >= 0)
            {
                CheckPlayStandbySongs(tcKlang.SelectedIndex);
                AktKlangPlaylist.MaxSongsParallel = (sender as IntBox).Value;

                try { Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void grdKlang0_Drop(object sender, DragEventArgs e)
        {
            try
            {                
                Cursor = Cursors.Wait;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] gedroppteDateien = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    string[] extension = new String[4] { ".mp3", ".wav", ".ogg", ".wma" };

                    foreach (string droppedFilePath in gedroppteDateien)
                    {
                        if (Array.IndexOf(extension, droppedFilePath.Substring(droppedFilePath.Length - 4)) != -1)
                        {
                            //erstelle ein leeres Titel-Objekt
                            Audio_Titel titel = Global.ContextAudio.New<Audio_Titel>();
                            //eigenschaften setzen
                            titel.Name = System.IO.Path.GetFileNameWithoutExtension(droppedFilePath);
                            titel.Pfad = droppedFilePath;
                            //zur datenbank hinzufügen
                            if (Global.ContextAudio.Insert<Audio_Titel>(titel))
                            {
                                Global.ContextAudio.AddTitelToPlaylist(AktKlangPlaylist, titel);

                                int seite = tcKlang.SelectedIndex;
                                zeilenAufSeite[seite]++;

                                IntBox klangsongparallel = (this.FindName("klangsongparallel" + seite)) as IntBox;
                                klangsongparallel.MaxValue = zeilenAufSeite[tcKlang.SelectedIndex];

                                List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel);

                                playlisttitel[0].VolumeChange = false;
                                playlisttitel[0].Volume = 50;
                                playlisttitel[0].VolumeMin = 0;
                                playlisttitel[0].VolumeMax = 100;

                                playlisttitel[0].PauseChange = false;
                                playlisttitel[0].Pause = 1000;
                                playlisttitel[0].PauseMin = 100;
                                playlisttitel[0].PauseMax = 10000;

                                KlangNewRow(droppedFilePath, seite, zeilenAufSeite[seite] - 1, playlisttitel[0]);
                            }
                        }
                    }
                }

            }
            finally
            {
                Cursor = Cursor;
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
                List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Audio_PlaylistGUID.Equals(AktPlaylist.Audio_PlaylistGUID)).ToList();// .Name.Equals(tboxPlaylistName.Text)).ToList();
                if (playlistliste.Count == 0)
                {
                    Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                    playlist.Name = NeuerPlaylistName;
                    playlist.Hintergrundmusik = false;
                    playlist.MaxSongsParallel = ((this.FindName("klangsongparallel" + tcKlang.SelectedIndex)) as IntBox).Value;
                    
                    //zur datenbank hinzufügen
                    if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                    {
                        List<Audio_Titel> titelMitNeuImNamen = Global.ContextAudio.TitelListe.Where(t => t.Name.StartsWith("Neu")).ToList();
                    }
                }
                else
                {
                    playlistliste[0].Name = NeuerPlaylistName;
                    if (Global.ContextAudio.Update<Audio_Playlist>(playlistliste[0]))
                    {

                    }
                }
            }
            return AktPlaylist;
        }

    /*    private void tiKlang0_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            InputTextBox.Text = Convert.ToString((this.FindName("tiKlang" + tcKlang.SelectedIndex) as TabItem).Header);
            txtblock.Text = "Neuer Name der " + Convert.ToString(tcKlang.SelectedIndex + 1) + ". Playlist:";
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void PlaylistNameOkayButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            AktKlangPlaylist = UpdatePlaylist(AktKlangPlaylist, InputTextBox.Text);
            (this.FindName("tiKlang" + tcKlang.SelectedIndex) as TabItem).Header = AktKlangPlaylist.Name;

            btnplaylistladen_Click(sender, new RoutedEventArgs());
            InputTextBox.Text = String.Empty;
        }

        private void PlaylistNameAbbruchButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            InputTextBox.Text = String.Empty;
        }

        private void InputBox_MouseLeave(object sender, MouseEventArgs e)
        {
            PlaylistNameAbbruchButton_Click(sender, new RoutedEventArgs());
        } */

      /*  private void btnplaylistspeichern_Click(object sender, RoutedEventArgs e)
        {
            //erstelle ein leeres Playlist-Objekts
            Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
            //eigenschaften setzen
            playlist.Name = Convert.ToString(((this.FindName("tiKlang" + tcKlang.SelectedIndex)) as TabItem).Header);
            playlist.Hintergrundmusik = false;
            playlist.MaxSongsParallel = ((this.FindName("klangsongparallel" + tcKlang.SelectedIndex)) as IntBox).Value;

            //zur datenbank hinzufügen
            if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))
            {
                //erfolgreich hinzugefügt
                //nun kann man verschiedene titel zB. so holen
                List<Audio_Titel> titelMitNeuImNamen = Global.ContextAudio.TitelListe.Where(t => t.Name.StartsWith("Neu")).ToList();
            }
        }
        */
        private void btnplaylistladen_Click(object sender, RoutedEventArgs e)
        {
            if (tcAudioPlayer.SelectedIndex == 0)
                AktualisiereHintergrundPlaylist();
            else
                if (tcAudioPlayer.SelectedIndex == 1)
                    AktualisiereKlangPlaylist();

            Grid grdKlangPlaylistInfo = this.FindName("grdKlangPlaylistInfo" + tcKlang.SelectedIndex) as Grid;

            for (int i = 0; i <= grdKlangPlaylistInfo.Children.Count-1; i++)
                grdKlangPlaylistInfo.Children[i].Visibility = Visibility.Hidden;
        }

        private void AktualisiereHintergrundPlaylist()
        {
            lbBackground.Items.Clear();
            List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.ToList();
            for (int i = 0; i < playlistliste.Count; i++)
            {
                if (playlistliste[i].Hintergrundmusik)
                {
                    ListBoxItem lbitem = new ListBoxItem();
                    lbitem.Name = "lb" + playlistliste[i].Name;
                    lbitem.Tag = playlistliste[i].Audio_PlaylistGUID;
                    lbitem.Content = playlistliste[i].Name;
                    lbBackground.Items.Add(lbitem);
                }
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
                    lbitem.Name = "lb" + playlistliste[i].Name;
                    lbitem.Tag = playlistliste[i].Audio_PlaylistGUID;
                    lbitem.Content = playlistliste[i].Name;
                    lbKlang.Items.Add(lbitem);
                }
                if ((playlistliste[i].Hintergrundmusik == false) && (rbKlangAlle.IsChecked == true || rbKlangKlang.IsChecked == true))
                {
                    ListBoxItem lbitem = new ListBoxItem();
                    lbitem.Name = "lb" + playlistliste[i].Name;
                    lbitem.Tag = playlistliste[i].Audio_PlaylistGUID;
                    lbitem.Content = playlistliste[i].Name;
                    lbKlang.Items.Add(lbitem);
                }
            }
        }


        private void btnneutitel_Click(object sender, RoutedEventArgs e)
        {
            //erstelle ein leeres Playlist-Objekts
            Audio_Titel titel = Global.ContextAudio.New<Audio_Titel>();
       //     Audio_Playlist_Titel playlisttitel = Global.ContextAudio.New<Audio_Playlist_Titel>();

            List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(tboxPlaylistName.Text)).ToList();
            if (playlistliste.Count == 0)
            {
                Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                playlist.Name = Convert.ToString(((this.FindName("tiKlang" + tcKlang.SelectedIndex)) as TabItem).Header);
                playlist.Hintergrundmusik = false;
                playlist.MaxSongsParallel = ((this.FindName("klangsongparallel" + tcKlang.SelectedIndex)) as IntBox).Value;

                //zur datenbank hinzufügen
                if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                {                    
                    //nun kann man verschiedene titel zB. so holen
                    List<Audio_Titel> titelMitNeuImNamen = Global.ContextAudio.TitelListe.Where(t => t.Name.StartsWith("Neu")).ToList();
                }
            }
            else
            {
                Audio_Playlist playlist = playlistliste[0];                
            }
            //eigenschaften abfragen
            textBox5.Text = Convert.ToString(titel.Audio_TitelGUID);            
        }

        private void bntNeuePlaylist_Click(object sender, RoutedEventArgs e)
        {
            string NeuePlaylist = "NeuePlayliste" + tcKlang.SelectedIndex;
            List<Audio_Playlist> playlistliste = Global.ContextAudio.PlaylistListe.Where(t => t.Name.Equals(NeuePlaylist)).ToList();
            if (playlistliste.Count == 0)
            {
                Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                playlist.Name = Convert.ToString(NeuePlaylist);
                if (rbKlangKlang.IsChecked.Value)
                    playlist.Hintergrundmusik = false;
                else
                    playlist.Hintergrundmusik = true;
                playlist.MaxSongsParallel = ((this.FindName("klangsongparallel" + tcKlang.SelectedIndex)) as IntBox).Value;

                //zur datenbank hinzufügen
                if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                {
                    AktualisiereKlangPlaylist();
                    //nun kann man verschiedene titel zB. so holen
          //          List<Audio_Titel> titelMitNeuImNamen = Global.ContextAudio.TitelListe.Where(t => t.Name.StartsWith("Neu")).ToList();
                }
            }
            else
            {
                Audio_Playlist playlist = playlistliste[0];
            }
        }

        private void tiHintergrund_Loaded(object sender, RoutedEventArgs e)
        {
            if (lbBackground.Items.Count == 0)
                AktualisiereHintergrundPlaylist();
        }

        private void tiKlang_Loaded(object sender, RoutedEventArgs e)
        {
            if (lbKlang.Items.Count == 0)
                AktualisiereKlangPlaylist();
        }

        private void lbhintergrundtitellist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((lbhintergrundtitellist.SelectedIndex >= 0) && 
               ((lbhintergrundtitellist.SelectedItem as ListBoxItem).Background != Brushes.Red))
            {
                ListBoxItem lbItem = (lbhintergrundtitellist.SelectedItem as ListBoxItem);
                string st = Convert.ToString(lbItem.Tag);

                List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktHintergrundPlaylist);

                if (titel.Count == 0)
                {
                    if (!File.Exists(titel[lbhintergrundtitellist.SelectedIndex].Pfad))
                        lbItem.Background = Brushes.Red;
                    lbItem.ToolTip = "Datei nicht gefunden";

                    lbBackground_SelectionChanged(lbhintergrundtitellist, e);
                }
                else
                {
                    if (!File.Exists(titel[lbhintergrundtitellist.SelectedIndex].Pfad))
                    {
                        lbItem.Background = Brushes.Red;
                        lbItem.ToolTip = "Datei nicht gefunden";
                        SpieleNeuenHintergrundTitel();
                    }
                    else
                    {
                        if (HintergrundPlayer != null)
                        {
                            HintergrundPlayer.Stop();
                            HintergrundProgBarTimer.Stop();
                            HintergrundPlayer = null;
                        }
                        lblBgTimeMax.Content = "-:--";
                        lblBgTitel.Content = "";
                        lblBgAlbum.Content = "";
                        lblBgArtist.Content = "";
                        lblBgJahr.Content = "";
                        lblBgGenre.Content = "";
                        HintergrundPlayer = PlayFile(-1, 0, HintergrundPlayer, titel[lbhintergrundtitellist.SelectedIndex].Pfad, slBGVolume.Value / 100);

                        if (HintergrundPlayer != null)
                        {
                         //   btnBGAbspielen.Tag = 1;
                            btnBGAbspielen.Content = "Stoppen";

                            pbarBGSong.Value = 0;
                            string s = "0:00";
                            if (HintergrundPlayer.NaturalDuration.HasTimeSpan)
                            {
                                pbarBGSong.Maximum = HintergrundPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                                if (HintergrundPlayer.NaturalDuration.TimeSpan.Minutes < 10)
                                    s = "0" + HintergrundPlayer.NaturalDuration.TimeSpan.Minutes + ":";
                                else
                                    s = HintergrundPlayer.NaturalDuration.TimeSpan.Minutes + ":";

                                if (HintergrundPlayer.NaturalDuration.TimeSpan.Seconds < 10)
                                    s = s + "0" + HintergrundPlayer.NaturalDuration.TimeSpan.Seconds;
                                else
                                    s = s + HintergrundPlayer.NaturalDuration.TimeSpan.Seconds;
                                lblBgTimeMax.Content = s;
                            }
                            btnBGNext.IsEnabled = true;
                            btnBGAbspielen.IsEnabled = true;
                            grdSongInfo.Visibility = Visibility.Visible;

                            ListBoxItem lbi = (lbBackground.SelectedItem as ListBoxItem);
                            HintergrundProgBarTimer.Tag = -1;
                            HintergrundProgBarTimer.Start();
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
            {
                throw new ArgumentException("Aruments out of range");
            }

            //Länge des zu betrachtenden Ausschnittes
            int length = pos2 - pos1 + 1;

            //neues Char-Array anlegen der Länge length
            Char[] chars = new Char[length];

            //packe alle Bytes von pos1 bis pos2 als
            //Char konvertiert in Array chars
            for (int i = 0; i < length; i++)
            {
                chars[i] = Convert.ToChar(bytes[i + pos1]);
            }//end for

            //konvertiere Char-Array in String und gebe es zurück
            String s = new String(chars);
            s = s.Replace("\0","");

            return s; // new String(s);
        }
        
        private void HintergrundProgBarTimer_Tick(object sender, EventArgs e)
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

            if ("".Equals(lblBgTitel.Content))
            {
                FileInfo file = new FileInfo(HintergrundPlayer.Source.LocalPath);
                Stream str = file.OpenRead();

                byte[] bytes = new byte[128];
                str.Seek(-128, SeekOrigin.End);
                int numBytesToRead = 128;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = str.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                    {
                        break;
                    }
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                str.Close();

                String tag = ConvertByteToString(bytes, 0, 2);
                if (tag != "TAG")
                {
                    lblBgTitel.Content = System.IO.Path.GetFileNameWithoutExtension(HintergrundPlayer.Source.LocalPath);
                    lblBgArtist.Content = "---";
                    lblBgAlbum.Content = "---";
                    lblBgJahr.Content = "---";
                    lblBgGenre.Content = "---";
                }
                else
                {
                    lblBgTitel.Content = ConvertByteToString(bytes, 3, 32);
                    lblBgArtist.Content = ConvertByteToString(bytes, 33, 62);
                    lblBgAlbum.Content = ConvertByteToString(bytes, 63, 92);
                    lblBgJahr.Content = ConvertByteToString(bytes, 93, 96);
                    //m_comment = ConvertByteToString(bytes, 97, 126);
                    int z = Convert.ToInt32(bytes[127]);
                    if (z <= _genres.Length-1)
                        lblBgGenre.Content = _genres[z];
                }
            }

            if ("-:--".Equals(lblBgTimeMax.Content))
            {
                if (HintergrundPlayer.NaturalDuration.HasTimeSpan)
                {
                    string t = "0:00";
                    pbarBGSong.Maximum = HintergrundPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                    if (HintergrundPlayer.NaturalDuration.TimeSpan.Minutes < 10)
                        t = "0" + HintergrundPlayer.NaturalDuration.TimeSpan.Minutes + ":";
                    else
                        t = HintergrundPlayer.NaturalDuration.TimeSpan.Minutes + ":";

                    if (HintergrundPlayer.NaturalDuration.TimeSpan.Seconds < 10)
                        t = t + "0" + HintergrundPlayer.NaturalDuration.TimeSpan.Seconds;
                    else
                        t = t + HintergrundPlayer.NaturalDuration.TimeSpan.Seconds;
                    lblBgTimeMax.Content = t;
                }
            }

            pbarBGSong.Value = HintergrundPlayer.Position.TotalMilliseconds;
            string s = "";
            if (HintergrundPlayer.Position.Minutes < 10)
                s = "0" + HintergrundPlayer.Position.Minutes + ":";
            else
                s = HintergrundPlayer.Position.Minutes + ":";

            if (HintergrundPlayer.Position.Seconds < 10)
                s = s + "0" + HintergrundPlayer.Position.Seconds;
            else
                s = s + HintergrundPlayer.Position.Seconds;
            lblBgTimeActual.Content = s;

            if ((HintergrundPlayer.NaturalDuration.HasTimeSpan &&
                 HintergrundPlayer.Position.TotalMilliseconds == HintergrundPlayer.NaturalDuration.TimeSpan.TotalMilliseconds) )
            //||
            //    HintergrundPlayer.Position.TotalMilliseconds == 0) 
            {
                if (HintergrundPlayer != null)
                {
                    HintergrundPlayer.Stop();
                    HintergrundPlayer = null;
                } 
                HintergrundProgBarTimer.Stop();
                SpieleNeuenHintergrundTitel();
            }
            else
        /*     if  ((HintergrundPlayer.NaturalDuration.HasTimeSpan == false &&
                 HintergrundPlayer.Position.TotalMilliseconds == Convert.ToDouble(HintergrundProgBarTimer.Tag)))
                 {
                HintergrundProgBarTimer.Stop();
                SpieleNeuenHintergrundTitel();
            }
          */  
                HintergrundProgBarTimer.Tag = HintergrundPlayer.Position.TotalMilliseconds;
        }

        private void sldVol0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as Slider).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].Volume = Convert.ToInt32(Math.Round((sender as Slider).Value));
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void chkVolMove0_0_Click(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as CheckBox).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].VolumeChange = (sender as CheckBox).IsChecked.Value;
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void iboxVolMin0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as IntBox).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].VolumeMin = (sender as IntBox).Value;
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void iboxVolMax0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as IntBox).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].VolumeMax = (sender as IntBox).Value;
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void sldKlangPause0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as Slider).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);
            
            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].Pause = Convert.ToInt32(Math.Round((sender as Slider).Value));
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void chkKlangPauseMove0_0_Click(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as CheckBox).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].PauseChange = (sender as CheckBox).IsChecked.Value;
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void iboxKlangPauseMin0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as IntBox).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].PauseMin = (sender as IntBox).Value;
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void iboxKlangPauseMax0_0_LostFocus(object sender, RoutedEventArgs e)
        {
            int zeile = Convert.ToInt32((sender as IntBox).Tag);
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            List<Audio_Playlist_Titel> playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(AktKlangPlaylist, titel[zeile]);

            if (playlisttitel.Count != 0)
            {
                playlisttitel[0].PauseMax = (sender as IntBox).Value;
                try { Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel[0]); }
                catch (Exception ex)
                {
                    var errWin = new MsgWindow("Datenfehler", "Die Datenbank konnte nicht aktualisiert werden", ex);
                    errWin.ShowDialog();
                    errWin.Close();
                }
            }
        }

        private void sldKlangPause0_0_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (sender as Slider).ToolTip = Math.Round(e.NewValue) + " ms";
        }

        private void imgTrash0_0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            List<Audio_Titel> titel = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
            Grid grdKlang = this.FindName("grdKlang" + tcKlang.SelectedIndex) as Grid;

            for (int i=0; i <= titel.Count-1; i++)
                if (titel[i].Audio_TitelGUID.Equals((sender as Image).Tag))
                {
                    CheckBox chbx = (grdKlang.FindName("spnlKlangRow" + tcKlang.SelectedIndex + "_" + i) as StackPanel).FindName("chkTitel" + tcKlang.SelectedIndex + "_" + i) as CheckBox;
                    if (chbx.IsChecked.Value == true)
                    {                    
                        chbx.IsChecked = false;
                        chkTitel0_0_Click(chbx, new RoutedEventArgs());
                    }
                    Global.ContextAudio.RemoveTitelFromPlaylist(AktKlangPlaylist, titel[i]);      
               //     lbKlang_SelectionChanged(lbKlang, new SelectionChangedEventArgs(null,null,null));
                }
        }

        private void rbKlangKlang_Click(object sender, RoutedEventArgs e)
        {
            btnplaylistladen_Click(sender, e);
            btnNeuePlaylist.Visibility = Visibility.Visible;
        }

        private void rbKlangAlle_Click(object sender, RoutedEventArgs e)
        {
            btnplaylistladen_Click(sender, e);
            btnNeuePlaylist.Visibility = Visibility.Hidden;
        }

        private void rbIstKlangPlaylist_Click(object sender, RoutedEventArgs e)
        {
            AktKlangPlaylist.Hintergrundmusik = false;
            Global.ContextAudio.Save();
        }
        
        private void rbIstMusikPlaylist_Click(object sender, RoutedEventArgs e)
        {
            AktKlangPlaylist.Hintergrundmusik = true;
            Global.ContextAudio.Save();
        }

        private void tboxPlaylistName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AktKlangPlaylist.Name = tboxPlaylistName.Text;
                Global.ContextAudio.Save();                
                (lbKlang.Items[lbKlang.SelectedIndex] as ListBoxItem).Content = AktKlangPlaylist.Name;
                (tcKlang.SelectedItem as TabItem).Header = AktKlangPlaylist.Name;
            }
        }


        private void pbarBGSong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pts = e.GetPosition(pbarBGSong);
            double total = pbarBGSong.Maximum;
            double res = ((pts.X * 100) / ((double)pbarBGSong.ActualWidth)) / 100;
            HintergrundPlayer.Position = TimeSpan.FromMilliseconds(total * res);          
        }        
    }
}