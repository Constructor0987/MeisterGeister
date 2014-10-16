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
            VM = new VM.SpielerScreenControlViewModel(ViewHelper.Popup, ViewHelper.Confirm, ViewHelper.ConfirmYesNoCancel, ViewHelper.ChooseFile, ViewHelper.ChooseDirectory, ViewHelper.ShowError);

            SpielerWindow.SpielerWindowInstantiated += SpielerWindow_SpielerWindowInstantiated;
            SpielerWindow.SpielerWindowClosed += SpielerWindow_Closed;

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
                Global.CurrentSpielerScreen = value;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.Refresh();
        }


        // TODO: Methoden ins ViewModel verlagern

        private void ListBoxDirectory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listBoxDirectory.SelectedItem != null)
                VM.LoadImage(((dynamic)_listBoxDirectory.SelectedItem).Pfad);
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

        private void ImageVorschau_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(VM.SelectedImagePath);
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Fehler beim Starten eines externen Programms", "Beim Starten eines externen Programms ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
            }
        }

        void SpielerWindow_SpielerWindowInstantiated(object sender, EventArgs e)
        {
            SetPreviews();
        }

        private void SetPreviews()
        {
            double width = SpielerWindow.Instance.Width;
            double height = SpielerWindow.Instance.Height;
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

    }
}
