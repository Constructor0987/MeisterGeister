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
        #region //KONSTRUKTOR
        public ViewHeldAllgemein() {
            this.InitializeComponent();            
            this.DataContext = new VM.AllgemeinViewModel();            
            (this.DataContext as VM.AllgemeinViewModel).RefreshNotiz += (s, ev) => { RefreshNotizentool(); };            
        }
        #endregion

        #region //EVENTS        
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {            
        }        
        private void ImageWikiHeldenbrief_MouseDown(object sender, MouseButtonEventArgs e) {
            if ((this.DataContext as VM.AllgemeinViewModel).SelectedHeld != null)
                System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/Spielerhelden/" 
                    + (this.DataContext as VM.AllgemeinViewModel).SelectedHeld.Name.Replace(" ", "_"));
        }
        private void ButtonBildLinkSetzenDatei_Click(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.AllgemeinViewModel).SelectedHeld != null && (this.DataContext as VM.AllgemeinViewModel).SelectedHeld != null) {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Bild (*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF)|*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF";

                if (dlg.ShowDialog() == DialogResult.OK)
                    (this.DataContext as VM.AllgemeinViewModel).SelectedHeld.BildLink = dlg.FileName;
                
            }
        }
        private void ButtonBildLinkDelete_Click(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.AllgemeinViewModel).SelectedHeld != null)
                (this.DataContext as VM.AllgemeinViewModel).SelectedHeld.BildLink = null;
        }
        private void TextBlockBild_MouseDown(object sender, MouseButtonEventArgs e) {
            if ((this.DataContext as VM.AllgemeinViewModel).SelectedHeld != null
                && !string.IsNullOrWhiteSpace((this.DataContext as VM.AllgemeinViewModel).SelectedHeld.BildLink)) {
                try {
                    System.Diagnostics.Process.Start((this.DataContext as VM.AllgemeinViewModel).SelectedHeld.BildLink);
                } catch (Exception) {
                    System.Windows.MessageBox.Show(string.Format("Das Bild '{0}' konnte nicht geöffnet werden.", (this.DataContext as VM.AllgemeinViewModel).SelectedHeld.BildLink), "Bild");
                }

            }
        }
        private void RefreshNotizentool(bool refreshRepräsentationen = true) {
            if ((this.DataContext as VM.AllgemeinViewModel).SelectedHeld == null || String.IsNullOrEmpty((this.DataContext as VM.AllgemeinViewModel).SelectedHeld.Notizen))
                RTBNotiz.ParseTextToFlowDoument(string.Empty);
            else
                RTBNotiz.ParseTextToFlowDoument((this.DataContext as VM.AllgemeinViewModel).SelectedHeld.Notizen);
        }
        private void RTBNotiz_LostFocus(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.AllgemeinViewModel).SelectedHeld != null)
                (this.DataContext as VM.AllgemeinViewModel).SelectedHeld.Notizen = RTBNotiz.ParseFlowDoumentToText();
        }
        private void ButtonBildLinkSetzenWeb_Click(object sender, RoutedEventArgs e) {
            if ((this.DataContext as VM.AllgemeinViewModel).SelectedHeld != null) {
                InputWindow inBox = new InputWindow();
                inBox.Title = "Web-Link zum Charakter-Bild";
                inBox.Beschreibung = "Bitte den vollständigen Link zum Bild des Charakters angeben.";
                inBox.ShowDialog();
                if (inBox.OK_Click)
                    (this.DataContext as VM.AllgemeinViewModel).SelectedHeld.BildLink = inBox.Wert;
            }
        }
        #endregion
    }
}