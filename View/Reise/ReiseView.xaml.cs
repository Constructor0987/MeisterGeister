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
//Eigene Usings
using VMR = MeisterGeister.ViewModel.Reise;
using MeisterGeister.View.General;

namespace MeisterGeister.View.Reise
{
    /// <summary>
    /// Interaktionslogik für ReiseView.xaml
    /// </summary>
    public partial class ReiseView : UserControl
    {
        public ReiseView()
        {
            InitializeComponent();
            //VM = new VMR.ReiseViewModel();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VMR.ReiseViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VMR.ReiseViewModel))
                    return null;
                return DataContext as VMR.ReiseViewModel;
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
