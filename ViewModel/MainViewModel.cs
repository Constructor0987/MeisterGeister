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

namespace MeisterGeister.ViewModel
{
    public class MainViewModel : Base.ViewModelBase
    {

        private MainViewModel() : base(View.General.ViewHelper.ShowError)
        {
            App.Queue.ProgressChanged += Queue_ProgressChanged;
            BuildMenu();
            OpenTabs();
            ShowFavPlaylist = Einstellungen.ShowPlaylistFavorite &&
                (OpenTools.FirstOrDefault(t => t.Name == "Audio") != null);

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

        #region Kalender
        private string _aktuellesDatum = null;
        public string AktuellesDatum
        {
            get { return _aktuellesDatum; }
            set { Set(ref _aktuellesDatum, value); }
        }

        #endregion


    }
}
