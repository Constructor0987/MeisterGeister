using MeisterGeister.View.General;
using MeisterGeister.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Menu
{
    public class AddExternMenuItemViewModel : MenuItemViewModel
    {
        public AddExternMenuItemViewModel() : base()
        {
            Header = "Externe Verknüpfung...";
            ToolTip = "Verknüpfung zu einem externen Programm (etc.) ins Menü einfügen.";
            Children = new Logic.Extensions.ExtendedObservableCollection<MenuItemViewModel>();
            var mi = new MenuItemViewModel();
            mi.Header = "Programm oder Datei";
            mi.ToolTip = "Verknüpfung zu einem externen Programm (etc.) ins Menü einfügen.";
            mi.Icon = "/DSA MeisterGeister;component/Images/Icons/meistertools_02.png";
            mi.Command = new Base.CommandBase((o) => { ShowInputDialog("Programm oder Datei"); }, null);
            Children.Add(mi);
            mi = new MenuItemViewModel();
            mi.Header = "Ordner";
            mi.ToolTip = "Verknüpfung zu einem Ordner ins Menü einfügen.";
            mi.Icon = "/DSA MeisterGeister;component/Images/Icons/General/oeffnen.png";
            mi.Command = new Base.CommandBase((o) => { ShowInputDialog("Ordner"); }, null);
            Children.Add(mi);
            mi = new MenuItemViewModel();
            mi.Header = "Webseite";
            mi.ToolTip = "Verknüpfung zu einer Webseite ins Menü einfügen.";
            mi.Icon = "/DSA MeisterGeister;component/Images/Icons/General/web.png";
            mi.Command = new Base.CommandBase((o) => { ShowInputDialog("Webseite"); }, null);
            Children.Add(mi);
        }

        MainViewModel mainViewModel = null;
        public MainViewModel MainViewModel
        {
            get { return mainViewModel; }
            set { Set(ref mainViewModel, value); }
        }

        string gruppe = null;

        public string Gruppe
        {
            get { return gruppe; }
            set { Set(ref gruppe, value); }
        }

        private void ShowInputDialog(string menu)
        {
            try
            {
                bool insert = false;
                string pfad = string.Empty;
                string name = string.Empty;
                string bild = null;

                if (Gruppe != null && MainViewModel != null)
                {
                    if (menu == "Programm oder Datei")
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
                    else if (menu == "Ordner")
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
                    else if (menu == "Webseite")
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
                            MenuPunkt = Gruppe,
                            ProgrammPfad = pfad,
                            Name = name,
                            Bild = bild
                        };
                        Global.ContextMenuLink.Insert<Model.MenuLink>(mLink);
                        if (MainViewModel != null)
                            MainViewModel.AddExternesProgramm(mLink);
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
