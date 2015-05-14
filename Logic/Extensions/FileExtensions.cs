using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.Extensions
{
    public static class FileExtensions
    {
        public static string[] EXTENSIONS_IMAGES = new string[] { "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff" };
        public static string[] EXTENSIONS_AUDIO = new string[] { "mp3", "wav", "ogg", "wma" };

        /// <summary>
        /// Wandelt 'path' in eine relative Pfadangabe in Relation zum MeisterGeister-Verzeichnis um.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConvertAbsoluteToRelativePath(string path)
        {
            Uri file = new Uri(path);
            Uri homePath = new Uri(GetHomeDirectory());
            Uri relativePath = homePath.MakeRelativeUri(file);
            return Uri.UnescapeDataString(relativePath.ToString()).Replace("/", "\\");
        }

        /// <summary>
        /// Gibt das MeisterGeister Stammverzeichnis als String zurück.
        /// </summary>
        /// <returns></returns>
        public static string GetHomeDirectory()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\";
        }

        /// <summary>
        /// Setzt das CurrentDirectory auf das MeisterGeister Stammverzeichnis.
        /// </summary>
        public static void SetCurrentDir()
        {
            Environment.CurrentDirectory = Logic.Extensions.FileExtensions.GetHomeDirectory();
        }

        /// <summary>
        /// Setzt das CurrentDirectory auf "path'.
        /// </summary>
        /// <param name="path"></param>
        public static void SetCurrentDir(string path)
        {
            Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(path);
        }

        [DllImport("Shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, [In][Out] ref uint pcchOut);

        /// <summary>
        /// Gibt auf Windows-Systemen Informationen zum Standardprogramm zu einer Dateiendung zurück.
        /// </summary>
        /// <param name="assocStr">z.B. für den Programmpfad AssocStr.Executable</param>
        /// <param name="doctype">z.B. ".pdf"</param>
        /// <returns></returns>
        public static string FileExtentionInfo(AssocStr assocStr, string doctype)
        {
            uint pcchOut = 0;
            AssocQueryString(AssocF.Verify, assocStr, doctype, null, null, ref pcchOut);

            StringBuilder pszOut = new StringBuilder((int)pcchOut);
            AssocQueryString(AssocF.Verify, assocStr, doctype, null, pszOut, ref pcchOut);
            return pszOut.ToString();
        }

        [Flags]
        public enum AssocF
        {
            Init_NoRemapCLSID = 0x1,
            Init_ByExeName = 0x2,
            Open_ByExeName = 0x2,
            Init_DefaultToStar = 0x4,
            Init_DefaultToFolder = 0x8,
            NoUserSettings = 0x10,
            NoTruncate = 0x20,
            Verify = 0x40,
            RemapRunDll = 0x80,
            NoFixUps = 0x100,
            IgnoreBaseClass = 0x200
        }

        public enum AssocStr
        {
            Command = 1,
            Executable,
            FriendlyDocName,
            FriendlyAppName,
            NoOpen,
            ShellNewValue,
            DDECommand,
            DDEIfExec,
            DDEApplication,
            DDETopic
        }
    }
}
