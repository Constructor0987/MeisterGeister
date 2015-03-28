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
    /// Interaktionslogik für ListboxItemBtn.xaml
    /// </summary>
    public partial class ListboxItemBtn : ListBoxItem
    {
        public ListboxItemBtn()
        {
            InitializeComponent();
        }
        
        private void btnStdPfad_Click(object sender, RoutedEventArgs e)
        {
            ((ListBox)lbi.Parent).SelectedItem = lbi;
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            
            if (lblStdPfad.Content != null && Directory.Exists(lblStdPfad.Content.ToString()))
                dialog.SelectedPath = lblStdPfad.Content.ToString();            
            dialog.ShowDialog();
            if (dialog.SelectedPath != "")
                btnStdPfad.Tag = dialog.SelectedPath;

            if (lbi == ((ListBox)lbi.Parent).Items[((ListBox)lbi.Parent).Items.Count-1])
            {
                ListboxItemBtn lbiBtn = new ListboxItemBtn();
                ((ListBox)lbi.Parent).Items.Add(lbiBtn);
            }
        }
    }
}
