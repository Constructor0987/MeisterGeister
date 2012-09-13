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
    /// Interaktionslogik für VorNachteileView.xaml
    /// </summary>
    public partial class VorNachteileView : UserControl
    {
        public VorNachteileView()
        {
            InitializeComponent();
            this.DataContext = new VM.VorNachteileViewModel(View.General.ViewHelper.Confirm, View.General.ViewHelper.ShowError);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as VM.VorNachteileViewModel).Init();
            }
            catch (Exception)
            {
            }
        }

        private void ContextMenuVorNachteile_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHeldVorNachteile.SelectedItem == null)
            {
                _menuItemVorNachteilLöschen.IsEnabled = false;
                _menuItemVorNachteilWiki.IsEnabled = false;
            }
            else
            {
                _menuItemVorNachteilLöschen.IsEnabled = true;
                _menuItemVorNachteilWiki.IsEnabled = true;
            }
        }

    }
}
