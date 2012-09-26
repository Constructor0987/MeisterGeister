using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;
using VM = MeisterGeister.ViewModel.Proben;

namespace MeisterGeister.View.Proben
{
    /// <summary>
    /// Interaktionslogik für ProbenView.xaml
    /// </summary>
    public partial class ProbenView : UserControl
    {
        public ProbenView()
        {
            InitializeComponent();

            DataContext = new VM.ProbenViewModel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // einfache Probe
            Probe p1 = new Probe();
            ProbenErgebnis e1 = p1.Würfeln();

            // Talent-Probe
            Model.Talent p2 = new Model.Talent();
            ProbenErgebnis e2 = p2.Würfeln();

            Model.Talent p3 = Global.ContextTalent.TalentListe.Where(t => t.Name == "Sinnenschärfe").FirstOrDefault();
            ProbenErgebnis e3 = p3.Würfeln();

            Model.Held_Talent p4 = new Model.Held_Talent();
            ProbenErgebnis e4 = p4.Würfeln();

            Model.Held_Talent p5 = Global.ContextHeld.HeldenGruppeListe.FirstOrDefault().Held_Talent.Where(t => t.Talent == p3).FirstOrDefault();
            ProbenErgebnis e5 = p5.Würfeln();
        }

    }
}
