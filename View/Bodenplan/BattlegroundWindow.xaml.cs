using MeisterGeister.ViewModel.Bodenplan;
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
using System.Windows.Shapes;

namespace MeisterGeister.View.Bodenplan
{
    /// <summary>
    /// Interaction logic for BattlegroundWindow.xaml
    /// </summary>
    public partial class BattlegroundWindow : Window
    {
        public BattlegroundWindow()
        {
            InitializeComponent();
        }

        public BattlegroundViewModel VM
        {
            get { return battlegroundView1.VM; }
            set { battlegroundView1.VM = value; }
        }
    }
}
