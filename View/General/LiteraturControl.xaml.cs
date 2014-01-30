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

            _selectedSeitenangabe = null;
            _selectedLiteraturangabe = null;
            _contextMenu.ItemsSource = literaurList;

            if (literaurList == null) //parserfehler
                return;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                e.Handled = true;

                // bei nur einer eindeutigen Literaturangabe diese sofort öffnen, ohne ContextMenu einzublenden
                if (literaurList.Count == 1 && literaurList[0].Seiten.Count == 1)
                {
                    _selectedLiteraturangabe = literaurList[0];
                    _selectedSeitenangabe = literaurList[0].Seiten.FirstOrDefault();
                    OpenPdf();
                }
                else
                {
                    _contextMenu.PlacementTarget = this;
                    _contextMenu.IsOpen = true;
                }
            }
        }

        private void OpenPdf()
        {
            // TODO (markus): PDF öffnen - offene Punkte...
            // Ist kein PDF hinterlegt, sollte ein Link zum Ulisses-PDF-Shop angezeigt werden.
            // Errata müssen in Logic.General.Pdf.OpenReader() abgefragt werden.
            try
            {
                if (_selectedLiteraturangabe == null)
                    return;

                Model.Literatur li = Model.Literatur.GetByAbkürzung(_selectedLiteraturangabe.Kürzel);
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
                if (_selectedSeitenangabe == null)
                    Logic.General.Pdf.OpenReader(_selectedLiteraturangabe, _selectedLiteraturangabe.Seiten.FirstOrDefault());
                else
                    Logic.General.Pdf.OpenReader(_selectedLiteraturangabe, _selectedSeitenangabe);
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

        void MenuItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is StackPanel)
            {
                object liObject = ((StackPanel)sender).Tag;
                if (liObject != null && liObject is Logic.Literatur.Literaturangabe)
                {
                    Logic.Literatur.Literaturangabe literaturangabe = (Logic.Literatur.Literaturangabe)liObject;
                    _selectedLiteraturangabe = literaturangabe;

                    if (literaturangabe.Seiten.Count == 1)
                    {
                        OpenPdf();
                        e.Handled = true;
                    } // bei mehreren Seiten bubbelt das Event weiter zum Seiten-Button
                }
            }
        }

        private Logic.Literatur.Literaturangabe _selectedLiteraturangabe;
        private Logic.Literatur.Seitenangabe _selectedSeitenangabe;

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button)
            {
                object liObject = ((Button)sender).Tag;
                if (liObject != null && liObject is Logic.Literatur.Seitenangabe)
                {
                    Logic.Literatur.Seitenangabe seitenangabe = (Logic.Literatur.Seitenangabe)liObject;
                    _selectedSeitenangabe = seitenangabe;

                    OpenPdf();
                }
            }
        }
    }
}
