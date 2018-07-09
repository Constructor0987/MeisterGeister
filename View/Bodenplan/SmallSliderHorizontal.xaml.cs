using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace MeisterGeister.View.Bodenplan
{
    /// <summary>
    /// Interaction logic for SmallSliderHorizontal.xaml
    /// </summary>
    public partial class SmallSliderHorizontal : UserControl
    {
        public SmallSliderHorizontal()
        {
            InitializeComponent();
        }

        //Dependency property for get/set slider value
        public double? SliderValue
        {
            get { return (double?) GetValue(SliderValueProperty); }
            set { SetValue(SliderValueProperty, value); }
        }
        public static DependencyProperty SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double?), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        
        //Dependency property for get/set MINVALUE
        public double? SliderMinimum
        {
            get { return (double?)GetValue(SliderMinimumProperty); }
            set { SetValue(SliderMinimumProperty, value); }
        }
        public static DependencyProperty SliderMinimumProperty = DependencyProperty.Register("SliderMinimum", typeof(double?), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set MAXVALUE
        public double? SliderMaximum
        {
            get { return (double?)GetValue(SliderMaximumProperty); }
            set { SetValue(SliderMaximumProperty, value); }
        }
        public static DependencyProperty SliderMaximumProperty = DependencyProperty.Register("SliderMaximum", typeof(double?), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set MAXVALUE
        public bool SliderIsSnapToTickEnabled
        {
            get { return (bool)GetValue(SliderIsSnapToTickEnabledProperty); }
            set { SetValue(SliderIsSnapToTickEnabledProperty, value); }
        }
        public static DependencyProperty SliderIsSnapToTickEnabledProperty = DependencyProperty.Register("SliderIsSnapToTickEnabled", typeof(bool), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set SMALLCHANGE (minimumstep on slider)
        public double? SliderTickFrequency
        {
            get { return (double?)GetValue(SliderTickFrequencyProperty); }
            set { SetValue(SliderTickFrequencyProperty, value); }
        }
        public static DependencyProperty SliderTickFrequencyProperty = DependencyProperty.Register("SliderTickFrequency", typeof(double?), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set SMALLCHANGE
        public double? SliderSmallChange
        {
            get { return (double?)GetValue(SliderSmallChangeProperty); }
            set { SetValue(SliderSmallChangeProperty, value); }
        }
        public static DependencyProperty SliderSmallChangeProperty = DependencyProperty.Register("SliderSmallChange", typeof(double?), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set LARGECHANGE
        public double? SliderLargeChange
        {
            get { return (double?)GetValue(SliderLargeChangeProperty); }
            set { SetValue(SliderLargeChangeProperty, value); }
        }
        public static DependencyProperty SliderLargeChangeProperty = DependencyProperty.Register("SliderLargeChange", typeof(double?), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set TICKS
        public DoubleCollection SliderTicks
        {
            get { return (DoubleCollection)GetValue(SliderTicksProperty); }
            set { SetValue(SliderTicksProperty, value); }
        }
        public static DependencyProperty SliderTicksProperty = DependencyProperty.Register("SliderTicks", typeof(DoubleCollection), typeof(SmallSliderHorizontal),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private void Slider_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0) 
            { if (SliderValue - SliderTickFrequency >= SliderMinimum) SliderValue -= SliderTickFrequency; else SliderValue = SliderMinimum; }
            else
            { if (SliderValue + SliderTickFrequency <= SliderMaximum) SliderValue += SliderTickFrequency; else SliderValue = SliderMaximum; }
        }

        ////Dependency property for get/set TICKPLACEMENT
        //public TickPlacement? SliderTickPlacement
        //{
        //    get { return (double?)GetValue(SliderTickPlacementProperty); }
        //    set { SetValue(SliderTickPlacementProperty, value); }
        //}
        //public static DependencyProperty SliderTickPlacementProperty = DependencyProperty.Register("SliderTickPlacement", typeof(double?), typeof(SmallSliderHorizontal),
        //        new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }
}
