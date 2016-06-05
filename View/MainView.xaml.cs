using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
// Eigene Usings
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.View.General;
using Cursors = System.Windows.Forms.Cursors;
// Weitere Usings
using System.Windows.Documents;
using MeisterGeister.View.Windows;
using MeisterGeister.View.SpielerScreen;
using MeisterGeister.Daten;
using System.Windows.Media.Animation;
using MeisterGeister.View.Settings;
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel;
using System.ComponentModel;

namespace MeisterGeister.View
{

    /// <summary>
    /// Erweiterungsmethode, die ein UI-Element zwingt, sich neu zu zeichnen.
    /// Wird benutzt, um eine IsBusy-Anzeige zu realisieren.
    /// </summary>
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

            VM = MainViewModel.Instance;
            VM.PropertyChanged += VM_PropertyChanged;

            Application.Current.MainWindow = this;

            Opacity = 0.5;

            // MeisterGeisterID abrufen
            Guid mgID = Einstellungen.MeisterGeisterID;

            _labelVersion.ToolTip = string.Format("MeisterGeisterID: {0}", mgID.ToString());
#if TEST
            _labelVersion.Foreground = Brushes.Red;
            _labelVersion.Background = Brushes.LightYellow;
#endif
            if (Global.INTERN)
            {
                _labelVersion.Foreground = Brushes.Red;
                _labelVersion.Background = Brushes.LightYellow;
            }
        }

        void VM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBusy")
            {
                //Dispatcher.BeginInvoke(new Action(() => { _busyBorder.Refresh(); }), System.Windows.Threading.DispatcherPriority.Render, null);
            }
        }


        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public MainViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is MainViewModel))
                    return null;
                return DataContext as MainViewModel;
            }
            set { DataContext = value; }
        }

        public void StarteTab(string toolName)
        {
            if (VM == null || toolName == null || !Tool.ToolListe.ContainsKey(toolName))
                return;
            var t = Tool.ToolListe[toolName];
            VM.OpenTool(t);
        }

        //public void AddMenuExternesProgramm(Model.MenuLink mlink)
        //{
        //    if (VM == null || mlink == null)
        //        return;
        //    VM.AddExternesProgramm(mlink);
        //}

        private void TabItemControl_RefreshDatumAktuell(object sender, EventArgs e)
        {
            foreach (var tab in _tabControlMain.Items)
            {
                if (tab is TabItem)
                {
                    if (((TabItem)tab).Content is ZooBot.ZooBotView)
                        ((ZooBot.ZooBotView)((TabItem)tab).Content).VM.SetHeldWerte();
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
                        ((ZooBot.ZooBotView)((TabItem)tab).Content).VM.SetHeldWerte(talentname);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Schließe Spieler-Fenster
            SpielerWindow.Close();
            SpielerInfoPreviewWindow.Close();
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
            StarteTab("SpielerInfo");
            SpielerWindow.Show();
        }

        private void MenuItemUpdateCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdates(true);
        }

        private void CheckForUpdates(bool popupWhenUpToDate)
        {
            _statusInfoTextBox.Text = "Es wird auf neue Updates geprüft...";
            _statusBar.Visibility = System.Windows.Visibility.Visible;

            // Update-Prüfung in einem eigenen Thread ausführen
            var bw = new System.ComponentModel.BackgroundWorker();

            // define the event handlers
            bw.DoWork += (sender, args) => {
                App.CheckUpdates(popupWhenUpToDate);
            };
            bw.RunWorkerCompleted += (sender, args) =>
            {
                _statusInfoTextBox.Text = string.Empty;
                _statusBar.Visibility = System.Windows.Visibility.Collapsed;
            };

            bw.RunWorkerAsync(); // starts the background worker
        }

        private void MenuItemEinstellungen_Click(object sender, RoutedEventArgs e)
        {
            EinstellungenWindow gui = new EinstellungenWindow();
            gui.Owner = this;
            gui.ShowDialog();
        }

        private void MenuItemHilfeÜber_Click(object sender, RoutedEventArgs e)
        {
            OpenInfoWindow();
        }

        private void MenuItemSysInfo_Click(object sender, RoutedEventArgs e)
        {
            MsgWindow sysInfoview = new MsgWindow();
            sysInfoview.Owner = this;
            sysInfoview.ShowDialog();
        }

        private static void OpenInfoWindow()
        {
            InfoWindow info = new InfoWindow();
            info.Owner = App.Current.MainWindow;
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
            System.Diagnostics.Process.Start("http://forum.meistergeister.org/");
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

        private void MenuItemWeb_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(((MenuItem)sender).Tag.ToString());
        }

        private void ButtonWürfel_Wx_Click(object sender, RoutedEventArgs e)
        {
            View.General.ViewHelper.OpenWürfelDialog(string.Empty);
        }

        private void ButtonWürfel_W20_Click(object sender, RoutedEventArgs e)
        {
            View.General.ViewHelper.OpenWürfelDialog("1W20");
        }

        private void ButtonWürfel_W6_Click(object sender, RoutedEventArgs e)
        {
            View.General.ViewHelper.OpenWürfelDialog("1W6");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Regeledition prüfen und festlegen
            if (string.IsNullOrEmpty(Global.Regeledition))
            {
                // noch keine Regeledition festgelegt, also abfragen...
                View.Windows.RegeleditionWindow regWin = new RegeleditionWindow();
                regWin.Owner = this;
                regWin.ShowDialog();
                regWin = null;
                Application.Current.Shutdown();
                return;
            }
            // wenn kein Jingle abgespielt wird, kann das Startup-Fenster spätestens jetzt geschlossen werden
#if !(NO_JINGLE)
            if (Einstellungen.JingleAbstellen)
#endif
            App.CloseSplashScreen();

#if TEST
            MessageBox.Show("ACHTUNG: Dies ist eine interne Test-Version!\n"
                      + "\nSie ist nicht mit dem normalen Release-Zweig kompatibel und dient nur internen Testzwecken!", "Test-Version",
                      MessageBoxButton.OK, MessageBoxImage.Warning);
#endif

#if !(DEBUG)
            // UpdateCheck (nicht ausführen, wenn von IDE ausgeführt)
            if (System.Diagnostics.Debugger.IsAttached == false && Einstellungen.CheckForUpdates && DateTime.Parse(Einstellungen.LastUpdateCheck).Date.CompareTo(DateTime.Now.Date) != 0)
                CheckForUpdates(false);

            // ChangeLog Meldung (nicht ausführen, wenn von IDE ausgeführt)
            if (System.Diagnostics.Debugger.IsAttached == false && Einstellungen.ShowChangeLog)
                ViewHelper.ShowBrowserChangeLog(true);
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

        
        private void Grid_Drop(object sender, DragEventArgs e)
        {            
            MeisterGeister.ViewModel.AudioPlayer.Logic.lbEditorItemVM source = e.Data.GetData("lbiPlaylistVM") as
                MeisterGeister.ViewModel.AudioPlayer.Logic.lbEditorItemVM;
            if (source != null && source.APlaylist.Hintergrundmusik)
            {
                source.APlaylist.Favorite = true;
                Global.ContextAudio.Update<MeisterGeister.Model.Audio_Playlist>(source.APlaylist);
                VM.UpdateFavorites();
            }
            else
            {
                MeisterGeister.ViewModel.AudioPlayer.Logic.lbEditorThemeItemVM sourceTheme = e.Data.GetData("lbiThemeVM") as
                    MeisterGeister.ViewModel.AudioPlayer.Logic.lbEditorThemeItemVM;
                if (sourceTheme != null)
                {
                    sourceTheme.ATheme.Favorite = true;
                    Global.ContextAudio.Update<MeisterGeister.Model.Audio_Theme>(sourceTheme.ATheme);
                    VM.UpdateFavorites();
                }
            }
        }

    }
}
