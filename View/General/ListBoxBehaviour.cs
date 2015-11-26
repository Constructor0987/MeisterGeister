using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeisterGeister.View.General
{
    public static class ListBoxBehaviour
    {
        #region DeselectOnClick

        public static bool GetDeselectOnClick(DependencyObject obj)
        {
            return (bool)obj.GetValue(DeselectOnClickProperty);
        }

        public static void SetDeselectOnClick(DependencyObject obj, bool value)
        {
            obj.SetValue(DeselectOnClickProperty, value);
        }

        // Using a DependencyProperty as the backing store for DeselectOnClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeselectOnClickProperty =
            DependencyProperty.RegisterAttached("DeselectOnClick", typeof(bool), typeof(ListBoxBehaviour), new PropertyMetadata(false, onDeselectOnClickChanged));

        private static void onDeselectOnClickChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ListBox element = (ListBox)obj;
            if ((bool)e.OldValue)
            {
                element.SelectionChanged -= ListBox_SelectionChanged;
                element.SelectionMode = GetPreviousSelectionMode(element);
                element.ClearValue(PreviousSelectionModeProperty);
            }
            if ((bool)e.NewValue)
            {
                SetPreviousSelectionMode(element, element.SelectionMode);
                element.SelectionMode = SelectionMode.Multiple;
                element.SelectionChanged += ListBox_SelectionChanged;
            }
        }

        private static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                ((ListBox)sender).SelectedItem = e.AddedItems[0];
            }
        }

        private static SelectionMode GetPreviousSelectionMode(DependencyObject obj)
        {
            return (SelectionMode)obj.GetValue(PreviousSelectionModeProperty);
        }
        private static void SetPreviousSelectionMode(DependencyObject obj, SelectionMode value)
        {
            obj.SetValue(PreviousSelectionModeProperty, value);
        }
        private static readonly DependencyProperty PreviousSelectionModeProperty =
            DependencyProperty.RegisterAttached("PreviousSelectionMode", typeof(SelectionMode), typeof(ListBoxBehaviour), new PropertyMetadata(SelectionMode.Single));



        #endregion
    }
}
