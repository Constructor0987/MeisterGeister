using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.Logic.General;
using MeisterGeister.View.Kampf;
using MeisterGeister.View.Windows;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Stellt einige statische Methoden bereit, die jedes View gebrauchen kann.
    /// </summary>
    public static class ViewHelper
    {
        public static void Popup(string msg)
        {
            System.Windows.MessageBox.Show(msg);
        }

        public static void ShowError(string msg, Exception ex)
        {
            MsgWindow errWin = new MsgWindow("Fehler", msg, ex);
            errWin.ShowDialog();
        }

        public static bool Confirm(string caption, string msg)
        {
            return (System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes);
        }

        public static int ConfirmYesNoCancel(string caption, string msg)
        {
            MessageBoxResult res = System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes || res == MessageBoxResult.OK)
                return 2;
            if (res == MessageBoxResult.No)
                return 1;
            return 0;
        }

        public static string InputDialog(string caption, string msg)
        {
            string input = null;
            InputWindow inBox = new InputWindow();
            inBox.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
            inBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            inBox.Title = caption;
            inBox.Beschreibung = msg;
            inBox.ShowDialog();
            if (inBox.OK_Click)
                input = inBox.Wert;
            return input;
        }

        public static string ChooseFile(string title, string filename, bool saveFile, params string[] extensions)
        {
            FileDialog objDialog;
            if (saveFile)
                objDialog = new SaveFileDialog();
            else
                objDialog = new OpenFileDialog();
            objDialog.Title = title;
            objDialog.DefaultExt = String.Empty;
            objDialog.Filter = String.Empty;
            //TODO: mehr extension-Typen?
            foreach (string extension in extensions)
            {
                string filter = String.Empty;
                switch (extension)
                {
                    case "xml":
                        filter = "XML-Dateien (*.xml)|*.xml";
                        break;
                    case "*.*":
                    case "*":
                        filter = "Alle Dateien (*.*)|*.*";
                        break;
                    default:
                        break;
                }
                if (objDialog.Filter != String.Empty)
                    objDialog.Filter += "|";
                objDialog.Filter += filter;
            }
            if(objDialog.Filter==String.Empty)
                objDialog.Filter = "Alle Dateien (*.*)|*.*";
            
            objDialog.AddExtension = true;
            objDialog.FileName = filename;
            objDialog.InitialDirectory = Environment.CurrentDirectory;
            if (objDialog.ShowDialog() == DialogResult.OK)
            {
                //_workingPath =  ?.Replace(objDialog.FileName, null);
                return objDialog.FileName;
            }
            return null;
        }

        public static ProbenErgebnis ShowProbeDialog(Probe probe, Model.Held held)
        {
            View.Proben.ProbeDialog dlg = new View.Proben.ProbeDialog(probe, held);
            dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
            dlg.ShowDialog();
            return dlg.Ergebnis;
        }

        /// <summary>
        /// Zeigt ein Würfel Dialog-Fenster an.
        /// </summary>
        /// <param name="würfel">Würfel-Text (z.B. 2W+3, W20).</param>
        /// <param name="infoText">Info-Text, der angezeigt wird und den Würfelwurf beschreibt.</param>
        /// <returns>Würfelergebnis.</returns>
        public static int ShowWürfelDialog(string würfel, string infoText = "")
        {
            int ergebnis = 0;
            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
            {
                View.Würfeln.WürfelDialog dlg = new View.Würfeln.WürfelDialog(würfel, infoText);
                dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
                bool? result = dlg.ShowDialog();
                if (result == true)
                    ergebnis = dlg.Ergebnis;
            }
            else
                ergebnis = Logic.General.Würfel.Parse(würfel, true);
            return ergebnis;
        }

        public static int ShowWürfelDialog(WürfelEnum würfel, string infoText = "")
        {
            string w = string.Empty;
            switch (würfel)
            {
                case WürfelEnum._1W3:
                    w = "1W3";
                    break;
                case WürfelEnum._1W6:
                    w = "1W6";
                    break;
                case WürfelEnum._2W6:
                    w = "2W6";
                    break;
                case WürfelEnum._1W10:
                    w = "1W10";
                    break;
                case WürfelEnum._1W20:
                    w = "1W20";
                    break;
                default:
                    break;
            }
            return ShowWürfelDialog(w, infoText);
        }

        public static void ShowGegnerView(ViewModel.Kampf.Logic.Kampf kampf)
        {
            GegnerWindow win = new GegnerWindow(kampf);
            win.Show();
        }

        public static string SelectImage()
        {
            string path = null;
            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
            {
                View.General.SelectImageDialog dlg = new SelectImageDialog();
                dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
                bool? result = dlg.ShowDialog();
                if (result == true)
                    path = dlg.SelectedPath;
            }
            return path;
        }

        public static Image GetImageFromControl(FrameworkElement controlToRender)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)controlToRender.ActualWidth,
                (int)controlToRender.ActualHeight,
                90, 90,
                PixelFormats.Default);

            Visual vis = (Visual)controlToRender;
            rtb.Render(vis);

            Image img = new Image();
            img.Source = rtb;
            img.Stretch = Stretch.None;
            img.Measure(new System.Windows.Size(
                            (int)controlToRender.ActualWidth,
                            (int)controlToRender.ActualHeight));
            System.Windows.Size sizeImage = img.DesiredSize;
            img.Arrange(new System.Windows.Rect(new System.Windows.Point(0, 0), sizeImage));

            RenderTargetBitmap rtb2 = new RenderTargetBitmap(
                (int)rtb.Width,
                (int)rtb.Height,
                90,
                90,
                PixelFormats.Default);
            rtb2.Render(img);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb2));

            Stream ms = new MemoryStream();
            png.Save(ms);

            ms.Position = 0;

            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.StreamSource = ms;
            myBitmapImage.EndInit();

            Image imgCon = new Image();
            imgCon.Source = myBitmapImage;
            return imgCon;
        }

    }
}
