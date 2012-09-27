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

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IntBox box = (IntBox)d;

            if (box.MarkPlusValue)
                box.MarkRed((int)box.Value > 0);

            box._textBoxInt.Text = box.Value.ToString();

            if (box.NumValueChanged != null)
                box.NumValueChanged(box);
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
                // Focus kurz entfernen, um eine aktualiserung zu erzwingen
                _textBoxInt.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                _textBoxInt.Focus();
            }
        }

        private void _textBoxInt_LostFocus(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if (Int32.TryParse((sender as TextBox).Text, out i))
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
