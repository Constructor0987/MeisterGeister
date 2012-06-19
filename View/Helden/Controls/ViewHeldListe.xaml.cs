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

using MeisterGeister.Model;
using MeisterGeister.View.Windows;
using System.Windows.Forms;
using VM = MeisterGeister.ViewModel.Helden.Controls;

namespace MeisterGeister.View.Helden.Controls
{
	/// <summary>
	/// Interaktionslogik für ViewHeldListe.xaml
	/// </summary>
    public partial class ViewHeldListe : System.Windows.Controls.UserControl
	{
		public ViewHeldListe()
		{
			this.InitializeComponent();
#if !(DEBUG)
            _buttonExportDemo.Visibility = System.Windows.Visibility.Collapsed;
#endif
            //VM an View Registrieren
            this.DataContext = new VM.ViewModelHeldListe();
        }

        private string _workingPath = Environment.CurrentDirectory;

        private void ButtonHeldNeu_Click(object sender, RoutedEventArgs e)
        {
            _buttonHeldNeu.ContextMenu.IsOpen = true;
        }

        private void HeldNeu()
        {
            //TODO JT: Held.Neu();
        }

        private void _buttonExportDemo_Click(object sender, RoutedEventArgs e)
        {
            //TODO JT: Held.GeneriereDemoHelden();
        }

        private void _listBoxHelden_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is System.Windows.Controls.ListBox && e.Key == Key.Delete)
            {
                var liBo = (System.Windows.Controls.ListBox)sender;
                if (liBo.SelectedItem != null)
                    HeldDelete();
            }
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
                        System.Windows.MessageBox.Show(file + "\n\nFalscher Dateityp!");
                        continue;
                    }
                    try
                    {
                        //TODO JT: string expPfad = Held.ImportHeldNeu(file);
                    }
                    catch (Exception ex)
                    {
                        MsgWindow errWin = new MsgWindow("Fehler beim Import", "Beim Import ist ein Fehler aufgetreten!", ex);
                        errWin.ShowDialog();
                    }
                }
            }
        }

        private void ListBoxHelden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
            {
                App.SaveAll();
                //TODO JT: 
                //Global.SelectedHeldGUID = SelectedHeld.Id;
                //Global.SelectedHeld.PropertyChanged += SelectedHeld_PropertyChanged;
                //RefreshHeld();
            }
        }

        private void ListBoxHelden_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_listBoxHelden.SelectedItem != null)
            {
                switch (e.Key)
                {
                    // Aktion löschen
                    case Key.Delete:
                        HeldDelete();
                        break;
                    default:
                        break;
                }
            }
        }

        private void HeldDelete()
        {
            Held h = (this.DataContext as VM.ViewModelHeldListe).SelectedHeld;
            if (System.Windows.MessageBox.Show(string.Format("Sind Sie sicher, dass Sie den Helden '{0}' löschen möchten?", h.Name), "Held löschen",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //TODO JT: h.Delete();
                _listBoxHelden.SelectedIndex = -1;
            }
        }

        private void MenuItemHeldImportDemo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _listBoxHelden.UnselectAll();
                _listBoxHelden.SelectionChanged -= ListBoxHelden_SelectionChanged;
                //TODO JT: Held.LadeDemoHelden();
                _listBoxHelden.SelectionChanged += ListBoxHelden_SelectionChanged;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Windows.MessageBox.Show(ex.FileName + "\n" + ex.Message, "Demo-Helden laden", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (System.Xml.XmlException ex)
            {
                System.Windows.MessageBox.Show("Die Demo-Helden Datei ist beschädigt." + "\n\n" + ex.Message, "Demo-Helden laden", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItemHeldLöschen_Click(object sender, RoutedEventArgs e)
        {
            HeldDelete();
        }

        private void MenuItemHeldNeu_Click(object sender, RoutedEventArgs e)
        {
            HeldNeu();
        }

        private void MenuItemHeldExport_Click(object sender, RoutedEventArgs e)
        {
            //TODO JT: 
            //var objDialog = new SaveFileDialog();
            //objDialog.Title = "Held exportieren";
            //objDialog.Filter = "XML-Dateien (*.xml)|*.xml";
            //objDialog.DefaultExt = "xml";
            //objDialog.AddExtension = true;
            //objDialog.FileName = SelectedHeld.Name;
            //objDialog.InitialDirectory = _workingPath;
            //DialogResult objResult = objDialog.ShowDialog();
            //if (objResult == DialogResult.OK)
            //{
            //    try
            //    {
            //        string expPfad = SelectedHeld.ExportHeld(objDialog.FileName);
            //        _workingPath = expPfad.Replace(objDialog.FileName, null);

            //        System.Windows.MessageBox.Show("Der Held wurde in \'" + expPfad + "\' gespeichert.");
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgWindow errWin = new MsgWindow("Fehler beim Export", "Beim Export des Helden ist ein Fehler aufgetreten!", ex);
            //        errWin.ShowDialog();
            //    }
            //}
        }

        private void MenuItemHeldImport_Click(object sender, RoutedEventArgs e)
        {
            //TODO JT: 
            //if (System.Windows.MessageBox.Show("Beim Importieren wird der ausgewählte Held überschrieben. Soll der Vorgang fortgesetzt werden?", "Held importieren",
            //    MessageBoxButton.YesNo) == MessageBoxResult.No)
            //    return;
            //var objDialog = new OpenFileDialog();
            //objDialog.Title = "Held importieren";
            //objDialog.Filter = "MeisterGeister und Helden-Software Dateien (*.xml)|*.xml";
            //objDialog.DefaultExt = "xml";
            //objDialog.AddExtension = true;
            //objDialog.FileName = SelectedHeld.Name;
            //objDialog.InitialDirectory = _workingPath;
            //DialogResult objResult = objDialog.ShowDialog();
            //if (objResult == DialogResult.OK)
            //{
            //    try
            //    {
            //        string expPfad = SelectedHeld.ImportHeld(objDialog.FileName);
            //        _workingPath = expPfad.Replace(objDialog.SafeFileName, null);

            //        System.Windows.MessageBox.Show("Der Held wurde aus \'" + expPfad + "\' importiert.");
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgWindow errWin = new MsgWindow("Fehler beim Import", "Beim Import ist ein Fehler aufgetreten!", ex);
            //        errWin.ShowDialog();
            //    }
            //}
        }

        private void MenuItemHeldImportNeu_Click(object sender, RoutedEventArgs e)
        {
            //TODO JT: 
            //var objDialog = new OpenFileDialog();
            //objDialog.Title = "Neuen Helden importieren";
            //objDialog.Filter = "MeisterGeister und Helden-Software Dateien (*.xml)|*.xml";
            //objDialog.DefaultExt = "xml";
            //objDialog.AddExtension = true;
            //objDialog.FileName = SelectedHeld.Name;
            //objDialog.InitialDirectory = _workingPath;
            //DialogResult objResult = objDialog.ShowDialog();
            //if (objResult == DialogResult.OK)
            //{
            //    try
            //    {
            //        string expPfad = Held.ImportHeldNeu(objDialog.FileName);
            //        _workingPath = expPfad.Replace(objDialog.SafeFileName, null);

            //        System.Windows.MessageBox.Show("Der Held wurde aus \'" + expPfad + "\' importiert."
            //            + "\n\nHINWEIS: Helden können auch per Drag&Drop importiert werden.");
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgWindow errWin = new MsgWindow("Fehler beim Import", "Beim Import ist ein Fehler aufgetreten!", ex);
            //        errWin.ShowDialog();
            //    }
            //}
        }

        private void MenuItemHeldKopieren_Click(object sender, RoutedEventArgs e)
        {
            //TODO JT: SelectedHeld.Kopie();
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_listBoxHelden.SelectedItem == null)
            {
                _menuItemHeldLöschen.IsEnabled = false;
                _menuItemHeldImport.IsEnabled = false;
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