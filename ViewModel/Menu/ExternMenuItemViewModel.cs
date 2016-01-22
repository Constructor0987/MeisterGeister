using MeisterGeister.View.General;
using MeisterGeister.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeisterGeister.ViewModel.Menu
{
    public class ExternMenuItemViewModel : MenuItemViewModel
    {
        public ExternMenuItemViewModel()
            : base()
        {
            SetFromViewHelper();
        }

        MainViewModel mainViewModel = null;
        public MainViewModel MainViewModel
        {
            get { return mainViewModel; }
            set { Set(ref mainViewModel, value); }
        }

        Model.MenuLink menuLink = null;
        public Model.MenuLink MenuLink
        {
            get { return menuLink; }
            set
            {
                if (Set(ref menuLink, value))
                    Command = new Base.CommandBase(o =>
                        {
                            StartExtern(menuLink.ProgrammPfad);
                        }, null);
            }
        }

        public override string Icon
        {
            get
            {
                if (MenuLink == null || String.IsNullOrWhiteSpace(MenuLink.Bild))
                    return null;
                return "/DSA%20MeisterGeister;component/Images/" + MenuLink.Bild;
            }
            set { return; }
        }

        public string Name
        {
            get {
                if (MenuLink == null)
                    return null;
                return MenuLink.Name; 
            }
        }

        static string headerXaml =
@"<DockPanel LastChildFill=""False"">
            <TextBlock Text=""{Binding Name}"" DockPanel.Dock=""Left"" VerticalAlignment=""Center"" />
            <Button DockPanel.Dock=""Right"" Margin=""15,0,0,0"" Height=""20"" ToolTip=""Link entfernen"" Command=""{Binding OnDelete}"" Opacity=""0.4"" Padding=""0"">
                <Image Source=""/Images/Icons/General/entf_01.png"" Stretch=""Fill"" Margin=""2"" />
            </Button>
        </DockPanel>";

        object header = null;
        public new object Header
        {
            get
            {
                if (MenuLink == null)
                    return null;
                if(header == null)
                {
                    var dp = (DockPanel)MeisterGeister.View.General.ViewHelper.ParseXAML(headerXaml);
                    header = dp;
                }
                return header;
            }
            set { return; }
        }

        Base.CommandBase ondelete = null;
        public Base.CommandBase OnDelete
        {
            get
            {
                if (ondelete == null)
                    ondelete = new Base.CommandBase(o => { Delete(); }, null);
                return ondelete;
            }
            set { ondelete = value; }
        }


        private void Delete()
        {
            if (MenuLink == null)
                return;
            if(!Confirm("Menüeintrag löschen", String.Format("Wollen sie den Eintrag {0} wirklich löschen?", MenuLink.Name)))
                return;
            string gruppe = MenuLink.MenuPunkt;
            Global.ContextHeld.Delete<Model.MenuLink>(MenuLink);
            if(gruppe != null && MainViewModel != null && MainViewModel.Gruppen.ContainsKey(gruppe))
                MainViewModel.Gruppen[gruppe].Children.Remove(this);
        }

        private void StartExtern(string ProgrammPfad)
        {
            if (!string.IsNullOrWhiteSpace(ProgrammPfad))
            {
                try
                {
                    string curDir = Environment.CurrentDirectory;
                    string fileName = ProgrammPfad;
                    if (!ProgrammPfad.StartsWith("http"))
                    {
                        if (fileName.StartsWith("\\")) // bei relativem Pfad das HomeDir hinzufügen
                        {
                            fileName = fileName.Remove(0, 1).Insert(0, Logic.Extensions.FileExtensions.GetHomeDirectory());
                            Logic.Extensions.FileExtensions.SetCurrentDir(fileName);
                        }
                        else
                            Logic.Extensions.FileExtensions.SetCurrentDir();
                    }

                    System.Diagnostics.Process.Start(fileName);
                    Environment.CurrentDirectory = curDir;
                }
                catch (Exception ex)
                {
                    ShowError("Beim Starten eines externen Programms ist ein Fehler aufgetreten!", ex);
                }
            }
        }
    }
}
