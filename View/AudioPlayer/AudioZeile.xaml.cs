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
using System.Text.RegularExpressions;

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
        
        private void _btnPauseMaxPlus_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Tag = (Convert.ToInt32(tboxPauseMax.Text) >= 10000) ? 5000 :
                                   (Convert.ToInt32(tboxPauseMax.Text) >= 2000) ? 1000 : 200;

            int sollWert =  Convert.ToInt32(tboxPauseMax.Text) + Convert.ToInt32(((Button)sender).Tag);
            int max = Convert.ToInt32(sldKlangPause.Maximum);
            int pauseMax_wert = (sollWert <= max)? sollWert < 0 ? 0 : sollWert : max;

            tboxPauseMax.Text = Convert.ToString(pauseMax_wert);

            if (pauseMax_wert < Convert.ToInt32(tboxPauseMin.Text))
                tboxPauseMin.Text = tboxPauseMax.Text;

            ((Button)sender).Tag = (pauseMax_wert >= 10000)? 5000: 
                                   (pauseMax_wert >= 2000)? 1000: 200;
        }

        private void _btnPauseMaxMinus_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Tag = (Convert.ToInt32(tboxPauseMax.Text) >= 10000) ? -5000 :
                                   (Convert.ToInt32(tboxPauseMax.Text) >= 2000) ? -1000 : -200;

            int sollWert =  Convert.ToInt32(tboxPauseMax.Text) + Convert.ToInt32(((Button)sender).Tag);
            int pauseMax_wert = (sollWert < 0) ? 0 : sollWert;

            tboxPauseMax.Text = Convert.ToString(pauseMax_wert);

            if (pauseMax_wert < Convert.ToInt32(tboxPauseMin.Text))
                tboxPauseMin.Text = tboxPauseMax.Text;

            ((Button)sender).Tag = (pauseMax_wert >= 10000) ? -5000 :
                                   (pauseMax_wert >= 2000) ? -1000 : -200;
        }

        private void _btnPauseMinPlus_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Tag = (Convert.ToInt32(tboxPauseMin.Text) >= 10000) ? 5000 :
                                   (Convert.ToInt32(tboxPauseMin.Text) >= 2000) ? 1000 : 200;

            int sollWert = Convert.ToInt32(tboxPauseMin.Text) + Convert.ToInt32(((Button)sender).Tag);
            int max = Convert.ToInt32(sldKlangPause.Maximum);
            int pauseMin_wert = (sollWert >= Convert.ToInt32(sldKlangPause.Minimum)) ? sollWert > max ? max : sollWert : max;

            tboxPauseMin.Text = Convert.ToString(pauseMin_wert);

            if (pauseMin_wert > Convert.ToInt32(tboxPauseMax.Text))
                tboxPauseMax.Text = tboxPauseMin.Text;

            ((Button)sender).Tag = (pauseMin_wert >= 10000) ? 5000 :
                                   (pauseMin_wert >= 2000) ? 1000 : 200;
        }

        private void _btnPauseMinMinus_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Tag = (Convert.ToInt32(tboxPauseMin.Text) >= 10000) ? -5000 :
                                   (Convert.ToInt32(tboxPauseMin.Text) >= 2000) ? -1000 : -200;

            int sollWert = Convert.ToInt32(tboxPauseMin.Text) + Convert.ToInt32(((Button)sender).Tag);
            int pauseMin_wert = (sollWert < 0) ? 0 : sollWert;
            tboxPauseMin.Text = Convert.ToString(pauseMin_wert);

            if (pauseMin_wert > Convert.ToInt32(tboxPauseMax.Text))
                tboxPauseMax.Text = tboxPauseMin.Text;
            
            ((Button)sender).Tag = (pauseMin_wert >= 10000) ? -5000 :
                                   (pauseMin_wert >= 2000) ? -1000 : -200;
        }

        private void chkValidChange(object sender, TextChangedEventArgs e)
        {
            var oldIndex = ((TextBox)sender).CaretIndex;
            var help1 = Regex.Replace((((TextBox)sender).Text), @"\D", "");
            help1 = help1.Replace(" ", "");
            ((TextBox)sender).Text = help1;
            ((TextBox)sender).CaretIndex = help1.Length > oldIndex ? oldIndex : help1.Length;
        }

        private void tboxPauseMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            chkValidChange(sender, e);
            int wert = Convert.ToInt32(((TextBox)sender).Text);
            ((TextBox)sender).ToolTip = (wert < 1000) ? wert + " ms" : (wert < 60000) ? wert / 1000 + " sek." : wert / 60000 + " min.";
        }
        
    }
}
