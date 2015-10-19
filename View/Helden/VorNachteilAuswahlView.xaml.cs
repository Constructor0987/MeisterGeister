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
using System.Windows.Shapes;
using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden
{
    /// <summary>
    /// Interaktionslogik für VorNachteilAuswahlView.xaml
    /// </summary>
    public partial class VorNachteilAuswahlView : Window
    {
        public VorNachteilAuswahlView(Model.VorNachteil vn)
        {
            InitializeComponent();
            OK_Click = false;

            VM = new VM.VorNachteilAuswahlViewModel(vn);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.VorNachteilAuswahlViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.VorNachteilAuswahlViewModel))
                    return null;
                return DataContext as VM.VorNachteilAuswahlViewModel;
            }
            set { DataContext = value; }
        }

        public ViewModel.Helden.VorNachteilAuswahlItem Auswahl
        {
            get
            {
                if (VM != null)
                    return VM.Auswahl;
                return null;
            }
        }

        public bool OK_Click { get; set; }

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            OK_Click = true;
            Close();
        }

    }
}
