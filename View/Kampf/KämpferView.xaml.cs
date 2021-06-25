using MeisterGeister.View.AudioPlayer;
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

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für KämpferView.xaml
    /// </summary>
    public partial class KämpferView : UserControl
    {
        public KämpferView()
        {
            InitializeComponent();
        }


        private void AudioSpeedButtonVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //_scrollViewerKämpferDetails.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            if (e.Delta > 1)
            {
                ((Slider)sender).Value += ((((Slider)sender).Value < 98) ? 3 : ((100 - ((Slider)sender).Value)));
            }
            else
            { ((Slider)sender).Value += ((((Slider)sender).Value > 2) ? -3 : 0); }
            //_scrollViewerKämpferDetails.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
        }

        private void btnAudioSpeedButtonWesenZuweisen_Click(object sender, RoutedEventArgs e)
        {
            MeisterGeister.Model.Held h = (Global.CurrentKampf.SelectedKämpfer.Kämpfer is MeisterGeister.Model.Held) ?
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer as MeisterGeister.Model.Held) : null;
            MeisterGeister.Model.GegnerBase g = (Global.CurrentKampf.SelectedKämpfer.Kämpfer is MeisterGeister.Model.GegnerBase) ?
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer as MeisterGeister.Model.GegnerBase) :
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer is MeisterGeister.Model.Gegner) ?
                (Global.CurrentKampf.SelectedKämpfer.Kämpfer as MeisterGeister.Model.Gegner).GegnerBase : null;
            if (h != null)
            {
                PlaylistWesenAuswahlView wesenAuswahlView = new PlaylistWesenAuswahlView(h);
                wesenAuswahlView.ShowDialog();
            }
            else
                if (g != null)
                {
                    PlaylistWesenAuswahlView wesenAuswahlView = new PlaylistWesenAuswahlView(g);
                    wesenAuswahlView.ShowDialog();
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Kampf.Logic.KämpferInfo ki = Global.CurrentKampf.SelectedKämpfer;
            Model.Gegner g = ki.Kämpfer as MeisterGeister.Model.Gegner;
            Model.GegnerBase SelectedGegnerBase = (Global.CurrentKampf.SelectedKämpfer.Kämpfer as MeisterGeister.Model.Gegner).GegnerBase;
            var ausr = Global.ContextInventar.WaffeListe[10].Ausrüstung;

            if (ausr == null)
                return;
            if (ausr.Waffe != null)
                AddAngriff(SelectedGegnerBase, Model.GegnerBase_Angriff.FromWaffe(ausr.Waffe, SelectedGegnerBase));
            if (ausr.Fernkampfwaffe != null)
                AddAngriff(SelectedGegnerBase, Model.GegnerBase_Angriff.FromFernkampfwaffe(ausr.Fernkampfwaffe, SelectedGegnerBase));
        }

        private Model.GegnerBase_Angriff AddAngriff(Model.GegnerBase SelectedGegnerBase, Model.GegnerBase_Angriff ga)
        {
            if (SelectedGegnerBase == null || ga == null)
                return null;
            var g = SelectedGegnerBase;
            string name = ga.Name; int i = 0;
            while (g.GegnerBase_Angriff.Any(gba => gba.Name == name))
                name = String.Format("{0} ({1})", ga.Name, ++i);
            ga.Name = name;
            ga.GegnerBaseGUID = g.GegnerBaseGUID;
            ga.GegnerBase = g;
            SelectedGegnerBase.GegnerBase_Angriff.Add(ga);
            //SaveGegner();
            //OnChanged("AngriffListe");
            return ga;
        }

        private void ZeigeGegnername_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Kampf.Logic.KämpferInfo ki = Global.CurrentKampf.SelectedKämpfer;
            Model.Gegner g = ki.Kämpfer as MeisterGeister.Model.Gegner;
            g.KämpferTempName = g.Name;
        }
    }
}
