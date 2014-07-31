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

namespace MeisterGeister.View.SpielerScreen
{
    // TODO ??: MVVM konform umbauen

    /// <summary>
    /// Interaktionslogik für SpielerScreenControlView.xaml
    /// </summary>
    public partial class SpielerScreenControlView : System.Windows.Controls.UserControl
    {
        public SpielerScreenControlView()
        {
            InitializeComponent();

            // Letzten Bilderpfad laden
            _textBlockFilePath.Text = Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad;
            LoadImagesFromDir(_textBlockFilePath.Text);

            SpielerWindow.SpielerWindowInstantiated += SpielerWindow_SpielerWindowInstantiated;
            SpielerWindow.SpielerWindowClosed += SpielerWindow_Closed;

            if (SpielerWindow.IsInstantiated)
                SetPreviews();
        }

        private void ButtonSpielerInfoClose_Click(object sender, RoutedEventArgs e)
        {
            SpielerWindow.Hide();
        }

        private void ButtonOpenImg_Click(object sender, RoutedEventArgs e)
        {
            string pfad = ViewHelper.ChooseFile("Bild auswähllen", "", false, false, Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES);
            if (!String.IsNullOrEmpty(pfad))
                LoadImage(pfad);
        }

        private void LoadImage(string file)
        {
            _textBlockFilePath.Text = file;
            try
            {
                // Bild
                LoadImage(_textBlockFilePath.Text, _image1);
            }
            catch
            {
                System.Windows.MessageBox.Show("Laden des Bildes fehlgeschlagen!");
            }
        }

        private void ButtonOpenDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad = dlg.SelectedPath;
                _textBlockFilePath.Text = Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad;

                LoadImagesFromDir(dlg.SelectedPath);
            }
        }

        private void LoadImagesFromDir(string pfad)
        {
            if (string.IsNullOrWhiteSpace(pfad))
                return;

            string[] filesBmp = Directory.GetFiles(pfad, "*.bmp");
            string[] filesGif = Directory.GetFiles(pfad, "*.gif");
            string[] filesJpg = Directory.GetFiles(pfad, "*.jpg");
            string[] filesJpeg = Directory.GetFiles(pfad, "*.jpeg");
            string[] filesJpe = Directory.GetFiles(pfad, "*.jpe");
            string[] filesJfif = Directory.GetFiles(pfad, "*.jfif");
            string[] filesPng = Directory.GetFiles(pfad, "*.png");
            string[] filesTif = Directory.GetFiles(pfad, "*.tif");
            string[] filesTiff = Directory.GetFiles(pfad, "*.tiff");

            List<dynamic> fileList = new List<dynamic>();
            AddImages(fileList, filesBmp);
            AddImages(fileList, filesBmp);
            AddImages(fileList, filesGif);
            AddImages(fileList, filesJpg);
            AddImages(fileList, filesJpeg);
            AddImages(fileList, filesJpe);
            AddImages(fileList, filesJfif);
            AddImages(fileList, filesPng);
            AddImages(fileList, filesTif);
            AddImages(fileList, filesTiff);

            _listBoxDirectory.ItemsSource = fileList.OrderBy(img => img.Name);
        }

        private void AddImages(List<dynamic> fileList, string[] files)
        {
            foreach (string file in files)
                fileList.Add(new { Pfad = file, Name = Path.GetFileNameWithoutExtension(file) });
        }

        private void ButtonBildZeigen_Click(object sender, RoutedEventArgs e)
        {
            SpielerWindow.SetImage(_textBlockFilePath.Text, (_checkBoxMax.IsChecked == true) ? Stretch.Uniform : Stretch.None );
        }

        private void LoadImage(string path, Image img)
        {
            System.Windows.Threading.DispatcherOperation op =
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate()
            {
                try
                {
                    BitmapImage bmi = new BitmapImage();
                    bmi.BeginInit();
                    bmi.CacheOption = BitmapCacheOption.OnLoad;
                    bmi.UriSource = new Uri(path, UriKind.Relative);
                    bmi.EndInit();

                    bmi.Freeze();		// freeze image source, used to move it across the thread
                    img.Source = bmi;
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Bild konnte nicht geladn werden:\n" + path, "Bild laden");
                }
            });
        }

        private void ListBoxDirectory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listBoxDirectory.SelectedItem != null)
                LoadImage(((dynamic)_listBoxDirectory.SelectedItem).Pfad);
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
                System.Diagnostics.Process.Start(_textBlockFilePath.Text);
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

        private void ButtonKampf_Click(object sender, RoutedEventArgs e)
        {
            SpielerWindow.SetKampfInfoView();
        }

        private void ButtonSpielerInfo_Click(object sender, RoutedEventArgs e)
        {
            SpielerWindow.ReOpen();
        }

        private void ButtonBodenplan_Click(object sender, RoutedEventArgs e)
        {
            SpielerWindow.SetBodenplanView();
        }

        private void ButtonVorschau_Click(object sender, RoutedEventArgs e)
        {
            SpielerInfoPreviewWindow.Show();
        }

    }
}
