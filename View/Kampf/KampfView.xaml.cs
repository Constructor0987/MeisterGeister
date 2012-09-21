using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeisterGeister.Daten;
using System.Collections.Generic;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Settings;
using MeisterGeister.View.Arena;
using MeisterGeister.ViewModel.Kampf.LogicAlt;

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für KampfView.xaml
    /// </summary>
    public partial class KampfView : UserControl
    {
        public KampfView()
        {
            InitializeComponent();
            _listBoxKämpfer.DataContext = _kampf.KämpferListe;
            _listBoxAktionen.ItemsSource = _kampf.AktionenListe;

            _kampf.NächsterKämpferRollover += NächsterKämpferRollover_EventHandler;

            _comboBoxTrefferzone.ItemsSource = Trefferzone.TrefferzonenListe();
            _comboBoxTrefferzone.SelectedIndex = 0;

#if !(DEBUG)
            _stackPanelAktionIn.Visibility = Visibility.Collapsed;
            _stackPanelAktionen.Visibility = Visibility.Collapsed;
            _buttonKriegskunstProbe.Visibility = Visibility.Collapsed;
#endif
        }

        private MeisterGeister.ViewModel.Kampf.LogicAlt.Kampf _kampf = new MeisterGeister.ViewModel.Kampf.LogicAlt.Kampf();

        public MeisterGeister.ViewModel.Kampf.LogicAlt.Kampf Kampf
        {
            get { return _kampf; }
        }

        public void HeldenEinfügen()
        {
            foreach (DatabaseDSADataSet.HeldRow heldRow in App.DatenDataSet.Held)
            {
                if (heldRow.AktiveHeldengruppe)
                {
                    Held h = _kampf.AddHeld(heldRow);
                    h.ProbeWürfelnEvent += WesenProbeWürfeln_Event;
                }
            }
            SortKämpfer();
        }

        public LogicAlt.General.ProbenErgebnis WesenProbeWürfeln_Event(Wesen wesen, IProbe probe, string aktion)
        {
            int wert = 0;
            if (wesen is Held)
            {
                wert = ((Held)wesen).Eigenschaft(probe.Name);
            }
            LogicAlt.General.ProbenErgebnis pErgebnis = new LogicAlt.General.ProbenErgebnis();
            if (MessageBox.Show(string.Format("{0}: {1}-Probe ({2}).\nGelungen?", aktion, probe.Name,
                wert), wesen.Name,
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                pErgebnis.Gelungen = true;
            }
            else
            {
                pErgebnis.Gelungen = false;
            }

            return pErgebnis;
        }

        public void ClearKämpferListe()
        {
            _kampf.KämpferListe.Clear();
            SortKämpfer();
        }

        private IKämpfer _selectedKämpfer;

        private KampfAktion _selectedAktion;

        private void ListBoxKämpfer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _radioButtonAktion1.Content = "1. Aktion";
            _radioButtonAktion2.Content = "2. Aktion";
            if (_listBoxKämpfer.SelectedItem != null && _listBoxKämpfer.SelectedItem is IKämpfer)
            {
                _selectedKämpfer = (IKämpfer)_listBoxKämpfer.SelectedItem;
                _textBoxAktionQuelle.Text = _selectedKämpfer.Kurzname;
                _radioButtonAktion1.Content += string.Format(" [{0}]", _selectedKämpfer.Initiative);
                _radioButtonAktion2.Content += string.Format(" [{0}]", _selectedKämpfer.Initiative - 8);
            }
            else
            {
                _selectedKämpfer = null;
                _textBoxAktionQuelle.Text = string.Empty;
            }
        }

        private void SetLebensenergie()
        {
            _energieControlLebensenergie.SetEnergie();
        }

        private void SetAusdauer()
        {
            _energieControlAusdauer.SetEnergie();
        }

        private void SetAstralenergie()
        {
            _energieControlAstralenergie.SetEnergie();
        }

        private void SetKarmaenergie()
        {
            _energieControlKarmaenergie.SetEnergie();
        }

        private void ListBoxKämpfer_KeyUp(object sender, KeyEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                switch (e.Key)
                {
                    // Kämpfer löschen
                    case Key.Delete:
                        KämpferEntfernen();
                        break;
                    // Initiative erhöhen
                    case Key.OemPlus:
                        InitiativeErhöhen();
                        break;
                    case Key.Add:
                        InitiativeErhöhen();
                        break;
                    // Initiative senken
                    case Key.OemMinus:
                        InitiativeSenken();
                        break;
                    case Key.Subtract:
                        InitiativeSenken();
                        break;
                    default:
                        break;
                }
            }
        }

        private void KämpferEntfernen()
        {
            if (MessageBox.Show("Soll der Kämpfer entfernt werden?", "Kämpfer entfernen", MessageBoxButton.YesNo,
                                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                _kampf.KämpferListe.Remove(_selectedKämpfer);
                SortKämpfer();
            }
        }

        private void InitiativeSenken()
        {
            _selectedKämpfer.InitiativeMod--;
            SortKämpfer();
            RefreshAktionen();
        }

        public void SortKämpfer()
        {
            _kampf.KämpferListe.Sort();
            _listBoxKämpfer.Items.Refresh();

            // Liste im Spieler-Info-Fenster aktualisieren
            if (MainView.WindowSpieler != null && MainView.WindowSpieler.IsKampfInfoModus)
                ShowSpielerInfo();
        }

        private void RefreshAktionen()
        {
            _listBoxAktionen.Items.Refresh();
        }

        private void InitiativeErhöhen()
        {
            _selectedKämpfer.InitiativeMod++;
            SortKämpfer();
            RefreshAktionen();
        }

        private void ButtonNeueKampfrunde_Click(object sender, RoutedEventArgs e)
        {
            NeueKampfrunde();
        }

        private void NeueKampfrunde()
        {
            _kampf.NeueKampfrunde();
            if (_kampf.Kampfrunde > 0)
                _buttonNeueKR.Content = "Neue KR";
            else
                _buttonNeueKR.Content = "Starten";
            SetKampfrunde();
            RefreshAktionen();

            ListBoxKämpfer_FocusItem();
        }

        private void SetKampfrunde()
        {
            _textBlockKampfrunde.Text = _kampf.Kampfrunde.ToString();
            _textBlockKampfZeit.Text = _kampf.KampfZeit.ToString();
            _kampf.AktionenListe.Sort();
            SortKämpfer();
            RefreshAktionen();
        }

        private void ButtonNeuerKampf_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Soll ein neuer Kampf gestartet werden?", "Neuer Kampf", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                _kampf.NeuerKampf();
                _buttonNeueKR.Content = "Starten";
                _listBoxKämpfer.DataContext = _kampf.KämpferListe;
                _listBoxAktionen.ItemsSource = _kampf.AktionenListe;
                SetKampfrunde();
                SortKämpfer();
            }
        }

        public event ProbeWürfelnEventHandler ProbeWürfeln;

        private void ButtonKriegskunstProbe_Click(object sender, RoutedEventArgs e)
        {
            if (ProbeWürfeln != null)
            {
                ProbeWürfeln("Kriegskunst");
            }
        }

        private void ButtonAktion_Click(object sender, RoutedEventArgs e)
        {
            AddAktion();
        }

        private void AddAktion()
        {
            Aktion ak = Aktion.Aktion1;
            if (_radioButtonAktion1.IsChecked == true)
            {
                ak = Aktion.Aktion1;
            }
            else if (_radioButtonAktion2.IsChecked == true)
            {
                ak = Aktion.Aktion2;
            }

            _kampf.AddAktion(_textBoxAktionQuelle.Text, _textBoxAktionName.Text,
                           _intBoxAktionDauer.Value, _selectedKämpfer, ak);

            SortKämpfer();
            RefreshAktionen();
        }

        private void ListBoxAktionen_KeyUp(object sender, KeyEventArgs e)
        {
            if (_selectedAktion != null)
            {
                switch (e.Key)
                {
                    // Aktion löschen
                    case Key.Delete:
                        AktionEntfernen();
                        break;
                    default:
                        break;
                }
            }
        }

        private void AktionEntfernen()
        {
            if (MessageBox.Show("Soll die Aktion entfernt werden?", "Aktion entfernen", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                _kampf.AktionenListe.Remove(_selectedAktion);
                if (_selectedAktion.Kämpfer != null)
                    _selectedAktion.Kämpfer.AktionenLaufend.Remove(_selectedAktion);
                SortKämpfer();
                RefreshAktionen();
            }
        }

        private void ListBoxAktionen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listBoxAktionen.SelectedItem != null && _listBoxAktionen.SelectedItem is KampfAktion)
            {
                _selectedAktion = (KampfAktion)_listBoxAktionen.SelectedItem;
            }
            else
            {
                _selectedAktion = null;
            }
        }

        private void WürfelControlInitiative_WürfelGeändert(uint ergebnis)
        {
            _selectedKämpfer.InitiativeWurf = ergebnis;
            SortKämpfer();
            RefreshAktionen();
        }

        private void ButtonSchadenspunkte_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                _selectedKämpfer.Schadenspunkte(_intBoxSchaden.Value, Trefferzone.GetTrefferzoneEnum(_comboBoxTrefferzone.SelectedValue.ToString()), 
                    (bool)_radioButtonWundschwelleGesenkt.IsChecked, (bool)_radioButtonWundeKeine.IsChecked);
                SetLebensenergie();
            }
        }

        private void ButtonAstralpunkte_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                _selectedKämpfer.AstralenergieAktuell -= _intBoxSchaden.Value;
                SetAstralenergie();
            }
        }

        private void ButtonKarmapunkte_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                _selectedKämpfer.KarmaenergieAktuell -= _intBoxSchaden.Value;
                SetKarmaenergie();
            }
        }

        private void ButtonAttacke_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                _kampf.AddAktion(_selectedKämpfer.Name, "Attacke", 1, _selectedKämpfer, GetAktion());

                SortKämpfer();
                RefreshAktionen();
            }
        }

        private void ButtonOrientieren_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                int dauer = 2;
                if (_selectedKämpfer is Wesen)
                {
                    if (((Wesen)_selectedKämpfer).HatAufmerksamkeit
                        && ((Wesen)_selectedKämpfer).HatVoraussetzungenAufmerksamkeit)
                    {
                        dauer = 1;
                    }
                }
                _kampf.AddAktion(_selectedKämpfer.Name, "Orientieren", dauer, _selectedKämpfer, GetAktion(),
                               _selectedKämpfer.Orientieren);

                SortKämpfer();
                RefreshAktionen();
            }
        }

        private Aktion GetAktion()
        {
            Aktion ak = Aktion.Aktion1;
            if (_radioButtonAktion1.IsChecked == true)
            {
                ak = Aktion.Aktion1;
            }
            else if (_radioButtonAktion2.IsChecked == true)
            {
                ak = Aktion.Aktion2;
            }
            return ak;
        }

        private void ButtonTrefferpunkte_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                _selectedKämpfer.Trefferpunkte(_intBoxSchaden.Value, Trefferzone.GetTrefferzoneEnum(_comboBoxTrefferzone.SelectedValue.ToString()), 
                    (bool)_radioButtonWundschwelleGesenkt.IsChecked, (bool)_radioButtonWundeKeine.IsChecked);
                SetLebensenergie();
            }
        }

        private void ListBoxAktionen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_selectedAktion != null)
            {
                _selectedAktion.AktionAusführen();
                _kampf.AktionenListe.Remove(_selectedAktion);
                if (_selectedAktion.Kämpfer != null)
                    _selectedAktion.Kämpfer.AktionenLaufend.Remove(_selectedAktion);
                SortKämpfer();
                RefreshAktionen();
            }
        }

        private void ListBoxKämpfer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxKämpfer.SelectedItem = null;
        }

        private void ListBoxAktionen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxAktionen.SelectedItem = null;
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxKämpfer.SelectedItem == null)
            {
                _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void MenuItemKämpferEntfernen_Click(object sender, RoutedEventArgs e)
        {
            KämpferEntfernen();
        }

        private void MenuItemHeldenEinfügen_Click(object sender, RoutedEventArgs e)
        {
            HeldenEinfügen();
        }

        private void MenuItemKämpferEinfügen_Click(object sender, RoutedEventArgs e)
        {
            ShowKämpferVerwaltung();
        }

        private void MenuItemListeLeeren_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Soll die Liste geleert werden?", "Liste leeren", MessageBoxButton.YesNo,
                                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                ClearKämpferListe();
            }
        }

        private void MenuItemAktionEntfernen_Click(object sender, RoutedEventArgs e)
        {
            AktionEntfernen();
        }

        private void ContextMenuAktionen_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxAktionen.SelectedItem == null)
            {
                _menuItemAktionEntfernen.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                _menuItemAktionEntfernen.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void MenuItemAktionListeLeeren_Click(object sender, RoutedEventArgs e)
        {
            _kampf.RemoveAktionenAll();
            SortKämpfer();
            RefreshAktionen();
        }

        private void MenuItemHandleAktionAlt_Click(object sender, RoutedEventArgs e)
        {
            List<KampfAktion> aktionenRemove = new List<KampfAktion>();
            foreach (KampfAktion aktion in _kampf.AktionenListe)
            {
                if (aktion.Vorbei)
                {
                    aktion.AktionAusführen();
                    aktionenRemove.Add(aktion);
                }
            }
            foreach (KampfAktion aktion in aktionenRemove)
            {
                _kampf.AktionenListe.Remove(aktion);
                if (aktion.Kämpfer != null)
                    aktion.Kämpfer.AktionenLaufend.Remove(aktion);
            }
            SortKämpfer();
            RefreshAktionen();
        }

        private void _textBoxLebensenergieAktuell_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            SetLebensenergie();
        }

        private void _textBoxAusdauerAktuell_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            SetAusdauer();
        }

        private void _textBoxAstralenergieAktuell_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            SetAstralenergie();
        }

        private void _textBoxKarmaenergieAktuell_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            SetKarmaenergie();
        }

        private void _textBoxBehinderung_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            SortKämpfer();
            RefreshAktionen();
        }

        private void _textBoxWunden_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            RefreshKämpfer();
        }

        private void IntBox_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            SortKämpfer();
            RefreshAktionen();
        }

        private void ButtonSchadenspunkteAusdauer_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                _selectedKämpfer.SchadenspunkteAusdauer(_intBoxSchaden.Value, (bool)_checkBoxAusdauerSchadenLeAbziehen.IsChecked, Trefferzone.GetTrefferzoneEnum(_comboBoxTrefferzone.SelectedValue.ToString()), 
                    (bool)_radioButtonWundschwelleGesenkt.IsChecked, (bool)_radioButtonWundeKeine.IsChecked);
                SetLebensenergie();
                SetAusdauer();
            }
        }

        private void ButtonTrefferpunkteAusdauer_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKämpfer != null)
            {
                _selectedKämpfer.TrefferpunkteAusdauer(_intBoxSchaden.Value, (bool)_checkBoxAusdauerSchadenLeAbziehen.IsChecked, Trefferzone.GetTrefferzoneEnum(_comboBoxTrefferzone.SelectedValue.ToString()), 
                    (bool)_radioButtonWundschwelleGesenkt.IsChecked, (bool)_radioButtonWundeKeine.IsChecked);
                SetLebensenergie();
                SetAusdauer();
            }
        }

        private void ButtonNächsterKämpfer_Click(object sender, RoutedEventArgs e)
        {
            _kampf.NächsterKämpfer();
            ListBoxKämpfer_FocusItem();
        }

        private void ListBoxKämpfer_FocusItem()
        {
            _listBoxKämpfer.SelectedItem = _kampf.AktuellerKämpfer;
            _listBoxKämpfer.ScrollIntoView(_listBoxKämpfer.SelectedItem);
        }

        public void RefreshKämpfer()
        {
            SortKämpfer();
            RefreshAktionen();
            var tmpKämpfer = _listBoxKämpfer.SelectedItem;
            _listBoxKämpfer.SelectedItem = null;
            _listBoxKämpfer.SelectedItem = tmpKämpfer;
        }

        private void NächsterKämpferRollover_EventHandler(object sender, EventArgs e)
        {
            var res = MessageBoxResult.Yes;
            if (!Einstellungen.FrageNeueKampfrundeAbstellen)
                res = MessageBox.Show("Soll eine neue KR begonnen werden?\n(Diese Frage kann unter Einstellungen abgeschaltet werden.)",
                    "Ende der Kämpfer-Liste", MessageBoxButton.YesNoCancel);
            switch (res)
            {
                case MessageBoxResult.Cancel:
                    _kampf.VorherigerKämpfer();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    NeueKampfrunde();
                    break;
                default:
                    break;
            }
        }

        private void MenuItemKämpferFarbmarkierung_Click(object sender, RoutedEventArgs e)
        {
            _selectedKämpfer.Farbmarkierung = ((MenuItem)sender).Background;
        }

        private void MenuItemKämpferAktuell_Click(object sender, RoutedEventArgs e)
        {
            _kampf.NächsterKämpfer(_selectedKämpfer);
        }

        private KämpferWindow _kämpferGui;

        private void GegnerExpander_Expanded(object sender, RoutedEventArgs e)
        {
            _kämpferExpander.IsExpanded = false;
            ShowKämpferVerwaltung();
        }

        private void ShowKämpferVerwaltung()
        {
            if (_kämpferGui == null || _kämpferGui.IsVisible == false)
                _kämpferGui = new KämpferWindow(this);
            _kämpferGui.Owner = App.Current.MainWindow;
            _kämpferGui.Show();
        }

        private void ButtonArena_Click(object sender, RoutedEventArgs e)
        {
            ArenaWindow arenaWindow = new ArenaWindow(_cbArena.IsChecked == true ? _kampf : null);
            arenaWindow.Width = 1200;
            arenaWindow.Height = 800;
            arenaWindow.Show();
        }

        private void ButtonSpielerInfo_Click(object sender, RoutedEventArgs e)
        {
            ShowSpielerInfo();
        }

        private void ShowSpielerInfo()
        {
            KampfInfoView infoView = new KampfInfoView(_kampf);
            MainView.ShowSpielerInfo(infoView);
        }

    }

    public delegate void ProbeWürfelnEventHandler(string talentname);
}
