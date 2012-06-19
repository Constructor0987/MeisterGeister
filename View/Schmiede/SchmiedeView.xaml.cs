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
using VM = MeisterGeister.ViewModel;
//Weitere Usings
using System.Diagnostics;

namespace MeisterGeister.View.Schmiede          
{
    /// <summary>
    /// Interaktionslogik für FeuerUndEisen.xaml
    /// </summary>
    public partial class SchmiedeView : UserControl
    {
        public SchmiedeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.Schmiede.SchmiedeViewModel).LoadDaten();
            }
            catch (Exception) { }
        }
    }
}
