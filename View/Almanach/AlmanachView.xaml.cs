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
using VM = MeisterGeister.ViewModel.Almanach;

namespace MeisterGeister.View.Almanach
{
    /// <summary>
    /// Interaktionslogik für AlmanachView.xaml
    /// </summary>
    public partial class AlmanachView : UserControl
    {
        public AlmanachView()
        {
            InitializeComponent();
            VM = new VM.AlmanachViewModel();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.AlmanachViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.AlmanachViewModel))
                    return null;
                return DataContext as VM.AlmanachViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.Refresh();
        }
    }
}
