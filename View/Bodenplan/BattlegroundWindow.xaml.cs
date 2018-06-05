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

        private static VisualBrush _visualBrush = null;
        public static VisualBrush VisualBrush
        {
            get
            {
                if (_visualBrush == null && _instance != null)
                    _visualBrush = new VisualBrush(BattlegroundWindow.Instance);
                return _visualBrush;
            }
        }

        public static event EventHandler BattlegroundWindowInstantiated;
        private static BattlegroundWindow _instance;
        public static BattlegroundWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattlegroundWindow();
                    _visualBrush = new VisualBrush(_instance);
                    if (BattlegroundWindowInstantiated != null)
                        BattlegroundWindowInstantiated(_instance, new EventArgs());
                }
                return _instance;
            }
        }
    }
}
