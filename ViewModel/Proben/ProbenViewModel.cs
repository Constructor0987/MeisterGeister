using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Settings;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.ViewModel.Proben
{
    public class ProbenViewModel : ViewModel.Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onWürfeln;
        public Base.CommandBase OnWürfeln
        {
            get { return onWürfeln; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        public List<Model.Held> HeldListe
        {
            get { return Global.ContextHeld.HeldenGruppeListe; }
        }

        private List<ProbeControlViewModel> _probeErgebnisListe = new List<ProbeControlViewModel>();
        public List<ProbeControlViewModel> ProbeErgebnisListe
        {
            get { return _probeErgebnisListe.OrderByDescending(vm => vm.Ergebnis.Übrig).
                ThenByDescending(vm => vm.Ergebnis.Qualität).
                ThenByDescending(vm => vm.Probe.Fertigkeitswert).ToList(); }
            set
            {
                _probeErgebnisListe = value;
                OnChanged("ProbeErgebnisListe");
            }
        }

        private Probe _selectedProbe = null;
        public Probe SelectedProbe
        {
            get { return _selectedProbe; }
            set 
            { 
                _selectedProbe = value;
                RefreshProbeErgebnisListe();
                OnChanged("SelectedProbe");
            }
        }

        private FilterItem _selectedFilterItem = null;
        public FilterItem SelectedFilterItem
        {
            get { return _selectedFilterItem; }
            set 
            { 
                _selectedFilterItem = value;
                FilterProbeListe();
                OnChanged("SelectedFilterItem");
            }
        }

        int _modifikator = 0;
        public int Modifikator
        {
            get { return _modifikator; }
            set
            {
                _modifikator = value;

                // Modifikator an ProbeControls weiterreichen
                foreach (ProbeControlViewModel er in ProbeErgebnisListe)
                    er.Modifikator = _modifikator;
                OnChanged("Modifikator");
                OnChanged("ProbeErgebnisListe");
            }
        }

        public string GruppenErgebnis
        {
            get
            {
                int tapSum = 0, vorSum = 0, i = 1;
                string art = SelectedProbe == null ? "Punkte*" : SelectedProbe.PunkteText + "*";
                foreach (ProbeControlViewModel er in ProbeErgebnisListe)
                {
                    if (er.Ergebnis.Übrig >= 0) //nur positive Ergebnisse addieren
                    {
                        tapSum += er.Ergebnis.Übrig;
                        vorSum += Convert.ToInt32(Math.Round((double)er.Ergebnis.Übrig / i, 0, MidpointRounding.AwayFromZero));
                        i++;
                    }
                }

                return string.Format("Unabhängige Zusammenarbeit: {0} {1}\nMit fähigstem Held als Vorarbeiter: {2} {1}", tapSum, art, vorSum);
            }
        }

        public bool SoundAbspielen
        {
            get { return Einstellungen.WuerfelSoundAbspielen; }
            set { Einstellungen.WuerfelSoundAbspielen = value; OnChanged("SoundAbspielen"); }
        }

        // Listen
        List<Probe> _probeListe = new List<Probe>();
        public List<Probe> ProbeListe
        {
            get { return _probeListe; }
            set { _probeListe = value; FilterProbeListe(); OnChanged("ProbeListe"); }
        }

        List<Probe> _filteredProbeListe = new List<Probe>();
        public List<Probe> FilteredProbeListe
        {
            get { return _filteredProbeListe; }
            set { _filteredProbeListe = value; OnChanged("FilteredProbeListe"); }
        }

        List<FilterItem> _filterListe = new List<FilterItem>();
        public List<FilterItem> FilterListe
        {
            get { return _filterListe; }
            set { _filterListe = value; FilterProbeListe(); OnChanged("FilterListe"); }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public ProbenViewModel()
        {
            onWürfeln = new Base.CommandBase(Würfeln, null);
            Einstellungen.WuerfelSoundAbspielenChanged += WuerfelSoundAbspielenChanged;
            Global.GruppenProbeWürfeln += Global_GruppenProbeWürfeln;

            InitFilterListe();

            Refresh();
            InitProbeListe();
            RefreshProbeErgebnisListe();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void InitFilterListe()
        {
            // Filter-Liste
            _filterListe.Add(new FilterItem("Alle"));
            _filterListe.Add(new FilterItem("Häufig verwendet"));
            _filterListe.Add(new FilterItem("Eigenschaft"));

            _filterListe.Add(new FilterItem("Talent"));
            // Talentgruppen
            _filterListe.Add(new FilterItem("Kampf"));
            _filterListe.Add(new FilterItem("Körper"));
            _filterListe.Add(new FilterItem("Gesellschaft"));
            _filterListe.Add(new FilterItem("Natur"));
            _filterListe.Add(new FilterItem("Wissen"));
            _filterListe.Add(new FilterItem("Handwerk"));
            _filterListe.Add(new FilterItem("Sprachen/Schriften"));
            _filterListe.Add(new FilterItem("Gabe"));
            _filterListe.Add(new FilterItem("Ritualkenntnis"));
            _filterListe.Add(new FilterItem("Liturgiekenntnis"));
            _filterListe.Add(new FilterItem("Meta"));
            _filterListe.Add(new FilterItem("Basis"));
            _filterListe.Add(new FilterItem("Spezial"));

            _filterListe.Add(new FilterItem("Zauber"));

            _selectedFilterItem = FilterListe.FirstOrDefault();
        }

        private void InitProbeListe()
        {
            ProbeListe.Clear();
            // Eigenschaften hinzufügen
            ProbeListe.AddRange(Eigenschaft.EigenschaftenListe);
            // Talente hinzufügen
            ProbeListe.AddRange(Global.ContextHeld.Liste<Model.Talent>());
            // Zauber hinzufügen
            ProbeListe.AddRange(Global.ContextHeld.Liste<Model.Zauber>());

            FilterProbeListe();
        }

        private void FilterProbeListe()
        {
            if (SelectedFilterItem == null)
            {
                FilteredProbeListe = ProbeListe.OrderBy(p => p.Probenname).ToList();
                return;
            }

            switch (SelectedFilterItem.Name)
            {
                case"Alle":
                    FilteredProbeListe = ProbeListe.OrderBy(p => p.Probenname).ToList();
                    return;
                case "Häufig verwendet":
                    List<string> häufigList = new List<string> { "Fährtensuchen", "Gassenwissen", "Gefahreninstinkt", "Menschenkenntnis", 
                        "Orientierung", "Schleichen", "Sich Verstecken", "Sinnenschärfe", "Überreden", "Wildnisleben" };
                    FilteredProbeListe = ProbeListe.Where(p => p is Model.Talent
                        && häufigList.Contains(p.Probenname)).OrderBy(p => p.Probenname).ToList();
                    return;
                case "Eigenschaft":
                    FilteredProbeListe = ProbeListe.Where(p => p is Eigenschaft).OrderBy(p => p.Probenname).ToList();
                    return;
                case "Talent":
                    FilteredProbeListe = ProbeListe.Where(p => p is Model.Talent).OrderBy(p => p.Probenname).ToList();
                    return;
                case "Basis":
                case "Spezial":
                    FilteredProbeListe = ProbeListe.Where(p => p is Model.Talent
                        && (p as Model.Talent).Talenttyp == SelectedFilterItem.Name).OrderBy(p => p.Probenname).ToList();
                    return;
                case "Zauber":
                    FilteredProbeListe = ProbeListe.Where(p => p is Model.Zauber).OrderBy(p => p.Probenname).ToList();
                    return;
                default:
                    FilteredProbeListe = ProbeListe.Where(p => p is Model.Talent 
                        && (p as Model.Talent).Talentgruppe.Kurzname == SelectedFilterItem.Name).OrderBy(p => p.Probenname).ToList();
                    return;
            }
        }

        public void Refresh()
        {
            OnChanged("HeldListe");
        }

        private void RefreshProbeErgebnisListe()
        {
            _probeErgebnisListe.Clear();

            foreach (var item in HeldListe)
            {
                // TODO MT: Probem wenn der Held einen Zauber in mehreren Rep. hat
                // Solange wird erstmal der Zauber mit dem höchsten ZfW ausgewählt

                ProbeControlViewModel vm = new ProbeControlViewModel();
                vm.Held = item;
                if (SelectedProbe is Model.Talent)
                    vm.Probe = item.Held_Talent.Where(t => t.Talent == SelectedProbe).FirstOrDefault();
                else if (SelectedProbe is Model.Zauber)
                    vm.Probe = item.Held_Zauber.Where(z => z.Zauber == SelectedProbe).OrderByDescending(z => z.ZfW).FirstOrDefault();
                else if (SelectedProbe is Eigenschaft)
                    vm.Probe = item.Eigenschaft((SelectedProbe as Eigenschaft).Name);
                vm.Gewürfelt += ProbeControlGewürfelt;

                // nur einfügen, wenn der Held die Fähigkeit besitzt
                if (vm.Probe != null)
                    _probeErgebnisListe.Add(vm);
            }
            OnChanged("ProbeErgebnisListe");
        }

        private void Würfeln(object obj)
        {
            // Alle Proben neu würfeln
            foreach (var item in ProbeErgebnisListe)
            {
                item.LockSoundAbspielen = true;
                item.Würfeln();
                item.LockSoundAbspielen = false;
            }

            // Sound abspielen
            if (Einstellungen.WuerfelSoundAbspielen)
                MeisterGeister.Logic.General.AudioPlayer.PlayWürfel();

            OnChanged("ProbeErgebnisListe");
            OnChanged("GruppenErgebnis");
        }

        #endregion

        #region //---- EVENTS ----

        private void WuerfelSoundAbspielenChanged(object sender, EventArgs e)
        {
            OnChanged("SoundAbspielen");
        }

        private void ProbeControlGewürfelt(object sender, EventArgs e)
        {
            OnChanged("GruppenErgebnis");
        }

        private void Global_GruppenProbeWürfeln(Probe probe, EventArgs e)
        {
            SelectedFilterItem = FilterListe.FirstOrDefault();
            if (probe is Model.Held_Talent)
                SelectedProbe = (probe as Model.Held_Talent).Talent;
            else if (probe is Model.Held_Zauber)
                SelectedProbe = (probe as Model.Held_Zauber).Zauber;
            else
                SelectedProbe = probe;
            Würfeln(null);
        }

        #endregion
    }

    #region //---- SUBKLASSEN ----

    public class FilterItem
    {
        public FilterItem(string name)
        {
            Name = name;
            SetImagePath();
        }
        public string Name { get; set; }

        private string _imagePath = string.Empty;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        private void SetImagePath()
        {
            switch (Name)
            {
                case "Alle":
                    // TODO ??: Passenderes Icon
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/General/filter.png";
                    break;
                case "Häufig verwendet":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/General/neu.png";
                    break;
                case "Eigenschaft":
                    // TODO ??: Passenderes Icon
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/Wuerfel/w20.png";
                    break;
                case "Talent":
                    // TODO ??: Passenderes Icon
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/helden.png";
                    break;
                case "Kampf":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/nahkampf_01.png";
                    break;
                case "Körper":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/ueberanstrengung.png";
                    break;
                case "Gesellschaft":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/helden_kopieren.png";
                    break;
                case "Natur":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/kraeutersuche.png";
                    break;
                case "Wissen":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/foliant.png";
                    break;
                case "Handwerk":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/schmiede.png";
                    break;
                case "Sprachen/Schriften":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/sprache.png";
                    break;
                case "Gabe":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/hesinde.png";
                    break;
                case "Ritualkenntnis":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/zauberzeichen.png";
                    break;
                case "Liturgiekenntnis":
                    // TODO ??: Passenderes Icon
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/audio2.png";
                    break;
                case "Meta":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/jagd.png";
                    break;
                case "Basis":
                    // TODO ??: Passenderes Icon
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/General/kreise.png";
                    break;
                case "Spezial":
                    // TODO ??: Passenderes Icon
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/General/info.png";
                    break;
                case "Zauber":
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/magie.png";
                    break;
                default:
                    // TODO ??: Passenderes Icon
                    _imagePath = "/DSA MeisterGeister;component/Images/Icons/General/question.png";
                    break;
            }
        }
    }

    public class ProbeErgebnisItem : Base.ViewModelBase
    {
        private Model.Held _held = null;
        public Model.Held Held 
        { 
            get { return _held; } 
            set { _held = value; OnChanged("Held"); } 
        }

        public Probe Probe { get; set; }

        private ProbeControlViewModel _vm = null;
        public ProbeControlViewModel VM
        {
            get { return _vm; }
            set { _vm = value; OnChanged("VM"); }
        }
    }

    #endregion
}
