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
//Eigene Usings
using VM = MeisterGeister.ViewModel.Inventar;
//Weitere Usings
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace MeisterGeister.View.Helden.Controls
{
    public partial class InventarView : UserControl
    {

        public InventarView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.InventarViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.InventarViewModel))
                    return null;
                return DataContext as VM.InventarViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }

        #region Events
        #region --UI
        private void brdKlicked(object sender, RoutedEventArgs e) {
            switch ((sender as Border).Name) {
                case "borderAll":
                    VM.SelectedFilterIndex = 0;
                    break;
                case "borderNahkampf":
                    VM.SelectedFilterIndex = 1;
                    break;
                case "borderFernkampf":
                    VM.SelectedFilterIndex = 2;
                    break;
                case "borderSchild":
                    VM.SelectedFilterIndex = 3;
                    break;
                case "borderRuestung":
                    VM.SelectedFilterIndex = 4;
                    break;
                case "borderSonstiges":
                    VM.SelectedFilterIndex = 5;
                    break;
                default:
                    break;
            }
        }

        //INIT
        private void InventarLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
             VM = new VM.InventarViewModel();
			            try
            {
                VM.LoadDaten();
            }
            catch (Exception) { }
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;

            Ruestung.Visibility = Visibility.Hidden;
            Uebersicht.Visibility = Visibility.Visible;
        }

        private void OpenRuestung(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	Storyboard uebersicht = (Storyboard)TryFindResource("CloseUebersicht");
            if (uebersicht != null) {
                

                uebersicht.Completed += (obj, args) => {
                    Uebersicht.Visibility = Visibility.Hidden;
                    Ruestung.Visibility = Visibility.Visible;

                    Storyboard ruestung = (Storyboard)TryFindResource("OpenRuestung");
                    if (ruestung != null)
                        ruestung.Begin(this);
                };
                uebersicht.Begin(this);                
            }

			
        }

        private void CloseRuestung(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	Storyboard uebersicht = (Storyboard)TryFindResource("CloseRuestung");
            if (uebersicht != null) {                
                uebersicht.Completed += (obj, args) => {
                    Ruestung.Visibility = Visibility.Hidden;
                    Uebersicht.Visibility = Visibility.Visible;

                    Storyboard ruestung = (Storyboard)TryFindResource("OpenUebersicht");
                    if (ruestung != null)
                        ruestung.Begin(this);
                };
                uebersicht.Begin(this);
            }
        }
        #endregion
        #endregion
    }
}