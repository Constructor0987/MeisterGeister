using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.Extensions
{
    public static class StringExtenstions
    {
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
        /// Wandelt 'path' in eine relative Pfadangabe in Relation zum MeisterGeister-Verzeichnis um.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConvertAbsoluteToRelativePath(string path)
        {
            // TODO: Funktioniert nicht, wenn der Pfad auf einem anderen Laufwerk liegt.
            Uri file = new Uri(path);
            Uri homePath = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\");
            Uri relativePath = homePath.MakeRelativeUri(file);
            return relativePath.ToString().Replace("/", "\\").Insert(0, "\\");
        }

    }
}
