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

namespace MeisterGeister.View.Windows
{
    /// <summary>
    /// Interaktionslogik für InputIntWindow.xaml
    /// </summary>
    public partial class InputIntWindow : Window
    {
        public InputIntWindow()
        {
            InitializeComponent();
            OK_Click = false;
        }

        public string Beschreibung
        {
            get { return _textBlockBeschreibung.Text; }
            set { _textBlockBeschreibung.Text = value; }
        }

        public bool OK_Click { get; set; }

        public int? Wert
        {
            get { return _intBoxWert.Value; }
            set { _intBoxWert.Value = value; }
        }

        public int WertMin
        {
            get { return _intBoxWert.MinValue; }
            set { _intBoxWert.MinValue = value; }
        }

        public int WertMax
        {
            get { return _intBoxWert.MaxValue; }
            set { _intBoxWert.MaxValue = value; }
        }

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            OK_Click = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _intBoxWert.Focus();
        }
    }
}
