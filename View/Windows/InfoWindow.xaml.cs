using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using MeisterGeister.Daten;

namespace MeisterGeister.View.Windows
{
    /// <summary>
    /// Interaktionslogik für InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();

            // Get the version of the current application.
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            _textBoxProgramm.Text = assemName.Name;
            _textBoxVersion.Text = App.GetVersionString(App.GetVersionProgramm());
            _textBoxDatenbankVersion.Text = string.Format("Datenbank Version {0}", DatabaseUpdate.DatenbankVersionAktuell);
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void ImageMeisterGeister_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.meistergeister.org/");
        }

        private void ImageUlisses_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ulisses-spiele.de/");
        }

        private void ImageDG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dereglobus.org/");
        }

        private void ImageFanprojekt_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                // toggle INTERN Modus
                Global.INTERN = !Global.INTERN;
                if (Global.INTERN)
                {
                    if (View.General.ViewHelper.Confirm("GegnerDaten laden", "Der INTERN Modus wurde aktiviert.\n Sollen die Gegnerdaten geladen werden?\nIm Anschluss das Programm bitte neu starten."))
                        Daten.DatabaseUpdate.InterneGegnerDatenEinfügen();
                }
                else
                    MessageBox.Show("INTERN Modus wurde deaktiviert. Bitte neu starten.");
            }
        }
    }
}
