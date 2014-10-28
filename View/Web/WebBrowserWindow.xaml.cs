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
using System.Windows.Shapes;

namespace MeisterGeister.View.Web
{
    /// <summary>
    /// Interaktionslogik für WebBrowserWindow.xaml
    /// </summary>
    public partial class WebBrowserWindow : Window
    {
        public WebBrowserWindow()
        {
            InitializeComponent();
        }

        public WebBrowserWindow(string title) : this()
        {
            Title += ": " + title;
        }

        private void _webBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            _textUrl.Text = e.Uri.AbsoluteUri;
        }

        private void ButtonOpenBrowser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_webBrowser.Source.AbsoluteUri);
            }
            catch (Exception ex)
            {
                View.General.ViewHelper.ShowError("Fehler beim Starten des Browsers", ex);
            }
        }

        public void Navigate(string url)
        {
            _textUrl.Text = url;
            _webBrowser.Navigate(url);
        }
    }
}
