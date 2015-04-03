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

        public int IncWheel
        {
            get { return (int)GetValue(IncWheelProperty); }
            set { SetValue(IncWheelProperty, value); }
        }
        public static DependencyProperty IncWheelProperty = DependencyProperty.Register("IncWheel", typeof(int), typeof(IntBox),
                new PropertyMetadata(1));

        public int DecWheel
        {
            get { return (int)GetValue(DecWheelProperty); }
            set { SetValue(DecWheelProperty, value); }
        }
        public static DependencyProperty DecWheelProperty = DependencyProperty.Register("DecWheel", typeof(int), typeof(IntBox),
                new PropertyMetadata(1));

        public int DecValue
        {
            get { return (int)GetValue(DecValueProperty); }
            set { SetValue(DecValueProperty, value); }
        }
        public static DependencyProperty DecValueProperty = DependencyProperty.Register("DecValue", typeof(int), typeof(IntBox),
                new PropertyMetadata(1));

        public int IncValue
        {
            get { return (int)GetValue(IncValueProperty); }
            set { SetValue(IncValueProperty, value); }
        }
        public static DependencyProperty IncValueProperty = DependencyProperty.Register("IncValue", typeof(int), typeof(IntBox),
                new PropertyMetadata(1));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(IntBox),
                new PropertyMetadata(1));

        public Visibility ButtonHiddenMode
        {
            get { return (Visibility)GetValue(ButtonHiddenModeProperty); }
            set { SetValue(ButtonHiddenModeProperty, value); }
        }
        public static DependencyProperty ButtonHiddenModeProperty = DependencyProperty.Register("ButtonHiddenMode", typeof(Visibility), typeof(IntBox),
                new PropertyMetadata(Visibility.Collapsed));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(IntBox),
                new PropertyMetadata(false));

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

        public bool CanBeNull
        {
            get { return (bool)GetValue(CanBeNullProperty); }
            set { SetValue(CanBeNullProperty, value); }
        }
        public static DependencyProperty CanBeNullProperty = DependencyProperty.Register("CanBeNull", typeof(bool), typeof(IntBox),
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

        public int? Value
        {
            get { return (int?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int?), typeof(IntBox),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentValueChanged),
                    new CoerceValueCallback(OnCoerceValue)));

        private static object OnCoerceValue(DependencyObject d, Object baseValue)
        {
            IntBox box = (IntBox)d;
            int? value = (int?)baseValue;
            if (value == null)
            {
                if (box.CanBeNull)
                    return null;
                else
                    value = 0;
            }

            if ((int)value < box.MinValue)
                return box.MinValue;
            else if ((int)value > box.MaxValue)
                return box.MaxValue;
            return value;
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IntBox box = (IntBox)d;
            int? value = box.Value;
            if (box.MarkPlusValue)
                box.MarkRed(value != null && value > 0);

            box._textBoxInt.Text = (value==null)?String.Empty:value.ToString();

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

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (!IsReadOnly)
                IncreaseValue(IncValue);
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (!IsReadOnly)
                DecreaseValue(DecValue);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!NoMouseWheel && !IsReadOnly)
            {
                if (e.Delta < 0)
                    DecreaseValue(DecWheel);
                else
                    IncreaseValue(IncWheel);
                e.Handled = true;
            }
        }

        private void _textBoxInt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Focus entfernen, um eine Aktualiserung zu erzwingen
                if (!_textBoxInt.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right)))
                    _textBoxInt.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void _textBoxInt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsReadOnly)
            {
                if (e.Key == Key.Up)
                    IncreaseValue(IncValue);
                else if (e.Key == Key.Down)
                    DecreaseValue(DecValue);
            }
        }

        private void _textBoxInt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)e.OriginalSource;
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void _textBoxInt_LostFocus(object sender, RoutedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (CanBeNull && input == null || input == string.Empty)
            {
                Value = null;
                return;
            }
            input = input.Trim();
            Value = Logic.General.Würfel.Parse(input, true);
            // Wert zurück in TextBox schreiben, falls Value korrigiert wurde
            (sender as TextBox).Text = Value.ToString();
        }
    }
}
