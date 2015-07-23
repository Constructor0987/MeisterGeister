using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public class DämonenBeschwörungViewModel : BeschwörungViewModel
    {
        public DämonenBeschwörungViewModel()
        {
            magiekundeProbe = new Base.CommandBase(magiekunde, null);
            malenProbe = new Base.CommandBase(malen, null);
            editMagiekunde = new Base.CommandBase((o) => InvocatioIntegraMagiekundePunkte = 1, null);
            editMalen = new Base.CommandBase((o) => InvocatioIntegraMalenPunkte = 1, null);
            PropertyChanged += propertyChanged;
        }

        protected override List<GegnerBase> loadWesen()
        {
            return Global.ContextHeld.LoadDämonen();
        }

        private void propertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Hörner" || e.PropertyName == "WahrerName")
            {
                InvocatioIntegra = false;
                OnChanged("InvocatioIntegraMöglich");
            }
        }

        protected override void reset()
        {
            base.reset();
            InvocatioIntegra = false;
            Hörner = 0;
            AndererDämon = false;
            Affinität = false;
            Bannschwert = false;
        }

        #region Beschwören

        protected override void beschwörungMisslungen(ProbenErgebnis erg)
        {
            int wurf = View.General.ViewHelper.ShowWürfelDialog(Blutmagie == Blutmagie.Keine ? "2W6" : "3W6", "Beschwörung Misslungen");
            if (WahrerName == 0) wurf += 7;
            if (wurf <= 6)
            {
                Ergebnis = "Außer einem kalten, übel riechenden Hauch erscheint ... nichts. Die Beschwörungskosten betragen die Hälfte dessen, was für den Spruch üblich ist.";
            }
            else if (wurf <= 11)
            {
                Ergebnis = "Es erscheint ein Dämon aus derselben Domäne und von derselben Klasse (Niederer oder gehörnter Dämon) wie der angerufene, jedoch von niedrigerer Beschwörungsschwierigkeit. Existiert kein solcher Dämon, gilt die nächst höhere Auswirkung. Die Beschwörungskosten betragen die Hälfte dessen, was für den Spruch üblich ist.";
                AndererDämon = true;
            }
            else if (wurf <= 15)
            {
                Ergebnis = "Es erscheint ein Dämon aus derselben Domäne und von derselben Klasse (Niederer oder gehörnter Dämon) wie der angerufene, jedoch von höherer Beschwörungsschwierigkeit. Existiert kein solcher Dämon, gilt die nächst höhere Auswirkung. Die Beschwörungskosten betragen die Hälfte dessen, was für den Spruch üblich ist.";
                AndererDämon = true;
            }
            else if (wurf <= 19)
            {
                Ergebnis = "Es erscheint ein Dämon aus derselben Domäne, jedoch auf jeden Fall ein Gehörnter Dämon von höherer Beschwörungsschwierigkeit. Existiert kein solcher Dämon, gilt die nächst höhere Auswirkung. Wenn Sie mit den Experten-Regeln zum Schleichenden Verfall spielen, erhält der Beschwörer 1 Punkt Verfall. Die Beschwörungskosten betragen 19 AsP.";
                AndererDämon = true;
            }
            else
            {
                Ergebnis = "Es erscheint ein Gehörnter Dämon von höherer Beschwörungs-Schwierigkeit, unabhängig von der Domäne des Gerufenen. Wenn Sie mit den Experten-Regeln zum Schleichenden Verfall spielen, erhält der Beschwörer 1W6 Punkte Verfall. Die Beschwörungskosten betragen 19 AsP.";
                AndererDämon = true;
            }
        }

        protected override void beherrschungMisslungen(ProbenErgebnis erg)
        {
            int wurf = View.General.ViewHelper.ShowWürfelDialog("2W6", "Kontrolle Misslungen");
            if (erg.Ergebnis == Logic.General.ErgebnisTyp.PATZER)
            {
                wurf += Kontrollschwierigkeit;
                if (erg.Qualität > 0)
                    wurf -= erg.Qualität;
            }
            else wurf += (int)Math.Round(Kontrollschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
            if (Hörner > 0) wurf += 5;
            if (wurf <= 1)
            {
                Ergebnis = "Der Beschwörer zwingt den Dämon binnen 2 Aktionen doch noch unter seine Kontrolle und presst ihm die Erfüllung eines Dienstes ab, eventuelle weitere Dienste verfallen. Dämon und Beschwörer sind während dieser Zeit in einem Duell der Willenskraft verstrickt und können keine anderen Handlungen unternehmen. Wenn Sie mit den Experten-Regeln zum Schleichenden Verfall spielen, erhält der Beschwörer 2 Punkte Verfall.";
            }
            else if (wurf <= 5)
            {
                Ergebnis = "Der Dämon zieht sich verärgert in seine Sphäre zurück, alle noch offenen Dienste verfallen.";
            }
            else if (wurf <= 9)
            {
                Ergebnis = "Der Dämon zieht sich verärgert in seine Sphäre zurück, alle noch offenen Dienste verfallen. " +
                           "Die Beschwörung dieses speziellen Dämonen ist für den Beschwörer in Zukunft um 3 Punkte erschwert. " +
                           "(Dies lässt sich durch 20 AP wieder aufheben; Paktierer können stattdessen 20 Pakt-GP einsetzen; entstammen Dämon und Pakt nicht derselben Domäne, betragen die Kosten 50 Pakt-GP.) 2 Punkte Verfall.";
            }
            else if (wurf <= 13)
            {
                Ergebnis = "Der Dämon greift den Beschwörer eine Kampfrunde lang mit allen ihm zur Verfügung stehenden Mitteln an – auch mit Angriffen von längerfristiger Auswirkung wie z. B. Besessenheit – und verschwindet dann. Alle weiteren Dienste verfallen. Wenn Sie mit den Experten-Regeln zum Schleichenden Verfall spielen, erhält der Beschwörer 3 Punkte Verfall.";
            }
            else if (wurf <= 17)
            {
                Ergebnis = "Der Dämon greift den Beschwörer W6+3 Kampfrunden lang mit allen ihm zur Verfügung stehenden Mitteln an; W6+3 Punkte Verfall";
            }
            else if (wurf <= 21)
            {
                Ergebnis = "Der Dämon greift den Beschwörer W6+3 Kampfrunden lang mit allen ihm zur Verfügung stehenden Mitteln an, jedoch raubt jeder erfolgreiche Angriff (auch z. B. ein Furcht Einflößen zählt in diesem Sinne als Angriff) des Dämons dem Beschwörer zusätzlich einen permanenten AsP (wenn keine mehr vorhanden, dann permanente LeP); W6+3 Punkte Verfall oder eine passende Schlechte Eigenschaft im Wert von 6 GP.";
            }
            else
            {
                Ergebnis = "Der Dämon greift den Beschwörer W6+3 Kampfrunden lang mit allen ihm zur Verfügung stehenden Mitteln an, jedoch raubt jeder erfolgreiche Angriff (auch z. B. ein Furcht Einflößen zählt in diesem Sinne als Angriff) des Dämons dem Beschwörer zusätzlich einen permanenten AsP (wenn keine mehr vorhanden, dann permanente LeP); W6+3 Punkte Verfall oder eine passende Schlechte Eigenschaft im Wert von 6 GP. Jedoch können nach Meisterentscheid und bösartiger Kreativität weitere Nebeneffekte eintreten. Als Beispiele seien hier genannt: Der Dämon lässt nicht eher vom Beschwörer ab, bis er ausgetrieben wird. Der Dämon wird freigesetzt und macht auf eigene Faust Aventurien unsicher. Der Dämon reißt den Beschwörer in die Niederhöllen. Der Dämon zieht sich in den näheren Limbus zurück und wartet dort auf die nächste Beschwörung, um dann zusätzlich zur gerufenen Entität zu erscheinen. Ein mächtiger Gehörnter zwingt dem Beschwörer einen minderen Pakt auf.";
            }
        }

        #endregion

        #region InvocatioIntegra


        private Base.CommandBase magiekundeProbe;
        public Base.CommandBase MagiekundeProbe
        {
            get { return magiekundeProbe; }
        }

        private Base.CommandBase malenProbe;
        public Base.CommandBase MalenProbe
        {
            get { return malenProbe; }
        }

        private Base.CommandBase editMagiekunde;
        public Base.CommandBase EditMagiekunde
        {
            get { return editMagiekunde; }
            set { editMagiekunde = value; }
        }

        private Base.CommandBase editMalen;
        public Base.CommandBase EditMalen
        {
            get { return editMalen; }
            set { editMalen = value; }
        }




        private void magiekunde(object obj)
        {
            //TODO: Talentspezialisierung Magiekunde: Dämonologie beachten
            int taW;
            Held_Talent ht = Global.SelectedHeld.GetHeldTalent("Magiekunde", false, out taW);
            ht.Talent.Fertigkeitswert = taW;
            ht.Talent.Modifikator = (int)Math.Round(Beschwörungsschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
            ProbenErgebnis erg = ShowProbeDialog(ht.Talent, ht.Held);
            InvocatioIntegraMagiekundePunkte = erg.Gelungen ? erg.Übrig : 0;

        }
        private void malen(object obj)
        {
            int taW;
            Held_Talent ht = Global.SelectedHeld.GetHeldTalent("Malen/Zeichnen", false, out taW);
            ht.Talent.Fertigkeitswert = taW;
            ht.Talent.Modifikator = (int)Math.Round(Beschwörungsschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
            ProbenErgebnis erg = ShowProbeDialog(ht.Talent, ht.Held);
            InvocatioIntegraMalenPunkte = erg.Gelungen ? erg.Übrig : 0;
        }

        public bool InvocatioIntegraMöglich
        {
            //TODO: Prüfen ob SF vorhanden
            get { return Hörner > 0 && WahrerName > 0; }
        }


        private bool invocatioIntegra;
        public bool InvocatioIntegra
        {
            get { return invocatioIntegra; }
            set
            {
                Set(ref invocatioIntegra, value);
                OnChanged("ZauberWert");
                OnChanged("KontrollWert");
                OnChanged("InvocatioIntegraMagiekundeRufMod");
                OnChanged("InvocatioIntegraMalenRufMod");
                OnChangedSum();
            }
        }

        private bool invocatioIntegraVorbereiten;
        public bool InvocatioIntegraVorbereiten
        {
            get { return invocatioIntegraVorbereiten; }
            set
            {
                Set(ref invocatioIntegraVorbereiten, value);
                OnChanged("InvocatioIntegraMagiekundeRufMod");
                OnChanged("InvocatioIntegraMalenRufMod");
                OnChangedSum();
            }
        }

        private int invocatioIntegraMagiekundePunkte;
        public int InvocatioIntegraMagiekundePunkte
        {
            get { return invocatioIntegraMagiekundePunkte; }
            set
            {
                Set(ref invocatioIntegraMagiekundePunkte, value);
                OnChanged("InvocatioIntegraMagiekundeRufMod");
                OnChangedSum();
            }
        }

        private int invocatioIntegraMalenPunkte;
        public int InvocatioIntegraMalenPunkte
        {
            get { return invocatioIntegraMalenPunkte; }
            set
            {
                Set(ref invocatioIntegraMalenPunkte, value);
                OnChanged("InvocatioIntegraMalenRufMod");
                OnChangedSum();
            }
        }

        public int InvocatioIntegraMagiekundeRufMod
        {
            get
            {
                if (!InvocatioIntegra || !InvocatioIntegraVorbereiten)
                    return 0;
                else if (InvocatioIntegraMagiekundePunkte > 0)
                    return (int)Math.Round(-InvocatioIntegraMagiekundePunkte / 2.0, MidpointRounding.AwayFromZero);
                return 3;
            }
        }

        public int InvocatioIntegraMalenRufMod
        {
            get
            {
                if (!InvocatioIntegra || !InvocatioIntegraVorbereiten)
                    return 0;
                else if (InvocatioIntegraMalenPunkte > 0)
                    return (int)Math.Round(-InvocatioIntegraMalenPunkte / 2.0, MidpointRounding.AwayFromZero);
                else return 3;
            }
        }


        #endregion

        #region Properties

        private bool bannschwert;
        public bool Bannschwert
        {
            get { return bannschwert; }
            set
            {
                bannschwert = value;
                OnInputChanged();
            }
        }

        public int BannschwertRufMod
        {
            get { return Bannschwert ? -1 : 0; }
        }
        public int BannschwertHerrschMod
        {
            get { return Bannschwert ? -1 : 0; }
        }

        private bool affinität;
        public bool Affinität
        {
            get { return affinität; }
            set
            {
                affinität = value;
                OnInputChanged();
            }
        }

        public int AffinitätHerrschMod
        {
            get { return Affinität ? -3 : 0; }
        }

        private bool andererDämon = false;
        public bool AndererDämon
        {
            get { return andererDämon; }
            set
            {
                Set(ref andererDämon, value);
                OnChangedSum();
            }
        }

        private int hörner = 0;
        public int Hörner
        {
            get { return hörner; }
            set
            {
                Set(ref hörner, value);
                OnChanged("InvocatioIntegraMöglich");
                Zauber = Hörner == 0 ? "Invocatio minor" : "Invocatio maior";
            }
        }

        public int BlutmagieBonus
        {
            get
            {
                if (InvocatioIntegra)
                {
                    switch (Blutmagie)
                    {
                        case Beschwörung.Blutmagie.Tieropfer:
                            return 3;
                        case Beschwörung.Blutmagie.IntelligentesWesen:
                            return 7;
                    }
                }
                return 0;
            }
        }

        public override Blutmagie Blutmagie
        {
            get
            {
                return base.Blutmagie;
            }
            set
            {
                base.Blutmagie = value;
                OnChanged("ZauberWert");
                OnChanged("KontrollWert");
            }
        }

        public override int WahrerNameRufMod
        {
            get
            {
                return WahrerName == 0 ? 7 : -WahrerName;
            }
        }

        #endregion

        public override int GesamtRufMod
        {
            get
            {
                return base.GesamtRufMod
                    +BannschwertRufMod
                    + InvocatioIntegraMagiekundeRufMod
                    + InvocatioIntegraMalenRufMod;
            }
        }

        public override int GesamtHerrschMod
        {
            get
            {
                return base.GesamtHerrschMod
                    + (AndererDämon ? Kontrollschwierigkeit : 0)
                    + AffinitätHerrschMod
                    +BannschwertHerrschMod;
            }
        }

        public override int ZauberWert
        {
            get
            {
                return base.ZauberWert + (InvocatioIntegra ? 7 + BlutmagieBonus : 0);
            }
        }

        public override string KontrollFormel
        {
            get { return "(MU + MU + KL + CH + ZfW) / 5"; }
        }

        protected override int calcKontrollWert()
        {
            return (int)Math.Round((held.Mut * 2 + held.Klugheit + held.Charisma + ZauberWert) / 5.0, MidpointRounding.AwayFromZero);
        }
    }
}
