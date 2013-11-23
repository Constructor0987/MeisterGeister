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
using System.IO;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für ListboxItemIcon.xaml
    /// </summary>
    public partial class ListboxItemIcon : ListBoxItem
    {
        public ListboxItemIcon()
        {
            InitializeComponent();
        }

        private void spnllbiIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            double d = ((Border)((ListBox)this.Parent).Parent).ActualWidth - grd.ColumnDefinitions[0].ActualWidth - 10;
            double anzVis = ((ListBox)this.Parent).Items.Count * this.ActualHeight;
            if (lbText.ActualWidth != d - btnExport.Width - btnLöschen.Width)
                lbText.Width = d - btnExport.Width - btnLöschen.Width -
                 ((((ListBox)this.Parent).ActualHeight < anzVis)? 18: 0);
            btnExport.Visibility = Visibility.Visible;
            btnLöschen.Visibility = Visibility.Visible;
        }

        private void spnllbiIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            btnExport.Visibility = Visibility.Collapsed;
            btnLöschen.Visibility = Visibility.Collapsed;
            lbText.Width = double.NaN;
        }
    }
}
