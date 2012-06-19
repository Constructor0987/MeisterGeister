using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MeisterGeister.View.Arena
{
    public class CreatureContextMenu : ContextMenu {

        private static int ICON_SIZE = 16;

        private CreatureCanvas _cc;

        public CreatureContextMenu(CreatureCanvas cc) : base(){
            _cc = cc;
           
            MenuItem mi;
            mi = createMenuItem("Kreatur löschen", "entf_01.png");
            mi.Click += onRemove;
            Items.Add(mi);

            mi = createMenuItem("Bewegungsradius ein/ausblenden", "kreise.png");
            mi.Click += onCircles;
            Items.Add(mi);
                        
        }
        

        private MenuItem createMenuItem(String label, String iconPath){
            MenuItem mi = new MenuItem();
            mi.Header = label;
            

            Image icon = new Image();
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@ArenaWindow.ICON_DIR + iconPath, UriKind.Relative);
            myBitmapImage.DecodePixelWidth = ICON_SIZE;
            myBitmapImage.DecodePixelHeight = ICON_SIZE;
            myBitmapImage.EndInit();
            icon.Source = myBitmapImage;
            mi.Icon = icon;           

            return mi;
        }

        private void onRemove(object sender, RoutedEventArgs args) {
            //if (MessageBox.Show("Kreatur wirklich löschen?", "Kreatur löschen", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
            _cc.ArenaViewer.RemoveCreature(_cc);
                

            //}
        }

        private void onCircles(object sender, RoutedEventArgs args) {
            _cc.ArenaViewer.SwitchShowCircles(_cc.Creature);
            _cc.ArenaViewer.DrawArena();
        }        
    }
}
