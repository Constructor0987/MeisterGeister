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
using System.Windows.Shapes;
using MeisterGeister.Daten;
// Eigene Usings
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Kampf.LogicAlt;

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für KämpferWindow.xaml
    /// </summary>
    public partial class KämpferWindow : Window
    {
        public KämpferWindow()
        {
            InitializeComponent();
        }

        public KämpferWindow(KampfView kampfCon)
        {
            InitializeComponent();

            _kampfControl = kampfCon;
        }

        private KampfView _kampfControl { get; set; }

        private void ButtonKampfHeldenAdd_Click(object sender, RoutedEventArgs e)
        {
            _kampfControl.HeldenEinfügen();
        }

        private void ButtonKampfClear_Click(object sender, RoutedEventArgs e)
        {
            _kampfControl.ClearKämpferListe();
        }

        private void ButtonAddGegner_Click(object sender, RoutedEventArgs e)
        {
            AddGegner();
            _kampfControl.SortKämpfer();
        }

        private void AddGegner()
        {
            Gegner g = new Gegner(_textBoxGegner.Text, _intBoxGegnerInitiative.Value ?? 0,
                                  _intBoxGegnerLebensenergie.Value ?? 0,
                                  _intBoxGegnerAusdauer.Value ?? 0);
            g.KO = _intBoxGegnerKonstitution.Value ?? 0;
            g.RüstungsschutzGesamt = _intBoxGegnerRSges.Value ?? 0;
            g.RüstungsschutzArmL = _intBoxGegnerRSAL.Value ?? 0;
            g.RüstungsschutzArmR = _intBoxGegnerRSAR.Value ?? 0;
            g.RüstungsschutzBauch = _intBoxGegnerRSBa.Value ?? 0;
            g.RüstungsschutzBeinL = _intBoxGegnerRSBL.Value ?? 0;
            g.RüstungsschutzBeinR = _intBoxGegnerRSBR.Value ?? 0;
            g.RüstungsschutzBrust = _intBoxGegnerRSBr.Value ?? 0;
            g.RüstungsschutzKopf = _intBoxGegnerRSKo.Value ?? 0;
            g.RüstungsschutzRücken = _intBoxGegnerRSRü.Value ?? 0;
            g.Besonderheiten = _textBoxGegnerBesonderheiten.Text;
            g.Sonderfertigkeiten = _textBoxGegnerSonderfertigkeiten.Text;
            g.Kampfwerte = _textBoxGegnerKampfwerte.Text;
            g.Parade = _intBoxGegnerParade.Value ?? 0;
            g.ProbeWürfelnEvent += _kampfControl.WesenProbeWürfeln_Event;

            // Auf vorhandene Einträge prüfen
            int count = _kampfControl.Kampf.KämpferListe.ContainsName(g);
            if (count > 0)
                g.Name += " " + ++count;

            _kampfControl.Kampf.AddKämpfer(g, (bool)_checkBoxVerbündeter.IsChecked);
        }

        private void ComboBoxGegnerMehrere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                int count = Convert.ToInt32(((ComboBoxItem)e.AddedItems[0]).Content);
                string gegner = _textBoxGegner.Text;
                for (int i = 1; i <= count; i++)
                    AddGegner();
                _kampfControl.SortKämpfer();
                ((ComboBox)sender).SelectedIndex = -1;
            }
        }

        private void ButtonSaveGegner_Click(object sender, RoutedEventArgs e)
        {
            // Prüfen ob es die Vorlage bereits gibt
            var rows = App.DatenDataSet.Bestiarium.Select("Name = '" + _textBoxGegner.Text.Trim().Replace("'", "''") + "'");
            if (rows.Length > 0)
            {
                string msg = "Zu diesem Gegner-Namen existiert bereits ein Eintrag.\nSoll dieser aktualiert werden?";
                msg += "\n\n[JA] = Überschreiben    [NEIN] = Umbenennen und erneut speichern";

                var msgBoxResult = MessageBox.Show(msg, "Gegner-Vorlage ändern", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.Yes);

                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    var rowGegner = (DatabaseDSADataSet.BestiariumRow)rows[0];
                    rowGegner.Name = _textBoxGegner.Text;
                    rowGegner.INI_Basis = _intBoxGegnerInitiative.Value ?? 0;
                    rowGegner.LE = _intBoxGegnerLebensenergie.Value ?? 0;
                    rowGegner.AU = _intBoxGegnerAusdauer.Value.ToString();
                    rowGegner.KO = _intBoxGegnerKonstitution.Value ?? 0;
                    rowGegner.RS = _intBoxGegnerRSges.Value ?? 0;
                    rowGegner.RSArmL = _intBoxGegnerRSAL.Value ?? 0;
                    rowGegner.RSArmR = _intBoxGegnerRSAR.Value ?? 0;
                    rowGegner.RSBauch = _intBoxGegnerRSBa.Value ?? 0;
                    rowGegner.RSBeinL = _intBoxGegnerRSBL.Value ?? 0;
                    rowGegner.RSBeinR = _intBoxGegnerRSBR.Value ?? 0;
                    rowGegner.RSBrust = _intBoxGegnerRSBr.Value ?? 0;
                    rowGegner.RSKopf = _intBoxGegnerRSKo.Value ?? 0;
                    rowGegner.RSRücken = _intBoxGegnerRSRü.Value ?? 0;
                    rowGegner.Besonderheiten = _textBoxGegnerBesonderheiten.Text;
                    rowGegner.Sonderfertigkeiten = _textBoxGegnerSonderfertigkeiten.Text;
                    rowGegner.Kampfwerte = _textBoxGegnerKampfwerte.Text;
                    rowGegner.PA = _intBoxGegnerParade.Value ?? 0;
                }
            }
            else
            {
                // Gegner neu einfügen
                DatabaseDSADataSet.BestiariumRow b = App.DatenDataSet.Bestiarium.NewBestiariumRow();
                b.Name = _textBoxGegner.Text;
                b.INI_Basis = _intBoxGegnerInitiative.Value ?? 0;
                b.LE = _intBoxGegnerLebensenergie.Value ?? 0;
                b.AU = _intBoxGegnerAusdauer.Value.ToString();
                b.KO = _intBoxGegnerKonstitution.Value ?? 0;
                b.RS = _intBoxGegnerRSges.Value ?? 0;
                b.RSArmL = _intBoxGegnerRSAL.Value ?? 0;
                b.RSArmR = _intBoxGegnerRSAR.Value ?? 0;
                b.RSBauch = _intBoxGegnerRSBa.Value ?? 0;
                b.RSBeinL = _intBoxGegnerRSBL.Value ?? 0;
                b.RSBeinR = _intBoxGegnerRSBR.Value ?? 0;
                b.RSBrust = _intBoxGegnerRSBr.Value ?? 0;
                b.RSKopf = _intBoxGegnerRSKo.Value ?? 0;
                b.RSRücken = _intBoxGegnerRSRü.Value ?? 0;
                b.Besonderheiten = _textBoxGegnerBesonderheiten.Text;
                b.Sonderfertigkeiten = _textBoxGegnerSonderfertigkeiten.Text;
                b.Kampfwerte = _textBoxGegnerKampfwerte.Text;
                b.PA = _intBoxGegnerParade.Value ?? 0;
                App.DatenDataSet.Bestiarium.AddBestiariumRow(b);

                MessageBox.Show("Gegner-Vorlage im Bestiarium gespeichert.", "Gegner-Vorlage gespeichert", MessageBoxButton.OK);
            }
            App.SaveGegner(); // Genger speichern
        }

        private void ButtonDeleteGegner_Click(object sender, RoutedEventArgs e)
        {
            // Vorlage löschen
            var rows = App.DatenDataSet.Bestiarium.Select("Name = '" + _textBoxGegner.Text.Trim().Replace("'", "''") + "'");
            if (rows.Length > 0)
            {
                var msgBoxResult = MessageBox.Show(string.Format("Soll die Vorlage '{0}' wirklich gelöscht werden?", _textBoxGegner.Text.Trim()),
                    "Gegner-Vorlage löschen", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.Yes);

                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    foreach (var item in rows)
                        item.Delete();
                    App.SaveGegner(); // Genger speichern
                    _listBoxGegner.SelectedIndex = -1;
                    MessageBox.Show("Gegner-Vorlage gelöscht!", "Gegner-Vorlage löschen", MessageBoxButton.OK);
                }
            }
            else
                MessageBox.Show("Unter diesem Namen existiert keine Gegner-Vorlage, \ndie gelöscht werden könnte.", "Gegner-Vorlage löschen", MessageBoxButton.OK);
        }

        private void ListBoxGegner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
            {
                if (e.AddedItems.Count > 0)
                {
                    var wesen = (DatabaseDSADataSet.BestiariumRow)((System.Data.DataRowView)(((object[])(e.AddedItems))[0])).Row;
                    _textBoxGegner.Text = wesen.Name;
                    _intBoxGegnerInitiative.Value = wesen.IsINI_BasisNull() ? 0 : wesen.INI_Basis;
                    _intBoxGegnerLebensenergie.Value = wesen.IsLENull() ? 0 : wesen.LE;
                    int au = 0;
                    if (!wesen.IsAUNull())
                        Int32.TryParse(wesen.AU, out au);
                    _intBoxGegnerAusdauer.Value = au;
                    _intBoxGegnerRSges.Value = wesen.IsRSNull() ? 0 : wesen.RS;
                    _intBoxGegnerRSAL.Value = wesen.IsRSArmLNull() ? 0 : wesen.RSArmL;
                    _intBoxGegnerRSAR.Value = wesen.IsRSArmRNull() ? 0 : wesen.RSArmR;
                    _intBoxGegnerRSBa.Value = wesen.IsRSBauchNull() ? 0 : wesen.RSBauch;
                    _intBoxGegnerRSBL.Value = wesen.IsRSBeinLNull() ? 0 : wesen.RSBeinL;
                    _intBoxGegnerRSBR.Value = wesen.IsRSBeinRNull() ? 0 : wesen.RSBeinR;
                    _intBoxGegnerRSBr.Value = wesen.IsRSBrustNull() ? 0 : wesen.RSBrust;
                    _intBoxGegnerRSKo.Value = wesen.IsRSKopfNull() ? 0 : wesen.RSKopf;
                    _intBoxGegnerRSRü.Value = wesen.IsRSRückenNull() ? 0 : wesen.RSRücken;
                    _intBoxGegnerKonstitution.Value = wesen.IsKONull() ? 0 : wesen.KO;
                    _intBoxGegnerParade.Value = wesen.IsPANull() ? 0 : wesen.PA;
                    _textBoxGegnerKampfwerte.Text = wesen.IsKampfwerteNull() ? string.Empty : wesen.Kampfwerte;
                    _textBoxGegnerBesonderheiten.Text = wesen.IsBesonderheitenNull() ? string.Empty : wesen.Besonderheiten;
                    _textBoxGegnerSonderfertigkeiten.Text = wesen.IsSonderfertigkeitenNull() ? string.Empty : wesen.Sonderfertigkeiten;
                }
            }
        }

        private bool _rsChanging = false;
        private void IntBoxGegnerRS_NumValueChanged(IntBox sender)
        {
            if (IsInitialized && !_rsChanging)
            {
                _rsChanging = true;
                if (sender == _intBoxGegnerRSges)
                {
                    _intBoxGegnerRSAL.Value = _intBoxGegnerRSges.Value;
                    _intBoxGegnerRSAR.Value = _intBoxGegnerRSges.Value;
                    _intBoxGegnerRSBa.Value = _intBoxGegnerRSges.Value;
                    _intBoxGegnerRSBL.Value = _intBoxGegnerRSges.Value;
                    _intBoxGegnerRSBR.Value = _intBoxGegnerRSges.Value;
                    _intBoxGegnerRSBr.Value = _intBoxGegnerRSges.Value;
                    _intBoxGegnerRSKo.Value = _intBoxGegnerRSges.Value;
                    _intBoxGegnerRSRü.Value = _intBoxGegnerRSges.Value;
                }
                else
                {
                    double gRS = 0.0;

                    gRS = (_intBoxGegnerRSKo.Value * 2
                        + _intBoxGegnerRSBr.Value * 4
                        + _intBoxGegnerRSRü.Value * 4
                        + _intBoxGegnerRSBa.Value * 4
                        + _intBoxGegnerRSAL.Value + _intBoxGegnerRSAR.Value
                        + _intBoxGegnerRSBL.Value * 2 + _intBoxGegnerRSBR.Value ?? 0 * 2) / 20.0;

                    _intBoxGegnerRSges.Value = (int)Math.Round(gRS, 0, MidpointRounding.AwayFromZero);
                }
                _rsChanging = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // keinen Listen-Eintrag auswählen -> Standard-Gegner wird angezeigt
            _listBoxGegner.SelectedIndex = -1;
        }
    }
}
