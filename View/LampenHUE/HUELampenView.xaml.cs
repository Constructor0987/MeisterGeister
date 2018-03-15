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
//Eigene Usings
using VM = MeisterGeister.ViewModel.LampenHUE;
//Weitere Usings
using System.Diagnostics;
using Q42.HueApi.Interfaces;
using Q42.HueApi;


namespace MeisterGeister.View.LampenHUE
{
    /// <summary>
    /// Interaktionslogik für HUELampenView.xaml
    /// </summary>
    public partial class HUELampenView : UserControl
    {
        public HUELampenView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.HUELampenViewModel).LoadDaten();
            }
            catch (Exception ex)
            {
                View.Windows.MsgWindow errWin = new View.Windows.MsgWindow("HUELampen-Tool", "Beim Laden des HUELampen-Tools ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IBridgeLocator locator = new HttpBridgeLocator();

            locator.LocateBridgesAsync(TimeSpan.FromSeconds(4));

            ////For Windows 8 and .NET45 projects you can use the SSDPBridgeLocator which actually scans your network. 
            ////See the included BridgeDiscoveryTests and the specific .NET and .WinRT projects
            //IEnumerable<string> bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

        }
    }
}
