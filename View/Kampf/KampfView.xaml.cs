﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeisterGeister.Daten;
using System.Collections.Generic;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Settings;
using MeisterGeister.View.Arena;
using MeisterGeister.ViewModel.Kampf.Logic;
using VM = MeisterGeister.ViewModel.Kampf;

namespace MeisterGeister.View.Kampf
{
    /// <summary>
    /// Interaktionslogik für KampfView.xaml
    /// </summary>
    public partial class KampfView : UserControl
    {
        public KampfView()
        {
            InitializeComponent();
            // TODO ??: Umstellen auf neues Kampf-Model
            //_listBoxAktionen.ItemsSource = _kampf.AktionenListe;

            // TODO ??: Umstellen auf neues Kampf-Model
            //_kampf.NächsterKämpferRollover += NächsterKämpferRollover_EventHandler;

            // TODO ??: Umstellen auf neues Kampf-Model
            //_comboBoxTrefferzone.ItemsSource = Trefferzone.TrefferzonenListe();
            //_comboBoxTrefferzone.SelectedIndex = 0;
            VM = new VM.KampfViewModel(View.General.ViewHelper.ShowGegnerView, View.General.ViewHelper.Confirm);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.KampfViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.KampfViewModel))
                    return null;
                return DataContext as VM.KampfViewModel;
            }
            set { DataContext = value; }
        }

        private void ButtonNeueKampfrunde_Click(object sender, RoutedEventArgs e)
        {
            NeueKampfrunde();
        }

        private void NeueKampfrunde()
        {
            //_kampf.NeueKampfrunde();
            //if (_kampf.Kampfrunde > 0)
            //    _buttonNeueKR.Content = "Neue KR";
            //else
                //_buttonNeueKR.Content = "Starten";
            //SetKampfrunde();
        }

        private void ButtonNeuerKampf_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Soll ein neuer Kampf gestartet werden?", "Neuer Kampf", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                //_kampf.KampfEnde();
                _buttonNeueKR.Content = "Starten";
                //_listBoxKämpfer.DataContext = _kampf.Kämpfer;
                // TODO ??: Umstellen auf neues Kampf-Model
                //_listBoxAktionen.ItemsSource = _kampf.AktionenListe;
                //SetKampfrunde();
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (_treeInitiative.SelectedItem == null)
            {
                _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Collapsed;
                _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                _menuItemKämpferFarbmarkierung.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferEntfernen.Visibility = System.Windows.Visibility.Visible;
                _menuItemKämpferAktuell.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void NächsterKämpferRollover_EventHandler(object sender, EventArgs e)
        {
            var res = MessageBoxResult.Yes;
            if (!Einstellungen.FrageNeueKampfrundeAbstellen)
                res = MessageBox.Show("Soll eine neue KR begonnen werden?\n(Diese Frage kann unter Einstellungen abgeschaltet werden.)",
                    "Ende der Kämpfer-Liste", MessageBoxButton.YesNoCancel);
            switch (res)
            {
                case MessageBoxResult.Cancel:
                    // TODO ??: Umstellen auf neues Kampf-Model    
                //_kampf.VorherigerKämpfer();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    NeueKampfrunde();
                    break;
                default:
                    break;
            }
        }

        private void MenuItemKämpferFarbmarkierung_Click(object sender, RoutedEventArgs e)
        {
            // TODO ??: Umstellen auf neues Kampf-Model
            //_selectedKämpfer.Farbmarkierung = ((MenuItem)sender).Background;
        }

        private void ButtonArena_Click(object sender, RoutedEventArgs e)
        {
            // TODO ??: Umstellen auf neues Kampf-Model
            ViewModel.Kampf.Logic.Kampf k = VM.Kampf;
            ArenaWindow arenaWindow = new ArenaWindow(_cbArena.IsChecked == true ? k : null);
            arenaWindow.Width = 1200;
            arenaWindow.Height = 800;
            arenaWindow.Show();
        }

        private void ButtonSpielerInfo_Click(object sender, RoutedEventArgs e)
        {
            ShowSpielerInfo();
        }

        private void ButtonSpielerInfoClose_Click(object sender, RoutedEventArgs e)
        {
            MainView.CloseSpielerFenster();
        }

        private void ShowSpielerInfo()
        {
            KampfInfoView infoView = new KampfInfoView(VM.Kampf);
            MainView.ShowSpielerInfo(infoView);
        }

        private void InitiativeListe_TreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            var parent = ItemsControl.ItemsControlFromItemContainer(e.OriginalSource as TreeViewItem);
            VM.KämpferSelected = parent is TreeView;
        }

    }

    public delegate void ProbeWürfelnEventHandler(string talentname);
}
