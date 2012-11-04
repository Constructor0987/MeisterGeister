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
using System.Windows.Shapes;
using System.Diagnostics;
// Eigene Usings
using VM = MeisterGeister.ViewModel;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.View.Arena
{
    /// <summary>
    /// Interaktionslogik für ArenaWindow.xaml
    /// </summary>
    public partial class ArenaWindow : Window
    {
        private static double CONTROL_WIDTH = 200;

        public static String ICON_DIR = "/Images/Icons/General/";
        
        private Grid _mainGrid;
        private ArenaViewer _arenaViewer;
        private ArenaControlPanel _arenaControlPane;

        private ScrollViewer _arenaSV;

        public ArenaWindow(MeisterGeister.ViewModel.Kampf.Logic.Kampf kampf)
        {
            InitializeComponent();

            Debug.Listeners.Clear();
            Debug.Listeners.Add(new TextWriterTraceListener(System.Console.Out));           

            ResizeMode = System.Windows.ResizeMode.CanResize;            

            //SetIcon();
            CreateElements();
            AddElements();

            PreviewMouseWheel += onPreviewMouseWheel;
            //MouseWheel += onMouseWheel;

            VM.Arena.Arena arena = new VM.Arena.Arena(25, 25);
            
            _arenaViewer.Arena = arena;

            if (kampf != null) {
                _arenaViewer.Arena.Populate(kampf);
                _arenaControlPane.OnArenaPopulated(kampf);
            }

            _arenaViewer.DrawArena();

           
        }

        private void onPreviewMouseWheel(object sender, MouseWheelEventArgs e) {


            _arenaControlPane.OnMouseWheelZoom(e.Delta);

            e.Handled = true;
        }

        //private void onMouseWheel(object sender, MouseEventArgs e) {
        //    Debug.WriteLine("Wheel!");
        //}

        //private void SetIcon() {

        //    BitmapImage myBitmapImage = new BitmapImage();

        //    myBitmapImage.BeginInit();
        //    myBitmapImage.UriSource = new Uri(@ArenaWindow.ICON_DIR + "dsa_icon.png", UriKind.Relative);
        //   // myBitmapImage.DecodePixelWidth = (int)_portrait.Width;
        //   // myBitmapImage.DecodePixelHeight = (int)_portrait.Height;
        //    myBitmapImage.EndInit();
        //   // _portrait.Source = myBitmapImage;

        //    Icon = myBitmapImage;
        //}

        private void CreateElements() {

            _mainGrid = new Grid();
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef2.Width = new GridLength(CONTROL_WIDTH);
            _mainGrid.ColumnDefinitions.Add(colDef1);
            _mainGrid.ColumnDefinitions.Add(colDef2);
            
            RowDefinition rowDef1 = new RowDefinition();
            _mainGrid.RowDefinitions.Add(rowDef1);

            _mainGrid.Background = new SolidColorBrush(Colors.White);

            _arenaSV = new ScrollViewer();

            _arenaViewer = new ArenaViewer();
            _arenaViewer.CurrentScrollViewer = _arenaSV;
            
            _arenaControlPane = new ArenaControlPanel(CONTROL_WIDTH, Double.NaN, _arenaViewer, this);
            
        }

        private void AddElements() {
            this.Content = _mainGrid;
            
            _arenaSV.Content = _arenaViewer;
            _arenaSV.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            _arenaSV.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            _arenaSV.Background = new SolidColorBrush(Colors.Black);

            ScrollViewer controlSV = new ScrollViewer();
            controlSV.Content = _arenaControlPane;
            controlSV.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;


            Grid.SetRow(_arenaSV, 0);
            Grid.SetColumn(_arenaSV, 0);

            Grid.SetRow(controlSV, 0);
            Grid.SetColumn(controlSV, 1);

            _mainGrid.Children.Add(_arenaSV);
            _mainGrid.Children.Add(controlSV);
        }
    }
}
