using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;

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
                {
                    WertCount = _probe.Werte.Length;
                }

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

                Model.Talent talent = null;
                if (Probe is Model.Talent)
                    talent = Probe as Model.Talent;
                else if (Probe is Model.Held_Talent)
                    talent = (Probe as Model.Held_Talent).Talent;

                if (talent != null && _eigenschaftWurfItemListe.Count >= 3)
                {
                    // Eigenschaftsnamen setzen
                    _eigenschaftWurfItemListe[0].Name = talent.Eigenschaft1;
                    _eigenschaftWurfItemListe[1].Name = talent.Eigenschaft2;
                    _eigenschaftWurfItemListe[2].Name = talent.Eigenschaft3;

                    if (Held != null)
                    {
                        // Eigenschaftswerte setzen
                        _eigenschaftWurfItemListe[0].Wert = Held.GetEigenschaftWert(talent.Eigenschaft1);
                        _eigenschaftWurfItemListe[1].Wert = Held.GetEigenschaftWert(talent.Eigenschaft2);
                        _eigenschaftWurfItemListe[2].Wert = Held.GetEigenschaftWert(talent.Eigenschaft3);
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

        public void Würfeln(object obj)
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
            if (Würfel.SoundAbspielen)
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
        /// Der Name der GetEigenschaftWert, auf die geworfen wird.
        /// </summary>
        public string Name { get { return _name; } set { _name = value; OnChanged("Name"); } }

        private int _wert = 0;
        /// <summary>
        /// Der Wert der GetEigenschaftWert.
        /// </summary>
        public int Wert { get { return _wert; } set { _wert = value; OnChanged("Wert"); } }

        private int _wurf = 0;
        /// <summary>
        /// Das Würfelergebnis des Wurfes.
        /// </summary>
        public int Wurf { get { return _wurf; } set { _wurf = value; OnChanged("Wurf"); } }
    }

    #endregion
}
