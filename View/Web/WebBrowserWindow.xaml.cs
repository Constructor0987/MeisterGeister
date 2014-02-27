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

        public WebBrowserWindow(string url, string title) : this()
        {
            Title += ": " + title;
            _textUrl.Text = url;
            _webBrowser.Navigate(url);
        }

        private void _webBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            _textUrl.Text = e.Uri.AbsoluteUri;
        }
    }
}
