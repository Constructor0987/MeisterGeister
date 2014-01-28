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

namespace MeisterGeister.View.General
{
    public enum LiteraturAnzeigeArt
    {
        NurIcon,
        TextLang,
        TextKurz
    }


    /// <summary>
    /// Interaktionslogik für LiteraturControl.xaml
    /// </summary>
    public partial class LiteraturControl : UserControl
    {
        public LiteraturControl()
        {
            InitializeComponent();
        }

        public string Literaturangabe
        {
            get { return (string)GetValue(LiteraturangabeProperty); }
            set { SetValue(LiteraturangabeProperty, value); }
        }
        public static readonly DependencyProperty LiteraturangabeProperty = DependencyProperty.Register(
          "Literaturangabe", typeof(string), typeof(LiteraturControl));

        public LiteraturAnzeigeArt LiteraturAnzeigeArt
        {
            get { return (LiteraturAnzeigeArt)GetValue(LiteraturAnzeigeArtProperty); }
            set { SetValue(LiteraturAnzeigeArtProperty, value); }
        }
        public static readonly DependencyProperty LiteraturAnzeigeArtProperty = DependencyProperty.Register(
          "LiteraturAnzeigeArt", typeof(LiteraturAnzeigeArt), typeof(LiteraturControl), new UIPropertyMetadata(LiteraturAnzeigeArt.TextKurz));

        public string LiteraturangabeLang
        {
            get { return Model.Literatur.ReplaceAbkürzungen(Literaturangabe); }
        }

        private void LiteraturControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<Logic.Literatur.Literaturangabe> literaurList = Model.Literatur.Parse(Literaturangabe);

            _contextMenu.Items.Clear();

            foreach (var item in literaurList)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Tag = item;
                menuItem.Header = item.ToString();
                menuItem.Click += menuItem_Click;
                _contextMenu.Items.Add(menuItem);
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _contextMenu.PlacementTarget = this;
                _contextMenu.IsOpen = true;
                e.Handled = true;
            }
        }

        void menuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                object liObject = ((MenuItem)sender).Tag;
                if (liObject != null && liObject is Logic.Literatur.Literaturangabe)
                {
                    Logic.Literatur.Literaturangabe literaturangabe = (Logic.Literatur.Literaturangabe)liObject;

                    // TODO (markus): PDF öffnen
                    // Auf Basis der Literaturangabe muss das passende PDF mit Dateipfad gesucht werden.
                    // Ist kein PDF hinterlegt, sollte ein FileOpen-Dialog erscheinen oder ein Link zum Ulisses-PDF-Shop angezeigt werden.
                    // Sollten bei einer Literaturangabe mehrere Seiten angegeben sein, muss der User eine auswählen.
                    try
                    {
                        Logic.General.Pdf.OpenReader(literaturangabe, literaturangabe.Seiten.FirstOrDefault());
                    }
                    catch (Logic.Literatur.LiteraturPfadMissingException ex)
                    {
                        MessageBox.Show(ex.Message + "\nIn den Einstellungen können die Pfade zu den Dateien eingegeben werden.");
                    }
                }
            }
        }
    }
}
