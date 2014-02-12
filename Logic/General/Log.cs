using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace MeisterGeister.Logic.General
{
    /// <summary>
    /// Klasse zum Erzeugen von Logging Informationen für Debug-Zwecke.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Schreibt einen Text in eine Debug-Datei.
        /// Für jedes Datum wird eine eigene Datei erzeugt: "Log_yyyy_MM_dd.log".
        /// </summary>
        /// <param name="message"></param>
        public static void LogMsg(String message)
        {
            DateTime datet = DateTime.Now;
            String filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + "Log" + datet.ToString("yyyy_MM_dd") + ".log";
            if (!File.Exists(filePath))
            {
                FileStream files = File.Create(filePath);
                files.Close();
            }
            try
            {
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine(datet.ToString("yyyy/MM/dd hh:mm") + "> " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}