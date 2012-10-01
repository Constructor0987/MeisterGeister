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

using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für ZauberView.xaml
    /// </summary>
    public partial class ZauberView : UserControl
    {
        public ZauberView()
        {
            InitializeComponent();
            VM = new VM.ZauberViewModel(View.General.ViewHelper.Popup, View.General.ViewHelper.Confirm, View.General.ViewHelper.ShowError);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.ZauberViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ZauberViewModel))
                    return null;
                return DataContext as VM.ZauberViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }

        private void ContextMenuZauber_Opened(object sender, RoutedEventArgs e)
        {
            if (_dataGridHeldZauber.SelectedItem == null)
            {
                _menuItemZauberLöschen.IsEnabled = false;
                _menuItemZauberWiki.IsEnabled = false;
            }
            else
            {
                _menuItemZauberLöschen.IsEnabled = true;
                _menuItemZauberWiki.IsEnabled = true;
            }
        }

        private void MenuItemZauberProben_Click(object sender, RoutedEventArgs e)
        {
            // TODO MT: Probe würfeln
            //if (ProbeWürfeln != null)
            //{
            //    ProbeWürfeln(SelectedZauberRow.ZauberRow.Name);
            //}
        }

    }
}
