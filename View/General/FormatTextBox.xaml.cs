using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using System.Windows.Markup;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für FormatTextBox.xaml
    /// </summary>
    public partial class FormatTextBox : UserControl {

        #region //Eigenschaften

        public TextSelection Selection {
            get {
                return RTBNotiz.Selection;
            }
        }
        public FlowDocument Document {
            get {
                return RTBNotiz.Document;
            }
            set { RTBNotiz.Document = value; }
        }
        public RichTextBox RTBBox {
            get { return RTBNotiz; }
        }

        #endregion

        #region //Konstruktor

        public FormatTextBox() {
            this.InitializeComponent();

            LoadSystemFonts();
        }

        #endregion

        #region //Instanzmethoden

        public void SelectAll() {
            RTBNotiz.SelectAll();
        }

        /// <summary>
        /// Lädt die installierten System-Fonts in die ComboBox
        /// </summary>
        void LoadSystemFonts()
        {
            var viewSource = new CollectionViewSource();
            viewSource.Source = Fonts.SystemFontFamilies;
            viewSource.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Source", System.ComponentModel.ListSortDirection.Ascending));
            cbFontStyle.ItemsSource = viewSource.View;
        }

        #region Font, Color & Size

        private void cbFontStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != null)
                RTBNotiz.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily((e.OriginalSource as ComboBox).SelectedValue.ToString()));
            ReturnFocus();
        }

        private void cmbFontSizes_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (RTBNotiz != null && RTBNotiz.Selection.Text.Length > 0) {
                RTBNotiz.Selection.ApplyPropertyValue(Run.FontSizeProperty, double.Parse((cmbFontSizes.SelectedItem as ComboBoxItem).Tag.ToString()));
            }
            ReturnFocus();
        }

        private void cmbFontColor_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (RTBNotiz != null && RTBNotiz.Selection.Text.Length > 0) {
                string color = (cmbFontColor.SelectedItem as ComboBoxItem).Tag.ToString();

                SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                    byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)));
                RTBNotiz.Selection.ApplyPropertyValue(Run.ForegroundProperty, brush);
            }
            ReturnFocus();
        }

        #endregion

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text-Dateien|*.txt|XAML-Dateien|*.xaml|RTF-Dateien|*.rtf";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string format = null;
                ;
                switch (dialog.FilterIndex)
                {
                    case 1:
                        format = DataFormats.Text;
                        break;
                    case 2:
                        format = DataFormats.Xaml;
                        break;
                    case 3:
                        format = DataFormats.Rtf;
                        break;
                }
                FlowDocument document = RTBNotiz.Document;
                TextRange range = new TextRange(document.ContentStart, document.ContentEnd);
                FileStream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                range.Save(stream, format);
                stream.Close();
            }
        }

        // Zeigt den RTBox-Text in Spieler-Fenster
        private void btnSpielerInfo_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)_checkBoxShowSelectedText.IsChecked == false)
                RTBNotiz.SelectAll();

            RTBNotiz.Copy();
            SpielerScreen.SpielerWindow.SetTextFromClipboard();
        }

        private void btnSpielerInfoClose_Click(object sender, RoutedEventArgs e)
        {
            SpielerScreen.SpielerWindow.Hide();
        }

        public void ParseTextToFlowDoument(string text)
        {
            try
            {
                // Umwandlung versuchen
                if (text.StartsWith("<FlowDocument"))
                    Document = XamlReader.Parse(text) as FlowDocument;
                else
                    TextToFlowDocument(text);
            }
            catch (XamlParseException)
            {
                TextToFlowDocument(text);
            }
        }

        private void TextToFlowDocument(string text)
        {
            // Text entspricht keinem gültigen FlowDoument-Format.
            // Neues Doument anlegen und Text als Paragraphen einfügen.
            FlowDocument tmpDoc = new FlowDocument(new Paragraph(new Run(text)));
            Document = tmpDoc;
        }

        public string ParseFlowDoumentToText()
        {
            try
            {
                // FlowDocument in String umwandeln
                StringWriter wr = new StringWriter();
                XamlWriter.Save(Document, wr);
                return wr.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ReturnFocus() {
            if (RTBNotiz != null) {
                RTBNotiz.Focus();
            }
        }

        #endregion

        #region //Events

        public event EventHandler TextChanged;

        private void RTBNotiz_TextChanged(object sender, TextChangedEventArgs e) {
            if (TextChanged != null)
                TextChanged(this, new EventArgs());
        }

        #endregion

    }
}