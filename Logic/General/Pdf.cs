﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MeisterGeister.Logic.General
{
    public static class Pdf
    {
        private static string openCommand = null;
        public static string OpenCommand
        {
            get {
                if (Pdf.openCommand == null)
                {
                    Pdf.openCommand = Settings.Einstellungen.PdfReaderCommand;
                    if (String.IsNullOrWhiteSpace(Pdf.openCommand))
                        SetReader("Adobe Acrobat Reader");
                }
                return Pdf.openCommand; 
            }
            set { Pdf.openCommand = value; }
        }

        private static string openArguments = null;
        public static string OpenArguments
        {
            get
            {
                if (Pdf.openArguments == null)
                {
                    Pdf.openArguments = Settings.Einstellungen.PdfReaderArguments;
                    if (String.IsNullOrWhiteSpace(Pdf.openArguments))
                        SetReader("Adobe Acrobat Reader");
                } 
                return Pdf.openArguments;
            }
            set { Pdf.openArguments = value; }
        }

        public static Dictionary<string, string[]> readers = new Dictionary<string, string[]>();

        static Pdf()
        {
            readers.Add("Adobe Acrobat Reader", new string[] { "AcroRd32.exe", "/A \"page={1}\" \"{0}\"" });
            readers.Add("Adobe Acrobat", new string[] { "Acrobat.exe", "/A \"page={1}\" \"{0}\"" });
            readers.Add("Evince", new string[] { "evince.exe", "--page-label={1} \"{0}\"" });
            readers.Add("Xpdf", new string[] { "xpdf.exe", "\"{0}\" {1}" });
            readers.Add("Foxit Reader", new string[] { "Foxit Reader.exe", "/A \"page={1}\" \"{0}\"" });
            readers.Add("Sumatra PDF", new string[] { "SumatraPDF.exe", "-page {1} \"{0}\"" });
        }

        public static bool SetReader(string readerName)
        {
            if(readers.ContainsKey(readerName))
            {
                Pdf.OpenCommand = readers[readerName][0];
                Pdf.OpenArguments = readers[readerName][1];
                return true;
            }
            return false;
        }

        public static Process OpenReader(Literatur.Literaturangabe literaturangabe, Literatur.Seitenangabe seitenangabe)
        {
            if (literaturangabe == null)
                throw new ArgumentNullException("literaturangabe");
            if (seitenangabe == null)
                throw new ArgumentNullException("seitenangabe");
            return OpenReader(literaturangabe.Kürzel, seitenangabe.Seite, seitenangabe.IsErrata);
        }

        public static Process OpenReader(string literaturKürzel, int page = 1, bool isErrata = false)
        {
            var l = Model.Literatur.GetByAbkürzung(literaturKürzel, isErrata);
            if (l == null || String.IsNullOrWhiteSpace(l.Pfad))
                throw new Literatur.LiteraturPfadMissingException(literaturKürzel, l);
            string fileName = l.Pfad;
            return OpenFileInReader(fileName, page);
        }

        public static Process OpenFileInReader(string fileName, int page = 1)
        {
            if(String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName", "Dateiname (fileName) wurde nicht angegeben.");
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo(OpenCommand, String.Format(OpenArguments, fileName, page));
            p.StartInfo = pi;
            pi.UseShellExecute = true;
            p.Start();
            return p;
        }

        
    }
}
