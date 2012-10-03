using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

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

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        private ProbenErgebnis _ergebnis = new ProbenErgebnis();
        public ProbenErgebnis Ergebnis
        {
            get { return _ergebnis; }
            set
            {
                _ergebnis = value;
                OnChanged("Ergebnis");
                OnChanged("ErgebnisImagePath");
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
            }
        }

        private Probe _probe = null;
        public Probe Probe
        {
            get { return _probe; }
            set
            {
                _probe = value;

                if (_probe != null)
                    WertCount = _probe.Werte.Length;

                OnChanged("Probe");
                OnChanged("Probenname");
                OnChanged("EigenschaftWurfItemListe");
            }
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

        public string Probenname
        {
            get 
            {
                if (Probe == null)
                    return string.Empty;
                if (Probe is Model.Talent)
                    return (Probe as Model.Talent).Talentname;
                if (Probe is Model.Held_Talent)
                    return (Probe as Model.Held_Talent).Talent.Talentname;
                else if (Probe is Model.Zauber)
                    return (Probe as Model.Zauber).Name;
                if (Probe is Model.Held_Zauber)
                    return (Probe as Model.Held_Zauber).Zauber.Name;
                // TODO MT: GetEigenschaftWert muss noch von Probe ableiten
                //else if (Probe is GetEigenschaftWert) 
                //    return (Probe as GetEigenschaftWert).Name;
                return string.Empty; 
            }
        }

        public int WertCount
        {
            get { return EigenschaftWurfItemListe.Count; }
            set
            {
                if (value < 0)
                    return;

                var list = new List<EigenschaftWurfItem>(value);
                for (int i = 0; i < value; i++)
                {
                    var item = new EigenschaftWurfItem();
                    item.PropertyChanged += EigenschaftWurfItem_PropertyChanged;
                    list.Add(item);
                }
                EigenschaftWurfItemListe = list;

                OnChanged("WertCount");
            }
        }

        private List<EigenschaftWurfItem> _eigenschaftWurfItemListe = new List<EigenschaftWurfItem>();
        public List<EigenschaftWurfItem> EigenschaftWurfItemListe
        {
            get 
            {
                // TODO MT: Für Zauber und Eigenschaften erweitern

                foreach (var item in _eigenschaftWurfItemListe)
                    item.Held = Held;

                Model.Talent talent = null;
                Model.Zauber zauber = null;
                if (Probe is Model.Talent)
                    talent = Probe as Model.Talent;
                else if (Probe is Model.Held_Talent)
                    talent = (Probe as Model.Held_Talent).Talent;
                else if (Probe is Model.Zauber)
                    zauber = Probe as Model.Zauber;
                else if (Probe is Model.Held_Zauber)
                    zauber = (Probe as Model.Held_Zauber).Zauber;

                if ((talent != null || zauber != null) && _eigenschaftWurfItemListe.Count >= 3)
                {
                    // Eigenschaftsnamen setzen
                    string e1 = (talent != null) ? talent.Eigenschaft1
                        : ((zauber != null) ? zauber.Eigenschaft1 : string.Empty);
                    string e2 = (talent != null) ? talent.Eigenschaft2
                        : ((zauber != null) ? zauber.Eigenschaft2 : string.Empty);
                    string e3 = (talent != null) ? talent.Eigenschaft3
                        : ((zauber != null) ? zauber.Eigenschaft3 : string.Empty);

                    _eigenschaftWurfItemListe[0].Name = e1;
                    _eigenschaftWurfItemListe[1].Name = e2;
                    _eigenschaftWurfItemListe[2].Name = e3;

                    if (Held != null)
                    {
                        // Eigenschaftswerte setzen
                        _eigenschaftWurfItemListe[0].Wert = Held.GetEigenschaftWert(e1);
                        _eigenschaftWurfItemListe[1].Wert = Held.GetEigenschaftWert(e2);
                        _eigenschaftWurfItemListe[2].Wert = Held.GetEigenschaftWert(e3);
                    }
                }

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

        public ProbeControlViewModel()
        {
            WertCount = 3;

            onWürfeln = new Base.CommandBase(Würfeln, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        internal void Refresh()
        {
            OnChanged("Held");
            OnChanged("Ergebnis");
        }

        public void Würfeln(object obj = null)
        {
            if (Probe != null)
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

            // Sound abspielen
            if (MeisterGeister.Logic.Settings.Einstellungen.WuerfelSoundAbspielen)
                MeisterGeister.Logic.General.AudioPlayer.PlayWürfel();

            // Event werfen
            if (Gewürfelt != null)
                Gewürfelt(this, new EventArgs());
        }

        #endregion

        #region //---- EVENTS ----

        private void EigenschaftWurfItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Wurf")
            {
                if (sender is EigenschaftWurfItem)
                {
                    if (Probe != null)
                    {
                        if (Ergebnis.Würfe == null)
                            Ergebnis.Würfe = new int[this.WertCount];
                        for (int i = 0; i < Ergebnis.Würfe.Length; i++)
                            Ergebnis.Würfe[i] = EigenschaftWurfItemListe[i].Wurf;

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
            set { _held = value; OnChanged("Held"); OnChanged("StartWert"); OnChanged("ModList"); } 
        }

        public int StartWert
        {
            get 
            {
                if (Held == null)
                    return Wert;
                return Held.GetEigenschaftWert(Name, true);
            }
        }

        public List<dynamic> ModList
        {
            get
            {
                if (Held == null)
                    return new List<dynamic>();
                return Held.ModifikatorenListe(Eigenschaft.GetModType(Name), StartWert);
            }
        }
    }

    #endregion
}
