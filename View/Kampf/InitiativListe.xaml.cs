using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

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

        private bool MouseIsOverScrViewer = false;

        private void scrViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!MouseIsOverScrViewer && ((ScrollViewer)sender).ScrollableWidth > 0)
            {
                var anzInisDavor = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count(t => t.Kampfrunde < Global.CurrentKampf.Kampf.Kampfrunde);
                var width1Ini = ((ScrollViewer)sender).ExtentWidth / Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Count();
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
