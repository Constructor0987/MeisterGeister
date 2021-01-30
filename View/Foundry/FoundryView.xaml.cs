using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
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
using CefSharp;
using MeisterGeister.View.General;
// Eigene Usings
using VM = MeisterGeister.ViewModel.Foundry;

namespace MeisterGeister.View.Foundry
{
    /// <summary>
    /// Interaktionslogik für FoundryView.xaml
    /// </summary>
    public partial class FoundryView : UserControl
    {

        //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        //[ComVisible(true)]
        //public class ObjectForScriptingHelper
        //{
        //    public void Invoke()
        //    {
        //        MessageBox.Show("Hallo");
        //    }
        //}

        public FoundryView()
        {
            InitializeComponent();

            VM = new VM.FoundryViewModel();
            Grid.SetRow(VM.cWebBrowser, 0);
            grdBrowser.Children.Add(VM.cWebBrowser);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.FoundryViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.FoundryViewModel))
                    return null;
                return DataContext as VM.FoundryViewModel;
            }
            set { DataContext = value; }
        }

       
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            KeyEvent k = new KeyEvent();
            k.WindowsKeyCode = 0x18;   //Space KEY
            VM.cWebBrowser.GetBrowser().GetHost().SendKeyEvent(k);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string localURL = ViewHelper.InputDialog("Server IP Adresse", "Bitte gebe den kompletten Internet-Link ein.\n\nUnter 'Game Settings'->" +
                "'Invitation Links' kann per Mausklick die Adresse in die ZWischenablage gespeichert werden", VM.LocalUri);
            VM.LocalUri = localURL;
            VM.cWebBrowser.Address = localURL;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string inetURL =ViewHelper.InputDialog("Server IP Adresse", "Bitte gebe den kompletten Internet-Link ein.\n\nUnter 'Game Settings'->" +
                "'Invitation Links' kann per Mausklick die Adresse in die ZWischenablage gespeichert werden", VM.InetUri);
            VM.InetUri = inetURL;
            VM.cWebBrowser.Address = inetURL;
        }
    }
}
