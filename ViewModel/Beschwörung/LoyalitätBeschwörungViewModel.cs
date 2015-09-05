using MeisterGeister.Logic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public abstract class LoyalitätBeschwörungViewModel : BeschwörungViewModel
    {

        private Base.CommandBase loyalitätSteigern;
        public Base.CommandBase LoyalitätSteigern
        {
            get
            {
                if (loyalitätSteigern == null)
                    LoyalitätSteigern = new Base.CommandBase((o) => steigern(), (o) => loyalitätSteigernMöglich());
                return loyalitätSteigern;
            }
            private set { Set(ref loyalitätSteigern, value); }
        }

        private Base.CommandBase würfleLoyalität;
        public Base.CommandBase WürfleLoyalität
        {
            get
            {
                if (würfleLoyalität == null)
                    würfleLoyalität = new Base.CommandBase((o) => würfle(), (o) => loyalitätWürfelnMöglich());
                return würfleLoyalität;
            }
            private set { Set(ref würfleLoyalität, value); }
        }

        private void steigern()
        {
            ProbenErgebnis erg = beherrschungsProbe();
            if (erg.Gelungen)
                Loyalität += loyalitätsSteigerung.Value;
            else
                Status = BeschwörungsStatus.BeherrschungMisslungen;
        }

        protected override void reset()
        {
            base.reset();
            Loyalität = null;
        }

        private void würfle()
        {
            Loyalität = BeschworenesWesen.Beschwörbares.Kreatur.LOBasis + LoyalitätAusAnrufung
                + View.General.ViewHelper.ShowWürfelDialog(BeschworenesWesen.Beschwörbares.Kreatur.LOZufall, "Loyalität");
        }

        private bool loyalitätSteigernMöglich()
        {
            return Status == BeschwörungsStatus.BeherrschungGelungen && Loyalität.HasValue;
        }

        private bool loyalitätWürfelnMöglich()
        {
            return Status == BeschwörungsStatus.BeherrschungGelungen &&
                !String.IsNullOrEmpty(BeschworenesWesen.Beschwörbares.Kreatur.LOZufall) &&
                !Loyalität.HasValue;
        }

        public override BeschwörungsStatus Status
        {
            get
            {
                return base.Status;
            }
            protected set
            {
                //Loyalitäts-Mods deaktivieren
                if (Status == BeschwörungsStatus.BeherrschungGelungen)
                {
                    deactivateMods();
                }

                base.Status = value;

                //Loyalitäts-Mods aktivieren
                if (Status == BeschwörungsStatus.BeherrschungGelungen)
                {
                    loyalitätsMods();
                }

                LoyalitätSteigern.Invalidate();
                WürfleLoyalität.Invalidate();
            }
        }

        private int? loyalität;
        public int? Loyalität
        {
            get { return loyalität; }
            set
            {
                Set(ref loyalität, value);
                loyalitätSteigern.Invalidate();
                WürfleLoyalität.Invalidate();
            }
        }


        private const string MOD_LOYALITÄT = "Loyalität";

        private BeschwörungsModifikator<int> loyalitätsSteigerung;

        protected override void addMods()
        {
            //Erschwerniss von 4 pro LO-Steigerung
            loyalitätsSteigerung = new BeschwörungsModifikator<int>();
            loyalitätsSteigerung.Value = 1;
            loyalitätsSteigerung.GetKontrollMod = () => loyalitätsSteigerung.Value * 4;
            loyalitätsSteigerung.IsActive = false;
            Mods.Add(MOD_LOYALITÄT, loyalitätsSteigerung);

            bezahlung.GetKostenMod = () => (int)Math.Round(-bezahlung.Value * 0.2 * 7 + 7, MidpointRounding.AwayFromZero);
            bezahlung.IsActive = false;
            Mods.Add(MOD_BEZAHLUNG, bezahlung);
        }

        private int LoyalitätAusAnrufung
        {
            get
            {
                if (Zauber == "Skelettarius")
                {
                    return punkte.Value;
                }
                else return 0;
            }
        }

        private void loyalitätsMods()
        {
            //Falls wir keinen Zufallswurf machen müssen tragen wir die Loyalität gleich ein
            if (String.IsNullOrEmpty(BeschworenesWesen.Beschwörbares.Kreatur.LOZufall))
                Loyalität = BeschworenesWesen.Beschwörbares.Kreatur.LOBasis + LoyalitätAusAnrufung;

            punkte.IsActive = false;
            loyalitätsSteigerung.Value = 1;
            loyalitätsSteigerung.IsActive = true;
            bezahlung.IsActive = true;
        }

        private void deactivateMods()
        {
            punkte.IsActive = true;
            loyalitätsSteigerung.IsActive = false;
            bezahlung.IsActive = false;
        }
    }
}
