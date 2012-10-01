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
using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden.Controls
{
    public partial class AllgemeinView : System.Windows.Controls.UserControl
    {
        #region //KONSTRUKTOR
        public AllgemeinView()
        {
            this.InitializeComponent();
            VM = new VM.AllgemeinViewModel();
            VM.RefreshNotiz += (s, ev) => { RefreshNotizentool(); };
        }
        #endregion

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.AllgemeinViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.AllgemeinViewModel))
                    return null;
                return DataContext as VM.AllgemeinViewModel;
            }
            set { DataContext = value; }
        }

        #region //EVENTS
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }
        private void ImageWikiHeldenbrief_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (VM.SelectedHeld != null)
                System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/Spielerhelden/"
                    + VM.SelectedHeld.Name.Replace(" ", "_"));

        }

        private void ButtonBildLinkSetzenDatei_Click(object sender, RoutedEventArgs e)
        {
            if (VM.SelectedHeld != null && VM.SelectedHeld != null)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Bild (*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF)|*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF";

                if (dlg.ShowDialog() == DialogResult.OK)
                    VM.SelectedHeld.BildLink = dlg.FileName;

            }
        }
        private void ButtonBildLinkDelete_Click(object sender, RoutedEventArgs e)
        {
            if (VM.SelectedHeld != null)
                VM.SelectedHeld.BildLink = null;
        }
        private void TextBlockBild_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (VM.SelectedHeld != null
                && !string.IsNullOrWhiteSpace(VM.SelectedHeld.BildLink))
            {
                try
                {
                    System.Diagnostics.Process.Start(VM.SelectedHeld.BildLink);
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show(string.Format("Das Bild '{0}' konnte nicht geöffnet werden.", (this.DataContext as VM.AllgemeinViewModel).SelectedHeld.BildLink), "Bild");
                }

            }
        }
        private void RefreshNotizentool(bool refreshRepräsentationen = true)
        {
            if (VM.SelectedHeld == null || String.IsNullOrEmpty(VM.SelectedHeld.Notizen))
                RTBNotiz.ParseTextToFlowDoument(string.Empty);
            else
                RTBNotiz.ParseTextToFlowDoument(VM.SelectedHeld.Notizen);
        }
        private void RTBNotiz_LostFocus(object sender, RoutedEventArgs e)
        {
            if (VM.SelectedHeld != null)
                VM.SelectedHeld.Notizen = RTBNotiz.ParseFlowDoumentToText();
        }
        private void ButtonBildLinkSetzenWeb_Click(object sender, RoutedEventArgs e)
        {
            if (VM.SelectedHeld != null)
            {
                InputWindow inBox = new InputWindow();
                inBox.Title = "Web-Link zum Charakter-Bild";
                inBox.Beschreibung = "Bitte den vollständigen Link zum Bild des Charakters angeben.";
                inBox.ShowDialog();
                if (inBox.OK_Click)
                    VM.SelectedHeld.BildLink = inBox.Wert;
            }
        }
        #endregion
    }
}