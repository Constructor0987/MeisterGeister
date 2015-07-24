using MeisterGeister.Logic.General;
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
        protected override List<Model.GegnerBase> loadWesen()
        {
            return Global.ContextHeld.LoadElementare();
        }

        private Element element;
        public Element Element
        {
            get { return element; }
            set
            {
                element = value;
                OnChanged();
                OnChanged("GegenElement");
            }
        }

        public Element GegenElement
        {
            get { return ~Element; }
        }

        private ElementarWesen typ;
        public ElementarWesen Typ
        {
            get { return typ; }
            set
            {
                typ = value;
                OnChanged();
                switch (value)
                {
                    case ElementarWesen.Elementargeist:
                        Zauber = "Elementarer Diener";
                        break;
                    case ElementarWesen.Dschinn:
                        Zauber = "Dschinnenruf";
                        break;
                    case ElementarWesen.ElementarerMeister:
                        Zauber = "Meister der Elemente";
                        break;
                }
            }
        }

        protected override void reset()
        {
            base.reset();
            BegabungElement = MerkmalElement = BegabungGegenElement = BegabungGegenElement = DämonGerufen = Paktierer = Affinität = VerhüllteAura = false;
            Dämonisch = SchwacheAusstrahlung = Stigma = 0;
            Element = Element.Feuer;
            Typ = ElementarWesen.Elementargeist;
        }

        #region Properties

        private bool begabungElement;
        public bool BegabungElement
        {
            get { return begabungElement; }
            set
            {
                Set(ref begabungElement, value);
                OnChanged("ElementRufMod");
                OnChanged("ElementHerrschMod");
                OnChangedSum();
            }
        }

        private bool merkmalElement;
        public bool MerkmalElement
        {
            get { return merkmalElement; }
            set
            {
                Set(ref merkmalElement, value);
                OnChanged("ElementRufMod");
                OnChanged("ElementHerrschMod");
                OnChangedSum();
            }
        }

        public int ElementRufMod
        {
            get { return (BegabungElement ? -2 : 0) + (MerkmalElement ? -2 : 0); }
        }
        public int ElementHerrschMod
        {
            get { return (BegabungElement ? -2 : 0) + (MerkmalElement ? -2 : 0); }
        }


        private bool begabungGegenElement;
        public bool BegabungGegenElement
        {
            get { return begabungGegenElement; }
            set
            {
                Set(ref begabungGegenElement, value);
                OnChanged("GegenElementRufMod");
                OnChanged("GegenElementHerrschMod");
                OnChangedSum();
            }
        }

        private bool merkmalGegenElement;
        public bool MerkmalGegenElement
        {
            get { return merkmalGegenElement; }
            set
            {
                Set(ref merkmalGegenElement, value);
                OnChanged("GegenElementRufMod");
                OnChanged("GegenElementHerrschMod");
                OnChangedSum();
            }
        }

        public int GegenElementRufMod
        {
            get { return (BegabungGegenElement ? 4 : 0) + (MerkmalGegenElement ? 4 : 0); }
        }
        public int GegenElementHerrschMod
        {
            get { return (BegabungGegenElement ? 2 : 0) + (MerkmalGegenElement ? 2 : 0); }
        }

        private int dämonisch;
        public int Dämonisch
        {
            get { return dämonisch; }
            set
            {
                dämonisch = value;
                OnInputChanged();
            }
        }

        public int DämonischRufMod
        {
            get { return Dämonisch * 2; }
        }

        public int DämonischHerrschMod
        {
            get { return Dämonisch * 4; }
        }

        private bool dämonGerufen;
        public bool DämonGerufen
        {
            get { return dämonGerufen; }
            set
            {
                dämonGerufen = value;
                OnInputChanged();
            }
        }

        public int DämonGerufenHerrschMod
        {
            get { return DämonGerufen ? 4 : 0; }
        }

        private bool paktierer;
        public bool Paktierer
        {
            get { return paktierer; }
            set
            {
                paktierer = value;
                OnInputChanged();
            }
        }

        public int PaktiererRufMod
        {
            get { return Paktierer ? 6 : 0; }
        }

        public int PaktiererHerrschMod
        {
            get { return Paktierer ? 9 : 0; }
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

        private bool verhüllteAura;
        public bool VerhüllteAura
        {
            get { return verhüllteAura; }
            set
            {
                verhüllteAura = value;
                OnInputChanged();
            }
        }

        public int VerhüllteAuraHerrschMod
        {
            get { return VerhüllteAura ? 1 : 0; }
        }

        private int schwacheAusstrahlung;
        public int SchwacheAusstrahlung
        {
            get { return schwacheAusstrahlung; }
            set
            {
                schwacheAusstrahlung = value;
                OnInputChanged();
            }
        }

        public int SchwacheAusstrahlungHerrschMod
        {
            get { return SchwacheAusstrahlung; }
        }

        private int stigma;
        public int Stigma
        {
            get { return stigma; }
            set
            {
                stigma = value;
                OnInputChanged();
            }
        }

        public int StigmaHerrschMod
        {
            get { return (int)Math.Round(Stigma / 12.0, MidpointRounding.AwayFromZero); }
        }

        public override int BlutmagieHerrschMod
        {
            get
            {
                return (Blutmagie == Blutmagie.Keine) ? 0 : 12;
            }
        }

        #endregion

        public override int GesamtRufMod
        {
            get
            {
                return base.GesamtRufMod
                    + ElementRufMod
                    + GegenElementRufMod
                    + DämonischRufMod
                    + PaktiererRufMod;
            }
        }

        public override int GesamtHerrschMod
        {
            get
            {
                return base.GesamtHerrschMod
                    + ElementHerrschMod
                    + GegenElementHerrschMod
                    + DämonischHerrschMod
                    + DämonGerufenHerrschMod
                    + PaktiererHerrschMod
                    + AffinitätHerrschMod
                    + VerhüllteAuraHerrschMod
                    + SchwacheAusstrahlungHerrschMod
                    + StigmaHerrschMod;
            }
        }


        protected override void beschwörungMisslungen(ProbenErgebnis erg)
        {
            Ergebnis = "Es erscheint kein Elementarwesen";
        }

        protected override void beherrschungMisslungen(ProbenErgebnis erg)
        {
            Ergebnis = "Das Elementarwesen erfüllt den Dienst nicht. Die AsP werden trotzdem abgezogen. Wenn noch AsP übrig sind steht das Wesen weiterhin zur Verfügung.";
        }

        public override string KontrollFormel
        {
            get { return "(MU + IN + CH + CH + ZfW) / 5"; }
        }

        protected override int calcKontrollWert()
        {
            return (int)Math.Round((Held.Mut + Held.Intuition + Held.Charisma * 2 + ZauberWert) / 5.0, MidpointRounding.AwayFromZero);
        }
    }
    public enum Element
    {
        [Description("Feuer")]
        Feuer = 0,
        [Description("Wasser")]
        Wasser = ~Feuer,
        [Description("Humus")]
        Humus = 1,
        [Description("Eis")]
        Eis = ~Humus,
        [Description("Luft")]
        Luft = 2,
        [Description("Erz")]
        Erz = ~Luft
    }
    public enum ElementarWesen
    {
        [Description("Elementargeist")]
        Elementargeist,
        [Description("Dschinn")]
        Dschinn,
        [Description("Elementarer Meister")]
        ElementarerMeister
    }
}
