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

        [DependentProperty("ProbeItemListe")]
        public int WertCount
        {
            get { return EigenschaftWurfItemListe.Count; }
            set
            {
                if (value < 0)
                    return;

                EigenschaftWurfItemListe = new List<EigenschaftWurfItem>(value);
                for (int i = 0; i < value; i++)
                    EigenschaftWurfItemListe.Add(new EigenschaftWurfItem());

                OnChanged("WertCount");
            }
        }

        private List<EigenschaftWurfItem> _eigenschaftWurfItemListe = new List<EigenschaftWurfItem>();
        public List<EigenschaftWurfItem> EigenschaftWurfItemListe
        {
            get { return _eigenschaftWurfItemListe; }
            set
            {
                _eigenschaftWurfItemListe = value;
                OnChanged("ProbeItemListe");
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public ProbeControlViewModel()
        {
            // TODO MT: Testdaten
            WertCount = MeisterGeister.Logic.General.Würfel.Wurf(4);
            //ProbeItems = new List<ProbeItem>() { 
            //    new ProbeItem() { Wert = 18, Name = "MU" },
            //    new ProbeItem() { Wert = 8, Name = "KL"}, 
            //    new ProbeItem() { Wert = 20, Name = "GE"}};

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
            Probe p = new Probe();
            p.Werte = new int[EigenschaftWurfItemListe.Count];
            for (int i = 0; i < p.Werte.Length; i++)
                p.Werte[i] = EigenschaftWurfItemListe[i].Wert;

            Ergebnis = p.Würfeln();

            for (int i = 0; i < Ergebnis.Würfe.Length; i++)
                EigenschaftWurfItemListe[i].Wurf = Ergebnis.Würfe[i];
            
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
    }

    #endregion
}
