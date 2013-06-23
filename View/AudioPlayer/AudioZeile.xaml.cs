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

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für AudioZeile.xaml
    /// </summary>
    public partial class AudioZeile : UserControl
    {
        public AudioZeile()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) - 1 >= 0)
                sldPlaySpeed.Value = sldPlaySpeed.Ticks[sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) - 1];
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            if (sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) <= sldPlaySpeed.Ticks.Count-2)
                sldPlaySpeed.Value = sldPlaySpeed.Ticks[sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) + 1];
        }
    }
}
