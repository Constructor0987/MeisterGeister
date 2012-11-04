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

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für SelectImageDialog.xaml
    /// </summary>
    public partial class SelectImageDialog : Window
    {
        public SelectImageDialog()
        {
            InitializeComponent();
            SelectedPath = null;
            
        }

        public string SelectedPath { get; set; }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
