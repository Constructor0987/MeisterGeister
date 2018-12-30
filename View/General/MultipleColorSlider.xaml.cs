using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MeisterGeister.View.General
{
    // public delegate void NumSldValueChangedEventHandler(MultipleColorSlider sender);

    /// <summary>
    /// Interaktionslogik für MultipleColorSlider.xaml
    /// </summary>
    public partial class MultipleColorSlider : UserControl
    {
        public MultipleColorSlider()
        {
             InitializeComponent();
        }

        #region //---- EIGENSCHAFTEN ----
        public int ColorWechsel
        {
            get { return (int)GetValue(ColorWechselProperty); }
            set { SetValue(ColorWechselProperty, value); }
        }

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public Color FirstColor
        {
            get { return (Color)GetValue(FirstColorProperty); }
            set { SetValue(FirstColorProperty, value); }
        }

        public Color SecondColor
        {
            get { return (Color)GetValue(SecondColorProperty); }
            set { SetValue(SecondColorProperty, value); }
        }

        public Color ThirdColor
        {
            get { return (Color)GetValue(ThirdColorProperty); }
            set { SetValue(ThirdColorProperty, value); }
        }

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        public double Upper1Value
        {
            get { return (double)GetValue(Upper1ValueProperty); }
            set { SetValue(Upper1ValueProperty, value); }
        }

        public double Upper2Value
        {
            get { return (double)GetValue(Upper2ValueProperty); }
            set { SetValue(Upper2ValueProperty, value); }
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public bool IsSnapToTickEnabled
        {
            get { return (bool)GetValue(IsSnapToTickEnabledProperty); }
            set { SetValue(IsSnapToTickEnabledProperty, value); }
        }

        public double TickFrequency
        {
            get { return (double)GetValue(TickFrequencyProperty); }
            set { SetValue(TickFrequencyProperty, value); }
        }

        public TickPlacement TickPlacement
        {
            get { return (TickPlacement)GetValue(TickPlacementProperty); }
            set { SetValue(TickPlacementProperty, value); }
        }

        public DoubleCollection Ticks
        {
            get { return (DoubleCollection)GetValue(TicksProperty); }
            set { SetValue(TicksProperty, value); }
        }

        #endregion

        #region //---- REGISTER ----
        public static readonly DependencyProperty ColorWechselProperty =
            DependencyProperty.Register("ColorWechsel", typeof(int), typeof(MultipleColorSlider), new UIPropertyMetadata(4));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(0d));
        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.1d, null, LowerValueCoerceValueCallback));
        public static readonly DependencyProperty Upper1ValueProperty =
            DependencyProperty.Register("Upper1Value", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.5d, null, Upper1ValueCoerceValueCallback));
        public static readonly DependencyProperty Upper2ValueProperty =
            DependencyProperty.Register("Upper2Value", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.9d, null, Upper2ValueCoerceValueCallback));
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(1d));
        public static readonly DependencyProperty IsSnapToTickEnabledProperty =
            DependencyProperty.Register("IsSnapToTickEnabled", typeof(bool), typeof(MultipleColorSlider), new UIPropertyMetadata(false));
        public static readonly DependencyProperty TickFrequencyProperty =
            DependencyProperty.Register("TickFrequency", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(0.1d));
        public static readonly DependencyProperty TickPlacementProperty =
            DependencyProperty.Register("TickPlacement", typeof(TickPlacement), typeof(MultipleColorSlider), new UIPropertyMetadata(TickPlacement.BottomRight));
        public static readonly DependencyProperty TicksProperty =
            DependencyProperty.Register("Ticks", typeof(DoubleCollection), typeof(MultipleColorSlider), new UIPropertyMetadata(null));
        public static readonly DependencyProperty StartColorProperty =
            DependencyProperty.Register("StartColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static readonly DependencyProperty EndColorProperty =
            DependencyProperty.Register("EndColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static readonly DependencyProperty FirstColorProperty =
            DependencyProperty.Register("FirstColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static readonly DependencyProperty SecondColorProperty =
            DependencyProperty.Register("SecondColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static readonly DependencyProperty ThirdColorProperty =
            DependencyProperty.Register("ThirdColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));

        #endregion

        #region --- Fuctions ---
        
        #endregion

        #region --- Events ---

        private static object LowerValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            MultipleColorSlider targetSlider = (MultipleColorSlider)target;
            double value = (double)valueObject;

            return
                (targetSlider.ColorWechsel >= 4) ?
                Math.Min(value, targetSlider.Upper1Value - .01) :
                value;
        }

        private static object Upper1ValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            MultipleColorSlider targetSlider = (MultipleColorSlider)target;
            double value = (double)valueObject;

            return 
                (targetSlider.ColorWechsel >= 5)?
                Math.Min(Math.Max(value, targetSlider.LowerValue + .01), targetSlider.Upper2Value - .01):
                Math.Max(value, targetSlider.LowerValue + .01);

        }

        private static object Upper2ValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            MultipleColorSlider targetSlider = (MultipleColorSlider)target;
            double value = (double)valueObject;

            return Math.Max(value, targetSlider.Upper1Value + .01);
        }

        #endregion
    }

}
