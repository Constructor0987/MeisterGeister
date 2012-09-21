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
            DataContext = new VM.ProbeControlViewModel();
        }

        private void UserControl_DataContextChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && DataContext != null)
                (DataContext as VM.ProbeControlViewModel).Refresh();
        }

        //private VM.ProbeControlViewModel VM { get; set; }


        //public VM.ProbeControlViewModel VM
        //{
        //    get { return (VM.ProbeControlViewModel)GetValue(VmProperty); }
        //    set { SetValue(VmProperty, value); }
        //}
        //public static readonly DependencyProperty VmProperty = DependencyProperty.Register(
        //  "VM", typeof(VM.ProbeControlViewModel), typeof(ProbeControl),
        //        new FrameworkPropertyMetadata(new VM.ProbeControlViewModel(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnViewModelChanged)));

        //private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ProbeControl control = (ProbeControl)d;
        //    control.DataContext = control.VM;
        //    //(e.NewValue as VM.ProbeControlViewModel).Gewürfelt += control.ProbeControl_Gewürfelt;
        //}

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

        //public Model.Held Held
        //{
        //    get { return (Model.Held)GetValue(HeldProperty); }
        //    set { SetValue(HeldProperty, value); }
        //}
        //public static readonly DependencyProperty HeldProperty = DependencyProperty.Register(
        //  "Held", typeof(Model.Held), typeof(ProbeControl),
        //        new FrameworkPropertyMetadata(new Model.Held(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnHeldChanged)));

        //private static void OnHeldChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ProbeControl control = (ProbeControl)d;
        //    control.VM.Held = control.Held;// e.NewValue as Model.Held;
        //}


        //private void ProbeControl_Gewürfelt(object sender, EventArgs e)
        //{
        //    // Event werfen
        //    if (Gewürfelt != null)
        //        Gewürfelt(this, new EventArgs());
        //}

        //public event EventHandler Gewürfelt;
    }
}
