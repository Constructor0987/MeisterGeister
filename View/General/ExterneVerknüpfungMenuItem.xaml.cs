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
using MeisterGeister.View.Windows;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für ExternProgrammMenuItem.xaml
    /// </summary>
    public partial class ExternProgrammMenuItem : MenuItem
    {
        public ExternProgrammMenuItem(Model.MenuLink mLink)
        {
            InitializeComponent();
            MenuLink = mLink;
            Header = mLink.Name;
            ProgrammPfad = mLink.ProgrammPfad;
            SetBild(string.IsNullOrEmpty(mLink.Bild) ? string.Empty : mLink.Bild);
        }

        public new object Header
        {
            get { return _taxtBlockHeader.Text; }
            set { _taxtBlockHeader.Text = value.ToString(); }
        }

        public Model.MenuLink MenuLink { get; set; }

        private string _programmPfad;
        public string ProgrammPfad 
        {
            get { return _programmPfad; }
            set
            {
                _programmPfad = value;
                ToolTip = value;
            }
        }

        public bool IsWebUrl
        {
            get { return ProgrammPfad.StartsWith("http"); }
        }

        public void SetBild(string bildName)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            if (bildName == string.Empty)
                bildName = "Icons/meistertools_02.png";
            bi.UriSource = new Uri(@"/DSA%20MeisterGeister;component/Images/" + bildName, UriKind.Relative);
            bi.EndInit();
            _image.Source = bi;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProgrammPfad))
            {
                try
                {
                    string curDir = Environment.CurrentDirectory;
                    if (!IsWebUrl)
                        Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(ProgrammPfad);
                    System.Diagnostics.Process.Start(ProgrammPfad);
                    Environment.CurrentDirectory = curDir;
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("Fehler beim Starten eines externen Programms", "Beim Starten eines externen Programms ist ein Fehler aufgetreten!", ex);
                    errWin.ShowDialog();
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MenuLink != null)
            {
                Global.ContextMenuLink.Delete<Model.MenuLink>(MenuLink);
                if (Parent != null && Parent is MenuItem)
                    ((MenuItem)Parent).Items.Remove(this);
            }
        }
    }
}
