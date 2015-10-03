using MeisterGeister.ViewModel.Karte;
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

namespace MeisterGeister.View.Karte
{
    /// <summary>
    /// Interaktionslogik für PflanzenMitLandschaftControl.xaml
    /// </summary>
    public partial class PflanzenMitLandschaftControl : UserControl
    {
        public PflanzenMitLandschaftControl()
        {
            InitializeComponent();
        }

        //braucht kein VM, sondern nur eine IEnumerable<Model.Pflanze> als DependencyProperty
        public IEnumerable<Model.Pflanze> Pflanzen
        {
            get { return (IEnumerable<Model.Pflanze>)GetValue(PflanzenProperty); }
            set { SetValue(PflanzenProperty, value); }
        }
        public static readonly DependencyProperty PflanzenProperty = DependencyProperty.Register(
          "Pflanzen", typeof(IEnumerable<Model.Pflanze>), typeof(PflanzenMitLandschaftControl));

    }
}
