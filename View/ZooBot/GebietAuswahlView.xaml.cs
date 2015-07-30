using MeisterGeister.View.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// Eigene Usings
using VM = MeisterGeister.ViewModel.ZooBot;

namespace MeisterGeister.View.ZooBot
{
    /// <summary>
    /// Interaktionslogik für BekanntePflanzenView.xaml
    /// </summary>
    public partial class GebietAuswahlView : Window
    {
        public VM.GebietAuswahlVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.GebietAuswahlVM))
                    return null;
                return DataContext as VM.GebietAuswahlVM;
            }
            set
            {
                DataContext = value;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.Refresh();
            VM = new VM.GebietAuswahlVM();
        }

        public GebietAuswahlView()
        {
            InitializeComponent();
            VM = new VM.GebietAuswahlVM();
            
            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Location.X + 20);
            Top = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Location.Y + 20);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
