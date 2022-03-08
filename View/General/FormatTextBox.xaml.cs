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

            VM = new FormatTextBoxViewModel(RTBBox, this);
        }

        #endregion

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public FormatTextBoxViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is FormatTextBoxViewModel))
                    return null;
                return DataContext as FormatTextBoxViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        #region //Instanzmethoden

        public void SelectAll() {
            RTBNotiz.SelectAll();
        }

        private void cmbFontColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RTBNotiz != null)
            {
                string color = (cmbFontColor.SelectedItem as ComboBoxItem).Tag.ToString();

                SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                    byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)));

                if (RTBNotiz.Selection.IsEmpty)
                { // kein Text ausgewählt
                    RTBNotiz.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
                }
                else // Text ausgewählt
                    RTBNotiz.Selection.ApplyPropertyValue(Inline.ForegroundProperty, brush);
            }
            ReturnFocus();
        }

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

        public void ShowOnSpielerScreen()
        {
            btnSpielerInfo_Click(null, null);
        }

        private void btnSpielerInfoClose_Click(object sender, RoutedEventArgs e)
        {
            SpielerScreen.SpielerWindow.Hide();
        }

        private void btnQuelltext_Click(object sender, RoutedEventArgs e)
        {
            View.Windows.MsgWindow msg = new Windows.MsgWindow("Quelltext", ParseFlowDoumentToText()
                .Replace("</", Environment.NewLine + "</")
                .Replace("<Paragraph", Environment.NewLine + "<Paragraph"), false);
            msg.Show();
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

        public void ReturnFocus() {
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

        private void RTBNotiz_SelectionChanged(object sender, RoutedEventArgs e)
        {
            VM.SetCurrentFontFormat();
        }

    }

    public class FormatTextBoxViewModel : ViewModel.Base.ViewModelBase
    {
        public FormatTextBoxViewModel(RichTextBox rtb, FormatTextBox ftb)
        {
            RTBNotiz = rtb;
            FormatTB = ftb;

            Init();
        }

        private void Init()
        {
            // Schriftgrade
            _fontSizes.Add(8);
            _fontSizes.Add(9);
            _fontSizes.Add(10);
            _fontSizes.Add(11);
            _fontSizes.Add(12);
            _fontSizes.Add(14);
            _fontSizes.Add(16);
            _fontSizes.Add(18);
            _fontSizes.Add(20);
            _fontSizes.Add(22);
            _fontSizes.Add(24);
            _fontSizes.Add(26);
            _fontSizes.Add(28);
            _fontSizes.Add(36);
            _fontSizes.Add(48);
            _fontSizes.Add(72);

            LoadSystemFonts();
        }

        /// <summary>
        /// Lädt die installierten System-Fonts in die ComboBox
        /// </summary>
        void LoadSystemFonts()
        {
            var viewSource = new CollectionViewSource();
            viewSource.Source = Fonts.SystemFontFamilies;
            viewSource.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("Source", System.ComponentModel.ListSortDirection.Ascending));
            FontFamilies = viewSource.View;
        }

        public RichTextBox RTBNotiz { get; set; }
        public FormatTextBox FormatTB { get; set; }

        private List<double> _fontSizes = new List<double>();
        public List<double> FontSizes
        {
            get { return _fontSizes; }
            set
            {
                _fontSizes = value;
                OnChanged("FontSizes");
            }
        }

        private Nullable<double> _currentFontSize = 11;
        public Nullable<double> CurrentFontSize
        {
            get { return _currentFontSize; }
            set
            {
                _currentFontSize = value;
                if (value.HasValue && !FontSizes.Contains(value.Value))
                    FontSizes.Add(value.Value);
                ChangeFontSize();
                OnChanged("CurrentFontSize");
            }
        }

        private ICollectionView _fontFamilies;
        public ICollectionView FontFamilies
        {
            get { return _fontFamilies; }
            set
            {
                _fontFamilies = value;
                OnChanged("FontFamilies");
            }
        }

        private FontFamily _currentFontFamily;
        public FontFamily CurrentFontFamily
        {
            get { return _currentFontFamily; }
            set
            {
                _currentFontFamily = value;
                ChangeFontFamily();
                OnChanged("CurrentFontFamily");
            }
        }

        private bool _currentFontBold = false;
        public bool CurrentFontBold
        {
            get { return _currentFontBold; }
            set
            {
                _currentFontBold = value;
                OnChanged("CurrentFontBold");
            }
        }

        private bool _currentFontItalic = false;
        public bool CurrentFontItalic
        {
            get { return _currentFontItalic; }
            set
            {
                _currentFontItalic = value;
                OnChanged("CurrentFontItalic");
            }
        }

        private bool _currentFontUnderline = false;
        public bool CurrentFontUnderline
        {
            get { return _currentFontUnderline; }
            set
            {
                _currentFontUnderline = value;
                OnChanged("CurrentFontUnderline");
            }
        }

        private bool _menuVisible = true;
        public bool MenuVisible
        {
            get { return _menuVisible; }
            set
            {
                _menuVisible = value;
                OnChanged("MenuVisible");
            }
        }

        private ViewModel.Base.CommandBase _onMenuVisible = null;
        public ViewModel.Base.CommandBase OnMenuVisible
        {
            get
            {
                if (_onMenuVisible == null)
                    _onMenuVisible = new ViewModel.Base.CommandBase(MenuVisibleSwitch, null);
                return _onMenuVisible;
            }
        }

        private void MenuVisibleSwitch(object obj)
        {
            MenuVisible = !MenuVisible;
        }

        private void ChangeFontSize()
        {
            if (RTBNotiz != null && !_positionInfo)
            {
                if (RTBNotiz.Selection.IsEmpty)
                { // kein Text ausgewählt
                    RTBNotiz.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, CurrentFontSize);
                }
                else // Text ausgewählt
                    RTBNotiz.Selection.ApplyPropertyValue(Inline.FontSizeProperty, CurrentFontSize);
            }
            FormatTB.ReturnFocus();
        }

        private void ChangeFontFamily()
        {
            if (RTBNotiz != null && !_positionInfo)
            {
                if (RTBNotiz.Selection.IsEmpty)
                { // kein Text ausgewählt
                    RTBNotiz.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(CurrentFontFamily.ToString()));
                }
                else // Text ausgewählt
                    RTBNotiz.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, new FontFamily(CurrentFontFamily.ToString()));
            }
            FormatTB.ReturnFocus();
        }

        private bool _positionInfo = false;

        public void SetCurrentFontFormat()
        {
            if (RTBNotiz != null)
            {
                _positionInfo = true;

                TextRange tr = new TextRange(RTBNotiz.Selection.Start, RTBNotiz.Selection.End);

                object temp = tr.GetPropertyValue(Inline.FontWeightProperty);
                CurrentFontBold = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

                temp = tr.GetPropertyValue(Inline.FontStyleProperty);
                CurrentFontItalic = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

                temp = tr.GetPropertyValue(Inline.TextDecorationsProperty);
                if (temp != DependencyProperty.UnsetValue)
                {
                    TextDecorationCollection decorations = (TextDecorationCollection)temp;
                    if (decorations != null && decorations.Count > 0)
                    {
                        foreach (TextDecoration item in decorations)
                        {
                            if (item.Location == TextDecorationLocation.Underline)
                            {
                                CurrentFontUnderline = true;
                                break;
                            }
                            CurrentFontUnderline = false;
                        }
                    }
                    else
                        CurrentFontUnderline = false;
                }
                else
                    CurrentFontUnderline = false;

                temp = tr.GetPropertyValue(Inline.FontFamilyProperty);
                if (temp != DependencyProperty.UnsetValue)
                    CurrentFontFamily = (FontFamily)temp;
                else
                    CurrentFontFamily = null;

                temp = tr.GetPropertyValue(Inline.FontSizeProperty);
                if (temp != DependencyProperty.UnsetValue)
                    CurrentFontSize = (double)temp;
                else
                    CurrentFontSize = null;

                _positionInfo = false;
            }
        }
    }
}
