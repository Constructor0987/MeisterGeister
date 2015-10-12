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
        }

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(IntBox),
                new PropertyMetadata(double.MinValue));

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(IntBox),
                new PropertyMetadata(double.MaxValue));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        public bool NoMouseWheel
        {
            get { return (bool)GetValue(NoMouseWheelProperty); }
            set { SetValue(NoMouseWheelProperty, value); }
        }
        public static DependencyProperty NoMouseWheelProperty = DependencyProperty.Register("NoMouseWheel", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        public bool NoBackground
        {
            get { return (bool)GetValue(NoBackgroundProperty); }
            set { SetValue(NoBackgroundProperty, value); }
        }
        public static DependencyProperty NoBackgroundProperty = DependencyProperty.Register("NoBackground", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        public bool WeissAufSchwarz
        {
            get { return (bool)GetValue(WeissAufSchwarzProperty); }
            set { SetValue(WeissAufSchwarzProperty, value); }
        }
        public static DependencyProperty WeissAufSchwarzProperty = DependencyProperty.Register("WeissAufSchwarz", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        public bool MarkPlusValue
        {
            get { return (bool)GetValue(MarkPlusValueProperty); }
            set { SetValue(MarkPlusValueProperty, value); }
        }
        public static DependencyProperty MarkPlusValueProperty = DependencyProperty.Register("MarkPlusValue", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        public bool ShowButtons
        {
            get { return (bool)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
        }
        public static DependencyProperty ShowButtonsProperty = DependencyProperty.Register("ShowButtons", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        public bool CanBeNull
        {
            get { return (bool)GetValue(CanBeNullProperty); }
            set { SetValue(CanBeNullProperty, value); }
        }
        public static DependencyProperty CanBeNullProperty = DependencyProperty.Register("CanBeNull", typeof(bool), typeof(DoubleBox),
                new PropertyMetadata(false));

        new public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        new public static DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(DoubleBox),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentForegroundChanged),
                    new CoerceValueCallback(OnCoerceForeground)));

        private static object OnCoerceForeground(DependencyObject d, Object baseValue)
        {
            DoubleBox box = (DoubleBox)d;

            if ((Brush)baseValue == box._textBoxDouble.Background)
                return box._textBoxDouble.Foreground;
            return baseValue;
        }

        private static void OnCurrentForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DoubleBox box = (DoubleBox)d;
            box._textBoxDouble.Foreground = (Brush)e.NewValue;
        }

        public double? Value
        {
            get { return (double?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double?), typeof(DoubleBox),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentValueChanged),
                    new CoerceValueCallback(OnCoerceValue)));

        private static object OnCoerceValue(DependencyObject d, Object baseValue)
        {
            DoubleBox box = (DoubleBox)d;
            double? value = (double?)baseValue;
            if (value == null)
            {
                if (box.CanBeNull)
                    return null;
                else
                    value = 0.0;
            }

            if ((double)value < box.MinValue)
                return box.MinValue;
            else if ((double)value > box.MaxValue)
                return box.MaxValue;
            return value;
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DoubleBox box = (DoubleBox)d;
            double? value = box.Value;
            if (box.MarkPlusValue)
                box.MarkRed(value != null && value > 0.0);

            box._textBoxDouble.Text = (value == null) ? String.Empty : value.ToString();

            if (box.NumValueChanged != null)
                box.NumValueChanged(box);
        }

        private void MarkRed(bool mark)
        {
            if (mark)
                _textBoxDouble.Foreground = Brushes.Red;
            else
            {
                if (WeissAufSchwarz)
                    _textBoxDouble.Foreground = Brushes.White;
                else
                    _textBoxDouble.Foreground = Brushes.Black;
            }
        }

        private void IncreaseValue(double i = 1)
        {
            double value = (Value ?? 0.0) + i;
            if (value < MaxValue)
                Value = value;
            else
                Value = MaxValue;
        }

        private void DecreaseValue(double i = 1)
        {
            double value = (Value ?? 0.0) - i;
            if (value > MinValue)
                Value = value;
            else
                Value = MinValue;
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (!IsReadOnly)
                IncreaseValueCeiling();
        }

        private void IncreaseValueCeiling()
        {
            double nextValue = Math.Ceiling(Value.GetValueOrDefault());
            double addValue = (nextValue == Value) ? 1 : nextValue - Value.GetValueOrDefault();
            IncreaseValue(addValue);
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (!IsReadOnly)
                DecreaseValueFloor();
        }

        private void DecreaseValueFloor()
        {
            double nextValue = Math.Floor(Value.GetValueOrDefault());
            double subValue = (nextValue == Value) ? 1 : Value.GetValueOrDefault() - nextValue;
            DecreaseValue(subValue);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!NoMouseWheel && !IsReadOnly && _textBoxDouble.IsFocused)
            {
                if (e.Delta < 0)
                    DecreaseValueFloor();
                else
                    IncreaseValueCeiling();
                e.Handled = true;
            }
        }

        private void _textBoxDouble_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Focus entfernen, um eine Aktualiserung zu erzwingen
                if (!_textBoxDouble.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right)))
                    _textBoxDouble.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void _textBoxDouble_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsReadOnly)
            {
                if (e.Key == Key.Up)
                    IncreaseValue();
                else if (e.Key == Key.Down)
                    DecreaseValue();
            }
        }

        private void _textBoxDouble_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)e.OriginalSource;
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void _textBoxDouble_LostFocus(object sender, RoutedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (CanBeNull && input == null || input == string.Empty)
            {
                Value = null;
                return;
            }
            input = input.Trim();
            if ((input.Contains(',') || input.Contains('.')) != true) // nur parsen, wenn keine Dezimalzahl enthalten
                Value = Logic.General.Würfel.Parse(input, true);
            else
            {
                double tmp = 0.0;
                Double.TryParse(input, out tmp);
                Value = tmp;
            }
            // Wert zurück in TextBox schreiben, falls Value korrigiert wurde
            (sender as TextBox).Text = Value.ToString();
        }
    }
}
