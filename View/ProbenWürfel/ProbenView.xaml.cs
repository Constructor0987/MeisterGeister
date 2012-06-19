using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using MeisterGeister.Daten;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.View.General;
using MeisterGeister.View;
using MeisterGeister.View.Windows;

namespace MeisterGeister.View.ProbenWürfel
{
    /// <summary>
    /// Interaktionslogik für ProbenView.xaml
    /// </summary>
    public partial class ProbenView : UserControl
    {
        public ProbenView()
        {
            InitializeComponent();

            List<string> probenFilter = new List<string>();
            probenFilter.AddRange(new string[] { "Alle", "Häufig verwendet", "Eigenschaften", "Talente", 
                "Kampf", "Körper", "Gesellschaft", "Natur", "Wissen", "Handwerk", "Sprachen/Schriften", "Gabe", 
                "Ritualkenntnis", "Liturgiekenntnis", "Meta", "Basis", "Spezial", "Zauber"});
            _comboBoxProbenFilter.SelectionChanged -= ComboBoxProbenFilter_SelectionChanged;
            _comboBoxProbenFilter.ItemsSource = probenFilter;
            _comboBoxProbenFilter.SelectedIndex = 1;
            _comboBoxProbenFilter.SelectionChanged += ComboBoxProbenFilter_SelectionChanged;

            _checkBoxSoundAbspielen.Checked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.IsChecked = Würfel.SoundAbspielen;
            _checkBoxSoundAbspielen.Checked += CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked += CheckBoxSoundAbspielen_Changed;

            Würfel.SoundAbspielenChanged += WürfelSoundAbspielen_Changed;

            if (App.DatenDataSet != null && App.DatenDataSet.Einstellungen != null)
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("ProbenFavoriten");
                if (row != null && !row.IsWertTextNull())
                {
                    string[] talente = row.WertText.Split('#');
                    foreach (string t in talente)
                    {
                        if (t != string.Empty)
                            AddProbenFavorit(t);
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.DatenDataSet != null)
            {
                SetComboProben();
            }
        }

        private void WürfelSoundAbspielen_Changed(object sender, EventArgs e)
        {
            _checkBoxSoundAbspielen.Checked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.IsChecked = Würfel.SoundAbspielen;
            _checkBoxSoundAbspielen.Checked += CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked += CheckBoxSoundAbspielen_Changed;
        }

        private void SetComboProben()
        {
            if (!IsInitialized)
                return;

            List<string> proben = new List<string>();
            bool talenteAlle = false;
            bool talenteAuswahl = false;
            bool talenteBasis = false;
            bool talenteSpezial = false;
            bool eigenschaften = false;
            bool zauber = false;

            List<string> aktivListe = null;
            bool nurAktive = Convert.ToBoolean(_checkBoxNurAktiveTalente.IsChecked);

            foreach (var item in _wrapPanelProbenFavoriten.Children)
            {
                if (item is Button)
                    ((Button)item).Opacity = 1.0;
            }

            // Nur Aktive Talent-Liste setzen
            if (nurAktive)
            {
                // Talente
                var aktivTalente = (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                    .Select("Parent(Held_FK).AktiveHeldengruppe = 1");
                aktivListe = new List<string>();
                foreach (var row in aktivTalente)
                {
                    if (!aktivListe.Contains(row["Talentname"].ToString()))
                        aktivListe.Add(row["Talentname"].ToString());
                }
                // Meta-Talente
                foreach (DatabaseDSADataSet.TalentRow talentRow in App.DatenDataSet.Talent.Select("Parent(Gruppe).Kurzname = 'Meta'"))
                {
                    foreach (var held in Held.AktiveHelden())
                    {
                        DreierProbenWert t = held.MetaTalentTaW(talentRow.Talentname);
                        if (t.Aktiviert)
                        {
                            if (!aktivListe.Contains(talentRow.Talentname))
                                aktivListe.Add(talentRow.Talentname);
                        }
                    }
                }
                // Zauber
                var aktivZauber = (DatabaseDSADataSet.Held_ZauberRow[])App.DatenDataSet.Held_Zauber
                    .Select("Parent(Held_Zauber_HeldFK).AktiveHeldengruppe = 1");
                foreach (var row in aktivZauber)
                {
                    if (!aktivListe.Contains(row["Zaubername"].ToString()))
                        aktivListe.Add(row["Zaubername"].ToString());
                }
                // Favoriten
                foreach (var item in _wrapPanelProbenFavoriten.Children)
                {
                    if (item is Button)
                    {

                        if (!aktivListe.Contains(((Button)item).Content.ToString())
                            && !Eigenschaften.ContainsKürzel(((Button)item).Content.ToString()))
                            ((Button)item).Opacity = 0.3;
                    }
                }
            }

            string filter = _comboBoxProbenFilter.SelectedItem.ToString();
            switch (filter)
            {
                case "Alle":
                    talenteAlle = true;
                    eigenschaften = true;
                    zauber = true;
                    break;
                case "Häufig verwendet":
                    talenteAlle = false;
                    List<string> häufigList = new List<string> { "Fährtensuchen", "Gassenwissen", "Gefahreninstinkt", "Menschenkenntnis", "Orientierung", 
                        "Schleichen", "Sich Verstecken", "Sinnenschärfe", "Überreden", "Wildnisleben" };
                    if (nurAktive)
                    {
                        List<string> temp = new List<string>();
                        foreach (string t in häufigList)
                        {
                            if (aktivListe.Contains(t))
                                temp.Add(t);
                        }
                        häufigList = temp;
                    }
                    proben.AddRange(häufigList);
                    break;
                case "Talente":
                    talenteAlle = true;
                    break;
                case "Basis":
                    talenteBasis = true;
                    break;
                case "Spezial":
                    talenteSpezial = true;
                    break;
                case "Eigenschaften":
                    eigenschaften = true;
                    break;
                case "Zauber":
                    zauber = true;
                    break;
                default:
                    talenteAuswahl = true;
                    break;
            }

            if (talenteAlle)
            {
                // Talente hinzufügen
                foreach (DatabaseDSADataSet.TalentRow talentRow in App.DatenDataSet.Talent)
                {
                    if (nurAktive)
                    {
                        if (!aktivListe.Contains(talentRow.Talentname))
                            continue;
                    }
                    proben.Add(talentRow.Talentname);
                }
            }
            if (talenteAuswahl)
            {
                // Talente hinzufügen
                foreach (DatabaseDSADataSet.TalentRow talentRow in App.DatenDataSet.Talent.Select("Parent(Gruppe).Kurzname = '" + filter + "'"))
                {
                    if (nurAktive)
                    {
                        if (!aktivListe.Contains(talentRow.Talentname))
                            continue;
                    }
                    proben.Add(talentRow.Talentname);
                }
            }
            if (talenteBasis)
            {
                // Talente hinzufügen
                foreach (DatabaseDSADataSet.TalentRow talentRow in App.DatenDataSet.Talent.Select("Talenttyp LIKE 'Basis%'"))
                {
                    if (nurAktive)
                    {
                        if (!aktivListe.Contains(talentRow.Talentname))
                            continue;
                    }
                    proben.Add(talentRow.Talentname);
                }
            }
            if (talenteSpezial)
            {
                // Talente hinzufügen
                foreach (DatabaseDSADataSet.TalentRow talentRow in App.DatenDataSet.Talent.Select("Talenttyp LIKE '%Spezial'"))
                {
                    if (nurAktive)
                    {
                        if (!aktivListe.Contains(talentRow.Talentname))
                            continue;
                    }
                    proben.Add(talentRow.Talentname);
                }
            }
            if (zauber)
            {
                // Zauber hinzufügen
                foreach (DatabaseDSADataSet.ZauberRow zauberRow in App.DatenDataSet.Zauber)
                {
                    if (nurAktive)
                    {
                        if (!aktivListe.Contains(zauberRow.Name))
                            continue;
                    }
                    proben.Add(zauberRow.Name);
                }
            }
            if (eigenschaften)
            {
                // Eigenschaften hinzufügen
                proben.AddRange(Eigenschaften.Kürzel);
            }

            if (!eigenschaften)
                proben.Sort();
            _comboBoxProbe.ItemsSource = proben;
            if (!proben.Contains(_comboBoxProbe.Text))
            {
                _comboBoxProbe.SelectedIndex = 0;
            }
        }

        private IProbe SelectedProbe()
        {
            IProbe probe = null;
            if (_comboBoxProbe.SelectedItem != null)
            {
                string n = _comboBoxProbe.SelectedItem.ToString();
                if (Eigenschaften.ContainsKürzel(n))
                {
                    probe = Eigenschaften.Get(n);
                }
                else if (App.DatenDataSet.Talent.FindByTalentname(n) != null)
                {
                    probe = new Talent(n);
                }
                else
                {
                    probe = new Zauber(n);
                }
            }
            return probe;
        }

        private string ProbenModText()
        {
            string txt = string.Empty;
            if (_intBoxProbeMod.Value > 0)
                txt = " +" + _intBoxProbeMod.Value;
            else if (_intBoxProbeMod.Value < 0)
                txt = " " + _intBoxProbeMod.Value;
            return txt;
        }

        private void ButtonProbenWürfeln_Click(object sender, RoutedEventArgs e)
        {
            ProbeWürfeln();
            if (Würfel.SoundAbspielen)
            {
                try
                {
                    Würfel.PlaySound();
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("Audio Fehler", ex.Message);
                    errWin.ShowDialog();
                }
            }
        }

        private void ProbeWürfeln()
        {
            IProbe probe = SelectedProbe();
            if (probe != null)
            {
                _textBlockTalentProbe.Text = probe.ProbenText + ProbenModText();

                _probenErgebnisse.Clear();

                foreach (DatabaseDSADataSet.HeldRow heldRow in App.DatenDataSet.Held)
                {
                    if (heldRow.AktiveHeldengruppe)
                        AddHeldProbenItem(new Held(heldRow), probe, heldRow.Proben);
                }
                _probenErgebnisse.Sort(new ProbenErgebnisComparer()); // Sortieren

                BerechneProbeÜbrigSumme();

                // Liste binden
                _listBoxProbenErgebnis.DataContext = _probenErgebnisse;
                _listBoxProbenErgebnis.Items.Refresh();
            }
        }

        private void ComboBoxTalentProbe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetProbe();
        }

        private void SetProbe()
        {
            _buttonZooBot.Visibility = System.Windows.Visibility.Collapsed;

            IProbe probe = SelectedProbe();
            if (probe != null)
            {
                // Allgemeine Informationen setzen
                _textBlockTalentProbe.Text = probe.ProbenText + ProbenModText();
                _textBlockTalentInfo.Inlines.Clear();
                _textBlockTalentInfo.Inlines.Add(new Bold(new Run("Quelle: ")));
                _textBlockTalentInfo.Inlines.Add(new Run(probe.Literatur));
                if (probe is Talent)
                {
                    _textBlockTalentInfo.Inlines.Add(new Bold(new Run("  eBE: ")));
                    _textBlockTalentInfo.Inlines.Add(new Run(((Talent)probe).EffektiveBehinderung));
                    if (((Talent)probe).Voraussetzungen != string.Empty)
                    {
                        _textBlockTalentInfo.Inlines.Add(new Bold(new Run("  Voraussetzungen: ")));
                        _textBlockTalentInfo.Inlines.Add(new Run(((Talent)probe).Voraussetzungen));
                    }
                    if (((Talent)probe).Spezialisierungen != string.Empty)
                    {
                        _textBlockTalentInfo.Inlines.Add(new Bold(new Run("  Spezialisierungen: ")));
                        _textBlockTalentInfo.Inlines.Add(new Run(((Talent)probe).Spezialisierungen));
                    }
                    if (((Talent)probe).TalentDataRow.TalentgruppeID == 8 || ((Talent)probe).Name == "Fischen/Angeln"
                         || ((Talent)probe).Name == "Fallenstellen")
                        _buttonZooBot.Visibility = System.Windows.Visibility.Visible;
                }
                else if (probe is Zauber)
                {
                    _textBlockTalentInfo.Inlines.Add(new Bold(new Run("  Komplexität: ")));
                    _textBlockTalentInfo.Inlines.Add(new Run(((Zauber)probe).ZauberDataRow.Komplex));
                    _textBlockTalentInfo.Inlines.Add(new Bold(new Run("  Merkmale: ")));
                    _textBlockTalentInfo.Inlines.Add(new Run(((Zauber)probe).ZauberDataRow.Merkmale));
                    _textBlockTalentInfo.Inlines.Add(new Bold(new Run("  Repräsentationen: ")));
                    _textBlockTalentInfo.Inlines.Add(new Run(((Zauber)probe).ZauberDataRow.Repräsentationen));
                }

                _probenErgebnisse.Clear();
                foreach (DatabaseDSADataSet.HeldRow heldRow in App.DatenDataSet.Held)
                {
                    if (heldRow.AktiveHeldengruppe)
                        AddHeldProbenItem(new Held(heldRow), probe);
                }
                _probenErgebnisse.Sort(new ProbenErgebnisComparer()); // Sortieren

                _textBlockTalentProbeSumme.Text = "Unabhängige Zusammenarbeit: -\nMit fähigstem Held als Vorarbeiter: -";

                // Liste binden
                _listBoxProbenErgebnis.DataContext = _probenErgebnisse;
                _listBoxProbenErgebnis.Items.Refresh();
            }
        }

        private void BerechneProbeÜbrigSumme()
        {
            int tapSum = 0, vorSum = 0, i = 1;
            string art = "Übrig";
            foreach (ProbenErgebnis er in _probenErgebnisse)
            {
                if (er.Held.HeldDataRow.Proben) // nur summieren, wenn Held geprobt werden soll
                {
                    if (er.Übrig >= 0) //nur positive Ergebnisse addieren
                    {
                        tapSum += er.Übrig;
                        vorSum += Convert.ToInt32(Math.Round((double)er.Übrig / i, 0));
                        i++;
                    }
                    if (er is DreierProbenErgebnis)
                        art = ((DreierProbenErgebnis)er).PunkteArt + "*";
                }
            }
            _textBlockTalentProbeSumme.Text = string.Format("Unabhängige Zusammenarbeit: {0} {1}\nMit fähigstem Held als Vorarbeiter: {2} {1}", tapSum, art, vorSum);
        }

        public void AddHeldProbenItem(Held held, IProbe probe, bool probeDurchführen = false)
        {
            if (probe is Talent || probe is Zauber)
            {

                List<DreierProbenWert> wList = new List<DreierProbenWert>();
                if (probe is Talent)
                {
                    wList.Add(held.Talentwert((Talent)probe));
                }
                else
                {
                    wList = held.Zauberwert((Zauber)probe);
                }
                foreach (var w in wList)
                {
                    var er = new DreierProbenErgebnis { Held = held };
                    if (w.Aktiviert)
                    {
                        // Behinderung
                        int be = BerechneEffBehinderung(er);
                        int mod = _intBoxProbeMod.Value;
                        mod += Math.Max(0, be);

                        // Eigenschaften
                        int e1 = 0, e2 = 0, e3 = 0;
                        if (((IDreierProbe)probe).Eigenschaft1 != "?")
                            e1 = held.Eigenschaft(((IDreierProbe)probe).Eigenschaft1);
                        if (((IDreierProbe)probe).Eigenschaft2 != "?")
                            e2 = held.Eigenschaft(((IDreierProbe)probe).Eigenschaft2);
                        if (((IDreierProbe)probe).Eigenschaft3 != "?")
                            e3 = held.Eigenschaft(((IDreierProbe)probe).Eigenschaft3);

                        int taw = w.Wert;
                        er.TextHinweis = w.TextHinweis;
                        if (probe is Talent)
                            er.PunkteArt = "TaP";
                        else if (probe is Zauber)
                            er.PunkteArt = "ZfP";

                        if (!probeDurchführen)
                        {
                            er.Behinderung = be;
                            er.Wert = taw;
                            er.Mod = mod;
                            er.E1Ergebnis = new EigenschaftProbenErgebnis(er) { Wert = e1 };
                            er.E2Ergebnis = new EigenschaftProbenErgebnis(er) { Wert = e2 };
                            er.E3Ergebnis = new EigenschaftProbenErgebnis(er) { Wert = e3 };
                        }
                        else
                        { // Probe würfeln
                            if (probe is Talent)
                                er = Talent.Probe(e1, e2, e3, taw, probe.Name, held, mod);
                            else
                                er = Zauber.Probe(e1, e2, e3, taw, probe.Name, held, mod);
                            er.Behinderung = be;
                            er.TextHinweis = w.TextHinweis;
                            if (probe is Talent)
                                er.PunkteArt = "TaP";
                            else if (probe is Zauber)
                                er.PunkteArt = "ZfP";
                        }
                        _probenErgebnisse.Add(er);
                    }
                }
            }
            else if (probe is Eigenschaft)
            {
                var ergebnis = new EigenschaftProbenErgebnis { Held = held };
                int e = held.Eigenschaft(((Eigenschaft)probe).Name);
                int mod = _intBoxProbeMod.Value;

                if (!probeDurchführen)
                {
                    ergebnis.Wert = e;
                    ergebnis.Mod = mod;
                }
                else
                { // Eigenschafts-Probe würfeln
                    ergebnis = Eigenschaft.Probe(e, held, _intBoxProbeMod.Value);
                    ergebnis.Held = held;
                }

                _probenErgebnisse.Add(ergebnis);
            }
        }

        private readonly List<ProbenErgebnis> _probenErgebnisse = new List<ProbenErgebnis>();

        private void ImageProbeWiki_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedProbe() != null)
                System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedProbe().WikiLink);
        }

        public void ProbeWürfeln(string probenname)
        {
            _comboBoxProbenFilter.SelectedIndex = 0;
            if (!_comboBoxProbe.Items.Contains(probenname))
                _checkBoxNurAktiveTalente.IsChecked = false;
            _comboBoxProbe.Text = probenname;
            ProbeWürfeln();
        }

        private void CheckBoxBehinderung_Checked(object sender, RoutedEventArgs e)
        {
            SetProbe();
        }

        private void CheckBoxBehinderung_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProbe();
        }

        private void WürfelControlEigenschaft_WürfelGeändert(uint ergebnis, EigenschaftProbenErgebnis probenErgebnis)
        {
            probenErgebnis.Gewürfelt = ergebnis;
            ProbenErgebnisAktualisieren(probenErgebnis);
            _listBoxProbenErgebnis.Items.Refresh();
            BerechneProbeÜbrigSumme();
        }

        private void ProbenErgebnisAktualisieren(EigenschaftProbenErgebnis probenErgebnis)
        {
            IProbe probe = SelectedProbe();
            if (probe is Talent || probe is Zauber)
            {
                // Behinderung
                int be = BerechneEffBehinderung(probenErgebnis);

                int mod = _intBoxProbeMod.Value;
                mod += Math.Max(0, be);

                var tProbe = probenErgebnis.TalentProbenErgebnis;
                if (probe is Talent)
                    Talent.Probe(ref tProbe, probe.Name, probenErgebnis.Held, mod,
                        probenErgebnis.TalentProbenErgebnis.E1Ergebnis.Gewürfelt, probenErgebnis.TalentProbenErgebnis.E2Ergebnis.Gewürfelt,
                        probenErgebnis.TalentProbenErgebnis.E3Ergebnis.Gewürfelt);
                if (probe is Zauber)
                    Zauber.Probe(ref tProbe, probe.Name, probenErgebnis.Held, mod,
                        probenErgebnis.TalentProbenErgebnis.E1Ergebnis.Gewürfelt, probenErgebnis.TalentProbenErgebnis.E2Ergebnis.Gewürfelt,
                        probenErgebnis.TalentProbenErgebnis.E3Ergebnis.Gewürfelt);

            }
            else if (probe is Eigenschaft)
            {
                var eProbe = probenErgebnis;
                Eigenschaft.Probe(ref eProbe, _intBoxProbeMod.Value, eProbe.Gewürfelt);
            }
        }

        private int BerechneEffBehinderung(ProbenErgebnis probenErgebnis)
        {
            IProbe probe = SelectedProbe();
            int be = 0;
            if (_checkBoxBehinderung.IsChecked == true)
            {
                string eBe = string.Empty;
                if (probe is Talent)
                    eBe = ((Talent)probe).EffektiveBehinderung;
                if (eBe == "BE")
                    be = probenErgebnis.Held.Behinderung;
                else if (eBe.StartsWith("BEx"))
                    be = probenErgebnis.Held.Behinderung * Convert.ToInt32(eBe.Substring(3));
                else if (eBe.StartsWith("BE-"))
                    be = probenErgebnis.Held.Behinderung - Convert.ToInt32(eBe.Substring(3));
                else if (eBe.StartsWith("BE+"))
                    be = probenErgebnis.Held.Behinderung + Convert.ToInt32(eBe.Substring(3));
                else
                    be = 0;
            }
            return be;
        }

        private void ButtonSpielerInfo_Click(object sender, RoutedEventArgs e)
        {
            // Informationen als Bild anzeigen
            Image imgCon = MainView.GetControlImage(_scrollViewerProbenErgebnis);

            MainView.ShowSpielerInfo(imgCon);
        }

        private void IntBoxProbeMod_NumValueChanged(IntBox sender)
        {
            SetModLabels(sender.Value);
            SetProbe();
        }

        private void SetModLabels(int p)
        {
            // Auf Default setzen
            _labelModMinus7.BorderThickness = new Thickness(0.0);
            _labelModMinus3.BorderThickness = new Thickness(0.0);
            _labelMod0.BorderThickness = new Thickness(0.0);
            _labelModPlus3.BorderThickness = new Thickness(0.0);
            _labelModPlus7.BorderThickness = new Thickness(0.0);
            _labelModPlus12.BorderThickness = new Thickness(0.0);
            _labelModPlus18.BorderThickness = new Thickness(0.0);
            _labelModPlus25.BorderThickness = new Thickness(0.0);

            // Passenden Modifikator hervorheben
            if (p <= -7)
                _labelModMinus7.BorderThickness = new Thickness(1.0);
            else if (p <= -3)
                _labelModMinus3.BorderThickness = new Thickness(1.0);
            else if (p <= 0)
                _labelMod0.BorderThickness = new Thickness(1.0);
            else if (p <= 3)
                _labelModPlus3.BorderThickness = new Thickness(1.0);
            else if (p <= 7)
                _labelModPlus7.BorderThickness = new Thickness(1.0);
            else if (p <= 12)
                _labelModPlus12.BorderThickness = new Thickness(1.0);
            else if (p <= 18)
                _labelModPlus18.BorderThickness = new Thickness(1.0);
            else if (p <= 25)
                _labelModPlus25.BorderThickness = new Thickness(1.0);
        }

        private void ComboBoxProbenFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComboProben();
        }

        private void Mod_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _intBoxProbeMod.Value = Convert.ToInt32(((Label)sender).Tag);
        }

        private void CheckBoxSoundAbspielen_Changed(object sender, RoutedEventArgs e)
        {
            if (IsInitialized)
            {
                Würfel.SoundAbspielenChanged -= WürfelSoundAbspielen_Changed;
                Würfel.SoundAbspielen = (bool)_checkBoxSoundAbspielen.IsChecked;
                Würfel.SoundAbspielenChanged += WürfelSoundAbspielen_Changed;
            }
        }

        private void CheckBoxProben_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsInitialized)
                BerechneProbeÜbrigSumme();
        }

        private void CheckBoxNurAktiveTalente_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsInitialized && IsLoaded)
                SetComboProben();
        }

        private void ButtonAddProbenFavorit_Click(object sender, RoutedEventArgs e)
        {
            _buttonAddProbenFavorit.ContextMenu = new ContextMenu();
            MenuItem m = null;
            m = new MenuItem();
            m.Header = "Aktuelle Probe";
            m.Click += new RoutedEventHandler(MenuItemAddProbenFavorit_Click);
            _buttonAddProbenFavorit.ContextMenu.Items.Add(m);
            _buttonAddProbenFavorit.ContextMenu.Items.Add(new Separator());

            foreach (DatabaseDSADataSet.TalentRow talentRow in App.DatenDataSet.Talent)
            {
                m = new MenuItem();
                m.Header = talentRow.Talentname;
                m.Click += new RoutedEventHandler(MenuItemAddProbenFavorit_Click);
                _buttonAddProbenFavorit.ContextMenu.Items.Add(m);
            }

            _buttonAddProbenFavorit.ContextMenu.IsOpen = true;
        }

        void MenuItemAddProbenFavorit_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is MenuItem)
            {
                string talent = ((MenuItem)sender).Header.ToString();
                if (talent == "Aktuelle Probe")
                {
                    IProbe p = SelectedProbe();
                    if (p != null)
                        talent = p.Name;
                }
                AddProbenFavorit(talent);

                // Speichern
                if (App.DatenDataSet != null && App.DatenDataSet.Einstellungen != null)
                {
                    var row = App.DatenDataSet.Einstellungen.FindByName("ProbenFavoriten");
                    if (row != null)
                    {
                        if (row.IsWertTextNull())
                            row.WertText = string.Empty;
                        if (row.WertText != string.Empty)
                            row.WertText += "#";
                        row.WertText += talent;
                    }
                }
            }
        }

        private void AddProbenFavorit(string talent)
        {
            if (talent != "Aktuelle Probe")
            {
                Button b = new Button();
                b.Margin = new Thickness(5, 0, 2, 2);
                b.Padding = new Thickness(4, 0, 4, 0);
                b.Height = 23;
                b.Content = talent;
                b.ContextMenu = new System.Windows.Controls.ContextMenu();
                // Löschen
                MenuItem m = new MenuItem();
                m.Header = "Löschen";
                m.Tag = b;
                m.Click += new RoutedEventHandler(MenuItemProbeFavoritDelete_Click);
                b.ContextMenu.Items.Add(m);
                // Alle Löschen
                m = new MenuItem();
                m.Header = "Alle Löschen";
                m.Tag = b;
                m.Click += new RoutedEventHandler(MenuItemProbeFavoritDeleteAll_Click);
                b.ContextMenu.Items.Add(m);

                b.Click += new RoutedEventHandler(ButtonProbeFavorit_Click);
                _wrapPanelProbenFavoriten.Children.Add(b);

                SetComboProben();
            }
        }

        void MenuItemProbeFavoritDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            _wrapPanelProbenFavoriten.Children.Clear();

            if (App.DatenDataSet != null && App.DatenDataSet.Einstellungen != null)
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("ProbenFavoriten");
                if (row != null && !row.IsWertTextNull())
                {
                    row.WertText = string.Empty;
                }
            }
        }

        void MenuItemProbeFavoritDelete_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)((MenuItem)sender).Tag;
            string talent = b.Content.ToString();
            _wrapPanelProbenFavoriten.Children.Remove(b);

            if (App.DatenDataSet != null && App.DatenDataSet.Einstellungen != null)
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("ProbenFavoriten");
                if (row != null && !row.IsWertTextNull())
                {
                    row.WertText = row.WertText.Replace(talent + "#", string.Empty);
                    row.WertText = row.WertText.Replace(talent, string.Empty);
                }
            }
        }

        void ButtonProbeFavorit_Click(object sender, RoutedEventArgs e)
        {
            ProbeWürfeln(((Button)sender).Content.ToString());
        }

        public event ZooBotEventHandler ZooBotClick;
        
        private void ButtonZooBot_Click(object sender, RoutedEventArgs e)
        {
            if (ZooBotClick != null && SelectedProbe() != null)
            {
                ZooBotClick(SelectedProbe().Name);
            }
        }
    }

    public delegate void ZooBotEventHandler(string talentname);
}
