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
                menuItem.Header = new TextBlock() { Text = item.ToString(), VerticalAlignment = System.Windows.VerticalAlignment.Center };
                menuItem.Click += menuItem_Click;
                _contextMenu.Items.Add(menuItem);
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                e.Handled = true;

                // bei nur einer Literaturangabe diese sofort öffnen, ohne ContextMenu einzublenden
                if (literaurList.Count == 1)
                {
                    OpenPdf(literaurList[0]);
                }
                else
                {
                    _contextMenu.PlacementTarget = this;
                    _contextMenu.IsOpen = true;
                }
            }
        }

        private void OpenPdf(Logic.Literatur.Literaturangabe literaturangabe)
        {
            // TODO (markus): PDF öffnen - offene Punkte...
            // Ist kein PDF hinterlegt, sollte ein Link zum Ulisses-PDF-Shop angezeigt werden.
            // Sollten bei einer Literaturangabe mehrere Seiten angegeben sein, muss der User eine auswählen.
            try
            {
                Model.Literatur li = Model.Literatur.GetByAbkürzung(literaturangabe.Kürzel);
                if (string.IsNullOrEmpty(li.Pfad))
                {
                    if (ViewHelper.ConfirmYesNoCancel("Kein PDF hinterlegt",
                        string.Format("Zu '{0}' wurde noch kein PDF hinterlegt. Soll nun ein PDF ausgewählt werden, um die Literaturangabe aufrufen zu können?", li.Name)) == 2)
                    {
                        string file = ViewHelper.ChooseFile(string.Format("Zu '{0}' ein PDF auswählen", li.Name), string.Format("{0}.pdf", li.Name), false, "pdf");
                        if (string.IsNullOrEmpty(file))
                            return;
                        li.Pfad = file;
                    }
                    else
                        return;
                }
                Logic.General.Pdf.OpenReader(literaturangabe, literaturangabe.Seiten.FirstOrDefault());
            }
            catch (Logic.Literatur.LiteraturPfadMissingException ex)
            {
                MessageBox.Show(ex.Message + "\nIn den Einstellungen können die Pfade zu den Dateien eingegeben werden.");
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message + "\nIn den Einstellungen kann ein anderer PDF Reader eingestellt werden.");
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
                    OpenPdf(literaturangabe);
                }
            }
        }
    }
}
