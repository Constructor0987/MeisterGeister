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
            set
            {
                SetValue(LowerValueProperty, value);
                LowerValueProcent = value / Maximum;
            }
        }

        public double LowerValueProcent
        {
            get { return (double)GetValue(LowerValueProcentProperty); }
            set { SetValue(LowerValueProcentProperty, value); }
        }

        public double Upper1Value
        {
            get { return (double)GetValue(Upper1ValueProperty); }
            set { SetValue(Upper1ValueProperty, value); }
        }

        public double Upper1ValueProcent
        {
            get { return (double)GetValue(Upper1ValueProcentProperty); }
            set { SetValue(Upper1ValueProcentProperty, value); }
        }


        public double Upper2Value
        {
            get { return (double)GetValue(Upper2ValueProperty); }
            set { SetValue(Upper2ValueProperty, value); }
        }

        public double Upper2ValueProcent
        {
            get { return (double)GetValue(Upper2ValueProcentProperty); }
            set { SetValue(Upper2ValueProcentProperty, value); }
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
        public static DependencyProperty ColorWechselProperty =
            DependencyProperty.Register("ColorWechsel", typeof(int), typeof(MultipleColorSlider), new UIPropertyMetadata(4));

        public static DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(0d));
        public static DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.1d, null, LowerValueCoerceValueCallback));
        public static DependencyProperty LowerValueProcentProperty =
            DependencyProperty.Register("LowerValueProcent", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.1d));
        public static DependencyProperty Upper1ValueProperty =
            DependencyProperty.Register("Upper1Value", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.5d, null, Upper1ValueCoerceValueCallback));
        public static DependencyProperty Upper1ValueProcentProperty =
            DependencyProperty.Register("Upper1ValueProcent", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.5d));
        public static DependencyProperty Upper2ValueProperty =
            DependencyProperty.Register("Upper2Value", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.9d, null, Upper2ValueCoerceValueCallback));
        public static DependencyProperty Upper2ValueProcentProperty =
            DependencyProperty.Register("Upper2ValueProcent", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(.9d));
        public static DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(1d));
        public static DependencyProperty IsSnapToTickEnabledProperty =
            DependencyProperty.Register("IsSnapToTickEnabled", typeof(bool), typeof(MultipleColorSlider), new UIPropertyMetadata(false));
        public static DependencyProperty TickFrequencyProperty =
            DependencyProperty.Register("TickFrequency", typeof(double), typeof(MultipleColorSlider), new UIPropertyMetadata(0.1d));
        public static DependencyProperty TickPlacementProperty =
            DependencyProperty.Register("TickPlacement", typeof(TickPlacement), typeof(MultipleColorSlider), new UIPropertyMetadata(TickPlacement.BottomRight));
        public static DependencyProperty TicksProperty =
            DependencyProperty.Register("Ticks", typeof(DoubleCollection), typeof(MultipleColorSlider), new UIPropertyMetadata(null));
        public static DependencyProperty StartColorProperty =
            DependencyProperty.Register("StartColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static DependencyProperty EndColorProperty =
            DependencyProperty.Register("EndColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static DependencyProperty FirstColorProperty =
            DependencyProperty.Register("FirstColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static DependencyProperty SecondColorProperty =
            DependencyProperty.Register("SecondColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));
        public static DependencyProperty ThirdColorProperty =
            DependencyProperty.Register("ThirdColor", typeof(Color), typeof(MultipleColorSlider), new UIPropertyMetadata(Colors.White));

        #endregion

        #region --- Fuctions ---
        
        #endregion

        #region --- Events ---

        private static object LowerValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            MultipleColorSlider targetSlider = (MultipleColorSlider)target;
            double value = (double)valueObject;

            double valReturn = (targetSlider.ColorWechsel >= 4) ?
                Math.Min(value, targetSlider.Upper1Value * .99) :
                value;
            targetSlider.LowerValueProcent = valReturn / targetSlider.Maximum;
            
            return valReturn;
        }

        private static object Upper1ValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            MultipleColorSlider targetSlider = (MultipleColorSlider)target;
            double value = (double)valueObject;

            double valReturn =
                (targetSlider.ColorWechsel >= 5)?
                Math.Min(Math.Max(value, targetSlider.LowerValue * 1.01), targetSlider.Upper2Value * .99):
                Math.Max(value, targetSlider.LowerValue * 1.01);
            targetSlider.Upper1ValueProcent = valReturn / targetSlider.Maximum;

            return valReturn;

        }

        private static object Upper2ValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            MultipleColorSlider targetSlider = (MultipleColorSlider)target;
            double value = (double)valueObject;
            double valReturn = Math.Max(value, targetSlider.Upper1Value * 1.01);
            targetSlider.Upper2ValueProcent = valReturn / targetSlider.Maximum;

            return valReturn;
        }

        #endregion
    }

}
