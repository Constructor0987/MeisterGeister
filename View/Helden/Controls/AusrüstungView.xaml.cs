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
using VM = MeisterGeister.ViewModel.Inventar;

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für AusrüstungView.xaml
    /// </summary>
    public partial class AusrüstungView : UserControl
    {
        public AusrüstungView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.AusrüstungViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.AusrüstungViewModel))
                    return null;
                return DataContext as VM.AusrüstungViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            VM = new VM.AusrüstungViewModel();
        }
    }
}
