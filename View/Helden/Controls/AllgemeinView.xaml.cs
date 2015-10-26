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
using MeisterGeister.Logic.General;
using MeisterGeister.View.General;

namespace MeisterGeister.View.Helden.Controls
{
    public partial class AllgemeinView : System.Windows.Controls.UserControl
    {
        #region //KONSTRUKTOR
        public AllgemeinView()
        {
            this.InitializeComponent();
            VM = new VM.AllgemeinViewModel(ViewHelper.SelectImage);
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

        private void RefreshNotizen(object sender, EventArgs args)
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
                    VM.SelectedHeld.Bild = inBox.Wert;
            }
        }

        //LoadedEvent: Init VM hier um zur DesignTime die UI laden zu können
        private void AllgemeinLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.RefreshNotiz += RefreshNotizen;
                RefreshNotizen(null, null);
            }
        }
        #endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.RefreshNotiz -= RefreshNotizen;
        }
    }
}