using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class VorNachteil : MeisterGeister.Logic.Literatur.ILiteratur
    {
        public bool Usergenerated
        {
            get { return !VorNachteilGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

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

        // Geweiht
        public const string GeweihtZwölfgöttlicheKirche = "Geweiht [zwölfgöttliche Kirche]";
        public const string GeweihtHRanga = "Geweiht [H'Ranga]";
        public const string GeweihtAngrosch = "Geweiht [Angrosch]";
        public const string GeweihtGravesh = "Geweiht [Gravesh]";
        public const string GeweihtNichtAlveranischeGottheit = "Geweiht [nicht-alveranische Gottheit]";
        public const string Sacerdos = "Sacerdos";
    }
}
