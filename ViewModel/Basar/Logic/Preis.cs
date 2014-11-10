using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Basar.Logic
{
    public class Preis
    {
        #region //----- KONSTRUKTOR ----

        public Preis() { ;}

        public Preis(double preis)
        {
            _untererPreis = preis;
        }

        public Preis(string preis)
        {
            SetByString(preis);
        }

        public Preis(double unter, double ober, string prä, string suf, string wsuf)
        {
            _währungSuffix = wsuf;
            _untererPreis = unter;
            _obererPreis = ober;
            _präfix = prä;
            _suffix = suf;
        }

        #endregion

        #region //----- FELDER, EIGENSCHAFTEN ----

        private string _präfix = string.Empty; // z.B. "ca. "
        private string _suffix = string.Empty; // z.B. "+"

        // Währungs-Suffix
        private string _währungSuffix = "";
        public string WährungSuffix
        {
            get { return _währungSuffix; }
            set { _währungSuffix = value; }
        }

        // untere Preisgrenze
        private double _untererPreis = 0;
        public double UntererPreis
        { 
            get { return _untererPreis; } 
            set { _untererPreis = value; } 
        }

        // obere Preisgrenze
        private double _obererPreis = 0;
        public double ObererPreis
        {
            get { return _obererPreis; }
            set { _obererPreis = value; }
        }

        #endregion

        #region //----- METHODEN ----

        private void SetByString(string preis)
        {
            if (string.IsNullOrEmpty(preis))
                return;

            if (!Double.TryParse(preis, out _untererPreis))
            {
                // Präfix auslesen
                foreach (char c in preis)
                {
                    if (!Char.IsDigit(c))
                        _präfix += c;
                    else
                        break;
                }
                preis = preis.Remove(0, _präfix.Length);

                // Suffix auslesen
                foreach (char c in preis.Reverse())
                {
                    if (!Char.IsDigit(c))
                        _suffix = c + _suffix;
                    else
                        break;
                }
                preis = preis.Remove(preis.Length - _suffix.Length, _suffix.Length);

                // Unteren und oberen Preis auslesen
                string[] preisTeile;
                preisTeile = preis.Split('-');

                if (preisTeile.Length > 0)
                    Double.TryParse(preisTeile[0], out _untererPreis);
                if (preisTeile.Length > 1)
                    Double.TryParse(preisTeile[1], out _obererPreis);
            }
        }

        public override string ToString()
        {
            if (_obererPreis == 0)
                return string.Format("{0}{1:0.##}{2} {3}", _präfix, _untererPreis, _suffix, _währungSuffix);
            else
                return string.Format("{0}{1:0.##}-{2:0.##}{3} {4}", _präfix, _untererPreis, _obererPreis, _suffix, _währungSuffix);
        }

        #endregion

        #region //----- OPERATOREN ----

        public static Preis operator *(Preis preis1,
                               double preis2)
        {
            if (preis1 == null)
                return new Preis();
            return new Preis(preis1._untererPreis * preis2, preis1._obererPreis * preis2,
                preis1._präfix, preis1._suffix, preis1.WährungSuffix);
        }

        #endregion

    }
}
