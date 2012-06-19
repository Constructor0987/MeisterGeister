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
    /// Interaktionslogik für InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
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

        public string Wert
        {
            get { return _textBoxWert.Text; }
            set { _textBoxWert.Text = value; }
        }

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            OK_Click = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _textBoxWert.Focus();
        }
    }
}
