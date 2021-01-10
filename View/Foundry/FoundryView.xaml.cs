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
// Eigene Usings
using VM = MeisterGeister.ViewModel.Foundry;

namespace MeisterGeister.View.Foundry
{
    /// <summary>
    /// Interaktionslogik für FoundryView.xaml
    /// </summary>
    public partial class FoundryView : UserControl
    {

        public FoundryView()
        {
            InitializeComponent();
           // VM = new VM.MeisterSpickerViewModel();

        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.FoundryViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.FoundryViewModel))
                    return null;
                return DataContext as VM.FoundryViewModel;
            }
            set { DataContext = value; }
        }

    }
}
