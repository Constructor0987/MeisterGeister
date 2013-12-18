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
using System.Windows.Shapes;
using System.Diagnostics;
// Eigene Usings
using GeneralLogic = MeisterGeister.Logic.General;
using MeisterGeister.Logic.Settings;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.View.Windows;
using MeisterGeister.ViewModel.Settings;

namespace MeisterGeister.View.Settings
{
    /// <summary>
    /// Interaktionslogik für EinstellungenWindow.xaml
    /// </summary>
    public partial class EinstellungenWindow : Window
    {
        public EinstellungenWindow()
        {
            InitializeComponent();

            // DataContext setzen
            //DataContext = Global.ContextHeld.Liste<Model.Einstellung>(); //TODO CRAP! FIXME - ein echtes VM muss her.
            VM = new EinstellungenViewModel();

            //_listBoxSettings.ItemsSource = Global.ContextHeld.Liste<Model.Setting>();
        }

        public EinstellungenViewModel VM
        {
            get { return DataContext as EinstellungenViewModel; }
            set { DataContext = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //_checkBoxFrageNeueKampfrundeAbstellen.IsChecked = Einstellungen.FrageNeueKampfrundeAbstellen;
            //_checkBoxJingleAbstellen.IsChecked = Einstellungen.JingleAbstellen;

            //_checkboxGleichSpielen.IsChecked = Einstellungen.AudioDirektAbspielen;
            //_sldFading.Value = Einstellungen.Fading;
            //tbStdPfad.Text = Einstellungen.GetEinstellung("AudioVerzeichnis", @"C:\");
        }

        private void _checkBoxFrageNeueKampfrundeAbstellen_Checked(object sender, RoutedEventArgs e)
        {
            Einstellungen.FrageNeueKampfrundeAbstellen = (bool)_checkBoxFrageNeueKampfrundeAbstellen.IsChecked;
        }

        private void _checkBoxFrageNeueKampfrundeAbstellen_Unchecked(object sender, RoutedEventArgs e)
        {
            Einstellungen.FrageNeueKampfrundeAbstellen = (bool)_checkBoxFrageNeueKampfrundeAbstellen.IsChecked;
        }

        private void CheckBoxJingleAbstellen_Checked(object sender, RoutedEventArgs e)
        {
            Einstellungen.JingleAbstellen = (bool)_checkBoxJingleAbstellen.IsChecked;
        }

        private void CheckBoxJingleAbstellen_Unchecked(object sender, RoutedEventArgs e)
        {
            Einstellungen.JingleAbstellen = (bool)_checkBoxJingleAbstellen.IsChecked;
        }

        private void ButtonPlayJingle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Jingle abspielen
                GeneralLogic.AudioPlayer.PlayJingle();
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Audio Fehler", ex.Message);
                errWin.ShowDialog();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Global.ContextHeld.Save();
        }

        private void _checkboxGleichSpielen_Checked(object sender, RoutedEventArgs e)
        {
            Einstellungen.AudioDirektAbspielen = (bool)_checkboxGleichSpielen.IsChecked;
        }

        private void _checkboxGleichSpielen_Unchecked(object sender, RoutedEventArgs e)
        {
            Einstellungen.AudioDirektAbspielen = (bool)_checkboxGleichSpielen.IsChecked;
        }

        public void btnStdPfad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.SelectedPath = Einstellungen.AudioVerzeichnis; // btnStdPfad.Tag.ToString(); 
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    MeisterGeister.Logic.Settings.Einstellungen.AudioVerzeichnis = dialog.SelectedPath;
                    btnStdPfad.Tag = dialog.SelectedPath;                    
                    tbStdPfad.Text = btnStdPfad.Tag.ToString();
                }
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Eingabefehler", "Das Auswählen des Standard-Verzeichnisses hat eine Exeption ausgelöst.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        public void _sldFading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                _sldFading.ToolTip = Math.Round(e.NewValue / 100, 1) + " Sekunden In-/Out-Fading";
                Einstellungen.Fading = (int)e.NewValue;
            }
        }
    }
}
