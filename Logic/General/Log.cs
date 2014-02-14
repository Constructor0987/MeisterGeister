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
        public static void LogMsgToFile(String message)
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

        public static string PerformanceLog { get; set; }

        public static void SetPerformanceLog(string logMsg)
        {
            PerformanceLog = String.Join(Environment.NewLine, PerformanceLog, logMsg);
        }

        
        /// <summary>
        /// Startet eine Zeit-Performance Messung.
        /// </summary>
        /// <param name="msg">Angabe was gemessen wird.</param>
        public static LogInfo PerformanceLogStart(string msg)
        {
            return new LogInfo(msg);
        }

        public static LogInfo PerformanceLogEnd(LogInfo log)
        {
            log.EndTimeStamp = DateTime.Now;
            SetPerformanceLog(log.ToString());
            return log;
        }
    }

    public class LogInfo
    {
        public DateTime StartTimeStamp { get; set; }
        public DateTime EndTimeStamp { get; set; }
        public string Message { get; set; }

        public TimeSpan TimeSpan
        {
            get { return EndTimeStamp - StartTimeStamp; }
        }

        public LogInfo()
        {
            StartTimeStamp = DateTime.Now;
            EndTimeStamp = DateTime.Now;
        }

        public LogInfo(string msg) : this()
        {
            Message = msg;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", TimeSpan, Message);
        }
    }
}