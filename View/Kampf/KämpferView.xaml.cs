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
            if (e.Delta > 1)
                ((Slider)sender).Value += ((((Slider)sender).Value < 98) ? 3 : ((100 - ((Slider)sender).Value)));
            else
                ((Slider)sender).Value += ((((Slider)sender).Value > 2) ? -3 : 0); 
        }
    }


}
