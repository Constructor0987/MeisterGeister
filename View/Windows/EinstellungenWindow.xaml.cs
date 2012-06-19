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

namespace MeisterGeister.View.Windows
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
            DataContext = Global.ContextRegeln.RegelnListe;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _checkBoxFrageNeueKampfrundeAbstellen.IsChecked = Einstellungen.FrageNeueKampfrundeAbstellen;
            _checkBoxJingleAbstellen.IsChecked = Einstellungen.JingleAbstellen;
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
            Global.ContextRegeln.Save();
        }
    }
}
