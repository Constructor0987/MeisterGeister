using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;
using VM = MeisterGeister.ViewModel.Proben;

namespace MeisterGeister.View.Proben
{
    /// <summary>
    /// Interaktionslogik für ProbenSpielerInfoView.xaml
    /// </summary>
    public partial class ProbenSpielerInfoView : UserControl
    {
        public ProbenSpielerInfoView()
        {
            InitializeComponent();

            //VM = new VM.ProbenViewModel(View.General.ViewHelper.Confirm, View.General.ViewHelper.ShowError);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.ProbenViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ProbenViewModel))
                    return null;
                return DataContext as VM.ProbenViewModel;
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
