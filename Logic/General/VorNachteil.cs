using System;
using System.ComponentModel;
using MeisterGeister.Daten;
using System.Xml;
using System.Collections.Generic;

namespace MeisterGeister.Logic.General
{
    public class VorNachteil
    {
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


        public static int GetVorNachteilId(string vorNachteil)
        {
            var vorNachteilRows = App.DatenDataSet.VorNachteil.Select(string.Format("Name = '{0}'", vorNachteil.Replace("'", "''")));
            if (vorNachteilRows.Length == 1)
                return Convert.ToInt32(vorNachteilRows[0]["VorNachteilID"]);
            return -1;
        }
    }
}
