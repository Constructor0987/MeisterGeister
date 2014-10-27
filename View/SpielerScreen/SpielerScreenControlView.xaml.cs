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
using System.Windows.Navigation;
using System.Windows.Forms;
// Eigene Usings
using MeisterGeister.View;
using System.IO;
using MeisterGeister.View.Windows;
using MeisterGeister.View.General;
using VM = MeisterGeister.ViewModel.SpielerScreen;

namespace MeisterGeister.View.SpielerScreen
{
    /// <summary>
    /// Interaktionslogik für SpielerScreenControlView.xaml
    /// </summary>
    public partial class SpielerScreenControlView : System.Windows.Controls.UserControl
    {
        public SpielerScreenControlView()
        {
            InitializeComponent();
            if (Global.CurrentSpielerScreen == null)
            {
                VM = new VM.SpielerScreenControlViewModel(ViewHelper.Popup, ViewHelper.Confirm, ViewHelper.ConfirmYesNoCancel, ViewHelper.ChooseFile, ViewHelper.ChooseDirectory, ViewHelper.ShowError);
                Global.CurrentSpielerScreen = VM;
            }
            else
                VM = Global.CurrentSpielerScreen;

            SpielerWindow.SpielerWindowInstantiated += SpielerWindow_SpielerWindowInstantiated;
            SpielerWindow.SpielerWindowClosed += SpielerWindow_Closed;

            // Text-Feld setzen
            if (!String.IsNullOrEmpty(VM.TextToShow))
                _RTBNotiz.ParseTextToFlowDoument(VM.TextToShow);

            if (SpielerWindow.IsInstantiated)
                SetPreviews();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.SpielerScreenControlViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.SpielerScreenControlViewModel))
                    return null;
                return DataContext as VM.SpielerScreenControlViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.Refresh();

            
        }

        private void ListBoxDirectory_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string key = e.Key.ToString();
            key = key.Replace("NumPad", string.Empty)
                .Replace("OemQuotes", "Ä").Replace("Oem1", "Ü").Replace("Oem3", "Ö");
            if (key.Length == 2 && key.StartsWith("D"))
                key = key.Replace("D", string.Empty);
            foreach (dynamic item in _listBoxDirectory.Items)
            {
                if (item.Name.StartsWith(key))
                {
                    _listBoxDirectory.SelectedItem = item;
                    _listBoxDirectory.ScrollIntoView(item);
                    break;
                }
            }
        }

        void SpielerWindow_SpielerWindowInstantiated(object sender, EventArgs e)
        {
            SetPreviews();
        }

        private void SetPreviews()
        {
            double width = VM.SpielerScreen.Bounds.Width;
            double height = VM.SpielerScreen.Bounds.Height;
            _spielerWindowVorschau.Height = _spielerWindowVorschau.Width / width * height;
            _spielerWindowVorschau.Fill = SpielerWindow.VisualBrush;
            _spielerWindowVorschau.ToolTip = new System.Windows.Shapes.Rectangle() { Width = 400, Height = 400.0 / width * height, Fill = SpielerWindow.VisualBrush };

            if (SpielerInfoPreviewWindow.IsInstantiated)
                SpielerInfoPreviewWindow.Instance.SetVisualBrush();
        }

        void SpielerWindow_Closed(object sender, EventArgs e)
        {
            _spielerWindowVorschau.Fill = null;
            _spielerWindowVorschau.ToolTip = null;
            if (SpielerInfoPreviewWindow.IsInstantiated)
                SpielerInfoPreviewWindow.Instance.SetVisualBrush();
        }

        private void ButtonVorschau_Click(object sender, RoutedEventArgs e)
        {
            SpielerInfoPreviewWindow.Show();
        }

        private void RTBNotiz_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.TextToShow = _RTBNotiz.ParseFlowDoumentToText();
        }

        private void Bilder_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] droppedFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[];
                string file = droppedFiles.FirstOrDefault();

                FileAttributes attr = File.GetAttributes(file);

                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    VM.DirectoryPath = file;
                    Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad = file;
                }
                else
                {
                    FileInfo fi = new FileInfo(file);
                    if (!Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES.Contains(fi.Extension.Replace(".", string.Empty)))
                        ViewHelper.Popup(file + "\n\nFalscher Dateityp!");
                    else
                        VM.SelectedImagePath = file;
                }
            }
        }

        private void _listBoxDirectory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (VM.SelectedImageObject != null)
                VM.ShowImage();
        }
    }
}
