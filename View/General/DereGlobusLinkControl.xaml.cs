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

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für DereGlobusLinkControl.xaml
    /// </summary>
    public partial class DereGlobusLinkControl : UserControl
    {
        public DereGlobusLinkControl()
        {
            InitializeComponent();
        }

        private void ImageDereGlobus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Tag != null)
            {
                if (Tag is DgSuche.Ortsmarke)
                    ((DgSuche.Ortsmarke)Tag).BetrachteInDereGlobus();
                else if (Tag is string)
                    new DgSuche.Ortsmarke(Tag.ToString()).BetrachteInDereGlobus();
            }
            else
                MessageBox.Show("Kein Eintrag ausgewählt.", "Globus");
        }
    }
}
