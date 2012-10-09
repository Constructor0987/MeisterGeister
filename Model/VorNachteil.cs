using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class VorNachteil
    {
        private const int maxid = 412;
        public bool Usergenerated
        {
            get { return VorNachteilID > maxid; }
        }

        public const string Eisern = "Eisern";
        public const string Glasknochen = "Glasknochen";
        public const string Viertelzauberer = "Viertelzauberer";
        public const string ViertelzaubererUnbewusst = "Viertelzauberer (unbewusst)";
        public const string Halbzauberer = "Halbzauberer";
        public const string Vollzauberer = "Vollzauberer";
        public const string Empathie = "Empathie";
        public const string Gefahreninstinkt = "Gefahreninstinkt";
        public const string Geräuschhexerei = "Geräuschhexerei";
        public const string Magiegespür = "Magiegespür";
        public const string Prophezeien = "Prophezeien";
        public const string Zwergennase = "Zwergennase";

        // Geweiht
        public const string GeweihtZwölfgöttlicheKirche = "Geweiht [zwölfgöttliche Kirche]";
        public const string GeweihtHRanga = "Geweiht [H'Ranga]";
        public const string GeweihtAngrosch = "Geweiht [Angrosch]";
        public const string GeweihtGravesh = "Geweiht [Gravesh]";
        public const string GeweihtNichtAlveranischeGottheit = "Geweiht [nicht-alveranische Gottheit]";
        public const string Sacerdos = "Sacerdos";
    }
}
