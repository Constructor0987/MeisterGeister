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


/// <summary>
/// Interaction logic for TabControlButtons.xaml
/// </summary>
/// 


namespace MeisterGeister.View.AudioPlayer
{
    public partial class TCButtons : TabItem
    {
        bool editMode = false;        

        public TCButtons()
        {
            this.MouseDown += new MouseButtonEventHandler(spnlHeader_MouseDown);
            InitializeComponent();
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

        private void spnlHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
                CloseTool();            
        }
        
        private void imgEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editMode = true;
            _tbEditText.Text = _tbText.Text;
            _tbEditText.Visibility = Visibility.Visible;
            _tbText.Visibility = Visibility.Collapsed;
            _tbEditText.Focus();
            _imgOk.Tag = _tbText.Text;
            this.Height = 40;
            _imgEdit.Visibility = Visibility.Collapsed;
            _imgOk.Visibility = Visibility.Visible;
        }

        private void imgOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editMode = false;
            _tbText.Text = _tbEditText.Text;
            _tbEditText.Visibility = Visibility.Collapsed;
            _tbText.Visibility = Visibility.Visible;
            _imgOk.Tag = _tbText.Text;
            _imgEdit.Visibility = Visibility.Visible;
            _imgOk.Visibility = Visibility.Collapsed;
            this.Height = 18;
        }

        private void TabItem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_imgEdit.Visibility == Visibility.Visible)
                _btnEditOkay.Visibility = Visibility.Collapsed;
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            _btnEditOkay.Visibility = Visibility.Visible;
        }
    }
}