using System;
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
                    if (Pdf.openCommand == null)
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
                    if (Pdf.openArguments == null)
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


        public static void OpenReader(string litertaturShort, int page)
        {
            string fileName = "C:\\Spiele\\DSA\\Books\\Wege der Alchimie - Preisliste.pdf";
            OpenFile(fileName, page);
        }

        public static void OpenFile(string fileName, int page)
        {
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo("AcroRd32.exe", String.Format(openArguments, fileName, page));
            p.StartInfo = pi;
            pi.UseShellExecute = true;
            p.Start();
        }

        
    }
}
