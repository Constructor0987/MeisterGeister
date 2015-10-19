using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Helden
{
    public class VorNachteilAuswahlViewModel : Base.ViewModelBase
    {
        public VorNachteilAuswahlViewModel(Model.VorNachteil vn)
        {
            VorNachteil = vn;
            Init();
        }

        private Model.VorNachteil _vorNachteil = null;
        public Model.VorNachteil VorNachteil
        {
            get { return _vorNachteil; }
            set
            {
                _vorNachteil = value;
                OnChanged();
            }
        }

        private void Init()
        {
            Beschreibung = string.Format("Bitte treffe für den Vor-/Nachteil '{0}' folgende Auswahl (siehe {1}):\n{2}", _vorNachteil.Name, _vorNachteil.Literatur, _vorNachteil.Auswahl);

            List<VorNachteilAuswahlItem> list = new List<VorNachteilAuswahlItem>();

            bool isSKT = _vorNachteil.Auswahl == "[FERTIGKEIT]" || _vorNachteil.Auswahl == "[TALENT]" || _vorNachteil.Auswahl == "[KAMPFTECHNIK]";
            Dictionary<string, double> kostenList = new Dictionary<string, double>(5);
            if (isSKT)
            { // Die Auswahl hat von der SKT abhängige Kosten
              // TODO: Kosten für DSA4.1 VorNachteile ergänzen.
              // Kosten nach DSA5
                if (_vorNachteil.Name == "Begabung")
                {
                    kostenList.Add("A", 6);
                    kostenList.Add("B", 12);
                    kostenList.Add("C", 18);
                    kostenList.Add("D", 24);
                    kostenList.Add("E", 30); // E-Kosten interpoliert
                }
                else if (_vorNachteil.Name == "Herausragende Fertigkeit")
                {
                    kostenList.Add("A", 2);
                    kostenList.Add("B", 4);
                    kostenList.Add("C", 6);
                    kostenList.Add("D", 8);
                    kostenList.Add("E", 10); // E-Kosten interpoliert
                }
                else if (_vorNachteil.Name == "Herausragende Kampftechnik")
                {
                    kostenList.Add("A", 4); // A-Kosten interpoliert
                    kostenList.Add("B", 8);
                    kostenList.Add("C", 12);
                    kostenList.Add("D", 16);
                    kostenList.Add("E", 20); // E-Kosten interpoliert
                }
                else if (_vorNachteil.Name == "Waffenbegabung")
                {
                    kostenList.Add("A", 0); // A-Kosten interpoliert
                    kostenList.Add("B", 10);
                    kostenList.Add("C", 20);
                    kostenList.Add("D", 30);
                    kostenList.Add("E", 40); // E-Kosten interpoliert
                }
                else if (_vorNachteil.Name == "Unfähig")
                {
                    kostenList.Add("A", -1);
                    kostenList.Add("B", -2);
                    kostenList.Add("C", -3);
                    kostenList.Add("D", -4);
                    kostenList.Add("E", -5); // E-Kosten interpoliert
                }
            }

            if (_vorNachteil.Auswahl == "[FERTIGKEIT]")
            {
                // Talente (ohne Kampftechniken), Zauber, Rituale, Liturgien
                foreach (var talent in Global.ContextHeld.TalentListe.Where(t => t.TalentgruppeID != 1).OrderBy(t => t.Name))
                    list.Add(new VorNachteilAuswahlItem(talent.Name, kostenList[talent.Steigerung]));
                // TODO: andere fertigkeiten hinzufügen
            }
            else if (_vorNachteil.Auswahl == "[TALENT]")
            {
                foreach (var talent in Global.ContextHeld.TalentListe.Where(t => t.TalentgruppeID != 1).OrderBy(t => t.Name))
                    list.Add(new VorNachteilAuswahlItem(talent.Name, kostenList[talent.Steigerung]));
            }
            else if (_vorNachteil.Auswahl == "[KAMPFTECHNIK]")
            {
                foreach (var talent in Global.ContextHeld.TalentListe.Where(t => t.TalentgruppeID == 1).OrderBy(t => t.Name))
                    list.Add(new VorNachteilAuswahlItem(talent.Name, kostenList[talent.Steigerung]));
            }
            // TODO: weitere VorNachteil Auswahl-Typen

            AuswahlListe = list;
        }

        
        private List<VorNachteilAuswahlItem> _auswahlListe = new List<VorNachteilAuswahlItem>();
        public List<VorNachteilAuswahlItem> AuswahlListe
        {
            get
            {
                return _auswahlListe;
            }
            set
            {
                _auswahlListe = value;
                OnChanged();
            }
        }

        private VorNachteilAuswahlItem _auswahl = null;
        public VorNachteilAuswahlItem Auswahl
        {
            get
            {
                return _auswahl;
            }
            set
            {
                _auswahl = value;
                OnChanged();
            }
        }

        public string Beschreibung { get; private set; }
    }
}
