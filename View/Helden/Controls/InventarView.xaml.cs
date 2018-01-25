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
using VM = MeisterGeister.ViewModel.Inventar;
//Weitere Usings
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace MeisterGeister.View.Helden.Controls {
    public partial class InventarView : UserControl {

        
        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public InventarView() {
            InitializeComponent();
        }

        #endregion

        #region Ereignisse

        private void SelectedAusrüstung_Click(object sender, RoutedEventArgs e)
        {
            if (MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung == 0 || MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung == 3)
                (DataContext as MeisterGeister.ViewModel.Inventar.InventarViewModel).SelectedHeld.BerechneRüstungswerte();
            if (MeisterGeister.Logic.Einstellung.Einstellungen.BEBerechnung == 0)
                (DataContext as MeisterGeister.ViewModel.Inventar.InventarViewModel).SelectedHeld.BerechneBehinderung();

            (DataContext as MeisterGeister.ViewModel.Inventar.InventarViewModel).SelectedHeld.BerechneAusruestungsGewicht();
        }
        
        #endregion

    }
}