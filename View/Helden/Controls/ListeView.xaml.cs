using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

//Eigene Usings
using MeisterGeister.Model;
using MeisterGeister.View.General;
using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für ViewHeldListe.xaml
    /// </summary>
    public partial class ListeView : System.Windows.Controls.UserControl
    {
        public ListeView()
        {
            InitializeComponent();

#if !(DEBUG)
            _buttonExportDemo.Visibility = System.Windows.Visibility.Collapsed;
#endif

            VM = new VM.ListeViewModel(ViewHelper.Popup, ViewHelper.Confirm, ViewHelper.ConfirmYesNoCancel, ViewHelper.ChooseFile, ViewHelper.ShowError);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.ListeViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ListeViewModel))
                {
                    return null;
                }

                return DataContext as VM.ListeViewModel;
            }

            set { DataContext = value; }
        }

        private void ButtonWithContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.ContextMenu.PlacementTarget = this;
            button.ContextMenu.IsOpen = true;
        }

        // TODO ??: MVVM konform umbauen
        private void ListBoxHelden_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                var droppedFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[];

                Held importHeld = null;
                foreach (var file in droppedFiles)
                {
                    if (!(file.EndsWith("xml") || file.EndsWith("xls") || file.EndsWith("xlsx") || file.EndsWith("xlsb")))
                    {
                        ViewHelper.Popup(file + "\n\nFalscher Dateityp!");
                        continue;
                    }
#if !DEBUG
                    try
                    {
#endif
                    Global.SetIsBusy(true, string.Format("{0} wird importiert...", file));
                    importHeld = VM.ImportHeld(file);
#if !DEBUG
                    } catch (Exception ex) {
                        ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten!", ex);
                    }
#endif
                }
                VM.SelectedHeld = importHeld;
                Global.SetIsBusy(false);
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
                _menuItemHeldLöschen.IsEnabled = !VM.IsReadOnly;
                _menuItemHeldImport.IsEnabled = true;
                _menuItemHeldExport.IsEnabled = true;
                _menuItemHeldKopie.IsEnabled = true;
            }
        }

        private void ListBoxHelden_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            _listBoxHelden.SelectedItem = null;
        }
    }
}
