using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeisterGeister.ViewModel.Karte;

namespace MeisterGeister.View.Karte
{
    public enum KoordinatenFormat
    {
        Dezimalgrad,
        Dezimalminuten,
        GradMinutenSekunden
    }

    /// <summary>
    /// Control zur Anzeige von Koordinaten.
    /// </summary>
    public partial class KoordinatenControl : UserControl
    {
        public KoordinatenControl()
        {
            InitializeComponent();
        }
        //TODO Eingabe möglich machen.

        //Koordinatenform wählbar. 
        public KoordinatenFormat Format
        {
            get { return (KoordinatenFormat)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }
        public static DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(KoordinatenFormat), typeof(KoordinatenControl),
                new FrameworkPropertyMetadata(KoordinatenFormat.Dezimalminuten, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnFormatChanged),
                    null));

        private static void OnFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            KoordinatenControl koords = (KoordinatenControl)d;
            Draw(koords);
        }

        public Point? Value
        {
            get { return (Point?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Point?), typeof(KoordinatenControl),
                new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCurrentValueChanged),
                    new CoerceValueCallback(OnCoerceValue)));

        private static object OnCoerceValue(DependencyObject d, Object baseValue)
        {
            KoordinatenControl koords = (KoordinatenControl)d;
            Point? value = (Point?)baseValue;
            if (value == null)
            {
                //if (box.CanBeNull)
                //    return null
                //else
                    value = new Point();
            }
            return value;
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            KoordinatenControl koords = (KoordinatenControl)d;
            Draw(koords);
        }

        private static void Draw(KoordinatenControl koords)
        {
            Point? value = koords.Value;
            KoordinatenFormat f = koords.Format;
            koords.text.Text = GetKoordinatenString(value ?? new Point(), f);
        }

        private static string GetKoordinatenString(Point p, KoordinatenFormat format=KoordinatenFormat.Dezimalminuten)
        {
            double xgrad, ygrad;
            double xminuten, yminuten;
            double xsekunden, ysekunden;
            xgrad = p.X;
            ygrad = p.Y;
            string ns = "N", ow = "E";
            if(format == KoordinatenFormat.Dezimalgrad)
                return String.Format("φ {0:0.000000}° λ {1:0.000000}°", ygrad, xgrad);
            if (p.Y < 0)
                ns = "S";
            if (p.X < 0)
                ow = "W";
            xgrad = Math.Floor(Math.Abs(p.X));
            ygrad = Math.Floor(Math.Abs(p.Y));
            xminuten = (Math.Abs(p.X) - xgrad) * 60.0;
            yminuten = (Math.Abs(p.Y) - ygrad) * 60.0;
            if(format == KoordinatenFormat.Dezimalminuten)
                return String.Format("{0}{1}° {2:0.0##} {3}{4}° {5:0.0##}", ns, ygrad, yminuten, ow, xgrad, xminuten);
            xsekunden = xminuten;
            ysekunden = yminuten;
            xminuten = Math.Floor(xminuten);
            yminuten = Math.Floor(yminuten);
            xsekunden = (xsekunden - xminuten) * 60.0;
            ysekunden = (ysekunden - yminuten) * 60.0;
            return String.Format("{0} {1}° {2}' {3:0.#}\" {4} {5}° {6}' {7:0.#}\"", ns, ygrad, yminuten, ysekunden, ow, xgrad, xminuten, ysekunden);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
