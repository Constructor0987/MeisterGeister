using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeisterGeister.ViewModel.Karte;
using DgSuche;

namespace MeisterGeister.View.Karte
{
    /// <summary>
    /// Interaktionslogik für KarteView.xaml
    /// </summary>
    public partial class KarteView : UserControl
    {
        private WrappingConverter dereGlobusToMapConverter;
        private WrappingConverter mapToDereGlobusConverter;

        public KarteView()
        {
            dereGlobusToMapConverter = new WrappingConverter();
            mapToDereGlobusConverter = new WrappingConverter();
            this.Resources.Add("DereGlobusToMapConverter", dereGlobusToMapConverter);
            this.Resources.Add("MapToDereGlobusConverter", mapToDereGlobusConverter);
            InitializeComponent();
            VM = new KarteViewModel();
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public KarteViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is KarteViewModel))
                    return null;
                return DataContext as KarteViewModel;
            }
            set {
                if (VM != null)
                    VM.PropertyChanged -= VM_PropertyChanged;
                DataContext = value;
                VM.PropertyChanged += VM_PropertyChanged;
                RefreshConverter();
            }
        }

        void RefreshConverter()
        {
            if (VM == null)
                return;
            dereGlobusToMapConverter.Converter = VM.DereGlobusToMapConverter;
            mapToDereGlobusConverter.Converter = VM.MapToDereGlobusConverter;
        }

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DereGlobusToMapConverter" || e.PropertyName == "MapToDereGlobusConverter")
                RefreshConverter();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.ZoomControlSize = MapScrollViewer.RenderSize;
                VM.Refresh();
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VM.ZoomControlSize = MapScrollViewer.RenderSize;
        }

        private void DGSuche_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var win = button != null ? new Globus.SelectLandmarkeWindow(button.Name) : new Globus.SelectLandmarkeWindow();
            win.LandmarkeDoubleClicked += DGSuche_LandmarkeDoubleClicked;
            win.Owner = Application.Current.MainWindow;
            win.Show();
        }

        private void DGSuche_LandmarkeDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var win = sender as MeisterGeister.View.Globus.SelectLandmarkeWindow;
            if (win == null)
                return;

            Ortsmarke ort = win.SelectedItem;

            switch (win.ButtonName)
            {
                case "btnRouteStarting":
                    VM.RouteStarting = ort;
                    break;
                case "btnRouteEnding":
                    VM.RouteEnding = ort;
                    break;
                default:
                    break;
            }
        }        
    }
}
