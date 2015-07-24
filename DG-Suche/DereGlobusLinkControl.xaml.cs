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

namespace DgSuche
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
                if (Tag is Ortsmarke)
                    ((Ortsmarke)Tag).BetrachteInDereGlobus();
                else if (Tag is string)
                    new Ortsmarke(Tag.ToString()).BetrachteInDereGlobus();
            }
            else
                MessageBox.Show("Kein Eintrag ausgewählt.", "Globus");
        }
    }
}
