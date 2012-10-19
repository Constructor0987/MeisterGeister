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
using MeisterGeister.Logic.General;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für WikiLinkControl.xaml
    /// </summary>
    public partial class WikiLinkControl : UserControl
    {
        public WikiLinkControl()
        {
            InitializeComponent();
        }

        private void ImageWiki_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Tag != null && Tag.ToString().Trim() != string.Empty)
                WikiAventurica.OpenBrowser(Tag.ToString());
            else
                MessageBox.Show("Kein Eintrag ausgewählt.", "Wiki-Aventurica");
        }
    }
}
