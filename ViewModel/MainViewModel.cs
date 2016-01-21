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

namespace MeisterGeister.ViewModel
{
    public class MainViewModel : Base.ViewModelBase
    {
        public MainViewModel() : base(View.General.ViewHelper.ShowError)
        {
            App.Queue.ProgressChanged += Queue_ProgressChanged;
            BuildMenu();
            OpenTabs();
        }

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

        MenuItemViewModel AddExternesProgramm(Model.MenuLink mLink)
        {
            if(!Gruppen.ContainsKey(mLink.MenuPunkt))
                return null;
            var g = Gruppen[mLink.MenuPunkt];
            var mi = new MenuItemViewModel();
            mi.Header = mLink.Name;
            mi.Icon = mLink.Bild;
            Action<object> c = o => 
                {
                    StartExtern(mLink.ProgrammPfad);
                };
            mi.Command = new Base.CommandBase(c, null);
            g.Children.Add(mi);
            return mi;
        }

        public Dictionary<string, MenuItemViewModel> Gruppen;

        MenuItemViewModel AddGruppe(string name)
        {
            var mi = new MenuItemViewModel();
            mi.Header = name;
            MenuItems.Add(mi);
            return mi;
        }

        MenuItemViewModel AddTool(Tool tool)
        {
            var g = Gruppen[tool.MenuGruppe];
            var mi = new MenuItemViewModel();
            mi.Header = tool.Name;
            mi.Icon = (String.IsNullOrWhiteSpace(tool.Icon)?null:"/Images/"+tool.Icon);
            Action<object> c = o => 
                {
                    OpenTool(tool);
                };
            mi.Command = new Base.CommandBase(c, null);
            g.Children.Add(mi);
            return mi;
        }
        ExtendedObservableCollection<MenuItemViewModel> menuItems;
        public ExtendedObservableCollection<MenuItemViewModel> MenuItems { get { return menuItems; } protected set { menuItems = value as ExtendedObservableCollection<MenuItemViewModel>; } }
        #endregion

        #region Tabs
        ExtendedObservableCollection<ToolViewModelBase> openTools;
        public ExtendedObservableCollection<ToolViewModelBase> OpenTools
        {
            get { return openTools; }
            set { Set(ref openTools, value); }
        }

        int selectedTab = 0;
        public int SelectedTab
        {
            get { return selectedTab; }
            set
            {
                if (Set(ref selectedTab, value))
                    Einstellungen.SelectedTab = value;
            }
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

        void OpenTool(Tool t)
        {
            if (t == null)
                return;

            // Falls Tool bereits geöffnet, kein zweites öffnen, sondern geöffnetes aktivieren
            var tvm = OpenTools.Where(tv => tv.Tool == t).FirstOrDefault();;
            if(tvm != null)
            {
                tvm.IsSelected = true;
                return;
            }

            //sonst erstellen und auswählen
            Global.SetIsBusy(true, string.Format("{0} Tab wird geladen...", t.Name));
            tvm = t.CreateToolViewModel();
            if (tvm != null)
            {
                //TODO showerror, popup, etc verdrahten. - evtl sollten die in der base durch events ersetzt werden auf die sich dann die views oder andere VMs registrieren können. Nochmal durchdenken...
                OpenTools.Add(tvm);
                tvm.IsSelected = true;
            }
            Global.SetIsBusy(false);
        }

        void StartExtern(string ProgrammPfad)
        {
            if (!string.IsNullOrWhiteSpace(ProgrammPfad))
            {
                try
                {
                    string curDir = Environment.CurrentDirectory;
                    string fileName = ProgrammPfad;
                    if (!ProgrammPfad.StartsWith("http"))
                    {
                        if (fileName.StartsWith("\\")) // bei relativem Pfad das HomeDir hinzufügen
                        {
                            fileName = fileName.Remove(0, 1).Insert(0, Logic.Extensions.FileExtensions.GetHomeDirectory());
                            Logic.Extensions.FileExtensions.SetCurrentDir(fileName);
                        }
                        else
                            Logic.Extensions.FileExtensions.SetCurrentDir();
                    }

                    System.Diagnostics.Process.Start(fileName);
                    Environment.CurrentDirectory = curDir;
                }
                catch (Exception ex)
                {
                    ShowError("Beim Starten eines externen Programms ist ein Fehler aufgetreten!", ex);
                }
            }
        }

        void OpenTools_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (Base.ToolViewModelBase tool in e.NewItems)
                    tool.RequestClose += this.ToolRequestClose;
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (Base.ToolViewModelBase tool in e.OldItems)
                    tool.RequestClose -= this.ToolRequestClose;
        }

        void ToolRequestClose(object sender, EventArgs e)
        {
            Base.ToolViewModelBase tool = sender as Base.ToolViewModelBase;
            if(tool != null)
                this.OpenTools.Remove(tool);
        }


        //TODO Drag und Drop der Position
        #endregion
    }
}
