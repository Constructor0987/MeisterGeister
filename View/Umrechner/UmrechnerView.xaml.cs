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
using System.Windows.Navigation;
using System.Windows.Shapes;
// Eigene Usings
using MeisterGeister.View.General;
using MeisterGeister.Logic.Settings;
using MeisterGeister.Logic.Umrechner;

namespace MeisterGeister.View.Umrechner
{
    /// <summary>
    /// Interaktionslogik für UmrechnerView.xaml
    /// </summary>
    public partial class UmrechnerView : UserControl
    {
        public UmrechnerView()
        {
            InitializeComponent();

            // Länge
            _comboBoxLängeNach.ItemsSource = _längen;
            _comboBoxLängeNach.DisplayMemberPath = "Key";
            _comboBoxLängeNach.SelectedValuePath = "Key";
            _comboBoxLängeNach.SelectedIndex = 0;
            _comboBoxLängeVon.ItemsSource = _längen;
            _comboBoxLängeVon.DisplayMemberPath = "Key";
            _comboBoxLängeVon.SelectedValuePath = "Key";
            _comboBoxLängeVon.SelectedIndex = 0;

            // Masse
            _comboBoxMasseNach.ItemsSource = _massen;
            _comboBoxMasseNach.DisplayMemberPath = "Key";
            _comboBoxMasseNach.SelectedValuePath = "Key";
            _comboBoxMasseNach.SelectedIndex = 0;
            _comboBoxMasseVon.ItemsSource = _massen;
            _comboBoxMasseVon.DisplayMemberPath = "Key";
            _comboBoxMasseVon.SelectedValuePath = "Key";
            _comboBoxMasseVon.SelectedIndex = 0;

            // Volumen
            _comboBoxVolumenNach.ItemsSource = _volumen;
            _comboBoxVolumenNach.DisplayMemberPath = "Key";
            _comboBoxVolumenNach.SelectedValuePath = "Key";
            _comboBoxVolumenNach.SelectedIndex = 0;
            _comboBoxVolumenVon.ItemsSource = _volumen;
            _comboBoxVolumenVon.DisplayMemberPath = "Key";
            _comboBoxVolumenVon.SelectedValuePath = "Key";
            _comboBoxVolumenVon.SelectedIndex = 0;

            // Flächen
            _comboBoxFlächeNach.ItemsSource = _flächen;
            _comboBoxFlächeNach.DisplayMemberPath = "Key";
            _comboBoxFlächeNach.SelectedValuePath = "Key";
            _comboBoxFlächeNach.SelectedIndex = 0;
            _comboBoxFlächeVon.ItemsSource = _flächen;
            _comboBoxFlächeVon.DisplayMemberPath = "Key";
            _comboBoxFlächeVon.SelectedValuePath = "Key";
            _comboBoxFlächeVon.SelectedIndex = 0;

            // Währung
            _comboBoxWährungNach.ItemsSource = _währung;
            _comboBoxWährungNach.DisplayMemberPath = "Key";
            _comboBoxWährungNach.SelectedValuePath = "Key";
            _comboBoxWährungNach.SelectedIndex = 0;
            _comboBoxWährungVon.ItemsSource = _währung;
            _comboBoxWährungVon.DisplayMemberPath = "Key";
            _comboBoxWährungVon.SelectedValuePath = "Key";
            _comboBoxWährungVon.SelectedIndex = 0;

            // Zeit
            _comboBoxZeitNach.ItemsSource = _zeit;
            _comboBoxZeitNach.DisplayMemberPath = "Key";
            _comboBoxZeitNach.SelectedValuePath = "Key";
            _comboBoxZeitNach.SelectedIndex = 0;
            _comboBoxZeitVon.ItemsSource = _zeit;
            _comboBoxZeitVon.DisplayMemberPath = "Key";
            _comboBoxZeitVon.SelectedValuePath = "Key";
            _comboBoxZeitVon.SelectedIndex = 0;
        }

        private Längen _längen = new Längen();
        private Massen _massen = new Massen();
        private Volumen _volumen = new Volumen();
        private Flächen _flächen = new Flächen();
        private Währung _währung = new Währung();
        private Zeit _zeit = new Zeit();

        private void UmrechnenLänge()
        {
            if (IsInitialized && _comboBoxLängeVon.SelectedIndex != -1 && _comboBoxLängeNach.SelectedIndex != -1)
                _textBoxLängeErgebnis.Text = _längen.WertUmrechnen(_comboBoxLängeVon.SelectedValue.ToString(),
                    _comboBoxLängeNach.SelectedValue.ToString(), _doubleBoxLängeWert.Value).ToString().Replace("n. def.", "-");
        }

        private void UmrechnenMasse()
        {
            if (IsInitialized && _comboBoxMasseVon.SelectedIndex != -1 && _comboBoxMasseNach.SelectedIndex != -1)
                _textBoxMasseErgebnis.Text = _massen.WertUmrechnen(_comboBoxMasseVon.SelectedValue.ToString(),
                    _comboBoxMasseNach.SelectedValue.ToString(), _doubleBoxMasseWert.Value).ToString().Replace("n. def.", "-");
        }

        private void UmrechnenVolumen()
        {
            if (IsInitialized && _comboBoxVolumenVon.SelectedIndex != -1 && _comboBoxVolumenNach.SelectedIndex != -1)
                _textBoxVolumenErgebnis.Text = _volumen.WertUmrechnen(_comboBoxVolumenVon.SelectedValue.ToString(),
                    _comboBoxVolumenNach.SelectedValue.ToString(), _doubleBoxVolumenWert.Value).ToString().Replace("n. def.", "-");
        }

        private void UmrechnenFläche()
        {
            if (IsInitialized && _comboBoxFlächeVon.SelectedIndex != -1 && _comboBoxFlächeNach.SelectedIndex != -1)
                _textBoxFlächeErgebnis.Text = _flächen.WertUmrechnen(_comboBoxFlächeVon.SelectedValue.ToString(),
                    _comboBoxFlächeNach.SelectedValue.ToString(), _doubleBoxFlächeWert.Value).ToString().Replace("n. def.", "-");
        }

        private void UmrechnenWährung()
        {
            if (IsInitialized && _comboBoxWährungVon.SelectedIndex != -1 && _comboBoxWährungNach.SelectedIndex != -1)
                _textBoxWährungErgebnis.Text = _währung.WertUmrechnen(_comboBoxWährungVon.SelectedValue.ToString(),
                    _comboBoxWährungNach.SelectedValue.ToString(), _doubleBoxWährungWert.Value).ToString().Replace("n. def.", "-");
        }

        private void UmrechnenZeit()
        {
            if (IsInitialized && _comboBoxZeitVon.SelectedIndex != -1 && _comboBoxZeitNach.SelectedIndex != -1)
                _textBoxZeitErgebnis.Text = _zeit.WertUmrechnen(_comboBoxZeitVon.SelectedValue.ToString(),
                    _comboBoxZeitNach.SelectedValue.ToString(), _doubleBoxZeitWert.Value).ToString().Replace("n. def.", "-");
        }

        private void _doubleBoxLängeWert_NumValueChanged(DoubleBox sender)
        {
            UmrechnenLänge();
        }

        private void _comboBoxLängeVon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UmrechnenLänge();
        }

        private void _doubleBoxMasseWert_NumValueChanged(DoubleBox sender)
        {
            UmrechnenMasse();
        }

        private void _comboBoxMasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UmrechnenMasse();
        }

        private void _doubleBoxVolumenWert_NumValueChanged(DoubleBox sender)
        {
            UmrechnenVolumen();
        }

        private void _comboBoxVolumen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UmrechnenVolumen();
        }

        private void _doubleBoxFlächeWert_NumValueChanged(DoubleBox sender)
        {
            UmrechnenFläche();
        }

        private void _comboBoxFläche_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UmrechnenFläche();
        }

        private void _doubleBoxWährungWert_NumValueChanged(DoubleBox sender)
        {
            UmrechnenWährung();
        }

        private void _comboBoxWährung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UmrechnenWährung();
        }

        private void _doubleBoxZeitWert_NumValueChanged(DoubleBox sender)
        {
            UmrechnenZeit();
        }

        private void _comboBoxZeit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UmrechnenZeit();
        }

        private void ButtonLängeWechseln_Click(object sender, RoutedEventArgs e)
        {
            int indexVon = _comboBoxLängeVon.SelectedIndex;
            int indexNach = _comboBoxLängeNach.SelectedIndex;

            _comboBoxLängeVon.SelectedIndex = indexNach;
            _comboBoxLängeNach.SelectedIndex = indexVon;
        }

        private void ButtonMasseWechseln_Click(object sender, RoutedEventArgs e)
        {
            int indexVon = _comboBoxMasseVon.SelectedIndex;
            int indexNach = _comboBoxMasseNach.SelectedIndex;

            _comboBoxMasseVon.SelectedIndex = indexNach;
            _comboBoxMasseNach.SelectedIndex = indexVon;
        }

        private void ButtonVolumenWechseln_Click(object sender, RoutedEventArgs e)
        {
            int indexVon = _comboBoxVolumenVon.SelectedIndex;
            int indexNach = _comboBoxVolumenNach.SelectedIndex;

            _comboBoxVolumenVon.SelectedIndex = indexNach;
            _comboBoxVolumenNach.SelectedIndex = indexVon;
        }

        private void ButtonFlächeWechseln_Click(object sender, RoutedEventArgs e)
        {
            int indexVon = _comboBoxFlächeVon.SelectedIndex;
            int indexNach = _comboBoxFlächeNach.SelectedIndex;

            _comboBoxFlächeVon.SelectedIndex = indexNach;
            _comboBoxFlächeNach.SelectedIndex = indexVon;
        }

        private void ButtonWährungWechseln_Click(object sender, RoutedEventArgs e)
        {
            int indexVon = _comboBoxWährungVon.SelectedIndex;
            int indexNach = _comboBoxWährungNach.SelectedIndex;

            _comboBoxWährungVon.SelectedIndex = indexNach;
            _comboBoxWährungNach.SelectedIndex = indexVon;
        }

        private void ButtonZeitWechseln_Click(object sender, RoutedEventArgs e)
        {
            int indexVon = _comboBoxZeitVon.SelectedIndex;
            int indexNach = _comboBoxZeitNach.SelectedIndex;

            _comboBoxZeitVon.SelectedIndex = indexNach;
            _comboBoxZeitNach.SelectedIndex = indexVon;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Expanded Sections
            string sections = Einstellungen.UmrechnerExpandedSections;
            if (sections.Length >= 1)
                _expanderLänge.IsExpanded = (sections[0] == '1');
            if (sections.Length >= 2)
                _expanderMasse.IsExpanded = (sections[1] == '1');
            if (sections.Length >= 3)
                _expanderVolumen.IsExpanded = (sections[2] == '1');
            if (sections.Length >= 4)
                _expanderFläche.IsExpanded = (sections[3] == '1');
            if (sections.Length >= 5)
                _expanderWährung.IsExpanded = (sections[4] == '1');
            if (sections.Length >= 6)
                _expanderZeit.IsExpanded = (sections[5] == '1');
        }

        private void Expander_ExpandedCollapsed(object sender, RoutedEventArgs e)
        {
            // Expanded Sections speichern
            if (IsInitialized && IsLoaded)
            {
                string sections = string.Empty;
                sections += (_expanderLänge.IsExpanded ? "1" : "0");
                sections += (_expanderMasse.IsExpanded ? "1" : "0");
                sections += (_expanderVolumen.IsExpanded ? "1" : "0");
                sections += (_expanderFläche.IsExpanded ? "1" : "0");
                sections += (_expanderWährung.IsExpanded ? "1" : "0");
                sections += (_expanderZeit.IsExpanded ? "1" : "0");
                Einstellungen.UmrechnerExpandedSections = sections;
            }
        }
    }
}
