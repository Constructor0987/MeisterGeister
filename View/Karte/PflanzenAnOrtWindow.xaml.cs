using MeisterGeister.ViewModel.Karte;
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

namespace MeisterGeister.View.Karte
{
    /// <summary>
    /// Interaktionslogik für PflanzenAnOrtWindow.xaml
    /// </summary>
    public partial class PflanzenAnOrtWindow : Window
    {
        public PflanzenAnOrtWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public PflanzenAnOrtViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is PflanzenAnOrtViewModel))
                    return null;
                return DataContext as PflanzenAnOrtViewModel;
            }
            set { DataContext = value; }
        }
    }
}
