﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            return Uri.UnescapeDataString(relativePath.ToString()).Replace("/", "\\").Insert(0, "\\");
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

    }
}
