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
using MeisterGeister.ViewModel.Bodenplan;

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
                Width = App.Current.MainWindow.ActualWidth * 0.9;
                Height = App.Current.MainWindow.ActualHeight * 0.9;
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
            if (_kampf != null && _gegnerView.VM.SelectedGegnerBase != null)
            {
                Model.GegnerBase gegnerBase = _gegnerView.VM.SelectedGegnerBase;

                for (int i = 0; i < _intBoxGegnerAnzahl.Value; i++)
                {
                    Model.Gegner gegner = new Model.Gegner(gegnerBase);
                    var name = gegner.Name;
                    int j = 0;
                    while (_kampf.Kämpfer.Any(k => k.Kämpfer.Name == name))
                        name = String.Format("{0} ({1})", gegner.Name, ++j);
                    gegner.Name = name;
                    Global.ContextHeld.Insert<Model.Gegner>(gegner);
                    _kampf.Kämpfer.Add(gegner, 2);

                    // zur Arena hinzufügen
                    //if (_kampf.Bodenplan != null)
                    //    ((BattlegroundViewModel)_kampf.Bodenplan.battlegroundView1.DataContext).AddEnemy(gegner);
                }

                // Arena neu zeichnen
                //if (_kampf.Bodenplan != null)
                //    _kampf.Bodenplan.BodenplanWindow.DrawArena();
            }
        }
    }
}
