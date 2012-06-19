using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MeisterGeister.ViewModel.ZooBot.Logic.Pflanzen
{
    public enum EVorkommen
    {
        SEHRHAEUFIG = 1,
        HAEUFIG = 2,
        GELEGENTLICH = 4,
        SELTEN = 8,
        SEHRSELTEN = 16,
        KEINE = 100
    }

    public abstract class BasisPflanze
    {
        private string m_Name;
        private int m_SeiteZBA;
        private int m_Bestimmung;
        private string m_Grundmenge;
        private ArrayList m_Verbreitung = new ArrayList();
        private ArrayList m_Erntezeit = new ArrayList();

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

        protected int Bestimmung
        {
            get { return this.m_Bestimmung; }
            set { this.m_Bestimmung = value; }
        }

        protected string Grundmenge
        {
            get { return this.m_Grundmenge; }
            set { this.m_Grundmenge = value; }
        }

        protected ArrayList Verbreitung
        {
            get { return this.m_Verbreitung; }
        }

        protected ArrayList Erntezeit
        {
            get { return this.m_Erntezeit; }
        }

        /// <summary>
        /// Gibt die Grundmenge, die bei einer Pflanze gefunden wird, zurück. Bei Besonderheiten (z.B. Abhängigkeit vom Suchmonat)
        /// sollte die Methode in der jeweiligen Pflanze entsprechend überladen werden.
        /// </summary>
        /// <returns></returns>
        public virtual string GetGrundmenge(string monat, int tapstern)
        {
            return this.Grundmenge;
        }

        /// <summary>
        /// Gibt die Bestimmungsschwierigkeit der Pflanze zurück. Bei Besonderheiten (z.B. Abhängigkeit vom Suchmonat) sollte
        /// die Methode in der jeweiligen Pflanze entsprechend überladen werden.
        /// </summary>
        /// <param name="monat">Name des Suchmonat</param>
        /// <param name="speziell">Eintrag im Feld Speziell</param>
        public virtual int GetBestimmung(string monat, string speziell, string landschaft)
        {
            return this.Bestimmung;
        }

        /// <summary>
        /// Gibt einen fertig formatierten Text zurück über die Gefahr bzw. Besonderheit die möglicherweise bei der Suche oder
        /// Ernte der Pflanze auftreten kann. Muss in den jeweiligen Pflanzen überlanden werden.
        /// </summary>
        public virtual string GetGefahr()
        {
            return "";
        }

        /// <summary>
        /// Gibt die Verbreitungs ArrayListe zurück, kann in der einzelnen Pflanze überladen werden für besondere Verbreitung
        /// je nach Suchmonat.
        /// </summary>
        /// <param name="monat">Suchmonat</param>
        public virtual ArrayList GetVerbreitung(string monat)
        {
            return this.Verbreitung;
        }

        /// <summary>
        /// Gibt die Erntezeit ArrayListe zurück, kann in der einzelnen Pflanze überladen werden, für besondere Verbreitung
        /// je nach Region.
        /// </summary>
        /// <param name="region">Region in der gesucht wird.</param>
        public virtual ArrayList GetErntezeit(string region)
        {
            return this.Erntezeit;
        }
    }

    public class VerbreitungsElementPflanzen
    {
        private string m_landschaft;
        private int m_vorkommen;

        public VerbreitungsElementPflanzen(string l, int v)
        {
            this.Landschaft = l;
            this.Vorkommen = v;
        }

        public string Landschaft
        {
            get { return this.m_landschaft; }
            set { this.m_landschaft = value; }
        }

        public int Vorkommen
        {
            get { return this.m_vorkommen; }
            set { this.m_vorkommen = value; }
        }
    }

    #region Pflanzen A-D
    public class PflanzeAlraune : BasisPflanze
    {
        public PflanzeAlraune()
        {
            this.Name = "Alraune";
            this.Bestimmung = 9;
            this.SeiteZBA = 227;
            this.Grundmenge = "eine Pflanze";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeAlveranie : BasisPflanze
    {
        public PflanzeAlveranie()
        {
            this.Name = "Alveranie";
            this.Bestimmung = -5;
            this.SeiteZBA = 228;
            this.Grundmenge = "12 einzelne Blätter, in der Farbe des Monats";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Eis", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
        }
    }

    public class PflanzeArganstrauch : BasisPflanze
    {
        public PflanzeArganstrauch()
        {
            this.Name = "Arganstrauch";
            this.Bestimmung = 4;
            this.SeiteZBA = 228;
            this.Grundmenge = "eine Wurzel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeAtanKiefer : BasisPflanze
    {
        public PflanzeAtanKiefer()
        {
            this.Name = "Atan-Kiefer";
            this.Bestimmung = 6;
            this.SeiteZBA = 228;
            this.Grundmenge = "W20 Stein Rinde, bei komplettem Abschälen Verdreifachung des Wertes";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeAtmon : BasisPflanze
    {
        public PflanzeAtmon()
        {
            this.Name = "Atmon";
            this.Bestimmung = 5;
            this.SeiteZBA = 229;
            this.Grundmenge = "W6 Büschel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Peraine");
        }
    }

    public class PflanzeAxordaBaum : BasisPflanze
    {
        public PflanzeAxordaBaum()
        {
            this.Name = "Axorda-Baum";
            this.Bestimmung = 4;
            this.SeiteZBA = 229;
            this.Grundmenge = "ein Baum";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeBasilamine : BasisPflanze
    {
        public PflanzeBasilamine()
        {
            this.Name = "Basilamine";
            this.Bestimmung = 15;
            this.SeiteZBA = 230;
            this.Grundmenge = "W20+10 Schoten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (monat.Equals("Ingerimm"))
                return 5;
            else
                return base.GetBestimmung(monat,speziell,landschaft);
        }

        public override string GetGefahr()
        {
            return "Wer in einem Feld von Basilaminen steht, wird von der säurehaltigen Schoten beschossen.";
        }
    }

    public class PflanzeBelmart : BasisPflanze
    {
        public PflanzeBelmart()
        {
            this.Name = "Belmart";
            this.Bestimmung = 6;
            this.SeiteZBA = 230;
            this.Grundmenge = "2W20 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");;
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeBlutblatt : BasisPflanze
    {
        public PflanzeBlutblatt()
        {
            this.Name = "Blutblatt";
            this.Bestimmung = 4;
            this.SeiteZBA = 230;
            this.Grundmenge = "W20+2 Zweige pro 10 AsP der Quelle";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Eis", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeBoronie : BasisPflanze
    {
        public PflanzeBoronie()
        {
            this.Name = "Boronie";
            this.Bestimmung = -2;
            this.SeiteZBA = 231;
            this.Grundmenge = "5 Blüten, die kurz vor dem Verblühen sind";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeBoronsschlinge : BasisPflanze
    {
        public PflanzeBoronsschlinge()
        {
            this.Name = "Boronsschlinge";
            this.Bestimmung = 15;
            this.SeiteZBA = 231;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Wer sich der Boronsschlinge auf einen halben Schritt nähert muss eine KO+4 Probe ablegen oder er schläft binnen einer halben Minute ein und wird anschließend von den Ranken umschlungen.";
        }
    }

    public class PflanzeCarlog : BasisPflanze
    {
        public PflanzeCarlog()
        {
            this.Name = "Carlog";
            this.Bestimmung = 5;
            this.SeiteZBA = 232;
            this.Grundmenge = "W6 Blüten mit je einem Stempel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Peraine");
        }
    }

    public class PflanzeCheriaKaktus : BasisPflanze
    {
        public PflanzeCheriaKaktus()
        {
            this.Name = "Cheria-Kaktus";
            this.Bestimmung = 4;
            this.SeiteZBA = 232;
            this.Grundmenge = "W3 Stein Kaktusfleisch und pro Stein 3W6+8 Stacheln";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Werden bei der Kaktusernte keine dicken Lederhandschuhe getragen, muss eine FF Probe abgelegt werden. Ansonsten verletzt man sich an den Stacheln und wird vergiftet.";
        }
    }

    public class PflanzeChonchinis : BasisPflanze
    {
        public PflanzeChonchinis()
        {
            this.Name = "Chonchinis";
            this.Bestimmung = 6;
            this.SeiteZBA = 233;
            this.Grundmenge = "W20 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeDisdychonda : BasisPflanze
    {
        public PflanzeDisdychonda()
        {
            this.Name = "Disdychonda";
            this.Bestimmung = 5;
            this.SeiteZBA = 234;
            this.Grundmenge = "4 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Die Disdychonda greift mit ihren Blättern an. Außerdem befindet sich in der Umgebung möglicherweise noch ein Feld von Raubnesseln.";
        }
    }

    public class PflanzeDonf : BasisPflanze
    {
        public PflanzeDonf()
        {
            this.Name = "Donf";
            this.Bestimmung = 6;
            this.SeiteZBA = 234;
            this.Grundmenge = "ein Stängel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
           
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeDornrose : BasisPflanze
    {
        public PflanzeDornrose()
        {
            this.Name = "Dornrose";
            this.Bestimmung = 3;
            this.SeiteZBA = 235;
            this.Grundmenge = "Strauch mit W6 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }
    #endregion

    #region Pflanzen E-G
    public class PflanzeEfeuer : BasisPflanze
    {
        public PflanzeEfeuer()
        {
            this.Name = "Efeuer";
            this.Bestimmung = 4;
            this.SeiteZBA = 235;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Höhle (feucht)", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Höhle (trocken)", (int)EVorkommen.GELEGENTLICH));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (speziell.Equals("Ruine"))
                return 0;
                return base.GetBestimmung(monat, speziell, landschaft);
        }

        public override string GetGefahr()
        {
            return "Efeuer gilt als gefährliches Dornicht (ZBA S.205) und eine Berührung verursacht Schaden.";
        }
    }

    public class PflanzeEgelschreck : BasisPflanze
    {
        public PflanzeEgelschreck()
        {
            this.Name = "Egelschreck";
            this.Bestimmung = 6;
            this.SeiteZBA = 235;
            this.Grundmenge = "2W20 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
        }
    }

    public class PflanzeEitrigerKrötenschemel : BasisPflanze
    {
        public PflanzeEitrigerKrötenschemel()
        {
            this.Name = "Eitriger Krötenschemel";
            this.Bestimmung = 2;
            this.SeiteZBA = 236;
            this.Grundmenge = "2W6 Pilzhäute";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
        }
    }

    public class PflanzeFeuermoosEfferdmoos : BasisPflanze
    {
        public PflanzeFeuermoosEfferdmoos()
        {
            this.Name = "Feuermoos und Efferdmoos";
            this.Bestimmung = 15;
            this.SeiteZBA = 236;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Höhle (feucht)", (int)EVorkommen.GELEGENTLICH));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Die Berührung mit Feuer- bzw. Efferdmoos erzeugt schwere Verätzungen. Die Wirkungen von Feuer- und Efferdmoos heben sich jedoch gegenseitig auf.";
        }
    }

    public class PflanzeFeuerschlick : BasisPflanze
    {
        public PflanzeFeuerschlick()
        {
            this.Name = "Feuerschlick";
            this.Bestimmung = 6;
            this.SeiteZBA = 237;
            this.Grundmenge = "W6 Stein der Algen";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Meer", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (monat.Equals("Rondra") || monat.Equals("Efferd"))
            {
                if (speziell.Equals("Vollmondnacht (+/- 1 Tag)"))
                    return -5;
                else
                    return base.GetBestimmung(monat,speziell,landschaft);
            }
            else
                return base.GetBestimmung(monat, speziell,landschaft);
        }

        public override ArrayList GetVerbreitung(string monat)
        {
            if (monat.Equals("Rondra") || monat.Equals("Efferd"))
            {
                ArrayList erg = new ArrayList();
                erg.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SEHRHAEUFIG));
                erg.Add(new VerbreitungsElementPflanzen("Meer", (int)EVorkommen.SEHRSELTEN));
                return erg;

            }
            else
                return base.GetVerbreitung(monat);
        }
    }

    public class PflanzeFinage : BasisPflanze
    {
        public PflanzeFinage()
        {
            this.Name = "Finage";
            this.Bestimmung = 5;
            this.SeiteZBA = 238;
            this.Grundmenge = "Baum mit W20 Trieben und Bast";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Peraine");
        }

        public override string GetGrundmenge(string monat, int tapstern)
        {
            if (monat.Equals("Boron") || monat.Equals("Hesinde") || monat.Equals("Firun"))
                return "Bast eines Baumes";
            else if (monat.Equals("Peraine"))
                return "Baum mit W20 Trieben";
            else
                return base.GetGrundmenge(monat, tapstern);
        }
    }

    public class PflanzeGrüneSchleimschlange : BasisPflanze
    {
        public PflanzeGrüneSchleimschlange()
        {
            this.Name = "Grüne Schleimschlange";
            this.Bestimmung = 4;
            this.SeiteZBA = 238;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Bei Anblick eines überwucherten Kadavers ist eine MU Probe fällig (vgl. MGS 51/54 Dämoneneigenschaft Schreckgestalt I) und ein Patzer bringt Phobie gegen die Pflanze als permanenten Nachteil.";
        }
    }

    public class PflanzeGulmond : BasisPflanze
    {
        public PflanzeGulmond()
        {
            this.Name = "Gulmond";
            this.Bestimmung = 6;
            this.SeiteZBA = 238;
            this.Grundmenge = "2W6 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }


    #endregion

    #region Pflanzen H-K
    public class PflanzeHiradwurz : BasisPflanze
    {
        public PflanzeHiradwurz()
        {
            this.Name = "Hiradwurz";
            this.Bestimmung = 8;
            this.SeiteZBA = 239;
            this.Grundmenge = "eine Wurzel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (monat.Equals("Efferd") || monat.Equals("Travia") || monat.Equals("Tsa") || monat.Equals("Phex"))
                return 5;
            else
                return base.GetBestimmung(monat, speziell,landschaft);
        }
    }

    public class PflanzeHollbeere : BasisPflanze
    {
        public PflanzeHollbeere()
        {
            this.Name = "Hollbeere";
            this.Bestimmung = 4;
            this.SeiteZBA = 240;
            this.Grundmenge = "2W6 Sträucher mit jeweils 2W6+5 Beeren und 2W6+3 Blätter der untersten Zweige";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.HAEUFIG));

            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
        }
    }

    public class PflanzeHoellenkraut : BasisPflanze
    {
        public PflanzeHoellenkraut()
        {
            this.Name = "Höllenkraut";
            this.Bestimmung = 8;
            this.SeiteZBA = 240;
            this.Grundmenge = "W10 Stein der Ranken";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeHorusche : BasisPflanze
    {
        public PflanzeHorusche()
        {
            this.Name = "Horusche";
            this.Bestimmung = 7;
            this.SeiteZBA = 240;
            this.Grundmenge = "W6 erntereife Schoten mit je W3 Kernen";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeIlmenblatt : BasisPflanze
    {
        public PflanzeIlmenblatt()
        {
            this.Name = "Ilmenblatt";
            this.Bestimmung = 2;
            this.SeiteZBA = 241;
            this.Grundmenge = "W20 Blätter und Blüten";

            //TODO ??: Grundmenge an Harz fehlt in ZBA

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Ingerimm");
        }
    }

    public class PflanzeIribaarslilie : BasisPflanze
    {
        public PflanzeIribaarslilie()
        {
            this.Name = "Iribaarslilie";
            this.Bestimmung = 12;
            this.SeiteZBA = 241;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Die Iribaarslilie verzaubert jeden, der sich ihr nähert und zieht ihn anschließend in die Tiefe.";
        }
    }

    public class PflanzeJagdgras : BasisPflanze
    {
        public PflanzeJagdgras()
        {
            this.Name = "Jagdgras";
            this.Bestimmung = 15;
            this.SeiteZBA = 242;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Jagdgras wandert nachts und läßt sich auf Opfern nieder um seine Wurzeln in sie zu schlagen. Wird manchmal mit Wirselkraut verwechselt, was schlimme Folgen hat.";
        }

    }

    public class PflanzeJoruga : BasisPflanze
    {
        public PflanzeJoruga()
        {
            this.Name = "Joruga";
            this.Bestimmung = 7;
            this.SeiteZBA = 243;
            this.Grundmenge = "eine Wurzel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeKairan : BasisPflanze
    {
        public PflanzeKairan()
        {
            this.Name = "Kairan";
            this.Bestimmung = 6;
            this.SeiteZBA = 243;
            this.Grundmenge = "ein Halm";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeKajubo : BasisPflanze
    {
        public PflanzeKajubo()
        {
            this.Name = "Kajubo";
            this.Bestimmung = 4;
            this.SeiteZBA = 244;
            this.Grundmenge = "2W6 Knospen (Nur die Hälfte um den Strauch zu schonen)";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeKhomMhanadiknolle : BasisPflanze
    {
        public PflanzeKhomMhanadiknolle()
        {
            this.Name = "Khôm- oder Mhanadiknolle";
            this.Bestimmung = 12;
            this.SeiteZBA = 244;
            this.Grundmenge = "eine Wurzel mit W6 Maß klarem Wasser";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (monat.Equals("Praios") || monat.Equals("Rondra") || monat.Equals("Efferd"))
                return 5;
            else
                return base.GetBestimmung(monat, speziell,landschaft);
        }
    }

    public class PflanzeKlippenzahn : BasisPflanze
    {
        public PflanzeKlippenzahn()
        {
            this.Name = "Klippenzahn";
            this.Bestimmung = 8;
            this.SeiteZBA = 245;
            this.Grundmenge = "2W6 Stängel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeKukuka : BasisPflanze
    {
        public PflanzeKukuka()
        {
            this.Name = "Kukuka";
            this.Bestimmung = 10;
            this.SeiteZBA = 245;
            this.Grundmenge = "1W3 x 20 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }
    #endregion

    #region Pflanzen L-M
    public class PflanzeLotusFaerber : BasisPflanze
    {
        public PflanzeLotusFaerber()
        {
            this.Name = "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)";
            this.Bestimmung = 9;
            this.SeiteZBA = 246;
            this.Grundmenge = "2W6+1 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.GELEGENTLICH));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeLotusPurpurner : BasisPflanze
    {
        public PflanzeLotusPurpurner()
        {
            this.Name = "Purpurner Lotus";
            this.Bestimmung = 7;
            this.SeiteZBA = 246;
            this.Grundmenge = "W6+1 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Im Umkreis von 5 Schritt ist eine KO Probe nötig um den giftigen Blütenstaub nicht einzuatmen. Fehlende Punkte entsprechen der Zahl an eingeatmeten Dosen.";
        }
    }

    public class PflanzeLotusSchwarzer : BasisPflanze
    {
        public PflanzeLotusSchwarzer()
        {
            this.Name = "Schwarzer Lotus";
            this.Bestimmung = 6;
            this.SeiteZBA = 246;
            this.Grundmenge = "W6 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Im Umkreis von 5 Schritt ist eine KO Probe nötig um den giftigen Blütenstaub nicht einzuatmen. Fehlende Punkte entsprechen der Zahl an eingeatmeten Dosen.";
        }
    }

    public class PflanzeLotusGrauer : BasisPflanze
    {
        public PflanzeLotusGrauer()
        {
            this.Name = "Grauer Lotus";
            this.Bestimmung = 8;
            this.SeiteZBA = 246;
            this.Grundmenge = "W6+1 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Im Umkreis von 5 Schritt ist eine KO Probe nötig um den giftigen Blütenstaub nicht einzuatmen. Fehlende Punkte entsprechen der Zahl an eingeatmeten Dosen.";
        }
    }

    public class PflanzeLotusWeisser : BasisPflanze
    {
        public PflanzeLotusWeisser()
        {
            this.Name = "Weißer Lotus";
            this.Bestimmung = 10;
            this.SeiteZBA = 247;
            this.Grundmenge = "W6+1 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Im Umkreis von 5 Schritt ist eine KO Probe nötig um den giftigen Blütenstaub nicht einzuatmen. Fehlende Punkte entsprechen der Zahl an eingeatmeten Dosen.";
        }
    }

    public class PflanzeLotusWeissgelber : BasisPflanze
    {
        public PflanzeLotusWeissgelber()
        {
            this.Name = "Weißgelber Lotus";
            this.Bestimmung = 10;
            this.SeiteZBA = 247;
            this.Grundmenge = "W3 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Im Umkreis von 5 Schritt ist eine KO Probe nötig um den giftigen Blütenstaub nicht einzuatmen. Fehlende Punkte entsprechen der Zahl an eingeatmeten Dosen.";
        }
    }

    public class PflanzeLulanie : BasisPflanze
    {
        public PflanzeLulanie()
        {
            this.Name = "Lulanie";
            this.Bestimmung = 5;
            this.SeiteZBA = 248;
            this.Grundmenge = "eine Blüte";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeMadabluete : BasisPflanze
    {
        public PflanzeMadabluete()
        {
            this.Name = "Madablüte";
            this.Bestimmung = 15;
            this.SeiteZBA = 248;
            this.Grundmenge = "eine Blüte";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (speziell.Equals("Vollmondnacht (+/- 1 Tag)"))
                return 5;
            else
                return base.GetBestimmung(monat, speziell,landschaft);
        }
    }

    public class PflanzeMenchalKaktus : BasisPflanze
    {
        public PflanzeMenchalKaktus()
        {
            this.Name = "Menchal-Kaktus";
            this.Bestimmung = 4;
            this.SeiteZBA = 249;
            this.Grundmenge = "ein Kaktus mit W3 Maß Menchalsaft; bei 1 auf W20 außerdem mit W6 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeMerachStrauch : BasisPflanze
    {
        public PflanzeMerachStrauch()
        {
            this.Name = "Merach-Strauch";
            this.Bestimmung = 2;
            this.SeiteZBA = 250;
            this.Grundmenge = "2W20 reife Früchte";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
        }
    }

    public class PflanzeMessergras : BasisPflanze
    {
        public PflanzeMessergras()
        {
            this.Name = "Messergras";
            this.Bestimmung = 6;
            this.SeiteZBA = 250;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Messergras verletzt bei Berührungen und eine Reise durch ein derart bewachsende Gebiet kann tödlich enden.";
        }
    }

    public class PflanzeMibelrohr : BasisPflanze
    {
        public PflanzeMibelrohr()
        {
            this.Name = "Mibelrohr";
            this.Bestimmung = 10;
            this.SeiteZBA = 251;
            this.Grundmenge = "2W6 Kolben";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeMirbelstein : BasisPflanze
    {
        public PflanzeMirbelstein()
        {
            this.Name = "Mirbelstein";
            this.Bestimmung = 8;
            this.SeiteZBA = 251;
            this.Grundmenge = "1 Wurzelknolle";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.HAEUFIG));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeMirhamerSeidenliane : BasisPflanze
    {
        public PflanzeMirhamerSeidenliane()
        {
            this.Name = "Mirhamer Seidenliane";
            this.Bestimmung = 4;
            this.SeiteZBA = 251;
            this.Grundmenge = "eine Ranke mit W2+1 Knoten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGrundmenge(string monat, int tapstern)
        {
            if (monat.Equals("Tsa") || monat.Equals("Phex") || monat.Equals("Peraine") || monat.Equals("Ingerimm"))
                return "eine Ranke mit W2+1 Knoten";
            else if (monat.Equals("Komplettes Jahr"))
                return base.GetGrundmenge(monat, tapstern);
            else
                return "eine Ranke";
        }
    }

    public class PflanzeMohnWeisser : BasisPflanze
    {
        public PflanzeMohnWeisser()
        {
            this.Name = "Bleichmohn (Weißer Mohn)";
            this.Bestimmung = 5;
            this.SeiteZBA = 252;
            this.Grundmenge = "W6 geschlossene Samenkapseln";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Rondra");
        }
    }

    public class PflanzeMohnBunter : BasisPflanze
    {
        public PflanzeMohnBunter()
        {
            this.Name = "Bunter Mohn";
            this.Bestimmung = -5;
            this.SeiteZBA = 252;
            this.Grundmenge = "eine geschlossene Samenkapsel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Travia");
        }
    }

    public class PflanzeMohnGrauer : BasisPflanze
    {
        public PflanzeMohnGrauer()
        {
            this.Name = "Grauer Mohn";
            this.Bestimmung = 1;
            this.SeiteZBA = 253;
            this.Grundmenge = "eine geschlossene Samenkapsel und eine Blüte";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGrundmenge(string monat, int tapstern)
        {
            if (monat.Equals("Rondra"))
                return "eine geschlossene Samenkapsel und eine Blüte";
            else if (monat.Equals("Komplettes Jahr"))
                return base.GetGrundmenge(monat, tapstern);
            else
                return "eine Blüte";
        }
    }

    public class PflanzeMohnPurpur : BasisPflanze
    {
        public PflanzeMohnPurpur()
        {
            this.Name = "Purpurmohn";
            this.Bestimmung = 3;
            this.SeiteZBA = 253;
            this.Grundmenge = "eine geschlossene Samenkapsel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Rahja");
        }
    }

    public class PflanzeMohnSchwarzer : BasisPflanze
    {
        public PflanzeMohnSchwarzer()
        {
            this.Name = "Schwarzer Mohn";
            this.Bestimmung = 5;
            this.SeiteZBA = 253;
            this.Grundmenge = "2 Blätter und eine geschlossene Samenkapsel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRHAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SEHRHAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRHAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRHAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRHAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRHAEUFIG));

            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
        }
    }

    public class PflanzeMohnTiger : BasisPflanze
    {
        public PflanzeMohnTiger()
        {
            this.Name = "Tigermohn";
            this.Bestimmung = 10;
            this.SeiteZBA = 254;
            this.Grundmenge = "eine geschlossene Samenkapsel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Travia");
        }
    }

    public class PflanzeMorgendornstrauch : BasisPflanze
    {
        public PflanzeMorgendornstrauch()
        {
            this.Name = "Morgendornstrauch";
            this.Bestimmung = 13;
            this.SeiteZBA = 254;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Die Berührung einer Blüte des Morgendornstrauchs verwandelt denjenigen binnen einer Woche in eine Sumpfranze.";
        }
    }
    #endregion

    #region Pflanzen N-R
    public class PflanzeNaftanstaude : BasisPflanze
    {
        public PflanzeNaftanstaude()
        {
            this.Name = "Naftanstaude";
            this.Bestimmung = 1;
            this.SeiteZBA = 255;
            this.Grundmenge = "eine Staude";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Der Saft der Naftanstaude ist stark ätzend und kann nur mit einer FF+2 Probe gefahrlos geerntet werden.";
        }

        public override ArrayList GetErntezeit(string region)
        {
            if (region.Equals("Südländische Grasländer und Steppen") || region.Equals("Wüstenrandgebiete"))
            {
                ArrayList erg = new ArrayList();
                erg.Add("Praios");
                erg.Add("Rondra");
                erg.Add("Efferd");
                erg.Add("Travia");
                erg.Add("Boron");
                erg.Add("Hesinde");
                erg.Add("Firun");
                erg.Add("Tsa");
                erg.Add("Phex");
                erg.Add("Peraine");
                erg.Add("Ingerimm");
                erg.Add("Rahja");
                erg.Add("Namenlose Tage");
                return erg;
            }
            else
                return base.GetErntezeit(region);
        }

        public override ArrayList GetVerbreitung(string monat)
        {
            if (monat.Equals("Boron") || monat.Equals("Hesinde") || monat.Equals("Firun"))
            {
                ArrayList erg = new ArrayList();
                erg.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SELTEN));
                return erg;
            }
            else
                return base.GetVerbreitung(monat);
        }
    }

    public class PflanzeNeckerkraut : BasisPflanze
    {
        public PflanzeNeckerkraut()
        {
            this.Name = "Neckerkraut";
            this.Bestimmung = 4;
            this.SeiteZBA = 255;
            this.Grundmenge = "W20+5 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeNothilf : BasisPflanze
    {
        public PflanzeNothilf()
        {
            this.Name = "Nothilf";
            this.Bestimmung = 6;
            this.SeiteZBA = 256;
            this.Grundmenge = "W20+2 Blüten und 2W20+10 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Peraine");
        }
    }

    public class PflanzeOlginwurz : BasisPflanze
    {
        public PflanzeOlginwurz()
        {
            this.Name = "Olginwurz";
            this.Bestimmung = 10;
            this.SeiteZBA = 256;
            this.Grundmenge = "W3 Moosballen";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeOrazal : BasisPflanze
    {
        public PflanzeOrazal()
        {
            this.Name = "Orazal";
            this.Bestimmung = 4;
            this.SeiteZBA = 257;
            this.Grundmenge = "W6 verholzte Stängel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "In großer Hitze kann sich Orazal so sehr aufheizen, dass die Pflanze bei Berührung auf der Haut festklebt und beim Ablösen die Haut verletzt.";
        }
    }

    public class PflanzeOrklandBovist : BasisPflanze
    {
        public PflanzeOrklandBovist()
        {
            this.Name = "Orklandbovist";
            this.Bestimmung = 4;
            this.SeiteZBA = 258;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "In den Monaten Ingerimm, Rahja, Praios und Rondra ist der Orklandbovist gefährlich. Platzt er auf, so kann man sich in 5 Schritt Umkreis nur durch eine Athletik-Probe +15 in Deckung bringen. Andernfalls muss eine KO Probe klären ob man die Pilzsporen eingeatmet hat.";
        }
    }

    public class PflanzePestsporenpilz : BasisPflanze
    {
        public PflanzePestsporenpilz()
        {
            this.Name = "Pestsporenpilz";
            this.Bestimmung = 6;
            this.SeiteZBA = 258;
            this.Grundmenge = "eine Pilzhaut";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");     
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
        }

        public override string GetGefahr()
        {
            return "Wird beim Ernten die Haut des Pestsporenpilzes nicht vorsichtig abgelöst (FF+2 Probe) oder stolpert man versehentlich über einen Pilz (GE+2 Probe) so setzt der Pilz eine giftige Wolke frei.";
        }
    }

    public class PflanzePhosphorpilz : BasisPflanze
    {
        public PflanzePhosphorpilz()
        {
            this.Name = "Phosphorpilz";
            this.Bestimmung = 10;
            this.SeiteZBA = 259;
            this.Grundmenge = "W6 Stein Geflechtstücke";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Höhle (feucht)", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Höhle (trocken)", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (landschaft.Equals("Höhle (feucht)"))
                return -3;
            else
                return base.GetBestimmung(monat, speziell, landschaft);
        }
    }

    public class PflanzeQuasselwurz : BasisPflanze
    {
        public PflanzeQuasselwurz()
        {
            this.Name = "Quasselwurz";
            this.Bestimmung = 12;
            this.SeiteZBA = 259;
            this.Grundmenge = "eine Wurzel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeQuinja : BasisPflanze
    {
        public PflanzeQuinja()
        {
            this.Name = "Quinja";
            this.Bestimmung = 6;
            this.SeiteZBA = 260;
            this.Grundmenge = "W3+2 Beeren";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Verwechslung mit Scheinquinja möglich (zusätzliche Pflanzenkunde-Probe +8) welcher leicht giftig ist.";
        }
    }

    public class PflanzeRahjalieb : BasisPflanze
    {
        public PflanzeRahjalieb()
        {
            this.Name = "Rahjalieb";
            this.Bestimmung = 5;
            this.SeiteZBA = 260;
            this.Grundmenge = "2W6 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeRattenpilz : BasisPflanze
    {
        public PflanzeRattenpilz()
        {
            this.Name = "Rattenpilz";
            this.Bestimmung = 7;
            this.SeiteZBA = 260;
            this.Grundmenge = "ein Pilz";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (speziell.Equals("Stätte Namenloser Macht"))
                return -7;
            else
                return base.GetBestimmung(monat, speziell, landschaft);
        }

        public override string GetGefahr()
        {
            return "Der Rattenpilz verströmt eine magische Anziehungskraft auf jeden Wanderer.";
        }
    }

    public class PflanzeRauschgurke : BasisPflanze
    {
        public PflanzeRauschgurke()
        {
            this.Name = "Rauschgurke";
            this.Bestimmung = 3;
            this.SeiteZBA = 261;
            this.Grundmenge = "3W6 reife Rauschgurken";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeRotePfeilbluete : BasisPflanze
    {
        public PflanzeRotePfeilbluete()
        {
            this.Name = "Rote Pfeilblüte";
            this.Bestimmung = 7;
            this.SeiteZBA = 261;
            this.Grundmenge = "W6 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
        }
    }

    public class PflanzeRoterDrachenschlund : BasisPflanze
    {
        public PflanzeRoterDrachenschlund()
        {
            this.Name = "Roter Drachenschlund";
            this.Bestimmung = 10;
            this.SeiteZBA = 262;
            this.Grundmenge = "W6 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (monat.Equals("Rahja") || monat.Equals("Ingerimm"))
                return 3;
            else
                return base.GetBestimmung(monat, speziell, landschaft);
        }
    }
    #endregion

    #region Pflanzen S-T
    public class PflanzeSansaro : BasisPflanze
    {
        public PflanzeSansaro()
        {
            this.Name = "Sansaro";
            this.Bestimmung = 12;
            this.SeiteZBA = 262;
            this.Grundmenge = "eine Pflanze";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Meer", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override ArrayList GetVerbreitung(string monat)
        {
            if (monat.Equals("Boron") || monat.Equals("Hesinde") || monat.Equals("Firun"))
            {
                ArrayList erg = new ArrayList();
                erg.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SELTEN));
                return erg;
            }
            else
                return base.GetVerbreitung(monat);
        }
    }

    public class PflanzeSatuariensbusch : BasisPflanze
    {
        public PflanzeSatuariensbusch()
        {
            this.Name = "Satuariensbusch";
            this.Bestimmung = -2;
            this.SeiteZBA = 263;
            this.Grundmenge = "4W20 Blätter, W20 Blüten, W20 Früchte, W3 Flux Saft";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.GELEGENTLICH));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGrundmenge(string monat, int tapstern)
        {
            string ergebnis = "";

            if (monat.Equals("Ingerimm") || monat.Equals("Rahja") || monat.Equals("Namenlose Tage") || monat.Equals("Praios"))
            {
                if (!ergebnis.Equals(""))
                    ergebnis += ", ";
                ergebnis += "4W20 Blätter";
            }
            if (monat.Equals("Ingerimm") || monat.Equals("Rahja"))
            {
                if (!ergebnis.Equals(""))
                    ergebnis += ", ";
                ergebnis += "W20 Blüten";
            }
            if (monat.Equals("Efferd") || monat.Equals("Travia"))
            {
                if (!ergebnis.Equals(""))
                    ergebnis += ", ";
                ergebnis += "W20 Früchte";
            }
            if (monat.Equals("Phex") || monat.Equals("Peraine") || monat.Equals("Ingerimm") || monat.Equals("Rahja") || monat.Equals("Namenlose Tage") || monat.Equals("Praios"))
            {
                if (!ergebnis.Equals(""))
                    ergebnis += ", ";
                ergebnis += "W3 Flux Saft";
            }

            if (monat.Equals("Komplettes Jahr"))
                return base.GetGrundmenge(monat, tapstern);
            else
                return ergebnis;
        }
    }

    public class PflanzeSchlangenzuenglein : BasisPflanze
    {
        public PflanzeSchlangenzuenglein()
        {
            this.Name = "Schlangenzünglein";
            this.Bestimmung = 3;
            this.SeiteZBA = 263;
            this.Grundmenge = "Saft einer Pflanze";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeSchleichenderTod : BasisPflanze
    {
        public PflanzeSchleichenderTod()
        {
            this.Name = "Schleichender Tod";
            this.Bestimmung = 6;
            this.SeiteZBA = 264;
            this.Grundmenge = "W6 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
        }
    }

    public class PflanzeSchleimigerSumpfknoeterich : BasisPflanze
    {
        public PflanzeSchleimigerSumpfknoeterich()
        {
            this.Name = "Schleimiger Sumpfknöterich";
            this.Bestimmung = 3;
            this.SeiteZBA = 264;
            this.Grundmenge = "2W6 Pilze";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
        }

        public override string GetGefahr()
        {
            return "Die Berührung mit bloßer Haut verursacht 3 SP pro Pilz.";
        }
    }

    public class PflanzeSchlinggras : BasisPflanze
    {
        public PflanzeSchlinggras()
        {
            this.Name = "Schlinggras";
            this.Bestimmung = 12;
            this.SeiteZBA = 265;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Eine IN Probe klärt ob man rechtzeitig auf das Schlinggras aufmerksam wird um es zu umgehen. Andernfalls versucht die Pflanze einen zu packen und ins Moor zu ziehen.";
        }
    }

    public class PflanzeSchwarmschwamm : BasisPflanze
    {
        public PflanzeSchwarmschwamm()
        {
            this.Name = "Schwarmschwamm";
            this.Bestimmung = 3;
            this.SeiteZBA = 265;
            this.Grundmenge = "ein Schwamm und W2 Samenkörper";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeSchwarzerWein : BasisPflanze
    {
        public PflanzeSchwarzerWein()
        {
            this.Name = "Schwarzer Wein";
            this.Bestimmung = 2;
            this.SeiteZBA = 266;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.HAEUFIG));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGrundmenge(string monat, int tapstern)
        {
            if (tapstern >= 7)
                return "7W6 Beeren";
            else
                return base.GetGrundmenge(monat, tapstern);
        }

        public override string GetGefahr()
        {
            return "Schwarzer Wein bildet nur dann Früchte aus, wenn zuvor Menschen ausgesaugt wurden. Außerdem sind die Ranken gefährlich und giftig. Nur bei mehr als 7 TaP* kann man einige Beeren finden ohne zuvor Menschen opfern zu müssen.";
        }
    }

    public class PflanzeShurinstrauch : BasisPflanze
    {
        public PflanzeShurinstrauch()
        {
            this.Name = "Shurinstrauch";
            this.Bestimmung = 2;
            this.SeiteZBA = 267;
            this.Grundmenge = "W20 Knollen";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");;
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeTalaschin : BasisPflanze
    {
        public PflanzeTalaschin()
        {
            this.Name = "Talaschin";
            this.Bestimmung = 5;
            this.SeiteZBA = 268;
            this.Grundmenge = "W6 Flechten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Eis", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeTarnblatt : BasisPflanze
    {
        public PflanzeTarnblatt()
        {
            this.Name = "Tarnblatt";
            this.Bestimmung = 8;
            this.SeiteZBA = 268;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Tarnblatt ist leicht giftig und gibt sich je nach Jahreszeit als eine andere Pflanze aus.";
        }
    }

    public class PflanzeTarnele : BasisPflanze
    {
        public PflanzeTarnele()
        {
            this.Name = "Tarnele";
            this.Bestimmung = 4;
            this.SeiteZBA = 268;
            this.Grundmenge = "eine Pflanze";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeThonnys : BasisPflanze
    {
        public PflanzeThonnys()
        {
            this.Name = "Thonnys";
            this.Bestimmung = 12;
            this.SeiteZBA = 269;
            this.Grundmenge = "W6+4 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            //Quelle DSA3 Herbarium S.58, zweiwöchige Blüte im Rahja
            if (monat.Equals("Rahja"))
                return 5;
            else
                return base.GetBestimmung(monat, speziell, landschaft);
        }
    }

    public class PflanzeTraschbart : BasisPflanze
    {
        public PflanzeTraschbart()
        {
            this.Name = "Traschbart";
            this.Bestimmung = 6;
            this.SeiteZBA = 269;
            this.Grundmenge = "W6 Flechten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.HAEUFIG));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeTrichterwurzel : BasisPflanze
    {
        public PflanzeTrichterwurzel()
        {
            this.Name = "Trichterwurzel";
            this.Bestimmung = 11;
            this.SeiteZBA = 270;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Nur eine Sinnesschärfe-Probe +8 erlaubt es die Grube der Trichterwurzel rechtzeitig zu erkennen. Andernfalls fällt man in diese hinein und wird zusätzlich von Wurzeln attackiert.";
        }
    }

    public class PflanzeTuurAmashKelch : BasisPflanze
    {
        public PflanzeTuurAmashKelch()
        {
            this.Name = "Tuur-Amash-Kelch";
            this.Bestimmung = 1;
            this.SeiteZBA = 270;
            this.Grundmenge = "W6+3 Kelche";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGrundmenge(string monat, int tapstern)
        {
            if (tapstern >= 13)
                return "W6+3 Kelche und eine Beere";
            else
                return base.GetGrundmenge(monat, tapstern);
        }

        public override string GetGefahr()
        {
            return "Nur eine Sinnesschärfe-Probe +7 erlaubt es den Tuur-Amash-Kelch rechtzeitig zu entdecken. Andernfalls greift eine Pflanze an und kurz darauf weitere. Nur bei mehr als 13 TaP* findet sich auch eine Beere.";
        }
    }
    #endregion

    #region Pflanzen U-Z
    public class PflanzeUlmenwürger : BasisPflanze
    {
        public PflanzeUlmenwürger()
        {
            this.Name = "Ulmenwürger";
            this.Bestimmung = 2;
            this.SeiteZBA = 271;
            this.Grundmenge = "W20 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
        }
    }

    public class PflanzeVierblatt : BasisPflanze
    {
        public PflanzeVierblatt()
        {
            this.Name = "Vierblättrige Einbeere";
            this.Bestimmung = 5;
            this.SeiteZBA = 271;
            this.Grundmenge = "W6 Beeren";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Eis", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.HAEUFIG));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeVragieswurzel : BasisPflanze
    {
        public PflanzeVragieswurzel()
        {
            this.Name = "Vragieswurzel";
            this.Bestimmung = 6;
            this.SeiteZBA = 272;
            this.Grundmenge = "eine Wurzel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
        }

        public override ArrayList GetErntezeit(string region)
        {
            if (region.Equals("Regenwald") || region.Equals("Südliche Regengebirge"))
            {
                ArrayList erg = new ArrayList();
                erg.Add("Efferd");
                erg.Add("Travia");
                erg.Add("Boron");
                return erg;
            }
            else
                return base.GetErntezeit(region);
        }
    }

    public class PflanzeWaldwebe : BasisPflanze
    {
        public PflanzeWaldwebe()
        {
            this.Name = "Waldwebe";
            this.Bestimmung = 9;
            this.SeiteZBA = 272;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Sofern keine besonderne Umstände eine leichte Erkennung erlauben, ist eine Sinnensschärfe-Probe +12 nötig um das Netz der Waldwebe zu erkennen. Andernfalls verfängt man sich im Netz.";
        }
    }

    public class PflanzeWasserrausch : BasisPflanze
    {
        public PflanzeWasserrausch()
        {
            this.Name = "Wasserrausch";
            this.Bestimmung = 1;
            this.SeiteZBA = 273;
            this.Grundmenge = "2W20 Blüten";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SELTEN));
           
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGrundmenge(string monat, int tapstern)
        {
            if (monat.Equals("Rahja") || monat.Equals("Namenlose Tage") || monat.Equals("Praios") || monat.Equals("Rondra") || monat.Equals("Efferd"))
            {
                if ((monat.Equals("Rondra") || monat.Equals("Efferd") || monat.Equals("Travia")) && tapstern >= 12)
                    return "2W20 Blüten und eine Frucht";
                else
                    return "2W20 Blüten";
            }
            else if ((monat.Equals("Travia")) && tapstern >= 12)
                return "eine Frucht";
            else if ((monat.Equals("Komplettes Jahr")) && tapstern >= 12)
                return "2W20 Blüten und eine Frucht";
            else
                return base.GetGrundmenge(monat, tapstern);
        }

        public override string GetGefahr()
        {
            return "Im Umkreis von 5 Metern um die Blüten des Wasserrausches ist eine KO+5 Probe nötig. Andernfalls fällt man in berauschende Träume, was für eine Schwimmer den Tod bedeuten kann. Nur bei mehr als 12 TaP* findet sich auch eine Frucht.";
        }
    }

    public class PflanzeWinselgras : BasisPflanze
    {
        public PflanzeWinselgras()
        {
            this.Name = "Winselgras";
            this.Bestimmung = 12;
            this.SeiteZBA = 273;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override int GetBestimmung(string monat, string speziell, string landschaft)
        {
            if (speziell.Equals("Nacht") || speziell.Equals("Vollmondnacht (+/- 1 Tag)"))
                return -2;
            else
                return base.GetBestimmung(monat, speziell, landschaft);
        }

        public override string GetGefahr()
        {
            return "Das Heulen des Winselgrases kann einen um den Schlaf bringen und vermindert die nächtliche Regeneration um 2 Punkte.";
        }
    }

    public class PflanzeWirselkraut : BasisPflanze
    {
        public PflanzeWirselkraut()
        {
            this.Name = "Wirselkraut";
            this.Bestimmung = 5;
            this.SeiteZBA = 273;
            this.Grundmenge = "W6+4 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.GELEGENTLICH));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeWuergedattel : BasisPflanze
    {
        public PflanzeWuergedattel()
        {
            this.Name = "Würgedattel";
            this.Bestimmung = 5;
            this.SeiteZBA = 274;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }

        public override string GetGefahr()
        {
            return "Wer eine Frucht der Würgedattel berührt, wird von den Würgeschlingen der Pflanze attackiert.";
        }
    }

    public class PflanzeYaganstrauch : BasisPflanze
    {
        public PflanzeYaganstrauch()
        {
            this.Name = "Yaganstrauch";
            this.Bestimmung = 6;
            this.SeiteZBA = 274;
            this.Grundmenge = "W6 Nüsse";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Boron");
        }
    }

    public class PflanzeZithabar : BasisPflanze
    {
        public PflanzeZithabar()
        {
            this.Name = "Zithabar";
            this.Bestimmung = 5;
            this.SeiteZBA = 275;
            this.Grundmenge = "3W20 Blätter";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.HAEUFIG));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeZunderschwamm : BasisPflanze
    {
        public PflanzeZunderschwamm()
        {
            this.Name = "Zunderschwamm";
            this.Bestimmung = 4;
            this.SeiteZBA = 275;
            this.Grundmenge = "W6 Pilze";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.HAEUFIG));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }

    public class PflanzeZwoelfblatt : BasisPflanze
    {
        public PflanzeZwoelfblatt()
        {
            this.Name = "Zwölfblatt";
            this.Bestimmung = 5;
            this.SeiteZBA = 276;
            this.Grundmenge = "12 Stängel";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.GELEGENTLICH));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.GELEGENTLICH));
            
            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }
    #endregion

    #region Musterpflanze
    public class PflanzeMuster : BasisPflanze
    {
        public PflanzeMuster()
        {
            this.Name = "Musterpflanze";
            this.Bestimmung = 0;
            this.SeiteZBA = 2;
            this.Grundmenge = "";

            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Eis", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wüste und Wüstenrand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Gebirge", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Hochland", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Steppe", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Grasland, Wiesen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Fluss- und Seeufer, Teiche", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Küste, Strand", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Flussauen", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Sumpf und Moor", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Regenwald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Wald", (int)EVorkommen.SEHRSELTEN));
            this.Verbreitung.Add(new VerbreitungsElementPflanzen("Waldrand", (int)EVorkommen.SEHRSELTEN));

            this.Erntezeit.Add("Praios");
            this.Erntezeit.Add("Rondra");
            this.Erntezeit.Add("Efferd");
            this.Erntezeit.Add("Travia");
            this.Erntezeit.Add("Boron");
            this.Erntezeit.Add("Hesinde");
            this.Erntezeit.Add("Firun");
            this.Erntezeit.Add("Tsa");
            this.Erntezeit.Add("Phex");
            this.Erntezeit.Add("Peraine");
            this.Erntezeit.Add("Ingerimm");
            this.Erntezeit.Add("Rahja");
            this.Erntezeit.Add("Namenlose Tage");
        }
    }
    #endregion

}
