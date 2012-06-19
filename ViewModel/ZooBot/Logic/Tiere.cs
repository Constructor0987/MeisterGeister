using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MeisterGeister.ViewModel.ZooBot.Logic.Tiere
{
    public abstract class BasisTier
    {
        private string m_Name;
        private int m_SeiteZBA;
        private int m_Jagdschwierigkeit;
        private string m_Beute;
        private ArrayList m_Verbreitung = new ArrayList();

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public int SeiteZBA
        {
            get { return this.m_SeiteZBA; }
            set { this.m_SeiteZBA = value; }
        }

        protected int Jagdschwierigkeit
        {
            get { return this.m_Jagdschwierigkeit; }
            set { this.m_Jagdschwierigkeit = value; }
        }

        protected string Beute
        {
            get { return this.m_Beute; }
            set { this.m_Beute = value; }
        }

        protected ArrayList Verbreitung
        {
            get { return this.m_Verbreitung; }
        }

        /// <summary>
        /// Gibt die Beute, die bei der Jagd erzielt wird zurück.
        /// </summary>
        public virtual string GetBeute()
        {
            return this.Beute;
        }

        /// <summary>
        /// Gibt die Jagdschwierigkeit zurück.
        /// </summary>
        public virtual int GetJagdschwierigkeit()
        {
            return this.Jagdschwierigkeit;
        }

        /// <summary>
        /// Gibt einen fertig formatierten Text zurück über die Gefahr bzw. Besonderheit die möglicherweise bei der Jagd auftreten kann.
        /// Muss beim jeweiligen Tier überlanden werden.
        /// </summary>
        public virtual string GetGefahr()
        {
            return "";
        }

        /// <summary>
        /// Gibt die Verbreitungs ArrayListe zurück.
        /// </summary>
        public virtual ArrayList GetVerbreitung()
        {
            return this.Verbreitung;
        }
    }

    #region Tiere A-D
    //public class TierAffeZirkusaffe : BasisTier
    //{
    //    public TierAffeZirkusaffe()
    //    {
    //        this.Name = "Zirkusaffe";
    //        this.Jagdschwierigkeit = 15;
    //        this.SeiteZBA = 66;
    //        this.Beute = "0.5 bis 2 Rationen Fleisch, Fell (besser)";

    //        this.Verbreitung.Add("Wald");
    //    }
    //}

    public class TierAffePurzelaffe : BasisTier
    {
        public TierAffePurzelaffe()
        {
            this.Name = "Purzelaffe";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 66;
            this.Beute = "0.5 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierAffeMoosaffe : BasisTier
    {
        public TierAffeMoosaffe()
        {
            this.Name = "Moosaffe";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 66;
            this.Beute = "0.5 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierAffeLoewenaffe : BasisTier
    {
        public TierAffeLoewenaffe()
        {
            this.Name = "Löwenaffe";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 67;
            this.Beute = "0.5 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
        }
    }

    //public class TierAffeZuckeraffe : BasisTier
    //{
    //    public TierAffeZuckeraffe()
    //    {
    //        this.Name = "Zuckeraffe";
    //        this.Jagdschwierigkeit = 15;
    //        this.SeiteZBA = 67;
    //        this.Beute = "0.5 bis 2 Rationen Fleisch, Fell (besser)";

    //        this.Verbreitung.Add("Wald");
    //    }
    //}

    //public class TierAffeBoronsaeffchen : BasisTier
    //{
    //    public TierAffeBoronsaeffchen()
    //    {
    //        this.Name = "Boronsäffchen";
    //        this.Jagdschwierigkeit = 15;
    //        this.SeiteZBA = 67;
    //        this.Beute = "0.5 bis 2 Rationen Fleisch, Fell (besser)";

    //        this.Verbreitung.Add("Wald");
    //    }
    //}

    //public class TierAffeTotenkopfaeffchen : BasisTier
    //{
    //    public TierAffeTotenkopfaeffchen()
    //    {
    //        this.Name = "Totenkopfäffchen";
    //        this.Jagdschwierigkeit = 15;
    //        this.SeiteZBA = 67;
    //        this.Beute = "0.5 bis 2 Rationen Fleisch, Fell (besser)";

    //        this.Verbreitung.Add("Wald");
    //    }
    //}

    //public class TierAffeBruellaffe : BasisTier
    //{
    //    public TierAffeBruellaffe()
    //    {
    //        this.Name = "Brüllaffe";
    //        this.Jagdschwierigkeit = 2;
    //        this.SeiteZBA = 67;
    //        this.Beute = "bis 40 Rationen Fleisch (zäh), Fell (besser)";

    //        this.Verbreitung.Add("Regenwald");
    //    }
    //}

    public class TierAffeOtanOtan : BasisTier
    {
        public TierAffeOtanOtan()
        {
            this.Name = "Otan-Otan";
            this.Jagdschwierigkeit = 2;
            this.SeiteZBA = 67;
            this.Beute = "bis 40 Rationen Fleisch (zäh), Fell (besser)";

            this.Verbreitung.Add("Regenwald");
        }
    }

    public class TierAffeRiesenaffe : BasisTier
    {
        public TierAffeRiesenaffe()
        {
            this.Name = "Riesenaffe";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 67;
            this.Beute = "190 Rationen Fleisch (zäh), Fell (besser)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierAffeSumpfranze : BasisTier
    {
        public TierAffeSumpfranze()
        {
            this.Name = "Sumpfranze";
            this.Jagdschwierigkeit = 2;
            this.SeiteZBA = 68;
            this.Beute = "25 Rationen Fleisch (ungenießbar), Fell (wertlos)";

            this.Verbreitung.Add("Sumpf");
        }
    }

    public class TierAntilopeAlKebirAntilope : BasisTier
    {
        public TierAntilopeAlKebirAntilope()
        {
            this.Name = "Al'Kebir-Antilope";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 69;
            this.Beute = "140 Rationen Fleisch, Geweih (Trophäe), Fell (besser) oder Leder (teuer)";

            this.Verbreitung.Add("Steppe");            
        }
    }

    public class TierAntilopeHalmarAntilope : BasisTier
    {
        public TierAntilopeHalmarAntilope()
        {
            this.Name = "Halmar-Antilope";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 69;
            this.Beute = "15 Rationen Fleisch, Geweih (Trophäe), Fell (besser) oder Leder (teuer)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierAntilopeGabelantilope : BasisTier
    {
        public TierAntilopeGabelantilope()
        {
            this.Name = "Gabelantilope";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 69;
            this.Beute = "9 Rationen Fleisch, Geweih (Trophäe), Fell (besser) oder Leder (teuer)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Wüste und Wüstenrand");
        }
    }

    public class TierAntilopeKaren : BasisTier
    {
        public TierAntilopeKaren()
        {
            this.Name = "Karen";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 70;
            this.Beute = "40 Rationen Fleisch, Geweih (Trophäe), Fell (besser) oder Leder (besser)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierAntilopeSpringbock : BasisTier
    {
        public TierAntilopeSpringbock()
        {
            this.Name = "Springbock";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 70;
            this.Beute = "19 Rationen Fleisch, Geweih (Trophäe), Fell (besser) oder Leder (teuer)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierAntilopeVytaggaAntilope : BasisTier
    {
        public TierAntilopeVytaggaAntilope()
        {
            this.Name = "Vy'Tagga-Antilope";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 70;
            this.Beute = "15 Rationen Fleisch (ungenießbar), Geweih (Trophäe), Fell (besser) oder Leder (besser)";

            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierBaerBaumbaer : BasisTier
    {
        public TierBaerBaumbaer()
        {
            this.Name = "Baumbär";
            this.Jagdschwierigkeit = 6;
            this.SeiteZBA = 72;
            this.Beute = "15-20 Rationen Fleisch (zäh), Fell (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierBaerBaumwuerger : BasisTier
    {
        public TierBaerBaumwuerger()
        {
            this.Name = "Baumwürger";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 73;
            this.Beute = "30 Rationen Fleisch (zäh), Fell (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierBaerBorkenbaer : BasisTier
    {
        public TierBaerBorkenbaer()
        {
            this.Name = "Borkenbär";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 73;
            this.Beute = "110 Rationen Fleisch, Fell (einfach)";

            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierBaerFirunsbaer : BasisTier
    {
        public TierBaerFirunsbaer()
        {
            this.Name = "Firunsbär";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 73;
            this.Beute = "600 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Küste, Strand");
        }
    }

    public class TierBaerHoehlenbaer : BasisTier
    {
        public TierBaerHoehlenbaer()
        {
            this.Name = "Höhlenbär";
            this.Jagdschwierigkeit = 6;
            this.SeiteZBA = 73;
            this.Beute = "350 Rationen Fleisch, Fell (teuer)";

            this.Verbreitung.Add("Gebirge");
        }
    }

    public class TierBaerOrklandbaer : BasisTier
    {
        public TierBaerOrklandbaer()
        {
            this.Name = "Orklandbär";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 73;
            this.Beute = "50 Rationen Fleisch, Fell (billig)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierBaerSchwarzbaer : BasisTier
    {
        public TierBaerSchwarzbaer()
        {
            this.Name = "Schwarzbär";
            this.Jagdschwierigkeit = 4;
            this.SeiteZBA = 74;
            this.Beute = "380 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Wald");
        }
    }

    //public class TierBaerGrimmbaer : BasisTier
    //{
    //    public TierBaerGrimmbaer()
    //    {
    //        this.Name = "Grimmbär";
    //        this.Jagdschwierigkeit = 4;
    //        this.SeiteZBA = 74;
    //        this.Beute = "380 Rationen Fleisch, Fell (Luxusartikel)";

    //        this.Verbreitung.Add("Wald");
    //    }
    //}

    public class TierDachsStreifendachs : BasisTier
    {
        public TierDachsStreifendachs()
        {
            this.Name = "Streifendachs";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 77;
            this.Beute = "4 Rationen Fleisch, Fell (einfach)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierDachsSchneedachs : BasisTier
    {
        public TierDachsSchneedachs()
        {
            this.Name = "Schneedachs";
            this.Jagdschwierigkeit = 9;
            this.SeiteZBA = 77;
            this.Beute = "5 Rationen Fleisch, Fell (einfach)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    //public class TierDachsSuessmaul : BasisTier
    //{
    //    public TierDachsSuessmaul()
    //    {
    //        this.Name = "Süßmaul";
    //        this.Jagdschwierigkeit = 15;
    //        this.SeiteZBA = 77;
    //        this.Beute = "4 Rationen Fleisch, Fell (einfach)";

    //        this.Verbreitung.Add("Steppe");
    //        this.Verbreitung.Add("Waldrand");
    //    }
    //}
    #endregion

    #region Tiere E-G
    public class TierElefantBrabakerWaldelefant : BasisTier
    {
        public TierElefantBrabakerWaldelefant()
        {
            this.Name = "Brabaker Waldelefant";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 90;
            this.Beute = "1500 bis 2000 Rationen Fleisch, Haut (Leder, teuer), Stoßzähne (5 D je Stein / pro Zahn bis zu 15 Stein)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierElefantMastodon : BasisTier
    {
        public TierElefantMastodon()
        {
            this.Name = "Mastodon";
            this.Jagdschwierigkeit = 8;
            this.SeiteZBA = 90;
            this.Beute = "1600 bis 2400 Rationen Fleisch, Haut (Fell oder Leder, teuer), Stoßzähne (3 D je Stein / pro Zahn bis zu 20 Stein)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierElefantMammut : BasisTier
    {
        public TierElefantMammut()
        {
            this.Name = "Mammut";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 91;
            this.Beute = "2800 bis 3400 Rationen Fleisch, Haut (Fell oder Leder, teuer), Stoßzähne (4 D je Stein / pro Zahn bis zu 40 Stein)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierElefantZwergelefant : BasisTier
    {
        public TierElefantZwergelefant()
        {
            this.Name = "Zwergelefant";
            this.Jagdschwierigkeit = 4;
            this.SeiteZBA = 91;
            this.Beute = "500 Rationen Fleisch, Haut (Fell oder Leder, teuer), Stoßzähne (3 D je Stein / pro Zahn bis zu 30 Stein)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierFuchsRotfuchs : BasisTier
    {
        public TierFuchsRotfuchs()
        {
            this.Name = "Rotfuchs";
            this.Jagdschwierigkeit = 9;
            this.SeiteZBA = 98;
            this.Beute = "3 Rationen Fleisch (sehr zäh), Pelz (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierFuchsGelbfuchs : BasisTier
    {
        public TierFuchsGelbfuchs()
        {
            this.Name = "Gelbfuchs";
            this.Jagdschwierigkeit = 13;
            this.SeiteZBA = 98;
            this.Beute = "3 Rationen Fleisch (sehr zäh), Pelz (billig)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierFuchsBlaufuchs : BasisTier
    {
        public TierFuchsBlaufuchs()
        {
            this.Name = "Blaufuchs";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 98;
            this.Beute = "3 Rationen Fleisch (sehr zäh), Pelz (Luxusartikel)";

            this.Verbreitung.Add("Wald");
        }
    }

    //public class TierFuchsSilberfuchs : BasisTier
    //{
    //    public TierFuchsSilberfuchs()
    //    {
    //        this.Name = "Silberfuchs";
    //        this.Jagdschwierigkeit = 15;
    //        this.SeiteZBA = 98;
    //        this.Beute = "3 Rationen Fleisch (sehr zäh), Pelz (Luxusartikel)";

    //        this.Verbreitung.Add("Wald");
    //    }
    //}

    public class TierGefluegelAuerhahn : BasisTier
    {
        public TierGefluegelAuerhahn()
        {
            this.Name = "Auerhahn";
            this.Jagdschwierigkeit = 6;
            this.SeiteZBA = 100;
            this.Beute = "3 Rationen Fleisch";

            this.Verbreitung.Add("Wald");
        }
    }

    //public class TierGefluegelEnte : BasisTier
    //{
    //    public TierGefluegelEnte()
    //    {
    //        this.Name = "Ente";
    //        this.Jagdschwierigkeit = 6;
    //        this.SeiteZBA = 100;
    //        this.Beute = "1 Ration Fleisch";

    //        this.Verbreitung.Add("Fluss- und Seeufer, Teiche");
    //        this.Verbreitung.Add("Flussauen");
    //    }
    //}

    public class TierGefluegelFasan : BasisTier
    {
        public TierGefluegelFasan()
        {
            this.Name = "Fasan";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 100;
            this.Beute = "1 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierGefluegelRegenbogenfasan : BasisTier
    {
        public TierGefluegelRegenbogenfasan()
        {
            this.Name = "Regenbogenfasan";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 100;
            this.Beute = "1 Ration Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierGefluegelRebhuhn : BasisTier
    {
        public TierGefluegelRebhuhn()
        {
            this.Name = "Rebhuhn";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 100;
            this.Beute = "1 bis 2 Rationen Fleisch";

            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    //public class TierGefluegelWachtel : BasisTier
    //{
    //    public TierGefluegelWachtel()
    //    {
    //        this.Name = "Wachtel";
    //        this.Jagdschwierigkeit = 5;
    //        this.SeiteZBA = 100;
    //        this.Beute = "0.2 Rationen Fleisch";

    //        this.Verbreitung.Add("Wald");
    //        this.Verbreitung.Add("Waldrand");
    //    }
    //}

    public class TierGefluegelTrappe : BasisTier
    {
        public TierGefluegelTrappe()
        {
            this.Name = "Trappe";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 101;
            this.Beute = "5 Rationen Fleisch";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Fluss- und Seeufer, Teiche");
            this.Verbreitung.Add("Flussauen");
        }
    }
    #endregion

    #region Tiere H-K
    public class TierHaseKarnickel : BasisTier
    {
        public TierHaseKarnickel()
        {
            this.Name = "Karnickel";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 108;
            this.Beute = "1 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierHaseOrklandkarnickel : BasisTier
    {
        public TierHaseOrklandkarnickel()
        {
            this.Name = "Orklandkarnickel";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 109;
            this.Beute = "1 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierHasePfeifhase : BasisTier
    {
        public TierHasePfeifhase()
        {
            this.Name = "Pfeifhase";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 109;
            this.Beute = "1 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierHaseRiesenloeffler : BasisTier
    {
        public TierHaseRiesenloeffler()
        {
            this.Name = "Riesenlöffler";
            this.Jagdschwierigkeit = 4;
            this.SeiteZBA = 109;
            this.Beute = "2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierHaseRotpueschel : BasisTier
    {
        public TierHaseRotpueschel()
        {
            this.Name = "Rotpüschel";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 109;
            this.Beute = "1 Ration Fleisch, Fell (besser)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierHaseSilberbock : BasisTier
    {
        public TierHaseSilberbock()
        {
            this.Name = "Silberbock";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 109;
            this.Beute = "1 bis 2 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierHirschElch : BasisTier
    {
        public TierHirschElch()
        {
            this.Name = "Elch";
            this.Jagdschwierigkeit = 4;
            this.SeiteZBA = 110;
            this.Beute = "450 Rationen Fleisch, Geweih (Trophäe), Fell (besser) oder Leder (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierHirschKronenhirsch : BasisTier
    {
        public TierHirschKronenhirsch()
        {
            this.Name = "Kronenhirsch";
            this.Jagdschwierigkeit = 6;
            this.SeiteZBA = 110;
            this.Beute = "110 Rationen Fleisch, Geweih (Trophäe), Fell (teuer) oder Leder (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierHirschRehwild : BasisTier
    {
        public TierHirschRehwild()
        {
            this.Name = "Rehwild";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 110;
            this.Beute = "11 Rationen Fleisch, Geweih (Trophäe), Fell (besser) oder Leder (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierHirschFirunshirsch : BasisTier
    {
        public TierHirschFirunshirsch()
        {
            this.Name = "Firunshirsch";
            this.Jagdschwierigkeit = 8;
            this.SeiteZBA = 110;
            this.Beute = "60 Rationen Fleisch, Geweih (Trophäe), Fell (teuer, in reinem Weiß Luxusware) oder Leder (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierHundSteppenhund : BasisTier
    {
        public TierHundSteppenhund()
        {
            this.Name = "Steppenhund";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 116;
            this.Beute = "12 Rationen Fleisch (zäh), Fell (billig)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierKatzeWildkatze : BasisTier
    {
        public TierKatzeWildkatze()
        {
            this.Name = "Wildkatze";
            this.Jagdschwierigkeit = 20;
            this.SeiteZBA = 120;
            this.Beute = "3 Rationen Fleisch, Fell (einfach)";

            this.Verbreitung.Add("Grasland, Wiesen");
            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierKlippechse : BasisTier
    {
        public TierKlippechse()
        {
            this.Name = "Klippechse";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 121;
            this.Beute = "10 Rationen Fleisch, Haut (Leder, teuer), Eier (diverse alchemistische Anwendungen)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Fluss- und Seeufer, Teiche");
            this.Verbreitung.Add("Sumpf und Moor");            
        }
    }
    #endregion

    #region Tiere L-M
    public class TierLoeweBergloewe : BasisTier
    {
        public TierLoeweBergloewe()
        {
            this.Name = "Berglöwe";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 128;
            this.Beute = "65 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Gebirge");            
        }
    }

    public class TierLoeweSandloewe : BasisTier
    {
        public TierLoeweSandloewe()
        {
            this.Name = "Sandlöwe";
            this.Jagdschwierigkeit = 6;
            this.SeiteZBA = 128;
            this.Beute = "100 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Wüste und Wüstenrand");
            this.Verbreitung.Add("Steppe");            
        }
    }

    public class TierLoeweSchattenloewe : BasisTier
    {
        public TierLoeweSchattenloewe()
        {
            this.Name = "Lioma";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 128;
            this.Beute = "90 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Regenwald");            
        }
    }

    public class TierLoeweWaldloewe : BasisTier
    {
        public TierLoeweWaldloewe()
        {
            this.Name = "Waldlöwe";
            this.Jagdschwierigkeit = 9;
            this.SeiteZBA = 128;
            this.Beute = "60 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Wald");            
        }
    }

    public class TierLuchsRotluchs : BasisTier
    {
        public TierLuchsRotluchs()
        {
            this.Name = "Rotluchs";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 129;
            this.Beute = "10 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierLuchsRaschtulsluchs : BasisTier
    {
        public TierLuchsRaschtulsluchs()
        {
            this.Name = "Raschtulsluchs";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 129;
            this.Beute = "10 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierLuchsSonnenluchs : BasisTier
    {
        public TierLuchsSonnenluchs()
        {
            this.Name = "Sonnenluchs";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 129;
            this.Beute = "10 Rationen Fleisch, Fell (besser)";

            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierLuchsFirnluchs : BasisTier
    {
        public TierLuchsFirnluchs()
        {
            this.Name = "Firnluchs";
            this.Jagdschwierigkeit = 20;
            this.SeiteZBA = 129;
            this.Beute = "12 Rationen Fleisch, Fell (teuer)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierLuchsGaenseluchs : BasisTier
    {
        public TierLuchsGaenseluchs()
        {
            this.Name = "Gänseluchs";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 129;
            this.Beute = "3 Rationen Fleisch, Fell (einfach)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");            
        }
    }
    #endregion

    #region Tiere N-R
    //public class TierPantherJaguar : BasisTier
    //{
    //    public TierPantherJaguar()
    //    {
    //        this.Name = "Jaguar";
    //        this.Jagdschwierigkeit = 15;
    //        this.SeiteZBA = 147;
    //        this.Beute = "40 Rationen Fleisch, Fell (Luxusartikel)";

    //        this.Verbreitung.Add("Wald");            
    //    }
    //}

    public class TierPantherFleckenpanther : BasisTier
    {
        public TierPantherFleckenpanther()
        {
            this.Name = "Fleckenpanther";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 147;
            this.Beute = "40 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierPantherKhomgepard : BasisTier
    {
        public TierPantherKhomgepard()
        {
            this.Name = "Khômgepard";
            this.Jagdschwierigkeit = 15;
            this.SeiteZBA = 147;
            this.Beute = "15 Rationen Fleisch, Fell (Luxusartikel)";

            this.Verbreitung.Add("Wüste und Wüstenrand");
            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierRinderAuerochse : BasisTier
    {
        public TierRinderAuerochse()
        {
            this.Name = "Auerochse";
            this.Jagdschwierigkeit = 8;
            this.SeiteZBA = 158;
            this.Beute = "550 Rationen Fleisch, Hörner (Trophäe), Haut (einfach), Leder (teuer)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierRinderFirnyak : BasisTier
    {
        public TierRinderFirnyak()
        {
            this.Name = "Firnyak";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 158;
            this.Beute = "140 Rationen Fleisch, Hörner (Trophäe), Fell (teuer, in reinem weiß Luxusware) oder Leder (teuer)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierRinderOngalobulle : BasisTier
    {
        public TierRinderOngalobulle()
        {
            this.Name = "Ongalobulle";
            this.Jagdschwierigkeit = 4;
            this.SeiteZBA = 159;
            this.Beute = "300 Rationen Fleisch, Hörner (Trophäe), Haut (einfach), Leder (besser)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierRinderSteppenrind : BasisTier
    {
        public TierRinderSteppenrind()
        {
            this.Name = "Steppenrind";
            this.Jagdschwierigkeit = 12;
            this.SeiteZBA = 159;
            this.Beute = "600 Rationen Fleisch, Hörner (Trophäe), Haut (einfach), Leder (teuer)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierRobbeFelsrobbe : BasisTier
    {
        public TierRobbeFelsrobbe()
        {
            this.Name = "Felsrobbe";
            this.Jagdschwierigkeit = 2;
            this.SeiteZBA = 160;
            this.Beute = "bis 45 Rationen Fleisch, Talg (Fett, Öl, 5 S), Tran (1 D), Haut (Fell, besser, von Jungtieren teuer)";

            this.Verbreitung.Add("Küste, Strand");
        }
    }

    public class TierRobbeMeerkalb : BasisTier
    {
        public TierRobbeMeerkalb()
        {
            this.Name = "Meerkalb";
            this.Jagdschwierigkeit = 8;
            this.SeiteZBA = 160;
            this.Beute = "bis 250 Rationen Fleisch, Tran (5-7 D), Haut (Leder, besser)";

            this.Verbreitung.Add("Küste, Strand");
        }
    }

    public class TierRobbeSeetiger : BasisTier
    {
        public TierRobbeSeetiger()
        {
            this.Name = "Seetiger";
            this.Jagdschwierigkeit = 13;
            this.SeiteZBA = 160;
            this.Beute = "250-300 Rationen Fleisch, Tran (12-15 D), Haut (Leder, besser), Bein (teuer), Ambra (3 D)";

            this.Verbreitung.Add("Küste, Strand");
        }
    }
    #endregion

    #region Tiere S-Z
    public class TierSaebelzahntigerSteppentiger : BasisTier
    {
        public TierSaebelzahntigerSteppentiger()
        {
            this.Name = "Steppentiger";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 162;
            this.Beute = "100 Rationen Fleisch, Fell (Luxusartikel), Zähne (Trophäe)";

            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierSaebelzahntigerDschungeltiger : BasisTier
    {
        public TierSaebelzahntigerDschungeltiger()
        {
            this.Name = "Dschungeltiger";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 162;
            this.Beute = "100 Rationen Fleisch, Fell (Luxusartikel), Zähne (Trophäe)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierSaebelzahntigerSilberloewe : BasisTier
    {
        public TierSaebelzahntigerSilberloewe()
        {
            this.Name = "Silberlöwe";
            this.Jagdschwierigkeit = 10;
            this.SeiteZBA = 163;
            this.Beute = "100 Rationen Fleisch, Fell (Luxusartikel), Zähne (Trophäe)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierSchreckkatze : BasisTier
    {
        public TierSchreckkatze()
        {
            this.Name = "Schreckkatze";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 169;
            this.Beute = "12 Rationen Fleisch (nur für Orks und Goblins genießbar), Fell wertlos";

            this.Verbreitung.Add("Steppe");            
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierSchweinMaraskanischesStachelschwein : BasisTier
    {
        public TierSchweinMaraskanischesStachelschwein()
        {
            this.Name = "Maraskanisches Stachelschwein";
            this.Jagdschwierigkeit = 7;
            this.SeiteZBA = 172;
            this.Beute = "1 Ration Fleisch, 2W20+30 Stacheln (FF-Probe, um sie inklusive Giftdrüse aus dem Tier zu ziehen)";

            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Regenwald");            
        }
    }

    public class TierSchweinWildschwein : BasisTier
    {
        public TierSchweinWildschwein()
        {
            this.Name = "Wildschwein";
            this.Jagdschwierigkeit = 2;
            this.SeiteZBA = 173;
            this.Beute = "bis 130 Rationen Fleisch, Hauer (Trophäe), Fell (billig) oder Leder (einfach)";

            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }

    public class TierSchweinWarzenschwein : BasisTier
    {
        public TierSchweinWarzenschwein()
        {
            this.Name = "Warzenschwein";
            this.Jagdschwierigkeit = 2;
            this.SeiteZBA = 173;
            this.Beute = "bis 130 Rationen Fleisch, Hauer (Trophäe), Fell (billig) oder Leder (einfach)";

            this.Verbreitung.Add("Wald");
        }
    }
    public class TierStrauss : BasisTier
    {
        public TierStrauss()
        {
            this.Name = "Strauß";
            this.Jagdschwierigkeit = 4;
            this.SeiteZBA = 180;
            this.Beute = "50 Rationen Fleisch, 15 bis 60 Eier im Nest (jedes Ei eine Ration), Gefieder (teuer)";

            this.Verbreitung.Add("Wüste und Wüstenrand");
            this.Verbreitung.Add("Steppe");
        }
    }

    public class TierVielfrass : BasisTier
    {
        public TierVielfrass()
        {
            this.Name = "Vielfraß";
            this.Jagdschwierigkeit = 6;
            this.SeiteZBA = 182;
            this.Beute = "8 Rationen Fleisch (ungenießbar), Fell (besser)";

            this.Verbreitung.Add("Wald");            
        }
    }

    public class TierVielfrassBaumschleimer : BasisTier
    {
        public TierVielfrassBaumschleimer()
        {
            this.Name = "Baumschleimer";
            this.Jagdschwierigkeit = 5;
            this.SeiteZBA = 183;
            this.Beute = "6 Rationen Fleisch (ungenießbar), Fell (wertlos)";

            this.Verbreitung.Add("Wald");
        }
    }

    public class TierZiegeGebirgsbock : BasisTier
    {
        public TierZiegeGebirgsbock()
        {
            this.Name = "Gebirgsbock";
            this.Jagdschwierigkeit = 20;
            this.SeiteZBA = 189;
            this.Beute = "60 Rationen Fleisch (zäh), Fell (einfach), Horn (Trophäe)";

            this.Verbreitung.Add("Gebirge");            
        }
    }

    #endregion

    #region Mustertier
    public class TierMuster : BasisTier
    {
        public TierMuster()
        {
            this.Name = "Mustertier";
            this.Jagdschwierigkeit = 0;
            this.SeiteZBA = 2;
            this.Beute = "";

            this.Verbreitung.Add("Eis");
            this.Verbreitung.Add("Wüste und Wüstenrand");
            this.Verbreitung.Add("Gebirge");
            this.Verbreitung.Add("Hochland");
            this.Verbreitung.Add("Steppe");
            this.Verbreitung.Add("Grasland, Wiesen");
            this.Verbreitung.Add("Fluss- und Seeufer, Teiche");
            this.Verbreitung.Add("Küste, Strand");
            this.Verbreitung.Add("Flussauen");
            this.Verbreitung.Add("Sumpf und Moor");
            this.Verbreitung.Add("Regenwald");
            this.Verbreitung.Add("Wald");
            this.Verbreitung.Add("Waldrand");
        }
    }
    #endregion

}

