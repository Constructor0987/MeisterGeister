using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using System.Windows.Forms;
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

        public static bool Confirm(string msg, string caption)
        {
            return (System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes);
        }

        public static int ConfirmYesNoCancel(string msg, string caption)
        {
            MessageBoxResult res = System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes || res == MessageBoxResult.OK)
                return 2;
            if (res == MessageBoxResult.No)
                return 1;
            return 0;
        }

        public static string ChooseFile(string title, string extension, string filename, bool saveFile)
        {
            FileDialog objDialog;
            if (saveFile)
                objDialog = new SaveFileDialog();
            else
                objDialog = new OpenFileDialog();
            objDialog.Title = title;
            //TODO: mehr extensions?
            switch (extension)
            {
                case "xml":
                    objDialog.Filter = "XML-Dateien (*.xml)|*.xml";
                    objDialog.DefaultExt = "xml";
                    break;
                default:
                    goto case "xml";
            }
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

    }
}
