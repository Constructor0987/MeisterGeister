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
using VM = MeisterGeister.ViewModel.Generator;
//Weitere Usings
using System.Diagnostics;
using System.IO;
using MeisterGeister.View.Windows;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.View.Generator
{
    public partial class GeneratorView : UserControl
    {
        public GeneratorView()
        {
            InitializeComponent();
            VM = new VM.GeneratorViewModel();
        }

        public VM.GeneratorViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.GeneratorViewModel))
                    return null;
                return DataContext as VM.GeneratorViewModel;
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
