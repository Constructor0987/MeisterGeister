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
//eigene
using MeisterGeister.ViewModel.AudioPlayer;
using VM = MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.AudioPlayer.Logic;

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für lbEditorThemeItem.xaml
    /// </summary>
    public partial class lbEditorThemeItem : ListViewItem
    {
        public bool _animateOnMouseEvent = true;
        
        public VM.lbEditorThemeItemVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.lbEditorThemeItemVM))
                    return null;
                return DataContext as VM.lbEditorThemeItemVM;
            }
            set { DataContext = value; }
        }

        public lbEditorThemeItem()
        {
            InitializeComponent();            
        }

        private void lbiEditorTheme_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.VM != null)
            {
                VM.PlayerVM.SelectedEditorThemeItem = this.VM;
                if (e.LeftButton == MouseButtonState.Pressed)
                    VM.PlayerVM.DnDZielObject = e.GetPosition(null);
            }
        }

    }
}
