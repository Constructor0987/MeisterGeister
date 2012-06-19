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

namespace MeisterGeister.View.General
{
    public delegate void DoubleValueChangedEventHandler(DoubleBox sender);

    /// <summary>
    /// Interaktionslogik für DoubleBox.xaml
    /// </summary>
    public partial class DoubleBox : UserControl
    {
        public event DoubleValueChangedEventHandler NumValueChanged;

        public DoubleBox()
        {
            InitializeComponent();

            _backgroundBrushDefault = _textBoxDouble.Background;
            _borderBrushDefault = _textBoxDouble.BorderBrush;

            MinValue = Double.MinValue;
            MaxValue = Double.MaxValue;
        }

        public Double MinValue
        {
            get;
            set;
        }

        public Double MaxValue
        {
            get;
            set;
        }

        private Brush _backgroundBrushDefault;
        private Brush _borderBrushDefault;

        private bool _noBackground = false;
        public bool NoBackground
        {
            get { return _noBackground; }
            set 
            { 
                _noBackground = value;
                if (value)
                {
                    _textBoxDouble.Background = null;
                    _textBoxDouble.BorderBrush = null;
                }
                else
                {
                    _textBoxDouble.Background = _backgroundBrushDefault;
                    _textBoxDouble.BorderBrush = _borderBrushDefault;
                }
            }
        }

        public bool ShowButtons
        {
            get { return (bool)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
        }

        public static DependencyProperty ShowButtonsProperty = DependencyProperty.Register("ShowButtons", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        public Double Value
        {
            get { return (Double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Double), typeof(DoubleBox),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentValueChanged),
                    new CoerceValueCallback(OnCoerceValue)));

        private static object OnCoerceValue(DependencyObject d, Object baseValue)
        {
            DoubleBox box = (DoubleBox)d;
            if ((double)baseValue < box.MinValue)
                return box.MinValue;
            else if ((double)baseValue > box.MaxValue)
                return box.MaxValue;
            return baseValue;
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DoubleBox box = (DoubleBox)d;
            if (box.NumValueChanged != null)
                box.NumValueChanged(box);
        }


        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            // Umwandlung in decimal, um Rundungsfehler zu vermeiden
            Value = (double)((decimal)Value + 0.1M);
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            // Umwandlung in decimal, um Rundungsfehler zu vermeiden
            Value = (double)((decimal)Value - 0.1M);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Umwandlung in decimal, um Rundungsfehler zu vermeiden
            if (e.Delta < 0)
                Value = (double)((decimal)Value - 0.1M);
            else
                Value = (double)((decimal)Value + 0.1M);
        }

        private void _textBoxDouble_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Focus kurz entfernen, um eine aktualiserung zu erzwingen
                _textBoxDouble.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                _textBoxDouble.Focus();
            }
        }
    }
}
