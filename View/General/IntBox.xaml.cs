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
    public delegate void NumValueChangedEventHandler(IntBox sender);

    /// <summary>
    /// Interaktionslogik für IntBox.xaml
    /// </summary>
    public partial class IntBox : UserControl
    {
        public event NumValueChangedEventHandler NumValueChanged;

        public IntBox()
        {
            InitializeComponent();

            MinValue = int.MinValue;
            MaxValue = int.MaxValue;
        }

        public int MinValue
        {
            get;
            set;
        }

        public int MaxValue
        {
            get;
            set;
        }

        public bool NoMouseWheel
        {
            get { return (bool)GetValue(NoMouseWheelProperty); }
            set { SetValue(NoMouseWheelProperty, value); }
        }
        public static DependencyProperty NoMouseWheelProperty = DependencyProperty.Register("NoMouseWheel", typeof(bool), typeof(IntBox),
                new PropertyMetadata(false));

        public bool NoBackground
        {
            get { return (bool)GetValue(NoBackgroundProperty); }
            set { SetValue(NoBackgroundProperty, value); }
        }
        public static DependencyProperty NoBackgroundProperty = DependencyProperty.Register("NoBackground", typeof(bool), typeof(IntBox),
                new PropertyMetadata(false));

        public bool WeissAufSchwarz
        {
            get { return (bool)GetValue(WeissAufSchwarzProperty); }
            set { SetValue(WeissAufSchwarzProperty, value); }
        }
        public static DependencyProperty WeissAufSchwarzProperty = DependencyProperty.Register("WeissAufSchwarz", typeof(bool), typeof(IntBox),
                new PropertyMetadata(false));

        public bool MarkPlusValue
        {
            get { return (bool)GetValue(MarkPlusValueProperty); }
            set { SetValue(MarkPlusValueProperty, value); }
        }
        public static DependencyProperty MarkPlusValueProperty = DependencyProperty.Register("MarkPlusValue", typeof(bool), typeof(IntBox),
                new PropertyMetadata(false));

        public bool ShowButtons
        {
            get { return (bool)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
        }
        public static DependencyProperty ShowButtonsProperty = DependencyProperty.Register("ShowButtons", typeof(bool), typeof(IntBox),
                new PropertyMetadata(false));

        new public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        new public static DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(IntBox),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentForegroundChanged),
                    new CoerceValueCallback(OnCoerceForeground)));

        private static object OnCoerceForeground(DependencyObject d, Object baseValue)
        {
            IntBox box = (IntBox)d;

            if ((Brush)baseValue == box._textBoxInt.Background)
                return box._textBoxInt.Foreground;
            return baseValue;
        }

        private static void OnCurrentForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IntBox box = (IntBox)d;
            box._textBoxInt.Foreground = (Brush)e.NewValue;
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(IntBox),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentValueChanged),
                    new CoerceValueCallback(OnCoerceValue)));

        private static object OnCoerceValue(DependencyObject d, Object baseValue)
        {
            IntBox box = (IntBox)d;

            if ((int)baseValue < box.MinValue)
                return box.MinValue;
            else if ((int)baseValue > box.MaxValue)
                return box.MaxValue;
            return baseValue;
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IntBox box = (IntBox)d;

            if (box.MarkPlusValue)
                box.MarkRed((int)box.Value > 0);

            box._textBoxInt.Text = box.Value.ToString();

            if (box.NumValueChanged != null)
                box.NumValueChanged(box);
        }

        private void MarkRed(bool mark)
        {
            if (mark)
                _textBoxInt.Foreground = Brushes.Red;
            else
            {
                if (WeissAufSchwarz)
                    _textBoxInt.Foreground = Brushes.White;
                else
                    _textBoxInt.Foreground = Brushes.Black;
            }
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Value < MaxValue)
                Value++;
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Value > MinValue)
                Value--;
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!NoMouseWheel)
            {
                if (e.Delta < 0)
                {
                    if (Value > MinValue)
                        Value--;
                }
                else
                {
                    if (Value < MaxValue)
                        Value++;
                }
            }
        }

        private void _textBoxInt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Focus entfernen, um eine Aktualiserung zu erzwingen
                _textBoxInt.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
            }
        }

        private void _textBoxInt_LostFocus(object sender, RoutedEventArgs e)
        {
            int i = 0; int tmp = 0;
            string input = (sender as TextBox).Text.Trim();
            if (input.Contains('+'))
            {
                string[] operanden = input.Split('+');
                foreach (var item in operanden)
                {
                    i = 0;
                    if (Int32.TryParse(item, out i))
                        tmp += i;
                }
                input = tmp.ToString();
            }
            else if (input.Contains('-'))
            {
                string[] operanden = input.Split('-');
                if (operanden.Length >= 1)
                {
                    if (Int32.TryParse(operanden[0], out i))
                        tmp = i;
                }
                for (int j = 1; j < operanden.Length; j++ )
                {
                    i = 0;
                    if (Int32.TryParse(operanden[j], out i))
                        tmp -= i;
                }
                input = tmp.ToString();
            }

            i = 0;
            if (Int32.TryParse(input, out i))
            {
                if (i < MinValue)
                    i = MinValue;
                else if (i > MaxValue)
                    i = MaxValue;
                Value = i;
            }

            // Wert zurück in TextBox schreiben, falls Value korrigiert wurde
            (sender as TextBox).Text = Value.ToString();
        }
    }
}
