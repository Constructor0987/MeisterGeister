﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
// Eigene Usings
using MeisterGeister.Logic.Settings;
using MeisterGeister.View.General;
using Cursors = System.Windows.Forms.Cursors;
// Weitere Usings
using System.Windows.Documents;
using MeisterGeister.View.Windows;
using MeisterGeister.View.SpielerScreen;
using MeisterGeister.Daten;
using System.Windows.Media.Animation;

namespace MeisterGeister.View
{

    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate() { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, EmptyDelegate);
        }
    }

    // TODO ??: MVVM konform umbauen!    
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {

        #region //FELDER

        private bool isPopOut = true;

        #endregion

        public MainView()
        {
            InitializeComponent();
            Opacity = 0.5;

            InitializeMenuPunkte();

            _tabControlMain.SelectionChanged -= _tabControlMain_SelectionChanged;

            string[] tabs = Einstellungen.StartTabs.Split('#');
            foreach (string tab in tabs)
                StarteTab(tab);

            if (_tabControlMain.Items.Count > 0 && Einstellungen.SelectedTab >=0 && Einstellungen.SelectedTab < _tabControlMain.Items.Count)
                _tabControlMain.SelectedIndex = Einstellungen.SelectedTab;

            _tabControlMain.SelectionChanged += _tabControlMain_SelectionChanged;

            _labelVersion.Content = string.Format("V {0} / {1}", App.GetVersionString(App.GetVersionProgramm()),
                DatabaseUpdate.DatenbankVersionAktuell);

#if !(DEBUG || INTERN)
            _menuItemAudioPlayer.Visibility = System.Windows.Visibility.Collapsed;
            _menuItemAbenteuer.Visibility = System.Windows.Visibility.Collapsed;
            _menuItemAlchimie.Visibility = System.Windows.Visibility.Collapsed;
#endif

#if BETA
            _labelVersion.Foreground = Brushes.Red;
            _labelVersion.Background = Brushes.LightYellow;
            _labelVersion.Content += " BETA";
#endif
#if INTERN
            _labelVersion.Foreground = Brushes.Red;
            _labelVersion.Background = Brushes.LightYellow;
            _labelVersion.Content += " Intern";
#endif
        }

        public bool IsBusy
        {
            get { return _busyBorder.Visibility == System.Windows.Visibility.Visible ? true : false; }
            set { _busyBorder.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; _busyBorder.Refresh(); }
        }


        private static Dictionary<string, MenuItem> MenuPunkte = new Dictionary<string, MenuItem>(5);

        /// <summary>
        /// Erzeugt die Menü-Einträge.
        /// </summary>
        private void InitializeMenu()
        {
            // TODO MT: Tool-Menü-Eintzräge erzeugen

            // User-Verknüpfungen ins Menü einhängen
            foreach (var mLink in Global.ContextMenuLink.Liste<Model.MenuLink>())
                AddMenuExternesProgramm(mLink);
        }

        public static void AddMenuExternesProgramm(Model.MenuLink mLink)
        {
            MenuItem menuItem = MenuPunkte[mLink.MenuPunkt];
            if (menuItem != null)
            {
                ExternProgrammMenuItem extItem = new ExternProgrammMenuItem(mLink);

                if (menuItem.Items.Count > 0)
                    menuItem.Items.Insert(menuItem.Items.Count - 1, extItem);
            }
        }

        private void InitializeMenuPunkte()
        {
            foreach (var item in _menuTools.Items)
            {
                if (item is MenuItem)
                {
                    MenuItem menuItem = (MenuItem)item;
                    if (!(menuItem.Header is Image))
                        MenuPunkte.Add(menuItem.Header.ToString(), menuItem);
                }
            }
        }

        public static SpielerWindow WindowSpieler { get; set; }

        private void StarteTab(string tabName, int position = -1)
        {
            // falls Tool-Name nicht vorhanden, Tab-Erzeugung abbrechen
            if (!Tool.ToolListe.ContainsKey(tabName)) return;

            Tool t = Tool.ToolListe[tabName];
            Control con = t.CreateToolView();

            // Falls Tool bereits geöffnet, kein zweites öffnen, sondern geöffnetes aktivieren
            TabItem tabItem = IsTabOpend(tabName);
            if (tabItem != null)
            {
                _tabControlMain.SelectedItem = tabItem;
                return;
            }

            // falls View nicht erzeugt werden konnte, abbrechen
            if (con == null) return;

            // Event-Handler verbinden
            switch (tabName)
            {
                // TODO MT: Events auf neues MVVM-Proben-Tool umstellen
                case "ProbenAlt":
                    ((ProbenWürfel.ProbenView)con).ZooBotClick += TabItemControl_ZooBotProbe;
                    break;
                case "Kampf":
                    ((Kampf.KampfView)con).ProbeWürfeln += TabItemControl_ProbeWürfeln;
                    break;
                case "Helden":
                    // TODO ??: ProbeWürfeln Event
                    //((Helden.HeldenView)con).ProbeWürfeln += TabItemControl_ProbeWürfeln;
                    //((Helden.HeldenView)con).HeldChanged += TabItemControl_RefreshHeld;
                    break;
                case "Kalender":
                    ((Kalender.KalenderView)con).DatumAktuellChanged += TabItemControl_RefreshDatumAktuell;
                    break;
                default:
                    break;
            }

            if (con != null)
            {
                TabItemControl tab = new TabItemControl(con, tabName, t.Icon);
                if (position < 0)
                {
                    _tabControlMain.Items.Add(tab);
                    _tabControlMain.SelectedIndex = 0;
                }
                else
                {
                    _tabControlMain.Items.Insert(position + 1, tab);
                    _tabControlMain.SelectedIndex = position + 1;
                }
            }
        }

        private void TabItemControl_ProbeWürfeln(string talentname)
        {
            bool probenTab = false;
            foreach (var tab in _tabControlMain.Items)
            {
                if (tab is TabItem)
                {
                    if (((TabItem)tab).Content is ProbenWürfel.ProbenView)
                    {
                        probenTab = true;
                        _tabControlMain.SelectedItem = tab;
                        ((ProbenWürfel.ProbenView)((TabItem)tab).Content).ProbeWürfeln(talentname);
                    }
                }
            }
            // Falls Proben-Tool geschlossen -> Öffnen
            if (probenTab == false)
            {
                StarteTab("Proben");
                TabItemControl_ProbeWürfeln(talentname);
            }
        }

        private void TabItemControl_RefreshHeld(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            foreach (var tab in _tabControlMain.Items)
            {
                if (tab is TabItem)
                {
                    if (((TabItem)tab).Content is ZooBot.ZooBotView)
                        ((ZooBot.ZooBotView)((TabItem)tab).Content).SetHeldWerte();
                    else if (((TabItem)tab).Content is Kampf.KampfView)
                        ((Kampf.KampfView)((TabItem)tab).Content).RefreshKämpfer();
                }
            }
        }

        private void TabItemControl_RefreshDatumAktuell(object sender, EventArgs e)
        {
            foreach (var tab in _tabControlMain.Items)
            {
                if (tab is TabItem)
                {
                    if (((TabItem)tab).Content is ZooBot.ZooBotView)
                        ((ZooBot.ZooBotView)((TabItem)tab).Content).SetHeldWerte();
                }
            }
        }

        private void TabItemControl_ZooBotProbe(string talentname)
        {
            bool probenTab = false;
            foreach (var tab in _tabControlMain.Items)
            {
                if (tab is TabItem)
                {
                    if (((TabItem)tab).Content is ZooBot.ZooBotView)
                    {
                        probenTab = true;
                        _tabControlMain.SelectedItem = tab;
                        ((ZooBot.ZooBotView)((TabItem)tab).Content).SetHeldWerte(talentname);
                    }
                }
            }
            // Falls Tool geschlossen -> Öffnen
            if (probenTab == false)
            {
                StarteTab("ZooBot");
                TabItemControl_ZooBotProbe(talentname);
            }
        }

        private void ButtonHilfe_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private string OpenedTabs()
        {
            string tabs = string.Empty;
            foreach (TabItemControl tab in _tabControlMain.Items)
            {
                if (tabs != string.Empty)
                    tabs += "#";
                tabs += tab.Titel;
            }
            return tabs;
        }

        private TabItemControl IsTabOpend(string tabName)
        {
            TabItemControl tab = null;
            foreach (TabItemControl tabItem in _tabControlMain.Items)
            {
                if (tabItem.Titel == tabName)
                {
                    tab = tabItem;
                    break;
                }
            }
            return tab;
        }

        private void ShowTab(string tabName)
        {
            foreach (var tab in _tabControlMain.Items)
            {
                if (tab is TabItemControl)
                {
                    if (((TabItemControl)tab).Titel == tabName)
                    {
                        _tabControlMain.SelectedItem = tab;
                        break;
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Schließe Spieler-Fenster
            if (WindowSpieler != null)
                WindowSpieler.Close();

            // Tab-Einstellungen speichern
            Einstellungen.StartTabs = OpenedTabs();
            Einstellungen.SelectedTab = _tabControlMain.SelectedIndex;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Backup der Datenbank erstellen
            DatabaseTools.BackupDatabase();

            // Änderungen speichern
            App.SaveAll();

            // Datenbank optimieren
            DatabaseTools.ShrinkDatabase();

            // Temp-Ordner bereinigen
            try
            {
                System.Reflection.Assembly assem = System.Reflection.Assembly.GetExecutingAssembly();
                System.Reflection.AssemblyName assemName = assem.GetName();

                string path = System.IO.Path.GetTempPath() + assemName.Name;
                if (Directory.Exists(path))
                    Directory.Delete(path, true);

                // DG-Suche PlugIn Temp bereinigen
                assem = System.Reflection.Assembly.GetAssembly(typeof(DgSuche.GlobusControl));
                assemName = assem.GetName();

                path = System.IO.Path.GetTempPath() + assemName.Name;
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Fehler beim Bereinigen", "Beim Breinigen des Temporären Ordners ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
            }
        }

        private void MenuItemSpielerInfoControl_Click(object sender, RoutedEventArgs e)
        {
            TabItemControl tab = IsTabOpend("SpielerInfo");
            if (tab == null)
                StarteTab("SpielerInfo", _tabControlMain.SelectedIndex);
            else
                _tabControlMain.SelectedItem = tab;

            if (WindowSpieler == null)
                StarteSpielerFenster();
        }

        public static void ShowHideToggleSpielerFenster()
        {
            if (WindowSpieler != null)
            {
                WindowSpieler.Close();
                WindowSpieler = null;
            }
            else
            {
                StarteSpielerFenster();
            }
        }

        public static void ShowSpielerInfo(object info)
        {
            // Falls kein Fenster gestartet wurde, eines starten
            if (WindowSpieler == null)
            {
                StarteSpielerFenster();
            }

            WindowSpieler.Content = info;
            WindowSpieler.IsKampfInfoModus = (info is Kampf.KampfInfoView);
        }

        public static void ShowSpielerInfoText()
        {
            RichTextBox txtBlock = new RichTextBox();
            FlowDocument flowDoc = new FlowDocument();

            txtBlock.Document = flowDoc;
            txtBlock.Background = (ImageBrush)App.Current.FindResource("BackgroundPergamentQuer");
            txtBlock.BorderBrush = Brushes.Transparent;
            txtBlock.Margin = new Thickness(40);
            txtBlock.Padding = new Thickness(20);

            txtBlock.Paste();

            ShowSpielerInfo(txtBlock);
        }

        public static void ShowSpielerInfoBild(string pfad, Stretch stretch = Stretch.Uniform)
        {
            try
            {
                Image img = new Image();
                img.Stretch = stretch;

                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.UriSource = new Uri(pfad, UriKind.Relative);
                bmi.EndInit();

                bmi.Freeze();		// freeze image source, used to move it across the thread
                img.Source = bmi;

                ShowSpielerInfo(img);
            }
            catch { }
        }

        public static void StarteSpielerFenster()
        {
            List<System.Windows.Forms.Screen> objScreenList = System.Windows.Forms.Screen.AllScreens.ToList();

            int xPoint = 0, yPoint = 0;
            foreach (System.Windows.Forms.Screen objActualScreen in objScreenList)
            {
                if (!objActualScreen.Primary)
                {
                    xPoint = objActualScreen.Bounds.Location.X + 20;
                    yPoint = objActualScreen.Bounds.Location.Y + 20;
                }
            }
            if (WindowSpieler != null)
                WindowSpieler.Close();
            WindowSpieler = new SpielerWindow();
            WindowSpieler.WindowStartupLocation = WindowStartupLocation.Manual;
            WindowSpieler.Left = Convert.ToDouble(xPoint);
            WindowSpieler.Top = Convert.ToDouble(yPoint);
            WindowSpieler.Closed += new EventHandler(WindowSpieler_Closed);

            WindowSpieler.Show();
        }

        static void WindowSpieler_Closed(object sender, EventArgs e)
        {
            WindowSpieler = null;
        }

        public static Image GetControlImage(FrameworkElement controlToRender)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)controlToRender.ActualWidth,
                (int)controlToRender.ActualHeight,
                90, 90,
                PixelFormats.Default);

            Visual vis = (Visual)controlToRender;
            rtb.Render(vis);

            Image img = new Image();
            img.Source = rtb;
            img.Stretch = Stretch.None;
            img.Measure(new System.Windows.Size(
                            (int)controlToRender.ActualWidth,
                            (int)controlToRender.ActualHeight));
            System.Windows.Size sizeImage = img.DesiredSize;
            img.Arrange(new System.Windows.Rect(new System.Windows.Point(0, 0), sizeImage));

            RenderTargetBitmap rtb2 = new RenderTargetBitmap(
                (int)rtb.Width,
                (int)rtb.Height,
                90,
                90,
                PixelFormats.Default);
            rtb2.Render(img);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb2));

            Stream ms = new MemoryStream();
            png.Save(ms);

            ms.Position = 0;

            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.StreamSource = ms;
            myBitmapImage.EndInit();

            Image imgCon = new Image();
            imgCon.Source = myBitmapImage;
            return imgCon;
        }

        private void MenuItemUpdateCheck_Click(object sender, RoutedEventArgs e)
        {
            // Update-Prüfung in einem eigenen Thread ausführen
            System.Threading.Thread t = new System.Threading.Thread(
                delegate()
                {
                    App.CheckUpdates();
                });
            t.Start();
        }

        private void MenuItemEinstellungen_Click(object sender, RoutedEventArgs e)
        {
            EinstellungenWindow gui = new EinstellungenWindow();
            gui.ShowDialog();
        }

        private void MenuItemHilfeÜber_Click(object sender, RoutedEventArgs e)
        {
            OpenInfoWindow();
        }

        private void MenuItemSysInfo_Click(object sender, RoutedEventArgs e)
        {
            MsgWindow sysInfoview = new MsgWindow();
            sysInfoview.Show();
        }

        private static void OpenInfoWindow()
        {
            InfoWindow info = new InfoWindow();
            info.ShowDialog();
        }

        private void LabelVersion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenInfoWindow();
        }

        private void MenuItemDereGlobus_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dereglobus.org/");
        }

        private void MenuItemHerokonOnline_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.herokon-online.com/");
        }

        private void MenuItemHilfeHomepage_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.meistergeister.org/");
        }

        private void MenuItemHilfeForum_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://meistergeister.siteboard.org/");
        }

        private void MenuItemHilfeFacebook_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/pages/MeisterGeister-f%C3%BCr-Das-Schwarze-Auge/303337406358382?sk=wall");
        }

        private void MenuItemHilfeTwitter_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/#!/MeisterGeister");
        }

        private void MenuItemHilfeEMail_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(string.Format(
                "mailto:info@meistergeister.org?subject=DSA MeisterGeister&body=%0A%0A%0A{0}%0A%0ABetriebssystem: {1} ({2})%0A64bit-System: {3}%0ACLR-Version: {4}%0AArbeitsverzeichnis: {5}",
                "Die nachfolgenden Informationen können für einen zügigen Support hilfreich sein:",
                Environment.OSVersion.ToString(), App.GetOSName(), Environment.Is64BitOperatingSystem.ToString(), Environment.Version.ToString(), Environment.CurrentDirectory));
        }

        private void _tabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized && IsLoaded)
                App.SaveAll();
        }

        private void MenuItemWeb_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(((MenuItem)sender).Tag.ToString());
        }

        private void ButtonTool_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && (sender is Button || sender is MenuItem))
            {
                object tag = null;
                if (sender is Button)
                    tag = ((Button)sender).Tag;
                else if (sender is MenuItem)
                    tag = ((MenuItem)sender).Tag;
                if (tag != null && tag is string)
                    StarteTab(tag.ToString(), _tabControlMain.SelectedIndex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeMenu();

            // wenn kein Jingle abgespielt wird, kann das Startup-Fenster spätestens jetzt geschlossen werden
#if !(NO_JINGLE)
            if (Einstellungen.JingleAbstellen)
#endif
            App.CloseSplashScreen();

#if BETA
            MessageBox.Show("ACHTUNG: Dies ist eine Beta-Version!\n" + App.GetVersionString(App.GetVersionProgramm()), "BETA-Version");
#endif
        }

        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e != null && e.Source != null && e.Source is TabItem
                && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                TabItem item = (TabItem)(e.Source);
                DragDrop.DoDragDrop(item, item, DragDropEffects.All);
            }
        }

        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            if (e != null && e.Source != null
                && (e.Source is TabItem || e.Source is StackPanel || e.Source is Image || e.Source is TextBlock))
            {
                TabItem target = null;
                if (e.Source is TabItem)
                    target = (TabItem)(e.Source);
                else if (e.Source is StackPanel)
                    target = (TabItem)((StackPanel)e.Source).Parent;
                else if (e.Source is Image)
                    target = (TabItem)((StackPanel)((Image)e.Source).Parent).Parent;
                else if (e.Source is TextBlock)
                    target = (TabItem)((StackPanel)((TextBlock)e.Source).Parent).Parent;
                if (e.Data != null)
                {
                    TabItemControl source = (TabItemControl)(e.Data.GetData(typeof(TabItemControl)));
                    if (!source.Equals(target))
                    {
                        if (target != null && target.Parent != null && target.Parent is TabControl)
                        {
                            TabControl tab = (TabControl)(target.Parent);
                            int sourceIndex = tab.Items.IndexOf(source);
                            int targetIndex = tab.Items.IndexOf(target);
                            tab.Items.Remove(source);
                            tab.Items.Insert(targetIndex, source);
                            tab.SelectedItem = source;
                        }
                    }
                }
            }
        }

        #region //EVENTS

        //UI-Animation
        private void MenuAktivateBorder_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isPopOut) {
                Storyboard animation = (Storyboard)FindResource("MenuPopIn");
                animation.Begin(this);
                isPopOut = false;
            } else {
                Storyboard animation = (Storyboard)FindResource("MenuPopOut");
                animation.Begin(this);
                isPopOut = true;
            }
        }

        #endregion

    }
}
