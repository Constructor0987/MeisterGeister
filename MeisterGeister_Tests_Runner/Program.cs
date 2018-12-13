using System;
using System.IO;
using System.Reflection;

namespace MeisterGeister_Tests_Runner
{
    internal class Program
    {
        private static string nunitpath = @"C:\Program Files (x86)\NUnit 2.5.10\bin\net-2.0\nunit-console.exe";

        private static void Main(string[] args)
        {
            ReadNunitPath();
            AppDomain.CurrentDomain.ExecuteAssembly(
                nunitpath,
                new string[] { Assembly.GetAssembly(typeof(MeisterGeister_Tests.Service_Tests)).Location }
            );
        }

        private static void ReadNunitPath()
        {
            var nunitini = @"..\..\nunit.ini";
            if (!File.Exists(nunitini))
            {
                throw new FileNotFoundException(string.Format("Die Datei nunit.ini fehlt im MeisterGeister_Tests_Runner-Verzeichnis ({0}). In ihr muss der Pfad zu nunit-console.exe eingetragen werden.", Path.GetFullPath(nunitini)));
            }

            nunitpath = System.IO.File.ReadAllText(nunitini);
        }
    }
}
