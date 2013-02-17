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
using VM = MeisterGeister.ViewModel.Schmiede;
using MeisterGeister.View.General;
//Weitere Usings
using System.Diagnostics;

namespace MeisterGeister.View.Schmiede          
{
    /// <summary>
    /// Interaktionslogik für SchmiedeView.xaml
    /// </summary>
    public partial class SchmiedeView : UserControl
    {
        public SchmiedeView()
        {
            InitializeComponent();
            VM = new VM.SchmiedeViewModel();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.SchmiedeViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.SchmiedeViewModel))
                    return null;
                return DataContext as VM.SchmiedeViewModel;
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