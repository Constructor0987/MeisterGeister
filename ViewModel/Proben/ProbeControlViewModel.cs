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

        //public ProbeControlViewModel Self { get { return this; } set { return; } }

        private ProbenErgebnis _ergebnis = new ProbenErgebnis();
        public ProbenErgebnis Ergebnis
        {
            get { return _ergebnis; }
            set
            {
                _ergebnis = value;
                OnChanged("Ergebnis");
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

                // Wertanzahl setzen
                //if (Probe is Model.Talent || Probe is Model.Held_Talent
                //    || Probe is Model.Zauber || Probe is Model.Held_Zauber)
                //{
                //    WertCount = 3;
                //    if (Probe is Model.Talent)
                //    {
                //        EigenschaftWurfItemListe[0].Name = (Probe as Model.Talent).Eigenschaft1;
                //        EigenschaftWurfItemListe[1].Name = (Probe as Model.Talent).Eigenschaft2;
                //        EigenschaftWurfItemListe[2].Name = (Probe as Model.Talent).Eigenschaft3;
                //        if (Held != null)
                //        {
                //            // Eigenschaften setzen
                //            EigenschaftWurfItemListe[0].Wert = Held.GetEigenschaftWert((Probe as Model.Talent).Eigenschaft1);
                //            EigenschaftWurfItemListe[1].Wert = Held.GetEigenschaftWert((Probe as Model.Talent).Eigenschaft2);
                //            EigenschaftWurfItemListe[2].Wert = Held.GetEigenschaftWert((Probe as Model.Talent).Eigenschaft3);

                //            // TaW setzen
                //            Probe.Fertigkeitswert = 0;
                //        }
                //    }
                //    else if (Probe is Model.Held_Talent)
                //    {
                //        EigenschaftWurfItemListe[0].Name = (Probe as Model.Held_Talent).Talent.Eigenschaft1;
                //        EigenschaftWurfItemListe[1].Name = (Probe as Model.Held_Talent).Talent.Eigenschaft2;
                //        EigenschaftWurfItemListe[2].Name = (Probe as Model.Held_Talent).Talent.Eigenschaft3;
                //        if (Held != null)
                //        {
                //            // Eigenschaften setzen
                //            EigenschaftWurfItemListe[0].Wert = Held.GetEigenschaftWert((Probe as Model.Held_Talent).Talent.Eigenschaft1);
                //            EigenschaftWurfItemListe[1].Wert = Held.GetEigenschaftWert((Probe as Model.Held_Talent).Talent.Eigenschaft2);
                //            EigenschaftWurfItemListe[2].Wert = Held.GetEigenschaftWert((Probe as Model.Held_Talent).Talent.Eigenschaft3);

                //            // TaW setzen
                //            Probe.Fertigkeitswert = (Probe as Model.Held_Talent).TaW ?? 0;
                //        }
                //    }

                //    // TODO MT: Zauber und Eigenschaften ergänzen und in Untermethoden auslagern
                //}
                //else
                //    WertCount = 1;

                OnChanged("Probe");
                OnChanged("Probenname");
                OnChanged("EigenschaftWurfItemListe");
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
                    list.Add(new EigenschaftWurfItem());
                EigenschaftWurfItemListe = list;

                OnChanged("WertCount");
            }
        }

        private List<EigenschaftWurfItem> _eigenschaftWurfItemListe = new List<EigenschaftWurfItem>();
        public List<EigenschaftWurfItem> EigenschaftWurfItemListe
        {
            get 
            {
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
            //Probe p = new Probe();
            //p.Werte = new int[EigenschaftWurfItemListe.Count];
            

            if (Probe != null)
            {
                for (int i = 0; i < Probe.Werte.Length; i++)
                    Probe.Werte[i] = EigenschaftWurfItemListe[i].Wert;

                Ergebnis = Probe.Würfeln();

                for (int i = 0; i < Ergebnis.Würfe.Length; i++)
                    EigenschaftWurfItemListe[i].Wurf = Ergebnis.Würfe[i];
            }

            // Event werfen
            if (Gewürfelt != null)
                Gewürfelt(this, new EventArgs());
        }

        #endregion

        #region //---- EVENTS ----

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
