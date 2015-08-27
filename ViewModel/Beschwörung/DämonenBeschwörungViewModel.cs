using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Alchimie.Logic;
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
        private const string AFFINITÄT = "Affinität zu Dämonen";

        public DämonenBeschwörungViewModel()
        {
            magiekundeProbe = new Base.CommandBase((o) => magiekunde(), null);
            malenProbe = new Base.CommandBase((o) => malen(), null);
            editMagiekunde = new Base.CommandBase((o) => invocatioMagiekunde.Value = 1, null);
            editMalen = new Base.CommandBase((o) => invocatioMalen.Value = 1, null);
        }

        protected override void checkHeld()
        {
            base.checkHeld();
            if (Held != null)
                affinität.Value = Held.HatVorNachteil(AFFINITÄT);
            else
                affinität.Value = false;
        }

        protected override void checkBeschworenesWesen()
        {
            base.checkBeschworenesWesen();
            if (BeschworenesWesen != null)
            {
                Hörner = BeschworenesWesen.Beschwörbares.Dämon.Hörner ?? 0;
            }
        }

        protected override List<GegnerBase> loadWesen()
        {
            return Global.ContextHeld.LoadDämonen();
        }

        protected override void reset()
        {
            Hörner = 0;
            AndererDämon = false;

            base.reset();

            BeschwörungMisslungenErgebnis = BeherrschungMisslungenErgebnis = String.Empty;
            WürfleBeschwörungMisslungen = new Base.CommandBase((o) => würfleBeschwörungMisslungenEffekt(), (o) => Status == BeschwörungsStatus.BeschwörungMisslungen);
            WürfleBeherrschungMisslungen = null;
        }

        #region Beschwören

        private Base.CommandBase würfleBeschwörungMisslungen;
        public Base.CommandBase WürfleBeschwörungMisslungen
        {
            get { return würfleBeschwörungMisslungen; }
            private set { Set(ref würfleBeschwörungMisslungen, value); }
        }


        private void würfleBeschwörungMisslungenEffekt()
        {
            GegnerBase wesen;
            BeschwörungMisslungenErgebnis = String.Empty;
            int wurf = 0;
            if (name.Value == 0) wurf += 7;
            wurf += View.General.ViewHelper.ShowWürfelDialog(blutmagie.Value1 ? "3W6" : "2W6", "Beschwörung Misslungen");
            //Sollte beim Einsatz von Zauberkreide oder Beschwörungskerzen der Qualität F die Beschwörung misslingen besteht eine 50%ige Wahrscheinlichkeit einen stärkeren Dämon zu rufen
            if ((kerzen.Value1 && kerzen.Value2 == Qualität.F) || (kreide.Value1 && kreide.Value2 == Qualität.F))
            {
                BeschwörungMisslungenErgebnis = "Aufgrund von Zauberkreide/Beschwörungskerzen der Qualität F besteht eine 50%ige Wahrscheinlichkeit dass ein stärkerer Dämon erscheint. ";
                //Wenn eh schon ein stärkerer Dämon kommt machen wir nix
                if (wurf <= 11)
                {
                    if (Würfel.Wurf(2) == 1)
                    {
                        wurf = 12;
                        BeschwörungMisslungenErgebnis += "Der Beschwörer hat Pech. Dieser Effekt tritt auch ein." + Environment.NewLine;
                    }
                    else
                    {
                        BeschwörungMisslungenErgebnis += "Der Beschwörer hat aber diesmal noch Glück gehabt." + Environment.NewLine;
                    }
                }
                else
                {
                    BeschwörungMisslungenErgebnis += "Dies ist jedoch nicht relevant da dies laut Zufallswurf sowieso schon eintritt." + Environment.NewLine;
                }
            }
            if (wurf <= 6)
            {
                wesen = null;
                BeschwörungMisslungenErgebnis += "Es erscheint kein Dämon. Die Beschwörungskosten betragen nur die Hälfte des Üblichen.";
                //Button deaktivieren
                WürfleBeschwörungMisslungen = new Base.CommandBase((o) => { }, (o) => false);
            }
            else if (wurf <= 11 && (wesen = Global.ContextHeld.GetLeichtererDämon(BeschworenesWesen)) != null)
            {
                BeschwörungMisslungenErgebnis += "Ein anderer Dämon aus derselben Domäne und von derselben Klasse (niederer oder gehörnter Dämon) wie der Angerufene erscheint, jedoch von niedrigerer Beschwörungsschwierigkeit. Die Beschwörungskosten betragen nur die Hälfte des Üblichen.";
                Status = BeschwörungsStatus.Beherrschen;
                AndererDämon = true;
            }
            else if (wurf <= 15 && (wesen = Global.ContextHeld.GetSchwerererDämon(BeschworenesWesen)) != null)
            {
                BeschwörungMisslungenErgebnis += "Ein anderer Dämon aus derselben Domäne und von derselben Klasse (niederer oder gehörnter Dämon) wie der Angerufene erscheint, jedoch von höherer Beschwörungsschwierigkeit. Die Beschwörungskosten betragen nur die Hälfte des Üblichen.";
                Status = BeschwörungsStatus.Beherrschen;
                AndererDämon = true;
            }
            else if (wurf <= 19 && (wesen = Global.ContextHeld.GetGehörntererDämonAusDomäne(BeschworenesWesen)) != null)
            {
                BeschwörungMisslungenErgebnis += "Ein gehörnter Dämon aus derselben Domäne, jedoch von höherer Beschwörungsschwierigkeit erscheint. 1 Punkt schleichender Verfall.";
                Status = BeschwörungsStatus.Beherrschen;
                AndererDämon = true;
            }
            else
            {
                wesen = Global.ContextHeld.GetGehörntererDämon(BeschworenesWesen);
                BeschwörungMisslungenErgebnis += "Ein gehörnter Dämon von höherer Beschwörungsschwierigkeit erscheint. 1W6 Punkte schleichender Verfall.";
                Status = BeschwörungsStatus.Beherrschen;
                AndererDämon = true;
            }
            BeschworenesWesen = wesen;
        }

        protected override void beherrschungMisslungen(ProbenErgebnis erg)
        {
            base.beherrschungMisslungen(erg);
            WürfleBeherrschungMisslungen = new Base.CommandBase((o) => würfleBeherrschungMisslungenEffekt(erg), (o) => Status == BeschwörungsStatus.BeherrschungMisslungen);
        }

        private Base.CommandBase würfleBeherrschungMisslungen = null;
        public Base.CommandBase WürfleBeherrschungMisslungen
        {
            get { return würfleBeherrschungMisslungen; }
            private set { Set(ref würfleBeherrschungMisslungen, value); }
        }

        private void würfleBeherrschungMisslungenEffekt(ProbenErgebnis erg)
        {
            int wurf = 0;
            if (erg.Ergebnis == Logic.General.ErgebnisTyp.PATZER)
            {
                wurf += schwierigkeit.KontrollMod;
                if (erg.Qualität > 0)
                    wurf -= erg.Qualität;
            }
            else wurf += (int)Math.Round(schwierigkeit.Value2 / 2.0, MidpointRounding.AwayFromZero);
            if (Hörner > 0) wurf += 5;
            wurf += View.General.ViewHelper.ShowWürfelDialog("2W6", "Kontrolle Misslungen");
            if (wurf <= 1)
            {
                BeherrschungMisslungenErgebnis = "Dämon und Beschwörer sind 2 Aktionen lang in einem Duell der Willenskraft verstrickt. Dem Beschwörer gelingt es dem Dämon die Erfüllung eines Dienstes abzuringen. Evtl. weitere Dienste verfallen. 2 Punkte schleichender Verfall.";
            }
            else if (wurf <= 5)
            {
                BeherrschungMisslungenErgebnis = "Der Dämon zieht sich verärgert in die Niederhöllen zurück. Alle Dienste verfallen.";
            }
            else if (wurf <= 9)
            {
                BeherrschungMisslungenErgebnis = "Der Dämon zieht sich verärgert in die Niederhöllen zurück. Alle Dienste verfallen. " +
                           "Die Beschwörung dieses speziellen Dämonen ist für den Beschwörer in Zukunft um 3 Punkte erschwert. " +
                           "(Dies lässt sich durch 20 AP wieder aufheben; Paktierer können stattdessen 20 Pakt-GP einsetzen; entstammen Dämon und Pakt nicht derselben Domäne, betragen die Kosten 50 Pakt-GP.) 2 Punkte schleichender Verfall.";
            }
            else if (wurf <= 13)
            {
                BeherrschungMisslungenErgebnis = "Der Dämon greift den Beschwörer eine Kampfrunde lang mit allen ihm zur Verfügung stehenden Mitteln an und verschwindet dann. Alle weiteren Dienste verfallen. 3 Punkte schleichender Verfall.";
            }
            else if (wurf <= 17)
            {
                BeherrschungMisslungenErgebnis = "Der Dämon greift den Beschwörer W6+3 Kampfrunden lang mit allen ihm zur Verfügung stehenden Mitteln an. W6+3 Punkte schleichender Verfall.";
            }
            else if (wurf <= 21)
            {
                BeherrschungMisslungenErgebnis = "Der Dämon greift den Beschwörer W6+3 Kampfrunden lang mit allen ihm zur Verfügung stehenden Mitteln an, jedoch raubt jeder erfolgreiche Angriff des Dämons dem Beschwörer zusätzlich einen permanenten AsP (wenn keine mehr vorhanden, dann permanente LeP). W6+3 Punkte schleichender Verfall oder eine passende schlechte Eigenschaft im Wert von 6 GP.";
            }
            else
            {
                BeherrschungMisslungenErgebnis = "Der Dämon greift den Beschwörer W6+3 Kampfrunden lang mit allen ihm zur Verfügung stehenden Mitteln an, jedoch raubt jeder erfolgreiche Angriff des Dämons dem Beschwörer zusätzlich einen permanenten AsP (wenn keine mehr vorhanden, dann permanente LeP). W6+3 Punkte schleichender Verfall oder eine passende schlechte Eigenschaft im Wert von 6 GP. Jedoch können nach Meisterentscheid und bösartiger Kreativität weitere Nebeneffekte eintreten. Als Beispiele seien hier genannt: Der Dämon lässt nicht eher vom Beschwörer ab, bis er ausgetrieben wird. Der Dämon wird freigesetzt und macht auf eigene Faust Aventurien unsicher. Der Dämon reißt den Beschwörer in die Niederhöllen. Der Dämon zieht sich in den näheren Limbus zurück und wartet dort auf die nächste Beschwörung, um dann zusätzlich zur gerufenen Entität zu erscheinen. Ein mächtiger Gehörnter zwingt dem Beschwörer einen minderen Pakt auf.";
            }
            //Command wird deaktiviert
            WürfleBeherrschungMisslungen = new Base.CommandBase((o) => { }, (o) => false);
        }

        public override BeschwörungsStatus Status
        {
            get
            {
                return base.Status;
            }
            protected set
            {
                base.Status = value;
                //Hier updaten wir die CanExecute-Eigenschaft der Commands
                if (WürfleBeschwörungMisslungen != null)
                    WürfleBeschwörungMisslungen.Invalidate();
                if (WürfleBeherrschungMisslungen != null)
                    WürfleBeherrschungMisslungen.Invalidate();
            }
        }

        #endregion

        #region Proben

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


        private void magiekunde()
        {
            int taW;
            Held_Talent ht = Global.SelectedHeld.GetHeldTalent("Magiekunde", false, out taW);
            ht.Talent.Fertigkeitswert = taW;
            ht.Talent.Modifikator = div(schwierigkeit.Value1, 2);
            ProbenErgebnis erg = ht.Held.TalentProbe(ht.Talent, ht.Talent.Modifikator, "Dämonologie");
            invocatioMagiekunde.Value = erg.Gelungen ? erg.Übrig : 0;
        }
        private void malen()
        {
            int taW;
            Held_Talent ht = Global.SelectedHeld.GetHeldTalent("Malen/Zeichnen", false, out taW);
            ht.Talent.Fertigkeitswert = taW;
            ht.Talent.Modifikator = div(schwierigkeit.Value1, 2);
            ProbenErgebnis erg = ShowProbeDialog(ht.Talent, ht.Held);
            invocatioMalen.Value = erg.Gelungen ? erg.Übrig : 0;
        }

        #endregion

        #region Properties

        private const string MOD_BANNSCHWERT = "Bannschwert";
        private const string MOD_AFFINITÄT = "Affinität";
        private const string MOD_PAKTIERER = "Paktierer";
        private const string MOD_INVOCATIO_INTEGRA = "InvocatioIntegra";
        private const string MOD_INVOCATIO_INTEGRA_MALEN = "InvocatioIntegraMalen";
        private const string MOD_INVOCATIO_INTEGRA_MAGIEKUNDE = "InvocatioIntegraMagiekunde";

        private BeschwörungsModifikator<bool> bannschwert;
        private BeschwörungsModifikator<bool> affinität;
        private BeschwörungsModifikator<int> paktierer;
        private BeschwörungsModifikator<bool, bool> invocatioIntegra;
        private new BeschwörungsModifikator<bool, Opfer> blutmagie;
        private new BeschwörungsModifikator<int, int> befehl;
        private BeschwörungsModifikator<int> invocatioMalen;
        private BeschwörungsModifikator<int> invocatioMagiekunde;


        protected override void addMods()
        {
            //Bei der Dämonenbeschwörung entfällt die materielle Komponente
            Mods.Remove(MOD_MATERIAL);

            //Beschwörungskerzen und -kreide 
            Mods.Add(MOD_KERZEN, kerzen);
            Mods.Add(MOD_KREIDE, kreide);

            //Bei der Dämonenbeschwörung muss der Beschwörer die Dienstkosten selbst tragen
            Mods.Remove(MOD_BEFEHL);
            befehl = new BeschwörungsModifikator<int, int>();
            befehl.GetKontrollMod = () => befehl.Value1;
            befehl.GetKostenMod = () => befehl.Value2;
            Mods.Add(MOD_BEFEHL, befehl);
            befehl.PropertyChanged += (s, e) => bezahlung.Invalidate();

            //Die Zusätzliche Bezahlung bezieht sich auf die Dienstkosten
            bezahlung.GetKostenMod = () => (int)Math.Round(-bezahlung.Value * 0.2 * befehl.KostenMod, MidpointRounding.AwayFromZero);
            Mods.Add(MOD_BEZAHLUNG, bezahlung);

            //Wenn ein anderer Dämon erscheint als der gerufene wird die Kontrollschwierigkeit verdoppelt
            schwierigkeit.GetKontrollMod = () => schwierigkeit.Value2 * (AndererDämon ? 2 : 1);

            //Ein Bannschwert erleichtert Anrufung und Kontrolle um 1 Punkt
            bannschwert = new BeschwörungsModifikator<bool>();
            bannschwert.GetAnrufungsMod = bannschwert.GetKontrollMod = () => bannschwert.Value ? -1 : 0;
            Mods.Add(MOD_BANNSCHWERT, bannschwert);

            //Affinität zu Dämonen erleichtert die Kontrolle um 3
            affinität = new BeschwörungsModifikator<bool>();
            affinität.GetKontrollMod = () => affinität.Value ? -3 : 0;
            Mods.Add(MOD_AFFINITÄT, affinität);

            //Für einen Paktierer der passenden Domäne ist Anrufung und Kontrolle um seinen Kreis der Verdammnis erleichtert
            //Die Kontrolle ist zusätzlich um 3 erleichtert
            paktierer = new BeschwörungsModifikator<int>();
            paktierer.GetAnrufungsMod = () => -paktierer.Value;
            paktierer.GetKontrollMod = () => (paktierer.Value > 0) ? -paktierer.Value - 3 : 0;
            Mods.Add(MOD_PAKTIERER, paktierer);

            //Ohne Wahrer Name ist die Anrufung von Dämonen um 7 Punkte erschwert
            Func<int> defaultMod = name.GetAnrufungsMod;
            name.GetAnrufungsMod = () => (name.Value == 0) ? 7 : defaultMod();

            invocatioIntegra = new BeschwörungsModifikator<bool, bool>();
            //Invocatio Integra erhöht den effektiven ZfW um 7
            invocatioIntegra.GetZauberMod = () => invocatioIntegra.Value1 ? 7 : 0;
            Mods.Add(MOD_INVOCATIO_INTEGRA, invocatioIntegra);

            //Blutmagie erschwert die Kontrolle, und erhöht den ZfW wenn InvocatioIntegra aktiv ist
            blutmagie = new BeschwörungsModifikator<bool, Opfer>();
            blutmagie.GetKontrollMod = () => blutmagie.Value1 ? 2 : 0;
            blutmagie.GetZauberMod = () =>
            {
                if (!invocatioIntegra.Value1 || !blutmagie.Value1) return 0;
                else if (blutmagie.Value2 == Opfer.Tieropfer) return 3;
                else return 7;
            };
            Mods[MOD_BLUTMAGIE] = blutmagie;

            //Malen bringt Bonuspunkte auf die Anrufung wenn InvocatioIntegra mit vorbereitung aktiviert ist
            invocatioMalen = new BeschwörungsModifikator<int>();
            invocatioMalen.GetAnrufungsMod = () => invocatioProbeMod(invocatioMalen.Value);
            Mods.Add(MOD_INVOCATIO_INTEGRA_MALEN, invocatioMalen);

            //Magiekunde bringt Bonuspunkte auf die Anrufung wenn InvocatioIntegra mit vorbereitung aktiviert ist
            invocatioMagiekunde = new BeschwörungsModifikator<int>();
            invocatioMagiekunde.GetAnrufungsMod = () => invocatioProbeMod(invocatioMagiekunde.Value);
            Mods.Add(MOD_INVOCATIO_INTEGRA_MAGIEKUNDE, invocatioMagiekunde);

            //InvocatioIntegra beeinflusst sowohl Blutmagie, als auch die Malen-/Magiekundeprobe
            invocatioIntegra.PropertyChanged += (s, e) => { blutmagie.Invalidate(); invocatioMalen.Invalidate(); invocatioMagiekunde.Invalidate(); };

            //Wenn sich der Wahre Name auf 0 ändert kann Invocatio Integra nicht durchgeführt werden
            name.PropertyChanged += (s, e) => { if (name.Value == 0) invocatioIntegra.Value1 = false; };
        }

        private int invocatioProbeMod(int value)
        {
            if (invocatioIntegra.Value1 && invocatioIntegra.Value2)
            {
                if (value == 0) return 3;
                else return -div(value, 2);
            }
            else return 0;
        }

        private bool andererDämon = false;
        public bool AndererDämon
        {
            get { return andererDämon; }
            set
            {
                Set(ref andererDämon, value);
                schwierigkeit.Invalidate();
            }
        }

        private int hörner = 0;
        public int Hörner
        {
            get { return hörner; }
            set
            {
                Set(ref hörner, value);
                //Invocatio Integra ist nur bei gehörnten Dämonen möglich
                if (hörner == 0)
                    invocatioIntegra.Value1 = false;
                //Der Zauber für die Beschwörung ist bei gehörnten Dämonen ein anderer
                if (Hörner == 0)
                {
                    Zauber = "Invocatio minor";
                    ZauberKosten = 11;
                }
                else
                {
                    Zauber = "Invocatio maior";
                    ZauberKosten = 19;
                }
            }
        }

        private string beschwörungMisslungenErgebnis;
        public string BeschwörungMisslungenErgebnis
        {
            get { return beschwörungMisslungenErgebnis; }
            set { Set(ref beschwörungMisslungenErgebnis, value); }
        }

        private string beherrschungMisslungenErgebnis;
        public string BeherrschungMisslungenErgebnis
        {
            get { return beherrschungMisslungenErgebnis; }
            set { Set(ref beherrschungMisslungenErgebnis, value); }
        }

        public override string KontrollFormel
        {
            get { return "(MU + MU + KL + CH + ZfW) / 5"; }
        }

        protected override int calcKontrollWert()
        {
            return div(Held.Mut * 2 + Held.Klugheit + Held.Charisma + ZauberWert, 5);
        }

        #endregion
    }
    public enum Opfer
    {
        [Description("Tieropfer")]
        Tieropfer = 0,
        [Description("Opferung eines intelligenten Wesens")]
        IntelligentesWesen
    }
}
