using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MeisterGeister.Daten;
using System.Linq;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
using System.Collections.Generic;
// Eigene Usings
using VM = MeisterGeister.ViewModel;
using MeisterGeister.Logic.General;
using MeisterGeister.View.Windows;
using MeisterGeister.View.Kampf;
using System.IO;

namespace MeisterGeister.View.Helden
{
    /// <summary>
    /// Interaktionslogik für HeldenView.xaml
    /// </summary>
    public partial class HeldenView : System.Windows.Controls.UserControl
    {
        public HeldenView()
        {
            InitializeComponent();

#if !(DEBUG)
            _buttonExportDemo.Visibility = System.Windows.Visibility.Collapsed;
#endif

            _comboBoxTalentAktivieren.SelectedIndex = -1;
            _comboBoxSonderfertigkeit.SelectedIndex = -1;
            _comboBoxVorteil.SelectedIndex = -1;
            _comboBoxNachteil.SelectedIndex = -1;
            _comboBoxZauber.SelectedIndex = -1;
            _listBoxHelden.Tag = SelectedHeld;
        }

        private void ButtonHeldNeu_Click(object sender, RoutedEventArgs e)
        {
            _buttonHeldNeu.ContextMenu.IsOpen = true;
        }

        private void HeldNeu()
        {
            Held.Neu();
        }

        private void ListBoxHelden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
            {
                App.SaveAll();
                _listBoxHelden.Tag = SelectedHeld;
                Global.SelectedHeldGUID = SelectedHeld.Id;
                ((Held)_listBoxHelden.Tag).PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SelectedHeld_PropertyChanged);
                RefreshHeld();
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler HeldChanged;

        void SelectedHeld_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "This")
            {
                _energieControlAstralenergie.SetEnergie();
                _energieControlAusdauer.SetEnergie();
                _energieControlKarmaenergie.SetEnergie();
                _energieControlLebensenergie.SetEnergie();

                // Changed Event weitergeben
                if (HeldChanged != null)
                    HeldChanged(sender, e);
            }
        }

        //TODO DW: Trennen in Controls
        private void RefreshHeld(bool refreshRepräsentationen = true)
        {
            _labelAstralenergie.Visibility = System.Windows.Visibility.Visible;
            _labelKarmaenergie.Visibility = System.Windows.Visibility.Visible;
            _imageAeKeHinweis.Visibility = System.Windows.Visibility.Collapsed;

            Held held = (Held)_listBoxHelden.Tag;

            if (!held.Magiebegabt && !held.Geweiht)
                _imageAeKeHinweis.Visibility = System.Windows.Visibility.Visible;

            // Magiebegabung und Astralenergie
            if (!held.Magiebegabt)
            {
                _labelAstralenergie.Visibility = System.Windows.Visibility.Collapsed;
                if (tabControl1.SelectedItem == _tabItemZauber)
                    tabControl1.SelectedItem = _tabItemTalente;
            }
            else
            {
                if (refreshRepräsentationen)
                    _comboBoxRepräsentation.SelectedValue = held.RepräsentationStandard();

                // Zauber-Sortierung aktualisieren
                if (_dataGridHeldZauber.Items is System.Windows.Data.CollectionView)
                {
                    System.Windows.Data.CollectionViewSource csv = (System.Windows.Data.CollectionViewSource)FindResource("heldHeld_ZauberViewSource");
                    if (csv != null && csv.View != null)
                    {
                        csv.View.SortDescriptions.Clear();
                        csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Zaubername", System.ComponentModel.ListSortDirection.Ascending));
                        csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Repräsentation", System.ComponentModel.ListSortDirection.Ascending));
                        _dataGridHeldZauber.ItemsSource = csv.View;
                    }
                }
            }
            // Geweiht und Karmaenergie
            if (!held.Geweiht)
                _labelKarmaenergie.Visibility = System.Windows.Visibility.Collapsed;

            // Talente-Sortierung aktualisieren
            if (_dataGridHeldTalente.Items is System.Windows.Data.CollectionView)
            {
                System.Windows.Data.CollectionViewSource csv = (System.Windows.Data.CollectionViewSource)FindResource("heldHeld_TalentViewSource");
                if (csv != null && csv.View != null)
                {
                    csv.View.SortDescriptions.Clear();
                    csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("TalentgruppeID", System.ComponentModel.ListSortDirection.Ascending));
                    csv.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Talentname", System.ComponentModel.ListSortDirection.Ascending));
                    _dataGridHeldTalente.ItemsSource = csv.View;
                }
            }            
        }

        public Held SelectedHeld
        {
            get
            {
                DatabaseDSADataSet.HeldRow heldRow = null;
                if (_listBoxHelden != null && _listBoxHelden.SelectedItem != null)
                    heldRow = (DatabaseDSADataSet.HeldRow)((System.Data.DataRowView)_listBoxHelden.SelectedItem).Row;

                Held held = new Held(heldRow);
                return held;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.Helden.HeldenViewModel).LoadDaten();
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Helden-Verwaltung", "Beim Laden der Helden-Verwaltung ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }

            ListBoxHelden_SelectionChanged(sender, null);
        }

        private void HeldDelete()
        {
            Held h = SelectedHeld;
            if (MessageBox.Show(string.Format("Sind Sie sicher, dass Sie den Helden '{0}' löschen möchten?", h.Name), "Held löschen",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                h.Delete();
                _listBoxHelden.SelectedIndex = -1;
            }
        }

        private void ListBoxHelden_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_listBoxHelden.SelectedItem != null)
            {
                switch (e.Key)
                {
                    // Aktion löschen
                    case Key.Delete:
                        HeldDelete();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MenuItemHeldLöschen_Click(object sender, RoutedEventArgs e)
        {
            HeldDelete();
        }

        private void MenuItemHeldNeu_Click(object sender, RoutedEventArgs e)
        {
            HeldNeu();
        }

        private void MenuItemHeldExport_Click(object sender, RoutedEventArgs e)
        {
            var objDialog = new SaveFileDialog();
            objDialog.Title = "Held exportieren";
            objDialog.Filter = "XML-Dateien (*.xml)|*.xml";
            objDialog.DefaultExt = "xml";
            objDialog.AddExtension = true;
            objDialog.FileName = SelectedHeld.Name;
            objDialog.InitialDirectory = _workingPath;
            DialogResult objResult = objDialog.ShowDialog();
            if (objResult == DialogResult.OK)
            {
                try
                {
                    string expPfad = SelectedHeld.ExportHeld(objDialog.FileName);
                    _workingPath = expPfad.Replace(objDialog.FileName, null);

                    MessageBox.Show("Der Held wurde in \'" + expPfad + "\' gespeichert.");
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("Fehler beim Export", "Beim Export des Helden ist ein Fehler aufgetreten!", ex);
                    errWin.ShowDialog();
                }
            }
        }

        private string _workingPath = Environment.CurrentDirectory;

        private void MenuItemHeldImport_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Beim Importieren wird der ausgewählte Held überschrieben. Soll der Vorgang fortgesetzt werden?", "Held importieren",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            var objDialog = new OpenFileDialog();
            objDialog.Title = "Held importieren";
            objDialog.Filter = "MeisterGeister und Helden-Software Dateien (*.xml)|*.xml";
            objDialog.DefaultExt = "xml";
            objDialog.AddExtension = true;
            objDialog.FileName = SelectedHeld.Name;
            objDialog.InitialDirectory = _workingPath;
            DialogResult objResult = objDialog.ShowDialog();
            if (objResult == DialogResult.OK)
            {
                try
                {
                    string expPfad = SelectedHeld.ImportHeld(objDialog.FileName);
                    _workingPath = expPfad.Replace(objDialog.SafeFileName, null);

                    MessageBox.Show("Der Held wurde aus \'" + expPfad + "\' importiert.");
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("Fehler beim Import", "Beim Import ist ein Fehler aufgetreten!", ex);
                    errWin.ShowDialog();
                }
            }
        }

        private void MenuItemHeldImportNeu_Click(object sender, RoutedEventArgs e)
        {
            var objDialog = new OpenFileDialog();
            objDialog.Title = "Neuen Helden importieren";
            objDialog.Filter = "MeisterGeister und Helden-Software Dateien (*.xml)|*.xml";
            objDialog.DefaultExt = "xml";
            objDialog.AddExtension = true;
            objDialog.FileName = SelectedHeld.Name;
            objDialog.InitialDirectory = _workingPath;
            DialogResult objResult = objDialog.ShowDialog();
            if (objResult == DialogResult.OK)
            {
                try
                {
                    string expPfad = Held.ImportHeldNeu(objDialog.FileName);
                    _workingPath = expPfad.Replace(objDialog.SafeFileName, null);

                    MessageBox.Show("Der Held wurde aus \'" + expPfad + "\' importiert."
                        + "\n\nHINWEIS: Helden können auch per Drag&Drop importiert werden.");
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("Fehler beim Import", "Beim Import ist ein Fehler aufgetreten!", ex);
                    errWin.ShowDialog();
                }
            }
        }

        private void MenuItemHeldKopieren_Click(object sender, RoutedEventArgs e)
        {
            SelectedHeld.Kopie();
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHelden.SelectedItem == null)
            {
                _menuItemHeldLöschen.IsEnabled = false;
                _menuItemHeldImport.IsEnabled = false;
                _menuItemHeldExport.IsEnabled = false;
                _menuItemHeldKopie.IsEnabled = false;
            }
            else
            {
                _menuItemHeldLöschen.IsEnabled = true;
                _menuItemHeldImport.IsEnabled = true;
                _menuItemHeldExport.IsEnabled = true;
                _menuItemHeldKopie.IsEnabled = true;
            }
        }

        private void ListBoxHelden_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxHelden.SelectedItem = null;
        }

        private void _listBoxHeldSonderfertigkeiten_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_listBoxHeldSonderfertigkeiten.SelectedItem != null)
            {
                switch (e.Key)
                {
                    // Sonderfertigkeit löschen
                    case Key.Delete:
                        DeleteHeldSonderfertigkeit();
                        break;
                    default:
                        break;
                }
            }
        }

        private DatabaseDSADataSet.Held_SonderfertigkeitRow SelectedSonderfertigkeit
        {
            get
            {
                DatabaseDSADataSet.Held_SonderfertigkeitRow sfRow = null;
                if (_listBoxHeldSonderfertigkeiten.SelectedItem != null)
                    sfRow = (DatabaseDSADataSet.Held_SonderfertigkeitRow)((System.Data.DataRowView)_listBoxHeldSonderfertigkeiten.SelectedItem).Row;
                return sfRow;
            }
        }

        private void ListBoxSonderfertigkeiten_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxHeldSonderfertigkeiten.SelectedItem = null;
        }

        private void ContextMenuSonderfertigkeiten_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHeldSonderfertigkeiten.SelectedItem == null)
            {
                _menuItemSonderfertigkeitLöschen.IsEnabled = false;
                _menuItemSonderfertigkeitlWiki.IsEnabled = false;
            }
            else
            {
                _menuItemSonderfertigkeitLöschen.IsEnabled = true;
                _menuItemSonderfertigkeitlWiki.IsEnabled = true;
            }
        }

        private void MenuItemSonderfertigkeitLöschen_Click(object sender, RoutedEventArgs e)
        {
            DeleteHeldSonderfertigkeit();
        }

        private void ListBoxHeldVorNachteile_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_listBoxHeldVorNachteile.SelectedItem != null)
            {
                switch (e.Key)
                {
                    // VorNachteil löschen
                    case Key.Delete:
                        DeleteHeldVorNachteil();
                        break;
                    default:
                        break;
                }
            }
        }

        private DatabaseDSADataSet.Held_VorNachteilRow SelectedVorNachteil
        {
            get
            {
                DatabaseDSADataSet.Held_VorNachteilRow vorNachRow = null;
                if (_listBoxHeldVorNachteile.SelectedItem != null)
                    vorNachRow = (DatabaseDSADataSet.Held_VorNachteilRow)((System.Data.DataRowView)_listBoxHeldVorNachteile.SelectedItem).Row;

                return vorNachRow;
            }
        }

        private DatabaseDSADataSet.Held_TalentRow SelectedTalentRow
        {
            get
            {
                DatabaseDSADataSet.Held_TalentRow talentRow = null;
                if (_dataGridHeldTalente.SelectedItem != null)
                    talentRow = (DatabaseDSADataSet.Held_TalentRow)((System.Data.DataRowView)_dataGridHeldTalente.SelectedItem).Row;

                return talentRow;
            }
        }

        private DatabaseDSADataSet.Held_ZauberRow SelectedZauberRow
        {
            get
            {
                DatabaseDSADataSet.Held_ZauberRow zauberRow = null;
                if (_dataGridHeldZauber.SelectedItem != null)
                    zauberRow = (DatabaseDSADataSet.Held_ZauberRow)((System.Data.DataRowView)_dataGridHeldZauber.SelectedItem).Row;

                return zauberRow;
            }
        }

        private Talent SelectedTalent
        {
            get { return new Talent(SelectedTalentRow.TalentRow); }
        }

        private Zauber SelectedZauber
        {
            get { return new Zauber(SelectedZauberRow.ZauberRow); }
        }

        private void DeleteHeldVorNachteil()
        {
            DatabaseDSADataSet.Held_VorNachteilRow vorNach = SelectedVorNachteil;
            if (MessageBox.Show(string.Format("Soll der Vor-/Nachteil '{0}' entfernt werden?", vorNach.VorNachteilRow.Name), "Vor-/Nachteil entfernen", MessageBoxButton.YesNo,
                                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                {
                    SelectedHeld.DeleteVorNachteil(vorNach);
                    _listBoxHeldVorNachteile.SelectedIndex = -1;
                }
            }
        }

        private bool DeleteHeldTalent()
        {
            bool deleted = false;
            DatabaseDSADataSet.Held_TalentRow talent = SelectedTalentRow;
            if (MessageBox.Show(string.Format("Soll das Talent '{0}' entfernt werden?", talent.TalentRow.Talentname), "Talent entfernen", MessageBoxButton.YesNo,
                                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                SelectedHeld.DeleteTalent(talent);
                deleted = true;
            }
            return deleted;
        }

        private void DeleteHeldSonderfertigkeit()
        {
            DatabaseDSADataSet.Held_SonderfertigkeitRow sf = SelectedSonderfertigkeit;
            if (MessageBox.Show(string.Format("Soll die Sonderfertigkeit '{0}' entfernt werden?", sf.SonderfertigkeitRow.Name), "Sonderfertigkeit entfernen", MessageBoxButton.YesNo,
                                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                {
                    SelectedHeld.DeleteSonderfertigkeit(sf);
                    _listBoxHeldSonderfertigkeiten.SelectedIndex = -1;
                }
            }
        }

        private void ListBoxVorNachteile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxHeldVorNachteile.SelectedItem = null;
        }

        private void ContextMenuVorNachteile_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHeldVorNachteile.SelectedItem == null)
            {
                _menuItemVorNachteilLöschen.IsEnabled = false;
                _menuItemVorNachteilWiki.IsEnabled = false;
            }
            else
            {
                _menuItemVorNachteilLöschen.IsEnabled = true;
                _menuItemVorNachteilWiki.IsEnabled = true;
            }
        }

        private void MenuItemVorNachteilLöschen_Click(object sender, RoutedEventArgs e)
        {
            DeleteHeldVorNachteil();
        }

        private void _buttonExportDemo_Click(object sender, RoutedEventArgs e)
        {
            Held.GeneriereDemoHelden();
        }

        private void ContextMenuTalente_Opened(object sender, RoutedEventArgs e)
        {
            if (_dataGridHeldTalente.SelectedItem == null)
            {
                _menuItemTalentLöschen.IsEnabled = false;
                _menuItemTalentWiki.IsEnabled = false;
                _menuItemTalentProben.IsEnabled = false;
            }
            else
            {
                _menuItemTalentLöschen.IsEnabled = true;
                _menuItemTalentWiki.IsEnabled = true;
                _menuItemTalentProben.IsEnabled = true;
            }
        }

        private void ContextMenuZauber_Opened(object sender, RoutedEventArgs e)
        {
            if (_dataGridHeldZauber.SelectedItem == null)
            {
                _menuItemZauberLöschen.IsEnabled = false;
                _menuItemZauberWiki.IsEnabled = false;
            }
            else
            {
                _menuItemZauberLöschen.IsEnabled = true;
                _menuItemZauberWiki.IsEnabled = true;
            }
        }

        private void MenuItemTalentLöschen_Click(object sender, RoutedEventArgs e)
        {
            DeleteHeldTalent();
        }

        private void MenuItemTalentWiki_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedTalent.WikiLink);
        }

        public event ProbeWürfelnEventHandler ProbeWürfeln;

        private void MenuItemTalentProben_Click(object sender, RoutedEventArgs e)
        {
            if (ProbeWürfeln != null)
            {
                ProbeWürfeln(SelectedTalentRow.TalentRow.Talentname);
            }
        }

        private void MenuItemZauberProben_Click(object sender, RoutedEventArgs e)
        {
            if (ProbeWürfeln != null)
            {
                ProbeWürfeln(SelectedZauberRow.ZauberRow.Name);
            }
        }

        private void MenuItemVorNachteilWiki_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedVorNachteil.VorNachteilRow.Name);
        }

        private void MenuItemSonderfertigkeitWiki_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedSonderfertigkeit.SonderfertigkeitRow.Name);
        }

        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            switch (((System.Windows.Controls.Button)sender).Tag.ToString())
            {
                case "LE":
                    _intBoxLeAktuell.Value = Convert.ToInt32(_textBlockLebensenergieMax.Text);
                    break;
                case "AU":
                    _intBoxAuAktuell.Value = Convert.ToInt32(_textBlockAusdauerMax.Text);
                    break;
                case "AE":
                    _intBoxAeAktuell.Value = Convert.ToInt32(_textBlockAstralenergieMax.Text);
                    break;
                case "KE":
                    _intBoxKeAktuell.Value = Convert.ToInt32(_textBlockKarmaenergieMax.Text);
                    break;
                default:
                    break;
            }
        }
               
        
        
        private void _comboBoxTalentAktivieren_DropDownOpened(object sender, EventArgs e)
        {
            SetTalenteAktivierbar();
        }

        private void _comboBoxSonderfertigkeit_DropDownOpened(object sender, EventArgs e)
        {
            SetSonderfertigkeitenAktivierbar();
        }

        private void _comboBoxVorNachteil_DropDownOpened(object sender, EventArgs e)
        {
            SetVorNachteileAktivierbar();
        }

        private void SetTalenteAktivierbar()
        {
            _comboBoxTalentAktivieren.ItemsSource = SelectedHeld.TalenteAktivierbar;
            _comboBoxTalentAktivieren.SelectedIndex = -1;
        }

        private void SetSonderfertigkeitenAktivierbar()
        {
            _comboBoxSonderfertigkeit.ItemsSource = SelectedHeld.SonderfertigkeitenErlernbar;
            _comboBoxSonderfertigkeit.SelectedIndex = -1;
        }

        private void SetVorNachteileAktivierbar()
        {
            _comboBoxVorteil.ItemsSource = SelectedHeld.VorteileWählbar;
            _comboBoxVorteil.SelectedIndex = -1;
            _comboBoxNachteil.ItemsSource = SelectedHeld.NachteileWählbar;
            _comboBoxNachteil.SelectedIndex = -1;
        }

        private void _comboBoxTalentAktivieren_DropDownClosed(object sender, EventArgs e)
        {
            InsertTalent();
        }

        private void _comboBoxVorNachteil_DropDownClosed(object sender, EventArgs e)
        {
            if (sender == _comboBoxVorteil)
                InsertVorNachteil("Vorteil");
            else
                InsertVorNachteil("Nachteil");
        }

        private void _comboBoxSonderfertigkeit_DropDownClosed(object sender, EventArgs e)
        {
            InsertSonderfertigkeit();
        }

        private void _comboBoxTalentAktivieren_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                InsertTalent();
        }

        private void _comboBoxVorNachteil_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender == _comboBoxVorteil)
                    InsertVorNachteil("Vorteil");
                else
                    InsertVorNachteil("Nachteil");
            }
        }

        private void _comboBoxSonderfertigkeit_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                InsertSonderfertigkeit();
        }

        private void InsertTalent()
        {
            if (IsInitialized)
            {
                if (_comboBoxTalentAktivieren.SelectedItem != null && _comboBoxTalentAktivieren.SelectedItem is string && SelectedHeld.Id != Guid.Empty)
                {
                    string talent = _comboBoxTalentAktivieren.SelectedItem.ToString();
                    if (SelectedHeld.AddTalent(talent) == false)
                        MessageBox.Show(string.Format("Das Talent '{0}' ist bereits vorhanden.", talent.ToString()), "Talent hinzufügen");
                    RefreshHeld();
                    SetTalenteAktivierbar();
                }
            }
        }

        private void InsertSonderfertigkeit()
        {
            if (IsInitialized)
            {
                if (_comboBoxSonderfertigkeit.SelectedItem != null && SelectedHeld.Id != Guid.Empty)
                {
                    var sf = (System.Collections.Generic.KeyValuePair<string, int>)_comboBoxSonderfertigkeit.SelectedItem;
                    if (SelectedHeld.AddSonderfertigkeit(sf.Value) == false)
                        MessageBox.Show(string.Format("Die Sonderfertigkeit '{0}' ist bereits vorhanden.", sf.Key), "Sonderfertigkeit hinzufügen");
                    RefreshHeld();
                    SetSonderfertigkeitenAktivierbar();
                }
            }
        }

        private void InsertVorNachteil(string typ)
        {
            if (IsInitialized)
            {
                ComboBox comboVorNach;
                if (typ == "Vorteil")
                    comboVorNach = _comboBoxVorteil;
                else
                    comboVorNach = _comboBoxNachteil;

                if (comboVorNach.SelectedItem != null && SelectedHeld.Id != Guid.Empty)
                {
                    var vorNach = (System.Collections.Generic.KeyValuePair<string, int>)comboVorNach.SelectedItem;
                    if (SelectedHeld.AddVorNachteil(vorNach.Value) == false)
                    {
                        MessageBox.Show(string.Format("Der Vor-/Nachteil '{0}' ist bereits vorhanden.", vorNach.Key), "Vor-/Nachteil hinzufügen");
                    }
                    RefreshHeld();
                    SetVorNachteileAktivierbar();
                }
            }
        }

        private void _listBoxHelden_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is System.Windows.Controls.ListBox && e.Key == Key.Delete)
            {
                var liBo = (System.Windows.Controls.ListBox)sender;
                if (liBo.SelectedItem != null)
                    HeldDelete();
            }
        }

        private void _dataGridHeldTalente_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid && e.Key == Key.Delete)
            {
                var grid = (System.Windows.Controls.DataGrid)sender;

                if (grid.SelectedItems.Count > 0)
                {
                    e.Handled = !DeleteHeldTalent();
                }
            }
        }

        private void MenuItemHeldImportDemo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _listBoxHelden.UnselectAll();
                _listBoxHelden.SelectionChanged -= ListBoxHelden_SelectionChanged;
                Held.LadeDemoHelden();
                _listBoxHelden.SelectionChanged += ListBoxHelden_SelectionChanged;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.FileName + "\n" + ex.Message, "Demo-Helden laden", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("Die Demo-Helden Datei ist beschädigt." + "\n\n" + ex.Message, "Demo-Helden laden", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _comboBoxZauber_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                InsertZauber();
        }

        private void InsertZauber()
        {
            if (IsInitialized)
            {
                if (_comboBoxZauber.SelectedItem != null && SelectedHeld.Id != Guid.Empty)
                {
                    var zauber = (System.Collections.Generic.KeyValuePair<string, int>)_comboBoxZauber.SelectedItem;
                    string rep = _comboBoxRepräsentation.SelectedValue.ToString();
                    if (SelectedHeld.AddZauber(zauber.Value, rep) == false)
                        MessageBox.Show(string.Format("Der Zauber '{0}' in der Repräsentation '{1}' ist bereits vorhanden.", zauber.Key, rep), "Zauber hinzufügen");
                    RefreshHeld(false);
                    SetZauberAktivierbar();
                }
            }
        }

        private void SetZauberAktivierbar()
        {
            _comboBoxZauber.ItemsSource = Zauber.ZauberList;
            _comboBoxZauber.SelectedIndex = -1;
        }

        private void _comboBoxZauber_DropDownOpened(object sender, EventArgs e)
        {
            SetZauberAktivierbar();
        }

        private void _comboBoxZauber_DropDownClosed(object sender, EventArgs e)
        {
            InsertZauber();
        }

        private void _dataGridHeldZauber_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid && e.Key == Key.Delete)
            {
                var grid = (System.Windows.Controls.DataGrid)sender;

                if (grid.SelectedItems.Count > 0)
                {
                    DatabaseDSADataSet.Held_ZauberRow zauber = SelectedZauberRow;
                    if (MessageBox.Show(string.Format("Soll der Zauber '{0}' entfernt werden?", zauber.ZauberRow.Name), "Zauber entfernen", MessageBoxButton.YesNo,
                                        MessageBoxImage.Question, MessageBoxResult.Yes) != MessageBoxResult.Yes)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void MenuItemZauberLöschen_Click(object sender, RoutedEventArgs e)
        {
            DeleteHeldZauber();
        }

        private void DeleteHeldZauber()
        {
            DatabaseDSADataSet.Held_ZauberRow zauber = SelectedZauberRow;
            if (MessageBox.Show(string.Format("Soll der Zauber '{0}' entfernt werden?", zauber.ZauberRow.Name), "Zauber entfernen", MessageBoxButton.YesNo,
                                        MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                SelectedHeld.DeleteZauber(zauber);
            }
        }

        private void MenuItemZauberWiki_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedZauber.WikiLink);
        }                

        private void ListBoxHelden_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] droppedFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[];

                foreach (string file in droppedFiles)
                {
                    if (!file.EndsWith("xml"))
                    {
                        MessageBox.Show(file + "\n\nFalscher Dateityp!");
                        continue;
                    }
                    try
                {
                    string expPfad = Held.ImportHeldNeu(file);
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("Fehler beim Import", "Beim Import ist ein Fehler aufgetreten!", ex);
                    errWin.ShowDialog();
                }
                }
            }
        }
        
    }
}
