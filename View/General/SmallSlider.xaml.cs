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
// Eigene Usings
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.View.General
{
    public delegate void NumSldValueChangedEventHandler(SmallSlider sender);

    /// <summary>
    /// Interaktionslogik für SmallSlider.xaml
    /// </summary>
    public partial class SmallSlider : UserControl
    {
        public event NumSldValueChangedEventHandler NumValueChanged;

        public SmallSlider()
        {
             InitializeComponent();
        }

        #region //---- EIGENSCHAFTEN ----

        public int? Value
        {
            get { return (int?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int?), typeof(SmallSlider),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentValueChanged),
                    new CoerceValueCallback(OnCoerceValue)));

        private static object OnCoerceValue(DependencyObject d, Object baseValue)
        {
            SmallSlider slider = (SmallSlider)d;
            int? value = (int?)baseValue;
            if (value == null)
                value = 0;

            if ((int)value < slider.MinValue)
                return slider.MinValue;
            else if ((int)value > slider.MaxValue)
                return slider.MaxValue;
            return value;
        }

        public bool NoMouseWheel
        {
            get { return (bool)GetValue(NoMouseWheelProperty); }
            set { SetValue(NoMouseWheelProperty, value); }
        }
        public static DependencyProperty NoMouseWheelProperty = DependencyProperty.Register("NoMouseWheel", typeof(bool), typeof(SmallSlider),
                new PropertyMetadata(false));

        public int IncWheel
        {
            get { return (int)GetValue(IncWheelProperty); }
            set { SetValue(IncWheelProperty, value); }
        }
        public static DependencyProperty IncWheelProperty = DependencyProperty.Register("IncWheel", typeof(int), typeof(SmallSlider),
                new PropertyMetadata(1));

        public int DecWheel
        {
            get { return (int)GetValue(DecWheelProperty); }
            set { SetValue(DecWheelProperty, value); }
        }
        public static DependencyProperty DecWheelProperty = DependencyProperty.Register("DecWheel", typeof(int), typeof(SmallSlider),
                new PropertyMetadata(1));
        
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(SmallSlider),
                new PropertyMetadata(int.MinValue));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(SmallSlider),
                new PropertyMetadata(int.MaxValue));
        
        public bool VerticalStyle
        {
            get { return (bool)GetValue(VerticalStyleProperty); }
            set { SetValue(VerticalStyleProperty, value); }
        }
        public static DependencyProperty VerticalStyleProperty = DependencyProperty.Register("VerticalStyle", typeof(bool), typeof(SmallSlider),
                new PropertyMetadata(false));

        public bool HorizontalStyle
        {
            get { return (bool)GetValue(HorizontalStyleProperty); }
            set { SetValue(HorizontalStyleProperty, value); }
        }
        public static DependencyProperty HorizontalStyleProperty = DependencyProperty.Register("HorizontalStyle", typeof(bool), typeof(SmallSlider),
                new PropertyMetadata(false));

        #endregion

        #region --- Fuctions ---

        private void IncreaseValue(int i = 1)
        {
            int value = (Value ?? 0) + i;
            if (value < MaxValue)
                Value = value;
            else
                Value = MaxValue;
        }

        private void DecreaseValue(int i = 1)
        {
            int value = (Value ?? 0) - i;
            if (value > MinValue)
                Value = value;
            else
                Value = MinValue;
        }

        #endregion

        #region --- Events ---

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SmallSlider slider = (SmallSlider)d;
            int? value = slider.Value;

            slider._sliderHoriz.ToolTip = "Wert = " + value == null ? 0 : value.Value;
            slider._sliderVert.ToolTip = "Wert = " + value == null ? 0 : value.Value;

            if (slider.NumValueChanged != null)
                slider.NumValueChanged(slider);
        }


        private void Control_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!NoMouseWheel)
            {
                if (e.Delta < 0)
                    DecreaseValue(DecWheel);
                else
                    IncreaseValue(IncWheel);
                e.Handled = true;
            }
        }

        #endregion
    }

}