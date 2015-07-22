using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeisterGeister.View.General
{
    public static class TrackBehavior
    {
        public static readonly DependencyProperty TrackOpenLocationProperty = DependencyProperty.RegisterAttached("TrackOpenLocation", typeof(bool), typeof(TrackBehavior), new UIPropertyMetadata(false, OnTrackOpenLocationChanged));

        public static bool GetTrackOpenLocation(ContextMenu item)
        {
            return (bool)item.GetValue(TrackOpenLocationProperty);
        }

        public static void SetTrackOpenLocation(ContextMenu item, bool value)
        {
            item.SetValue(TrackOpenLocationProperty, value);
        }

        public static readonly DependencyProperty OpenLocationProperty = DependencyProperty.RegisterAttached("OpenLocation", typeof(Point), typeof(TrackBehavior), new UIPropertyMetadata(new Point()));

        public static Point GetOpenLocation(ContextMenu item)
        {
            return (Point)item.GetValue(OpenLocationProperty);
        }

        public static void SetOpenLocation(ContextMenu item, Point value)
        {
            item.SetValue(OpenLocationProperty, value);
        }

        static void OnTrackOpenLocationChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var menu = dependencyObject as ContextMenu;
            if (menu == null)
            {
                return;
            }

            if (!(e.NewValue is bool))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                menu.Opened += menu_Opened;

            }
            else
            {
                menu.Opened -= menu_Opened;
            }
        }

        static void menu_Opened(object sender, RoutedEventArgs e)
        {
            if (!ReferenceEquals(sender, e.OriginalSource))
            {
                return;
            }

            var menu = e.OriginalSource as ContextMenu;
            if (menu != null)
            {
                SetOpenLocation(menu, Mouse.GetPosition(menu.PlacementTarget));
            }
        }
    }
}
