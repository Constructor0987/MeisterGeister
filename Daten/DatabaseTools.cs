using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.IO;
using MeisterGeister.View.Windows;

namespace MeisterGeister.Daten
{
    public static class DatabaseTools
    {
        #region //---- FELDER ----

        public static readonly string DATABASE_FOLDER = @"Daten\";
        public static readonly string BACKUP_FOLDER = @"Daten\Backup\";
        public static readonly string MAIN_DATABASE = "DatabaseDSA.sdf";
        public static readonly string MAIN_DATABASE_PATH = DATABASE_FOLDER + MAIN_DATABASE;

        #endregion

        #region //---- METHODEN ----

        /// <summary>
        /// Erstellt Backups der Datenbank im Backup-Verzeichnis.
        /// </summary>
        /// <param name="name"></param>
        public static void BackupDatabase(string name = "BACKUP")
        {
            // Backup-Verzeichnis prüfen
            if (!Directory.Exists(BACKUP_FOLDER))
                Directory.CreateDirectory(BACKUP_FOLDER);

            if (name == "BACKUP")
                name += "_" + DatabaseUpdate.DatenbankVersionAktuell;
            if (File.Exists(MAIN_DATABASE_PATH))
                File.Copy(MAIN_DATABASE_PATH, BACKUP_FOLDER + name + "_" + MAIN_DATABASE, (name != "BACKUP_UPDATE_")); // Update-Backups nicht überschreiben
        }

        public static void MoveBackupsToBackupFolder()
        {
            string[] backupFiles = Directory.GetFiles(DATABASE_FOLDER, "BACKUP_*");
            if (backupFiles.Length > 0)
            {
                // Backup-Verzeichnis prüfen
                if (!Directory.Exists(BACKUP_FOLDER))
                    Directory.CreateDirectory(BACKUP_FOLDER);

                // Backup Dateien verschieben
                foreach (string file in backupFiles)
                {
                    string fName = file.Substring(DATABASE_FOLDER.Length);
                    if (!File.Exists(BACKUP_FOLDER + fName))
                        File.Move(file, BACKUP_FOLDER + fName);
                }
            }
        }

        public static void ShrinkDatabase()
        {
            try
            {
                SqlCeEngine engine = new SqlCeEngine(MeisterGeister.Properties.Settings.Default.DatabaseDSAConnectionString);
                engine.Shrink();
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Datenbank optimieren", "Beim Optimieren der Datenbank ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
            }
        }

        public static void CompactDatabase()
        {
            CompactDatabase(MeisterGeister.Properties.Settings.Default.DatabaseDSAConnectionString);
        }

        public static void CompactDatabase(string connectionString)
        {
            try
            {
                SqlCeEngine engine = new SqlCeEngine(connectionString);
                engine.Compact(string.Empty);
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Datenbank optimieren", "Beim Optimieren der Datenbank ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
            }
        }

        #endregion
    }
}
