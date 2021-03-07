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
using Un4seen.Bass;


using Q42.HueApi;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.ColorConverters.OriginalWithModel;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.ColorConverters;
using MeisterGeister.ViewModel.Settings;
using Q42.HueApi.Models.Groups;
using Newtonsoft.Json;
using Q42.HueApi.Models;

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
            InitBASS();
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
            // BASS.Net freigeben
            Bass.FreeMe();
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

        public void InitBASS()
        {
            string appBaseDir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string sourcePath = appBaseDir + ((Utils.Is64Bit) ? @"\Audio\BASS\X64" : @"\Audio\BASS\X86");

            //Kopieren der BASS.DLL & BASS_FX.DLL 32bit oder 64bit Variante ins Hauptverzeichnis von MG
            try
            {
                File.Copy(sourcePath + @"\bass.dll", appBaseDir + @"\bass.dll", true);
                File.Copy(sourcePath + @"\bass_fx.dll", appBaseDir + @"\bass_fx.dll", true);
            }
            finally
            {
                int i = Bass.BASS_PluginLoad("basswma.dll");
                if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                {
                    BASS_INFO info = new BASS_INFO();
                    Bass.BASS_GetInfo(info);
                    Console.WriteLine(info.ToString());
                    Dictionary<int, string> lst = Bass.BASS_PluginLoadDirectory(Environment.CurrentDirectory + @"\Audio\BASS\Plugin\");
                }
            }
        }

        #region ---- HUE - LAMPE ----
        private Boolean IsMouseDown = false;

        private void CanvasImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;
            UpdateColor();
            if (VM.Client != null)
            {
                //Control the lights                
                LightCommand command = new LightCommand();
                command.TurnOn().SetColor(new RGBColor(VM.SelectedColor.R, VM.SelectedColor.G, VM.SelectedColor.B));
                command.Brightness = (byte)BrightnessSlider.Value;
                if (VM.HUELampenSelected && VM.HUELightsSelected.Count != 0 )
                {
                    VM.Client.SendCommandAsync(command, VM.lstHUELights.Where(t => VM.HUELightsSelected.Select(z => z.Id).Contains(t.Id)).Select(t => t.Id).ToList());
                }
                else
                if (VM.HUEGruppenSelected && VM.lstHUEGroups.Count != 0 && VM.Client != null)
                {
                    foreach (string hgString in VM.lstHUEGroups.Where(t => VM.HUEGroupsSelected.Select(z => z.Id).Contains(t.Id)).Select(t => t.Id).ToList())
                        VM.Client.SendGroupCommandAsync(command, hgString);
                }
            }
        }

        ///// <summary>
        ///// Send command to a group
        ///// </summary>
        ///// <param name="command"></param>
        ///// <param name="group"></param>
        ///// <returns></returns>
        //public Task<HueResults> SendGroupCommandAsync(LightCommand command, string group = "0")
        //{
        //    if (command == null)
        //        throw new ArgumentNullException("command");

        //    string jsonCommand = JsonConvert.SerializeObject(command, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

        //    return SendGroupCommandAsync(jsonCommand, group);
        //}


        private void CanvasImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = false;
            //UpdateColor();
        }

        private void CanvasImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
                UpdateColor();
        }


        /// <summary>
        /// Sets a new Selected Color based on the color of the pixel under the mouse pointer.
        /// </summary>
        private void UpdateColor()
        {
            // Test to ensure we do not get bad mouse positions along the edges
            int imageX = (int)Mouse.GetPosition(canvasImage).X;
            int imageY = (int)Mouse.GetPosition(canvasImage).Y;
            if ((imageX < 0) || (imageY < 0) || (imageX > ColorImage.Width - 1) || (imageY > ColorImage.Height - 1))
                return;
            // Get the single pixel under the mouse into a bitmap and copy it to a byte array
            CroppedBitmap cb = new CroppedBitmap(ColorImage.Source as BitmapSource, new Int32Rect(imageX, imageY, 1, 1));
            byte[] pixels = new byte[4];
            cb.CopyPixels(pixels, 4, 0);
            // Update the mouse cursor position and the Selected Color
            ellipsePixel.SetValue(Canvas.LeftProperty, (double)(Mouse.GetPosition(canvasImage).X - (ellipsePixel.Width / 2.0)));
            ellipsePixel.SetValue(Canvas.TopProperty, (double)(Mouse.GetPosition(canvasImage).Y - (ellipsePixel.Width / 2.0)));
            canvasImage.InvalidateVisual();
            // Set the Selected Color based on the cursor pixel and Alpha Slider value
            VM.SelectedColor = Color.FromArgb((byte)BrightnessSlider.Value, pixels[2], pixels[1], pixels[0]);
        }

        private void HUESlider_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int change = e.Delta / Math.Abs(e.Delta);
            ((Slider)sender).Value = ((Slider)sender).Value + (double)change*10;
        }


        #endregion

        private void SaettigungSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void HUESlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        { 
            try
            {
                var alt = e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Alt);
                var ctrl = e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control);
                var altGr = alt & ctrl;
                if (VM.hotkeyListUsed.Count > 0 && altGr)
                {
                    string s = Convert.ToString(e.Key);
                    if (e.Key >= Key.D0 && e.Key <= Key.D9)
                        s = s.Remove(0, s.Length - 1);

                    AudioPlayer.btnHotkey hkey = VM.hotkeyListUsed.FindAll(t => t.VM.aPlaylistGuid != (Guid.Empty)).
                        FirstOrDefault(t => Convert.ToChar(t.VM.taste).ToString() == s);
                    if (hkey != null)
                    {
                        e.Handled = true;
                        hkey.VM.OnBtnClick(hkey.btn);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswerten des Tastenklicks ist ein Fehler aufgetreten.", ex);
            }
        }

        private void lvHUELights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.HUELampenSelected = true;
            List<Light> HUEListLampenSelected = new List<Light>();

            foreach (var item in (e.Source as ListView).SelectedItems)
            {
                if (item is Light)
                    HUEListLampenSelected.Add(item as Light);
            }
            VM.HUELightsSelected = HUEListLampenSelected;
        }

        private void lvHUEGruppen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.HUEGruppenSelected = true;
            List<Group> HUEListGruppenSelected = new List<Group>();

            foreach (var item in (e.Source as ListView).SelectedItems)
            {
                if (item is Group)
                    HUEListGruppenSelected.Add(item as Group);
            }
            VM.HUEGroupsSelected = HUEListGruppenSelected;
        }

        private void lvHUEScenen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Scene> HUEListScenenSelected = new List<Scene>();

            foreach (var item in (e.Source as ListView).SelectedItems)
            {
                if (item is Scene)
                    HUEListScenenSelected.Add(item as Scene);
            }
            VM.HUEScenenSelected = HUEListScenenSelected;
        }

        private void btnSceneGo_Click(object sender, RoutedEventArgs e)
        {
            if (VM.Client != null)
            {
                foreach (Scene s in VM.HUEScenenSelected)
                {
                    var command = new SceneCommand { Scene = s.Id };
                    VM.Client.SendGroupCommandAsync(command, s.Group);
                }
            }

            lvHUEScenes.SelectedIndex = -1;
        }
    }
}
