using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using MeisterGeister.Model;
using MeisterGeister.View.Windows;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Helden.Controls;

namespace MeisterGeister.View.Helden.Controls {
    public partial class ViewHeldAllgemein : System.Windows.Controls.UserControl {
        //ALLGEMEIN: Daten immer aus dem ViewModel greifen
        //=> (this.DataContext as VM.ViewModelHeldAllgemein).ÖffentlicheEigenschaft

        #region //KONSTRUKTOR                
        public ViewHeldAllgemein() {
            this.InitializeComponent();
            //VM an View Registrieren
            this.DataContext = new VM.ViewModelHeldAllgemein();
            //EventsRegistrieren
            (this.DataContext as VM.ViewModelHeldAllgemein).RefreshNotiz += (s, ev) => { RefreshNotizentool(); };            
        }
        #endregion

        #region //EVENTS
        //LoadAktionen wie Services oder View-Init's
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {            
        }        
        private void ImageWikiHeldenbrief_MouseDown(object sender, MouseButtonEventArgs e) {
            if ((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld != null)
                System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/Spielerhelden/" 
                    + (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.Name.Replace(" ", "_"));
        }
        private void ButtonBildLinkSetzenDatei_Click(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld != null && (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld != null) {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Bild (*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF)|*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF";

                if (dlg.ShowDialog() == DialogResult.OK)
                    (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.BildLink = dlg.FileName;
                //System.Diagnostics.Debug.Assert(Global.SelectedHeld.BildLink == (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.BildLink);
            }
        }
        private void ButtonBildLinkDelete_Click(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld != null)
                (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.BildLink = null;
        }
        private void TextBlockBild_MouseDown(object sender, MouseButtonEventArgs e) {
            if ((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld != null
                && !string.IsNullOrWhiteSpace((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.BildLink)) {
                try {
                    System.Diagnostics.Process.Start((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.BildLink);
                } catch (Exception) {
                    System.Windows.MessageBox.Show(string.Format("Das Bild '{0}' konnte nicht geöffnet werden.", (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.BildLink), "Bild");
                }

            }
        }
        private void RefreshNotizentool(bool refreshRepräsentationen = true) {
            if ((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld == null || String.IsNullOrEmpty((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.Notizen))
                RTBNotiz.ParseTextToFlowDoument(string.Empty);
            else
                RTBNotiz.ParseTextToFlowDoument((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.Notizen);
        }
        private void RTBNotiz_LostFocus(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld != null)
                (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.Notizen = RTBNotiz.ParseFlowDoumentToText();
        }
        private void ButtonBildLinkSetzenWeb_Click(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld != null) {
                InputWindow inBox = new InputWindow();
                inBox.Title = "Web-Link zum Charakter-Bild";
                inBox.Beschreibung = "Bitte den vollständigen Link zum Bild des Charakters angeben.";
                inBox.ShowDialog();
                if (inBox.OK_Click)
                    (this.DataContext as VM.ViewModelHeldAllgemein).SelectedHeld.BildLink = inBox.Wert;
            }
        }
        #endregion
    }
}