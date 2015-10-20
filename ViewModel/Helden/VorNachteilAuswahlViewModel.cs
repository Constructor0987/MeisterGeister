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
            Beschreibung = string.Format("Bitte treffe für den Vor-/Nachteil '{0}' folgende Auswahl:\n{1}", _vorNachteil.Name, _vorNachteil.Auswahl);

            List<VorNachteilAuswahlItem> list = new List<VorNachteilAuswahlItem>();

            bool isSKT = _vorNachteil.Auswahl == "[FERTIGKEIT]" || _vorNachteil.Auswahl == "[TALENT]" || _vorNachteil.Auswahl == "[KAMPFTECHNIK]";
            Dictionary<string, double> kostenList = new Dictionary<string, double>(5);
            if (isSKT)
            { // Die Auswahl hat von der SKT abhängige Kosten
              // TODO: Kosten für DSA4.1 VorNachteile ergänzen.
              // Kosten nach DSA5

                // SKT Faktor. Sonderfall 'Waffenbegabung' beginnt bei 0
                int faktor = _vorNachteil.Name == "Waffenbegabung" ? 0 : 1;

                for (int i = 1; i < 6; i++)
                {   // Berechnet die Kosten nach Steigerungsfaktor.
                    // Erzeugt eine Liste von SKT A bis E mit jeweils den Kosten des Steigerungsfaktors.
                    kostenList.Add(Convert.ToChar(i + 64).ToString(), faktor * (_vorNachteil.Kosten ?? 0));
                    faktor++;
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
            else
            { // Auswahl Einträge aus Tabelle abrufen
                var listAuswahl = Global.ContextVorNachteil.VorNachteilAuswahlListeByKategorie(_vorNachteil.Auswahl.Trim('[', ']'));
                foreach (var item in listAuswahl)
                {
                    list.Add(new VorNachteilAuswahlItem(item.Name, item.Kosten));
                }
            }
            
            // TODO: weitere spezielle VorNachteil Auswahl-Typen wie z.B. GIFT und KRANKHEIT

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
