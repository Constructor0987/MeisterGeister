﻿using System;
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
using VM = MeisterGeister.ViewModel.Kampf;
using MeisterGeister.View.General;

namespace MeisterGeister.View.Kampf.Controls
{
	/// <summary>
	/// Interaktionslogik für GegnerView.xaml
	/// </summary>
    public partial class GegnerView : System.Windows.Controls.UserControl
	{
		public GegnerView()
		{
			this.InitializeComponent();
#if !(DEBUG)
            _buttonExportDemo.Visibility = System.Windows.Visibility.Collapsed;
#endif
            //VM an View Registrieren
            VM = new VM.GegnerViewModel(ViewHelper.Popup, ViewHelper.Confirm, ViewHelper.ConfirmYesNoCancel, ViewHelper.ChooseFile, ViewHelper.ShowError);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.GegnerViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.GegnerViewModel))
                    return null;
                return DataContext as VM.GegnerViewModel;
            }
            set { DataContext = value; }
        }

        private void ButtonHeldNeu_Click(object sender, RoutedEventArgs e)
        {
            _buttonHeldNeu.ContextMenu.PlacementTarget = this;
            _buttonHeldNeu.ContextMenu.IsOpen = true;
        }

        private void ListBoxGegnerBase_Drop(object sender, System.Windows.DragEventArgs e)
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
                        GegnerBase importHeld = VM.ImportGegnerBase(file);
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
            if (_listBoxGegnerBase.SelectedItem == null)
            {
                _menuItemGegnerLöschen.IsEnabled = false;
                _menuItemGegnerExport.IsEnabled = false;
                _menuItemGegnerKopie.IsEnabled = false;
            }
            else
            {
                _menuItemGegnerLöschen.IsEnabled = true;
                _menuItemGegnerImport.IsEnabled = true;
                _menuItemGegnerExport.IsEnabled = true;
                _menuItemGegnerKopie.IsEnabled = true;
            }
        }

        private void ListBoxGegner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listBoxGegnerBase.SelectedItem = null;
        }

	}
}