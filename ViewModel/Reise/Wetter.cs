using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Reise
{
    /// <summary>
    /// Wettergenerator nach Wege des Entdeckers Seite 156ff.
    /// </summary>
    public class Wetter
    {
        #region Properties
        private Wolken bewölkung;

        public Wolken Bewölkung
        {
            get { return bewölkung; }
            set
            {
                bewölkung = value;
                BestimmeWolkenTempMod();
                NeuerNiederschlag();
            }
        }
        private int wolkenTempModTag = 0;
        private int wolkenTempModNacht = 0;
        private int windTempMod = 0;

        private int tagestemperatur;

        /// <summary>
        /// Durchschnittliche Mittagstemperatur
        /// </summary>
        public int Tagestemperatur
        {
            get { return tagestemperatur + wolkenTempModTag + windTempMod; }
            set { tagestemperatur = value - wolkenTempModTag - windTempMod; }
        }

        private int nachttemperatur; //nächtliche Tiefsttemperatur

        /// <summary>
        /// Niedrigste Nachttemperatur
        /// </summary>
        public int Nachttemperatur
        {
            get { return nachttemperatur + wolkenTempModNacht + windTempMod; }
            set { nachttemperatur = value - wolkenTempModNacht - windTempMod; }
        }

        public int TemperaturDifferenz
        {
            get { return Math.Abs(Tagestemperatur - Nachttemperatur); }
        }

        private Wind windstärke;

        public Wind Windstärke
        {
            get { return windstärke; }
            set
            {
                windstärke = value;
                BestimmeWindTempMod();
            }
        }

        private Niederschlag niederschlag;

        public Niederschlag Niederschlag
        {
            get { return niederschlag; }
            set { niederschlag = value; }
        }

        private Unwetter unwetter;

        public Unwetter Unwetter
        {
            get { return unwetter; }
            set { unwetter = value; }
        }


        private bool wüste = false;

        /// <summary>
        /// Wüstenwetter?
        /// </summary>
        public bool Wüste
        {
            get { return wüste; }
            set
            {
                wüste = value;
                NeueBewölkung();
            }
        }

        private int windreich = 0; //0,1,2
        /// <summary>
        /// Windbonus für windreiche Gegenden (0,1,2).
        /// In Küstennähe, auf dem offenen Meer, im Flachland und auf Höhenzügen ist der Wind eher stärker.
        /// </summary>
        public int Windreich
        {
            get { return windreich; }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 2)
                    value = 2;
                windreich = value;
                NeueWindstärke();
            }
        }
        private Klimazone klimazone = Klimazone.ZentralesMittelreich;

        public Klimazone Klimazone
        {
            get { return klimazone; }
            set
            {
                klimazone = value;
                NeueTemperatur();
            }
        }

        private Logic.Kalender.DsaTool.Season jahreszeit;

        public Logic.Kalender.DsaTool.Season Jahreszeit
        {
            get { return jahreszeit; }
            set
            {
                jahreszeit = value;
                NeueWindstärke();
                NeueTemperatur();
            }
        }

        private Wetter morgen;

        public Wetter Morgen
        {
            get
            {
                if (morgen == null)
                {
                    morgen = new Wetter(this);
                    morgen.gestern = this;
                }
                return morgen;
            }
        }

        private Wetter gestern;

        public Wetter Gestern
        {
            get
            {
                if (gestern == null)
                {
                    gestern = new Wetter(this);
                    gestern.morgen = this;
                }
                return gestern;
            }
        }


        #endregion

        public Wetter()
        {
            Generiere();
        }

        private Wetter(Wetter gestern)
        {
            CopyFrom(gestern);
            Veränderung();
        }

        private void CopyFrom(Wetter gestern)
        {
            bewölkung = gestern.bewölkung;
            wolkenTempModNacht = gestern.wolkenTempModNacht;
            wolkenTempModTag = gestern.wolkenTempModTag;
            windTempMod = gestern.windTempMod;
            tagestemperatur = gestern.tagestemperatur;
            nachttemperatur = gestern.nachttemperatur;
            windstärke = gestern.windstärke;
            niederschlag = gestern.niederschlag;
            wüste = gestern.wüste;
            windreich = gestern.windreich;
            klimazone = gestern.klimazone;
            jahreszeit = gestern.jahreszeit;
        }

        public void Generiere()
        {
            NeueWindstärke(); //2 Wind
            NeueBewölkung(); //1,4 Bewölkung und Niederschlag
            NeueTemperatur(); //3 Temperatur
            NeuesUnwetter(); //5 Interpretation
        }

        #region Generierung (Basis)

        /// <summary>
        /// Neue Bewölkung auswürfeln.
        /// Verändert auch den Niederschlag.
        /// </summary>
        public void NeueBewölkung()
        {
            int w = Logic.General.RandomNumberGenerator.W20;
            if (wüste)
            {
                switch (w)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        Bewölkung = Wolken.Wolkenlos;
                        break;
                    case 17:
                    case 18:
                        Bewölkung = Wolken.EinzelneWolken;
                        break;
                    case 19:
                        Bewölkung = Wolken.Bewölkt;
                        break;
                    case 20:
                    default:
                        Bewölkung = Wolken.Wolkendecke;
                        break;
                }
            }
            else
            {
                switch (w)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        Bewölkung = Wolken.Wolkenlos;
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        Bewölkung = Wolken.EinzelneWolken;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        Bewölkung = Wolken.Bewölkt;
                        break;
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    default:
                        Bewölkung = Wolken.Wolkendecke;
                        break;
                }
            }
        }

        /// <summary>
        /// Neue Windstärke auswürfeln.
        /// </summary>
        public void NeueWindstärke()
        {
            int w = Logic.General.RandomNumberGenerator.W20;
            w += windreich;
            if (jahreszeit == Logic.Kalender.DsaTool.Season.Autumn)
            {
                switch (w)
                {
                    case 1:
                    case 2:
                    case 3:
                        Windstärke = Wind.Windstill;
                        break;
                    case 4:
                    case 5:
                        Windstärke = Wind.LeichterWind;
                        break;
                    case 6:
                    case 7:
                        Windstärke = Wind.SanfteBrise;
                        break;
                    case 8:
                    case 9:
                    case 10:
                        Windstärke = Wind.FrischeBrise;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                        Windstärke = Wind.SteifeBrise;
                        break;
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        Windstärke = Wind.StarkerWind;
                        break;
                    case 19:
                    case 20:
                    default:
                        Windstärke = Wind.Sturm;
                        break;
                }
            }
            else
            {
                switch (w)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        Windstärke = Wind.Windstill;
                        break;
                    case 5:
                    case 6:
                    case 7:
                        Windstärke = Wind.LeichterWind;
                        break;
                    case 8:
                    case 9:
                    case 10:
                        Windstärke = Wind.SanfteBrise;
                        break;
                    case 11:
                    case 12:
                    case 13:
                        Windstärke = Wind.FrischeBrise;
                        break;
                    case 14:
                    case 15:
                    case 16:
                        Windstärke = Wind.SteifeBrise;
                        break;
                    case 17:
                    case 18:
                    case 19:
                        Windstärke = Wind.StarkerWind;
                        break;
                    case 20:
                    default:
                        Windstärke = Wind.Sturm;
                        break;
                }
            }
        }

        /// <summary>
        /// Bestimmt die Temperatur anhand der Region und der Jahreszeit. Die Nachttemperatur wird neu ausgewürfelt.
        /// </summary>
        public void NeueTemperatur()
        {
            int jzMod = 5;
            int mittagstemp = 0;
            switch (klimazone) //Frülings-/Herbsttemperaturen
            {
                case Klimazone.EwigesEis:
                    jzMod = 10;
                    mittagstemp = -30;
                    break;
                case Klimazone.EhernesSchwert:
                    jzMod = 10;
                    mittagstemp = -20;
                    break;
                case Klimazone.HoherNorden:
                    jzMod = 10;
                    mittagstemp = -10;
                    break;
                case Klimazone.TundraTaiga:
                    mittagstemp = 0;
                    break;
                case Klimazone.BornlandThorwal:
                case Klimazone.StreitendeKönigreicheBisWeiden:
                    mittagstemp = 5;
                    break;
                case Klimazone.ZentralesMittelreich:
                    mittagstemp = 10;
                    break;
                case Klimazone.NördlichesHorasreichAlmadaAranien:
                    mittagstemp = 15;
                    break;
                case Klimazone.HöhenDesRaschtulswalls:
                    mittagstemp = 0;
                    break;
                case Klimazone.SüdlichesHorasreichReichDerErstenSonne:
                    mittagstemp = 0;
                    break;
                case Klimazone.Khôm:
                    mittagstemp = 35;
                    break;
                case Klimazone.EchsensümpfeMeridiana:
                    mittagstemp = 25;
                    break;
                case Klimazone.AltoumGewürzinselnSüdmeer:
                default:
                    mittagstemp = 30;
                    break;
            }

            switch (jahreszeit)
            {
                case Logic.Kalender.DsaTool.Season.Summer:
                    mittagstemp += jzMod;
                    break;
                case Logic.Kalender.DsaTool.Season.Winter:
                    mittagstemp -= jzMod;
                    break;
            }

            if (klimazone == Klimazone.BornlandThorwal && (jahreszeit == Logic.Kalender.DsaTool.Season.Spring || jahreszeit == Logic.Kalender.DsaTool.Season.Autumn))
                mittagstemp = 3;
            else if (klimazone == Klimazone.HöhenDesRaschtulswalls && jahreszeit == Logic.Kalender.DsaTool.Season.Winter)
                mittagstemp = -10;

            tagestemperatur = mittagstemp/* + (int)Math.Round(Logic.General.RandomNumberGenerator.RandomNormalDistributionMinMax(-5, 5))*/; //dieser Random-Anteil ist nicht nach WdE
            nachttemperatur = tagestemperatur - Logic.General.RandomNumberGenerator.RandomInt(6, 25);
        }

        /// <summary>
        /// Neuen Niederschlag auswürfeln.
        /// </summary>
        public void NeuerNiederschlag()
        {
            int w = Logic.General.RandomNumberGenerator.W20;
            switch (Bewölkung)
            {
                case Wolken.Wolkenlos:
                    Niederschlag = Niederschlag.Keiner;
                    return;
                case Wolken.EinzelneWolken:
                    if (w > 1)
                    {
                        Niederschlag = Niederschlag.Keiner;
                        return;
                    }
                    break;
                case Wolken.Bewölkt:
                    if (w > 4)
                    {
                        Niederschlag = Niederschlag.Keiner;
                        return;
                    }
                    break;
                case Wolken.Wolkendecke:
                default:
                    if (w > 10)
                    {
                        Niederschlag = Niederschlag.Keiner;
                        return;
                    }
                    break;
            }
            NeueNiederschlagsArt();
        }

        /// <summary>
        /// Nur neue Niederschlagsart auswürfeln.
        /// </summary>
        public void NeueNiederschlagsArt()
        {
            //Niederschlagsart WdE 158 oben
            int w = Logic.General.RandomNumberGenerator.W20;
            switch (Windstärke)
            {
                case Wind.Windstill:
                    switch (w)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                            Niederschlag = Niederschlag.Nieselregen;
                            break;
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                            Niederschlag = Niederschlag.Regen;
                            break;
                        case 20:
                        default:
                            Niederschlag = Niederschlag.Wolkenbruch;
                            break;
                    }
                    break;
                case Wind.LeichterWind:
                    switch (w)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            Niederschlag = Niederschlag.Nieselregen;
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                            Niederschlag = Niederschlag.Regen;
                            break;
                        case 19:
                        case 20:
                        default:
                            Niederschlag = Niederschlag.Wolkenbruch;
                            break;
                    }
                    break;
                case Wind.SanfteBrise:
                    switch (w)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            Niederschlag = Niederschlag.Nieselregen;
                            break;
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                            Niederschlag = Niederschlag.Regen;
                            break;
                        case 18:
                        case 19:
                        case 20:
                        default:
                            Niederschlag = Niederschlag.Wolkenbruch;
                            break;
                    }
                    break;
                case Wind.FrischeBrise:
                    switch (w)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            Niederschlag = Niederschlag.Nieselregen;
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                            Niederschlag = Niederschlag.Regen;
                            break;
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        default:
                            Niederschlag = Niederschlag.Wolkenbruch;
                            break;
                    }
                    break;
                case Wind.SteifeBrise:
                    switch (w)
                    {
                        case 1:
                        case 2:
                        case 3:
                            Niederschlag = Niederschlag.Nieselregen;
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                            Niederschlag = Niederschlag.Regen;
                            break;
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        default:
                            Niederschlag = Niederschlag.Wolkenbruch;
                            break;
                    }
                    break;
                case Wind.StarkerWind:
                    switch (w)
                    {
                        case 1:
                        case 2:
                            Niederschlag = Niederschlag.Nieselregen;
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                            Niederschlag = Niederschlag.Regen;
                            break;
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        default:
                            Niederschlag = Niederschlag.Wolkenbruch;
                            break;
                    }
                    break;
                case Wind.Sturm:
                default:
                    switch (w)
                    {
                        case 1:
                            Niederschlag = Niederschlag.Nieselregen;
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            Niederschlag = Niederschlag.Regen;
                            break;
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        default:
                            Niederschlag = Niederschlag.Wolkenbruch;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Anhand der generierten Werte wird bestimmt ob ein Unwetter auftritt
        /// </summary>
        private void NeuesUnwetter()
        {
            //Hier ermitteln wir zu welcher Temperatur das Unwetter auftritt.
            //Das ist relevant um z.B. zu entscheiden ob es sich um Regen oder Schnee handelt
            //Laut Regelwerk ist es bei geschlossener Wolkendecke möglich dass es nachts wärmer als Tagsüber ist
            //Diesen Fall müssen wir prüfen sonst crasht der RandomNumberGenerator
            double minTemp = Math.Min(Nachttemperatur, Tagestemperatur);
            double maxTemp = Math.Max(Nachttemperatur, Tagestemperatur);
            double unwetterTemp = Logic.General.RandomNumberGenerator.RandomNormalDistributionMinMax(minTemp, maxTemp);

            if (Windstärke == Wind.Sturm && Wüste)
            {
                Unwetter = Unwetter.Sandsturm;
                return;
            }

            if (Niederschlag == Niederschlag.Wolkenbruch && unwetterTemp < 0)
            {
                Unwetter = Unwetter.Hagelschlag;
                return;
            }

            if (Windstärke == Wind.Sturm && Niederschlag == Niederschlag.Keiner)
            {
                Unwetter = Unwetter.Windhose;
                return;
            }

            if (Windstärke == Wind.Sturm && Niederschlag >= Niederschlag.Nieselregen)
            {
                Unwetter = Unwetter.Gewitter;
                return;
            }

            if (Windstärke >= Wind.StarkerWind && Niederschlag >= Niederschlag.Regen && unwetterTemp < 0)
            {
                Unwetter = Unwetter.Schneesturm;
                return;
            }

            if (Niederschlag == Niederschlag.Wolkenbruch && unwetterTemp >= 0)
            {
                Unwetter = Unwetter.Starkregen;
                return;
            }

            if (Windstärke >= Wind.StarkerWind)
            {
                Unwetter = Unwetter.HeftigerWind;
                return;
            }

            if (Niederschlag == Niederschlag.Nieselregen && Bewölkung <= Wolken.EinzelneWolken)
            {
                Unwetter = Unwetter.Nebel;
                return;
            }

            Unwetter = Unwetter.Keines;
        }

        private void Veränderung()
        {
            int w = Logic.General.RandomNumberGenerator.W20;
            switch (Jahreszeit)
            {
                case Logic.Kalender.DsaTool.Season.Summer:
                case Logic.Kalender.DsaTool.Season.Winter:
                    switch (w)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            break;
                        case 10:
                            NeueWindstärke();
                            break;
                        case 11:
                            NeueTemperatur();
                            break;
                        case 12:
                            NeuerNiederschlag();
                            break;
                        case 13:
                            NeueBewölkung();
                            break;
                        case 14:
                            NeueWindstärke();
                            NeueTemperatur();
                            break;
                        case 15:
                            NeueWindstärke();
                            NeuerNiederschlag();
                            break;
                        case 16:
                            NeuerNiederschlag();
                            NeueTemperatur();
                            break;
                        case 17:
                            NeueWindstärke();
                            NeuerNiederschlag();
                            NeueTemperatur();
                            break;
                        case 18:
                            NeueBewölkung();
                            NeueTemperatur();
                            break;
                        case 19:
                            NeueWindstärke();
                            NeueBewölkung();
                            break;
                        case 20:
                        default:
                            NeueWindstärke();
                            NeueBewölkung();
                            NeueTemperatur();
                            break;
                    }
                    break;
                case Logic.Kalender.DsaTool.Season.Autumn:
                case Logic.Kalender.DsaTool.Season.Spring:
                default:
                    switch (w)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            break;
                        case 5:
                            NeueWindstärke();
                            break;
                        case 6:
                            NeueTemperatur();
                            break;
                        case 7:
                            NeuerNiederschlag();
                            break;
                        case 8:
                        case 9:
                            NeueBewölkung();
                            break;
                        case 10:
                        case 11:
                            NeueWindstärke();
                            NeueTemperatur();
                            break;
                        case 12:
                        case 13:
                            NeueWindstärke();
                            NeuerNiederschlag();
                            break;
                        case 14:
                        case 15:
                            NeuerNiederschlag();
                            NeueTemperatur();
                            break;
                        case 16:
                            NeueWindstärke();
                            NeuerNiederschlag();
                            NeueTemperatur();
                            break;
                        case 17:
                            NeueBewölkung();
                            NeueTemperatur();
                            break;
                        case 18:
                            NeueWindstärke();
                            NeueBewölkung();
                            break;
                        case 19:
                        case 20:
                        default:
                            NeueWindstärke();
                            NeueBewölkung();
                            NeueTemperatur();
                            break;
                    }
                    break;
            }
            NeuesUnwetter();
        }

        private void BestimmeWindTempMod()
        {
            switch (windstärke)
            {
                case Wind.Windstill:
                    windTempMod = 4;
                    return;
                case Wind.LeichterWind:
                    windTempMod = 2;
                    return;
                case Wind.SanfteBrise:
                case Wind.FrischeBrise:
                    windTempMod = 0;
                    return;
                case Wind.SteifeBrise:
                    windTempMod = -2;
                    return;
                case Wind.StarkerWind:
                    windTempMod = -4;
                    return;
                case Wind.Sturm:
                default:
                    windTempMod = -6;
                    return;
            }
        }

        private void BestimmeWolkenTempMod()
        {
            switch (Bewölkung)
            {
                case Wolken.Wolkenlos:
                    wolkenTempModTag = 10;
                    wolkenTempModNacht = -10;
                    return;
                case Wolken.EinzelneWolken:
                    wolkenTempModTag = 5;
                    wolkenTempModNacht = -5;
                    return;
                case Wolken.Bewölkt:
                    wolkenTempModTag = 0;
                    wolkenTempModNacht = 0;
                    return;
                case Wolken.Wolkendecke:
                default:
                    wolkenTempModTag = -5;
                    wolkenTempModNacht = 5;
                    return;
            }
        }

        #endregion

    }

    public enum Wolken
    {
        [Description("völlig wolkenlos")]
        Wolkenlos = 0,
        [Description("einzelne Wolken")]
        EinzelneWolken = 1,
        [Description("bewölkt mit Wolkenlücken")]
        Bewölkt = 2,
        [Description("geschlossene Wolkendecke")]
        Wolkendecke = 3
    }

    public enum Wind
    {
        [Description("windstill")]
        Windstill = 0,
        [Description("leichter Wind")]
        LeichterWind = 1,
        [Description("sanfte Brise")]
        SanfteBrise = 2,
        [Description("frische Brise")]
        FrischeBrise = 3,
        [Description("steife Brise")]
        SteifeBrise = 4,
        [Description("starker Wind")]
        StarkerWind = 5,
        [Description("Sturm")]
        Sturm = 6
    }

    public enum Niederschlag
    {
        [Description("kein Niederschlag")]
        Keiner = 0,
        [Description("Nieselregen / ein paar Flocken")]
        Nieselregen = 1,
        [Description("ergiebiger Regen / ergiebiger Schneefall")]
        Regen = 2,
        [Description("Wolkenbruch / Dauerschnee oder Hagel")]
        Wolkenbruch = 3
    }

    public enum Klimazone
    {
        [Description("Ewiges Eis")]
        EwigesEis,
        [Description("Ehrenes Schwert")]
        EhernesSchwert,
        [Description("Hoher Norden")]
        HoherNorden,
        [Description("Tundra / Taiga")]
        TundraTaiga,
        [Description("Bornland / Thorwal")]
        BornlandThorwal,
        [Description("Streitende Königreiche bis Weiden")]
        StreitendeKönigreicheBisWeiden,
        [Description("Zentrales Mittelreich")]
        ZentralesMittelreich,
        [Description("Nördliches Horasreich / Almada / Aranien")]
        NördlichesHorasreichAlmadaAranien,
        [Description("Höhen des Raschtulswalls")]
        HöhenDesRaschtulswalls,
        [Description("Südliches Horasreich / Reich der ersten Sonne")]
        SüdlichesHorasreichReichDerErstenSonne,
        [Description("Khôm")]
        Khôm,
        [Description("Echsensümpfe / Meridiana")]
        EchsensümpfeMeridiana,
        [Description("Altoum / Gewürzinseln / Südmeer")]
        AltoumGewürzinselnSüdmeer
    }

    public enum Unwetter
    {
        [Description("Kein Unwetter")]
        Keines,
        [Description("Sandsturm")]
        Sandsturm,
        [Description("Schneesturm")]
        Schneesturm,
        [Description("Hagelschlag")]
        Hagelschlag,
        [Description("Gewitter")]
        Gewitter,
        [Description("Starkregen")]
        Starkregen,
        [Description("Windhose")]
        Windhose,
        [Description("Nebel")]
        Nebel,
        [Description("Heftiger Wind")]
        HeftigerWind
    }
}
