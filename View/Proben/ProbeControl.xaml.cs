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
using MeisterGeister.Logic.General;
//Eigene Usings
using VM = MeisterGeister.ViewModel.Proben;

namespace MeisterGeister.View.Proben
{
    /// <summary>
    /// Interaktionslogik für ProbeControl.xaml
    /// </summary>
    public partial class ProbeControl : UserControl
    {
        public ProbeControl()
        {
            InitializeComponent();
        }

        private VM.ProbeControlViewModel VM 
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ProbeControlViewModel))
                    return null;
                return DataContext as VM.ProbeControlViewModel;
            }
            set
            {
                DataContext = value;
            }
        }


        //public Probe Probe
        //{
        //    get { return (Probe)GetValue(ProbeProperty); }
        //    set { SetValue(ProbeProperty, value); }
        //}
        //public static readonly DependencyProperty ProbeProperty = DependencyProperty.Register(
        //  "Probe", typeof(Probe), typeof(ProbeControl),
        //        new FrameworkPropertyMetadata(new Probe(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnProbeChanged)));

        //private static void OnProbeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ProbeControl control = (ProbeControl)d;
        //    //(e.NewValue as Probe).Gewürfelt += control.ProbeControl_Gewürfelt;
        //}

        
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

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Neues ViewModel erzeugen, falls keins vorhanden
            if (VM == null)
                VM = new VM.ProbeControlViewModel();
        }


        //private void ProbeControl_Gewürfelt(object sender, EventArgs e)
        //{
        //    // Event werfen
        //    if (Gewürfelt != null)
        //        Gewürfelt(this, new EventArgs());
        //}

        //public event EventHandler Gewürfelt;
    }
}
