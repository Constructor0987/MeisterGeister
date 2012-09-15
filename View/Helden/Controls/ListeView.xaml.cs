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
using VM = MeisterGeister.ViewModel.Helden;
using MeisterGeister.View.General;

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
            this.DataContext = new VM.ListeViewModel(ViewHelper.Popup, ViewHelper.Confirm, ViewHelper.ConfirmYesNoCancel, ViewHelper.ChooseFile, ViewHelper.ShowError);
        }

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
                        ViewHelper.Popup(file + "\n\nFalscher Dateityp!");
                        continue;
                    }
                    try
                    {
                        Held importHeld = (this.DataContext as VM.ListeViewModel).ImportHeld(file);
                    }
                    catch (Exception ex)
                    {
                        ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten!", ex);
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