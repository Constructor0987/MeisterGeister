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

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für KampfInfoView.xaml
    /// </summary>
    public partial class KampfInfoView : UserControl
    {
        public KampfInfoView(ViewModel.Kampf.Logic.Kampf kampf)
        {
            InitializeComponent();
            this.DataContext = kampf;
            //_listBoxKämpfer.DataContext = kampf.KämpferListe;
        }
    }
}
