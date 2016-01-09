using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MeisterGeister.Logic.Extensions
{
    public static class StringExtenstions
    {
        private static CompareInfo compareInfo = CultureInfo.CurrentCulture.CompareInfo;
        /// <summary>
        /// Ist schneller und erstellt weniger überflüssige string Instanzen als ToLower()
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tofind"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string source, string tofind)
        {
            return compareInfo.IndexOf(source, tofind) >= 0;
        }

        public static string Left(this string str, int length)
        {
            if (str == null)
                return null;
            if (length > str.Length)
                length = str.Length;
            return str.Substring(0, length);
        }

        public static string Right(this string str, int length)
        {
            if (str == null)
                return null;
            if (length > str.Length)
                length = str.Length;
            return str.Substring(str.Length-length, length);
        }

        private static Regex reKlammern = new Regex("([^\\(]+)\\((.+)\\)");

        public static string GetAusdruckInKlammern(this string str)
        {
            if (str == null)
                return null;
            var m = reKlammern.Match(str);
            if (m != null && m.Groups.Count == 3)
                return m.Groups[2].Value.Trim();
            return null;
        }

        /// <summary>
        /// Entfernt mehrfache Leerzeichen aus einem String. Diese werden durch ein einfaches ersetzt.
        /// </summary>
        public static string EntferneMehrfacheLeerzeichen(this string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, "[ ]+", " ");
        }

        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
