using MeisterGeister.Daten;
using MeisterGeister.ViewModel.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MeisterGeister.Logic.Extensions;
using System.Windows.Controls;
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.ViewModel.Base;
using System.Windows.Data;

using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.Logic.Kalender;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.View.Settings;
using Q42.HueApi.Interfaces;
using Q42.HueApi;
using Q42.HueApi.Models.Bridge;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.ColorConverters.OriginalWithModel;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.ColorConverters;

using MeisterGeister.View.General;
using static MeisterGeister.ViewModel.Settings.EinstellungenViewModel;
using MeisterGeister.ViewModel.Settings;
using System.Windows.Media;
using System.Windows.Threading;
using Q42.HueApi.Models.Groups;
using Q42.HueApi.Models;

namespace MeisterGeister.ViewModel
{
    public class MainViewModel : Base.ViewModelBase
    {

        #region -- HUE running ---

        #region --- HUE Command

        //command keys for queue
        private const string ON_OFF = "ON_OFF";
        private const string BRIGHTNESS = "BRIGHTNESS";
        private const string COLOR = "COLOR";

        private Dictionary<string, LightCommand> _commandQueue;
        DispatcherTimer _timer; //timer for queue

        #endregion


        private void Timer_Tick(object sender, object e)
        {
            //stop timer
            _timer.Stop();

            //execute queue commands
            if (_commandQueue.Count > 0)
            {
                foreach (var cmd in _commandQueue)
                {
                    List<string> lstLights = cmd.Key.Split('|').ToList();
                    lstLights.RemoveAt(0);
                    //fire and forget
                    Client.SendCommandAsync(cmd.Value, lstLights);
                }
            }

            //clear queue
            _commandQueue.Clear();
            //start timer back up again
            _timer.Start();
        }

        //helper method to queue light commands for execution
        public void QueueCommand(string commandType, LightCommand cmd, List<string> lstLights)
        {
            if (_commandQueue == null && Client != null)
            {
                //initialize command queue
                _commandQueue = new Dictionary<string, LightCommand>();

                //intilialize command queue timer (avoid sending too many commands per second)
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(0.5);
                _timer.Tick += Timer_Tick;
                _timer.Start();
            }
            if (_commandQueue == null)
                return;
            if (_commandQueue.ContainsKey(commandType))
            {
                //replace with most recent
                _commandQueue[commandType] = cmd;
            }
            else
            {
                _commandQueue.Add(commandType + "|" +string.Join("|", lstLights), cmd);
            }
        }

        #endregion

        private MainViewModel() : base(View.General.ViewHelper.ShowError)
        {
            Global.MainVM = this;
            App.Queue.ProgressChanged += Queue_ProgressChanged;
            BuildMenu();
            OpenTabs();
            ShowFavPlaylist = Einstellungen.ShowPlaylistFavorite &&
                (OpenTools.FirstOrDefault(t => t.Name == "Audio") != null);
            UpdateHotkeyUsed();
            LoadHUE();

            Einstellungen.EinstellungChanged += Einstellungen_EinstellungChanged;                     
        }

        void Einstellungen_EinstellungChanged(object sender, EinstellungChangedEventArgs e)
        {
            if (e.PropertyName == "ShowPlaylistFavorite")
            {
                ShowFavPlaylist = Einstellungen.ShowPlaylistFavorite &&
                (OpenTools.FirstOrDefault(t => t.Name == "Audio") != null);
            }
        }

        static MainViewModel instance;
        public static MainViewModel Instance
        {
            get {
                if(instance == null)
                {
                    instance = new MainViewModel();
                }
                return MainViewModel.instance; 
            }
            private set { MainViewModel.instance = value; }
        }

        #region Held
        public void InvalidateHelden()
        {
            helden = null;
        }

        private ExtendedObservableCollection<Model.Held> helden = null;
        public ExtendedObservableCollection<Model.Held> Helden
        {
            get
            {
                if(helden == null)
                {
                    helden = new ExtendedObservableCollection<Model.Held>(Global.ContextHeld.Liste<Model.Held>());
                }
                return helden;
            }
            set { Set(ref helden, value); }
        }

        private void filterHeldengruppe(object sender, FilterEventArgs f)
        {
            Model.Held h = (Model.Held)f.Item;
            f.Accepted = (h.AktiveHeldengruppe ?? false) && (h.Regelsystem == Global.Regeledition) && !(h.HeldGUID.ToString("D").ToUpperInvariant().StartsWith("00000000-0000-0000-045C"));
        }

        CollectionViewSource heldenGruppe = null;
        public ICollectionView HeldenGruppe
        {
            get
            {
                if (heldenGruppe == null)
                {
                    heldenGruppe = new CollectionViewSource() { Source = Helden };
                    heldenGruppe.Filter += filterHeldengruppe;
                    heldenGruppe.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }

                return heldenGruppe.View;
            }
        }


        private Model.Held selectedHeld = null;
        public Model.Held SelectedHeld
        {
            get { return selectedHeld; }
            set
            {
                if (value != null && value.HeldGUID == Guid.Empty)
                    value = null;
                //Eventuell abspeichern oder nicht? Vorher wurde das abspeichern zu oft ausgeführt.
                if(Set(ref selectedHeld, value))
                {
                    Logic.Einstellung.Einstellungen.SelectedHeld = (value != null) ? value.HeldGUID.ToString() : null;
                }
            }
        }

        private Model.Held nsc = null;
        public Model.Held NSC
        {
            get
            {
                if (nsc == null)
                {
                    Guid g = Guid.Parse(String.Format("00000000-0000-0000-045c-0000000000{0:00}", Global.RegeleditionNummer.Replace(".", "").PadLeft(2, '0')));
                    nsc = Helden.Where(h => h.HeldGUID == g).FirstOrDefault();
                    if (nsc == null)
                    {
                        nsc = Global.ContextHeld.New<Model.Held>();
                        nsc.HeldGUID = g;
                        nsc.Name = "NSC";
                        nsc.Regelsystem = Global.Regeledition;
                        nsc.AddBasisTalente();
                        Global.ContextHeld.Insert<Model.Held>(nsc);
                        Helden.Add(nsc);
                    }
                }
                return nsc;
            }
        }
        #endregion

        #region Regeledition und Version
        public string VersionInfo
        {
            get
            {
                string version = string.Format("V {0} / {1}", App.GetVersionString(App.GetVersionProgramm()), DatabaseUpdate.DatenbankVersionAktuell);
#if TEST
                version += " Test";
#endif
                if (Global.INTERN)
                    version += " Intern";
                return version;
            }
        }

        public string RegeleditionNummer
        {
            get
            {
                return Global.RegeleditionNummer;
            }
        }
        #endregion

        #region ProgressBar
        private int currentProgress;
        public int CurrentProgress
        {
            get
            {
                return this.currentProgress;
            }
            set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    OnChanged("CurrentProgress");
                }
            }
        }

        private string currentUserState;
        public string CurrentUserState
        {
            get
            {
                return this.currentUserState;
            }
            set
            {
                if (this.currentUserState != value)
                {
                    this.currentUserState = value;
                    OnChanged("CurrentUserState");
                }
            }
        }

        private Visibility progressBarVisibility;
        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                if (value != progressBarVisibility)
                {
                    progressBarVisibility = value;
                    OnChanged("ProgressBarVisibility");
                }
            }
        }

        private void Queue_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                Thread.Sleep(1000);
                ProgressBarVisibility = Visibility.Hidden;
            }
            else
                ProgressBarVisibility = Visibility.Visible;
            this.CurrentProgress = e.ProgressPercentage;
            this.CurrentUserState = e.UserState.ToString();
        }
        #endregion

        #region Menu
        void BuildMenu()
        {
            MenuItems = new ExtendedObservableCollection<MenuItemViewModel>();
            Gruppen = new Dictionary<string, MenuItemViewModel>();
            foreach(var g in Tool.Gruppen)
                Gruppen.Add(g, AddGruppe(g));
            foreach(var tool in Tool.ToolListe.Values)
                AddTool(tool);
            foreach (var mLink in Global.ContextMenuLink.Liste<Model.MenuLink>())
                AddExternesProgramm(mLink);
        }

        public Dictionary<string, MenuItemViewModel> Gruppen;

        public MenuItemViewModel AddGruppe(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;
            if (Gruppen.ContainsKey(name))
                return Gruppen[name];
            var mi = new MenuItemViewModel();
            mi.Header = name;
            MenuItems.Add(mi);
            var ext = new AddExternMenuItemViewModel();
            ext.MainViewModel = this;
            ext.Gruppe = name;
            mi.Children.Add(ext);
            return mi;
        }

        public MenuItemViewModel AddTool(Tool tool)
        {
            var g = Gruppen[tool.MenuGruppe];
            var mi = new MenuItemViewModel();
            mi.Header = tool.Name;
            mi.Icon = tool.Icon;
            Action<object> c = o =>
            {
                OpenTool(tool);
            };
            mi.Command = new Base.CommandBase(c, null);
            g.Children.Insert(g.Children.Count - 1, mi);
            return mi;
        }

        public MenuItemViewModel AddExternesProgramm(Model.MenuLink mLink)
        {
            if(mLink == null || !Gruppen.ContainsKey(mLink.MenuPunkt))
                return null;
            var g = Gruppen[mLink.MenuPunkt];
            var mi = new ExternMenuItemViewModel();
            mi.MenuLink = mLink;
            mi.MainViewModel = this;
            g.Children.Insert(g.Children.Count - 1, mi);
            return mi;
        }

        ExtendedObservableCollection<MenuItemViewModel> menuItems;
        public ExtendedObservableCollection<MenuItemViewModel> MenuItems { get { return menuItems; } protected set { Set(ref menuItems, value); } }
        #endregion

        #region Tabs
        ExtendedObservableCollection<ToolViewModelBase> openTools;
        public ExtendedObservableCollection<ToolViewModelBase> OpenTools
        {
            get { return openTools; }
            set { Set(ref openTools, value); }
        }

        int selectedTab = 0;
        public int SelectedTabIndex
        {
            get { return selectedTab; }
            set
            {
                if (Set(ref selectedTab, value))
                    Einstellungen.SelectedTab = value;
            }
        }

        public int SelectTool(Tool t)
        {
            for(int j=0; j<OpenTools.Count; j++)
                if(OpenTools[j].Tool == t)
                    return SelectedTabIndex = j;
            return -1 ;
        }

        public int SelectTool(ToolViewModelBase tv)
        {
            return OpenTools.IndexOf(tv);
        }

        void OpenTabs()
        {
            OpenTools = new ExtendedObservableCollection<ToolViewModelBase>();
            OpenTools.CollectionChanged += OpenTools_CollectionChanged;
            //StartTabs
            string[] tabs = Einstellungen.StartTabs.Split('#');
            foreach (string tab in tabs)
            {
                if(Tool.ToolListe.ContainsKey(tab))
                    OpenTool(Tool.ToolListe[tab]);
            }
            SelectedTabIndex = Einstellungen.SelectedTab;
        }

        private string OpenedTabs()
        {
            string tabs = string.Empty;
            foreach (var tab in OpenTools)
            {
                if (tabs != string.Empty)
                    tabs += "#";
                tabs += tab.Name;
            }
            return tabs;
        }

        public void OpenTool(Tool t)
        {
            if (t == null)
                return;

            // Falls Tool bereits geöffnet, kein zweites öffnen, sondern geöffnetes aktivieren
            var tvm = OpenTools.Where(tv => tv.Tool == t).FirstOrDefault();
            if(tvm != null)
            {
                SelectTool(tvm);
                return;
            }

            //sonst erstellen und auswählen
            Global.SetIsBusy(true, string.Format("{0} Tab wird geladen...", t.Name));
            tvm = t.CreateToolViewModel();
            if (tvm != null)
            {
                //showerror, popup, etc verdrahten.
                tvm.SetFromViewHelper(); //evtl sollten die in der base durch events ersetzt werden auf die sich dann die views oder andere VMs registrieren können. Nochmal durchdenken...
                OpenTools.Add(tvm);
                SelectTool(tvm);
            }
            Global.SetIsBusy(false);
        }

        void OpenTools_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (Base.ToolViewModelBase tool in e.NewItems)
                    tool.RequestClose += this.ToolRequestClose;
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (Base.ToolViewModelBase tool in e.OldItems)
                    tool.RequestClose -= this.ToolRequestClose;

            // geöffnete Tabs abspeichern
            Einstellungen.StartTabs = OpenedTabs();
        }

        void ToolRequestClose(object sender, EventArgs e)
        {
            Base.ToolViewModelBase tool = sender as Base.ToolViewModelBase;
            if(tool != null)
                this.OpenTools.Remove(tool);
        }


        //TODO Drag und Drop der Position
        #endregion

        #region Audio
        #region AudioFavoriten
        private AudioPlayer.AudioPlayerViewModel _aPlayerVM = null;
        public AudioPlayer.AudioPlayerViewModel aPlayerVM
        {
            get { return _aPlayerVM; }
            set { Set(ref _aPlayerVM, value); }
        }
        
        private List<object> _favPlaylist = new List<object>();
        public List<object> FavPlaylist
        {
            get { return _favPlaylist; }
            set { Set(ref _favPlaylist, value); }
        }

        private bool _showFavPlaylist = false;
        public bool ShowFavPlaylist
        {
            get { return _showFavPlaylist; }
            set
            {
                if (Set(ref _showFavPlaylist, value))
                    UpdateFavorites();
            }
        }
        
        public void UpdateFavorites()
        {
            ToolViewModelBase audioTool = OpenTools.FirstOrDefault(t => t.Name == "Audio");
            if (audioTool != null)
            {
                List<object> favList = new List<object>();
				Global.ContextAudio.ThemeListe.Where(t => t.Favorite != null).Where(t2 => t2.Favorite.Value).OrderBy(t3 => t3.Name).ToList().
                    ForEach(fav => favList.Add(fav));
                Global.ContextAudio.PlaylistListe.Where(t => t.Favorite != null).Where(t2 => t2.Favorite.Value).OrderBy(t3 => t3.Name).ToList().
                    ForEach(fav => favList.Add(fav));
                FavPlaylist = favList;
                if (favList.Count > 0)
                {
                    aPlayerVM = audioTool as AudioPlayer.AudioPlayerViewModel;
                    if (Einstellungen.ShowPlaylistFavorite) ShowFavPlaylist = true;
                }
                else
                    ShowFavPlaylist = false;
            }
        }

        
        private Base.CommandBase _onBtnFavPlaylistClick = null;
        public Base.CommandBase OnBtnFavPlaylistClick
        {
            get
            {
                if (_onBtnFavPlaylistClick == null)
                    _onBtnFavPlaylistClick = new Base.CommandBase(BtnFavPlaylistClick, null);
                return _onBtnFavPlaylistClick;
            }
        }
        void BtnFavPlaylistClick(object obj)
        {
			if (obj is MeisterGeister.Model.Audio_Theme)
            {
                System.Windows.Controls.Primitives.ToggleButton tbtn = aPlayerVM.ErwPlayerThemeListe.FirstOrDefault(t => t.VM.Theme == obj as MeisterGeister.Model.Audio_Theme).tbtnTheme;
                tbtn.IsChecked =  !tbtn.IsChecked.Value;
                aPlayerVM.ThemeButton_Checked(tbtn, null);
            }
            else
                if (obj is MeisterGeister.Model.Audio_Playlist)
                    aPlayerVM.SelectedMusikPlaylistItem = aPlayerVM.MusikListItemListe.FirstOrDefault(t => t.VM.aPlaylist == obj as MeisterGeister.Model.Audio_Playlist);                

		}

        private Base.CommandBase _onFavPlaylistEntfernenClick = null;
        public Base.CommandBase OnFavPlaylistEntfernenClick
        {
            get
            {
                if (_onFavPlaylistEntfernenClick == null)
                    _onFavPlaylistEntfernenClick = new Base.CommandBase(FavPlaylistEntfernenClick, null);
                return _onFavPlaylistEntfernenClick;
            }
        }
        void FavPlaylistEntfernenClick(object obj)
        {
            if (obj is MeisterGeister.Model.Audio_Playlist)
            {
                MeisterGeister.Model.Audio_Playlist aPlaylist = obj as MeisterGeister.Model.Audio_Playlist;
                aPlaylist.Favorite = false;
                Global.ContextAudio.Update<MeisterGeister.Model.Audio_Playlist>(aPlaylist);
            }
            else
            if (obj is MeisterGeister.Model.Audio_Theme)
            {
                MeisterGeister.Model.Audio_Theme aTheme = obj as MeisterGeister.Model.Audio_Theme;
                aTheme.Favorite = false;
                Global.ContextAudio.Update<MeisterGeister.Model.Audio_Theme>(aTheme);
            }
            UpdateFavorites();
        }

        #endregion

        #region HUE Lampen

        /// <summary>
        /// Gets or privately sets the Selected Color.
        /// </summary>
        private Color selectedColor = Colors.Transparent;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    this.selectedColor = value;
                    CreateAlphaLinearBrush();
                    //  UpdateTextBoxes();
                    //  UpdateInk();
                }
            }
        }

        LinearGradientBrush _alphaBrush = new LinearGradientBrush();
        LinearGradientBrush alphaBrush
        {
            get { return _alphaBrush; }
            set { Set(ref _alphaBrush, value); }
        }

        /// <summary>
        /// Creates a new LinearGradientBrush background for the Alpha area slider.  This is based on the current color.
        /// </summary>
        private void CreateAlphaLinearBrush()
        {
            Color startColor = Color.FromArgb((byte)0, SelectedColor.R, SelectedColor.G, SelectedColor.B);
            Color endColor = Color.FromArgb((byte)255, SelectedColor.R, SelectedColor.G, SelectedColor.B);
            alphaBrush = new LinearGradientBrush(startColor, endColor, new System.Windows.Point(0, 0), new System.Windows.Point(1, 0));
        }

        private double _hueLampeSaettigung = 255;
        public double HUELampeSaettigung
        {
            get { return _hueLampeSaettigung; }
            set 
            { 
                Set(ref _hueLampeSaettigung, value);

                if (HUELightsSelected.Count != 0 && Client != null)
                {
                    List<Light> lstLights = lstHUELights.Where(t => HUELightsSelected.Select(z => z.Id).Contains(t.Id)).ToList();
                    if (lstLights.Count == 0) return;
                    //Control the lights                
                    LightCommand command = new LightCommand();

                    //   command.TurnOn().SetColor(new RGBColor(SelectedColor.R, SelectedColor.G, SelectedColor.B));
                    //    command.Brightness = (byte)HUELampeBrightness;
                    if (HUELampeBrightness != 0)
                        command.TurnOn().SetColor(new RGBColor(SelectedColor.R, SelectedColor.G, SelectedColor.B));
                    command.Saturation = (byte)value;
                    //Or send it to all lights
                    //   hueClient.SendCommandAsync(command);
                    Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());
                }
            }
        }
        private double _hueLampeBrightness = 255;
        public double HUELampeBrightness
        {
            get { return _hueLampeBrightness; }
            set 
            { 
                Set(ref _hueLampeBrightness, value);

                if (HUELightsSelected.Count != 0 && Client != null)
                {
                    List<Light> lstLights = lstHUELights.Where(t => HUELightsSelected.Select(z => z.Id).Contains(t.Id)).ToList();
                    if (lstLights.Count == 0)
                        return;
                    //Control the lights                
                    LightCommand command = new LightCommand();
                    if (value > 1)
                    {
                        command.TurnOn().SetColor(new RGBColor(SelectedColor.R, SelectedColor.G, SelectedColor.B));
                        command.Brightness = (byte)value;
                    }
                    else
                        command.TurnOff();
                    Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());
                }
            }
        }

        private List<string> _lstHUEDeviceID = new List<string>();
        public List<string> lstHUEDeviceID
        {
            get { return _lstHUEDeviceID; }
            set { Set(ref _lstHUEDeviceID, value); }
        }
        private List<Light> _HUELightsSelected = new List<Light>();
        public List<Light> HUELightsSelected
        {
            get { return _HUELightsSelected; }
            set { Set(ref _HUELightsSelected, value); }
        }

        private List<Group> _hUEGroups = new List<Group>();
        public List<Group> HUEGroupsSelected
        {
            get { return _hUEGroups; }
            set { Set(ref _hUEGroups, value); }
        }

        private List<Light> _lstHUELights = new List<Light>();
        public List<Light> lstHUELights
        {
            get { return _lstHUELights; }
            set { Set(ref _lstHUELights, value); }
        }

        private List<Group> _lstHUEGroups = new List<Group>();
        public List<Group> lstHUEGroups
        {
            get { return _lstHUEGroups; }
            set { Set(ref _lstHUEGroups, value); }
        }

        private List<Scene> _lstHUEScenes = new List<Scene>();
        public List<Scene> lstHUEScenes
        {
            get { return _lstHUEScenes; }
            set { Set(ref _lstHUEScenes, value); }
        }

        private bool _hUELampenSelected = false;
        public bool HUELampenSelected
        {
            get { return _hUELampenSelected; }
            set
            {
                Set(ref _hUELampenSelected, value);
                if (value)
                    HUEGruppenSelected = false;
            }
        }

        private bool _hUEGruppenSelected = false;
        public bool HUEGruppenSelected
        {
            get { return _hUEGruppenSelected; }
            set
            {
                Set(ref _hUEGruppenSelected, value);
                if (value)
                    HUELampenSelected = false;
            }
}

        private List<HUESzene> _lstHUESzenen = new List<HUESzene>();
        public List<HUESzene> lstHUESzenen
        {
            get { return _lstHUESzenen; }
            set { Set(ref _lstHUESzenen, value); }
        }

        private HUESzene _hUESzeneSelected = null;
        public HUESzene HUESzeneSelected
        {
            get { return _hUESzeneSelected; }
            set 
            { 
                Set(ref _hUESzeneSelected, value);
                if (value == null)
                    return;
                foreach (LightColor LC in value.lstLightColor)
                {
                    if (LC.color != null)
                    {
                        //Control the lights                
                        LightCommand command = new LightCommand();
                        command.TurnOn().SetColor(new RGBColor(LC.color.R, LC.color.G, LC.color.B));
                        command.Brightness = (byte)LC.color.A;

                        List<string> lst = new List<string>();
                        lst.Add(LC.light.Id);

                        QueueCommand(COLOR, command, lst);
            //            Global.MainVM.Client.SendCommandAsync(command, lst);
                    }
                }
            }
        }

        private List<LocatedBridge> _lstHUEGaterways = new List<LocatedBridge>();
        public List<LocatedBridge> lstHUEGateways
        {
            get { return _lstHUEGaterways; }
            set { Set(ref _lstHUEGaterways, value); }
        }

        private LocatedBridge _HUEGWSelected = null;
        public LocatedBridge HUEGWSelected
        {
            get { return _HUEGWSelected; }
            set { Set(ref _HUEGWSelected, value); }
        }

        private HUETheme _HUEThemeSelected;
        public HUETheme HUEThemeSelected
        {
            get { return _HUEThemeSelected; }
            set { Set(ref _HUEThemeSelected, value); }
        }
        private List<HUETheme> _lstHUEThemes = new List<HUETheme>();
        public List<HUETheme> lstHUEThemes
        {
            get { return _lstHUEThemes; }
            set { Set(ref _lstHUEThemes, value); }
        }

        private LocalHueClient _client = null;
        public LocalHueClient Client
        {
            get { return _client; }
            set { Set(ref _client, value); }
        }

        public async void LoadHUE()
        {
            string HUE_ID = Logic.Einstellung.Einstellungen.GetEinstellung<string>("HUE_GatewayID");
            string HUE_Regkey = Logic.Einstellung.Einstellungen.GetEinstellung<string>("HUE_Registerkey");
            if (!string.IsNullOrEmpty(HUE_ID) && !string.IsNullOrEmpty(HUE_Regkey))
            {
                try
                {
                    IBridgeLocator locator = new HttpBridgeLocator();
                    var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
                    lstHUEGateways = bridgeIPs.ToList();

                    LocatedBridge HUEBridge = bridgeIPs.Where(t => t.BridgeId == HUE_ID).FirstOrDefault();
                    if (HUEBridge == null)
                        return;
                    HUEGWSelected = HUEBridge;
                    Client = new LocalHueClient(HUEBridge.IpAddress);
                    Client.Initialize(HUE_Regkey);

                    //Search for new lights
                    await Client.SearchNewLightsAsync(lstHUEDeviceID);
                    //Get all lights
                    var resultLights = await Client.GetLightsAsync();
                    //Get all Scenes
                    var resultScene = await MainVM.Client.GetScenesAsync();
                    //Get all Groups
                    var resultGroups = await MainVM.Client.GetGroupsAsync();

                    lstHUELights = resultLights as List<Light>;
                    lstHUEGroups = resultGroups as List<Group>;
                    lstHUEScenes = resultScene as List<Scene>;
                }
                catch (Exception ex)
                {
                    Logic.Einstellung.Einstellungen.SetEinstellung<string>("HUE_GatewayID", null);
                }
            }
        }

        public async void UpdateHUE()
        {
            //Get all lights
            var resultLights = await Client.GetLightsAsync();
            lstHUELights = resultLights as List<Light>;
        }


        private Base.CommandBase _onBtnHUEOnOff = null;
        public Base.CommandBase OnBtnHUEOnOff
        {
            get
            {
                if (_onBtnHUEOnOff == null)
                    _onBtnHUEOnOff = new Base.CommandBase(HUEOnOff, null);
                return _onBtnHUEOnOff;
            }
        }
        void HUEOnOff(object obj)
        {
            if (HUELightsSelected == null)
                return;

            HUELightsSelected.ForEach(delegate (Light hue)
            {
                State actState = hue.State;
                actState.On = !actState.On;
            });            
        }

        private Base.CommandBase _onBtnDoHUETheme = null;
        public Base.CommandBase OnBtnDoHUETheme
        {
            get
            {
                if (_onBtnDoHUETheme == null)
                    _onBtnDoHUETheme = new Base.CommandBase(DoHUETheme, null);
                return _onBtnDoHUETheme;
            }
        }
        void DoHUETheme(object obj)
        {
            if (HUEThemeSelected == null)
                return;
            HUEThemeSelected.lstLights = new List<Light>();
            HUEThemeSelected.lstLights.AddRange(HUELightsSelected);

            if (!HUEThemeSelected.isRunning)
            {
                //gCol.Clear();
                //gCol.Add(new GradientStop(HUEThemeSelected.lstLightProcess[0].Color, 0));
                //for (var i = 1; i < HUEThemeSelected.lstLightProcess.Count; i++)
                //{
                //    gCol.Add(new GradientStop(HUEThemeSelected.lstLightProcess[i].Color, HUEThemeSelected.lstLightProcess[i - 1].DauerProzent));
                //}
                HUEThemeSelected.actLightProcess = null;
                HUEThemeSelected.StartTime = Environment.TickCount;
                //ThemeToDo.actLightProcess = 0;
                HUEThemeSelected.isRunning = true;
                HUEThemeSelected._timer.Start();
            }
            else
            {
                HUEThemeSelected._timer.Stop();
                HUEThemeSelected.isRunning = false;
            }
        }


        #endregion

        #region Hotkeys

        private List<btnHotkey> _hotkeyListUsed = new List<btnHotkey>();
        public List<btnHotkey> hotkeyListUsed
        {
            get { return _hotkeyListUsed; }
            set { Set(ref _hotkeyListUsed, value); }
        }

        private int _hotkeyVolume = Einstellungen.GeneralHotkeyVolume;
        public int HotkeyVolume
        {
            get { return _hotkeyVolume; }
            set
            {
                Set(ref _hotkeyVolume, value);
                Einstellungen.SetEinstellung<int>("GeneralHotkeyVolume", _hotkeyVolume);
            }
        }

        public void UpdateHotkeyUsed()
        {
            if (Global.ContextAudio.PlaylistListe == null) return;
            List<btnHotkey> lstHotKeyUsed = new List<btnHotkey>();

            foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null).OrderBy(tt => tt.Key))
            {
                btnHotkey hkey = new btnHotkey();
                hkey.VM.aPlaylistGuid = aPlaylist.Audio_PlaylistGUID;
                hkey.VM.taste = (char)aPlaylist.Key[0];
                hkey.VM.aPlaylist = aPlaylist;
                lstHotKeyUsed.Add(hkey);
            };
            hotkeyListUsed = lstHotKeyUsed;
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
            hotkeyListUsed.ForEach(delegate (btnHotkey hkey)
            {
                hkey.VM.TitelPlayList.FindAll(t => t.mp != null && t.mp.audioStream != 0).ForEach(delegate (AudioPlayer.Logic.btnHotkeyVM.TitelPlay titelPlay)
                { titelPlay.mp.Stop(); });
            });
        }

        #endregion
        #endregion

        #region Kalender
        private string _aktuellesDatum = null;
        public string AktuellesDatum
        {
            get { return _aktuellesDatum;}
            set { Set(ref _aktuellesDatum, value); }
        }

        #endregion

        #region Region
        private List<string> _momentaneRegion = null;
        public List<string> MomentaneRegion
        {
            get { return _momentaneRegion; }
            set { Set(ref _momentaneRegion, value); }
        }

        #endregion

    }
}
