using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// eigene
using MeisterGeister.View.General;

namespace MeisterGeister.View.Karte
{
    /// <summary>
    /// Interaktionslogik für PflanzenSucheAllgemeinView.xaml
    /// </summary>
    public partial class PflanzenSucheAllgemeinView : UserControl
    {
        public PflanzenSucheAllgemeinView()
        {
            InitializeComponent();
        }

        private void BtnBekanntePflanzenForm_Click(object sender, RoutedEventArgs e)
        {
            if (Global.SelectedHeld == null)
                ViewHelper.Popup("Bitte zuerst einen Helden auswählen");
            else
            {
                BekanntePflanzenView wndBekanntePflanzen = new BekanntePflanzenView();
                wndBekanntePflanzen.ShowDialog();
            }
        }
    }
}
