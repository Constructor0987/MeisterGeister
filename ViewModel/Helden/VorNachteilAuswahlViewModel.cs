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

            bool isSKT = ((_vorNachteil.KostenFaktor ?? 0) != 0) && (_vorNachteil.Auswahl == "[FERTIGKEIT]" || _vorNachteil.Auswahl == "[TALENT]" || _vorNachteil.Auswahl == "[KAMPFTECHNIK]");
            Dictionary<string, double> kostenList = new Dictionary<string, double>(5);
            if (isSKT)
            { // Die Auswahl hat von der SKT abhängige Kosten
              // Kosten nach DSA5
                int faktor = 1;
                for (int i = 1; i < 6; i++)
                {   // Berechnet die Kosten nach Steigerungsfaktor.
                    // Erzeugt eine Liste von SKT A bis E mit jeweils den Kosten des Steigerungsfaktors.
                    kostenList.Add(Convert.ToChar(i + 64).ToString(), faktor * (_vorNachteil.KostenFaktor ?? 0));
                    faktor++;
                }
            }

            // TODO: Kosten für DSA4.1 VorNachteile ergänzen.
            double? kosten = 0;
            if (_vorNachteil.Auswahl == "[FERTIGKEIT]")
            {
                // Talente (ohne Kampftechniken), Zauber, Rituale, Liturgien
                foreach (var talent in Global.ContextHeld.TalentListe.Where(t => t.TalentgruppeID != 1).OrderBy(t => t.Name))
                {
                    if (isSKT) // variable Kosten nach SKT
                        kosten = (_vorNachteil.KostenGrund ?? 0) + kostenList[talent.Steigerung];
                    else // Fixkosten
                        kosten = _vorNachteil.KostenGrund ?? 0;
                    list.Add(new VorNachteilAuswahlItem(talent, kosten));
                }
                // TODO: andere fertigkeiten hinzufügen
            }
            else if (_vorNachteil.Auswahl == "[TALENT]")
            {
                List<Model.Talent> talentListe = null;
                if (Global.DSA5)
                    talentListe = Global.ContextHeld.TalentListe.Where(t => t.TalentgruppeID > 1).OrderBy(t => t.Name).ToList();
                else
                    // keine Sprachen & Schrifte, Meta-Talente
                    talentListe = Global.ContextHeld.TalentListe.Where(t => (t.TalentgruppeID != 0) && (t.TalentgruppeID != 7) && (t.TalentgruppeID != 8)).OrderBy(t => t.Name).ToList();
                foreach (var talent in talentListe)
                {
                    if (isSKT) // variable Kosten nach SKT
                        kosten = (_vorNachteil.KostenGrund ?? 0) + kostenList[talent.Steigerung];
                    else if (_vorNachteil.KostenGrund.GetValueOrDefault(0) == 0) // keine Kosten angegeben -> Sonderberechnung
                    { // Vorteil: Begabung für Talent
                      // Kosten: Kampf, Körper (außer Schwimmen, Sich Verstecken, Singen, Tanzen, Zechen) 6 GP, andere 4 GP, Sprache/Schrift nicht
                        if (talent.Name == "Schwimmen" || talent.Name == "Sich Verstecken" || talent.Name == "Singen" || talent.Name == "Tanzen" || talent.Name == "Zechen")
                            kosten = 4;
                        else if (talent.TalentgruppeID == 1 /*Kampf*/ || talent.TalentgruppeID == 2 /*Körper*/)
                            kosten = 6;
                        else
                            kosten = 4;
                    }
                    else // Fixkosten
                        kosten = _vorNachteil.KostenGrund ?? 0;
                    list.Add(new VorNachteilAuswahlItem(talent, kosten));
                }
            }
            else if (_vorNachteil.Auswahl == "[KAMPFTECHNIK]")
            {
                foreach (var talent in Global.ContextHeld.TalentListe.Where(t => t.TalentgruppeID == 1).OrderBy(t => t.Name))
                {
                    if (isSKT) // variable Kosten nach SKT
                        kosten = (_vorNachteil.KostenGrund ?? 0) + kostenList[talent.Steigerung];
                    else // Fixkosten
                        kosten = _vorNachteil.KostenGrund ?? 0;
                    list.Add(new VorNachteilAuswahlItem(talent, kosten));
                }
            }
            else if (_vorNachteil.Auswahl == "[RITUAL]")
            {
                // TODO: Auswahl für RITUAL
                // Kosten: 1/50 der AP Lern-Kosten als GP
            }
            else
            { // Auswahl Einträge aus Tabelle abrufen
                var listAuswahl = Global.ContextVorNachteil.VorNachteilAuswahlListeByKategorie(_vorNachteil.Auswahl.Trim('[', ']'));
                foreach (var item in listAuswahl)
                    list.Add(new VorNachteilAuswahlItem(_vorNachteil, item));
            }

            // TODO: weitere spezielle VorNachteil Auswahl-Typen wie z.B. GIFT und KRANKHEIT

            if (list.Count == 0) // keine Auswahl-Einträge gefunden, also Dummy anlegen
                list.Add(new VorNachteilAuswahlItem(_vorNachteil.Auswahl, _vorNachteil.KostenGrund, _vorNachteil.Literatur));

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
