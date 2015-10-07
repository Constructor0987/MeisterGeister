using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister.ViewModel.Proben
{
    public class ProbeControlViewModel : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onWürfeln;
        public Base.CommandBase OnWürfeln
        {
            get { return onWürfeln; }
        }

        private Base.CommandBase onKontrollProbeWürfeln;
        public Base.CommandBase OnKontrollProbeWürfeln
        {
            get { return onKontrollProbeWürfeln; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        private bool _nichtProben = false;
        public bool NichtProben
        {
            get { return _nichtProben; }
            set 
            { 
                _nichtProben = value;
                Ergebnis = new ProbenErgebnis();
                foreach (var item in EigenschaftWurfItemListe)
                {
                    item.PropertyChanged -= EigenschaftWurfItem_PropertyChanged;
                    item.Wurf = 0;
                    item.PropertyChanged += EigenschaftWurfItem_PropertyChanged;
                }
                Opacity = value ? 0.5 : 1.0;
                OnChanged("NichtProben");
            }
        }

        private double _opacity = 1.0;
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OnChanged("Opacity");
            }
        }

        private Orientation _orientation = Orientation.Horizontal;
        public Orientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; OnChanged("Orientation"); }
        }

        /// <summary>
        /// Bei 'true' wird das Abspielen des WürfelSounds unterbunden.
        /// </summary>
        public bool LockSoundAbspielen { get; set; }

        public bool SoundAbspielen
        {
            get { return Einstellungen.WuerfelSoundAbspielen; }
            set { Einstellungen.WuerfelSoundAbspielen = value; OnChanged("SoundAbspielen"); }
        }

        private ProbenErgebnis _ergebnis = new ProbenErgebnis();
        public ProbenErgebnis Ergebnis
        {
            get { return _ergebnis; }
            set
            {
                _ergebnis = value;
                OnChanged("Ergebnis");
                OnChanged("ErgebnisImagePath");
                OnChanged("KontrollProbeVisibility");
                OnChanged("KontrollProbeErgebnisImagePath");
                OnChanged("Erfolgschance");
                NotifyErgebnisChanged();
            }
        }

        private Model.Held _held = null;
        public Model.Held Held
        {
            get { return _held; }
            set
            {
                _held = value;
                foreach (var item in EigenschaftWurfItemListe)
                    item.Held = value;
                OnChanged("Held");
                OnChanged("Readonly");
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return _held != null ? _held.Name : _name; }
            set
            {
                _name = value;
                OnChanged("Held");
                OnChanged("Name");
            }
        }

        public bool Readonly
        {
            get { return _held != null; }
        }

        private Probe _probe = new Probe();
        public Probe Probe
        {
            get { return _probe; }
            set
            {
                _probe = value;

                if (_probe != null && _probe.Werte != null)
                    WertCount = _probe.Werte.Length;

                OnChanged("Probe");
                OnChanged("Probenname");
                OnChanged("EigenschaftWurfItemListe");
            }
        }

        private int _fertigkeitswert = 0;
        public int Fertigkeitswert
        {
            get { return Probe != null ? Probe.Fertigkeitswert : _fertigkeitswert; }
            set
            {
                if (NichtProben)
                    return;
                _fertigkeitswert = value;
                if (Probe != null)
                {
                    Probe.Fertigkeitswert = _fertigkeitswert;
                    Ergebnis = Probe.ProbenErgebnisBerechnen(Ergebnis);
                }

                OnChanged("Fertigkeitswert");
                OnChanged("Erfolgschance");
            }
        }

        private int _modifikator = 0;
        public int Modifikator
        {
            get { return Probe != null ? Probe.Modifikator : _modifikator; }
            set
            {
                if (NichtProben)
                    return;
                _modifikator = value;
                if (Probe != null)
                {
                    Probe.Modifikator = _modifikator;
                    Ergebnis = Probe.ProbenErgebnisBerechnen(Ergebnis);
                }

                OnChanged("Modifikator");
                OnChanged("Erfolgschance");
            }
        }

        public int ModifikatorProben
        {
            get
            {
                if (Held == null)
                    return 0;
                return Held.GetModifikatorProben(Probe);
            }
        }

        public List<dynamic> ModListProben
        {
            get
            {
                if (Held == null)
                    return new List<dynamic>();
                return Held.GetModifikatorenListe(Probe);
            }
        }

        public double Erfolgschance
        {
            get { return Probe != null ? Probe.Erfolgschance : 0.0; }
            set { }
        }

        public string ErgebnisImagePath
        {
            get
            {
                if (Ergebnis != null)
                {
                    switch (Ergebnis.Ergebnis)
                    {
                        case ErgebnisTyp.KEIN_ERGEBNIS:
                            break;
                        case ErgebnisTyp.MISSLUNGEN:
                            return "/DSA MeisterGeister;component/Images/Icons/General/entf_01.png";
                        case ErgebnisTyp.PATZER:
                            return "/DSA MeisterGeister;component/Images/Icons/Wetter/gewitter.png";
                        case ErgebnisTyp.FATALER_PATZER:
                            return "/DSA MeisterGeister;component/Images/Icons/tot.png";
                        case ErgebnisTyp.GELUNGEN:
                            return "/DSA MeisterGeister;component/Images/Icons/General/ok.png";
                        case ErgebnisTyp.GLÜCKLICH:
                            return "/DSA MeisterGeister;component/Images/Icons/General/neu.png";
                        case ErgebnisTyp.MEISTERHAFT:
                            return "/DSA MeisterGeister;component/Images/Icons/Wetter/sonne.png";
                        default:
                            break;
                    }
                }
                return "/DSA MeisterGeister;component/Images/Icons/General/question.png";
            }
        }

        public System.Windows.Visibility KontrollProbeVisibility
        {
            get
            {
                if (Probe.KontrollProbeErgebnis == null)
                    return System.Windows.Visibility.Collapsed;
                return System.Windows.Visibility.Visible;
            }
        }

        public string KontrollProbeErgebnisImagePath
        {
            get
            {
                if (Probe.KontrollProbeErgebnis != null)
                {
                    switch (Probe.KontrollProbeErgebnis.Ergebnis)
                    {
                        case ErgebnisTyp.KEIN_ERGEBNIS:
                            break;
                        case ErgebnisTyp.MISSLUNGEN:
                            return "/DSA MeisterGeister;component/Images/Icons/General/entf_01.png";
                        case ErgebnisTyp.PATZER:
                            return "/DSA MeisterGeister;component/Images/Icons/Wetter/gewitter.png";
                        case ErgebnisTyp.FATALER_PATZER:
                            return "/DSA MeisterGeister;component/Images/Icons/tot.png";
                        case ErgebnisTyp.GELUNGEN:
                            return "/DSA MeisterGeister;component/Images/Icons/General/ok.png";
                        case ErgebnisTyp.GLÜCKLICH:
                            return "/DSA MeisterGeister;component/Images/Icons/General/neu.png";
                        case ErgebnisTyp.MEISTERHAFT:
                            return "/DSA MeisterGeister;component/Images/Icons/Wetter/sonne.png";
                        default:
                            break;
                    }
                }
                return "/DSA MeisterGeister;component/Images/Icons/General/question.png";
            }
        }

        public int WertCount
        {
            get { return EigenschaftWurfItemListe.Count; }
            set
            {
                if (value < 0)
                    return;

                if (Probe != null && Probe.Werte.Length != value)
                    Probe.Werte = new int[value];
                SetupEigenschaftWurfItemListe(value);
                UpdateEigenschaftWurfItemListe();

                OnChanged("WertCount");
            }
        }

        private void SetupEigenschaftWurfItemListe(int length)
        {
            var list = new List<EigenschaftWurfItem>(length);
            for (int i = 0; i < length; i++)
            {
                var item = new EigenschaftWurfItem();
                if (Probe != null)
                    item.Wert = Probe.Werte[i];
                item.PropertyChanged += EigenschaftWurfItem_PropertyChanged;
                list.Add(item);
            }
            EigenschaftWurfItemListe = list;
        }

        private void UpdateEigenschaftWurfItemListe()
        {
            foreach (var item in _eigenschaftWurfItemListe)
            {
                item.PropertyChanged -= EigenschaftWurfItem_PropertyChanged;
                item.Held = Held;
            }

            Model.Talent talent = null;
            Model.Zauber zauber = null;
            Eigenschaft eigenschaft = null;
            if (Probe is Model.Talent)
                talent = Probe as Model.Talent;
            else if (Probe is Model.Held_Talent)
                talent = (Probe as Model.Held_Talent).Talent;
            else if (Probe is MetaTalent)
                talent = (Probe as MetaTalent).Talent;
            else if (Probe is Model.Zauber)
                zauber = Probe as Model.Zauber;
            else if (Probe is Model.Held_Zauber)
                zauber = (Probe as Model.Held_Zauber).Zauber;
            else if (Probe is Eigenschaft)
                eigenschaft = Probe as Eigenschaft;

            if ((talent != null || zauber != null) && _eigenschaftWurfItemListe.Count >= 3)
            {
                // Eigenschaftsnamen setzen
                string e1 = (talent != null) ? talent.Eigenschaft1
                    : ((zauber != null) ? zauber.Eigenschaft1 : string.Empty);
                string e2 = (talent != null) ? talent.Eigenschaft2
                    : ((zauber != null) ? zauber.Eigenschaft2 : string.Empty);
                string e3 = (talent != null) ? talent.Eigenschaft3
                    : ((zauber != null) ? zauber.Eigenschaft3 : string.Empty);
                string[] e = { e1, e2, e3 };

                _eigenschaftWurfItemListe[0].Name = e1;
                _eigenschaftWurfItemListe[1].Name = e2;
                _eigenschaftWurfItemListe[2].Name = e3;

                if (Held != null)
                {
                    // Eigenschaftswerte setzen
                    for (int i = 0; i < _eigenschaftWurfItemListe.Count; i++)
                    {
                        _eigenschaftWurfItemListe[i].Wert = Held.EigenschaftWert(e[i]);
                    }
                }
            }
            else if (eigenschaft != null)
            {
                _eigenschaftWurfItemListe[0].Name = eigenschaft.Abkürzung;
                _eigenschaftWurfItemListe[0].Wert = eigenschaft.Wert;
            }

            foreach (var item in _eigenschaftWurfItemListe)
            {
                item.PropertyChanged += EigenschaftWurfItem_PropertyChanged;
            }
        }

        private List<EigenschaftWurfItem> _eigenschaftWurfItemListe = new List<EigenschaftWurfItem>();
        public List<EigenschaftWurfItem> EigenschaftWurfItemListe
        {
            get 
            {
                return _eigenschaftWurfItemListe; 
            }
            set
            {
                _eigenschaftWurfItemListe = value;
                OnChanged("EigenschaftWurfItemListe");
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public ProbeControlViewModel() : base(View.General.ViewHelper.ShowProbeDialog)
        {
            WertCount = Probe.Werte.Length;

            onWürfeln = new Base.CommandBase(Würfeln, null);
            onKontrollProbeWürfeln = new Base.CommandBase(KontrollProbeWürfeln, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        internal void Refresh()
        {
            OnChanged("Held");
            OnChanged("Ergebnis");
            OnChanged("Erfolgschance");
        }

        public void Würfeln(object obj = null)
        {
            UpdateEigenschaftWurfItemListe();
            if (Probe != null && !NichtProben)
            {
                for (int i = 0; i < Probe.Werte.Length; i++)
                    Probe.Werte[i] = EigenschaftWurfItemListe[i].Wert;

                Ergebnis = Probe.Würfeln();

                for (int i = 0; i < Ergebnis.Würfe.Length; i++)
                {
                    EigenschaftWurfItemListe[i].PropertyChanged -= EigenschaftWurfItem_PropertyChanged;
                    EigenschaftWurfItemListe[i].Wurf = Ergebnis.Würfe[i];
                    EigenschaftWurfItemListe[i].PropertyChanged += EigenschaftWurfItem_PropertyChanged;
                }
            }

            // Refresh, damit die UI aktulisiert wird
            OnChanged("Held");
            OnChanged("Probe");
            OnChanged("ModifikatorProben");
            OnChanged("ModListProben");

            // Sound abspielen
            if (!LockSoundAbspielen && MeisterGeister.Logic.Einstellung.Einstellungen.WuerfelSoundAbspielen)
                MeisterGeister.Logic.General.AudioPlayer.PlayWürfel();

            NotifyErgebnisChanged();
        }

        private void KontrollProbeWürfeln(object obj)
        {
            Probe.KontrollProbeErgebnis = ShowProbeDialog(Probe.KontrollProbe(Probe), Held);
            Ergebnis = Probe.ProbenErgebnisBerechnen(Ergebnis);
        }

        private void NotifyErgebnisChanged()
        {
            // Event werfen
            if (Gewürfelt != null)
                Gewürfelt(this, new EventArgs());
        }

        #endregion

        #region //---- EVENTS ----

        private void EigenschaftWurfItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Wurf" || e.PropertyName == "Wert")
            {
                if (sender is EigenschaftWurfItem)
                {
                    if (NichtProben)
                    {
                        foreach (var item in EigenschaftWurfItemListe)
                        {
                            item.PropertyChanged -= EigenschaftWurfItem_PropertyChanged;
                            item.Wurf = 0;
                            item.PropertyChanged += EigenschaftWurfItem_PropertyChanged;
                        }
                        return;
                    }
                    if (Probe != null)
                    {
                        bool changed = false;
                        if (e.PropertyName == "Wurf")
                        {
                            if (Ergebnis.Würfe == null)
                                Ergebnis.Würfe = new int[this.WertCount];
                            for (int i = 0; i < Ergebnis.Würfe.Length; i++)
                            {
                                if (Ergebnis.Würfe[i] != EigenschaftWurfItemListe[i].Wurf)
                                {
                                    Ergebnis.Würfe[i] = EigenschaftWurfItemListe[i].Wurf;
                                    changed = true;
                                }
                            }
                        }
                        else if (e.PropertyName == "Wert")
                        {
                            for (int i = 0; i < Probe.Werte.Length; i++)
                            {
                                if (Probe.Werte[i] != EigenschaftWurfItemListe[i].Wert)
                                {
                                    Probe.Werte[i] = EigenschaftWurfItemListe[i].Wert;
                                    changed = true;
                                }
                            }
                        }

                        if (changed)
                            Ergebnis = Probe.ProbenErgebnisBerechnen(Ergebnis);
                    }
                }
            }
        }

        public event EventHandler Gewürfelt;

        #endregion
    }

    #region //---- SUBKLASSEN ----

    public class EigenschaftWurfItem : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onSetWurf;
        public Base.CommandBase OnSetWurf
        {
            get { return onSetWurf; }
        }

        #endregion //---- COMMANDS ----

        #region //---- KONSTRUKTOR ----

        public EigenschaftWurfItem()
        {
            onSetWurf = new Base.CommandBase(SetWurf, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void SetWurf(object obj)
        {
            int wurf = 0;
            if (Int32.TryParse(obj.ToString(), out wurf))
                Wurf = (wurf == 0) ? Würfel.Wurf(20) : wurf; // bei Wurf=0 wird gewürfelt
            ContextMenuIsOpen = false;
        }

        #endregion //---- INSTANZMETHODEN ----

        #region //---- EIGENSCHAFTEN & FELDER ----

        // UI-Steuerung
        private bool _contextMenuIsOpen;
        public bool ContextMenuIsOpen { get { return _contextMenuIsOpen; } set { _contextMenuIsOpen = value; OnChanged("ContextMenuIsOpen"); } }

        private string _name = string.Empty;
        /// <summary>
        /// Der Name der Eigenschaft, auf die geworfen wird.
        /// </summary>
        public string Name { get { return _name; } set { _name = value; OnChanged("Name"); } }

        private int _wert = 0;
        /// <summary>
        /// Der Wert der Eigenschaft.
        /// </summary>
        public int Wert { get { return _wert; } set { _wert = value; OnChanged("Wert"); } }

        private int _wurf = 0;
        /// <summary>
        /// Das Würfelergebnis des Wurfes.
        /// </summary>
        public int Wurf { get { return _wurf; } set { _wurf = value; OnChanged("Wurf"); } }

        private Model.Held _held = null;
        public Model.Held Held 
        { 
            get { return _held; }
            set { _held = value; OnChanged("Held"); OnChanged("StartWert"); OnChanged("ModList"); OnChanged("Readonly"); } 
        }

        public bool Readonly
        {
            get { return _held != null; }
        }

        public int StartWert
        {
            get 
            {
                if (Held == null)
                    return Wert;
                return Held.EigenschaftWert(Name, true);
            }
        }

        public List<dynamic> ModList
        {
            get
            {
                Type modType = Eigenschaft.GetModType(Name);
                if (Held == null || modType == null)
                    return new List<dynamic>();
                return Held.ModifikatorenListe(modType, StartWert);
            }
        }

        #endregion //---- EIGENSCHAFTEN & FELDER ----
    }

    #endregion
}
