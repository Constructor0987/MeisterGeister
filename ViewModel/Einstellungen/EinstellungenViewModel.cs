using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ImpromptuInterface;
using MeisterGeister.Model.Extensions;


using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.ColorConverters.OriginalWithModel;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.Models.Bridge;
using System.Windows.Threading;
using MeisterGeister.View.Settings;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace MeisterGeister.ViewModel.Settings
{

    public class EinstellungenViewModel : Base.ViewModelBase
    {
        //public class LightCommand
        //{
        //    public Light Light { get; set; }
        //    public LightCommand LightCmd { get; set; }
        //}

        public class LightProcess
        {
            public bool IsSelected { get; set; }
            public int Phase { get; set; }
            public double Dauer { get; set; }
            public Color Color { get; set; }
            public int? Brightness { get; set; }
        }

        public class HUETheme
        {
            public string Name { get; set; }
            public LightProcess LightProcessSelected { get; set; } 
            public List<LightProcess> lstLightProcess { get; set; }
            public List<Light> lstLights { get; set; } 
            public bool doLoop { get; set; }

            public bool isRunning { get; set; }
            public int? actLightProcess { get; set; }
            public int StartTime { get; set; }

            public DispatcherTimer _timer = new DispatcherTimer();
            public bool InitDone = false;

            public EinstellungenViewModel vm;
            //LightCommand command = new LightCommand();
            
            public LightCommand command { get; set; }

            private async void regHUE()
            {
                vm.appKey = await vm.Client.RegisterAsync("MGmeetsHUE", "PC");
            }

            private Light _addLightToTheme = null;
            public Light AddLightToTheme
            {
                get { return _addLightToTheme; }
                set
                {
                    _addLightToTheme = value;
                    List<Light> lst = new List<Light>();
                    if (lstLights != null) lst.AddRange(lstLights);
                    if (!lst.Contains(value))
                        lst.Add(value);
                    else
                        lst.Remove(value);
                    lstLights = lst;
                }
            }
            

            public void _timer_Tick(object sender, EventArgs e)
            {
                if (actLightProcess == null)
                {
                    actLightProcess = 0;
                    StartTime = Environment.TickCount;

                    //Control the lights
                    command = new LightCommand();
                    //command.On = true;
                    
                    //Turn the light on and set a Hex color for the command (see the section about Color Converters)
                    command.TurnOn().SetColor(new RGBColor(
                        lstLightProcess[actLightProcess.Value].Color.R, 
                        lstLightProcess[actLightProcess.Value].Color.G,
                        lstLightProcess[actLightProcess.Value].Color.B));
                    //Helligkeit ?
                    command.TurnOn().Brightness = (byte)this.lstLightProcess[actLightProcess.Value].Brightness;                    
                    //Or start a colorloop
                    //command.Effect = Q42.HueApi.Effect.ColorLoop;
                    command.Effect = Q42.HueApi.Effect.None;
                    //Once you have composed your command, send it to one or more lights
                    vm.Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());

                    return;
                }
                

                int tDelta = Environment.TickCount - StartTime;
                if (tDelta > lstLightProcess[actLightProcess.Value].Dauer)
                {
                    StartTime = Environment.TickCount;
                    if (actLightProcess.Value +1 < lstLightProcess.Count)
                    { actLightProcess++; }
                    else
                        if (doLoop)
                    { actLightProcess = 0; }
                    else
                    { _timer.Stop(); return; }
                                    
                    //Control the lights
                    command = new LightCommand();
                    command.On = true;
                    //Turn the light on and set a Hex color for the command (see the section about Color Converters)
                    command.TurnOn().SetColor(new RGBColor(
                        lstLightProcess[actLightProcess.Value].Color.R,
                        lstLightProcess[actLightProcess.Value].Color.G,
                        lstLightProcess[actLightProcess.Value].Color.B));
                    //Helligkeit ?
                    command.TurnOn().Brightness = (byte)lstLightProcess[actLightProcess.Value].Brightness;
                    //Or start a colorloop
                    //command.Effect = Q42.HueApi.Effect.ColorLoop;
                    command.Effect = Q42.HueApi.Effect.None;
                    //Once you have composed your command, send it to one or more lights
                    vm.Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());
                }

                return;
                    //    if (!InitDone)
                    //        DoInit();

                    //Or send it to all lights
                    //  vm.Client.SendCommandAsync(command);

                    //List<string> deviceIds = new List<string>();
                    ////Search for new lights
                    //await vm.Client.SearchNewLightsAsync(vm.deviceIds);

                    ////Get all lights
                    //var resultLights = await vm.Client.GetLightsAsync();

                    //Control the lights
                    command = new LightCommand();
                command.On = true;

                //Turn the light on and set a Hex color for the command (see the section about Color Converters)
                command.TurnOn().SetColor(new RGBColor(
                        this.lstLightProcess[this.actLightProcess.Value].Color.R,
                        this.lstLightProcess[this.actLightProcess.Value].Color.G,
                        this.lstLightProcess[this.actLightProcess.Value].Color.B));
                
                //Helligkeit ?
                command.TurnOn().Brightness = (byte) lstLightProcess[0].Brightness;

                //Blink once
                //   command.Alert = Alert.Once;

                //Or start a colorloop
                //command.Effect = Q42.HueApi.Effect.ColorLoop;
                command.Effect = Q42.HueApi.Effect.None;

                //Once you have composed your command, send it to one or more lights
                vm.Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());

                //ToAll();

                _timer.Stop();     
            }
            
            private async void ToAll()
            {
                //Or send it to all lights
                var result = await vm.Client.SendCommandAsync(command);
            }

            public HUETheme()
            {
                _timer.Tick += new EventHandler(_timer_Tick);
                _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            }
        }

        private HUETheme _HUEThemeSelected;
        public HUETheme HUEThemeSelected
        {
            get { return _HUEThemeSelected; }
            set { Set(ref _HUEThemeSelected, value); }
        }

        private List<HUETheme> _lstHUEThemes = new List<HUETheme>();

      //  [DependentProperty("HUEThemeSelected")]
        public List<HUETheme> lstHUEThemes
        {
            get { return _lstHUEThemes; }
            set { Set(ref _lstHUEThemes, value); }
        }

        private string _HUEProgress = "";
        public string HUEProgress
        {
            get { return _HUEProgress; }
            set { Set(ref _HUEProgress, value); }
        }

        private LocatedBridge _HUEGWSelected = null;
        public LocatedBridge HUEGWSelected
        {
            get { return _HUEGWSelected; }
            set { Set(ref _HUEGWSelected, value); }
        }

        private List<LocatedBridge> _lstHUEGaterways = new List<LocatedBridge>();
        public List<LocatedBridge> lstHUEGateways
        {
            get { return _lstHUEGaterways; }
            set { Set(ref _lstHUEGaterways, value); }
        }

        
        public LocalHueClient Client = null;
        string appKey = "H3ZWpfbrmLp3-Fx3AuMT-sqhyt51Q2a1IYFdKefQ";

        private async void _Init()
        {
            try
            {
                //m_Initialized = false;
                HUEProgress = "Q42Hue attempting to initialize ..";

                IBridgeLocator locator = new HttpBridgeLocator();

                var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

                List<LocatedBridge> lstGW = new List<LocatedBridge>();
                foreach (var bridgeIp in bridgeIPs)
                {
                    lstGW.Add(bridgeIp);
                    HUEProgress = bridgeIp + ": " + bridgeIp.IpAddress;
                }
                lstHUEGateways = lstGW;
                if (bridgeIPs.Count() > 0)
                {
                    HUEProgress = bridgeIPs.Count() + " Gateway" + (bridgeIPs.Count() == 1 ? "s" : "") + " gefunden";
                    HUEGWSelected = lstHUEGateways[0];                    
                }
                else
                {
                    HUEProgress = "Q42Hue Error: Could not find bridge!";
                }
            }
            catch (Exception ex)
            {
                View.General.ViewHelper.ShowError(string.Format("Die Suche der HUE-Gateways über das Netzwerk konnte nicht durchgeführt werden"), ex);
            }
        }

        private List<string> _lstDeviceID = new List<string>();
        public List<string> lstDeviceID
        {
            get { return _lstDeviceID; }
            set { Set(ref _lstDeviceID, value); }
        }

        private List<LightCommand> _lstHUELightCmd = new List<LightCommand>();
        public List<LightCommand> lstHUELightCmd
        {
            get { return _lstHUELightCmd; }
            set { Set(ref _lstHUELightCmd, value); }
        }

        private List<Light> _lstHUELights = new List<Light>();
        public List<Light> lstHUELights
        {
            get { return _lstHUELights; }
            set
            {
                Set(ref _lstHUELights, value);
                if (value.Count > 0)
                    SelectedHUELight = value[0];
                List<LightCommand> lstLightCmd = new List<LightCommand>();
                //lstHUELights.ForEach(q => lstLightCmd.Add(new LightCommand() { Light = q, LightCmd = new LightCommand() }));
                lstHUELightCmd = lstLightCmd;
            }
        }

        private Light _selectedHUELight = new Light();
        public Light SelectedHUELight
        {
            get { return _selectedHUELight; }
            set { Set(ref _selectedHUELight, value); }
        }

        private async void _ActivateHUE()
        {
            string ip = HUEGWSelected.IpAddress.ToString();

            Client = new LocalHueClient(ip);
            if (appKey == null)
            {
                //Register your application
                //Link button drücken zum Registrieren !!!
                appKey = await Client.RegisterAsync("MGmeetsHUE", "PC");
                // "H3ZWpfbrmLp3-Fx3AuMT-sqhyt51Q2a1IYFdKefQ"
                //Save the app key for later use
            }
            else
            {
                //If you already registered an appname, you can initialize the HueClient with the app's key:
                Client.Initialize(appKey);
            }

            //Search for new lights
            await Client.SearchNewLightsAsync(lstDeviceID);

            //Get all lights
            var resultLights = await Client.GetLightsAsync();


            lstHUELights = resultLights as List<Light>;
        }

        #region Property

        private Base.CommandBase _onBtnSelectHUEColor = null;
        public Base.CommandBase onBtnSelectHUEColor
        {
            get
            {
                if (_onBtnSelectHUEColor == null)
                    _onBtnSelectHUEColor = new Base.CommandBase(SelectHUEColor, null);
                return _onBtnSelectHUEColor;
            }
        }
        
        private void SelectHUEColor(object obj)
        {
            if (HUEThemeSelected == null)
                HUEThemeSelected = lstHUEThemes[0];

            HUEColorDialog colorDialog = new HUEColorDialog();
            colorDialog.colorPicker.Client = Client;
            colorDialog.SelectedColor = HUEThemeSelected.LightProcessSelected.Color;
         //   colorDialog.SelectedColor = (SolidColorBrush)HUEThemeSelected.LightProcessSelected.Color);//, HUEThemeSelected.LightProcessSelected.Color.G, HUEThemeSelected.LightProcessSelected.Color.B);//((SolidColorBrush))// HUEThemeSelected.lstLightProcess[0].Color.ToString();// this.RectColorPicked.Fill).Color;
            //colorDialog.Owner =  this;
            if ((bool)colorDialog.ShowDialog())
            {
                HUETheme aktTheme = new Settings.EinstellungenViewModel.HUETheme();
                aktTheme = HUEThemeSelected;
                HUEThemeSelected.LightProcessSelected.Color = colorDialog.SelectedColor;

                List<HUETheme> lst = new List<Settings.EinstellungenViewModel.HUETheme>();
                lst.AddRange(lstHUEThemes);
                lstHUEThemes = lst;
                //RectColorPicked.Fill = new SolidColorBrush(colorDialog.SelectedColor);
                //SendColorToLamps(colorDialog.SelectedColor);
            }

        }

        private Base.CommandBase _onBtnDoTheme = null;
        public Base.CommandBase onBtnDoTheme
        {
            get
            {
                if (_onBtnDoTheme == null)
                    _onBtnDoTheme = new Base.CommandBase(DoTheme, null);
                return _onBtnDoTheme;
            }
        }

        List<HUETheme> RunningThemes = new List<HUETheme>();

        private void DoTheme(object obj)
        {
            HUETheme ThemeToDo = obj as HUETheme;
            if (obj == null) return;

            if (!ThemeToDo.isRunning)
            {
                ThemeToDo.vm = this;
                ThemeToDo.actLightProcess = null;
                ThemeToDo.StartTime = 0;

                ThemeToDo._timer.Start();
            }
            else
            {
                ThemeToDo._timer.Stop();
            }            
        }


        private Base.CommandBase _onbtnNeuesHUETheme = null;
        public Base.CommandBase onbtnNeuesHUETheme
        {
            get
            {
                if (_onbtnNeuesHUETheme == null)
                    _onbtnNeuesHUETheme = new Base.CommandBase(NeuesHUETheme, null);
                return _onbtnNeuesHUETheme;
            }
        }

        private void NeuesHUETheme(object obj)
        {
            List<HUETheme> lst = new List<HUETheme>();
            lst.AddRange(lstHUEThemes);
            lst.Add(new HUETheme()
            {
                Name = "Neues_HUE",
                doLoop = true,
                lstLightProcess = new List<LightProcess>() {
                    new LightProcess() { Brightness= 100, Color = Colors.Red, Dauer= 2000, Phase=0 },
                    new LightProcess() { Brightness= 100, Color = Colors.Yellow, Dauer= 2000, Phase=1 },
                    new LightProcess() { Brightness= 100, Color = Colors.Green, Dauer= 2000, Phase=2 } }
            });
            lstHUEThemes = lst;
        }

        private Base.CommandBase _onTBtnThemeProcessColor = null;
        public Base.CommandBase onTBtnThemeProcessColor
        {
            get
            {
                if (_onTBtnThemeProcessColor == null)
                    _onTBtnThemeProcessColor = new Base.CommandBase(TBtnThemeProcessColor, null);
                return _onTBtnThemeProcessColor;
            }
        }

        private void TBtnThemeProcessColor(object obj)
        {
            if (((LightProcess)obj).IsSelected)
            {
                HUEThemeSelected.LightProcessSelected = (LightProcess)obj;
                //lstHUEThemes.ForEach(delegate(HUETheme ht) { ht.lstLightProcess.ForEach(delegate (LightProcess lp) { if (lp != obj) lp.IsSelected = false; }); });
            }
        }

        private Base.CommandBase _onbtnAddHUEProcess = null;
        public Base.CommandBase onbtnAddHUEProcess
        {
            get
            {
                if (_onbtnAddHUEProcess == null)
                    _onbtnAddHUEProcess = new Base.CommandBase(AddHUEProcess, null);
                return _onbtnAddHUEProcess;
            }
        }

        private void AddHUEProcess(object obj)
        {
            List<HUETheme> lstThemes = new List<HUETheme>();
            lstThemes.AddRange(lstHUEThemes);

            List<LightProcess> lst = new List<LightProcess>();
            HUEThemeSelected = lstHUEThemes[lstHUEThemes.Count-1];
            if (HUEThemeSelected.lstLightProcess != null) lst.AddRange(HUEThemeSelected.lstLightProcess);
            lst.Add(new LightProcess() { Phase = lst.Count, Dauer = 5000, Color = Colors.AliceBlue, Brightness =255 });

            ((HUETheme)obj).lstLightProcess = lst;
            lstHUEThemes = lstThemes;
        }

        private Base.CommandBase _onBtnHUEGWsuchen = null;
        public Base.CommandBase onBtnHUEGWsuchen
        {
            get
            {
                if (_onBtnHUEGWsuchen == null)
                    _onBtnHUEGWsuchen = new Base.CommandBase(HUEGWsuchen, null);
                return _onBtnHUEGWsuchen;
            }
        }

        private void HUEGWsuchen(object obj)
        {
            _Init();
        }

        private Base.CommandBase _onBtnActivateHUEGW = null;
        public Base.CommandBase onBtnActivateHUEGW
        {
            get
            {
                if (_onBtnActivateHUEGW == null)
                    _onBtnActivateHUEGW = new Base.CommandBase(ActivateHUEGW, null);
                return _onBtnActivateHUEGW;
            }
        }

        private void ActivateHUEGW(object obj)
        {
            _ActivateHUE();            
        }


        public string Regeledition
        {
            get { return Global.Regeledition; }
            set { Global.Regeledition = value; }
        }

        private Base.CommandBase _onSetRegeledition = null;
        public Base.CommandBase OnSetRegeledition
        {
            get
            {
                if (_onSetRegeledition == null)
                    _onSetRegeledition = new Base.CommandBase(SetRegeledition, null);
                return _onSetRegeledition;
            }
        }

        private void SetRegeledition(object obj)
        {
            View.Windows.RegeleditionWindow regWin = new View.Windows.RegeleditionWindow();
            regWin.Owner = System.Windows.Application.Current.MainWindow;
            bool? dlgResult = regWin.ShowDialog();
            regWin = null;
            if (dlgResult == true)
                System.Windows.Application.Current.Shutdown();
        }

        public Boolean IsAudioSpieldauerBerechnen
        {
            get { return MeisterGeister.Logic.Einstellung.Einstellungen.AudioSpieldauerBerechnen; }
            set
            {
                MeisterGeister.Logic.Einstellung.Einstellungen.AudioSpieldauerBerechnen = value;
                OnChanged("IsAudioSpieldauerBerechnen");
            }
        }

        public Boolean IsInAnderemPfadSuchen
        {
            get { return MeisterGeister.Logic.Einstellung.Einstellungen.AudioInAnderemPfadSuchen; }
            set
            {
                MeisterGeister.Logic.Einstellung.Einstellungen.AudioInAnderemPfadSuchen = value;
                OnChanged("IsInAnderemPfadSuchen");
            }
        }
        
        public Boolean IsShowPlaylistFavorite
        {
            get { return MeisterGeister.Logic.Einstellung.Einstellungen.ShowPlaylistFavorite; }
            set
            {
                MeisterGeister.Logic.Einstellung.Einstellungen.ShowPlaylistFavorite = value;
                OnChanged("IsShowPlaylistFavorite");
            }
        }

        public Boolean IsMitUeberlastung
        {
            get { return MeisterGeister.Logic.Einstellung.Einstellungen.IsMitUeberlastung; }
            set
            {
                MeisterGeister.Logic.Einstellung.Einstellungen.IsMitUeberlastung = value;
                OnChanged("IsMitUeberlastung");
            }
        }

        private List<EinstellungItem> einstellungListe;
        public List<EinstellungItem> EinstellungListe
        {
            get { return einstellungListe; }
            set
            {
                einstellungListe = value;
                OnChanged("EinstellungListe");
            }
        }

        public ermittleRuestung BerechnungRuestung
        {
            get { return (ermittleRuestung)MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung; }
            set
            {        
                switch (value)
                {
                    case ermittleRuestung.AutomatischZonen:
                        MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;
                    case ermittleRuestung.Einfach:
                        MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;
                    case ermittleRuestung.Zonen:
                        MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;
                    case ermittleRuestung.AutomatischEinfach:
                        MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;
                    default:
                        return;
                }
                OnChanged("BerechnungRuestung");
            }
        }

        public ermittleBehinderung BerechnungBehinderung
        {
            get { return (ermittleBehinderung)MeisterGeister.Logic.Einstellung.Einstellungen.BEBerechnung; }
            set
            {
                switch (value)
                {
                    case ermittleBehinderung.Automatisch:
                        MeisterGeister.Logic.Einstellung.Einstellungen.BEBerechnung = (int)value;
                        break;
                    case ermittleBehinderung.Eingabe:
                        MeisterGeister.Logic.Einstellung.Einstellungen.BEBerechnung = (int)value;
                        break;
                    default:
                        return;
                }
                OnChanged("BerechnungBehinderung");
            }
        }

        public ermitteleUeberlastung BerechnungUeberlastung
        {
            get { return (ermitteleUeberlastung)MeisterGeister.Logic.Einstellung.Einstellungen.UeberlastungBerechnung; }
            set
            {
                switch (value)
                {
                    case ermitteleUeberlastung.Automatisch:
                        MeisterGeister.Logic.Einstellung.Einstellungen.UeberlastungBerechnung = (int)value;
                        break;
                    case ermitteleUeberlastung.Eingabe:
                        MeisterGeister.Logic.Einstellung.Einstellungen.UeberlastungBerechnung = (int)value;
                        break;
                    default:
                        return;
                }
                OnChanged("BerechnungUeberlastung");
            }
        }        


        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> InventarListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Inventar").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<String> KontextListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Select(e => e.Kontext).Distinct().ToList();
            }
            set { KontextListe = value; }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> AllgemeinListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Allgemein").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> ProbenListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Proben").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> KampfListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Kampf").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> AudioplayerListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Audioplayer").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> AlmanachListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Almanach").ToList();
            }
        }

        public List<string> PDFReaders
        {
            get 
            { 
                return Logic.General.Pdf.readers.Keys.ToList<string>(); 
            }
        }

        private string _selectedPDFReader;
        public string SelectedPDFReader
        {
            get
            {
                return _selectedPDFReader;
            }
            set
            {
                _selectedPDFReader = value;
                Logic.Einstellung.Einstellungen.PdfReaderCommand = Logic.General.Pdf.readers[value][0];
                Logic.Einstellung.Einstellungen.PdfReaderArguments = Logic.General.Pdf.readers[value][1];
                Logic.General.Pdf.SetReader(value);
            }
        }

        private List<Model.Setting> settingListe;
        [DependentProperty("EinstellungListe")]
        public List<Model.Setting> SettingListe
        {
            get
            {
                if (settingListe == null)
                    return null;
                return settingListe;
            }
        }

        public List<LiteraturItem> LiteraturListe
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public EinstellungenViewModel()
        {
            LoadDaten();
        }
        #endregion

        #region Public Methods
        public void LoadDaten()
        {
            if (Global.ContextHeld != null)
            {
                EinstellungListe = Global.ContextHeld.Liste<Model.Einstellung>().Where(e => e.Kategorie != "Versteckt").OrderBy(h => h.Name).Select(e => EinstellungItem.GetTypedEinstellungItem(e)).ToList();
                settingListe = Global.ContextHeld.Liste<Model.Setting>().ToList();
                LiteraturListe = Global.ContextHeld.Liste<Model.Literatur>().OrderBy(h => h.Name).Select(e => new LiteraturItem(e)).ToList();
            }
        }
        #endregion
    }

    #region LiteraturItem

    public class LiteraturItem : INotifyPropertyChanged
    {
        public Model.Literatur Literatur { get; set; }

        public LiteraturItem() : this(new Model.Literatur()) { }

        public LiteraturItem(Model.Literatur l)
        {
            Literatur = l;
            Literatur.PropertyChanged += Literatur_PropertyChanged;

            onOpenFileDialog = new Base.CommandBase(OpenFileDialog, null);
            onOpenUrlPdf = new Base.CommandBase(OpenUrlPdf, null);
            onOpenUrlPrint = new Base.CommandBase(OpenUrlPrint, null);
            onOpenPdf = new Base.CommandBase(OpenPdf, null);
        }

        private void OpenPdf(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(Pfad))
                    Logic.General.Pdf.OpenFileInReader(Pfad);
            }
            catch (Exception ex)
            {
                View.General.ViewHelper.ShowError(string.Format("Das PDF konnte nicht geöffnet werden.\nReader: {0}\nDatei: {1}\n", Logic.General.Pdf.OpenCommand, Pfad), ex);
            }
        }

        private void OpenFileDialog(object obj)
        {
            string file = View.General.ViewHelper.ChooseFile(string.Format("Zu '{0}' ein PDF auswählen", Name), string.Format("{0}.pdf", Name), false, true, "pdf");
            if (string.IsNullOrEmpty(file))
                return;
            Pfad = file;

            OnChanged("IsOriginal");
        }

        private void OpenUrlPdf(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlPdf))
                    System.Diagnostics.Process.Start(UrlPdf);
            }
            catch (Exception) { }
        }

        private void OpenUrlPrint(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlPrint))
                    System.Diagnostics.Process.Start(UrlPrint);
            }
            catch (Exception) { }
        }

        public String UrlPdf
        {
            get { return Literatur.UrlPdf; }
            set { Literatur.UrlPdf = value; }
        }

        public String UrlPrint
        {
            get { return Literatur.UrlPrint; }
            set { Literatur.UrlPrint = value; }
        }

        public String Abkürzung
        {
            get { return Literatur.Abkürzung; }
            set { Literatur.Abkürzung = value; }
        }

        public String Name
        {
            get { return Literatur.Name; }
            set { Literatur.Name = value; }
        }

        public String Pfad
        {
            get { return Literatur.Pfad; }
            set { Literatur.Pfad = value; }
        }

        public int Seitenoffset
        {
            get { return Literatur.Seitenoffset; }
            set { Literatur.Seitenoffset = value; }
        }

        public Nullable<bool> IsOriginal
        {
            get { return Literatur.IsOriginal; }
        }

        private void Literatur_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnChanged(e.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Base.CommandBase onOpenPdf;
        public Base.CommandBase OnOpenPdf
        {
            get { return onOpenPdf; }
        }

        private Base.CommandBase onOpenFileDialog;
        public Base.CommandBase OnOpenFileDialog
        {
            get { return onOpenFileDialog; }
        }

        private Base.CommandBase onOpenUrlPdf;
        public Base.CommandBase OnOpenUrlPdf
        {
            get { return onOpenUrlPdf; }
        }

        private Base.CommandBase onOpenUrlPrint;
        public Base.CommandBase OnOpenUrlPrint
        {
            get { return onOpenUrlPrint; }
        }
    }

    #endregion

    #region EinstellungItem
    //Falls typsensitive Hilfsklassen gebraucht werden.
    public class EinstellungItemString : EinstellungItemGeneric<String>
    {
        public EinstellungItemString(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemBoolean : EinstellungItemGeneric<Boolean>
    {
        public EinstellungItemBoolean(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemInteger : EinstellungItemGeneric<int>
    {
        public EinstellungItemInteger(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemDouble : EinstellungItemGeneric<double>
    {
        public EinstellungItemDouble(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemGeneric<T> : EinstellungItem
    {
        public EinstellungItemGeneric(Model.Einstellung e) : base(e) { }

        public T Wert
        {
            get { return einstellung.Get<T>(); }
            set { einstellung.Set<T>(value); }
        }
    }

    public class EinstellungItem : INotifyPropertyChanged
    {
        protected Model.Einstellung einstellung = null;
        public EinstellungItem(Model.Einstellung e)
        {
            einstellung = e;
            einstellung.PropertyChanged += einstellung_PropertyChanged;
        }

        public String Kontext
        {
            get { return einstellung.Kontext; }
            set { einstellung.Kontext = value; }
        }

        public String Kategorie
        {
            get { return einstellung.Kategorie; }
            set { einstellung.Kategorie = value; }
        }

        public String Name
        {
            get { return einstellung.Name; }
            set { einstellung.Name = value; }
        }

        public String Beschreibung
        {
            get { return einstellung.Beschreibung; }
            set { einstellung.Beschreibung = value; }
        }

        public String Typ
        {
            get { return einstellung.Typ; }
            set { einstellung.Typ = value; }
        }

        public Type Type
        {
            get { return einstellung.Type; }
        }

        private void einstellung_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
                OnChanged("Wert");
            else if (e.PropertyName == "Wert")
            { }
            else
                OnChanged(e.PropertyName);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static EinstellungItem GetTypedEinstellungItem(Model.Einstellung e)
        {
            if (e.Type == typeof(Boolean))
                return new EinstellungItemBoolean(e);
            if (e.Type == typeof(String))
                return new EinstellungItemString(e);
            if (e.Type == typeof(int))
                return new EinstellungItemInteger(e);
            if (e.Type == typeof(double))
                return new EinstellungItemDouble(e);
            return Impromptu.InvokeConstructor(typeof(EinstellungItemGeneric<>).MakeGenericType(e.Type), e);
        }
    }
    #endregion

    public enum ermittleRuestung
    {        
        AutomatischZonen,
        Einfach,
        Zonen,
        AutomatischEinfach
    }
    public enum ermittleBehinderung
    {
        Automatisch,
        Eingabe
    }
    public enum ermitteleUeberlastung
    {
        Automatisch,
        Eingabe
    }
}
