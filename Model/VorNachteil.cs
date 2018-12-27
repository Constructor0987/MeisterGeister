namespace MeisterGeister.Model
{
    public partial class VorNachteil : MeisterGeister.Logic.Literatur.ILiteratur
    {
        public const string Eisern = "Eisern";

        public const string Glasknochen = "Glasknochen";

        public const string Viertelzauberer = "Viertelzauberer";

        public const string ViertelzaubererUnbewusst = "Viertelzauberer (unbewusst)";

        public const string Halbzauberer = "Halbzauberer";

        public const string Vollzauberer = "Vollzauberer";

        public const string Zauberer = "Zauberer";

        public const string Empathie = "Empathie";

        public const string Gefahreninstinkt = "Gefahreninstinkt";

        public const string Geräuschhexerei = "Geräuschhexerei";

        public const string Magiegespür = "Magiegespür";

        public const string Prophezeien = "Prophezeien";

        public const string Zwergennase = "Zwergennase";

        public const string TierempathieAlle = "Tierempathie (alle)";

        public const string TierempathieSpeziell = "Tierempathie (speziell)";

        public const string Zauberhaar = "Zauberhaar";

        public const string HoheLebenskraft = "Hohe Lebenskraft";

        public const string NiedrigeLebenskraft = "Niedrige Lebenskraft";

        public const string Ausdauernd = "Ausdauernd";

        public const string Kurzatmig = "Kurzatmig";

        public const string Astralmacht = "Astralmacht";

        public const string HoheAstralkraft = "Hohe Astralkraft";

        public const string NiedrigeAstralkraft = "Niedrige Astralkraft";

        public const string HoheKarmalkraft = "Hohe Karmalkraft";

        public const string NiedrigeKarmalkraft = "Niedrige Karmalkraft";

        public const string HoheKarmaenergie = "Hohe Karmaenergie";

        public const string Flink = "Flink";
        public const string FlinkII = "Flink II";
        public const string Zwergenwuchs = "Zwergenwuchs";
        public const string Kleinwüchsig = "Kleinwüchsig";
        public const string Behäbig = "Behäbig";

        public const string Entfernungssinn = "Entfernungssinn";
        public const string Dämmerungssicht = "Dämmerungssicht";
        public const string Nachtsicht = "Nachtsicht";
        public const string Nachtblind = "Nachtblind";
        public const string Einäugig = "Einäugig";
        public const string Farbenblind = "Farbenblind";
        // Geweiht
        public const string GeweihtZwölfgöttlicheKirche = "Geweiht [zwölfgöttliche Kirche]";

        public const string GeweihtHRanga = "Geweiht [H'Ranga]";

        public const string GeweihtAngrosch = "Geweiht [Angrosch]";

        public const string GeweihtGravesh = "Geweiht [Gravesh]";

        public const string GeweihtNichtAlveranischeGottheit = "Geweiht [nicht-alveranische Gottheit]";

        public const string Sacerdos = "Sacerdos";

        public const string GeweihtXoArtal = "Geweiht [Xo'Artal-Stadtpantheon]";

        public const string Geweihter = "Geweihter";

        public const string Karmatiker = "Karmatiker";

        public bool Usergenerated
        {
            get { return !VorNachteilGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
