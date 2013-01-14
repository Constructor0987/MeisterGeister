using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Umrechner
{
    public class Währung : System.Collections.Generic.Dictionary<string, double>
    {
        public Währung()
        {
            // siehe im Wiki unter
            // Aventurische Währungen

            Add("-- Mittelreichische Münzen --", 0);
            Add("Kreuzer", 0.01);
            Add("Heller", 0.1);
            Add("Silbertaler", 1);
            Add("Dukaten", 10);
            Add("Balihoer Rad (veraltet)", 100);
            Add("Puniner Dublone (veraltet)", 8);
            Add("Nickel (veraltet)", 0.02);
            Add("Eslamo (veraltet)", 100);

            Add("-- Nostria und Andergast --", 0);
            Add("Nostrische Krone", 5);
            Add("Andrataler", 5);

            Add("-- Al'Anfanische Münzen --", 0);
            Add("Dirham", 0.01);
            Add("Kleiner Oreal", 0.5);
            Add("Oreal/Schilling", 1);
            Add("Dublone", 20);

            Add("-- Amazonische Münzen --", 0);
            Add("Amazonenkrone", 12);

            Add("-- Brabaker Münzen --", 0);
            Add("Brabaker Kreuzer", 0.01);
            Add("Brabaker Krone", 10);
            Add("Brabaker Krone (außerhalb Brabaks)", 5);

            Add("-- Münzen der Schwarzen Lande --", 0);
            Add("Gulden (Glorania)", 5);
            Add("Splitter (Xeraanien)", 0.14);
            Add("Zholvari (Xeraanien)", 1);
            Add("Borbaradstaler (Xeraanien)", 7);

            Add("-- Aranische Münzen --", 0);
            Add("Rosenkreuzer", 0.01);
            Add("Hallah", 0.1);
            Add("Schekel", 1);
            Add("Dinar", 10);

            Add("-- sonstige tulamidische Münzen --", 0);
            Add("Selemer Kupferschilling (veraltet)", 0.1);
            Add("Piaster (Rashdul)", 50);
            Add("Alastren (Khunchom)", 5000);

            Add("-- Bornländische Münzen --", 0);
            Add("Deut", 0.1);
            Add("Silbergroschen/Groschen", 1);
            Add("Batzen", 10);

            Add("-- Münzen des Kalifats --", 0);
            Add("Muwlat", 0.05);
            Add("Zechine", 2);
            Add("Marawedi", 20);
            Add("Shekel (veraltet)", 0.05);
            Add("Denar (veraltet)", 2);
            Add("Piaster (veraltet)", 20);

            Add("-- Münzen im Großemirat Mengbilla --", 0);
            Add("Ikossar", 0.05);
            Add("Tesar", 0.25);
            Add("Telar", 1);
            Add("Dekat", 10);
            Add("Mengbillaner Unze (veraltet)", 10);

            Add("-- Horasische Münzen --", 0);
            Add("Kusliker Rad (Horasdor)", 200);
            Add("Krone (veraltet)", 10);
            Add("Zehnt (veraltet)", 1);
            Add("Schilling (veraltet)", 0.1);
            Add("Arivorer Silberdukaten (veraltet)", 5);

            Add("-- Paavische Münzen --", 0);
            Add("Gulden", 5);

            Add("-- Trahelische Münzen --", 0);
            Add("Trümmer", 0.01);
            Add("Ch'ryskl", 0.1);
            Add("Hedsch", 1);
            Add("Suvar", 10);

            Add("-- Vallusanische Münzen --", 0);
            Add("Flindrich", 0.1);
            Add("Stüber", 1);
            Add("Witten", 10);

            Add("-- Münzen der Zwerge --", 0);
            Add("Atebrox (Zwergengroschen)", 0.2);
            Add("Arganbrox (Zwergenschilling)", 2);
            Add("Auromox (Zwergentaler)", 12);

            Add("-- Weitere Münzen --", 0);
            Add("Chorhoper Heller", 0.1);
            Add("Syllaner Taler", 1);
            Add("Minisepe", 0.1);

        }

        public double WertUmrechnen(string von, string nach, double? wert)
        {
            double ergebnis = 0;

            if (ContainsKey(von) && ContainsKey(nach))
                ergebnis = (double)wert * this[von] / this[nach];

            return ergebnis;
        }
    }
}
