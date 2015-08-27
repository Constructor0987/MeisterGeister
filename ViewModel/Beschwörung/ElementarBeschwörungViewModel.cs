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
    public class ElementarBeschwörungViewModel : BeschwörungViewModel
    {
        #region Mods
        private const string MERKMAL = "Merkmalskenntnis (Elementar ({0}))";
        private const string BEGABUNG = "Begabung für Merkmal (Elementar ({0}))";
        private const string ELEMENT_AURA = "Elementarharmonisierte Aura ({0}/{1})";
        private const string VERHÜLLTE_AURA = "Verhüllte Aura";
        private const string AFFINITÄT = "Affinität zu Elementaren";
        private const string MERKMAL_DÄMONISCH = "Merkmalskenntnis (Dämonisch";
        private const string BEGABUNG_DÄMONISCH = "Begabung für Merkmal (Dämonisch";

        private const string MOD_WESEN = "Wesen";
        private const string MOD_MENGE = "Menge";
        private const string MOD_ELEMENT = "Element";
        private const string MOD_GEGENELEMENT = "Gegenelement";
        private const string MOD_DÄMONISCH = "Dämonisch";
        private const string MOD_DÄMON_GERUFEN = "DämonGerufen";
        private const string MOD_PAKTIERER = "Paktierer";
        private const string MOD_AFFINITÄT = "Affinität";
        private const string MOD_VERHÜLLTE_AURA = "VerhüllteAura";
        private const string MOD_SCHWACHE_AUSSTRAHLUNG = "SchwacheAusstrahlung";
        private const string MOD_STIGMA = "Stigma";

        private BeschwörungsModifikator<Element, ElementarWesen> wesen;
        private BeschwörungsModifikator<bool> menge;
        private BeschwörungsModifikator<bool, bool> elementMod, gegenelementMod;
        private BeschwörungsModifikator<int> dämonisch;
        private BeschwörungsModifikator<bool> dämonGerufen, paktierer;
        private BeschwörungsModifikator<bool> affinität;
        private BeschwörungsModifikator<bool> verhüllteAura;
        private BeschwörungsModifikator<int> schwacheAusstrahlung;
        private BeschwörungsModifikator<int> stigma;

        protected override void addMods()
        {
            wesen = new BeschwörungsModifikator<Element, ElementarWesen>();
            wesen.GetKostenMod = () =>
            {
                switch (wesen.Value2)
                {
                    case ElementarWesen.Geist:
                        return 12;
                    case ElementarWesen.Dschinn:
                        return 30;
                    case ElementarWesen.Meister:
                        return 48;
                    default:
                        return 0;
                }
            };
            wesen.PropertyChanged += (s, e) =>checkHeldElement();
            Mods.Add(MOD_WESEN, wesen);


            //Mit Blutmagie ist die Kontrolle von Elementaren um 12 erschwert
            blutmagie.GetKontrollMod = () => blutmagie.Value ? 12 : 0;

            //10-fache Menge gibt Erleichterung von 2 auf die Anrufung
            menge = new BeschwörungsModifikator<bool>();
            menge.GetAnrufungsMod = () => menge.Value ? -2 : 0;
            Mods.Add(MOD_MENGE, menge);

            //Begabung und Merkmalskenntnis des entsprechenden Elements erleichtert alles
            elementMod = new BeschwörungsModifikator<bool, bool>();
            elementMod.GetAnrufungsMod = () => (elementMod.Value1 ? -2 : 0) + (elementMod.Value2 ? -2 : 0);
            elementMod.GetKontrollMod = () => (elementMod.Value1 ? -2 : 0) + (elementMod.Value2 ? -2 : 0);
            Mods.Add(MOD_ELEMENT, elementMod);

            //Begabung und Merkmalskenntnis des Gegenelements erschwert alles
            gegenelementMod = new BeschwörungsModifikator<bool, bool>();
            gegenelementMod.GetAnrufungsMod = () => ElementarharmonisierteAura ? 0 : (gegenelementMod.Value1 ? 4 : 0) + (gegenelementMod.Value2 ? 4 : 0);
            gegenelementMod.GetKontrollMod = () => ElementarharmonisierteAura ? 0 : (gegenelementMod.Value1 ? 2 : 0) + (gegenelementMod.Value2 ? 2 : 0);
            Mods.Add(MOD_GEGENELEMENT, gegenelementMod);

            //Dämonische Begabungen und Merkmale erschwerden die Probe
            dämonisch = new BeschwörungsModifikator<int>();
            dämonisch.GetAnrufungsMod = () => dämonisch.Value * 2;
            dämonisch.GetKontrollMod = () => dämonisch.Value * 4;
            Mods.Add(MOD_DÄMONISCH, dämonisch);

            //Wer kürzlich einen Dämon gerufen hat dessen Kontrollprobe ist erschwert
            dämonGerufen = new BeschwörungsModifikator<bool>();
            dämonGerufen.GetKontrollMod = () => dämonGerufen.Value ? 4 : 0;
            Mods.Add(MOD_DÄMON_GERUFEN, dämonGerufen);

            //Ein Paktierer
            paktierer = new BeschwörungsModifikator<bool>();
            paktierer.GetAnrufungsMod = () => paktierer.Value ? 6 : 0;
            paktierer.GetKontrollMod = () => paktierer.Value ? 9 : 0;
            Mods.Add(MOD_PAKTIERER, paktierer);

            //Affinität zu Elementaren erleichtert die Kontrolle um 3
            affinität = new BeschwörungsModifikator<bool>();
            affinität.GetKontrollMod = () => affinität.Value ? -3 : 0;
            Mods.Add(MOD_AFFINITÄT, affinität);

            //Verhüllte Aura erschwert Kontrolle um 1
            verhüllteAura = new BeschwörungsModifikator<bool>();
            verhüllteAura.GetKontrollMod = () => verhüllteAura.Value ? 1 : 0;
            Mods.Add(MOD_VERHÜLLTE_AURA, verhüllteAura);

            //Schwache Ausstrahlung erschwert die Kontrolle
            schwacheAusstrahlung = new BeschwörungsModifikator<int>();
            schwacheAusstrahlung.GetKontrollMod = () => schwacheAusstrahlung.Value;
            Mods.Add(MOD_SCHWACHE_AUSSTRAHLUNG, schwacheAusstrahlung);

            //Stigmas erschwerden die Kontrolle
            stigma = new BeschwörungsModifikator<int>();
            stigma.GetKontrollMod = () => div(stigma.Value, 4);
            Mods.Add(MOD_STIGMA, stigma);
        }
        #endregion

        protected override void checkHeld()
        {
            base.checkHeld();
            checkHeldElement();
            if (Held != null)
            {
                affinität.Value = Held.HatVorNachteil(AFFINITÄT);
                verhüllteAura.Value = Held.HatSonderfertigkeitUndVoraussetzungen(VERHÜLLTE_AURA);
            }
            else
            {
                affinität.Value = false;
                verhüllteAura.Value = false;
            }
        }

        private void checkHeldElement()
        {
            switch (wesen.Value2)
            {
                case ElementarWesen.Geist:
                    Zauber = "Elementarer Diener";
                    break;
                case ElementarWesen.Dschinn:
                    Zauber = "Dschinnenruf";
                    break;
                case ElementarWesen.Meister:
                    Zauber = "Meister der Elemente";
                    break;
            }

            if (Held != null)
            {
                elementMod.Value1 = Held.HatSonderfertigkeitUndVoraussetzungen(String.Format(MERKMAL, wesen.Value1));
                gegenelementMod.Value1 = Held.HatSonderfertigkeitUndVoraussetzungen(String.Format(MERKMAL, ~wesen.Value1));

                elementMod.Value2 = Held.HatVorNachteil(String.Format(BEGABUNG, wesen.Value1));
                gegenelementMod.Value2 = Held.HatVorNachteil(String.Format(BEGABUNG, ~wesen.Value1));

                ElementarharmonisierteAura = Held.HatSonderfertigkeitUndVoraussetzungen(String.Format(ELEMENT_AURA, wesen.Value1, ~wesen.Value1))
                                          || Held.HatSonderfertigkeitUndVoraussetzungen(String.Format(ELEMENT_AURA, ~wesen.Value1, wesen.Value1));

                dämonisch.Value = Held.Sonderfertigkeiten.Keys.Where((sf) => sf.Name.StartsWith(MERKMAL_DÄMONISCH)).Count()
                                + Held.Vorteile.Keys.Where((vorteil) => vorteil.Name.StartsWith(BEGABUNG_DÄMONISCH)).Count();
            }
            else
            {
                elementMod.Value1 = elementMod.Value2 = gegenelementMod.Value1 = gegenelementMod.Value2 = false;
                ElementarharmonisierteAura = false;
            }
        }

        protected override void checkBeschworenesWesen()
        {
            base.checkBeschworenesWesen();
            if (BeschworenesWesen != null)
            {
                wesen.Value1 = (Element)Enum.Parse(typeof(Element), BeschworenesWesen.Beschwörbares.Elementarwesen.Element);
                wesen.Value2 = (ElementarWesen)Enum.Parse(typeof(ElementarWesen), BeschworenesWesen.Beschwörbares.Elementarwesen.Wesen);
            }
        }

        protected override List<Model.GegnerBase> loadWesen()
        {
            List<GegnerBase> elementare = Global.ContextHeld.LoadElementare();
            return elementare;
        }

        protected override void reset()
        {
            base.reset();
            wesen.Value1 = Element.Feuer;
            wesen.Value2 = ElementarWesen.Geist;
        }

        #region Properties

        private bool elementarharmonisierteAura;
        public bool ElementarharmonisierteAura
        {
            get { return elementarharmonisierteAura; }
            set
            {
                Set(ref elementarharmonisierteAura, value);
                gegenelementMod.Invalidate();
            }
        }

        public override string KontrollFormel
        {
            get { return "(MU + IN + CH + CH + ZfW) / 5"; }
        }

        protected override int calcKontrollWert()
        {
            return (int)Math.Round((Held.Mut + Held.Intuition + Held.Charisma * 2 + ZauberWert) / 5.0, MidpointRounding.AwayFromZero);
        }

        #endregion


        //protected override void beschwörungMisslungen(ProbenErgebnis erg)
        //{
        //    BeschwörungsErgebnis = "Es erscheint kein Elementarwesen";
        //}

        //protected override void beherrschungMisslungen(ProbenErgebnis erg)
        //{
        //    BeherrschungsErgebnis = "Das Elementarwesen erfüllt den Dienst nicht. Die AsP werden trotzdem abgezogen. Wenn noch AsP übrig sind steht das Wesen weiterhin zur Verfügung.";
        //}
    }

    public enum ElementarWesen
    {
        [Description("Elementargeist")]
        Geist,
        [Description("Dschinn")]
        Dschinn,
        [Description("Elementarer Meister")]
        Meister
    }
}
