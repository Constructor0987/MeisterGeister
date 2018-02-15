using MeisterGeister.View.AudioPlayer;
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
    /// Interaktionslogik für KämpferView.xaml
    /// </summary>
    public partial class KämpferView : UserControl
    {
        public KämpferView()
        {
            InitializeComponent();
        }


        private void AudioSpeedButtonVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //_scrollViewerKämpferDetails.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            if (e.Delta > 1)
            {
                ((Slider)sender).Value += ((((Slider)sender).Value < 98) ? 3 : ((100 - ((Slider)sender).Value)));
            }
            else
            { ((Slider)sender).Value += ((((Slider)sender).Value > 2) ? -3 : 0); }
            //_scrollViewerKämpferDetails.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
        }

        private void btnAudioSpeedButtonWesenZuweisen_Click(object sender, RoutedEventArgs e)
        {
            MeisterGeister.Model.Held h = (Global.CurrentKampf.SelectedKämpfer.Kämpfer is MeisterGeister.Model.Held) ?
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer as MeisterGeister.Model.Held) : null;
            MeisterGeister.Model.GegnerBase g = (Global.CurrentKampf.SelectedKämpfer.Kämpfer is MeisterGeister.Model.GegnerBase) ?
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer as MeisterGeister.Model.GegnerBase) :
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer is MeisterGeister.Model.Gegner) ?
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer as MeisterGeister.Model.Gegner).GegnerBase : null;
            if (h != null)
            {
                PlaylistWesenAuswahlView wesenAuswahlView = new PlaylistWesenAuswahlView(h);
                wesenAuswahlView.ShowDialog();
            }
            else
                if (g != null)
                {
                    PlaylistWesenAuswahlView wesenAuswahlView = new PlaylistWesenAuswahlView(g);
                    wesenAuswahlView.ShowDialog();
                }
        }
    }
}
