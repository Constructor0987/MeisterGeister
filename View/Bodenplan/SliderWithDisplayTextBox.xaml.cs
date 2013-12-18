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
    /// Interaction logic for SliderWithDisplayTextBox.xaml
    /// </summary>
    public partial class SliderWithDisplayTextBox : UserControl
    {
        public SliderWithDisplayTextBox()
        {
            InitializeComponent();
        }

        //Dependency property for get/set slider value
        public double? SliderValue
        {
            get
            {
                return (double?) GetValue(SliderValueProperty);       
            }
            set
            {
                SetValue(SliderValueProperty, value);
            }
        }
        public static DependencyProperty SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double?), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set control label text
        public String SliderLabelTextValue
        {
            get { return (String)GetValue(SliderLabelTextProperty); }
            set { SetValue(SliderLabelTextProperty, value); }
        }
        public static DependencyProperty SliderLabelTextProperty = DependencyProperty.Register("SliderLabelTextValue", typeof(String), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata("title", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set MINVALUE
        public double? SliderMinValue
        {
            get { return (double?)GetValue(SliderMinValueProperty); }
            set { SetValue(SliderMinValueProperty, value); }
        }
        public static DependencyProperty SliderMinValueProperty = DependencyProperty.Register("SliderMinValue", typeof(double?), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set MAXVALUE
        public double? SliderMaxValue
        {
            get { return (double?)GetValue(SliderMaxValueProperty); }
            set { SetValue(SliderMaxValueProperty, value); }
        }
        public static DependencyProperty SliderMaxValueProperty = DependencyProperty.Register("SliderMaxValue", typeof(double?), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set SMALLCHANGE (minimumstep on slider)
        public double? TickFrequencyValue
        {
            get { return (double?)GetValue(TickFrequencyValueProperty); }
            set { SetValue(TickFrequencyValueProperty, value); }
        }
        public static DependencyProperty TickFrequencyValueProperty = DependencyProperty.Register("TickFrequencyValue", typeof(double?), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set SMALLCHANGE (minimumstep on slider)
        public bool TextBoxVisibilityValue
        {
            get { return (bool)GetValue(TextBoxVisibilityValueProperty); }
            set { SetValue(TextBoxVisibilityValueProperty, value); }
        }
        public static DependencyProperty TextBoxVisibilityValueProperty = DependencyProperty.Register("TextBoxVisibilityValue", typeof(bool), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //Dependency property for get/set control label text
        public String SliderMinTextValue
        {
            get { return (String)GetValue(SliderMinTextValueProperty); }
            set { SetValue(SliderMinTextValueProperty, value); }
        }
        public static DependencyProperty SliderMinTextValueProperty = DependencyProperty.Register("SliderMinTextValue", typeof(String), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata("min", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        
        //Dependency property for get/set control label text
        public String SliderMaxTextValue
        {
            get { return (String)GetValue(SliderMaxTextValueProperty); }
            set { SetValue(SliderMaxTextValueProperty, value); }
        }
        public static DependencyProperty SliderMaxTextValueProperty = DependencyProperty.Register("SliderMaxTextValue", typeof(String), typeof(SliderWithDisplayTextBox),
                new FrameworkPropertyMetadata("max", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        private void UIStrokeThicknessTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SliderValue != Convert.ToDouble(UIStrokeThicknessTextBox.Text)) UpdateSliderAndValue(); //, CultureInfo.InvariantCulture
        }

        private void UIStrokeThicknessTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Escape) UpdateSliderAndValue();
        }

        private void UpdateSliderAndValue()
        {
            if(UIStrokeThicknessTextBox.Text!="") SliderValue = Convert.ToDouble(UIStrokeThicknessTextBox.Text);
        }

        private void UIStrokeThicknessTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndValue();
        }
    }
}
