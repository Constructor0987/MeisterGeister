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

        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null)
                return null;

            if (element.GetType() == type)
                return element;

            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                    break;

            }
            return foundElement;
        }

        private void spnllbiIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.Parent.GetType() == typeof(ListBox))
            {
                double d = ((Border)((ListBox)this.Parent).Parent).ActualWidth - grd.ColumnDefinitions[0].ActualWidth - 10;

                var scrollViewer = GetDescendantByType((ListBox)this.Parent, typeof(ScrollViewer)) as ScrollViewer;

                lbText.Width = (((ScrollViewer)scrollViewer).ComputedVerticalScrollBarVisibility == Visibility.Visible) ?
                    d - btnExport.Width - btnLöschen.Width - 18 :
                    d - btnExport.Width - btnLöschen.Width;

                btnExport.Visibility = Visibility.Visible;
                btnLöschen.Visibility = Visibility.Visible;
            }
        }

        private void spnllbiIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            btnExport.Visibility = Visibility.Collapsed;
            btnLöschen.Visibility = Visibility.Collapsed;
            lbText.Width = double.NaN;
        }
    }
}
