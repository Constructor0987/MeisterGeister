using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MeisterGeister.Daten
{
    public static class SqlCeUpgrade
    {
        private const string DatabasePwd = ";Password=m3ist3rg3ist3r;Persist Security Info=True";

        public static void Run(string fileName)
        {
            string connectionString = "Data Source=" + fileName + DatabasePwd;
            var engine = new System.Data.SqlServerCe.SqlCeEngine(connectionString);

            // Check Password
            if (!engine.Verify())
            {
                engine = new System.Data.SqlServerCe.SqlCeEngine("Data Source=" + fileName);
                engine.Compact(connectionString);
                engine = new System.Data.SqlServerCe.SqlCeEngine(connectionString);
            }

            engine.EnsureVersion40(fileName);
        }

        public static void RunDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                throw new DirectoryNotFoundException();

            string[] files = Directory.GetFiles(dirPath, "*.sdf");

            foreach (string file in files)
                Run(file);
        }

        public static void EnsureVersion40(this System.Data.SqlServerCe.SqlCeEngine engine, string filename)
        {
            SQLCEVersion fileversion = DetermineVersion(filename);
            if (fileversion == SQLCEVersion.SQLCE20)
                throw new ApplicationException("Unable to upgrade from 2.0 to 4.0");

            if (SQLCEVersion.SQLCE40 > fileversion)
            {
                engine.Upgrade();
            }
        }
        private enum SQLCEVersion
        {
            SQLCE20 = 0,
            SQLCE30 = 1,
            SQLCE35 = 2,
            SQLCE40 = 3
        }
        private static SQLCEVersion DetermineVersion(string filename)
        {
            var versionDictionary = new Dictionary<int, SQLCEVersion> 
        { 
            { 0x73616261, SQLCEVersion.SQLCE20 }, 
            { 0x002dd714, SQLCEVersion.SQLCE30},
            { 0x00357b9d, SQLCEVersion.SQLCE35},
            { 0x003d0900, SQLCEVersion.SQLCE40}
        };
            int versionLONGWORD = 0;
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fs.Seek(16, SeekOrigin.Begin);
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        versionLONGWORD = reader.ReadInt32();
                    }
                }
            }
            catch
            {
                throw;
            }
            if (versionDictionary.ContainsKey(versionLONGWORD))
            {
                return versionDictionary[versionLONGWORD];
            }
            else
            {
                throw new ApplicationException("Unable to determine database file version");
            }
        }


    }
}