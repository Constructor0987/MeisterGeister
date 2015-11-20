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
using System.Windows.Navigation;
using System.Windows.Shapes;
// Eigene Usings
using VM = MeisterGeister.ViewModel.ZooBot;
using MeisterGeister.Logic.General;
using MeisterGeister.Daten;
using MeisterGeister.View.Windows;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.View.ZooBot
{
    /// <summary>
    /// Interaktionslogik für ZooBotView.xaml
    /// </summary>
    public partial class ZooBotView : UserControl
    {
        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.ZooBotViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ZooBotViewModel))
                    return null;
                return DataContext as VM.ZooBotViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.Refresh();
            Reload();
        }


        public ZooBotView()
        {
            InitializeComponent();

            VM = new VM.ZooBotViewModel();            
        }

        public void Reload()
        {
            _comboBoxHeld.ItemsSource = Global.ContextHeld.HeldenGruppeListe;

        }

        private void BtnBekanntePflanzenForm_Click(object sender, RoutedEventArgs e)
        {
            BekanntePflanzenView wndBekanntePflanzen = new BekanntePflanzenView();
            wndBekanntePflanzen.VM.ZooBotVM = VM;
            //wndBekanntePflanzen.VM.SelectedHeld = VM.SelectedHeld;
            wndBekanntePflanzen.ShowDialog();
        }

        private void BtnGebieteAuswahlView_Click(object sender, RoutedEventArgs e)
        {
            BekanntePflanzenView wndBekanntePflanzen = new BekanntePflanzenView();
            wndBekanntePflanzen.VM.ZooBotVM = VM;
            //wndBekanntePflanzen.VM.SelectedHeld = VM.SelectedHeld;
            wndBekanntePflanzen.ShowDialog();
        }

        private void KarteAusrufen_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && (sender is Button || sender is MenuItem))
            {
                object tag = null;
                if (sender is Button)
                    tag = ((Button)sender).Tag;
                else if (sender is MenuItem)
                    tag = ((MenuItem)sender).Tag;
                if (tag != null && tag is string)
                    (App.Current.MainWindow as View.MainView).StarteTab(tag.ToString());
            }

        }

        private void Expander_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ComboBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (VM.HeldenPos != new Point(Global.HeldenLon, Global.HeldenLat))
                VM.GebieteVonPos(null);
        }
    }
}
