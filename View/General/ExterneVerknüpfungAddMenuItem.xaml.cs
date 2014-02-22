using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// Eigene Usings
using MeisterGeister.View;
using MeisterGeister.View.Windows;


namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für ExterneVerknüpfungAddMenuItem.xaml
    /// </summary>
    public partial class ExterneVerknüpfungAddMenuItem : MenuItem
    {
        public ExterneVerknüpfungAddMenuItem()
        {
            InitializeComponent();
        }

        public string MenuPunkt
        {
            get
            {
                if (Parent != null && Parent is MenuItem)
                {
                    return ((MenuItem)Parent).Header.ToString();
                }
                return string.Empty;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool insert = false;
                string pfad = string.Empty;
                string name = string.Empty;
                string bild = null;

                if (sender is MenuItem)
                {
                    if (((MenuItem)sender).Header.ToString() == "Programm oder Datei")
                    {
                        pfad = ViewHelper.ChooseFile("Programm oder Datei auswählen", "", false, true);
                        if (!String.IsNullOrEmpty(pfad))
                        {
                            InputWindow inBox = new InputWindow();
                            inBox.Title = "Name eingeben";
                            inBox.Beschreibung = "Bitte einen Namen für den Menü-Eintrag eingeben.";
                            System.IO.FileInfo file = new System.IO.FileInfo(pfad);
                            string filename = file.Name.Replace(file.Extension, string.Empty);
                            inBox.Wert = filename;
                            inBox.ShowDialog();
                            insert = inBox.OK_Click;
                            if (insert)
                            {
                                name = inBox.Wert;
                                if (file.Extension == ".pdf")
                                    bild = "Icons/Extensions/pdf.png";
                                else if (file.Extension == ".txt" || file.Extension == ".rtf")
                                    bild = "Icons/Extensions/txt.png";
                                else if (file.Extension == ".doc" || file.Extension == ".docx" || file.Extension == ".odt")
                                    bild = "Icons/Extensions/doc.png";
                                else if (file.Extension == ".xls" || file.Extension == ".xlsx" || file.Extension == ".ods")
                                    bild = "Icons/Extensions/xls.png";
                                else if (file.Extension == ".mp3" || file.Extension == ".wav" || file.Extension == ".wma")
                                    bild = "Icons/General/audio.png";
                                else if (file.Extension == ".exe")
                                {
                                    filename = filename.ToLower();
                                    if (filename == "wmplayer" || filename == "winamp" || filename == "vlc" || filename == "audacity"
                                         || filename == "rpgsoundmixer" || filename == "spockplayer" || filename == "itunes")
                                        bild = "Icons/General/audio.png";
                                }
                            }
                        }
                    }
                    else if (((MenuItem)sender).Header.ToString() == "Ordner")
                    {
                        System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            pfad = dlg.SelectedPath;
                            InputWindow inBox = new InputWindow();
                            inBox.Title = "Name eingeben";
                            inBox.Beschreibung = "Bitte einen Namen für den Menü-Eintrag eingeben.";
                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(pfad);
                            inBox.Wert = dir.Name;
                            inBox.ShowDialog();
                            insert = inBox.OK_Click;
                            name = inBox.Wert;
                            bild = "Icons/General/oeffnen.png";
                        }
                    }
                    else if (((MenuItem)sender).Header.ToString() == "Webseite")
                    {
                        InputWindow inBox = new InputWindow();
                        inBox.Title = "Webadresse eingeben";
                        inBox.Beschreibung = "Bitte eine Webadresse eingeben.";
                        inBox.ShowDialog();
                        if (inBox.OK_Click)
                        {
                            pfad = inBox.Wert;
                            inBox = new InputWindow();
                            inBox.Title = "Name eingeben";
                            inBox.Beschreibung = "Bitte einen Namen für den Menü-Eintrag eingeben.";
                            inBox.Wert = pfad.Replace("http://", string.Empty);
                            inBox.ShowDialog();
                            insert = inBox.OK_Click;
                            name = inBox.Wert;
                            bild = "Icons/General/web.png";
                        }
                    }

                    if (insert)
                    {
                        Model.MenuLink mLink = new Model.MenuLink()
                        {
                            MenuPunkt = MenuPunkt,
                            ProgrammPfad = pfad,
                            Name = name,
                            Bild = bild
                        };
                        Global.ContextMenuLink.Insert<Model.MenuLink>(mLink);
                        MainView.AddMenuExternesProgramm(mLink);
                    }
                }
            }
            catch (System.Data.ConstraintException)
            {
                MsgWindow errWin = new MsgWindow("Fehler beim Zuweisen einer externen Verknüpfung", "Ein Eintrag kann nicht mehrfach eingefügt werden.");
                errWin.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Fehler beim Zuweisen einer externen Verknüpfung", "Beim Zuweisen einer externen Verknüpfung ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
            }
        }
    }
}
