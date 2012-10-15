using System.Windows;
using System.Windows.Controls;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Proben;

namespace MeisterGeister.View.Proben
{
    /// <summary>
    /// Interaktionslogik für ProbeControl.xaml
    /// </summary>
    public partial class ProbeControl : UserControl
    {
        // TODO MT: Erfolgswahrscheinlichkeit hinzufügen

        #region //---- KONSTRUKTOR ----

        public ProbeControl()
        {
            InitializeComponent();
        }

        #endregion //---- KONSTRUKTOR ----

        #region //---- EIGENSCHAFTEN & FELDER ----

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.ProbeControlViewModel VM 
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ProbeControlViewModel))
                    return null;
                return DataContext as VM.ProbeControlViewModel;
            }
            set { DataContext = value; }
        }

        #endregion //---- EIGENSCHAFTEN & FELDER ----

        #region //---- DEPENDENCY PROPERTIES ----

        public Model.Held Held
        {
            get { return (Model.Held)GetValue(HeldProperty); }
            set { SetValue(HeldProperty, value); }
        }
        public static readonly DependencyProperty HeldProperty = DependencyProperty.Register(
          "Held", typeof(Model.Held), typeof(ProbeControl),
                new FrameworkPropertyMetadata(new Model.Held(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnHeldChanged)));

        private static void OnHeldChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProbeControl control = (ProbeControl)d;
            if (control.VM == null)
                control.VM = new VM.ProbeControlViewModel();
            if (e.NewValue is Model.Held)
                control.VM.Held = e.NewValue as Model.Held;
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
          "Orientation", typeof(Orientation), typeof(ProbeControl),
                new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion //---- DEPENDENCY PROPERTIES ----

        #region //---- EVENTS ----

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Neues ViewModel erzeugen, falls keins vorhanden
            if (VM == null)
                VM = new VM.ProbeControlViewModel();
        }

        #endregion //---- EVENTS ----
    }
}
