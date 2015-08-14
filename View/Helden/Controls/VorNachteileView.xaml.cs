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
            VM = new VM.VorNachteileViewModel(View.General.ViewHelper.Confirm, View.General.ViewHelper.ShowError);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.VorNachteileViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.VorNachteileViewModel))
                    return null;
                return DataContext as VM.VorNachteileViewModel;
            }
            set { DataContext = value; }
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
                _menuItemVorNachteilLöschen.IsEnabled = !VM.IsReadOnly;
                _menuItemVorNachteilWiki.IsEnabled = true;
            }
        }
    }
}
