using System;
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
        public const int DatenbankVersionAktuell = 93;

        private const string DatabasePwd = ";Password=m3ist3rg3ist3r;Persist Security Info=False";

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
                databases.Add("Data Source=" + file + DatabasePwd);

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
            System.Diagnostics.Debug.WriteLine(string.Format("Datenbank Update wird gestartet: {0}", connectionString.Replace(DatabasePwd, string.Empty)));

            GetUserDatabaseVersion(connectionString);

            if (UserDatabaseVersion > DatenbankVersionAktuell) // Programm ist zu alt für die Datenbank
                return DatabaseUpdateResult.DatenbankNeuer;
            else if (UserDatabaseVersion < DatenbankVersionAktuell) // Version ist falsch --> Update durchführen
            {
                // Backup erstellen
                DatabaseTools.BackupDatabase("BACKUP_UPDATE_" + UserDatabaseVersion);

                //Datenbank reparieren
                DatabaseTools.RepairDatabase(connectionString);

                // Updates durchführen
                for (int i = ((int)UserDatabaseVersion + 1); i <= DatenbankVersionAktuell; i++)
                {
                    //die letzte Version mit dem Handelsgüter-Skript.
                    if (DatenbankVersionAktuell >= 60 && i == 60)
                        HandelsgüterEinfügen(connectionString);
                    UpdateDatabase(i, connectionString);
                }

                // ChangeLog Anzeige beim Start zurück setzen
                GetScalarFromDatabase("UPDATE Einstellung SET Wert = 'True' WHERE Name = 'ShowChangeLog'", connectionString);

                // Datenbank optimieren
                DatabaseTools.CompactDatabase(connectionString);

                return DatabaseUpdateResult.DatenbankUpdateOK;
            }
            return DatabaseUpdateResult.DatenbankVersionOK; // kein Update erforderlich
        }

        static string DropPrimaryKeySql(string tableName, string connectionString)
        {
            string keyName = (string)GetScalarFromDatabase(String.Format("SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = '{0}'", tableName), connectionString);
            if (keyName == null)
                throw new InvalidDataException(String.Format("Für die Tabelle {0} konnte kein Primärschlüssel gefunden werden.", tableName));
            return String.Format("ALTER TABLE {0} DROP CONSTRAINT {1}", tableName, keyName);
        }

        public static void InterneGegnerDatenEinfügen(string connectionString = null)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                connectionString = MeisterGeister.Properties.Settings.Default.DatabaseDSAConnectionString;
            // Gegnerskript ausführen
            SqlCeConnection connection = new SqlCeConnection(connectionString);
            SqlCeTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                // lies die Insert-Befehle aus der Resourcen-Datei
                StreamReader reader = new StreamReader(App.GetResourceStream(new Uri("/DSA MeisterGeister;component/Daten/Updateskripte/InsertGegner.sql", UriKind.Relative)).Stream, Encoding.UTF8);
                string inserts = reader.ReadToEnd();
                ExecuteSqlCommands(inserts, "InsertGegner", connection, transaction, false);
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

        static void HandelsgüterEinfügen(string connectionString)
        {
            // Handelsgüter einfügen
            SqlCeConnection connection = new SqlCeConnection(connectionString);
            SqlCeTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                // lies die Insert-Befehle aus der Resourcen-Datei
                StreamReader reader = new StreamReader(App.GetResourceStream(new Uri("/DSA MeisterGeister;component/Daten/Updateskripte/InsertHandelsgut.sql", UriKind.Relative)).Stream, Encoding.UTF8);
                string inserts = reader.ReadToEnd();
                ExecuteSqlCommands(inserts, "InsertHandelsgut", connection, transaction, false);
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

            try
            {
                // prüft auf vorhandene Resourcen-Datei
                string pfad = "/DSA MeisterGeister;component/Daten/Updateskripte/" + string.Format("UpdateTo_V{0}", version.ToString("D4")) + ".sql";

                StreamReader reader = new StreamReader(App.GetResourceStream(new Uri(pfad, UriKind.Relative)).Stream, Encoding.UTF8);
                string skript = reader.ReadToEnd().Trim();
                if (!String.IsNullOrEmpty(skript))
                    sqlCommands.Add(pfad, skript);
            }
            catch (Exception) { /* Exception unterdrücken */ }

            //Sonderbehandlung wegen nichteindeutiger Key-Benennung
            if (version == 59)
            {
                sqlCommands.Add("",
                    "ALTER TABLE Held_Zauber DROP CONSTRAINT Zauber_FK" + Environment.NewLine + "GO" + Environment.NewLine
                    + DropPrimaryKeySql("Zauber", connectionString) + Environment.NewLine + "GO" + Environment.NewLine
                    );
            }

            //Sonderbehandlung: Skripte 82 und 83 wurden evtl. nicht ausgeführt
            if (version == 86)
            {
                var result = GetScalarFromDatabase("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Literatur' AND COLUMN_NAME = 'Größe'", connectionString);
                if (result == null)
                {
                    sqlCommands.Add(" Korrektur_2.3.4.1-1", "ALTER TABLE [Literatur] ADD [Größe] float NULL;");
                    sqlCommands.Add(" Korrektur_2.3.4.1-2", "ALTER TABLE [Literatur] ADD [GrößeKomprimiert] float NULL;");
                    sqlCommands.Add(" Korrektur_2.3.4.1-3", "ALTER TABLE [Literatur] ADD [UrlPdf] nvarchar(500) NULL;");
                    sqlCommands.Add(" Korrektur_2.3.4.1-4", "ALTER TABLE [Literatur] ADD [UrlPrint] nvarchar(500) NULL;");
                    sqlCommands.Add(" Korrektur_2.3.4.1-5", "ALTER TABLE Audio_Titel ADD Datei nvarchar(500) NULL;");
                    sqlCommands.Add(" Korrektur_2.3.4.1-6", "--#NOERROR;" + Environment.NewLine + "ALTER TABLE Audio_Playlist_Titel DROP CONSTRAINT DF__Audio_Playlist_Titel__0000000000000A3F;");
                    sqlCommands.Add(" Korrektur_2.3.4.1-7", "ALTER TABLE Audio_Playlist_Titel ADD Reihenfolge int NOT NULL DEFAULT 0;");
                }
            }

            // Update-Commands ausführen
            SqlCeConnection connection = new SqlCeConnection(connectionString);
            SqlCeTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                foreach (var command in sqlCommands)
                    ExecuteSqlCommands(command.Value, command.Key, connection, transaction, false);

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

        private static string StripSqlComments(string skript)
        {
            if (skript == null)
                return skript;
            //Entfernt alle Kommentare, außer sie beginnen mit --# solche kommentare sind laufzeitanweisungen für das Update.
            var r = new System.Text.RegularExpressions.Regex(@"(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/)|(--(?!#).*)");
            skript = r.Replace(skript, string.Empty);
            return skript;
        }

        public static void ExecuteSqlCommands(string commands, string skriptName, SqlCeConnection connection, SqlCeTransaction transaction, bool closeConnection = true)
        {
            try
            {
                //Kommentare entfernen
                commands = StripSqlComments(commands);

                string[] statements = null;
                statements = commands.Split(new string[] { "GO" + Environment.NewLine, ";" + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                //Trim auf alle
                for (int i = 0; i < statements.Length; i++)
                {
                    var statement = statements[i];
                    statement = statement.Trim();
                    if (statement.EndsWith(Environment.NewLine + "GO"))
                        statement = statement.Substring(0, statement.Length - (Environment.NewLine + "GO").Length);
                    if (statement.EndsWith("GO"))
                        statement = statement.Substring(0, statement.Length - ("GO").Length);
                    if (statement.EndsWith(";"))
                        statement = statement.Substring(0, statement.Length - (";").Length);
                    statements[i] = statement;
                }

                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                try
                {
                    RecursiveExecuteSqlCommands(statements, skriptName, connection, transaction);
                }
                catch (Exception)
                {
                    throw;
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

        private static void RecursiveExecuteSqlCommands(string[] statements, string skriptName, SqlCeConnection connection, SqlCeTransaction transaction)
        {
            try {
                SqlCeCommand command = new SqlCeCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                for(int i=0; i<statements.Length; i++)
                {
                    string statement = statements[i];
                    if (statement == string.Empty || statement == null)
                        continue;

                    if (statement.StartsWith("--#")) //Sonderbefehl
                    {
                        if (statement == "--#WHILE")
                        {
                            //find next non-empty statement
                            int j = i;
                            statement = null;
                            while (String.IsNullOrWhiteSpace(statement))
                            {
                                j++;
                                if (j >= statements.Length)
                                    throw new Exception("Das Updateskript hat eine unbeendete --#WHILE-Anweisung");
                                statement = statements[j];
                            }
                            if (!statements[j].StartsWith("SELECT"))
                                throw new Exception("Hinter --#WHILE fehlt eine skalare SELECT-Anweisung mit einem integer als Rückgabewert.");

                            SqlCeCommand conditionCmd = new SqlCeCommand();
                            conditionCmd.Connection = connection;
                            conditionCmd.Transaction = transaction;
                            conditionCmd.CommandText = statement;

                            statement = null;
                            while (String.IsNullOrWhiteSpace(statement))
                            {
                                j++;
                                if (j >= statements.Length)
                                    throw new Exception("Das Updateskript hat eine unbeendete --#WHILE-Anweisung");
                                statement = statements[j];
                            }
                            if (statement != "--#DO")
                                throw new Exception("Hinter --#WHILE fehlt das --#DO ... --#END mit dem auszuführenden Block.");
                            j++;
                            int blockStart = j; //erstes block statement
                            while (statement != "--#END")
                            {
                                j++;
                                if (j >= statements.Length)
                                    throw new Exception("Das Updateskript hat eine unbeendete --#WHILE-Anweisung");
                                statement = statements[j];
                            }
                            int blockEnd = j - 1;

                            string[] statementBlock = new string[blockEnd - blockStart + 1];
                            Array.Copy(statements, blockStart, statementBlock, 0, blockEnd - blockStart + 1);
                            while ((int)conditionCmd.ExecuteScalar() != 0)
                            {
                                try
                                {
                                    RecursiveExecuteSqlCommands(statementBlock, skriptName, connection, transaction);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                            i = j;
                        }
                        else if (statement == "--#NOERROR") //Error Unterdrückung
                        {
                            //find next non-empty statement
                            int j = i;
                            statement = null;
                            while (String.IsNullOrWhiteSpace(statement))
                            {
                                j++;
                                statement = statements[j];
                            }

                            UpdateSkript = skriptName + Environment.NewLine + statement;
                            command.CommandText = statement;
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {
                                // Exeption wird unterdrückt
                            }
                            i = j;
                        }
                        else
                            throw new Exception("Unbekannter SQL-Makrobefehl");
                    }
                    else //normaler befehl
                    {
                        UpdateSkript = skriptName + Environment.NewLine + statement;
                        command.CommandText = statement;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
