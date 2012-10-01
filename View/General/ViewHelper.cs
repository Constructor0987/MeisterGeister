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

    }
}
