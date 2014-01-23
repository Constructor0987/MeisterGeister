using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Einstellung;

namespace MeisterGeister.Logic.Kalender
{
    /// <summary>
    /// Beschreibt ein aventurisches Datum.
    /// </summary>
    public class Datum
    {
        private static Datum _aktuell = null;
        public static Datum Aktuell
        {
            get
            {
                if (_aktuell == null)
                    _aktuell = new Datum(Einstellungen.DatumAktuell);
                return _aktuell;
            }
            set
            {
                _aktuell = value;
                Einstellungen.DatumAktuell = value.ToString();
            }
        }

        private int _jahr = 993;
        /// <summary>
        /// Jahr in BF.
        /// </summary>
        public int Jahr
        {
            get { return _jahr; }
            //set { _jahr = value; }
        }

        private Monat _monat = Monat.Praios;
        /// <summary>
        /// Monat.
        /// </summary>
        public Monat Monat
        {
            get { return _monat; }
            set
            {
                _monat = value;
                if (Monat == Monat.NamenloseTage && Tag > 5)
                    Tag = 5;
            }
        }

        private int _tag = 1;
        /// <summary>
        /// Tag.
        /// </summary>
        public int Tag
        {
            get { return _tag; }
            set
            {
                if (value > 30)
                    value = 30;
                else if (value < 1)
                    value = 1;
                if (Monat == Monat.NamenloseTage && value > 5)
                    value = 5;
                _tag = value;
            }
        }

        public Kalender KalenderAnzeige { get; set; }

        public Datum() { }

        public Datum(int j, Monat m, int t, Kalender kal = Kalender.BosparansFall)
        {
            _jahr = j;
            Monat = m;
            Tag = t;
            KalenderAnzeige = kal;
        }

        public Datum(int tageszahl, int jahr, Kalender kal = Kalender.BosparansFall)
        {
            _jahr = jahr;
            int m = (tageszahl - 1) / 30;
            Monat = (Monat)m;
            Tag = tageszahl - m * 30;
            KalenderAnzeige = kal;
        }

        public Datum(string datum)
        {
            string[] parts = datum.Split('|');
            Kalender von = Kalender.BosparansFall;
            if (parts.Length > 3) // Jahreszählung
                von = (Kalender)Convert.ToInt32(parts[3]);
            _jahr = Convert.ToInt32(parts[2]);
            Monat = (Monat)Convert.ToInt32(parts[1]);
            Tag = Convert.ToInt32(parts[0]);
            KalenderAnzeige = von;
        }

        /// <summary>
        /// Gibt ein neues Datum zurück, das die angegebene Anzahl der Jahre zum Wert dieser Instanz addiert.
        /// </summary>
        /// <param name="jahre"></param>
        /// <returns></returns>
        public Datum AddJahr(int jahre)
        {
            Datum neuDatum = new Datum(Jahr + jahre, Monat, Tag, KalenderAnzeige);
            return neuDatum;
        }

        /// <summary>
        /// Gibt ein neues Datum zurück, das die angegebene Anzahl der Monate zum Wert dieser Instanz addiert.
        /// </summary>
        /// <param name="jahre"></param>
        /// <returns></returns>
        public Datum AddMonat(int monate)
        {
            int neuMonat = (int)Monat + monate;
            int jahre;
            if (neuMonat < 0)
            {
                jahre = (neuMonat - 12) / 13;
            }
            else
                jahre = neuMonat / 13;
            if (jahre != 0)
            {
                neuMonat -= jahre * 13;
            }
            Datum neuDatum = new Datum(Jahr + jahre, (Monat)neuMonat, Tag, KalenderAnzeige);
            return neuDatum;
        }

        /// <summary>
        /// Gibt ein neues Datum zurück, das die angegebene Anzahl der Tage zum Wert dieser Instanz addiert.
        /// </summary>
        /// <param name="jahre"></param>
        /// <returns></returns>
        public Datum AddTag(int tage)
        {
            if (tage == 0) // keine Änderung
                return new Datum(Jahr, Monat, Tag, KalenderAnzeige);

            int t = Tageszahl() + tage;
            Datum neuDatum;

            if (t > 365) // neues Jahr
                neuDatum = new Datum(t - 365, Jahr + 1, KalenderAnzeige);
            else if (t <= 0) // voriges Jahr
                neuDatum = new Datum(t + 365, Jahr - 1, KalenderAnzeige);
            else // gleiches Jahr
                neuDatum = new Datum(t, Jahr, KalenderAnzeige);

            return neuDatum;
        }

        public int Tageszahl()
        {
            return (int)Monat * 30 + Tag;
        }

        public string MonatString(Kalender z = Kalender.BosparansFall)
        {
            switch (z)
            {
                case Kalender.Irdisch:
                    switch (Monat)
                    {
                        case Monat.Praios:
                            return "Juli";
                        case Monat.Rondra:
                            return "August";
                        case Monat.Efferd:
                            return "September";
                        case Monat.Travia:
                            return "Oktober";
                        case Monat.Boron:
                            return "November";
                        case Monat.Hesinde:
                            return "Dezember";
                        case Monat.Firun:
                            return "Januar";
                        case Monat.Tsa:
                            return "Februar";
                        case Monat.Phex:
                            return "März";
                        case Monat.Peraine:
                            return "April";
                        case Monat.Ingerimm:
                            return "Mai";
                        case Monat.Rahja:
                            return "Juni";
                        default:
                            return string.Empty;
                    }
                case Kalender.Thorwal:
                    switch (Monat)
                    {
                        case Monat.Praios:
                            return "Midsonnmond";
                        case Monat.Rondra:
                            return "Kornmond";
                        case Monat.Efferd:
                            return "Heimamond";
                        case Monat.Travia:
                            return "Schlachtmond";
                        case Monat.Boron:
                            return "Sturmmond";
                        case Monat.Hesinde:
                            return "Frostmond";
                        case Monat.Firun:
                            return "Grimfrostmond";
                        case Monat.Tsa:
                            return "Goimond";
                        case Monat.Phex:
                            return "Friskenmond";
                        case Monat.Peraine:
                            return "Eimond";
                        case Monat.Ingerimm:
                            return "Faramond";
                        case Monat.Rahja:
                            return "Vinmond";
                        case Monat.NamenloseTage:
                            return "Hranngartag";
                        default:
                            return string.Empty;
                    }
                case Kalender.Zwerge:
                    switch (Monat)
                    {
                        case Monat.Praios:
                            return "Sommermond";
                        case Monat.Rondra:
                            return "Hitzemond";
                        case Monat.Efferd:
                            return "Regenmond";
                        case Monat.Travia:
                            return "Weinmond";
                        case Monat.Boron:
                            return "Nebelmond";
                        case Monat.Hesinde:
                            return "Dunkelmond";
                        case Monat.Firun:
                            return "Frostmond";
                        case Monat.Tsa:
                            return "Neugeburt";
                        case Monat.Phex:
                            return "Marktmond";
                        case Monat.Peraine:
                            return "Saatmond";
                        case Monat.Ingerimm:
                            return "Feuermond";
                        case Monat.Rahja:
                            return "Brautmond";
                        case Monat.NamenloseTage:
                            return "Drachentag";
                        default:
                            return string.Empty;
                    }
                default:
                    if (Monat == Monat.NamenloseTage)
                        return "Tag des Namenlosen";
                    else
                        return Monat.ToString();
            }
        }

        public string WochentagString(Kalender z = Kalender.BosparansFall)
        {
            WochentagEnum wo = Wochentag();
            string woStr = wo.ToString();

            switch ((WochentagEnum)wo)
            {
                case WochentagEnum.Windstag:
                    if (z == Kalender.Thorwal && Zeitrechnung.JahrUmrechnen(KalenderAnzeige, Kalender.BosparansFall, Jahr) < 1027)
                        woStr = "Orozarsdag";
                    else if (z == Kalender.Thorwal && Zeitrechnung.JahrUmrechnen(KalenderAnzeige, Kalender.BosparansFall, Jahr) >= 1027)
                        woStr = "Trondesdag";
                    else if (z == Kalender.Irdisch)
                        woStr = "Donnerstag";
                    break;
                case WochentagEnum.Erdstag:
                    if (z == Kalender.Thorwal)
                        woStr = "Ifirnsdag";
                    else if (z == Kalender.Irdisch)
                        woStr = "Freitag";
                    else
                        woStr = "Erd(s)tag";
                    break;
                case WochentagEnum.Markttag:
                    if (z == Kalender.Horas)
                        woStr = "Horastag";
                    else if (z == Kalender.Irdisch)
                        woStr = "Samstag";
                    break;
                case WochentagEnum.Praiostag:
                    if (z == Kalender.Golgari) // TODO ??: Mengbilla, Kemi
                        woStr = "Borontag";
                    else if (z == Kalender.Thorwal)
                        woStr = "Swafnirsdag";
                    else if (z == Kalender.Kurkum) // und Neetha, Weiden
                        woStr = "Rondratag";
                    else if (z == Kalender.Irdisch)
                        woStr = "Sonntag";
                    break;
                case WochentagEnum.Rohalstag:
                    if (z == Kalender.Thorwal)
                        woStr = "Traviasdag";
                    else if (z == Kalender.Bornland)
                        woStr = "Schneetag";
                    else if (z == Kalender.Irdisch)
                        woStr = "Montag";
                    break;
                case WochentagEnum.Feuertag:
                    if (z == Kalender.Thorwal)
                        woStr = "Jurgasdag";
                    else if (z == Kalender.Irdisch)
                        woStr = "Dienstag";
                    break;
                case WochentagEnum.Wassertag:
                    if (z == Kalender.Thorwal)
                        woStr = "Hjaldisdag";
                    else if (z == Kalender.Bornland)
                        woStr = "Zinstag";
                    else if (z == Kalender.Irdisch)
                        woStr = "Mittwoch";
                    break;
                default:
                    break;
            }

            return woStr;
        }

        public int Mondphase()
        {
            int j = Zeitrechnung.JahrUmrechnen(KalenderAnzeige, Kalender.BosparansFall, Jahr) - 993;

            int teil = j / 28;

            if (j < 0)
            {
                j = j + ((teil - 1) * -28);
                if (j == 28)
                    j = 0;
            }
            else
                j = j - (teil * 28);

            int mada = (j + (int)Monat * 2 + Tag) % 28;
            if (mada == 0)
                mada = 28;
            return mada;
        }

        private WochentagEnum Wochentag()
        {
            int j = Zeitrechnung.JahrUmrechnen(KalenderAnzeige, Kalender.BosparansFall, Jahr) - 993;

            int teil = j / 28;

            if (j < 0)
            {
                j = j + ((teil - 1) * -28);
                if (j == 28)
                    j = 0;
            }
            else
                j = j - (teil * 28);

            int mada = (j + (int)Monat * 2 + Tag) % 28;
            if (mada == 0)
                mada = 28;

            teil = (mada - 1) / 7;

            int wo = mada - (teil * 7);

            return (WochentagEnum)wo;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}", Tag, (int)Monat, Jahr, (int)KalenderAnzeige);
        }

        public string ToStringShort()
        {
            return string.Format("{0}. {1} {2} BF", Tag, MonatString(), Zeitrechnung.JahrUmrechnen(KalenderAnzeige, Kalender.BosparansFall, Jahr));
        }

        public string ToString(Kalender nach)
        {
            int j = Jahr;
            switch (nach)
            {
                case Kalender.BosparansFall:
                    string str = string.Empty;

                    str = string.Format("{0}, {1}. {2} {3}", WochentagString(nach), Tag, MonatString(nach),
                        Zeitrechnung.JahrUmrechnenToString(KalenderAnzeige, nach, Jahr));

                    if (Wochentag() == WochentagEnum.Rohalstag || Wochentag() == WochentagEnum.Wassertag)
                    { // Borland
                        str += string.Format("\n{0}, {1}. {2} {3} (Bornland)", WochentagString(Kalender.Bornland), Tag, MonatString(Kalender.Bornland),
                            Zeitrechnung.JahrUmrechnenToString(KalenderAnzeige, nach, Jahr));
                    }
                    else if (Wochentag() == WochentagEnum.Praiostag)
                    { // Neetha und Weiden
                        str += string.Format("\n{0}, {1}. {2} {3} (Neetha & Weiden)", WochentagString(Kalender.Kurkum), Tag, MonatString(Kalender.BosparansFall),
                            Zeitrechnung.JahrUmrechnenToString(KalenderAnzeige, nach, Jahr));
                    }
                    return str;
                case Kalender.Rastullah:
                    return string.Format("{0} {1}", NovadiTag(Tageszahl()),
                        Zeitrechnung.JahrUmrechnenToString(KalenderAnzeige, nach, Jahr, this));
                case Kalender.Imperium:
                    return string.Format("{0} {1}", MyranorTag(Tageszahl()),
                        Zeitrechnung.JahrUmrechnenToString(KalenderAnzeige, nach, Jahr, this));
                default:
                    return string.Format("{0}, {1}. {2} {3}", WochentagString(nach), Tag, MonatString(nach),
                        Zeitrechnung.JahrUmrechnenToString(KalenderAnzeige, nach, Jahr));
            }
        }

        public string MyranorTag(int tageszahl)
        {
            string myranorTag = string.Empty;

            if (tageszahl == 1)
            {
                myranorTag = "Sonnentag (Brajans-Solstitium)";
            }
            else if (tageszahl >= 2 && tageszahl <= 46)
            {
                myranorTag = MyranorNone(tageszahl - 1) + " im Brajan (5. Oktal)";
            }
            else if (tageszahl >= 47 && tageszahl <= 91)
            {
                myranorTag = MyranorNone(tageszahl - 46) + " im Raia (6. Oktal)";
            }
            else if (tageszahl == 92)
            {
                myranorTag = "Sturmtag (Chrysir-Aequinox)";
            }
            else if (tageszahl >= 93 && tageszahl <= 137)
            {
                myranorTag = MyranorNone(tageszahl - 92) + " im Chrysir (7. Oktal)";
            }
            else if (tageszahl >= 138 && tageszahl <= 182)
            {
                myranorTag = MyranorNone(tageszahl - 137) + " im Gyldara (8. Oktal)";
            }
            else if (tageszahl == 183)
            {
                myranorTag = "Frosttag (Nereton-Solstitium)";
            }
            else if (tageszahl == 184)
            {
                myranorTag = "Thearchentag (Neujahr)";
            }
            else if (tageszahl >= 185 && tageszahl <= 229)
            {
                myranorTag = MyranorNone(tageszahl - 184) + " im Nereton (1. Oktal)";
            }
            else if (tageszahl >= 230 && tageszahl <= 274)
            {
                myranorTag = MyranorNone(tageszahl - 229) + " im Siminia (2. Oktal)";
            }
            else if (tageszahl == 275)
            {
                myranorTag = "Nebeltag (Zatura-Aequinox)";
            }
            else if (tageszahl >= 276 && tageszahl <= 320)
            {
                myranorTag = MyranorNone(tageszahl - 276) + " im Zatura (3. Oktal)";
            }
            else if (tageszahl >= 321 && tageszahl <= 365)
            {
                myranorTag = MyranorNone(tageszahl - 320) + " im Shinxir (4. Oktal)";
            }

            if (myranorTag.StartsWith("Opfertag (5. Tag) der 3. None im "))
            {
                myranorTag = myranorTag.Replace("Opfertag (5. Tag) der 3. None im ", string.Empty)
                    .Replace(" (", "tag (Opfertag der 3. None im ");
            }

            return myranorTag;
        }

        private string MyranorNone(int tag)
        {
            string none = string.Empty;

            int tagesNr = (tag % 9);
            if (tagesNr == 0)
                tagesNr = 9;

            int nonenNr = ((int)Math.Ceiling(tag / 9.0));

            string tagName = string.Empty;
            switch (tagesNr)
            {
                case 1:
                    tagName = "Schaffenstag";
                    break;
                case 2:
                    tagName = "Ahnentag";
                    break;
                case 3:
                    tagName = "Ackertag";
                    break;
                case 4:
                    tagName = "Markttag";
                    break;
                case 5:
                    tagName = "Opfertag";
                    break;
                case 6:
                    tagName = "Rechtstag";
                    break;
                case 7:
                    tagName = "Fuhrtag";
                    break;
                case 8:
                    tagName = "Streittag";
                    break;
                case 9:
                    tagName = "Ruhetag";
                    break;
                default:
                    break;
            }
            none = tagName + " (" + tagesNr.ToString() + ". Tag) der " + nonenNr.ToString() + ". None";

            return none;
        }

        public string NovadiTag(int tageszahl)
        {
            string novadiTag = string.Empty;

            if (tageszahl >= 1 && tageszahl <= 68)
            { // +4 nach dem 3. Rh.
                tageszahl += 4;
                novadiTag = " nach dem 3. Rastullahellah";
            }
            else if (tageszahl == 69)
            { // 4. Rastullahellah
                tageszahl += 200;
                novadiTag = "4. Rastullahellah";
            }
            else if (tageszahl >= 70 && tageszahl <= 141)
            { // -69 nach dem 4. Rh.
                tageszahl -= 69;
                novadiTag = " nach dem 4. Rastullahellah";
            }
            else if (tageszahl == 142)
            { // 5. Rastullahellah
                novadiTag = "5. Rastullahellah";
            }
            else if (tageszahl >= 143 && tageszahl <= 214)
            { // -142 nach dem 5. Rh.
                tageszahl -= 142;
                novadiTag = " nach dem 5. Rastullahellah";
            }
            else if (tageszahl == 215)
            { // 1. Rastullahellah
                novadiTag = "1. Rastullahellah";
            }
            else if (tageszahl >= 216 && tageszahl <= 287)
            { // -215 nach dem 1. Rh.
                tageszahl -= 215;
                novadiTag = " nach dem 1. Rastullahellah";
            }
            else if (tageszahl == 288)
            { // 2. Rastullahellah
                novadiTag = "2. Rastullahellah";
            }
            else if (tageszahl >= 289 && tageszahl <= 360)
            { // -289 nach dem 2. Rh.
                tageszahl -= 289;
                novadiTag = " nach dem 2. Rastullahellah";
            }
            else if (tageszahl == 361)
            { // 3. Rastullahellah
                novadiTag = "3. Rastullahellah";
            }
            else if (tageszahl >= 362 && tageszahl <= 365)
            { // -361 nach dem 3. Rh.
                tageszahl -= 361;
                novadiTag = " nach dem 3. Rastullahellah";
            }

            if (tageszahl <= 9)
            { // 1. Gottesname
                novadiTag = tageszahl + ". Nacht des 1. Gottesnamen" + novadiTag;
            }
            else if (tageszahl <= 18)
            { // 2. Gottesname
                novadiTag = (tageszahl - 9) + ". Nacht des 2. Gottesnamen" + novadiTag;
            }
            else if (tageszahl <= 27)
            { // 3. Gottesname
                novadiTag = (tageszahl - 18) + ". Nacht des 3. Gottesnamen" + novadiTag;
            }
            else if (tageszahl <= 36)
            { // 4. Gottesname
                novadiTag = (tageszahl - 27) + ". Nacht des 4. Gottesnamen" + novadiTag;
            }
            else if (tageszahl <= 45)
            { // 5. Gottesname
                novadiTag = (tageszahl - 36) + ". Nacht des 5. Gottesnamen" + novadiTag;
            }
            else if (tageszahl <= 54)
            { // 6. Gottesname
                novadiTag = (tageszahl - 45) + ". Nacht des 6. Gottesnamen" + novadiTag;
            }
            else if (tageszahl <= 63)
            { // 7. Gottesname
                novadiTag = (tageszahl - 54) + ". Nacht des 7. Gottesnamen" + novadiTag;
            }
            else if (tageszahl <= 72)
            { // 8. Gottesname
                novadiTag = (tageszahl - 63) + ". Nacht des 8. Gottesnamen" + novadiTag;
            }

            novadiTag = novadiTag.Replace("1. Nacht", "Laila-al-Kira (1. Nacht / Nacht des Sieges)")
                .Replace("2. Nacht", "Laila-al-Kadir (2. Nacht / Nacht der Rechtsprechung)")
                .Replace("3. Nacht", "Laila-al-Iqbal (3. Nacht / Nacht des Glücks)")
                .Replace("4. Nacht", "Laila-ar-Ra'ad (4. Nacht / Nacht des Donners)")
                .Replace("5. Nacht", "Laila-ar-Rashid (5. Nacht / Weisheitsnacht)")
                .Replace("6. Nacht", "Laila-ash-Sharisa (6. Nacht / Nacht des Tanzes)")
                .Replace("7. Nacht", "Laila-al-Mhânash (7. Nacht / Nacht der Tradition)")
                .Replace("8. Nacht", "Laila-as-Sefra'iz (8. Nacht / Nacht des Frevels)")
                .Replace("9. Nacht", "Laila-al-Hafla (9. Nacht / Nacht des Festes)")
                .Replace("nach dem", "\nnach dem");

            return novadiTag;
        }

        public static string MondphaseString(int mada)
        {
            string str = string.Empty;
            if (mada < 7)
                str = "Zunehmend\n";
            else if (mada == 7)
                str = "Kelch\n";
            if (mada > 7 && mada < 14)
                str = "Zunehmend\n";
            else if (mada == 14)
                str = "Rad\n";
            if (mada > 14 && mada < 21)
                str = "Abnehmend\n";
            else if (mada == 21)
                str = "Helm\n";
            if (mada > 21 && mada < 28)
                str = "Abnehmend\n";
            else if (mada == 28)
                str = "Tote Mada\n";
            return string.Format("{0}(Phase {1})", str, mada);
        }

        public string SonnenAufUnterGang(DgSuche.Ortsmarke standort)
        {
            string aufStr = string.Empty;
            //double h1 = -0.0145; // geometrische Horizonthöhe
            double h1 = -0.20943951;
            double h2 = 0;
            //double h2 = -0.001745329251994332; // bürgerlicher Dämmerung
            //double h = -0.003490658503988664; // nautische Dämmerung
            //double h1 = -0.005235987755982996; // astronomische Dämmerung

            // Gareth
            decimal breite = 0.0M; // Breite, Latitude
            decimal länge = 0.0M; // Länge, Longitude
            if (standort != null && standort.Name != null && standort.Name != string.Empty)
            {
                try
                {
                    breite = Convert.ToDecimal(standort.Latitude, new System.Globalization.CultureInfo("en-US"));
                    länge = Convert.ToDecimal(standort.Longitude, new System.Globalization.CultureInfo("en-US"));
                }
                catch (Exception)
                {
                }
            }
            else
                standort = new DgSuche.Ortsmarke();

            decimal b = breite;
            b = (decimal)Math.PI * b / 180;
            int tag = Tageszahl() + 183;
            if (tag > 365)
                tag -= 365;

            double zeitgleichung = -0.171 * Math.Sin(0.0337 * tag + 0.465) - 0.1299 * Math.Sin(0.01787 * tag - 0.168);
            double deklination = 0.4095 * Math.Sin(0.016906 * (tag - 80.086));
            double zeitdifferenz1 = 12 * Math.Acos((Math.Sin(h1) - Math.Sin((double)b) * Math.Sin(deklination)) / (Math.Cos((double)b) * Math.Cos(deklination))) / Math.PI;
            double zeitdifferenz2 = 12 * Math.Acos((Math.Sin(h2) - Math.Sin((double)b) * Math.Sin(deklination)) / (Math.Cos((double)b) * Math.Cos(deklination))) / Math.PI;

            double aufgangMOZ1 = (12 - zeitdifferenz1 - zeitgleichung);// -länge / 15;
            double untergangMOZ1 = 12 + zeitdifferenz1 - zeitgleichung;
            double aufgangMOZ2 = 12 - zeitdifferenz2 - zeitgleichung;
            double untergangMOZ2 = 12 + zeitdifferenz2 - zeitgleichung;

            int stunde = (int)aufgangMOZ1;
            int minute = (int)((aufgangMOZ1 - ((int)aufgangMOZ1)) * 60);
            string morgenDäm = string.Format("Morgendämmerung:\t{0}:{1} Uhr", stunde.ToString("00"), minute.ToString("00"));
            stunde = (int)untergangMOZ2;
            minute = (int)((untergangMOZ2 - ((int)untergangMOZ2)) * 60);
            string abendDäm = string.Format("Abenddämmerung:\t{0}:{1} Uhr", stunde.ToString("00"), minute.ToString("00"));
            stunde = (int)aufgangMOZ2;
            minute = (int)((aufgangMOZ2 - ((int)aufgangMOZ2)) * 60);
            string aufgang = string.Format("Sonnenaufgang:\t\t{0}:{1} Uhr", stunde.ToString("00"), minute.ToString("00"));
            stunde = (int)untergangMOZ1;
            minute = (int)((untergangMOZ1 - ((int)untergangMOZ1)) * 60);
            string untergang = string.Format("Sonnenuntergang:\t{0}:{1} Uhr", stunde.ToString("00"), minute.ToString("00"));

            aufStr = string.Format("{0}\n{1}\n{2}\n{3})", morgenDäm, aufgang, abendDäm, untergang);

            return aufStr;
        }

        public List<Feiertag> Feiertage
        {
            get
            {
                List<Feiertag> feiertageListe = new List<Feiertag>();

                switch (Monat)
                {
                    case Monat.Praios:
                        if (Tag == 1)
                            feiertageListe.Add(new Feiertag("Sommersonnenwende", "religiöser Feiertag (Praios)",
                                "Jahresbeginn, höchstes Fest zu Ehren Praios", "Aventurien"));
                        if (Tag >= 1 && Tag <= 8)
                            feiertageListe.Add(new Feiertag("Kaiserturnier", "Turnier",
                                "Ritterturnier und Volksspiele", "Gareth"));
                        if (Tag == 2 || Tag == 3)
                        {
                            Feiertag f = new Feiertag("Praios- und Greifenfest", "religiöser Feiertag (Praios)",
                                "Fest zu Ehren Praios', Erhebungen in den höheren Adelsstand", "Mittelreich");
                            f.WikiLink = "Praiostag";
                            feiertageListe.Add(f);
                        }
                        if (Tag <= 7 && Wochentag() == WochentagEnum.Erdstag)
                            feiertageListe.Add(new Feiertag("Madatag", "religiöser Feiertag (Mada)",
                                "Fest zu Ehren Madas", "Magier (Aventurien)"));
                        if (Tag == 7)
                            feiertageListe.Add(new Feiertag("Horas' Erscheinen", "weltlicher Feiertag",
                                "", "Horasreich"));
                        if (Tag == 15 || Tag == 16)
                            feiertageListe.Add(new Feiertag("Bürgerheerparade", "weltlicher Feiertag",
                                "", "Gareth"));
                        break;
                    case Monat.Rondra:
                        if (Tag == 5)
                            feiertageListe.Add(new Feiertag("Tag des Schwurs", "religiöser Feiertag (Rondra)",
                                "", "Rondra-Gläubige"));
                        if (Tag == 15 || Tag == 16)
                            feiertageListe.Add(new Feiertag("Schwertfest", "religiöser Feiertag (Rondra)",
                                "", "Rondra-Kirche"));
                        if (Tag == 19)
                            feiertageListe.Add(new Feiertag("Maraskanisches Neujahrsfest", "religiöser Feiertag (Rur und Gror)",
                                "", "Maraskan, Rur und Gror-Gläubige"));
                        if (Tag == 27)
                            feiertageListe.Add(new Feiertag("Vertreibung Fuldigors", "religiöser Feiertag (Horas)",
                                "", "Horasreich"));
                        break;
                    case Monat.Efferd:
                        if (Tag == 1)
                            feiertageListe.Add(new Feiertag("Tag des Wassers", "religiöser Feiertag (Efferd)",
                                "", "Thorwal, alle aventurische Küsten, Teile der Khôm"));
                        if (Tag == 9)
                        {
                            Feiertag f = new Feiertag("4. Rastullahellah, Ruhetag", "religiöser Feiertag (Rastullah)",
                                "", "Kalifat, Rastullahgläubige");
                            f.WikiLink = "Rastullahellah";
                            feiertageListe.Add(f);
                        }
                        if (Tag == 16)
                            feiertageListe.Add(new Feiertag("Nebelfest", "religiöser Feiertag (Phex)",
                                "", "Phex-gläubige Händler und Diebe"));
                        if (Tag == 30)
                        {
                            feiertageListe.Add(new Feiertag("Fischerfest", "religiöser Feiertag (Efferd)",
                                "", "Efferd-gläubige Fischer und Seefahrer"));
                            feiertageListe.Add(new Feiertag("Prüfungsfest", "religiöser Feiertag (Hesinde)",
                                "", ""));
                        }
                        break;
                    case Monat.Travia:
                        if (Tag == 1)
                            feiertageListe.Add(new Feiertag("Tag der Heimkehr", "religiöser Feiertag (Travia)",
                                "", "Nordaventurien"));
                        if (Tag >= 1 && Tag <= 3)
                            feiertageListe.Add(new Feiertag("Fest der eingebrachten Früchte", "religiöser Feiertag (Peraine)",
                                "", "zwölfgöttergläubige Länder"));
                        if (Tag == 4)
                            feiertageListe.Add(new Feiertag("Tag der Helden", "religiöser Feiertag (Rondra)",
                                "", "Rondra-Kirche"));
                        if (Tag == 12)
                            feiertageListe.Add(new Feiertag("Tag der Treue", "religiöser Feiertag (Travia)",
                                "", ""));
                        if (Tag == 29)
                            feiertageListe.Add(new Feiertag("Tag des Heiligen Gilborn", "religiöser Feiertag (Praios)",
                                "", "Länder mit Zwölfgötter-Glauben"));
                        break;
                    case Monat.Boron:
                        if (Tag == 1)
                            feiertageListe.Add(new Feiertag("Totenfest", "religiöser Feiertag (Boron)",
                                "", "Aventurien"));
                        if (Tag == 22)
                        {
                            Feiertag f = new Feiertag("5. Rastullahellah, Neujahrsfest", "religiöser Feiertag (Rastullah)",
                                "", "Kalifat, Rastullahgläubige");
                            f.WikiLink = "Rastullahellah";
                            feiertageListe.Add(f);

                            feiertageListe.Add(new Feiertag("Borbarads Verkörperung", "religiöser Feiertag (Borbarad-Kirche)",
                                "", "Borbaradianer"));
                        }
                        if (Tag == 23)
                            feiertageListe.Add(new Feiertag("Rastullahs Erscheinen", "religiöser Feiertag (Rastullah)",
                                "", "Kalifat, Rastullahgläubige"));
                        if (Tag == 30)
                            feiertageListe.Add(new Feiertag("Tag des Großen Schlafs", "religiöser Feiertag (Boron)",
                                "", "Al'Anfa"));
                        break;
                    case Monat.Hesinde:
                        if (Tag == 30)
                            feiertageListe.Add(new Feiertag("Erleuchtungsfest", "religiöser Feiertag (Hesinde)",
                                "", ""));
                        break;
                    case Monat.Firun:
                        if (Tag == 1)
                        {
                            Feiertag f = new Feiertag("Tag der Jagd", "religiöser Feiertag (Firun)",
                                "", "Norden Aventuriens und Mittelreich");
                            f.WikiLink = "Tag der Jagd (Feiertag)";
                            feiertageListe.Add(f);
                            feiertageListe.Add(new Feiertag("Wintersonnenwende", "kultureller Feiertag",
                                "", "Thorwal, Trollzacker"));
                        }
                        if (Tag == 11)
                            feiertageListe.Add(new Feiertag("Tag des Hirsches", "religiöser Feiertag (Firun)",
                                "", "Bjaldorn, Nostria"));
                        if (Tag == 30)
                        {
                            Feiertag f = new Feiertag("Tag der Ifirn / Tag der Weißen Maid (Weiden)", "religiöser Feiertag (Firun, Ifirn)",
                                "", "Nordaventurien (wo Schnee liegt)");
                            f.WikiLink = "Tag der Ifirn";
                            feiertageListe.Add(f);
                        }
                        break;
                    case Monat.Tsa:
                        if (Tag == 5)
                        {
                            Feiertag f = new Feiertag("1. Rastullahellah, Tag des Fastens", "religiöser Feiertag (Rastullah)",
                                "", "Kalifat, Rastullahgläubige");
                            f.WikiLink = "Tag des Fastens";
                            feiertageListe.Add(f);
                        }
                        if (Tag == 30)
                            feiertageListe.Add(new Feiertag("Tag der Erneuerung", "religiöser Feiertag (Tsa)",
                                "", "Aventurien, vor allem Norden"));
                        break;
                    case Monat.Phex:
                        if (Tag == 1)
                            feiertageListe.Add(new Feiertag("Tag der Erneuerung", "religiöser Feiertag (Tsa)",
                                "", "Aventurien, vor allem Norden"));
                        if (Tag == 16)
                            feiertageListe.Add(new Feiertag("Tag des Phex", "religiöser Feiertag (Phex)",
                                "", ""));
                        if (Tag == 24)
                            feiertageListe.Add(new Feiertag("Glückstag", "religiöser Feiertag (Phex)",
                                "", "Aventurien"));
                        if (Tag == 30)
                            feiertageListe.Add(new Feiertag("Versenkungsfest", "religiöser Feiertag (Hesinde)",
                                "", ""));
                        break;
                    case Monat.Peraine:
                        if (Tag == 1)
                            feiertageListe.Add(new Feiertag("Saatfest", "religiöser Feiertag (Peraine)",
                                "", ""));
                        if (Tag == 4)
                            feiertageListe.Add(new Feiertag("Tag der Heiligen Thalionmel", "religiöser Feiertag (Rondra)",
                                "", "Westaventurien, besonders Neetha"));
                        if (Tag == 18)
                        {
                            Feiertag f = new Feiertag("2. Rastullahellah, Tag der Treue", "religiöser Feiertag (Rastullah)",
                                "", "Kalifat, Rastullahgläubige");
                            f.WikiLink = "Rastullahellah";
                            feiertageListe.Add(f);
                        }
                        break;
                    case Monat.Ingerimm:
                        if (Tag == 1)
                            feiertageListe.Add(new Feiertag("Tag des Feuers", "religiöser Feiertag (Angrosch)",
                                "", "Zwerge, Angbar"));
                        if (Tag == 7)
                            feiertageListe.Add(new Feiertag("Tag der Abschwörung", "religiöser Feiertag (Angrosch)",
                                "Abschwörung gegen Drachen (alle sieben Jahre)", "Xorlosch"));
                        if (Tag == 8)
                            feiertageListe.Add(new Feiertag("Tag des Aufbruchs", "religiöser Feiertag (Angrosch)",
                                "", "Zwerge"));
                        if (Tag == 21)
                            feiertageListe.Add(new Feiertag("Tag der Waffenschmiede", "religiöser Feiertag (Ingerimm)",
                                "", "hauptsächlich Nord- und Mittelaventurien"));
                        if (Tag == 23)
                            feiertageListe.Add(new Feiertag("Borbarads Entrückung", "religiöser Feiertag (Borbarad-Kirche)",
                                "", "Borbaradianer"));
                        break;
                    case Monat.Rahja:
                        if (Tag >= 1 && Tag <= 7)
                            feiertageListe.Add(new Feiertag("Fest der Freuden", "religiöser Feiertag (Rahja)",
                                "", "Aventurien"));
                        if (Tag == 2)
                            feiertageListe.Add(new Feiertag("Borbarads Enthüllung", "religiöser Feiertag (Borbarad-Kirche)",
                                "", "Borbaradianer"));
                        if (Tag == 30)
                        {
                            feiertageListe.Add(new Feiertag("Reinigungsfest", "religiöser Feiertag (Hesinde)",
                                "", ""));
                            feiertageListe.Add(new Feiertag("Tag des Blutes", "religiöser Feiertag (Schoma)",
                                "", "Trollzacker"));
                        }
                        break;
                    case Monat.NamenloseTage:
                        if (Tag == 1)
                        {
                            Feiertag f = new Feiertag("3. Rastullahellah, Tag der Blutrache", "religiöser Feiertag (Rastullah)",
                                "", "Kalifat, Rastullahgläubige");
                            f.WikiLink = "Tag der Blutrache";
                            feiertageListe.Add(f);
                        }
                        break;
                    default:
                        break;
                }
                return feiertageListe;
            }
        }
    }
}
