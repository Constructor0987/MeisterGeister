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
//Eigene Usings
using MeisterGeister.Model;
using MeisterGeister.View.Windows;
using VM = MeisterGeister.ViewModel.Helden;
//Weitere Usings
using System.Windows.Forms;

namespace MeisterGeister.View.Helden.Controls
{
	/// <summary>
	/// Interaktionslogik für ViewHeldListe.xaml
	/// </summary>
    public partial class ListeView : System.Windows.Controls.UserControl
	{
		public ListeView()
		{
			this.InitializeComponent();
#if !(DEBUG)
            _buttonExportDemo.Visibility = System.Windows.Visibility.Collapsed;
#endif
            //VM an View Registrieren
            this.DataContext = new VM.ListeViewModel(Popup, Confirm, ConfirmYesNoCancel, ChooseFile, ShowError);
        }

        private void Popup(string msg)
        {
            System.Windows.MessageBox.Show(msg);
        }

        private void ShowError(string msg, Exception ex)
        {
            MsgWindow errWin = new MsgWindow("Fehler", msg, ex);
            errWin.ShowDialog();
        }

        private bool Confirm(string msg, string caption)
        {
            return (System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes);
        }

        private int ConfirmYesNoCancel(string msg, string caption)
        {
            MessageBoxResult res = System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes || res == MessageBoxResult.OK)
                return 2;
            if (res == MessageBoxResult.No)
                return 1;
            return 0;
        }

        private string ChooseFile(string title, string extension, string filename, bool saveFile)
        {
            FileDialog objDialog;
            if(saveFile)
                objDialog = new SaveFileDialog();
            else
                objDialog = new OpenFileDialog();
            objDialog.Title = title;
            //TODO: mehr extensions und diese gemeinsam nutzbar machen?
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
            objDialog.InitialDirectory = _workingPath;
            if (objDialog.ShowDialog() == DialogResult.OK)
            {
                //_workingPath =  ?.Replace(objDialog.FileName, null);
                return objDialog.FileName;
            }
            return null;
        }

        private string _workingPath = Environment.CurrentDirectory;

        private void ButtonHeldNeu_Click(object sender, RoutedEventArgs e)
        {
            _buttonHeldNeu.ContextMenu.PlacementTarget = this;
            _buttonHeldNeu.ContextMenu.IsOpen = true;
        }

        private void ListBoxHelden_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] droppedFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[];

                foreach (string file in droppedFiles)
                {
                    if (!file.EndsWith("xml"))
                    {
                        Popup(file + "\n\nFalscher Dateityp!");
                        continue;
                    }
                    try
                    {
                        string expPfad = (this.DataContext as VM.ListeViewModel).ImportHeld(file);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Beim Import ist ein Fehler aufgetreten!", ex);
                    }
                }
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHelden.SelectedItem == null)
            {
                _menuItemHeldLöschen.IsEnabled = false;
                _menuItemHeldExport.IsEnabled = false;
                _menuItemHeldKopie.IsEnabled = false;
            }
            else
            {
                _menuItemHeldLöschen.IsEnabled = true;
                _menuItemHeldImport.IsEnabled = true;
                _menuItemHeldExport.IsEnabled = true;
                _menuItemHeldKopie.IsEnabled = true;
            }
        }

        private void ListBoxHelden_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxHelden.SelectedItem = null;
        }

	}
}