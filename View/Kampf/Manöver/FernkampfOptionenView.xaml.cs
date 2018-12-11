using MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.ViewModel.Kampf.Logic.Manöver;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeisterGeister.View.Kampf.Manöver
{
    /// <summary>
    /// Interaktionslogik für FernkampfOptionenView.xaml
    /// </summary>
    public partial class FernkampfOptionenView : UserControl
    {
        public FernkampfOptionenView()
        {
            InitializeComponent();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
                ckbxZielgröße.SelectedIndex -= 1;
            else
                ckbxZielgröße.SelectedIndex += 1;
        }

        private void currentPosition_Loaded(object sender, RoutedEventArgs e)
        {
            //Beim Anzeigen des Fernkampf-Fensters die momentane Position mit dem Bodenplan abgleichen    
            IKämpfer bodenplanKämpfer = (Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Where(t => t is IKämpfer)
                    .FirstOrDefault(t => ((IKämpfer)t) == Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.Kämpfer) as IKämpfer);
        
            if (bodenplanKämpfer != null &&
                ((ManöverModifikator<Position, IFernkampfwaffe>)
                (Global.CurrentKampf.SelectedManöver.Manöver as FernkampfManöver).Mods["PositionSelbst"]).Value != bodenplanKämpfer.Position)

                ((ManöverModifikator<Position, IFernkampfwaffe>)
                    (Global.CurrentKampf.SelectedManöver.Manöver as FernkampfManöver).Mods["PositionSelbst"]).Value = bodenplanKämpfer.Position;

            
        }
    }
}
