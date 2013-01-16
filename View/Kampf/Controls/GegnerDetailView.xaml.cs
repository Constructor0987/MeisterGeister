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

namespace MeisterGeister.View.Kampf.Controls
{
    /// <summary>
    /// Interaktionslogik für GegnerDetailView.xaml
    /// </summary>
    public partial class GegnerDetailView : UserControl
    {
        public GegnerDetailView()
        {
            InitializeComponent();
        }

        private void ButtonCloseZonenRsControl_Click(object sender, RoutedEventArgs e)
        {
            _rsZonenRsControl.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ButtonZonenRsControl_Click(object sender, RoutedEventArgs e)
        {
            _rsZonenRsControl.Visibility = System.Windows.Visibility.Visible;
        }

        private void TextBoxAngriffName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender != null && sender is TextBox)
                (sender as TextBox).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
