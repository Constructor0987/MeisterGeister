using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Umrechner
{
    public class Währung : System.Collections.Generic.Dictionary<string, Währungsinformationen>
    {
        public Währung()
        {
            // siehe im Wiki unter
            // Aventurische Währungen

            Add("-- Mittelreichische Münzen --", new Währungsinformationen(0,""));
            Add("Kreuzer", new Währungsinformationen(0.01,"K"));
            Add("Heller", new Währungsinformationen(0.1,"H"));
            Add("Silbertaler", new Währungsinformationen(1,"S"));
            Add("Dukaten", new Währungsinformationen(10,"D"));
            Add("Balihoer Rad (veraltet)", new Währungsinformationen(100, "BaR"));
            Add("Puniner Dublone (veraltet)", new Währungsinformationen(8, "PuD"));
            Add("Nickel (veraltet)", new Währungsinformationen(0.02,"Ni"));
            Add("Eslamo (veraltet)", new Währungsinformationen(100,"Es"));

            Add("-- Nostria und Andergast --", new Währungsinformationen(0,""));
            Add("Nostrische Krone", new Währungsinformationen(5, "NoKr"));
            Add("Andrataler", new Währungsinformationen(5,"An"));

            Add("-- Al'Anfanische Münzen --", new Währungsinformationen(0,""));
            Add("Dirham", new Währungsinformationen(0.01,"Dir"));
            Add("Kleiner Oreal", new Währungsinformationen(0.5,"KOr"));
            Add("Oreal/Schilling", new Währungsinformationen(1,"Or"));
            Add("Dublone", new Währungsinformationen(20,"Dub"));

            Add("-- Amazonische Münzen --", new Währungsinformationen(0,""));
            Add("Amazonenkrone", new Währungsinformationen(12, "Kro"));

            Add("-- Brabaker Münzen --", new Währungsinformationen(0,""));
            Add("Brabaker Kreuzer", new Währungsinformationen(0.01,"Kr"));
            Add("Brabaker Krone", new Währungsinformationen(10,"Kro"));
            Add("Brabaker Krone (außerhalb Brabaks)", new Währungsinformationen(5, "Kro"));

            Add("-- Münzen der Schwarzen Lande --", new Währungsinformationen(0,""));
            Add("Gulden (Glorania)", new Währungsinformationen(5, "Gul"));
            Add("Splitter (Xeraanien)", new Währungsinformationen(0.14, "Spl"));
            Add("Zholvari (Xeraanien)", new Währungsinformationen(1, "Zho"));
            Add("Borbaradstaler (Xeraanien)", new Währungsinformationen(7, "Bor"));

            Add("-- Aranische Münzen --", new Währungsinformationen(0,""));
            Add("Rosenkreuzer", new Währungsinformationen(0.01, "Ros"));
            Add("Hallah", new Währungsinformationen(0.1, "Hal"));
            Add("Schekel", new Währungsinformationen(1, "Sch"));
            Add("Dinar", new Währungsinformationen(10, "Din"));

            Add("-- sonstige tulamidische Münzen --", new Währungsinformationen(0,""));
            Add("Selemer Kupferschilling (veraltet)", new Währungsinformationen(0.1, "SKu"));
            Add("Piaster (Rashdul)", new Währungsinformationen(50,"Pia"));
            Add("Alastren (Khunchom)", new Währungsinformationen(5000,"Ala"));

            Add("-- Bornländische Münzen --", new Währungsinformationen(0,""));
            Add("Deut", new Währungsinformationen(0.1,"Deu"));
            Add("Silbergroschen/Groschen", new Währungsinformationen(1,"Gro"));
            Add("Batzen", new Währungsinformationen(10,"Bat"));

            Add("-- Münzen des Kalifats --", new Währungsinformationen(0,""));
            Add("Muwlat", new Währungsinformationen(0.05,"Mu"));
            Add("Zechine", new Währungsinformationen(2,"Ze"));
            Add("Marawedi", new Währungsinformationen(20,"Ma"));
            Add("Shekel (veraltet)", new Währungsinformationen(0.05,"She"));
            Add("Denar (veraltet)", new Währungsinformationen(2,"De"));
            Add("Piaster (veraltet)", new Währungsinformationen(20,"Pia"));

            Add("-- Münzen im Großemirat Mengbilla --", new Währungsinformationen(0,""));
            Add("Ikossar", new Währungsinformationen(0.05, "Iko"));
            Add("Tesar", new Währungsinformationen(0.25, "Tes"));
            Add("Telar", new Währungsinformationen(1, "Tel"));
            Add("Dekat", new Währungsinformationen(10, "Dek"));
            Add("Mengbillaner Unze (veraltet)", new Währungsinformationen(10,"MUz"));

            Add("-- Horasische Münzen --", new Währungsinformationen(0,""));
            Add("Kusliker Rad (Horasdor)", new Währungsinformationen(200, "KRa"));
            Add("Krone (veraltet)", new Währungsinformationen(10, "Kro"));
            Add("Zehnt (veraltet)", new Währungsinformationen(1,"Z"));
            Add("Schilling (veraltet)", new Währungsinformationen(0.1,"Sch"));
            Add("Arivorer Silberdukaten (veraltet)", new Währungsinformationen(5,"Sid"));

            Add("-- Paavische Münzen --", new Währungsinformationen(0, ""));
            Add("Gulden", new Währungsinformationen(5, "Gu"));

            Add("-- Trahelische Münzen --", new Währungsinformationen(0,""));
            Add("Trümmer", new Währungsinformationen(0.01, "Tr"));
            Add("Ch'ryskl", new Währungsinformationen(0.1, "Ch"));
            Add("Hedsch", new Währungsinformationen(1, "Hed"));
            Add("Suvar", new Währungsinformationen(10, "Suv"));

            Add("-- Vallusanische Münzen --", new Währungsinformationen(0,""));
            Add("Flindrich", new Währungsinformationen(0.1, "Fl"));
            Add("Stüber", new Währungsinformationen(1, "St"));
            Add("Witten", new Währungsinformationen(10, "Wi"));

            Add("-- Münzen der Zwerge --", new Währungsinformationen(0,""));
            Add("Atebrox (Zwergengroschen)", new Währungsinformationen(0.2, "Ate"));
            Add("Arganbrox (Zwergenschilling)", new Währungsinformationen(2, "Arg"));
            Add("Auromox (Zwergentaler)", new Währungsinformationen(12, "Aur"));

            Add("-- Weitere Münzen --", new Währungsinformationen(0,""));
            Add("Chorhoper Heller", new Währungsinformationen(0.1, "Ch"));
            Add("Syllaner Taler", new Währungsinformationen(1, "Syl"));
            Add("Minisepe", new Währungsinformationen(0.1, "Min"));

            Add("-- Myranische Münzen --", new Währungsinformationen(0,""));
            Add("Obulos", new Währungsinformationen(0.01, "Ob"));
            Add("Pekunos", new Währungsinformationen(0.1, "Pk"));
            Add("Argental", new Währungsinformationen(1, "Ag"));
            Add("Aureal", new Währungsinformationen(10, "Au"));

        }

        public double WertUmrechnen(string von, string nach, double? wert)
        {
            double ergebnis = 0;

            if (ContainsKey(von) && ContainsKey(nach))
                ergebnis = (double)wert * this[von].WährungsFaktor / this[nach].WährungsFaktor;

            return ergebnis;
        }
    }

    public class Währungsinformationen
    {
        public Währungsinformationen(double währungsFaktor, string währungAbkürzung)
        {
            WährungAbkürzung = währungAbkürzung;
            WährungsFaktor = währungsFaktor;
        }

        public double WährungsFaktor { get; set; }
        public string WährungAbkürzung { get; set; }
    }
}
