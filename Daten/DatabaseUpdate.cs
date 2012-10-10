﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Reflection;
using System.IO;
//Eigene Usings
using App = System.Windows.Application;
using MeisterGeister.View.Windows;

namespace MeisterGeister.Daten
{
    public static class DatabaseUpdate
    {
        #region //---- FELDER & EIGENSCAHFTEN ----

        /// <summary>
        /// Die aktuell benötigte Datenbank-Version.
        /// </summary>
        public const int DatenbankVersionAktuell = 58;

        /// <summary>
        /// Das zuletzt ausgeführte Update-Skript.
        /// </summary>
        public static string UpdateSkript { get; set; }

        public static int UserDatabaseVersion { get; set; }

        public static int UpdatetToVersion { get; set; }

        public static string UpdateStatus
        {
            get
            {
                return "\nBenutzer-Datenbank Version: " + UserDatabaseVersion
                    + "\nLetzte Update Version: " + UpdatetToVersion
                    + "\n\nUpdate-Skript: " + UpdateSkript;
            }
        }

        #endregion

        #region //---- METHODEN ----

        /// <summary>
        /// Führt ein Update aller Datenbanken in einem Verzeichnis durch.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static DatabaseUpdateResult PerformDirectoryDatabaseUpdate(string directoryPath)
        {
            return PerformDatabaseUpdate(GetDatabaseConnectionStringsFromDir(directoryPath));
        }

        /// <summary>
        /// Gibt eine Liste von ConnectionStrings aller Datenbanekn in einem Verzeichnis zurück.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static List<string> GetDatabaseConnectionStringsFromDir(string directoryPath)
        {
            List<string> databases = new List<string>();

            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException();

            string[] files = Directory.GetFiles(directoryPath, "*.sdf");

            foreach (string file in files)
                databases.Add("Data Source=" + file);

            return databases;
        }

        /// <summary>
        /// Gibt die VersionsNummer der Datenbank zurück.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static int GetUserDatabaseVersion(string connectionString)
        {
            UpdateSkript = "GetDatabaseVersion";
            UserDatabaseVersion = Convert.ToInt32(GetScalarFromDatabase("SELECT VersionsNummer FROM Version WHERE Name = 'Datenbank'", connectionString));
            return UserDatabaseVersion;
        }

        /// <summary>
        /// Führt auf der Standard Datenbank ein Update aus, falls erforderlich.
        /// </summary>
        /// <returns></returns>
        public static DatabaseUpdateResult PerformDatabaseUpdate()
        {
            return PerformDatabaseUpdate(MeisterGeister.Properties.Settings.Default.DatabaseDSAConnectionString);
        }

        /// <summary>
        /// Führt auf eine Liste von Datenbanken ein Update durch.
        /// </summary>
        /// <param name="connectionStrings">Liste mit ConnectionStrings.</param>
        /// <returns></returns>
        public static DatabaseUpdateResult PerformDatabaseUpdate(List<string> connectionStrings)
        {
            foreach (string db in connectionStrings)
                PerformDatabaseUpdate(db);

            return DatabaseUpdateResult.DatenbankUpdateOK;
        }

        /// <summary>
        /// Führt auf der angegebenen Datenbank ein Update aus, falls erforderlich.
        /// </summary>
        /// <param name="connectionString">Datenbank, die upgedated werden soll.</param>
        /// <returns></returns>
        public static DatabaseUpdateResult PerformDatabaseUpdate(string connectionString)
        {
            GetUserDatabaseVersion(connectionString);

            if (UserDatabaseVersion > DatenbankVersionAktuell) // Programm ist zu alt für die Datenbank
                return DatabaseUpdateResult.DatenbankNeuer;
            else if (UserDatabaseVersion < DatenbankVersionAktuell) // Version ist falsch --> Update durchführen
            {
                // Backup erstellen
                DatabaseTools.BackupDatabase("BACKUP_UPDATE_" + UserDatabaseVersion);

                // Updates durchführen
                for (int i = ((int)UserDatabaseVersion + 1); i <= DatenbankVersionAktuell; i++)
                    UpdateDatabase(i, connectionString);

                // Handelsgüter einfügen
                SqlCeConnection connection = new SqlCeConnection(connectionString);
                SqlCeTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    // lies die Insert-Befehle aus der Resourcen-Datei
                    StreamReader reader = new StreamReader(App.GetResourceStream(new Uri("/DSA MeisterGeister;component/Daten/InsertHandelsgut.txt", UriKind.Relative)).Stream);
                    string inserts = reader.ReadToEnd();
                    ExecuteSqlCommand(inserts, "InsertHandelsgut", connection, transaction, false);
                    if (transaction != null)
                        transaction.Commit();
                }
                catch (Exception)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    throw;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

                // Datenbank optimieren
                DatabaseTools.CompactDatabase(connectionString);

                return DatabaseUpdateResult.DatenbankUpdateOK;
            }
            return DatabaseUpdateResult.DatenbankVersionOK; // kein Update erforderlich
        }

        /// <summary>
        /// Löscht alte, nicht mehr benötigte Dateien und Verzeichneisse.
        /// </summary>
        public static void CleanUpDirectory()
        {
            // DLLs aufräumen
            // werden in den aktuellen Versionen nicht mehr benötigt
            // da Compact Framework Installations-Voraussetzung ist
            File.Delete("sqlcecompact35.dll");
            File.Delete("sqlceme35.dll");
            File.Delete("sqlceqp35.dll");
            File.Delete("sqlcese35.dll");
            File.Delete("System.Data.SqlServerCe.dll");
            // PlugIn-DLLs aufräumen
            File.Delete("DSAToolPlugin.dll");
            if (Directory.Exists("PlugIns"))
            {
                foreach (string file in Directory.GetFiles("PlugIns"))
                    File.Delete(file);

                Directory.Delete("PlugIns");
            }

            // Datenbank-Backups aufräumen
            DatabaseTools.MoveBackupsToBackupFolder();
        }

        // Connection wird nicht geschlossen!
        private static int UpdateTo_SetDatabaseVersion(int version, SqlCeConnection connection)
        {
            UpdateSkript = "SetDatabaseVersion";
            int result = 0;

            string command = string.Format("UPDATE Version SET VersionsNummer = {0} WHERE Name = 'Datenbank'", version);
            SqlCeCommand com = new SqlCeCommand(command, connection);
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            result = com.ExecuteNonQuery();

            return result;
        }

        public static void UpdateDatabase(int version, string connectionString)
        {
            // Ermitteln, ob es Update-Commands in den Resources gibt
            Type resourcesType = Type.GetType("MeisterGeister.Properties.Resources");
            System.Collections.Generic.SortedList<string, string> sqlCommands = new System.Collections.Generic.SortedList<string, string>();
            foreach (var property in resourcesType.GetProperties(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (property.Name.StartsWith(string.Format("UpdateTo_V{0}", version.ToString("D4"))))
                    sqlCommands.Add(property.Name, property.GetValue(null, null).ToString());
            }

            // Update-Commands ausführen
            SqlCeConnection connection = new SqlCeConnection(connectionString);
            SqlCeTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                
                foreach (var command in sqlCommands)
                    ExecuteSqlCommand(command.Value, command.Key, connection, transaction, false, command.Key.EndsWith("_GO"));

                // Neue Versionsnummer setzen
                UpdateTo_SetDatabaseVersion(version, connection);
                UpdatetToVersion = version;

                if (transaction != null)
                    transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static void ExecuteSqlCommand(string commands, string skriptName, SqlCeConnection connection, SqlCeTransaction transaction, bool closeConnection = true, bool splitWithGO = false)
        {
            try
            {
                SqlCeCommand command = new SqlCeCommand();
                command.Connection = connection;
                command.Transaction = transaction;

                string[] statements = null;
                if (splitWithGO)
                    statements = commands.Split(new string[] { "GO" + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                else
                    statements = commands.Split(new string[] { ";" + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                foreach (string statement in statements)
                {
                    string statementtrimmed = statement.Trim();
                    if (statementtrimmed.EndsWith(Environment.NewLine + "GO"))
                        statementtrimmed = statementtrimmed.Substring(0, statementtrimmed.Length - (Environment.NewLine + "GO").Length);
                    if (statementtrimmed == string.Empty)
                    {
                        break;
                    }
                    UpdateSkript = skriptName + Environment.NewLine + statementtrimmed;
                    command.CommandText = statementtrimmed;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection && connection != null)
                    connection.Close();
            }
        }

        public static object GetScalarFromDatabase(string command, string connString)
        {
            var conn = new SqlCeConnection(connString);
            object result = null;

            try
            {
                SqlCeCommand com = new SqlCeCommand(command, conn);
                com.Connection.Open();
                result = com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        #endregion

    }

    public enum DatabaseUpdateResult
    {
        UnbekanntesErgebnis,
        /// <summary>
        /// Die Datenbank-Version stimmt überein. Update war nicht erforderlich.
        /// </summary>
        DatenbankVersionOK,
        /// <summary>
        /// Datenbank-Version ist neuer als die benötigte Version.
        /// </summary>
        DatenbankNeuer,
        /// <summary>
        /// Das Datenbank-Update wurde erfolgreich durchgeführt.
        /// </summary>
        DatenbankUpdateOK,
        /// <summary>
        /// Beim Update ist ein Fehler aufgetreten.
        /// </summary>
        DatenbankUpdateError
    }
}
