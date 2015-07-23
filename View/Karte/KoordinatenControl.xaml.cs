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
    /// <summary>
    /// Control zur Anzeige von Koordinaten.
    /// </summary>
    public partial class KoordinatenControl : UserControl
    {
        public KoordinatenControl()
        {
            InitializeComponent();
        }

        //TODO: Koordinatenform wählbar. Commnand zum auswählen auf der Karte und zur Anzeige in DG. Eingabe möglich machen.

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
            Point? value = koords.Value;
            koords.textX.Text = (value == null) ? String.Empty : value.Value.X.ToString();
            koords.textY.Text = (value == null) ? String.Empty : value.Value.Y.ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
