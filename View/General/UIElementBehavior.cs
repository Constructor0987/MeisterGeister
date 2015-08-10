using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.View.General
{
    public static class UIElementBehavior
    {
        public static ViewModelBase GetViewModel(UIElement obj)
        {
            return (ViewModelBase)obj.GetValue(ViewModelProperty);
        }
        public static void SetViewModel(UIElement obj, ViewModelBase value)
        {
            obj.SetValue(ViewModelProperty, value);
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.RegisterAttached("ViewModel", typeof(ViewModelBase), typeof(UIElementBehavior), new UIPropertyMetadata(null, onViewModelChanged));


        private static void onViewModelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //Wenn das Behavior auf einem UIElement aktiviert wurde müssen wir mitkriegen wann sich die Sichtbarkeit ändert
            UIElement element = (UIElement)obj;
            if(e.OldValue != null)
            {
                element.IsVisibleChanged -= element_IsVisibleChanged;
            }
            if(e.NewValue != null)
            {
                element.IsVisibleChanged += element_IsVisibleChanged;
            }
        }

        private static void element_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Je nachdem ob das UIElement sichtbar ist oder nicht soll das ViewModel die Events an-/abmelden
            ViewModelBase vm = GetViewModel((UIElement)sender);
            if ((bool)e.NewValue) vm.RegisterEvents();
            else vm.UnregisterEvents();
        }
    }
}
