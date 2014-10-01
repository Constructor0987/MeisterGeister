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

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für grdThemeButton.xaml
    /// </summary>
    public partial class grdThemeButton : UserControl
    {
        public grdThemeButton()
        {
            InitializeComponent();        
        }

        private void grdTheme_MouseEnter(object sender, MouseEventArgs e)
        {
            chkbxPlus.Visibility = Visibility.Visible;
        }

        private void grdTheme_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.Height != 42)
                chkbxPlus.Visibility = Visibility.Hidden;
        }

    }
}
