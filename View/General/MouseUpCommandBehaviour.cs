using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MeisterGeister.View.General
{
    public static partial class MouseUpCommandBehaviour
    {
        public static readonly DependencyProperty MouseUpCommandProperty = DependencyProperty.RegisterAttached("MouseUpCommand", 
            typeof(ICommand), typeof(MouseUpCommandBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseUpCommandChanged)));

        private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.MouseUp += new MouseButtonEventHandler(element_MouseUp);
        }
        static void element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseUpCommand(element);

            command.Execute(e);
        }
        public static void SetMouseUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseUpCommandProperty, value);
        }
        public static ICommand GetMouseUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseUpCommandProperty);
        }
    }

}
