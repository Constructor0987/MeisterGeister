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
using VM = MeisterGeister.ViewModel.MeisterSpicker;

namespace MeisterGeister.View.MeisterSpicker
{
    /// <summary>
    /// Interaktionslogik für MeisterSpickerView.xaml
    /// </summary>
    public partial class MeisterSpickerView : UserControl
    {

        public MeisterSpickerView()
        {
            InitializeComponent();
           // VM = new VM.MeisterSpickerViewModel();

        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.MeisterSpickerViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.MeisterSpickerViewModel))
                    return null;
                return DataContext as VM.MeisterSpickerViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM.con != null)
                VM.con.Close();
        }
    }
}
