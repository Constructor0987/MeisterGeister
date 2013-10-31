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
    /// Interaktionslogik für SliderRange.xaml
    /// </summary>
    public partial class SliderRange : UserControl
    {
        public SliderRange()
        {
            InitializeComponent();

            this.Loaded += Slider_Loaded;
        }
        
        void Slider_Loaded(object sender, RoutedEventArgs e)
        {
            LowerSlider.ValueChanged += LowerSlider_ValueChanged;
            UpperSlider.ValueChanged += UpperSlider_ValueChanged;
        }
        
        private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double posLeft = (root.ActualWidth - 17) / (LowerSlider.Maximum / e.NewValue);

            UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
            UpperValue = UpperSlider.Value;

            LowerSlider.ToolTip = TimeSpan.FromMilliseconds(LowerValue).ToString(@"mm\:ss");
            UpperSlider.ToolTip = TimeSpan.FromMilliseconds(UpperValue).ToString(@"mm\:ss");

            brdLine.Margin = new Thickness(posLeft, 0, brdLine.Margin.Right, 0);
        }

        private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double posRight = this.ActualWidth - 17 - (root.ActualWidth - 17) / (LowerSlider.Maximum / e.NewValue);

            LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
            LowerValue = LowerSlider.Value;

            LowerSlider.ToolTip = TimeSpan.FromMilliseconds(LowerValue).ToString(@"mm\:ss");
            UpperSlider.ToolTip = TimeSpan.FromMilliseconds(UpperValue).ToString(@"mm\:ss");

            brdLine.Margin = new Thickness(brdLine.Margin.Left, 0, posRight, 0);  
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
            set { SetValue(LowerValueProperty, value); }
        }

        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(SliderRange), new UIPropertyMetadata(0d));

        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
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

    }
}