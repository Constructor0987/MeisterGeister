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
    public partial class AudioZeile : ContentControl
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

        private void btnGewichtung_Click(object sender, RoutedEventArgs e)
        {
            int gewichtung = Convert.ToInt32(((Button)sender).Tag);
            gewichtung++;
            if (gewichtung == 6) gewichtung = 0;
            foreach (var img in grdbtnGewichtung.Children)
                if (img.GetType() == typeof(Image))
                    ((Image)img).Visibility = ((Convert.ToInt32(((Image)img).Tag)) <= gewichtung && gewichtung > 0)? Visibility.Visible : Visibility.Hidden;
            ((Button)sender).Tag = gewichtung;
            ((Button)sender).ToolTip = "Gewichtung " + gewichtung;
        }
    }
}
