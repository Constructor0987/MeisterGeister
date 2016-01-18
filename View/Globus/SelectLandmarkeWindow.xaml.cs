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
using MeisterGeister.View.Windows;


namespace MeisterGeister.View.Globus
{
    /// <summary>
    /// Interaktionslogik für SelectLandmarkeWindow.xaml
    /// </summary>
    public partial class SelectLandmarkeWindow : Window
    {
        public string ButtonName { get; private set; }

        public SelectLandmarkeWindow(string buttonName)
            : this()
        {
            ButtonName = buttonName;
        }

        public SelectLandmarkeWindow()
        {
            InitializeComponent();

            // PlugIn DLL laden
            try
            {
                _globusControl = new DgSuche.GlobusControl(true);
                _grid.Children.Add(_globusControl);

                ListBox listBoxOrtsmarken = _globusControl.ListBoxOrtsmarken;
                {
                    listBoxOrtsmarken.SelectionChanged += Landmarke_SelectionChanged;
                    listBoxOrtsmarken.MouseDoubleClick += ListBoxOrtsmarken_MouseDoubleClick;
                }
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden eines PlugIns ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }

        private void Landmarke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetSelectedItem();
        }

        private void ListBoxOrtsmarken_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetSelectedItem();
            OnLandmarkeDoubleClicked(e.MouseDevice, e.Timestamp, e.ChangedButton);
            Close();
        }

        private void SetSelectedItem()
        {
            if (IsInitialized && IsLoaded)
            {
                if (Kalender != null)
                {
                    Kalender.SetzeStandort(SelectedItem);
                }
                OnSelectedItemChanged();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            SelectedItemChanged = null;
            base.OnClosed(e);
        }

        private DgSuche.GlobusControl _globusControl = null;

        public MeisterGeister.View.Kalender.KalenderView Kalender { get; set; }

        public DgSuche.Ortsmarke SelectedItem
        {
            get
            {
                if (_globusControl.SelectedItem != null && _globusControl.SelectedItem is DgSuche.Ortsmarke)
                    return (DgSuche.Ortsmarke)_globusControl.SelectedItem;
                return null;
            }
        }

        private void _buttonOK(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event EventHandler SelectedItemChanged;
        private void OnSelectedItemChanged()
        {
            if (SelectedItemChanged != null)
            {
                var e = new EventArgs();
                SelectedItemChanged(this, e);
            }
        }

        public event MouseButtonEventHandler LandmarkeDoubleClicked;
        private void OnLandmarkeDoubleClicked(MouseDevice mouse, int timestamp, MouseButton button)
        {
            if (LandmarkeDoubleClicked != null)
            {
                var e = new MouseButtonEventArgs(mouse, timestamp, button);
                LandmarkeDoubleClicked(this, e);
            }
        }
    }
}
