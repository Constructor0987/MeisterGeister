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
using MeisterGeister.Model.Extensions;
//eigene
using VM = MeisterGeister.ViewModel.AudioPlayer;
using MeisterGeister.Model;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
//using MeisterGeister.Model;

namespace MeisterGeister.View.AudioPlayer
{

    /// <summary>
    /// Interaktionslogik für AudioZeile.xaml
    /// </summary>
    public partial class AudioZeile : UserControl
    {
        public bool HintergrundMusik = false;


        public VM.Logic.AudioZeileVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.Logic.AudioZeileVM))
                    return null;
                return DataContext as VM.Logic.AudioZeileVM;
            }
            set { DataContext = value; }
        }

        public AudioZeile()
        {
            InitializeComponent();
            //contextAudioZeile.Visibility = Visibility.Visible;
        }

        
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) - 1 >= 0)
                sldPlaySpeed.Value = sldPlaySpeed.Ticks[sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) - 1];
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            if (sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) <= sldPlaySpeed.Ticks.Count - 2)
                sldPlaySpeed.Value = sldPlaySpeed.Ticks[sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) + 1];
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

        private void PauseWert_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double aktWert = ((Slider)sender).Value;

            if (e.Delta < 0)
            {
                if (!VM.SliderTicks.Contains(aktWert))
                {
                    int i = 0;
                    while (VM.SliderTicks[i] != VM.SliderTicks.Max() && VM.SliderTicks[i] < aktWert) i++;
                    aktWert = VM.SliderTicks[i];
                }
                else
                    if (aktWert > VM.SliderTicks.Min())
                        ((Slider)sender).Value = VM.SliderTicks[VM.SliderTicks.IndexOf(((Slider)sender).Value) - 1]; 
            }
            else
            {
                if (!VM.SliderTicks.Contains(aktWert))
                {
                    int i = VM.SliderTicks.Count-1;
                    while (VM.SliderTicks[i] != VM.SliderTicks.Min() && VM.SliderTicks[i] > aktWert) i++;
                    aktWert = VM.SliderTicks[i];
                }
                else
                    if (aktWert < VM.SliderTicks.Max())
                        ((Slider)sender).Value = VM.SliderTicks[VM.SliderTicks.IndexOf(((Slider)sender).Value) + 1];
            }
        }
        
        private void SpeedWert_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double aktWert = ((Slider)sender).Value;

            if (e.Delta < 0)
            {
                if (aktWert > ((Slider)sender).Ticks.Min())
                    ((Slider)sender).Value = ((Slider)sender).Ticks[((Slider)sender).Ticks.IndexOf(aktWert) - 1]; 
            }
            else
            {
                if (aktWert < ((Slider)sender).Ticks.Max())
                    ((Slider)sender).Value = ((Slider)sender).Ticks[((Slider)sender).Ticks.IndexOf(aktWert) + 1];
            }
        }
                
        private void VolumeWert_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)                                                                // ---  Volume verringern  ---
            {
                VM.volSmallChg = VM.UpdateVolSmallChgBeimVerringern(VM.aPlayTitelVolume, VM.volSmallChg);
                VM.aPlayTitelVolume = VM.VolumeVerringern(VM.aPlayTitelVolume, VM.volSmallChg);
            }
            else                                                                            // ---  Volume erhöhen  ---
            {
                VM.volSmallChg = VM.UpdateVolSmallChgBeimErhöhen(VM.aPlayTitelVolume, VM.volSmallChg);
                VM.aPlayTitelVolume = VM.VolumeErhöhen(VM.aPlayTitelVolume, VM.volSmallChg);                
            }
        }

        private void AudioZeile_DragOver(object sender, DragEventArgs e)
        {
            VM.PlayerVM.audioZeileMouseOverDropped = VM.PlayerVM.FilteredLbEditorAudioZeilenListe.IndexOf(this.VM);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VM.volSmallChg = (e.NewValue < 1 || e.OldValue == 2 && e.NewValue == 1) ? .1 : 3;
            if (e.OldValue > e.NewValue && e.NewValue < .5)
                VM.aPlayTitelVolume = 0;
            else
                if (e.OldValue < e.NewValue && e.NewValue < .5) VM.aPlayTitelVolume = .5;
        }
    }
}
