using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
// Eigene Usings
using MeisterGeister.ViewModel.ZooBot.Logic.Pflanzen;

namespace MeisterGeister.ViewModel.ZooBot.Logic.Regionen
{
    public abstract class BasisRegion
    {
        private string m_Name;
        private ArrayList m_Tiere = new ArrayList();
        private ArrayList m_Pflanzen = new ArrayList();
        private ArrayList m_Landschaften = new ArrayList();
        private int m_EssbarePflanzen;
        private int m_Wildvorkommen;

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public ArrayList Tiere
        {
            get { return this.m_Tiere; }
        }

        public ArrayList Pflanzen
        {
            get { return this.m_Pflanzen; }
        }

        public ArrayList Landschaften
        {
            get { return this.m_Landschaften; }
        }

        public int EssbarePflanzen
        {
            get { return this.m_EssbarePflanzen; }
            set { this.m_EssbarePflanzen = value; }
        }

        public int Wildvorkommen
        {
            get { return this.m_Wildvorkommen; }
            set { this.m_Wildvorkommen = value; }
        }

    }

    public class VerbreitungsElementTiere
    {
        private string m_tier;
        private int m_vorkommen;

        public VerbreitungsElementTiere(string t, int v)
        {
            this.Tier = t;
            this.Vorkommen = v;
        }

        public string Tier
        {
            get { return this.m_tier; }
            set { this.m_tier = value; }
        }

        public int Vorkommen
        {
            get { return this.m_vorkommen; }
            set { this.m_vorkommen = value; }
        }
    }

    public class RegionEwigesEis : BasisRegion
    {
        public RegionEwigesEis()
        {
            this.Name = "Ewiges Eis";
            this.Landschaften.Add("Eis");
            this.EssbarePflanzen = (int)EVorkommen.KEINE;
            this.Wildvorkommen = (int)EVorkommen.SEHRSELTEN;

            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Rattenpilz");

            this.Tiere.Add(new VerbreitungsElementTiere(("Felsrobbe"),(int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firnyak"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firnluchs"),(int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firunsb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Mammut"),(int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Mastodon"),(int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Meerkalb"),(int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schneedachs"), (int)EVorkommen.GELEGENTLICH));            
            this.Tiere.Add(new VerbreitungsElementTiere(("Seetiger"),(int)EVorkommen.SELTEN));
        }
    }

    public class RegionNoerdlicheTundra : BasisRegion
    {
        public RegionNoerdlicheTundra()
        {
            this.Name = "N�rdliche Tundra";
            this.EssbarePflanzen = (int)EVorkommen.SELTEN;
            this.Wildvorkommen = (int)EVorkommen.SELTEN;

            this.Landschaften.Add("Eis");
            this.Landschaften.Add("Steppe");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");

            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Bunter Mohn");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Talaschin");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Wirselkraut");

            this.Tiere.Add(new VerbreitungsElementTiere(("Felsrobbe"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Karen"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Elch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firnluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firnyak"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schneedachs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Blaufuchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firunsb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Mammut"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Mastodon"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Meerkalb"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Seetiger"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Steppenhund"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Steppentiger"), (int)EVorkommen.SEHRSELTEN));

        }
    }

    public class RegionNoerdlicheGraslaenderUndSteppen : BasisRegion
    {
        public RegionNoerdlicheGraslaenderUndSteppen()
        {
            this.Name = "N�rdliche Grasl�nder und Steppen";
            this.EssbarePflanzen = (int)EVorkommen.GELEGENTLICH;
            this.Wildvorkommen = (int)EVorkommen.GELEGENTLICH;

            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Steppe");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Donf");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gr�ne Schleimschlange");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Klippenzahn");
            this.Pflanzen.Add("Madabl�te");
            this.Pflanzen.Add("Messergras");
            this.Pflanzen.Add("Bunter Mohn");
            this.Pflanzen.Add("Naftanstaude");
            this.Pflanzen.Add("Orklandbovist");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Thonnys");
            this.Pflanzen.Add("Tigermohn");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Karen"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Steppenhund"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Gelbfuchs"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Elch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firunshirsch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Klippechse"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schneedachs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Steppenrind"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Trappe"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Blaufuchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Borkenb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firnluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Halmar-Antilope"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Mammut"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Orklandb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Steppentiger"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Vielfra�"), (int)EVorkommen.SELTEN));
        }
    }

    public class RegionNoerdlichesHochland : BasisRegion
    {
        public RegionNoerdlichesHochland()
        {
            this.Name = "N�rdliches Hochland";
            this.EssbarePflanzen = (int)EVorkommen.GELEGENTLICH;
            this.Wildvorkommen = (int)EVorkommen.GELEGENTLICH;

            this.Landschaften.Add("Gebirge");
            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Basilamine");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gr�ne Schleimschlange");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Klippenzahn");
            this.Pflanzen.Add("Messergras");
            this.Pflanzen.Add("Bunter Mohn");
            this.Pflanzen.Add("Naftanstaude");
            this.Pflanzen.Add("Orklandbovist");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Orklandkarnickel"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Gelbfuchs"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Halmar-Antilope"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Trappe"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Klippechse"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Orklandb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotfuchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schreckkatze"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sonnenluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Borkenb�r"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Mammut"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Steppentiger"), (int)EVorkommen.SEHRSELTEN));
            
        }
    }

    public class RegionKalkgebirge : BasisRegion
    {
        public RegionKalkgebirge()
        {
            this.Name = "Kalkgebirge";
            this.EssbarePflanzen = (int)EVorkommen.SELTEN;
            this.Wildvorkommen = (int)EVorkommen.SELTEN;

            this.Landschaften.Add("Gebirge");
            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            this.Landschaften.Add("H�hle (feucht)");
            this.Landschaften.Add("H�hle (trocken)");

            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Atan-Kiefer");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Feuermoos und Efferdmoos");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Madabl�te");
            this.Pflanzen.Add("Bleichmohn (Wei�er Mohn)");
            this.Pflanzen.Add("Grauer Mohn");
            this.Pflanzen.Add("Nothilf");
            this.Pflanzen.Add("Phosphorpilz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Talaschin");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Thonnys");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Vragieswurzel");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Gebirgsbock"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Bergl�we"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("H�hlenb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sonnenluchs"), (int)EVorkommen.SEHRSELTEN));           
        }
    }

    public class RegionAndereMittellaendischeGebirge : BasisRegion
    {
        public RegionAndereMittellaendischeGebirge()
        {
            this.Name = "Andere Mittell�ndische Gebirge";
            this.EssbarePflanzen = (int)EVorkommen.SELTEN;
            this.Wildvorkommen = (int)EVorkommen.SELTEN;

            this.Landschaften.Add("Gebirge");
            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            this.Landschaften.Add("H�hle (feucht)");
            this.Landschaften.Add("H�hle (trocken)");

            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Feuermoos und Efferdmoos");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Madabl�te");
            this.Pflanzen.Add("Bleichmohn (Wei�er Mohn)");
            this.Pflanzen.Add("Grauer Mohn");
            this.Pflanzen.Add("Nothilf");
            this.Pflanzen.Add("Phosphorpilz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Schleimiger Sumpfkn�terich");
            this.Pflanzen.Add("Talaschin");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Thonnys");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Vragieswurzel");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Gebirgsbock"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenl�ffler"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Bergl�we"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sonnenluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("H�hlenb�r"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.SEHRSELTEN));
        }
    }

    public class RegionNoerdlicheWaelderWestkueste : BasisRegion
    {
        public RegionNoerdlicheWaelderWestkueste()
        {
            this.Name = "N�rdliche W�lder (Westk�ste)";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.HAEUFIG;

            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Basilamine");
            this.Pflanzen.Add("Belmart");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Carlog");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Hollbeere");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Klippenzahn");
            this.Pflanzen.Add("Mibelrohr");
            this.Pflanzen.Add("Orklandbovist");
            this.Pflanzen.Add("Pestsporenpilz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Thonnys");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Ulmenw�rger");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildschwein"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerhahn"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Streifendachs"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fasan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Kronenhirsch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotfuchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerochse"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Baumb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Blaufuchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Borkenb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Vielfra�"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Waldl�we"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("H�hlenb�r"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Silberl�we"), (int)EVorkommen.SEHRSELTEN));            
        }
    }

    public class RegionNoerdlicheWaelderTaiga : BasisRegion
    {
        public RegionNoerdlicheWaelderTaiga()
        {
            this.Name = "N�rdliche W�lder (Taiga)";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.SEHRHAEUFIG;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");

            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Belmart");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Nothilf");
            this.Pflanzen.Add("Pestsporenpilz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Satuariensbusch");
            this.Pflanzen.Add("Schleimiger Sumpfkn�terich");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Thonnys");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Ulmenw�rger");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildschwein"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fasan"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Elch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schneedachs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Blaufuchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firunshirsch"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Karen"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Kronenhirsch"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schwarzb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sonnenluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Streifendachs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Vielfra�"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Waldl�we"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerochse"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Borkenb�r"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotfuchs"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Silberl�we"), (int)EVorkommen.SEHRSELTEN));           
        }
    }

    public class RegionNoerdlicheWaelderBornland : BasisRegion
    {
        public RegionNoerdlicheWaelderBornland()
        {
            this.Name = "N�rdliche W�lder (Bornland)";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.SEHRHAEUFIG;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");

            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Belmart");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Nothilf");
            this.Pflanzen.Add("Pestsporenpilz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Satuariensbusch");
            this.Pflanzen.Add("Schleimiger Sumpfkn�terich");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Thonnys");
            this.Pflanzen.Add("Ulmenw�rger");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Wasserrausch");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Silberbock"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildschwein"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Blaufuchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Elch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Kronenhirsch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sonnenluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Streifendachs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sumpfranze"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerochse"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Firunshirsch"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("H�hlenb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schneedachs"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schwarzb�r"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Vielfra�"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Waldl�we"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.SEHRSELTEN));
        }
    }

    public class RegionNoerdlicheSuempfeMarschenMoore : BasisRegion
    {
        public RegionNoerdlicheSuempfeMarschenMoore()
        {
            this.Name = "N�rdliche S�mpfe, Marschen und Moore";
            this.EssbarePflanzen = (int)EVorkommen.GELEGENTLICH;
            this.Wildvorkommen = (int)EVorkommen.SELTEN;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Sumpf und Moor");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Carlog");
            this.Pflanzen.Add("Donf");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gr�ne Schleimschlange");
            this.Pflanzen.Add("Iribaarslilie");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Mibelrohr");
            this.Pflanzen.Add("Morgendornstrauch");
            this.Pflanzen.Add("Neckerkraut");
            this.Pflanzen.Add("Pestsporenpilz");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Schleimiger Sumpfkn�terich");
            this.Pflanzen.Add("Schlinggras");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Klippechse"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sumpfranze"), (int)EVorkommen.GELEGENTLICH));
        }
    }

    public class RegionMittellaendischeGraslaenderHeideSteppe : BasisRegion
    {
        public RegionMittellaendischeGraslaenderHeideSteppe()
        {
            this.Name = "Mittell�ndische Grasl�nder, Heide und Steppe";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.HAEUFIG;

            this.Landschaften.Add("Steppe");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Donf");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Hiradwurz");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("F�rberlotus (Gelber, Blauer, Roter und Rosa Lotus)");
            this.Pflanzen.Add("Purpurner Lotus");
            this.Pflanzen.Add("Schwarzer Lotus");
            this.Pflanzen.Add("Grauer Lotus");
            this.Pflanzen.Add("Wei�er Lotus");
            this.Pflanzen.Add("Wei�gelber Lotus");
            this.Pflanzen.Add("Lulanie");
            this.Pflanzen.Add("Messergras");
            this.Pflanzen.Add("Mibelrohr");
            this.Pflanzen.Add("Mirbelstein");
            this.Pflanzen.Add("Bunter Mohn");
            this.Pflanzen.Add("Purpurmohn");
            this.Pflanzen.Add("Schwarzer Mohn");
            this.Pflanzen.Add("Tigermohn");
            this.Pflanzen.Add("Naftanstaude");
            this.Pflanzen.Add("Neckerkraut");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Schlangenz�nglein");
            this.Pflanzen.Add("Schwarmschwamm");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Winselgras");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("G�nseluchs"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Trappe"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fasan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Gelbfuchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Gabelantilope"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sonnenluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Steppentiger"), (int)EVorkommen.SEHRSELTEN));
        }
    }

    public class RegionMittellaendischeWaelderGemaesigt : BasisRegion
    {
        public RegionMittellaendischeWaelderGemaesigt()
        {
            this.Name = "Mittell�ndische W�lder (Gem��igtes Klima)";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.HAEUFIG;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Belmart");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Carlog");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("F�rberlotus (Gelber, Blauer, Roter und Rosa Lotus)");
            this.Pflanzen.Add("Purpurner Lotus");
            this.Pflanzen.Add("Schwarzer Lotus");
            this.Pflanzen.Add("Grauer Lotus");
            this.Pflanzen.Add("Wei�er Lotus");
            this.Pflanzen.Add("Wei�gelber Lotus");
            this.Pflanzen.Add("Lulanie");
            this.Pflanzen.Add("Mibelrohr");
            this.Pflanzen.Add("Mirbelstein");
            this.Pflanzen.Add("Neckerkraut");
            this.Pflanzen.Add("Quasselwurz");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Satuariensbusch");
            this.Pflanzen.Add("Schleimiger Sumpfkn�terich");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Ulmenw�rger");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Vragieswurzel");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenl�ffler"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildschwein"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("G�nseluchs"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Pfeifhase"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerhahn"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fasan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Kronenhirsch"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotfuchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Streifendachs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerochse"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Baumb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("H�hlenb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Schwarzb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Silberbock"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Silberl�we"), (int)EVorkommen.SEHRSELTEN));            
        }
    }

    public class RegionMittellaendischeWaelderYaquir : BasisRegion
    {
        public RegionMittellaendischeWaelderYaquir()
        {
            this.Name = "Mittell�ndische W�lder (Yaquirisches Klima)";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.HAEUFIG;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Arganstrauch");
            this.Pflanzen.Add("Belmart");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Boronsschlinge");
            this.Pflanzen.Add("Carlog");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("F�rberlotus (Gelber, Blauer, Roter und Rosa Lotus)");
            this.Pflanzen.Add("Purpurner Lotus");
            this.Pflanzen.Add("Schwarzer Lotus");
            this.Pflanzen.Add("Grauer Lotus");
            this.Pflanzen.Add("Wei�er Lotus");
            this.Pflanzen.Add("Wei�gelber Lotus");
            this.Pflanzen.Add("Lulanie");
            this.Pflanzen.Add("Mibelrohr");
            this.Pflanzen.Add("Purpurmohn");
            this.Pflanzen.Add("Schwarzer Mohn");
            this.Pflanzen.Add("Neckerkraut");
            this.Pflanzen.Add("Quasselwurz");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Satuariensbusch");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Ulmenw�rger");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Vragieswurzel");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenl�ffler"), (int)EVorkommen.SEHRHAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotfuchs"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildschwein"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("G�nseluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Regenbogenfasan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Baumb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Raschtulsluchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Streifendachs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.SELTEN));            
        }
    }

    public class RegionMittellaendischeWaelderTobrisch : BasisRegion
    {
        public RegionMittellaendischeWaelderTobrisch()
        {
            this.Name = "Mittell�ndische W�lder (Tobrisches Klima)";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.GELEGENTLICH;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Belmart");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Mirbelstein");
            this.Pflanzen.Add("Purpurmohn");
            this.Pflanzen.Add("Quasselwurz");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Satuariensbusch");
            this.Pflanzen.Add("Schwarmschwamm");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Tuur-Amash-Kelch");
            this.Pflanzen.Add("Ulmenw�rger");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Wasserrausch");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenl�ffler"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerhahn"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fasan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotfuchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildschwein"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerochse"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Baumb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SEHRSELTEN));            
        }
    }

    public class RegionImmgergrueneWaelderSuedosten : BasisRegion
    {
        public RegionImmgergrueneWaelderSuedosten()
        {
            this.Name = "Immergr�ne W�lder (S�dosten)";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.HAEUFIG;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Dornrose");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("F�rberlotus (Gelber, Blauer, Roter und Rosa Lotus)");
            this.Pflanzen.Add("Purpurner Lotus");
            this.Pflanzen.Add("Schwarzer Lotus");
            this.Pflanzen.Add("Grauer Lotus");
            this.Pflanzen.Add("Wei�er Lotus");
            this.Pflanzen.Add("Wei�gelber Lotus");
            this.Pflanzen.Add("Purpurmohn");
            this.Pflanzen.Add("Quasselwurz");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Satuariensbusch");
            this.Pflanzen.Add("Schwarzer Wein");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenl�ffler"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Auerhahn"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Ongalobulle"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Raschtulsluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rebhuhn"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Regenbogenfasan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotfuchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Warzenschwein"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Al'Kebir-Antilope"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Baumb�r"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Springbock"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildkatze"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Moosaffe"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SEHRSELTEN));            
        }
    }

    public class RegionSuedlaendischeGraslaenderSteppen : BasisRegion
    {
        public RegionSuedlaendischeGraslaenderSteppen()
        {
            this.Name = "S�dl�ndische Grasl�nder und Steppen";
            this.EssbarePflanzen = (int)EVorkommen.HAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.GELEGENTLICH;

            this.Landschaften.Add("W�ste und W�stenrand");
            this.Landschaften.Add("Steppe");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Atmon");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Dornrose");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Finage");
            this.Pflanzen.Add("Hiradwurz");
            this.Pflanzen.Add("Jagdgras");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Kh�m- oder Mhanadiknolle");
            this.Pflanzen.Add("Menchal-Kaktus");
            this.Pflanzen.Add("Merach-Strauch");
            this.Pflanzen.Add("Messergras");
            this.Pflanzen.Add("Mirbelstein");
            this.Pflanzen.Add("Bunter Mohn");
            this.Pflanzen.Add("Purpurmohn");
            this.Pflanzen.Add("Naftanstaude");
            this.Pflanzen.Add("Olginwurz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Schlangenz�nglein");
            this.Pflanzen.Add("Schwarzer Wein");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Talaschin");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Winselgras");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Yaganstrauch");
            this.Pflanzen.Add("Zithabar");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Al'Kebir-Antilope"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Gabelantilope"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Springbock"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Kh�mgepard"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Ongalobulle"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Raschtulsluchs"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Strau�"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Warzenschwein"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fasan"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Gelbfuchs"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Regenbogenfasan"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sandl�we"), (int)EVorkommen.SELTEN));
        }
    }

    public class RegionWuestenrandgebiete : BasisRegion
    {
        public RegionWuestenrandgebiete()
        {
            this.Name = "W�stenrandgebiete";
            this.EssbarePflanzen = (int)EVorkommen.SELTEN;
            this.Wildvorkommen = (int)EVorkommen.SELTEN;

            this.Landschaften.Add("W�ste und W�stenrand");
            this.Landschaften.Add("Steppe");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Atmon");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Cheria-Kaktus");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Finage");
            this.Pflanzen.Add("Hiradwurz");
            this.Pflanzen.Add("Kh�m- oder Mhanadiknolle");
            this.Pflanzen.Add("Menchal-Kaktus");
            this.Pflanzen.Add("Merach-Strauch");
            this.Pflanzen.Add("Messergras");
            this.Pflanzen.Add("Purpurmohn");
            this.Pflanzen.Add("Naftanstaude");
            this.Pflanzen.Add("Olginwurz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Talaschin");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Winselgras");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Gabelantilope"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Springbock"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Al'Kebir-Antilope"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Kh�mgepard"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Sandl�we"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Strau�"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Raschtulsluchs"), (int)EVorkommen.SEHRSELTEN));
        }
    }

    public class RegionWueste : BasisRegion
    {
        public RegionWueste()
        {
            this.Name = "W�ste";
            this.EssbarePflanzen = (int)EVorkommen.SEHRSELTEN;
            this.EssbarePflanzen = (int)EVorkommen.SEHRSELTEN;

            this.Landschaften.Add("W�ste und W�stenrand");

            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Cheria-Kaktus");
            this.Pflanzen.Add("Kh�m- oder Mhanadiknolle");
            this.Pflanzen.Add("Menchal-Kaktus");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Talaschin");

            this.Tiere.Add(new VerbreitungsElementTiere(("Sandl�we"), (int)EVorkommen.SELTEN));
        }
    }

    public class RegionSuedlaendischeGebirge : BasisRegion
    {
        public RegionSuedlaendischeGebirge()
        {
            this.Name = "S�dl�ndische Gebirge";
            this.EssbarePflanzen = (int)EVorkommen.SELTEN;
            this.Wildvorkommen = (int)EVorkommen.SELTEN;

            this.Landschaften.Add("Gebirge");
            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Atmon");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Bleichmohn (Wei�er Mohn)");
            this.Pflanzen.Add("Olginwurz");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Talaschin");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Wirselkraut");

            this.Tiere.Add(new VerbreitungsElementTiere(("Raschtulsluchs"), (int)EVorkommen.HAEUFIG));            
        }
    }

    public class RegionMaraskan : BasisRegion
    {
        public RegionMaraskan()
        {
            this.Name = "Maraskan";
            this.EssbarePflanzen = (int)EVorkommen.GELEGENTLICH;
            this.Wildvorkommen = (int)EVorkommen.HAEUFIG;

            this.Landschaften.Add("Gebirge");
            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Sumpf und Moor");
            this.Landschaften.Add("Regenwald");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Axorda-Baum");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Disdychonda");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Horusche");
            this.Pflanzen.Add("Jagdgras");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Rauschgurke");
            this.Pflanzen.Add("Schlangenz�nglein");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Tarnblatt");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Trichterwurzel");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Yaganstrauch");

            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenl�ffler"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Otan-Otan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rotp�schel"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Vy'Tagga-Antilope"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Wildschwein"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Maraskanisches Stachelschwein"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Baumschleimer"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Baumw�rger"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Rehwild"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SEHRSELTEN));
        }
    }

    public class RegionSuedlicheSuempfe : BasisRegion
    {
        public RegionSuedlicheSuempfe()
        {
            this.Name = "S�dliche S�mpfe";
            this.EssbarePflanzen = (int)EVorkommen.SELTEN;
            this.Wildvorkommen = (int)EVorkommen.SELTEN;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("K�ste, Strand");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Sumpf und Moor");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Arganstrauch");
            this.Pflanzen.Add("Atmon");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Carlog");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Disdychonda");
            this.Pflanzen.Add("Donf");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Iribaarslilie");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Kajubo");
            this.Pflanzen.Add("F�rberlotus (Gelber, Blauer, Roter und Rosa Lotus)");
            this.Pflanzen.Add("Purpurner Lotus");
            this.Pflanzen.Add("Schwarzer Lotus");
            this.Pflanzen.Add("Grauer Lotus");
            this.Pflanzen.Add("Wei�er Lotus");
            this.Pflanzen.Add("Wei�gelber Lotus");
            this.Pflanzen.Add("Mirhamer Seidenliane");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Rote Pfeilbl�te");
            this.Pflanzen.Add("Sansaro");
            this.Pflanzen.Add("Schleichender Tod");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("Zw�lfblatt");

            this.Tiere.Add(new VerbreitungsElementTiere(("Sumpfranze"), (int)EVorkommen.SEHRSELTEN));
        }
    }

    public class RegionRegenwald : BasisRegion
    {
        public RegionRegenwald()
        {
            this.Name = "Regenwald";
            this.EssbarePflanzen = (int)EVorkommen.SEHRHAEUFIG;
            this.Wildvorkommen = (int)EVorkommen.SEHRHAEUFIG;

            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("K�ste, Strand");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Regenwald");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Arganstrauch");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Boronie");
            this.Pflanzen.Add("Boronsschlinge");
            this.Pflanzen.Add("Carlog");
            this.Pflanzen.Add("Cheria-Kaktus");
            this.Pflanzen.Add("Disdychonda");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Finage");
            this.Pflanzen.Add("H�llenkraut");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Kajubo");
            this.Pflanzen.Add("Kukuka");
            this.Pflanzen.Add("F�rberlotus (Gelber, Blauer, Roter und Rosa Lotus)");
            this.Pflanzen.Add("Purpurner Lotus");
            this.Pflanzen.Add("Schwarzer Lotus");
            this.Pflanzen.Add("Grauer Lotus");
            this.Pflanzen.Add("Wei�er Lotus");
            this.Pflanzen.Add("Wei�gelber Lotus");
            this.Pflanzen.Add("Merach-Strauch");
            this.Pflanzen.Add("Mirhamer Seidenliane");
            this.Pflanzen.Add("Orazal");
            this.Pflanzen.Add("Quinja");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Rote Pfeilbl�te");
            this.Pflanzen.Add("Schleichender Tod");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Vragieswurzel");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("W�rgedattel");
            this.Pflanzen.Add("Zunderschwamm");

            this.Tiere.Add(new VerbreitungsElementTiere(("Moosaffe"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Otan-Otan"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("L�wenaffe"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Purzelaffe"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fleckenpanther"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Zwergelefant"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Brabaker Waldelefant"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Dschungeltiger"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Lioma"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SEHRSELTEN));
        }
    }

    
    public class RegionSuedlicheRegengebirge : BasisRegion
    {
        public RegionSuedlicheRegengebirge()
        {
            this.Name = "S�dliche Regengebirge";
            this.EssbarePflanzen = (int)EVorkommen.GELEGENTLICH;
            this.Wildvorkommen = (int)EVorkommen.GELEGENTLICH;

            this.Landschaften.Add("Gebirge");
            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("Regenwald");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            this.Landschaften.Add("H�hle (feucht)");
            
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Atmon");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Feuermoos und Efferdmoos");
            this.Pflanzen.Add("Finage");
            this.Pflanzen.Add("H�llenkraut");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Kukuka");
            this.Pflanzen.Add("Merach-Strauch");
            this.Pflanzen.Add("Mirhamer Seidenliane");
            this.Pflanzen.Add("Bleichmohn (Wei�er Mohn)");
            this.Pflanzen.Add("Grauer Mohn");
            this.Pflanzen.Add("Orazal");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Schleichender Tod");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Vragieswurzel");

            this.Tiere.Add(new VerbreitungsElementTiere(("Karnickel"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("L�wenaffe"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Purzelaffe"), (int)EVorkommen.HAEUFIG));
            this.Tiere.Add(new VerbreitungsElementTiere(("Moosaffe"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Otan-Otan"), (int)EVorkommen.GELEGENTLICH));
            this.Tiere.Add(new VerbreitungsElementTiere(("Fleckenpanther"), (int)EVorkommen.SELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Dschungeltiger"), (int)EVorkommen.SEHRSELTEN));
            this.Tiere.Add(new VerbreitungsElementTiere(("Riesenaffe"), (int)EVorkommen.SEHRSELTEN));            
        }
    }

    public class RegionIfirnsOzean : BasisRegion
    {
        public RegionIfirnsOzean()
        {
            this.Name = "Ifirns Ozean";
            this.EssbarePflanzen = (int)EVorkommen.KEINE;
            this.Wildvorkommen = (int)EVorkommen.KEINE;

            this.Landschaften.Add("Meer");
        }
    }

    public class RegionMeerSiebenWinde : BasisRegion
    {
        public RegionMeerSiebenWinde()
        {
            this.Name = "Meer der Sieben Winde";
            this.EssbarePflanzen = (int)EVorkommen.KEINE;
            this.Wildvorkommen = (int)EVorkommen.KEINE;

            this.Landschaften.Add("Meer");
        }
    }

    public class RegionSuedmeer : BasisRegion
    {
        public RegionSuedmeer()
        {
            this.Name = "S�dmeer (Feuermeer)";
            this.EssbarePflanzen = (int)EVorkommen.KEINE;
            this.Wildvorkommen = (int)EVorkommen.KEINE;

            this.Landschaften.Add("Meer");
        }
    }

    public class RegionPerlenmeer : BasisRegion
    {
        public RegionPerlenmeer()
        {
            this.Name = "Perlenmeer";
            this.EssbarePflanzen = (int)EVorkommen.KEINE;
            this.Wildvorkommen = (int)EVorkommen.KEINE;

            this.Landschaften.Add("Meer");

            this.Pflanzen.Add("Feuerschlick");
            this.Pflanzen.Add("Sansaro");
        }
    }

    #region Musterregion
    public class RegionMuster : BasisRegion
    {
        public RegionMuster()
        {
            this.Name = "Musterregion";
            this.EssbarePflanzen = (int)EVorkommen.KEINE;
            this.Wildvorkommen = (int)EVorkommen.KEINE;

            this.Landschaften.Add("Eis");
            this.Landschaften.Add("W�ste und W�stenrand");
            this.Landschaften.Add("Gebirge");
            this.Landschaften.Add("Hochland");
            this.Landschaften.Add("Steppe");
            this.Landschaften.Add("Grasland, Wiesen");
            this.Landschaften.Add("Fluss- und Seeufer, Teiche");
            this.Landschaften.Add("K�ste, Strand");
            this.Landschaften.Add("Flussauen");
            this.Landschaften.Add("Sumpf und Moor");
            this.Landschaften.Add("Regenwald");
            this.Landschaften.Add("Wald");
            this.Landschaften.Add("Waldrand");
            this.Landschaften.Add("Meer");
            this.Landschaften.Add("H�hle (feucht)");
            this.Landschaften.Add("H�hle (trocken)");

            this.Pflanzen.Add("Alraune");
            this.Pflanzen.Add("Alveranie");
            this.Pflanzen.Add("Arganstrauch");
            this.Pflanzen.Add("Atan-Kiefer");
            this.Pflanzen.Add("Atmon");
            this.Pflanzen.Add("Axorda-Baum");
            this.Pflanzen.Add("Basilamine");
            this.Pflanzen.Add("Belmart");
            this.Pflanzen.Add("Blutblatt");
            this.Pflanzen.Add("Boronie");
            this.Pflanzen.Add("Boronsschlinge");
            this.Pflanzen.Add("Carlog");
            this.Pflanzen.Add("Cheria-Kaktus");
            this.Pflanzen.Add("Chonchinis");
            this.Pflanzen.Add("Disdychonda");
            this.Pflanzen.Add("Donf");
            this.Pflanzen.Add("Dornrose");
            this.Pflanzen.Add("Efeuer");
            this.Pflanzen.Add("Egelschreck");
            this.Pflanzen.Add("Eitriger Kr�tenschemel");
            this.Pflanzen.Add("Feuermoos und Efferdmoos");
            this.Pflanzen.Add("Feuerschlick");
            this.Pflanzen.Add("Finage");
            this.Pflanzen.Add("Gr�ne Schleimschlange");
            this.Pflanzen.Add("Gulmond");
            this.Pflanzen.Add("Hiradwurz");
            this.Pflanzen.Add("H�llenkraut");
            this.Pflanzen.Add("Hollbeere");
            this.Pflanzen.Add("Horusche");
            this.Pflanzen.Add("Ilmenblatt");
            this.Pflanzen.Add("Iribaarslilie");
            this.Pflanzen.Add("Jagdgras");
            this.Pflanzen.Add("Joruga");
            this.Pflanzen.Add("Kairan");
            this.Pflanzen.Add("Kajubo");
            this.Pflanzen.Add("Kh�m- oder Mhanadiknolle");
            this.Pflanzen.Add("Klippenzahn");
            this.Pflanzen.Add("Kukuka");
            this.Pflanzen.Add("F�rberlotus (Gelber, Blauer, Roter und Rosa Lotus)");
            this.Pflanzen.Add("Purpurner Lotus");
            this.Pflanzen.Add("Schwarzer Lotus");
            this.Pflanzen.Add("Grauer Lotus");
            this.Pflanzen.Add("Wei�er Lotus");
            this.Pflanzen.Add("Wei�gelber Lotus");
            this.Pflanzen.Add("Lulanie");
            this.Pflanzen.Add("Madabl�te");
            this.Pflanzen.Add("Menchal-Kaktus");
            this.Pflanzen.Add("Merach-Strauch");
            this.Pflanzen.Add("Messergras");
            this.Pflanzen.Add("Mibelrohr");
            this.Pflanzen.Add("Mirbelstein");
            this.Pflanzen.Add("Mirhamer Seidenliane");
            this.Pflanzen.Add("Bleichmohn (Wei�er Mohn)");
            this.Pflanzen.Add("Bunter Mohn");
            this.Pflanzen.Add("Grauer Mohn");
            this.Pflanzen.Add("Purpurmohn");
            this.Pflanzen.Add("Schwarzer Mohn");
            this.Pflanzen.Add("Tigermohn");
            this.Pflanzen.Add("Morgendornstrauch");
            this.Pflanzen.Add("Naftanstaude");
            this.Pflanzen.Add("Neckerkraut");
            this.Pflanzen.Add("Nothilf");
            this.Pflanzen.Add("Olginwurz");
            this.Pflanzen.Add("Orazal");
            this.Pflanzen.Add("Orklandbovist");
            this.Pflanzen.Add("Pestsporenpilz");
            this.Pflanzen.Add("Phosphorpilz");
            this.Pflanzen.Add("Quasselwurz");
            this.Pflanzen.Add("Quinja");
            this.Pflanzen.Add("Rahjalieb");
            this.Pflanzen.Add("Rattenpilz");
            this.Pflanzen.Add("Rauschgurke");
            this.Pflanzen.Add("Rote Pfeilbl�te");
            this.Pflanzen.Add("Roter Drachenschlund");
            this.Pflanzen.Add("Sansaro");
            this.Pflanzen.Add("Satuariensbusch");
            this.Pflanzen.Add("Schlangenz�nglein");
            this.Pflanzen.Add("Schleichender Tod");
            this.Pflanzen.Add("Schleimiger Sumpfkn�terich");
            this.Pflanzen.Add("Schlinggras");
            this.Pflanzen.Add("Schwarmschwamm");
            this.Pflanzen.Add("Schwarzer Wein");
            this.Pflanzen.Add("Shurinstrauch");
            this.Pflanzen.Add("Talaschin");
            this.Pflanzen.Add("Tarnblatt");
            this.Pflanzen.Add("Tarnele");
            this.Pflanzen.Add("Thonnys");
            this.Pflanzen.Add("Traschbart");
            this.Pflanzen.Add("Trichterwurzel");
            this.Pflanzen.Add("Tuur-Amash-Kelch");
            this.Pflanzen.Add("Ulmenw�rger");
            this.Pflanzen.Add("Vierbl�ttrige Einbeere");
            this.Pflanzen.Add("Vragieswurzel");
            this.Pflanzen.Add("Waldwebe");
            this.Pflanzen.Add("Wasserrausch");
            this.Pflanzen.Add("Winselgras");
            this.Pflanzen.Add("Wirselkraut");
            this.Pflanzen.Add("W�rgedattel");
            this.Pflanzen.Add("Yaganstrauch");
            this.Pflanzen.Add("Zithabar");
            this.Pflanzen.Add("Zunderschwamm");
            this.Pflanzen.Add("Zw�lfblatt");

            /*
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            this.Tiere.Add(new VerbreitungsElementTiere((""), (int)EVorkommen.));
            */
        }
    }
    #endregion
}
