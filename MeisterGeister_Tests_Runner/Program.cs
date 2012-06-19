using System;
using System.Reflection;
using System.IO;
using System.Text;

namespace MeisterGeister_Tests_Runner
{
    class Program
    {
        static string nunitpath = @"C:\Program Files (x86)\NUnit 2.5.10\bin\net-2.0\nunit-console.exe";
        static void Main(string[] args)
        {
            ReadNunitPath();
            AppDomain.CurrentDomain.ExecuteAssembly(
                nunitpath,
                new string[] { Assembly.GetAssembly(typeof(MeisterGeister_Tests.Service_Tests)).Location }
            );
        }

        static void ReadNunitPath()
        {
            string nunitini = @"..\..\nunit.ini";
            if (!File.Exists(nunitini))
                throw new FileNotFoundException(String.Format("Die Datei nunit.ini fehlt im MeisterGeister_Tests_Runner-Verzeichnis ({0}). In ihr muss der Pfad zu nunit-console.exe eingetragen werden.", Path.GetFullPath(nunitini)));
            nunitpath = System.IO.File.ReadAllText(nunitini);
        }
    }
}
