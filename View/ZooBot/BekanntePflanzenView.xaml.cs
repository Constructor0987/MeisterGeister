using MeisterGeister.Model;
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
    public partial class BekanntePflanzenView : Window
    {
        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.BekanntePflanzenVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.BekanntePflanzenVM))
                    return null;
                return DataContext as VM.BekanntePflanzenVM;
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
            VM = new VM.BekanntePflanzenVM();
        }
                
        public BekanntePflanzenView()
        {
            InitializeComponent();
            VM = new VM.BekanntePflanzenVM();

            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Location.X + 20);
            Top = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Location.Y + 20);
        }

        private void btnBekanntePflanzeEntfernen_Click(object sender, RoutedEventArgs e)
        {
            Held_Pflanze hPflanze = ((System.Windows.Controls.Button)sender).Tag as Held_Pflanze;
            if (VM.ZooBotVM.SelectedHeld.Held_Pflanze.Contains(hPflanze))
            {
                Global.ContextZooBot.Delete<Held_Pflanze>(hPflanze);
                VM.Refresh();
            }
        }
    }
}
