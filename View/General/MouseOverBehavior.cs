using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.View.General
{
    public static partial class MouseOverBehaviour
    {
        private static readonly List<FrameworkElement> _wiredUpElements = new List<FrameworkElement>();
        private static readonly object _wiredUpElementsLock = new object();

        public static readonly DependencyProperty MouseOverProperty
            = DependencyProperty.RegisterAttached(
                "MouseOver",
                typeof(object),
                typeof(MouseOverBehaviour),
                new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    MouseOverBindingPropertyChanged));

        public static void SetMouseOver(FrameworkElement element, object value)
        {
            element.SetValue(MouseOverProperty, value);
        }

        public static void SetMouseLeave(FrameworkElement element)
        {
            element.SetValue(MouseOverProperty, null);
        }

        private static void MouseOverBindingPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element == null || _wiredUpElements.Contains(element))
            {
                return;
            }
            lock (_wiredUpElementsLock)
            {
                if (_wiredUpElements.Contains(element))
                {
                    return;
                }
                _wiredUpElements.Add(element);
                element.MouseEnter += (sender, args) =>
                {
                    var s = (FrameworkElement)sender;
                    SetMouseOver(s, s.DataContext);
                };
                element.MouseLeave += (sender, args) =>
                {
                    var s = (FrameworkElement)sender;
                    SetMouseLeave(s);
                };
            }
        }
    }
}
