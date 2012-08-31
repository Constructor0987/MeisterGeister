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

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für EigenschaftenView.xaml
    /// </summary>
    public partial class EigenschaftenView : UserControl
    {
        public EigenschaftenView()
        {
            InitializeComponent();

            // TODO MT: ViewModel erstellen
        }

        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            switch (((System.Windows.Controls.Button)sender).Tag.ToString())
            {
                case "LE":
                    _intBoxLeAktuell.Value = Convert.ToInt32(_textBlockLebensenergieMax.Text);
                    break;
                case "AU":
                    _intBoxAuAktuell.Value = Convert.ToInt32(_textBlockAusdauerMax.Text);
                    break;
                case "AE":
                    _intBoxAeAktuell.Value = Convert.ToInt32(_textBlockAstralenergieMax.Text);
                    break;
                case "KE":
                    _intBoxKeAktuell.Value = Convert.ToInt32(_textBlockKarmaenergieMax.Text);
                    break;
                default:
                    break;
            }
        }
    }
}
