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
        }

        private void ButtonSpielerInfoControl_Click(object sender, RoutedEventArgs e)
        {
            MainView.ShowSpielerFenster();
        }

        private void ButtonSpielerInfoClose_Click(object sender, RoutedEventArgs e)
        {
            MainView.CloseSpielerFenster();
        }

        private void ButtonOpenImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Bild (*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF)|*.BMP;*.GIF;*.JPG;*.JPEG;*.JPE;*.JFIF;*.PNG;*.TIF;*.TIFF";

            if (dlg.ShowDialog() == DialogResult.OK)
                LoadImage(dlg.FileName);
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

        private string _openedDir = string.Empty;

        private void ButtonOpenDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = _openedDir;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _openedDir = dlg.SelectedPath;
                _textBlockFilePath.Text = _openedDir;

                string[] filesBmp = Directory.GetFiles(dlg.SelectedPath, "*.bmp");
                string[] filesGif = Directory.GetFiles(dlg.SelectedPath, "*.gif");
                string[] filesJpg = Directory.GetFiles(dlg.SelectedPath, "*.jpg");
                string[] filesJpeg = Directory.GetFiles(dlg.SelectedPath, "*.jpeg");
                string[] filesJpe = Directory.GetFiles(dlg.SelectedPath, "*.jpe");
                string[] filesJfif = Directory.GetFiles(dlg.SelectedPath, "*.jfif");
                string[] filesPng = Directory.GetFiles(dlg.SelectedPath, "*.png");
                string[] filesTif = Directory.GetFiles(dlg.SelectedPath, "*.tif");
                string[] filesTiff = Directory.GetFiles(dlg.SelectedPath, "*.tiff");

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
        }

        private void AddImages(List<dynamic> fileList, string[] files)
        {
            foreach (string file in files)
                fileList.Add(new { Pfad = file, Name = Path.GetFileNameWithoutExtension(file) });
        }

        private void ButtonBildZeigen_Click(object sender, RoutedEventArgs e)
        {
            MainView.ShowSpielerInfoBild(_textBlockFilePath.Text, (_checkBoxMax.IsChecked == true) ? Stretch.Uniform : Stretch.None );
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

    }
}
