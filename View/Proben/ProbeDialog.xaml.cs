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
using System.Windows.Shapes;
using MeisterGeister.Logic.General;

namespace MeisterGeister.View.Proben
{
    /// <summary>
    /// Interaktionslogik für ProbeDialog.xaml
    /// </summary>
    public partial class ProbeDialog : Window
    {
        public ProbeDialog()
        {
            InitializeComponent();
        }

        public ProbeDialog(Probe probe, Model.Held held = null)
        {
            InitializeComponent();

            _probeControl.VM = new ViewModel.Proben.ProbeControlViewModel();
            _probeControl.VM.Probe = probe;
            _probeControl.VM.Held = held;
        }
    }
}
