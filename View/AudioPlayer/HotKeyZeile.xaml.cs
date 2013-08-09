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
using System.Windows.Shapes;
using System.IO;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für HotKeyZeile.xaml
    /// </summary>
    public partial class HotKeyZeile : ListBoxItem
    {
        public string lastPath = "C:\\";
        public bool aktiv = false;
        public Char taste = new Char();

        public HotKeyZeile()
        {
            InitializeComponent();
        }

        private void btnClearHotkey_Click(object sender, RoutedEventArgs e)
        {
            tboxGeräuschname.Text = "";
            lbDatei.Content = "";
            lbDatei.Visibility = Visibility.Collapsed;
            brdGeräuschname.Visibility = Visibility.Collapsed;
            btnClearHotkey.Visibility = Visibility.Collapsed;
            aktiv = false;
        }

        private void btnEditHotkey_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Multiselect = false;
            dlg.DefaultExt = ".mp3;.wav;.wma;.ogg"; // Extensionen
            dlg.Filter = "Alle Musikdateien |*.mp3;*.wav;*.wma;*.ogg|MP3-Dateien|*.mp3|Wave-Dateien|*.wav|Windows Media Player-Dateien|*.wma|OGG-Dateien|*.ogg"; // Filter Dateien pro extension
            dlg.InitialDirectory = Directory.Exists(lastPath) ? lastPath : "C:\\";

            // Zeige File-Öffnen Dialog
            Nullable<bool> result = dlg.ShowDialog();

            // Öffnen bestätigt
            if (result == true)
            {
                // Öffne das Dokument
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    if (dlg.FileNames.Length != 0)
                    {
                        btnClearHotkey.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                        lbDatei.Content = dlg.FileName;
                        tboxGeräuschname.Text = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                        lbDatei.Visibility = Visibility.Visible;
                        brdGeräuschname.Visibility = Visibility.Visible;
                        btnClearHotkey.Visibility = Visibility.Visible;
                        aktiv = true;
                    }                        
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
            }
        }        
    }
}
