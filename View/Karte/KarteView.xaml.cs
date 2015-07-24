using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeisterGeister.ViewModel.Karte;

namespace MeisterGeister.View.Karte
{
    /// <summary>
    /// Interaktionslogik für KarteView.xaml
    /// </summary>
    public partial class KarteView : UserControl
    {
        public KarteView()
        {
            InitializeComponent();
            VM = new KarteViewModel();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public KarteViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is KarteViewModel))
                    return null;
                return DataContext as KarteViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.ZoomControlSize = MapScrollViewer.RenderSize;
                VM.Refresh();
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UserControl_Loaded(sender, null);
        }
    }
}
