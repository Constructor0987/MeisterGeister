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
using MeisterGeister.Model;
using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für EigenschaftenView.xaml
    /// </summary>
    public partial class EigenschaftenView : UserControl
    {
        public EigenschaftenView()
        {
            InitializeComponent();

            //VM an View Registrieren
            this.DataContext = new VM.EigenschaftenViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.EigenschaftenViewModel).Init();
            }
            catch (Exception)
            {
            }
            if (DataContext as VM.Logic.IChangeListener != null)
                (this.DataContext as VM.Logic.IChangeListener).ListenToChangeEvents = IsVisible;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (DataContext as VM.Logic.IChangeListener != null)
                (this.DataContext as VM.Logic.IChangeListener).ListenToChangeEvents = IsVisible;
        }
    }
}
