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

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für TabControl.xaml
    /// </summary>
    public partial class TabItemControl : TabItem
    {
        public string Titel
        {
            get { return _textBlockTitel.Text; }
        }

        public TabItemControl()
        {
            InitializeComponent();
        }

        public TabItemControl(Control con, string tabName, string bildName)
        {
            InitializeComponent();
            InitializeComponent2(con, tabName, bildName);
        }

        private void InitializeComponent2(Control con, string tabName, string bildName)
        {
            // Bild
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/Images/" + bildName, UriKind.Relative);
            bi.EndInit();
            _image.Source = bi;
            // Titel
            _textBlockTitel.Text = tabName;

            Content = con;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            CloseTool();
        }

        private void CloseTool()
        {
            if (Parent != null && Parent is TabControl)
            {
                ((TabControl)Parent).Items.Remove(this);
                App.SaveAll();
            }
        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                //CloseTool();
            }
        }

    }
}
