using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaktionslogik für SliderRange.xaml
    /// </summary>
    public partial class SliderRange : UserControl
    {
        double HOffset = 0;


        public SliderRange()
        {
            InitializeComponent();
            this.Loaded += Slider_Loaded;
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }
        
        private void SliderRange_Unloaded(object sender, RoutedEventArgs e)
        {
            ((ToolTip)LowerSlider.ToolTip).IsOpen = false;
            ((ToolTip)UpperSlider.ToolTip).IsOpen = false;
        }
        
        void Slider_Loaded(object sender, RoutedEventArgs e)
        {
            LowerSlider.ValueChanged += LowerSlider_ValueChanged;
            UpperSlider.ValueChanged += UpperSlider_ValueChanged;
        }
        
        private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (((Slider)sender).ActualWidth == 0)
                return;
            double posLeft = (sldRange.ActualWidth - 17) / (LowerSlider.Maximum / e.NewValue) + 8;

            UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
            UpperValue = UpperSlider.Value;

            ttLwrSlider.Text = TimeSpan.FromMilliseconds(LowerValue).ToString(@"mm\:ss");
            ttUprSlider.Text = TimeSpan.FromMilliseconds(UpperValue).ToString(@"mm\:ss");

            if (IsInitialized && this.Visibility == Visibility.Visible)
                ((ToolTip)LowerSlider.ToolTip).IsOpen = true;
            LowerValue = LowerSlider.Value;
            if (PlayTitel != null)
                PlayTitel.TeilStart = LowerValue;
        }

        private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (((Slider)sender).ActualWidth == 0)
                return;
            double posRight = ((Slider)sender).ActualWidth - 17 - (sldRange.ActualWidth - 17) / (LowerSlider.Maximum / e.NewValue) + 8;

            LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
            LowerValue = LowerSlider.Value;

            ttUprSlider.Text = TimeSpan.FromMilliseconds(UpperValue).ToString(@"mm\:ss");
            ttLwrSlider.Text = TimeSpan.FromMilliseconds(LowerValue).ToString(@"mm\:ss");

            if (IsInitialized && this.Visibility == Visibility.Visible)
                ((ToolTip)UpperSlider.ToolTip).IsOpen = true;
            UpperValue = UpperSlider.Value;
            if (PlayTitel != null)
                PlayTitel.TeilEnde = UpperValue;
        }

        private void Slider_MouseMove(object sender, MouseEventArgs e)
        {
            ((ToolTip)(sender as Slider).ToolTip).IsOpen = e.LeftButton == MouseButtonState.Pressed;
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                HOffset = e.GetPosition(sender as Slider).X;
                ((ToolTip)(sender as Slider).ToolTip).HorizontalOffset = 0;
            }
            if (e.LeftButton == MouseButtonState.Pressed &&
                ((ToolTip)(sender as Slider).ToolTip).IsOpen)
                ((ToolTip)(sender as Slider).ToolTip).HorizontalOffset = e.GetPosition(sender as Slider).X - HOffset;
        }

        private void Slider_MouseLeave(object sender, MouseEventArgs e)
        {
            ((ToolTip)(sender as Slider).ToolTip).IsOpen = false;
        }
        
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(SliderRange), new UIPropertyMetadata(0d));

        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { if (this.IsEnabled && this.IsVisible) SetValue(LowerValueProperty, value);
            UpdateLayout();
            if (PlayTitel != null)
                PlayTitel.TeilStart = LowerValue;
            OnChanged("LowerValue");
            OnChanged("PlayTitel");
            }
        }

        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(SliderRange), new UIPropertyMetadata(0d));

        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set
            {
                if (this.IsEnabled && this.IsVisible) SetValue(UpperValueProperty, value);
            UpdateLayout();
            OnChanged("UpperValue");
            }
        }

        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double), typeof(SliderRange), new UIPropertyMetadata(0d));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(SliderRange), new UIPropertyMetadata(1d));


        public Audio_Playlist_Titel PlayTitel
        {
            get { return (Audio_Playlist_Titel)GetValue(PlayTitelProperty); }
            set { SetValue(PlayTitelProperty, value);
            OnChanged("PlayTitel");
            }
        }

        public static readonly DependencyProperty PlayTitelProperty =
            DependencyProperty.Register("PlayTitel", typeof(Audio_Playlist_Titel), typeof(SliderRange), null);

        private void SliderRange_Loaded(object sender, RoutedEventArgs e)
        {   
            ttUprSlider.Text = TimeSpan.FromMilliseconds(UpperValue).ToString(@"mm\:ss");
            ttLwrSlider.Text = TimeSpan.FromMilliseconds(LowerValue).ToString(@"mm\:ss");
        }

        #region //---- INotifyPropertyChanged Member ----
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}