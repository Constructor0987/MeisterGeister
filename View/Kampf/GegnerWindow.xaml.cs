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

            _gegnerView.ListBox_DoubleClicked += ListBox_DoubleClicked;

            _kampf = kampf;

            if (App.Current.MainWindow != null)
            {
                Owner = App.Current.MainWindow;
                Width = App.Current.MainWindow.ActualWidth * 0.7;
                Height = App.Current.MainWindow.ActualHeight * 0.8;
            }
        }

        private void ListBox_DoubleClicked(object sender, EventArgs e)
        {
            AddGegnerToKampf();
        }

        private ViewModel.Kampf.Logic.Kampf _kampf;

        private void ButtonAddToKampf_Click(object sender, RoutedEventArgs e)
        {
            AddGegnerToKampf();
        }

        private void AddGegnerToKampf()
        {
            if (_kampf != null)
            {
                Model.GegnerBase gegnerBase = _gegnerView.VM.SelectedGegnerBase;

                for (int i = 0; i < _intBoxGegnerAnzahl.Value; i++)
                {
                    Model.Gegner gegner = new Model.Gegner(gegnerBase);
                    Global.ContextHeld.Insert<Model.Gegner>(gegner);
                    _kampf.Kämpfer.Add(gegner, 2);
                }
            }
        }
    }
}
