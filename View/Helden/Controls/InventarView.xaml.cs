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
using VM = MeisterGeister.ViewModel.Inventar;
//Weitere Usings
using System.Diagnostics;

namespace MeisterGeister.View.Helden.Controls
{
    public partial class InventarView : UserControl
    {

        public InventarView()
        {
            InitializeComponent();
            VM = new VM.InventarViewModel();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.InventarViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.InventarViewModel))
                    return null;
                return DataContext as VM.InventarViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.LoadDaten();
            }
            catch (Exception) { }
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }

    }
}
