using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MeisterGeister.Daten;
using System.Linq;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
using System.Collections.Generic;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Helden;
using MeisterGeister.Logic.General;
using MeisterGeister.View.Windows;
using MeisterGeister.View.Kampf;
using System.IO;

namespace MeisterGeister.View.Helden
{
    /// <summary>
    /// Interaktionslogik für HeldenView.xaml
    /// </summary>
    public partial class HeldenView : System.Windows.Controls.UserControl
    {
        public HeldenView()
        {
            InitializeComponent();

            //VM an View Registrieren
            this.DataContext = new VM.HeldenViewModel(); 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ListBoxHelden_SelectionChanged(sender, null);
        }

        //public event ProbeWürfelnEventHandler ProbeWürfeln;
        
    }
}
