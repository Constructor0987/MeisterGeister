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

namespace MeisterGeister.View.Helden.Controls {
    public partial class InventarView : UserControl {

        #region Public

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.InventarViewModel VM {
            get {
                if (DataContext == null || !(DataContext is VM.InventarViewModel))
                    return null;
                return DataContext as VM.InventarViewModel;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public InventarView() {
            InitializeComponent();

            Ruestung.Visibility = Visibility.Hidden;
            Uebersicht.Visibility = Visibility.Visible;
        }

        #endregion

        #region Events

        #region --UI

        /// <summary>
        /// Welches Border?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Loaded-Event: init VM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InventarLoaded(object sender, System.Windows.RoutedEventArgs e) {
            if (VM == null) {
                VM = new VM.InventarViewModel();
                VM.ListenToChangeEvents = IsVisible;
            } else {
                VM.ListenToChangeEvents = IsVisible;
            }

            VM.LoadDaten();           
        }

        /// <summary>
        /// BtnKlick: StartAnimation Open Ruestung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenRuestung(object sender, System.Windows.Input.MouseButtonEventArgs e) {
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

        /// <summary>
        /// BtnKlick: StartAnimation Open Uebersicht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseRuestung(object sender, System.Windows.Input.MouseButtonEventArgs e) {
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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }

        #endregion

        #endregion
    }
}