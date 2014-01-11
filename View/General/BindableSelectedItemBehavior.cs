using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MeisterGeister.View.General
{
    public class BindableSelectedItemBehavior : Behavior<TreeView>
    {
        #region SelectedItem Property

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(BindableSelectedItemBehavior), new UIPropertyMetadata(null, OnSelectedItemChanged));

        //private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    var item = e.NewValue as TreeViewItem;
        //    if (item != null)
        //    {
        //        item.SetValue(TreeViewItem.IsSelectedProperty, true);
        //    }
        //}

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as BindableSelectedItemBehavior;

            if (behavior == null) return;

            var tree = behavior.AssociatedObject;

            if (tree == null) return;

            if (e.NewValue == null)
                foreach (var item in tree.Items.OfType<TreeViewItem>())
                    item.SetValue(TreeViewItem.IsSelectedProperty, false);

            var treeViewItem = e.NewValue as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.SetValue(TreeViewItem.IsSelectedProperty, true);
            }
            else
            {
                var itemsHostProperty = tree.GetType().GetProperty("ItemsHost", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (itemsHostProperty == null) return;
                var itemsHost = itemsHostProperty.GetValue(tree, null) as Panel;
                if (itemsHost == null) return;
                foreach (var item in itemsHost.Children.OfType<TreeViewItem>())
                    if (WalkTreeViewItem(item, e.NewValue, behavior.PreferNestedItems)) break;
            }
        }
        #endregion

        public static bool WalkTreeViewItem(TreeViewItem treeViewItem, object selectedValue, bool preferNestedItems = false)
        {
            if (treeViewItem == null)
                return false;

            if (preferNestedItems)
                for(int i=0; i<treeViewItem.Items.Count; i++)
                    if (WalkTreeViewItem((TreeViewItem)treeViewItem.ItemContainerGenerator.ContainerFromIndex(i), selectedValue, preferNestedItems)) return true;

            if (treeViewItem.DataContext == selectedValue)
            {
                treeViewItem.SetValue(TreeViewItem.IsSelectedProperty, true);

                var parent = VisualTreeHelper.GetParent(treeViewItem);
                while (parent != null && !(parent is Window))
                    parent = VisualTreeHelper.GetParent(parent);
                if(parent != null && parent is Window && (parent as Window).IsActive)
                    treeViewItem.Focus();
                return true;
            }

            if(!preferNestedItems)
                for (int i = 0; i < treeViewItem.Items.Count; i++)
                    if (WalkTreeViewItem((TreeViewItem)treeViewItem.ItemContainerGenerator.ContainerFromIndex(i), selectedValue, preferNestedItems)) return true;

            return false;
        }

        #region PreferNestedItems
        public bool PreferNestedItems
        {
            get { return (bool)GetValue(PreferNestedItemsProperty); }
            set { SetValue(PreferNestedItemsProperty, value); }
        }

        public static readonly DependencyProperty PreferNestedItemsProperty =
            DependencyProperty.Register("PreferNestedItems", typeof(bool), typeof(BindableSelectedItemBehavior), new UIPropertyMetadata(false, OnPreferNestedItemsChanged));

        private static void OnPreferNestedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedItem = e.NewValue;
        }
    }
}
