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

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für GegnerWindow.xaml
    /// </summary>
    public partial class GegnerWindow : Window
    {
        public GegnerWindow(ViewModel.Kampf.Logic.Kampf kampf)
        {
            InitializeComponent();

            _kampf = kampf;

            if (App.Current.MainWindow != null)
            {
                Owner = App.Current.MainWindow;
                Width = App.Current.MainWindow.ActualWidth * 0.7;
                Height = App.Current.MainWindow.ActualHeight * 0.8;
            }
        }

        private ViewModel.Kampf.Logic.Kampf _kampf;

        private void ButtonAddToKampf_Click(object sender, RoutedEventArgs e)
        {
            if (_kampf != null)
            {
                Model.GegnerBase gegnerBase = _gegnerView.VM.SelectedGegnerBase;
                
                // TODO ??: Gegner aus GegnerBase erzeugen

                //Model.Gegner gegner = new Model.Gegner();
                //gegner.GegnerBase = gegnerBase;
                //_kampf.Kämpfer.Add(gegner, 2);
            }
        }
    }
}
