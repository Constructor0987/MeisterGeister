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
using System.Xml;
// Eigene Usings
using MeisterGeister.View.General;
using MeisterGeister.Logic.Settings;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.View.Kalender
{
    /// <summary>
    /// Interaktionslogik für KalenderView.xaml
    /// </summary>
    public partial class KalenderView : UserControl
    {
        public KalenderView()
        {
            InitializeComponent();

            Standort = Global.Standort;

            _comboBoxMonat.SelectionChanged -= _comboBoxMonat_SelectionChanged;
            _comboBoxTag.SelectionChanged -= _comboBoxTag_SelectionChanged;
            _comboBoxZeitrechnung.SelectionChanged -= _comboBoxZeitrechnung_SelectionChanged;

            foreach (Monat item in Enum.GetValues(typeof(Monat)))
                _comboBoxMonat.Items.Add(item);
            for (int i = 1; i <= 30; i++)
                _comboBoxTag.Items.Add(i);

            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "BF", Tag = Logic.Kalender.Kalender.BosparansFall });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Hal", Tag = Logic.Kalender.Kalender.Hal });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Reto", Tag = Logic.Kalender.Kalender.Reto });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Bardo/Cella", Tag = Logic.Kalender.Kalender.BardoCella });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Perval", Tag = Logic.Kalender.Kalender.Perval });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Jahr des Lichts", Tag = Logic.Kalender.Kalender.JahreDesLichts });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Horas", Tag = Logic.Kalender.Kalender.Horas });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Golgari", Tag = Logic.Kalender.Kalender.Golgari });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "JL", Tag = Logic.Kalender.Kalender.Thorwal });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "d. U.", Tag = Logic.Kalender.Kalender.AndergastNostria });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Kurkum", Tag = Logic.Kalender.Kalender.Kurkum });
            _comboBoxZeitrechnung.Items.Add(new ComboBoxItem() { Content = "Engasal", Tag = Logic.Kalender.Kalender.Engasal });
            _comboBoxZeitrechnung.SelectedIndex = 0;

            _comboBoxMonat.SelectionChanged += _comboBoxMonat_SelectionChanged;
            _comboBoxTag.SelectionChanged += _comboBoxTag_SelectionChanged;
            _comboBoxZeitrechnung.SelectionChanged += _comboBoxZeitrechnung_SelectionChanged;
            Global.StandortChanged += Global_StandortChanged;
        }

        void Global_StandortChanged(object sender, EventArgs e)
        {
            Berechnen();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Expanded Sections
            string sections = Einstellungen.KalenderExpandedSections;
            if (Einstellungen.KalenderExpandedSections.Length >= 1)
                _expanderZeitrechnungen.IsExpanded = (sections[0] == '1');
            if (Einstellungen.KalenderExpandedSections.Length >= 2)
                _expanderIrdisch.IsExpanded = (sections[1] == '1');
            if (Einstellungen.KalenderExpandedSections.Length >= 3)
                _expanderVeraltete.IsExpanded = (sections[2] == '1');
            if (Einstellungen.KalenderExpandedSections.Length >= 4)
                _expanderFeiertage.IsExpanded = (sections[3] == '1');
            if (Einstellungen.KalenderExpandedSections.Length >= 5)
                _expanderMadamal.IsExpanded = (sections[4] == '1');
            if (Einstellungen.KalenderExpandedSections.Length >= 6)
                _expanderSonnenuhr.IsExpanded = (sections[5] == '1');

            Datum d = Datum.Aktuell;
            _comboBoxTag.SelectedItem = d.Tag;
            _comboBoxMonat.SelectedItem = d.Monat;
            _intBoxJahr.Value = d.Jahr;
            foreach (var item in _comboBoxZeitrechnung.Items)
            {
                Logic.Kalender.Kalender von = (Logic.Kalender.Kalender)(((ComboBoxItem)item).Tag);
                if (d.KalenderAnzeige == von)
                {
                    _comboBoxZeitrechnung.SelectionChanged -= _comboBoxZeitrechnung_SelectionChanged;
                    _comboBoxZeitrechnung.SelectedItem = item;
                    _comboBoxZeitrechnung.SelectionChanged += _comboBoxZeitrechnung_SelectionChanged;
                    break;
                }
            }

            Berechnen();
        }

        private void _comboBoxMonat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
            {
                if ((Monat)_comboBoxMonat.SelectedItem == Monat.NamenloseTage)
                {
                    _comboBoxTag.Items.Clear();
                    for (int i = 1; i <= 5; i++)
                        _comboBoxTag.Items.Add(i);
                    _comboBoxTag.SelectedItem = 1;
                }
                else
                {
                    if (_comboBoxTag.Items.Count < 30)
                    {
                        _comboBoxTag.Items.Clear();
                        for (int i = 1; i <= 30; i++)
                            _comboBoxTag.Items.Add(i);
                        _comboBoxTag.SelectedItem = 1;
                    }
                }
                Berechnen();
            }
        }

        private void _comboBoxZeitrechnung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
                Berechnen();
        }

        private void _intBoxJahr_NumValueChanged(IntBox sender)
        {
            if (IsInitialized)
                Berechnen();
        }

        private void _comboBoxTag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
                Berechnen();
        }

        public event EventHandler DatumAktuellChanged;

        private void Berechnen()
        {
            if (_comboBoxMonat.SelectedItem != null && _comboBoxTag.SelectedItem != null)
            {
                Logic.Kalender.Kalender von = (Logic.Kalender.Kalender)(((ComboBoxItem)_comboBoxZeitrechnung.SelectedItem).Tag);

                Datum d = new Datum(_intBoxJahr.Value ?? 0, (Monat)(_comboBoxMonat.SelectedItem), (int)(_comboBoxTag.SelectedItem), von);
                Datum.Aktuell = new Datum(_intBoxJahr.Value ?? 0, (Monat)_comboBoxMonat.SelectedItem, (int)_comboBoxTag.SelectedItem, von);
                // Changed Event weitergeben
                if (DatumAktuellChanged != null)
                    DatumAktuellChanged(this, null);

                _labelWochentag.Content = d.WochentagString(Logic.Kalender.Kalender.Irdisch);
                _labelMonat.Content = d.MonatString(Logic.Kalender.Kalender.Irdisch);

                int mada = d.Mondphase();
                _txtBlockMadaZahl.Text = Datum.MondphaseString(mada);

                string imagesPath = string.Format("pack://application:,,/DSA MeisterGeister;component/Images/Madamal/mada{0}.jpg", mada);
                Uri uri = new Uri(imagesPath, UriKind.RelativeOrAbsolute);
                BitmapImage bitmap = new BitmapImage(uri);
                _imageMada.Source = bitmap;

                _labelBF.Content = d.ToString(Logic.Kalender.Kalender.BosparansFall);
                _labelHal.Content = d.ToString(Logic.Kalender.Kalender.Hal);
                _labelHoras.Content = d.ToString(Logic.Kalender.Kalender.Horas);
                _labelReto.Content = d.ToString(Logic.Kalender.Kalender.Reto);
                _labelBardoCella.Content = d.ToString(Logic.Kalender.Kalender.BardoCella);
                _labelPerval.Content = d.ToString(Logic.Kalender.Kalender.Perval);
                _labelGolgari.Content = d.ToString(Logic.Kalender.Kalender.Golgari);
                _labelAndergastNostria.Content = d.ToString(Logic.Kalender.Kalender.AndergastNostria);
                _labelKurkum.Content = d.ToString(Logic.Kalender.Kalender.Kurkum);
                _labelPriesterkaiser.Content = d.ToString(Logic.Kalender.Kalender.JahreDesLichts);
                _labelRastullah.Content = d.ToString(Logic.Kalender.Kalender.Rastullah);
                _labelThorwal.Content = d.ToString(Logic.Kalender.Kalender.Thorwal);
                _labelZwerge.Content = d.ToString(Logic.Kalender.Kalender.Zwerge);
                _labelImperium.Content = d.ToString(Logic.Kalender.Kalender.Imperium);
                _labelEngasal.Content = d.ToString(Logic.Kalender.Kalender.Engasal);

                _labelSonne.Content = d.SonnenAufUnterGang(Standort);
                if (Standort != null)
                    _labelStandort.Content = Standort.ToStringKoordinaten();

                _listBoxFeiertage.ItemsSource = d.Feiertage;
                Datum d2 = new Datum(d.AddTag(1).ToString());
                _listBoxFeiertageVorschau.ItemsSource = d2.Feiertage;
            }
        }

        private void _intBoxJahr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                _comboBoxTag.Focus();
        }

        private void _buttonTagVor_Click(object sender, RoutedEventArgs e)
        {
            _comboBoxMonat.SelectionChanged -= _comboBoxMonat_SelectionChanged;
            _comboBoxTag.SelectionChanged -= _comboBoxTag_SelectionChanged;
            _comboBoxZeitrechnung.SelectionChanged -= _comboBoxZeitrechnung_SelectionChanged;

            Datum d = Datum.Aktuell.AddTag(1);
            _comboBoxTag.SelectedItem = d.Tag;
            _comboBoxMonat.SelectedItem = d.Monat;
            _intBoxJahr.Value = d.Jahr;

            Berechnen();

            _comboBoxMonat.SelectionChanged += _comboBoxMonat_SelectionChanged;
            _comboBoxTag.SelectionChanged += _comboBoxTag_SelectionChanged;
            _comboBoxZeitrechnung.SelectionChanged += _comboBoxZeitrechnung_SelectionChanged;
        }

        private void _buttonTagZurück_Click(object sender, RoutedEventArgs e)
        {
            _comboBoxMonat.SelectionChanged -= _comboBoxMonat_SelectionChanged;
            _comboBoxTag.SelectionChanged -= _comboBoxTag_SelectionChanged;
            _comboBoxZeitrechnung.SelectionChanged -= _comboBoxZeitrechnung_SelectionChanged;

            Datum d = Datum.Aktuell.AddTag(-1);
            _comboBoxMonat.SelectedItem = d.Monat;
            _comboBoxTag.SelectedItem = d.Tag;
            _intBoxJahr.Value = d.Jahr;

            Berechnen();

            _comboBoxMonat.SelectionChanged += _comboBoxMonat_SelectionChanged;
            _comboBoxTag.SelectionChanged += _comboBoxTag_SelectionChanged;
            _comboBoxZeitrechnung.SelectionChanged += _comboBoxZeitrechnung_SelectionChanged;
        }

        private void ControlTag_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
                _buttonTagZurück_Click(null, null);
            else if (e.Delta > 0)
                _buttonTagVor_Click(null, null);
        }

        private void ImageDereGlobus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dereglobus.org/");
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void Expander_ExpandedCollapsed(object sender, RoutedEventArgs e)
        {
            // Expanded Sections speichern
            if (IsInitialized && IsLoaded)
            {
                string sections = string.Empty;
                sections += (_expanderZeitrechnungen.IsExpanded ? "1" : "0");
                sections += (_expanderIrdisch.IsExpanded ? "1" : "0");
                sections += (_expanderVeraltete.IsExpanded ? "1" : "0");
                sections += (_expanderFeiertage.IsExpanded ? "1" : "0");
                sections += (_expanderMadamal.IsExpanded ? "1" : "0");
                sections += (_expanderSonnenuhr.IsExpanded ? "1" : "0");
                Einstellungen.KalenderExpandedSections = sections;
            }
        }

        private DgSuche.Ortsmarke Standort = Global.Standort;

        private void ImageStandort_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var win = new MeisterGeister.View.Globus.SelectLandmarkeWindow();
            win.Kalender = this;
            win.Owner = Application.Current.MainWindow;
            win.ShowDialog();
            SetzeStandort(win.SelectedItem);
        }

        internal void SetzeStandort(DgSuche.Ortsmarke ort)
        {
            if (Standort != null)
            {
                Standort = ort;
                Global.Standort = ort;
            }
        }

        private void DereGlobusLinkControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _dereGlobusLinkControl.Tag = Standort;
        }
    }
}
