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

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für InitiativListe.xaml
    /// </summary>
    public partial class InitiativListe : UserControl
    {
        public InitiativListe()
        {
            InitializeComponent();
        }
        bool MouseIsOverScrViewer = false;

        private void scrViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!MouseIsOverScrViewer && ((ScrollViewer)sender).ScrollableWidth > 0)
            {
                int anzInisDavor = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count(t => t.Kampfrunde < Global.CurrentKampf.Kampf.Kampfrunde);
                double width1Ini = ((ScrollViewer)sender).ExtentWidth / Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count();
                ((ScrollViewer)sender).ScrollToHorizontalOffset(width1Ini * anzInisDavor);
            }
        }

        private void scrViewer_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseIsOverScrViewer = true;
        }

        private void scrViewer_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseIsOverScrViewer = false;
        }
    }
}
