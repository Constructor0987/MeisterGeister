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
    /// Interaktionslogik für lbEditorItem.xaml
    /// </summary>
    public partial class lbEditorItem : ListViewItem
    {
        public bool _animateOnMouseEvent = true;


        public VM.lbEditorItemVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.lbEditorItemVM))
                    return null;
                return DataContext as VM.lbEditorItemVM;
            }
            set { DataContext = value; }
        }

        public lbEditorItem()
        {
            InitializeComponent();
            
        }

        private void lbiEditorPlaylist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.VM != null)
            {
                VM.PlayerVM.SelectedEditorItem = this.VM;
                if (VM.PlayerVM.rbEditorEditPlaylist) return;
                if (e.LeftButton == MouseButtonState.Pressed)
                    VM.PlayerVM.DnDZielObject = e.GetPosition(null);
            }
        }

        private P FindVisualParent<P>(DependencyObject child)
            where P : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            P parent = parentObject as P;
            if (parent != null)
                return parent;

            return FindVisualParent<P>(parentObject);
        }

        private void lbitemEditor_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("meineAudioZeile"))
            {
                VM.PlayerVM.DropZielPlaylist = (Audio_Playlist)((StackPanel)sender).Tag;
                AudioZeile aZeile = e.Data.GetData("meineAudioZeile") as AudioZeile;
            }
            else
            {
                if (e.Data.GetData("lbiPlaylistVM") is lbEditorItemVM)
                {
                    lbEditorItemVM source = e.Data.GetData("lbiPlaylistVM") as lbEditorItemVM;
                    lbEditorItem lbi = FindVisualParent<lbEditorItem>(((DependencyObject)e.OriginalSource));
                    lbEditorItemVM target = lbi.DataContext as lbEditorItemVM; 

                    int sourceIndex = VM.PlayerVM.FilteredEditorListBoxItemListe.IndexOf(source);
                    int targetIndex = VM.PlayerVM.FilteredEditorListBoxItemListe.IndexOf(target);

                    VM.PlayerVM.MoveLbEditorItem(source.APlaylist, targetIndex - sourceIndex);
                    VM.PlayerVM.FilteredEditorListBoxItemListe = VM.PlayerVM.FilteredEditorListBoxItemListe.OrderBy(t => t.APlaylist.Reihenfolge).ToList();
                }
            }
        }

        private void spnlSubObject_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VM != null) VM.MouseOnSubObject = true;
        }

        private void spnlSubObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (VM != null) VM.MouseOnSubObject = false;
        }
       
    }
}
